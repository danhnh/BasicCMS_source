/*jslint unparam: true, white: true, browser: true, devel: true */
/*global bettercms */
bettercms.define('bcms.austar', ['bcms.jquery', 'bcms', 'bcms.modal', 'bcms.siteSettings', 'bcms.dynamicContent', 'bcms.ko.extenders', 'bcms.ko.grid', 'bcms.grid', 'bcms.security', 'bcms.antiXss'],
    function ($, bcms, modal, siteSettings, dynamicContent, ko, kogrid, grid, security, antiXss) {
        'use strict';
        var university = {},
            selectors = {
            	siteSettingsParentRow: 'tr:first',
				siteSettingsRow: 'tr',
            	siteSettingsRowCells: 'td',
            	searchField: '.bcms-search-query',
            	searchButton: '#bcms-sitemaps-search-btn',
            	siteSettingsHistoryButton: '.bcms-grid-item-history-button',
            	siteSettingsEditButton: '.bcms-grid-item-edit-button',
            	siteSettingsCreateButton: '#bcms-create-sitemap-button',
            	siteSettingsDeleteButton: '.bcms-grid-item-delete-button',
            	siteSettingsTitleCell: '.bcms-row-title',

            	downloadSubscribersInCsv: '#download-subscribers-in-csv',
            	siteSettingsButtonOpener: ".bcms-btn-opener",
            	siteSettingsButtonHolder: ".bcms-btn-opener-holder",
            	siteSettingsSitemapsForm: "#bcms-sitemaps-form"
            },
            links = {
                loadSiteSettingsUniversitiesUrl: null,
                loadUniversitiesUrl: null,
                saveUniversityUrl: null,
                deleteUniversityUrl: null
            },
            globalization = {
            	creatorDialogTitle: null,
            	editorDialogTitle: null
            };

        university.links = links;
        university.globalization = globalization;
        university.selectors = selectors;
        university.universitiesListViewModel = null;

        var UniversitiesListViewModel = (function (_super) {
        	bcms.extendsClass(UniversitiesListViewModel, _super);
        	function UniversitiesListViewModel(container, items, gridOptions) {
        		_super.call(this, container, links.loadUniversitiesUrl, items, gridOptions);
        	}

        	UniversitiesListViewModel.prototype.createItem = function (item) {
        		var newItem = new UniversityViewModel(this, item);
        		return newItem;
        	};
        	return UniversitiesListViewModel;
        })(kogrid.ListViewModel);

        var UniversityViewModel = (function (_super) {
        	bcms.extendsClass(UniversityViewModel, _super);
        	function UniversityViewModel(parent, item) {
                _super.call(this, parent, item);
                var self = this;
                self.name = ko.observable().extend({ required: "", name: "", maxLength: { maxLength: 200 } });
                self.registerFields(self.name);
                self.name(item.Name);
                self.shortname = ko.observable().extend({ required: "", shortname: "", maxLength: { maxLength: 200 } });
                self.registerFields(self.shortname);
                self.shortname(item.ShortName);

                self.streetaddress = ko.observable().extend({ required: "", streetaddress: "", maxLength: { maxLength: 200 } });
                self.registerFields(self.streetaddress);
                self.streetaddress(item.StreetAddress);
                self.postcode = ko.observable().extend({ required: "", postcode: "", maxLength: { maxLength: 200 } });
                self.registerFields(self.postcode);
                self.postcode(item.PostCode);
                self.websiteurl = ko.observable().extend({ required: "", websiteurl: "", maxLength: { maxLength: 200 } });
                self.registerFields(self.websiteurl);
                self.websiteurl(item.WebsiteUrl);
        	}
        	UniversityViewModel.prototype.getDeleteConfirmationMessage = function () {
                return $.format("Do you want to delete university {0}", this.name());
            };
        	UniversityViewModel.prototype.getSaveParams = function () {
                var params = _super.prototype.getSaveParams.call(this);
                params.Name = this.email();
                params.ShortName = this.shortname();
                params.StreetAddress = this.streetaddress();
                params.PostCode = this.postcode();
                params.WebsiteUrl = this.websiteurl();
                return params;
        	};
        	UniversityViewModel.prototype.getRowId = function () {
        		if (!this.rowId) {
        			this.rowId = 'bcms-university-row-' + this.id();
        		}
        		return this.rowId;
        	};
        	return UniversityViewModel;
        })(kogrid.ItemViewModel);


        university.loadSiteSettingsUniversities = function () {
        	dynamicContent.bindSiteSettings(siteSettings, links.loadSiteSettingsUniversitiesUrl, {
        		contentAvailable: university.initializeSiteSettingsUniversities
        	});
        };

        university.initializeSiteSettingsUniversities = function (isSearchResult) {
        	var dialog = siteSettings.getModalDialog(),
                container = dialog.container,
                form = container.find(selectors.siteSettingsSitemapsForm);

        	grid.bindGridForm(form, function (data) {
        		siteSettings.setContent(data);
        		university.initializeSiteSettingsUniversities();
        	});

        	form.on('submit', function (event) {
        		bcms.stopEventPropagation(event);
        		searchUniversities(form);
        		return false;
        	});

        	bcms.preventInputFromSubmittingForm(form.find(selectors.searchField), {
        		preventedEnter: function () {
        			searchUniversities(form);
        		}
        	});

        	form.find(selectors.searchButton).on('click', function (event) {
        		var parent = $(this).parent();
        		if (!parent.hasClass('bcms-active-search')) {
        			form.find(selectors.searchField).prop('disabled', false);
        			parent.addClass('bcms-active-search');
        			form.find(selectors.searchField).focus();
        		} else {
        			form.find(selectors.searchField).prop('disabled', true);
        			parent.removeClass('bcms-active-search');
        			form.find(selectors.searchField).val('');
        		}
        	});

        	if (isSearchResult === true) {
        		form.find(selectors.searchButton).parent().addClass('bcms-active-search');
        	} else {
        		form.find(selectors.searchField).prop('disabled', true);
        	}

        	initializeListItems(container);

        	grid.focusSearchInput(container.find(selectors.searchField), true);
        /*	var container = siteSettings.getMainContainer(),
                data = (json.Success == true) ? json.Data : {},
                holder = container.find(selectors.siteSettingsButtonHolder);

        	var viewModel = new UniversitiesListViewModel(container, data.Items, data.GridOptions);
        	viewModel.deleteUrl = links.deleteUniversityUrl;
        	viewModel.saveUrl = links.saveUniversityUrl;
        	ko.cleanNode(container.get(0));
        	ko.applyBindings(viewModel, container.get(0));*/

        	//$(container.find(selectors.downloadSubscribersInCsv)).on('click', function () {
        	//	//window.location.href = links.downoadCsvUrl;
        	//});

        	//container.find(selectors.siteSettingsButtonOpener).on('click', function (event) {
        	//	bcms.stopEventPropagation(event);
        	//	if (!holder.hasClass('bcms-opened')) {
        	//		holder.addClass('bcms-opened');
        	//	} else {
        	//		holder.removeClass('bcms-opened');
        	//	}
        	//});

        	//bcms.on(bcms.events.bodyClick, function (event) {
        	//	if (holder.hasClass('bcms-opened')) {
        	//		holder.removeClass('bcms-opened');
        	//	}
        	//});
        }
        university.loadAddNodeDialog = function (id, onClose, dialogTitle) {
        	var addUniversityController = new AddUniversityController();
        	modal.open({
        		title: dialogTitle || globalization.sitemapEditorDialogTitle,
        		onLoad: function (dialog) {
        			dynamicContent.setContentFromUrl(dialog, $.format(links.sitemapEditDialogUrl, id), {
        				done: function (content) {
        					addUniversityController.initialize(content, dialog);
        				}
        			});
        		},
        		onAccept: function (dialog) {
        			var canContinue = forms.valid(dialog.container.find(selectors.sitemapForm));

        			if (canContinue) {
        				addUniversityController.save(function (json) {
        					if (json.Success) {
        						dialog.close();
        						if (onClose && $.isFunction(onClose)) {
        							onClose(json);
        						}
        						messages.refreshBox(selectors.siteSettingsSitemapsForm, json);
        					} else {
        						sitemap.showMessage(json);
        					}
        				});
        			}
        			return false;
        		}
        	});
        };
        function editSitemap(self, container) {
        	var id = self.data('id');

        	university.loadAddNodeDialog(id, function (data) {
        		if (data.Data != null) {
        			var row = self.parents(selectors.siteSettingsParentRow),
                        cell = row.find(selectors.siteSettingsTitleCell);
        			cell.html(antiXss.encodeHtml(data.Data.Name));
        			row.find(selectors.siteSettingsDeleteButton).data('version', data.Data.Version);
        		}
        	}, globalization.editorDialogTitle);
        };
        function searchUniversities(form) {
        	grid.submitGridForm(form, function (htmlContent) {
        		siteSettings.setContent(htmlContent);
        		university.initializeSiteSettingsUniversities(true);
        	});
        };
        function initializeListItems(container, masterContainer) {
        	container.find(selectors.siteSettingsCreateButton).on('click', function (event) {
        		bcms.stopEventPropagation(event);
        		addSitemap(masterContainer || container);
        	});

        	container.find(selectors.siteSettingsRowCells).on('click', function (event) {
        		bcms.stopEventPropagation(event);
        		var editButton = $(this).parents(selectors.siteSettingsRow).find(selectors.siteSettingsEditButton);
        		if (editButton.length > 0) {
        			editSitemap(editButton, masterContainer || container);
        		}
        	});

        	container.find(selectors.siteSettingsHistoryButton).on('click', function (event) {
        		bcms.stopEventPropagation(event);
        		viewSitemapHistory($(this), masterContainer || container);
        	});

        	container.find(selectors.siteSettingsDeleteButton).on('click', function (event) {
        		bcms.stopEventPropagation(event);
        		deleteSitemap($(this), masterContainer || container);
        	});
        };
        
        function AddUniversityController() {
        	var self = this;
        	self.container = null;
        	self.pageLinksModel = null;

        	self.initialize = function (content, dialog) {

        	};
        	self.save = function (onDoneCallback) {
        		//if (sitemap.activeMapModel) {
        		//	sitemap.activeMapModel.save(onDoneCallback);
        		//}
        	};
        }

        university.init = function () {
        	bcms.logger.info('Initializing bcms.austar module.');
        };

        bcms.registerInit(university.init);
        return university;
    });
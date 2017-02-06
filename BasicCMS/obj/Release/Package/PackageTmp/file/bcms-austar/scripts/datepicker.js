var datepicker = {},
        links = {
        	calendarImageUrl: "/file/bcms-root/styles/images/calendar.svg"
        },
        globalization = {
        	dateFormat: "mm/dd/yy",
        	currentCulture: null
        };

// Assign objects to module.
datepicker.links = links;
datepicker.globalization = globalization;

datepicker.isDateValid = function (value) {
	var ok = true;
	try {
		$.datepicker.parseDate(datepicker.globalization.dateFormat, value);
	} catch (err) {
		ok = false;
	}
	return ok;
};

datepicker.parseDate = function (value) {
	try {
		return $.datepicker.parseDate(datepicker.globalization.dateFormat, value);
	} catch (err) {
		// Ignore errors.
	}
	return null;
};

datepicker.init = function () {

	$.validator.addMethod("jqdatevalidation", function (value, element, params) {
		if (element.value) {
			return datepicker.isDateValid(element.value);
		}

		return true;
	},
		function (params) {
			return params.message;
		});

	$.validator.unobtrusive.adapters.add("datevalidation", [], function (options) {
		options.rules["jqdatevalidation"] = { message: options.message };
	});

	$.validator.addMethod('date',
		function (value, element) {
			if (this.optional(element)) {
				return true;
			}
			return datepicker.isDateValid(value);
		});

	$.fn.initializeDatepicker = function (tooltipTitle, opts) {
		var options = $.extend({
			showOn: 'button',
			buttonImage: datepicker.links.calendarImageUrl,
			buttonImageOnly: true,
			dateFormat: datepicker.globalization.dateFormat
		}, opts);

		$(this).datepicker(options);
		if (tooltipTitle == null) {
			tooltipTitle = "";
		}
		$(this).datepicker("option", "buttonText", tooltipTitle);
	};
};
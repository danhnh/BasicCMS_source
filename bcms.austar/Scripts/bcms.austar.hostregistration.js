/*jslint unparam: true, white: true, browser: true, devel: true */
/*global bettercms */
bettercms.define('bcms.austar.hostregistration', ['bcms.jquery', 'bcms', 'bcms.processor', 'bcms.jquery.select2', 'bcms.datepicker', 'bcms.messages'],
function ($, bcms, processor, select2, datepicker, messages) {
	'use strict';
	var hostregistration = {},
		autocomplete = null,
		componentForm = {
			street_number: 'short_name',
			route: 'long_name',
			locality: 'long_name',
			administrative_area_level_1: 'short_name',
			country: 'long_name',
			postal_code: 'short_name'
		};


	hostregistration.init = function () {
		bcms.logger.info('Initializing Application Form.');
		$("#user_dob").initializeDatepicker("");
		$("#user_gender").select2({ minimumResultsForSearch: -1 });
		$("#user_email").blur(function () {
			if (!$(this).hasClass("bcms-input-validation-error")) {
				$.post('/bcms-austar/registration/checkemail', { id: $(this).val() }, function (d) {
					if (d.id != undefined) {
						if (!d.valid)
							$('#user_email_check').show();
						else
							$('#user_email_check').hide();
					}
				});
			}
		});
		autocomplete = new google.maps.places.Autocomplete($("#user_streetAddress")[0], { types: ['geocode'] });
		autocomplete.addListener('place_changed', function () {
			var place = autocomplete.getPlace();
			var $addr = $("#address_group");
			for (var i = 0; i < place.address_components.length; i++) {
				var addressType = place.address_components[i].types[0];
				if (componentForm[addressType]) {
					var val = place.address_components[i][componentForm[addressType]];
					var $inp = $addr.find("#address_" + addressType).val(val);
				}
			}
			$("#user_streetAddress").val(place.formatted_address);
			$addr.show();
		});
		$("#registrationForm").submit(function (e) {
			e.preventDefault();
			var $this = $(this);
			$.post($this.attr("action"), $this.serialize()).done(function (data) {
				messages.refreshBox($this, data);
				if (data.Success) {
					
				}
			});
		});
	};

	bcms.registerInit(hostregistration.init);
	return hostregistration;
});
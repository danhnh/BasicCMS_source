function toggleGroup($info, show, desc) {
	var $input = $('.bcms-field-text', $info);
	var $select = $('select', $info);
	if (show) {
		$select.removeClass('data-val-ignore');
		var checkBox = $input.parents('.bcms-checkbox-holder.has-moreinfo').first().find('input[type="checkbox"]');
		if (checkBox.length == 0 || checkBox.is(':checked')) {
			$input.removeClass('data-val-ignore');
			//$input.val('');
		}
		$info.show();
		if (desc) {
			var title = $info.find('.bcms-content-titles');
			if (!title.text())
				$info.find('.bcms-content-titles').html(desc + ' <i class="fa fa-asterisk text-danger"></i>');
		}
		$input.trigger('change');
		$select.trigger('change');
	} else {
		$select.addClass('data-val-ignore');
		$select.removeClass('bcms-input-validation-error');
		$input.addClass('data-val-ignore');
		$input.removeClass('bcms-input-validation-error');
		$info.hide();
	}
}
$(function () {
	datepicker.init();
	//$.validator.defaults.ignore = ".data-val-ignore, :disabled, :hidden";
	var $form = $("#registrationForm");
	$form.find('.bcms-datepicker').initializeDatepicker(datepicker.globalization.dateFormat);
	$form.find('.check-email-unique').blur(function () {
		var $this = $(this);
		if (!$this.hasClass("bcms-input-validation-error")) {
			$.post('/bcms-austar/registration/checkemail', { id: $(this).val() }, function (d) {
				if (d.id != undefined) {
					if (!d.valid) {
						$this.addClass('bcms-input-validation-error');
						$this.removeClass('valid');
					} else if ($this.valid()) {
						$this.removeClass('bcms-input-validation-error');
						$this.addClass('valid');
					}
				}
			});
		}
	});
	$form.find('input:radio').on('change', function (e) {
		var $info = $("#info_" + $(this).attr("id"));
		var $parent = $(this).closest('label');
		if ($info.length > 0) {
			if ($(this).is(':checked') && $parent.hasClass("has-moreinfo")) {
				toggleGroup($info, true);
			} else {
				toggleGroup($info, false);
			}
		}
	});
	$form.find('select')
	.on('change', function (e) {
		var $info = $('#info_' + e.target.getAttribute('id'));
		var $input = $('#info_' + e.target.getAttribute('id') + ' .bcms-field-text');
		var $select = $('#info_' + e.target.getAttribute('id') + ' select');
		if (e.target.getAttribute('showsetailsbyindex')) {
			var index = e.target.selectedIndex;
			for (var i = 0; i < e.target.length;i++){
				var $d = $('#info_' + e.target.getAttribute('id') + '_Index_' + i);
				toggleGroup($d, i < index);
			}
		}
		//console.log(e);
		var desc = $($(this)[0].options[$(this)[0].selectedIndex]).data('moreinfo');
		//var desc = $(e.added.element[0]).data('moreinfo');
		if (desc != undefined) {
			$select.removeClass('data-val-ignore');
			var checkBox = $input.parents('.bcms-checkbox-holder.has-moreinfo').first().find('input[type="checkbox"]');
			if (checkBox.length == 0 || checkBox.is(':checked')) {
				$input.removeClass('data-val-ignore');
				//$input.val('');
			}
			$info.show();
			if ($info) {
				var title = $info.find('.bcms-content-titles');
				if (!title.text())
					$info.find('.bcms-content-titles').html(desc + ' <i class="fa fa-asterisk text-danger"></i>');
			}
		} else {
			$select.addClass('data-val-ignore');
			$select.removeClass('bcms-input-validation-error');
			$input.addClass('data-val-ignore');
			$input.removeClass('bcms-input-validation-error');
			$info.hide();
		}
		$(this).valid();
	}).select2({ minimumResultsForSearch: -1 });

	$form.find('.bcms-checkbox-holder:has(input[type="checkbox"]) .bcms-js-edit-label').on('click', function () {
		var checkBox = $(this).parents('.bcms-checkbox-holder, .bcms-edit-check-field').first().find('input[type="checkbox"]');
		checkBox.trigger('click').trigger('change');
		return false;
	});
	$form.find('.bcms-checkbox-holder:has(input[type="checkbox"]).has-moreinfo input[type="checkbox"]').on('change', function () {
		var $this = $(this);
		var $parent = $(this).parents('.bcms-checkbox-holder, .bcms-edit-check-field').first();
		if ($this.is(':checked')) {
			$parent.find('.input-more-info').show();
			$parent.find('.input-more-info .bcms-field-text').removeClass('data-val-ignore');
		} else {
			$parent.find('.input-more-info').hide();
			$parent.find('.input-more-info .bcms-field-text').addClass('data-val-ignore');
			console.log('b');
		}
	});
	//$form.data("validator").settings.ignore = ".data-val-ignore, :hidden";

	$form.on('submit', function (e) {
		e.preventDefault();
		var $this = $(this);
		//$this.removeData("validator").removeData("unobtrusiveValidation");
		//$.validator.unobtrusive.parse($this);
		//$this.data("validator").settings.ignore.push(".data-val-ignore");
		if ($this.valid()) {
			$.post($this.attr("action"), $this.serialize()).done(function (d) {
				if (d.Success) {
					location.reload();
				} else {
					messages.refreshBox($this, d);
				}
			});
		}
		return false;
	});
	if ($form.data("validator"))
		$form.data("validator").settings.ignore = ".data-val-ignore";
	else
		console.log('no validator');
	$('#H_Bathrooms,#H_AvailableBedrooms,#H_FamilyMembers').trigger('change');
	$('input:radio, input[type="checkbox"], select').trigger('change');
});
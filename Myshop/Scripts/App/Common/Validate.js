/// <reference path="Utility.js" />
/// <reference path="../../jquery-1.10.2.js" />

var validate = {};
validate.validImage = function(title) {
    return '<img src="../../Images/Icons/check.png" data-validate alt="Validated" title="' + title + '"  class="validate-icon" />';
}

validate.invalidImage = function (title) {
    return '<img src="../../Images/Icons/if_Close_Icon_1398919.png" data-validate alt="Not Validated" title="' + title + '" class="validate-icon"/>';
}

//Password Validation

$(document).on('change', 'input[data-validate-password="true"]',function () {
    var isValid = utility.validatePassword($(this).val());
    let message = $(this).data('validate-password-error');
    $(this).next('img[data-validate]').remove();
    if (isValid) {
        $(this).after(validate.validImage('Validated'));
    }
    else {
        $(this).after(validate.invalidImage('Not Validated'));
        utility.SetAlert(message, 'danger');
    }
    $('[name="' + $(this).data('validate-compare') + '"]').val('').next('img[data-validate]').remove(); //reset value on dependent password field
});

$(document).on('change','input[data-validate-user="true"]',function () {
    var ctrl = $(this);
    let message = $(this).data('validate-user-error');
    utility.ajaxHelper(app.urls.isUserExist, { username: $(this).val().trim() }, function (data) {       
        $(ctrl).next('img[data-validate]').remove();
        if (!data) {
            $(ctrl).after(validate.validImage('User is available'));
        }
        else {
            $(ctrl).after(validate.invalidImage('User is not available'));
            utility.SetAlert(message, 'danger');
        }
    });    
});

$(document).on('change', 'input[data-validate-compare]',function () {
    var isValid = $('[name="' + $(this).data('validate-compare') + '"]').val() == $(this).val() ? true : false;
    let message = $(this).data('validate-compare-error');
    $(this).next('img[data-validate]').remove();
    if (isValid) {
        $(this).after(validate.validImage('Matched'));
    }
    else {
        $(this).after(validate.invalidImage("Not Matched"));
        utility.SetAlert(message, 'danger');
    }
});

$(document).on('change', 'input[data-validate-numeric]',function () {
    var isValid = /^-?(0|[1-9]\d*|(?=\.))(\.\d+)?$/.test($(this).val()) ? true : false;
    let message = $(this).data('validate-numeric-error');
    $(this).next('img[data-validate]').remove();
    if (isValid) {
        $(this).after(validate.validImage("Its a numeric value"));
    }
    else {
        $(this).after(validate.invalidImage("Its not a numeric value"));
        utility.SetAlert(message, 'danger');
    }
});

validate.form = function ($formId) {
    let $form = $('#' + $formId);
    let $isValid = true;
    $form.find('input[type="text"]').each(function ($ind, $ele) {
        var $textValue = $($ele).val();
        var $isRequired = $($ele).attr('required') === 'required' ? true : false;
        var $minLength = parseInt($($ele).attr('minlength'));
        if ($isRequired && $textValue.length == 0) {
            $($ele).addClass('shop_hasError');
            $isValid = false;
        }
        else {
            $($ele).removeClass('shop_hasError');
        }
        if (!isNaN($minLength) && $textValue.length < $minLength) {
            $($ele).addClass('shop_hasError');
            $isValid = false;
        }
        else {
            $($ele).removeClass('shop_hasError');
        }
    });
    return $isValid;
}



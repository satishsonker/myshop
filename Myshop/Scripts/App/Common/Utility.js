﻿/// <reference path="App.js" />
/// <reference path="../../jquery-1.10.2.intellisense.js" />
var utility = {};
var firebaseConfig = {};
utility.promise = {};

//Set Application Base URL
utility.baseURL = "http://localhost:60543";

//Various Ajax URLs

//utility.urls = {};
//utility.urls.GetPermissionJson = '/Global/user/GetPermissionJson';
//utility.urls.UpdateSinglePermission = '/Global/user/UpdateSinglePermission';
//utility.urls.SaveSinglePermission = '/Global/user/SaveSinglePermission';
//utility.urls.DeletesSinglePermission = '/Global/user/DeletesSinglePermission';
//utility.urls.GetShopMap = '/Global/user/GetShopMap';
//utility.urls.SetShopJson = '/Global/user/SetShopJson';
//utility.urls.DeleteShopMaps = '/Global/user/DeleteShopMap';
//utility.urls.GetUserJson = '/Global/user/GetUserJson';
//utility.urls.UpdateUserAccess = '/Global/user/UpdateUserAccess';
//utility.urls.GetErrorLog = '/Global/Admin/GetErrorLog';
//utility.urls.UpdateErrorLog = '/Global/Admin/UpdateErrorLog';
//utility.urls.isUserExist = '/Global/user/isUserExist';

//firebaseConfig = {
//    apiKey: "AIzaSyAF6K63i3Ihn-uEe8p-33UFZHvivaLGt0k",
//    authDomain: "myshop-4a2fc.firebaseapp.com",
//    databaseURL: "https://myshop-4a2fc.firebaseio.com",
//    projectId: "myshop-4a2fc",
//    storageBucket: "",
//    messagingSenderId: "285525393288"
//};
//firebase.initializeApp(firebaseConfig);

$(document).ready(function () {
    $('body').css('height', $(document).height() - 40);
    $("#mainAlert").fadeOut(6500);
    if ($('#myModal').length > 0) {
        $('#showModel').click();
    }

    $('#layoutContainer').css('min-height', ($(window).height() - 50) + 'px');

    //Set Footer
    $(document).scroll(utility.setFooter());
    $(window).resize(utility.setFooter());
    $(document).find('input[type="text"]:eq(1)').focus();

    $('[data-toggle="tooltip"]').tooltip();
});

utility.ajaxHelper = function (url, data, success, error, method) {
    error = utility.isNullOrUndefined(error) ? utility.errorCall : error;
    method = utility.isNullOrUndefined(method) ? app.const.ajaxMethod.post : method;
    var param = {};
    param.url = url;
    param.contentType = "application/json";
    param.method = method;
    param.success = success;
    param.error = error;
    param.data = JSON.stringify(data),
        $.ajax(param);
}

utility.ajaxHelperGet = function (url, success, error, method) {
    error = utility.isNullOrUndefined(error) ? utility.errorCall : error;
    method = utility.isNullOrUndefined(method) ? app.const.ajaxMethod.post : method;
    var param = {};
    param.url = url,
        param.contentType = "application/json",
        param.method = method,
        param.success = success,
        param.error = error
    $.ajax(param);
}

utility.ajaxWithoutDataHelper = function (url, success, error) {
    error = error === undefined ? utility.errorCall : error;
    $.ajax({
        url: url,
        contentType: "application/json",
        method: "post",
        success: success,
        error: error
    });
}

//Set Alert messge on _layout View
utility.SetAlert = function (message, alertType, cancelBotton) {
    alertType = alertType === undefined ? "info" : alertType;
    cancelBotton = cancelBotton === undefined ? false : cancelBotton;
    var alertColor;
    let className = 'alert-' + alertType;
    switch (alertType) {
        case 'info':
            alertColor = 'Information';
            break;
        case 'danger':
            alertColor = 'Error';
            break;
        case 'success':
            alertColor = 'Success';
            break;
        case 'warning':
            alertColor = 'Warning';
            break;

    }

    let $html = '<div class="shop_mainAlert">' +
        '<div class="panel panel-' + alertType + '">' +
        '<div class="panel-heading"><span>' + alertColor + '</span><img class="alertClose" src="../../Images/Icons/delete.png" style="" />' +
        '</div>' +
        '<div class="panel-body">' +
        '<div class="row">' +
        '<div class="col-lg-12">' +
        '<span id="message">' + message + '</span>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '<div class="panel-footer" style="height: 45px;padding: 5px 15px;">' +
        '<div class="btn-group pull-right">' +
        '<input type="button" id="btnAlertOk" value="OK" class="btn btn-primary" />' +
        (cancelBotton ? '<input type="button" id="btnAlertCancel" value="Cancel" class="btn btn-danger" />' : '') +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>';

    $('body').append($html);
}

$(document).on('click', '.alertClose,#btnAlertCancel,#btnAlertOk', function () {
    $('.shop_mainAlert').remove();
});

//Error Handler Function
utility.errorCall = function (e, x, settings, exception) {
    var message;
    var statusErrorMap = {
        '400': "Server understood the request, but request content was invalid.",
        '401': "Unauthorized access.",
        '403': "Forbidden resource can't be accessed.",
        '500': "Internal server error.",
        '503': "Service unavailable."
    };
    if (x.status) {
        message = statusErrorMap[e.status];
        if (!message) {
            message = "Unknown Error \n.";
        }
    } else if (exception == 'parsererror') {
        message = "Error.\nParsing JSON Request failed.";
    } else if (exception == 'timeout') {
        message = "Request Time out.";
    } else if (exception == 'abort') {
        message = "Request was aborted by the server";
    } else {
        message = "Unknown Error \n.";
    }
    alert(message);
}

utility.popupClose = function (id) {
    if (!id) {
        //$("img#popupclose").parent().parent().hide();
        $("div.popupmodel").hide();
    } else
        $("#" + id).hide();
};

utility.popupTableData = function (methodType) {
    var urls;
    var shopDDl = $('#ShopId');
    var shopid = parseInt($('#layoutShopId').val());

    if (shopDDl.length == 0 || (!isNaN(shopid) && shopid > 0)) {

        urls = app.urls[methodType]; //Fetching URLs
        if (urls === undefined && methodType.split('.').length > 0) {
            let urlParts = methodType.split('.');
            var tempUrl = app.urls;
            $(urlParts).each(function (ind, ele) {
                tempUrl = tempUrl[ele];
            });
            urls = tempUrl;

        }
        //Call Ajax helper
        utility.ajaxHelper(urls, { shopid: shopid }, function (data) {
            var list = data.objList !== undefined ? data.objList : data;
            var totalRecord = data.TotalRecord !== undefined ? data.TotalRecord : data.length;
            utility.tableBinder(list, 'popuptable', totalRecord);
        }, utility.errorCall);
    }
    else {
        utility.SetAlert('Please select shop name', 'info');
        $(shopDDl).focus();
    }
}

utility.isJson = function isJson(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}

utility.isNullOrEmpty = function (str) {
    return str == null || str === '' || str === undefined ? true : false;
}

utility.isNullOrUndefined = function (str) {
    return str == null || str === undefined ? true : false;
}

utility.tableBinder = function (data, tableid, totalRecord) {
    if (typeof data === 'string' && !utility.isJson(data)) {
        utility.SetAlert('There is some error');
    }

    tableid = tableid === undefined ? 'popuptable' : tableid;
    if (typeof data === 'object') {
        var table = $('#' + tableid);
        var thead = $(table).find('thead tr:eq(0) th');
        var tbody = $(table).find('tbody');
        var tblHtml = '';

        // empty table body
        tbody.empty();

        $(data).each(function (ind, ele) {
            tblHtml = tblHtml + '<tr>';
            $(thead).each(function (indH, eleH) {
                var eId = $(eleH).attr('id');
                if (eId === 'btnSelect') {
                    tblHtml = tblHtml + '<td><button type="button" class="btn btn-warning btn-sm" id="btnSelectRow_' + ind + '" value="Select" data-data=""><i class="fas fa-check black-text"></i> Select</button></td>';
                }
                else if (eId.toLowerCase().indexOf('date') > -1 || eId.toLowerCase().indexOf('dob') > -1) {
                    var format = $('#' + eId).data('dateformat');
                    var date = new Date(parseInt(ele[eId].substr(6)));
                    if (format === undefined)
                        tblHtml = tblHtml + '<td>' + date.toDateString() + '</td>';
                    else if (format === 'full')
                        tblHtml = tblHtml + '<td>' + date.toDateString() + ' ' + utility.getLeadingZero(date.getHours()) + ':' + utility.getLeadingZero(date.getMinutes()) + ':' + utility.getLeadingZero(date.getSeconds()) + '</td>';
                    else if (format === 'short')
                        tblHtml = tblHtml + '<td>' + date.toDateString() + '</td>';
                }
                else if (eId.toLowerCase().indexOf('image') > -1 || eId.toLowerCase().indexOf('photo') > -1) {
                    let imagePath = ele[eId] == "" ? '/Images/Icons/FemaleUser.png' : 'data:image/png;base64,' + ele[eId];
                    tblHtml = tblHtml + '<td><img class="shop-thumbnail" style="border-radius: 30px;padding-bottom: 0px !Important;" src="' + imagePath + '" alt="Image" /></td>';
                }
                else {
                    tblHtml = tblHtml + '<td>' + (utility.isNullOrEmpty(ele[eId]) ? '' : ele[eId]) + '</td>';
                }
            });
            tblHtml = tblHtml + '</tr>';
            tbody.append(tblHtml);
            tblHtml = '';
            $('[id*="btnSelectRow_"]').last().data('data', ele);
        });

        $('#' + tableid).DataTable();
        $('.popupmodel').show();
        $('.popup').show();


        //if ($('.popuptableWrapper').length == 0) {
        //$('#popuptable').wrap('<div class="popuptableWrapper" style="max-height: 320px;overflow-y: scroll;" class="table">'); //table scrollable
        //}

    }
    else if (typeof data === 'string') {
        let data = $($.parseHTML(data)[12]).find('div.jumbotron').length > 0 ? $.parseHTML(data)[12] : $.parseHTML(data)[14];
        this.SetAlert($($data).find('div.jumbotron').html().trim() == "" ? $($data).find('p#message').html().trim() : $($data).find('div.jumbotron').html().trim(), 'warning');
    }
    else
        throw new Error('Invalid parameter: expect object only');
}

utility.bottonGroupManager = function (rowSelect, isCancel) {
    var btnCancel = $('.bottonGroup [value="Cancel"]');
    var btnDelete = $('.bottonGroup [value="Delete"]');
    var btnSave = $('.bottonGroup [value="Save"]');
    var btnUpdate = $('.bottonGroup [value="Update"]');
    var btnView = $('.bottonGroup [value="View"]');
    var panelBody = $(btnCancel).parent().parent().parent().find('.panel-body');
    if (rowSelect == true) {
        btnCancel.removeClass('hideCtrl');
        btnDelete.removeClass('hideCtrl');
        btnUpdate.removeClass('hideCtrl');
        btnView.removeClass('hideCtrl');
        btnSave.addClass('hideCtrl');
    }
    else {

        btnSave.removeClass('hideCtrl');
        btnView.removeClass('hideCtrl');
        btnCancel.addClass('hideCtrl');
        btnDelete.addClass('hideCtrl');
        btnUpdate.addClass('hideCtrl');
    }

    if (isCancel == true) {
        var ctrl = $(btnCancel).parent().parent().find('fieldset');
        $(ctrl).find('input').val('');
        $(ctrl).find('img').attr('scr', '/Images/Icons/FemaleUser.png');
        $(ctrl).find('select#ShopId').val('').change();
        if (panelBody) {
            $(panelBody).find('input').val('');
            $(panelBody).find('select').val('')
        }
    }
}

utility.setFormPostUrl = function (formId, action, controller, area) {
    if (formId === undefined || typeof formId !== 'string' || action === undefined || typeof action !== 'string' || controller === undefined || typeof controller !== 'string') {
        throw new Error('Invalid Parameter: expect string only');
        return;
    }
    else {
        var form = $('#' + formId);
        var url = area === undefined ? '/' + controller + '/' + action : '/' + area + '/' + controller + '/' + action;
        form.attr('action', url);
    }
}

utility.confirmBox = function (message, confirmHandler) {
    $.confirm({
        title: 'Confirm!',
        content: message,
        buttons: {
            confirm: function () {
                confirmHandler();
            },
            cancel: function () {
            }
        }
    });
}

utility.disableCtrl = function (ctrlId, onlyDisable) {
    if (typeof ctrlId == 'string') {
        if (onlyDisable)
            $('#' + ctrlId).attr('disabled', 'disabled').addClass('disableCtrl');
        else
            $('#' + ctrlId).val('').attr('disabled', 'disabled').addClass('disableCtrl');
    }
    else if ($.isArray(ctrlId)) {
        $(ctrlId).each(function (ind, ele) {
            if (typeof ele === 'string') {
                if (onlyDisable)
                    $('#' + ele).attr('disabled', 'disabled').addClass('disableCtrl');
                else
                    $('#' + ele).val('').attr('disabled', 'disabled').addClass('disableCtrl');
            }
            else if (typeof ele === 'object') {
                if (onlyDisable)
                    $(ele).attr('disabled', 'disabled').addClass('disableCtrl');
                else
                    $(ele).val('').attr('disabled', 'disabled').addClass('disableCtrl');
            }
        });
    }
    else if (typeof ctrlId == 'object') {
        if (onlyDisable)
            $(ctrlId).attr('disabled', 'disabled').addClass('disableCtrl');
        else
            $(ctrlId).val('').attr('disabled', 'disabled').addClass('disableCtrl');
    }
};

utility.enableCtrl = function (ctrlId, onlyEnable) {
    if (typeof ctrlId == 'string') {
        if (onlyEnable)
            $('#' + ctrlId).removeAttr('disabled').removeClass('disableCtrl');
        else
            $('#' + ctrlId).val('').removeAttr('disabled').removeClass('disableCtrl');
    }
    else if ($.isArray(ctrlId)) {
        $(ctrlId).each(function (ind, ele) {
            if (typeof ele === 'string') {
                if (onlyEnable)
                    $('#' + ele).removeAttr('disabled').removeClass('disableCtrl');
                else
                    $('#' + ele).val('').removeAttr('disabled').removeClass('disableCtrl');
            }
            else if (typeof ele === 'object') {
                if (onlyEnable)
                    $(ele).removeAttr('disabled').removeClass('disableCtrl');
                else
                    $(ele).val('').removeAttr('disabled').removeClass('disableCtrl');
            }
        });
    }
    else if (typeof ctrlId == 'object') {
        if (onlyEnable)
            $(ctrlId).removeAttr('disabled').removeClass('disableCtrl');
        else
            $(ctrlId).val('').removeAttr('disabled').removeClass('disableCtrl');
    }
};

utility.bindDdlByAjax = function (methodUrl, ddlId, text, value, callback, CleaarOptionIndex) {
    CleaarOptionIndex = CleaarOptionIndex === undefined ? 0 : CleaarOptionIndex;
    var urls = app.urls[methodUrl];
    urls = urls === undefined ? methodUrl : urls;
    utility.ajaxWithoutDataHelper(urls, function (data) {
        if (typeof data === 'object') {
            var ddl = $('#' + ddlId);
            utility.enableCtrl(ddl);
            ddl.find(':gt(' + CleaarOptionIndex + ')').remove();
            $(data).each(function (ind, ele) {
                var Value = utility.isNullOrUndefined(value) ? ele["Value"] : ele[value];
                var Text = utility.isNullOrUndefined(text) ? ele["Text"] : ele[text];
                ddl.append('<option value=' + Value + '>' + Text + '</option>');
            });
            if ($('#ddlProduct').length > 0) {
                $('.ddlProduct').dropdown({
                    limitCount: 5
                });
            }
            if ($('#ddlCustType').length > 0) {
                $('.ddlCustType').dropdown({
                    limitCount: 5
                });
            }
        } else if (typeof data === 'string') {
            let $data = $($.parseHTML(data)[12]).find('div.jumbotron').length > 0 ? $.parseHTML(data)[12] : $.parseHTML(data)[14];
            utility.SetAlert($($data).find('div.jumbotron').html().trim() == "" ? $($data).find('p#message').html().trim() : $($data).find('div.jumbotron').html().trim(), 'warning');
        }
        else
            throw new Error('Invalid parameter: expect object only');

        if (callback)
            callback();
    });
}

utility.bindDdlByAjaxWithParam = function (methodUrl, ddlId, param, text, value, htmlDataAttr, callback, CleaarOptionIndex) {
    CleaarOptionIndex = CleaarOptionIndex === undefined ? 0 : CleaarOptionIndex;
    var urls = app.urls[methodUrl];
    urls = urls === undefined ? methodUrl : urls;
    utility.ajaxHelper(urls, param, function (data) {

        var ddl = $('#' + ddlId);

        if (htmlDataAttr !== undefined) {
            ddl.data(htmlDataAttr, data);
        }

        utility.enableCtrl(ddl); //Enable Control

        ddl.find(':gt(' + CleaarOptionIndex + ')').remove(); // Remove all pre. Options

        $(data).each(function (ind, ele) {
            var Value = utility.isNullOrUndefined(value) ? ele["Value"] : ele[value];
            var Text = utility.isNullOrUndefined(text) ? ele["Text"] : ele[text];
            ddl.append('<option value=' + Value + '>' + Text + '</option>');
        });

        if (callback !== undefined)
            callback();
    });
}

utility.validatePassword = function (str) {
    let isLower = false;
    let isUpper = false;
    let isNumber = false;
    let isSpecialChar = false;
    let isLength = false;
    if (str !== undefined && typeof str == 'string') {
        isLength = str.length >= 6 ? true : false;
        for (var i = 0; i < str.length; i++) {
            if ('!@#$%^&*()?><:".,|'.lastIndexOf(str[i]) > -1)
                isSpecialChar = true;
            else if (!isNaN(parseInt(str[i])))
                isNumber = true;
            else if (str[i].toUpperCase() === str[i])
                isUpper = true;

            else if (str[i].toLowerCase() === str[i])
                isLower = true;
        }

        return (isLower && isUpper && isNumber && isSpecialChar && isLength) ? true : false;
    }
}

utility.popup = function (message, HeaderTitle) {
    $('#Popup').find('.panel-body').text(message);
    $('#Popup').find('.panel-heading .row div:eq(0) ').text(HeaderTitle);
    $('#Popup').show();
}

utility.createDatetimePicker = function (selector) {
    $(selector).datetimepicker({
        numberOfMonths: 1,
        minDate: 0,
        maxDate: 30
    });
}

utility.getLeadingZero = function (int) {
    if (int !== undefined) {
        if (int < 10)
            return '0' + int.toString();
        else
            return int.toString();
    }
    else
        return '00';
}

/**
 * Get Date and time (DD/MM/YYYY hh:mm:ss) string from JSON date object
 * @param {JSON} jsonDate
 */
utility.getJsDateTimeFromJson = function (jsonDate) {
    var date = new Date(parseInt(jsonDate.substr(6)));
    return date.toLocaleDateString() + ' ' + date.toLocaleTimeString('en-US', { hour12: false });
}

/**
 * Get Date string in DD-MM-YYYY format from JSON Date object
 * @param {JSON} jsonDate
 */
utility.getFormatedDate = function (jsonDate) {
    var date = utility.getJsDateTimeFromJson(jsonDate);
    date = date.split(' ')[0].split('/');
    return date[2] + '-0' + date[0] + '-0' + date[1];
}

/**
 * Get JS Date object from JSON
 * @param {any} jsonDate
 */
utility.getDateObject = function (jsonDate) {
    return new Date(parseInt(jsonDate.substr(6)));
}

utility.getInrCurrency = function (amount) {
    var convertedAmount = parseFloat(amount);
    convertedAmount = isNaN(convertedAmount) ? 0.00 : convertedAmount;
    return app.const.htmlCode.rupeesSymbol + ' ' + convertedAmount.toFixed(2);
}

utility.setFraction = function (val) {
    if (val !== undefined) {
        val = val.toString();
        if ((typeof val === 'string' || typeof val === 'number') && val.length > 0) {
            return val = val.indexOf('.') > -1 ? (val.indexOf('.') == 0 ? '0' + val : val) : val + '.00';
        }
        else
            return '0.00';
    }
    else
        return '0.00';
};

utility.setRequired = function (ctrlId, isAdd) {
    isAdd = isAdd === undefined ? true : isAdd;
    if (typeof ctrlId === undefined) {
        throw new Error('Invalid parameter');
    }
    else if (typeof ctrlId === 'object') {
        $(ctrlId).each(function (ind, ele) {
            if (typeof ele === 'object') {
                if (isAdd) {
                    $(ele).attr('required', "required");
                } else {
                    $(ele).removeAttr('required');
                }
            }
            else if (typeof ele === 'string') {
                if (isAdd) {
                    $('#' + ele).attr('required', "required");
                } else {
                    $('#' + ele).removeAttr('required');
                }
            }
        });
    }
    else if (typeof ctrlId === 'string') {
        if (isAdd) {
            $('#' + ctrlId).attr('required', "required");
        } else {
            $('#' + ctrlId).removeAttr('required');
        }
    }
}
utility.setClass = function (ctrlId, className, isAdd) {
    isAdd = isAdd === undefined ? true : isAdd;
    if (typeof ctrlId === undefined || typeof className === undefined || typeof className !== 'string') {
        throw new Error('Invalid parameter');
    }
    else if (typeof ctrlId === 'object') {
        $(ctrlId).each(function (ind, ele) {
            if (typeof ele === 'object') {
                if (isAdd) {
                    $(ele).addClass(className);
                } else {
                    $(ele).removeClass(className);
                }
            }
            else if (typeof ele === 'string') {
                if (isAdd) {
                    $('#' + ele).addClass(className);
                } else {
                    $('#' + ele).removeClass(className);
                }
            }
        });
    }
    else if (typeof ctrlId === 'string') {
        if (isAdd) {
            $('#' + ctrlId).addClass(className);
        } else {
            $('#' + ctrlId).removeClass(className);
        }
    }
}

utility.createToast = function (message, color) {
    var toast = $('.toast')

    toast.text(message);
    var toastWidth = parseInt(toast.css('width')) / 2;
    var docWidth = $(window).width() / 2;
    toast.css('left', (docWidth - toastWidth) + 'px');
    toast.fadeIn('slow').delay(3000).fadeOut('slow');
    if (color !== undefined) {
        $('.toast').css('background-color', color);
    }
}


utility.setFooter = function () {
    //if ($(window).scrollTop() >= (($(document).height() - $(window).height()) - $('#layoutFooter-low').innerHeight())) {
    //    $('#layoutFooter-low').hide()
    //}
    //else {
    //    $('#layoutFooter-low').show()
    //}
}

utility.promise.get = function (url) {
    return new Promise(function (resolve, reject) {
        utility.ajaxHelperGet(url, function (response) {
            resolve(response);
        }, function (x, y, z) {
            resolve(x);
        });
    });
}

$(document).ajaxStart(function () {
    $('.loader').show();
});

$(document).ajaxComplete(function () {
    $('.loader').hide();
});

utility.isDataExist = function (validateDataOf, data, ctrl) {
    var msg = ctrl === undefined ? "Data is already exist" : $(ctrl).data('error-dataexist');
    var param = {};
    param.DataType = validateDataOf,
        param.data = data;
    utility.ajaxHelper(app.urls.isExist, param, function (response) {
        utility.SetAlert('')
    });
}

$(document).on('click', '.shop-thumbnail', function () {
    if ($(this)[0].nodeName == 'IMG') {
        $('.shop-thumbnail-img').attr('src', $(this).attr('src'));
        $('.shop-thumbnail-pre').show();
    }
});

utility.alertType =
    {
        information: 'info',
        error: 'danger',
        warning: 'warning',
        success: 'success'
    }

utility.setAjaxAlert = function (response, $successCall) {
    var resp = response.length > 1 ? response[1] : response[0];
    var $key = resp.Key;
    var $val = resp.Value;

    if (resp.hasOwnProperty('Key')) {
        if ($key == 110 || $key == 115)
            utility.SetAlert($val, utility.alertType.error);
        else if ($key == 101 || $key == 100 || $key == 102) {
            utility.SetAlert($val, utility.alertType.success);
            $successCall();
        }
        else
            utility.SetAlert($val, utility.alertType.warning);

    }
    else {
        var $msg = '';
        $(response).each(function (ind, ele) {
            $msg += (ind + 1) + '. ' + ele + '\n<br/>';
        })
        utility.SetAlert($msg, utility.alertType.error);
    }
}

/**
 * Get Value from current page URI Query string
 * @param {string} key
 */
utility.getQueryStringValue = function (key) {
    return decodeURIComponent(window.location.search.replace(new RegExp("^(?:.*[&\\?]" + encodeURIComponent(key).replace(/[\.\+\*]/g, "\\$&") + "(?:\\=([^&]*))?)?.*$", "i"), "$1"));
}

/**
 * Get digit in equivalent words
 * @param {number} num
 */
utility.currentyInWords = function (num) {
    var a = app.const.digitInWord;
    var b = app.const.digitx10InWord;
    if ((num = num.toString()).length > 9) return 'Invalid or access amount';
    n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
    if (!n) return; var str = '';
    str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
    str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
    str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
    str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';
    str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
    return str;
}

/**
 * Get the first index of array item that matches the input substring
 * @param {Array} array
 * @param {string} substring
 */
utility.getIndexOfSubStrInArray = function (array, substring) {
    var result = -1;
    for (index = 0; index < array.length; ++index) {
        value = array[index];
        if (value.toLowerCase().indexOf(substring.toLowerCase()) > -1) {
            result = value;
            break;
        }
    }
    return array.indexOf(result);
}

/**
 * Convert html table to array of object that quivalant to Datatable 
 * @param {string} $gridId
 * @param {number} $colStartIndex
 * @returns {Array}
 */
utility.gridToDatatable = ($gridId, $colStartIndex) => {
    $colStartIndex = utility.parseInt($colStartIndex, 0);

    let $table = $('#' + $gridId);
    let $tHead = $($table).find('thead tr:eq(0) th:gt(' + ($colStartIndex - 1) + ')');
    let $tbody = $($table).find('tbody tr');
    let $colNameArray = [];
    let $dataTable = [];

    $($tHead).each(function ($ind, $ele) {
        $colNameArray.push($($ele).text().trim());
    });

    $($tbody).each(function ($indRow, $eleRow) {
        var $dtRow = {};
        $($colNameArray).each(function ($indColName, $eleColName) {
            $dtRow[$eleColName] = $($($eleRow).find('td:gt(' + ($colStartIndex - 1) + ')')[$indColName]).text();
        });
        $dataTable.push($dtRow);
    });
    return $dataTable;
}

/**
 * Convert html table to array of object that quivalant to Datatable Json
 * @param {string} $gridId
 * @param {number} $colStartIndex
 * @returns {string}
 */
utility.gridToDatatableJson = ($gridId, $colStartIndex) => {
    return JSON.stringify(utility.gridToDatatable($gridId, $colStartIndex));
};

/**
 * Convter any value to the int otherwise replace by default value
 * @param {any} $value
 * @param {number} $defaultValue
 * @returns {number}
 */
utility.parseInt = ($value, $defaultValue) => {
    return $value === undefined ? $defaultValue : (typeof $value === 'number' ? $value : (typeof $value === 'string' ? (isNaN(parseInt($value)) ? $defaultValue : parseInt($value)) : $defaultValue));
};

/**
 * get current date and difference date 
 * @param {number} monthDifference
 * @returns {object}
 */
utility.getFromToDate = (monthDifference) => {
    monthDifference = utility.parseInt(monthDifference, 1);
    let $date = {};
    let now = new Date();

    $date.currentDay = parseInt(("0" + now.getDate()).slice(-2));
    $date.currentMonth = parseInt(("0" + (now.getMonth() + 1)).slice(-2));
    $date.currentYear = now.getFullYear();
    $date.currentDate = now.getFullYear() + "-" + ($date.currentMonth) + "-" + ($date.currentDay);

    let monthbefore = new Date();
    monthbefore.setMonth(monthbefore.getMonth() - monthDifference);

    $date.beforeDay = parseInt(("0" + monthbefore.getDate()).slice(-2));
    $date.beforeMonth = parseInt(("0" + (monthbefore.getMonth() + 1)).slice(-2));
    $date.beforeYear = monthbefore.getFullYear();
    $date.beforeDate = monthbefore.getFullYear() + "-" + ($date.beforeMonth) + "-" + ($date.beforeDay);
    return $date;
}

/**
 * Get Date and time differance between two dates
 * @param {Date} date1
 * @param {Date} date2
 */
utility.getDateDifference = function (date1, date2) {
    var timeDiff = Math.abs(date2.getTime() - date1.getTime());
    var diffDays = Math.floor(timeDiff / (1000 * 3600 * 24));
    return {
        timeDiff: utility.milisecondToTime(timeDiff),
        diffDays: diffDays
    }
}

/**
 * Convert milisecond to time
 * @param {number} milisecond
 */
utility.milisecondToTime = function (milisecond) {
    var milliseconds = parseInt((milisecond % 1000) / 100),
        seconds = parseInt((milisecond / 1000) % 60),
        minutes = parseInt((milisecond / (1000 * 60)) % 60),
        hours = parseInt((milisecond / (1000 * 60 * 60)) % 24);

    return {
        time: hours + ":" + minutes + ":" + seconds + "." + milliseconds,
        hours: hours,
        minutes: minutes,
        seconds: seconds,
        milliseconds: milliseconds
    };
}

/**
 * Get total elaps time in string like : 2 days ago or 5 mins ago
 * @param {Date} date1
 * @param {Date} date2
 */
utility.getElapsTime = function (date1, date2) {
    let $diff = utility.getDateDifference(date1, date2);
    let $result = '';
    if ($diff.diffDays > 0)
        $result = $diff.diffDays + " Days ago";
    else if ($diff.timeDiff.hours > 0)
        $result = $diff.timeDiff.hours + " Hours ago";
    else if ($diff.timeDiff.minutes > 0)
        $result = $diff.timeDiff.minutes + " Minutes ago";
    else if ($diff.timeDiff.seconds > 0)
        $result = $diff.timeDiff.seconds + " Seconds ago";
    else
        $result = "Just Now";
    return $result;
}

/**
 * Convter string into floating-point number upto 2 precision
 * @param {any} inputString
 * @returns {number}
 */
utility.toDecimal = (inputString) => {
    let $convertedValue = parseFloat(inputString);
    return isNaN($convertedValue) ? 0.00 : $convertedValue.toFixed(2);
};

/**
 * get Unique GUID
 * @returns {string}
 */
utility.getGUID = function guid() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
            .toString(16)
            .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
}

utility.local = {};
utility.local.set = function ($key, $value) {
    var $$val = '';
    if (typeof $value === 'object')
        $$val = JSON.stringify($value);
    if (typeof $value === 'string')
        $$val = $value;
    localStorage.setItem($key, $$val);
}

utility.local.get = function ($key, $isObject) {
    var $data = localStorage.getItem($key);
    return $isObject !== undefined && $isObject !== null && $isObject ? JSON.parse($data) : $data;
}

utility.local.removeItem = function ($key) {
    localStorage.removeItem($key);
}

/**
 * Convert Base64 string into plain string
 * @param {string} encodedString
 */
utility.decodeBase64 = function (encodedString) {
    return atob(encodedString);
}

utility.webSession = function () {
    return JSON.parse(utility.decodeBase64($('#hdnsessionjson').val()));
}

utility.localData = {};
utility.localData.isScreenLocked = false;

utility.getTextArray = function (objCollection) {
    let $arr = [];
    if (objCollection != undefined && typeof objCollection === 'object') {
        $(objCollection).text(function (ind, ele) {
            $arr.push(ele);
        });
    }
    return $arr;
}

/**
 * Check whether URL is valid of not
 * @param {any} url
 * @returns {boolean}
 */
utility.isUrlValid = function(url) {
    return /^(https?|s?ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i.test(url);
}

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    $('.popupmodel').hide();
});
/**
 * Set Date to input date control
 * @param {string} ctrlId
 * @param {Date} dateObj
 */
utility.setDateToDateControl = function (ctrlId,dateObj) {
    var now = dateObj;

    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);

    var today = now.getFullYear() + "-" + (month) + "-" + (day);

    $('#'+ctrlId).val(today);
}

/**
 * 
 * @param {any} ctrlId
 */
utility.getDateControlFormatDate = function (ctrlId) {
    var now = '';
    if (typeof ctrlId === 'string') {
        now = new Date($('#' + ctrlId).val());
    }
    if (typeof ctrlId === 'object') {
        now = ctrlId;
    }

    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);

    return now.getFullYear() + "-" + (month) + "-" + (day);
}
/**
 * 
 * @param {string} fromDateId
 * @param {string} toDateId
 */
utility.setDateRange = function (fromDateId, toDateId) {
    $('#' + toDateId).attr('max', utility.getDateControlFormatDate(new Date()));
    $('#' + fromDateId).attr('max', utility.getDateControlFormatDate(new Date()));
    $(document).on('change', '#' + fromDateId, function () {
        let $toDate = new Date($('#' + toDateId).val());
        let $fromDate = new Date($(this).val());
        if ($fromDate > $toDate) {
            $(this).val(utility.getDateControlFormatDate(toDateId));
        }
        $(this).attr('max', utility.getDateControlFormatDate(toDateId));
        $($('#' + toDateId)).attr('min', utility.getDateControlFormatDate(fromDateId));
    });

    $(document).on('change', '#' + toDateId, function () {
        let $toDate = new Date($(this).val());
        let $fromDate = new Date($('#' + fromDateId).val());
        if ($toDate < $fromDate) {
            $('#' + fromDateId).val(utility.getDateControlFormatDate(toDateId));
        }
        $(this).attr('max', utility.getDateControlFormatDate(new Date()));
        $($('#' + fromDateId)).attr('max', utility.getDateControlFormatDate(toDateId));
    });
}

utility.getProgressbarColor = function (value) {
    if (value < 33) {
        return '#70ca63'
    }
    else if (value > 33 && value < 56) {
        return '#f7e621'
    }
    else if (value > 56 && value < 75) {
        return '#f78e21'
    }
    else {
        return '#f70303'
    }
}

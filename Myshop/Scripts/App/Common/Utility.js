/// <reference path="App.js" />
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
    $('body').css('height', $(document).height()-40);
    $("#mainAlert").fadeOut(6500);
    if ($('#myModal').length > 0) {
        $('#showModel').click();
    }

    $('#layoutContainer').css('min-height', ($(window).height() - 50) + 'px');

    //Set Footer
    $(document).scroll(utility.setFooter());
    $(window).resize(utility.setFooter());
});

utility.ajaxHelper = function (url, data, success, error) {
    error = error === undefined ? utility.errorCall : error;
    $.ajax({
        url: url,
        contentType: "application/json",
        method: "post",
        success: success,
        data: JSON.stringify(data),
        error: error
    });
}

utility.ajaxHelperGet = function (url, success, error,method) {
    error = utility.isNullOrUndefined(error) ? utility.errorCall : error;
    method = utility.isNullOrUndefined(method) ? app.const.ajaxMethod.post : method;
    var param = {};
    param.url= url,
    param.contentTyp= "application/json",
    param.method = method,
    param.success=success,
    param.error=error
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
utility.SetAlert = function (message, alertType) {
    alertType = alertType === undefined ? "info" : alertType;
    var alertColor;
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
    var ele = $("#mainAlert");
    if (ele.length > 0) {
        ele.show().fadeIn(300);;
        $("#alertColor").empty().text(alertColor + '!');
        $("#message").empty().text(message);
    }
    else {
        $("#mainalertContainer").empty().append('<div class="alert alert-' + alertType + ' text-center alert-dismissible" id="mainAlert" style="margin-top: 0px;position: fixed !important;width: 80% !important;display: none;z-index: 100000000 !important;margin-left: 10% !important;" role="alert">' +
                        '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                        '<strong  id="alertColor">' + alertColor + '!</strong><p id="message">' + message + '</p></div>');
    }
    $("#mainAlert").delay(7000).fadeOut(500);
}

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
        message = statusErrorMap[x.status];
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
        $("img#popupclose").parent().parent().hide();
    }else
    $("#"+id).hide();
};

utility.popupTableData = function (methodType) {
    var urls;
    var shopDDl = $('#ShopId');
    var shopid = parseInt($('#layoutShopId').val());

    if (shopDDl.length == 0 || (!isNaN(shopid) && shopid > 0)) {

        urls = app.urls[methodType]; //Fetching URLs

        //Call Ajax helper
        utility.ajaxHelper(urls, { shopid: shopid }, function (data) {
            var list = data.objList !== undefined ? data.objList : data;
            var totalRecord = data.TotalRecord !== undefined ? data.TotalRecord : data.length;
            utility.tableBinder(list,'popuptable',totalRecord);
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

utility.tableBinder = function (data, tableid,totalRecord) {
    if (typeof data === 'string' && !utility.isJson(data))
    {
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
                    tblHtml = tblHtml + '<td><input type="button" id="btnSelectRow_' + ind + '" value="Select" data-data="" /></td>';
                }
                else if (eId.toLowerCase().indexOf('date') > -1 || eId.toLowerCase().indexOf('dob') > -1) {
                    var format = $('#' + eId).data('dateformat');
                    var date = new Date(parseInt(ele[eId].substr(6)));
                    if (format === undefined)
                        tblHtml = tblHtml + '<td>' + date.toDateString() + '</td>';
                    else if (format === 'full')
                        tblHtml = tblHtml + '<td>' + date.toDateString() + ' ' + utility.getLeadingZero(date.getHours()) + ':' + utility.getLeadingZero(date.getMinutes()) + ':' + utility.getLeadingZero(date.getSeconds()) + '</td>';
                    else if (format === 'short')
                        tblHtml = tblHtml + '<td>' + date.toDateString()+ '</td>';
                }
                else if (eId.toLowerCase().indexOf('image')>-1)
                {
                    tblHtml = tblHtml + '<td><img class="shop-thumbnail" src="data:image/png;base64,' + ele[eId] + '" alt="Image" /></td>';
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
    var btnCancel = $('.bottonGroup input[value="Cancel"]');
    var btnDelete = $('.bottonGroup input[value="Delete"]');
    var btnSave = $('.bottonGroup input[value="Save"]');
    var btnUpdate = $('.bottonGroup input[value="Update"]');
    var btnView = $('.bottonGroup input[value="View"]');
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
        $(ctrl).find('select#ShopId').val('').change();
        if(panelBody)
        {
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
            ddl.find(':gt('+CleaarOptionIndex+')').remove();
            $(data).each(function (ind, ele) {
                var Value = utility.isNullOrUndefined(value)? ele["Value"] : ele[value];
                var Text = utility.isNullOrUndefined(text) ? ele["Text"] : ele[text];
                ddl.append('<option value=' + Value + '>' + Text + '</option>');
            });
            if ($('#ddlProduct').length > 0) {
                $('.ddlProduct').dropdown({
                    limitCount:5
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

utility.popup = function (message,HeaderTitle) {
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

utility.getJsDateTimeFromJson = function (jsonDate) {
    var date = new Date(parseInt(jsonDate.substr(6)));
    return date.toLocaleDateString() + ' ' + date.toLocaleTimeString('en-US', { hour12: false });
}

utility.getFormatedDate = function(jsonDate)
{
    var date = utility.getJsDateTimeFromJson(jsonDate);
    date = date.split(' ')[0].split('/')
    return date[2]+'-0'+date[0] + '-0' + date[1] ;
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
    var toastWidth =parseInt(toast.css('width'))/2;
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
    var param={};
    param.DataType = validateDataOf,
    param.data=data;
    utility.ajaxHelper(app.urls.isExist, param, function (response) {
        utility.SetAlert('')
    });
}

$(document).on('click', '.shop-thumbnail', function () {
    if ($(this)[0].nodeName == 'IMG')
    {
        $('.shop-thumbnail-img').attr('src', $(this).attr('src'));
        $('.shop-thumbnail-pre').show();
    }
});
var $multiselect = {};

$(document).on('click', '.shop-multiselect ul li:gt(0)', function (e) {
    if (!$(this).hasClass('norecord')) {
        $(this).toggleClass('selected'); multiselectStatus();
    }
    else {
        $(this).parent().hide();
        e.stopPropagation();
    }
});

$(document).on('click', '.shop-multiselect', function (e) {
    $ul = $(this).find('ul');
    $ul.removeClass('active');
    $ul.show();
});

$(document).on('click', 'body', function (e) {
    let $ul = $('.shop-multiselect ul');
    if ($ul.hasClass('active')) {
        $ul.hide();
    } else {
        $ul.addClass('active');
    }

});

$(document).on('click', '.shop-multiselect ul li .fa-check', function () {
    $('.shop-multiselect ul li:gt(0)').addClass('selected');
    multiselectStatus();
});

$(document).on('click', '.shop-multiselect ul li .fa-times', function () {
    $('.shop-multiselect ul li:gt(0)').removeClass('selected');
    multiselectStatus();
});

$(document).on('keydown', '#_mSearch', function () {
    let $data = $(this).data('listdata');
    let $text = $(this).val();
    let $len = $text.length;
    var $html = '';
    if (!utility.isNullOrUndefined($data)) {
        let $list = $('.shop-multiselect ul');
        if ($len > 0) {
            $($list).find('li:gt(0)').remove();
           
            $($data).each(function (ind, ele) {
                if (ele.toLowerCase().indexOf($text.toLowerCase()) > -1)
                    $html += '<li>' + ele + '</li>';
            });
            if ($html != '')
                $($list).append($html);
            else
                $($list).append('<li class="norecord">No Record</li>');
        }
        else {
            $($list).find('li:gt(0)').remove();
            $($data).each(function (ind, ele) {
                    $html += '<li>' + ele + '</li>';
            });
            if ($html != '')
                $($list).append($html);
        }
    }
});
function multiselectStatus() {
    let $selectedElements = $('.shop-multiselect ul li.selected');
    let $selectedEleCount = $selectedElements.length;
    let $selectedData = $multiselect.getTextArray($selectedElements).join(',').split(',');
    if ($selectedEleCount == 0) {
        $('.shop-multiselect .header').empty().append('<i class="fa fa-list-ul defaultText"> Select Items</i>');
        $selectedData = [];
    }
    else if ($selectedEleCount < 3) {
        $('.shop-multiselect').data('selecteddata');
        $('.shop-multiselect .header').text($selectedData.join(','));
    }
    else {
        $('.shop-multiselect .header').text($selectedEleCount + ' selected');
    }

    $('.shop-multiselect').data('selecteddata', $selectedData);
}

$multiselect.getTextArray = function (objCollection) {
    let $arr = [];
    if (objCollection != undefined && typeof objCollection === 'object') {
        $(objCollection).text(function (ind, ele) {
            $arr.push(ele);
        });
    }
    return $arr;
}


jQuery.fn.extend({
    multiselect: function (options) {
        let $ctrl = $(this);
        let $li = '';
        if (typeof options === 'object') {
            if (options.hasOwnProperty('data')) {
                if (typeof options.data === 'object' && options.data.length > 0) {
                    $(options.data).each(function (ind, ele) {
                        $li += '<li>' + ele + '</li>';
                    });
                    let $html = '<span class="header"><i class="fa fa-list-ul defaultText"> Select Items</i></span>' +
                        '<ul>' +
                        '<li>' +
                        '<div class="input-group margin-bottom-sm">' +
                        '<span class="input-group-addon"><i class="fa fa-search fa-fw" aria-hidden="true"></i></span>' +
                        '<input class="form-control" id="_mSearch" data-listdata="' + options.data+'" autocomplete="off" type="text" placeholder="Search">' +
                        ' </div>' +
                        '<i class="fa fa-check pull-left" title="Select All"></i><i class="fa fa-times pull-right" title="Unselect All"></i>' +
                        '</li>' + $li +
                        '</ul>';
                    $($ctrl).addClass('shop-multiselect');
                    $($ctrl).append($html);
                    $($ctrl).data('selecteddata', '');
                }
                if (typeof options.data === 'string') {
                    $multiselect.getData(options.data).then(function (rep) {
                        $(rep).each(function (ind, ele) {
                            $li += '<li>' + ele + '</li>';
                        });
                        let $html = '<span class="header"><i class="fa fa-list-ul defaultText"> Select Items</i></span>' +
                            '<ul>' +
                            '<li>' +
                            '<div class="input-group margin-bottom-sm">' +
                            '<span class="input-group-addon"><i class="fa fa-search fa-fw" aria-hidden="true"></i></span>' +
                            '<input class="form-control" id="_mSearch" data-listdata="' + rep +'" autocomplete="off" type="text" placeholder="Search">' +
                            ' </div>' +
                            '<i class="fa fa-check pull-left" title="Select All"></i><i class="fa fa-times pull-right" title="Unselect All"></i>' +
                            '</li>' + $li +
                            '</ul>';
                        $($ctrl).addClass('shop-multiselect');
                        $($ctrl).append($html);
                        $($ctrl).data('selecteddata', '');
                        $('#_mSearch').data('listdata', rep);
                        $('#_mSearch').val('');
                    });
                }
            } else {
                options.data = [];
            }
        }
        else {
            let $html = '<span class="header"><i class="fa fa-list-ul defaultText"> Select Items</i></span>' +
                '<ul>' +
                '<li>' +
                '<div class="input-group margin-bottom-sm">' +
                '<span class="input-group-addon"><i class="fa fa-search fa-fw" aria-hidden="true"></i></span>' +
                '<input class="form-control" id="_mSearch" data-listdata="" autocomplete="off" type="text" placeholder="Search">' +
                ' </div>' +
                '<i class="fa fa-check pull-left" title="Select All"></i><i class="fa fa-times pull-right" title="Unselect All"></i>' +
                '</li>' + $li +
                '</ul>';
            $($ctrl).addClass('shop-multiselect');
            $($ctrl).data('selecteddata','');
            $($ctrl).append($html);
        }       
    },
    multiselectValue: function () {
        return $(this).data('selecteddata');
    }
});

/**
 * Check whether URL is valid of not
 * @param {any} url
 * @returns {boolean}
 */
$multiselect.isUrlValid = function (url) {
    return /^(https?|s?ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i.test(url);
}

$multiselect.ajaxHelperGet = function (url, success, error, method) {
    error = $multiselect.isNullOrUndefined(error) ? utility.errorCall : error;
    method = $multiselect.isNullOrUndefined(method) ? 'GET' : method;
    var param = {};
    param.url = url,
        param.contentType = "application/json",
        param.method = method,
        param.success = success,
        param.error = error
    $.ajax(param);
}

$multiselect.isNullOrUndefined = function (str) {
    return str == null || str === undefined ? true : false;
}

$multiselect.getData = function (url) {
    return new Promise(function (resolve, reject) {
        $multiselect.ajaxHelperGet(url, function (data) {
            var $data=[]
            $(data).each(function (ind, ele) {
                $data.push(ele.Text);
            });
            resolve($data);
        }, function (err) {
            reject(err);
        });
    });
}
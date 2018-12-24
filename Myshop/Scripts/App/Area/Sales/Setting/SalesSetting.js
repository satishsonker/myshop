/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#GSTIN').val(data.GSTIN);
    $('#SalesOpeningTime').val(data.SalesOpeningTime);
    $('#SalesClosingTime').val(data.SalesClosingTime);
    $('#ReturnPolicy').val(data.ReturnPolicy);
    $('#salessettingid').val(data.Id);
    $('#WeeklyClosingDay').val(data.WeeklyClosingDay);
    $('#ExchangeDayTime').val(data.ExchangeDayTime);
    $('#gstRate').val(data.GstRate);
    $('.popup').hide();
});

$(document).on('click', '[id*="btnSave"]', function () {
    $('#salessettingid').val('0');
    utility.setFormPostUrl('salesettingform', 'SaveSetting', 'Settings', 'SalesManagement');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('salesettingform', 'UpdateSetting', 'Settings', 'SalesManagement');
        $('#salesettingform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('salesettingform', 'DeleteSetting', 'Settings', 'SalesManagement');
        $('#salesettingform').submit();
    });
    return false;
});

$(document).on('keyup', '#ExchangeDayTime', function () {
    let $txt = $(this).val();
    let $position = $(this).position();
    $position.width = $(this).width();
    let $autofill = $(this).next('.shop_SearchControl');
    $($autofill).css({ 'left': $position.left, 'width': $position.width, 'top': ($position.top + 34) });
    var $index = -1;
    var $html = '';
    if ($txt.length >= 3 && $txt.length < 9 && $txt.indexOf('-') ==-1) {
        $index = utility.getIndexOfSubStrInArray(app.const.weekDayArray, $txt);
        var $weekday = app.const.weekDayArray[$index];
        $html = '';
        if ($index > -1) {
            for (var i = $index + 1; i < app.const.weekDayArray.length; i++) {
                $html += '<li>' + $weekday + '-' + app.const.weekDayArray[i] + ' '+'</li>';
            }
        }
        else {
            $html += '<li class="norecord">No Records</li>';
        }

        $('#daySuggestion').empty().append($html);
        $($autofill).show();
    }
    else if ($txt.indexOf('-') > 5)
    {
        $index = utility.getIndexOfSubStrInArray(app.const.hourArray, (($txt.split(' ')[1]).split('-')[0]).split(':')[0]);
        var $hour = app.const.hourArray[$index];
        $html = '';
        if ($index > -1) {
            for (var i = $index + 1; i < app.const.hourArray.length; i++) {
                $html += '<li>' + $hour + '-' + app.const.hourArray[i] + '</li>';
            }
        }
        else {
            $html += '<li class="norecord">No Records</li>';
        }

        $('#daySuggestion').empty().append($html);
        $($autofill).show();
    }
    else {
        $('#daySuggestion').empty();
        $($autofill).hide();
    }
});

$(document).on('click', '.shop_SearchControl li', function () {
    if (!$(this).hasClass('norecord')) {
        var $selectedText = $('#ExchangeDayTime').data('selectedday');
        $selectedText = $selectedText === '' ? '' : $selectedText + ' ';
        $(this).parent().parent().find('input').val($selectedText+ $(this).text());
        $('#ExchangeDayTime').data('selectedday', $(this).text()).focus();
    }
    else {
        $(this).parent().parent().find('input').val('');
        $('#ExchangeDayTime').data('selectedday', '');
    }
    $(this).parent().empty().hide();
});
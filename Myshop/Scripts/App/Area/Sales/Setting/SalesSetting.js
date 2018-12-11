/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#GSTIN').val(data.GSTIN);
    $('#SaleOpeningTime').val(data.SaleOpeningTime);
    $('#SaleClosingTime').val(data.SaleClosingTime);
    $('#ReturnPolicy').val(data.ReturnPolicy);
    $('.popup').hide();
});

$(document).on('click', '[id*="btnSave"]', function () {
    $('#salessettingid').val('0');
    utility.setFormPostUrl('salesettingform', 'SaveSetting', 'Settings', 'SalesManagement');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('salesettingform', 'Updatetting', 'Settings', 'SalesManagement');
        $('#salesettingform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('salesettingform', 'Delete', 'Settings', 'SalesManagement');
        $('#salesettingform').submit();
    });
    return false;
});
/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#paymodeid').val(data.PayModeId);
    $('#paymode').val(data.PayMode);
    $('#paymodedesc').val(data.Description === 'No Description' ? '' : data.Description);
    $('.popup').hide();
});

$(document).on('click', '[id*="btnSave"]', function () {
    $('#paymodeid').val('0');
    utility.setFormPostUrl('paymodeform', 'SetPayMode', 'masters', 'Global');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('paymodeform', 'UpdatePayMode', 'masters', 'Global');
        $('#paymodeform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('paymodeform', 'DeletePayMode', 'masters', 'Global');
        $('#paymodeform').submit();
    });
    return false;
});
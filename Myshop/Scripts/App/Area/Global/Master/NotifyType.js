/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#notificationtypeid').val(data.AccountTypeId);
    $('#accounttype').val(data.AccountType);
    $('#accounttypedesc').val(data.Description === 'No Description' ? '' : data.Description);
    $('.popup').hide();
});

$(document).on('click', '[id*="btnSave"]', function () {
    $('#notificationtypeid').val('0');
    utility.setFormPostUrl('notifytypeform', 'SetNotificationType', 'masters', 'Global');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('notifytypeform', 'UpdateNotificationType', 'masters', 'Global');
        $('#notifytypeform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('notifytypeform', 'DeleteNotificationType', 'masters', 'Global');
        $('#notifytypeform').submit();
    });
    return false;
});
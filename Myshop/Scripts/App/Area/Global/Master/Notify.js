/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).ready(function() {
    utility.bindDdlByAjax(app.urls.GetNotificationTypeSelectList, 'ddlNotificationtypeid');
    utility.bindDdlByAjax(app.urls.GetUserSelectList, 'ddlUserid',null,null,null,1);
});

$(document).on('click', '#ddlUserid', function () {
    $('#isforall').val($(this).find(':selected').val() == '0' ? 'true' : 'false');
});

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#ddlNotificationtypeid').val(data.NotificationTypeId);
    $('#NotificationType').val(data.NotificationType);
    $('#Description').val(data.Description === 'No Description' || utility.isNullOrEmpty(data.Description) ? '' : data.Description);
    $('.popup').hide();
});

$(document).on('click', '[id*="btnSave"]', function () {
    $('#notificationtypeid').val('0');
    utility.setFormPostUrl('notifyform', 'SetNotification', 'masters', 'Global');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('notifyform', 'UpdateNotification', 'masters', 'Global');
        $('#notifyform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('notifyform', 'DeleteNotification', 'masters', 'Global');
        $('#notifyform').submit();
    });
    return false;
});
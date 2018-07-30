$(document).ready(function () {
    getNotificationList();
});

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    var notifyId = $(this).data('data');
    utility.ajaxHelper(app.urls.UsersController.DeleteUserNotificationList, { notificationId: notifyId }, function (data) {
        if (data[0].Key == 100) {
            utility.SetAlert('Notification deleted successfully', app.const.alertType.success);
            getNotificationList();
        }
    }, function (x, y, z) {
        x;
    });
});

var getNotificationList = function() {
    utility.ajaxHelperGet(app.urls.UsersController.GetUserNotificationList, function (data) {
        if (data.length > 0) {
            var tbody = $('#tblNotificationList tbody');
            tbody.empty();
            var tr = '<tr>';
            var srno = 1;
            $(data).each(function (ind, ele) {
                tr += '<td>' + srno + '</td>';
                tr += '<td>' + ele.Message + '</td>';
                tr += '<td class="bottonGroup"><input style="width: 100px;" type="button" id="btnSelectRow_' + srno + '" value="Delete" data-data="' + ele.NotificationId + '" /></td>';
                tr += '</tr>';
            });
            $(tbody).append(tr);
        }
    }, null, app.const.ajaxMethod.get);
}
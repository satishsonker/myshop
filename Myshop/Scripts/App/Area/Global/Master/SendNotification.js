$(document).ready(function () {
    getNotificationList();
});

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    var notifyId = $(this).data('data');
    utility.ajaxHelper(app.urls.GlobalArea.MasterController.SendNotification, { notificationId: notifyId }, function (data) {
        if (data[0].Key == 109) {
            utility.SetAlert('Notification pushed/Send successfully', app.const.alertType.success);
            getNotificationList();
        }
    }, function (x, y, z) {
        x;
    });
});

var getNotificationList = function() {
    utility.ajaxHelper(app.urls.GlobalArea.MasterController.GetNotificationJson,{'fetchAll':false}, function (data) {
        if (data.length > 0) {
            var tbody = $('#tblNotificationList tbody');
            tbody.empty();
            var tr = '<tr>';
            var srno = 1;
            $(data).each(function (ind, ele) {
                tr += '<td>' + srno + '</td>';
                tr += '<td>' + ele.UserName + '</td>';
                tr += '<td>' + ele.NotificationType + '</td>';
                tr += '<td>' + ele.Message + '</td>';
                tr += '<td>' + (ele.IsForAll?'Yes':'No') + '</td>';
                tr += '<td>' + utility.getJsDateTimeFromJson(ele.MessageExpireDate) + '</td>';
                tr += '<td>' +utility.getJsDateTimeFromJson(ele.CreatedDate) + '</td>';
                tr += '<td class="bottonGroup"><input style="width: 100px;" type="button" id="btnSelectRow_' + srno + '" value="' + (ele.NotificationType.toLocaleLowerCase().indexOf('push')>-1?'Push':'Send') + '" data-data="' + ele.NotificationId + '" /></td>';
                tr += '</tr>';
                srno += 1;
            });
            $(tbody).append(tr);
        }
    }, null, app.const.ajaxMethod.post);
}
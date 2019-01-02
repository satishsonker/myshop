$(document).ready(function () {
    getNotificationList();
});

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    var notifyId = $(this).data('data');
    let $ctrl = $(this);
    utility.ajaxHelper(app.urls.GlobalArea.MasterController.SendNotification, { notificationId: notifyId }, function (data) {
        utility.setAjaxAlert(data, () => {
            $($ctrl).parent().parent().remove();
            $($ctrl).parent().parent().parent().append('<tr><td style="text-align:center;" colspan="8">No Pending Notications</td></tr>');
        });
       
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
                tr += '<td class="bottonGroup"><i class="fas fa-paper-plane fontButton skyBlue-Text" id="btnSelectRow_' + srno + '" title="' + (ele.NotificationType.toLocaleLowerCase().indexOf('push') > -1 ? 'Push' : 'Send') + ' this notification" data-data="' + ele.NotificationId + '"></i><i class="fas fa-trash-alt fontButton red-text" title="Delete this notification" id="btnDelete_' + srno + '"></i></td>';
                tr += '</tr>';
                srno += 1;
            });
            $(tbody).append(tr);
        }
    }, null, app.const.ajaxMethod.post);
}
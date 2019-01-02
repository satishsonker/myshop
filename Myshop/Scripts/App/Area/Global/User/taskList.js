var $taskList = {};

$taskList.completeTask = function ($taskId, $crtl) {
    utility.ajaxHelper(app.urls.GlobalArea.AdminController.TaskMarkComplete, { taskId: $taskId }, function (resp) {
        utility.setAjaxAlert(resp, function () {

            $($crtl).parent().remove();
            if ($('#_taskList li').length == 0) {
                $('#_taskList').append('<li style="text-align:center"><i class="far fa-smile-wink green-text" style="font-size: 52px;"> No Task</i></li>');
            }
        });
    });
}

$(document).on('click', '[id*="btnTaskComplete_"]', function () {
    var $id = $(this).data('id');
    $taskList.completeTask($id, $(this));
});
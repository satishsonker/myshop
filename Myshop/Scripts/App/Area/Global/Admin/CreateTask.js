/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../Common/App.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).ready(function () {
   
});

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('input[type="hidden"]#AssignedUserId').val(data.TaskAssignedUserId);
    $('#taskid').val(data.TaskId);
    $('#taskdetails').val(data.TaskDetails);
    $('#priority').val(data.Priority);
    $('#isimportant').attr('checked',data.IsImportant);
    
    $('.popup').hide();
});

$(document).on('click', '[id*="btnSave"]', function (e) {
    let pass = $('#password').val();
    let conPass = $('#conpassword').val();
    if (pass != conPass) {
        utility.SetAlert('Password and confirm password should be same', 'warning');
        e.preventDefault();
        return false;
    }
    $('#userid').val('0');
    utility.setFormPostUrl('createtaskform', 'SaveUserTask', 'admin', 'Global');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function (e) {
    let pass = $('#password').val();
    let conPass = $('#conpassword').val();
    if (pass != conPass) {
        utility.SetAlert('Password and confirm password should be same', 'warning');
        e.preventDefault();
        return false;
    }
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('createtaskform', 'UpdateUserTask', 'admin', 'Global');
        $('#createtaskform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to Delete.', function () {
        utility.setFormPostUrl('createtaskform', 'DeleteUserTask', 'user', 'Global');
        $('#createtaskform').submit();
    });
    return false;
});
/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../Common/App.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).ready(function () {
    utility.bindDdlByAjax(app.urls.GetUserTypeUrl, 'usertypeid');
});

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#username').val(data.Username);
    $('#mobile').val(data.Mobile);
    $('#name').val(data.Name);
    $('#usertypeid').val(data.UserTypeId);
    $('.popup').hide();
});

$(document).on('click', '[id*="btnSave"]', function (e) {
    let pass = $('#password').val();
    let conPass = $('#conpassword').val();
    if (pass != conPass)
    {
        utility.SetAlert('Password and confirm password should be same', 'warning');
        e.preventDefault();
        return false;
    }
    $('#userid').val('0');
    utility.setFormPostUrl('createuserform', 'SetUser', 'user', 'Global');
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
        utility.setFormPostUrl('createuserform', 'UpdateUser', 'user', 'Global');
        $('#createuserform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to Delete.', function () {
        utility.setFormPostUrl('createuserform', 'DeleteUser', 'user', 'Global');
        $('#createuserform').submit();
    });
    return false;
});
$(document).on('click', '[id*="changeProfile"]', function () {

    $('#userpicture').click();
});
$(document).on('change', '[id*="userpicture"]', function () {
    var formData = new FormData();
    var totalFiles = document.getElementById("userpicture").files.length;
    for (var i = 0; i < totalFiles; i++) {
        var file = document.getElementById("userpicture").files[i];
        formData.append("imageUploadForm", file);
    }
    $.ajax({
        type: "POST",
        url: '/common/FileSaveOnServer',
        data: formData,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (response) {
            $('#imguserpicture').attr('src', 'data:image/png;base64,' + response.Image);
            $('#picturename').val(response.fileName)
        },
        error: function (error) {
            $('#picturename').val('')
        }
    })
    return false;
});
/// <reference path="../../../../jquery-1.10.2.js" />
/// <reference path="../../../Common/App.js" />
/// <reference path="../../../Common/Utility.js" />
$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#roleid').val(data.RoleId);
    $('#roletype').val(data.RoleType);
    $('#description').val(data.Description === 'null' ? '' : data.Description);
    $('.popup').hide();
});

$(document).on('click', '[id*="btnSave"]', function () {
    $('#roleid').val('0');
    utility.setFormPostUrl('roletypeform', 'SetRoleType', 'master', 'EmployeesManagement');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('roletypeform', 'UpdateRoleType', 'master', 'EmployeesManagement');
        $('#roletypeform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('roletypeform', 'DeleteRoleType', 'master', 'EmployeesManagement');
        $('#roletypeform').submit();
    });
    return false;
});
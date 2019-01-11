/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#exptypeid').val(data.ExpTypeId);
    $('#exptype').val(data.ExpType);
    $('#exptypedesc').val(data.ExpTypeDesc === 'No Description' ? '' : data.ExpTypeDesc);
    $('.popup').hide();
});

$(document).on('click', '[id*="btnSave"]', function () {
    $('#exptypeid').val('0');
    utility.setFormPostUrl('exptypeform', 'SetExpenseType', 'master', 'ExpenseManagement');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('exptypeform', 'UpdateExpenseType', 'master', 'ExpenseManagement');
        $('#exptypeform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('exptypeform', 'DeleteExpenseType', 'master', 'ExpenseManagement');
        $('#exptypeform').submit();
    });
    return false;
});
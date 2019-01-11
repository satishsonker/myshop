$(document).ready(function () {
    utility.bindDdlByAjax(app.urls.CommonController.GetExpenseTypeSelectList, 'exptypeid', 'Text', 'Value');
});

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#expitem').val(data.ExpItem);
    $('#exptypeid').val(data.ExpTypeId);
    $('#expitemid').val(data.ExpItemId);
    $('#expitemdesc').val(data.ExpItemDesc === 'No Description' ? '' : data.ExpItemDesc);
    $('.popup').hide();
});

$(document).on('click', '[id*="btnSave"]', function () {
    $('#expitemid').val('0');
    utility.setFormPostUrl('expitemform', 'SetExpenseItem', 'master', 'expensemanagement');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('expitemform', 'UpdateExpenseItem', 'master', 'expensemanagement');
        $('#expitemform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to Delete.', function () {
        utility.setFormPostUrl('expitemform', 'DeleteExpenseItem', 'master', 'expensemanagement');
        $('#expitemform').submit();
    });
    return false;
});
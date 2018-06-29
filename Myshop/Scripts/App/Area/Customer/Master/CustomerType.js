/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#customertypeid').val(data.CustomerTypeId);
    $('#customertype').val(data.CustomerType);
    $('#description').val(data.Description === 'No Description' ? '' : data.Description);
    $('.popup').hide();
});

$(document).on('click', '[id*="btnSave"]', function () {
    $('#customertypeid').val('0');
    utility.setFormPostUrl('custtypeform', 'SetCustmerType', 'master', 'CustomersManagement');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('custtypeform', 'UpdateCustmerType', 'master', 'CustomersManagement');
        $('#custtypeform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to delete.', function () {
        utility.setFormPostUrl('custtypeform', 'DeleteCustmerType', 'master', 'CustomersManagement');
        $('#custtypeform').submit();
    });
    return false;
});
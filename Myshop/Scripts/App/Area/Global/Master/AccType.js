/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#accounttypeid').val(data.AccountTypeId);
    $('#accounttype').val(data.AccountType);
    $('#accounttypedesc').val(data.Description === 'No Description' ? '' : data.Description);
    $('.popup').hide();
});

$(document).on('click', '[id*="btnSave"]', function () {
    $('#accounttypeid').val('0');
    utility.setFormPostUrl('acctypeform', 'SetAccountType', 'masters', 'Global');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('acctypeform', 'UpdateAccountType', 'masters', 'Global');
        $('#acctypeform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('acctypeform', 'DeleteAccountType', 'masters', 'Global');
        $('#acctypeform').submit();
    });
    return false;
});
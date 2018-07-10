/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).ready(function () {
    utility.bindDdlByAjax(app.urls.GetShopSelectList, 'ddlshop');
});

// Popup table row button select function

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#bankid').val(data.BankId);
    $('#acctypeid').val(data.AccTypeId);
    $('#accountname').val(data.AccountName);
    $('#accountholdername').val(data.AccHolderName);
    $('#accountno').val(data.AccountNo);
    $('#branchname').val(data.BranchName);
    $('#branchifsc').val(data.BranchIFSC);
    $('#branchaddress').val(data.BranchAddress);
    $('.popup').hide(); 
});


$(document).on('click', '[id*="btnSave"]', function () {
    $('#bankaccid').val('0');
    utility.setFormPostUrl('logoform', 'setlogo', 'masters', 'Global');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('bankaccform', 'UpdateBankAccount', 'masters', 'Global');
        $('#bankaccform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to delete.', function () {
        utility.setFormPostUrl('bankaccform', 'DeleteBankAccount', 'masters', 'Global');
        $('#bankaccform').submit();
    });
    return false;
});
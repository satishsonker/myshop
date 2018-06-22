/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).ready(function () {
    utility.bindDdlByAjax(app.urls.GetBanksUrl, 'bankid');
    utility.bindDdlByAjax(app.urls.GetAccTypeUrl, 'acctypeid');
    //fillBankDDL();
    //fillAccTypeDDL();
});

//function fillBankDDL() {
//    var urls = $('#GetBanksUrl').data('request-url');
//    utility.ajaxHelper(urls,{shopid:0}, function (data) {
//        var ddl = $('#bankid');
//        utility.enableCtrl(ddl);
//        ddl.find(':gt(0)').remove();
//        $(data).each(function (ind, ele) {
//            ddl.append('<option value=' + ele.Value + '>' + ele.Text + '</option>');
//        });
//    });
//}

//function fillAccTypeDDL() {
//    var urls = $('#GetAccTypeUrl').data('request-url');
//    utility.ajaxHelper(urls,{shopid:0}, function (data) {
//        var ddl = $('#acctypeid');

//        utility.enableCtrl(ddl);

//        ddl.find(':gt(0)').remove();
//        $(data).each(function (ind, ele) {
//            ddl.append('<option value=' + ele.Value + '>' + ele.Text + '</option>');
//        });
//    });
//};

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
    utility.setFormPostUrl('bankaccform', 'SetBankAccount', 'masters', 'Global');
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
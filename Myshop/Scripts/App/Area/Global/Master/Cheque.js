/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).ready(function () {
    //fillBankDDL();
    utility.bindDdlByAjax(app.urls.GetBankAccUrl, 'bankaccid');
});

//function fillBankDDL() {
//    var urls = $('#GetBankAccUrl').data('request-url');
//    utility.ajaxHelper(urls,{shopid:0}, function (data) {
//        var ddl = $('#bankaccid');
//        utility.enableCtrl(ddl);
//        ddl.find(':gt(0)').remove();
//        $(data).each(function (ind, ele) {
//            ddl.append('<option value=' + ele.Value + '>' + ele.Text + '</option>');
//        });
//    });
//}


// Popup table row button select function

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#desc').val(data.Description);
    $('#bankaccid').val(data.BankAccId);
    $('#pagesize').val(data.PageSize);
    $('#pagestartno').val(data.PageStartNo);
    $('#pageendno').val(data.PageEndNo);
    $('#desc').val(data.Desc);
    // $('#issuedate').val(new Date(parseInt(data.IssueDate.substr(6))).toDateString());
    document.getElementById("issuedate").valueAsDate = new Date(parseInt(data.IssueDate.substr(6))) ;
    $('#chequeid').val(data.ChequeId);
    $('.popup').hide(); 
});


$(document).on('click', '[id*="btnSave"]', function () {
    $('#chequeid').val('0');
    utility.setFormPostUrl('chequeform', 'SetCheque', 'masters', 'Global');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('chequeform', 'UpdateCheque', 'masters', 'Global');
        $('#chequeform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to delete.', function () {
        utility.setFormPostUrl('chequeform', 'DeleteCheque', 'masters', 'Global');
        $('#chequeform').submit();
    });
    return false;
});

$(document).on('change', '[id="pagestartno"],[id="pazesize"]', function () {
    let pageSize = parseInt($('#pagesize').val());
    let pageEndNo = $('#pageendno');
    let pageStartNo = parseInt($('#pagestartno').val());
    pageEndNo.val(pageSize + pageStartNo);
});
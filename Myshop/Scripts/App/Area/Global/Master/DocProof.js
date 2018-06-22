/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).ready(function () {
    utility.bindDdlByAjax(app.urls.GetDocProofTypesListJson, 'docprooftypeid');
});


// Popup table row button select function

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#docproofid').val(data.DocProofId);
    $('#docprooftypeid').val(data.DocProofTypeId);
    $('#DocProof').val(data.DocProof);
    $('#Description').val(data.Description);
    $('.popup').hide();
});


$(document).on('click', '[id*="btnSave"]', function () {
    $('#docproofid').val('0');
    utility.setFormPostUrl('doctypeform', 'SetDocProof', 'masters', 'Global');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('doctypeform', 'UpdateDocProof', 'masters', 'Global');
        $('#doctypeform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to delete.', function () {
        utility.setFormPostUrl('doctypeform', 'DeleteDocProof', 'masters', 'Global');
        $('#doctypeform').submit();
    });
    return false;
});
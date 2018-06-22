/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#docprooftypeid').val(data.DocProofTypeId);
    $('#docprooftype').val(data.DocProofType);
    $('#description').val(data.Description === 'No Description' ? '' : data.Description);
    $('.popup').hide();
});

$(document).on('click', '[id*="btnSave"]', function () {
    $('#docprooftypeid').val('0');
    utility.setFormPostUrl('docprooftypeform', 'SetDocProofType', 'masters', 'Global');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('docprooftypeform', 'UpdateDocProofType', 'masters', 'Global');
        $('#docprooftypeform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('docprooftypeform', 'DeleteDocProofType', 'masters', 'Global');
        $('#docprooftypeform').submit();
    });
    return false;
});
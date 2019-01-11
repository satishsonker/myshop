/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).ready(function () {
    utility.bindDdlByAjax(app.urls.GetUserSelectList, 'owner');
});

// Popup table row button select function

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#shopid').val(data.ShopId);
    $('#email').val(data.Email);
    $('#owner').val(data.OwnerId);
    $('#mobile').val(data.Mobile);
    $('#isprimary').prop('checked',data.IsPrimary);
    $('#address').val(data.Address);
    $('#gstin').val(data.GSTIN);
    $('#name').val(data.Name);
    $('#ptlDdlState').val(data.StateId);
    $('#ptlDdlState').data('selectedcity',data.DistrictId);
    $('#ptlDdlState').change();
    
    $('.popup').hide();
});


$(document).on('click', '[id*="btnSave"]', function () {
    $('#shopid').val('0');
    utility.setFormPostUrl('shopform', 'SetShop', 'masters', 'Global');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('shopform', 'UpdateShop', 'masters', 'Global');
        $('#shopform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to delete.', function () {
        utility.setFormPostUrl('shopform', 'DeleteShop', 'masters', 'Global');
        $('#shopform').submit();
    });
    return false;
});
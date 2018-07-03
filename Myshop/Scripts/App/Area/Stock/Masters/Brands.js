/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#brandid').val(data.BrandId);
    $('#brandname').val(data.BrandName);
    $('#branddesc').val(data.Description === 'No Description' ? '' : data.Description);
    $('.popup').hide();
});

$(document).on('click', '[id*="btnSave"]', function () {
    $('#brandid').val('0');
    utility.setFormPostUrl('brandform', 'SetBrand', 'masters', 'StockManagement');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('brandform', 'UpdateBrand', 'masters', 'StockManagement');
        $('#brandform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('brandform', 'DeleteBrand', 'masters', 'StockManagement');
        $('#brandform').submit();
    });
    return false;
});
/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#ShopId').val(data.ShopId);
    $('#vendorid').val(data.VendorId);
    $('#vendorname').val(data.VendorName);
    $('#vendormobile').val(data.VendorMobile);
    $('#vendoraddress').val(data.VendorAddress);
    $('#vendordesc').val(data.Description === 'No Description' ? '' : data.Description);
    $('.popup').hide();
});

$(document).on('click', '[id*="btnSave"]', function () {
    $('#vendorid').val('0');
    utility.setFormPostUrl('vendorform', 'SetVendor', 'masters', 'StockManagement');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('vendorform', 'UpdateVendor', 'masters', 'StockManagement');
        $('#vendorform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('vendorform', 'DeleteVendor', 'masters', 'StockManagement');
        $('#vendorform').submit();
    });
    return false;
});
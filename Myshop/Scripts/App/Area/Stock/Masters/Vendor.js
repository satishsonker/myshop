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
    if (!$vendor.isMobileValid()) {
        return false;
    }
    $('#vendorid').val('0');
    utility.setFormPostUrl('vendorform', 'SetVendor', 'masters', 'StockManagement');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    if (!$vendor.isMobileValid()) {
        return false;
    }
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('vendorform', 'UpdateVendor', 'masters', 'StockManagement');
        $('#vendorform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    if (!$vendor.isMobileValid()) {
        return false;
    }
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('vendorform', 'DeleteVendor', 'masters', 'StockManagement');
        $('#vendorform').submit();
    });
    return false;
});

var $vendor = {};

$vendor.isMobileValid = function () {
    let $mobile = $('#vendormobile').val();
    if (!app.const.regex.mobile.test($mobile)) {
        utility.SetAlert('Invalid mobile number', utility.alertType.warning);
        return false;
    }
    else
        return true;
}

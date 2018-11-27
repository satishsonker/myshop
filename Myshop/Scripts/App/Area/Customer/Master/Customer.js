/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).ready(function () {
    utility.bindDdlByAjax(app.urls.GetCustomerTypeSelectListJson, 'customertypeid');
});

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#customerid').val(data.CustomerId);
    $('#customertypeid').val(data.CustomerTypeId);
    $('#firstname').val(data.FirstName);
    $('#middlename').val(data.MiddleName);
    $('#lastname').val(data.LastName);
    $('#mobile').val(data.Mobile);
    $('#email').val(data.Email);
    $('#address').val(data.Address);
    $('#ptlDdlState').val(data.State);
    $('#ptlDdlState').change();
    var $selectCity = setInterval(function () {        
        $('#ptlDdlCity').val(data.District);
        if ($('#ptlDdlCity').val() == data.District)
            clearInterval($selectCity);
        },500);
    $('#pincode').val(data.PINCode);
    $('.popup').hide();
});

$(document).on('click', '[id*="btnSave"]', function () {
    $('#customerid').val('0');
    utility.setFormPostUrl('customerform', 'SetCustmer', 'master', 'CustomersManagement');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('customerform', 'UpdateCustmer', 'master', 'CustomersManagement');
        $('#customerform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to delete.', function () {
        utility.setFormPostUrl('customerform', 'DeleteCustmer', 'master', 'CustomersManagement');
        $('#customerform').submit();
    });
    return false;
});
﻿/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#ShopId').val(data.ShopId);
    $('#unitid').val(data.UnitId);
    $('#unitname').val(data.UnitName);
    $('#catdesc').val(data.Description === 'No Description' ? '' : data.Description);
    $('.popup').hide();
});

$(document).on('click', '[id*="btnSave"]', function () {
    $('#unitid').val('0');
    utility.setFormPostUrl('unitform', 'SetUnit', 'masters', 'StockManagement');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('unitform', 'UpdateUnit', 'masters', 'StockManagement');
        $('#unitform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to delete.', function () {
        utility.setFormPostUrl('unitform', 'DeleteUnit', 'masters', 'StockManagement');
        $('#unitform').submit();
    });
    return false;
});
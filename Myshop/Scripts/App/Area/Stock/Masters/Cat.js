﻿/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#ShopId').val(data.ShopId);
    $('#catid').val(data.CatId);
    $('#catname').val(data.CatName);
    $('#catdesc').val(data.Description === 'No Description' ? '' : data.Description);
    $('.popup').hide();
});

$(document).on('click', '[id*="btnSave"]', function () {
    $('#catid').val('0');
    utility.setFormPostUrl('catform', 'SetCategory', 'masters', 'StockManagement');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('catform', 'UpdateCategory', 'masters', 'StockManagement');
        $('#catform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to delete.', function () {
        utility.setFormPostUrl('catform', 'DeleteCategory', 'masters', 'StockManagement');
        $('#catform').submit();
    });
   return false;
});
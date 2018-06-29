/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../Common/App.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).ready(function () {
    fillCatDDL();
});

$(document).on('change', '#ShopId', function () {
    var ele = $('#ShopId');
    var selectedId = parseInt(ele.find(':selected').val());
    var ddl = $('#catid');
    ddl.find(':gt(0)').remove();
    ddl.attr('disabled', 'disabled').addClass('disableCtrl');
    if (isNaN(selectedId) || selectedId < 1) {
        utility.SetAlert("Please select Shop", "warning");
    } else {
        fillCatDDL();
    }
});

function fillCatDDL() {
    var ele = $('#ShopId');
    var selectedId = parseInt(ele.find(':selected').val());
    var urls = app.urls['GetCatogariesUrl'];
    //selectedId = isNaN(selectedId) ? 0 : selectedId;
    
    //var ddl = $('#catid');
    utility.bindDdlByAjaxWithParam(urls, 'catid', { Shopid: selectedId });
    //utility.ajaxHelper(urls, { Shopid: selectedId }, function (data) {
       
    //    if (typeof data === 'object') {
    //        $(ddl).removeAttr('disabled').removeClass('disableCtrl');           
    //        $(data).each(function (ind, ele) {
    //            ddl.append('<option value=' + ele.Value + '>' + ele.Text + '</option>');
    //        });
    //    }
    //});
}

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);

    var data = $(this).data('data');
    var eleShop = $('#ShopId');

    if (parseInt(eleShop.val()) != data.ShopId) {
        $(eleShop).val(data.ShopId);
    }

    $('#subcatid').val(data.SubCatId);
    $('#catid').val(data.CatId);
    $('#subcatname').val(data.SubCatName);
    $('#subcatdesc').val(data.Description === 'No Description' ? '' : data.Description);
    $('.popup').hide();
});

$(document).on('click', '[id*="btnSave"]', function () {
    $('#subcatid').val('0');
    utility.setFormPostUrl('subcatform', 'SetSubCategory', 'masters', 'StockManagement');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('subcatform', 'UpdateSubCategory', 'masters', 'StockManagement');
        $('#subcatform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to delete.', function () {
        utility.setFormPostUrl('subcatform', 'DeleteSubCategory', 'masters', 'StockManagement');
        $('#subcatform').submit();
    });
    return false;
});
/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />
/// <reference path="../../../Common/App.js" />

$(document).ready(function () {
    $('#ShopId').change();
});

$(document).on('change', '#ShopId', function () {
    if ($(this).find(':selected').val() > 0) {
        fillCatDDL();
        fillUnitDDL();
    }
    else
    {
        utility.disableCtrl(['catid', 'brandid', 'unitid']);
    }
    utility.disableCtrl('subcatid');
});

$(document).on('change', '#catid', function () {
    
    if ($(this).find(':selected').val() > 0) {
        fillSubCatDDL();
    }
    else {
        utility.disableCtrl('subcatid');
    }
});

function fillCatDDL() {
    var ele = $('#ShopId');
    var selectedId = parseInt(ele.find(':selected').val());
    if (selectedId < 1) {
        utility.SetAlert("Please select Shop", "warning");
    }
    else {
        utility.bindDdlByAjaxWithParam(app.urls.GetCatogariesUrl, 'catid', { 'Shopid': selectedId }, undefined, undefined,undefined, function () {
            utility.enableCtrl(ddl);
        });
    }
}

function fillSubCatDDL() {
    var eleCat = $('#catid');
    var CatselectedId = parseInt(eleCat.find(':selected').val());
    var eleShop = $('#ShopId');
    var ShopselectedId = parseInt(eleShop.find(':selected').val());
    if (CatselectedId < 1 || ShopselectedId<1) {
        utility.SetAlert("Please select Shop/Category", "warning");
    }
    else {
        utility.bindDdlByAjaxWithParam(app.urls.GetSubCatogariesUrl, 'subcatid',  { 'catid': CatselectedId,'shopId':ShopselectedId }, undefined, undefined, undefined, function () {
            utility.enableCtrl(ddl);
        });
    }
}

function fillBrandDDL() {
    var ele = $('#ShopId');
    var selectedId = parseInt(ele.find(':selected').val());
    if (selectedId < 1) {
        utility.SetAlert("Please select Shop", "warning");
    }
    else {
        utility.bindDdlByAjaxWithParam(app.urls.GetBrandUrl, 'brandid', { Shopid: selectedId }, undefined, undefined, undefined, function () {
            utility.enableCtrl(ddl);
        });
    }
}

function fillUnitDDL() {
    var ele = $('#ShopId');
    var selectedId = parseInt(ele.find(':selected').val());
    if (selectedId < 1) {
        utility.SetAlert("Please select Shop", "warning");
    }
    else {
        utility.bindDdlByAjaxWithParam(app.urls.GetUnitUrl, 'unitid', { Shopid: selectedId }, undefined, undefined, undefined, function () {
            utility.enableCtrl(ddl);
        });
    }
}

// Popup table row button select function

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    if (parseInt($('#ShopId').val()) !== data.ShopId) {
        $('#ShopId').val(data.ShopId).change();
    }
    $('#productname').val(data.ProductName);
    $('#minquantity').val(data.MinQuantity);
    $('#productcode').val(data.ProductCode);
    $('#color').val(data.ProductColor);
    $('#purchaseprice').val(data.PurchasePrice);
    $('#sellprice').val(data.SellPrice);
    $('#desc').val(data.Description);
    $('#ShopId').val(data.ShopId);
    $('#productid').val(data.ProductId);
    $('.popup').hide();   
  
   
    var x = setInterval(function() {
        if ($('#unitid').attr('disabled') === undefined)
        {
            $('#catid').val(data.CatId).change();
            $('#unitid').val(data.UnitId);
            clearInterval(x);
            var y = setInterval(function () {
                if ($('#subcatid').attr('disabled') === undefined) {
                    $('#catid').val(data.CatId);
                    $('#subcatid').val(data.SubCatId);
                    $('#unitid').val(data.UnitId);
                    clearInterval(y);
                }
            }, 500)
        }
    },500)
});


$(document).on('click', '[id*="btnSave"]', function () {
    $('#productid').val('0');
    utility.setFormPostUrl('proform', 'SetProduct', 'masters', 'StockManagement');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('proform', 'UpdateProduct', 'masters', 'StockManagement');
        $('#proform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to delete.', function () {
        utility.setFormPostUrl('proform', 'DeleteProduct', 'masters', 'StockManagement');
        $('#proform').submit();
    });
    return false;
});
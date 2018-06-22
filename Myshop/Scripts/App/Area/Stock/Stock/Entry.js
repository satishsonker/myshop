/// <reference path="../../../../jquery-1.10.2.js" />
/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../Common/App.js" />
/// <reference path="../../../Common/Validate.js" />

//Global Variable
let selectProducts = [];
//

$(document).ready(function () {
    utility.bindDdlByAjax('GetVendorListUrl', 'vendorid', 'Text', 'Value');
    utility.bindDdlByAjax('GetPaymodeListUrl', 'paymodeid', 'Text', 'Value');
    utility.bindDdlByAjax(app.urls.GetCatListUrl, 'cat__0', 'Text', 'Value', function () {
        $('.addRow').data('newrow', $('.tbl #producttable tbody').html());
    });   

    selectProducts = []; //Set it blank of each refresh
});

$(document).on('click', 'input[value="Cancel"]', function () {
    $(this).parents().find('input[type="text"]').val('');
    $(this).parents().find('input[type="number"]').val('0.00');
    $('.tbl #producttable tbody tr:gt(0)').remove();
    $(this).parents().find('select').val('').change();
    utility.enableCtrl('cat__0', false);
});

$(document).on('change', '#totalamt,#additionalamt,#paidamt,#remainingamt', function () {
    var total = $('#totalamt');
    total.val(utility.setFraction(total.val()));
    total = parseFloat(total.val());
    total = isNaN(total) ? 0 : total;

    var addAmt = $('#additionalamt');
    addAmt.val(utility.setFraction(addAmt.val()));
    addAmt = parseFloat(addAmt.val());
    addAmt = isNaN(addAmt) ? 0 : addAmt;

    var paidAmt = $('#paidamt');
    paidAmt.val(utility.setFraction(paidAmt.val()));
    paidAmt = parseFloat(paidAmt.val());
    paidAmt = isNaN(paidAmt) ? 0 : paidAmt;

    var RemAmt = ((total + addAmt) - paidAmt);
    if (RemAmt < 0) {
        utility.createToast('You have enter some invalid amount, Remainig Amount should be 0.00 or greater.!', app.const.toastColor.red);
    }

    $('#remainingamt').val((RemAmt).toFixed(2));
});

$(document).on('change', '#debitaccountid', function (e,data) {
    var selectCheque = $('#ChequePageId');
    if ($('select[id="paymodeid"]').find(':selected').text().toLowerCase().indexOf('cheque') > -1) {
        var accId = parseInt($(this).find(':selected').val());
        if (accId > 0 && !isNaN(accId)) {
            utility.setRequired([selectCheque], true);
            $(selectCheque).val('');
            utility.bindDdlByAjaxWithParam('GetChequeListUrl', 'ChequePageId', { 'AccId': accId,'isallcheque':true }, 'Text', 'Value', undefined, function() {
                $('#ChequePageId').val(data);
            });
        }
    }
    else
    {
        utility.setRequired([selectCheque], false);
        $(selectCheque).val('0');
    }
});

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#stockid').val(data.StockId);
    $('#vendorid').val(data.VendorId);
    $('#paymodeid').val(data.PayModeId).trigger('change', { debit: data.DebitAccount, cheque: data.ChequePageId });
    $('#vendorreceiptno').val(data.VendorReceiptNo);
    $('#vendorreceiptdate').val(utility.getFormatedDate(data.VendorReceiptDate));
    $('#shopreceiptentryno').val(data.ShopReceiptEntryNo);
    $('#totalamt').val(data.TotalAmt);
    $('#additionalamt').val(utility.setFraction(data.AdditionalAmt));
    $('#paidamt').val(data.PaidAmt);
    $('#remainingamt').val(data.RemainingAmt);
    $('.popup').hide();

    utility.ajaxHelper(app.urls.GetStockDetailsJosn, { 'stockId': data.StockId }, function (response) {
        var rr = response;
        $('#producttable').parent().show();
        $('#producttable tbody tr:gt(0)').remove();
        $(response).each(function (ind, ele) {                   
            if(ind>0)
            {
                $('.addRow')[0].click();
            }
            var cat = $('#cat__' + ind);
            var subCat = $('#catsub__' + ind);
            var brand = $('#brand__' + ind);
            var product = $('#product__' + ind);
            var pp = $('#pp__' + ind);
            var sp = $('#sp__' + ind);
            var quat = $('#quat__' + ind);
            var desc = $('#desc__' + ind);
            var color = $('#color__' + ind);//
            $('#removerow__' + ind).remove();
            $('#StockTrId__' + ind).val(ele.StockTrId);
            desc.val(ele.Description);
            color.val(ele.Color);
            cat.val(ele.CatId);
            subCat.append('<option selected value="' + ele.SubCatId + '">' + ele.SubCatName + '</option>');
            product.append('<option selected value="' + ele.ProductId + '">' + ele.ProductName + '</option>');
            brand.append('<option selected value="' + ele.BrandId + '">' + ele.BrandName + '</option>');
            pp.val(utility.setFraction(ele.PurchasePrice));
            quat.val(utility.setFraction(ele.Qty));
            sp.val(utility.setFraction(ele.SellPrice));
            utility.enableCtrl([pp, sp, quat, desc, color], true);
            utility.disableCtrl([cat], true);
            pp.change();
            $('#addrow__' + ind).data('crudtype', 'update');
        });
    }, utility.errorCall)
});

$(document).on('change', 'select[id="paymodeid"]', function (e,data) {
    var selectedOption = $(this).find(':selected');
    var selectCheque = $('#ChequePageId');
    var selectAcc = $('#debitaccountid');
    var payModeId = $(selectedOption).val();
    var payModeText = $(selectedOption).text().toLowerCase();
    utility.setClass([$(selectAcc).parent(), $(selectCheque).parent()], 'hideCtrl', true);

    utility.setRequired([selectCheque, selectAcc], false);
    $(selectCheque).val('0'); // bypass server validation
    $(selectAcc).val('0');

    if (payModeText !== 'cash' && payModeId > 0) {
        if (payModeText === 'cheque') {
            $(selectCheque).parent().removeClass('hideCtrl').addClass('showCtrl');
            utility.setRequired([selectCheque], true);
            $(selectCheque).val(''); // Make Required enable
        }
        else
        {
            utility.setRequired([selectCheque], false);
            $(selectCheque).val('0');
        }
        $(selectAcc).parent().removeClass('hideCtrl').addClass('showCtrl');
        utility.setRequired([selectAcc], true);
        $(selectAcc).val(''); // Make Required enable
        utility.bindDdlByAjax('GetBankAccListUrl', 'debitaccountid', 'Text', 'Value', function () {
            if(data)
            $('#debitaccountid').val(data.debit).trigger('change', data.cheque);
        });
    }
    else
    {
        utility.setRequired([selectCheque, selectAcc], false);
        $(selectCheque).val('0');
        $(selectAcc).val('0');
    }
});

$(document).on('click', '[id*="btnSave"]', function () {
    $('#stockid').val('0');
    utility.setFormPostUrl('stockentryform', 'SetStockEntry', 'stock', 'StockManagement');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('stockentryform', 'UpdateStockEntry', 'stock', 'StockManagement');
        $('#stockentryform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('stockentryform', 'DeleteStockEntry', 'stock', 'StockManagement');
        $('#stockentryform').submit();
    });
    return false;
});

$(document).on('click', '.addRow', function () {
    var rowHTML = $(this).data('newrow');

    var trs = $('.tbl #producttable tbody tr');

    rowHTML = rowHTML.replace(/__0/g, '__' + (trs.length).toString()); // Replace Id
    rowHTML = rowHTML.replace(/\[0\]./g, '' + (trs.length).toString() + ']');  // Replace Name
    $('.tbl #producttable tbody').append(rowHTML);
    $('#total_price__' + trs.length.toString()).text('00.00');
    //utility.bindDdlByAjax(app.urls.GetCatListUrl, 'cat__' + trs.length.toString());

    trs = $('.tbl #producttable tbody tr'); //Refresh selection after append the row

    $(trs).each(function (ind, ele) {  // Refresh the Row/Serial No.
        $(ele).find('td:eq(0)').text((ind + 1).toString() + '.');
    });
});

$(document).on('click', '.removeRow', function () {
    if ($(this).parent().parent().parent().parent().find('tr').length > 1) {
        $(this).parent().parent().parent().remove();
    }
    else {
        utility.createToast("You can't remove this row");
    }

    $('.tbl #producttable tbody tr').each(function (ind, ele) {
        $(ele).find('td:eq(0)').text((ind + 1).toString() + '.');
    });
});

$(document).on('click', '.resetRow', function () {
    if ($(this).data('crudtype') === 'insert') {
        $(this).parent().parent().parent().find('input[id*="pp__"]').val('0.00').change();
        $(this).parent().parent().parent().find('select[id*="cat__"]').val('').change();
    }
    else if ($(this).data('crudtype') === 'update')
    {
        $(this).parent().parent().parent().find('input[id*="pp__"]').val('0.00').change();
        $(this).parent().parent().parent().find('input[id*="quat__"]').val('0.00')
    }
});

$(document).on('change', 'select[id*="cat__"]', function () {
    var val = parseInt($(this).find(':selected').val());
    var id = parseInt($(this).attr('id').split('__')[1]).toString();
    var ctrlArray = ['sp__' + id, 'pp__' + id, 'quat__' + id, 'color__' + id, 'desc__' + id, 'product__' + id, 'brand__' + id, 'catsub__' + id];
    if (!isNaN(val) && val > 0) {
        utility.bindDdlByAjaxWithParam(app.urls.GetSubCatogariesUrl, 'catsub__' + id, { 'catid': val, 'shopId': 1 }, undefined, undefined, undefined, function () {
            utility.enableCtrl('catsub__' + id,false);
        });
    }
    else {
        utility.disableCtrl(ctrlArray);
    }
});

$(document).on('change', 'select[id*="catsub__"]', function () {
    var val = parseInt($(this).find(':selected').val());
    var id = parseInt($(this).attr('id').split('__')[1]).toString();
    var ctrlArray = ['sp__' + id, 'pp__' + id, 'quat__' + id, 'color__' + id, 'desc__' + id, 'product__' + id, 'brand__' + id];
    if (!isNaN(val) && val > 0) {
        utility.bindDdlByAjaxWithParam(app.urls.GetProductsUrl, 'product__' + id, { 'subcatid': val, 'shopId': 1 }, 'ProductName', 'ProductId', undefined, function () {
            utility.enableCtrl('product__' + id,false);
        });

        utility.bindDdlByAjaxWithParam(app.urls.GetBrandListUrl, 'brand__' + id, { 'subcatid': val, 'shopId': 1 }, undefined, undefined, undefined, function () {
            utility.enableCtrl('brand__' + id, false);
        });

    }
    else {
        utility.disableCtrl(ctrlArray);
    }
});

$(document).on('change', 'select[id*="product__"]', function () {
    var val = parseInt($(this).find(':selected').val());
    var id = parseInt($(this).attr('id').split('__')[1]).toString();
    var ctrlArray = ['sp__' + id, 'pp__' + id, 'quat__' + id, 'color__' + id, 'desc__' + id];
    if (!isNaN(val) && val > 0) {
        var selectProduct = $('#cat__' + id).val() + $('#catsub__' + id).val() + $('#brand__' + id).val() + $('#product__' + id).val();
        if ($.inArray(selectProduct, selectProducts) == -1) {
            selectProducts[id] = selectProduct;
            utility.enableCtrl(ctrlArray,false);
        }
        else {
            selectProducts[id] = '';
            utility.createToast('You have already seleted this product', app.const.toastColor.red);
            utility.disableCtrl(ctrlArray);
        }
    }
    else {
        utility.disableCtrl(ctrlArray);
        selectProducts[id] = '';
    }
});

$(document).on('change', 'input[id*="pp__"],input[id*="quat__"]', function () {

    var accumulatedAmount = 0.00, total = 0.00;

    $('.tbl #producttable tbody tr').each(function (ind, ele) {
        var purchasePrice = parseFloat($('input[id*="pp__' + ind + '"]').val());

        $('input[id*="pp__' + ind + '"]').val(parseFloat($('input[id*="pp__' + ind + '"]').val()).toFixed(2));
        $('input[id*="sp__' + ind + '"]').val(parseFloat($('input[id*="pp__' + ind + '"]').val()).toFixed(2));
        $('input[id*="quat__' + ind + '"]').val(parseInt($('input[id*="quat__' + ind + '"]').val()).toFixed(2)); // Remove fraction point from Quantity

        var quantity = parseFloat($('input[id*="quat__' + ind + '"]').val());

        total = ((isNaN(purchasePrice) ? 0 : purchasePrice) * (isNaN(quantity) ? 0 : quantity));

        $('input[id*="quat__' + ind + '"]').parent().next('td').text((total).toFixed(2));

        accumulatedAmount += total;
    });

    var id = parseInt($(this).attr('id').split('__')[1]).toString();
    $('#totalamt').val(parseFloat(accumulatedAmount).toFixed(2)).change();

    $('#grandTotal').text(parseFloat(accumulatedAmount).toFixed(2));
});

$(document).scroll(function () {
    var sc_position = $(window).scrollTop();
    if(sc_position>60)
    {
       // alert(sc_position);
    }
});
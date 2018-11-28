﻿$(document).on('click', '#_ptlSearchInvoiceGenerate', function () {
    let $data = $('#_ptlSearchInvoiceGenerate').data('productinfo');
    let $tbody = $('#tblInvoice');
    let $html = '';
    $($data.Products).each(function ($ind, $ele) {
        $html += '<tr>' +
            '<td class="shop_vMiddle"><input type="checkbox" class="form-inline form-control" id="chkPro_' + $ind + '" /></td>' +
            '<td class="shop_vMiddle">' + ($ind + 1) + '.</td >' +
            '<td class="shop_vMiddle">' + $ele.ProductName + '</td > ' +
            '<td class="shop_vMiddle">' + parseFloat($ele.Discount).toFixed(2) + '</td> ' +
            '<td class="shop_vMiddle">' + ($ele.Remark == null ? '' : $ele.Remark) + '</td>' +
            '<td class="shop_vMiddle shop_hCentre">' + parseFloat($ele.SalePrice).toFixed(2) + '</td>' +
            '<td class="shop_vMiddle shop_hCentre">' + $ele.Qty + '</td>' +
            '<td class="shop_vMiddle shop_hRigth">' + (($ele.Qty * $ele.SalePrice) - parseFloat($ele.Discount)).toFixed(2) + '</td>' +
            '<td class="shop_vMiddle shop_hRigth">0</td>' +
            '<td class="shop_vMiddle shop_hRigth"></td>' +
            '<td class="shop_vMiddle shop_hRigth lblReturnAmount">0.00</td>' +
            '</tr>';
    });

    $('.defaultRow').remove();
    $('#invPre').text($('#invPre').text().replace('$invoiceNo', $data.InvoiceId));
    $('#invPre').text($('#invPre').text().replace('$invoiceDate', utility.getJsDateTimeFromJson($data.InvoiceDate)));
    $('#custPre').text($('#custPre').text().replace('$name', $data.CustomerName));
    $('#custPre').text($('#custPre').text().replace('$address', $data.CustomerAddress));
    $('.tdSubTotal').before($html);
    $('#lblSubTotal').text(parseFloat($data.SubTotalAmount).toFixed(2));
    $('#lblGst').text($data.GstAmount);
    $('#lblGrandAmount').text($data.GrandTotal);
    $('#lblGrandTotalWord').text(utility.currentyInWords($data.GrandTotal.toFixed(0)));
});

$(document).on('click', '#chkSelectAll', function () {
    //if ($(this).is(':checked')) {
    $('input[id*="chkPro_"]').prop('checked', !$(this).is(':checked')).click();
    //}
});

$(document).on('click', 'input[id*="chkPro_"]', function () {
    let $currentTr = $(this).parent().parent();
    let $trIndex = $($currentTr).index();
    let $maxQty = $($currentTr).find('td:eq(6)').text().trim();

    if ($(this).is(':checked')) {       
        $($currentTr).find('td:eq(8)').empty().append('<input type="number" value="0" min="0" max="' + $maxQty + '" id="txtRtnQty_' + $trIndex.toString() + '" class="form-control" style="width:100%;" />');
        $($currentTr).find('td:eq(9)').empty().append('<input type="text" autocomplete="off" value="" maxlength="250" id="txtRtnRmk_' + $trIndex.toString() + '" class="form-control" style="width:100px;" />');
    }
    else {
        $($currentTr).find('td:eq(8)').empty().text('0');
        $($currentTr).find('td:eq(9)').empty().text('');
    }
});

$(document).on('change', 'input[id*="txtRtnQty_"]', function () {
    let $currentTr = $(this).parent().parent();
    let $salePrice = parseFloat($($currentTr).find('td:eq(5)').text().trim());
    let $saleQty = parseInt($(this).val());
    let $saleDiscount = parseInt($($currentTr).find('td:eq(3)').text().trim());
    let totalAmount = parseFloat($salePrice * $saleQty) - $saleDiscount;
    let returnSubTotal = 0.00;
    $($currentTr).find('.lblReturnAmount').text(totalAmount.toFixed(2));

    $('.lblReturnAmount').each(function (ind, ele) {
        returnSubTotal += parseFloat($(ele).text());
    });
   
    $('#lblRtnSubTotal').text(returnSubTotal.toFixed(2));
    $('#lblRtnGst').text(parseFloat((returnSubTotal / 100) * 12).toFixed(2));
    $('#lblRtnGrandAmount').text(parseFloat(returnSubTotal + ((returnSubTotal / 100) * 12)).toFixed(2));
});

$(document).on('mouseover', 'input[id*="txtRtnRmk_"]', function () {
    $(this).parent().attr('title', $(this).val());
});

$(document).on('click', '#chkDefaultRow', function () {
    utility.SetAlert('Please search the invoice first', utility.alertType.warning);
});

$(document).on('click', '#btnSaveReturnInvoice', function () {
    if (!$('[id*="chkPro_"]').is(":checked")) {
        utility.SetAlert("Please select atleast one product to return..!", utility.alertType.warning);
    }
    else {
        let $invoiceId = $('#hdnInvoiceId').val();
        let $refundAmount = $('#lblRtnGrandAmount').text();
        if ($invoiceId === "0") {
            utility.SetAlert('Please search the invoice', utility.alertType.warning);
            return false;
        }
        let $data = {};
        $data.InvoiceId = $invoiceId;
        $data.RefundAmount = $refundAmount;
        $data.BalanceAmount = $invoiceId;
        $data.IsAmountRefunded = $invoiceId;
        $data.RefundPayModeId = $invoiceId;
        $data.Products = $invoiceId;
        utility.ajaxHelper(app.urls.SaleArea.SalesController.SaveReturnInvoice, $data, function (_responce) {
            utility.setAjaxAlert(_responce);
        });
    }
});
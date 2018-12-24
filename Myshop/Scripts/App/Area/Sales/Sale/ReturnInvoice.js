var $returnInvoice = {}
let $gstRate = 12.00;
$(document).ready(function () {
});

$(document).on('click', '#_ptlSearchInvoiceGenerate', function () {
    let $data = $('#_ptlSearchInvoiceGenerate').data('productinfo');
    let $html = '';
    $gstRate = $data.GstRate;
    $('#lblGstRate').text('GST @ ' + $gstRate + '%');
    $returnInvoice.resetInvoice(); //Set default table
   
    $('#hdnInvoiceId').val($data.InvoiceId);
    $($data.Products).each(function ($ind, $ele) {
        $html += '<tr data-proid="' + $ele.ProductId + '" class="' + ($ele.IsReturn || $data.IsCancelled ? "returned" : "") + '" title="' + ($ele.IsReturn ? "This product has been return on " + utility.getJsDateTimeFromJson($ele.ReturnDate) : "") + '">' +
            '<td class="shop_vMiddle"><input type="checkbox" ' + ($ele.IsReturn || $data.IsCancelled ? 'disabled="disabled"' : "")+' class="form-inline form-control" id="chkPro_' + $ind + '" /></td>' +
            '<td class="shop_vMiddle">' + ($ind + 1) + '.</td >' +
            '<td class="shop_vMiddle">' + $ele.ProductName + '</td > ' +
            '<td class="shop_vMiddle">' + parseFloat($ele.Discount).toFixed(2) + '</td> ' +
            '<td class="shop_vMiddle">' + ($ele.Remark == null ? '' : $ele.Remark) + '</td>' +
            '<td class="shop_vMiddle shop_hCentre">' + parseFloat($ele.SalePrice).toFixed(2) + '</td>' +
            '<td class="shop_vMiddle shop_hCentre">' + $ele.Qty + '</td>' +
            '<td class="shop_vMiddle shop_hRigth">' + (($ele.Qty * $ele.SalePrice) - parseFloat($ele.Discount)).toFixed(2) + '</td>' +
            '<td class="shop_vMiddle shop_hRigth">' + ($ele.IsReturn || $data.IsCancelled ? $ele.ReturnQty : "") + '</td>' +
            '<td class="shop_vMiddle shop_hRigth">' + ($ele.IsReturn || $data.IsCancelled ? $ele.ReturnRemark : "") + '</td>' +
            '<td class="shop_vMiddle shop_hRigth lblReturnAmount">' + ($ele.IsReturn || $data.IsCancelled ? parseFloat($ele.ReturnAmount).toFixed(2) : "") + '</td>' +
            '</tr>';
    });

    $('.defaultRow').remove();
    $('#invPre').text($('#invPre').text().replace('$invoiceNo', $data.InvoiceId));
    $('#invPre').text($('#invPre').text().replace('$invoiceDate', utility.getJsDateTimeFromJson($data.InvoiceDate)));
    $('#custPre').text($('#custPre').text().replace('$name', $data.CustomerName));
    $('#custPre').text($('#custPre').text().replace('$address', $data.CustomerAddress));
    $('.tdSubTotal').before($html);
    $('#lblSubTotal').text(parseFloat($data.SubTotalAmount).toFixed(2));
    $('#lblGst').text(parseFloat($data.GstAmount).toFixed(2));
    $('#lblGrandAmount').text(parseFloat($data.GrandTotal).toFixed(2));
    $('#lblGrandTotalWord').text(utility.currentyInWords($data.GrandTotal.toFixed(0)));

    $returnInvoice.markReturnRow();

    $('.invoicestatus').empty().append('Invoice Status : <span class="label label-' + ($data.IsCancelled ? 'danger' : ($data.IsRefund ? 'warning' : 'success')) + '">' + ($data.IsCancelled ? 'Cancelled' : ($data.IsRefund ? 'Return' : 'Billed')) + '</span>');
});

$(document).on('click', '#chkSelectAll', function () {
    //if ($(this).is(':checked')) {
    $('input[id*="chkPro_"]').prop('checked', $(this).is(':checked')).click();
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
        if (!$(ele).parent().hasClass('returned')) {  // Exclude sum of Return amount
            var $currentRowAmount = parseFloat($(ele).text());
            returnSubTotal += isNaN($currentRowAmount) ? 0.00 : $currentRowAmount;
        }
    });

    $('#lblRtnSubTotal').text(returnSubTotal.toFixed(2));
    let $gstAmount = ((returnSubTotal / 100) * $gstRate);
$('#lblRtnGst').text(parseFloat($gstAmount).toFixed(2));
    $('#lblRtnGrandAmount').text(parseFloat(returnSubTotal + $gstAmount).toFixed(2));
    $('#txtAmountToBePaid').val($('#lblRtnGrandAmount').text());
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
        let $payModeDdl = $('#_ptlPaymentMode');
        let $payModeRefNo = $('#_ptlPayReNo');
        let $data = {};
        let $products = [];
        let $flag = false;
        if ($invoiceId === "0") {
            utility.SetAlert('Please search the invoice', utility.alertType.warning);
            return false;
        }
        
        $data.InvoiceId = $invoiceId;
        $data.RefundAmount = $('#txtAmountToBePaid').val();
        $data.BalanceAmount = $('#txtBalanceAmount').val();
        $data.IsAmountRefunded = true;
        $data.RefundPayModeId = $('#_ptlPaymentMode option:selected').val();
        
        $('[id*="chkPro_"]:checked').each(function(ind,ele) {
            var $newReturn = {};
            var $txtRtnRemarks = $(ele).parent().parent().find('input[id*="txtRtnRmk_"]');
            var $txtRtnQty = $(ele).parent().parent().find('input[id*="txtRtnQty_"]');
            $newReturn.ProductId = $(ele).parent().parent().data('proid');
            $newReturn.ReturnQty = parseInt($($txtRtnQty).val());
            $newReturn.ReturnAmount = $(ele).parent().parent().find('.lblReturnAmount').text();
            $newReturn.ReturnRemark = $($txtRtnRemarks).val();
            if ($newReturn.ReturnQty > 0 && $newReturn.ReturnRemark === '') {
                utility.SetAlert('Please put the remarks', utility.alertType.warning);
                $($txtRtnRemarks).addClass('shop_hasError').focus();
                $flag = true;
                return false;
            }
            else {
                $($txtRtnRemarks).removeClass('shop_hasError');
            }
            if ($newReturn.ReturnQty <1) {
                utility.SetAlert('Please enter the return quantity', utility.alertType.warning);
                $($txtRtnQty).addClass('shop_hasError').focus();
                $flag = true;
                return false;
            }
            else {
                $($txtRtnQty).removeClass('shop_hasError');
            }

            $products.push($newReturn);
        });

        if ($flag)
            return false;  //Return if no remarks for return product

        if ($($payModeDdl).find('option:selected').val() === "") {
            utility.SetAlert('Please select the payment mode', utility.alertType.warning);
            $($payModeDdl).addClass('shop_hasError').focus();
            return false;
        }
        else {
            $($payModeDdl).removeClass('shop_hasError');
        }
        if ($($payModeRefNo).val() === "" && $($payModeRefNo).is(':visible')) {
            utility.SetAlert('Please enter payment reference number', utility.alertType.warning);
            $($payModeRefNo).addClass('shop_hasError').focus();
            return false;
        }
        else {
            $($payModeRefNo).removeClass('shop_hasError');
        }
       
        $data.PayModeId = $($payModeDdl).find('option:selected').val();
        $data.PayModeRefNo = $($payModeRefNo).val();
        $data.Products = $products;
        utility.ajaxHelper(app.urls.SaleArea.SalesController.SaveReturnInvoice, $data, function (_response) {           
                utility.setAjaxAlert(_response);
        });
    }
});

$returnInvoice.markReturnRow=function () {
    $('tr.returned td').css('background', '#ff5e0075');
}

$returnInvoice.calculateReturnAmount = function () {
    let $returnSubTotal = 0.00;
    $('#tblInvoicedetails tbody tr td:eq(10)').each(function (ind, ele) {
        var $currentReturnAmount = parseFloat($(ele).text());
        $returnSubTotal += (isNaN($currentReturnAmount) ? 0 : $currentReturnAmount);
    });
}

$returnInvoice.resetInvoice=function () {
    $('#txtAmountToBePaid').val('0');
    $('#txtBalanceAmount').val('0');
    $('#_ptlPaymentMode').val('');
    $('#tblInvoicedetails tbody tr:lt(' + ($('#tblInvoicedetails tbody tr').length - 3) + ')').remove();
    $('.tdSubTotal').before('<tr class="defaultRow">' +
        '<td class= "shop_vMiddle">1.</td>' +
        '<td class="shop_vMiddle">No Product were selected yet</td>' +
        '<td><input type="number" id="txtDiscount_0" class="form-control" /></td>' +
        '<td><input type="text" title="Put remark if discount is applicable" id="txtRemark_0" class="form-control" /></td>' +
        '<td class="shop_vMiddle shop_hCentre">0.00</td>' +
        '<td class="shop_vMiddle shop_hCentre"><input type="number" id="txtQty_0" class="form-control" /></td>' +
        '<td class="shop_vMiddle shop_hRigth">0.00</td>' +
        '<td class="shop_vMiddle shop_hCentre">' +
        '<div class="btn-group-sm">' +
        '<img style="width:25px;cursor:pointer;" title="Delete this row" src="../../Images/Icons/delete.png" />' +
        '<img style="width:25px;cursor:pointer;" title="Reset this row" src="../../Images/Icons/refresh.png" />' +
        '</div>' +
        '</td>' +
        '</tr>');
    $('#_ptlPaymentMode').val('');
    $('#_ptlPaymentMode').css('width', '100%');;
    $('#_ptlPayReNo').val('').hide();
    $('#lblSubTotal').text('0.00');
    $('#lblGst').text('0.00');
    $('#lblGrandAmount').text('0.00');
    $('#lblRtnSubTotal').text('0.00');
    $('#lblRtnGst').text('0.00');
    $('#lblRtnGrandAmount').text('0.00');
}


/// <reference path="../../common/app.js" />
/// <reference path="../../../jquery-1.10.2.js" />
/// <reference path="../../common/validate.js" />
/// <reference path="../../common/utility.js" />

var duplicateInvoice = {};

$(document).on('click', '#_ptlSearchInvoiceGenerate', function () {
    let $data = $('#_ptlSearchInvoiceGenerate').data('productinfo');
    let $tbody = $('#tblInvoice');
    let $html = '';
    $($data.Products).each(function ($ind, $ele) {
        $html += '<tr>' +
            '<td class="shop_vMiddle">' + ($ind+1) + '.</td >' +
            '<td class="shop_vMiddle">' + $ele.ProductName + '</td > ' +
            '<td>' + parseFloat($ele.Discount).toFixed(2) + '</td> ' +
            '<td>' + ($ele.Remark == null ? '' : $ele.Remark) + '</td>' +
            '<td class="shop_vMiddle shop_hCentre">' + parseFloat($ele.SalePrice).toFixed(2) + '</td>' +
            '<td class="shop_vMiddle shop_hCentre">' + $ele.Qty + '</td>' +
            '<td class="shop_vMiddle shop_hRigth">' + ($ele.Qty * $ele.SalePrice).toFixed(2) + '</td>' +
            '</tr>';
    });
    $('#tblInvoicedetails tbody tr:lt(' + ($('#tblInvoicedetails tbody tr').length - 3) + ')').remove();
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

$(document).ready(function () {
    $('#_ptlSearchInvoice').val(utility.getQueryStringValue('invoiceno')).keypress().keydown();
   var interval= setInterval(function () {
        $('#_ptlSearchInvoiceList li').each(function (ind, ele) {
            if ($(ele).text().indexOf(utility.getQueryStringValue('invoiceno')) > -1) {
                $(ele).click();
                $('#_ptlSearchInvoiceGenerate').click();
                clearInterval(interval);
            }
        });
    },100)
});
﻿/// <reference path="../../common/app.js" />
/// <reference path="../../../jquery-1.10.2.js" />
/// <reference path="../../common/validate.js" />
/// <reference path="../../common/utility.js" />

var $newSales = {};

$newSales.setCustomerRecord = function (data) {
    $('.customerAddress').text(data.FirstName + " " + data.LastName + ", \n" + data.Mobile + ",\n" + data.Email + ",\n" + data.Address + ",\n" + data.District + "," + data.State + "-" + data.PINCode);
    $('#divInvoiceDetails').show();
    $('#hdnCustomerId').val(data.CustomerId);
}
$(document).on('click', '[id*="btnDelete_"]', function () {
    $(this).parent().parent().parent().remove();
    let $tbody = $('#tblInvoicedetails tbody');
    let $totalRows = $($tbody).find('tr:lt(' + ($($tbody).find('tr').length - 3) + ')');
    $($totalRows).each(function (ind, ele) {
        $(ele).find('td:eq(0)').text((ind + 1) + '.');
    });
    $('[id*="txtQty_"],[id*="txtDiscount_"]').change();
});
$(document).on('click', '#_ptlSearchProductSave', function () {
    let $invoiceDetail = {};
    let $subTotal = parseFloat($('#lblSubTotal').text());
    let $gstAmount = (($subTotal / 100) * 12);
    let $grandAmount = $subTotal + $gstAmount;
    let $paidAmount = 1.00;
    $invoiceDetail.CustomerId = $('#hdnCustomerId').val();
    $invoiceDetail.PayModeId = 1;
    $invoiceDetail.SubTotalAmount = $subTotal;
    $invoiceDetail.GstAmount = $gstAmount;
    $invoiceDetail.PaidAmount = $paidAmount;
    $invoiceDetail.PayModeRefNo = 1;
    $invoiceDetail.GrandTotal = $grandAmount;
    $invoiceDetail.BalanceAmount = $grandAmount - $paidAmount;
    $invoiceDetail.Products = [];
    $('#tblInvoicedetails tbody tr').each(function (ind, ele) {
        if ($(ele).find('td').length == 8) {
            var _newProduct = {};
            _newProduct.ProductId = $(ele).find('td:eq(1)').data('proid');
            _newProduct.Qty = $(ele).find('td:eq(5) #txtQty_' + ind).val();
            _newProduct.SalePrice = $(ele).find('td#lblPrice_' + ind).text();
            _newProduct.Discount = $(ele).find('td:eq(2) #txtDiscount_' + ind).val();
            _newProduct.Remark = $(ele).find('td:eq(3) #txtRemark_' + ind).val();
            $invoiceDetail.Products.push(_newProduct);
        }
    });

    if (validateInvoice($invoiceDetail)) {
        utility.ajaxHelper(app.urls.SaleArea.SalesController.SaveInvoice, { invoiceDetails: $invoiceDetail }, function (data) {

            $('#lblInvoiceNo').text($('#lblInvoiceNo').text().replace('$invoiceNo', data[0].Key));
            utility.setAjaxAlert(data);
        });
    }
});
$(document).on('click', '[id*="btnReset_"]', function () {
    $(this).parent().parent().parent().find('input[type="number"]').val('0').change();
});
$(document).on('click', '#btnAddCustSave', function () {
    if (validate.form('popAddCustomer')) {
        let $firstName = $('#txtFirstName').val();
        let $lastName = $('#txtLastName').val();
        let $mobile = $('#txtCustMobile').val();
        if ($firstName.length) {
            utility.ajaxHelper(app.urls.SaleArea.SalesController.AddCustomer,
                {
                    FirstName: $firstName,
                    LastName: $lastName,
                    CustMobile: $mobile
                },
                function (data) {
                    utility.setAjaxAlert(data);
                    $('#_ptlSearchCustomer').change();
                    $('#popAddCustomer').hide();
                });
        }
    }
    else {
        utility.SetAlert('Please rectify all validation errors', utility.alertType.warning);
    }
});

$(document).on('click', '#btnAddCustCancel', function () {
    $('#popAddCustomer').hide();
});

$(document).on('click', '#_ptlSearchCustomerList li', function () {
    if (!$(this).hasClass('norecord')) {
        $newSales.setCustomerRecord($(this).data('data'));
    }
    else {
        $('#popAddCustomer').show();
        $('#txtCustMobile').val($('#_ptlSearchCustomer').val());
    }
});
$(document).on('click change', '[id*="txtQty_"],[id*="txtDiscount_"]', function () {
    let $rowId = $(this).attr('id').split('_')[1];
    let $price = parseFloat($('#lblPrice_' + $rowId).text()).toFixed(2);
    let $qty = parseFloat($('[id="txtQty_' + $rowId + '"]').val());
    let $amount = $('#lblAmount_' + $rowId);
    let $discount = parseFloat($('#txtDiscount_' + $rowId).val());
    let $totalAmount = (($price * $qty) - $discount);
    let $lblSubTotal = $('#lblSubTotal');
    let $subTotal = 0.00;
    let $gstAmount = 0.00;
    let $grandAmount = 0.00;
    let $grandAmountInText = '';
    $amount.text($totalAmount.toFixed(2));
    $('[id*="lblAmount_"]').each(function (ind, ele) {
        $subTotal += parseFloat($(ele).text());
    });

    $gstAmount = ($subTotal / 100) * 12;
    $grandAmount = $subTotal + $gstAmount;


    $('#lblGst').text($gstAmount.toFixed(2));
    $grandAmountInText = inWords($grandAmount.toFixed(0));
    $grandAmountInText = $grandAmountInText == '' ? 'Zero Only' : $grandAmountInText;
    $('#lblGrandTotalWord').text($grandAmountInText);
    $('#lblGrandAmount').text(($subTotal + $gstAmount).toFixed(2));
    $lblSubTotal.text($subTotal.toFixed(2));
});

$(document).on('click', '#_ptlSearchProductAdd', function () {
    let $data = $(this).data('productinfo');
    if (!$.isEmptyObject($data)) {

        $('.defaultRow').remove();

        let $tbody = $('#tblInvoicedetails tbody');
        let $totalRows = $($tbody).find('tr').length;
        let $html = '<tr>' +
            '<td class="shop_vMiddle">' + ($totalRows - 2) + '.</td >' +
            '<td class="shop_vMiddle" data-proid="' + $data.ProductId + '">' + $data.ProductName + '</td>' +
            '<td> <input type="number" min="0" value="0" id="txtDiscount_' + ($totalRows - 3) + '" class="form-control" /></td > ' +
            '<td> <input type="text" title="Put remark if discount is applicable" id="txtRemark_' + ($totalRows - 3) + '" class="form-control" /></td> ' +
            '<td class="shop_vMiddle shop_hCentre" id="lblPrice_' + ($totalRows - 3) + '">' + $data.SalePrice.toFixed(2) + '</td> ' +
            '<td class="shop_vMiddle shop_hCentre"><input min="0" value="1"  type="number" id="txtQty_' + ($totalRows - 3) + '" class="form-control" /></td> ' +
            '<td class="shop_vMiddle shop_hRigth" id="lblAmount_' + ($totalRows - 3) + '"> 0.00</td> ' +
            '<td class="shop_vMiddle shop_hCentre">' +
            '<div class="btn-group">' +
            '<img style="width:25px;cursor:pointer;" title="Delete this row"  id="btnDelete_' + ($totalRows - 3) + '" src="../../Images/Icons/delete.png" />' +
            '<img style="width:25px;cursor:pointer;" title="Reset this row" id="btnReset_' + ($totalRows - 3) + '" src="../../Images/Icons/refresh.png" />' +
            '</div > ' +
            '</td> ' +
            '</tr>';
        $('.tdSubTotal').before($html);
        $('[id*="txtQty_"],[id*="txtDiscount_"]').change();
        $(this).data('productinfo', {});
    }
    else {
        utility.SetAlert("Please search the product", utility.alertType.warning);
    }
    $('#_ptlSearchProduct').val('');
});

var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Tourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
var b = ['', '', 'Twenty', 'Thirty', 'Torty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];

function inWords(num) {
    if ((num = num.toString()).length > 9) return 'Invalid or access amount';
    n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
    if (!n) return; var str = '';
    str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
    str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
    str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
    str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';
    str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
    return str;
}

function validateInvoice(obj) {
    if (obj.CustomerId === "0") {
        utility.SetAlert("Please select/search the customer");
        return false;
    } else if (obj.Products == undefined || obj.Products[0].ProductId === undefined) {
        utility.SetAlert("Please add atleast one product.");
        return false;
    }
    else if (obj.Products == undefined || obj.Products[0].SalePrice === '') {
        utility.SetAlert("Sale price is not available");
        return false;
    }
    else if (obj.GrandTotal === 0) {
        utility.SetAlert("Grand Total amount is not calculated");
        return false;
    }
    else if (obj.SubTotalAmount === 0) {
        utility.SetAlert("Sub Total amount is not calculated");
        return false;
    }
    else if (obj.SubTotalAmount === 0) {
        utility.SetAlert("Sub Total amount is not calculated");
        return false;
    }
    else
        return true;
}
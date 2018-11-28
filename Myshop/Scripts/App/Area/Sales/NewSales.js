/// <reference path="../../common/app.js" />
/// <reference path="../../../jquery-1.10.2.js" />
/// <reference path="../../common/validate.js" />
/// <reference path="../../common/utility.js" />

var $newSales = {};

$newSales.setCustomerRecord = function (data) {
    let $address = data.FirstName + " " + data.LastName;
    $address += ", \n" + data.Mobile +"\n";
    $address += (data.Email == "" ? "" : data.Email + "\n");
    $address += (data.Address == "" ? "" : data.Address + "\n");
    $address += data.District + "," + data.State; 
    $address += data.PINCode === "" ? "" : "-" + data.PINCode;
    $('.customerAddress').text($address);
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
    $('#lblSubTotal').text('0.00');
    $('#lblGst').text('0.00');
    $('#lblGrandAmount').text('0.00');
    $('#lblGrandTotalWord').text('Zero Only');
});
$(document).on('click', '#_ptlSearchProductSave', function () {
    let $payModeDdl = $('#_ptlPaymentMode');
    let $payModeRefNo = $('#_ptlPayReNo');
    let $amountToBePaid = $('#txtAmountToBePaid');
    if ($('#_ptlSearchProductSave').data('invoiceno') !== 0) {
        utility.SetAlert('This invoice is already saved', utility.alertType.information);
        return false;
    }
    else if ($('.defaultRow').length > 0) {
        utility.SetAlert('Please add atleast one product', utility.alertType.warning);
        return false;
    }
    let $flag = false;
    let $invoiceDetail = {};
    let $subTotal = parseFloat($('#lblSubTotal').text());
    let $gstAmount = (($subTotal / 100) * 12);
    let $grandAmount = $subTotal + $gstAmount;
    let $paidAmount = 0.00;
    $invoiceDetail.CustomerId = $('#hdnCustomerId').val();
    $invoiceDetail.SubTotalAmount = $subTotal;
    $invoiceDetail.GstAmount = $gstAmount;
    $invoiceDetail.PaidAmount = parseFloat($($amountToBePaid).val());
    $invoiceDetail.PayModeRefNo = '';
    $invoiceDetail.GrandTotal = $grandAmount;    
    $invoiceDetail.BalanceAmount = $grandAmount - $invoiceDetail.PaidAmount;

    $invoiceDetail.PayModeId = $($payModeDdl).find('option:selected').val();
    $invoiceDetail.Products = [];
    $('#tblInvoicedetails tbody tr').each(function (ind, ele) {
        if ($(ele).find('td').length == 8) {
            var $discount = parseFloat($(ele).find('td:eq(2) #txtDiscount_' + ind).val());
            var $remark = $(ele).find('td:eq(3) #txtRemark_' + ind).val();
            if ($discount > 0 && $remark === '') {
                utility.SetAlert('Please put the remarks because you are giving the discount', utility.alertType.warning);
                $(ele).find('td:eq(3) #txtRemark_' + ind).addClass('shop_hasError').focus();
                $flag = true;
                return false;
            }
            else {
                $(ele).find('td:eq(3) #txtRemark_' + ind).removeClass('shop_hasError');
            }
            var _newProduct = {};
            _newProduct.ProductId = $(ele).find('td:eq(1)').data('proid');
            _newProduct.Qty = $(ele).find('td:eq(5) #txtQty_' + ind).val();
            _newProduct.SalePrice = $(ele).find('td#lblPrice_' + ind).text();
            _newProduct.Discount = $(ele).find('td:eq(2) #txtDiscount_' + ind).val();
            _newProduct.Remark = $(ele).find('td:eq(3) #txtRemark_' + ind).val();
            $invoiceDetail.Products.push(_newProduct);
        }
    });
    if ($flag)
        return false;
    if ($($payModeDdl).find('option:selected').val() === "") {
        utility.SetAlert('Please select the payment mode', utility.alertType.warning);
        $($payModeDdl).addClass('shop_hasError').focus();
        return false;
    }
    else {
        $($payModeDdl).removeClass('shop_hasError');
    }

    if ($invoiceDetail.PaidAmount > $invoiceDetail.GrandTotal) {
        utility.SetAlert('Paid amount cannot be greater than Grand Total', utility.alertType.warning);
        $($amountToBePaid).addClass('shop_hasError').focus();
        return false;
    }
    else {
        $($amountToBePaid).removeClass('shop_hasError');
    }

    if ($($payModeRefNo).val() === "" && $($payModeRefNo).is(':visible')) {
        utility.SetAlert('Please enter payment reference number', utility.alertType.warning);
        $($payModeRefNo).addClass('shop_hasError').focus();
        return false;
    }
    else {
        $($payModeRefNo).removeClass('shop_hasError');
        $invoiceDetail.PayModeRefNo = $($payModeRefNo).val();
    }
    if (parseFloat($('#txtBalanceAmount').val())<0) {
        utility.SetAlert('Please enter payment reference number', utility.alertType.warning);
        $('#txtBalanceAmount').addClass('shop_hasError').focus();
        return false;
    }
    else {
        $('#txtBalanceAmount').removeClass('shop_hasError');
    }

    if (validateInvoice($invoiceDetail)) {
        utility.ajaxHelper(app.urls.SaleArea.SalesController.SaveInvoice, { invoiceDetails: $invoiceDetail }, function (data) {

            $('#lblInvoiceNo').text($('#lblInvoiceNo').text().replace('$invoiceNo', data[0].Key));
            $('#_ptlSearchProductSave').data('invoiceno', data[0].Key);
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
        let $state = $('#ptlDdlState option:selected').val();
        let $city = $('#ptlDdlCity option:selected').val();
        if ($firstName.length) {
            utility.ajaxHelper(app.urls.SaleArea.SalesController.AddCustomer,
                {
                    FirstName: $firstName,
                    LastName: $lastName,
                    CustMobile: $mobile,
                    State: $state,
                    City: $city
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
    $grandAmountInText = utility.currentyInWords($grandAmount.toFixed(0));
    $grandAmountInText = $grandAmountInText == '' ? 'Zero Only' : $grandAmountInText;
    $('#lblGrandTotalWord').text($grandAmountInText);
    $('#lblGrandAmount').text(($subTotal + $gstAmount).toFixed(2));
    $('#txtAmountToBePaid').val(($subTotal + $gstAmount).toFixed(2));
    $('#txtAmountToBePaid').attr('max',($subTotal + $gstAmount).toFixed(2));
    $lblSubTotal.text($subTotal.toFixed(2));
});

$(document).on('click', '#_ptlSearchProductAdd', function () {
    let $data = $(this).data('productinfo');
    let $tbody = $('#tblInvoicedetails tbody');
    let $totalRows = 0;
    if (!$.isEmptyObject($data)) {

        $('.defaultRow').remove();
        $totalRows = $($tbody).find('tr').length;
        let $hasProduct = false;
        $($tbody).find('tr td').each(function (ind, ele) {
            if ($(ele).data('proid') == $data.ProductId) {
                utility.SetAlert('This product is already added in list', utility.alertType.warning);
                $(ele).css('background', '#acfde7c4');s
                $hasProduct = true
            }
        });

        if ($hasProduct)
            return false;

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

$(document).on('change', '#txtAmountToBePaid', function () {
    let $amount = parseFloat($('#lblGrandAmount').text());
    let $balAmount = $amount - parseFloat($(this).val());
    $('#txtBalanceAmount').val($balAmount.toFixed(2));
});

$(document).on('click', '#_ptlSearchProductNewInvoice', function () {
    $('#_ptlSearchProductSave').data('invoiceno', 0);
    resetInvoice();
});

function resetInvoice() {
    $('.customerAddress').text('');
    $('#txtAmountToBePaid').val('0');
    $('#txtBalanceAmount').val('0');
    $('#_ptlPaymentMode').val('');
    $('#lblInvoiceNo').text('Invoice No. : $invoiceNo');
    $('#lblInvoiceDate').text('Invoice Date : $invoiceDate');
    $('#tblInvoicedetails tbody tr:lt(' + ($('#tblInvoicedetails tbody tr').length-3) + ')').remove();
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

    $('#_ptlSearchProductList').empty();
    $('#_ptlSearchProduct').text('');
    $('#_ptlSearchCustomerList').empty();
    $('#_ptlSearchCustomer').text('');
    $('#_ptlPaymentMode').val('');
    $('#_ptlPaymentMode').css('width', '100%');;
    $('#_ptlPayReNo').val('').hide();
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
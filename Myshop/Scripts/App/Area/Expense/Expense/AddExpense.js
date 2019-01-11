
var $expense = {};

$expense.searchItem = function ($val) {
    return new Promise(function (resolve, reject) {
        utility.ajaxHelper(app.urls.ExpenseArea.ExpenseController.SearchExpItem, { searchValue: $val }, function (resp) {
            resolve(resp);
        }, function (err) {
            reject(err);
        });
    });
}

$expense.saveExpense = function ($data) {
    return new Promise(function (resolve, reject) {
        utility.ajaxHelper(app.urls.ExpenseArea.ExpenseController.SaveExpense, { model: $data }, function (resp) {
            resolve(resp);
        }, function (err) {
            reject(err);
        });
    });
}

$(document).ready(function () {
    utility.bindDdlByAjax(app.urls.GetVendorJson, 'vendorid','VendorName','VendorId');
});

$(document).on('keypress', "#_ptlSearchExpenseItem", function () {
    let $pos = $(this).position();
    $pos.width = $(this).width();
    if ($(this).val().length > 0) {
        $expense.searchItem($(this).val()).then(function (resp) {
            let $ul = $('#_ptlSearchProductList');
            $($ul).css({ 'top': $pos.top+34, 'left': $pos.left, 'width': $pos.width });
            let $li = '';
            $ul.empty();
            if (resp.length > 0) {
                $(resp).each(function (ind, ele) {
                    $li += "<li data-data='" + JSON.stringify(ele) + "' style='height: 45px;color:black;'><span style='width: 100%;float: left;color:black; padding: 2px;font-size:14px;'>" + ele.ExpItem + "</span><span style='font-size: 11px;text-align:center;float: right;padding: 2px;'>Type:" + ele.ExpTypeName + ", Price:" + utility.getInrCurrency(ele.ExpItemPrice) + '</span></li>';
                });
            }
            else {
                $li += '<li class="norecord">No Record</li>';
            }
            $($ul).append($li);
            $($ul).show();
        });
    }
});

$(document).on('click', "#_ptlSearchProductList li", function () {
    let $txt = $('#_ptlSearchExpenseItem');
    $($txt).val($(this).find('span:eq(0)').text().trim());
    $($txt).data('data', JSON.stringify($(this).data('data')));
    $('#_ptlSearchProductList').empty().hide();
});

$(document).on('change', ".txtqty", function () {
    let $qty = parseFloat($(this).val());
    let $price = parseFloat($(this).parent().parent().find('.lblprice').text().trim());
    $(this).parent().parent().find('.lblamount').text(parseFloat($qty * $price).toFixed(2));
    let $total = 0.00;
    $('.lblamount').each(function (ind, ele) {
        $total += parseFloat($(ele).text().trim());
    });

    $('.lbltotalamount').text($total.toFixed(2));
    $('#txtPaidAmount').val($total.toFixed(2));
    $('#txtPaidAmount').attr('max', $total.toFixed(2));
    $('#txtBalanceAmount').attr('max', $total.toFixed(2));
});

$(document).on('click', "#btnSave", function () {
    if (!$(this).hasClass('saved')) {
        if ($expense.validateSave()) {
     $expense.saveExpense($expense.getData()).then(function (resp) {
                debugger;
                if (resp.length > 1) {
                    $('.lblexpenseno').text('Expense No. : ' + resp[0].Key);
                    $('#btnSave').addClass('saved');
                    $expense.disableForm();
                }
                utility.setAjaxAlert(resp);
            });
        }
    }
    else {
        utility.SetAlert('You have already save this expense', utility.alertType.information);
    }
});

$(document).on('click', "#btnList", function () {
    window.location = '/expensemanagement/Expense/ExpenseList';
});

$(document).on('click', "#btnNew", function () {
    window.location = '/expensemanagement/Expense/AddExpenses';
});

$(document).on('click', "#_ptlSearchItemAdd", function () {
    let $txt = $('#_ptlSearchExpenseItem');
    if (!$expense.hasItem($($txt).val().trim())) {
        let $data = $($txt).data('data');
        if ($data !== null && $data !== '') {
            $data = JSON.parse($($txt).data('data'));
            $($txt).data('data', '');
            $(this).parent().parent().parent().prepend('<tr data-itemid="' + $data.ExpItemId+'">' +
                '<td class= "shop_vMiddle" > 1.</td>' +
                '<td>' + $data.ExpItem +
                '</td>' +
                '<td class="shop_vMiddle shop_hCentre">' + $data.ExpTypeName + '</td>' +
                '<td class="shop_vMiddle shop_hCentre lblprice">' + parseFloat($data.ExpItemPrice).toFixed(2) + '</td>' +
                '<td class="shop_vMiddle shop_hCentre"><input type="number" value="1" class="form-control txtqty" placeholder="Qty" min="1" max="99999" /></td>' +
                '<td class="shop_vMiddle shop_hRigth lblamount">' + parseFloat($data.ExpItemPrice).toFixed(2) + '</td>' +
                '<td class="shop_vMiddle shop_hCentre" style="width:72px;">' +
                '<div class="btn-group-sm">' +
                '<img style="width:25px;cursor:pointer;" id="btnDelete_" title="Delete this row" src="../../Images/Icons/delete.png" />' +
                '<img style="width:25px;cursor:pointer;" id="btnReset_" title="Reset this row" src="../../Images/Icons/refresh.png" />' +
                '</div>' +
                '</td>' +
                '</tr>');
            $($txt).val('');
            $expense.resetRowNo();
            $(".txtqty").change();
        }
        else {
            utility.SetAlert('Please search the Expense item', utility.alertType.warning);
            return false;
        }
    }
});

$(document).on('change', '#txtPaidAmount', function () {
    let $total = parseFloat($('.lbltotalamount').text().trim());
    let $paid = parseFloat($('#txtPaidAmount').val());
    let $balAmount = $total - $paid;
    $('#txtBalanceAmount').val(parseFloat($balAmount).toFixed(2));
});

$(document).on('change', '#txtBalanceAmount', function () {
    let $total = parseFloat($('.lbltotalamount').text().trim());
    let $balAmount = parseFloat($(this).val());
    let $paid = $total - $balAmount;
    $('#txtPaidAmount').val(parseFloat($paid).toFixed(2));
});

$(document).on('click', '[id*="btnDelete_"]', function () {
    let $parentRow = $(this).parent().parent().parent();
    if ($($parentRow).hasClass('defaultrow')) {
        utility.SetAlert('You can\'t delete this row', utility.alertType.warning);
        return false;
    }
    $($parentRow).remove();
    $expense.resetRowNo();
    $('.txtqty').change();
});

$(document).on('click', '[id*="btnReset_"]', function () {
    $(this).parent().parent().parent().find('input[type="number"]').val('1').change();
});

$expense.resetRowNo = function () {
    $('#tblExpense tbody tr').each(function (ind, ele) {
        $(ele).find('td:eq(0)').text((ind + 1) + '.');
    });
}

$expense.hasItem = function (item) {
    let $flag = false;
    $('#tblExpense tbody tr').each(function (ind, ele) {
        if ($(ele).find('td:eq(1)').text().trim() == item.trim() && item!=='') {
            utility.SetAlert(item + ' is already exist', utility.alertType.warning);
            $(ele).find('td').css('backgroud', 'red !important');
            $flag = true;
        }
    });

    return $flag;
}

$expense.getData = function() {
    var $data = {
        ExpTr: {
            TotalAmout: '',
            PaidAmount: '',
            BalanceAmount: '',
            VendorId: '',
            PayModeId: '',
            PayModeRefNo:''
        },
        ExpDtl: []
    };

    $data.ExpTr.TotalAmout = $('.lbltotalamount').text().trim();
    $data.ExpTr.VendorId = $('#vendorid').find(':selected').val();
    $data.ExpTr.PaidAmount = $('#txtPaidAmount').val();
    $data.ExpTr.BalanceAmount = $('#txtBalanceAmount').val();
    $data.ExpTr.PayModeId = $('#_ptlPaymentMode').find(':selected').val();
    if ($('#_ptlPaymentMode').find('option:selected').text().toLowerCase() !== 'cash') {
        $data.ExpTr.PayModeRefNo = $('#_ptlPayReNo').val();
    }
    else {
        $data.ExpTr.PayModeRefNo = '';
    }

    $('#tblExpense tbody tr:not(.defaultrow)').each(function (ind, ele) {
        var $dtl = {
            ExpItemId: $(ele).data('itemid'),
            Qty: $(ele).find('input[type="number"]').val()
        }
        $data.ExpDtl.push($dtl);
    });

    return $data;
}

$expense.validateSave = function () {
    let $flag = true;
    let $total = parseFloat($('.lbltotalamount').text());
    let $paid = parseFloat($('#txtPaidAmount').val());
    let $bal = parseFloat($('#txtBalanceAmount').val());
    let $vendor = $('#vendorid');
    let $paymode = $('#_ptlPaymentMode');
    if ($('#tblExpense tbody tr:not(.defaultrow)').length == 0) {
        $flag = false;
        utility.SetAlert('Please atleast one expense item', utility.alertType.error); return false;
    }
    $('#tblExpense tbody tr:not(.defaultrow)').each(function (ind, ele) {
        if (parseInt($(ele).data('itemid')) < 1) {
            $flag = false;
            utility.SetAlert('Invalid Expense Item', utility.alertType.error);
            $(ele).css('border', '1px solid red');
            return false;
        }
        else {
            $(ele).css('border', '1px solid green');
        }
        if (parseInt($(ele).find('input[type="number"]').val()) < 1) {
            $flag = false;
            utility.SetAlert('Invalid Expense Item', utility.alertType.error);
            $(ele).find('input[type="number"]').css('border', '1px solid red');
            return false;
        }
        else {
            $(ele).find('input[type="number"]').css('border', '1px solid green');
        }      
    });

    if ($total < 1) {
        $flag = false;
        utility.SetAlert('Total Amount can not be zero', utility.alertType.error);
        $('.lbltotalamount').css('color', 'red');
        return false;
    }
    else {
        $('.lbltotalamount').css('color', 'green');
    }

    if ($paid === 0 && $bal === 0) {
        $flag = false;
        utility.SetAlert('Paid and Balance amount both can not be zero', utility.alertType.error);
        $('#txtPaidAmount').css('border', '1px solid red');
        $('#txtBalanceAmount').css('border', '1px solid red');
        return false;
    }
    else {
        $('#txtPaidAmount').css('border', '1px solid green');
        $('#txtBalanceAmount').css('border', '1px solid green');
    }
    if ($bal > $total) {
        $flag = false;
        utility.SetAlert('Balance amount can not be greater than Total amount', utility.alertType.error);
        $('#txtBalanceAmount').css('border', '1px solid red');
        return false;
    }
    else {
        $('#txtBalanceAmount').css('border', '1px solid green');
    }
    if ($paid > $total) {
        $flag = false;
        utility.SetAlert('Paid amount can not be greater than Total amount', utility.alertType.error);
        $('#txtPaidAmount').css('border', '1px solid red');
        return false;
    }
    else {
        $('#txtPaidAmount').css('border', '1px solid green');
    }
    if ($($vendor).find(':selected').val() == '') {
        $flag = false;
        utility.SetAlert('Please select the Vendor', utility.alertType.warning);
        $($vendor).css('border', '1px solid red');
        return false;
    }
    else {
        $($vendor).css('border', '1px solid green');
    }
    if ($($paymode).find(':selected').val() == '') {
        $flag = false;
        utility.SetAlert('Please select the Payment Mode', utility.alertType.warning);
        $($paymode).css('border', '1px solid red');
        return false;
    }
    else {
        $($paymode).css('border', '1px solid green');
    }
    return $flag;
}

$expense.disableForm = function() {
    $('#txtPaidAmount').attr('disabled', 'disabled');
    $('#txtBalanceAmount').attr('disabled', 'disabled');
    $('#_ptlPaymentMode').attr('disabled', 'disabled');
    $('#_ptlPayReNo').attr('disabled', 'disabled'); $('#defaultQty').attr('disabled', 'disabled');
    $('#vendorid').attr('disabled', 'disabled');
    $('#_ptlSearchExpenseItem').attr('disabled', 'disabled');
    $("#_ptlSearchItemAdd").remove();
    $('[id*="btnDelete_"]').remove();
    $('.txtqty').attr('disabled', 'disabled');
    $('[id*="btnReset_"]').remove();
}
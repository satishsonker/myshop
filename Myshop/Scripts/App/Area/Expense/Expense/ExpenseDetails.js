var $expenseDtl = {};

$expenseDtl.searchExpNo = function ($val) {
    return new Promise(function (resolve, reject) {
        utility.ajaxHelper(app.urls.ExpenseArea.ExpenseController.SearchExpenseNo, { searchValue: $val }, function (resp) {
            resolve(resp);
        }, function (err) {
            reject(err);
        });
    });
}

$expenseDtl.getExpDetails = function ($val) {
    return new Promise(function (resolve, reject) {
        utility.ajaxHelper(app.urls.ExpenseArea.ExpenseController.ExpenseDetails, { expid: $val }, function (resp) {
            resolve(resp);
        }, function (err) {
            reject(err);
        });
    });
}
$expenseDtl.cancelExpenseItem = function ($expId, $expDtlId, $remarks) {
    return new Promise(function (resolve, reject) {
        utility.ajaxHelper(app.urls.ExpenseArea.ExpenseController.CancelExpenseItem, { ExpId: $expId, ExpDtlId: $expDtlId, CancelReason:$remarks }, function (resp) {
            resolve(resp);
        }, function (err) {
            reject(err);
        });
    });
}
$expenseDtl.cancelExpense = function ($expId, $remarks) {
    return new Promise(function (resolve, reject) {
        utility.ajaxHelper(app.urls.ExpenseArea.ExpenseController.CancelExpense, { ExpId: $expId, CancelReason: $remarks }, function (resp) {
            resolve(resp);
        }, function (err) {
            reject(err);
        });
    });
}
$(document).ready(function () {
    let $expId = utility.getQueryStringValue('expid');
    if ($expId !== '') {
        $('#txtSearchExpense').val($expId);
        $('#btnGo').click();
    }
});

$(document).on('keyup', '#txtSearchExpense', function () {
    if ($(this).val().length > 0) {
        $expenseDtl.searchExpNo($(this).val()).then(function (resp) {
            let $ul = $('.expList');
            //$($ul).css({ 'top': $pos.top + 34, 'left': $pos.left, 'width': $pos.width });
            let $li = '';
            $ul.empty();
            if (resp.length > 0) {
                $(resp).each(function (ind, ele) {
                    $li += "<li " + (ele.IsCancelled ?"class='cancelitem' title='This expense is Cancelled'":"") + " data-data='" + JSON.stringify(ele) + "' style='color:black;'><span style='width: 100%;float: left;color:black; padding: 2px;font-size:14px;'>" + ele.ExpId + "</span><span style='font-size: 11px;text-align:center;float: right;padding: 2px;'>Vendor: " + (ele.VendorName == null ? "No Vendor" : ele.VendorName) + ", Amount: " + utility.getInrCurrency(ele.TotalAmout) + '</span></li>';
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
$(document).on('click', ".expList li", function () {
    let $txt = $('#txtSearchExpense');
    $($txt).val($(this).find('span:eq(0)').text().trim());
    $($txt).data('data', JSON.stringify($(this).data('data')));
    $('.expList').empty().hide();
});
$(document).on('click', '#btnGo', function () {
    let $expNo = parseInt($('#txtSearchExpense').val());
    if (isNaN($expNo) || $expNo < 1) {
        $expNo = parseInt(utility.getQueryStringValue('expid'));
        if (isNaN($expNo) || $expNo < 1) {
            utility.SetAlert('Please search or enter Expense number', utility.alertType.warning);
        }
    }

    $expenseDtl.getExpDetails($expNo).then(function (resp) {
        // if (resp.length > 0) {
        let $html = '';
        let $total = 0.00;
        $(resp.ExpDtl).each(function (ind, ele) {
            if (ele.IsCancelled || resp.ExpTr.IsCancelled) {
                var $reason = resp.ExpTr.CancelReason === '' ? ele.CancelReason : resp.ExpTr.CancelReason;
                var $date = resp.ExpTr.CancelledDate === '' ? ele.CancCancelledDateelReason : resp.ExpTr.CancelledDate;
                $html += '<tr data-toggle="tooltip" class="cancelitem" title="Reason: ' + $reason + '\n Date: ' + utility.getJsDateTimeFromJson($date) + '">';
            }
            else {
                $html += '<tr>';
                $total += (ele.ExpItemPrice * ele.Qty);
            }
            $html += '<td><span class="badge badge-danger">' + (ind + 1) + '<span></td>';
            $html += '<td>' + ele.ExpItemName + '</td>';
            $html += '<td>' + ele.ExpTypeName + '</td>';
            $html += '<td class="shop_vMiddle shop_hRigth">' + utility.getInrCurrency(ele.ExpItemPrice) + '</td>';
            $html += '<td class="shop_vMiddle shop_hRigth">' + parseFloat(ele.Qty).toFixed(2) + '</td>';
            $html += '<td class="shop_vMiddle shop_hRigth ' + (ele.IsCancelled || resp.ExpTr.IsCancelled ?'cancelitem shop-textStrike':'')+'">' + utility.getInrCurrency((ele.ExpItemPrice * ele.Qty)) + '</td>';
            $html += '<td class="shop_vMiddle shop_hCentre">' + (ele.IsCancelled || resp.ExpTr.IsCancelled? '' : '<i data-dtlid="' + ele.ExpDtlId + '" id="cancelId_' + ele.ExpDtlId + '" title="Cancel this Item" class="fas fa-times fa-2x red-text" style="cursor:pointer;"></i>') + '</td>';
            $html += '</tr>';
        });
        $('#tblExpense tbody').empty().append($html);

        

        if ($html === '') {
            $html += '<tr><td colspan="6" style="text-align:center;">No Expense Found</td></tr>';
            $('.lbltotalamount').text('₹ 0.00');
            $('#vendorid').text('No Vendor');
            $('#paidby').text('No Payment');
            $('#paidRef').text('No Payment');
            $('#paidamount').text(' 0.00');
            $('#balamount').text(' 0.00');
        }
        else {
            $('.lbltotalamount').text('₹' + parseFloat($total).toFixed(2));
            $('#vendorid').text(' ' + resp.ExpTr.VendorName);
            $('#paidby').text(' ' + resp.ExpTr.PayMode);
            $('#paidRef').text(' ' + resp.ExpTr.PayRefNo);
            $('#paidamount').text(' ' + parseFloat(resp.ExpTr.PaidAmount).toFixed(2));
            if (resp.ExpTr.BalanceAmount < 0) {
                $('#balamount').parent().parent().find('[data-toggle="tooltip"]').remove();
                $('#balamount').parent().after('<i data-toggle="tooltip" title="Negative amount indicate that you have to take money from vendor" class="fas fa-question-circle black-text"></i>')
            }
                $('#balamount').text(' ' + parseFloat(resp.ExpTr.BalanceAmount).toFixed(2)+' ');
        }
        if (!resp.ExpTr.IsCancelled) {
            $('#btnCancel').show();
        }
        else {
            $('#calreason').text(resp.ExpTr.CancelReason);
        }
        $('[data-toggle="tooltip"]').tooltip(); 
    })
});

$(document).on('click', "#btnList", function () {
    window.location = '/expensemanagement/Expense/ExpenseList';
});

$(document).on('click', '[id*="cancelId_"]', function () {
    $('.cancelreason').show(500);
    $('#txtCancelReason').val('');
    $('#btnCancelMain').data('id', $(this).attr('id').split('_')[1]);
    $('#btnCancelMain').data('type', 'item');
});
$(document).on('click', '#btnCancel', function () {
    $('.cancelreason').show(500);
    $('#txtCancelReason').val('');
    $('#btnCancelMain').data('id','');
    $('#btnCancelMain').data('type', 'expense');
});


$(document).on('click', '#btnCancelRemark', function () {
    $('.cancelreason').hide(500);
});

$(document).on('click', '#btnCancelMain', function () {
    let $itemId = $(this).data('id');
    let $expId = $('#txtSearchExpense').val().trim();
    let $remarks = $('#txtCancelReason').val().trim();
    let $type = $(this).data('type');
    if (($itemId == undefined || $itemId == '') && $type=='item') {
        utility.SetAlert('Cancel reason should be greater than 5 charectors', utility.alertType.warning);
        $('#txtCancelReason').css('border', '1px solid red');
        return false;
    }
    if ($remarks.length < 5) {
        utility.SetAlert('Cancel reason should be greater than 5 charectors', utility.alertType.warning);
        $('#txtCancelReason').css('border', '1px solid red');
        return false;
    }
    else {
        $('#txtCancelReason').css('border', '1px solid green');
    }
    if (isNaN(parseInt($expId))) {
        utility.SetAlert('Invalid Expense Number', utility.alertType.warning);
        $('#txtSearchExpense').css('border', '1px solid red');
        return false;
    }
    else {
        $('#txtSearchExpense').css('border', '1px solid green');
    }
    if ($type == 'item') {
        $expenseDtl.cancelExpenseItem($expId, $itemId, $remarks).then(function (resp) {
            utility.setAjaxAlert(resp);
            $('#btnGo').click();
        });
    }
    if ($type == 'expense') {
        $expenseDtl.cancelExpense($expId, $remarks).then(function (resp) {
            utility.setAjaxAlert(resp);
            $('#btnGo').click();
        });
    }
});




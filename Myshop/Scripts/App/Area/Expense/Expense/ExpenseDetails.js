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
$(document).ready(function () {
    let $expId = utility.getQueryStringValue('expid');
    if ($expId !== '') {
        $('#txtSearchExpense').val();
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
                    $li += "<li data-data='" + JSON.stringify(ele) + "' style='color:black;'><span style='width: 100%;float: left;color:black; padding: 2px;font-size:14px;'>" + ele.ExpId + "</span><span style='font-size: 11px;text-align:center;float: right;padding: 2px;'>Vendor: " + (ele.VendorName == null ? "No Vendor" : ele.VendorName) + ", Amount: " + utility.getInrCurrency(ele.TotalAmout) + '</span></li>';
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
            $(resp.ExpDtl).each(function (ind, ele) {
                $html += '<tr>';
                $html += '<td><span class="badge badge-danger">' + (ind + 1) + '<span></td>';
                $html += '<td>' + ele.ExpItemName + '</td>';
                $html += '<td>' + ele.ExpTypeName + '</td>';
                $html += '<td class="shop_vMiddle shop_hRigth">' + utility.getInrCurrency(ele.ExpItemPrice) + '</td>';
                $html += '<td class="shop_vMiddle shop_hRigth">' + parseFloat(ele.Qty).toFixed(2) + '</td>';
                $html += '<td class="shop_vMiddle shop_hRigth">' + utility.getInrCurrency((ele.ExpItemPrice*ele.Qty)) + '</td>';
                $html += '</tr>';
        });
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
            $('.lbltotalamount').text('₹' + parseFloat(resp.ExpTr.TotalAmount).toFixed(2));
            $('#vendorid').text(' ' + resp.ExpTr.VendorName);
            $('#paidby').text(' ' + resp.ExpTr.PayMode);
            $('#paidRef').text(' ' + resp.ExpTr.PayRefNo);
            $('#paidamount').text(' ' + parseFloat(resp.ExpTr.PaidAmount).toFixed(2));
            $('#balamount').text(' ' + parseFloat(resp.ExpTr.BalanceAmount).toFixed(2));
        }
        $('#tblExpense tbody').empty().append($html);
        
       // }
    })
});

$(document).on('click', "#btnList", function () {
    window.location = '/expensemanagement/Expense/ExpenseList';
});

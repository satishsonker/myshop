$(document).ready(function () {
    bindTable(1, 10);
});
$(document).on('change', '#ddlPageSize', function () {
    bindTable(1, $(this).find(':selected').val());
});

function bindTable(pageNo, pageSize, pageBtn) {
    let $selectedPage = pageBtn == undefined ? 'page_1' : $(pageBtn).attr('id');
    $('[id*="page_"]').removeClass('pageActive');
    utility.ajaxHelper(app.urls.SaleArea.SalesController.GetSalesList, { PageNo: pageNo, PageSize: pageSize }, function (response) {
        let $table = $('#tblSaleList');
        let $tbody = $($table).find('tbody');
        $('#hdntotalRecord').val(response[0].TotalInvoice);
        let $html = '';
        $tbody.empty();
        $(response).each(function (ind, ele) {
            $html += '<tr>' +
                '<td class="shop_vMiddle">' + (ind + 1) + '</td>' +
                '<td class="shop_vMiddle"><a href="/salesmanagement/sale/GetInvoice?invoiceno=' + ele.InvoiceNo + '">' + ele.InvoiceNo +'</a></td>' +
                '<td class="shop_vMiddle">' + ele.CustomerName + '</td>' +
                '<td class="shop_vMiddle shop_hRigth">' + app.const.htmlCode.rupeesSymbol + ' ' + parseFloat(ele.Amount).toFixed(2) + '</td>' +
                '<td class="shop_vMiddle">' + utility.getJsDateTimeFromJson(ele.InvoiceDate) + '</td>' +
                '<td class="shop_vMiddle shop_hRigth">' + app.const.htmlCode.rupeesSymbol + ' '+ parseFloat(ele.BalanceAmount).toFixed(2) + '</td>' +
                '<td class="shop_vMiddle shop_hRigth">' + app.const.htmlCode.rupeesSymbol + ' '+ parseFloat(ele.RefundAmount).toFixed(2) + '</td>' +
                '<td class="shop_vMiddle">' + ele.PaymentMode + '</td>' +
                '<td class="shop_vMiddle"><span class="label label-' + (ele.IsCancelled ? 'danger' : (ele.IsRefund ? 'warning' : 'success')) + '">' + (ele.IsCancelled ? 'Cancelled' : (ele.IsRefund ? 'Return' : 'Billed')) + '</td>' +
                '<td class="shop_vMiddle">' + (!ele.IsCancelled ?'<input type="button" class="btn btn-danger" data-invoiceid="' + ele.InvoiceNo +'" id="shopCancelPopup" value="Cancel">':'')+'</td>' +
                '</tr>';
        });
        $($tbody).append($html);
        bindPaging();
        $('#'+$selectedPage).addClass('pageActive');
    });
}

function bindPaging() {
    let $paging = $('#tblPaging');
    let $pageSize = parseInt($('#ddlPageSize').find(':selected').val());
    let $noOfRecords = parseInt($('#hdntotalRecord').val());
    let $noOfPage = $noOfRecords % $pageSize > 0 ? ($noOfRecords / $pageSize) + 1 : ($noOfRecords / $pageSize);
    $($paging).empty()
    for (var i = 1; i <= $noOfPage; i++) {
        $($paging).append('<button class="btn btn-default" id="page_'+i+'" onclick="bindTable(' + i + ',' + $pageSize + ',this)" >' + i + '</button>');
    }
}

$(document).on('click','.closePopup',  function () {
    $(this).parent().parent().parent().hide();
});
$(document).on('click','#shopCancelPopup',  function () {
    $('.popupAlert').show();
    let $invoiceId = $(this).data('invoiceid');
    $('.popupAlert').find('label').text('Cancellation remark for Invoice Id : ' + $invoiceId);
    $('#btnCancelInvoice').data('invoiceid', $invoiceId);
});

$(document).on('click', '#btnCancelInvoice', function () {
    let $invoiceId = parseInt($(this).data('invoiceid'));
    let $remark = $('#txtCancelRemark').val();
    if (isNaN($invoiceId) || $invoiceId < 1) {
        utility.SetAlert('Invoice Number is invalid', utility.alertType.error);
        return false;
    }
    else if ($remark.length < 5) {
        utility.SetAlert('Please enter the invoice cancellation remark more than 5 charectors', utility.alertType.warning);
        $('#txtCancelRemark').addClass('shop_hasError').focus();
        return false;
    }
    else {
        $('#txtCancelRemark').removeClass('shop_hasError');
    }
    utility.ajaxHelper(app.urls.SaleArea.SalesController.CancelInvoice, { invoiceid: $(this).data('invoiceid'), invoiceRemark: $remark }, function (_response) {
        utility.setAjaxAlert(_response);

        $('#txtCancelRemark').val('');

        $('.closePopup').click();

        var $selectedPageSize = parseInt($('#ddlPageSize option:selected').val());
        var $selectedPage = parseInt($('.pageActive').text());

        bindTable($selectedPage, $selectedPageSize);
    });
});
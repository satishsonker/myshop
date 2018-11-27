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
                '<th>' + (ind + 1) + '</th>' +
                '<th>' + ele.InvoiceNo + '</th>' +
                '<th>' + ele.CustomerName + '</th>' +
                '<th>' + app.const.htmlCode.rupeesSymbol + ' ' + parseFloat(ele.Amount).toFixed(2) + '</th>' +
                '<th>' + utility.getJsDateTimeFromJson(ele.InvoiceDate) + '</th>' +
                '<th><span class="label label-' + (ele.IsRefund ? 'warning' : 'success') + '">' + (ele.IsRefund ? 'Return' : 'Sale') + '</th>' +
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
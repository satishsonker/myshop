$(document).ready(function () {
    let $fromToDate = utility.getFromToDate(1);
    $('#txtToDate').val($fromToDate.currentDate);
    $('#txtFromDate').val($fromToDate.beforeDate);
    bindTable(1, 10);
});
$(document).on('change', '#ddlPageSize', function () {
    bindTable(1, $(this).find(':selected').val());
});

function bindTable(pageNo, pageSize, pageBtn) {
    let $selectedPage = pageBtn == undefined ? 'page_1' : $(pageBtn).attr('id');
    let $toDate = $('#txtToDate').val();
    let $fromDate = $('#txtFromDate').val();
    $('[id*="page_"]').removeClass('pageActive');
    utility.ajaxHelper(app.urls.SaleArea.reportsController.GetMostSaleProduct, { PageNo: pageNo, PageSize: pageSize, FromDate: $fromDate, ToDate: $toDate }, function (response) {
        let $table = $('#tblSaleList');
        let $tbody = $($table).find('tbody');
        $('#hdntotalRecord').val(response[0].TotalRecord);
        let $html = '';
        $tbody.empty();
        $(response).each(function (ind, ele) {
            $html += '<tr>' +
                '<th>' + (ind + 1) + '</th>' +
                '<th class="shop_hCentre"><a href="/salesmanagement/sale/GetInvoice?invoiceno=' + ele.ProductName + '">' + ele.ProductName + '</a></th>' +
                '<th class="shop_hCentre">' + ele.TotalQty + '</th>' +
                '</tr>';
        });
        $($tbody).append($html);
        bindPaging();
        $('#' + $selectedPage).addClass('pageActive');
    });
}

function bindPaging() {
    let $paging = $('#tblPaging');
    let $pageSize = parseInt($('#ddlPageSize').find(':selected').val());
    let $noOfRecords = parseInt($('#hdntotalRecord').val());
    let $noOfPage = $noOfRecords % $pageSize > 0 ? ($noOfRecords / $pageSize) + 1 : ($noOfRecords / $pageSize);
    $($paging).empty()
    for (var i = 1; i <= $noOfPage; i++) {
        $($paging).append('<button class="btn btn-default" id="page_' + i + '" onclick="bindTable(' + i + ',' + $pageSize + ',this)" >' + i + '</button>');
    }
}
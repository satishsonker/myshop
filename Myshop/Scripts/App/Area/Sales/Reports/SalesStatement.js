var $saleStatement = {};

$(document).ready(function () {
    let $dates = utility.getFromToDate(1);
    $('#txtToDate').val($dates.currentDate);
    $('#txtFromDate').val($dates.beforeDate);
});

$(document).on('click', '#btnGetStatement', function () {
    let $fromDate = $('#txtFromDate').val();
    let $toDate = $('#txtToDate').val();
    $saleStatement.statement($fromDate,$toDate);
});

$saleStatement.statement = function (fromDate, toDate) {
    var $data = { FromDate: fromDate, ToDate: toDate };
    utility.ajaxHelper(app.urls.SaleArea.reportsController.GetSalesStatement, $data, function (response) {
        let $tbody = $('#tblStatement tbody');
        let $html = '';
        $($tbody).empty();

        $(response).each(function (ind, eleDate) {
            $html += '<tr><td style="background: #bce8f1;" colspan="7"><strong>Invoice Date : ' + new Date(utility.getJsDateTimeFromJson(eleDate.Key).substr(0, 10)).toDateString() + '</strong></td></tr>';
            for (Key in eleDate.Value) {
                $html += '<tr><td style="background: antiquewhite;"><strong>Payment Through</strong></td>' +
                    '<td style="background: antiquewhite;" colspan="6"><strong>' + Key + '</strong></td></tr>';
                $html += '<tr><td class="shop_hCentre"><strong>Invoice No.</strong></td>' +
                    '<td class="' + app.cssClass.hCentre + '"><strong>Customer Name</strong></td>' +
                    '<td class="' + app.cssClass.hCentre + '"><strong>Grand Amount</strong></td>' +
                    '<td class="' + app.cssClass.hCentre + '"><strong>Paid Amount</strong></td>' +
                    '<td class="' + app.cssClass.hCentre + '"><strong>Balance Amount</strong></td>' +
                    '<td class="' + app.cssClass.hCentre + '"><strong>Refund Amount</strong></td>' +
                    '<td class="' + app.cssClass.hCentre + '"><strong>Referance No.</strong></td></tr>';
                var $total_grand = 0;
                var $total_paid = 0;
                var $total_balance = 0;
                var $total_refund = 0;
                $(eleDate.Value[Key]).each(function (ind, eleDetail) {                    
                    $html += '<tr><td><a href="/salesmanagement/sale/GetInvoice?invoiceno=' + eleDetail.InvoiceId + '">' + eleDetail.InvoiceId + '</a></td>';
                    $html += '<td>' + eleDetail.CustomerName + '</td>';
                    $html += '<td class="' + app.cssClass.hRigth + '">' + utility.toDecimal(eleDetail.GrandTotal) + '</td>';
                    $html += '<td class="' + app.cssClass.hRigth + '">' + utility.toDecimal(eleDetail.PaidAmount) + '</td>';
                    $html += '<td class="' + app.cssClass.hRigth + '">' + utility.toDecimal(eleDetail.BalanceAmount) + '</td>';
                    $html += '<td class="' + app.cssClass.hRigth + '">' + utility.toDecimal(eleDetail.RefundAmount) + '</td>';
                    $html += '<td class="' + app.cssClass.hRigth + '">' + (eleDetail.PayRefNo == null ? '' : eleDetail.PayRefNo) + '</td></tr>';
                    $total_grand += eleDetail.GrandTotal;
                    $total_paid += eleDetail.PaidAmount;
                    $total_balance += eleDetail.BalanceAmount;
                    $total_refund += eleDetail.RefundAmount;
                });
                $html += '<tr><td colspan="2" class="shop_hRigth">Total</td>' +
                    '<td class="shop_hRigth"><strong>' + utility.getInrCurrency($total_grand) + '</strong></td>' +
                    '<td class="shop_hRigth"><strong>' + utility.getInrCurrency($total_paid) + '</strong></td>' +
                    '<td class="shop_hRigth">' + utility.getInrCurrency($total_balance) + '</td>' +
                    '<td class="shop_hRigth">' + utility.getInrCurrency($total_refund) + '</td><td></td></tr>';
            } 
            $html += '<tr><td style="height: 35px;background:white;" colspan="7"></td></tr>';
        });
        $($tbody).append($html);
    });
}
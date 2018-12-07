var $saleStatement = {};

$(document).ready(function () {
    var now = new Date();

    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);

    var today = now.getFullYear() + "-" + (month) + "-" + (day);

    var monthbefore = new Date();
    monthbefore.setMonth(monthbefore.getMonth()-1);

    var monthbeforeDay = ("0" + monthbefore.getDate()).slice(-2);
    var monthbeforeMonth = ("0" + (monthbefore.getMonth() + 1)).slice(-2);

    var today = now.getFullYear() + "-" + (month) + "-" + (day);
    var monthbeforeToday = monthbefore.getFullYear() + "-" + (monthbeforeMonth) + "-" + (monthbeforeDay);
    $('#txtToDate').val(today);
    $('#txtFromDate').val(monthbeforeToday);
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
                $html += '<tr><td style="background: antiquewhite;"><strong>Payment Through</strong></td><td style="background: antiquewhite;" colspan="6"><strong>' + Key + '</strong></td></tr>';
                $html += '<tr><td>Invoice No.</td><td>Customer Name</td><td>Grand Amount</td><td>Paid Amount</td><td>Balance Amount</td><td>Refund Amount</td><td>Referance No.</td></tr>';
                var $total_grand = 0;
                var $total_paid = 0;
                var $total_balance = 0;
                var $total_refund = 0;
                $(eleDate.Value[Key]).each(function (ind, eleDetail) {                    
                    $html += '<tr><td><a href="/salesmanagement/sale/GetInvoice?invoiceno=' + eleDetail.InvoiceId + '">' + eleDetail.InvoiceId + '</a></td>';
                    $html += '<td>' + eleDetail.CustomerName + '</td>';
                    $html += '<td class="shop_hRigth">' + parseFloat(eleDetail.GrandTotal).toFixed(2) + '</td>';
                    $html += '<td class="shop_hRigth">' + parseFloat(eleDetail.PaidAmount).toFixed(2) + '</td>';
                    $html += '<td class="shop_hRigth">' + parseFloat(eleDetail.BalanceAmount).toFixed(2) + '</td>';
                    $html += '<td class="shop_hRigth">' + parseFloat(eleDetail.RefundAmount).toFixed(2) + '</td>';
                    $html += '<td class="shop_hRigth">' + (eleDetail.PayRefNo == null ? '' : eleDetail.PayRefNo) + '</td></tr>';
                    $total_grand += eleDetail.GrandTotal;
                    $total_paid += eleDetail.PaidAmount;
                    $total_balance += eleDetail.BalanceAmount;
                    $total_refund += eleDetail.RefundAmount;
                });
                $html += '<tr><td colspan="2" class="shop_hRigth">Total</td><td class="shop_hRigth"><strong>' + utility.getInrCurrency($total_grand) + '</strong></td><td class="shop_hRigth"><strong>' + utility.getInrCurrency($total_paid) + '</strong></td><td class="shop_hRigth">' + utility.getInrCurrency($total_balance) + '</td><td class="shop_hRigth">' + utility.getInrCurrency($total_refund) + '</td><td></td></tr>';
            } 
            $html += '<tr><td style="height: 35px;background:white;" colspan="7"></td></tr>';
        });
        $($tbody).append($html);
    });
}
var $saleStatement = {};

$(document).ready(function () {
    let $fromToDate = utility.getFromToDate(1);
    $('#txtToDate').val($fromToDate.currentDate);
    $('#txtFromDate').val($fromToDate.beforDate);
});

$(document).on('click', '#btnGetStatement', function () {
    let $fromDate = $('#txtFromDate').val();
    let $toDate = $('#txtToDate').val();
    $saleStatement.statement($fromDate,$toDate);
});

$saleStatement.statement = function (fromDate, toDate) {
    var $data = { FromDate: fromDate, ToDate: toDate };
    utility.ajaxHelper(app.urls.SaleArea.reportsController.GetGstStatement, $data, function (response) {
        let $tbody = $('#tblStatement tbody');
        let $html = '';
        $($tbody).empty();

        $(response).each(function (ind, eleDate) {
            $html += '<tr><td style="background: #bce8f1;" colspan="7"><strong>Invoice Date : ' + new Date(utility.getJsDateTimeFromJson(eleDate.Key).substr(0, 10)).toDateString() + '</strong></td></tr>';
            $html += '<tr><td>Invoice No.</td><td>Customer Name</td><td>Sub Total Amount</td><td>GST Rate</td><td>GST Amount</td><td>Grand Amount</td></tr>';
            var $total_grand = 0;
            var $total_sub = 0;
            var $total_gstRate = 0;
            for (Key in eleDate.Value) {
                $(eleDate.Value[Key]).each(function (ind, eleDetail) {                    
                    $html += '<tr><td><a href="/salesmanagement/sale/GetInvoice?invoiceno=' + eleDetail.InvoiceId + '">' + eleDetail.InvoiceId + '</a></td>';
                    $html += '<td>' + eleDetail.CustomerName + '</td>';
                    $html += '<td class="shop_hRigth">' + parseFloat(eleDetail.GrandTotal - eleDetail.GstAmount).toFixed(2) + '</td>';
                    $html += '<td class="shop_hRigth"><img class="shop_info" src="../../Images/Icons/info.png" title="GST Rate is as same as the time of this invoice generated" />' + parseFloat(eleDetail.GstRate).toFixed(2) + '%</td>';
                    
                    $html += '<td class="shop_hRigth">' + parseFloat(eleDetail.GstAmount).toFixed(2) + '</td>';
                    $html += '<td class="shop_hRigth">' + parseFloat(eleDetail.GrandTotal).toFixed(2) + '</td>';
                    $total_grand += eleDetail.GrandTotal;
                    $total_gstRate += eleDetail.GstAmount;
                    $total_sub += eleDetail.GrandTotal - eleDetail.GstAmount;
                });                
            }
            $html += '<tr><td colspan="2" class="shop_hRigth">Total</td><td class="shop_hRigth"><strong>' + utility.getInrCurrency($total_sub) + '</strong></td><td class="shop_hRigth"></td><td class="shop_hRigth">' + utility.getInrCurrency($total_gstRate) + '</td><td class="shop_hRigth"><strong>' + utility.getInrCurrency($total_grand) + '</strong></td></tr>';
            $html += '<tr><td style="height: 35px;background:white;" colspan="7"></td></tr>';
        });
        $($tbody).append($html);
    });
}
var $balReport = {};

$(document).ready(function () {
    utility.bindDdlByAjax(app.urls.GetVendorJson, 'ddlVendor', 'VendorName', 'VendorId');
    $balReport.bindReport(new Date().getFullYear(), new Date().getMonth() + 1, 0);
});

$(document).on('click', '.filterTab,tr[data-toggle="collapse"]', function () {
    if ($(this).find('i').hasClass('fa-sort-down')) {
        $(this).find('i').removeClass('fa-sort-down').addClass('fa-sort-up');
        if ($(this).hasClass('filterTab'))
            $('.filterTabCon').show(500);
    }
    else if ($(this).find('i').hasClass('fa-sort-up')) {
        $(this).find('i').removeClass('fa-sort-up').addClass('fa-sort-down');
        if ($(this).hasClass('filterTab'))
            $('.filterTabCon').hide(500);
    }
});

$(document).on('change', '#ddlYear,#ddlVendor,#ddlMonth', function () {
    let $year = $('#ddlYear').find(':selected').val();
    
    let $vendor = $('#ddlVendor').find(':selected').val();
    if ($year === '0') {
        $('#ddlMonth').val('0');
    }

    let $month = $('#ddlMonth').find(':selected').val();

    $balReport.bindReport($year, $month, $vendor);
});

$balReport.bindReport = function (year, month, vendorid) {
    $balReport.getReport(year, month, vendorid).then(function (resp) {

        let $html = '';
        for (key in resp) {
            var $totalBal = 0.00;
            resp[key].map(function (ele) {
                $totalBal += ele.BalanceAmount;
            }, $totalBal);

            $html += '<tr>' +
                '<td colspan="7" style="background: #3ea2e5;"> <i class="fas fa-user black-text"><span data-toggle="tooltip" data-placement="top" title="Vendor Name">' + key + '</span></i> <span style="float:right;font-size:15px;color:black;">Total balance <strong>' + utility.getInrCurrency($totalBal) + '</strong></span></td>' +
                '</tr>' +
                '<tr class="asth">' +
                '<td class="shop_vMiddle shop_hCentre">Sr. No</td>' +
                '<td class="shop_vMiddle shop_hCentre">Expense No.</td>' +
                '<td class="shop_vMiddle shop_hCentre">Total Amount</td>' +
                '<td class="shop_vMiddle shop_hCentre">Paid Amount</td>' +
                '<td class="shop_vMiddle shop_hCentre">Cancel Amount</td>' +
                '<td class="shop_vMiddle shop_hCentre">Balance Amount</td>' +
                '<td class="shop_vMiddle shop_hCentre">Date</td>' +
                '</tr>';
            $(resp[key]).each(function (indP, eleP) {
                var $cancelAmount = 0.00;
                $(eleP.Data).each(function (indCr, eleCr) {
                    if (eleCr.IsCancelled) {
                        $cancelAmount += eleCr.Amount;
                    }
                });
                var $reason = eleP.CancelReason;
                var $date = utility.getJsDateTimeFromJson(eleP.CancelDate);
                $html += '<tr data-toggle="collapse" data-target="#exp'  + eleP.ExpId + '" ' + (eleP.IsCancelled ? 'class="cancelitem"  title="Reason : ' + $reason + '\n Date : ' + utility.getJsDateTimeFromJson($date) + '"' : '') + '>' +
                    '<td > <span class="badge badge-danger">' + (indP + 1) + '</span><i class="fas fa-sort-down pull-right black-text"></i></td>' +
                    '<td class="shop_vMiddle shop_hCentre"><span data-toggle="tooltip" data-placement="top" title="Click here to view expense details"> <a class="expNo" href="/expensemanagement/Expense/ExpenseDetails?expid=' + eleP.ExpId + '">' + eleP.ExpId + '</a></span></td> ' +
                    '<td class="shop_vMiddle shop_hRigth">' + utility.getInrCurrency(eleP.TotalAmount) + '</td > ' +
                    '<td class="shop_vMiddle shop_hRigth">' + utility.getInrCurrency(eleP.PaidAmount) + '</td > ' +
                    '<td class="shop_vMiddle shop_hRigth">' + utility.getInrCurrency($cancelAmount) + '</td > ' +
                    '<td class="shop_vMiddle shop_hRigth">' + utility.getInrCurrency(eleP.BalanceAmount) + '</td>' +
                    '<td class="shop_vMiddle shop_hCentre">' + utility.getJsDateTimeFromJson(eleP.CreatedDate) + '</td>' +
                    '</tr >' +
                    '<tr id="exp' + eleP.ExpId + '" class="collapse out">' +
                    '<td colspan="7" style="background: chocolate;">' +
                    '<table class="table table-bordered table-condensed table-responsive table-striped" style="margin-bottom:0px;" id="innerTable">' +
                    '<thead>' +
                    '<tr>' +
                    '<th class="shop_vMiddle shop_hCentre">Sr. No</th>' +
                    '<th class="shop_vMiddle shop_hCentre">Item Name</th>' +
                    '<th class="shop_vMiddle shop_hCentre">Price</th>' +
                    '<th class="shop_vMiddle shop_hCentre">Unit</th>' +
                    '<th class="shop_vMiddle shop_hCentre">Qty</th>' +
                    '<th class="shop_vMiddle shop_hCentre">Amount</th>' +
                    '</tr>' +
                    '</thead>' +
                    '<tbody>';
                $(eleP.Data).each(function (indC, eleC) {
                    var $reason = eleP.CancelReason === '' ? eleC.CancelReason : eleP.CancelReason;
                    var $date = utility.getJsDateTimeFromJson(eleP.CancelDate).indexOf('1/1/1') > -1 ? eleC.CancelDate : eleP.CancelDate;
                    $html += '<tr ' + (eleC.IsCancelled ? 'class="cancelitem" data-toggle="tooltip" title="Reason : ' + $reason + '\n Date : ' + utility.getJsDateTimeFromJson($date) + '"' : '') + '>' +
                        '<td class="shop_vMiddle shop_hCentre"><span class="badge ' + (eleC.IsCancelled ? 'badge-danger' : 'badge-info') + '">' + (indC + 1) + '</span></td>' +
                        '<td class="shop_vMiddle shop_hCentre">' + eleC.ItemName + '</td>' +
                        '<td class="shop_vMiddle shop_hRigth">' + utility.getInrCurrency(eleC.Price) + '</td>' +
                        '<td class="shop_vMiddle shop_hCentre">' + (eleC.Unit) + '</td>' +
                        '<td class="shop_vMiddle shop_hRigth">' + parseFloat(eleC.Qty).toFixed(2) + '</td>' +
                        '<td class="shop_vMiddle shop_hRigth">' + utility.getInrCurrency(eleC.Amount) + '</td>' +
                        '</tr>';
                });
                var $totalPaid = 0.00;
                eleP.Data.map(function (ele) {
                    $totalPaid += ele.Amount;
                }, $totalPaid);
                $html += '</tbody>' +
                    '<tfoot>' +
                    '<tr>' +
                    '<td colspan="5" class="shop_vMiddle shop_hRigth"><strong>Total Amount</strong></td>' +
                    '<td class="shop_vMiddle shop_hRigth">' + utility.getInrCurrency($totalPaid) + '</td>' +
                    '</tr>' +
                    '</tfoot>' +
                    '</table>' +
                    '</td>' +
                    '</tr>';
            });
        }
        if ($html === '') {
            $html += '<tr><td class="shop_vMiddle shop_hCentre"><span class="badge badge-danger">No Record Found</span></td></tr>';
        }
        $('#tblBalReport tbody').empty().append($html);

        $('[data-toggle="tooltip"]').tooltip();
    });
}

$balReport.getReport = function (year, month, vendorid) {
    return new Promise(function (res, rej) {
        utility.ajaxHelper(app.urls.ExpenseArea.ReportsController.GetBalanceReport, { Year: year, Month: month, VendorId: vendorid }, function (resp) {
            res(resp);
        }, function (x) {
            rej(x);
        });
    });
}
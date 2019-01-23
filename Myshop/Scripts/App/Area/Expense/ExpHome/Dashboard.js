$(document).ready(function () {
    let $year = $('#ddlYear').find(':selected').val(), $month = $('#ddlMonth').find(':selected').val();
    let $filterData = { Year: $year, Month: $month };
    getDashboardData($filterData);
   });

$(document).on('click', '.filterTab', function () {
    if ($(this).find('i').hasClass('fa-sort-down')) {
        $(this).find('i').removeClass('fa-sort-down').addClass('fa-sort-up');
        $('.filterTabCon').show(500);
    }
    else if ($(this).find('i').hasClass('fa-sort-up')) {
        $(this).find('i').removeClass('fa-sort-up').addClass('fa-sort-down');
        $('.filterTabCon').hide(500);
    }
});

$(document).on('click', '#ddlYear,#ddlMonth', function () {
    let $year = $('#ddlYear').find(':selected').val(), $month = $('#ddlMonth').find(':selected').val();
    let $filterData = { Year: $year, Month: $month };
    getDashboardData($filterData);
});

function getDashboardData($filterData) {
    utility.ajaxHelper(app.urls.ExpenseArea.ExpenseHomeController.GetMonthlyExpenseChart, $filterData, function (resp) {
        $('#line-chart').addClass('graph-Main').empty();
        if (resp.length > 0) {
            var config = {
                data: resp,
                xkey: 'Y',
                ykeys: ['A'],
                labels: ['Total Expense', 'Date'],
                fillOpacity: 0.6,
                hideHover: 'auto',
                behaveLikeLine: true,
                resize: true,
                pointFillColors: ['#ffffff'],
                pointStrokeColors: ['black'],
                lineColors: ['green'],
                parseTime: false,
                gridIntegers: true,
                redraw: true, resize: true
            };
            config.element = 'line-chart';
            Morris.Line(config);
            $('.morris-hover').remove();
        }
        else {
            $('#line-chart').removeClass('graph-Main').append('<span class="badge badge-danger" style="padding:20px;margin:20px;">No Data Found</span>');
        }
    });

    utility.ajaxHelper(app.urls.ExpenseArea.ExpenseHomeController.TopExpenses, $filterData, function (resp) {

        let $html = '';
        if (resp.length > 0) {
            let $totalAmount = resp[0].MonthlyExpense;
            $('#monthlyExpense').text('Total Expense : ' + app.const.htmlCode.rupeesSign+' ' + parseFloat($totalAmount).toFixed(2));
            $(resp).each(function (ind, ele) {
                var $totalPercent = parseFloat(((ele.Amount / $totalAmount) * 100)).toFixed(2);
                $html += '<tr>';
                $html += '<td style="padding: 3px;"><div class="progress" style="background-color: #e6e0e0;height: 7px;"><div class="progress-bar" role="progressbar" aria-valuenow="' + $totalPercent + '" aria-valuemin="0" aria-valuemax="100" style="width:' + $totalPercent + '%;color: black;background-color:' + utility.getProgressbarColor($totalPercent) + '"></div></div><span style="position: absolute;float: left;margin: -20px 0 0 0;"><strong>' + ele.Item + '</strong> , ' + utility.getInrCurrency(ele.Amount) + '</span><span style="float: right;margin: -20px 0 0 0;">' + $totalPercent + '%</span></td>';

                $html += '</tr>';
            });
        }
        else {
            $html += '<tr><td colspan="3"><span class="badge badge-danger">You didn\' expend anything in selected month</span></td></tr>';
            $('#monthlyExpense').text('Total Expense : 0.00');
        }
        $('#tblTopExp tbody').empty().append($html);
    });

    utility.ajaxHelper(app.urls.ExpenseArea.ExpenseHomeController.TopBalance, $filterData, function (resp) {

        let $html = '';
        if (resp.length > 0) {

            $(resp).each(function (ind, ele) {
                var $totalPercent = parseFloat(((ele.BalanceAmount / ele.TotalAmount) * 100)).toFixed(2);
                $html += '<tr>';
                $html += '<td style="padding: 3px;">' +
                    '<div class="progress" style="background-color: #e6e0e0;height: 7px;">' +
                    '<div class="progress-bar" role="progressbar" aria-valuenow="' + $totalPercent + '" aria-valuemin="0" aria-valuemax="100" style="width:' + $totalPercent + '%;color: black;background-color:' + utility.getProgressbarColor($totalPercent) + '"></div>' +
                    '</div>' +
                    '<span style="position: absolute;float: left;margin: -20px 0 0 0;"><strong>' + ele.VendorName + '</strong></span>' +
                    '<span style="float: right;margin: -20px 0 0 0;">' + $totalPercent + '%</span>' +
                    '<span><strong>Total</strong> : ' + utility.getInrCurrency(ele.TotalAmount) + ', <strong>Paid</strong> : ' + utility.getInrCurrency(ele.PaidAmount) + ', <strong>Balance</strong> : ' + utility.getInrCurrency(ele.BalanceAmount) + '</span>'+
                    '</td>';

                $html += '</tr>';
            });
        }
        else {
            $html += '<tr><td colspan="3"><span class="badge badge-danger">You didn\' expend anything in selected month</span></td></tr>';
            $('#monthlyExpense').text('Total Expense : 0.00');
        }
        $('#tblTopBal tbody').empty().append($html);
    });

    utility.ajaxHelper(app.urls.ExpenseArea.ExpenseHomeController.Overview, $filterData, function (resp) {
        $('#totalExp').text('₹' + parseFloat(resp.TotalExpense).toFixed(2));
        $('#monthExp').text(app.const.htmlCode.rupeesSign + parseFloat(resp.MonExp).toFixed(2));
        $('#monBal').text(app.const.htmlCode.rupeesSign + parseFloat(resp.MonBal).toFixed(2));
        $('#monBig').text(app.const.htmlCode.rupeesSign + parseFloat(resp.BigExp).toFixed(2));
    });

}
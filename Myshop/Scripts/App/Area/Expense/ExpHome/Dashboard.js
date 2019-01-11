$(document).ready(function () {
    utility.ajaxHelper(app.urls.ExpenseArea.ExpenseHomeController.GetMonthlyExpenseChart, { Year: new Date().getFullYear(), Month: new Date().getMonth() + 1 }, function (resp) {
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
            redraw: true,resize: true
        };
        config.element = 'line-chart';
        Morris.Line(config);
        $('.morris-hover').remove();
    });

    utility.ajaxHelper(app.urls.ExpenseArea.ExpenseHomeController.TopExpenses, { Year: new Date().getFullYear(), Month: new Date().getMonth() + 1 }, function (resp) {

        let $html = '';
        if (resp.length > 0) {
            let $totalAmount = resp[0].MonthlyExpense;
            $(resp).each(function (ind, ele) {
                var $totalPercent = parseFloat(((ele.Amount / $totalAmount) * 100)).toFixed(2);
                $html += '<tr>';
                $html += '<td style="padding: 3px;"><div class="progress" style="background-color: #e6e0e0;height: 7px;"><div class="progress-bar" role="progressbar" aria-valuenow="' + $totalPercent + '" aria-valuemin="0" aria-valuemax="100" style="width:' + $totalPercent + '%;color: black;background-color:' + utility.getProgressbarColor($totalPercent) + '"></div></div><span style="position: absolute;float: left;margin: -20px 0 0 0;"><strong>' + ele.Item + '</strong> , ' + utility.getInrCurrency(ele.Amount) + '</span><span style="float: right;margin: -20px 0 0 0;">' + $totalPercent + '%</span></td>';
               
                $html += '</tr>';
            });
        }
        else {
            $html += '<tr><td colspan="3"><span class="badge badge-danger">You didn\' expend anything in selected month</span></td></tr>';
        }
        $('#tblTopExp tbody').append($html);
    });
});
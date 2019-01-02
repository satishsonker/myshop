/// <reference path="../../common/app.js" />
/// <reference path="../../../jquery-1.10.2.js" />
/// <reference path="../../common/validate.js" />
/// <reference path="../../common/utility.js" />

$(document).on('change', '#ddlPeriod', function () {
    utility.ajaxHelper(app.urls.SaleArea.SalesController.GetDashboard, { Days: $(this).val() }, function (response) {
        $('.totalSales').text(response.TotalSales);
        $('.totalProduct').text(response.TotalProduct);
        $('.totalProductQty').text(response.TotalQty);
        if (response.TotalIncome.toString().length > 8)
            $('.totalIncome').attr('style', 'font-size:21px !important;');
        else
            $('.totalIncome').removeAttr('style');

        $('.totalIncome').text("₹" + parseFloat(response.TotalIncome).toFixed(2).toString());

        $('.monthlyIncome').text("₹" + parseFloat(response.MonthlyIncome).toFixed(2).toString());

        $('.TotalIncomeTillNow').text("₹" + parseFloat(response.TotalIncomeTillNow).toFixed(2).toString());

        let $html = '';
        $(response.InvoiceMDetals).each(function (ind, ele) {
            $html += '<tr>'+
                '<td><a href="/salesmanagement/sale/GetInvoice?invoiceno=' + ele.InvoiceNo +'">' + ele.InvoiceNo+'</a></td>'+
                '<td>' + ele.CustomerName +'</td> '+
                '<td>' + ele.Amount + '</td> ' +
                '<td>' + utility.getJsDateTimeFromJson(ele.InvoiceDate) + '</td> ' +
                '<td><span class="label label-' + (ele.IsRefund ? 'warning' : 'success') +'">' +(ele.IsRefund?'Return':'Sale')+'</span></td> '+
                '</tr > ';
        });
        $html = $html != '' ? $html: '<tr><td colspan="5">No Sale</td></tr>';
        $('#tblInvoice').empty().append($html);

        $html = '';
        $(response.TopCustomersData).each(function (ind, ele) {
            $html += '<tr>' +
                '<td>' + (ind + 1).toString() + '</td> ' +
                '<td class="shop_hCentre"><a href="#">' + ele.CustomerName + '</a></td>' +
                '<td class="shop_hCentre">' + ele.TotalPurchase + '</td> ' +
                '<td class="shop_hCentre">' + app.const.htmlCode.rupeesSymbol+' '+ ele.TotalPurchaseAmount + '</td> ' +
                '<td class="shop_hCentre">' + ele.TotalPurchaseProduct + '</td> ' +
               '</tr > ';
        });
        $html = $html != '' ? $html : '<tr><td colspan="5">No Products</td></tr>';
        $('#tblTopCustomer').empty().append($html);

        $html = '';
        $(response.SallingProducts).each(function (ind, ele) {
            $html += '<tr>' +
                '<td>' + (ind + 1).toString() + '</td> ' +
                '<td><a href="#">' + ele.ProductName + '</a></td>' +
                '<td>' + ele.TotalQty + '</td> ' +
                '</tr > ';
        });
        $html = $html != '' ? $html : '<tr><td colspan="5">No Products</td></tr>';
        $('#tblsaling').empty().append($html);
    });

    lineProductChart($('#ddlPeriod').val());
    SaleStatusProductChart($('#ddlPeriod').val());
});

$(document).ready(function () {
    $('#ddlPeriod').change();
    lineProductChart($('#ddlPeriod').val());
    SaleStatusProductChart($('#ddlPeriod').val());
});

function lineProductChart(duration) {
    utility.ajaxHelper(app.urls.SaleArea.SalesController.GetSalesChartData, {'Days': duration }, function (data) {
        var jsonData = JSON.parse(data.Data.replace(/#/g, '"').replace('|', '').replace('|', ''));
       var config = {
            data: jsonData,
            xkey: 'y',
            ykeys: ['d'],
            labels: data.Labels,
            fillOpacity: 0.6,
            hideHover: 'auto',
            behaveLikeLine: true,
            resize: true,
            pointFillColors: ['#ffffff'],
            pointStrokeColors: ['black'],
            lineColors: ['green'],
            parseTime: false,
            gridIntegers: true
        };
        $('#line-chart').empty();
        config.element = 'line-chart';
        Morris.Line(config);

        config.ykeys = ['e'];
        config.lineColors= ['red'],
        $('#line-chart-Sale').empty();
        config.element = 'line-chart-Sale';
        Morris.Line(config);
    });
}

function SaleStatusProductChart(duration) {
    utility.ajaxHelper(app.urls.SaleArea.SalesController.GetSalesStatusChartData, { 'Days': duration }, function (data) {
        //var jsonData = JSON.parse(data.Data.replace(/#/g, '"').replace('|', '').replace('|', ''));
        var config = {
            data: data,
            //xkey: 'y',
            //ykeys: ['d'],
            //labels: data.Labels,
            //fillOpacity: 0.6,
            //hideHover: 'auto',
            //behaveLikeLine: true,
            //resize: true,
            //pointFillColors: ['#ffffff'],
            //pointStrokeColors: ['black'],
            //lineColors: ['green'],
            //parseTime: false,
            //gridIntegers: true
            labelColor: "#9CC4E4",
            colors: ['#E53935', 'rgb(0, 188, 212)', 'rgb(255, 152, 0)', 'rgb(0, 150, 136)']
        };
        $('#pie-chart-Sale').empty();
        config.element = 'pie-chart-Sale';
        Morris.Donut(config);
    });
}
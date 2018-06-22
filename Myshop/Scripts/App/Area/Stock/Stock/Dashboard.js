/// <reference path="../../../../jquery-1.10.2.js" />
/// <reference path="../../../Common/App.js" />
/// <reference path="../../../Common/Utility.js" />

$(document).ready(function () {
    utility.bindDdlByAjax('GetStockProductJosn', 'ddlProduct', 'ProductName', 'ProductId');
    
});

$(document).on('click', '#btnGo', function () {
    var productId=$('#ddlProduct').val(),duration=$('#ddlDuration').val();
    
    lineProductChart(productId, duration);
    pieTotalAmountChart(productId, duration);


});

function lineProductChart(pro,duration)
{
    utility.ajaxHelper(app.urls.StockProductLineChart , { 'ProductId': pro, 'Duration': duration }, function (data) {
        var jsonData = JSON.parse(data.Data.replace(/#/g, '"').replace('|', '').replace('|', ''));
        config = {
            data: jsonData,
            xkey: 'y',
            ykeys: ['d1', 'd2', 'd3', 'd4', 'd5'],
            labels: data.Labels,
            fillOpacity: 0.6,
            hideHover: 'auto',
            behaveLikeLine: true,
            resize: true,
            pointFillColors: ['#ffffff'],
            pointStrokeColors: ['black'],
            lineColors: ['gray', 'red', 'black', 'green', 'blue'],
            parseTime: false,
            gridIntegers: true
        };
        $('#line-chart').empty();
        config.element = 'line-chart';
        Morris.Line(config);
    });
}

function pieTotalAmountChart(pro,duration) {
    utility.ajaxHelper(app.urls.StockTotalAmountPieChart, { 'ProductId': pro, 'Duration': duration }, function (data) {
        var jsonData = JSON.parse(data.Data.replace(/#/g, '"').replace('|', '').replace('|', ''));
        config = {
            data: jsonData,
            xkey: 'y',
            ykeys: ['d1', 'd2', 'd3', 'd4', 'd5'],
            labels: data.Labels,
            fillOpacity: 0.6,
            hideHover: 'auto',
            behaveLikeLine: true,
            resize: true,
            pointFillColors: ['#ffffff'],
            pointStrokeColors: ['black'],
            lineColors: ['gray', 'red', 'black', 'green', 'blue'],
            parseTime: false,
            gridIntegers: true
        };
        $('#totalAmount-pie').empty();
        config.element = 'totalAmount-pie';
        Morris.Donut(config);
    });
}
    
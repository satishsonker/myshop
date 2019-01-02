/// <reference path="../../../../jquery-1.10.2.js" />
/// <reference path="../../../Common/App.js" />
/// <reference path="../../../Common/Utility.js" />

$(document).ready(function () {
    //utility.ajaxHelperGet(app.urls.GetCustomerTypeSelectListJson,)
    //utility.bindDdlByAjax(app.urls.GetCustomerTypeSelectListJson, 'ddlCustType');    
    $('#multiselect').multiselect({ data:app.urls.GetCustomerTypeSelectListJson});
});

$(document).on('click', '#btnGo', function () {
    var custTypeId = $('#multiselect').multiselectValue(),
        duration = $('#ddlDuration').val();

    if (custTypeId !== null && custTypeId.length > 0) {
        if (parseInt(duration) > 0) {
            $('#line-chart').empty();
            $('#totalAmount-pie').empty();
            $('#totalQty-pie').empty();
            lineProductChart(custTypeId, duration);
            pieTotalAmountChart(custTypeId, duration);
           // pieTotalQtyChart(custTypeId, duration);
        }
        else {
            utility.SetAlert("Please select duration",app.const.alertType.warning);
        }
    }
    else
    {
        utility.SetAlert("Please select atleast one customer type", app.const.alertType.warning);
    }
});

function lineProductChart(pro,duration)
{
    utility.ajaxHelper(app.urls.GetCustomesChartData, { 'CustTypeId': pro, 'Duration': duration }, function (data) {
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
    utility.ajaxHelper(app.urls.GetTotalCustomerTypePieChartData, { 'CustTypeId': pro, 'Duration': duration }, function (data) {
        var jsonData = data;
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
    
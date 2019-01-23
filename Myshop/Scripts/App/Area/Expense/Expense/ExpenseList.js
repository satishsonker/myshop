
$(document).ready(function () {
    utility.bindDdlByAjax(app.urls.GetPayModeJson, 'ddlPayMode', 'PayMode', 'PayModeId');
    utility.setDateToDateControl('txtFromDate', new Date(new Date().setMonth(-1)));
    utility.setDateToDateControl('txtToDate', new Date());
    bindTable(1, 10);
    utility.setDateRange('txtFromDate', 'txtToDate');
});
$(document).on('change', '#ddlPageSize,#ddlPayMode,#txtFromDate,#txtToDate', function () {
    bindTable(1, $('#ddlPageSize').find(':selected').val());
});

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

function bindTable(pageNo, pageSize, pageBtn) {
    let $selectedPage = pageBtn == undefined ? 'page_1' : $(pageBtn).attr('id');
    let $payid = $('#ddlPayMode').find(':selected').val();
    let $fromDate = $('#txtFromDate').val();
    let $toDate = $('#txtToDate').val();
    $('[id*="page_"]').removeClass('pageActive');
    utility.ajaxHelper(app.urls.ExpenseArea.ExpenseController.ExpenseList, { from: $fromDate, to: $toDate, paymodeid: $payid, pageSize: pageSize, pageNo: pageNo }, function (resp) {
        let $html = '';
        if (resp.length > 0) {
            $('#hdntotalRecord').val(resp[0].TotalRecords);
            $(resp).each(function (ind, ele) {
                if (ele.IsCancelled) {
                    $html += '<tr data-toggle="tooltip" class="cancelitem" title="Cancelled Expense\nReason: ' + ele.CancelReason + '\nDate: ' + utility.getJsDateTimeFromJson(ele.CancelledDate) + '">';
                } else {
                    $html += '<tr>';
                }
                $html += '<td><span  class="badge badge-danger">' + (ind + 1) + '</span></td>';
                $html += '<td class="shop_vMiddle shop_hCentre"><a class="expNo" href="ExpenseDetails?expid=' + ele.ExpId + '">' + ele.ExpId + '</a></td>';
                $html += '<td>' + utility.getJsDateTimeFromJson(ele.CreatedDate) + '</td>';
                $html += '<td class="shop_vMiddle shop_hRigth">' + utility.getInrCurrency(ele.TotalAmout) + '</td>';
                $html += '<td class="shop_vMiddle shop_hRigth">' + utility.getInrCurrency(ele.PaidAmount) + '</td>';
                $html += '<td class="shop_vMiddle shop_hRigth">' + utility.getInrCurrency(ele.BalanceAmount) + '</td>';
                $html += '<td class="shop_vMiddle shop_hRigth">' + utility.getInrCurrency(ele.BalancePaidAmount) + '</td>';
                $html += '<td>' + ele.PayMode + '</td>';
                $html += '<td>' + ele.CreatedBy + '</td>';
                $html += '</tr>';
            });
            $('#tblExpList tbody').empty().append($html);
            bindPaging();
            $('[data-toggle="tooltip"]').tooltip(); 
            $('#' + $selectedPage).addClass('pageActive');
        } else {
            $('#tblExpList tbody').empty().append('<tr><td colspan="7" style="text-align:center;">No Record Found</td></tr>');
            $('#tblPaging').empty();
        }
    });
}

$(document).on('click', '.filterTab', function () {
    if ($(this).find('i').hasClass('fa-sort-down')) {
        $(this).find('i').removeClass('fa-sort-down').addClass('fa-sort-up');
        $('.filterTabCon').show(500);
    }
    else if ($(this).find('i').hasClass('fa-sort-up')){
        $(this).find('i').removeClass('fa-sort-up').addClass('fa-sort-down');
        $('.filterTabCon').hide(500);
    }
});
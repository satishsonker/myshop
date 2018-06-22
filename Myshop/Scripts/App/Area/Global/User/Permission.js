/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../Common/App.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).ready(function () {
    utility.bindDdlByAjax(app.urls.GetModuleUrl, 'moduleid');
    utility.bindDdlByAjaxWithParam(app.urls.GetUserUrl, 'users', { allList: false }, "Name", "UserId");
    utility.disableCtrl('moduleid');
});

$(document).on('change', '#moduleid', function () {
    let moduleId = parseInt($(this).find('option:selected').val());
    let userId = parseInt($('#users').find('option:selected').val());
    var table = $('#popuptable tbody');
    table.empty();
    if (isNaN(userId) ) {
        utility.SetAlert('Please select user', 'warning');
    }
    else if (isNaN(moduleId)) {
        utility.SetAlert('Please select application module', 'warning');
    }
    else {
        utility.ajaxHelper(app.urls.GetPermissionJson, { moduleid: moduleId,userid:userId }, function (data) {
            var tblContainer = $('.popup');
            if (data.length > 0) {
                tblContainer.show(); // Show the table

                //Looping data object
                $(data).each(function (ind, ele) {
                    var html = '<tr>';
                    html += '<td>' + (ind + 1) + '</td>';
                    html += '<td>' + ele.PageName + '</td>';
                    html += '<td>' + ele.Url + '</td>';
                    html += '<td><input type="checkbox" id="isblockAccess_' + ind + '" name="isblockAccess"/></td>';
                    html += '<td><input type="checkbox" id="read_' + ind + '" name="read" /></td>';
                    html += '<td><input type="checkbox" id="write_' + ind + '" name="write"/></td>';
                    html += '<td><input type="checkbox" id="update_' + ind + '" name="update"/></td>';
                    html += '<td><input type="checkbox" id="delete_' + ind + '" name="delete"/></td>';
                    html += '<td><input type="button"  id="btnUpdate_' + ind + '" name="btnupdate_' + ind + '" value="Update" onclick="updateSingal(this)" data-data=""/><input type="button" value="Delete" onclick="updateSingal(this)" data-data="' + ele.PermissionId + '"/></td>';
                    html += '</tr>';

                    $(table).append(html);

                    $('#read_' + ind).attr('checked', ele.Read);
                    $('#write_' + ind).attr('checked', ele.Write);
                    $('#update_' + ind).attr('checked', ele.Update);
                    $('#delete_' + ind).attr('checked', ele.Delete);
                    $('#isblockAccess_' + ind).attr('checked', ele.IsBlockAccess);
                    $('#btnUpdate_' + ind).data('data', ele);
                });
            }
            else {
                utility.SetAlert('Data Not found', 'info');
            }
        }, utility.errorCall);
    }
});
$(document).on('change', '#users', function () {
    let userId = parseInt($(this).find('option:selected').val());
    if (isNaN(userId))
    {
        utility.disableCtrl('moduleid');
    }
    else
    {
        utility.enableCtrl('moduleid');
        $('#moduleid').val('');
    }
    $('#popuptable tbody').empty();
});
$(document).on('change', 'select[id*="pageid_"]', function () {
    let pageid = parseInt($(this).find(':selected').val());
    if (pageid > 0) {
        let data = $(this).data('data');
        let btnInsert = $(this).parent().parent().find('td:eq(8) input[value="Insert"]');
        let td = $(this).parent().parent().find('td:eq(2)');

        if (data.length > 0) {
            $(data).each(function (ind, ele) {
                if(ele.Value==pageid)
                {
                    var table = $('#popuptable tbody tr');
                    var flag=false;
                    $(table).each((ind1, ele1) => {
                        if($(ele1).find('td:eq(2)').text().toLowerCase() == ele.Url.toLowerCase())
                        {
                            flag = true;
                        }
                    });
                    if (!flag) {
                        $(td).text(ele.Url);
                        $(btnInsert).removeAttr('disabled').removeClass('disableCtrl');
                    }
                    else
                    {
                        utility.SetAlert('This Page is already Added', 'warning');
                        $(td).text('');
                        $(btnInsert).attr('disabled', 'disabled').addClass('disableCtrl');
                    }
                }
            });
           
        }
        else {
            $(td).text('');
        }
    }
});


function selectAll(ele) {
    var isChecked = $(ele).is(':checked');
    var id = $(ele).attr('id');
    switch (id) {
        case 'isBlock':
            $('[id*="isblockAccess_"]').prop('checked', isChecked);
            break;
        case 'isView':
            $('[id*="read_"]').prop('checked', isChecked);
            break;
        case 'isInsert':
            $('[id*="write_"]').prop('checked', isChecked);
            break;
        case 'isUpdate':
            $('[id*="update_"]').prop('checked', isChecked);
            break;
        case 'isDelete':
            $('[id*="delete_"]').prop('checked', isChecked);
            break;
    }
};

function updateSingal(ctrl) {
    var val = parseInt($('#users').find('option:selected').val());
    let pageId = parseInt($(ctrl).parent().parent().find('td:eq(1) select option:selected').val());
    let data = $(ctrl).data('data');
    pageId = (isNaN(pageId) ? data.PageId : pageId)===undefined ? data : (isNaN(pageId) ? data.PageId : pageId);
    if (isNaN(val)) {
        utility.SetAlert('Please select user', 'warning');
    }
    else {
       
        let inputData = [];
        let $data = {};
        let type = $(ctrl).attr('value');
        let checkbox = $(ctrl).parent().parent().find('input');
        if(type=="Update All")
        {
            checkbox = $('[id*="btnUpdate_"]');
            let newCtrl = "";
            $(checkbox).each(function (ind, ele) {
                var newData = {};
                newCtrl = $(ele).parent().parent().find('input');
                newData.UserId = val;
                newData.PageName = $(newCtrl[0]).val();
                newData.Url = $(newCtrl[1]).val();
                newData.Read = $(newCtrl[3]).is(':checked');
                newData.Write = $(newCtrl[4]).is(':checked');
                newData.Delete = $(newCtrl[6]).is(':checked');
                newData.Update = $(newCtrl[5]).is(':checked');
                newData.IsBlockAccess = $(newCtrl[2]).is(':checked');
                newData.PermissionId = $(newCtrl[7]).data('data') === undefined ? 0 : $(newCtrl[7]).data('data').PermissionId;
                inputData.push(newData);
            });
        }
        else
        {
            $data.UserId = val;
            $data.PageId = pageId;
            $data.PageName = $(checkbox[0]).val();
            $data.Url = $(checkbox[1]).val();
            $data.Read = $(checkbox[1]).is(':checked');
            $data.Write = $(checkbox[2]).is(':checked');
            $data.Delete = $(checkbox[4]).is(':checked');
            $data.Update = $(checkbox[3]).is(':checked');
            $data.IsBlockAccess = $(checkbox[0]).is(':checked');
            inputData = [];
            inputData[0] = $data;
        }
        let url = "";
        switch (type) {
            case 'Insert':
                url = app.urls.SaveSinglePermission;
                $data.PermissionId = 0;
                break;
            case 'Delete':
                url = app.urls.DeletesSinglePermission;
                $data.PermissionId = data;
                break;
            case 'Update':
                url = app.urls.UpdateSinglePermission;
                $data.PermissionId = data.PermissionId;
                break;
            case 'Update All':
                url = app.urls.UpdateSinglePermission;
                break;
        }
       
        utility.ajaxHelper(url, inputData, function (response) {          
            utility.SetAlert(response, 'info');
            $('#moduleid').change();
        });
    }
}


function addRow() {
    var val = parseInt($('#users').find('option:selected').val());
    let moduleId = parseInt($('#moduleid').find('option:selected').val());
    if (isNaN(val)) {
        utility.SetAlert('Please select user', 'warning');
    }
    else if (isNaN(moduleId)) {
        utility.SetAlert('Please select application module', 'warning');
    }
    else {
        var table = $('#popuptable tbody');
        let trs = $(table).find('tr').length;
        var html = '<tr>';
        html += '<td>' + (trs + 1) + '</td>';
        html += '<td> <select id="pageid_'+trs+'" class="ddl" data-data="" required="required" ><option value="">Select Page</option></select></td>';
        html += '<td></td>';
        html += '<td><input type="checkbox" id="isblockAccess_' + trs + '" name="isblockAccess"/></td>';
        html += '<td><input type="checkbox" id="read_' + trs + '" name="read" /></td>';
        html += '<td><input type="checkbox" id="write_' + trs + '" name="write"/></td>';
        html += '<td><input type="checkbox" id="update_' + trs + '" name="update"/></td>';
        html += '<td><input type="checkbox" id="delete_' + trs + '" name="delete"/></td>';
        html += '<td><input type="button" value="Insert" id="btnUpdate_" onclick="updateSingal(this)"/><input type="button" value="Remove" onclick="deleteRow(this)"/></td>';
        html += '</tr>';
        $(table).append(html);

        utility.bindDdlByAjaxWithParam(app.urls.GetPageUrl, 'pageid_' + trs, { moduleid: moduleId }, undefined, undefined, 'data');
    }
}

function deleteRow(ctrl) {
   
    $(ctrl).parent().parent().find('td').css('background-color', 'red').fadeOut(800);
    setTimeout(function () {
        $(ctrl).parent().parent().remove();
    }, 800);
    var table = $('#popuptable tbody tr');
    $(table).each((ind1, ele1) => {
        $(ele1).find('td:eq(0)').text((ind1 + 1));
    });
}
/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).ready(function () {
    bindTable();
});

function bindTable() {
    var table = $('#popuptable tbody');
    utility.ajaxHelper(app.urls.GetUserJson,{allList:true}, function (data) {
            var tblContainer = $('.popup');
            if (data.length > 0) {
                tblContainer.show(); // Sho the table

                table.empty();
                //Looping data object
                $(data).each(function (ind, ele) {
                    var html = '<tr>';
                    html += '<td>' + (ind + 1) + '</td>';
                    html += '<td>' + ele.Username + '</td>';
                    html += '<td>' + ele.Name + '</td>';
                    html += '<td>' + ele.UserType + '</td>';
                    html += '<td><input type="checkbox" id="isactive_' + ind + '" name="isactive" /></td>';
                    html += '<td><input type="checkbox" id="isblocked_' + ind + '" name="isblocked"/></td>';
                    html += '<td><input type="button"  id="btnUpdate_' + ind + '" name="btnupdate_' + ind + '" value="Update" onclick="updateAccess(this)" data-data=""/></td>';
                    html += '</tr>';

                    $(table).append(html);

                    $('#isactive_' + ind).attr('checked', ele.IsActive);
                    $('#isblocked_' + ind).attr('checked', ele.IsBlocked);
                    $('#btnUpdate_' + ind).data('data', ele);
                });
            }
        }, utility.errorCall);
};

function updateAccess(ctrl) {

    let data = $(ctrl).data('data').UserId;
    let type = $(ctrl).attr('id');
    let inputData = [];
    let $data = {};
    let checkbox = $(ctrl).parent().parent().find('input');

    $data.UserId = data;
    $data.IsActive = $(checkbox[0]).is(':checked');
    $data.IsBlocked = $(checkbox[1]).is(':checked');
    inputData = [];
    inputData[0] = $data;
    utility.ajaxHelper(app.urls.UpdateUserAccess, inputData, function (response) {
        utility.SetAlert(response, 'success');
    });
};

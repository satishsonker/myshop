$(document).ready(function () {
    utility.bindDdlByAjax(app.urls.GetUserUrl, 'users', 'Name','UserId');
    utility.bindDdlByAjax(app.urls.GetShopUrl, 'shops');
});

$(document).on('change', "#users", function () {
    var val = parseInt($(this).find(':selected').val());
    let table = $('#popuptable tbody');
    table.empty();
    if(isNaN(val) || val<1)
    {
        utility.SetAlert('Please select user', 'warning');
    }
    else {
        utility.ajaxHelper(app.urls.GetShopMap, { userid: val }, function (response) {
            if(response.length !== undefined && response.length>0)
            {
                $(response).each(function(ind,ele) {
                    var html = '<tr>';
                    html += '<td>' + (ind + 1) + '</td>';
                    html += '<td>' + ele.Name + '</td>';
                    html += '<td>' + ele.ShopName + '</td>';
                    html += '<td>' + ele.Address + '</td>';
                    html += '<td>' + new Date(parseInt(ele.CreatedDate.substr(6))).toDateString() + '</td>';
                    html += '<td><input type="button" value="Delete" data-data="' + ele.ShopId + '" onclick="deleteMap(this)" /></td>';
                    html += '</tr>';
                    table.append(html);
                });
            }
        });
    }

});

$(document).on('click', "#maps", function () {
    let userId = parseInt($('#users').find(':selected').val());
    let shopId = parseInt($('#shops').find(':selected').val());
    if (isNaN(userId) || userId < 1) {
        utility.SetAlert('Please select user', 'warning');
    }
    else if (isNaN(shopId) || shopId < 1) {
        utility.SetAlert('Please select shop', 'warning');
    }
    else {
        utility.ajaxHelper(app.urls.SetShopJson, { userid: userId,shopid:shopId }, function (response) {
            alert(response);
            $('#users').change();       
        });
    }
});

function deleteMap(ctrl) {
    utility.confirmBox('Are you sure..! want to delete.', function () {
        let userId = parseInt($('#users').find(':selected').val());
        let shopId = $(ctrl).data('data');
        if (isNaN(userId) || userId < 1) {
            utility.SetAlert('Please select user', 'warning');
        }
        else {
            utility.ajaxHelper(app.urls.DeleteShopMaps, { userid: userId, shopid: shopId }, function (response) {
                alert(response);
                $('#users').change();
            });
        }
    });
    
};


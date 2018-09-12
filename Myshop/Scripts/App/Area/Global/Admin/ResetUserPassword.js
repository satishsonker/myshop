/// <reference path="../../../Common/Utility.js" />

$(document).on('keydown', '#txtSearchUser', function () {
    let $searchList = $('#searchList');
    if ($(this).val().length > 2)
    {
        utility.ajaxHelper(app.urls.GetUserJson, { 'searchValue': $(this).val() }, function (data) {
            var list = "";
            $(data).each(function (ind, ele) {
                list += '<li data-id="' + ele.UserId + '"><img src="data:image/png;base64,' + ele.Photo + '" style="border-radius: 60px;width: 32px;height: 32px;padding: 0px;margin: 5px;"/>' +
                '<span>' + ele.Name + ' (' + ele.Username + ')</span></li>';
            });
            list = list == "" ? '<li style="border-radius: 60px;width: 32px;height: 32px;padding: 0px;margin: 5px;"/><span>No Record Found</span></li>' : list;
            $($searchList).empty().append(list);
        })
    }
    else
    {
        $($searchList).empty();
    }
});
$(document).on('click', '#searchList li', function () {
    if ($(this).data('id') !== '') {
        $('#txtSearchUser').val($(this).find('span').text());
        $('#userid').val('0');
    }
    else {
        $('#userid').val($(this).data('id'));
    }
    $(this).parent().empty();
});

$(document).on('click', '#btnResetPassword', function () {
    utility.setFormPostUrl('resetpasswordform', 'ResetUserPassword', 'admin', 'Global');
    $(document).submit();
});
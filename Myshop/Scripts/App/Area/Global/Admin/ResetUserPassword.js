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
            list = list == "" ? '<li style="padding: 10px;text-align: center;"><span>No Record Found</span></li>' : list;
            $($searchList).empty().append(list);
        })
    }
    else
    {
        $($searchList).empty();
    }
});
$(document).on('click', '#searchList li', function () {
    if ($(this).data('id') !== undefined) {
        $('#txtSearchUser').val($(this).find('span').text());
        $('#userid').val($(this).data('id'));
    }
    else {
        $('#userid').val('0');
        $('#txtSearchUser').val('');
    }
    $(this).parent().empty();
});

$(document).on('click', '#btnResetPassword', function () {
    if ($('#txtSearchUser').val() !== '') {
        utility.confirmBox('Are you sure! want to Reset the password.', function () {
            utility.setFormPostUrl('resetpasswordform', 'ResetUserPassword', 'admin', 'Global');
            $(document).submit();
        });
    }
    else {
       // utility.SetAlert("Please set the user", utility.alertType.warning);
        return false;
    }
   
});
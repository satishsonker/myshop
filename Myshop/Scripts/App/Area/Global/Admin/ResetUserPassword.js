/// <reference path="../../../Common/Utility.js" />

$(document).on('change', '#txtSearchUser', function () {
    if($(this).val().length>2)
    {
        utility.ajaxHelper(app.urls.GetUserJson, { 'searchValue': $(this).val() }, function(data) {

        })
    }
});
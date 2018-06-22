/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#moduleid').val(data.ModuleId);
    $('#modulename').val(data.ModuleName);
    $('#moduledesc').val(data.Description === 'No Description' ? '' : data.Description);
    $('.popup').hide();
});

$(document).on('click', '[id*="btnSave"]', function () {
    $('#moduleid').val('0');
    utility.setFormPostUrl('moduleform', 'SetAppModule', 'menu', 'Global');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('moduleform', 'UpdateAppModule', 'menu', 'Global');
        $('#moduleform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to Delete.', function () {
        utility.setFormPostUrl('moduleform', 'DeleteAppModule', 'menu', 'Global');
        $('#moduleform').submit();
    });
    return false;
});
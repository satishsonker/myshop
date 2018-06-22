/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../Common/App.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

$(document).ready(function () {
    utility.bindDdlByAjax(app.urls.GetModuleUrl, "moduleid");
    utility.disableCtrl('parentid');
});

$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#moduleid').val(data.ModuleId);
    $('#pageid').val(data.PageId);
    $('#pagename').val(data.PageName);
    $('#url').val(data.Url);
    
    $('#pagedesc').val(data.Description === 'No Description' ? '' : data.Description);
    $('.popup').hide();
    $('#moduleid').change();
    let x = setInterval(() =>{
        if($('#parentid option').length>1)
        {
            $('#parentid').val(data.ParentId);
            clearInterval(x);
        }
    }, 300)
});

$(document).on('change', '#moduleid', () => {
    let moduleId = parseInt($('#moduleid').find("option:selected").val());
    if(isNaN(moduleId))
    {
        utility.SetAlert("Please select App Module", 'warning');
        utility.disableCtrl('parentid');
    }
    else
    {
        utility.bindDdlByAjaxWithParam(app.urls.GetPageUrl, "parentid", { moduleid: moduleId });
        if ($('#parentid option').length > 1) {
            utility.enableCtrl('parentid');
        }
        else
            utility.disableCtrl('parentid');
        
    }
});

$(document).on('click', '[id*="btnSave"]', function () {
    $('#pageid').val('0');
    utility.setFormPostUrl('pageform', 'SetAppPage', 'menu', 'Global');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('pageform', 'UpdateAppPage', 'menu', 'Global');
        $('#pageform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to Delete.', function () {
        utility.setFormPostUrl('pageform', 'DeleteAppPage', 'menu', 'Global');
        $('#pageform').submit();
    });
    return false;
});
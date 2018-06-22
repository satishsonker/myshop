/// <reference path="../../../Common/Utility.js" />
/// <reference path="../../../Common/App.js" />
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />

function fetchLog(all) {
    window.location = app.urls.GetErrorLog + '?isAllLog=' + all
}

function UpdateLog(ctrl) {
    let errorId = parseInt($(ctrl).data('data'));

    if(!isNaN(errorId))
    {
        utility.ajaxHelper(app.urls.UpdateErrorLog, { ErrorId: errorId }, function (data) {
            if(data===2)
            {
                $(ctrl).css('background-color', '#ffffff').css('background-image', '');
                let x = setInterval(function () {
                    $(ctrl).remove();
                    clearInterval(x);
                }, 1500);  
            }
        });
    }
}
/// <reference path="../../../../jquery-1.10.2.intellisense.js" />
/// <reference path="../../../Common/Validate.js" />
/// <reference path="../../../Common/Utility.js" />

$(document).ready(function() {
    //utility.createDatetimePicker('#DownTimeStart');
    //utility.createDatetimePicker('#DownTimeEnd');

    var startDateTextBox = $('#DownTimeStart');
    var endDateTextBox = $('#DownTimeEnd');

    $.timepicker.datetimeRange(
        startDateTextBox,
        endDateTextBox,
        {
            minInterval: (1000 * 60 * 60), // 1hr
            dateFormat: 'mm/dd/yy',
            timeFormat: 'HH:mm',
            start: {
                minDate: 0
            }, // start picker options
            end: {} // end picker options					
        }
    );
});


$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#message').val(data.Message);
    $('#DownTimeStart').val(utility.getJsDateTimeFromJson(data.DownTimeStartDate));
    $('#DownTimeEnd').val(utility.getJsDateTimeFromJson(data.DownTimeEndDate));
    $('#downtimeid').val(data.Id);
    $('.popup').hide();
});

$(document).on('click', '[id*="btnSave"]', function (e) {
    let pass = $('#password').val();
    let conPass = $('#conpassword').val();
    if (pass != conPass) {
        utility.SetAlert('Password and confirm password should be same', 'warning');
        e.preventDefault();
        return false;
    }
    $('#userid').val('0');
    utility.setFormPostUrl('downtimeform', 'SetDowntime', 'Setting', 'Global');
    $(document).submit();
});

$(document).on('click', '[id*="btnUpdate"]', function (e) {
    let pass = $('#password').val();
    let conPass = $('#conpassword').val();
    if (pass != conPass) {
        utility.SetAlert('Password and confirm password should be same', 'warning');
        e.preventDefault();
        return false;
    }
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('downtimeform', 'UpdateDowntime', 'Setting', 'Global');
        $('#downtimeform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to Delete.', function () {
        utility.setFormPostUrl('downtimeform', 'DeleteDowntime', 'Setting', 'Global');
        $('#downtimeform').submit();
    });
    return false;
});


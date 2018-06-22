/// <reference path="../../jquery-1.10.2.js" />
/// <reference path="../Common/App.js" />
/// <reference path="../Common/Utility.js" />
 

$(document).ready(function () {
    var timer = 60;
    $('#resend').attr('disabled', 'disabled');
    setInterval(function () {
        timer = timer - 1;
        if (timer > 0) {
            if (timer >= 10) {
                $('#resend').attr('value', '00:' + timer);
            }
            else {
                $('#resend').attr('value', '00:0' + timer);
            }
        }
        else {
            $('#resend').attr('value', 'Resend');
            $('#resend').removeAttr('disabled');
        }
    }, 1000);   
});

$(document).on('click','#resend',function myfunction() {
    utility.setFormPostUrl('otpform', 'SendResetLink', 'login');
    utility.setRequired('otp', false);
    $('#otpform').submit();
});

$('#Validate').click(function myfunction() {
    utility.setFormPostUrl('otpform', 'ValidateOtp', 'login');
    utility.setRequired('otp', true);
    $('#otpform').submit();
});
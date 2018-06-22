/// <reference path="../../../../jquery-1.10.2.js" />
/// <reference path="../../../Common/App.js" />
/// <reference path="../../../Common/Utility.js" />

$(document).ready(function () {
    utility.bindDdlByAjax('GetEmpRoleTypeListJson', 'roleid');
    $('#state').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/common/GetStateName",
                dataType: "json",
                data: {
                    stateName: $('#state').val()
                },
                success: function (data) {
                    response(data);
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            log("Selected: " + ui.item.value + " aka " + ui.item.id);
        }
    });
    $('#city').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/common/GetCityName",
                dataType: "json",
                data: {
                    CityName: $('#city').val()
                },
                success: function (data) {
                    response(data);
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            log("Selected: " + ui.item.value + " aka " + ui.item.id);
        }
    });

    $("#imageUploadForm").change(function () {
        var formData = new FormData();
        var totalFiles = document.getElementById("imageUploadForm").files.length;
        for (var i = 0; i < totalFiles; i++) {
            var file = document.getElementById("imageUploadForm").files[i];
            formData.append("imageUploadForm", file);
        }
        $.ajax({
            type: "POST",
            url: '/common/FileSaveOnServer',
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (response) {
                $('#EmpImage').attr('src', 'data:image/png;base64,' + response.Image);
            },
            error: function (error) {
                alert("errror");
            }
        }).done(function () {
            alert('success');
        }).fail(function (xhr, status, errorThrown) {
            alert('fail');
        });
    });
    $("#tabs").tabs();
    
});
$(document).on('click', '[id*="btnSelectRow_"]', function () {
    utility.bottonGroupManager(true);
    var data = $(this).data('data');
    $('#empid').val(data.EmpId);
    $('#roleid').val(data.RoleType);
    $('#aadharNo').val(data.AadharNo);
    $('#address').val(data.Address);
    $('#city').val(data.City);
    $('#dob').val(utility.getJsDateTimeFromJson(data.DOB));
    $('#doj').val(data.DOJ);
    $('#DOR').val(data.DOR);
    $('#district').val(data.Distict);
    $('#email').val(data.EmailId);
    $('#fathername').val(data.FatherName);
    $('#firstname').val(data.FirstName);
    $('#lastname').val(data.LastName);
    $('#mobile').val(data.Mobile);
    $('#pancard').val(data.PANCardNo);
    $('#state').val(data.State);
    $('#pincode').val(data.PINCode);
    $('.popup').hide();
});

$(document).on('click', '[id*="btnSave"]', function () {
    var empId = $('#empid').val();
    if (empId == '0') {
        $('#empid').val('0');
        $('#aadharNo').val($('#aadharNo').val().replace(/\//g, ''));
        utility.setFormPostUrl('empform', 'SetEmployee', 'master', 'EmployeesManagement');
        $(document).submit();
    }
    else {
        utility.SetAlert('You have already saved this details.', app.const.alertType.danger);
    }
});

$(document).on('keypress paste', 'input[id="aadharNo"]', function (e) {
    var len = $(this).val().replace(/\//g, '').length;
    var $text = '';
    var $finalText = '';
    if (e.originalEvent.clipboardData !== undefined) {
        $text = e.originalEvent.clipboardData.getData('text');
        for (var i = 0; i < $text.length; i++) {
            if (i % 4 == 0 && i > 0)
                $finalText = $finalText + '/' + $text.charAt(i);
            else
                $finalText = $finalText + $text.charAt(i);
        }
        $(this).val($finalText);
    }
    else {
        if (len == 4 || len == 8)
            $(this).val($(this).val() + '/');
    }

    $(this).val($(this).val().substr(0, 15))
});

$('input[id="aadharNo"]').bind("paste", function (e) {
    // access the clipboard using the api
    var pastedData = e.originalEvent.clipboardData.getData('text');
    alert(pastedData);
});

$(document).on('click', '[id*="btnUpdate"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('empform', 'UpdateRoleType', 'master', 'EmployeesManagement');
        $('#empform').submit();
    });
    return false;
});

$(document).on('click', '[id*="btnDelete"]', function () {
    utility.confirmBox('Are you sure! want to Update.', function () {
        utility.setFormPostUrl('empform', 'DeleteRoleType', 'master', 'EmployeesManagement');
        $('#empform').submit();
    });
    return false;
});
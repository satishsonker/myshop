var $layout = {};

$layout.getPushNotification = () => {
    return new Promise(function (resolve, reject) {
        utility.ajaxHelperGet(app.urls.CommonController.GetPushNotification, function (res) {
            resolve(res);
        }, function (x) {
            reject(x);
        });
    });
};

$(document).ready(function () {
    $('#myModal').modal();
    let _screenTime = null;
    $(document).on('click', '#_layoutUnlockScreen', function () {
        let $password = $('#_layoutUnlockPassword');
        utility.ajaxHelper(app.urls.CommonController.CheckPassword, { password: $password.val() }, function (data) {
            if (data) {
                clearInterval(_screenTime);
                $('.lock').hide(500);
                $password.val('');
            }
            else {
                $('#_layoutScreenLockMsg').text('Password is invalid');
                _screenTime = setTimeout(function () {
                    $('#_layoutScreenLockMsg').text('Your Screen is locked');
                }, 2000);
            }
        });
    });

    $(document).on('click', '#_layoutLockScreen', function () {
        _screenTime = setInterval(function () {
            $('.lock').show();
        }, 100);
        //$('.lock').show(100);
        return false;
    });
    let interval = setInterval(function () {
        let height = $('body').css('height');
        if (height == 'auto !important') {
            clearInterval(interval);
        }
        else {
            //$('body').css({ 'height': '1000px !important' });
        }
    }, 100)

    //Assign active arrow to the current menu
    $('aside li').removeClass('active');
    $('aside a').each(function (ind, ele) {
        if ($(ele).attr('href').toLowerCase() == location.pathname.toLowerCase()) {

            if ($(ele).parent().parent().hasClass('hoe-sub-menu')) {
                $(ele).parent().parent().parent().find('a:first-child').click();
            }
            $('aside li').removeClass('active');
            $(ele).parent().addClass('active');
        }
    });

    setInterval(() => {
        $layout.getPushNotification().then(function (resp) {
            $('#lblNotiCount').text(resp.length);
            $('.lblNotiCount').text(resp.length + ' Pending');
            let $html = "";
            let $notiHtml ='';
            $(resp).each(function (ind, ele) {
                if ($('li[data-notifyid="' + ele.NotificationId + '"]').length == 0) {
                    $html += '<li data-notifyid="' + ele.NotificationId + '">' +
                        '<a href = "#" class="clearfix">' +
                        '<i class="fa fa-database blue-text"></i>' +
                        '<span class="notification-title">New Notification</span>' +
                        '<span class="notification-ago  ">3 min ago</span>' +
                        '<p class="notification-message">' + ele.Message + '</p>' +
                        '</a>' +
                        '</li>';
                var $guid = utility.getGUID();
                $notiHtml += '<div id="content" class="notiItem" data-id="' + $guid + '">' +
                    '<div id="left">' +
                    '<i class="fa fa-bell blue-text"></i>' +
                    '</div>' +
                    '<div id="right"> ' +
                    '<span style="font-size:12px;">New Notification</span>' +
                    '<hr style="clear:both"> ' +
                    '<span style="font-size:11px;">' + ele.Message + '</span>' +
                    '</div> ' +
                        '</div>';
              var $rm=  setInterval(() => {
                  $('div[data-id="' + $guid + '"]').remove();
                  if ($('.notiItem').length == 0) {
                      $('.notistack').hide();
                  }
                  clearInterval($rm);
                    }, 10000);
                }
            });

            $('.notiList').append($html);
            if ($notiHtml !== '') {
                $('.notistack').empty().append($notiHtml);
                $('.notistack').show();
            }
        });
    }, 60000);
});


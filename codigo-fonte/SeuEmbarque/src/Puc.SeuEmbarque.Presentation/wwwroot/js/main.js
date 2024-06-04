(function ($) {

    "use strict";

    var fullHeight = function () {

        $('.js-fullheight').css('height', $(window).height());
        $(window).resize(function () {
            $('.js-fullheight').css('height', $(window).height());
        });

    };
    fullHeight();

    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });

})(jQuery);


function CloseModal() {
    $('#registry-mainregister-modal').modal('hide');   
    setTimeout(function () {
        location.reload();
    }, 600);   
}

function CloseModalPopup() {
    $('#mod-registro-popup').modal('hide');
}


function ShowModalLoading() {
    $('.modalLoading').modal('show');

}

function HideModalLoading() {
    setTimeout(function () {
        $('.modalLoading').modal('hide');
    }, 800);

}

function UpdatePage() {
    console.log('Updated');
    ShowModalLoading();
    parent.location.reload();
}

function UpdateMainContent() {
    console.log('Updated');
    ShowModalLoading();
    location.reload();
}

function OpenDynamicModal(modalURL, modalData, modalId, timeout) {

    /*Optional Paramters*/
    if (timeout === undefined) {
        timeout = 0;
    }

    ShowModalLoading();
    $(modalId).modal('hide');
    setTimeout(function () {
        $.ajax({
            method: 'post',
            cache: false,
            url: modalURL,
            data: modalData,
            success: function (modalContent) {
                HideModalLoading();
                $(modalId).attr('data-backdrop', 'static');
                $(modalId).find('#modal-content').html(modalContent);
                if (modalContent.indexOf('<div') >= 0)
                    $(modalId).modal('show');
                else
                    $(modalId).find('#modal-content').html('');
            },
            error: function () {
                HideModalLoading();
            }
        }).done(function () {
            HideModalLoading();
        });
        HideModalLoading();
    }, timeout);
}

function isMobile() {
    return (typeof window.orientation !== "undefined") || (navigator.userAgent.indexOf('IEMobile') !== -1);
}

function Autenticar() {
    var senhaHash = CryptoJS.SHA256($('#senha').val()).toString(CryptoJS.enc.Hex);
    var params = {};
    params.Email = $('#email').val();
    params.Senha = senhaHash;
    params.ManterLogado = $('#manterLogado').is(':checked');

    $.ajax({
        url: $('#loginForm').attr('action'),
        method: 'POST',
        data: params,
        success: function (response) {
            if (response.success) {
                // Redirecionar ou executar outras ações necessárias
                window.location.href = response.redirectUrl;
            } else {
                ExibirErro(true, response.message, 'error');
            }
        },
        error: function (xhr, status, error) {
            // Manipular erro de requisição, se necessário
            console.error(error);
        }
    });
}
//USER AVATAR----------------------------------
function getUserInitials(name) {
    const names = name.split(' ');
    let initials = names[0][0];
    if (names.length > 1) {
        initials += names[names.length - 1][0].toUpperCase();
    }
    return initials;
}

function createInitialsImage(initials, bgColor = '#195770', textColor = '#FFF') {
    const canvas = document.getElementById('initialsCanvas');
    const context = canvas.getContext('2d');

    // Background
    context.fillStyle = bgColor;
    context.fillRect(0, 0, canvas.width, canvas.height);

    // Text
    context.fillStyle = textColor;
    context.font = 'bold 25px Arial';
    context.textAlign = 'center';
    context.textBaseline = 'middle';
    context.fillText(initials, canvas.width / 2, canvas.height / 1.9);

    // Convert to image
    return canvas.toDataURL('image/png');

}

function getRandomColor() {
    const letters = '0123456789ABCDEF';
    let color = '#';
    for (let i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}
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

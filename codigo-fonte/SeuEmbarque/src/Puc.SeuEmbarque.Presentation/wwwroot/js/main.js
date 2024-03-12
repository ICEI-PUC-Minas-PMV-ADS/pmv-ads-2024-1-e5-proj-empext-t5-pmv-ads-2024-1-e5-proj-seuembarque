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

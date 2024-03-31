// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function ValidarEmail(seletorInput, seletorErro, mensagem) {

    $(seletorInput).on("input", function () {
        var email = $(this).val();
        var regex = /^[^\s@]+@[^\s@]+\.[^\s@]{3,}$/
        if (regex.test(email)) {
            $(seletorErro).text("");
        } else {
            $(seletorErro).text(mensagem);
            $(seletorInput).addClass("error-shadow");
        }
    });
}


function ExibirErro(acionar, text, campo) {
    const toastTrigger = acionar;
    const toastLive = $('#liveToast');

    let texto = text;
    let titulo = campo;

    if (toastTrigger) {
        const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastLive)
        $('.toast-body').text(texto);
        //$('.titulo').text(titulo);
        toastBootstrap.show()
    }
}




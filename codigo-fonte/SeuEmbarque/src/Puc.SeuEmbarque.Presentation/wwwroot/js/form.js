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


function limparData(clear) {
    if (clear == 1) {
        $("#dataIda").val("");
        $("#dataIda").trigger("change");
    }
    else {
        $("#dataVolta").val("");
        $("#dataVolta").trigger("change");
        MudarIcone(1, 2);
    }
}

function HabilitarHospedagem() {
    var dataVoltaVal = $('#dataVolta').val();
    var hospedagem = $('.chk-section');

    if (dataVoltaVal != "") {
        hospedagem.fadeIn();
    } else {
        hospedagem.fadeOut();
    }

    ConfigurarHospedagem(dataVoltaVal);

}

function ConfigurarDatepicker() {
    $("#dataIda").datepicker({
        dateFormat: 'dd/mm/yy',
        showAnim: "slideDown",
        minDate: 0,
        onSelect: function (selectedDate) {
            var selectedDateObj = $(this).datepicker('getDate');
            $("#dataVolta").datepicker("option", "minDate", selectedDateObj);
            $("#dataVolta").prop('disabled', false);
            MudarIcone(1);
        }
    });

    $("#dataIda").on('change', function () {
        if ($(this).val() == "") {
            $("#dataVolta").val("");
            HabilitarHospedagem();
            $("#dataVolta").prop('disabled', true);
            MudarIcone(2, 2);
        }
    });

    $("#dataVolta").datepicker({
        dateFormat: 'dd/mm/yy',
        showAnim: "slideDown",
        minDate: 0
    });

}

function MudarIcone(changeIda = 0, changeVolta = 0) {
    if (changeIda != 0) {
        if (changeIda == 1) {
            $('.calendarIda').hide();
            $('.clearIda').fadeIn();
        } else {
            $('.calendarIda').fadeIn();
            $('.clearIda').hide();
        }
    }
    if (changeVolta != 0) {
        if (changeVolta == 1) {
            $('.calendarVolta').hide();
            $('.clearVolta').fadeIn();
        } else {
            $('.calendarVolta').fadeIn();
            $('.clearVolta').hide();
        }
    }

}

function MontaTextoInputViajantes(toggle) {
    var classeVal = $('#classe').val();
    var adultosVal = parseInt($('#qtdAdultos').val());
    var criancasVal = parseInt($('#qtdCriancas').val());
    var somaPessoas = (adultosVal + criancasVal);
    var txtPessoa = somaPessoas > 1 ? "Pessoas" : "Pessoa";

    var textInput = `${somaPessoas} ${txtPessoa}, ${classeVal}`;

    if (toggle)
        $('#dropdownMenuButton').click();

    $('#textInputViajantes').text(textInput);
}

function AdicionarViajante(addViajante = 0) {
    var totalAdulto = parseInt(qtdAdulto.val());
    var totalCrianca = parseInt(qtdCrianca.val());

    if (addViajante == 1) {
        qtdAdulto.val(totalAdulto + 1);
    } else {
        qtdCrianca.val(totalCrianca + 1);
    }

    DesabilitarBotaoRemove();
}

function RemoverViajante(removerViajante = 0) {
    var totalAdulto = parseInt(qtdAdulto.val());
    var totalCrianca = parseInt(qtdCrianca.val());

    if (removerViajante == 1) {
        qtdAdulto.val(totalAdulto > 1 ? totalAdulto - 1 : totalAdulto = 1);
    } else {
        qtdCrianca.val(totalCrianca > 0 ? totalCrianca - 1 : totalCrianca = 0);
    }

    DesabilitarBotaoRemove();
}

function DesabilitarBotaoRemove() {
    if (qtdAdulto.val() == "1")
        $('#subAdulto').addClass('disabled');
    else
        $('#subAdulto').removeClass('disabled');

    if (qtdCrianca.val() == "0")
        $('#subCrianca').addClass('disabled');
    else
        $('#subCrianca').removeClass('disabled');
}

function ComboClasse() {
    var classeOptions = [
        "Econômica",
        "Executiva",
        "Primeira Classe"
    ];
    $("#classe").autocomplete({
        source: classeOptions,
        minLength: 0,
        appendTo: "#autocomplete-container"
    });
    $("#classe").val("Econômica"); //default
    $('#classe').blur(function () {
        if ($(this).val() == "") {
            $(this).val("Econômica");
        }
    })
    $("#classe").on("click", function () {
        if ($(this).val() != "") {
            $(this).val("")
        }
        $(this).autocomplete("search");
    });
    $(".ui-autocomplete").css("z-index", 1020);
}

function ConfigurarAutocomplete(campo) {
    $(campo).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '@Url.Content("/Aeroporto/PesquisarAeroporto")',
                method: 'GET',
                dataType: 'json',
                data: {
                    termo: request.term
                },
                success: function (result) {
                    var mappedData = $.map(result.data, function (item) {
                        return {
                            label: item.name,
                            value: item.aeroportoId
                        };
                    });
                    response(mappedData);
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            $(campo).val(ui.item.label);
            return false;
        },
        open: function (event, ui) {
            var $input = $(this);
            var $menu = $input.autocomplete("widget");
            var $scrollParent = $menu.css('overflow', 'auto');
            $scrollParent.css({
                'max-height': '330px',
                'overflow-y': 'auto'
            });
        }
    });
}

function MontarMensagem() {
    var nome = $('#nomeCliente').val();
    var email = $('#emailCliente').val();
    var origem = $('#origem').val();
    var destino = $('#destino').val();
    var dataIda = $('#dataIda').val();
    var dataVolta = $('#dataVolta').val();
    var opcionais = $('#options').val();
    var classe = $('#classe').val();
    var adultos = parseInt($('#qtdAdultos').val());
    var criancas = parseInt($('#qtdCriancas').val());

    var textoLink = "Olá%20Seu%20Embarque!%20Segue%20informações%20da%20viagem%20que%20desejo%20para%20cotação:%0A%0A";

    textoLink += "*Nome:* " + nome + "%0A";
    textoLink += "*Email:* " + email + "%0A";
    textoLink += "*Origem:* " + origem + "%0A";
    textoLink += "*Destino:* " + destino + "%0A";
    textoLink += "*Data de Ida:* " + dataIda + "%0A";
    if (dataVolta != "")
        textoLink += "*Data de Volta:* " + dataVolta + "%0A";

    textoLink += "*Adultos:* " + adultos + "%0A";

    if (criancas > 0)
        textoLink += "*Crianças:* " + criancas + "%0A";

    textoLink += "*Classe:* " + classe + "%0A";

    if (comHospedagem)
        opcionais != "" ? textoLink += "*Hospedagem com:* " + opcionais.replace(/\s*\+\s*/g, ', ') : textoLink += "*Hospedagem:* " + "Sim";



    return textoLink;
}

function ConfigurarHospedagem(hasValue) {

    const options = $('.option');
    const chkSim = $('#chkSim');
    const chkNao = $('#chkNao');
    let chks = $('#chkSim, #chkNao');

    chks.addClass('disabled');

    //var hasValue = valueData; datavolta on change

    if (hasValue != "") {
        $(chks).prop('disabled', false);
        chks.removeClass('disabled');
    }
    else {
        chkSim.prop('checked', false);
        if (!chkNao.is(":checked")) {
            chkNao.click();
        }
        chks.prop('disabled', true);
        chks.addClass('disabled');
    }

    chkSim.change(function () {
        if ($(this).prop('checked')) {
            options.removeClass('disabled');
            chkNao.prop('checked', false);
            comHospedagem = true;
        } else {
            options.addClass('disabled').removeClass('option-selected');
        }
    });

    chkNao.change(function () {
        if ($(this).prop('checked')) {
            options.addClass('disabled').removeClass('option-selected');
            chkSim.prop('checked', false);
            comHospedagem = false;
            $('#options').val('');
        } else {
            options.removeClass('disabled');
        }
    });

    options.click(function () {
        if (!$(this).hasClass('disabled')) {
            var isSelected = $(this).hasClass('option-selected');

            // Remove a seleção de todos os elementos
            options.removeClass('option-selected');

            // Limpa o valor do campo
            $('#options').val('');

            // Se o elemento n estiver selecionado, adiciona a seleçao
            if (!isSelected) {
                $(this).addClass('option-selected');
                var value = $(this).text().trim();
                $('#options').val(value);
            }

            var option = $('#options').val();
            console.log("Selected option:", option.replace(/\s*\+\s*/g, ', '));
        }
    });
}

function ValidarForm() {
    var nome = $('#nomeCliente').val();
    var email = $('#emailCliente').val();
    var origem = $('#origem').val();
    var destino = $('#destino').val();
    var dataIda = $('#dataIda').val();
    //var dataVolta = $('#dataVolta').val();

    if (nome == '') {
        ExibirErro(true, "Por Favor preencha o Nome!", "Nome Completo");
        setTimeout(function () { $('#nomeCliente').focus(); }, 800);
        return false;
    }
    if (email == '') {
        ExibirErro(true, "Por Favor preencha o Email!", "Email");
        setTimeout(function () { $('#emailCliente').focus(); }, 800);
        return false;
    }
    if (origem == '') {
        ExibirErro(true, "Por Favor preencha a Origem!", "Origem");
        setTimeout(function () { $('#origem').focus(); }, 800);
        return false;
    }
    if (destino == '') {
        ExibirErro(true, "Por Favor preencha o Destino!", "Destino");
        setTimeout(function () { $('#destino').focus(); }, 800);
        return false;
    }
    if (dataIda == '') {
        ExibirErro(true, "Não é possivel realizar a cotação sem a data de Ida!", "Data Ida");
        setTimeout(function () { $('#dataIda').focus(); }, 800);
        return false;
    }
    //if (dataVolta == '') {
    //    $('#dataVolta_error').text('Por Favor preencha a Data!');
    //    setTimeout(function () { $('#dataVolta').focus(); }, 800);
    //    return false;
    //}

    return true;
}

function AdicionarPacote(clienteId) {
    var params = LoadParametrosPacote();
    params.pacote.client_id = clienteId;

    $.ajax({
        url: '@Url.Content("/Pacotes/InserirPacote")',
        method: 'POST',
        dataType: 'json',
        data: params.pacote,
        success: function (result) {

        }
    });
}

function AdicionarCliente() {
    var params = LoadParametrosPacote();

    $.ajax({
        url: '@Url.Content("/Clientes/InserirCliente")',
        method: 'POST',
        dataType: 'json',
        data: params.cliente,
        success: function (result) {
            if (result) {
                var idCliente = result.content.client_id;
                if (result.content.client_id > 0) {
                    AdicionarPacote(result.content.client_id);
                }
            }
        }
    });
}

function FormatarHospedagem(opcional) {
    var opcionais = opcional;
    switch (opcional) {
        case "All inclusive":
            opcionais = "ALL"
            break;
        case "Café da Manhã":
            opcionais = "C"
            break;
        case "Café da Manhã + Almoço":
            opcionais = "CA"
            break;
        case "Café da Manhã + Almoço + Jantar":
            opcionais = "CAJ"
            break;
        default:
            opcionais = "";
    }

    return opcionais;
}

function FormatarClasse(classe) {
    var nome = "";
    switch (classe) {
        case "Econômica":
            nome = "economica"
            break;
        case "Executiva":
            nome = "executiva"
            break;
        case "Primeira Classe":
            nome = "primeira_classe"
            break;
        default:
            nome = classe;
    }
    return nome;
}

function LoadParametrosPacote() {
    var nome = $('#nomeCliente').val();
    var email = $('#emailCliente').val();

    var origem = $('#origem').val();
    var destino = $('#destino').val();
    var dataIda = $('#dataIda').val();
    var dataVolta = $('#dataVolta').val();
    var opcional = $('#options').val();
    var classe = FormatarClasse($('#classe').val());
    var adultos = parseInt($('#qtdAdultos').val());
    var criancas = parseInt($('#qtdCriancas').val());

    var opcionais = FormatarHospedagem(opcional);

    var params = {
        pacote: {
            client_id: 0,
            origin: origem,
            destination: destino,
            departure_date: dataIda,
            return_date: dataVolta,
            meals: opcionais,
            accommodation: comHospedagem,
            kids: criancas,
            adults: adultos,
            travel_class: classe
        },

        cliente: {
            name: nome,
            email: email
        }
    };

    return params;
}

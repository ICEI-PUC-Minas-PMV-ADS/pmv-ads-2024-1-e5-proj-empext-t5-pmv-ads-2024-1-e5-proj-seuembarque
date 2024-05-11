function MontaTextoInputViajantes(toggle) {
    var classeVal = $('#classe').val();
    var adultosVal = parseInt($('#qtdAdultos').val());
    var criancasVal = parseInt($('#qtdCriancas').val());
    var somaPessoas = (adultosVal + criancasVal);
    //var txtPessoa = somaPessoas > 1 ? "Pessoas" : "Pessoa";
    var txtAdulto = somaPessoas > 2 ? "Adultos" : "Adulto";
    var txtCrianca = somaPessoas > 2 ? "Crianças" : "Criança";

    var textInput = `${adultosVal} ${txtAdulto},${criancasVal} ${txtCrianca}`;

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
    $(".ui-autocomplete").css("z-index", 1030);
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


function ConfigurarDatepicker() {
    $("#frmRegPacote").find("#dataIda").datepicker({
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

    $("#frmRegPacote").find("#dataIda").on('change', function () {
        if ($(this).val() == "") {
            $("#dataVolta").val("");
            HabilitarHospedagem();
            $("#dataVolta").prop('disabled', true);
            MudarIcone(2, 2);
        }
    });

    $('#frmRegPacote').find("#dataVolta").datepicker({
        dateFormat: 'dd/mm/yy',
        showAnim: "slideDown",
        minDate: 0
    });

}


function HabilitarHospedagem() {
    var dataVoltaVal = $('#dataVolta').val();
    var hospedagem = $('#txtHospedagem');

    if (dataVoltaVal != "") {
        hospedagem.prop("disabled", false);
        hospedagem.trigger('change');
    } else {
        hospedagem.val(0);
        hospedagem.trigger('change');
        hospedagem.prop("disabled", true);
    }

    // ConfigurarHospedagem(dataVoltaVal);

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
function FormatarTelefone(input) {
    $(input).on('input', function () {
        var telefone = $(this).val();
        telefone = telefone.replace(/\D/g, ''); // Remove todos os caracteres que não são números
        telefone = telefone.replace(/^(\d{2})(\d)/g, '($1) $2'); // Adiciona o formato (00) no início
        telefone = telefone.replace(/(\d{5})(\d)/, '$1-$2'); // Adiciona o formato 00000-0000
        $(this).val(telefone);
    });
}
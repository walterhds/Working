function Cadastrar(date, hour, description) {
    $.ajax({
        url: '/Compromisso/CadastrarAjax/',
        data: {
            data: date,
            hora: hour,
            descricao: description
        },
        type: 'post',
        success: function (response, status, xhr) {
            alert('Cadastro realizado!');
            location.reload();
        },
        error: function (xhr, status, response) { alert('Erro no processamento: ' + response); }
    });
    document.getElementById("Cancelar").click();
}

$("#Cancelar").click(function () {
    $("#Data").val("");
    $("#Hora").val("");
    $("#Descricao").val("");
});

$("#Salvar").click(function () {
    var data = $("#Data").val();
    var hora = $("#Hora").val();
    var descricao = $("#Descricao").val();

    Cadastrar(data, hora, descricao);
});

function PopularModal(id) {
    $.ajax({
        url: '/Compromisso/PopularModal/',
        data: {
            id: id
        },
        type: 'post',
        success: function (lista) {
            $("#DataAlterar").val(lista.data);
            $("#HoraAlterar").val(lista.hora);
            $("#DescricaoAlterar").val(lista.conteudo);
            $("#id").val(id);
        },
        error: function (xhr, status, reponse) { alert("BUGO"); }
    });
}

function Alterar(id, date, hour, description) {
    $.ajax({
        url: '/Compromisso/AlterarAjax/',
        data: {
            id: id,
            data: date,
            hora: hour,
            descricao: description
        },
        type: 'post',
        success: function (response, status, xhr) {
            alert('Alteração realizada!');
            location.reload();
        },
        error: function (xhr, status, response) { alert('Erro no processamento: ' + response); }
    });
    document.getElementById("CancelarAlterar").click();
}

$("#CancelarAlterar").click(function () {
    $("#DataAlterar").val("");
    $("#HoraAlterar").val("");
    $("#DescricaoAlterar").val("");
});

function DeletaCompromisso(id) {
    $.ajax({
        url: '/Compromisso/DeletaCompromisso/',
        data: {
            id: id
        },
        type: 'post',
        success: function (response, status, xhr) {
            location.reload();
        },
        error: function (xhr, status, response) { alert('Erro no processamento: ' + response); }
    });
}

function ConfirmarCompromisso(id) {
    $.ajax({
        url: '/Compromisso/ConfirmarCompromisso/',
        data: {
            id: id
        },
        type: 'post',
        success: function () { location.reload(); },
        error: function (xhr, status, response) { alert('Erro no processamento: ' + response) }
    });
}
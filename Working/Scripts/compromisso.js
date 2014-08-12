function DeletarTabelaCompromisso() {
    var numeroLinhas = $("#Compromissos >tbody >tr").length;
    for (var i = 0; i < numeroLinhas; i++) {
        document.getElementById("Compromissos").deleteRow(0);
    }
}

function PreencherTabelaCompromisso() {
    var tabela = document.getElementById("Compromissos");
    var cont = 0;
    $.get("/Compromisso/ListarCompromissosJson/", function (data) {
        $.each(data, function (i, res) {
            if (res != null) {
                var linhaCabecalho = tabela.insertRow(cont);
                var dataCompromisso = document.createElement('th');
                dataCompromisso.innerHTML = "<th>" +
                    "<a onclick='PopularModal(" + res.id + ");' id='" + res.id + "' title='Alterar' data-toggle='modal' data-target='#AlterarCompromisso' style='font-size: 11px; padding-top: 5px; width: 15px;'></a>" +
                    res.Data +
                    "<span class='alteracao'>Última alteração feita por: " + res.Alterado + "</span>" +
                    "<button type='button' class='close' onclick='ConfirmarCompromisso(" + res.id + ")' aria-hidden='true'><span class='glyphicon glyphicon-ok' style='color: green; font-size: 15px; padding-top: 5px; width: 15px;'></span></button>" +
                    "</th>";

                var linhaDescricao = tabela.insertRow(++cont);
                var descricao = document.createElement('td');

                descricao.innerHTML = "<td>" +
                    "<small class='glyphicon glyphicon-time pull-right'>" +
                    "<span style='font-family: helvetica'>" +
                    "<strong> " + res.Hora + "</strong>" +
                    "</span></small>" +
                    res.Descricao +
                    "</td>";

                linhaCabecalho.appendChild(dataCompromisso);
                linhaDescricao.appendChild(descricao);
                $(dataCompromisso).addClass("glyphicon-compromisso");
                $("#" + res.id).addClass('glyphicon glyphicon-pencil pull-left');
                cont++;
            }
        });
        tabela.css('refresh');
    });
}

function Cadastrar(date, hour, description, job) {
    $.ajax({
        url: '/Compromisso/CadastrarAjax/',
        data: {
            data: date,
            hora: hour,
            descricao: description,
            idJob: job
        },
        type: 'post',
        success: function () {
            DeletarTabelaCompromisso();
            PreencherTabelaCompromisso();
            document.getElementById("Cancelar").click();
        },
        error: function (xhr, status, response) { alert('Erro no processamento: ' + response); }
    });
}

function CadastrarAjax(date, hour, description) {
    $.ajax({
        url: '/Compromisso/CadastrarAjax/',
        data: {
            data: date,
            hora: hour,
            descricao: description
        },
        type: 'post',
        success: function () {
            DeletarTabelaCompromisso();
            PreencherTabelaCompromisso();
            document.getElementById("Cancelar").click();
            if (location.href.indexOf("/Compromisso/Index") || location.href.indexOf("/Compromisso/Index#")) {
                location.reload();
            }
        },
        error: function (xhr, status, response) { alert('Erro no processamento: ' + response); }
    });
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

    Cadastrar(data, hora, descricao,null);
});

$("#SalvarAjax").click(function () {
    var data = $("#Data").val();
    var hora = $("#Hora").val();
    var descricao = $("#Descricao").val();
    
    CadastrarAjax(data, hora, descricao);
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
            $("#DescricaoAlterarCompromisso").val(lista.conteudo);
            $("#id").val(id);
        },
        error: function () { alert("BUGO"); }
    });
}

function Alterar(id, date, hour, description) {
    $.ajax({
        url: '/Compromisso/AlterarAjax/',
        data: {
            id: id,
            data: date,
            hora: hour,
            descricao: description,
            idJob: null
        },
        type: 'post',
        success: function () {
            location.reload();
        },
        error: function (xhr, status, response) { alert('Erro no processamento: ' + response); }
    });
    document.getElementById("CancelarAlterar").click();
}

function AlterarAjax(id, date, hour, description, idJob) {
    $.ajax({
        url: '/Compromisso/AlterarAjax/',
        data: {
            id: id,
            data: date,
            hora: hour,
            descricao: description,
            idJob: idJob
        },
        type: 'post',
        success: function () {
            DeletarTabelaCompromisso();
            PreencherTabelaCompromisso();
            document.getElementById("Cancelar").click();
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
        success: function () {
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
        success: function () {
            location.reload();
        },
        error: function (xhr, status, response) { alert('Erro no processamento: ' + response); }
    });
}

function ConfirmarCompromissoAjax(id) {
    $.ajax({
        url: '/Compromisso/ConfirmarCompromisso/',
        data: {
            id: id
        },
        type: 'post',
        success: function () {
            DeletarTabelaCompromisso();
            PreencherTabelaCompromisso();
            document.getElementById("Cancelar").click();
        },
        error: function (xhr, status, response) { alert('Erro no processamento: ' + response); }
    });
}
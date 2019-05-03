// Write your JavaScript code.
var listaArtigos = {};
var listaFuncionarios = {};
var listaProjeto = {};
var listaNegocio = {};
var tipoArtigo = 3;

var tempFuncionario = {};

$(document).ready(function () {

    $(".linhasDoc").hide();
    var tipoAccao = "";

    $('#ListBoxfuncionario').on('select2:select', function (e) {

        var data = e.params.data;
        if(data.id == "") {
            $(".linhasDoc").hide();
        } else {
            $(".linhasDoc").show();
        }

        daFuncionario(data.id);
        
    });
    

    GetListaArtigos(tipoArtigo);
    GetListaArtigosModal(tipoArtigo);

    GetListaFuncionarios();

    GetListaProjeto();

    GetListaNegocio();
    
    $('.btremover').html("<span class='glyphicon glyphicon-remove-circle'></span>");
    $('.btadicionar').html("<span class='glyphicon glyphicon-ok-circle'></span>");

});

function GetListaArtigos(tipo) {
    $.ajax({
        url: "/Iventario/ListaArtigos?tipo=" + tipo,
        success: function (data) {
            listaArtigos = data;
        }
    });
}

function getFuncionarioCodigoBarras() {

    $.ajax({
        type: "POST",
        url: "Saida.aspx/getFuncionarioCodigoBarras",
        data: '{CodigoBarras: "' + $("#MainContent_txtInput").val() + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(data.d)
            $('#MainContent_ListBoxfuncionario').val(data.d)
            $("#MainContent_ListBoxfuncionario").selectpicker("refresh");

            $("#MainContent_txtInput").val("")
        },
        failure: function (response) {
            alert(response.d);
        }
    });

}

function AddNewFerramentaBt_Click() {

    var linha = "<tr style = 'padding: 0px;'>";

    linha += "<td width='50px' style = 'padding: 0px;'> " +
        "<select id = 'ListBoxfuncionario' class='selectpicker ferramentas form-control' data-live-search='true'>";

    $.each(listaArtigos, function (key, value) {
        linha += "<option value = '" + value.artigo + "' data-tokens='" + value.artigo + "'>" + value.artigo + "</option>";
    });
    linha += "</select > </td > "; // Ferramenta

    linha += "<td width='150px' style = 'padding: 0px;'> " +
        "<select id = 'ListBoxfuncionario' class='selectpicker descricao form-control' data-live-search='true' >";
    $.each(listaArtigos, function (key, value) {
        linha += "<option value = '" + value.artigo + "' data-tokens='" + value.descricao + "'>" + value.descricao + "</option>";
    });
    linha += "</select > </td > "; // Descrição

    //linha += "<td width='75px' style = 'padding: 0px;'> <input class='w-150' type= 'text'  /> </td>"; // Codigo Barras
    linha += "<td width='30px' style = 'padding: 0px;'> <input class='w-100' type= 'number' value = '1'  /> </td>"; //Quantidade
   

    linha += "<td width='100px' style = 'padding: 0px;'> " +
        "<select id = 'ListAreaNegocio' class='selectpicker form-control' data-live-search='true' >";
    $.each(listaNegocio, function (key, value) {
        linha += "<option value = '" + value.artigo + "' data-tokens='" + value.descricao + "'>" + value.descricao + "</option>";
    });
    linha += "</select > </td > "; // Area de Negocio

    linha += "<td width='100px' style = 'padding: 0px;'> " +
        "<select id = 'ListBoxprojecto' class='selectpicker form-control' data-live-search='true' >";
    $.each(listaProjeto, function (key, value) {
        linha += "<option value = '" + value.codigo + "' data-tokens='" + value.descricao + "'>" + value.descricao + "</option>";
    });
    linha += "</select > </td > "; // Projecto

    //<th>Area de Negocio</th>
    //    <th>Projecto</th>
    linha += "<td style = 'padding: 0px;'> <textarea class='form-control' rows='w' ></textarea> </td>"; // Notas

    linha += "<td style = 'padding: 0px;'> " +
        "<button class='btn btn-primary btn-sm'  onclick = 'addRow($(this))' >" +
        "<span class='fa fa-check-square'></span>" +
        "</button >" +
        "<button class='btn btn-danger btn-sm'  onclick = 'removeRow($(this))' >" +
        "<span class='fa fa-remove'></span>" +
        "</button >" +

        "</td > ";   //<th></th>
    linha += '</tr>';

    $('#tableFerramentas').append(linha);
    $(".selectpicker").select2();

    $('.ferramentas').on('change', function (e) {
        selecionaDescricaoLinha(this);
    });

    $('.descricao').on('change', function (e) {
        selecionaFerramentaLinha(this);
    });
}

function removeRow(row) {
    row.closest('tr').remove();
}

function selecionaDescricaoLinha(row) {
    var tr = row.closest('tr');

    var linha = {
        "artigo": $(tr).find("td").eq(0).find('select option:selected').val(),
        "descricao": $(tr).find("td").eq(0).find('select option:selected').val() ,
        "quantidade": $(tr).find("td").eq(2).find('input').val(),
        "area": $(tr).find("td").eq(3).find('input').val(),
        "projecto": $(tr).find("td").eq(4).find('input').val(),
        "notas": $(tr).find("td").eq(5).find('input').val()
    };

    $(tr).find("td").eq(1).find('select').val(linha.artigo).trigger("change") ;

}

function selecionaFerramentaLinha(row) {
    var tr = row.closest('tr');

    var linha = {
        "artigo": $(tr).find("td").eq(1).find('select option:selected').val(),
        "descricao": $(tr).find("td").eq(1).find('select option:selected').val(),
        "quantidade": $(tr).find("td").eq(2).find('input').val(),
        "area": $(tr).find("td").eq(3).find('input').val(),
        "projecto": $(tr).find("td").eq(4).find('input').val(),
        "notas": $(tr).find("td").eq(5).find('input').val()
    };
    
    $(tr).find("td").eq(0).find('select').val(linha.artigo).trigger("change");

}

function actualizaProjecto() {
    var area = areaNegocio;

    if (area.length > 0) {
        $.ajax({
            url: "/Iventario/ListaProjetos_CentroCusto?areaNegocio=" + area,
            data: areaNegocio,
            type: "Get",
            contentType: "application/json",
            success: function (data) {

                listaProjeto = data;

                $("#listBoxProjecto").empty();

                var o = new Option("Selecione o Projecto", "");

                $("#listBoxProjecto").append(o);

                $.each(listaProjeto, function (key, value) {
                    var o = new Option(value.descricao, value.codigo);
                    $("#listBoxProjecto").append(o);
                });
            }
        });
    } else {
        $("#listBoxProjecto").empty();
    }
    

    

}

function GetListaFuncionarios() {
    $.ajax({
        url: "/Iventario/ListaFuncionarios",
        type: "Get",
        contentType: "application/json",
        success: function (data) {

            listaFuncionarios = data;

            $("#ListBoxfuncionario").empty();

            var o = new Option("Selecione o Funcionario", "");

            $("#ListBoxfuncionario").append(o);

            $.each(listaFuncionarios, function (key, value) {
                var o = new Option(value.nome, value.codigo);
                $("#ListBoxfuncionario").append(o);
            });
        }
    });
}

function GetListaProjeto() {
    $.ajax({
        url: "/Iventario/ListaProjetos_CentroCusto",
        type: "Get",
        contentType: "application/json",
        success: function (data) {

            listaProjeto = data;

            $("#listBoxProjecto").empty();

            var o = new Option("Selecione o Projecto", "");

            $("#listBoxProjecto").append(o);

            $.each(listaProjeto, function (key, value) {
                var o = new Option(value.descricao, value.codigo);
                $("#listBoxProjecto").append(o);
            });

        }
    });
}

function GetListaNegocio() {
    $.ajax({
        url: "/Iventario/ListaNegocio_CentroCusto",
        type: "Get",
        contentType: "application/json",
        success: function (data) {

            listaNegocio = data;

            $("#ListAreaNegocio").empty();

            var o = new Option("Selecione a Area de Negocio", "");

            $("#ListAreaNegocio").append(o);

            $.each(listaNegocio, function (key, value) {
                var o = new Option(value.descricao, value.codigo);
                $("#ListAreaNegocio").append(o);
            });
        }
    });
}

function daFuncionario(funcionario) {
    var ccusto = "";
    
    tempFuncionario = {};

    $.each(listaFuncionarios, function (key, value) {
        
        if (value.codigo == funcionario) {
            
            tempFuncionario = value;
            ccusto = tempFuncionario.ccusto;
           
        }
    });

    $("#ListAreaNegocio").val(ccusto).trigger("change");
}

/* function daNomeFuncionario(funcionario) {
    var nome = "";

    tempFuncionario = {};

    $.each(listaFuncionarios, function (key, value) {
        
        if (value.codigo == funcionario) {
            nome = value.nome;
            tempFuncionario = value;
            console.log(tempFuncionario);
        }
    });

    return nome;
} */

function GetListaArtigosModal(tipo) {
    $.ajax({
        url: "/Iventario/ListaArtigos?tipo=" + tipo,
        //type: "Get",
        //contentType: "application/json",
        //data: JSON.stringify(sendJsonData),
        success: function (data) {

            listaArtigos = data;

            $("#listaArtigos").empty();

            var o = new Option("Selecione o Artigo", "");

            $("#listaArtigos").append(o);

            $.each(listaArtigos, function (key, value) {
                var o = new Option(value.descricao, value.artigo);
                $("#listaArtigos").append(o);
            });
        }
    });
}

function Sair(titulo) {

    if (confirm('Tem a certeza que deseja cancelar a janela de ' + titulo + '?')) {
        var url = '/Ferramentas/Index';
        window.location.href = url;
    } else {
        // Do nothing!
    }
    
}
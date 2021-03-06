
var ferramentasTable;
var listaNegocio = {};
var listaFuncionarios = {};
var tempFuncionario = {};
var listaArtigos = {};
var tipoArtigo = 3;

var listaLinhasRemovidas = {};
var linhasEditadas = {};
var linha = {};

$("#listaArtigos").select2({
    dropdownParent: $("#ferramentaModal")
});

$("#ListAreaNegocio").select2({
    dropdownParent: $("#ferramentaModal")
});

$("#ListAreaNegocio").prop('disabled', true);

$("#listBoxProjecto").select2({
    dropdownParent: $("#ferramentaModal")
});

$('#ListAreaNegocio').on('change', function (e) {

});

ferramentasTable = $('#tableFerramentas').DataTable({
    select: {
        style: 'single'
    },
    "ordering": true,
    searching: false,
    responsive: true,
    "bProcessing": true,
    "lengthChange": false,
    "columnDefs": [
        {
            "targets": [0],
            "visible": false,
            "searchable": false
        }
    ],
    "columns": [
        { data: 'id', "title": "Id" },
        { data: 'artigo', "title": "Ferramenta" },
        { data: 'descricao', "title": "Descri��o" },
        { data: 'quantidade', "title": "Quant." },
        { data: 'notas', "title": "Notas" },
        { data: 'areaNegocio', "title": "Centro de Negocio" },
        { data: 'projecto', "title": "Projecto" }
    ]
});

$(document).ready(function () {
    
    $('#ListBoxfuncionario').on('select2:select', function (e) {

        var data = e.params.data;
        if (data.id == "") {
            $(".linhasDoc").hide();
        } else {
            $(".linhasDoc").show();
        }

        daFuncionario(data.id);

    });

    GetListaFuncionarios();

    GetListaArtigos(tipoArtigo);
    GetListaArtigosModal(tipoArtigo);
    GetListaProjeto();
    GetListaNegocio();

    inicializaTabela();

});

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

function gravar(sair, id,tipodoc) {

    var nomeFunc = $("#ListBoxfuncionario").select2('data')[0].text;

    var cabecDoc = {
        "id": id,
        "tipodoc": tipodoc,
        "funcionario": $('#ListBoxfuncionario').val(),
        "nome": nomeFunc,
        "nrDocExterno": $('#nrDocExterno').val(),
        "data": moment($("#dtData").data("date")).format(),
        //"notas": "",
        "linhas": [],
    };

    var table1 = $('#tableFerramentas').DataTable();

    for (var i = 0; i < table1.data().count(); i++) {

        var rows = table1.rows(i).data();

        rows.each(function () {
            linha = {
                "id": this[0].id,
                "artigo": this[0].artigo,
                "descricao": this[0].descricao,
                "codbarrasCabec": "",
                "quantidade": this[0].quantidade,
                "quantTrans": 0,
                "quantPendente": this[0].quantidade,
                "notas": this[0].notas,
                "areaNegocio": this[0].areaNegocio,
                "projecto": this[0].projecto,
                "CabecStockId": id
            };

            cabecDoc.linhas.push(linha);
        });
    }

    if (cabecDoc.funcionario.length == 0) {
        alert("O Funcion�rio � de preenchemento obrigatorio!");
        return;
    }

    if (cabecDoc.nrDocExterno.length == 0) {
        alert("O N�mero da Guia � de preenchemento obrigatorio!");
        return;
    }

    if (cabecDoc.linhas.length > 0) {

        $("#loadMe").modal({
            backdrop: "static", //remove ability to close modal with click
            keyboard: false, //remove option to close with keyboard
            show: true //Display loader!
        });

        $.ajax({
            url: "/Ferramentas/EditarDocumento",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            headers: {
                "RequestVerificationToken": $('input[name = __RequestVerificationToken]').val()
            },
            data: JSON.stringify(cabecDoc),
            dataType: "json",
            success: function (data) {


                if (data.success == true) {
                    if (sair == true) {
                        var url = '/Ferramentas';
                        window.location.href = url;
                    } else {
                        var url = '/Ferramentas';
                        window.location.href = url;
                    }


                } else {

                    alert("Ocorreu um erro durante a grava��o");
                }

                $("#loadMe").modal("hide");

            },
            error: function (xhr, ajaxOptions, thrownError) {
                $("#loadMe").modal("hide");
                alert(xhr.status);
                alert(thrownError);
            }
        });
    } else {
        alert("Preencha as Linhas");
    }
}

function GetListaArtigos(tipo) {
    $.ajax({
        url: "/Iventario/ListaArtigos?tipo=" + tipo,
        success: function (data) {
            listaArtigos = data;
        }
    });
}

function GetListaArtigosModal(tipo) {
    $.ajax({
        url: "/Iventario/ListaArtigos?tipo=" + tipo,

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

function GetListaProjeto() {
    $.ajax({
        url: "/Iventario/ListaProjetos",
        type: "Get",
        contentType: "application/json",
        success: function (data) {

            listaProjeto = data;

            $("#listBoxProjecto").empty();

            var o = new Option("Selecione o Projecto", "");

            $("#listBoxProjecto").append(o);

            $.each(listaProjeto, function (key, value) {
                var o = new Option(value.codigo, value.codigo);
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

            var func = $("#funcionario").val();

            $("#ListBoxfuncionario").val(func).trigger("change");
            daFuncionarioCNegocio(func);
        }
    });
}

function daFuncionarioCNegocio(funcionario) {
    var cNegocio = "";

    tempFuncionario = {};

    $.each(listaFuncionarios, function (key, value) {

        if (value.codigo == funcionario) {

            tempFuncionario = value;
            cNegocio = tempFuncionario.ccusto;

        }
    });
    return cNegocio;
}

function daDescricaoCNegocio(cNegocio) {
    var descricao = "";

    $.each(listaNegocio, function (key, value) {

        if (value.codigo == cNegocio) {

            descricao = value.descricao;
        }
    });

    return descricao;
}

function AddRow() {
    var func = $('#ListBoxfuncionario').val();
    if (func.length == 0) {
        alert("Selecione Primeiro o Funcionario n� janela de saidas");
        return;
    }

    var desc = $("#listaArtigos").select2('data')[0].text;
    var desc_centro = $("#ListAreaNegocio").select2('data')[0].text;

    var linha = {

        Ferramenta: $("#listaArtigos").val(),
        Descricao: desc,
        CodBarras: "",
        Quantidade: $("#quantidade").val(),
        Notas: $("#notas").val(),
        AreaNegocio: $('#ListAreaNegocio').val(),
        Projecto: $('#listBoxProjecto').val()

    };

    if (linha.Ferramenta.length == 0) {
        alert("� obrigatorio o preenchimento do campo de Ferramentas");

        $().alert()
    } else {

        //var table1 = $('#tableFerramentas').DataTable();
        ferramentasTable.row.add({

            'id': 0 ,
            'artigo': linha.Ferramenta,
            'descricao': linha.Descricao,
            'quantidade': linha.Quantidade,
            'notas': linha.Notas,
            'areaNegocio': linha.AreaNegocio,
            //"AreaNegocio_Descricao": desc_centro,
            'projecto': linha.Projecto
        }).draw(false);

        $("#ferramentaModal").modal('hide');
    }
}

function EditRow() {
    var func = $('#ListBoxfuncionario').val();
    if (func.length == 0) {
        alert("Selecione Primeiro o Funcionario n� janela de saidas");
        return;
    }

    var desc = $("#listaArtigos").select2('data')[0].text;
    var desc_centro = $("#ListAreaNegocio").select2('data')[0].text;

    var linha = {

        Ferramenta: $("#listaArtigos").val(),
        Descricao: desc,
        CodBarras: "",
        Quantidade: $("#quantidade").val(),
        Notas: $("#notas").val(),
        AreaNegocio: $('#ListAreaNegocio').val(),
        Projecto: $('#listBoxProjecto').val()

    };

    if (linha.Ferramenta.length == 0) {
        alert("� obrigatorio o preenchimento do campo de Ferramentas");

        $().alert()
    } else {

        removeFerramenta();
        //var table1 = $('#tableFerramentas').DataTable();
        ferramentasTable.row.add({

            'id': 0,
            'artigo': linha.Ferramenta,
            'descricao': linha.Descricao,
            'quantidade': linha.Quantidade,
            'notas': linha.Notas,
            'areaNegocio': linha.AreaNegocio,
            //"AreaNegocio_Descricao": desc_centro,
            'projecto': linha.Projecto
        }).draw(false);

        $("#ferramentaModal").modal('hide');
    }
}

function removeFerramenta() {
    ferramentasTable.row('.selected').remove().draw(false);
}

function editaFerramentaShow() {
    var data = ferramentasTable.rows({ selected: true }).data()[0];

    $("#listaArtigos").val(data.artigo).trigger("change");
    $("#quantidade").val(data.quantidade);
    $("#ListAreaNegocio").val(tempFuncionario.ccusto).trigger("change");
    $("#listBoxProjecto").val(data.projecto).trigger("change");
    $("#notas").val(data.notas);

    $("#btActualizar").show();
    $("#btRemover").show();

    $("#btLimpar").hide();
    $("#btAdicionar").hide();

    $("#ferramentaModal").modal({ show: true });
}

function addFerramentaShow() {
    clean();

    $("#btActualizar").hide();
    $("#btRemover").hide();
    

    $("#btLimpar").show();
    $("#btAdicionar").show();

    $("#ferramentaModal").modal({ show: true });
}

function inicializaTabela() {
    var idDoc = $('#id').val();

    $.ajax({
        url: "/Ferramentas/LinhasDocStock/"+idDoc,
        type: "Get",
        contentType: "application/json",
        success: function (data) {
            
            $.each(data, function (key, value) {
                
                ferramentasTable.row.add({
                    'id': value.id,
                    'artigo': value.artigo,
                    'descricao': value.descricao,
                    'quantidade': value.quantidade,
                    'notas': value.notas,
                    'areaNegocio': value.areaNegocio,
                    //"AreaNegocio_Descricao": desc_centro,
                    'projecto': value.projecto
                }).draw(false);
            });
        }
    });
}

function clean() {
       
    $("#listaArtigos").val("").trigger("change");;
    $("#quantidade").val(1);
    $("#ListAreaNegocio").val(tempFuncionario.ccusto).trigger("change");
    $("#listBoxProjecto").val("").trigger("change");;
    $("#notas").val("");
}
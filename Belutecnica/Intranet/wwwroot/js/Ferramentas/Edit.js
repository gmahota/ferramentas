
var ferramentasTable;
var listaNegocio = {};
var listaFuncionarios = {};
var tempFuncionario = {};
var listaArtigos = {};
var tipoArtigo = 3;

$(document).ready(function () {

    GetListaFuncionarios();

    GetListaArtigos(tipoArtigo);
    GetListaArtigosModal(tipoArtigo);
    GetListaProjeto();
    GetListaNegocio();

    var func = $("#funcionario").val();

    $("#ListBoxfuncionario").val(func).trigger("change");

    

    ferramentasTable = $('#tableFerramentas').DataTable({
        select: {
            style: 'single'
        },
        "ordering": true,
        searching: false,
        responsive: true,
        "bProcessing": true,
        //"bServerSide": true,
        //"sAjaxSource": '/Ferramentas/LinhasDocStock',
        //data: data,
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
            { data: 'descricao', "title": "Descrição" },
            { data: 'quantidade', "title": "Quant." },
            { data: 'notas', "title": "Notas" },
            { data: 'areaNegocio', "title": "Centro de Negocio" },
            { data: 'projecto', "title": "Projecto" },
            {
                "className": 'details-control',
                "orderable": false,
                "data": null,
                "defaultContent": ''
            }
            //{ data: 'Accoes' }

        ]
    });

    //// Add event listener for opening and closing details
    //$('#tableFerramentas tbody').on('click', 'tr', function () {

        
    //});

    inicializaTabela();

});

function gravar(sair, id) {

    var linha = {};

    var nomeFunc = $("#ListBoxfuncionario").select2('data')[0].text;

    var cabecDoc = {
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
                "artigo": this[0].Ferramenta,
                "descricao": this[0].Desc,
                "codbarrasCabec": "",
                "quantidade": this[0].Quantidade,
                "quantTrans": 0,
                "quantPendente": this[0].Quantidade,
                "notas": this[0].Notas,
                "areaNegocio": this[0].AreaNegocio,
                "projecto": this[0].Projecto
            };

            cabecDoc.linhas.push(linha);
        });
    }

    if (cabecDoc.funcionario.length == 0) {
        alert("O Funcionário é de preenchemento obrigatorio!");
        return;
    }

    if (cabecDoc.nrDocExterno.length == 0) {
        alert("O Número da Guia é de preenchemento obrigatorio!");
        return;
    }

    if (cabecDoc.linhas.length > 0) {

        $("#loadMe").modal({
            backdrop: "static", //remove ability to close modal with click
            keyboard: false, //remove option to close with keyboard
            show: true //Display loader!
        });

        $.ajax({
            url: "/Ferramentas/GravarSaida",
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
                        var url = '/Ferramentas/Saida';
                        window.location.href = url;
                    }


                } else {

                    alert("Ocorreu um erro durante a gravação");
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
        alert("Selecione Primeiro o Funcionario ná janela de saidas");
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
        alert("É obrigatorio o preenchimento do campo de Ferramentas");

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

function removeFerramenta() {
    ferramentasTable.row('.selected').remove().draw(false);
}

function editaFerramenta() {
    var data = ferramentasTable.rows({ selected: true }).data()[0];

    $("#listaArtigos").val(data.artigo).trigger("change");
    $("#quantidade").val(data.quantidade);
    $("#ListAreaNegocio").val(data.areaNegocio);
    $("#listBoxProjecto").val(data.projecto);
    $("#notas").val(data.notas);

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
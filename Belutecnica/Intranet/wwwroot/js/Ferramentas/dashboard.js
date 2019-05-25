﻿var entradaferramentasTable;
var listaNegocio = {};
var listaFuncionarios = {};
var tempFuncionario = {};

$(document).ready(function () {

    GetListaFuncionarios();

    GetListaNegocio();

});

entradaferramentasTable = $('#tableEntradaFerramentas').DataTable({
    select: {
        style: 'single'
    }, "ordering": true, searching: true,
    responsive: true, "bProcessing": true,
    "bServerSide": true,
    "sAjaxSource": '/Ferramentas/Dashboard_Funcionario',

    "columns": [
        { "data": "codigo", "title": "Funcionário" },
        { "data": "nome", "title": "Nome" },
        {
            "data": "ccusto", "title": "Centro Negócio",
            "render": function (data, type, row) {
                return daDescricaoCNegocio(daFuncionarioCNegocio(row.codigo));
            }
        },
        {
            "className": 'details-control',
            "orderable": false,
            "data": null,
            "defaultContent": ''
        }
        
    ]
});

// Add event listener for opening and closing details
$('#tableEntradaFerramentas tbody').on('click', 'td.details-control', function () {
    var tr = $(this).closest('tr');
    var row = entradaferramentasTable.row(tr);

    if (row.child.isShown()) {
        // This row is already open - close it
        row.child.hide();
        tr.removeClass('shown');
    }
    else {
        // Open this row
        var subtbl = $('<table class="table  table-bordered" style="width:100%"/>');
        subtbl.DataTable({
            data: row.data().linhas,
            "columnDefs": [
                {
                    "targets": [0], "render": function (data) {
                        return moment(data).format('DD-MM-YYYY HH:mm');
                    }
                }
            ],
            columns: [
                { data: "data", title: "Data" },
                { data: "artigo", title: "Ferramenta" },
                { data: "descricao", title: "Descrição" },    
                { data: "quantPendente", title: "Qnt. Pend" },
                { data: "nrDocExterno", title: "Nr. Guia" },
                { data: "notas", title: "Notas" }
            ]
        });
        row.child(subtbl).show();
        tr.addClass('shown');
    }
});



function GetListaNegocio() {
    $.ajax({
        url: "/Iventario/ListaNegocio_CentroCusto",
        type: "Get",
        contentType: "application/json",
        success: function (data) {

            listaNegocio = data;

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
﻿var entradaferramentasTable;

entradaferramentasTable = $('#tableEntradaFerramentas').DataTable({
    select: {
        style: 'multi',
        selector: 'td:first-child'
    },
    "ordering": true,
    searching: true,
    responsive: true,
    "bProcessing": true,
    "bServerSide": true,
    "sAjaxSource": '/Ferramentas/Linhas',
    "columnDefs": [
        {
            orderable: false,
            className: 'select-checkbox',
            targets: 0
        },
        {
            "targets": [1],
            "visible": false,
            "searchable": false
        },

        {
            "targets": [4], "render": function (data) {
                return moment(data).format('DD-MM-YYYY');
            }
        }
    ],
    "columns": [

        {
            "data": null, "className": 'select-checkbox',
            "defaultContent": '', "title": "Sel."
        },
        { "data": "id" },
        { "data": "cabecStockId", "title": "Doc." },
        { "data": "nrDocExterno", "title": "Nr. Guia" },
        { "data": "data", "title": "Data" },
        { "data": "artigo", "title": "Ferramenta" },
        { "data": "descricao", "title": "Descrição" },
        { "data": "quantidade", "title": "Qnt." },
        { "data": "notas", "title": "Notas" },
        //{"data":"status","title":"Status"},

    ]
});

$('#ListBoxfuncionario').on('select2:select', function (e) {

    var data = e.params.data;

    if (data.id == "") {
        $(".linhasDoc").hide();
    } else {
        $(".linhasDoc").show();
        preencheFerramentasFuncionario(data.id);
    }


});

function preencheFerramentasFuncionario(funcionario) {

    entradaferramentasTable.search(funcionario).draw();
}

function gravar(sair, tipodoc) {

    var linha = {};

    var nomeFunc = $("#ListBoxfuncionario").select2('data')[0].text;

    var cabecDoc = {
        "tipodoc": tipodoc,
        "funcionario": $('#ListBoxfuncionario').val(),
        "nome": nomeFunc,
        "nrDocExterno": $('#nrDocExterno').val(),
        "data": moment($("#dtData").data("date")).format(),
        //"notas": "",
        "status": 1,
        "linhas": [],
    };

    var rows = entradaferramentasTable.rows('.selected').data();


    for (var i = 0; i < rows.length; i++) {
        console.log(rows[i]);
        linha = {
            "artigo": rows[i].artigo,
            "descricao": rows[i].descricao,
            "codbarrasCabec": "",
            "quantidade": rows[i].quantidade,
            "notas": rows[i].notas,
            "idLinhaOrigem": rows[i].id,
            "idDocumentoOrigem": rows[i].cabecStockId,
            "status": 1
        };

        cabecDoc.linhas.push(linha);
    }

    if (cabecDoc.funcionario.length == 0) {
        alert("É obrigatorio preencher o Funcionário!");
        return;
    }
    
    if (cabecDoc.nrDocExterno.length == 0) {
        alert("É obrigatorio preencher O Número da Guia!");
        return;
    }
    

    if (cabecDoc.linhas.length > 0) {

        $.ajax({
            url: "/Ferramentas/GravarEntrada",
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
                        var url = '/Ferramentas/Entrada';
                        window.location.href = url;
                    }


                } else {

                    alert("Ocorreu um erro durante a gravação");
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });
    } else {
        alert("Preencha as Linhas");
    }
}
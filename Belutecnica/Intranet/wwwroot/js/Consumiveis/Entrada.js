var entradaferramentasTable;

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
    "sAjaxSource": '/Ferramentas/LinhasFerramentasFuncionario',
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
            "targets": [3], "render": function (data) {
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
        { "data": "data", "title": "Data" },
        { "data": "artigo", "title": "Ferramenta" },
        { "data": "descricao", "title": "Descricao" },
        { "data": "quantidade", "title": "Quantidade" },
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
        //"data": $('#dtData').val(),
        //"notas": "",
        "status": 1,
        "linhas": [],
    };

    var rows = entradaferramentasTable.rows('.selected').data();

    console.log(rows);

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
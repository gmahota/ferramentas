var ferramentasTable;

$(document).ready(function () {

    ferramentasTable = $('#ferramentasTable').DataTable({
        select: {
            style: 'single'
        }, "ordering": true, searching: true,
        responsive: true, "bProcessing": true,
        "bServerSide": true,
        "sAjaxSource": '/Ferramentas/Lista',
        "columnDefs": [
            {
                "targets": [0], "render": function (data) {
                    return moment(data).format('DD-MM-YYYY HH:mm');
                }
            }
        ],
        "columns": [


            //{ "data": "id", "title": "Nr. Doc." },
            { "data": "data","title":"Data" },
            { "data": "funcionario","title":"Funcionário" },
            { "data": "nome", "title": "Nome" },
            { "data": "nrDocExterno", "title": "Nr. da Guia" },
            { "data": "notas", "title": "Notas" },
            {
                "className": 'details-control',
                "orderable": false,
                "data": null,
                "defaultContent": ''
            }
        ]
    });

    // Add event listener for opening and closing details
    $('#ferramentasTable tbody').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = ferramentasTable.row(tr);

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
                columns: [
                    { data: "artigo", title: "Ferramenta" },
                    { data: "descricao", title: "Descrição" },
                    { data: "quantidade", title: "Quantidade" },
                    { data: "quantTrans", title: "Qnt. Trans" },
                    { data: "quantPendente", title: "Qnt. Pend" },
                    { data: "notas", title: "Notas" }
                    
                ]
            });
            row.child(subtbl).show();
            tr.addClass('shown');
        }
    });

});



$("#listaArtigos").select2({
    dropdownParent: $("#ferramentaModal")
});

$("#ListAreaNegocio").select2({
    dropdownParent: $("#ferramentaModal")
});

$("#ListAreaNegocio").prop('disabled',true);

$("#listBoxProjecto").select2({
    dropdownParent: $("#ferramentaModal")
});

$('#ListAreaNegocio').on('change', function (e) {
    
});

$('#tableFerramentas').DataTable({

    //data: data,
    columns: [
        { data: 'Ferramenta' },
        { data: 'Desc' },
        { data: 'Quantidade' },
        { data: 'Notas' },
        { data: 'AreaNegocio' },
        { data: 'Projecto' },
        { data: 'Accoes' }

    ]
});

function AddRow() {
    var func = $('#ListBoxfuncionario').val();
    if (func.length == 0) {
        alert("Selecione Primeiro o Funcionario ná janela de saidas");
        return;
    }

    var desc = $("#listaArtigos").select2('data')[0].text;
    
    
    var linha = {

        Ferramenta: $("#listaArtigos").val(),
        Descricao: desc,
        CodBarras: "",
        Quantidade: $("#quantidade").val(),
        Notas: $("#notas").val(),
        AreaNegocio:$('#ListAreaNegocio').val(),
        Projecto:$('#listBoxProjecto').val()

    };

    if (linha.Ferramenta.length == 0) {
        alert("É obrigatorio o preenchimento do campo de Ferramentas");

        $().alert()
    } else {
        var table1 = $('#tableFerramentas').DataTable();

        table1.row.add({
            "Ferramenta": linha.Ferramenta,
            "Desc": linha.Descricao,
            //"CodBarras": linha.CodBarras,
            "Quantidade": linha.Quantidade,
            "Notas": linha.Notas,
            "AreaNegocio":linha.AreaNegocio,
            "Projecto":linha.Projecto,
            "Accoes":
                //"<button class='btn btn-danger btn-sm' style ='font-size: 9px;' onclick = 'addRow($(this))' >" +
                //"<span class='fa fa-check-square'></span>" +
                //"</button >"
                //+
                "<button class='btn btn-danger btn-sm' style = 'font-size: 9px;' onclick = 'removeRow($(this))' >" +
                "<span class='fa fa-remove'></span>" +
                "</button >"
        }).draw(false);

        clean();
    }

    
}

function gravar(sair,tipodoc) {
    
    var linha = {};

    var nomeFunc = $("#ListBoxfuncionario").select2('data')[0].text;

    var cabecDoc = {
        "tipodoc":tipodoc,
        "funcionario": $('#ListBoxfuncionario').val(),
        "nome": nomeFunc,
        "nrDocExterno": $('#nrDocExterno').val(),
        //"data": $('#dtData').val(),
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

function clean() {

    $("#listaArtigos").val("").trigger("change");;
    $("#quantidade").val(1);
    $("#ListAreaNegocio").val(tempFuncionario.ccusto).trigger("change");
    $("#listBoxProjecto").val("").trigger("change");;
    $("#notas").val("");
}


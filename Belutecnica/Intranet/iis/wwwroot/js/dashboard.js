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
    "sAjaxSource": '/Ferramentas/Dashboard',
    "columnDefs": [
        {
            "targets": [0],
            "visible": false,
            "searchable": false
        },

        {
            "targets": [4], "render": function (data) {
                return moment(data).format('DD-MM-YYYY HH:mm');
            }
        }
    ],
    "columns": [

        
        { "data": "id" },
        { "data": "nome", "title": "Nome" },
        { "data": "documento", "title": "Doc." },
        { "data": "nrDocExterno", "title": "Nr. Guia" },
        { "data": "data", "title": "Data" },
        { "data": "artigo", "title": "Ferramenta" },
        { "data": "descricao", "title": "Descrição" },
        { "data": "quantidade", "title": "Qnt." },
        { "data": "notas", "title": "Notas" },

    ]
});


/* globals Chart:false, feather:false */


var MONTHS = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
var color = Chart.helpers.color;
var barChartData = {
    labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
    datasets: [{
        label: 'Dataset 1',
        backgroundColor: color(window.chartColors.red).alpha(0.5).rgbString(),
        borderColor: window.chartColors.red,
        borderWidth: 1,
        data: [
            randomScalingFactor(),
            randomScalingFactor(),
            randomScalingFactor(),
            randomScalingFactor(),
            randomScalingFactor(),
            randomScalingFactor(),
            randomScalingFactor()
        ]
    }, {
        label: 'Dataset 2',
        backgroundColor: color(window.chartColors.blue).alpha(0.5).rgbString(),
        borderColor: window.chartColors.blue,
        borderWidth: 1,
        data: [
            randomScalingFactor(),
            randomScalingFactor(),
            randomScalingFactor(),
            randomScalingFactor(),
            randomScalingFactor(),
            randomScalingFactor(),
            randomScalingFactor()
        ]
    }]

};

window.onload = (function () {
    'use strict'

    //feather.replace()

    // Graphs
    var ctx = document.getElementById('myChart')
    // eslint-disable-next-line no-unused-vars
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: [
                'Sunday',
                'Monday',
                'Tuesday',
                'Wednesday',
                'Thursday',
                'Friday',
                'Saturday'
            ],
            datasets: barChartData
        },
        options: {
            responsive: true,
            legend: {
                position: 'top',
            },
            title: {
                display: true,
                text: 'Chart.js Bar Chart'
            }
        }
    })
}());
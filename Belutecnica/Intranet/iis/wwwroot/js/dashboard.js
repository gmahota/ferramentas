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
        //{"data":"status","title":"Status"},

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

//window.onload = function () {
//    var ctx = document.getElementById('canvas').getContext('2d');
//    window.myBar = new Chart(ctx, {
//        type: 'bar',
//        data: barChartData,
//        options: {
//            responsive: true,
//            legend: {
//                position: 'top',
//            },
//            title: {
//                display: true,
//                text: 'Chart.js Bar Chart'
//            }
//        }
//    });

//};

//document.getElementById('randomizeData').addEventListener('click', function () {
//    var zero = Math.random() < 0.2 ? true : false;
//    barChartData.datasets.forEach(function (dataset) {
//        dataset.data = dataset.data.map(function () {
//            return zero ? 0.0 : randomScalingFactor();
//        });

//    });
//    window.myBar.update();
//});

//var colorNames = Object.keys(window.chartColors);
//document.getElementById('addDataset').addEventListener('click', function () {
//    var colorName = colorNames[barChartData.datasets.length % colorNames.length];
//    var dsColor = window.chartColors[colorName];
//    var newDataset = {
//        label: 'Dataset ' + (barChartData.datasets.length + 1),
//        backgroundColor: color(dsColor).alpha(0.5).rgbString(),
//        borderColor: dsColor,
//        borderWidth: 1,
//        data: []
//    };

//    for (var index = 0; index < barChartData.labels.length; ++index) {
//        newDataset.data.push(randomScalingFactor());
//    }

//    barChartData.datasets.push(newDataset);
//    window.myBar.update();
//});

//document.getElementById('addData').addEventListener('click', function () {
//    if (barChartData.datasets.length > 0) {
//        var month = MONTHS[barChartData.labels.length % MONTHS.length];
//        barChartData.labels.push(month);

//        for (var index = 0; index < barChartData.datasets.length; ++index) {
//            // window.myBar.addData(randomScalingFactor(), index);
//            barChartData.datasets[index].data.push(randomScalingFactor());
//        }

//        window.myBar.update();
//    }
//});

//document.getElementById('removeDataset').addEventListener('click', function () {
//    barChartData.datasets.pop();
//    window.myBar.update();
//});

//document.getElementById('removeData').addEventListener('click', function () {
//    barChartData.labels.splice(-1, 1); // remove the label first

//    barChartData.datasets.forEach(function (dataset) {
//        dataset.data.pop();
//    });

//    window.myBar.update();
//});

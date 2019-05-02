$(".selectpicker").select2({
    //templateSelection: formatState
});

$(document).ready(function () {
    var date = new Date();
    var today = new Date(date.getFullYear()-1, date.getMonth(), date.getDate());
    var end = new Date(date.getFullYear(), 12, 31);
    
    $('.datetimepicker-input').datetimepicker({
        defaultDate: moment()
    });
    
});


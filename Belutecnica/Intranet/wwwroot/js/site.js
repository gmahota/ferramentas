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



//$(document).ready(function () {
//    var pressed = false;
//    var chars = [];
//    $(window).keypress(function (e) {
//        if (e.which >= 48 && e.which <= 57) {
//            chars.push(String.fromCharCode(e.which));
//        }
//        //console.log(e.which + ":" + chars.join("|"));
//        if (pressed == false) {
//            setTimeout(function () {
//                if (chars.length >= 10) {
//                    var barcode = chars.join("");
//                    console.log("Barcode Scanned: " + barcode);
//                    // assign value to some input (or do whatever you want)
//                    $("#MainContent_txtInput").val(barcode);
//                    //var selected = $('#ListBoxfuncionario').find('option:selected').text();
//                    var selected = $('#MainContent_ListBoxfuncionario').find('option:selected').val();;
//                    if (selected == "") {
//                        getFuncionarioCodigoBarras();
//                    } else {
//                        var btn = document.getElementById("MainContent_NewFerramentaBt");
//                        if (btn) btn.click();
//                    }
//                } else {
//                }
//                chars = [];
//                pressed = false;
//            }, 500);
//        }
//        pressed = true;
//    });
//});


function asignarValorSalarioMensual(textboxSalarioMensual) {

    var costo1 = textboxSalarioMensual.value;
    var index = costo1.indexOf('.');

    var costo2 = costo1.replace(",", "");
    var costo3 = costo2.replace(",", "");
    var costo4 = costo3.replace("$", "");
    var costo = costo4.replace(".", "");

    if (index > -1) {
        var costo = costo.substring(0, costo.length - 2);
    }
    else {
        var costo = costo;
    }

    $("#slider-txtSalarioBaseCotizacion").slider({ value: costo });
}


// RARS
$(function () {

    $("#slider-txtSalarioBaseCotizacion").slider({

        range: "min",

        value: 15000,

        min: parseFloat(lInferior),

        max: parseFloat(lSuperior),

        step: 1.1,

        slide: function (event, ui) {

            $("#txtSalarioBaseCotizacion").val(formatCurrency(ui.value));
            $("#HiddenField1").val(ui.value);
            
            
        }

    });

    var valtxtSalarioBaseCotizacion = formatCurrency($("#slider-txtSalarioBaseCotizacion").slider("value"));
    $("#txtSalarioBaseCotizacion").val(valtxtSalarioBaseCotizacion);


});

$(function () {

    $("#slider-txtEdadRetiro").slider({

        range: "min",

        value: 60,

        min: 60,

        max: 67,

        step: 1,

        slide: function (event, ui) {

            $("#txtEdadRetiro").val(ui.value);
            $("#HiddenField1").val(ui.value);
        }

    });

    $("#txtEdadRetiro").val($("#slider-txtEdadRetiro").slider("value"));


});



$(function () {

    var maxAniosCotizados = document.getElementById('txtEdad').value - 14;

    $("#slider-txtAniosCotizados").slider({

        range: "min",

        value: 0,

        min: 0,

        max: maxAniosCotizados,

        step: 1,

        slide: function (event, ui) {

            $("#txtSemanasCotizadas").val(ui.value);
            $("#HiddenField1").val(ui.value);
        }

    });

    //$("#txtSemanasCotizadas").val($("#slider-txtAniosCotizados").slider("value"));


});

$(function () {

    $("#slider-txtRendimientoReralAntesComisiones").slider({

        range: "min",

        value: 4,

        min: 4,

        max: 5,

        step: 1,

        slide: function (event, ui) {

            $("#txtRendimientoReralAntesComisiones").val(ui.value + " %");
            $("#HiddenField1").val(ui.value);
        }

    });

    $("#txtRendimientoReralAntesComisiones").val($("#slider-txtRendimientoReralAntesComisiones").slider("value") + " %");


});

$(function () {

    $("#slider-txtDensidadCotizacion").slider({

        range: "min",

        value: 80,

        min: 80,

        max: 100,

        step: 20,

        slide: function (event, ui) {

            $("#txtDensidadCotizacion").val(ui.value + " %");
            $("#HiddenField1").val(ui.value);
        }

    });

    $("#txtDensidadCotizacion").val($("#slider-txtDensidadCotizacion").slider("value") + " %");
    


});

$(function () {
    $("#slider-txtSalarioBaseCotizacion").slider({ value: 0 });
    $("#slider-txtAniosCotizados").slider({ value: 0, max: 0 });
    $("#txtSalarioBaseCotizacion").val("");
});


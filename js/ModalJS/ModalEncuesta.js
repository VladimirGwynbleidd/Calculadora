$('#btnResultados').click(function () {
    LimpiarModal()
})


$('#btnMejorPension').click(function () {
    LimpiarModal()
})


$('#btnSuficienteRetiro').click(function () {
    LimpiarModal()
})


$('#btnPequenosEsfuerzos').click(function () {
    LimpiarModal()
})


$('#btnInfoBuena').click(function () {
    $('#InfoBuena').toggle();
    $('#InfoRegular').hide();
    $('#InfoBad').hide();
})

$('#btnInfoRegular').click(function () {
    $('#InfoRegular').toggle();
    $('#InfoBuena').hide();
    $('#InfoBad').hide();
})

$('#btnInfoBad').click(function () {
    $('#InfoBad').toggle();
    $('#InfoBuena').hide();
    $('#InfoRegular').hide();
})


function LimpiarModal() {
    //alert("limpiar")
    $('#InfoBuena').hide();
    $('#InfoRegular').hide();
    $('#InfoBad').hide();
}

$('#btnEnviarEncuesta').click(function () {
    LLamarOpcionSatisfaccion("InfoBuena")
})

function LLamarOpcionSatisfaccion(InformacionCarita) {
    var ValorCarita;

    var strSerializado = JSON.stringify(captura);

    var dato = 1
    if (InformacionCarita == "InfoBuena") {
        ValorCarita = 1
    }
    else if (InformacionCarita == "InfoRegular") {
        ValorCarita = 2
    }
    else {
        ValorCarita = 3
    }



    var captura = new function () {
        this.ValorCarita = ValorCarita;

    };

    var strSerializado = JSON.stringify(captura);


    $.ajax({
        type: "POST",
        url: "CalculadoraIMSS.aspx/LLamarOpcionSatisfaccion",
        contentType: "application/json;charset=utf-8",
        data: strSerializado,
        dataType: "json",
        success: function (data) {
            //alert('HOLA')
            $("#modalConfirmacion").modal('show')
            $("#ModalEncuestaSatisfaccion").modal('hide')

            setTimeout(function () {
                $("#ModalEncuestaSatisfaccion").modal('hide')
            }, 4000);


        },
        error: function (result) {
            return result
        }
    });

}
$('#btnCallModal').click(function () {
    LimpiarModal()
})


$('#btnInfoBuena').click(function () {
    LLamarOpcionSatisfaccion("InfoBuena")
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


        },
        error: function (result) {
            return result
        }
    });

}
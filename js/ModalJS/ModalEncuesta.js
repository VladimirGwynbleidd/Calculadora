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
    LLamarOpcionSatisfaccion("InfoBuena")
    $('#InfoBuena').toggle();
    $('#InfoRegular').hide();
    $('#InfoBad').hide();
})

$('#btnInfoRegular').click(function () {
    LLamarOpcionSatisfaccion("InfoRegular")
    $('#InfoRegular').toggle();
    $('#InfoBuena').hide();
    $('#InfoBad').hide();
})

$('#btnInfoBad').click(function () {
    LLamarOpcionSatisfaccion("InfoBad")
    $('#InfoBad').toggle();
    $('#InfoBuena').hide();
    $('#InfoRegular').hide();
})



$('#chkInfoBuenaEncuesta4').click(function () {
    if ($('#chkInfoBuenaEncuesta4').is(':checked')) {
        $("#idTxtArea").prop('disabled', false);
    }
});


$('#chkInfoBuenaEncuesta1').click(function () {
    if ($('#chkInfoBuenaEncuesta1').is(':checked')) {
        $("#idTxtArea").prop('disabled', true);
    }
});


$('#chkInfoBuenaEncuesta2').click(function () {
    if ($('#chkInfoBuenaEncuesta2').is(':checked')) {
        $("#idTxtArea").prop('disabled', true);
    }
});


$('#chkInfoBuenaEncuesta3').click(function () {
    if ($('#chkInfoBuenaEncuesta3').is(':checked')) {
        $("#idTxtArea").prop('disabled', true);
    }
});


function LimpiarModal() {

    //alert("limpiar")
    $("input:radio").removeAttr("checked");
    $('#InfoBuena').hide();
    $('#InfoRegular').hide();
    $('#InfoBad').hide();
}

$('#btnInfoBuenaEnviarEncuesta').click(function () {
    InsertarEncuestaSatisfaccion(1);
})

$('#btnInfoRegularEnviarEncuesta').click(function () {
    InsertarEncuestaSatisfaccion(1);
})

$('#btnInfoBadEnviarEncuesta').click(function () {
    InsertarEncuestaSatisfaccion(1);
})

function LLamarOpcionSatisfaccion(InformacionCarita) {
    var ValorCarita;


    if (InformacionCarita == "InfoBuena") {
        ValorCarita = 1
    }
    if (InformacionCarita == "InfoRegular") {
        ValorCarita = 2
    }
    if (InformacionCarita == "InfoBad") {
        ValorCarita = 0
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
            var res = $.parseJSON(data.d);

            if (res[0].N_ID_PREGUNTA == 1) {
                $('#idInfoBuenaEncuesta1').text(res[0].T_DSC_ENCUESTA);
                $('#idInfoBuenaEncuesta2').text(res[1].T_DSC_ENCUESTA);
                $('#idInfoBuenaEncuesta3').text(res[2].T_DSC_ENCUESTA);
                $('#idInfoBuenaEncuesta4').text(res[3].T_DSC_ENCUESTA);

                $('#chkInfoBuenaEncuesta1').text(res[0].N_ID_ENCUESTA);
                $('#chkInfoBuenaEncuesta2').text(res[1].N_ID_ENCUESTA);
                $('#chkInfoBuenaEncuesta3').text(res[2].N_ID_ENCUESTA);
                $('#chkInfoBuenaEncuesta4').text(res[3].N_ID_ENCUESTA);
            }

            if (res[0].N_ID_PREGUNTA == 2) {
                $('#idInfoRegular1').text(res[0].T_DSC_ENCUESTA);
                $('#idInfoRegular2').text(res[1].T_DSC_ENCUESTA);
                $('#idInfoRegular3').text(res[2].T_DSC_ENCUESTA);
                $('#chkInfoRegular1').text(res[0].N_ID_ENCUESTA);
                $('#chkInfoRegular2').text(res[1].N_ID_ENCUESTA);
                $('#chkInfoRegular3').text(res[2].N_ID_ENCUESTA);
            }


            if (res[0].N_ID_PREGUNTA == 0) {
                $('#idInfoBad1').text(res[0].T_DSC_ENCUESTA);
                $('#idInfoBad2').text(res[1].T_DSC_ENCUESTA);
                $('#idInfoBad3').text(res[2].T_DSC_ENCUESTA);
                $('#chkInfoBad1').text(res[0].N_ID_ENCUESTA);
                $('#chkInfoBad2').text(res[1].N_ID_ENCUESTA);
                $('#chkInfoBad3').text(res[2].N_ID_ENCUESTA);
            }
        },
        error: function (result) {
            return result
        }
    });
}



function InsertarEncuestaSatisfaccion(ValorCarita) {
    var strSerializado = "";


    if (ValorCarita == 1) {
        var captura = new function () {
            this.ValorCarita = ValorCarita;
            this.chkInfoBuenaEncuesta1 = $('input[id="chkInfoBuenaEncuesta1"]:checked').text()
            this.chkInfoBuenaEncuesta2 = $('input[id="chkInfoBuenaEncuesta2"]:checked').text()
            this.chkInfoBuenaEncuesta3 = $('input[id="chkInfoBuenaEncuesta3"]:checked').text()
            this.chkInfoBuenaEncuesta4 = $('input[id="chkInfoBuenaEncuesta4"]:checked').text()
            this.txtArea = $('#idTxtArea').val()

            //this.chkInfoBuenaEncuesta3 = $('#chkInfoBuenaEncuesta3').val();
        };

        strSerializado = JSON.stringify(captura);
    }





    alert(strSerializado)

    $.ajax({
        type: "POST",
        url: "CalculadoraIMSS.aspx/InsertarEncuestaSatisfaccion",
        contentType: "application/json;charset=utf-8",
        data: strSerializado,
        dataType: "json",
        success: function (data) {
            var res = $.parseJSON(data.d);


            $.each(res, function (index, value) {
                console.log(value.T_DSC_ENCUESTA)
            });
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
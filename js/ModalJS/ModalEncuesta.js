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
        $("#idTxtArea").val("");
    }

});


$('#chkInfoBuenaEncuesta2').click(function () {
    if ($('#chkInfoBuenaEncuesta2').is(':checked')) {
        $("#idTxtArea").prop('disabled', true);
        $("#idTxtArea").val("");
    }

});


$('#chkInfoBuenaEncuesta3').click(function () {
    if ($('#chkInfoBuenaEncuesta3').is(':checked')) {
        $("#idTxtArea").prop('disabled', true);
        $("#idTxtArea").val("");
    }

});


function LimpiarModal() {

    //alert("limpiar")
    $("#idTxtArea").val("");
    $("input:radio").removeAttr("checked");
    $('#InfoBuena').hide();
    $('#InfoRegular').hide();
    $('#InfoBad').hide();
}

$('#btnInfoBuenaEnviarEncuesta').click(function () {
    InsertarEncuestaSatisfaccion(1);
})

$('#btnInfoRegularEnviarEncuesta').click(function () {
    InsertarEncuestaSatisfaccion(2);
})

$('#btnInfoBadEnviarEncuesta').click(function () {
    InsertarEncuestaSatisfaccion(0);
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

    if ($("#idTxtArea").val() == "" && $('input[id="chkInfoBuenaEncuesta4"]:checked').text() == "4") {
        $("#modalComentario").modal('show')
        return;
    }




    if (ValorCarita == 1) {
        if ($('#chkInfoBuenaEncuesta1').is(':checked') || $('#chkInfoBuenaEncuesta2').is(':checked') || $('#chkInfoBuenaEncuesta3').is(':checked') || $('#chkInfoBuenaEncuesta4').is(':checked')) {
            if (ValorCarita == 1) {
                var captura = new function () {
                    this.ValorCarita = ValorCarita;
                    this.chkInfoBuenaEncuesta1 = $('input[id="chkInfoBuenaEncuesta1"]:checked').text()
                    this.chkInfoBuenaEncuesta2 = $('input[id="chkInfoBuenaEncuesta2"]:checked').text()
                    this.chkInfoBuenaEncuesta3 = $('input[id="chkInfoBuenaEncuesta3"]:checked').text()
                    this.chkInfoBuenaEncuesta4 = $('input[id="chkInfoBuenaEncuesta4"]:checked').text()
                    this.txtArea = $('#idTxtArea').val()

                };
                strSerializado = JSON.stringify(captura);



                $.ajax({
                    type: "POST",
                    url: "CalculadoraIMSS.aspx/InsertarEncuestaSatisfaccionBuena",
                    contentType: "application/json;charset=utf-8",
                    data: strSerializado,
                    dataType: "json",
                    success: function (data) {
                       
                      
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
        }
        else {
            $("#modalOpcion").modal('show')
        }
    }

    if (ValorCarita == 2) {
        if ($('#chkInfoRegular1').is(':checked') || $('#chkInfoRegular2').is(':checked') || $('#chkInfoRegular3').is(':checked')) {
            if (ValorCarita == 2) {

                var captura = new function () {
                    this.ValorCarita = ValorCarita;
                    this.chkInfoRegular1 = $('input[id="chkInfoRegular1"]:checked').text()
                    this.chkInfoRegular2 = $('input[id="chkInfoRegular2"]:checked').text()
                    this.chkInfoRegular3 = $('input[id="chkInfoRegular3"]:checked').text()

                };
                strSerializado = JSON.stringify(captura);



                $.ajax({
                    type: "POST",
                    url: "CalculadoraIMSS.aspx/InsertarEncuestaSatisfaccionRegular",
                    contentType: "application/json;charset=utf-8",
                    data: strSerializado,
                    dataType: "json",
                    success: function (data) {
                       

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
        }
        else {
            $("#modalOpcion").modal('show')
        }

    }


   
    if (ValorCarita == 0) {
        if ($('#chkInfoBad1').is(':checked') || $('#chkInfoBad2').is(':checked') || $('#chkInfoBad3').is(':checked')) {
            if (ValorCarita == 0) {
                var captura = new function () {
                    this.ValorCarita = ValorCarita;
                    this.chkInfoBad1 = $('input[id="chkInfoBad1"]:checked').text()
                    this.chkInfoBad2 = $('input[id="chkInfoBad2"]:checked').text()
                    this.chkInfoBad3 = $('input[id="chkInfoBad3"]:checked').text()

                };
                strSerializado = JSON.stringify(captura);


                $.ajax({
                    type: "POST",
                    url: "CalculadoraIMSS.aspx/InsertarEncuestaSatisfaccionBad",
                    contentType: "application/json;charset=utf-8",
                    data: strSerializado,
                    dataType: "json",
                    success: function (data) {
                                          
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
        }
        else {
            alert("0")
            $("#modalOpcion").modal('show')
        }

    }



    //alert(strSerializado)

   

}
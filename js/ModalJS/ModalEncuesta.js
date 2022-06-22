$('#btnResultados').click(function () {

    LimpiarCaritas()
    LimpiarModal()
})


$('#btnMejorPension').click(function () {
    LimpiarCaritas()
    LimpiarModal()
})


$('#btnSuficienteRetiro').click(function () {
    LimpiarCaritas()
    LimpiarModal()
})


$('#btnPequenosEsfuerzos').click(function () {
    LimpiarCaritas()
    LimpiarModal()
})


$('#btnInfoBuena').click(function () {
    LLamarOpcionSatisfaccion("InfoBuena")


    if ($('#idImgGood1:visible').is(":visible")) {
        $('#idImgGood2').css('visibility', 'visible');
        $("#idImgGood2").show();
        $('#idImgGood1').css('display', 'none');
        $("#idImgGood1").hide();


        $('#idImgRegular1').css('visibility', 'visible');
        $("#idImgRegular1").show();
        $('#idImgRegular2').css('display', 'none');
        $("#idImgRegular2").hide();


        $('#idImgBad1').css('visibility', 'visible');
        $("#idImgBad1").show();
        $('#idImgBad2').css('display', 'none');
        $("#idImgBad2").hide();


    } else {
        $('#idImgGood1').css('visibility', 'visible');
        $("#idImgGood1").show();
        $('#idImgGood2').css('display', 'none');
        $("#idImgGood2").hide();
    }



    //Encondemos las vventanas
    $('#InfoBuena').toggle();
    $('#InfoRegular').hide();
    $('#InfoBad').hide();


    $("#idTxtAreaRegular").val("");
    $("#idTxtAreaBad").val("");


    $("input:radio").removeAttr("checked");


    $("#idTxtAreaRegular").prop('disabled', true);
    $("#idTxtAreaBad").prop('disabled', true);


})

$('#btnInfoRegular').click(function () {
    LLamarOpcionSatisfaccion("InfoRegular")

    if ($('#idImgRegular1:visible').is(":visible")) {

        $('#idImgRegular2').css('visibility', 'visible');
        $('#idImgRegular1').css('display', 'none');
        $("#idImgRegular2").show();
        $("#idImgRegular1").hide();



        $('#idImgGood1').css('visibility', 'visible');
        $("#idImgGood1").show();
        $('#idImgGood2').css('display', 'none');
        $("#idImgGood2").hide();

        $('#idImgBad1').css('visibility', 'visible');
        $("#idImgBad1").show();
        $('#idImgBad2').css('display', 'none');
        $("#idImgBad2").hide();


    } else {
        $('#idImgRegular1').css('visibility', 'visible');
        $("#idImgRegular1").show();
        $('#idImgRegular2').css('display', 'none');
        $("#idImgRegular2").hide();
    }


    //Encondemos las vventanas
    $('#InfoRegular').toggle();
    $('#InfoBuena').hide();
    $('#InfoBad').hide();

    $("#idTxtArea").val("");
    $("#idTxtAreaBad").val("");

    $("input:radio").removeAttr("checked");

    $("#idTxtArea").prop('disabled', true);
    $("#idTxtAreaBad").prop('disabled', true);

})

$('#btnInfoBad').click(function () {
    LLamarOpcionSatisfaccion("InfoBad")

    if ($('#idImgBad1:visible').is(":visible")) {
        $('#idImgBad2').css('visibility', 'visible');
        $("#idImgBad2").show();
        $('#idImgBad1').css('display', 'none');
        $("#idImgBad1").hide();


        $('#idImgRegular1').css('visibility', 'visible');
        $("#idImgRegular1").show();
        $('#idImgRegular2').css('display', 'none');
        $("#idImgRegular2").hide();



        $('#idImgGood1').css('visibility', 'visible');
        $("#idImgGood1").show();
        $('#idImgGood2').css('display', 'none');
        $("#idImgGood2").hide();



    } else {
        $('#idImgBad1').css('visibility', 'visible');
        $("#idImgBad1").show();
        $('#idImgBad2').css('display', 'none');
        $("#idImgBad2").hide();

    }

    //Encondemos las vventanas
    $('#InfoBad').toggle();
    $('#InfoBuena').hide();
    $('#InfoRegular').hide();

    $("#idTxtArea").val("");
    $("#idTxtAreaRegular").val("");

    $("input:radio").removeAttr("checked");

    $("#idTxtArea").prop('disabled', true);
    $("#idTxtAreaRegular").prop('disabled', true);

})




//Feliz
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

$('#chkInfoBuenaEncuesta4').click(function () {
    if ($('#chkInfoBuenaEncuesta4').is(':checked')) {
        $("#idTxtArea").prop('disabled', false);
    }

});



//Regular
$('#chkInfoRegular1').click(function () {
    if ($('#chkInfoRegular1').is(':checked')) {
        $("#idTxtAreaRegular").prop('disabled', true);
        $("#idTxtAreaRegular").val("");
    }

});


$('#chkInfoRegular2').click(function () {
    if ($('#chkInfoRegular2').is(':checked')) {
        $("#idTxtAreaRegular").prop('disabled', true);
        $("#idTxtAreaRegular").val("");
    }

});


$('#chkInfoRegular3').click(function () {
    if ($('#chkInfoRegular3').is(':checked')) {
        $("#idTxtAreaRegular").prop('disabled', true);
        $("#idTxtAreaRegular").val("");
    }

});

$('#chkInfoRegular4').click(function () {
    if ($('#chkInfoRegular4').is(':checked')) {
        $("#idTxtAreaRegular").prop('disabled', false);
    }

});





//Bad
$('#chkInfoBad1').click(function () {
    if ($('#chkInfoBad1').is(':checked')) {
        $("#idTxtAreaBad").prop('disabled', true);
        $("#idTxtAreaBad").val("");
    }

});


$('#chkInfoBad2').click(function () {
    if ($('#chkInfoBad2').is(':checked')) {
        $("#idTxtAreaBad").prop('disabled', true);
        $("#idTxtAreaBad").val("");
    }

});


$('#chkInfoBad3').click(function () {
    if ($('#chkInfoBad3').is(':checked')) {
        $("#idTxtAreaBad").prop('disabled', true);
        $("#idTxtAreaBad").val("");
    }

});

$('#chkInfoBad4').click(function () {
    if ($('#chkInfoBad4').is(':checked')) {
        $("#idTxtAreaBad").prop('disabled', false);
    }

});





function LimpiarModal() {

    $("#idTxtArea").val("");
    $("#idTxtAreaRegular").val("");
    $("#idTxtAreaBad").val("");

    $("input:radio").removeAttr("checked");
    $('#InfoBuena').hide();
    $('#InfoRegular').hide();
    $('#InfoBad').hide();
}

function LimpiarCaritas() {
    $("#idImgGood1").show();
    $("#idImgRegular1").show();
    $("#idImgBad1").show();

    $('#idImgGood2').css('display', 'none');
    $('#idImgRegular2').css('display', 'none');
    $('#idImgBad2').css('display', 'none');

    $("#idImgGood2").hide();
    $("#idImgRegular2").hide();
    $("#idImgBad2").hide();
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
                $('#idInfoRegular4').text(res[3].T_DSC_ENCUESTA);


                $('#chkInfoRegular1').text(res[0].N_ID_ENCUESTA);
                $('#chkInfoRegular2').text(res[1].N_ID_ENCUESTA);
                $('#chkInfoRegular3').text(res[2].N_ID_ENCUESTA);
                $('#chkInfoRegular4').text(res[3].N_ID_ENCUESTA);
            }


            if (res[0].N_ID_PREGUNTA == 0) {
                $('#idInfoBad1').text(res[0].T_DSC_ENCUESTA);
                $('#idInfoBad2').text(res[1].T_DSC_ENCUESTA);
                $('#idInfoBad3').text(res[2].T_DSC_ENCUESTA);
                $('#idInfoBad4').text(res[3].T_DSC_ENCUESTA);


                $('#chkInfoBad1').text(res[0].N_ID_ENCUESTA);
                $('#chkInfoBad2').text(res[1].N_ID_ENCUESTA);
                $('#chkInfoBad3').text(res[2].N_ID_ENCUESTA);
                $('#chkInfoBad4').text(res[3].N_ID_ENCUESTA);
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

    if ($("#idTxtAreaRegular").val() == "" && $('input[id="chkInfoRegular4"]:checked').text() == "8") {
        $("#modalComentario").modal('show')
        return;
    }

    if ($("#idTxtAreaBad").val() == "" && $('input[id="chkInfoBad4"]:checked').text() == "12") {
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


                        $("#ModalEncuestaSatisfaccion").modal('hide')

                        $('#modalConfirmacion').modal('show');
                        setTimeout(function () {
                            $('#modalConfirmacion').modal('hide');
                        }, 3000);


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
        if ($('#chkInfoRegular1').is(':checked') || $('#chkInfoRegular2').is(':checked') || $('#chkInfoRegular3').is(':checked') || $('#chkInfoRegular4').is(':checked')) {
            if (ValorCarita == 2) {

                var captura = new function () {
                    this.ValorCarita = ValorCarita;
                    this.chkInfoRegular1 = $('input[id="chkInfoRegular1"]:checked').text()
                    this.chkInfoRegular2 = $('input[id="chkInfoRegular2"]:checked').text()
                    this.chkInfoRegular3 = $('input[id="chkInfoRegular3"]:checked').text()
                    this.chkInfoRegular4 = $('input[id="chkInfoRegular4"]:checked').text()
                    this.idTxtAreaRegular = $('#idTxtAreaRegular').val()

                };
                strSerializado = JSON.stringify(captura);



                $.ajax({
                    type: "POST",
                    url: "CalculadoraIMSS.aspx/InsertarEncuestaSatisfaccionRegular",
                    contentType: "application/json;charset=utf-8",
                    data: strSerializado,
                    dataType: "json",
                    success: function (data) {

                        $("#ModalEncuestaSatisfaccion").modal('hide')

                        $('#modalConfirmacion').modal('show');
                        setTimeout(function () {
                            $('#modalConfirmacion').modal('hide');
                        }, 3000);

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
        if ($('#chkInfoBad1').is(':checked') || $('#chkInfoBad2').is(':checked') || $('#chkInfoBad3').is(':checked') || $('#chkInfoBad4').is(':checked')) {
            if (ValorCarita == 0) {
                var captura = new function () {
                    this.ValorCarita = ValorCarita;
                    this.chkInfoBad1 = $('input[id="chkInfoBad1"]:checked').text()
                    this.chkInfoBad2 = $('input[id="chkInfoBad2"]:checked').text()
                    this.chkInfoBad3 = $('input[id="chkInfoBad3"]:checked').text()
                    this.chkInfoBad4 = $('input[id="chkInfoBad4"]:checked').text()
                    this.idTxtAreaBad = $('#idTxtAreaBad').val()

                };
                strSerializado = JSON.stringify(captura);


                $.ajax({
                    type: "POST",
                    url: "CalculadoraIMSS.aspx/InsertarEncuestaSatisfaccionBad",
                    contentType: "application/json;charset=utf-8",
                    data: strSerializado,
                    dataType: "json",
                    success: function (data) {

                        $("#ModalEncuestaSatisfaccion").modal('hide')


                        $('#modalConfirmacion').modal('show');
                        setTimeout(function () {
                            $('#modalConfirmacion').modal('hide');
                        }, 3000);

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



    //alert(strSerializado)



}
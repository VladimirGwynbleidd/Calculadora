<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalculadoraIMSS.aspx.cs" Inherits="CALCULADORA2014.Index" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link href="https://framework-gb.cdn.gob.mx/favicon.ico" rel="shortcut icon" />
    <link href="https://framework-gb.cdn.gob.mx/assets/styles/main.css" rel="stylesheet" />
    <link href="https://www.gob.mx/cms/application.css" rel="stylesheet" />
    <!-- Bootstrap Font Icon CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.5.0/font/bootstrap-icons.css" />
    <title>Calculadora IMSS</title>
    <!-- INICIO GENERAL -->
    <link href="css/Style.css" rel="stylesheet" />

    <%--  <script src="//code.jquery.com/jquery-1.10.2.js"></script>--%>
    <link href="tooltipster-master/css/tooltipster.css" rel="stylesheet" />
    <script src="js/jquery-1.7.2.min.js"></script>
    <script src="tooltipster-master/js/jquery.tooltipster.min.js"></script>
    <script src="ContentScripts/bootstrap.4.5.0/content/Scripts/bootstrap.js"></script>

    <!-- FIN GENERAL -->

    <script>
        $(window).load(function () {
            $('#cargando').hide();
        });
    </script>

    <!-- INICIO VALIDACION -->
    <link href="css/validate/validationEngine.jquery.css" rel="stylesheet" />
    <link href="css/validate/template.css" rel="stylesheet" />
    <script src="js/validate/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="js/validate/languages/jquery.validationEngine-es.js"></script>
    <!-- FIN VALIDACION -->

    <!-- CSS Notify -->
    <%--<link href="notifyme-master/assets/css/base.css" rel="stylesheet" />--%>
    <link href="notifyme-master/assets/css/notifyme.css" rel="stylesheet" />

    <!-- INICIO FORMATO MONEDA -->
    <script src="jquery.formatCurrency-1.4.0/jquery.formatCurrency-1.4.0.js"></script>
    <script src="js/moneda.js"></script>
    <!-- FIN FORMATO MONEDA -->



    <!-- INICIO SLIDERS -->
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/base/jquery-ui.css" />
    <link href="css/Sliders.css" rel="stylesheet" />
    <%--    <script src="//code.jquery.com/ui/1.11.1/jquery-ui.js"></script>--%>
    <script src="js/jquery-ui.js"></script>
    <script src="js/sliders.js"></script>
    <!-- FIN SLIDERS -->


    <!-- INICIO MENSAJE EMERGENTE INFORMACION -->
    <link href="css/Popup.css" rel="stylesheet" />
    <script src="js/mensaje/main.js"></script>
    <script src="js/mensaje/shCore.js"></script>
    <script src="js/mensaje/shBrushJScript.js"></script>
    <%--  <script>        $(function () { dp.SyntaxHighlighter.HighlightAll('code'); });</script>--%>
    <script src="js/mensaje/popup.js"></script>
    <!-- FIN MENSAJE EMERGENTE INFORMACION -->

    <!--- INICIO MASCARAS -->

    <%--  <link rel="stylesheet" href="http://code.jquery.com/qunit/qunit-1.11.0.css" type="text/css" media="all">--%>

    <script src="js/jquery.maskedinput.min.js"></script>

    <script src="js/jquery.placeholder.js"></script>

    <!-- FIN MASCARAS -->

    <script src="js/webmethods.js"></script>
    <script src="js/eventos.js"></script>
    <link href="css/Calendar.css" rel="stylesheet" />
    <script src="js/Chart.min.js"></script>

    <%--    <script src="js/knockout-3.0.0.js"></script>
    <script src="js/globalize.min.js"></script>
    <script src="js/dx.chartjs.js"></script>--%>

    <script src="js/ModalJS/ModalEncuesta.js"></script>
    <script>
        function ValidNum(e) {
            var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
            return ((tecla > 47 && tecla < 58) || tecla == 46 || tecla == 8 || tecla == 9 || tecla == 37 || tecla == 39);
        }

    </script>

    <script type="text/javascript">
        jQuery(document).ready(function () {
            // binds form submission and fields to the validation engine
            jQuery("#formID").validationEngine();

            //2015
            function disableselect(e) {
                if (omitformtags.indexOf(e.target.tagName.toLowerCase()) == -1)
                    return false
            }

            function reEnable() {
                return true
            }

            if (typeof document.onselectstart != "undefined")
                document.onselectstart = new Function("return false")
            else {
                document.onmousedown = disableselect
                document.onmouseup = reEnable
            }

            function disableSelection(target) {
                if (typeof target.onselectstart != "undefined") //IE route
                    target.onselectstart = function () { return false }
                else if (typeof target.style.MozUserSelect != "undefined") //Firefox route
                    target.style.MozUserSelect = "none"
                else //All other route (ie: Opera)
                    target.onmousedown = function () { return false }
                target.style.cursor = "default"
            }


            $("#slider-txtRendimientoReralAntesComisiones").slider({
                animate: true
            });

            $("#slider-txtEdadRetiro").slider({
                animate: true
            });

            $("#slider-txtSalarioBaseCotizacion").slider({
                animate: true
            });

            $("#slider-txtAniosCotizados").slider({
                animate: true
            });

        });

        $(function () {
            // Invoke the plugin
            $('input, textarea').placeholder();
        });

        jQuery(function ($) {
            $("#txtFechaNacimiento").mask("99/99/9999", { placeholder: "dd/mm/aaaa" });
        });

        function limpiarLabels() {
            $("#lblGHAhorro").text("");
            $("#lblGHEdadRetiro").text("");
            $("#lblGHEdadRetiro2").text("");
            $("#lblGHPensionRetiro").text("");
            $("#lblGHEdadRetiro3").text("");
            $("#lblGHPensionRetiro3").text("");
            $('#lblP4Saldo').text("");
            $('#lblSaldoAcumulado').text("");
            $('#lblSalarioBaseCotizacionMensual').text("");
            $('#lblSaldoActualAfore').text("");
            $('#lblEdad').text("");
            $('#lblEdadRetiro').text("");
            $('#lblAniosCotizados').text("");
            $('#lblRendimientoRealAntesComisiones').text("");
            $('#lblDensidadCotizacion').text("");
            $('#lblSalarioBaseCotizacionMensualNP').text("");
            $('#lblSaldoActualAforeNP').text("");
            $('#lblEdadNP').text("");
            $('#lblEdadRetiroNP').text("");
            $('#lblAniosCotizadosNP').text("");
            $('#lblRendimientoRealAntesComisionesNP').text("");
            $('#lblDensidadCotizacionNP').text("");
            $('#lblPensionMensualEstimada').text("");
            $('#lblPorcentajePensionMensual').text("");
            $('#lblSaldoActual').text("");
            $('#lblRendimientosFuturos').text("");
            $('#lblSaldoAcumuladoNP').text("");
            $('#lblSaldoActualNP').text("");
            $('#lblRendimientoFuturoNP').text("");
            $('#lblP2PME').text("");

            //PGO Agregar etiqueta para copiar la pensión mensual estimada
            $('#lblP2PMECopia').text("");
            $('#HdfSaldoFinalCompleto').text("");


            $('#lblP2Edad').text("");
            $('#lblP2SaldoPensionario').text("");
            $('#lblP2Deficit').text("");
            $('#lblMejorarPension').text("");
            $('#lblP4Pension').text("");
            $('#lblCig').text("");
            $('#lblCigPza').text("");
            $('#lblCer').text("");
            $('#lblCerPza').text("");
            $('#lblCom').text("");
            $('#lblComPza').text("");
            $('#lblCaf').text("");
            $('#lblCafPza').text("");
            $('#lblCin').text("");
            $('#lblCinPza').text("");
        }
        function valida_salario_cero(field, rules, i, options) {

            var psSalarioBaseCotizacionMensual = field.val();
            psSalarioBaseCotizacionMensual = psSalarioBaseCotizacionMensual.replace("$", "");
            psSalarioBaseCotizacionMensual = psSalarioBaseCotizacionMensual.replace(",", "").replace(",", "").replace(",", "");
            var pSalarioBaseCotizacionMensual = psSalarioBaseCotizacionMensual;
            //alert(pSalarioBaseCotizacionMensual);
            //var valor = 0;
            //valor = field.val();
            //var str = valor;
            //var n = str.replace("$", "");
            //n = n.replace(",", "");
            if (pSalarioBaseCotizacionMensual <= 0) {
                // this allows to use i18 for the error msgs
                //                return options.allrules.validate2fields.alertText;

                //RARS - Se agrega focus
                if ($('#hdfCampoFocus').val() == '' || $('#hdfCampoFocus').val() == 'txtSalarioBaseCotizacion') {
                    document.getElementById("txtSalarioBaseCotizacion").focus();
                    $("#hdfCampoFocus").val('txtSalarioBaseCotizacion');
                    $('#hdfCountFocus').val(parseInt($('#hdfCountFocus').val()) + 1);
                }

                return 'Por favor, introduce tu Salario Base de cotización Mensual.';
            }

            if ($('#hdfCampoFocus').val() == 'txtSalarioBaseCotizacion') {
                $("#hdfCampoFocus").val('');
            }
        }

        //Leer de la tabla de Parametros
        var lInferior = "<%=LInferior %>";
        var lSuperior = "<%=LSuperior %>";
        var lCigarros = "<%=Cigarros %>";
        var lCerveza = "<%=Cerveza %>";
        var lFastFood = "<%=FastFood %>";
        var lCafe = "<%=Cafe %>";
        var lCine = "<%=Cine %>";

        //PGO ODT-07
        var FechaCambio1 = "<%=Fechcambio1 %>";
        var FechaCambio2 = "<%=Fechcambio2 %>";
        var AportObligatoria = "<%=AOIMensual %>";



        function valida_salario_minimo(field, rules, i, options) {

            //PGO Salario de cotizacion 
            var SMGVDF = "<%=SMGVDF%>";  // RARS
            var SMGVZB = "<%=SMGVZB%>";  // RARS
            var Val_UMA = "<%=Val_UMA%>";  // RARS

            //var valor = 0;
            //valor = field.val();
            var psSalarioBaseCotizacionMensual = field.val();
            psSalarioBaseCotizacionMensual = psSalarioBaseCotizacionMensual.replace("$", "");
            psSalarioBaseCotizacionMensual = psSalarioBaseCotizacionMensual.replace(",", "").replace(",", "").replace(",", "");
            var pSalarioBaseCotizacionMensual = psSalarioBaseCotizacionMensual;
            //alert(pSalarioBaseCotizacionMensual);

            //var tope = Math.round((25 * (Val_UMA * 30) + 0.5));
            //var minimo = (SMGVDF * 30);

            var tope = Math.round((30.4) * (25 * Val_UMA));
            var minimo = (30.4 * SMGVDF);

            //var str = valor;
            //var n = str.replace("$", "");
            //n = n.replace(",", "");

            var minimook = parseFloat(minimo.toFixed(2));

            if (pSalarioBaseCotizacionMensual > tope || pSalarioBaseCotizacionMensual < minimook) {

                // this allows to use i18 for the error msgs
                //                return options.allrules.validate2fields.alertText;

                //RARS - Se agrega focus
                if ($('#hdfCampoFocus').val() == '' || $('#hdfCampoFocus').val() == 'txtSalarioBaseCotizacion') {
                    document.getElementById("txtSalarioBaseCotizacion").focus();
                    $("#hdfCampoFocus").val('txtSalarioBaseCotizacion');
                    $('#hdfCountFocus').val(parseInt($('#hdfCountFocus').val()) + 1);
                }

                //document.getElementById("txtSalarioBaseCotizacion").focus();
                //document.getElementById("txtSalarioBaseCotizacion").value = "";

                return "Esta cantidad debe estar entre 1 salario mínimo y 25 veces el valor de la UMA.";

            }

            $("#hdfCampoFocus").val('')
        }

        function valida_saldo_cero(field, rules, i, options) {

            var psSaldoActualAfore = document.getElementById('txtSaldoActualAfore').value;
            psSaldoActualAfore = psSaldoActualAfore.replace("$", "");
            psSaldoActualAfore = psSaldoActualAfore.replace(",", "");
            var pSaldoActualAfore = psSaldoActualAfore;

            //var valor = 0;
            //valor = field.val();
            if (pSaldoActualAfore < 0 || pSaldoActualAfore == '') {
                // this allows to use i18 for the error msgs
                //                return options.allrules.validate2fields.alertText;

                //RARS - Se agrega focus
                if ($('#hdfCampoFocus').val() == '' || $('#hdfCampoFocus').val() == 'txtSaldoActualAfore') {
                    document.getElementById("txtSaldoActualAfore").focus();
                    $("#hdfCampoFocus").val('txtSaldoActualAfore')
                    $('#hdfCountFocus').val(parseInt($('#hdfCountFocus').val()) + 1);
                }

                return "Por favor, introduce el Saldo Actual en tu Afore.";
            }

            $("#hdfCampoFocus").val('')
        }


        function valida_fecha_vacio(field, rules, i, options) {
            if (parseInt($('#hdfCountFocus').val()) >= 10) {
                $('#hdfCountFocus').val(0);
                //event.stopPropagation();
            }
            $('#hdfCountFocus').val(parseInt($('#hdfCountFocus').val()) + 1);
            if ($('#hdfCampoFocus').val() == '' || $('#hdfCampoFocus').val() == 'txtFechaNacimiento') {
                var mensaje = ValidaFechaNacimiento();

                if (mensaje.length > 0) {
                    return mensaje
                }

                //AniosCotizados();
            }
        }

        function valida_anio_afiliacion(field, rules, i, options) {
            if ($('#hdfCampoFocus').val() == '' || $('#hdfCampoFocus').val() == 'txtAnioAfiliacion') {
                return validaAfiliacion();
            }
        }

        //AJN - ODT -07
        function valida_semanas_cotizadas(field, rules, i, options) {
            if ($('#hdfCampoFocus').val() == '' || $('#hdfCampoFocus').val() == 'txtSemanasCotizadas') {
                return validaSemanasCotizadas();
            }
        }

        function validaAfiliacion() {

            var anioAfiliacion = parseInt($('#txtAnioAfiliacion').val());
            var currentTime = new Date();
            var currentYear = currentTime.getFullYear();
            var aniosCotizados = parseInt($('#txtSemanasCotizadas').val());

            var strfechaNacimiento = $('#txtFechaNacimiento').val();
            var valoresfecha = strfechaNacimiento.split('/');
            var fechaNacimiento = new Date(parseInt(valoresfecha[2]), parseInt(valoresfecha[1]), parseInt(valoresfecha[0]), 0, 0, 0, 0);
            var anioNacimiento = fechaNacimiento.getFullYear();

            //if (currentYear - anioAfiliacion < aniosCotizados) {
            //    if ($('#hdfCampoFocus').val() == '' || $('#hdfCampoFocus').val() == 'txtAnioAfiliacion') {
            //        document.getElementById("txtAnioAfiliacion").focus();
            //        $("#hdfCampoFocus").val('txtAnioAfiliacion')
            //        $('#hdfCountFocus').val(parseInt($('#hdfCountFocus').val()) + 1);
            //    }

            //    return "La diferencia en años, entre el año actual y el año de afiliación debe ser mayor o igual a lo que se reporte en el campo de años cotizados.";
            //}

            if (anioNacimiento + 15 > anioAfiliacion) {
                if ($('#hdfCampoFocus').val() == '' || $('#hdfCampoFocus').val() == 'txtAnioAfiliacion') {
                    document.getElementById("txtAnioAfiliacion").focus();
                    $("#hdfCampoFocus").val('txtAnioAfiliacion');
                    $('#hdfCountFocus').val(parseInt($('#hdfCountFocus').val()) + 1);
                }
                document.getElementById("txtAnioAfiliacion").value = "";
                document.getElementById("txtAnioAfiliacion").focus();
                document.getElementById("txtSemanasCotizadas").value = "";
                return "El año de afiliación debe ser mayor o igual al año de nacimiento más 15.";
            }
            else if (anioAfiliacion > currentYear) {

                document.getElementById("txtAnioAfiliacion").value = "";
                document.getElementById("txtAnioAfiliacion").focus();
                document.getElementById("txtSemanasCotizadas").value = "";
                $('#hdfCountFocus').val(parseInt($('#hdfCountFocus').val()) + 1);
                return "El año de afiliación debe ser menor o igual al año actual.";

            } else {
                $("#txtSemanasCotizadas").removeAttr('disabled');
            }

            if ($('#hdfCampoFocus').val() == 'txtAnioAfiliacion') {
                $("#hdfCampoFocus").val('');
            }

            if ($('#hdfAfiliacion').val() != $('#txtAnioAfiliacion').val()) {
                AniosCotizados(1);
            }
            $('#hdfAfiliacion').val($('#txtAnioAfiliacion').val());



        }

        //AJN - ODT -07
        function validaSemanasCotizadas() {

            var semanasCotizadas = $('#txtSemanasCotizadas').val();
            semanasCotizadas = semanasCotizadas.replace(',', '');
            semanasCotizadas = parseInt(semanasCotizadas);
            var anioAfiliacion = parseInt($('#txtAnioAfiliacion').val());
            var fechaCalculo = Date.now();
            var primerDiaAnioActual = new Date(new Date().getFullYear(), 0, 1);
            var semanasAlCorte = semanasTranscurridas(primerDiaAnioActual, fechaCalculo);
            var anioActual = new Date().getFullYear();
            var maxSemanasCotizadas = (anioActual - anioAfiliacion) * 52
            maxSemanasCotizadas = maxSemanasCotizadas + semanasAlCorte;

            console.log(semanasAlCorte)
            //if (!isNaN(semanasCotizadas)) {
            //    if (semanasCotizadas == 0) {
            //        if ($('#hdfCampoFocus').val() == '' || $('#hdfCampoFocus').val() == 'txtSemanasCotizadas') {
            //            document.getElementById("txtSemanasCotizadas").focus();
            //            $("#hdfCampoFocus").val('txtSemanasCotizadas');
            //            $('#hdfCountFocus').val(parseInt($('#hdfCountFocus').val()) + 1);
            //        }
            //        document.getElementById("txtSemanasCotizadas").value = "";
            //        document.getElementById("txtSemanasCotizadas").focus();
            //        return "Las semanas cotizadas deben ser mayores a cero.";
            //    } else {                 
            //            if (semanasCotizadas > maxSemanasCotizadas) {
            //                document.getElementById("txtSemanasCotizadas").value = "";
            //                $("#hdfCampoFocus").val('txtSemanasCotizadas');
            //                $('#hdfCountFocus').val(parseInt($('#hdfCountFocus').val()) + 1);
            //                return "El numero de semanas cotizadas debe ser menor a " + maxSemanasCotizadas + ".";                          
            //            }
            //    }
            //}

            if (!isNaN(semanasCotizadas)) {

                if (semanasCotizadas > maxSemanasCotizadas) {
                    document.getElementById("txtSemanasCotizadas").value = "";
                    $("#hdfCampoFocus").val('txtSemanasCotizadas');
                    $('#hdfCountFocus').val(parseInt($('#hdfCountFocus').val()) + 1);
                    return "El numero de semanas cotizadas debe ser menor a " + maxSemanasCotizadas + ".";
                }

            }

            if ($('#hdfCampoFocus').val() == 'txtSemanasCotizadas') {
                $("#hdfCampoFocus").val('');
            }
            $('#hdfAniosCotizados').val($('#txtSemanasCotizadas').val());
            //if ($('#hdfAniosCotizados').val() != $('#txtSemanasCotizadas').val()) {
            //    AniosCotizados(1);
            //}
        }


        function semanasTranscurridas(d1, d2) {
            //AJN - ODT -07 29112021
            //return Math.round((d2 - d1) / (7 * 24 * 60 * 60 * 1000));   Version 1
            return Math.trunc((d2 - d1) / (7 * 24 * 60 * 60 * 1000)); //  
        }

        function ValidaFecha() {
            var pFechaNacimiento = document.getElementById('txtFechaNacimiento').value;

            //var valor = 0;
            //valor = field.val();


            if (pFechaNacimiento == "" || pFechaNacimiento == '__/__/____') {
                // this allows to use i18 for the error msgs
                //                return options.allrules.validate2fields.alertText;

                //RARS - Se agrega focus
                if ($('#hdfCampoFocus').val() == '' || $('#hdfCampoFocus').val() == 'txtFechaNacimiento') {
                    document.getElementById("txtFechaNacimiento").focus();
                    $("#hdfCampoFocus").val('txtFechaNacimiento')
                }

                return "La fecha de nacimiento ingresada no es válida.";
            }
            else {
                var valoresfecha = pFechaNacimiento.split('/');

                if (parseFloat(valoresfecha[0]) > 31 || parseFloat(valoresfecha[0]) < 1) {
                    //RARS - Se agrega focus
                    if ($('#hdfCampoFocus').val() == '' || $('#hdfCampoFocus').val() == 'txtFechaNacimiento') {
                        document.getElementById("txtFechaNacimiento").focus();
                        $("#hdfCampoFocus").val('txtFechaNacimiento')
                    }

                    return "La fecha de nacimiento ingresada no es válida.";
                }
                if (parseFloat(valoresfecha[1]) > 12 || parseFloat(valoresfecha[1]) < 1) {
                    //RARS - Se agrega focus
                    if ($('#hdfCampoFocus').val() == '' || $('#hdfCampoFocus').val() == 'txtFechaNacimiento') {
                        document.getElementById("txtFechaNacimiento").focus();
                        $("#hdfCampoFocus").val('txtFechaNacimiento')
                    }

                    return "La fecha de nacimiento ingresada no es válida.";
                }
                var d = new Date();
                var n = d.getFullYear();

                //  if (parseFloat(valoresfecha[2]) > (n - 14) || parseFloat(valoresfecha[2]) < (n - 100)) { return "Debes tener 14 años como mínimo y hasta 100 años como máximo."; }
                if (parseFloat(valoresfecha[2]) > (n - 15) || parseFloat(valoresfecha[2]) < (n - 68)) {
                    document.getElementById("txtFechaNacimiento").focus();
                    return "Para poder realizar una estimación de pensión, debes tener mínimo 15 años y como máximo 67 años.";
                }


                $("#hdfCampoFocus").val('')

                return ''

                //var fechaGeneracion = new Date(1983, 7, 2, 0, 0, 0, 0);
                //var fechaNacimiento = new Date(parseInt(valoresfecha[2]), parseInt(valoresfecha[1]), parseInt(valoresfecha[0]), 0, 0, 0, 0);
                //if (fechaNacimiento <= fechaGeneracion && $('#hdfGeneracionTransicion').val() != fechaNacimiento) {
                //    $('#hdfGeneracionTransicion').val(fechaNacimiento);
                //    //return "Generacion de transicion.";
                //    $('#aGeneracionTransicion').trigger('click');
                //}
            }

        }

        function ValidaFechaNacimiento() {

            var strFecha = $('#txtFechaNacimiento').val();
            var strMensaje = '';

            var captura = new function () {
                this.strFecha = strFecha;
            };
            var strSerializado = JSON.stringify(captura);

            $.ajax
            ({
                type: "POST",
                url: "CalculadoraIMSS.aspx/ValidaFecha",
                dataType: "json",
                data: strSerializado,
                async: false,
                contentType: "application/json; charset=utf-8",
                error: function (jqXHR, textStatus, errorThrown)   //what to do if fails
                {
                    alert('Error: ' + errorThrown + ", " + jqXHR.responseText + ", " + textStatus);
                },
                success: function (data)           //what to do if succedded
                {
                    var res = $.parseJSON(data.d);

                    if (!res.Valida) {
                        if ($('#hdfCampoFocus').val() == '' || $('#hdfCampoFocus').val() == 'txtFechaNacimiento') {
                            document.getElementById("txtFechaNacimiento").focus();
                            $("#hdfCampoFocus").val('txtFechaNacimiento');
                            $('#hdfCountFocus').val(parseInt($('#hdfCountFocus').val()) + 1);
                        }
                        $('#txtEdad').val('');
                        $('#txtAnioAfiliacion').val('');
                        AniosCotizados();
                        strMensaje = res.MensajeError;
                    }
                    else {
                        if ($('#hdfCampoFocus').val() == 'txtFechaNacimiento') {
                            $("#hdfCampoFocus").val('');
                        }
                        strMensaje = '';
                        $('#txtEdad').val(res.Edad);

                        if ($('#hdfNacimiento').val() != $('#txtFechaNacimiento').val()) {
                            $('#txtAnioAfiliacion').val('');
                            $('#hdfAfiliacion').val('');
                            AniosCotizados();
                        }
                        $('#hdfNacimiento').val($('#txtFechaNacimiento').val());
                    }

                }
            });

            return strMensaje;
        }

        function AniosCotizados(opc) {
            //$(function () {
            var Edad = parseInt($('#txtEdad').val());
            var anioAfliacion = parseInt($('#txtAnioAfiliacion').val());
            var maxAniosCotizados = 0;
            var maxSemanasCotizadas = 0
            var minAniosCotizados = 0;

            var d = new Date();
            var n = d.getFullYear();

            if (!isNaN(Edad) && !isNaN(anioAfliacion)) {
                maxAniosCotizados = Edad - 14;
                maxSemanasCotizadas = maxAniosCotizados * 52

                if (n - anioAfliacion < maxAniosCotizados) {
                    maxAniosCotizados = n - anioAfliacion;
                    minAniosCotizados = 0;
                    maxSemanasCotizadas = maxAniosCotizados * 52
                }
            }

            if ($('#txtAniosSel').val() > 0) {
                minAniosCotizados = $('#txtAniosSel').val();
            } else {
                minAniosCotizados = 0;
            }

            if (opc == 1) {
                minAniosCotizados = 0;
                $("#txtSemanasCotizadas").val("");
            }

            //$("#slider-txtAniosCotizados").slider({
            //    range: "min",
            //    value: minAniosCotizados,
            //    min: 0,
            //    max: maxSemanasCotizadas,
            //    step: 1,
            //    slide: function (event, ui) {
            //        $("#txtSemanasCotizadas").val(ui.value);
            //        $("#HiddenField1").val(ui.value);
            //        $('#txtAniosSel').val(ui.value);
            //    },
            //    animate: true
            //});

            //$("#txtSemanasCotizadas").val($("#slider-txtAniosCotizados").slider("value"));
            //});
        }

    </script>

    <!-- Inicio Gasto Hormiga-->
    <script>
        var formatNumber = function (amount, decimales, separator) {
            var sep = separator || ",";
            var decimal = decimales == undefined ? 2 : decimales;
            // Allow the user to round a float to a whole number:
            if (decimal === 0) {
                var num = parseInt(amount);
                return Number(num).toString().replace(/(?=(?:\d{3})+$)(?!^)/g, sep);
            }
            if (decimal === undefined) {
                // Check if amount is a float:
                if (typeof amount === 'number' && amount % 1 !== 0) {
                    return Number(amount).toString().replace(/\d(?=(\d{3})+\.)/g, '$&' + sep);
                    // Otherwise treat it as an integer:
                } else {
                    return Number(amount).toString().replace(/(?=(?:\d{3})+$)(?!^)/g, sep);
                }
                // If a decimal value was provided,
                // format it to that amount:
            } else {
                return Number(amount).toFixed(decimal).replace(/\d(?=(\d{3})+\.)/g, '$&' + sep);

            }
        };

        function GastoHormiga(item, PUnit, fnTotal) {
            this.getTotal = function () { return miTotal };
            this.reset = function () {
                Pza.text("0");
                Precio.text("$0");
                miTotal = 0;
            }

            var grp = $("#" + item);
            var Precio = grp.find("span");
            var Der = grp.find(".CDer");
            var Izq = grp.find(".CIzq");
            var Pza = grp.find(".CCen");
            var miTotal = 0;

            Der.click(unoMas);
            Izq.click(unoMenos);
            this.reset();

            grp = null;

            function Calcular() {
                var t = parseInt("0" + Pza.text()) * PUnit;
                Precio.text("$" + formatNumber(t, 0));
                miTotal = t;
                fnTotal();
            };

            function unoMas() {
                var t = parseInt("0" + Pza.text());
                if (t <= 29) {
                    t = t + 1;
                    Pza.text(t);
                    Calcular();
                }
            };

            function unoMenos() {
                var t = parseInt("0" + Pza.text());
                if (t > 0) {
                    t = t - 1;
                    Pza.text(t);
                    Calcular();
                }
            };
        };


        var items = null;

        function cfgGastoHormiga() {
            if (items == null) {
                items = [, , , , ];
                items[0] = new GastoHormiga("divCigarros", lCigarros, Total);
                items[1] = new GastoHormiga("divCerveza", lCerveza, Total);
                items[2] = new GastoHormiga("divFastFood", lFastFood, Total);
                items[3] = new GastoHormiga("divCafe", lCafe, Total);
                items[4] = new GastoHormiga("divCine", lCine, Total);
            }
            else {
                items[0].reset();
                items[1].reset();
                items[2].reset();
                items[3].reset();
                items[4].reset();
            }
            $("#lblGHAhorro").text(0);

            Total();
        }

        function Total() {
            var totalItems = 0;
            for (i = 0; i < items.length; i++)
                totalItems += items[i].getTotal();

            $("#lblGHAhorro").text(Math.round(totalItems).formatMoney(0, "$", ",", "."));

            if (totalItems == 0) {
                $("#lblGHEdadRetiro").text("$ " + formatNumber(0, 0));
                $("#lblGHEdadRetiro2").text("$ " + formatNumber(0, 0));
                $("#lblGHEdadRetiro3").text("$ " + formatNumber(0, 0));
                $("#lblGHPensionRetiro").text($('#lblP4Pension').text().replace("$", "$ "));
                //$("#lblGHPensionRetiro3").text($('#lblP4Saldo').text());
                //$("#lblGHPensionRetiro3").text($('#lblP4pSaldoActualAfore').text());
                var saldofinal = $('#HdfSaldoFinalCompleto').text();
                var saldofinal2 = parseFloat(saldofinal.replace("$", ""));
                $("#lblGHPensionRetiro3").text("$ " + formatNumber(parseFloat(Math.round(saldofinal2)), 0));

            } else {

                //Sye alejandro.guevara
                var tRendimientoMensual2 = tRendimientoMensual.toFixed(5);
                var qwerty = parseFloat(tRendimientoMensual2);
                var nvo = parseFloat(Anexo1.d) * parseInt(totalItems) * (((Math.pow(1 + tRendimientoMensual, Anexo1.n)) - 1) / tRendimientoMensual); //saldo estimado a la Edad de retiro

                $("#lblGHEdadRetiro").text("$ " + formatNumber(parseFloat(Math.round(nvo)), 0));
                $("#lblGHEdadRetiro2").text("$ " + formatNumber(parseFloat(Math.round(nvo)), 0));
                $("#lblGHEdadRetiro3").text("$ " + formatNumber(parseFloat(Math.round(nvo)), 0));

                //PGO 03032022
                var nvo3 = eval(parseFloat($('#HdfSaldoFinalCompleto').text().replace("$", "").replaceAll(",", "")) + nvo);    // Pension con el ahorro
                nvo = eval(parseFloat($('#lblSaldoAcumulado').text().replace("$", "").replaceAll(",", "")) + nvo);    // Pension con el ahorro

                //var auxMC = 12 * parseFloat(Anexo1.URV);
                var auxMC = (12 * parseFloat(Anexo1.URV) * (1 + 0.02));
                nvo = nvo / auxMC;

                //$("#lblGHPensionRetiro").text("$ " + formatNumber(parseFloat(Math.round(1.0)), 0));
                $("#lblGHPensionRetiro").text("$ " + formatNumber(parseFloat(Math.round(nvo)), 0));
                $("#lblGHPensionRetiro3").text("$ " + formatNumber(parseFloat(Math.round(nvo3)), 0));
            }
        }
    </script>
    <!-- Fin Gasto Hormifa-->
    <link href="colorbox-master/example4/colorbox.css" rel="stylesheet" />

    <script src="colorbox-master/jquery.colorbox.js"></script>
    <script>
        $(document).ready(function () {
            //Examples of how to assign the Colorbox event to elements
            $(".group1").colorbox({ rel: 'group1' });
            $(".group2").colorbox({ rel: 'group2', transition: "fade" });
            $(".group3").colorbox({ rel: 'group3', transition: "none", width: "75%", height: "75%" });
            $(".group4").colorbox({ rel: 'group4', slideshow: true });
            $(".ajax").colorbox();
            $(".youtube").colorbox({ iframe: true, innerWidth: 640, innerHeight: 390 });
            $(".vimeo").colorbox({ iframe: true, innerWidth: 500, innerHeight: 409 });
            $(".iframe").colorbox({ iframe: true, width: "80%", height: "80%" });
            $(".inline").colorbox({ inline: true, width: "40%" });
            $(".callbacks").colorbox({
                onOpen: function () { alert('onOpen: colorbox is about to open'); },
                onLoad: function () { alert('onLoad: colorbox has started to load the targeted content'); },
                onComplete: function () { alert('onComplete: colorbox has displayed the loaded content'); },
                onCleanup: function () { alert('onCleanup: colorbox has begun the close process'); },
                onClosed: function () { alert('onClosed: colorbox has completely closed'); }
            });

            $('.non-retina').colorbox({ rel: 'group5', transition: 'none' })
            $('.retina').colorbox({ rel: 'group5', transition: 'none', retinaImage: true, retinaUrl: true });

            //Example of preserving a JavaScript event for inline calls.
            $("#click").click(function () {
                $('#click').css({ "background-color": "#f00", "color": "#fff", "cursor": "inherit" }).text("Open this window again and this message will still be here.");
                return false;
            });
        });
    </script>

    <style>
        body {
            /*background-image: url(imagenes/fondo.gif);*/
            background-color: #FFFFFF;
            -webkit-user-select: none;
            -moz-user-select: -moz-none;
            -ms-user-select: none;
            user-select: none;
        }

        canvas {
            background-color: white;
        }

        .AlineaBtnOpciones {
            padding-left: 25px;
            padding-right: 25px;
        }
    </style>
    <script type="text/javascript">
        var tipoCaso = "";

        function regresarResumen() {
            //document.getElementById('DVTemasImportanciaDesplegableIMSS').style.display = 'none';
            $("#divResumenAll").removeClass("panelOculto").addClass("panelVisible");
            $("#divP2").removeClass("panelVisible").addClass("panelOculto");
            $("#divP3").removeClass("panelVisible").addClass("panelOculto");
            $("#divP4").removeClass("panelVisible").addClass("panelOculto");
        }
        function verP2() {
            $("#divResumenAll").removeClass("panelVisible").addClass("panelOculto");
            $("#divP2").removeClass("panelOculto").addClass("panelVisible");
            Grafica2();
        }
        function verP3() {
            $("#divResumenAll").removeClass("panelVisible").addClass("panelOculto");
            $("#divP3").removeClass("panelOculto").addClass("panelVisible");
        }
        function verP4() {

            $("#divP4 .panelVisible").removeClass("panelVisible");
            $("#divP4 .panelOculto").removeClass("panelOculto");

            //alert(tipoCaso);

            if (tipoCaso == 'NP') {
                $("#trInicio").addClass("panelVisible").addClass("panelOculto");
                $("#divP4Resumen").removeClass("panelOculto").addClass("panelVisible");
                $("#divP4ResumenSaldo").removeClass("panelOculto").addClass("panelVisible");
                $("#divP4ResumenPension").removeClass("panelVisible").addClass("panelOculto");

                $("#GHInvertir1").removeClass("panelVisible").addClass("panelOculto");
                $("#GHInvertir2").removeClass("panelVisible").addClass("panelOculto");
                $("#GHInvertir3").removeClass("panelOculto").addClass("panelVisible");

            } else if (tipoCaso == "A1" || tipoCaso == "A2") {
                $("#divP4Resumen").addClass("panelVisible");
                $("#GHInvertir1").addClass("panelVisible");
                $("#GHInvertir2").addClass("panelOculto");
                $("#GHInvertir3").addClass("panelOculto");
                $("#trInicio").addClass("panelOculto");

            } else if (tipoCaso == "A3" || tipoCaso == "A4") {
                $("#divP4Resumen").addClass("panelOculto");

                $("#GHInvertir1").addClass("panelOculto");
                $("#GHInvertir2").addClass("panelVisible");;
                $("#GHInvertir3").addClass("panelOculto");
                $("#trInicio").addClass("panelOculto");
            }
            else {
                $("#divP4Resumen").addClass("panelVisible");

                $("#GHInvertir1").removeClass("panelOculto").addClass("panelVisible");
                $("#GHInvertir2").addClass("panelOculto");
                $("#GHInvertir3").addClass("panelOculto");
                $("#trInicio").addClass("panelVisible").addClass("panelOculto");
            }

            $("#divResumenAll").removeClass("panelVisible").addClass("panelOculto");
            $("#divP4").removeClass("panelOculto").addClass("panelVisible");

            cfgGastoHormiga();
        }

        function ocultarTemas() {
            //document.getElementById('DVTemasImportanciaDesplegableIMSS').style.display = 'none';
            $("#DVTemasImportanciaDesplegableIMSS").removeClass("panelVisible").addClass("panelOculto");
        }
        function mostrarTemas() {
            //document.getElementById('DVTemasImportanciaDesplegableIMSS').style.display = 'block';
            $("#DVTemasImportanciaDesplegableIMSS").removeClass("panelOculto").addClass("panelVisible");
        }

        /*AJN - ODT -07 02122021*/
        function point_it(event) {
            pos_x = event.offsetX ? (event.offsetX) : event.pageX - document.getElementById("DVTemasImportanciaDesplegableIMSS").offsetLeft;
            pos_y = event.offsetY ? (event.offsetY) : event.pageY - document.getElementById("DVTemasImportanciaDesplegableIMSS").offsetTop;

            if (pos_x >= 5 && pos_x <= 235) {
                if (pos_y >= 30 && pos_y <= 57) {
                    $('#lnkApVol').trigger('click');
                }
                //if (pos_y >= 58 && pos_y <= 85) {
                //    $('#lnkTasaRemp')[0].click();
                //}
            }
        }
    </script>

    <!-- codigo nuevo-->
    <!--  Codigo para las graficas -->
    <%--<script>
        function Grafica1(resultado) {
            var ctx = document.getElementById("chartContainer");
            new Chart(ctx, {
                type: 'line',
                data: {
                    labels: aLabels,
                    datasets: [
                      {
                          label: "RT",
                          backgroundColor: "#8BC43F",
                          data: aDatasets1
                      }, {
                          label: "AT",
                          backgroundColor: "#EE5C2B",
                          data: aDatasets2
                      }
                    ]
                },
                options: {
                    title: {
                        display: false,
                        text: 'Aportaciones y rendimientos'
                    },
                    scales: {
                        xAxes: [{
                            gridLines: {
                                display: false
                            },
                            display: false
                        }],
                        yAxes: [{
                            stacked: true,
                            gridLines: {
                                display: false
                            },
                            display: false
                        }]
                    },
                    legend: {
                        display: false
                    },
                    elements: { point: { radius: 0 } }
                }
            });
        }

        function Grafica2() {
            var ctx = document.getElementById("myChart2");
            new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: ["1900", "1950", "1999", "2050"],
                    datasets: [
                      {
                          label: "Africa",
                          backgroundColor: "#EF4832",
                          data: [133, 221, 783, 2478]
                      }, {
                          label: "Europe",
                          backgroundColor: "#85C94D",
                          data: [408, 547, 675, 734]
                      }
                    ]
                },
                options: {
                    title: {
                        display: false,
                        text: 'Population growth (millions)'
                    },
                    segmentShowStroke: false,
                    "hover": {
                        "animationDuration": 0
                    },
                    "animation": {
                        "duration": 1,
                        "onComplete": function () {
                            var chartInstance = this.chart,
                                ctx = chartInstance.ctx;
                            ctx.font = Chart.helpers.fontString(Chart.defaults.global.defaultFontSize, Chart.defaults.global.defaultFontStyle, Chart.defaults.global.defaultFontFamily);
                            ctx.textAlign = 'center';
                            ctx.textBaseline = 'bottom';
                            this.data.datasets.forEach(function (dataset, i) {
                                var meta = chartInstance.controller.getDatasetMeta(i);
                                meta.data.forEach(function (bar, index) {
                                    var data = dataset.data[index];
                                    ctx.fillText(data, bar._model.x, bar._model.y - 5);
                                    switch (dataset.label) {
                                        case 'Africa':
                                            ctx.fillStyle = "#85C94D";
                                            break;
                                        case 'Europe':
                                            ctx.fillStyle = "#EF4832";
                                            break;
                                    }
                                });
                            });
                        }
                    },
                    scales: {
                        xAxes: [{
                            gridLines: {
                                display: false
                            },
                            display: false,
                            ticks: {
                                fontFamily: "Courier New",
                                beginAtZero: true,
                                display: true,
                            }
                        }],
                        yAxes: [{
                            stacked: false,
                            gridLines: {
                                display: false
                            },
                            display: false,
                            ticks: {
                                fontFamily: "Courier New",
                                beginAtZero: true,
                                display: false,
                            }
                        }]
                    },
                    legend: {
                        "display": false
                    },
                    tooltips: {
                        "enabled": false
                    }
                }
            });
        }
    </script>--%>



    <%--<link href="/css/estilo.css" rel="stylesheet" type="text/css" />--%>

    <%--<link href="/css/menu.css" rel="stylesheet" type="text/css" />--%>

    <script src="http://f1.na.readspeaker.com/script/6824/ReadSpeaker.js?pids=embhl"
        type="text/javascript"></script>
    <%--<link rel="stylesheet" href="http://www.encuentra.gob.mx/css/apf.css" type="text/css" />--%>
    <%--<script src="http://www.encuentra.gob.mx/api/gobmxWidgetAPI-min.js" type="text/javascript"></script>--%>
    <%--<script src="http://www.encuentra.gob.mx/properties/gobmxWidgetAPI-conf.js" type="text/javascript"></script>--%>
    <%--<link href="/css/mapa.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="/css/slider.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="/css/listado.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="/css/uno.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="/css/dos.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="/css/tres.css" rel="stylesheet" type="text/css" />--%>

    <!-- codigo nuevo-->
    <script src="js/globos.js"></script>
    <script src="notifyme-master/assets/js/modernizr.js"></script>

    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-172059050-1"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());

        gtag('config', 'UA-172059050-1');
    </script>
</head>

<body onload="GetComisionesAforeClave();">

    <%--<script src="js/DigitalAnalitics.js"></script>--%>
    <script type="text/javascript">
        function udm_(e) { var t = "comScore=", n = document, r = n.cookie, i = "", s = "indexOf", o = "substring", u = "length", a = 2048, f, l = "&ns_", c = "&", h, p, d, v, m = window, g = m.encodeURIComponent || escape; if (r[s](t) + 1) for (d = 0, p = r.split(";"), v = p[u]; d < v; d++) h = p[d][s](t), h + 1 && (i = c + unescape(p[d][o](h + t[u]))); e += l + "_t=" + +(new Date) + l + "c=" + (n.characterSet || n.defaultCharset || "") + "&c8=" + g(n.title) + i + "&c7=" + g(n.URL) + "&c9=" + g(n.referrer), e[u] > a && e[s](c) > 0 && (f = e[o](0, a - 8).lastIndexOf(c), e = (e[o](0, f) + l + "cut=" + g(e[o](f + 1)))[o](0, a)), n.images ? (h = new Image, m.ns_p || (ns_p = h), h.src = e) : n.write("<", "p", "><", 'img src="', e, '" height="1" width="1" alt="*"', "><", "/p", ">") };
        function uid_call(a, b) {
            ui_c2 = 17183199; // your corporate c2 client value
            ui_ns_site = 'gobmx'; // your sites identifier
            window.b_ui_event = window.c_ui_event != null ? window.c_ui_event : "", window.c_ui_event = a;
            var ui_pixel_url = 'http://b.scorecardresearch.com/p?c1=2&c2=' + ui_c2 + '&ns_site=' + ui_ns_site + '&name=' + a + '&ns_type=hidden&type=hidden&ns_ui_type=' + b;
            var b = "comScore=", c = document, d = c.cookie, e = "", f = "indexOf", g = "substring", h = "length", i = 2048, j, k = "&ns_", l = "&", m, n, o, p, q = window, r = q.encodeURIComponent || escape; if (d[f](b) + 1) for (o = 0, n = d.split(";"), p = n[h]; o < p; o++) m = n[o][f](b), m + 1 && (e = l + unescape(n[o][g](m + b[h]))); ui_pixel_url += k + "_t=" + +(new Date) + k + "c=" + (c.characterSet || c.defaultCharset || "") + "&c8=" + r(c.title) + e + "&c7=" + r(c.URL) + "&c9=" + r(c.referrer) + "&b_ui_event=" + b_ui_event + "&c_ui_event=" + c_ui_event, ui_pixel_url[h] > i && ui_pixel_url[f](l) > 0 && (j = ui_pixel_url[g](0, i - 8).lastIndexOf(l), ui_pixel_url = (ui_pixel_url[g](0, j) + k + "cut=" + r(ui_pixel_url[g](j + 1)))[g](0, i)), c.images ? (m = new Image, q.ns_p || (ns_p = m), m.src = ui_pixel_url) : c.write("<p><img src='", ui_pixel_url, "' height='1' width='1' alt='*'></p>");
        }
        //udm_('http://b.scorecardresearch.com/b?c1=2&c2=17183199&ns_site=gobmx&name=consar.inicio.login');

        var urlHost = window.location.host.replace(/\//g, ".").toLocaleLowerCase();
        var quitarASPX = window.location.pathname.replace(/\//g, ".").toLocaleLowerCase();
        var urlPathName = quitarASPX.substring(0, quitarASPX.length - 5);
        //var NombreUbicacionAplicativo = urlHost + urlPathName;
        var NombreUbicacionAplicativo = "consar" + "." + urlHost + urlPathName;

        udm_('http://b.scorecardresearch.com/b?c1=2&c2=17183199&ns_site=gobmx&name=' + NombreUbicacionAplicativo);

    </script>

    <%--Inicio Mantenimiento--%>
    <%--<header><nav class="navbar navbar-inverse navbar-fixed-top" role="navigation"><div class="container"><div class="navbar-header"><button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbarMainCollapse"><span class="sr-only">Interruptor de Navegación</span><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button><a class="navbar-brand" href="https://www.gob.mx/"><img src="https://framework-gb.cdn.gob.mx/landing/img/logoheader.svg" width="128" height="48" alt="Página de inicio, Gobierno de México"></a></div><div class="collapse navbar-collapse" id="navbarMainCollapse"><ul class="nav navbar-nav navbar-right"><li><a href="https://www.gob.mx/tramites">Trámites</a></li><li><a href="https://www.gob.mx/gobierno">Gobierno</a></li><li><a href="https://www.gob.mx/busqueda"><span class="sr-only">Búsqueda</span><i class="icon-search"></i></a></li></ul></div></div></nav></header>--%>
    <%--Fin Mantenimiento--%>

    <%-- Control para la creación del menú de navegación 
   <nav class="navbar navbar-inverse sub-navbar">--%>
    <nav class="navbar navbar-inverse sub-navbar" style="margin-top: -1px;">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#subenlaces">Submenú</button>
            </div>

            <div class="collapse navbar-collapse" id="subenlaces">
                <ul class="nav navbar-nav navbar-right">
                    <li><a class="home_menu" href="http://www.gob.mx/consar/archivo/articulos" data-section=".news.section">Blog</a></li>
                    <li><a class="home_menu" href="http://www.gob.mx/consar/archivo/multimedia" data-section=".multimedia">Multimedia</a></li>
                    <li><a class="home_menu" href="http://www.gob.mx/consar/archivo/prensa" data-section=".press">Prensa</a></li>
                    <li><a class="home_menu" href="http://www.gob.mx/consar/archivo/acciones_y_programas" data-section=".programs">Acciones y Programas</a></li>
                    <li><a class="home_menu" href="http://www.gob.mx/consar/archivo/documentos" data-section=".documents">Documentos</a></li>
                    <li><a class="home_menu" href="http://www.consar.gob.mx/gobmx/Transparencia/transparencia.aspx" data-section=".tran.section">Transparencia</a></li>
                    <li><a class="home_menu" href="http://www.gob.mx/consar#contacto" data-section=".tran.section">Contacto</a></li>
                </ul>
            </div>
        </div>
    </nav>



    <%-- <nav class="warning-nav">
      <div class="container">
         <div class="row clearfix">
            <div class="col-xs-12 col-sm-12 col-md-12">
               <div>
                  <p>Este contenido será modificado temporalmente en atención a las disposiciones legales y normativas en materia electoral, con motivo del inicio de periodo de campaña</p>
               </div>
            </div>
         </div>
      </div>

   </nav>--%>

    <!-- Modal -->
    <div class="modal fade" id="ModalEncuestaSatisfaccion" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Encuesta de satisfacción</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <fieldset>
                        <legend class="label form-control clearfix" style="color: black; border: 0 none; width: 100%; white-space: normal; background-color: transparent; font-size: 18px; margin-bottom: 30px; font-weight: normal; box-shadow: inset 0 0 0 rgba(0,0,0,0)">¿Qué te parece esta herramienta?
                        </legend>
                        <div class="btn-group" style="font-weight: normal; width: 100%; margin-top: 25px; padding: 10px;">
                            <div class="col-sm-4">

                                <button type="button" id="btnInfoBuena" class="btn-special">
                                    <img src="imagenes/IconsModal/ico-satisfation-good.svg" />
                                </button>
                            </div>
                            <div class="col-sm-4">
                                <button type="button" id="btnInfoRegular" class="btn-special">
                                    <img src="imagenes/IconsModal/ico-satisfaction-regular.jpg" />
                                </button>
                            </div>

                            <div class="col-sm-4">
                                <button type="button" id="btnInfoBad" class="btn-special">
                                    <img src="imagenes/IconsModal/ico-satisfaction-bad.jpg" />
                                </button>
                            </div>

                        </div>


                        <div id="InfoBuena" style="display: none; padding: 10px;">
                            <fieldset>
                                <legend class="label form-control clearfix" style="color: black; border: 0 none; width: 100%; white-space: normal; background-color: transparent; font-size: 18px; margin-bottom: 30px; font-weight: normal; box-shadow: inset 0 0 0 rgba(0,0,0,0)">¿En qué puede mejorar este trámite o servicio?</legend>


                                <div class="checkbox-group clearfix" style="font-weight: normal; width: 100%; margin-top: 18px; font-weight: normal; width: 100%; margin-top: 18px;">
                                    <div class="col-sm-12" style="padding: 5px;" align="left">
                                        <label style="font-weight: normal;"></label>
                                        <input id="radEncA_D1" type="checkbox" name="A_D" value="1" />
                                        Que la información proporcionada sea más clara y completa
                                    </div>
                                    <div class="col-sm-12" style="padding: 5px; font-weight: normal; width: 100%; margin-top: 10px;" align="left">
                                        <label style="font-weight: normal;"></label>
                                        <input id="radEncA_D2" type="checkbox" name="A_D" value="2" />
                                        Tiempo de respuesta
                                    </div>
                                    <div class="col-sm-12" style="padding: 5px; font-weight: normal; width: 100%; margin-top: 10px;" align="left">
                                        <label style="font-weight: normal;"></label>
                                        <input id="radEncA_D3" type="checkbox" name="A_D" value="3" />
                                        Costo elevado
                                    </div>
                                    <div class="col-sm-12" style="padding: 5px; font-weight: normal; width: 100%; margin-top: 10px;" align="left">
                                        <label style="font-weight: normal;"></label>
                                        <input id="radEncA_D4" type="checkbox" name="A_D" value="4" />
                                        Número de requisitos
                                    </div>
                                    <div class="col-sm-12" style="padding: 5px; font-weight: normal; width: 100%; margin-top: 10px;" align="left">
                                        <label style="font-weight: normal;"></label>
                                        <input id="radEncA_D5" type="checkbox" name="A_D" value="5" />
                                        Que se pueda realizar 100% en Internet
                                    </div>
                                    <div class="col-sm-12" style="padding: 5px; font-weight: normal; width: 100%; margin-top: 10px;" align="left">
                                        <label style="font-weight: normal;"></label>
                                        <input id="radEncA_D6" type="checkbox" name="A_D" value="6" />
                                        El formato es muy difícil de llenar
                                    </div>
                                    <div class="col-sm-12" style="padding: 5px; font-weight: normal; width: 100%; margin-top: 10px;" align="left">
                                        <label style="font-weight: normal;"></label>
                                        <input id="radEncA_D7" type="checkbox" name="A_D" value="7" />
                                        El formulario web es muy difícil de llenar
                                    </div>
                                </div>


                                <div class="row" id="S_A" style="display: block; padding: 10px; margin-top: 25px;" align="left">
                                    <fieldset>
                                        <legend class="label form-control clearfix" style="color: black; border: 0 none; width: 100%; white-space: normal; background-color: transparent; font-size: 18px; margin-bottom: 30px; font-weight: normal; box-shadow: inset 0 0 0 rgba(0,0,0,0)">Mesa de ayuda</legend>
                                        <div class="col-sm-12" style="padding: 5px;" align="left">
                                            <label id="l_l_50" style="font-weight: normal;">gobmx@funcionpublica.gob.mx</label>
                                        </div>
                                        <div class="col-sm-12" style="padding: 5px;" align="left"><a href="https://www.gob.mx/tramites/ficha/presentacion-de-quejas-y-denuncias-en-la-sfp/SFP54" target="_blank" title="Abre en nueva ventana">Denuncia contra servidores públicos</a></div>
                                    </fieldset>
                                </div>
                            </fieldset>
                            <div class="form-group">
                                <div class="col-md-12 respuesta"></div>
                                <div class="modal-footer">
                                    <div class="row" style="padding: 10px;" align="center">
                                        <button id="btnEnviarEncuesta" style="color: #fff; background-color: #9D2449; border-color: #9D2449;" type="button" class="btn btn-primary">Enviar encuesta</button>
                                    </div>

                                </div>
                            </div>

                        </div>

                        <div id="InfoRegular" style="display: none; padding: 10px;">
                            <fieldset>
                                <legend class="label form-control clearfix" style="color: black; border: 0 none; width: 100%; white-space: normal; background-color: transparent; font-size: 18px; margin-bottom: 30px; font-weight: normal; box-shadow: inset 0 0 0 rgba(0,0,0,0)">¿En qué puede mejorar este trámite o servicio?</legend>


                                <div class="checkbox-group clearfix" style="font-weight: normal; width: 100%; margin-top: 18px; font-weight: normal; width: 100%; margin-top: 18px;">

                                    <div class="col-sm-12" style="padding: 5px; font-weight: normal; width: 100%; margin-top: 10px;" align="left">
                                        <label style="font-weight: normal;"></label>
                                        <input id="radEncA_D3" type="checkbox" name="A_D" value="3" />
                                        Costo elevado
                                    </div>
                                    <div class="col-sm-12" style="padding: 5px; font-weight: normal; width: 100%; margin-top: 10px;" align="left">
                                        <label style="font-weight: normal;"></label>
                                        <input id="radEncA_D4" type="checkbox" name="A_D" value="4" />
                                        Número de requisitos
                                    </div>

                                </div>


                                <div class="row" id="S_A" style="display: block; padding: 10px; margin-top: 25px;" align="left">
                                    <fieldset>
                                        <legend class="label form-control clearfix" style="color: black; border: 0 none; width: 100%; white-space: normal; background-color: transparent; font-size: 18px; margin-bottom: 30px; font-weight: normal; box-shadow: inset 0 0 0 rgba(0,0,0,0)">Mesa de ayuda</legend>
                                        <div class="col-sm-12" style="padding: 5px;" align="left">
                                            <label id="l_l_501" style="font-weight: normal;">gobmx@funcionpublica.gob.mx</label>
                                        </div>
                                        <div class="col-sm-12" style="padding: 5px;" align="left"><a href="https://www.gob.mx/tramites/ficha/presentacion-de-quejas-y-denuncias-en-la-sfp/SFP54" target="_blank" title="Abre en nueva ventana">Denuncia contra servidores públicos</a></div>
                                    </fieldset>
                                </div>
                            </fieldset>

                            <div class="form-group">
                                <div class="col-md-12 respuesta"></div>
                                <div class="modal-footer">
                                    <div class="row" style="padding: 10px;" align="center">
                                        <button style="color: #fff; background-color: #9D2449; border-color: #9D2449;" type="button" class="btn btn-primary">Enviar encuesta</button>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div id="InfoBad" style="display: none; padding: 10px;">
                            <fieldset>
                                <legend class="label form-control clearfix" style="color: black; border: 0 none; width: 100%; white-space: normal; background-color: transparent; font-size: 18px; margin-bottom: 30px; font-weight: normal; box-shadow: inset 0 0 0 rgba(0,0,0,0)">¿En qué puede mejorar este trámite o servicio?</legend>


                                <div class="checkbox-group clearfix" style="font-weight: normal; width: 100%; margin-top: 18px; font-weight: normal; width: 100%; margin-top: 18px;">
                                    <div class="col-sm-12" style="padding: 5px; font-weight: normal; width: 100%; margin-top: 10px;" align="left">
                                        <label style="font-weight: normal;"></label>
                                        <input id="radEncA_D5" type="checkbox" name="A_D" value="5" />
                                        Que se pueda realizar 100% en Internet
                                    </div>
                                    <div class="col-sm-12" style="padding: 5px; font-weight: normal; width: 100%; margin-top: 10px;" align="left">
                                        <label style="font-weight: normal;"></label>
                                        <input id="radEncA_D6" type="checkbox" name="A_D" value="6" />
                                        El formato es muy difícil de llenar
                                    </div>
                                    <div class="col-sm-12" style="padding: 5px; font-weight: normal; width: 100%; margin-top: 10px;" align="left">
                                        <label style="font-weight: normal;"></label>
                                        <input id="radEncA_D7" type="checkbox" name="A_D" value="7" />
                                        El formulario web es muy difícil de llenar
                                    </div>
                                </div>


                                <div class="row" id="S_A3" style="display: block; padding: 10px; margin-top: 25px;" align="left">
                                    <fieldset>
                                        <legend class="label form-control clearfix" style="color: black; border: 0 none; width: 100%; white-space: normal; background-color: transparent; font-size: 18px; margin-bottom: 30px; font-weight: normal; box-shadow: inset 0 0 0 rgba(0,0,0,0)">Mesa de ayuda</legend>
                                        <div class="col-sm-12" style="padding: 5px;" align="left">
                                            <label id="l_l_503" style="font-weight: normal;">gobmx@funcionpublica.gob.mx</label>
                                        </div>
                                        <div class="col-sm-12" style="padding: 5px;" align="left"><a href="https://www.gob.mx/tramites/ficha/presentacion-de-quejas-y-denuncias-en-la-sfp/SFP54" target="_blank" title="Abre en nueva ventana">Denuncia contra servidores públicos</a></div>
                                    </fieldset>
                                </div>
                            </fieldset>

                            <div class="form-group">
                                <div class="col-md-12"></div>
                                <div class="modal-footer">
                                    <div class="row" style="padding: 10px;" align="center">
                                        <button style="color: #fff; background-color: #9D2449; border-color: #9D2449;" type="button" class="btn btn-primary">Enviar encuesta</button>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </fieldset>

                </div>
                <div class="modal-footer" style="align-items: center">
                    <%--<button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>--%>
                </div>
            </div>
        </div>
    </div>



    <!-- Modal Confirmacion-->
    <div class="modal fade" id="modalConfirmacion" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel1" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <%--<div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel1">Modal title</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>--%>
                <div class="modal-body">
                    <div class="modal-title" style="align-content: center">
                        <div class="col-md-12"></div>

                        <div class="row" style="padding: 10px;">
                            <button style="color: #fff; background-color: #9D2449; border-color: #9D2449;" type="button" class="btn btn-primary">Enviar encuesta</button>
                        </div>

                    </div>
                </div>
                <div class="modal-footer btn-secondary" style="align-items: center">
                    <fieldset>
                        <legend class="text-center" style="font-size: medium">¡Gracias por compartir tu experiencia y ayudarnos a mejorar!</legend>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>


    <script>
      

    </script>


    <div class="inner-page interior documents">
        <div class="container">

            <div class="row">
                <div class="col-md-12">
                    <div class="container-notify">
                        <div id="myDiv">
                        </div>
                        <div class="btn-group">
                            <a class="btn error" style="display: none;" id="btnError"><i class="fa fa-warning"></i>Error</a>
                        </div>
                    </div>
                    <!-- SCRIPTS -->
                    <script src="notifyme-master/assets/js/notifyme.js"></script>
                    <script type="text/javascript">
                        $(document).ready(function () {
                            var imagen = '<img src="imagenes/Imagen_EdoCta-Cal_IMSS.png" style="display: block;margin-left: auto;margin-right: auto; width: 100%;">';   //AJN - ODT -07 29112021
                            var imgFlecha = '<img style="margin: 60px 0px 0px 10px;" src="imagenes/ImagenFlecha.png">';
                            $('.error').on('click', function () {
                                $(this).notifyMe(
                                    'bottom',
                                    'error',
                                    "<div id='DVImgEdoCuenta' style='align:center;'><a href='https://www.gob.mx/consar/articulos/pension-por-regimen-73'>" + imagen + "</a></div>",
                                    ""


                                );
                            });
                        });
                    </script>
                    <input id="hddLugarComision" type="hidden" />
                    <input id="PESinRedondeo" type="hidden" />
                    <div id="mensaje1">

                        <p class="text_gral_popup">
                            <!--La comisión es el cobro que te hace la AFORE por administrar los recursos de tu cuenta AFORE. Actualmente estás en la AFORE No. <b id='boldStuff'>dude</b> de 10 que hay en el mercado. A menor comisión podrías tener mayor pensión. Da clic  <a href="https://www.gob.mx/consar/es/articulos/comisiones-vigentes-en-2020?idiom=es" target="_blank">aquí</a> para que conozcas la comisión que cobran las demás AFORE.-->

                            La comisión es el cobro que te hace la AFORE por administrar los recursos de tu cuenta individual. A menor comisión podrías tener mayor pensión. Da clic  <a href="https://www.gob.mx/consar/es/articulos/comisiones-vigentes-en-2022?idiom=es" target="_blank">aquí</a>  para que conozcas la comisión que cobran las demás AFORE.
					 
                        </p>
                    </div>
                    <div id="mensaje2">
                        <p class="text_gral_popup">
                            Es el último salario base que aparece en tu recibo de nómina  o que aparece en tu estado de cuenta.
                        </p>
                    </div>
                    <div id="mensaje3">
                        <p class="text_gral_popup">
                            Ingresa el saldo de las subcuentas de Retiro, Cesantía en Edad Avanzada y Vejez <strong>(RCV)</strong>  más Ahorro Voluntario <strong>(AV)</strong> que aparece en tu estado de cuenta.
                        </p>
                    </div>
                    <div id="mensaje4">
                        <p class="text_gral_popup">
                            Esta fecha debe coincidir con tu acta de nacimiento. Acude a tu AFORE para actualizar tus datos si están equivocados.
                        </p>
                    </div>
                    <div id="mensaje5">
                        <p class="text_gral_popup">
                            Año en que inicio a cotizar al IMSS.
                        </p>
                    </div>
                    <div id="mensaje6">
                        <p class="text_gral_popup">
                            Es el rendimiento que te da tu AFORE antes del cobro de la comisión. Puedes elegir dos opciones de rendimiento: 4% y 5%. 
            Haz el cálculo con ambos porcentajes para que veas cómo aumenta tu saldo en un caso y en otro. 
            Para comparar a las AFORE en Rendimiento Neto haz clic <a href="http://www.gob.mx/consar/acciones-y-programas/como-elegir-la-mejor-afore?idiom=es" target="_blank">aquí</a>.
                        </p>
                    </div>
                    <div id="mensaje7">
                        <p class="text_gral_popup">
                            Si no conoces tus semanas de cotización entra a la siguiente liga <a href="http://www.imss.gob.mx/derechoH/semanas-cotizadas" target="_blank">http://www.imss.gob.mx/derechoH/ semanas-cotizadas</a> solamente tendrás que abrir una cuenta para que ahí recibas tu información. 
                        </p>
                    </div>
                    <div id="mensaje8">
                        <p class="text_gral_popup">
                            Es la edad en la que solicitarías tu pensión ante el IMSS. La Calculadora de retiro permite estimar tu pensión para una edad de retiro entre 60 y 67 años. Con las modificaciones a la Ley del Seguro Social, el monto de tu pensión podría depender de tu edad de retiro, las semanas que hayas cotizado y el salario promedio de tu carrera laboral. Para mayor información consulta la Ley del Seguro Social en el capítulo VI del seguro de retiro, cesantía en edad avanzada y vejez.
                        </p>
                    </div>
                    <div id="mensaje9">
                        <p class="text_gral_popup">
                        </p>
                    </div>
                    <div id="mensaje10">
                        <p class="text_gral_popup" style="margin-top: 5px;">
                            En la gráfica se muestra el saldo inicial de tu cuenta más las aportaciones que realices y los rendimientos estimados a futuro que genere la inversión de tu dinero a partir de hoy y hasta la edad de retiro.
                        </p>
                    </div>
                    <div id="mensaje10-1">
                        <p class="text_gral_popup" style="margin-top: 5px;">
                            En la gráfica se muestra el saldo inicial de tu cuenta más las aportaciones que realices y los rendimientos estimados a futuro que genere la inversión de tu dinero a partir de hoy y hasta la edad de retiro.
                        </p>
                    </div>
                    <div id="mensaje11">
                        <p class="text_gral_popup">
                            Esta estimación se realiza considerando la opción de una renta vitalicia (donde la pensión la paga una aseguradora), en caso de que seas un trabajador/a con derecho a pensión y sin beneficiarios. No incluye el Seguro de Sobrevivencia en caso de fallecimiento.
                        </p>
                    </div>
                    <div id="mensaje12">
                        <p class="text_gral_popup">
                            Esto se conoce como Tasa de Reemplazo y está calculada respecto a tu Salario Base de Cotización mensual.
                        </p>
                    </div>
                    <div id="mensaje13">
                        <p class="text_gral_popup">
                            Esta estimación se realiza considerando la opción de una renta vitalicia (donde la pensión la paga una aseguradora), en caso de que seas un trabajador/a con derecho a pensión y sin beneficiarios. No incluye el Seguro de Sobrevivencia en caso de fallecimiento.
                        </p>
                    </div>

                    <div id="mensaje14">
                        <p class="text_gral_popup">
                            La densidad de cotización es la proporción de tiempo que el trabajador, durante su vida laboral, cotiza al sistema de pensiones. Para efectos de las estimaciones de saldo de ahorro obligatorio y voluntario, la densidad de cotización supuesta para el futuro es de 80%.
                        </p>
                    </div>

                    <div id="mensaje15">
                        <p class="text_gral_popup">
                            La densidad de cotización es la proporción de tiempo que el trabajador, durante su vida laboral, cotiza al sistema de pensiones. Para efectos de las estimaciones de saldo de ahorro obligatorio y voluntario, la densidad de cotización supuesta para el futuro es de 80%.
                        </p>
                    </div>

                    <div style='display: none'>
                        <div id='msgGenracionTransicion' style='padding: 10px; background: #fff;'>
                            <p class="text_gral_justify_">
                                Si cotizaste para ambos regímenes del Seguro Social (LSS 1973 y LSS 1997), eres un trabajador de Transición, 
                por lo que seguramente te estarás pensionando con la Ley del Seguro Social 73.
                            </p>
                            <div class="GeneracionTransicionImage"></div>
                            <p class="text_gral_justify_" style="font-weight: 700;">
                                ¿Qué requisitos debes cumplir para pensionarte por el régimen de 1973?
                            </p>
                            <ul class="text_gral_justify_">
                                <li>Requieres mínimo 500 semanas de cotización
                                </li>
                                <li>No estar sujeto a una relación laboral 
                                </li>
                                <li>Tener 60 años de edad para una pensión por Cesantía en Edad Avanzada o 
                                </li>
                                <li>65 años para una pensión por vejez
                                </li>
                            </ul>
                            <p class="text_gral_justify_">
                                El monto de tu pensión se irá actualizando anualmente de acuerdo a los incrementos de la inflación.
                            </p>
                            <p class="text_gral_justify_" style="font-weight: 700;">
                                ¿Qué requisitos debes cumplir para pensionarte por el régimen de 1997?
                            </p>
                            <ul class="text_gral_justify_">
                                <li>Requieres mínimo 1,250 semanas de cotización
                                </li>
                                <li>No estar sujeto a una relación laboral 
                                </li>
                                <li>Tener 60 a 64 años (Pensión por Cesantía)
                                </li>
                                <li>Tener 65 años (Pensión por vejez)
                                </li>
                            </ul>
                            <p class="text_gral_justify_">
                                El monto de tu pensión se irá actualizando anualmente de acuerdo a los incrementos de la inflación.
                            </p>
                        </div>
                    </div>
                    <div style="display: none;">
                        <div id="msgAumentarAportacion">
                            <p class="text_Modales_Indi">
                                Si ya decidiste hacer ahorro voluntario, te invitamos a que utilices a cualquiera de estas vías.
                            </p>
                            <ul class="text_Modales_Indi">
                                <li>
                                    <span class="MarcaListaAR">Domiciliación. </span>Llenar tu solicitud e indicar la cuenta bancaria en la cual se realizará el cargo, monto y periodicidad.
                                </li>
                                <li>
                                    <span class="MarcaListaAR">Tiendas de conveniencia (7-eleven). </span>Únicamente deberás proporcionar tu CURP y el monto que deseas depositar al cajero de cualquier sucursal donde desees aportar. Puedes hacer aportaciones desde $50.00 y no te cobrarán comisión por tus depósitos.
                                </li>
                                <li>
                                    <span class="MarcaListaAR">Personalmente. </span>Acude a cualquier sucursal de la AFORE y realiza el depósito directamente en ventanilla.
                                </li>
                                <li>
                                    <span class="MarcaListaAR">A través de tu patrón. </span>Puedes solicitar que te descuenten vía nomina la cantidad que deseas ahorrar. Para formalizar este proceso, el patrón te pedirá que le autorices por escrito el monto que deseas ahorrar.
                                </li>
                                <li>
                                    <span class="MarcaListaAR">Por Internet. </span>Ingresa a la página de Internet de cada una de las AFORE para que llenes tu solicitud.
                                </li>
                            </ul>
                            <p class="text_Modales_Indi" style="font-weight: 700;">
                                Las Aportaciones Voluntarias ofrecen muchos beneficios:
                            </p>
                            <ul class="text_Modales_Indi">
                                <li>Te da atractivos rendimientos, superiores a los de cualquier instrumento bancario</li>
                                <li>Lo haces de acuerdo a tus posibilidades y cuando tú lo decidas</li>
                                <li>Son deducibles de impuestos</li>
                                <li>Tienes disponibilidad de tus recursos cada 2 o 6 meses, dependiendo la AFORE en la que te encuentres</li>
                            </ul>
                        </div>
                    </div>
                    <%--//AJN - ODT -07--%>
                    <form id="formID" runat="server">
                        <asp:HiddenField runat="server" ID="hdfGeneracionTransicion" Value="" />
                        <asp:HiddenField runat="server" ID="hdfAniosCotizados" Value="" />
                        <asp:HiddenField runat="server" ID="hdfCampoFocus" Value="" />
                        <asp:HiddenField runat="server" ID="hdfCountFocus" Value="0" />
                        <asp:HiddenField runat="server" ID="hdfNacimiento" Value="" />
                        <asp:HiddenField runat="server" ID="hdfAfiliacion" Value="" />
                        <asp:HiddenField runat="server" ID="txtAniosSel" Value="0" />

                        <%-- PGO-ODT 25 --%>
                        <asp:HiddenField runat="server" ID="hdfFechacambio1" Value="0" />
                        <asp:HiddenField runat="server" ID="hdfFechacambio2" Value="0" />
                        <asp:HiddenField runat="server" ID="HdfAOI" Value="0" />

                        <asp:HiddenField runat="server" ID="HdfSaldoFinalCompleto" Value="0" />


                        <!-- cuerpo nuevo -->

                        <div id="sombracompleta" style="position: absolute; margin-top: 1358px; width: 100%; z-index: -1; margin-left: 0px; background-color: #E9E9E9; height: 0px;">
                        </div>
                        <div class="center">



                            <%--<div>--%>
                            <!-- codigo calculadora-->
                            <!-- INICIO CONTENIDO CALCULADORA -->
                            <%--Inicio Mantenimiento--%>
                            <%-- <div>
                            <h2>Calculadora IMSS</h2>
                            <p>
                                El pasado 16 de diciembre de 2020 se publicó en el Diario Oficial de la Federación el DECRETO por el que se reforman, adicionan y derogan diversas disposiciones de la Ley del Seguro Social y de la Ley de los Sistemas de Ahorro para el Retiro, mediante el cual se modifican de forma sustancial los parámetros para poder realizar el cálculo estimado de pensión, por lo que  la <b>“Calculadora de pensión del IMSS” quedará deshabilitada hasta en tanto se realiza la actualización correspondiente.</b>
                            </p>
                        </div>--%>


                            <%--Fin Mantenimiento--%>
                            <%--  <div runat="server" id="ContenidoCalc" class="CentradoCalc">--%>
                            <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ToolkitScriptManager1" EnableScriptGlobalization="true"
                                EnablePartialRendering="true" EnableScriptLocalization="true" EnablePageMethods="true" />
                            <div id="DVFondoEncabezado" class="DVFondoEncabezadoIMSS" style="margin-top: 80px">
                                <img src="imagenes/encabezado_imss.png" width="988" height="100" alt="Independientes" style="border: none" usemap="#IMSSmap" />
                                <map name="IMSSmap">
                                    <area shape="rect" coords="820,81,925,100" href="PDF/Metodología_Calculadora_de_Retiro.pdf" target="_blank" alt="Metodología" />
                                </map>
                                <div id="DVTemasImportancia" onclick="mostrarTemas();">
                                </div>
                                <div id="DVTemasImportanciaDesplegableIMSS" onmouseout="ocultarTemas();" onclick="point_it(event)" class="panelOculto">
                                    <%--<img id="imgDesplegableTemasImportancia" src="imagenes/desplegable_2.png" width="242" height="122" alt="Temas de Importancia" 
                                style="border: none; position:relative;visibility:hidden;z-index:2;" usemap="#TemasImportancia"  class="panelOculto">--%>
                                </div>

                                <a id="lnkApVol" class='inline' href="#msgAumentarAportacion" style="display: none;">Generacion Transicion</a>
                                <a id="lnkTasaRemp" href="http://www.gob.mx/consar/articulos/que-factores-determinan-mi-pension-una-vision-sobre-las-tasas-de-reemplazo" target="_blank" style="display: none;">Tasa Remplazo</a>

                                <%--
                        <map name="TemasImportancia" >
                        <area shape="rect" coords="0,0,238,86" href="PDF/Metodología_Calculadora_de_Retiro1.pdf" target="_blank" alt="Realiza Aportaciones">
                        <area shape="rect" coords="2,26,238,56" href="PDF/Metodología_Calculadora_de_Retiro2.pdf" target="_blank" alt="Realiza Aportaciones">
                        <area shape="rect" coords="2,56,238,86" href="PDF/Metodología_Calculadora_de_Retiro3.pdf" target="_blank" alt="Realiza Aportaciones">
                        </map>--%>
                            </div>
                            <div id="DVContenedorCalculadora" class="panelVisible">
                                <div id="cargando"></div>
                                <div id="loadingDiv" style="display: none;">
                                    <div>
                                    </div>
                                </div>
                                <div id="DVTextoTitulo">
                                    <p>La Calculadora de Retiro te permite estimar, bajo ciertos supuestos, cuál podría ser tu pensión al finalizar tu vida laboral, además de mostrar las diversas posibilidades de ahorro voluntario. Te invitamos a hacer tu propio cálculo y a tomar acciones para el futuro.</p>
                                </div>

                                <div id="DVAfore">
                                    <img src="imagenes/afore.png" id="imgAfore" /><asp:DropDownList ID="cboAfore" runat="server" onchange="GetComisionAforeClave();" CssClass="validate[required]"></asp:DropDownList><label id="cobracomision">Cobra una comisión de:</label>
                                    <input id="txtComisionAfore" type="text" runat="server" readonly="true" />
                                    <div id="reference1">
                                        <p class="text_gral_center">
                                            <a href="#" onclick="Popup.show('mensaje1','reference1','above center',{'constrainToScreen':true});;return false;">
                                                <img src="imagenes/i_peque.png" id="imgInformacion" />
                                            </a>

                                        </p>
                                    </div>

                                </div>
                                <div id="DVFondoDatos">
                                    <div class="DVFondoDatos-100">
                                        <label for="txtSalarioBaseCotizacion" class="CTRLGeneral DVFondoDatosIzq">
                                            Salario Base de<br />
                                            Cotización mensual</label>
                                        <input id="txtSalarioBaseCotizacion" name="txtSalarioBaseCotizacion" class="CTRLGeneral validate[required,funcCall[valida_salario_cero], funcCall[valida_salario_minimo]]" type="text" onblur="javascript:asignarValorSalarioMensual(this);" maxlength="10" style="background-color: transparent; border: none;" runat="server" />
                                        <div id="slider-txtSalarioBaseCotizacion" class="CTRLGeneral slidergral" style="width: 422px; margin-top: 25px; margin-left: 40px;"></div>
                                        <div id="reference2">
                                            <p class="text_gral_center">
                                                <a href="#" onclick="Popup.show('mensaje2','reference2','above center',{'constrainToScreen':true});;return false;">
                                                    <img src="imagenes/i_peque.png" style="width: 65%; margin-top: 10px;" />
                                                </a>

                                            </p>
                                        </div>
                                        <span id="mensaje-txtSalarioBaseCotizacion" class="message" style="display: none;"></span>
                                    </div>

                                    <div class="DVFondoDatos-50">
                                        <label for="txtSaldoActualAfore" class="CTRLGeneral">
                                            Saldo actual<br />
                                            en tu cuenta AFORE:</label>
                                        <input id="txtSaldoActualAfore" name="txtSaldoActualAfore" class="CTRLGeneral inputRadius validate[required, funcCall[valida_saldo_cero]]" type="text" maxlength="12" style="background-color: transparent; border: none;" runat="server" />
                                        <span id="mensaje-txtSaldoActualAfore" class="message" style="display: none;"></span>
                                        <div id="reference3">
                                            <p class="text_gral_center">
                                                <a href="#" onclick="Popup.show('mensaje3','reference3','above center',{'constrainToScreen':true});;return false;">
                                                    <img src="imagenes/i_peque.png" style="width: 65%; margin-top: 10px;" />
                                                </a>
                                            </p>
                                        </div>
                                    </div>

                                    <div class="DVFondoDatos-50">
                                        <label for="txtGenero" class="CTRLGeneral" style="margin-top: 20px; margin-left: 37px">
                                            Género:<br />
                                            <br />
                                        </label>
                                        <img src="imagenes/mujer.png" class="CTRLGeneral" />
                                        <asp:RadioButton ID="chkMujer" runat="server" CssClass="checkbox CTRLGeneral" GroupName="Genero" Checked="true" />
                                        <img src="imagenes/hombre.png" class="CTRLGeneral" style="margin-left: 35px;" />
                                        <asp:RadioButton ID="chkHombre" runat="server" CssClass="checkbox CTRLGeneral" GroupName="Genero" />

                                    </div>

                                    <div class="DVFondoDatos-50">
                                        <label for="txtFechaNacimiento" class="CTRLGeneral">
                                            Fecha de<br />
                                            nacimiento:</label>
                                        <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="CTRLGeneral inputRadius validate[required, funcCall[valida_fecha_vacio]]"
                                            AutoCompleteType="Disabled"
                                            Style="margin-top: 19px; background-color: transparent; border: none;" placeholder="dd/mm/aaaa"></asp:TextBox>

                                        <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFechaNacimiento"
                                    CssClass="MyCalendar" PopupPosition="Right" />--%>

                                        <%--<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                    TargetControlID="txtFechaNacimiento"
                                    Mask="99/99/9999"
                                    MessageValidatorTip="true"
                                    OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError"
                                    MaskType="Date"
                                    DisplayMoney="Left"
                                    AcceptNegative="Left"
                                    ErrorTooltipEnabled="True" />--%>
                                        <div id="reference4">
                                            <p class="text_gral_center">
                                                <a href="#" onclick="Popup.show('mensaje4','reference4','above center',{'constrainToScreen':true});;return false;">
                                                    <img src="imagenes/i_peque.png" style="width: 65%; margin-top: 10px;" />
                                                </a>
                                            </p>
                                        </div>


                                        <a class='inline' href="#msgGenracionTransicion" id="aGeneracionTransicion" style="display: none;">Generacion Transicion</a>
                                    </div>
                                    <%--//AJN - ODT -07--%>
                                    <div class="DVFondoDatos-50">
                                        <label for="txtEdad" class="CTRLGeneral" style="margin-top: 20px; margin-left: 37px;">
                                            Edad:</label>
                                        <input id="txtEdad" name="txtEdad" class="CTRLGeneral inputTransparente " style="width: 40px; background-color: transparent; border: none;" type="text" readonly="true" />
                                    </div>

                                    <div class="DVFondoDatos-50">
                                        <%--<label for="txtDensidadCotizacion" class="CTRLGeneral">
                                    Densidad de<br />
                                cotización:</label>
                                <div id="slider-txtDensidadCotizacion" class="CTRLGeneral slidergral" style="width: 120px;"></div>
                                <input id="txtDensidadCotizacion" name="txtDensidadCotizacion" class="CTRLGeneral validate[required]" style="width: 50px; background-color: transparent; border: none;" type="text" readonly="true">--%>

                                        <%--<span class="DensidadCotizacionIMSS CTRLGeneral">Densidad de cotización 80%</span>--%>

                                        <label for="txtAnioAfiliacion" class="CTRLGeneral">
                                            Año de<br />
                                            afiliación:</label>
                                        <input id="txtAnioAfiliacion" name="txtAnioAfiliacion" class="CTRLGeneral validate[required, funcCall[valida_anio_afiliacion]]"
                                            style="position: relative; top: -4px; left: 13px; width: 100px; background-color: transparent; border: none;" type="text"
                                            maxlength="4" onkeypress="return ValidNum(event)" />

                                        <div id="reference5">
                                            <p class="text_gral_center">
                                                <a href="#" onclick="Popup.show('mensaje5','reference5','above center',{'constrainToScreen':true});;return false;">
                                                    <img src="imagenes/i_peque.png" style="width: 65%; margin-top: 10px;" />
                                                </a>
                                            </p>
                                        </div>

                                    </div>

                                    <div class="DVFondoDatos-50">
                                        <label for="txtSemanasCotizadas" class="CTRLGeneral DVFondoDatosDer" style="margin-top: 20px; margin-right: 23px; margin-left: 37px;">
                                            Semanas cotizadas:<br />
                                            <br />
                                        </label>
                                        <%--//AJN - ODT -07>--%>
                                        <div id="slider-txtAniosCotizados" class="CTRLGeneral slidergral" style="width: 120px;" hidden></div>
                                        <input id="txtSemanasCotizadas" name="txtAniosCotizados" class="CTRLGeneral validate[required, funcCall[valida_semanas_cotizadas]" style="margin-left: -9px; margin-top: 16px; width: 142px; border: none;" type="text" disabled="" maxlength="6" required />
                                        <div id="reference7">
                                            <p class="text_gral_center">
                                                <a href="#" onclick="Popup.show('mensaje7','reference7','above center',{'constrainToScreen':true});;return false;">
                                                    <img src="imagenes/i_peque.png" style="width: 65%" />
                                                </a>
                                            </p>
                                        </div>
                                    </div>

                                    <div class="DVFondoDatos-50">
                                        <label for="txtRendimientoReralAntesComisiones" class="CTRLGeneral" style="margin-top: 0px">
                                            Rendimiento real de<br>
                                            tu AFORE antes de 
                                    <br>
                                            comisiones:</label>
                                        <div id="slider-txtRendimientoReralAntesComisiones" class="CTRLGeneral slidergral " style="width: 120px;"></div>
                                        <input id="txtRendimientoReralAntesComisiones" name="txtRendimientoReralAntesComisiones validate[required]" class="CTRLGeneral"
                                            style="width: 40px; background-color: transparent; border: none;" type="text" readonly="true" />
                                        <div id="reference6">
                                            <p class="text_gral_center">
                                                <a href="#" onclick="Popup.show('mensaje6','reference6','above center',{'constrainToScreen':true});;return false;">
                                                    <img src="imagenes/i_peque.png" style="width: 65%; margin-top: 10px;" />
                                                </a>
                                            </p>
                                        </div>
                                    </div>

                                    <div class="DVFondoDatos-50">
                                        <label for="txtEdadRetiro" class="CTRLGeneral" style="margin-left: 37px; margin-right: 25px;">
                                            Edad a la que<br />
                                            deseas retirarte:</label>
                                        <div id="slider-txtEdadRetiro" class="CTRLGeneral slidergral" style="width: 120px;"></div>
                                        <input id="txtEdadRetiro" name="txtEdadRetiro" class="CTRLGeneral validate[required]" style="width: 40px; background-color: transparent; border: none;" type="text" readonly="true" />

                                        <div id="reference8">
                                            <p class="text_gral_center">
                                                <a href="#" onclick="Popup.show('mensaje8','reference8','above center',{'constrainToScreen':true});;return false;">
                                                    <img src="imagenes/i_peque.png" style="width: 65%" />
                                                </a>
                                            </p>
                                        </div>
                                    </div>

                                </div>

                                <div id="DVCalculadora-Pie">
                                    <div id="DVPieTexto">
                                        <%--<p>Con los supuestos referidos se estima un rango de pensión para cada nivel de ahorro adicional, bajo la modalidad de Retiro Programado, dado que no se consideran a los beneficiarios ni el costo del seguro de sobrevivencia que serviría para cubrir las pensiones de sus beneficiarios en caso de fallecimiento.</p>--%>
                                        <a href="http://www.gob.mx/consar/acciones-y-programas/calculadoras-de-ahorro-y-retiro?idiom=es">
                                            <div class="RegresaInicio" style="float: left;"></div>
                                        </a>
                                    </div>
                                    <div id="DVCalcular">
                                        <%--<asp:ImageButton ID="btnCalcular" runat="server" ImageUrl="~/imagenes/btn_calcula2.png" onmouseover="this.src='imagenes/btn_calcula2.1.png'" onmouseout="this.src='imagenes/btn_calcula2.png'" CssClass="btnCalcular" OnClientClick="GetResultadoCalculo();" />--%>
                                        <%--<input id="Button1" type="button" value="button" onclick="GetResultadoCalculoPrueba();" />--%>
                                        <%--AJN - ODT -07--%>
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/btn_calcula2.1.png" onmouseover="this.src='imagenes/btn_calcula2.png'" onmouseout="this.src='imagenes/btn_calcula2.1.png'" CssClass="btnCalcular cursorHand submit" type="submit" onClick="GetResultadoCalculoPrueba();" />
                                        <p><a class='inline' href="#inline_content" id="aMensajeError" style='display: none;'>Inline HTML</a></p>
                                    </div>
                                    <%--<asp:Label ID="lblPMG" runat="server" Text="* Tiene derecho a la Pensión Mínima Garantizada, otorgada por el Estado."></asp:Label>--%>
                                    <!-- This contains the hidden content for inline calls -->
                                    <div style='display: none'>
                                        <div id='inline_content' style='padding: 10px; background: #fff;'>
                                            <label id="lMensajeError"></label>
                                        </div>
                                    </div>
                                </div>
                                <div class="PiePagIP">
                                    <asp:Label ID="Label1" runat="server" Text="Usted es el visitante número:" CssClass="numVisitaslB"></asp:Label>
                                    &nbsp;&nbsp;<asp:Label ID="lbContVisitas" runat="server" Text="0" CssClass="numVisitas"></asp:Label>
                                </div>
                            </div>

                            <div id="DVContenedorResultado" class="panelOculto ContenedorResultados">

                                <div id="DVDatosProporcionados" class="EncResultados">
                                    <p>Datos proporcionados</p>
                                    <span id="spanTipoCaso" style="display: none;"></span>
                                </div>

                                <div id="DVDatosFormulario">
                                    <div id="DVSalarioBaseCotizacionMensualR">
                                        <asp:Label ID="lblSalarioBaseCotizacionMensual" runat="server" Text="" CssClass="lblSalarioBaseCotizacionMensualR"></asp:Label>
                                    </div>
                                    <div id="DVSaldoActualAforeR" style="">
                                        <asp:Label ID="lblSaldoActualAfore" runat="server" Text="" CssClass="lblSaldoActualAforeR"></asp:Label>
                                    </div>
                                    <div id="DVGeneroR">
                                        <asp:Image ID="imgGenero" runat="server" CssClass="imgGeneroR" />
                                    </div>
                                    <div id="DVEdadR">
                                        <asp:Label ID="lblEdad" runat="server" Text="" CssClass="lblEdadR"></asp:Label>
                                    </div>
                                    <div id="DVEdadRetiroR">
                                        <asp:Label ID="lblEdadRetiro" runat="server" Text="" CssClass="lblEdadRetiroR"></asp:Label>
                                    </div>
                                    <div id="DVAniosCotizadosR">
                                        <asp:Label ID="lblAniosCotizados" runat="server" Text="" CssClass="lblAniosCotizadosR"></asp:Label>
                                    </div>

                                    <div id="DVRendimientoRealAntesComisionesR">
                                        <asp:Label ID="lblRendimientoRealAntesComisiones" runat="server" Text="" CssClass="lblRendimientoRealAntesComisionesR"></asp:Label>
                                    </div>
                                    <div id="DVDensidadCotizacionR">
                                        <table>
                                            <tr>
                                                <td style="width: 80px;">
                                                    <asp:Label ID="lblDensidadCotizacion" runat="server" Text="" CssClass="lblDensidadCotizacionR"></asp:Label>
                                                </td>

                                                <td>
                                                    <div id="reference14">
                                                        <p class="text_gral_center" style="margin-top: 45px; margin-right: 10px;">
                                                            <a href="#" onclick="Popup.show('mensaje14','reference14','above center',{'constrainToScreen':true});;return false;">
                                                                <img src="imagenes/i_peque.png" style="width: 17px; margin-top: -5px;" />
                                                            </a>
                                                        </p>
                                                    </div>

                                                </td>
                                            </tr>
                                        </table>

                                    </div>
                                    <%--    <div style="text-align: right; padding-top: 10px;">
                                        <button type="button" data-toggle="modal" data-target="#exampleModal" style="float: right; border: 0 none;">
                                            <i class="bi bi-chat" style="font-size: 40px"></i>
                                            <img src="imagenes/IconsModal/Opinion.png" />
                                        </button>
                                    </div>--%>


                                    <%--<div id="reference13">
                                            <p class="text_gral_center">
                                               <a href="#" onclick="Popup.show('mensaje13','reference13','above center',{'constrainToScreen':true});;return false;">
                                                  <img src="imagenes/i_peque.png" />
                                               </a>
                                            </p>
                                         </div>--%>
                                </div>

                                <div id="DVDatosFormularioNP">
                                    <div id="DVSalarioBaseCotizacionMensualRNP">
                                        <asp:Label ID="lblSalarioBaseCotizacionMensualNP" runat="server" Text="" CssClass="lblSalarioBaseCotizacionMensualR"></asp:Label>
                                    </div>
                                    <div id="DVSaldoActualAforeRNP">
                                        <asp:Label ID="lblSaldoActualAforeNP" runat="server" Text="" CssClass="lblSaldoActualAforeR"></asp:Label>
                                    </div>
                                    <div id="DVGeneroRNP">
                                        <asp:Image ID="imgGeneroNP" runat="server" CssClass="imgGeneroR" />
                                    </div>
                                    <div id="DVEdadRNP">
                                        <asp:Label ID="lblEdadNP" runat="server" Text="" CssClass="lblEdadR"></asp:Label>
                                    </div>
                                    <div id="DVEdadRetiroRNP">
                                        <asp:Label ID="lblEdadRetiroNP" runat="server" Text="" CssClass="lblEdadRetiroR"></asp:Label>
                                    </div>
                                    <div id="DVAniosCotizadosRNP">
                                        <asp:Label ID="lblAniosCotizadosNP" runat="server" Text="" CssClass="lblAniosCotizadosR"></asp:Label>
                                    </div>
                                    <div id="DVRendimientoRealAntesComisionesRNP">
                                        <asp:Label ID="lblRendimientoRealAntesComisionesNP" runat="server" Text="" CssClass="lblRendimientoRealAntesComisionesR"></asp:Label>
                                    </div>
                                    <%--<div id="DVDensidadCotizacionRNP">
                                 <asp:Label ID="lblDensidadCotizacionNP" runat="server" Text="" CssClass="lblDensidadCotizacionR"></asp:Label>
                              </div>--%>


                                    <div id="DVDensidadCotizacionRNP">
                                        <table>
                                            <tr>
                                                <td style="width: 70px;">
                                                    <asp:Label ID="lblDensidadCotizacionNP" runat="server" Text="" CssClass="lblDensidadCotizacionR"></asp:Label>
                                                </td>

                                                <td>
                                                    <div id="reference15">
                                                        <p class="text_gral_center" style="margin-top: 45px; margin-right: 10px;">
                                                            <a href="#" onclick="Popup.show('mensaje15','reference15','above center',{'constrainToScreen':true});;return false;">
                                                                <img src="imagenes/i_peque.png" style="width: 17px; margin-top: -5px;" />
                                                            </a>
                                                        </p>
                                                    </div>

                                                </td>
                                            </tr>
                                        </table>
                                    </div>


                                    <%--<div id="reference13">
                                            <p class="text_gral_center">
                                               <a href="#" onclick="Popup.show('mensaje13','reference13','above center',{'constrainToScreen':true});;return false;">
                                                  <img src="imagenes/i_peque.png" />
                                               </a>
                                            </p>
                                         </div>--%>
                                </div>

                                <div id="divResumenAll" style="float: left; width: 100%;">
                                    <div id="divResumen" style="width: 100%;">
                                        <div style="float: left; width: 1000px;">
                                            <div id="PNLIzquierdo" style="width: 450px; float: left; margin-right: 5px; margin-left: 5px;">
                                                <div id="DVDatosResultados" class="EncResultados">
                                                    <p>Resultados</p>
                                                </div>
                                                <div id="DVResultadoPMT">
                                                    <div class="DVFondoDatos-100">
                                                        <div style="width: 93%">
                                                            <asp:Label ID="lblSaldoAcumulado" runat="server" Text="" CssClass="lblSaldoAcumuladoR"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="DVFondoDatos-100">

                                                        <div id="reference11">
                                                            <p class="text_gral_center">
                                                                <a href="#" onclick="Popup.show('mensaje11','reference11','above center',{'constrainToScreen':true});;return false;">
                                                                    <img src="imagenes/i_peque.png" style="border: none; width: 55%; margin-top: 25px; margin-left: 10px;" />
                                                                </a>
                                                            </p>
                                                        </div>
                                                        <div style="width: 93%">
                                                            <asp:Label ID="lblPensionMensualEstimada" runat="server" Text="" CssClass="lblPensionMensualEstimadaR"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="DVFondoDatos-100">

                                                        <div id="reference12">
                                                            <p class="text_gral_center">
                                                                <a href="#" onclick="Popup.show('mensaje12','reference12','above center',{'constrainToScreen':true});;return false;">
                                                                    <img src="imagenes/i_peque.png" style="border: none; width: 55%; margin-left: 10px; margin-top: 13px; margin-right: 2px;" />
                                                                </a>
                                                            </p>
                                                        </div>
                                                        <div style="width: 93%">
                                                            <asp:Label ID="lblPorcentajePensionMensual" runat="server" Text="" CssClass="lblPorcentajePensionMensualR"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--//AJN - ODT -07--%>
                                                <div id="DVlblPMG" class="spanNotaPMG">
                                                    <span id="spanNotaPMG">* Tienes derecho a la Pensión Garantizada (PG)                                                         
                                                            <a>
                                                                <img src="imagenes/i_peque.png" id="tooltip" style="border: none; width: 18px; padding-bottom: 4px;" />
                                                            </a>
                                                    </span>
                                                </div>

                                                <div style="" class="Salvaguardia">
                                                    <span><strong>Importante:</strong> Los resultados estimados fueron calculados con base en la información que proporcionaste y tomando en cuenta que sigas contando con el mismo Salario Base de Cotización hasta tu retiro. 
                                                <br />
                                                        Estos datos solo se muestran para fines ilustrativos, por lo que no representan una opinión de carácter oficial, ya que esta estimación de pensión no considera beneficiarios, por lo que el resultado podría variar significativamente en caso de que se tengan. 
                                                <br />
                                                        Rendimientos pasados, no garantizan rendimientos futuros.
                                                    </span>
                                                </div>

                                            </div>



                                            <button id="btnResultados" type="button" data-toggle="modal" data-target="#ModalEncuestaSatisfaccion" style="float: right;">
                                                <i class="bi bi-chat" style="font-size: 40px"></i>
                                                <%--<img src="imagenes/IconsModal/Opinion.png" />--%>
                                            </button>





                                            <div id="PNLDerecho" style="width: 450px; float: left; margin-right: 5px; margin-left: 5px;">
                                                <div id="divDer1" class="EncResultados">

                                                    <div id="reference10-1" class="EncResultados">
                                                        <p class="text_gral_center" style="font-size: 20px; text-align: center; margin-top: 10px">
                                                            Aportaciones y Rendimientos
                                                   <a href="#" onclick="Popup.show('mensaje10-1','reference10-1','above center',{'constrainToScreen':true});;return false;">
                                                       <img src="imagenes/i_peque.png" style="border: none; margin-top: -5px; margin-left: 3px; width: 20px;" />
                                                   </a>
                                                        </p>
                                                    </div>

                                                    <%--                                                    <button type="button" class="btn btn-warning" data-toggle="modal" data-target="#exampleModal" style="float: left; margin-right: 5px; margin-left: 5px;">
                                                        <i class="bi bi-chat-dots"></i>
                                                    </button>--%>
                                                </div>
                                                <div id="divDer2" style="width: 450px; height: 250px; border: none;">
                                                    <canvas id="chartContainer" width="450px" height="250px"></canvas>
                                                </div>
                                                <div id="divDer3" style="width: 100%; height: 80px; border: none; padding-top: 15px;">
                                                    <div style="text-align: center;">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <img src="imagenes/SaldoActual.png" style="border: none; padding-left: 35px;" />
                                                                </td>
                                                                <td>
                                                                    <img src="imagenes/RendimientosNetosFuturos.png" style="border: none; padding-left: 20px;" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblSaldoActual"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblRendimientosFuturos"></asp:Label></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="divOpciones" style="width: 100%; float: left; padding-top: 116px; padding-bottom: 20px;">
                                            <div style="text-align: center; padding-left: 15px; padding-right: 15px; float: none;">
                                                <asp:Image ID="imgCaminoMejorPension" src="imagenes/CaminoMejorPension.png" Style="border: none; cursor: pointer;" runat="server"
                                                    OnClick="javascript:verP2();" class="" />
                                                <asp:Image ID="imgAhorrasSuficiente" src="imagenes/AhorrasLosSuficiente.png" Style="border: none; cursor: pointer;" runat="server"
                                                    OnClick="javascript:verP3();" class="" />
                                                <asp:Image ID="imgPequenosEsfuerzos" src="imagenes/PequenosEsfuerzos.png" Style="border: none; cursor: pointer;" runat="server"
                                                    OnClick="javascript:verP4();" class="" />
                                            </div>
                                        </div>
                                    </div>

                                    <div id="divResumenNP" style="float: left; width: 100%; padding-bottom: 50px;">
                                        <div style="float: left; width: 1000px;">
                                            <div id="PNLIzquierdoNP" style="width: 450px; float: left; margin-right: 5px; margin-left: 5px;">
                                                <div id="DVDatosResultadosNP" class="EncResultados">
                                                    <p>Resultados</p>
                                                </div>
                                                <div id="DVResultadoPMTNP">
                                                    <div class="DVFondoDatos-96">
                                                        <asp:Label ID="lblSaldoAcumuladoNP" runat="server" Text="" CssClass="lblSaldoAcumuladoR"></asp:Label>
                                                    </div>
                                                </div>
                                                <div id="DVDatosTePareceSuficienteNP">
                                                    <p>
                                                        <%--Al no reunir las 1,250 semanas de cotización que pide la Ley del Seguro Social, No tienes derecho a una pensión. No obstante, puedes retirar en una sola exhibición los recursos acumulados a la edad de retiro o bien continuar haciendo aportaciones voluntarias para aumentar tu saldo.--%>
                                             Al no reunir las semanas cotizadas que pide la Ley del Seguro Social de acuerdo al año en que te retires (en 2022 el requisito mínimo es de 775 semanas e incrementa en 25 semanas anualmente, hasta llegar a un mínimo de 1,000 semanas a partir de 2031), podrás recibir el dinero de tu cuenta individual en una sola exhibición (artículos 154 y 162 de la Ley del Seguro Social), o bien, continuar haciendo aportaciones voluntarias para aumentar tu ahorro y si reúnes lo suficiente, tú mismo/misma podrías contratar una pensión.
                                                    </p>
                                                </div>
                                            </div>





                                            <div id="PNLDerechoNP" style="width: 450px; float: left; margin-right: 5px; margin-left: 5px;">
                                                <div id="divDer1NP" class="EncResultados">
                                                    <div id="reference10" class="EncResultados">
                                                        <p class="text_gral_center" style="font-size: 20px; text-align: center;">
                                                            Aportaciones y Rendimientos
                                                   <a href="#" onclick="Popup.show('mensaje10','reference10','above center',{'constrainToScreen':true});;return false;">
                                                       <img src="imagenes/i_peque.png" style="border: none; margin-top: -3px; margin-left: 3px; width: 20px;" />
                                                   </a>
                                                        </p>
                                                    </div>
                                                </div>
                                                <div id="divDer2NP" style="width: 450px; height: 186px; border: none 1px solid black;">
                                                    <canvas id="Canvas1" width="450px" height="186px"></canvas>
                                                </div>
                                                <div id="divDer3NP" style="width: 100%; height: 80px;">
                                                    <div style="text-align: center;">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <img src="imagenes/SaldoActual.png" style="border: none; padding-left: 35px;" />
                                                                </td>
                                                                <td>
                                                                    <img src="imagenes/RendimientosNetosFuturos.png" style="border: none; padding-left: 35px;" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblSaldoActualNP"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblRendimientoFuturoNP"></asp:Label></td>
                                                            </tr>
                                                        </table>
                                                        <%--<a id="aP1SaldoActualNP" runat="server" href="#" onclick="Popup.show('mensaje11','reference11','above center',{'constrainToScreen':true});;return false;">
                                                                <img src="imagenes/SaldoActual.png" style="border: none;" />
                                                            </a>
                                                            <a id="aP1RendimientosFuturosNP" runat="server" href="#" onclick="Popup.show('mensaje11','reference11','above center',{'constrainToScreen':true});;return false;">
                                                                <img src="imagenes/RendimientosNetosFuturos.png" style="border: none;" />
                                                            </a>--%>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>

                                        <div id="divP2NP" style="float: left; width: 100%; margin-bottom: 10px;" class="">
                                            <div id="DVResultadoPMT_2NP"></div>
                                            <div id="NPParteFormula"></div>
                                            <div style="text-align: center; width: 100%">
                                            </div>

                                            <div id="DVResultadoAVNP" style="text-align: center; width: 100%">
                                                <!-- Aqui se crea dinamicamente la tabla -->
                                            </div>
                                            <div class="NotasAlPie" style="">
                                                <div style="text-align: left; width: 700px; margin-left: auto; margin-right: auto;">
                                                    <sup>1/</sup> Este es el ahorro del que podrás disponer al momento del retiro, si decides hacer ahorro voluntario.
                                                </div>
                                            </div>
                                            <div id="DVResultadoAVNP_ODT5"></div>
                                        </div>

                                        <div id="divOpcionesNP" style="width: 100%; float: left; padding-top: 90px; padding-bottom: 20px;">
                                            <div style="text-align: center;">
                                                <asp:Image ID="imgEsfuerzosNP" src="imagenes/PequenosEsfuerzos.png" Style="border: none; cursor: pointer;" runat="server"
                                                    OnClick="javascript:verP4();" class="AlineaBtnOpciones" />
                                            </div>
                                        </div>
                                    </div>

                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 50%; vertical-align: top;">
                                                <a href="http://www.gob.mx/consar/acciones-y-programas/calculadoras-de-ahorro-y-retiro?idiom=es" class="RegresaInicio">
                                                    <div class="RegresaInicio" style="float: left;"></div>
                                                </a>
                                            </td>
                                            <td style="width: 50%;">
                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/otro_ejerc1.png" onmouseover="this.src='imagenes/otro_ejerc2.png'" onmouseout="this.src='imagenes/otro_ejerc1.png'"
                                                    CssClass="btnCalcular cursorHand submit" OnClick="javascript:OtroCalculo();" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>




                                <div id="divP2" style="float: left; width: 100%; margin-bottom: 80px; margin-top: 30px;" class="panelOculto">


                                    <div style="text-align: right; padding-top: 10px;">
                                        <button id="btnMejorPension" type="button" data-toggle="modal" data-target="#ModalEncuestaSatisfaccion" style="float: right;">
                                            <i class="bi bi-chat" style="font-size: 40px"></i>
                                        </button>
                                    </div>

                                    <div id="divP2PME" style="text-align: center; width: 500px; margin: auto; float: none; background-repeat: no-repeat;">

                                        <div style="width: 75%; text-align: right; padding-top: 10px;">
                                            <asp:Label ID="lblP2PME" runat="server" Text="" CssClass="EtiquetaTotal " Style="margin-top: auto; margin-bottom: auto;"></asp:Label>


                                        </div>


                                    </div>
                                    <div id="divP2PD" style="width: 600px; margin: auto; float: none; height: 70px; margin-top: 20px;">

                                        <table style="width: 100%;">
                                            <tr>
                                                <td rowspan="2" style="width: 200px;">
                                                    <label for="txtP2PD" class="CTRLGeneral" style="margin-top: auto; margin-bottom: auto;">Pensión Deseada</label>
                                                </td>
                                                <td>
                                                    <div style="vertical-align: bottom;">
                                                        <div id="divSliderN" style="width: 90%; margin-top: 20px;"></div>
                                                        <input id="txtP2PD" name="txtP2PD" class="CTRLGeneral validate[required,funcCall[valida_salario_cero], funcCall[valida_salario_minimo]]"
                                                            type="text" onblur="javascript:asignarValorSalarioMensual(this);"
                                                            maxlength="10" style="display: none; transparent; border: none;" runat="server" />
                                                        <div id="slider-txtP2PD" class="CTRLGeneral slidergral" style="display: none; width: 150px; margin-top: 5px; margin-left: 10px;"></div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table style="width: 90%">
                                                        <tr>
                                                            <td style="text-align: left; width: 30%;">
                                                                <asp:Label ID="txtP2PDIzq" runat="server" class="EtiquetaTotalTick " Style="text-align: left;">1</asp:Label>
                                                                <asp:Label ID="txtP2PDIzqNP" runat="server" class="EtiquetaTotalTick " Style="text-align: left; display: none;">1</asp:Label>
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <asp:Label ID="txtP2PDCen" runat="server" class="EtiquetaTotalTick " Style="width: 100%;">2</asp:Label>
                                                                <asp:Label ID="txtP2PDCenNP" runat="server" class="EtiquetaTotalTick " Style="width: 100%; display: none;">2</asp:Label>
                                                            </td>
                                                            <td style="text-align: right; width: 30%;">
                                                                <asp:Label ID="txtP2PDDer" runat="server" class="EtiquetaTotalTick " Style="text-align: right;">3</asp:Label>
                                                                <asp:Label ID="txtP2PDDerNP" runat="server" class="EtiquetaTotalTick " Style="text-align: right; display: none;">3</asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>

                                    </div>
                                    <div style="width: 670px; height: 350px; margin: 10px auto; float: none;">
                                        <div style="width: 100%; height: 350px; border: none;">
                                            <div class="G2Leyenda">
                                                <img src="imagenes/G2-Leyendas.png" />
                                            </div>
                                            <div style="margin-left: 30%; margin-top: 40px;">
                                                <span style="font-size: medium"><strong>Cifras estimadas en miles de pesos</strong></span>
                                            </div>
                                            <canvas id="myChart2" width="650" height="260" class="cursorHand"></canvas>
                                            <span style="margin-left: 50%; font-family: Arial;">Edad</span>
                                        </div>
                                        <%--<span style="margin-left:50%; font-family:Arial;">Edad</span>--%>
                                        <br />
                                        <%--<span style="font-size: small"><strong>Nota:</strong>Cifras estimadas en miles de pesos</span>--%>
                                    </div>
                                    <br />
                                    <br />
                                    <div style="width: 85%; margin: 15px auto; float: none; text-align: center; margin-top: 60px;">
                                        <%--<span>Para obtener la pensión deseada, a los <strong>
                                    <asp:Label ID="lblP2Edad" runat="server" Text="40 años" CssClass=""></asp:Label>
                                 </strong>&nbsp;deberías tener un saldo pensionario
                                                     de <strong>
                                                        <asp:Label ID="lblP2SaldoPensionario" runat="server" Text="$346.15" CssClass=""></asp:Label>
                                                     </strong>. Lo que representa un déficit en tu ahorro de <strong>
                                                        <asp:Label ID="lblP2Deficit" runat="server" Text="$91,155.23" CssClass=""></asp:Label>
                                                     </strong>.
                                 </span>--%>

                                        <%-- PGO - Modificacion de leyenda de pensión deseada --%>
                                        <span>Para obtener la pensión deseada de <strong>
                                            <asp:Label ID="lblP2PMECopia" runat="server" CssClass=""></asp:Label></strong>, a los 
                                      <strong>
                                          <asp:Label ID="lblP2Edad" runat="server" Text="" CssClass=""></asp:Label></strong>&nbsp;
                                            deberás tener un saldo pensionario de <strong>
                                                <asp:Label ID="lblP2SaldoPensionario" runat="server" Text="" CssClass=""></asp:Label></strong>.
                                            Por lo que te haría falta ahorrar de manera
                                            <br />
                                            adicional en tu AFORE 
                                      <strong>
                                          <asp:Label ID="lblP2Deficit" runat="server" Text="" CssClass=""></asp:Label></strong>.
                                        </span>

                                    </div>
                                    <div style="width: 100%; float: none; text-align: left; margin-top: 15px;">
                                        <table style="width: 100%; margin-bottom: 25px;">
                                            <tr>
                                                <td style="width: 30px;">
                                                    <asp:Image ID="imgP2Regresar" runat="server" ImageUrl="imagenes/Regresar.png"
                                                        onmouseover="this.src='imagenes/Regresar2.png'"
                                                        onmouseout="this.src='imagenes/Regresar.png'"
                                                        CssClass="boton texto cursorHand" OnClick="javascript:regresarResumen();" /><br />
                                                </td>
                                                <td>
                                                    <div style="margin-left: 15px;" class="Salvaguardia">
                                                        <span><strong>Importante:</strong> Los resultados estimados fueron calculados con base en la información que proporcionaste y tomando en cuenta que sigas contando con el mismo Salario Base de Cotización hasta tu retiro.
                                                <br />
                                                            Estos datos solo se muestran para fines ilustrativos, por lo que no representan una opinión de carácter oficial, ya que esta estimación de pensión no considera beneficiarios, por lo que el resultado podría variar significativamente en caso de que se tengan. 
                                                <br />
                                                            Rendimientos pasados, no garantizan rendimientos futuros.
                                                        </span>
                                                    </div>


                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>

                                <div id="divP3" style="float: left; margin-bottom: 80px;" class="panelOculto">
                                    <div id="DVResultadoPMT_2" style="display: none;">
                                    </div>
                                    <div id="DVPMG">
                                        <asp:Label ID="lblMejorarPension" runat="server" Text="" CssClass="leftCal lblMejorarPension"></asp:Label>
                                    </div>

                                    <table style="width: 100%; margin-top: 20px; margin-bottom: 45px;">
                                        <tr>
                                            <td style="width: 300px;">
                                                <div style="" class="Titulo">
                                                    <asp:Label ID="Label2" runat="server" Text="¿Cuánto quieres para tu pensión?"></asp:Label>
                                                </div>
                                            </td>
                                            <td style="width: 30px;"></td>
                                            <td style="">
                                                <div style="padding-top: 12px;" class="Titulo">
                                                    <asp:Label ID="Label3" runat="server" Text="¿De cuánto quieres tu pensión?"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Cuerpo Pie" style="vertical-align: top;">
                                                <div id="DVResultadoAV" style="width: 100%; height: 100%;">
                                                    <span id="tblAhorrar"></span>
                                                </div>
                                            </td>
                                            <td></td>
                                            <td class="Cuerpo Pie">
                                                <div id="DVGraficaContenedor" style="float: right;">
                                                    <table style="width: 100%; height: 350px;">
                                                        <tr>
                                                            <td colspan="2">
                                                                <div style="float: left; margin-left: 20%">
                                                                    <div id="slider1" style="float: left; width: 200px; margin-left: 30px; margin-top: 20px;"></div>
                                                                    <input disabled name="amount1" type="text" id="amount1" style="border: 0; color: #000; font-weight: bold; font-size: 16px; width: 70px; margin-left: 15px; text-align: center; background-color: transparent; position: relative; top: 14px;" class="Sombra" />
                                                                    <!--                                                                        <p style="float: left;">
                                                                            <label for="amount1"></label>
                                                                        </p> -->
                                                                </div>
                                                            </td>
                                                            <%--<td>
                                                   <div style="width: 100%;" class="TituloEti">
                                                      <asp:Label ID="Label4" runat="server" class="" Style="padding: 0px; margin: 0px;" Text="Porcentaje de tu salario actual"> </asp:Label>
                                                   </div>
                                                </td>--%>
                                                        </tr>


                                                        <tr>
                                                            <td style="width: 280px;">
                                                                <input name="amount31" type="text" id="amount31" class="porcentaje1" value="" readonly="" style="width: 100%; margin-top: 40px; margin-left: 28px;" />
                                                                <div class="dvGrafica1">
                                                                    <div id="barraGrafica1" class="barraGrafica-Termometro1">
                                                                        <div id="barraGraficaProgreso1" class="barraGraficaProgreso30">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div style="width: 100%">
                                                                    <div class="Nube" style="width: 70%;">
                                                                        PENSIÓN SIN AHORRO VOLUNTARIO
                                                                        <asp:Label ID="txtP3PensionSinAhorro" runat="server" Text="" />
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td style="width: 254px;">
                                                                <input name="amount3" type="text" id="amount3" class="porcentaje" readonly="" style="width: 100%;" />
                                                                <div class="dvGrafica2">
                                                                    <input name="txtPensionFlotante" type="text" id="txtPensionFlotante" class="pension pensionFlotante" readonly="" style="display: none;" />
                                                                    <div id="barraGrafica" class="barraGrafica">
                                                                        <div id="barraGraficaProgreso" class="barraGraficaProgreso30-Termometro">
                                                                            <!--<asp:Image ID="imgProgresoBarra" runat="server" ImageUrl="imagenes/indicador_grafica.png" CssClass="imgProgresoBarra" />-->
                                                                        </div>
                                                                    </div>
                                                                </div>


                                                                <table>
                                                                    <tr style="width: 100%">
                                                                        <td>
                                                                            <div class="Nube" style="width: 76%;">
                                                                                ¿QUIERES UNA PENSIÓN DE
                                                                                    <asp:Label ID="txtP3PensionConAhorro" runat="server" Text="" />
                                                                                ?
                                                                            </div>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>
                                                                            <div id="DVAhorroAdicional">
                                                                                Al mes tendrías que ahorrar voluntariamente en tu AFORE:
                                                                    
                                                                            </div>

                                                                            <div class="Nube2">
                                                                                <asp:Label ID="amount2" runat="server" Text="" CssClass=" "></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>

                                                            </td>
                                                            <%--<div style="width: 100%">
                                                      <div class="Nube" style="width: 76%;">
                                                         ¿QUIERES UNA PENSIÓN DE
                                                                            <asp:Label ID="txtP3PensionConAhorro" runat="server" Text="" />
                                                          ?
                                                      </div>
                                                   </div>
                                                   <div id="DVAhorroAdicional">
                                                      Al mes tendrías que ahorrar voluntariamente en tu AFORE:
                                                        <div class="Nube2">
                                                            <asp:Label ID="amount2" runat="server" Text="" CssClass=" "></asp:Label>
                                                        </div>
                                                   </div>--%>





                                                            <%-- <td style="vertical-align: top;">
                                                   
                                                   <div style="margin-top: 20px; display: none;">
                                                      <img id="imgBarra" src="imagenes/barra.png" alt="Calidad Pensión" />
                                                      <img id="imgFlecha" class="flechaIndicadorBaja" src="imagenes/flecha.png" alt="Indicador Calidad Pensión" />
                                                   </div>
                                                   <div style="width: 203px; height: 368px; margin-top: 50px; position: relative;">
                                                      <asp:Image ID="imgBarraTasa" runat="server" ImageUrl="imagenes/barra_tasa_vertical.png" />
                                                      <asp:Image ID="imgIndicadorTasa" ClientIDMode="Static" runat="server" ImageUrl="imagenes/boton_barra.png" CssClass="indicadorBarra indicadorBarra30" />
                                                   </div>
                                                </td>--%>
                                                        </tr>


                                                    </table>
                                                </div>
                                            </td>
                                            <td style="width: 30px; vertical-align: top;"></td>
                                            <td class="panelVisible">
                                                <div style="text-align: right; padding-top: 10px;">
                                                    <button id="btnSuficienteRetiro" type="button" data-toggle="modal" data-target="#ModalEncuestaSatisfaccion" style="float: right;">
                                                        <i class="bi bi-chat" style="font-size: 40px"></i>
                                                    </button>
                                                </div>
                                            </td>
                                        </tr>
                                        <%-- PGO Agregar leyenda de "Porcentaje de tu salario actual" como pie de página --%>
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td colspan="2">
                                                <p style="margin-left: 5%; margin-top: 1em; margin-bottom: 1em; margin-right: 0; font-size: initial;"><strong>*Tasa de reemplazo:</strong> porcentaje de tu salario actual que tendr&aacute;s disponible a manera de pensi&oacute;n cuando te retires.</p>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="">
                                                <asp:Image ID="imgP3Regresar" runat="server" ImageUrl="~/imagenes/Regresar.png"
                                                    onmouseover="this.src='imagenes/Regresar2.png'"
                                                    onmouseout="this.src='imagenes/Regresar.png'"
                                                    CssClass="boton texto cursorHand" OnClick="javascript:regresarResumen();" />
                                            </td>
                                            <td colspan="2">
                                                <div id="Div1" class="Salvaguardia" style="margin: 0px; width: 100%;">
                                                    <strong>Importante:</strong> Los resultados estimados fueron calculados con base en la información que proporcionaste y tomando en cuenta que sigas contando con el mismo Salario Base de Cotización hasta tu retiro.
                                                <br />
                                                    Estos datos solo se muestran para fines ilustrativos, por lo que no representan una opinión de carácter oficial, ya que esta estimación de pensión no considera beneficiarios, por lo que el resultado podría variar significativamente en caso de que se tengan. 
                                                <br />
                                                    Rendimientos pasados, no garantizan rendimientos futuros.
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="DVResultadoAV_ODT100"></div>
                                    <div id="DVNotaAhorro" class="NotasNP">
                                        <sup>1/</sup>Este es ahorro del que podrás disponer al momento del retiro, si decides hacer ahorro voluntario.
                                                 <div id="DVNota_ODT5">
                                                     Nota: Para el cáculo de acumulación por ahorro voluntario se tomó en cuenta la misma densidad de cotización proporcionada por el trabajador.
                                                 </div>
                                    </div>
                                </div>

                                <div id="divP4" style="float: left; width: 100%; margin-bottom: 80px;" class="panelOculto">
                                    <div id="divP4Resumen" style="text-align: left; width: 448px; margin: auto; float: none;">
                                        <div id="divP4ResumenTit" class="EncResultados">
                                            <p>Resultados</p>
                                        </div>
                                        <div id="divP4ResumenSaldo" style="" class="divSAAR">
                                            <div style="text-align: right; padding-top: 10px; padding-right: 15px;">
                                                <asp:Label ID="lblP4Saldo" runat="server" Text="" CssClass="EtiquetaTotal "></asp:Label>
                                                <%--<a href="#" onclick="Popup.show('mensaje15','reference15','above center',{'constrainToScreen':true});;return false;">
                                          <img src="imagenes/i_peque.png" />
                                       </a>--%>
                                            </div>
                                        </div>
                                        <div id="divP4ResumenPension" style="" class="divPME">
                                            <div style="text-align: right; padding-top: 10px; padding-right: 15px;">
                                                <asp:Label ID="lblP4Pension" runat="server" Text="" CssClass="EtiquetaTotal "></asp:Label>
                                                <div id="reference13">
                                                    <p class="text_gral_center">
                                                        <a href="#" onclick="Popup.show('mensaje13','reference13','above center',{'constrainToScreen':true});;return false;">
                                                            <img src="imagenes/i_peque.png" style="width: 75%; margin-top: 16px;" />
                                                        </a>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <table style="width: 100%">
                                        <tr>
                                            <td colspan="7">
                                                <div class="GastoHTitulo">¿Alguna vez has notado el impacto del gasto hormiga?</div>
                                                <div class="GastoHTexto">Observa qué pasaría si disminuyes al mes el consumo de algunos de los productos mostrados (selecciona la cantidad de cada uno de ellos): </div>
                                            </td>
                                            <td>
                                                <div style="text-align: right; padding-top: 10px;">
                                                    <button id="btnPequenosEsfuerzos" type="button" data-toggle="modal" data-target="#ModalEncuestaSatisfaccion">
                                                        <i class="bi bi-chat" style="font-size: 40px"></i>
                                                        <%--<img src="imagenes/IconsModal/Opinion.png" />--%>
                                                    </button>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr style="border-radius: 4px 4px; margin: 15px; text-align: center;">
                                            <td style="width: 14%"></td>
                                            <td id="divCigarros" style="width: 14%" class="GastoH">
                                                <div style="width: 100%; text-align: center;">
                                                    <asp:Label ID="lblCig" runat="server" Text="" Style="width: 100%; text-align: center;"></asp:Label>
                                                </div>
                                                <asp:Image ID="imgCig" runat="server" ImageUrl="~/imagenes/Cigarros.png" CssClass="" OnClick="" />
                                                <div class="PiezasH">
                                                    <div class="CIzq"></div>
                                                    <div class="CCen">
                                                        <asp:Label ID="lblCigPza" runat="server" Text="2"></asp:Label>
                                                    </div>
                                                    <div class="CDer"></div>
                                                </div>
                                            </td>
                                            <td id="divCerveza" style="width: 14%" class="GastoH">
                                                <div style="width: 100%; text-align: center;">
                                                    <asp:Label ID="lblCer" runat="server" Text="" Style="width: 100%; text-align: center;"></asp:Label>
                                                </div>
                                                <asp:Image ID="imgCer" runat="server" ImageUrl="~/imagenes/Cerveza.png" CssClass="" OnClick="" />
                                                <div class="PiezasH">
                                                    <div class="CIzq"></div>
                                                    <div class="CCen">
                                                        <asp:Label ID="lblCerPza" runat="server" Text="2" CssClass=""></asp:Label>
                                                    </div>
                                                    <div class="CDer"></div>
                                                </div>
                                            </td>
                                            <td id="divFastFood" style="width: 14%" class="GastoH">
                                                <div style="width: 100%; text-align: center;">
                                                    <asp:Label ID="lblCom" runat="server" Text="" Style="width: 100%; text-align: center;"></asp:Label>
                                                </div>
                                                <asp:Image ID="imgCom" runat="server" ImageUrl="~/imagenes/FastFood.png" CssClass="" OnClick="" />
                                                <div class="PiezasH">
                                                    <div class="CIzq"></div>
                                                    <div class="CCen">
                                                        <asp:Label ID="lblComPza" runat="server" Text="2" CssClass=""></asp:Label>
                                                    </div>
                                                    <div class="CDer"></div>
                                                </div>
                                            </td>
                                            <td id="divCafe" style="width: 14%" class="GastoH">
                                                <div style="width: 100%; text-align: center;">
                                                    <asp:Label ID="lblCaf" runat="server" Text="" Style="width: 100%; text-align: center;"></asp:Label>
                                                </div>
                                                <asp:Image ID="imgCaf" runat="server" ImageUrl="~/imagenes/Cafe.png" CssClass="" OnClick="" />
                                                <div class="PiezasH">
                                                    <div class="CIzq"></div>
                                                    <div class="CCen">
                                                        <asp:Label ID="lblCafPza" runat="server" Text="2" CssClass=""></asp:Label>
                                                    </div>
                                                    <div class="CDer"></div>
                                                </div>
                                            </td>
                                            <td id="divCine" style="width: 14%" class="GastoH">
                                                <div style="width: 100%; text-align: center;">
                                                    <asp:Label ID="lblCin" runat="server" Text=""></asp:Label>
                                                </div>
                                                <asp:Image ID="imgCin" runat="server" ImageUrl="~/imagenes/Cine.png" CssClass="" OnClick="" />
                                                <div class="PiezasH">
                                                    <div class="CIzq"></div>
                                                    <div class="CCen">
                                                        <asp:Label ID="lblCinPza" runat="server" Text="2" CssClass=""></asp:Label>
                                                    </div>
                                                    <div class="CDer"></div>
                                                </div>
                                            </td>
                                            <td style="width: 14%"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="7" style="text-align: center;">
                                                <div id="divGHAhorro" style="">
                                                    Podrías ahorrar al mes aproximadamente:
                                           <asp:Label ID="lblGHAhorro" runat="server" Text="" CssClass="EtiquetaTotalGH" Style="color: white;"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7" style="text-align: center;">
                                                <div class="GHInvertir">
                                                    <div id="GHInvertir1" class="panelOculto">
                                                        <div id="divGHR2" class="">
                                                            Si este monto lo inviertes en tu AFORE obtendrías a la edad de retiro
                                                               <asp:Label ID="lblGHEdadRetiro" runat="server" Text="" CssClass="EtiquetaTotalGH" Style="float: none;"></asp:Label>
                                                        </div>
                                                        <div class="">
                                                            de manera adicional. De esta manera elevarías tu pensión mensual a
                                                               <asp:Label ID="lblGHPensionRetiro" runat="server" Text="" CssClass="EtiquetaTotalGH" Style="float: none;"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div id="GHInvertir2" class="panelOculto">
                                                        <div id="div3" class="">
                                                            Si este monto lo inviertes en tu AFORE obtendrías
                                                        </div>
                                                        <div class="">
                                                            a la edad de retiro
                                                               <asp:Label ID="lblGHEdadRetiro2" runat="server" Text="" CssClass="EtiquetaTotalGH" Style="float: none;"></asp:Label>
                                                            de manera adicional
                                                        </div>
                                                    </div>
                                                    <div id="GHInvertir3" class="panelOculto">
                                                        <div id="div4" class="">
                                                            Si este monto lo inviertes en tu AFORE obtendrías a la edad de retiro
                                                               <asp:Label ID="lblGHEdadRetiro3" runat="server" Text="" CssClass="EtiquetaTotalGH" Style="float: none;"></asp:Label>
                                                        </div>
                                                        <div class="">
                                                            de manera adicional. Es decir, tendrías un ahorro total de 
                                                               <asp:Label ID="lblGHPensionRetiro3" runat="server" Text="" CssClass="EtiquetaTotalGH" Style="float: none;"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr id="trRegreso">
                                            <td colspan="2" style="">
                                                <asp:Image ID="imgPRegreso" runat="server" ImageUrl="~/imagenes/Regresar.png"
                                                    onmouseover="this.src='imagenes/Regresar2.png'"
                                                    onmouseout="this.src='imagenes/Regresar.png'"
                                                    CssClass="boton texto cursorHand" OnClick="javascript:regresarResumen();" />
                                            </td>
                                            <td colspan="5">
                                                <div id="divP4Salvaguardia" class="Salvaguardia" style="margin: 0px; width: 100%; margin-bottom: 25px;">
                                                    <strong>Importante:</strong> Los resultados estimados fueron calculados con base en la información que proporcionaste y tomando en cuenta que sigas contando con el mismo Salario Base de Cotización hasta tu retiro.
                                                <br />
                                                    Estos datos solo se muestran para fines ilustrativos, por lo que no representan una opinión de carácter oficial, ya que esta estimación de pensión no considera beneficiarios, por lo que el resultado podría variar significativamente en caso de que se tengan. 
                                                <br />
                                                    Rendimientos pasados, no garantizan rendimientos futuros.
                                                </div>
                                            </td>
                                        </tr>

                                        <%--<tr id="trInicio" class="panelOculto">
                                    <td colspan="7">
                                       <table style="width: 100%;">
                                          <tr>
                                             <td style="width: 50%; vertical-align: top;">
                                                <a href="http://www.gob.mx/consar/acciones-y-programas/calculadoras-de-ahorro-y-retiro?idiom=es" class="RegresaInicio"></a>
                                             </td>
                                             <td style="width: 50%;">
                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/imagenes/otro_ejerc1.png" onmouseover="this.src='imagenes/otro_ejerc2.png'" onmouseout="this.src='imagenes/otro_ejerc1.png'"
                                                   CssClass="btnCalcular cursorHand submit" OnClick="javascript:OtroCalculo2();" />
                                             </td>
                                          </tr>
                                       </table>
                                    </td>
                                 </tr>--%>
                                    </table>

                                    <table style="width: 100%;" id="trInicio" class="panelOculto">
                                        <tr>
                                            <td style="width: 100%; vertical-align: top;">
                                                <a href="http://www.gob.mx/consar/acciones-y-programas/calculadoras-de-ahorro-y-retiro?idiom=es" class="RegresaInicio">
                                                    <div class="RegresaInicio" style="float: left;"></div>
                                                </a>
                                            </td>
                                            <td>
                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/imagenes/otro_ejerc1.png" onmouseover="this.src='imagenes/otro_ejerc2.png'" onmouseout="this.src='imagenes/otro_ejerc1.png'"
                                                    CssClass="btnCalcular cursorHand submit" OnClick="javascript:OtroCalculo2();" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                            </div>
                        </div>
                        <!-- FIN CONTENIDO CALCULADORA -->
                    </form>
                </div>


                <!-- INICIO VALORES INICIALES -->
                <script src="js/util.js"></script>

                <script>
                    //$('#txtSaldoActualAfore').val("");
                    //var valtxtSaldoActualAfore = formatCurrency(document.getElementById('txtSaldoActualAfore').value);
                    //$("#txtSaldoActualAfore").val(valtxtSaldoActualAfore);

                    //$('#txtFechaNacimiento').val("21/07/1986");

                    //edad(document.getElementById('txtFechaNacimiento').value);


                    function OtroCalculo() {
                        //location.reload();

                        limpiarLabels();
                        //MC
                        ocultarDivCarga();
                        AniosCotizados();

                        if (tipoCaso == "A4" || tipoCaso == "A3") {
                            $("#divP4Resumen").removeClass("panelOculto");
                            $("#GHInvertir1").removeClass("panelOculto");
                            $("#GHInvertir2").addClass("panelOculto");
                        }

                        tipoCaso = "";
                        $("#DVContenedorResultado").removeClass("panelVisible").addClass("panelOculto");
                        $("#DVContenedorCalculadora").removeClass("panelOculto").addClass("panelVisible");
                        $("#DVContenedorResultadoNP").removeClass("panelVisible").addClass("panelOculto");
                    }

                    function OtroCalculo2() {
                        //location.reload();
                        //MC
                        limpiarLabels();
                        ocultarDivCarga();
                        AniosCotizados();

                        tipoCaso = "";
                        $("#DVContenedorResultado").removeClass("panelVisible").addClass("panelOculto");
                        $("#DVContenedorCalculadora").removeClass("panelOculto").addClass("panelVisible");
                        $("#DVContenedorResultadoNP").removeClass("panelVisible").addClass("panelOculto");

                        $("#divResumenAll").removeClass("panelOculto");
                        $("#divP4").removeClass("panelOculto");
                        $("#divP4Resumen").removeClass("panelOculto");
                        $("#GHInvertir1").removeClass("panelOculto");
                        $("#GHInvertir2").addClass("panelOculto");
                        $("#trRegreso").removeClass("panelOculto");
                        $("#trInicio").addClass("panelOculto");
                    }

                </script>
                <!-- FIN VALORES INICIALES -->

                <!-- Inicio mascara semanas cotizadas -->
                <script>
                    var elem = document.getElementById("txtSemanasCotizadas");

                    if (elem) {
                        elem.addEventListener("keydown", function (event) {
                            var key = event.which;
                            if ((key < 48 || key > 57) && (key < 96 || key > 105) && key != 8) event.preventDefault();
                        });

                        elem.addEventListener("keyup", function (event) {
                            var value = this.value.replace(/,/g, "");
                            this.dataset.currentValue = parseInt(value);
                            var caret = value.length - 1;
                            while ((caret - 3) > -1) {
                                caret -= 3;
                                value = value.split('');
                                value.splice(caret + 1, 0, ",");
                                value = value.join('');
                            }
                            this.value = value;
                        });
                    }
                </script>
                <!-- Fin mascara semanas cotizadas -->
                <%--<script type="text/javascript" src="http://code.jquery.com/qunit/qunit-1.11.0.js"></script>

                    
    <script src="jQuery-Mask-Plugin-master/test/sinon-1.10.3.js"></script>

    <script src="jQuery-Mask-Plugin-master/test/sinon-qunit-1.0.0.js"></script>

    <script src="jQuery-Mask-Plugin-master/src/jquery.mask.js"></script>

    <script src="jQuery-Mask-Plugin-master/test/jquery.mask.test.js"></script>--%>
            </div>

        </div>
    </div>

    <%-- </div>--%>

    <%--Inicia Mantenimiento--%>
    <%--<footer class="main-footer"><div class="list-info"><div class="container"><div class="row"><div class="col-sm-3"><img data-v-9e928f9a="" src="https://framework-gb.cdn.gob.mx/landing/img/logoheader.svg" href="/" alt="logo gobierno de méxico" class="logo_footer" style="max-width: 90%;"></div><div class="col-sm-3"><h5>Enlaces</h5><ul><li><a href="https://participa.gob.mx" target="_blank" rel="noopener" title="Enlace abre en ventana nueva">Participa</a></li><li><a href="https://www.gob.mx/publicaciones" target="_blank" rel="noopener" title="Enlace abre en ventana nueva">Publicaciones Oficiales</a></li><li><a href="http://www.ordenjuridico.gob.mx" target="_blank" rel="noopener" title="Enlace abre en ventana nueva">Marco Jurídico</a></li><li><a href="https://consultapublicamx.inai.org.mx/vut-web/" target="_blank" rel="noopener" title="Enlace abre en ventana nueva">Plataforma Nacional de Transparencia</a></li></ul></div><div class="col-sm-3"><h5>¿Qué es gob.mx?</h5><p>Es el portal único de trámites, información y participación ciudadana. <a href="https://www.gob.mx/que-es-gobmx">Leer más</a></p><ul><li><a href="https://datos.gob.mx">Portal de datos abiertos</a></li><li><a href="https://www.gob.mx/accesibilidad">Declaración de accesibilidad</a></li><li><a href="https://www.gob.mx/privacidadintegral">Aviso de privacidad integral</a></li><li><a href="https://www.gob.mx/privacidadsimplificado">Aviso de privacidad simplificado</a></li><li><a href="https://www.gob.mx/terminos">Términos y Condiciones</a></li><li><a href="https://www.gob.mx/terminos#medidas-seguridad-informacion">Política de seguridad</a></li><li><a href="https://www.gob.mx/sitemap">Mapa de sitio</a></li></ul></div><div class="col-sm-3"><h5><a href="https://www.gob.mx/tramites/ficha/presentacion-de-quejas-y-denuncias-en-la-sfp/SFP54">Denuncia contra servidores públicos</a></h5><h5>Síguenos en</h5><ul class="list-inline"><li><a class="social-icon facebook" target="_blank" rel="noopener" title="Enlace abre en ventana nueva" href="https://www.facebook.com/gobmexico" aria-label="Facebook de presidencia"></a></li><li><a class="social-icon twitter" target="_blank" rel="noopener" title="Enlace abre en ventana nueva" href="https://twitter.com/GobiernoMX" aria-label="Twitter de presidencia"></a></li></ul></div></div></div></div><div class="container-fluid footer-pleca"><div class="row"><div class="col"><br><br></div></div></div></footer>--%>

    <%--Fin Mantenimiento--%>


    <script src="https://framework-gb.cdn.gob.mx/gobmx.js"></script>

    <script src="js/ModalJS/ModalEncuesta.js"></script>
</body>
</html>

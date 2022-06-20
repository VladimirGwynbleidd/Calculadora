<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalculadoraIndi.aspx.cs" Inherits="CALCULADORA2014.CalculadoraIndi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

        <link href="https://framework-gb.cdn.gob.mx/favicon.ico" rel="shortcut icon" />
    <link href="http://framework-gb.cdn.gob.mx/assets/styles/main.css" rel="stylesheet" />
    <link href="http://www.gob.mx/cms/application.css" rel="stylesheet" />
    <title>Calculadora Independiente</title>
    <!-- INICIO GENERAL -->
    <link href="css/StyleCalcInde.css" rel="stylesheet" />
    <link href="css/validate/template.css" rel="stylesheet" />
    <%--  <script src="//code.jquery.com/jquery-1.10.2.js"></script>--%>

    <link href="tooltipster-master/css/tooltipsterCalcInde.css" rel="stylesheet" />
    <script src="js/jquery-1.7.2.js"></script>
    <script src="tooltipster-master/js/jquery.tooltipster.min.js"></script>

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



    <!-- INICIO FORMATO MONEDA -->
    <script src="jquery.formatCurrency-1.4.0/jquery.formatCurrency-1.4.0.js"></script>
    <script src="js/moneda.js"></script>
    <!-- FIN FORMATO MONEDA -->



    <!-- INICIO SLIDERS -->
    <link rel="stylesheet" href="http://localhost:50359/code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css" />
    <link href="css/Sliders.css" rel="stylesheet" />
    <script src="js/jquery-ui.js"></script>
    <%--<script src="//code.jquery.com/ui/1.11.1/jquery-ui.js"></script>--%>
    <%--<script src="js/sliders.js"></script>--%>
    <!-- FIN SLIDERS -->


    <!-- INICIO MENSAJE EMERGENTE INFORMACION -->
    <link href="css/Popup.css" rel="stylesheet" />
    <script src="js/mensaje/main.js"></script>
    <script src="js/mensaje/shCore.js"></script>
    <script src="js/mensaje/shBrushJScript.js"></script>
    <script>        $(function () { dp.SyntaxHighlighter.HighlightAll('code'); });</script>
    <script src="js/mensaje/popup.js"></script>
    <!-- FIN MENSAJE EMERGENTE INFORMACION -->





    <!--- INICIO MASCARAS -->

    <link rel="stylesheet" href="http://code.jquery.com/qunit/qunit-1.11.0.css" type="text/css" media="all">

    <!-- FIN MASCARAS -->


    <script src="js/webmethodsCalcInde.js"></script>
    <link href="css/Calendar.css" rel="stylesheet" />

    <script type="text/javascript">
        jQuery(document).ready(function () {
            // binds form submission and fields to the validation engine
            jQuery("#frmCalculadoraIndi").validationEngine();
        });

        function ValidNum(e) {
            var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
            return ((tecla > 47 && tecla < 58) || tecla == 46 || tecla == 8 || tecla == 9 || tecla == 37 || tecla == 39);
        }

        $(document).ready(function () {            
            $("#txtEdadActual").keydown(function (e) {
                // Allow: backspace, delete, tab, escape, enter and .
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                    // Allow: Ctrl+A
                    (e.keyCode == 65 && e.ctrlKey === true) ||
                    // Allow: home, end, left, right
                    (e.keyCode >= 35 && e.keyCode <= 39)) {
                    // let it happen, don't do anything
                    return;
                }
                // Ensure that it is a number and stop the keypress
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });

            $("#txtAportacion").keydown(function (e) {
                // Allow: backspace, delete, tab, escape, enter and .
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                    // Allow: Ctrl+A
                    (e.keyCode == 65 && e.ctrlKey === true) ||
                    // Allow: home, end, left, right
                    (e.keyCode >= 35 && e.keyCode <= 39)) {
                    // let it happen, don't do anything
                    return;
                }
                // Ensure that it is a number and stop the keypress
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });

            $("#txtSaldoAFORE").keydown(function (e) {
                // Allow: backspace, delete, tab, escape, enter and .
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                    // Allow: Ctrl+A
                    (e.keyCode == 65 && e.ctrlKey === true) ||
                    // Allow: home, end, left, right
                    (e.keyCode >= 35 && e.keyCode <= 39)) {
                    // let it happen, don't do anything
                    return;
                }
                // Ensure that it is a number and stop the keypress
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });
        });

    </script>

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

   <%-- <style>
        body
        {
            background-image: url(imagenes/fondo.gif);
        }
    </style>--%>

    <!-- codigo nuevo-->

    <link href="/css/estilo.css" rel="stylesheet" type="text/css" />

    <link href="/css/menu.css" rel="stylesheet" type="text/css" />

    <script src="http://f1.na.readspeaker.com/script/6824/ReadSpeaker.js?pids=embhl"
        type="text/javascript"></script>
    <link rel="stylesheet" href="http://www.encuentra.gob.mx/css/apf.css" type="text/css" />
    <script src="http://www.encuentra.gob.mx/api/gobmxWidgetAPI-min.js" type="text/javascript"></script>
    <script src="http://www.encuentra.gob.mx/properties/gobmxWidgetAPI-conf.js" type="text/javascript"></script>
    <link href="/css/mapa.css" rel="stylesheet" type="text/css" />
    <link href="/css/slider.css" rel="stylesheet" type="text/css" />
    <link href="/css/listado.css" rel="stylesheet" type="text/css" />
    <link href="/css/uno.css" rel="stylesheet" type="text/css" />
    <link href="/css/dos.css" rel="stylesheet" type="text/css" />
    <link href="/css/tres.css" rel="stylesheet" type="text/css" />
    <script src="js/eventos.js"></script>
    <!-- Validaciones -->

    <script type="text/javascript">


        function valida_Edad(field, rules, i, options) {
            //LimpiaResultado();
            var psEdad = parseInt($('#txtEdadActual').val());
            var valorEdadRetiro = parseInt($(".SeleccionEdad:checked").val());

            if (isNaN(psEdad)) {
                if ($('#hdfCampo').val() == '' || $('#hdfCampo').val() == 'txtEdadActual') {
                    $('#hdfCampo').val('txtEdadActual');

                    //LimpiaResultado();
                    document.getElementById("txtEdadActual").focus();
                    return;
                }
            }

            if (psEdad < 14 || psEdad > 66) {

                if ($('#hdfCampo').val() == '' || $('#hdfCampo').val() == 'txtEdadActual') {

                    $('#hdfCampo').val('txtEdadActual');

                    //LimpiaResultado();

                    document.getElementById("txtEdadActual").focus();

                    return "Debes tener 14 años como mínimo y hasta 66 como máximo.";
                }
            }
            else {
                if (psEdad >= valorEdadRetiro) {
                    $('#hdfCampo').val('txtEdadActual');

                    //LimpiaResultado();

                    document.getElementById("txtEdadActual").focus();

                    return "Debes tener una edad menor a la Edad de retiro.";
                }
                else {
                    $('#hdfCampo').val('');

                    if ($('#hdfBanderaBlur').val() == 0) {

                        // LlamaCalculo();
                    }

                }

            }

        }

        function valida_Edad_Radio60(field, rules, i, options) {

            var psEdad = parseInt($('#tx0tEdadActual').val());
            var valorEdadRetiro = parseInt($(".SeleccionEdad:checked").val());

            if (psEdad <= 14 || psEdad > 65) {

                if ($('#hdfCampo').val() == '' || $('#hdfCampo').val() == 'txtEdadActual') {

                    $('#hdfCampo').val('txtEdadActual');

                    LimpiaResultado();

                    document.getElementById("txtEdadActual").focus();

                    return "Debes tener 14 años como mínimo y hasta 64 años como máximo.";
                }
            }
            else {
                if (psEdad >= valorEdadRetiro) {
                    $('#hdfCampo').val('txtEdadActual');

                    LimpiaResultado();

                    document.getElementById("txtEdadActual").focus();

                    return "Debes tener una edad menor a la Edad de retiro.";
                }
                else {
                    $('#hdfCampo').val('');


                    if ($('#hdfBanderaBlur').val() == 0) {
                        limpiaMensajes();
                        //  LlamaCalculo();
                    }


                }
            }

        }

        function ValidaRadio60() {
            document.getElementById("txtEdadActual").focus();
            document.getElementById("rdbEdad60").focus();
            //  LlamaCalculo();
        }

        function valida_Edad_Radio65(field, rules, i, options) {

            var psEdad = parseInt($('#tx0tEdadActual').val());
            var valorEdadRetiro = parseInt($(".SeleccionEdad:checked").val());

            if (psEdad <= 14 || psEdad > 65) {

                if ($('#hdfCampo').val() == '' || $('#hdfCampo').val() == 'txtEdadActual') {

                    $('#hdfCampo').val('txtEdadActual');

                    LimpiaResultado();

                    document.getElementById("txtEdadActual").focus();

                    return "Debes tener 14 años como mínimo y hasta 64 años como máximo.";
                }
            }
            else {
                if (psEdad >= valorEdadRetiro) {
                    $('#hdfCampo').val('txtEdadActual');

                    LimpiaReLimpiaResultadosultado();

                    document.getElementById("txtEdadActual").focus();

                    return "Debes tener una edad menor a la Edad de retiro.";
                }
                else {
                    $('#hdfCampo').val('');


                    if ($('#hdfBanderaBlur').val() == 0) {
                        limpiaMensajes();
                        //   LlamaCalculo();
                    }


                }
            }

        }

        function valida_Aportacion(field, rules, i, options) {
            //LimpiaResultado();
            var psAportacion = document.getElementById('txtAportacion').value;
            psAportacion = psAportacion.replace("$", "");
            psAportacion = psAportacion.replace(",", "");
            var pAportacion = psAportacion;

            if (pAportacion < 0) {

                if ($('#hdfCampo').val() == '' || $('#hdfCampo').val() == 'txtAportacion') {

                    $('#hdfCampo').val('txtAportacion');

                    //LimpiaResultado();

                    document.getElementById("txtAportacion").focus();
                    return "El ahorro voluntario debe ser mayor a cero.";
                }
            }
            else {
                $('#hdfCampo').val('');
                //   LlamaCalculo();
            }
        }

        function valida_Saldo(field, rules, i, options) {
            //LimpiaResultado();
            var psSaldo = document.getElementById('txtSaldoAFORE').value;
            psSaldo = psSaldo.replace("$", "");
            psSaldo = psSaldo.replace(",", "");
            var pSaldo = psSaldo;

            if (pSaldo < 0) {

                if ($('#hdfCampo').val() == '' || $('#hdfCampo').val() == 'txtSaldoAFORE') {

                    $('#hdfCampo').val('txtSaldoAFORE');

                    document.getElementById("txtSaldoAFORE").focus();
                    //LimpiaResultado();
                    return "El saldo no puede ser negativo, si no tiene ahorro coloque 0.";
                }
            }
            else {
                $('#hdfCampo').val('');
                // LlamaCalculo();
            }
        }

        function LineAhorro() {
            this.AhorroAdicional = 0;
            this.AhorroTotalMensual = 0;
            this.AhorroAcumulado = 0;

        }

        
        function mostrarDivCarga() {
            //$('#cargando_test').hide();
            document.getElementById('loadingDiv').style.display = 'block';
        }


        function LlamaCalculo() {            
            $("#txtEdadActual").blur();
            $("#txtAportacion").blur();
            //$("#txtSaldoAFORE").blur();


            var valorEdadActual = parseInt($('#txtEdadActual').val());
            var valorEdadRetiro = parseInt($(".SeleccionEdad:checked").val());
            var valorAportacion = parseFloat($('#txtAportacion').val().replace('$', '').replace(',', '').replace(',', ''));
            var strValorAfore = $('#txtSaldoAFORE').val().replace('$', '').replace(',', '').replace(',', '').replace(',', '');
            var valorSeleccionGenero = parseInt($(".SeleccionGenero:checked").val());
            var psEdad = parseInt($('#txtEdadActual').val());
            var valorEdadRetiro = parseInt($(".SeleccionEdad:checked").val());

            if (strValorAfore.length == 0) {
                strValorAfore = '0';
            }
            var valorSaldoAfore = parseFloat(strValorAfore);

            if (valorEdadActual >= 14 && valorAportacion >= 0 && valorEdadRetiro > 0 && valorSaldoAfore >= 0 && valorEdadRetiro > valorEdadActual) {
                mostrarDivCarga();
                disable_scroll();
                $('#hdfBanderaBlur').val('0')

                var captura = new function () {
                    this.EdadActual = valorEdadActual;
                    this.EdadRetiro = valorEdadRetiro;
                    this.AhorroMensual = valorAportacion;
                    this.SaldoAFORE = valorSaldoAfore;
                    this.SeleccionGenero = valorSeleccionGenero;

                };
                var strSerializado = JSON.stringify(captura);

                $.ajax
                ({
                    type: "POST",
                    url: "CalculadoraIndi.aspx/Calculo",
                    dataType: "json",
                    data: strSerializado,
                    contentType: "application/json; charset=utf-8",
                    error: function (jqXHR, textStatus, errorThrown)   //what to do if fails
                    {
                        document.getElementById('loadingDiv').style.display = 'none';
                        //enable_scroll();
                        alert('bad, ' + errorThrown + ", " + jqXHR.responseText + ", " + textStatus);
                    },
                    success: function (data)           //what to do if succedded
                    {
                        document.getElementById('loadingDiv').style.display = 'none';
                        //enable_scroll();
                        var res = $.parseJSON(data.d);
                        $('#lblAhorroEstimado').text(res.AhorroTotalEstimado);
                        $('#lblPensionMensual').text(res.PensionMensual);
                        for (var icont = 0; icont < res.lstLineaAhorro.length; icont++) {
                            var iAhorroAdicional = res.lstLineaAhorro[icont].AhorroAdicional;
                            var strAhorroTotalMensual = res.lstLineaAhorro[icont].AhorroTotalMensual;
                            var strAhorroAcumulado = res.lstLineaAhorro[icont].AhorroAcumulado;
                            var strPensionMensual = res.lstLineaAhorro[icont].PensionMensual;
                            switch (iAhorroAdicional) {
                                case 100:
                                    $('#lblAhorroMensual50').text(strAhorroTotalMensual);
                                    $('#lblAhorroAcumulado50').text(strAhorroAcumulado);
                                    $('#lblPensionMensual50').text(strPensionMensual);
                                    break;
                                case 300:
                                    $('#lblAhorroMensual100').text(strAhorroTotalMensual);
                                    $('#lblAhorroAcumulado100').text(strAhorroAcumulado);
                                    $('#lblPensionMensual100').text(strPensionMensual);
                                    break;
                                case 1000:
                                    $('#lblAhorroMensual200').text(strAhorroTotalMensual);
                                    $('#lblAhorroAcumulado200').text(strAhorroAcumulado);
                                    $('#lblPensionMensual200').text(strPensionMensual);
                                    break;
                                default:
                            }
                        }

                       
                        //Mostrar la seccion de resultado
                        //document.getElementById('dvContenedorResultado').scrollIntoView();
                       
                    }
                });
            }
            else {
                LimpiaResultado();
            }
           
        }


        function limpiaMensajes() {

            var valorEdadActual = parseInt($('#txtEdadActual').val());
            var valorEdadRetiro = parseInt($(".SeleccionEdad:checked").val());

            $('#hdfBanderaBlur').val('1');

            $('#rdbEdad65').trigger("blur");
            $('#txtEdadActual').trigger("blur");

            $('#hdfBanderaBlur').val('0');
        }

        function LimpiaResultado() {
            $('#lblAhorroEstimado').text('$0.00');
            $('#lblPensionMensual').text('$0.00');
            $('#lblAhorroMensual50').text('$0.00');
            $('#lblAhorroAcumulado50').text('$0.00');
            $('#lblPensionMensual50').text('$0.00');
            $('#lblAhorroMensual100').text('$0.00');
            $('#lblAhorroAcumulado100').text('$0.00');
            $('#lblPensionMensual100').text('$0.00');
            $('#lblAhorroMensual200').text('$0.00');
            $('#lblAhorroAcumulado200').text('$0.00');
            $('#lblPensionMensual200').text('$0.00');
        }

        function replaceAll(find, replace, str) {
            return str.replace(new RegExp(find, 'g'), replace);
        }

        function ValidaRadio65() {
            if ($('#hdfBanderaBlur').val() == 0) {
                limpiaMensajes();
                LlamaCalculo();
            }
        }

    </script>

    <!--<script src="js/jquery-ui.js"></script>-->
    <!--<link href="css/globocss.css" rel="stylesheet" />-->
    <script src="js/globosCalcInde.js"></script>


</head>
<body>


     <script src="js/DigitalAnalitics.js"></script>
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
    <nav class="navbar navbar-inverse sub-navbar navbar-fixed-top">
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
                </ul>
            </div>
        </div>
    </nav>


    <div class="inner-page interior documents">
        <div class="container">

             <div class="row">
                <div class="col-md-12">
                    <form id="frmCalculadoraIndi" runat="server">
        <asp:HiddenField ID="hdfCampo" runat="server" Value="" />
        <asp:HiddenField ID="hdfBanderaBlur" runat="server" Value="0" />
        <!-- Mensajes -->
        <!-- cuerpo nuevo -->
        <div id="sombracompleta" style="position: absolute; margin-top: 1358px; width: 100%; z-index: -1; margin-left: 0px; background-color: #E9E9E9; height: 0px;">
        </div>
        <div class="center">

            <%--<div>--%>

            <!-- codigo calculadora-->
            <div class="CentradoCalc" style="margin-top:80px;">
                <div id="DVFondoEncabezadoIndi" class="DVFondoEncabezadoIndi">
                    <img src="imagenes/encabezado_independientes.png" width="988" height="100" style="border: none" alt="Independientes" usemap="#INDImap">
                    <map name="INDImap">
                        <area shape="rect" coords="820,81,925,100" href="PDF/Metodología_Calculadora_Independientes.pdf" alt="Metodología">
                    </map>
                </div>
                <div id="DVContenedorCalculadora" class="panelVisible" style="position: relative;">
                    <div id="loadingDiv" style="display: none;">
                        <div>
                        </div>
                    </div>
                    <p class="TextoMorado" style="clear: both;">
                        Ingresa tus datos
                    </p>
                    <div id="CapturaIndependientes" style="position: relative;">
                        <p class="textoDescripcionFormulario" style="position: absolute; left: 95px; top: 5px;"></p>
                        <input id="txtEdadActual" name="EdadActual" onblur="LimpiaResultado()" class="txtFormulario inputRadius validate[required,funcCall[valida_Edad]]"
                            type="text" maxlength="2" runat="server" style="position: relative; left: 217px; top: 20px; width: 110px;" />
                        <p class="textoDescripcionFormulario" style="position: absolute; left: 500px; top: 0px; text-align: right;"></p>
                        <div style="position: absolute; left: 499px; top: 17px;">
                            <span class="textoDescripcionFormulario"></span>&nbsp;
                           <input id="rdbEdad60" type="radio" name="EdadRetiro" value="60" runat="server" checked class="SeleccionEdad" onblur="LimpiaResultado()" onclick="ValidaRadio60()" />

                        </div>
                        <div style="position: absolute; left: 581px; top: 17px;">
                            <span class="textoDescripcionFormulario"></span>&nbsp;
                           <input id="rdbEdad65" type="radio" name="EdadRetiro" value="65" runat="server" class="SeleccionEdad inputRadius validate[funcCall[valida_Edad_Radio65]]" onblur="LimpiaResultado()" />

                        </div>
                        <div style="position: absolute; left: 660px; top: 17px;">
                            <span class="textoDescripcionFormulario"></span>&nbsp;
                           <input id="rdbEdad67" type="radio" name="EdadRetiro" value="67" runat="server" class="SeleccionEdad inputRadius validate[funcCall[valida_Edad_Radio65]]" onblur="LimpiaResultado();" />
                        </div>
                        <div style="position: absolute; left: 790px; top: 75px;">
                            <input id="sexoF" type="radio" name="SeleccionGenero" value="1" runat="server" checked class="SeleccionGenero validate[funcCall[LimpiaResultado]]" onblur="LimpiaResultado()" />
                        </div>
                        <div style="position: absolute; left: 860px; top: 75px;">
                            <input id="sexoM" type="radio" name="SeleccionGenero" value="0" runat="server" class="SeleccionGenero validate[funcCall[LimpiaResultado]]" onblur="LimpiaResultado()" />
                        </div>
                        <div>
                            <p class="textoDescripcionFormulario" style="text-align: right; position: absolute; left: 30px; top: 75px;"></p>
                            <input id="txtAportacion" type="text" name="AhorroMensual" onblur="LimpiaResultado()" class="txtFormulario inputRadius validate[required,funcCall[valida_Aportacion]]" maxlength="10" runat="server" style="position: absolute; left: 214px; top: 100px; width: 120px;" />
                            <%--Se quitó esta validación al campo txtAportacion a petición de Lider Consar 08042015 funcCall[valida_Aportacion]--%>
                            <span id="mensaje-txtAportacion" class="message" style="display: none;"></span>
                        </div>
                        <div>
                            <p class="textoDescripcionFormulario" style="position: absolute; left: 485px; top: 50px; text-align: center;"></p>
                            <input id="txtSaldoAFORE" type="text" name="SaldoAFORE" onblur="LimpiaResultado()" class="txtFormulario inputRadius validate[funcCall[valida_Saldo]]" maxlength="15" runat="server" style="position: absolute; left: 470px; top: 126px; width: 170px;" />
                            <span id="mensaje-txtSaldoAFORE" class="message" style="display: none;"></span>
                        </div>
                        <div id="referenceSaldoAcumulado" style="position: absolute; left: 690px; top: 110px;">
                            <p class="text_gral_center">
                                <a class='inline' href="#msgSaldoAcumulado" id="aMensajeError">
                                    <img src="imagenes/i_peque.png" style="border: none;" />
                                </a>
                            </p>
                        </div>
                        <div style='display: none'>
                            <div id='msgSaldoAcumulado' style='padding: 10px; background: #fff;'>
                                <p class="text_Modales_Indi">
                                    En caso de que anteriormente hayas cotizado al IMSS o al ISSSTE y hubieras  elegido una AFORE, 
                                    debes tener tus estados de cuenta ya que las AFORE lo envían 4 veces al año al domicilio que les hayan indicado.
                                </p>
                                <p class="text_Modales_Indi">
                                    Si no recuerdas en qué AFORE estás registrado puedes ingresar a <a href="http://www.e-sar.com.mx" target="_blank">www.e-sar.com.mx</a> o bien llamar a SARTEL 01 800 5000 747.
                                    <br />
                                    <br />
                                    Si eres un trabajador independiente y quieres registrarte en una AFORE y empezar a ganar altos rendimientos para tu ahorro, 
                                    solo deberás hablar a la AFORE en la que te quieras registrar y pedir que te manden un agente promotor a tu oficina o domicilio particular y listo.
                                    <br />
                                    <br />
                                    Recuerda que para elegir la AFORE que haga crecer más tu dinero debes fijarte en tres cosas básicas:  
                                </p>
                                <ul class="text_Modales_Indi">
                                    <li>Rendimientos</li>
                                    <li>Comisiones</li>
                                    <li>Servicios</li>
                                </ul>
                                <p class="text_Modales_Indi">
                                    Da clic <a href="https://www.gob.mx/consar/articulos/contacta-a-tu-afore" target="_blank">aquí</a> para contactar a las AFORE
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
                <br />

                <table style="border: hidden; border-color: tomato; width: 950px;">
                    <tr>
                        <td>
                            <br />
                            <br />
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/btn_calcular1.png" onmouseover="this.src='imagenes/btn_calcular2.png'" onmouseout="this.src='imagenes/btn_calcular1.png'" CssClass="btnCalcular cursorHand submit" type="submit" OnClick="LlamaCalculo();" />
                        </td>
                    </tr>
                </table>

                <div id="dvContenedorResultado" class="panelVisible" style="position: relative;">
                    <p class="TextoMorado" style="clear: both;">
                    </p>
                    <div id="dvAhorroEstimado" style="position: relative;">
                        <label id="lblAhorroEstimado" class="textoSaldoFormulario"
                            style="position: absolute; right: 10px; top: 29px;">
                            $0.00
                        </label>
                    </div>
                    <br />
                    <div id="dvPensionMensual" style="position: relative;">
                        <label id="lblPensionMensual" class="textoSaldoFormulario"
                            style="position: absolute; right: 60px; top: 90px;">
                            $0.00
                        </label>
                    </div>
                    <div id="dvAportacionVoluntaria" style="position: relative; margin-top: 40px;">
                        <!-- Fila Encabezado -->

                        <!-- Fila de 50 -->
                        <label id="lblAhorroMensual50" class="textoDescripcionFormulario"
                            style="position: absolute; right: 540px; top: 150px;">
                            $0.00
                        </label>
                        <label id="lblAhorroAcumulado50" class="textoDescripcionFormulario"
                            style="position: absolute; right: 290px; top: 150px;">
                            $0.00
                        </label>
                        <label id="lblPensionMensual50" class="textoDescripcionFormulario"
                            style="position: absolute; right: 38px; top: 150px;">
                            $0.00
                        </label>
                        <!-- Fila de 100 -->
                        <label id="lblAhorroMensual100" class="textoDescripcionFormulario"
                            style="position: absolute; right: 540px; top: 200px;">
                            $0.00
                        </label>
                        <label id="lblAhorroAcumulado100" class="textoDescripcionFormulario"
                            style="position: absolute; right: 290px; top: 200px;">
                            $0.00
                        </label>
                        <label id="lblPensionMensual100" class="textoDescripcionFormulario"
                            style="position: absolute; right: 38px; top: 200px;">
                            $0.00
                        </label>
                        <!-- Fila de 200 -->
                        <label id="lblAhorroMensual200" class="textoDescripcionFormulario"
                            style="position: absolute; right: 540px; top: 245px;">
                            $0.00
                        </label>
                        <label id="lblAhorroAcumulado200" class="textoDescripcionFormulario"
                            style="position: absolute; right: 290px; top: 245px;">
                            $0.00
                        </label>
                        <label id="lblPensionMensual200" class="textoDescripcionFormulario"
                            style="position: absolute; right: 38px; top: 245px;">
                            $0.00
                        </label>
                    </div>
                    <div style="position: absolute; left: 860px; top: 110px;">
                        <p class="text_gral_center">
                            <a>
                                <img src="imagenes/i_peque.png" id="tooltip" style="border: none; width:75%" />
                            </a>
                        </p>
                    </div>
                    <div style='display: none'>
                        <div id='msgMensajePension' style='padding: 10px; background: #fff;'>
                            <p class="text_Modales_Indi">
                                La Pensión mensual estimada se calcula solo con fines ilustrativos y con el fin de que veas la importancia que tiene el ahorro y como éste puede traducirse en una pensión.
                            </p>

                        </div>
                    </div>
                </div>
                <!--Fin JJMM 012_ODT_044_03-->
                <p class="TextoDescripcion" style="margin-left: 50px; margin-right: 50px; margin-top: 20px; margin-bottom: 20px; clear: both;">
                    Nota: Se considera para este cálculo un rendimiento de 4% y una comisión de 1.14% anual. 
                        Para comparar a las AFORE en Rendimiento Neto da clic <a href="http://www.gob.mx/consar/acciones-y-programas/como-elegir-la-mejor-afore?idiom=es" target="_blank">aquí</a>
                </p>

                <a href="http://www.gob.mx/consar/acciones-y-programas/calculadoras-de-ahorro-y-retiro?idiom=es">
                    <div class="RegresaInicio" style="float: left;"></div>
                    <div style="float:right">
                        <div class="PiePagIP">
                            <asp:Label ID="Label1" runat="server" Text="Usted es el visitante número:" CssClass="numVisitaslB"></asp:Label>
                            &nbsp;&nbsp;<asp:Label ID="lbContVisitas" runat="server" Text="0" CssClass="numVisitas"></asp:Label>
                        </div>
                    </div>
                </a>

                <div style="width: 100%; text-align: center; clear: both;">
                    <a class="inline" href="#msgAumentarAportacion" style="text-decoration: none;">
                        <span class="TextoMorado">¡Si ya decidiste aumentar tu aportación da clic aquí!</span>
                    </a>
                    <div style="display: none;">
                        <div id="msgAumentarAportacion">
                            <p class="text_Modales_Indi">
                                Si ya decidiste hacer ahorro voluntario, te invitamos a que utilices cualquiera de estas vías.
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
                </div>

            </div>
           <%-- <div class="pie" style="position: relative;">
                <div id="image_map">
                    <a href="/orienta_trabajador/opinion_iso.aspx" id="A3" class="menuredes"></a>
                    <a href="http://twitter.com/Consar_mx" id="A4" class="menuredes"></a><a href="http://www.facebook.com/pages/CONSAR/121018577916864"
                        id="A5" class="menuredes"></a><a href="http://www.youtube.com/user/CONSARMexico"
                            id="A6" class="menuredes"></a><a href="http://www.flickr.com/photos/consar/" id="A7"
                                class="menuredes"></a><a href="/rss/consar.xml" id="link6" class="menuredes"></a><a href="/politica_privacidad/privacidad.aspx" id="link7" class="menuredes"></a><a href="index.aspx" id="link8" class="menuredes"></a><a href="/orienta_trabajador/opinion_iso.aspx"
                                    id="link9" class="menuredes"></a>
                </div>
            </div>--%>
        </div>
    
    </form>
                </div>

            </div>
        </div>

    </div>



    

    <script src="https://framework-gb.cdn.gob.mx/gobmx.js"></script>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="CALCULADORA2014.Inicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <!-- INICIO GENERAL -->
    <link href="css/Style.css" rel="stylesheet" />
    <link href="css/validate/template.css" rel="stylesheet" />
    <%--  <script src="//code.jquery.com/jquery-1.10.2.js"></script>--%>
    <script src="js/jquery-1.7.2.min.js"></script>
    <!-- FIN GENERAL -->


    <!-- Inicio Master -->

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


    <!-- Fin Master -->

    <style>
        body {
            background-image: url(imagenes/fondo.gif);
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div id="sombracompleta" style="position: absolute; margin-top: 1358px; width: 100%; z-index: -1; margin-left: 0px; background-color: #E9E9E9; height: 0px;">
        </div>
        <div class="center" style="background-color: transparent;">
            <!-- Inicio Menus -->

            <div class="logo">
                <div id="image_map_logo">
                    <a href="http://www.shcp.gob.mx/Paginas/default.aspx" id="A1" class="menulogo"></a>
                    <a href="http://www.consar.gob.mx/" id="A2" class="menulogo"></a>
                </div>
            </div>
            <div class="menucito">
                <div id="site-sec-nav">
                    <ul>
                        <li><a href="http://portaltransparencia.gob.mx/pot/dependencia/showDependencia.do?method=begin&redireccionado=true&_idDependencia=06121">Directorio</a></li>
                        <li><a href="/contacto/contacto.aspx">Contacto</a></li>
                        <li><a href="/ingles/ingles_previo.aspx">English</a></li>
                    </ul>
                </div>
            </div>
            <div class="buscador">
                <%--<div style="margin-left: 140px;">
                <!-- http://www.encuentra.gob.mx -->
                <div id="gobmx-searchbox-container-CONSAR" class="div_searchbox">
                    <script type="text/javascript">                        _gmx.loadGobMXsearchAPI({ container: 'gobmx-searchbox-container-CONSAR', client: 'CONSAR', newWindow: true });</script>
                </div>
                <!-- http://www.encuentra.gob.mx -->
            </div>--%>
                <div style="margin-left: 50px;">
                    <script>
                        (function () {
                            var cx = '006122115772860426093:v6iwktf_ca8';
                            var gcse = document.createElement('script');
                            gcse.type = 'text/javascript';
                            gcse.async = true;
                            gcse.src = (document.location.protocol == 'https:' ? 'https:' : 'http:') +
                                '//www.google.com/cse/cse.js?cx=' + cx;
                            var s = document.getElementsByTagName('script')[0];
                            s.parentNode.insertBefore(gcse, s);
                        })();
                    </script>
                    <gcse:search></gcse:search>
                </div>
            </div>
            <div class="barrita">
            </div>
            <div class="menu">
                <div class="menu-center">
                    <ul class="nav">
                        <li><a href="/acerca_consar/consar.aspx">consar</a>
                            <ul>
                                <li><a href="/acerca_consar/acerca_consar.aspx">Acerca de CONSAR</a></li>
                                <li><a href="/acerca_consar/acerca_consar_administracion.aspx">Administraci&oacute;n</a></li>
                                <li><a href="/acerca_consar/acerca_consar-informes.aspx">Informes</a></li>
                                <li><a href="/acerca_consar/acerca_consar_oic.aspx">&Oacute;rgano Interno de Control</a></li>
                                <li><a href="/acerca_consar/acerca_consar-asuntos_internacionales.aspx">Asuntos Internacionales</a></li>
                            </ul>
                        </li>
                        <li><a href="#">para ahorradores</a>
                            <ul>
                                <li><a href="/principal/info_gral_trabajadores-menu-imss.aspx">Imss</a></li>
                                <li><a href="/principal/info_gral_trabajadores-menu-issste.aspx">Issste</a></li>
                                <li><a href="/principal/info_gral_trabajadores-menu-trabajadoresindependientes.aspx">Independientes</a></li>
                                <li><a href="/principal/info_gral_trabajadores-menu-loquedebessaber.aspx">Lo que necesitas
                            saber</a></li>
                            </ul>
                        </li>
                        <li><a href="/principal/informacion_para_empresas.aspx">para empresas</a> </li>
                        <li><a href="/principal/estadisticas_sar.aspx">estad&Iacute;stica</a>
                            <ul>
                                <li><a href="/principal/estadisticas_sar.aspx">Información y estadística</a></li>
                                <li><a href="/principal/encuesta_nacional_trayectorias_laborales.aspx">Encuesta de trayectorias
                            laborales</a></li>
                            </ul>
                        </li>
                        <li><a href="/normatividad/normatividad.aspx">normativa</a>
                            <ul>
                                <li><a rel="external" href="/normatividad/pdf/normatividad_ley_sar.pdf">Ley de los Sistemas
                            de
                            <br />
                                    Ahorro para el Retiro</a></li>
                                <li><a href="/normatividad/normatividad-otras_leyes.aspx">Otras leyes</a></li>
                                <li><a href="/normatividad/normatividad-reglamentos.aspx">Reglamentos</a></li>
                                <li><a href="/normatividad/normatividad-normatividad_consar.aspx">Normativa emitida
                            por la CONSAR</a></li>
                                <li><a href="/normatividad/normatividad-otros.aspx">Normativa emitida por organismos,<br />
                                    dependencias o entidades</a></li>
                                <li><a href="/normatividad/normatividad-sistemas_estatales.aspx">Sistemas estatales
                            de pensiones</a></li>
                                <li><a href="/normatividad/normatividad-otros_temas.aspx">Otros temas</a></li>
                            </ul>
                        </li>
                        <li><a href="/sala_prensa/sala_prensa.aspx">prensa</a>
                            <ul>
                                <li><a href="/sala_prensa/sala_prensa-boletines.aspx">Boletines</a></li>
                                <li><a href="/sala_prensa/sala_prensa-presentaciones.aspx">Presentaciones</a></li>
                            </ul>
                        </li>
                        <li><a href="/transparencia/transparencia.aspx">Transparencia</a>
                            <ul>
                                <li><a href="/transparencia/transparencia_normatividad.aspx">Normatividad en materia<br />
                                    de transparencia</a></li>
                                <li><a href="/transparencia/transparencia_informacion.aspx">Comit&eacute; de informaci&oacute;n</a>
                                </li>
                                <li><a href="/transparencia/transparencia_focalizada.aspx">Transparencia Focalizada</a></li>
                                <li><a href="/transparencia/transparencia_recomendaciones_consar.aspx">Recomendaciones recibidas<br />
                                    por la CONSAR</a></li>
                                <li><a href="/transparencia/transparencia_estudiosyopiniones.aspx">Estudios y opiniones
                                </a></li>
                                <li><a href="http://www.transparenciapresupuestaria.gob.mx/Portal/transform.nodo?id=4.0&transformacion=s&excel=n&zip=n&paramts=0=L509475">Indicadores de programas<br />
                                    presupuestales</a></li>
                                <li><a href="/transparencia/transparencia_programas_plan_nacional_desarrollo.aspx">Programas del Plan Nacional<br />
                                    de Desarrollo</a></li>
                                <li><a href="/transparencia/transparencia_rendicion_cuentas.aspx">Rendici&oacute;n de cuentas</a></li>
                                <li><a href="/transparencia/transparencia_participacion_ciudadana.aspx">Participación ciudadana</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>

            </div>
            <div class="barrita">
            </div>

            <!-- Fin Menus -->

            <div class="CentradoCalc">

                <div id="DVFondoEncabezadoInicio" class="DVFondoEncabezadoInicio">
                </div>
                <p class="TextoDescripcion" style="clear: both;">
                    Te presentamos las nuevas <b>CALCULADORAS</b> de ahorro y retiro que permitirán estimar, bajo ciertos supuestos,
            cuál podría ser tu pensión o ahorro al finalizar tu vida laboral. Si eres un(a) trabajador(a) que cotiza
            al IMSS o un(a) trabajador(a) independiente, te invitamos a hacer tu propio cálculo y a tomar
            acciones para tu futuro.
                </p>

                <div class="CentradoCalc SeleccionCalculadora">
                    <a href="CalculadoraIMSS.aspx">
                        <div class="TrabajadorIMSS"></div>
                    </a>
                    <a href="CalculadoraIndi.aspx" style="float: right;">
                        <div class="TrabajadorIndependiente"></div>
                    </a>
                </div>
                <div style="height: 50px; clear: both;"></div>
                <!-- Inicio Pie -->

                <div class="pie">
                    <div id="image_map">
                        <a href="/orienta_trabajador/opinion_iso.aspx" id="A3" class="menuredes"></a>
                        <a href="http://twitter.com/Consar_mx" id="A4" class="menuredes"></a><a href="http://www.facebook.com/pages/CONSAR/121018577916864"
                            id="A5" class="menuredes"></a><a href="http://www.youtube.com/user/CONSARMexico"
                                id="A6" class="menuredes"></a><a href="http://www.flickr.com/photos/consar/" id="A7"
                                    class="menuredes"></a><a href="/rss/consar.xml" id="link6" class="menuredes"></a><a href="/politica_privacidad/privacidad.aspx" id="link7" class="menuredes"></a><a href="index.aspx" id="link8" class="menuredes"></a><a href="/orienta_trabajador/opinion_iso.aspx"
                                        id="link9" class="menuredes"></a>
                    </div>
                </div>

                <!-- Fin Pies -->
            </div>
        </div>
    </form>
</body>
</html>

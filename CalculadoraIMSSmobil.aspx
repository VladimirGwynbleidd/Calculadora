<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalculadoraIMSSmobil.aspx.cs" Inherits="CALCULADORA2014.CalculadoraIMSSmobil" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <!-- INICIO GENERAL -->
    <link href="css/Style.css" rel="stylesheet" />

    <script src="js/jquery-1.7.2.min.js"></script>
    <!-- FIN GENERAL -->

    <script>
        $(window).load(function () {
            $('#cargando').hide();
        });
    </script>

    <!-- INICIO VALIDACION -->
    <script src="js/validate/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="js/validate/languages/jquery.validationEngine-es.js"></script>
    <!-- FIN VALIDACION -->



    <!-- INICIO FORMATO MONEDA -->
    <script src="js/moneda.js"></script>
    <!-- FIN FORMATO MONEDA -->

    <!-- INICIO SLIDERS -->
  <link href="css/Sliders.css" rel="stylesheet" />   
    <script src="js/jquery-ui.js"></script>
    <!-- FIN SLIDERS -->

    <!-- INICIO MENSAJE EMERGENTE INFORMACION -->
    <script src="js/mensaje/shCore.js"></script>
    <!-- FIN MENSAJE EMERGENTE INFORMACION -->

    <!--- INICIO MASCARAS -->
    <script src="js/jquery.maskedinput.min.js"></script>
    <script src="js/jquery.placeholder.js"></script>
    <!-- FIN MASCARAS -->


    <script src="js/webmethodsMobil.js"></script>

    <script type="text/javascript">        
        function RecibeParameters(Parametros) {
            GetResultadoCalculoPruebaMobil(Parametros);
            // alert("Hello Word " + Parametros);
        }  
    </script>

    <style>
        body {
            background-image: url(imagenes/fondo.gif);
        }
    </style>   
</head>
<body onload="GetComisionesAforeClave();">
    <input id="hddLugarComision" type="hidden" />

    <form id="formID" runat="server">
        <asp:HiddenField runat="server" ID="hdfGeneracionTransicion" Value="" />
        <asp:HiddenField runat="server" ID="hdfAniosCotizados" Value="0" />
        <asp:HiddenField runat="server" ID="hdfCampoFocus" Value="" />

        <!-- cuerpo nuevo -->

        <div id="sombracompleta" style="position: absolute; margin-top: 1358px; width: 100%; z-index: -1; margin-left: 0px; background-color: #E9E9E9; height: 0px;">
        </div>
        <div class="center">                       
            <!-- INICIO CONTENIDO CALCULADORA -->

            <div class="CentradoCalc">
                <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ToolkitScriptManager1" EnableScriptGlobalization="true"
                    EnablePartialRendering="true" EnableScriptLocalization="true" EnablePageMethods="true" />

             

                <div id="DVContenedorCalculadora" class="panelVisible" style="display: none;">
                    <div id="cargando"></div>

                    <div id="DVAfore">
                        <img src="imagenes/afore.png" id="imgAfore" /><asp:DropDownList ID="cboAfore" runat="server" onchange="GetComisionAforeClave();" CssClass="validate[required]"></asp:DropDownList><label id="cobracomision">Cobra una comisión de:</label>
                        <input id="txtComisionAfore" type="text" runat="server" readonly="true" />
                        <div id="reference1">
                            <p class="text_gral_center">
                                <a href="#" onclick="Popup.show('mensaje1','reference1','above center',{'constrainToScreen':true});;return false;">
                                    <img src="imagenes/i_grande.png" id="imgInformacion" />
                                </a>

                            </p>
                        </div>

                    </div>
                    <div id="DVFondoDatos">
                        <div class="DVFondoDatos-100">
                            <label for="txtSalarioBaseCotizacion" class="CTRLGeneral">
                                Salario Base de<br />
                                Cotización mensual</label>
                            <input id="txtSalarioBaseCotizacion" name="txtSalarioBaseCotizacion" class="CTRLGeneral validate[required,funcCall[valida_salario_cero], funcCall[valida_salario_minimo]]" type="text" onblur="javascript:asignarValorSalarioMensual(this);" maxlength="10" style="background-color: transparent; border: none;" runat="server">
                            <div id="slider-txtSalarioBaseCotizacion" class="CTRLGeneral slidergral" style="width: 450px; margin-top: 25px; margin-left: 20px;"></div>
                            <div id="reference2">
                                <p class="text_gral_center">
                                    <a href="#" onclick="Popup.show('mensaje2','reference2','above center',{'constrainToScreen':true});;return false;">
                                        <img src="imagenes/i_peque.png" />
                                    </a>

                                </p>
                            </div>
                            <span id="mensaje-txtSalarioBaseCotizacion" class="message" style="display: none;"></span>
                        </div>

                        <div class="DVFondoDatos-50" style="display: none;">
                            <label for="txtSaldoActualAfore" class="CTRLGeneral">
                                Saldo actual<br />
                                en tu AFORE:</label>
                            <input id="txtSaldoActualAfore" name="txtSaldoActualAfore" class="CTRLGeneral inputRadius validate[required, funcCall[valida_saldo_cero]]" type="text" maxlength="12" style="background-color: transparent; border: none;" runat="server">
                            <span id="mensaje-txtSaldoActualAfore" class="message" style="display: none;"></span>
                            <div id="reference8">
                                <p class="text_gral_center">
                                    <a href="#" onclick="Popup.show('mensaje8','reference8','above center',{'constrainToScreen':true});;return false;">
                                        <img src="imagenes/i_peque.png" />
                                    </a>
                                </p>
                            </div>
                        </div>

                        <div class="DVFondoDatos-50">
                            <label for="txtGenero" class="CTRLGeneral" style="margin-top: 20px;">
                                Género:<br />
                                <br />
                            </label>
                            <img src="imagenes/mujer.gif" class="CTRLGeneral" />
                            <asp:RadioButton ID="chkMujer" runat="server" CssClass="checkbox CTRLGeneral" GroupName="Genero" Checked="true" />
                            <img src="imagenes/hombre.gif" class="CTRLGeneral" style="margin-left: 35px;" />
                            <asp:RadioButton ID="chkHombre" runat="server" CssClass="checkbox CTRLGeneral" GroupName="Genero" />                           
                        </div>

                        <div class="DVFondoDatos-50">
                            <label for="txtFechaNacimiento" class="CTRLGeneral">
                                Fecha de<br />
                                nacimiento:</label>
                            <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="CTRLGeneral inputRadius validate[required, funcCall[valida_fecha_vacio]]"
                                AutoCompleteType="Disabled"
                                Style="margin-top: 19px; background-color: transparent; border: none;" placeholder="dd/mm/aaaa"></asp:TextBox>
                            <div id="reference3">
                                <p class="text_gral_center">
                                    <a href="#" onclick="Popup.show('mensaje3','reference3','above center',{'constrainToScreen':true});;return false;">
                                        <img src="imagenes/i_peque.png" />
                                    </a>
                                </p>
                            </div>
                            <a class='inline' href="#msgGenracionTransicion" id="aGeneracionTransicion" style="display: none;">Generacion Transicion</a>
                        </div>

                        <div class="DVFondoDatos-50">
                            <label for="txtEdad" class="CTRLGeneral" style="margin-top: 20px;">
                                Edad:</label>
                            <input id="txtEdad" name="txtEdad" class="CTRLGeneral inputTransparente " style="width: 40px; background-color: transparent; border: none;" type="text" readonly="true">
                        </div>

                        <div class="DVFondoDatos-50">
                            <label for="txtEdadRetiro" class="CTRLGeneral">
                                Edad de<br />
                                Retiro:</label>
                            <div id="slider-txtEdadRetiro" class="CTRLGeneral slidergral" style="width: 120px;"></div>
                            <input id="txtEdadRetiro" name="txtEdadRetiro" class="CTRLGeneral validate[required]" style="width: 40px; background-color: transparent; border: none;" type="text" readonly="true">
                            <div id="reference5">
                                <p class="text_gral_center">
                                    <a href="#" onclick="Popup.show('mensaje5','reference5','above center',{'constrainToScreen':true});;return false;">
                                        <img src="imagenes/i_peque.png" />
                                    </a>
                                </p>
                            </div>
                        </div>

                        <div class="DVFondoDatos-50">
                            <label for="txtAniosCotizados" class="CTRLGeneral" style="margin-top: 20px;">
                                Años cotizados:<br />
                                <br />
                            </label>
                            <div id="slider-txtAniosCotizados" class="CTRLGeneral slidergral" style="width: 120px;"></div>
                            <input id="txtAniosCotizados" name="txtAniosCotizados" class="CTRLGeneral validate[required]" style="width: 40px; background-color: transparent; border: none;" type="text" readonly="true">
                            <div id="reference9">
                                <p class="text_gral_center">
                                    <a href="#" onclick="Popup.show('mensaje9','reference9','above center',{'constrainToScreen':true});;return false;">
                                        <img src="imagenes/i_peque.png" />
                                    </a>
                                </p>
                            </div>
                        </div>

                        <div class="DVFondoDatos-50">
                            <label for="txtRendimientoReralAntesComisiones" class="CTRLGeneral">
                                Rendimiento real<br />
                                antes de comisiones:</label>
                            <div id="slider-txtRendimientoReralAntesComisiones" class="CTRLGeneral slidergral " style="width: 120px;"></div>
                            <input id="txtRendimientoReralAntesComisiones" name="txtRendimientoReralAntesComisiones validate[required]" class="CTRLGeneral" style="width: 40px; background-color: transparent; border: none;" type="text" readonly="true">
                            <div id="reference6">
                                <p class="text_gral_center">
                                    <a href="#" onclick="Popup.show('mensaje6','reference6','above center',{'constrainToScreen':true});;return false;">
                                        <img src="imagenes/i_peque.png" />
                                    </a>
                                </p>
                            </div>
                        </div>

                        <div class="DVFondoDatos-50">
                            <span class="DensidadCotizacionIMSS CTRLGeneral">Densidad de cotización 80%</span>                            
                            <div id="reference7">
                                <p class="text_gral_center">
                                    <a href="#" onclick="Popup.show('mensaje7','reference7','above center',{'constrainToScreen':true});;return false;">
                                        <img src="imagenes/i_peque.png" />
                                    </a>
                                </p>
                            </div>
                        </div>

                    </div>
                    <div id="DVCalculadora-Pie">
                        <div id="DVPieTexto">                          
                            <a href="http://www.gob.mx/consar/acciones-y-programas/calculadoras-de-ahorro-y-retiro?idiom=es">                            
                            </a>
                        </div>
                        <div id="DVCalcular">                           
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/btn_calcula2.png" onmouseover="this.src='imagenes/btn_calcula2.1.png'" onmouseout="this.src='imagenes/btn_calcula2.png'" CssClass="btnCalcular cursorHand submit" type="submit" OnClick="GetResultadoCalculoPrueba();" />
                            <p><a class='inline' href="#inline_content" id="aMensajeError" style='display: none'>Inline HTML</a></p>
                        </div>                       
                        <div style='display: none'>
                            <div id='inline_content' style='padding: 10px; background: #fff;'>
                                <label id="lMensajeError"></label>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="DVContenedorResultado" class="panelOculto">
                    <div id="DVDatosProporcionados" style="display: none;">
                    </div>
                    <div id="DVDatosFormulario" style="display: none;">
                        <div id="DVSalarioBaseCotizacionMensualR" style="display: none">
                            <asp:Label ID="lblSalarioBaseCotizacionMensual" runat="server" Text="" CssClass="lblSalarioBaseCotizacionMensualR"></asp:Label>
                        </div>
                        <div id="DVSaldoActualAforeR" style="display: none">
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
                            <asp:Label ID="lblDensidadCotizacion" runat="server" Text="" CssClass="lblDensidadCotizacionR"></asp:Label>
                        </div>


                    </div>
                    <div id="DVDatosResultados">
                        <p>Resultados</p>
                    </div>
                    <div id="DVResultadoPMT" style="display: none;">
                        <div class="DVFondoDatos-100">
                            <div style="width: 93%">
                                <asp:Label ID="lblSaldoAcumulado" runat="server" Text="" CssClass="lblSaldoAcumuladoR"></asp:Label>
                            </div>
                        </div>
                        <div class="DVFondoDatos-100">

                            <div id="reference11">
                                <p class="text_gral_center">
                                    <a href="#" onclick="Popup.show('mensaje11','reference11','above center',{'constrainToScreen':true});;return false;">
                                        <img src="imagenes/i_peque.png" style="border: none;" />
                                    </a>
                                </p>
                            </div>
                            <div style="width: 93%">
                                <asp:Label ID="lblPensionMensualEstimada" runat="server" Text="" CssClass="lblPensionMensualEstimadaR"></asp:Label>
                            </div>
                        </div>
                        <div class="DVFondoDatos-100">

                            <div id="reference10">
                                <p class="text_gral_center">
                                    <a href="#" onclick="Popup.show('mensaje10','reference10','above center',{'constrainToScreen':true});;return false;">
                                        <img src="imagenes/i_peque.png" style="border: none;" />
                                    </a>
                                </p>
                            </div>
                            <div style="width: 93%">
                                <asp:Label ID="lblPorcentajePensionMensual" runat="server" Text="" CssClass="lblPorcentajePensionMensualR"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div id="DVDatosTePareceSuficiente">
                        <p>
                            ¿Te parece suficiente?
                        </p>
                    </div>
                    <div id="DVResultadoPMT_2" style="display: none;">
                    </div>
                    <div id="DVPMG">


                        <asp:Label ID="lblMejorarPension" runat="server" Text="" CssClass="leftCal lblMejorarPension"></asp:Label>
                    </div>
                    <div id="DVResultadoAV" style="width: 30%">
                    
                    </div>

                    <div id="DVGraficaContenedor" style="float: right; width: 65%;" border="1">
                        <table style="width: 100%; height: 350px;">
                            <tr>
                                <td colspan="2">
                                    <div style="width: 100%; height: 50px; margin-bottom: -20px; text-align: center;">
                                        <p class="texto">¿De cuánto quieres tu pensión?</p>
                                    </div>

                                    <div style="float: left; margin-left: 100px;">
                                        <div id="slider1" style="float: left; width: 200px; margin-left: 30px; margin-top: 20px;"></div>
                                        <input name="amount1" type="text" id="amount1" readonly="" style="border: 0; color: #000; font-weight: bold; font-size: 16px; width: 70px; margin-left: 15px; text-align: center; background-color: transparent; position: relative; top: 18px;" />
                                        <p style="float: left;">
                                            <label for="amount1"></label>
                                        </p>
                                    </div>
                                </td>
                            </tr>
                            <tr>

                                <td>
                                    <div class="dvGrafica">
                                        <div id="barraGrafica">
                                            <div id="barraGraficaProgreso" class="barraGraficaProgreso30">
                                                <asp:Image ID="imgProgresoBarra" runat="server" ImageUrl="imagenes/indicador_grafica.png" CssClass="imgProgresoBarra" />
                                                <input name="amount3" type="text" id="amount3" class="porcentaje porcentajeFlotante" readonly="" />
                                                <input name="txtPensionFlotante" type="text" id="txtPensionFlotante" class="pension pensionFlotante" readonly="" />
                                            </div>
                                            <div id="DVAhorroAdicional">
                                                <p>Al mes tendrías que ahorrar adicionalmente ésta cantidad:</p>
                                                <input name="amount2" type="text" id="amount2" readonly="" style="margin-left: -45px; border: 0; color: #1A9381; font-weight: bold; font-size: 20px; text-align: center; background-color: transparent;" />
                                            </div>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div style="width: 100%; height: 10px;">
                                        <p class="textochico">Porcentaje de tu salario actual</p>
                                    </div>
                                    <div style="margin-top: 50px; display: none;">
                                        <img id="imgBarra" src="imagenes/barra_color.png" alt="Calidad Pensión" />
                                        <img id="imgFlecha" class="flechaIndicadorBaja" src="imagenes/flecha.png" alt="Indicador Calidad Pensión" />
                                    </div>
                                    <div style="width: 203px; height: 368px; margin-top: 50px; position: relative;">
                                        <asp:Image ID="imgBarraTasa" runat="server" ImageUrl="imagenes/barra_tasa_vertical.png" />
                                        <asp:Image ID="imgIndicadorTasa" ClientIDMode="Static" runat="server" ImageUrl="imagenes/boton_barra.png" CssClass="indicadorBarra indicadorBarra30" />
                                    </div>
                                </td>
                            </tr>
                        </table>

                    </div>


                    <asp:Label ID="lblPMG" runat="server">
                            <span class="NotaPMG">* Tiene derecho a la <strong>Pensión Mínima Garantizada (PMG)</strong>, otorgada por el Estado. </span>
                            <br/>
                            <br/>
                            <strong>PMG</strong>: Es aquella que otorga el Gobierno Federal a quienes reúnan los requisitos de las semanas cotizadas (1,250) y la edad (60-65 años), pero que los recursos acumulados en la cuenta AFORE sean insuficientes  para contratar una Renta Vitalicia o un Retiro Programado.<br/><br/>
                    </asp:Label>


                </div>
               
                <div id="DVContenedorResultadoNP" class="panelOculto">
                    <div id="DVDatosProporcionadosNP">
                    </div>
                    <div id="DVDatosFormularioNP" style="display: none;">
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
                        <div id="DVDensidadCotizacionRNP">
                            <asp:Label ID="lblDensidadCotizacionNP" runat="server" Text="" CssClass="lblDensidadCotizacionR"></asp:Label>
                        </div>
                    </div>
                    <div id="DVDatosResultadosNP">
                    </div>
                    <div id="DVResultadoPMTNP" style="display: none;">
                        <div class="DVFondoDatos-96" style="display: none;">
                            <asp:Label ID="lblSaldoAcumuladoNP" runat="server" Text="" CssClass="lblSaldoAcumuladoR"></asp:Label>
                        </div>
                    </div>
                    <div id="DVDatosTePareceSuficienteNP">
                    </div>
                    <div id="DVResultadoPMT_2NP" style="display: none;">
                    </div>

                    <!--necesito esto para ahorita-->
                    <div id="DVResultadoAVNP" style="width: 30%">
                        <!-- Aqui se crea dinamicamente la tabla -->
                    </div>


                </div>
            </div>

        </div>

    </form>
    <!-- INICIO VALORES INICIALES -->
    <script src="js/util.js"></script>

    <script>
        function OtroCalculo() {
            $("#DVContenedorResultado").removeClass("panelVisible").addClass("panelOculto");
            $("#DVContenedorCalculadora").removeClass("panelOculto").addClass("panelVisible");
            $("#DVContenedorResultadoNP").removeClass("panelVisible").addClass("panelOculto");
        }
    </script>
    <!-- FIN VALORES INICIALES -->
</body>
</html>

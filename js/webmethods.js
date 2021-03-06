
var pN_PORC_ANUAL_SALDO;
var pSalarioBaseCotizacionMensual;
var pFechaNacimiento;
var pEdad;
var pEdadRetiro;
var pSaldoActualAfore;
var nSemanasCotizadas;
var pDensidadCotizacion;
var pGenero = 0;
var pRendimiento;
var pComisionAfore;
var pAportacion;
var pFechaCorte;
//var PMG = 2601;
//PGO Inicializamos PGM (PGMIMSS)
var PMGIMSS = 0;
var PME = 0;
var sParametros_Calculo;
var sResultados_Calculo;
var pPorcAportacion;
var valorSlide;

//PGO - Variables ODT-2021
var rRealMensual;  //Rendimiento real mensual antes de comisiones
var cMensualAF;    //Comisión mensual sobre saldo que cobra la AFORE
var tRendimientoMensual; //Tasa de rendmiento mensual
var tRendimientoAnual; //Tasa de rendmiento Anual
var fRetiro;  //Fecha de retiro
var fRetiro_Dia, fRetiro_Mes, fRetiro_Anio;
var fCalculo;



var FechaCambio1;
var FechaCambio2;
var APOI;






// Extend the default Number object with a formatMoney() method:
// usage: someVar.formatMoney(decimalPlaces, symbol, thousandsSeparator, decimalSeparator)
// defaults: (2, "$", ",", ".")
Number.prototype.formatMoney = function (places, symbol, thousand, decimal) {
    places = !isNaN(places = Math.abs(places)) ? places : 2;
    symbol = symbol !== undefined ? symbol : "$";
    thousand = thousand || ",";
    decimal = decimal || ".";
    var number = this,
	    negative = number < 0 ? "-" : "",
	    i = parseInt(number = Math.abs(+number || 0).toFixed(places), 10) + "",
	    j = (j = i.length) > 3 ? j % 3 : 0;
    return symbol + negative + (j ? i.substr(0, j) + thousand : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousand) + (places ? decimal + Math.abs(number - i).toFixed(places).slice(2) : "");
};

var Anexo1 = {
    d: 0,
    rm: 0,
    cm: 0,
    cs: 0,
    mesesParaRetiro: 0,
    saldoActual: 0,
    Sf: 0,
    Ao: 0,
    PApOb: 6.5,
    Tr: 0,
    PMG: 0,
    URV: 0,
    edadRetiro: 0,
    edadActual: 0,
    salarioBase: 0,
    cuotoSocial: 0,
    n: 0,
    fechaCorte: new Date(),
    SD: function () { return salarioBase / 30; },
};

function LeerFormulario() {

    sParametros_Calculo = "";
    sResultados_Calculo = "";

    pFechaNacimiento = document.getElementById('txtFechaNacimiento').value;
    pEdadRetiro = document.getElementById('txtEdadRetiro').value;
    nSemanasCotizadas = document.getElementById('txtSemanasCotizadas').value;

    Anexo1.fechaCorte = new Date();
    Anexo1.fechaCorte = new Date(Anexo1.fechaCorte.getFullYear(), Anexo1.fechaCorte.getMonth(), Anexo1.fechaCorte.getDate());
    Anexo1.edadRetiro = parseInt(pEdadRetiro);
    Anexo1.edadActual = parseInt(document.getElementById('txtEdad').value);

    var psSaldoActualAfore = document.getElementById('txtSaldoActualAfore').value;
    psSaldoActualAfore = psSaldoActualAfore.replace("$", "");
    psSaldoActualAfore = psSaldoActualAfore.replace(",", "");
    pSaldoActualAfore = psSaldoActualAfore;
    Anexo1.saldoActual = parseFloat(pSaldoActualAfore.replace(",", "").replace("$", ""));

    // Se elimina obtencion de campo y se coloca 80
    //var psDensidadCotizacion = document.getElementById('txtDensidadCotizacion').value;
    //psDensidadCotizacion = psDensidadCotizacion.replace("%", "");
    //pDensidadCotizacion = psDensidadCotizacion / 100;
    pDensidadCotizacion = 0.8;
    Anexo1.d = pDensidadCotizacion;


    if (document.getElementById('chkMujer').checked) {
        pGenero = 1; document.getElementById('imgGenero').src = "imagenes/mujer.png";

        // Se egrega la seccion de Negativa de Pension
        pGenero = 1; document.getElementById('imgGeneroNP').src = "imagenes/mujer.png";
    }

    if (document.getElementById('chkHombre').checked) {
        pGenero = 0; document.getElementById('imgGenero').src = "imagenes/hombre.png";

        // Se egrega la seccion de Negativa de Pension
        pGenero = 0; document.getElementById('imgGeneroNP').src = "imagenes/hombre.png";
    }

    var psSalarioBaseCotizacionMensual = document.getElementById('txtSalarioBaseCotizacion').value;
    psSalarioBaseCotizacionMensual = psSalarioBaseCotizacionMensual.replace("$", "");
    psSalarioBaseCotizacionMensual = psSalarioBaseCotizacionMensual.replace(",", "");
    pSalarioBaseCotizacionMensual = psSalarioBaseCotizacionMensual;
    Anexo1.salarioBase = parseFloat(pSalarioBaseCotizacionMensual);



    var psRendimiento = document.getElementById('txtRendimientoReralAntesComisiones').value;
    psRendimiento = psRendimiento.replace("%", "");
    psRendimiento = psRendimiento.replace(" ", "");

    //PGO - ODT-07 Rendimieento real mensual (r^m)
    rRealMensual = Math.pow((1 + (psRendimiento / 100)), 1 / 12) - 1;
    pRendimiento = rRealMensual;
    //pRendimiento = psRendimiento / 100;
    Anexo1.rm = rRealMensual;


    var psComisionAfore = document.getElementById('txtComisionAfore').value;
    psComisionAfore = psComisionAfore.replace("%", "");
    psComisionAfore = psComisionAfore.replace(" ", "");
    //PGO - ODT-07 Comisión Mensual sobre saldo que cobra la AFORE (c^m)
    pComisionAfore = psComisionAfore / 100;
    cMensualAF = pComisionAfore / 12;
    //Anexo1.cm = pComisionAfore;
    Anexo1.cm = cMensualAF;

    //PGO - ODT-07 - Obtenemos la tasa de rendimiento mensual (tr^m)
    tRendimientoMensual = ((1 + rRealMensual) * (1 - cMensualAF)) - 1;

    //PGO - ODT-07 - Obtenemos la tasa de rendimiento anual (tr^a)
    tRendimientoAnual = Math.pow((1 + tRendimientoMensual), 12) - 1;

    //PGO - ODT-07 - Obtener la Fecha de retiro (FR)
    fRetiro_Dia = (parseInt(pFechaNacimiento.substr(0, 2)));
    fRetiro_Mes = (parseInt(pFechaNacimiento.substr(3, 2)));
    fRetiro_Anio = (parseInt(pFechaNacimiento.substr(6, 4)) + parseInt(pEdadRetiro));

    fRetiro = fRetiro_Dia + '/' + fRetiro_Mes + '/' + fRetiro_Anio;
 

    //PGO - ODT-07 - Obtener la Fecha de calculo (FC)
    var fCalculo_Dia, fCalculo_Mes, fCalculo_Anio;
    fCalculo = new Date();
    fCalculo_Dia = parseInt(fCalculo.getDate());
    fCalculo_Mes = parseInt(fCalculo.getMonth() + 1);
    fCalculo_Anio = parseInt(fCalculo.getFullYear());

    //Agregar cero a formato de dia y mes menores a 10
    //if (fCalculo_Dia < 10) {
    //    fCalculo_Dia = '0' + fCalculo_Dia;
    //}

    //if (fCalculo_Mes < 10) {
    //    fCalculo_Mes = '0' + fCalculo_Mes;
    //}
    fCalculo = fCalculo_Dia + '/' + fCalculo_Mes + '/' + fCalculo_Anio;



    //PGO - ODT-07 - Obtener Numero de meses que faltan para que el trabajador cumpla la edad del retiro (n)
    var n;

    fCalculo = new Date(fCalculo_Anio, fCalculo_Mes-1, fCalculo_Dia);
    fRetiro = new Date(fRetiro_Anio, fRetiro_Mes-1, fRetiro_Dia);

    //Cuando FC >= FR entonces n=0
    if (fCalculo >= fRetiro) {
        n = 0;
        Anexo1.n = 0;
    }
    //Si Día(FC) > 15 y Día(FR) <=15
    else if (fCalculo_Dia > 15 && fRetiro_Dia <= 15){
        //(Año (FR)-Año(FC)-1)*12+12-Mes(FC)+Mes (FR)-1
        n = (fRetiro_Anio - fCalculo_Anio - 1) * 12 + 12 - fCalculo_Mes + fRetiro_Mes - 1;
        Anexo1.n = (fRetiro_Anio - fCalculo_Anio - 1) * 12 + 12 - fCalculo_Mes + fRetiro_Mes - 1;
    }
    //Si Dia(FC)<=15 y Día(FR)>15
    else if (fCalculo_Dia <= 15 && fRetiro_Dia > 15) {
        n = (fRetiro_Anio - fCalculo_Anio - 1) * 12 + 12 - fCalculo_Mes + fRetiro_Mes + 1;
        Anexo1.n = (fRetiro_Anio - fCalculo_Anio - 1) * 12 + 12 - fCalculo_Mes + fRetiro_Mes + 1;
    }
    else {
        n = (fRetiro_Anio - fCalculo_Anio - 1) * 12 + 12 - fCalculo_Mes + fRetiro_Mes;
        Anexo1.n = (fRetiro_Anio - fCalculo_Anio - 1) * 12 + 12 - fCalculo_Mes + fRetiro_Mes;
    }

    //PGO - ODT-07 -OBTENER FECHAS DE CAMBIO

    FechaCambio1 = document.getElementById('hdfFechacambio1').value;
    FechaCambio2 = document.getElementById('hdfFechacambio2').value;
    APOI = document.getElementById('HdfAOI').value;

    //PGO - ODT-07 - Obtener primera fecha de cambios
    //var fCambio1;
    var fValidar1;

    var fCambio1_Dia = (parseInt(FechaCambio1.substr(8,2)));
    var fCambio1_Mes = (parseInt(FechaCambio1.substr(5, 2)));
    var fCambio1_Anio = (parseInt(FechaCambio1.substr(0, 4)));

    //fValidar1 = new Date(2022, 12 - 1, 31);
    fValidar1 = new Date(fCambio1_Anio, fCambio1_Mes - 1, fCambio1_Dia);

    //Cuando FC < 31/12/2022 <= FR
    if (fCalculo < fValidar1 && fValidar1 <= fRetiro) {
        FechaCambio1 = fValidar1;
    }
    //Cuando FC >= 31/12/2022
    else if (fCalculo >= fValidar1) {
        FechaCambio1 = fCalculo;
    }
    else {
        FechaCambio1 = fRetiro;
    }
    fCambio1_Dia = parseInt(FechaCambio1.getDate());
    fCambio1_Mes = parseInt(FechaCambio1.getMonth() + 1);
    fCambio1_Anio = parseInt(FechaCambio1.getFullYear());


    //PGO - ODT-07 - Obtener Segunda fecha de cambios
    var fCambio2_Dia = (parseInt(FechaCambio2.substr(8, 2)));
    var fCambio2_Mes = (parseInt(FechaCambio2.substr(5, 2)));
    var fCambio2_Anio = (parseInt(FechaCambio2.substr(0, 4)));

    var fValidar2;
    //fValidar2 = new Date(2030, 12 - 1, 31);
    fValidar2 = new Date(fCambio2_Anio, fCambio2_Mes - 1, fCambio2_Dia);


    //Cuando FC <= 31/12/2030 < FR
    if (fCalculo <= fValidar2 && fValidar2 < fRetiro) {
        FechaCambio2 = fValidar2;
    }
    //Cuando FR <= 31/12/2030
    else if (fRetiro <= fValidar2) {
        FechaCambio2 = fRetiro;
    }
    else {
        FechaCambio2 = fCalculo;
    }

    fCambio2_Dia = parseInt(FechaCambio2.getDate());
    fCambio2_Mes = parseInt(FechaCambio2.getMonth() + 1);
    fCambio2_Anio = parseInt(FechaCambio2.getFullYear());


    //PGO - ODT-07 - Obtención de Salario Diario.
    var SD;

    SD = Math.round((Anexo1.salarioBase / 30.4) * 1000000) / 1000000;



    //PGO - ODT-07 - Obtención de Aportación obligatoria Inicial Mensual.
    var AOI;
   // AOI = 0.065 * Anexo1.salarioBase;
    AOI = APOI * Anexo1.salarioBase;

    //PGO - ODT-07 Numero de meses entre FC1 y la fecha de cálculo.
    var n1;

    if (fCalculo_Dia > 15 && fCambio1_Dia <= 15) {
        n1 = ((fCambio1_Anio - fCalculo_Anio) - 1) * 12 + 12 - fCalculo_Mes + fCambio1_Mes - 1;
    }
    else if (fCalculo_Dia <= 15 && fCambio1_Dia > 15) {
        n1 = ((fCambio1_Anio - fCalculo_Anio) - 1) * 12 + 12 - fCalculo_Mes + fCambio1_Mes + 1;
    }
    else if (fCalculo > fValidar1 || fCalculo > fRetiro) {
        n1 = 0;
    }
    else {
        n1 = (fCambio1_Anio - fCalculo_Anio - 1) * 12 + 12 - fCalculo_Mes + fCambio1_Mes;
    }

   
    //PGO - ODT-07 Numero de meses entre FC1 y FC2.
    var n2 = 0;

    if (fCambio1_Dia > 15 && fCambio2_Dia <= 15) {
        n2 = ((fCambio2_Anio - fCambio1_Anio) - 1) * 12 + 12 - fCambio1_Mes + fCambio2_Mes - 1;
    }
    else if (fCambio1_Dia <= 15 && fCambio2_Dia > 15) {
        n2 = ((fCambio2_Anio - fCambio1_Anio) - 1) * 12 + 12 - fCambio1_Mes + fCambio2_Mes + 1;
    }
    else if (fCalculo > FechaCambio1 || FechaCambio1 >= FechaCambio2) {
        n2 = 0;
    }
    else {
        n2 = ((fCambio2_Anio - fCambio1_Anio) - 1) * 12 + 12 - fCambio1_Mes + fCambio2_Mes;
    }



    // PGO - ODT-07 - Número de periodos que se acumularía la aportación despues del último incremento en la aportación gradual
    var n2p = 0;

    if (fCambio2_Dia > 15) {
        n2p = fCambio2_Mes;
    }
    else {
        n2p = fCambio2_Mes - 1;
    }


    //PGO - ODT-07 Numero de meses entre FR y FC2.
    var n3 = 0;

    if (fCambio2_Dia > 15 && fRetiro_Dia <= 15) {
        n3 = ((fRetiro_Anio - fCambio2_Anio) - 1) * 12 + 12 - fCambio2_Mes + fRetiro_Mes - 1;
    }
    else if (fCambio2_Dia <= 15 && fRetiro_Dia > 15) {
        n3 = ((fRetiro_Anio - fCambio2_Anio) - 1) * 12 + 12 - fCambio2_Mes + fRetiro_Mes + 1;
    }
    else if (fRetiro <= FechaCambio2) {
        n3 = 0;
    }
    else {
        n3 = ((fRetiro_Anio - fCambio2_Anio) - 1) * 12 + 12 - fCambio2_Mes + fRetiro_Mes;
    }



    FechaCambio1 = parseInt(FechaCambio1.getDate()) + "/" + parseInt(FechaCambio1.getMonth() + 1) + "/" + parseInt(FechaCambio1.getFullYear());
    FechaCambio2 = parseInt(FechaCambio2.getDate()) + "/" + parseInt(FechaCambio2.getMonth() + 1) + "/" + parseInt(FechaCambio2.getFullYear());


 

    pAportacion = 6.50 / 100;
    pEdad = document.getElementById('txtEdad').value;

    sParametros_Calculo = pFechaNacimiento + '|' + pEdadRetiro + '|' + nSemanasCotizadas + '|' + pSaldoActualAfore + '|' +
                          pDensidadCotizacion + '|' + pGenero + '|' + pSalarioBaseCotizacionMensual + '|' + pRendimiento + '|' +
                          pComisionAfore + '|' + pAportacion + '|' + pEdad + '|' + parseInt($('#txtAnioAfiliacion').val()) + '|' +
                          pPorcAportacion + '|' + AOI + '|' + tRendimientoMensual + '|' + n1 + '|' + n + '|' + FechaCambio1 + '|' + FechaCambio2 + '|' +
                          n2 + '|' + n2p + '|' + tRendimientoAnual + '|' + n3;


  

    $('#lblSalarioBaseCotizacionMensual').text(Anexo1.salarioBase.formatMoney(0, "$", ",", ".")); //document.getElementById('txtSalarioBaseCotizacion').value;

    $('#lblEdad').text(pEdad);
    $('#lblEdadRetiro').text(pEdadRetiro);
    $('#lblRendimientoRealAntesComisiones').text(document.getElementById('txtRendimientoReralAntesComisiones').value);
    $('#lblAniosCotizados').text(nSemanasCotizadas);
    $('#lblSaldoActualAfore').text(Anexo1.saldoActual.formatMoney(0, "$", ",", "."));//Math.round(parseFloat(document.getElementById('txtSaldoActualAfore').value)); document.getElementById('txtSaldoActualAfore').value;
    // Se coloca fija 80%
    //document.getElementById('lblDensidadCotizacion').innerHTML = document.getElementById('txtDensidadCotizacion').value;
    $('#lblDensidadCotizacion').text('80%');


    // Se agrega la seccion de Negativa de Pension
    $('#lblSalarioBaseCotizacionMensualNP').text(Anexo1.salarioBase.formatMoney(0, "$", ",", "."));//document.getElementById('txtSalarioBaseCotizacion').value;
    $('#lblEdadNP').text(pEdad);
    $('#lblEdadRetiroNP').text(pEdadRetiro);
    $('#lblRendimientoRealAntesComisionesNP').text(document.getElementById('txtRendimientoReralAntesComisiones').value);
    $('#lblAniosCotizadosNP').text(nSemanasCotizadas);
    $('#lblSaldoActualAforeNP').text(Anexo1.saldoActual.formatMoney(0, "$", ",", ".")); //document.getElementById('txtSaldoActualAfore').value;
    $('#lblDensidadCotizacionNP').text('80%');
    //document.getElementById('lblDensidadCotizacionNP').innerHTML = document.getElementById('txtDensidadCotizacion').value;

}



function OnGetFechaCambio1(result) {

    //alert('regreso');
    //alert(result);

    document.getElementById('hdfFechacambio1').value = result;
}



function GetComisionAforeClave() {
    var mySelectedIndex = $("#cboAfore").val();

    PageMethods.GetComisionAfore(mySelectedIndex, OnGetComisionAfore);
}




function OnGetComisionAfore(result) {

    document.getElementById('txtComisionAfore').value = result;

    //GetComisionesAforeClave();

    var sComisiones = document.getElementById('hddLugarComision').value.split('|');
    var mySelectedIndex = $("#cboAfore").val();

    //alert(mySelectedIndex);
    //alert(sComisiones);

    for (var i = 1; i < sComisiones.length; i++) {

        //alert(sComisiones[i]);
        if (mySelectedIndex == sComisiones[i]) {
            //alert('numero: ' + i);
            document.getElementById('boldStuff').innerHTML = i;
            //alert(document.getElementById('boldStuff').value);
            //document.getElementById('lblLugarComision').innerHTML = "La comisión es el cobro que te hace la AFORE por administrar los recursos de tu cuenta AFORE. Actualmente estás en la AFORE No. " + i + "de 11 que hay en el mercado. A menor comisión mayor pensión. Da clic aquí para que conozcas la comisión que cobran las demás AFORE (aparece cuadro de comisiones)";
            //$(document).ready(function () {
            //    $('#descripcion').html(i);
            //});
        }
    }
}




function GetComisionesAforeClave() {

    //alert('carga');
    PageMethods.GetComisionesAfore(1, OnGetComisionesAfore);
}

function OnGetComisionesAfore(result) {

    //alert('regreso');
    //alert(result);

    document.getElementById('hddLugarComision').value = result;

    //alert(document.getElementById('hddLugarComision').value);

    //var sComisiones = result.split('|');
    //var mySelectedIndex = $("#cboAfore").val();

    //alert(mySelectedIndex);
    //alert(sComisiones);

    //for (var i = 1; i < sComisiones.length; i++) {

    //    alert(sComisiones[i]);
    //    if (mySelectedIndex == sComisiones[i])
    //    {

    //    }
    //}


}

//function GetResultadoCalculo() {
//    LeerFormulario();

//    PageMethods.GetCalculo(sParametros_Calculo, OnGetResultadoCalculo);
//}
//function OnGetResultadoCalculo(resultado) {

//}


function GetMensajesValidacion(resultado) {

    var listaResultadoN1 = resultado.split('&');
    if (listaResultadoN1.length != 2) {
        //No hay errores en validación
        if (parseInt($('#txtAnioAfiliacion').val()) < 1997) {
            //disable_scroll();
            $(document).bind('contextmenu', function (e) {
                e.preventDefault();
            });
            $('#btnError').trigger('click'); 
        }
        else {
            mostrarDivCarga();
            //disable_scroll();
            PageMethods.GetCalculo(sParametros_Calculo, OnGetResultadoCalculoPrueba);
            PageMethods.getLineChartData(sParametros_Calculo, Grafica1);
        }
    }
    else {
        //Errores validación
        $('#lMensajeError').text(listaResultadoN1[1]);
        $("#aMensajeError").click();
    }

    $('.notify-close').click(function () {
        //enable_scroll();
        mostrarDivCarga();
        PageMethods.GetCalculo(sParametros_Calculo, OnGetResultadoCalculoPrueba);
    });
}

function GetResultadoCalculoPrueba() {

    IniciarVariablesGraficas();

    $("#cboAfore").blur();
    $("#txtSalarioBaseCotizacion").blur();
    $("#txtSaldoActualAfore").blur();
    $("#txtAnioAfiliacion").blur();
    $('#txtSemanasCotizadas').blur()

    var mensaje = ValidaFechaNacimiento();

    if (mensaje.length > 0) {
        $("#txtFechaNacimiento").blur();
    }

    var erroAfiliacion = validaAfiliacion();
    var banderaNotificacion = false;

    pPorcAportacion = 6.5;

    $('.notify-close').click(function () {
        // Move notification
        banderaNotificacion = true;
    });
    if (document.getElementById('txtComisionAfore').value != "" && (erroAfiliacion == '' || erroAfiliacion == undefined) && $("#txtAnioAfiliacion").val() != '' && $("#txtSalarioBaseCotizacion").val() != '' && $("#txtSemanasCotizadas").val() != "" && $("#txtSaldoActualAfore").val() != "") {
        LeerFormulario();
        if (parseInt($('#txtAnioAfiliacion').val()) < 1997) {
            // Generacion transición
            $("#DVDensidadCotizacionRNP").removeClass("panelVisible").addClass("panelOculto");

            $("#DVCentraResultadosPT").addClass("DVCentraResultadosPT_sd");
            $("#DVSalarioBaseCotizacionMensualRNP").removeClass("DVSalarioBaseCotizacionMensualRNP").addClass("DVSalarioBaseCotizacionMensualRNP_sd");
            $("#DVGeneroRNP").removeClass("DVGeneroRNP").addClass("DVGeneroRNP_sd");
            $("#DVEdadRNP").removeClass("DVEdadRNP").addClass("DVEdadRNP_sd");
            $("#DVEdadRetiroRNP").removeClass("DVEdadRetiroRNP").addClass("DVEdadRetiroRNP_sd");
            $("#DVAniosCotizadosRNP").removeClass("DVAniosCotizadosRNP").addClass("DVAniosCotizadosRNP_sd");
            $("#DVRendimientoRealAntesComisionesRNP").removeClass("DVRendimientoRealAntesComisionesRNP").addClass("DVRendimientoRealAntesComisionesRNP_sd");

            PageMethods.GetMensajesCalculo(sParametros_Calculo, GetMensajesValidacion);
        }
        else {

            $("#DVDensidadCotizacionRNP").removeClass("panelOculto").addClass("panelVisible");

            $("#DVCentraResultadosPT").removeClass("DVCentraResultadosPT_sd");
            $("#DVSalarioBaseCotizacionMensualRNP").removeClass("DVSalarioBaseCotizacionMensualRNP_sd").addClass("DVSalarioBaseCotizacionMensualRNP");
            $("#DVGeneroRNP").removeClass("DVGeneroRNP_sd").addClass("DVGeneroRNP");
            $("#DVEdadRNP").removeClass("DVEdadRNP_sd").addClass("DVEdadRNP");
            $("#DVEdadRetiroRNP").removeClass("DVEdadRetiroRNP_sd").addClass("DVEdadRetiroRNP");
            $("#DVAniosCotizadosRNP").removeClass("DVAniosCotizadosRNP_sd").addClass("DVAniosCotizadosRNP");
            $("#DVRendimientoRealAntesComisionesRNP").removeClass("DVRendimientoRealAntesComisionesRNP_sd").addClass("DVRendimientoRealAntesComisionesRNP");

            PageMethods.GetMensajesCalculo(sParametros_Calculo, GetMensajesValidacion);
        }
    }

    $('.notify-close').click(function () {
        //enable_scroll();
        mostrarDivCarga();
        PageMethods.GetCalculo(sParametros_Calculo, OnGetResultadoCalculoPrueba);
    });
}

function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function Grafica1(resultado) {
    if (resultado) {
        aDataGlobal1 = resultado;
    }
    else {
        resultado = aDataGlobal1;
    }

    //AQUÍ DEBUGUEAR PARA VER QUE SE LLEVA PARA ARMAR TABLA DE ¿CUÁNTO AHORRAR PARA TU RETIRO?
    if (resultado != null)
    {
    var aData = resultado;
    var aLabels = aData[0];
    var aDatasets1 = aData[1];
    var aDatasets2 = aData[2];
    var aT = numberWithCommas(aData[3]);
    var rT = numberWithCommas(aData[4]);
    $('#lblSaldoActual').text("$ " + aT);
    $('#lblRendimientosFuturos').text("$ " + rT);
    $('#lblSaldoActualNP').text("$ " + aT);
    $('#lblRendimientoFuturoNP').text("$ " + rT);

    //document.getElementById('lblSaldoAcumuladoNP').innerHTML = numberWithCommas(aData[3] + aData[4]);

    var ctx = document.getElementById("chartContainer");

    if (G1 != null){
        G1.destroy();
		G1 = null;
	}

    G1 = new Chart(ctx, {
        type: 'line',
        data: {
            labels: aLabels,
            datasets: [
              {
                  label: "RT",
                  backgroundColor: "#225C4F",
                  data: aDatasets1
              }, {
                  label: "AT",
                  backgroundColor: "#BB945B",
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

    var ctx = document.getElementById("Canvas1");
    if (G1NP != null){
        G1NP.destroy();
		G1NP = null;
	}
		
    G1NP = new Chart(ctx, {
        type: 'line',
        data: {
            labels: aLabels,
            datasets: [
              {
                  label: "RT",
                  backgroundColor: "#225C4F",
                  data: aDatasets1
              }, {
                  label: "AT",
                  backgroundColor: "#BB945B",
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
}

function Grafica2() {
 
    //var Edades = [1, 2, 3, 4, 5, 6];
    //var datSe = [0, 0, 0, 0, 0, 0], datSeAux = [0, 0, 0, 0, 0, 0], datSeP = [0, 0, 0, 0, 0, 0];
    //var datSn = [0, 0, 0, 0, 0, 0], datSnAux = [0, 0, 0, 0, 0, 0], datSnP = [0, 0, 0, 0, 0, 0];
    //var datDef = [0, 0, 0, 0, 0, 0];
    //var FNac = new Date(pFechaNacimiento.substr(6, 4), parseInt(pFechaNacimiento.substr(3, 2)) - 1, pFechaNacimiento.substr(0, 2));

    //var dif = 0;

    //Edades[5] = Anexo1.edadRetiro;
    //Edades[0] = (parseInt(Anexo1.edadActual / 10) + 1) * 10
    //dif = parseInt((Edades[5] - Edades[0]) / 5);

    //for (var i = 1; i < 5; i++)
    //    Edades[i] = Edades[i - 1] + dif;

    //var Fcal;
    //var n;
    //var PE = parseFloat($("#PESinRedondeo").text());
    //for (var i = 0; i < 6; i++) {
    //    Fcal = new Date(Edades[i] + FNac.getFullYear(), FNac.getMonth(), FNac.getDate());

    //    n = Fcal.getTime() - Anexo1.fechaCorte.getTime();
    //    n = n / (1000 * 60 * 60 * 24);   // convertir de milisegundos a dias
    //    n = parseInt(n / 365 * 12);

    //    datSe[i] = Anexo1.saldoActual * Math.pow(1 + Anexo1.rm, n) * Math.pow(1 - Anexo1.cm, n) + (
    //                  Anexo1.d * (Anexo1.Ao + Anexo1.cs) * (
    //                      (Math.pow(1 + Anexo1.rm, n) * Math.pow(1 - Anexo1.cm, n) - 1) /
    //                      ((1 + Anexo1.rm) * (1 - Anexo1.cm) - 1)
    //                  ));

    //    datSeAux[i] = datSe[i];

    //    datSe[i] = parseInt(datSe[i]);

    //    datSn[i] = ((PE * Anexo1.URV * 12) - (
    //                 (Anexo1.d * (Anexo1.Ao + Anexo1.cs)) * (
    //                     (Math.pow((1 + Anexo1.rm) * (1 - Anexo1.cm), (Edades[5] - Edades[i]) * 12) - 1) /
    //                              ((1 + Anexo1.rm) * (1 - Anexo1.cm) - 1)))) /
    //                (Math.pow((1 + Anexo1.rm) * (1 - Anexo1.cm), (Edades[5] - Edades[i]) * 12));

    //    datSnAux[i] = datSn[i];
    //    datSn[i] = parseInt(Math.round(datSn[i]));

    //    datDef[i] = datSnAux[i] - datSeAux[i];

    //    datSeP[i] = Math.round(parseFloat(datSe[i] / 1000));
    //    datSnP[i] = Math.round(parseFloat(datSn[i] / 1000));
    //}

    //alert("Barra roja: " + datSeP); //4
    //alert("Barra verde: " + datSnP); //5

    //aDataGlobal2 = [Edades, datDef, datSe, datSn, datSeP, datSnP];

    //var ctx = document.getElementById("myChart2");

    //if (G2 != null) {
    //    G2.destroy();
    //    G2 = null;
    //}

    //G2 = new Chart(ctx, {
    //    type: 'bar',
    //    data: {
    //        labels: Edades,
    //        datasets: [
    //          {
    //              //label: "Ahorro para tu retiro estimado en diferentes edades",
    //              label: "",
    //              backgroundColor: "#FF5C26",
    //              data: datSeP
    //          }, {
    //              //label: "Ahorro requerido para alcanzar la pension deseada",
    //              label: "",
    //              backgroundColor: "#85C94D",
    //              data: datSnP
    //          }
    //        ]
    //    },
    //    options: {
    //        title: {
    //            display: true,
    //            text: ''
    //        },
    //        segmentShowStroke: false,
    //        "hover": { "animationDuration": 0 },
    //        "animation": {
    //            "duration": 1,
    //            "onComplete": function () {
    //                var chartInstance = this.chart,
    //                    ctx = chartInstance.ctx;
    //                ctx.font = Chart.helpers.fontString(Chart.defaults.global.defaultFontSize, Chart.defaults.global.defaultFontStyle, Chart.defaults.global.defaultFontFamily);
    //                ctx.textAlign = 'center';
    //                ctx.textBaseline = 'bottom';
    //                this.data.datasets.forEach(function (dataset, i) {
    //                    var meta = chartInstance.controller.getDatasetMeta(i);
    //                    meta.data.forEach(function (bar, index) {
    //                        var data = dataset.data[index].formatMoney(0, "", ",", ".");;
    //                        ctx.fillText(data, bar._model.x, bar._model.y + 1);
    //                        switch (dataset.label) {
    //                            case 'Africa':
    //                                ctx.fillStyle = "#85C94D";
    //                                break;
    //                            case 'Europe':
    //                                ctx.fillStyle = "#EF4832";
    //                                break;
    //                        }
    //                    });
    //                });
    //            }
    //        },
    //        onClick: function (c, i) {
    //            G2Click(i[0]._index);
    //        },
    //        scales: {
    //            xAxes: [{
    //                gridLines: { display: false },
    //                display: true,
    //                ticks: {
    //                    fontFamily: "Courier New",
    //                    beginAtZero: true,
    //                    display: true,
    //                }
    //            }],
    //            yAxes: [{
    //                stacked: false,
    //                gridLines: { display: false },
    //                display: false,
    //                ticks: {
    //                    fontFamily: "Courier New",
    //                    beginAtZero: true,
    //                    display: false,
    //                }
    //            }]
    //        },
    //        legend: {
    //            "display": false
    //        },
    //        tooltips: {
    //            "enabled": false
    //        }
    //    }
    //});

    //G2Click(0);

    var PE = parseFloat($("#PESinRedondeo").text());
   // alert(sParametros_Calculo);

    PageMethods.getLineChartData2(sParametros_Calculo + "|" + PE, ResultadoGrafica);
}

function ResultadoGrafica(resultado)
{
    var aData = resultado;
    var Edades = aData[0];
    var datSe = aData[1];
    var datSn = aData[2];
    var datSeP = [0, 0, 0, 0, 0, 0];
    var datSnP = [0, 0, 0, 0, 0, 0];
    var datSeAux = [0, 0, 0, 0, 0, 0];
    var datSnAux = [0, 0, 0, 0, 0, 0];
    var datDef = [0, 0, 0, 0, 0, 0];
    var FNac = new Date(pFechaNacimiento.substr(6, 4), parseInt(pFechaNacimiento.substr(3, 2)) - 1, pFechaNacimiento.substr(0, 2));
    
    for (var i = 0; i < 6; i++) {
        Fcal = new Date(Edades[i] + FNac.getFullYear(), FNac.getMonth(), FNac.getDate());

        datSeAux[i] = datSe[i];
        datSe[i] = parseInt(datSe[i]);
        datSnAux[i] = datSn[i];
        datSn[i] = parseInt(Math.round(datSn[i]));
        datDef[i] = datSnAux[i] - datSeAux[i];
        datSeP[i] = Math.round(parseFloat(datSe[i] / 1000));
        datSnP[i] = Math.round(parseFloat(datSn[i] / 1000));
        }
    

    aDataGlobal2 = [Edades, datDef, datSe, datSn, datSeP, datSnP];

    var ctx = document.getElementById("myChart2");

    if (G2 != null) {
        G2.destroy();
        G2 = null;
    }

    G2 = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: Edades,
            datasets: [
              {
                  //label: "Ahorro para tu retiro estimado en diferentes edades",
                  label: "",
                  backgroundColor: "#225C4F",
                  data: datSeP
              }, {
                  //label: "Ahorro requerido para alcanzar la pension deseada",
                  label: "",
                  backgroundColor: "#BB945B",
                  data: datSnP
              }
            ]
        },
        options: {
            title: {
                display: true,
                text: ''
            },
            segmentShowStroke: false,
            "hover": { "animationDuration": 0 },
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
                            var data = dataset.data[index].formatMoney(0, "", ",", ".");;
                            ctx.fillText(data, bar._model.x, bar._model.y + 1);
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
            onClick: function (c, i) {
                G2Click(i[0]._index);
            },
            scales: {
                xAxes: [{
                    gridLines: { display: false },
                    display: true,
                    ticks: {
                        fontFamily: "Courier New",
                        beginAtZero: true,
                        display: true,
                    }
                }],
                yAxes: [{
                    stacked: false,
                    gridLines: { display: false },
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

    G2Click(0);
}

function G2Click(columna) {
    var PMEstimada1 = $('#txtP2PDIzq').text();

    var PMEstimada2 = $('#txtP2PDCen').text();
    var PMEstimada3 = $('#txtP2PDDer').text();
   // var PMEstimada = $("#txtP2PD").text();
   
    Actual = columna
    $("#lblP2Edad").text(aDataGlobal2[0][columna] + " años");
    //$("#lblP2SaldoPensionario").text((aDataGlobal2[3][columna]).formatMoney(0, "$", ",", "."));
    //$("#lblP2Deficit").text(Math.round(aDataGlobal2[1][columna]).formatMoney(0, "$", ",", "."));

    $("#lblP2SaldoPensionario").text(parseFloat((aDataGlobal2[3][columna])).formatMoney(0, "$", ",", "."));
    $("#lblP2Deficit").text(parseFloat((aDataGlobal2[1][columna])).formatMoney(0, "$", ",", "."));
   
    

    //if (valorSlide = 1)
    //{
    //    alert(valorSlide)
    //}
    //else

    if (valorSlide == 1) {
        $('#lblP2PMECopia').text(PMEstimada1)
        //  $("#PESinRedondeo").text(parseFloat($("#txtP2PDIzqNP").text().replace("$", "").replace(",", "")));
    }

    else if (valorSlide == 2) {
        $('#lblP2PMECopia').text(PMEstimada2)
    }
    else if (valorSlide == 3) {
        $('#lblP2PMECopia').text(PMEstimada3)
    }

    else {
        $('#lblP2PMECopia').text(PMEstimada1)
    }

    //else {
    //    $('#lblP2PMECopia').text(PMEstimada1)
    //}
    //        //else if (ui.value == 2) {
            //    $("#PESinRedondeo").text(parseFloat($("#txtP2PDCenNP").text().replace("$", "").replace(",", "")));
            //}
            //else {
            //    $("#PESinRedondeo").text(parseFloat($("#txtP2PDDerNP").text().replace("$", "").replace(",", "")));
            //}

    //    alert(valorSlide)
   
    //$("#lblP2PMECopia").slider({
    //        range: "min",
    //        animate: true,
    //        value:0,
    //        min: 1,
    //        max: 3,
    //        step: 1,
    //        slide: function(event, ui) {
    //           // update(2,ui.value); //changed
    //        }
    //    });


    //$("#divSliderN").slider({
    //    range: "min",
    //    min: 1,
    //    max: 3,
    //    //value: contMin,
        
    //    slide: function (event, ui) {

    //    }

        //slide: function (event, ui) {
        //    if (ui.value == 1) {
        //        $("#PESinRedondeo").text(parseFloat($("#txtP2PDIzqNP").text().replace("$", "").replace(",", "")));
        //    }
        //    else if (ui.value == 2) {
        //        $("#PESinRedondeo").text(parseFloat($("#txtP2PDCenNP").text().replace("$", "").replace(",", "")));
        //    }
        //    else {
        //        $("#PESinRedondeo").text(parseFloat($("#txtP2PDDerNP").text().replace("$", "").replace(",", "")));
        //    }
        //    Grafica2();
        //}
    //});

    // $('#lblP2PMECopia').text(iPMG2);
   // $("#txtP2PD").text(parseInt(v));

    //$('#lblP2PMECopia').text(PMEstimada)
}

function OnGetResultadoCalculoPrueba(resultado) {
    //alert(resultado);
    $("#DVResultadoAV").empty(); //MC
    $("#DVResultadoPMT_2").removeClass("panelOculto").addClass("panelVisible");

    //PGO Obtener PMGIMSS de BD
    //var PMG = 0;  // RARS
    var PMGBD = 11;

    $.ajax({
        async: false,
        type: "POST",
        url: "CalculadoraIMSS.aspx/ObtenerParametros",
        contentType: "application/json; charset=utf-8",
        data: "{}",
        dataType: "json",
        success: function (data) {

            $.each(data.d, function (i, item) {
                if (item.Id == PMGBD) {
                    PMGIMSS = item.Valor;
                }
            });
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.responseText);
        }
    });

   


    //var iPMG = PMGIMSS.formatMoney() + " *";

    
    



    //var iPorcientoPMG = (2601.00 / pSalarioBaseCotizacionMensual) * 100;
    //var iPorcientoPMG = (PMGIMSS / pSalarioBaseCotizacionMensual) * 100;

    

    var listaResultadoN1 = resultado.split('&');
    var ArrayResultadosN1 = listaResultadoN1[0].split('|');

    if (ArrayResultadosN1[0] != "TRM" && ArrayResultadosN1[0] != "NP" && ArrayResultadosN1[0] != "PT"  && ArrayResultadosN1[0] != "") {
        //PGO - ODT-07
        
        var iPMG_Aux = ArrayResultadosN1[7].split('|');
        var iPMG = parseFloat(iPMG_Aux);
    }
    else if (ArrayResultadosN1[0] == "TRM") {
        var iPMG_Aux = listaResultadoN1[9];
        var iPMG = parseFloat(iPMG_Aux);
    }
    else if (ArrayResultadosN1[0] == "NP") {
        var iPMG_Aux = listaResultadoN1[1];
        var iPMG = parseFloat(iPMG_Aux.replace("$", "").replace(",", ""));


        //(Anexo1.Sf.replace("$", "").replace(",", ""));
    }

    else if (ArrayResultadosN1[0] == "PT") {
        var iPMG_Aux = listaResultadoN1[1];
        var iPMG = parseFloat(iPMG_Aux.replace("$", "").replace(",", ""));


        //(Anexo1.Sf.replace("$", "").replace(",", ""));
    }

   


    var iPorcientoPMG = (iPMG / pSalarioBaseCotizacionMensual) * 100;
    var iPorcientoPMG = iPorcientoPMG.toFixed(1) + "%";

    iPMG = iPMG.formatMoney() + " *";
  

   ////////// iPMG = iPMG.formatMoney() + " *";

    $("#DVContenedorResultado").removeClass("panelOculto").addClass("panelVisible");
    $("#divResumenAll").removeClass("panelOculto").addClass("panelVisible");
    if (listaResultadoN1[0] == "NP") {
        $("#divResumen").removeClass("panelVisible").addClass("panelOculto");
        $("#DVDatosFormulario").removeClass("panelVisible").addClass("panelOculto");

        $("#divResumenNP").removeClass("panelOculto").addClass("panelVisible");
        $("#lblSaldoActualNP").addClass("muevePosicion");
        $("#lblRendimientoFuturoNP").addClass("muevePosicion");
        $("#DVDatosFormularioNP").removeClass("panelOculto").addClass("panelVisible");
    }
    else {
        $("#divResumenNP").removeClass("panelVisible").addClass("panelOculto");
        $("#lblSaldoActualNP").removeClass("muevePosicion");
        $("#lblRendimientoFuturoNP").removeClass("muevePosicion");
        $("#DVDatosFormularioNP").removeClass("panelVisible").addClass("panelOculto");

        $("#divResumen").removeClass("panelOculto").addClass("panelVisible");
        $("#DVDatosFormulario").removeClass("panelOculto").addClass("panelVisible");
    }
    $("#divP2").removeClass("panelVisible").addClass("panelOculto");
    $("#divP2NP").removeClass("panelVisible").addClass("panelOculto");
    $("#divP3").removeClass("panelVisible").addClass("panelOculto");
    $("#divP4").removeClass("panelVisible").addClass("panelOculto");
    //$("#DVPieTexto").removeClass("panelVisible").addClass("panelOculto");


    if (listaResultadoN1[0] != "NP" && listaResultadoN1[0] != "PT") {
        //Caso A1 al A4
        var listaResultadoN2 = listaResultadoN1[0].split('|');

        //alert(listaResultadoN2);

        if (listaResultadoN1.length != 2) {
            Anexo1.mesesParaRetiro = 0;
            Anexo1.URV = 0;
            Anexo1.cs = 0;
            
            if (listaResultadoN2[0] != "PT" && listaResultadoN1[0] != "TRM") {
                if (listaResultadoN1[1] != 0) {
                    $('#lblSaldoAcumulado').text(listaResultadoN2[0]);
                    $('#lblP4Saldo').text(listaResultadoN2[0]);
                    $('#lblPensionMensualEstimada').text(listaResultadoN2[1]);
                    $('#lblP4Pension').text(listaResultadoN2[1]);
                    $('#txtP3PensionSinAhorro').text(listaResultadoN2[1]);
                    $('#lblP2PME').text(listaResultadoN2[1]);
                    $('#lblPorcentajePensionMensual').text(listaResultadoN2[2]);
                    $('#amount31').val(listaResultadoN2[2]);
                    barraProgreso1();
                    //MC
                    //document.getElementById('lblPorcentajePensionMensual').innerHTML = Math.round(parseFloat(listaResultadoN2[2])) + "%"; //Redondea el porcentaja y pone el signo %
                }
                else {
                    $('#lblSaldoAcumulado').text(listaResultadoN2[0]);
                    $('#lblP4Saldo').text(listaResultadoN2[0]);
                    $('#lblPensionMensualEstimada').text(iPMG);
                    $('#lblP4Pension').text(iPMG);
                    $('#txtP3PensionSinAhorro').text(iPMG);
                    $('#lblP2PME').text(iPMG);
                    $('#lblPorcentajePensionMensual').text(iPorcientoPMG);
                    $('#amount31').val(iPorcientoPMG);
                    barraProgreso1();
                }
                Anexo1.Sf = $('#lblSaldoAcumulado').text();
                Anexo1.Tr = $('#lblPorcentajePensionMensual').text();
                //$('#lblPensionMensualEstimada').text(iPMG);
                //$('#lblP4Pension').text(iPMG);
                //$('#txtP3PensionSinAhorro').text(iPMG);
                Anexo1.mesesParaRetiro = listaResultadoN2[3];
                Anexo1.URV = listaResultadoN2[4];
                Anexo1.cs = parseFloat(listaResultadoN2[5]) / Anexo1.d;
                Anexo1.Ao = listaResultadoN2[6];
            }
            else if (listaResultadoN1[4] == 0) {
                $('#lblPensionMensualEstimada').text(iPMG);
                $('#lblP4Pension').text(iPMG);
                $('#txtP3PensionSinAhorro').text(iPMG);
                $('#lblP2PME').text(iPMG);
                $('#lblPorcentajePensionMensual').text(iPorcientoPMG);
                $('#amount31').val(iPorcientoPMG);
                barraProgreso1();
                Anexo1.Sf = "0";
                Anexo1.Tr = iPorcientoPMG;
                Anexo1.mesesParaRetiro = listaResultadoN1[5];
                Anexo1.URV = "0"; //listaResultadoN2[6];
                Anexo1.cs = "0"; //listaresultadocuadro[7];
                Anexo1.Ao = "0"; //listaresultadocuadro[8];
            }
            else if (listaResultadoN1[4] == 1) {
                $('#lblSaldoAcumulado').text(listaResultadoN1[3]);
                $('#lblP4Saldo').text(listaResultadoN1[3]);
                $('#lblPensionMensualEstimada').text(listaResultadoN1[1]);
                $('#lblP4Pension').text(listaResultadoN1[1]);
                $('#txtP3PensionSinAhorro').text(listaResultadoN1[1]);
                $('#lblP2PME').text(listaResultadoN1[1]);
                $('#lblPorcentajePensionMensual').text(parseFloat(listaResultadoN1[2]).toFixed(1) + "%");
                $('#amount31').val(parseFloat(listaResultadoN1[2]).toFixed(1) + "%");
                barraProgreso1();

                Anexo1.Sf = listaResultadoN1[3];
                Anexo1.Tr = parseFloat(listaResultadoN1[2]).toFixed(1);
                //Anexo1.mesesParaRetiro = listaResultadoN2[5];
                Anexo1.mesesParaRetiro = listaResultadoN1[5];
                //Anexo1.URV = listaResultadoN2[6];
                Anexo1.URV = listaResultadoN1[6];
                Anexo1.cs = parseFloat(listaResultadoN2[6]) / Anexo1.d;
                Anexo1.Ao = listaResultadoN2[8];
            }
            Anexo1.Sf = parseFloat(Anexo1.Sf.replace("$", "").replace(",", ""));
            Anexo1.Tr = parseFloat(Anexo1.Tr.replace("%", "").replace(",", ""));
            Anexo1.rm = Math.pow((1 + Anexo1.rm), 1 / 12) - 1;
            Anexo1.cm = Anexo1.cm / 12;
            Anexo1.cs = parseFloat(Anexo1.cs);
            Anexo1.Ao = parseFloat(Anexo1.Ao);
            Anexo1.URV = parseFloat(Anexo1.URV);

            if (listaResultadoN1[4] != "0" && listaResultadoN1[4] != "1" && listaResultadoN2[0] == "PT" && listaResultadoN1[0] == "TRM") {
                $("#DVContenedorCalculadora").removeClass("panelVisible").addClass("panelOculto");
                $("#DVContenedorResultado").removeClass("panelOculto").addClass("panelVisible");
                $('#lblPensionMensualEstimada').text(iPMG);
                $('#lblP4Pension').text(iPMG);
                $('#txtP3PensionSinAhorro').text(iPMG);
                $('#lblP2PME').text(iPMG);
                $('#lblPorcentajePensionMensual').text(iPorcientoPMG);
                $('#amount31').val(iPorcientoPMG);
                barraProgreso1();

                $("#DVResultadoPMT_2").removeClass("panelVisible").addClass("panelOculto");
                $(document).ready(function () { //MC
                    $("#DVResultadoAV").empty(); //MC
                    DibujarGraficas();
                }); //MC
            }
            else { // (Pensión Mínima Garantizada) PMG == 1, No es pensión Mínima  
                if (listaResultadoN1[0] == "PT") {
                    // Se muestra mensaje de Generacion de Transicion
                    //$('#btnError').trigger('click');
                    //Es Trabajador Transición.
                    $("#DVResultadoAVNP").empty();
                    $("#DVResultadoAVNP").removeClass("panelVisible").addClass("panelOculto");
                    $("#DVContenedorCalculadora").removeClass("panelVisible").addClass("panelOculto");
                    $("#DVSaldoActualAforeRNP").removeClass("panelVisible").addClass("panelOculto");
                    $("#DVContenedorResultadoNP").removeClass("panelOculto").addClass("panelVisible");
                    $("#DVNotaDensidad").removeClass("panelVisible").addClass("panelOculto");
                    $("#DVDatosFormularioNP").removeClass("DVDatosFormularioNP").addClass("DVDatosFormularioNP_ODT5");
                    $("#DVResultadoPMT_2NP").removeClass("DVResultadoPMT_2NP_ODT").addClass("DVResultadoPMT_2NP_ODT");

                    $("#DVResultadoPMTNP").removeClass("panelVisible").addClass("panelOculto");
                    $("#DVDatosTePareceSuficienteNP").removeClass("panelVisible").addClass("panelOculto");
                    var listaResultadoN2 = listaResultadoN1[1].split('|');
                    //Construccion de Encabezado de tabla
                    var encabezadoTabla = "<table id='tblTablaResultadoNP_ODT5'><tr><th colspan='2'class='InformacionTabla'>Ahorro voluntario mensual</th><th colspan='3' class='InformacionTabla'>Ahorro voluntario<br/>acumulado</th></tr>";
                    //encabezadoTabla += "<tr><th class='RegistrostablaNP'>% del Salario</th><th class='RegistrostablaNP'>expresado en pesos</th><th class='RegistrostablaNP'>Por ahorro voluntario</th><th class='RegistrostablaNP'>Por ahorro obligatorio</th><th class='RegistrostablaNP'>Total ahorrado al momento del retiro<sup>1/</sup></th></tr>";
                    encabezadoTabla += "<tr><td colspan='5' class='RegistrostablaNP' style='padding-top: 0px; padding-bottom: 0px;'><img src='imagenes/encabezado_mejora_NP_ODT5.gif' ></td></tr>";
                    var cuerpoTabla = "";
                    var finTabla = "</table>";
                    //Se inicia en el 1 ya que lo anterior son los valores comunes del calculo
                    for (var i = 5; i < listaResultadoN1.length; i++) {
                        var listaresultadocuadro = listaResultadoN1[i].split('|');
                        cuerpoTabla += "<tr>";
                        for (var ir = 0; ir < listaresultadocuadro.length; ir++) {
                            switch (ir) {
                                case 0:
                                    cuerpoTabla += "<td style='width:100px;'>" + listaresultadocuadro[ir] + "</td>";
                                    break;
                                case 1:
                                    cuerpoTabla += "<td>$" + listaresultadocuadro[ir] + "</td>";
                                    break;
                                case 2:
                                    cuerpoTabla += "<td style='width:280px;'>$" + listaresultadocuadro[ir] + "</td>";
                                    break;
                            }
                        }
                        cuerpoTabla += "</tr>";
                    }
                    $(document).ready(function () { //MC
                        $("#DVResultadoAVNP_ODT5").empty(); //MC
                        $("#DVResultadoAVNP_ODT5").append(encabezadoTabla + cuerpoTabla + finTabla);
                        $("#DVResultadoAVNP_ODT5").removeClass("panelOculto").addClass("panelVisible");
                        DibujarGraficas();
                    }); //MC
                }
                else
                    if (listaResultadoN1[0] == "TRM") {

                        if ((parseFloat($("#lblPorcentajePensionMensual").text())) >= 100) {
                            //$("#imgCaminoMejorPension").addClass("panelVisible");
                            //$("#imgAhorrasSuficiente").addClass("panelVisible");
                            $("#divOpciones").addClass("panelVisible");
                        }

                        //if (parseInt($('#txtAnioAfiliacion').val()) < 1997) {
                        //    // Se muestra mensaje de Generacion de Transicion
                        //    //$('#btnError').trigger('click');
                        //}
                        $("#DVGraficaContenedor").removeClass("panelVisible").addClass("panelOculto");
                        $("#DVResultadoAVNP").empty();
                        $("#DVResultadoAVNP").removeClass("panelVisible").addClass("panelOculto");
                        $("#DVContenedorCalculadora").removeClass("panelVisible").addClass("panelOculto");
                        $("#DVSaldoActualAforeR").removeClass("panelOculto").addClass("panelVisible");
                        $("#DVContenedorResultado").removeClass("panelOculto").addClass("panelVisible");
                        $("#DVDatosFormulario").removeClass("DVDatosFormularioNP_ODT5").addClass("DVDatosFormularioNP");
                        $("#DVResultadoPMT").removeClass("panelOculto").addClass("panelVisible");
                        $("#DVResultadoPMT_2").removeClass("DVResultadoPMT_2_1").addClass("DVResultadoPMT_2_2");
                        if (listaResultadoN1[4] == "1") {
                            $("#DVResultadoPMT_2").removeClass("panelVisible").addClass("panelOculto");
                            $("#DVResultadoAV_ODT100").removeClass("panelVisible").addClass("panelOculto");
                            $("#DVNotaAhorro").removeClass("panelVisible").addClass("panelOculto");
                            $("#DVNota_ODT5").removeClass("panelVisible").addClass("panelOculto");
                        }
                        else {
                            $("#DVResultadoPMT_2").removeClass("panelOculto").addClass("panelVisible");
                            $("#DVResultadoAV_ODT100").removeClass("panelOculto").addClass("panelVisible");
                            $("#DVNotaAhorro").removeClass("panelOculto").addClass("panelVisible");
                            $("#DVNota_ODT5").removeClass("panelVisible").addClass("panelOculto");
                        }

                        $("#spanNotaPMG").removeClass("spanNotaPMG").addClass("spanNotaPMG_100");
                        $("#DVResultadoAV").empty(); //MC
                        $("#DVResultadoAV").removeClass("panelVisible").addClass("panelOculto");

                        //alert("5");
                        $('#lblSaldoAcumulado').text(listaResultadoN1[3]);
                        $("#lblP4Saldo").text(listaResultadoN1[3]);

                        //Construccion de Encabezado de tabla
                        var encabezadoTabla = "<table id='tblTablaResultadoNP_ODT5'><tr><th colspan='2'class='InformacionTabla'>Ahorro voluntario mensual</th><th colspan='3' class='InformacionTabla'>Ahorro voluntario<br/>acumulado</th></tr>";
                        encabezadoTabla += "<tr><td colspan='5' class='RegistrostablaNP' style='padding-top: 0px; padding-bottom: 0px;'><img src='imagenes/encabezado_mejora_NP_ODT5.gif' ></td></tr>";

                        var cuerpoTabla = "";
                        var finTabla = "</table>";

                        //Se inicia en el 5 ya que lo anterior son los valores comunes del calculo
                        for (var i = 5; i < listaResultadoN1.length; i++) {
                            var listaresultadocuadro = listaResultadoN1[i].split('|');
                            cuerpoTabla += "<tr>";
                            for (var ir = 0; ir < listaresultadocuadro.length; ir++) {
                                switch (ir) {
                                    case 0:
                                        cuerpoTabla += "<td style='width:100px;'>" + listaresultadocuadro[ir] + "</td>";
                                        break;
                                    case 1:
                                        cuerpoTabla += "<td>$" + listaresultadocuadro[ir] + "</td>";
                                        break;
                                    case 2:
                                        cuerpoTabla += "<td style='width:280px;'>$" + listaresultadocuadro[ir] + "</td>";
                                        break;
                                }
                            }
                            cuerpoTabla += "</tr>";
                        }
                        $(document).ready(function () { //MC
                            //$("#DVResultadoAV_ODT100").empty(); //MC
                            $("#DVResultadoAV_ODT100").append(encabezadoTabla + cuerpoTabla + finTabla);
                            DibujarGraficas();
                        }); //MC
                    }
                    else {
                        $("#DVResultadoAV_ODT100").empty();
                        $("#DVResultadoAV_ODT100").removeClass("panelVisible").addClass("panelOculto");
                        $("#DVNotaAhorro").removeClass("panelVisible").addClass("panelOculto");
                        if (listaResultadoN1[1] == "1") {
                            $("#DVResultadoPMT_2").removeClass("panelVisible").addClass("panelOculto");
                            $("#DVResultadoAV_ODT100").removeClass("panelVisible").addClass("panelOculto");
                            $("#DVNotaAhorro").removeClass("panelVisible").addClass("panelOculto");
                            $("#DVNota_ODT5").removeClass("panelVisible").addClass("panelOculto");

                        } else {
                            $("#DVResultadoPMT_2").removeClass("panelOculto").addClass("panelVisible");
                            $("#DVResultadoAV_ODT100").removeClass("panelVisible").addClass("panelOculto");
                            $("#DVNotaAhorro").removeClass("panelVisible").addClass("panelOculto");
                            $("#DVNota_ODT5").removeClass("panelVisible").addClass("panelOculto");
                        }

                        var CalculoInfinito = false;
                        var listaResultadoN3 = listaResultadoN1[2].split('|');

                        var stringtable = "";
                        for (var i = 1; i < listaResultadoN3.length; i++) {
                            var listaresultadocuadro = listaResultadoN3[i].split('*');
                            var orale = listaresultadocuadro[2].indexOf("-");
                            if (orale != 0) {
                                //var lstcuadro = listaresultadocuadro[2].replace('$', '');
                                //var lstcuadro2 = lstcuadro.replaceAll(',', '');
                                //var lstcuadro3 = Math.round(lstcuadro2, 0);
                                //var lstcuadro4 = formatNumber(parseFloat(lstcuadro3));
                                //stringtable = stringtable + "<tr style=\"\"><td>" + listaresultadocuadro[0] + "</td><td>" + "$ " + lstcuadro4 + "</td></tr>";
                                stringtable = stringtable + "<tr style=\"\"><td>" + listaresultadocuadro[0] + "</td><td>" + "$ " + formatNumber(parseFloat(Math.round(listaresultadocuadro[2].replace('$', '').replaceAll(',', ''))), 0) + "</td></tr>";
                            }
                            CalculoInfinito = isNaN(listaresultadocuadro[2].replace('$', '').replace(',', '').replace(',', ''));
                        }

                        //Muestra gráfica
                        $("#DVGraficaContenedor").removeClass("panelOculto").addClass("panelVisible");

                        $("#DVResultadoPMT_2").removeClass("DVResultadoPMT_2_2").addClass("DVResultadoPMT_2_1");
                        $("#DVResultadoPMT_2").removeClass("panelOculto").addClass("panelVisible");

                        stringtable = "<table id='tblTablaResultado' class=\"tblTablaResultadoImg\"><tr><th class=\"Dinero\">&nbsp;</th><th class=\"Cerdo\">&nbsp;</th></tr>" + stringtable + "</table>"

                        //Genera gráfica
                        Grafica(listaResultadoN1[3], listaResultadoN1[4], listaResultadoN1[5]);
                        $("#DVContenedorCalculadora").removeClass("panelVisible").addClass("panelOculto");
                        $("#DVContenedorResultado").removeClass("panelOculto").addClass("panelVisible");

                        //$(document).ready(function () {
                        $("#DVResultadoAV").empty(); //MC
                        $("#DVResultadoAV").append(stringtable);
                        document.getElementById("DVResultadoAV").innerHTML = stringtable;
                        $("#DVResultadoAV").removeClass("panelOculto").addClass("panelVisible");
                        DibujarGraficas();
                        //});

                    }
            }

            //var PM = parseFloat($('#lblPensionMensualEstimada').text().replace("$", ""));
            //PM = $('#lblPorcentajePensionMensual').text();
            //PM = PM.replace(" ","");
            //PM = PM.substr(0, 1) + "0";

            //$("#amount31").text(document.getElementById('lblPorcentajePensionMensual').innerHTML);
            //$("#txtP3PensionSinAhorro").text($("#lblPensionMensualEstimada").text());
            //$("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso20 barraGraficaProgreso30 barraGraficaProgreso40 barraGraficaProgreso50 barraGraficaProgreso60 barraGraficaProgreso70 barraGraficaProgreso80 barraGraficaProgreso90 barraGraficaProgreso100");
            //$("#barraGraficaProgreso1").addClass("barraGraficaProgreso"+PM);

            //$("#lblP4Saldo").text("");
            //$("#lblP4Saldo").text($("#lblSaldoAcumulado").text());
            //$("#lblP4Pension").text($("#lblPensionMensualEstimada").text());

            var PM = parseFloat($('#lblPensionMensualEstimada').text().replace("$", "").replace(",", ""));
            $("#divOpciones img").removeClass("panelOculto");

            //if (Anexo1.Tr < 100) {  // A1
            //    tipoCaso = "A1";
            //} else {
            //    tipoCaso = "A2";
            //}

            if (PM > parseFloat(iPMG.replace("$", "").replace(",", ""))) {  // casos A1 y A2
                $("#DVlblPMG").removeClass("panelVisible").addClass("panelOculto");
                if (Anexo1.Tr < 100) {  // A1
                    tipoCaso = "A1";
                  $("#divOpciones").addClass("panelVisible");
                    //$("#imgAhorrasSuficiente").addClass("botonVisible");
                    //$("#imgPequenosEsfuerzos").addClass("botonVisible");


                  if (parseInt(lblEdad.innerText) >= 50) {
                      $("#imgCaminoMejorPension").addClass("botonOculto");
                  } else {
                      $("#imgCaminoMejorPension").removeClass("botonOculto");
                      //PGO
                      $("#imgPequenosEsfuerzos").removeClass("botonOculto");
                      $("#imgAhorrasSuficiente").removeClass("botonOculto");


                  //if (parseInt(lblEdad.innerText) >= 60) {
                  //    $("#imgCaminoMejorPension").addClass("botonOculto");
                  //} else {
                  //    $("#imgCaminoMejorPension").removeClass("botonOculto");
                  //    //PGO
                  //    $("#imgPequenosEsfuerzos").removeClass("botonOculto");
                  //    $("#imgAhorrasSuficiente").removeClass("botonOculto");

                  }
               }
               else {  //  A2
                    tipoCaso = "A2";
                    //$("#imgCaminoMejorPension").addClass("botonVisible");
                  $("#divOpciones").addClass("panelVisible");
                  $("#imgCaminoMejorPension").addClass("botonOculto");
                  $("#imgAhorrasSuficiente").addClass("botonOculto");
                    //$("#imgPequenosEsfuerzos").addClass("botonVisible");
               }
            }
            else { // casus  A3 y A4
                $("#DVlblPMG").removeClass("panelOculto").addClass("panelVisible");
               if (Anexo1.Tr < 100) {  // A3
                   tipoCaso = "A3";
                   $("#divOpciones").addClass("panelVisible");
                   // $("#imgAhorrasSuficiente").addClass("botonVisible");
                   // $("#imgPequenosEsfuerzos").addClass("botonVisible");

                   if (parseInt(lblEdad.innerText) >= 50) {
                       $("#imgCaminoMejorPension").addClass("botonOculto");
                   } else {
                     //  $("#imgCaminoMejorPension").removeClass("botonOculto");
                       //PGO
                       $("#imgCaminoMejorPension").removeClass("botonOculto");
                       $("#imgPequenosEsfuerzos").removeClass("botonOculto");
                       $("#imgAhorrasSuficiente").removeClass("botonOculto");
                   }
               }
               else {  //  A4
                   tipoCaso = "A4";
                  $("#divOpciones").addClass("panelVisible");
                  $("#imgCaminoMejorPension").addClass("botonOculto");
                  $("#imgAhorrasSuficiente").addClass("botonOculto");
                  //$("#imgPequenosEsfuerzos").addClass("botonVisible");
               }
            }

            //alert(tipoCaso);

            $("#spanTipoCaso").text(tipoCaso);
            ocultarDivCarga();
        }
        else {
            // SI (listaResultadoN1.length == 2)
            //$('#lMensajeError').text(listaResultadoN1[1]);
            //$("#aMensajeError").click();
            ocultarDivCarga();
        }
    }
    else { // Seccion de Negativa de Pension

        $("#imgCaminoMejorPension").addClass("panelOculto");
        $("#imgAhorrasSuficiente").addClass("panelOculto");
        $("#DVContenedorCalculadora").removeClass("panelVisible").addClass("panelOculto");

        if (parseInt($('#txtAnioAfiliacion').val()) < 1997) {
            tipoCaso = "GT";
            Anexo1.mesesParaRetiro = parseInt(listaResultadoN1[5]);
            Anexo1.rm = parseFloat(listaResultadoN1[9]).toFixed(12);
            Anexo1.cm = parseFloat(listaResultadoN1[10]).toFixed(12);

            $("#divResumenAll").addClass("panelOculto");
            $("#divP4").removeClass("panelOculto");
            $("#DVPieTexto").removeClass("panelOculto");
            $("#divP4Resumen").addClass("panelOculto");
            $("#GHInvertir1").addClass("panelOculto");
            $("#GHInvertir2").removeClass("panelOculto");
            $("#GHInvertir3").addClass("panelOculto");
            $("#trRegreso").addClass("panelOculto");
            $("#trInicio").removeClass("panelOculto");

            cfgGastoHormiga();

            // Se muestra mensaje de Generacion de Transicion
            //$('#btnError').trigger('click');
            //Es Trabajador Transición.
            //$("#DVResultadoAVNP").empty();
            //$("#DVResultadoAVNP").removeClass("panelVisible").addClass("panelOculto");
            //$("#DVContenedorCalculadora").removeClass("panelVisible").addClass("panelOculto");
            //$("#DVSaldoActualAforeRNP").removeClass("panelVisible").addClass("panelOculto");
            //$("#DVContenedorResultadoNP").removeClass("panelOculto").addClass("panelVisible");
            //$("#DVNotaDensidad").removeClass("panelVisible").addClass("panelOculto");
            //$("#DVDatosFormularioNP").removeClass("DVDatosFormularioNP").addClass("DVDatosFormularioNP_ODT5");
            //$("#DVResultadoPMT_2NP").removeClass("DVResultadoPMT_2NP_ODT").addClass("DVResultadoPMT_2NP_ODT");
            //$("#DVResultadoPMTNP").removeClass("panelVisible").addClass("panelOculto");
            //$("#DVDatosTePareceSuficienteNP").removeClass("panelVisible").addClass("panelOculto");
            //var listaResultadoN2 = listaResultadoN1[1].split('|');
            ////Construccion de Encabezado de tabla
            //var encabezadoTabla = "<table id='tblTablaResultadoNP_ODT5'><tr><th colspan='2'class='InformacionTabla'>Ahorro voluntario mensual</th><th colspan='3' class='InformacionTabla'>Ahorro voluntario<br/>acumulado</th></tr>";
            ////encabezadoTabla += "<tr><th class='RegistrostablaNP'>% del Salario</th><th class='RegistrostablaNP'>expresado en pesos</th><th class='RegistrostablaNP'>Por ahorro voluntario</th><th class='RegistrostablaNP'>Por ahorro obligatorio</th><th class='RegistrostablaNP'>Total ahorrado al momento del retiro<sup>1/</sup></th></tr>";
            //encabezadoTabla += "<tr><td colspan='5' class='RegistrostablaNP' style='padding-top: 0px; padding-bottom: 0px;'><img src='imagenes/encabezado_mejora_NP_ODT5.gif' ></td></tr>";
            //var cuerpoTabla = "";
            //var finTabla = "</table>";
            ////Se inicia en el 1 ya que lo anterior son los valores comunes del calculo
            //for (var i = 2; i < listaResultadoN1.length; i++) {
            //    var listaresultadocuadro = listaResultadoN1[i].split('|');
            //    cuerpoTabla += "<tr>";
            //    for (var ir = 0; ir < listaresultadocuadro.length; ir++) {
            //        switch (ir) {
            //            case 0:
            //                cuerpoTabla += "<td style='width:100px;'>" + listaresultadocuadro[ir] + "</td>";
            //                break;
            //            case 1:
            //                cuerpoTabla += "<td>$" + listaresultadocuadro[ir] + "</td>";
            //                break;
            //            case 2:
            //                cuerpoTabla += "<td style='width:280px;'>$" + listaresultadocuadro[ir] + "</td>";
            //                break;
            //        }
            //    }
            //    cuerpoTabla += "</tr>";
            //}
            $(document).ready(function () { //MC
                //$("#DVResultadoAVNP_ODT5").empty();
                //$("#DVResultadoAVNP_ODT5").append(encabezadoTabla + cuerpoTabla + finTabla);
                //$("#DVResultadoAVNP_ODT5").removeClass("panelOculto").addClass("panelVisible");
                DibujarGraficas();
            });//MC
        }
        else {
            tipoCaso = "NP";
            
            $("#DVResultadoAVNP_ODT5").empty();
            $("#DVResultadoAVNP_ODT5").removeClass("panelVisible").addClass("panelOculto");
            $("#DVContenedorCalculadora").removeClass("panelVisible").addClass("panelOculto");
            $("#DVContenedorResultadoNP").removeClass("panelOculto").addClass("panelVisible");
            $("#DVResultadoPMTNP").removeClass("panelOculto").addClass("panelVisible");
            $("#DVDatosTePareceSuficienteNP").removeClass("panelOculto").addClass("panelVisible");
            $("#DVSaldoActualAforeRNP").removeClass("panelOculto").addClass("panelVisible");
            $("#DVNotaDensidad").removeClass("panelVisible").addClass("panelOculto");
            $("#DVDatosFormularioNP").removeClass("DVDatosFormularioNP_ODT5").addClass("DVDatosFormularioNP");
            $("#DVResultadoPMT_2NP").removeClass("DVResultadoPMT_2NP_ODT").addClass("DVResultadoPMT_2NP");

            $("#divP2NP").removeClass("panelOculto").addClass("panelVisible");

            //alert("Meses para retiro en NP: " + listaResultadoN1[2]);
            //alert("URV en NP: " + listaResultadoN1[3]);
            
            $('#lblSaldoAcumuladoNP').text("");
            var listaResultadoN2 = listaResultadoN1[6].split('|');

            //Double.Parse(HttpContext.Current.Session["sdoAcumRetiro"].ToString())

            $('#lblSaldoAcumuladoNP').text("$ " + listaResultadoN2[3]);
            $("#lblP4Saldo").text("");
            $("#lblP4Saldo").text("$ " + listaResultadoN2[3]);

            //PGO  03032022
            $("#HdfSaldoFinalCompleto").text("");
            $("#HdfSaldoFinalCompleto").text("$ " + listaResultadoN1[11]);

            Anexo1.mesesParaRetiro = parseInt(listaResultadoN1[2]);
            Anexo1.URV = listaResultadoN1[3];
            Anexo1.rm = parseFloat(listaResultadoN1[4]).toFixed(12);
            Anexo1.cm = parseFloat(listaResultadoN1[5]).toFixed(12);

            //Construccion de Encabezado de tabla
            var encabezadoTabla = "<table id='tblTablaResultadoNP' class=\"tblTablaResultadoNP\" style=\"margin-left: auto; margin-right: auto;\">" +
                                "<tr style=\"\"><td class=\"fdotr\" style=\"width: 150px; color:white !important;\">% del Salario ahorrado  voluntariamente al mes</td>" +
                                    "<td style=\"width: 100px; color:white !important; background-color:#225c4f; \">expresado en pesos</td>" +
                                    "<td style=\"width: 150px; color:white !important; background-color:#225c4f;\">Subtotal por ahorro voluntario</td>" +
                                    "<td style=\"width: 150px; color:white !important; background-color:#225c4f;\">Subtotal por ahorro obligatorio</td>" +
                                    "<td class=\"fdotrIzq\" style=\"width: 150px; color:white !important;\">Total ahorrado al momento del retiro <sup>1/</sup></td></tr>";

            var cuerpoTabla = "";
            var listaresultadocuadroNP = null;
            var aux = null;

            //PGO 03032022 - Se agrega -1 para no mostrar el saldo final con decimales en la tabla
            for (var iNP = 6; iNP < listaResultadoN1.length-1; iNP++) {
                listaresultadocuadroNP = listaResultadoN1[iNP].split('|');
                cuerpoTabla += "<tr>";
                for (var irNP = 0; irNP < listaresultadocuadroNP.length; irNP++) {
                    aux = listaresultadocuadroNP[irNP];
                    switch (irNP) {
                        case 0:
                            cuerpoTabla += "<td style='font-size:16px; font-family: robotoregular; font-weight: bold; color: #5b5656;'>" + aux + "</td>";
                            break;
                        case 1:
                            cuerpoTabla += "<td style='font-size:16px; font-family: robotoregular; font-weight: bold; color: #5b5656;'>" + aux + "</td>";
                            break;
                        case 2:
                            cuerpoTabla += "<td style='font-size:16px; font-family: robotoregular; font-weight: bold; color: #5b5656;'>" + "$ " + aux + "</td>";
                            break;
                        case 3:
                            cuerpoTabla += "<td style='font-size:16px; font-family: robotoregular; font-weight: bold; color: #5b5656;'>" + "$ " + aux + "</td>";
                            break;
                        case 4:
                            cuerpoTabla += "<td style='font-size:16px; font-family: robotoregular; font-weight: bold; color: #5b5656;'>" + "$ " + aux + "</td>";
                            break;
                        default:
                            cuerpoTabla += "<td>" + aux + "</td>";
                            break;
                    }
                }
               
                cuerpoTabla += "</tr>";
            }

            var finTabla = "</table>";

            window.onload()
            {
                $("#DVResultadoAVNP").empty();
                $("#DVResultadoAVNP").append(encabezadoTabla + cuerpoTabla + finTabla);
                $("#DVResultadoAVNP").removeClass("panelOculto").addClass("panelVisible");
                DibujarGraficas();
            }

            $(document).ready(function () {
                $("#DVResultadoAVNP").empty();
                $("#DVResultadoAVNP").append(encabezadoTabla + cuerpoTabla + finTabla);
                $("#DVResultadoAVNP").removeClass("panelOculto").addClass("panelVisible");
                DibujarGraficas();
            });
        }

        //alert(tipoCaso);
        $("#spanTipoCaso").text(tipoCaso);
        ocultarDivCarga();
    }


    function Grafica(tabla1, tabla2, tabla3) {
        var listaGral2 = tabla2.split('|');
        var lista2 = [];
        var i = 1;
        var contMin = 1;
        for (i = 1; i < listaGral2.length; i++) {
            lista2[i] = listaGral2[i];
            if (parseFloat(listaGral2[i].replace('$', '')) < 0) {
                contMin = i + 1;
            }
        }

        var listaGral1 = tabla1.split('|');
        var lista1 = [];
        i = 1;
        for (i = 1; i < listaGral1.length; i++) {
            lista1[i] = listaGral1[i];
        }

        var listaGral3 = tabla3.split('|');
        var lista3 = [];
        i = 1;
        for (i = 1; i < listaGral3.length; i++) {
            lista3[i] = listaGral3[i];
        }

        var listaGral = tabla3.split('|');
        var lista = [];
        i = 1;
        for (i = 1; i < listaGral.length; i++) {
            lista[i] = listaGral[i];
        }
        

        var valMap = lista2;//[90, 110, 125, 140, 160, 225, 250];
        var minAuxSlide = 0;

        if (lista2[1] == ",") {
            minAuxSlide = lista2[2].replace('$', '').replace(",", "");
        } else {
            minAuxSlide = lista2[1].replace('$', '').replace(",", "");
        }

        $("#amount2").text(Math.round(minAuxSlide).formatMoney(0, "$", ",", "."));

        var valMap = lista1;//[90, 110, 125, 140, 160, 225, 250];

        var auxSlide = 0;
        $("#slider1").slider({
            range: "min",
            min: 1,
            max: valMap.length - 1,
            value: contMin,
            slide: function (event, ui) {
                if (parseFloat(lista2[ui.value].replace('$', '')) >= 0) {
                    $("#amount1").val(lista1[ui.value]);
                    $("#txtP3PensionConAhorro").text(lista1[ui.value]);
                    $("#txtPensionFlotante").val(lista1[ui.value]);
                    $("#amount2").text("$" + formatNumber(Math.round(parseFloat(lista2[ui.value].replace('$', '').replaceAll(",", ""))), 0).replace(".00", ""));
                    $("#amount3").val(lista3[ui.value]);

                    BarraProgreso();
                }
                else {
                    return false;
                }
            }
        });

        parse = function (data) {
            data = Math.round(data * Math.pow(10, 2)) / Math.pow(10, 2);
            if (data != null) {
                var lastone = data.toString().split('').pop();
                if (lastone != '.') {
                    data = parseFloat(data.toString().slice(0, 3));
                }
            }
            return data;
        };

        // --------------------------------------------
        var an1SB = (Anexo1.Tr / 100);
        an1SB = parseFloat(an1SB);
        an1SB = Math.floor(an1SB * 100) / 100;
        an1SB = parseFloat(an1SB) + parseFloat(.10);

        var v = (parse(an1SB)) * Anexo1.salarioBase;       
        v = Math.round(parseFloat(v));

        $("#txtP2PDIzq").text("$" + formatNumber(v, 0));
        $("#txtP2PD").text(parseInt(v));

        var mc = (parse(an1SB)) * Anexo1.salarioBase;
        $("#txtP2PDIzqNP").text("$" + formatNumber(mc, 0));
        $("#PESinRedondeo").text(parseFloat(mc));

        var an2SB = (Anexo1.Tr / 100);
        an2SB = parseFloat(an2SB);
        an2SB = Math.floor(an2SB * 100) / 100;
        an2SB = parseFloat(an2SB) + parseFloat(.20);
        
        v = (parse(an2SB)) * Anexo1.salarioBase;
        v = Math.round(parseFloat(v));

        $("#txtP2PDCen").text("$" + formatNumber(v, 0));

        var mc2 = (parse(an2SB)) * Anexo1.salarioBase;
        $("#txtP2PDCenNP").text(parseFloat(mc2));

        var an3SB = (Anexo1.Tr / 100);
        an3SB = parseFloat(an3SB);
        an3SB = Math.floor(an3SB * 100) / 100;
        an3SB = parseFloat(an3SB) + parseFloat(.30);

        v = (parse(an3SB)) * Anexo1.salarioBase;
        v = Math.round(parseFloat(v));

        $("#txtP2PDDer").text("$" + formatNumber(v, 0));

        var mc3 = (parse(an3SB)) * Anexo1.salarioBase;
        $("#txtP2PDDerNP").text(parseFloat(mc3));
        // -----------------------------------------------

        //PGO - Limpiamos variable "ValorSlide
        valorSlide = null;

        $("#divSliderN").slider({
           
            range: "min",
            min: 1,
            max: 3,
            value: contMin,
            slide: function (event, ui) {
                if (ui.value == 1) {
                    $("#PESinRedondeo").text(parseFloat($("#txtP2PDIzqNP").text().replace("$", "").replace(",", "")));
                }
                else if (ui.value == 2) {
                    $("#PESinRedondeo").text(parseFloat($("#txtP2PDCenNP").text().replace("$", "").replace(",", "")));
                }
                else {
                    $("#PESinRedondeo").text(parseFloat($("#txtP2PDDerNP").text().replace("$", "").replace(",", "")));
                }
                
                Grafica2();
                valorSlide = ui.value;
                G2Click(0);
            }
        });

        $("#amount1").val(valMap[contMin]);
        $("#txtP3PensionConAhorro").text(valMap[contMin]);
        $("#txtPensionFlotante").val(valMap[contMin]);

        var valMap = lista3;//[0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100];

        $("#amount3").val(valMap[contMin]);
        BarraProgreso();

        var valMap = lista;//[0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100];

        $("#amount4").val(valMap[contMin]);
    }

    function barraProgreso1() {
        var porcentg1 = parseInt($("#amount31").val());

        $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100");

        if (porcentg1 <= 10) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso10");
        }

        if (porcentg1 > 10 && porcentg1 <= 15) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso10");
        }

        else if (porcentg1 > 15 && porcentg1 <= 19) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso15");
        }

        else if (porcentg1 == 20) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso20");
        }

        else if (porcentg1 > 20 && porcentg1 <= 25) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso25");
        }


        else if (porcentg1 > 25 && porcentg1 <= 29) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso25");
        }


        else if (porcentg1 == 30) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso30");
        }

        else if (porcentg1 > 30 && porcentg1 <= 35) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso35");
        }

        else if (porcentg1 > 35 && porcentg1 <= 39) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso35");
        }

        else if (porcentg1 == 40) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso40");
        }

        else if (porcentg1 > 40 && porcentg1 <= 45) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso45");
        }

        else if (porcentg1 > 45 && porcentg1 <= 49) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso45");
        }

        else if (porcentg1 == 50) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso50");
        }

        else if (porcentg1 > 50 && porcentg1 <= 55) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso55");
        }

        else if (porcentg1 > 55 && porcentg1 <= 59) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso55");
        }

        else if (porcentg1 == 60) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso60");
        }

        else if (porcentg1 > 60 && porcentg1 <= 65) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso65");
        }

        else if (porcentg1 > 65 && porcentg1 <= 69) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso65");
        }

        else if (porcentg1 == 70) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso70");
        }

        else if (porcentg1 > 70 && porcentg1 <= 75) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso75");
        }

        else if (porcentg1 > 75 && porcentg1 <= 79) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso75");
        }

        else if (porcentg1 == 80) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso80");
        }


        else if (porcentg1 > 80 && porcentg1 <= 85) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso85");
        }

        else if (porcentg1 > 85 && porcentg1 <= 89) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso90 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso85");
        }

        else if (porcentg1 == 90) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso95 barraGraficaProgreso100").addClass("barraGraficaProgreso90");
        }

        else if (porcentg1 > 90 && porcentg1 <= 95) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso100").addClass("barraGraficaProgreso95");
        }

        else if (porcentg1 > 95 && porcentg1 <= 99) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95").addClass("barraGraficaProgreso95");
        }


        else if (porcentg1 == 100) {
            $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso15 barraGraficaProgreso20 barraGraficaProgreso25 barraGraficaProgreso30 barraGraficaProgreso35 barraGraficaProgreso40 barraGraficaProgreso45 barraGraficaProgreso50 barraGraficaProgreso55 barraGraficaProgreso60 barraGraficaProgreso65 barraGraficaProgreso70 barraGraficaProgreso75 barraGraficaProgreso80 barraGraficaProgreso85 barraGraficaProgreso90 barraGraficaProgreso95").addClass("barraGraficaProgreso100");
        }


        //else if (porcentg1 > 80 && porcentg1 <= 90) {
        //    $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso20 barraGraficaProgreso30 barraGraficaProgreso40 barraGraficaProgreso50 barraGraficaProgreso60 barraGraficaProgreso70 barraGraficaProgreso80 barraGraficaProgreso100").addClass("barraGraficaProgreso90");
        //} else if (porcentg1 > 90 && porcentg1 <= 100) {
        //    $("#barraGraficaProgreso1").removeClass("barraGraficaProgreso10 barraGraficaProgreso20 barraGraficaProgreso30 barraGraficaProgreso40 barraGraficaProgreso50 barraGraficaProgreso60 barraGraficaProgreso70 barraGraficaProgreso80 barraGraficaProgreso90").addClass("barraGraficaProgreso100");
        //}
    }

    function BarraProgreso() {
        var porcent = parseInt($("#amount3").val());
        switch (porcent) {
            case 10:
                $("#barraGraficaProgreso").removeClass("barraGraficaProgreso20-Termometro barraGraficaProgreso30-Termometro barraGraficaProgreso40-Termometro barraGraficaProgreso50-Termometro barraGraficaProgreso60-Termometro barraGraficaProgreso70-Termometro barraGraficaProgreso80-Termometro barraGraficaProgreso90-Termometro barraGraficaProgreso100-Termometro").addClass("barraGraficaProgreso10");
                $("#imgIndicadorTasa").removeClass(" indicadorBarra20 indicadorBarra30 indicadorBarra40 indicadorBarra50 indicadorBarra60 indicadorBarra70 indicadorBarra80 indicadorBarra90 indicadorBarra100").addClass("indicadorBarra10");
                break;
            case 20:
                $("#barraGraficaProgreso").removeClass("barraGraficaProgreso10 barraGraficaProgreso30-Termometro barraGraficaProgreso40-Termometro barraGraficaProgreso50-Termometro barraGraficaProgreso60-Termometro barraGraficaProgreso70-Termometro barraGraficaProgreso80-Termometro barraGraficaProgreso90-Termometro barraGraficaProgreso100-Termometro").addClass("barraGraficaProgreso20-Termometro");
                $("#imgIndicadorTasa").removeClass(" indicadorBarra10 indicadorBarra30 indicadorBarra40 indicadorBarra50 indicadorBarra60 indicadorBarra70 indicadorBarra80 indicadorBarra90 indicadorBarra100").addClass("indicadorBarra20");
                break;
            case 30:
                $("#barraGraficaProgreso").removeClass("barraGraficaProgreso40-Termometro barraGraficaProgreso50-Termometro barraGraficaProgreso60-Termometro barraGraficaProgreso70-Termometro barraGraficaProgreso80-Termometro barraGraficaProgreso90-Termometro barraGraficaProgreso100-Termometro").addClass("barraGraficaProgreso30-Termometro");
                $("#imgIndicadorTasa").removeClass("indicadorBarra40 indicadorBarra50 indicadorBarra60 indicadorBarra70 indicadorBarra80 indicadorBarra90 indicadorBarra100").addClass("indicadorBarra30");
                break;
            case 40:
                $("#barraGraficaProgreso").removeClass("barraGraficaProgreso30-Termometro barraGraficaProgreso50-Termometro barraGraficaProgreso60-Termometro barraGraficaProgreso70-Termometro barraGraficaProgreso80-Termometro barraGraficaProgreso90-Termometro barraGraficaProgreso100-Termometro").addClass("barraGraficaProgreso40-Termometro");
                $("#imgIndicadorTasa").removeClass("indicadorBarra30 indicadorBarra50 indicadorBarra60 indicadorBarra70 indicadorBarra80 indicadorBarra90 indicadorBarra100").addClass("indicadorBarra40");
                break;
            case 50:
                $("#barraGraficaProgreso").removeClass("barraGraficaProgreso30-Termometro barraGraficaProgreso40-Termometro barraGraficaProgreso60-Termometro barraGraficaProgreso70-Termometro barraGraficaProgreso80-Termometro barraGraficaProgreso90-Termometro barraGraficaProgreso100-Termometro").addClass("barraGraficaProgreso50-Termometro");
                $("#imgIndicadorTasa").removeClass("indicadorBarra30 indicadorBarra40 indicadorBarra60 indicadorBarra70 indicadorBarra80 indicadorBarra90 indicadorBarra100").addClass("indicadorBarra50");
                break;
            case 60:
                $("#barraGraficaProgreso").removeClass("barraGraficaProgreso30-Termometro barraGraficaProgreso40-Termometro barraGraficaProgreso50-Termometro barraGraficaProgreso70-Termometro barraGraficaProgreso80-Termometro barraGraficaProgreso90-Termometro barraGraficaProgreso100-Termometro").addClass("barraGraficaProgreso60-Termometro");
                $("#imgIndicadorTasa").removeClass("indicadorBarra30 indicadorBarra40 indicadorBarra50 indicadorBarra70 indicadorBarra80 indicadorBarra90 indicadorBarra100").addClass("indicadorBarra60");
                break;
            case 70:
                $("#barraGraficaProgreso").removeClass("barraGraficaProgreso30-Termometro barraGraficaProgreso40-Termometro barraGraficaProgreso50-Termometro barraGraficaProgreso60-Termometro barraGraficaProgreso80-Termometro barraGraficaProgreso90-Termometro barraGraficaProgreso100-Termometro").addClass("barraGraficaProgreso70-Termometro");
                $("#imgIndicadorTasa").removeClass("indicadorBarra30 indicadorBarra40 indicadorBarra50 indicadorBarra60 indicadorBarra80 indicadorBarra90 indicadorBarra100").addClass("indicadorBarra70");
                break;
            case 80:
                $("#barraGraficaProgreso").removeClass("barraGraficaProgreso30-Termometro barraGraficaProgreso40-Termometro barraGraficaProgreso50-Termometro barraGraficaProgreso60-Termometro barraGraficaProgreso70-Termometro barraGraficaProgreso90-Termometro barraGraficaProgreso100-Termometro").addClass("barraGraficaProgreso80-Termometro");
                $("#imgIndicadorTasa").removeClass("indicadorBarra30 indicadorBarra40 indicadorBarra50 indicadorBarra60 indicadorBarra70 indicadorBarra90 indicadorBarra100").addClass("indicadorBarra80");
                break;
            case 90:
                $("#barraGraficaProgreso").removeClass("barraGraficaProgreso30-Termometro barraGraficaProgreso40-Termometro barraGraficaProgreso50-Termometro barraGraficaProgreso60-Termometro barraGraficaProgreso70-Termometro barraGraficaProgreso80-Termometro barraGraficaProgreso100-Termometro").addClass("barraGraficaProgreso90-Termometro");
                $("#imgIndicadorTasa").removeClass("indicadorBarra30 indicadorBarra40 indicadorBarra50 indicadorBarra60 indicadorBarra70 indicadorBarra80 indicadorBarra100").addClass("indicadorBarra90");
                break;
            case 100:
                $("#barraGraficaProgreso").removeClass("barraGraficaProgreso30-Termometro barraGraficaProgreso40-Termometro barraGraficaProgreso50-Termometro barraGraficaProgreso60-Termometro barraGraficaProgreso70-Termometro barraGraficaProgreso80-Termometro barraGraficaProgreso90-Termometro").addClass("barraGraficaProgreso100-Termometro");
                $("#imgIndicadorTasa").removeClass("indicadorBarra30 indicadorBarra40 indicadorBarra50 indicadorBarra60 indicadorBarra70 indicadorBarra80 indicadorBarra90").addClass("indicadorBarra100");
                break;
        }
    }
    $('#cargando').hide();

    //enable_scroll();
    ocultarDivCarga();
}

var aDataGlobal1 = null;
var aDataGlobal2 = null;
var G1, G1NP, G2, Actual;

function IniciarVariablesGraficas() {
   aDataGlobal1 = null;
   aDataGlobal2 = null;
   Actual = -1;

   if (G1 != null) {
       G1.destroy();
       G1 = null;
   }

   if (G1NP != null) {
       G1NP.destroy();
       G1NP = null;
   }

   if (G2 != null) {
       G2.destroy();
       G2 = null;
   }
}
function DibujarGraficas() {
    Grafica1();
}
function ocultarDivCarga() {
    document.getElementById('loadingDiv').style.display = 'none';
}
function mostrarDivCarga() {
    //$('#cargando_test').hide();
    document.getElementById('loadingDiv').style.display = 'block';
}

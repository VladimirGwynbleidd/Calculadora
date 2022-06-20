using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.Script.Services;
using System.Net;
using System.Net.NetworkInformation;
using System.Globalization;

namespace CALCULADORA2014
{
    public partial class Index : System.Web.UI.Page
    {
        public static double SMGVDF
        {
            get;
            set;
        }

        public static double SMGVZB
        {
            get;
            set;
        }

        public static double LInferior
        {
            get;
            set;
        }

        public static double LSuperior
        {
            get;
            set;
        }

        public static double Val_UMA
        {
            get;
            set;
        }


        public double Cigarros { get; set; }
        public double Cerveza { get; set; }
        public double FastFood { get; set; }
        public double Cafe { get; set; }
        public double Cine { get; set; }


        public String gsIdSesion
        {
            get
            {
                object o = ViewState["gsIdSesion"];
                return (o == null) ? String.Empty : (string)o;
            }

            set
            {
                ViewState["gsIdSesion"] = value;
            }
        }

        public String gsIp
        {
            get
            {
                object o = ViewState["gsIp"];
                return (o == null) ? String.Empty : (string)o;
            }

            set
            {
                ViewState["gsIp"] = value;
            }
        }


        public static String Fechcambio1 
        {
            get;
            set;
        }

        public static String Fechcambio2
        {
            get;
            set;
        }

        public static double AOIMensual
        {
            get;
            set;
        }


        


        public static double SdoFinalOrig { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //ContenidoCalc.Visible = false;
                if (!IsPostBack)
                {

                    List<CLASESGenerales.Parametros> parametros = getParametros();

                    const int IM01SMGVDF = 7;
                    const int IM01SMGVZB = 8;
                    const int IM01SMLINF = 9;
                    const int IM01SMLSUP = 10;
                    const int IM01VALUMA = 40;
                    const int Fcambio1 = 41;
                    const int Fcambio2 = 42;
                    const int ApObligatiria = 43;
                    foreach (var parametro in parametros)
                    {
                        switch (parametro.Id)
                        {
                            case IM01SMGVDF:
                                SMGVDF = parametro.Valor;
                                break;
                            case IM01SMGVZB:
                                SMGVZB = parametro.Valor;
                                break;
                            case IM01SMLINF:
                                LInferior = parametro.Valor;
                                break;
                            case IM01SMLSUP:
                                LSuperior = parametro.Valor;
                                break;
                            case 35:
                                Cigarros = parametro.Valor;
                                break;
                            case 36:
                                Cerveza = parametro.Valor;
                                break;
                            case 37:
                                FastFood = parametro.Valor;
                                break;
                            case 38:
                                Cafe = parametro.Valor;
                                break;
                            case 39:
                                Cine = parametro.Valor;
                                break;
                            case IM01VALUMA:
                                Val_UMA = parametro.Valor;
                                break;
                            case Fcambio1:
                                Fechcambio1 = parametro.ValorFecha;
                                hdfFechacambio1.Value = Fechcambio1;
                               
                                break;

                            case Fcambio2:
                                Fechcambio2 = parametro.ValorFecha;
                                hdfFechacambio2.Value = Fechcambio2;
                                break;
                                

                            case ApObligatiria:
                                AOIMensual = parametro.Valor;
                                HdfAOI.Value = Convert.ToString(AOIMensual);
                                break;
                            default:
                                break;
                        }

                    }

                  //  GetFechasCambio(Fechcambio1,Fechcambio2);

                    cboAfore.DataSource = CargaAfores();
                    cboAfore.DataValueField = "N_CVE_AFORE";
                    cboAfore.DataTextField = "T_DSC_AFORE";
                    cboAfore.DataBind();
                    ListItem lstInicio = new ListItem("", "");
                    cboAfore.Items.Insert(0, lstInicio);
                    cboAfore.SelectedIndex = 0;


                    List<CLASESGenerales.CLASETipoTrabajador> lstTipoTrabajador = new List<CLASESGenerales.CLASETipoTrabajador>();
                    CLASESGenerales.CLASETipoTrabajador tipoTrabajador = new CLASESGenerales.CLASETipoTrabajador();
                    tipoTrabajador.N_CVE_T_TRABAJADOR_ = 0;
                    tipoTrabajador.T_DSC_T_TRABAJADOR_ = "INDEPENDIENTE";
                    lstTipoTrabajador.Add(tipoTrabajador);
                    tipoTrabajador = new CLASESGenerales.CLASETipoTrabajador();

                    tipoTrabajador.N_CVE_T_TRABAJADOR_ = 1;
                    tipoTrabajador.T_DSC_T_TRABAJADOR_ = "IMSS";
                    lstTipoTrabajador.Add(tipoTrabajador);

                    //cboTipoTrabajador.DataSource = lstTipoTrabajador;
                    //cboTipoTrabajador.DataValueField = "N_CVE_T_TRABAJADOR_";
                    //cboTipoTrabajador.DataTextField = "T_DSC_T_TRABAJADOR_";
                    //cboTipoTrabajador.DataBind();
                    //ListItem lstInicio1 = new ListItem("", "");
                    //cboTipoTrabajador.Items.Insert(0, lstInicio1);
                    //cboTipoTrabajador.SelectedIndex = 0;

                    //Double pN_PORC_ANUAL_SALDO = Convert.ToDouble(ComisionAfore(cboAfore.SelectedItem.Value).Rows[0][2]);
                    //this.txtComisionAfore.Value = pN_PORC_ANUAL_SALDO.ToString("P2");

                    this.txtSalarioBaseCotizacion.Attributes.Add("onkeypress", "javascript:return ValidNum(event);");
                    this.txtSaldoActualAfore.Attributes.Add("onkeypress", "javascript:return ValidNum(event);");
                    //this.txtFechaNacimiento.Attributes.Add("onkeypress", "javascript:return ValidNum(event);");

                    //Guardarmos la sesion la primera vz que entro el usuario
                    if (gsIdSesion == String.Empty)
                    {
                        BDS_BITACORA_VISITAS objBitacora = new BDS_BITACORA_VISITAS(BDS_BITACORA_VISITAS.IMSS);
                        Int64 liRes = 0;
                        gsIdSesion = Session.SessionID;
                        //gsIp = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily.ToString().ToUpper().Equals("INTERNETWORK")).FirstOrDefault().ToString();

                        gsIp = Request.UserHostAddress;

                        if (gsIp == "" || gsIp.Length < 6)
                        {
                            if (Request.ServerVariables["REMOTE_ADDR"] != null)
                                gsIp = Request.ServerVariables["REMOTE_ADDR"].ToString();
                            if (Request.ServerVariables["HTTP_CLIENT-IP"] != null && (gsIp == "" || gsIp.Length < 6))
                                gsIp = Request.ServerVariables["HTTP_CLIENT-IP"].ToString();
                        }

                        objBitacora.ID_SESION = gsIdSesion;
                        objBitacora.IP = gsIp;
                        liRes = objBitacora.InsertarVisita();
                        liRes = objBitacora.ObtieneTotalDeVisitas();

                        lbContVisitas.Text = liRes.ToString("N0");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message.ToString() + "');</script>");
            }
        }

        //static String sCS = @"Data Source=172.16.10.23\dwwebconsar;Initial Catalog=BD_SQL_CALCULADORA_AV;User ID=calculadora_av;Password=c41cu14d0r4_4v;";
        static String sCS = RNCalculadora.ConnectionString;


        public DataTable CargaAfores()
        {
            DataTable dtAfores = new DataTable();
            SqlConnection cn = new SqlConnection(sCS);
            SqlCommand cmd = new SqlCommand("SELECT N_CVE_AFORE, T_DSC_AFORE FROM dbo.BDE_C_COMISION_SIEFORES WHERE VIG_FLAG = 1", cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            da.Fill(dtAfores);
            return dtAfores;
        }

        //public DataTable CargaAfores()
        //{
        //    DataTable dtAfores = new DataTable();
        //    SqlConnection cn = new SqlConnection(sCS);
        //    SqlCommand cmd = new SqlCommand("SELECT N_CVE_AFORE, T_DSC_AFORE FROM dbo.BDE_C_COMISION_SIEFORES WHERE VIG_FLAG = 1", cn);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    da.SelectCommand = cmd;
        //    da.Fill(dtAfores);
        //    return dtAfores;
        //}



        [WebMethod]
        public static String GetComisionesAfore(String N_CVE_AFORE)
        {
            String sComision = String.Empty;

            try
            {
                DataTable dtAfores = new DataTable();
                SqlConnection cn = new SqlConnection(sCS);
                SqlCommand cmd = new SqlCommand("SELECT N_CVE_AFORE FROM dbo.BDE_C_COMISION_SIEFORES WHERE VIG_FLAG = 1 ORDER BY (N_ID_ORDER_RANKING) ASC", cn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand = cmd;
                da.Fill(dtAfores);
                for (int i = 0; i < dtAfores.Rows.Count; i++)
                {
                    sComision += "|" + dtAfores.Rows[i][0];
                }
            }
            catch (Exception ex) { }
            return sComision;
        }

        //PGO - ODT- 07 - WebMethod para obtener valores de fechas de cambio y AOI
        
      

        [WebMethod]
        public static String GetComisionAfore(String N_CVE_AFORE)
        {
            String sComision = String.Empty;
            String PorcentajeComisionAux = String.Empty;

            try
            {
                DataTable dtAfores = new DataTable();
                SqlConnection cn = new SqlConnection(sCS);
                SqlCommand cmd = new SqlCommand("SELECT N_CVE_AFORE, T_DSC_AFORE, N_PORC_ANUAL_SALDO FROM dbo.BDE_C_COMISION_SIEFORES WHERE VIG_FLAG = 1 AND N_CVE_AFORE =" + N_CVE_AFORE, cn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand = cmd;
                da.Fill(dtAfores);
                //sComision = Convert.ToDecimal(dtAfores.Rows[0][2]).ToString("P");
                PorcentajeComisionAux = Convert.ToDecimal(dtAfores.Rows[0][2]).ToString();
                sComision = Decimal.Parse(PorcentajeComisionAux).ToString(("0.####") + "%");
            }
            catch (Exception ex) { }
            return sComision;
        }

        [WebMethod]
        public static String GetMensajesCalculo(String sParametros_Calculo)
        {
            String sResultados_Calculo = String.Empty;
            String MsgBox = String.Empty;
            int pNegativaPension = 0;
            try
            {
                string[] aParametros = sParametros_Calculo.Split('|');
                if (!aParametros[1].Trim().Equals(String.Empty) && !aParametros[2].Trim().Equals(String.Empty) && !aParametros[3].Trim().Equals(String.Empty) && !aParametros[4].Trim().Equals(String.Empty) && !aParametros[5].Trim().Equals(String.Empty)
                            && !aParametros[6].Trim().Equals(String.Empty) && !aParametros[7].Trim().Equals(String.Empty) && !aParametros[8].Trim().Equals(String.Empty) && !aParametros[9].Trim().Equals(String.Empty) && !aParametros[10].Trim().Equals(String.Empty))
                {
                    Double pSalarioBaseCotizacionMensual = Convert.ToDouble(aParametros[6]);
                    DateTime pFechaNacimiento = Convert.ToDateTime(aParametros[0]);
                    Double pEdadRetiro = Convert.ToDouble(aParametros[1]);
                    Double pSaldoActualAfore = Convert.ToDouble(aParametros[3]);
                    Double nSemanasCotizadas = Convert.ToDouble(aParametros[2]);
                    Double pDensidadCotizacion = Convert.ToDouble(aParametros[4]);

                    //Monto en pesos de la aportación obligatoria inicial mensual
                    Double aObligatoriaIniMensual = Convert.ToDouble(aParametros[13]);
                    Double tRendimientoMensual = Convert.ToDouble(aParametros[14]);

                    //Número de meses entre la fecha FC1 y la fecha de cálculo
                    Int32 n1 = Convert.ToInt32(aParametros[15]);
                    //Numero de meses que falta para que el trabajador cumpla la edad de retiro
                    Int32 n = Convert.ToInt32(aParametros[16]);
                    //Primera Fecha de Cambio
                    DateTime fCambio1 = Convert.ToDateTime(aParametros[17]);
                    //Segunda Fecha de cambio
                    DateTime fCambio2 = Convert.ToDateTime(aParametros[18]);

                    //PGO - ODT-07 Numero de meses entre FC1 y FC2.
                    Int32 n2 = Convert.ToInt32(aParametros[19]);

                    // PGO - ODT-07 - Número de años en los que se dan los incrementos en la  aportación gradual (N2)
                    //Int32 N2_icrementosAP = Convert.ToInt32(aParametros[20]);

                    // PGO - ODT-07 - Número de periodos que se acumularía la aportación despues del último incremento en la aportación gradual
                    Int32 n2p = Convert.ToInt32(aParametros[20]);

                    // PGO - ODT-07 - Tasa de rendimiento anual
                    Double tRendimientoAnual = Convert.ToDouble(aParametros[21]);

                    //PGO - ODT-07 Numero de meses entre FR y FC2.
                    Int32 n3 = Convert.ToInt32(aParametros[22]);

                    RNCalculadora oRNCalcularora = new RNCalculadora();

                    //PGO Obtiene parametros salarios
                    List<CLASESGenerales.Parametros> parametros = new List<CLASESGenerales.Parametros>();
                    parametros = getParametros();

                    //PGO Establece parametros
                    oRNCalcularora.Parametros = parametros;

                    if (oRNCalcularora.Calcular(pFechaNacimiento, pEdadRetiro, nSemanasCotizadas, pSaldoActualAfore, pDensidadCotizacion, pSalarioBaseCotizacionMensual, out MsgBox, out pNegativaPension, n))
                    {
                        //No hay errores
                        sResultados_Calculo = "&&&&";
                    }
                    else
                    {
                        //Hay errores
                        sResultados_Calculo = "&" + MsgBox;
                    }
                }
            }
            catch (Exception ex) { return "&" + ex.Message.ToString(); }
            return sResultados_Calculo;
        }


        [WebMethod]
        public static String GetFechasCambio(String Fechcambio1)
        {

            return Fechcambio1;
        }



        [WebMethod]
        public static String GetCalculo(String sParametros_Calculo)
        {
            String sResultados_Calculo = String.Empty;
            String SDatosGrafica = String.Empty;
            String MsgBox = String.Empty;
            int pNegativaPension = 0;

            //PGO 03032022
            double saldoFinalCompleto = 0;
            try
            {

                string[] aParametros = sParametros_Calculo.Split('|');
                if (!aParametros[1].Trim().Equals(String.Empty) && !aParametros[2].Trim().Equals(String.Empty) && !aParametros[3].Trim().Equals(String.Empty) && !aParametros[4].Trim().Equals(String.Empty) && !aParametros[5].Trim().Equals(String.Empty)
                    && !aParametros[6].Trim().Equals(String.Empty) && !aParametros[7].Trim().Equals(String.Empty) && !aParametros[8].Trim().Equals(String.Empty) && !aParametros[9].Trim().Equals(String.Empty) && !aParametros[10].Trim().Equals(String.Empty))
                {
                    Util oUtil = new Util();
                    Double pN_PORC_ANUAL_SALDO;
                    Double pSalarioBaseCotizacionMensual = Convert.ToDouble(aParametros[6]);
                    DateTime pFechaNacimiento = Convert.ToDateTime(aParametros[0]);
                    Double pEdad = Convert.ToDouble(aParametros[10]);
                    Double pEdadRetiro = Convert.ToDouble(aParametros[1]);
                    Double pSaldoActualAfore = Convert.ToDouble(aParametros[3]);
                    Double nSemanasCotizadas = Convert.ToDouble(aParametros[2]);
                    Double pDensidadCotizacion = Convert.ToDouble(aParametros[4]);
                    Double pGenero = Convert.ToDouble(aParametros[5]);
                    Double pRendimiento = Convert.ToDouble(aParametros[7]);
                    Double pComisionAfore = Convert.ToDouble(aParametros[8]);
                    Double pAportacion = Convert.ToDouble(aParametros[9]);
                    Double anioAfiliacion = Convert.ToDouble(aParametros[11]);

                    //Se guarda la información de los datos de captura(Salario Base de Cotización mensual, Género, Edad, Año de afiliación) para ODT 09
                    BDS_BITACORA_VISITAS objBitacora = new BDS_BITACORA_VISITAS(BDS_BITACORA_VISITAS.IMSS);
                    int nRespuesta = objBitacora.InsertarDatosFormulario(pSalarioBaseCotizacionMensual, pGenero, Convert.ToInt16(pEdad), Convert.ToInt16(anioAfiliacion));

                    //Monto en pesos de la aportación obligatoria inicial mensual
                    Double aObligatoriaIniMensual = Convert.ToDouble(aParametros[13]);
                    Double tRendimientoMensual = Convert.ToDouble(aParametros[14]);
                    //Número de meses entre la fecha FC1 y la fecha de cálculo
                    Int32 n1 = Convert.ToInt32(aParametros[15]);
                    //Numero de meses que falta para que el trabajador cumpla la edad de retiro
                    Int32 n = Convert.ToInt32(aParametros[16]);
                    //Primera Fecha de Cambio
                    DateTime fCambio1 = Convert.ToDateTime(aParametros[17]);
                    //Segunda Fecha de cambio
                    DateTime fCambio2 = Convert.ToDateTime(aParametros[18]);

                    //PGO - ODT-07 Numero de meses entre FC1 y FC2.
                    Int32 n2 = Convert.ToInt32(aParametros[19]);

                    // PGO - ODT-07 - Número de años en los que se dan los incrementos en la  aportación gradual (N2)
                    //Int32 N2_icrementosAP = Convert.ToInt32(aParametros[20]);

                    // PGO - ODT-07 - Número de periodos que se acumularía la aportación despues del último incremento en la aportación gradual
                    Int32 n2p = Convert.ToInt32(aParametros[20]);

                    // PGO - ODT-07 - Tasa de rendimiento anual
                    Double tRendimientoAnual = Convert.ToDouble(aParametros[21]);

                    //PGO - ODT-07 Numero de meses entre FR y FC2.
                    Int32 n3 = Convert.ToInt32(aParametros[22]);


                    DateTime pFechaCorte = DateTime.Now.Date;
                    //Double PMG = 2601;
                    //PGO PGM Pensión Minima garantizada
                    Double PMG = 0;
                    Double PME = 0;
                    Double strTasaReemplazo = 0;
                    Double auxTotalAhorro = 0;

                    String valMap1 = String.Empty;
                    String valMap2 = String.Empty;
                    String valMap3 = String.Empty;
                    String valMap4 = String.Empty;
                    String valMap5 = String.Empty;
                    String strResultadoPME = String.Empty;
                    String strTasaReemplazoCalc = String.Empty;

                    RNCalculadora oRNCalcularora = new RNCalculadora();
                    Boolean bAlcanzaPMG = false;
                    Double Tr = 100, MesesParaRetiro = 0, URVe = 0;

                    CLASESGenerales.CLASEAhorroVoluntario oCLASEAV = null;
                    CLASESGenerales.CLASEPensionMensualTasa OCLASEMT = null;

                    List<CLASESGenerales.CLASEAhorroVoluntario> lCLASEAhorroVoluntario = new List<CLASESGenerales.CLASEAhorroVoluntario>();
                    List<CLASESGenerales.CLASEPensionMensualTasa> lCLASEPensionMensualTasa = new List<CLASESGenerales.CLASEPensionMensualTasa>();

                    //PGO Obtiene parametros salarios
                    List<CLASESGenerales.Parametros> parametros = new List<CLASESGenerales.Parametros>();
                    parametros = getParametros();

                    //PGO Establece parametros
                    oRNCalcularora.Parametros = parametros;

                    //PGO Determina tipo de consulta (IMSS)
                    oRNCalcularora.Tipo = RNCalculadora.TipoConsulta.IMSS;

                    //PGO Se asigna el valor obtenido a la variable PMG (Pensión Mínima Garantizada)
                   // PMG = oRNCalcularora.PMGBD;

                    PME = oRNCalcularora.Modelo_mensual_iterativo(pSaldoActualAfore, 0, pGenero, pSalarioBaseCotizacionMensual, pRendimiento, pComisionAfore, pFechaNacimiento, 0, 0, pFechaCorte, pAportacion, 0, 0, pDensidadCotizacion, pEdadRetiro, 2, aObligatoriaIniMensual, tRendimientoMensual, n1, n, fCambio1, fCambio2, n2, n2p, tRendimientoAnual, n3, pEdad, nSemanasCotizadas);
                    PMG = oRNCalcularora.PensionGarantizada;


                    if (oRNCalcularora.Calcular(pFechaNacimiento, pEdadRetiro, nSemanasCotizadas, pSaldoActualAfore, pDensidadCotizacion, pSalarioBaseCotizacionMensual, out MsgBox, out pNegativaPension, n))
                    {
                        MesesParaRetiro = oRNCalcularora.MesesParaRetiro(pFechaNacimiento, (int)pEdadRetiro, pFechaCorte);
                        URVe = oRNCalcularora.URVe(pGenero, pEdadRetiro);

                        if (anioAfiliacion < 1997)
                        {
                            PME = oRNCalcularora.Modelo_mensual_iterativo(pSaldoActualAfore, 0, pGenero, pSalarioBaseCotizacionMensual, pRendimiento, pComisionAfore, pFechaNacimiento, 0, 0, pFechaCorte, pAportacion, 0, 0, pDensidadCotizacion, pEdadRetiro, 2, aObligatoriaIniMensual, tRendimientoMensual, n1, n, fCambio1, fCambio2, n2, n2p, tRendimientoAnual, n3, pEdad, nSemanasCotizadas);
                            if (PME > PMG)
                            {
                                strResultadoPME = "&1";
                                strTasaReemplazo = oRNCalcularora.TasaReemplazo * 100;
                            }
                            else
                            {
                                strResultadoPME = "&0";
                                strTasaReemplazo = (PMG / pSalarioBaseCotizacionMensual) * 100;
                            }
                            #region PT
                            //El primer campo sera para validar que es Personal De Transición                                  
                            sResultados_Calculo = "PT&" + PME.ToString("c0") + "&" + strTasaReemplazo + "&";

                            double saldoAcumulado = 0;

                            OCLASEMT = new CLASESGenerales.CLASEPensionMensualTasa();
                            OCLASEMT.Texto_ = "Saldo acumulado";

                            saldoAcumulado = oRNCalcularora.SaldoFinal; //getSdoFinal(sParametros_Calculo);

                            OCLASEMT.Numero_ = saldoAcumulado.ToString("c0");
                            lCLASEPensionMensualTasa.Add(OCLASEMT);
                            sResultados_Calculo += OCLASEMT.Numero_;
                            sResultados_Calculo += strResultadoPME;
                            sResultados_Calculo += "&" + MesesParaRetiro.ToString() + "&0&" +
                                                    oRNCalcularora.CuotaSocialMensual.ToString() + "&" +
                                                    oRNCalcularora.AportacionMensual.ToString() + "&" +
                                                    oRNCalcularora.Rendimiento_mensual(pRendimiento) + "&" +
                                                    oRNCalcularora.Comision_mensual(pComisionAfore);
                            RNCalculadora.CapturaCalculadora objCapturCalculadora = new RNCalculadora.CapturaCalculadora();

                            objCapturCalculadora.Rendimiento = pRendimiento;
                            objCapturCalculadora.Comision = pComisionAfore;
                            objCapturCalculadora.SalarioBase = pSalarioBaseCotizacionMensual;
                            objCapturCalculadora.FechaNacimiento = pFechaNacimiento;
                            objCapturCalculadora.EdadRetiro = (int)pEdadRetiro;
                            objCapturCalculadora.Edad = (int)pEdad;
                            objCapturCalculadora.SaldoAcumulado = saldoAcumulado;
                            objCapturCalculadora.TasaRendimientoMensual = tRendimientoMensual;


                            objCapturCalculadora.DensidadCotizacion = pDensidadCotizacion;

                            List<RNCalculadora.NegativaPension> lstobjCalculoNegativaPension = RNCalculadora.CalculaSaldoAcumuladoNP(objCapturCalculadora,n);
                            int icont = 0;

                            foreach (RNCalculadora.NegativaPension objNegativaPension in lstobjCalculoNegativaPension)
                            {
                                string valMapTMP = string.Empty;
                                valMapTMP += ((float)objNegativaPension.PorcentajeSalario / 100.0).ToString("p0") + "|";
                                valMapTMP += Math.Round((decimal)objNegativaPension.PorcentajeSalarioPesos).ToString("N0") + "|";
                                valMapTMP += Math.Round((decimal)objNegativaPension.SaldoAhorroVoluntario).ToString("N0") + "|";
                                valMapTMP += Math.Round((decimal)objNegativaPension.SaldoAhorroObligatorio).ToString("N0") + "|";
                                valMapTMP += Math.Round((decimal)objNegativaPension.SaldoAhorroTotal).ToString("N0");

                                icont++;

                                switch (icont)
                                {
                                    case 1:
                                        valMap1 = valMapTMP;
                                        break;
                                    case 2:
                                        valMap2 = valMapTMP;
                                        break;
                                    case 3:
                                        valMap3 = valMapTMP;
                                        break;
                                    case 4:
                                        valMap4 = valMapTMP;
                                        break;
                                    case 5:
                                        valMap5 = valMapTMP;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            #endregion
                        }
                        else if (pNegativaPension == 0)
                        { //NO es negativa de pensión
                            PME = oRNCalcularora.Modelo_mensual_iterativo(pSaldoActualAfore, 0, pGenero, pSalarioBaseCotizacionMensual, pRendimiento, pComisionAfore, pFechaNacimiento, 0, 0, pFechaCorte, pAportacion, 0, 0, pDensidadCotizacion, pEdadRetiro, 2, aObligatoriaIniMensual, tRendimientoMensual, n1, n, fCambio1, fCambio2, n2, n2p, tRendimientoAnual, n3, pEdad, nSemanasCotizadas);
                            if (PME > PMG)
                            {
                                strResultadoPME = "&1";
                                strTasaReemplazo = oRNCalcularora.TasaReemplazo * 100;
                            }
                            else
                            {
                                strResultadoPME = "&0";
                                strTasaReemplazo = (PMG / pSalarioBaseCotizacionMensual) * 100;
                            }
                            if (strTasaReemplazo >= 100) //
                            {
                                #region TRM
                                //El primer campo sera para validar que es Personal De Transición
                                sResultados_Calculo = "TRM&" + PME.ToString("c0") + "&" + strTasaReemplazo + "&";

                                double saldoAcumulado = 0;

                                OCLASEMT = new CLASESGenerales.CLASEPensionMensualTasa();
                                OCLASEMT.Texto_ = "Saldo acumulado";

                                saldoAcumulado = oRNCalcularora.SaldoFinal; //getSdoFinal(sParametros_Calculo);

                                OCLASEMT.Numero_ = saldoAcumulado.ToString("c0");
                                lCLASEPensionMensualTasa.Add(OCLASEMT);
                                sResultados_Calculo += OCLASEMT.Numero_;
                                sResultados_Calculo += strResultadoPME;
                                sResultados_Calculo += "&" + MesesParaRetiro.ToString();
                                sResultados_Calculo += "&" + URVe.ToString() +
                                                       "&" + oRNCalcularora.CuotaSocialMensual.ToString() +
                                                       "&" + oRNCalcularora.AportacionMensual.ToString() +
                                                       "&" + oRNCalcularora.PensionGarantizada.ToString();
                                RNCalculadora.CapturaCalculadora objCapturCalculadora = new RNCalculadora.CapturaCalculadora();

                                objCapturCalculadora.Rendimiento = pRendimiento;
                                objCapturCalculadora.Comision = pComisionAfore;
                                objCapturCalculadora.SalarioBase = pSalarioBaseCotizacionMensual;
                                objCapturCalculadora.FechaNacimiento = pFechaNacimiento;
                                objCapturCalculadora.EdadRetiro = (int)pEdadRetiro;
                                objCapturCalculadora.Edad = (int)pEdad;
                                objCapturCalculadora.SaldoAcumulado = saldoAcumulado;
                                objCapturCalculadora.DensidadCotizacion = pDensidadCotizacion;

                                List<RNCalculadora.NegativaPension> lstobjCalculoNegativaPension = RNCalculadora.CalculaSaldoAcumuladoNP(objCapturCalculadora,n);
                                int icont = 0;

                                foreach (RNCalculadora.NegativaPension objNegativaPension in lstobjCalculoNegativaPension)
                                {
                                    string valMapTMP = string.Empty;
                                    valMapTMP += ((float)objNegativaPension.PorcentajeSalario / 100.0).ToString("p0") + "|";
                                    valMapTMP += Math.Round((decimal)objNegativaPension.PorcentajeSalarioPesos).ToString("N0") + "|";
                                    valMapTMP += Math.Round((double)objNegativaPension.SaldoAhorroVoluntario).ToString("N0") + "|";
                                    valMapTMP += Math.Round((double)objNegativaPension.SaldoAhorroObligatorio).ToString("N0") + "|";
                                    valMapTMP += Math.Round((double)objNegativaPension.SaldoAhorroTotal).ToString("N0");

                                    icont++;

                                    switch (icont)
                                    {
                                        case 1:
                                            valMap1 = valMapTMP;
                                            break;
                                        case 2:
                                            valMap2 = valMapTMP;
                                            break;
                                        case 3:
                                            valMap3 = valMapTMP;
                                            break;
                                        case 4:
                                            valMap4 = valMapTMP;
                                            break;
                                        case 5:
                                            valMap5 = valMapTMP;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region Resto
                                OCLASEMT = new CLASESGenerales.CLASEPensionMensualTasa();
                                OCLASEMT.Texto_ = "Saldo acumulado";
                                OCLASEMT.Numero_ = oRNCalcularora.SaldoFinal.ToString("$ #,##0");
                                lCLASEPensionMensualTasa.Add(OCLASEMT);
                                sResultados_Calculo += OCLASEMT.Numero_;

                                OCLASEMT = new CLASESGenerales.CLASEPensionMensualTasa();
                                OCLASEMT.Texto_ = "Pensión mensual estimada";
                                PME = oRNCalcularora.MontoPME;
                                OCLASEMT.Numero_ = PME.ToString("c0");
                                lCLASEPensionMensualTasa.Add(OCLASEMT);
                                sResultados_Calculo += "|" + OCLASEMT.Numero_;

                                OCLASEMT = new CLASESGenerales.CLASEPensionMensualTasa();
                                OCLASEMT.Texto_ = "Tasa de reemplazo (porcentaje de la Pensión mensual estimada con respecto al Salario Base de Cotización mensual)";
                                OCLASEMT.Numero_ = oRNCalcularora.TasaReemplazo.ToString("##0.0 %");
                                lCLASEPensionMensualTasa.Add(OCLASEMT);
                                sResultados_Calculo += "|" + OCLASEMT.Numero_;
                                sResultados_Calculo += "|" + MesesParaRetiro.ToString() +
                                                       "|" + URVe.ToString() +
                                                       "|" + oRNCalcularora.CuotaSocialMensual.ToString() +
                                                       "|" + oRNCalcularora.AportacionMensual.ToString() +
                                                       "|" + oRNCalcularora.PensionGarantizada.ToString();
                                sResultados_Calculo += strResultadoPME + "&";
                                for (Int32 x = 0; x < 10; x++)
                                {
                                    auxTotalAhorro = pSalarioBaseCotizacionMensual * (Tr / 100);
                                    if (auxTotalAhorro < PMG)
                                    {
                                        break;
                                    }

                                    oCLASEAV = new CLASESGenerales.CLASEAhorroVoluntario();
                                    oCLASEAV.PensionMensual_ = (pSalarioBaseCotizacionMensual * (Tr / 100)).ToString("c0");
                                    oCLASEAV.TasaReemplazo_ = (Tr / 100).ToString("p0").Replace(" ", "");
                                    oCLASEAV.AhorroMensual_ = oRNCalcularora.Av_mensual_IMSS(pSaldoActualAfore, 0, pGenero, pSalarioBaseCotizacionMensual, pRendimiento, pComisionAfore, pFechaNacimiento, 0, 0, pFechaCorte, pAportacion, pDensidadCotizacion, pEdadRetiro, (Tr / 100), n).ToString("c");

                                    if (!oCLASEAV.AhorroMensual_.ToString().Contains("-"))
                                    {
                                        lCLASEAhorroVoluntario.Add(oCLASEAV);
                                    }

                                    sResultados_Calculo += "|" + (pSalarioBaseCotizacionMensual * (Tr / 100)).ToString("c0") + "*";
                                    sResultados_Calculo += (Tr / 100).ToString("p0").Replace(" ", "") + "*";
                                    sResultados_Calculo += oRNCalcularora.Av_mensual_IMSS(pSaldoActualAfore, 0, pGenero, pSalarioBaseCotizacionMensual, pRendimiento, pComisionAfore, pFechaNacimiento, 0, 0, pFechaCorte, pAportacion, pDensidadCotizacion, pEdadRetiro, (Tr / 100), n).ToString("c");

                                    if (!oCLASEAV.AhorroMensual_.ToString().Contains("-"))
                                    {
                                        valMap1 = "|" + (pSalarioBaseCotizacionMensual * (Tr / 100)).ToString("c0") + valMap1;
                                        valMap2 = "|" + oRNCalcularora.Av_mensual_IMSS(pSaldoActualAfore, 0, pGenero, pSalarioBaseCotizacionMensual, pRendimiento, pComisionAfore, pFechaNacimiento, 0, 0, pFechaCorte, pAportacion, pDensidadCotizacion, pEdadRetiro, (Tr / 100), n).ToString("c") + valMap2;
                                        valMap3 = "|" + (Tr / 100).ToString("p0").Replace(" ", "") + valMap3;
                                        valMap4 = "|" + (Tr / 100).ToString("p0").Replace(" ", "") + valMap4;
                                    }
                                    Tr = Tr - 10;
                                }
                                //gvPensionTasa.DataSource = lCLASEPensionMensualTasa;
                                //gvPensionTasa.DataBind();   
                                #endregion
                            }
                        }
                        else
                        {
                            #region NP
                            //El primer campo sera para validar que es Negativa De Pensión
                            sResultados_Calculo = "NP&";

                            double saldoAcumulado = 0;

                            OCLASEMT = new CLASESGenerales.CLASEPensionMensualTasa();
                            OCLASEMT.Texto_ = "Saldo acumulado";

                            saldoAcumulado = getSdoFinal(sParametros_Calculo);//oRNCalcularora.SaldoFinal;


                            //PGO 03032022
                            SdoFinalOrig = saldoAcumulado;
                           // ClasSGenerales.SaldoFinalOriginal = saldoAcumulado;


                            OCLASEMT.Numero_ = saldoAcumulado.ToString("c0");
                            lCLASEPensionMensualTasa.Add(OCLASEMT);
                            sResultados_Calculo += OCLASEMT.Numero_;

                            RNCalculadora.CapturaCalculadora objCapturCalculadora = new RNCalculadora.CapturaCalculadora();

                            objCapturCalculadora.Rendimiento = pRendimiento;
                            objCapturCalculadora.Comision = pComisionAfore;
                            objCapturCalculadora.SalarioBase = pSalarioBaseCotizacionMensual;
                            objCapturCalculadora.FechaNacimiento = pFechaNacimiento;
                            objCapturCalculadora.EdadRetiro = (int)pEdadRetiro;
                            objCapturCalculadora.Edad = (int)pEdad;
                            objCapturCalculadora.SaldoAcumulado = saldoAcumulado;

                            objCapturCalculadora.DensidadCotizacion = pDensidadCotizacion;
                            objCapturCalculadora.TasaRendimientoMensual = tRendimientoMensual;

                            List<RNCalculadora.NegativaPension> lstobjCalculoNegativaPension = RNCalculadora.CalculaSaldoAcumuladoNP(objCapturCalculadora,n);


                            //PGO 03032022
                            saldoFinalCompleto = lstobjCalculoNegativaPension[0].SaldoAhorroObligatorio;

                            int icont = 0;

                            foreach (RNCalculadora.NegativaPension objNegativaPension in lstobjCalculoNegativaPension)
                            {
                                string valMapTMP = string.Empty;
                                valMapTMP += ((float)objNegativaPension.PorcentajeSalario / 100.0).ToString("p0") + "|";
                                valMapTMP += Math.Round((decimal)objNegativaPension.PorcentajeSalarioPesos).ToString("N0") + "|";
                                valMapTMP += Math.Round((decimal)objNegativaPension.SaldoAhorroVoluntario).ToString("N0") + "|";
                                valMapTMP += Math.Round((decimal)objNegativaPension.SaldoAhorroObligatorio).ToString("N0") + "|";
                                valMapTMP += Math.Round((decimal)objNegativaPension.SaldoAhorroTotal).ToString("N0");

                                icont++;

                                switch (icont)
                                {
                                    case 1:
                                        valMap1 = valMapTMP;
                                        break;
                                    case 2:
                                        valMap2 = valMapTMP;
                                        break;
                                    case 3:
                                        valMap3 = valMapTMP;
                                        break;
                                    case 4:
                                        valMap4 = valMapTMP;
                                        break;
                                    case 5:
                                        valMap5 = valMapTMP;
                                        break;
                                    default:
                                        break;
                                }
                            }

                            sResultados_Calculo += "&" + MesesParaRetiro.ToString() + "&" + URVe.ToString() + "&" + oRNCalcularora.Rendimiento_mensual(pRendimiento) + "&" + oRNCalcularora.Comision_mensual(pComisionAfore);
                            #endregion
                        }
                    }
                    else
                    {
                        //lblMensaje.Text = MsgBox;
                        //mpeMensaje.Show();
                        return "&" + MsgBox;
                    }
                    SDatosGrafica = "&" + valMap1 + "&" + valMap2 + "&" + valMap3 + "&" + valMap4 + "&" + valMap5;
                }
            }
            catch (Exception ex) { return "&" + ex.Message.ToString(); }



            return sResultados_Calculo + SDatosGrafica + "&" + saldoFinalCompleto;
           // return sResultados_Calculo + SDatosGrafica;
        }

        public static String GetTablaPT()
        {
            return "";
        }

        public DataTable ComisionAfore(String N_CVE_AFORE)
        {
            DataTable dtAfores = new DataTable();
            try
            {
                SqlConnection cn = new SqlConnection(sCS);
                SqlCommand cmd = new SqlCommand("SELECT N_CVE_AFORE, T_DSC_AFORE, N_PORC_ANUAL_SALDO FROM dbo.BDE_C_COMISION_SIEFORES WHERE VIG_FLAG = 1 AND N_CVE_AFORE =" + N_CVE_AFORE, cn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand = cmd;
                da.Fill(dtAfores);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message.ToString() + "');</script>");
            }
            return dtAfores;
        }

        /// <summary>
        /// Valida que sea una fecha valida
        /// </summary>
        /// <param name="strFecha"></param>
        /// <returns>Json con objeto validacion de fecha</returns>
        /// 
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ValidaFecha(string strFecha)
        {
            RNCalculadora.FechaValida objFechaValida = new RNCalculadora.FechaValida();

            if (strFecha.Length == 10)
            {
                try
                {
                    int anio = Convert.ToInt32(strFecha.Substring(6, 4));
                    int mes = Convert.ToInt32(strFecha.Substring(3, 2));
                    int dia = Convert.ToInt32(strFecha.Substring(0, 2));
                    DateTime fechaNacimineto = new DateTime(anio, mes, dia);
                    DateTime fechaMaxima = DateTime.Now.AddYears(-15).Date;
                    DateTime fechaMinima = DateTime.Now.AddYears(-68).Date;
                    if (fechaMinima > fechaNacimineto || fechaNacimineto > fechaMaxima)
                    {
                        objFechaValida.Valida = false;
                        objFechaValida.MensajeError = "Para poder realizar una estimación de pensión, debes tener mínimo 15 años y como máximo 67 años.";
                    }
                    else
                    {
                        int edad = 0;
                        for (DateTime fechaNac = fechaNacimineto.AddYears(1); fechaNac <= DateTime.Now.Date; fechaNac = fechaNac.AddYears(1))
                        {
                            edad++;
                        }
                        objFechaValida.Valida = true;
                        objFechaValida.MensajeError = "";
                        objFechaValida.Edad = edad;
                    }
                }
                catch (Exception ex)
                {
                    objFechaValida.Valida = false;
                    objFechaValida.MensajeError = "La fecha de nacimiento ingresada no es válida.";
                }
            }
            else
            {
                objFechaValida.Valida = false;
                objFechaValida.MensajeError = "La fecha de nacimiento ingresada no es válida.";
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string theJson = serializer.Serialize(objFechaValida);

            return theJson;
        }

        /// <summary>
        /// Obtiene parametros de base de datos
        /// </summary>
        /// <returns>Json con lista de parametros</returns>
        [WebMethod]
        public static List<CLASESGenerales.Parametros> ObtenerParametros()
        {
            List<CLASESGenerales.Parametros> parametros = new List<CLASESGenerales.Parametros>();

            try
            {
                parametros = getParametros();
            }
            catch { }

            return parametros;
        }

        // PGO OBTENER PARÁMETROS DE SALARIOS MINIMOS DE BD Y GUARDARLOS EN UNA LISTA
        public static List<CLASESGenerales.Parametros> getParametros()
        //public  List<CLASESGenerales.Parametros> getParametros()
       
        {
            var listParametros = new List<CLASESGenerales.Parametros>();

            DataTable dt = new DataTable();
            try
            {
                SqlConnection cn = new SqlConnection(RNCalculadora.ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT N_ID_PAR, T_DSC_PAR, T_VALOR_PAR FROM dbo.BDE_C_PARAMETROS WHERE N_ID_PAR  in (7,8,9,10,11, 35, 36, 37, 38, 39,40, 41, 42, 43) and VIG_FLAG=1", cn);
                //SqlCommand cmd = new SqlCommand("SELECT N_ID_PAR, T_DSC_PAR, T_VALOR_PAR FROM dbo.BDE_C_PARAMETROS WHERE N_ID_PAR  in (41,42) and VIG_FLAG=1", cn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand = cmd;
                da.Fill(dt);

                CLASESGenerales.Parametros parametro;
                foreach (DataRow row in dt.Rows)
                {
                    parametro = new CLASESGenerales.Parametros();
                    parametro.Id = Convert.ToInt32(row["N_ID_PAR"]);
                    parametro.Descripcion = Convert.ToString(row["T_DSC_PAR"]);
                    

                    if (parametro.Id == 41 || parametro.Id == 42)
                    {
                        parametro.ValorFecha = Convert.ToString(row["T_VALOR_PAR"]);
                       // this.hdfFechacambio1.TemplateControl = parametro.ValorFecha;
                     //   hdfFechacambio1.text = parametro.ValorFecha;
                      //  hdfFechacambio1.
                    }
                    else
                    {
                        parametro.Valor = Convert.ToDouble(row["T_VALOR_PAR"]);
                    }
                    listParametros.Add(parametro);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return listParametros;
        }

        public static double getSdoFinal(String sParametros_Calculo)
        {
            double sFinal = 0;
            string[] aParametros = sParametros_Calculo.Split('|');
                Double pSalarioBaseCotizacionMensual = Convert.ToDouble(aParametros[6]);
                DateTime pFechaNacimiento = Convert.ToDateTime(aParametros[0]);
                Double pEdad = Convert.ToDouble(aParametros[10]);
                Double pEdadRetiro = Convert.ToDouble(aParametros[1]);
                Double pSaldoActualAfore = Convert.ToDouble(aParametros[3]);
                Double nSemanasCotizadas = Convert.ToDouble(aParametros[2]);
                Double pDensidadCotizacion = Convert.ToDouble(aParametros[4]);
                Double pGenero = Convert.ToDouble(aParametros[5]);
                Double pRendimiento = Convert.ToDouble(aParametros[7]);
                Double pComisionAfore = Convert.ToDouble(aParametros[8]);
                Double pAportacion = Convert.ToDouble(aParametros[9]);
                Double anioAfiliacion = Convert.ToDouble(aParametros[11]);

                //Monto en pesos de la aportación obligatoria inicial mensual
                Double aObligatoriaIniMensual = Convert.ToDouble(aParametros[13]);
                Double tRendimientoMensual = Convert.ToDouble(aParametros[14]);
                //Número de meses entre la fecha FC1 y la fecha de cálculo
                Int32 n1 = Convert.ToInt32(aParametros[15]);
                //Numero de meses que falta para que el trabajador cumpla la edad de retiro
                Int32 n = Convert.ToInt32(aParametros[16]);
                //Primera Fecha de Cambio
                DateTime fCambio1 = Convert.ToDateTime(aParametros[17]);
                //Segunda Fecha de cambio
                DateTime fCambio2 = Convert.ToDateTime(aParametros[18]);

                //PGO - ODT-07 Numero de meses entre FC1 y FC2.
                Int32 n2 = Convert.ToInt32(aParametros[19]);

                // PGO - ODT-07 - Número de años en los que se dan los incrementos en la  aportación gradual (N2)
                //Int32 N2_icrementosAP = Convert.ToInt32(aParametros[20]);

                // PGO - ODT-07 - Número de periodos que se acumularía la aportación despues del último incremento en la aportación gradual
                Int32 n2p = Convert.ToInt32(aParametros[20]);

                // PGO - ODT-07 - Tasa de rendimiento anual
                Double tRendimientoAnual = Convert.ToDouble(aParametros[21]);

                //PGO - ODT-07 Numero de meses entre FR y FC2.
                Int32 n3 = Convert.ToInt32(aParametros[22]);

                DateTime pFechaCorte = DateTime.Now.Date;
                RNCalculadora oRNCalcularora = new RNCalculadora();
                
                oRNCalcularora.Tipo = RNCalculadora.TipoConsulta.IMSS;
                sFinal = oRNCalcularora.GetSaldoFinal(pSaldoActualAfore, 0, pGenero, pSalarioBaseCotizacionMensual, pRendimiento, pComisionAfore, pFechaNacimiento, 0, 0, pFechaCorte, pAportacion, 0, 0, pDensidadCotizacion, pEdadRetiro, 3, aObligatoriaIniMensual, tRendimientoMensual, n1,n, fCambio1, fCambio2, n2, n2p, tRendimientoAnual, n3, pEdad);
                return sFinal;
        }

        [WebMethod]
        public static List<object> getLineChartData(String sParametros_Calculo)
        {
            List<object> iData = new List<object>();

            string[] aParametros = sParametros_Calculo.Split('|');
            if (!aParametros[1].Trim().Equals(String.Empty) && !aParametros[2].Trim().Equals(String.Empty) && !aParametros[3].Trim().Equals(String.Empty) && !aParametros[4].Trim().Equals(String.Empty) && !aParametros[5].Trim().Equals(String.Empty)
                && !aParametros[6].Trim().Equals(String.Empty) && !aParametros[7].Trim().Equals(String.Empty) && !aParametros[8].Trim().Equals(String.Empty) && !aParametros[9].Trim().Equals(String.Empty) && !aParametros[10].Trim().Equals(String.Empty))
            {
                Util oUtil = new Util();
                //Double pN_PORC_ANUAL_SALDO;
                Double pSalarioBaseCotizacionMensual = Convert.ToDouble(aParametros[6]);
                DateTime pFechaNacimiento = Convert.ToDateTime(aParametros[0]);
                Double pEdad = Convert.ToDouble(aParametros[10]);
                Double pEdadRetiro = Convert.ToDouble(aParametros[1]);
                Double pSaldoActualAfore = Convert.ToDouble(aParametros[3]);
                Double nSemanasCotizadas = Convert.ToDouble(aParametros[2]);
                Double pDensidadCotizacion = Convert.ToDouble(aParametros[4]);
                Double pGenero = Convert.ToDouble(aParametros[5]);
                Double pRendimiento = Convert.ToDouble(aParametros[7]);
                Double pComisionAfore = Convert.ToDouble(aParametros[8]);
                Double pAportacion = Convert.ToDouble(aParametros[9]);
                Double anioAfiliacion = Convert.ToDouble(aParametros[11]);

                //Monto en pesos de la aportación obligatoria inicial mensual
                Double aObligatoriaIniMensual = Convert.ToDouble(aParametros[13]);
                Double tRendimientoMensual = Convert.ToDouble(aParametros[14]);
                //Número de meses entre la fecha FC1 y la fecha de cálculo
                Int32 n1 = Convert.ToInt32(aParametros[15]);
                //Numero de meses que falta para que el trabajador cumpla la edad de retiro
                Int32 n = Convert.ToInt32(aParametros[16]);
                //Primera Fecha de Cambio
                DateTime fCambio1 = Convert.ToDateTime(aParametros[17]);
                //Segunda Fecha de cambio
                DateTime fCambio2 = Convert.ToDateTime(aParametros[18]);

                //PGO - ODT-07 Numero de meses entre FC1 y FC2.
                Int32 n2 = Convert.ToInt32(aParametros[19]);

                // PGO - ODT-07 - Número de años en los que se dan los incrementos en la  aportación gradual (N2)
               // Int32 N2_icrementosAP = Convert.ToInt32(aParametros[20]);

                // PGO - ODT-07 - Número de periodos que se acumularía la aportación despues del último incremento en la aportación gradual
                Int32 n2p = Convert.ToInt32(aParametros[20]);

                // PGO - ODT-07 - Tasa de rendimiento anual
                Double tRendimientoAnual = Convert.ToDouble(aParametros[21]);

                //PGO - ODT-07 Numero de meses entre FR y FC2.
                Int32 n3 = Convert.ToInt32(aParametros[22]);

                DateTime pFechaCorte = DateTime.Now.Date;
                RNCalculadora oRNCalcularora = new RNCalculadora();
                //Boolean bAlcanzaPMG = false;
                //Double Tr = 100;                                


                oRNCalcularora.Tipo = RNCalculadora.TipoConsulta.IMSS;
                //iData = oRNCalcularora.GetDataSetGraficaUno(pSaldoActualAfore, 0, pGenero, pSalarioBaseCotizacionMensual, pRendimiento, pComisionAfore, pFechaNacimiento, 0, 0, pFechaCorte, pAportacion, 0, 0, pDensidadCotizacion, pEdadRetiro, 3);
                iData = oRNCalcularora.GetDataSetGraficaUno(pSaldoActualAfore, 0, pGenero, pSalarioBaseCotizacionMensual, pRendimiento, pComisionAfore, pFechaNacimiento, 0, 0, pFechaCorte, pAportacion, 0, 0, pDensidadCotizacion, pEdadRetiro, 3, aObligatoriaIniMensual, tRendimientoMensual, n1, n, fCambio1, fCambio2, n2, n2p, tRendimientoAnual, n3, pEdad);
                                                                                                                                                                                               
                return iData;
            }
            return iData;
        }

      [WebMethod]
      public static List<object> getLineChartData2(String sParametros_Calculo)
      {
         List<object> iData = new List<object>();

         string[] aParametros = sParametros_Calculo.Split('|');
         if (!aParametros[1].Trim().Equals(String.Empty) && !aParametros[2].Trim().Equals(String.Empty) && !aParametros[3].Trim().Equals(String.Empty) && !aParametros[4].Trim().Equals(String.Empty) && !aParametros[5].Trim().Equals(String.Empty)
             && !aParametros[6].Trim().Equals(String.Empty) && !aParametros[7].Trim().Equals(String.Empty) && !aParametros[8].Trim().Equals(String.Empty) && !aParametros[9].Trim().Equals(String.Empty) && !aParametros[10].Trim().Equals(String.Empty))
         {
            Util oUtil = new Util();
            //Double pN_PORC_ANUAL_SALDO;
            Double pSalarioBaseCotizacionMensual = Convert.ToDouble(aParametros[6]);
            DateTime pFechaNacimiento = Convert.ToDateTime(aParametros[0]);
            Double pEdad = Convert.ToDouble(aParametros[10]);
            Double pEdadRetiro = Convert.ToDouble(aParametros[1]);
            Double pSaldoActualAfore = Convert.ToDouble(aParametros[3]);
            Double nSemanasCotizadas = Convert.ToDouble(aParametros[2]);
            Double pDensidadCotizacion = Convert.ToDouble(aParametros[4]);
            Double pGenero = Convert.ToDouble(aParametros[5]);
            Double pRendimiento = Convert.ToDouble(aParametros[7]);
            Double pComisionAfore = Convert.ToDouble(aParametros[8]);
            Double pAportacion = Convert.ToDouble(aParametros[9]);
            Double anioAfiliacion = Convert.ToDouble(aParametros[11]);

            //Monto en pesos de la aportación obligatoria inicial mensual
            Double aObligatoriaIniMensual = Convert.ToDouble(aParametros[13]);
            Double tRendimientoMensual = Convert.ToDouble(aParametros[14]);
            //Número de meses entre la fecha FC1 y la fecha de cálculo
            Int32 n1 = Convert.ToInt32(aParametros[15]);
            //Numero de meses que falta para que el trabajador cumpla la edad de retiro
            Int32 n = Convert.ToInt32(aParametros[16]);
            //Primera Fecha de Cambio
            DateTime fCambio1 = Convert.ToDateTime(aParametros[17]);
            //Segunda Fecha de cambio
            DateTime fCambio2 = Convert.ToDateTime(aParametros[18]);

            //PGO - ODT-07 Numero de meses entre FC1 y FC2.
            Int32 n2 = Convert.ToInt32(aParametros[19]);

            // PGO - ODT-07 - Número de años en los que se dan los incrementos en la  aportación gradual (N2)
            // Int32 N2_icrementosAP = Convert.ToInt32(aParametros[20]);

            // PGO - ODT-07 - Número de periodos que se acumularía la aportación despues del último incremento en la aportación gradual
            Int32 n2p = Convert.ToInt32(aParametros[20]);

            // PGO - ODT-07 - Tasa de rendimiento anual
            Double tRendimientoAnual = Convert.ToDouble(aParametros[21]);

            //PGO - ODT-07 Numero de meses entre FR y FC2.
            Int32 n3 = Convert.ToInt32(aParametros[22]);
            //Pension deseada
            Double nPensionDeseada = Convert.ToDouble(aParametros[23]);

            DateTime pFechaCorte = DateTime.Now.Date;
            RNCalculadora oRNCalcularora = new RNCalculadora();
            //Boolean bAlcanzaPMG = false;
            //Double Tr = 100;                                


            oRNCalcularora.Tipo = RNCalculadora.TipoConsulta.IMSS;
            iData = oRNCalcularora.GetDataSetGraficaDos(pSaldoActualAfore, 0, pGenero, pSalarioBaseCotizacionMensual, pRendimiento, pComisionAfore, pFechaNacimiento, 0, 0, pFechaCorte, pAportacion, 0, 0, pDensidadCotizacion, pEdadRetiro, 3, aObligatoriaIniMensual, tRendimientoMensual, n1, n, fCambio1, fCambio2, n2, n2p, tRendimientoAnual, n3, nSemanasCotizadas, pEdad, nPensionDeseada);

            return iData;
         }
         return iData;
      }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LLamarOpcionSatisfaccion(int ValorCarita)
        {
            
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string theJson = serializer.Serialize("HOLA");

            return theJson;
        }



    }
}
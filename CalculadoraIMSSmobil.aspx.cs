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

namespace CALCULADORA2014
{
    public partial class CalculadoraIMSSmobil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    
                    string parametros = string.Empty ;
                    parametros = Request.QueryString["fullname"];


                    if (parametros != string.Empty && parametros != null)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Datos", "RecibeParameters('" + parametros.Trim() + "');", true);                       
                    }
                    else
                    {
                       // Ejemplo de parametros para crear la tabla
                        parametros = "12/12/1950|65|5|200000.00|0.8|0|4000.00|0.04|0.0115|0.065|64";
                        // Ejemplo de parametros para crear la grafica
                         parametros = "12/12/1980|65|5|200000.00|0.8|0|4000.00|0.04|0.0115|0.065|34";
                        ClientScript.RegisterStartupScript(GetType(), "Datos", "RecibeParameters('" + parametros + "');", true); 
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

     
         [WebMethod]
        public static String GetComisionesAfore(String N_CVE_AFORE)
        {
            String sComision = String.Empty;

            try
            {
                DataTable dtAfores = new DataTable();
                SqlConnection cn = new SqlConnection(sCS);
                SqlCommand cmd = new SqlCommand("SELECT N_CVE_AFORE FROM dbo.BDE_C_COMISION_SIEFORES WHERE VIG_FLAG = 1 ORDER BY (N_PORC_ANUAL_SALDO) ASC", cn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand = cmd;
                da.Fill(dtAfores);
                for (int i = 0; i < dtAfores.Rows.Count; i++)
                {
                    sComision += "|" + dtAfores.Rows[i][0];
                }
            }
            catch(Exception ex){}
            return sComision;
        }


        [WebMethod]
        public static String GetComisionAfore(String N_CVE_AFORE)
        {
            String sComision = String.Empty;

            try
            {
                DataTable dtAfores = new DataTable();
                SqlConnection cn = new SqlConnection(sCS);
                SqlCommand cmd = new SqlCommand("SELECT N_CVE_AFORE, T_DSC_AFORE, N_PORC_ANUAL_SALDO FROM dbo.BDE_C_COMISION_SIEFORES WHERE VIG_FLAG = 1 AND N_CVE_AFORE =" + N_CVE_AFORE, cn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand = cmd;
                da.Fill(dtAfores);
                //sComision = Convert.ToDecimal(dtAfores.Rows[0][2]).ToString("P");
                sComision = Convert.ToDouble(dtAfores.Rows[0][2]).ToString("P");
            }
            catch(Exception ex){}
            return sComision;
        }

        [WebMethod]
        public static String GetCalculo(String sParametros_Calculo)
        {
            String sResultados_Calculo = String.Empty;
            String SDatosGrafica = String.Empty;
            String MsgBox = String.Empty;
            int pNegativaPension = 0;
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
                    Double PMG = 2601;
                    Double PME = 0;


                    String valMap1 = String.Empty;
                    String valMap2 = String.Empty;
                    String valMap3 = String.Empty;
                    String valMap4 = String.Empty;
                    String valMap5 = String.Empty;



                    RNCalculadora oRNCalcularora = new RNCalculadora();
                    Boolean bAlcanzaPMG = false;


                    Double Tr = 100;

                    CLASESGenerales.CLASEAhorroVoluntario oCLASEAV = null;
                    CLASESGenerales.CLASEPensionMensualTasa OCLASEMT = null;

                    List<CLASESGenerales.CLASEAhorroVoluntario> lCLASEAhorroVoluntario = new List<CLASESGenerales.CLASEAhorroVoluntario>();
                    List<CLASESGenerales.CLASEPensionMensualTasa> lCLASEPensionMensualTasa = new List<CLASESGenerales.CLASEPensionMensualTasa>();

                    if (oRNCalcularora.Calcular(pFechaNacimiento, pEdadRetiro, nSemanasCotizadas, pSaldoActualAfore, pDensidadCotizacion, pSalarioBaseCotizacionMensual, out MsgBox, out pNegativaPension, n))
                    {
                        if (pNegativaPension == 0)
                        {
                            OCLASEMT = new CLASESGenerales.CLASEPensionMensualTasa();
                            OCLASEMT.Texto_ = "Saldo acumulado";
                            OCLASEMT.Numero_ = oRNCalcularora.Modelo_mensual_iterativo(pSaldoActualAfore, 0, pGenero, pSalarioBaseCotizacionMensual, pRendimiento, pComisionAfore, pFechaNacimiento, 0, 0, pFechaCorte, pAportacion, 0, 0, pDensidadCotizacion, pEdadRetiro, 1, aObligatoriaIniMensual, tRendimientoMensual, n1, n, fCambio1, fCambio2, n2, n2p, tRendimientoAnual, n3, pEdad, nSemanasCotizadas).ToString("c0");
                            lCLASEPensionMensualTasa.Add(OCLASEMT);
                            sResultados_Calculo += OCLASEMT.Numero_;

                            OCLASEMT = new CLASESGenerales.CLASEPensionMensualTasa();
                            OCLASEMT.Texto_ = "Pensión mensual estimada";
                            PME = oRNCalcularora.Modelo_mensual_iterativo(pSaldoActualAfore, 0, pGenero, pSalarioBaseCotizacionMensual, pRendimiento, pComisionAfore, pFechaNacimiento, 0, 0, pFechaCorte, pAportacion, 0, 0, pDensidadCotizacion, pEdadRetiro, 2, aObligatoriaIniMensual, tRendimientoMensual, n1, n, fCambio1, fCambio2, n2,  n2p, tRendimientoAnual, n3, pEdad, nSemanasCotizadas);
                            OCLASEMT.Numero_ = PME.ToString("c0");
                            lCLASEPensionMensualTasa.Add(OCLASEMT);
                            sResultados_Calculo += "|" + OCLASEMT.Numero_;

                            OCLASEMT = new CLASESGenerales.CLASEPensionMensualTasa();
                            OCLASEMT.Texto_ = "Tasa de reemplazo (porcentaje de la Pensión mensual estimada con respecto al Salario Base de Cotización mensual)";
                            OCLASEMT.Numero_ = oRNCalcularora.Modelo_mensual_iterativo(pSaldoActualAfore, 0, pGenero, pSalarioBaseCotizacionMensual, pRendimiento, pComisionAfore, pFechaNacimiento, 0, 0, pFechaCorte, pAportacion, 0, 0, pDensidadCotizacion, pEdadRetiro, 3, aObligatoriaIniMensual, tRendimientoMensual, n1, n, fCambio1, fCambio2, n2, n2p, tRendimientoAnual, n3, pEdad, nSemanasCotizadas).ToString("p1");
                            lCLASEPensionMensualTasa.Add(OCLASEMT);
                            sResultados_Calculo += "|" + OCLASEMT.Numero_ + "&";



                            for (Int32 x = 0; x < 10; x++)
                            {
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

                            if (PME > PMG)
                            {
                                sResultados_Calculo += "&" + "1";
                                //gvAhorroVoluntario.DataSource = lCLASEAhorroVoluntario;
                                //gvAhorroVoluntario.DataBind();
                                //llenarcontrol(lCLASEAhorroVoluntario);

                            }
                            else
                            {
                                //MOstrar mensaje alcanza pension minima garantizada
                                bAlcanzaPMG = true;
                                sResultados_Calculo += "&" + "0";
                            }
                        }
                        else
                        {
                            //El primer campo sera para validar que es Nagetiva de pension
                            sResultados_Calculo = "NP&";

                            double saldoAcumulado = 0;

                            OCLASEMT = new CLASESGenerales.CLASEPensionMensualTasa();
                            OCLASEMT.Texto_ = "Saldo acumulado";

                            saldoAcumulado = oRNCalcularora.Modelo_mensual_iterativo(pSaldoActualAfore, 0, pGenero, pSalarioBaseCotizacionMensual,
                                                                                       pRendimiento, pComisionAfore, pFechaNacimiento, 0, 0, pFechaCorte,
                                                                                       pAportacion, 0, 0, pDensidadCotizacion, pEdadRetiro, 1, aObligatoriaIniMensual,
                                                                                       tRendimientoMensual, n1, n, fCambio1, fCambio2, n2, n2p,
                                                                                       tRendimientoAnual, n3, pEdad, nSemanasCotizadas);
                            OCLASEMT.Numero_ = saldoAcumulado.ToString("c0");
                            lCLASEPensionMensualTasa.Add(OCLASEMT);
                            sResultados_Calculo += OCLASEMT.Numero_ + "&";

                            RNCalculadora.CapturaCalculadora objCapturCalculadora = new RNCalculadora.CapturaCalculadora();

                            objCapturCalculadora.Rendimiento = pRendimiento;
                            objCapturCalculadora.Comision = pComisionAfore;
                            objCapturCalculadora.SalarioBase = pSalarioBaseCotizacionMensual;
                            objCapturCalculadora.FechaNacimiento = pFechaNacimiento;
                            objCapturCalculadora.EdadRetiro = (int)pEdadRetiro;
                            objCapturCalculadora.Edad = (int)pEdad;
                            objCapturCalculadora.SaldoAcumulado = saldoAcumulado;
                            objCapturCalculadora.DensidadCotizacion = pDensidadCotizacion;

                            List<RNCalculadora.NegativaPension> lstobjCalculoNegativaPension = RNCalculadora.CalculaSaldoAcumuladoNP(objCapturCalculadora, n);

                            
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

            return sResultados_Calculo + SDatosGrafica;

            
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
       [WebMethod]
       [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
       public static string ValidaFecha(string strFecha)
       {
           RNCalculadora.FechaValida objFechaValida = new RNCalculadora.FechaValida();

           if (strFecha.Length == 10)
           {
               try
               {
                   int anio = Convert.ToInt32(strFecha.Substring(6,4));
                   int mes = Convert.ToInt32(strFecha.Substring(3,2));
                   int dia = Convert.ToInt32(strFecha.Substring(0, 2));
                   DateTime fechaNacimineto = new DateTime(anio, mes, dia);
                   DateTime fechaMaxima = DateTime.Now.AddYears(-14).Date;
                   DateTime fechaMinima = DateTime.Now.AddYears(-101).Date;
                   if (fechaMinima > fechaNacimineto || fechaNacimineto > fechaMaxima)
                   {
                       objFechaValida.Valida = false;
                       objFechaValida.MensajeError = "Debes tener 15 años como mínimo y hasta 67 años como máximo.";
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
    }
}
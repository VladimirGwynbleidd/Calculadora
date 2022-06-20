using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace CALCULADORA2014
{
    public class ExecuteQuery
    {
        private string stringconnection = ConfigurationManager.ConnectionStrings["DbConection"].ConnectionString;

        public ExecuteQuery(string dbconnection)
        {
            stringconnection = dbconnection;
        }
        public ExecuteQuery() { }
       

        public DataSet Execute_CargaAfores(string sp_name)
        {
            DataSet objDs = null;
            using (SqlConnection objConn = new SqlConnection(stringconnection))
            {
                using (SqlCommand objCom = new SqlCommand(sp_name, objConn))
                {
                    objCom.CommandTimeout = 180;
                    objCom.CommandType = CommandType.StoredProcedure;                 
                    SqlDataAdapter objDa = new SqlDataAdapter(objCom);
                    objDs = new DataSet();
                    objDa.Fill(objDs);

                }
            }
            return objDs;
        }

        public List<CALCULADORA2014.Calculos.NegocioEntities> GetCalculo_Valores(String sParametros_Calculo)
        {
            try
            {
                List<CALCULADORA2014.Calculos.NegocioEntities> sResultados_Calculo = new List<CALCULADORA2014.Calculos.NegocioEntities>();
                //string sResultados_Calculo;
                sResultados_Calculo = GetCalculo(sParametros_Calculo, 1);

                return sResultados_Calculo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public List<CALCULADORA2014.Calculos.NegocioEntities> GetCalculo_Grafica(String sParametros_Calculo)
        {
            List<CALCULADORA2014.Calculos.NegocioEntities> sResultados_Calculo = new List<CALCULADORA2014.Calculos.NegocioEntities>();
            sResultados_Calculo = GetCalculo(sParametros_Calculo, 2);
            return sResultados_Calculo;
        }

        //*****************************OBTIENE VALORES ********************************/
       // public String GetCalculo(String sParametros_Calculo, int Opcion)
        public List<CALCULADORA2014.Calculos.NegocioEntities> GetCalculo(String sParametros_Calculo, int Opcion)
        {
            String sResultados_Calculo = String.Empty;
            String SDatosGrafica = String.Empty;
            String MsgBox = String.Empty;
            int pNegativaPension = 0;
            List<CALCULADORA2014.Calculos.NegocioEntities> Result = new List<CALCULADORA2014.Calculos.NegocioEntities>();
            CALCULADORA2014.Calculos.NegocioEntities ResultValores = new Calculos.NegocioEntities();

            List<object> ResultGrafica = new List<object>();
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
                 //  Double PMG = 2601;
                    //PGO Inicializamos PMG 
                    Double PMG = 0;
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

                            ResultValores.Saldo =OCLASEMT.Numero_.ToString();

                            OCLASEMT = new CLASESGenerales.CLASEPensionMensualTasa();
                            OCLASEMT.Texto_ = "Pensión mensual estimada";
                            PME = oRNCalcularora.Modelo_mensual_iterativo(pSaldoActualAfore, 0, pGenero, pSalarioBaseCotizacionMensual, pRendimiento, pComisionAfore, pFechaNacimiento, 0, 0, pFechaCorte, pAportacion, 0, 0, pDensidadCotizacion, pEdadRetiro, 2, aObligatoriaIniMensual, tRendimientoMensual, n1, n, fCambio1, fCambio2, n2, n2p, tRendimientoAnual, n3, pEdad, nSemanasCotizadas);
                            OCLASEMT.Numero_ = PME.ToString("c0");
                            lCLASEPensionMensualTasa.Add(OCLASEMT);
                            sResultados_Calculo += "|" + OCLASEMT.Numero_;

                            
                            ResultValores.Pension = OCLASEMT.Numero_.ToString();

                            OCLASEMT = new CLASESGenerales.CLASEPensionMensualTasa();
                            OCLASEMT.Texto_ = "Tasa de reemplazo (porcentaje de la Pensión mensual estimada con respecto al Salario Base de Cotización mensual)";
                            OCLASEMT.Numero_ = oRNCalcularora.Modelo_mensual_iterativo(pSaldoActualAfore, 0, pGenero, pSalarioBaseCotizacionMensual, pRendimiento, pComisionAfore, pFechaNacimiento, 0, 0, pFechaCorte, pAportacion, 0, 0, pDensidadCotizacion, pEdadRetiro, 3, aObligatoriaIniMensual, tRendimientoMensual, n1, n, fCambio1, fCambio2, n2, n2p, tRendimientoAnual, n3, pEdad, nSemanasCotizadas).ToString("p1");
                            lCLASEPensionMensualTasa.Add(OCLASEMT);
                            sResultados_Calculo += "|" + OCLASEMT.Numero_ + "&";
                            
                            ResultValores.Porcentaje = OCLASEMT.Numero_.ToString();

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

                            string[] Parametros = sResultados_Calculo.Split('|');

                            List<object> ResultadoCal = new List<object>();

                                                   
                            //PGO Se asigna el valor obtenido a la variable PMG (Pensión Mínima Garantizada)
                            PMG = oRNCalcularora.PMGBD;

                            

                            for (int i = 0; i < Parametros.Length ; i++)
                            {
                                ResultadoCal.Add(Parametros[i]); 
                            }
                                //gvPensionTasa.DataSource = lCLASEPensionMensualTasa;
                                //gvPensionTasa.DataBind();

                                if (PME > PMG)
                                {
                                    sResultados_Calculo += "&" + "1";
                                    //gvAhorroVoluntario.DataSource = lCLASEAhorroVoluntario;
                                    //gvAhorroVoluntario.DataBind();
                                    //llenarcontrol(lCLASEAhorroVoluntario);

                                    ResultValores.Tipo = "1";
                                }
                                else
                                {
                                    //MOstrar mensaje alcanza pension minima garantizada
                                    bAlcanzaPMG = true;
                                    sResultados_Calculo += "&" + "0";
                                    
                                    ResultValores.Tipo = "2";
                                    ResultValores.Pension = "$2,601.00 *";
                                    ResultValores.Porcentaje = (PMG / pSalarioBaseCotizacionMensual).ToString("P");
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

                            ResultValores.Saldo =saldoAcumulado.ToString() ; 
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
                            objCapturCalculadora.DensidadCotizacion = pDensidadCotizacion;

                            List<RNCalculadora.NegativaPension> lstobjCalculoNegativaPension = RNCalculadora.CalculaSaldoAcumuladoNP(objCapturCalculadora, n);

                            if (saldoAcumulado == 0)
                            {
                                objCapturCalculadora.SaldoAcumulado = Double.Parse(HttpContext.Current.Session["sdoAcumRetiro"].ToString());
                            }
                            else
                            {
                                objCapturCalculadora.SaldoAcumulado = saldoAcumulado;
                            }

                            int icont = 0;
                            ResultValores.Tipo = "3";
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
                        Result.Add(ResultValores);
                    }
                    else
                    {

                        Result = null;
                        return Result ;
                        //return "&" + MsgBox;
                    }

                    ResultGrafica.Add(valMap1);
                    ResultGrafica.Add(valMap2);
                    ResultGrafica.Add(valMap3);
                    ResultGrafica.Add(valMap4);
                    ResultGrafica.Add(valMap5);

                    SDatosGrafica = "&" + valMap1 + "&" + valMap2 + "&" + valMap3 + "&" + valMap4 + "&" + valMap5;
                }
            }
            catch (Exception ex) 
            {
                return null;
                //return "&" + ex.Message.ToString();
            }

          
                return Result;
               // return sResultados_Calculo;
            
            //return sResultados_Calculo + SDatosGrafica;
        }
   

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace CALCULADORA2014
{
    public class RNCalculadora
    {
        //PGO Constantes de parámetros para salariosMinimos(7,8), PMG (11)
        #region Constantes Parametros

        //Parametro para ID de SM ZonaA
        private const int PARAM7 = 7;
        //Parametro para ID de SM ZonaB
        private const int PARAM8 = 8;
        //Parametro para ID de PMG
        private const int PARAM11 = 11;

        private const int PARAM40 = 40;

        //private const int PARAM10 = 10;
        //private const int PARAM11 = 11;
        //private const int PARAM12 = 12;
        //private const int PARAM13 = 13;
        //private const int PARAM14 = 14;
        #endregion

        #region Propiedades parametros y condicion IMSS

        /// <summary>
        /// PGO Permite establecer los parametro para salarioss mínimos
        /// </summary>
        public List<CLASESGenerales.Parametros> Parametros
        {
            get;
            set;
        }

        /// <summary>
        /// IM01SMGVDF = IMSS salario mínimo Zona A
        /// </summary>
        public double IM01SMGVDF
        {
            get
            {
                if (this.Parametros == null)
                    return 0;

                if (this.Parametros.Count == 0)
                    return 0;

                var parametro = this.Parametros.Find(delegate (CLASESGenerales.Parametros par)
                {
                    return par.Id == PARAM7;
                });

                return parametro.Valor;
            }
        }

        public double IM01UMA
        {
            get
            {
                if (this.Parametros == null)
                    return 0;

                if (this.Parametros.Count == 0)
                    return 0;

                var parametro = this.Parametros.Find(delegate (CLASESGenerales.Parametros par)
                {
                    return par.Id == PARAM40;
                });

                return parametro.Valor;
            }
        }
        /// <summary>
        /// Saldo acumulado del ahorro para el retiro.
        /// 
        /// Nota:
        ///     Este valor es valido despues de ejecutar "Modelo_mensual_iterativo"
        /// </summary>
        public Double SaldoFinal { get { return Saldo_final; } set { Saldo_final = value; } }

        /// <summary>
        /// Monto de la Pension Mensual Estimada  (PME).
        /// 
        /// Nota:
        ///     Este valor es valido despues de ejecutar "Modelo_mensual_iterativo"
        /// </summary>
        public Double MontoPME { get { return monto_PME; } }

        /// <summary>
        /// Tasa de Reemplazo (Tr).
        /// 
        /// Nota:
        ///     Este valor es valido despues de ejecutar "Modelo_mensual_iterativo"
        /// </summary>
        public Double TasaReemplazo { get { return tasaR; } }

        /// <summary>
        /// Salario diario (SD).  =  Salario Base de Cotizacion / 30
        /// 
        /// 
        /// Nota:
        ///     Este valor es valido despues de ejecutar "Modelo_mensual_iterativo"
        /// </summary>
        public Double SalarioDiario { get { return salario_diario; } }

        /// <summary>
        /// Cuota Social Mensual
        /// 
        /// Nota:
        ///     Este valor es valido despues de ejecutar "Modelo_mensual_iterativo"
        /// </summary>
        public Double CuotaSocialMensual { get { return cuota_social; } }

        /// <summary>
        /// Aportacion Mensual Obligatoria
        /// 
        /// Nota:
        ///     Este valor es valido despues de ejecutar "Modelo_mensual_iterativo"
        /// </summary>
        public Double AportacionMensual { get { return aportacion_mensual; } }


        ///<sumary>
        /// Semas cotizadas despues de la fecha de calculo
        /// Este valor es valido despues de ejecutar "Modelo_mensual_iterativo"
        /// </sumary>
        public Double SCER { get { return scer; } }


        ///<sumary>
        /// Semanas cotizadas despues de la fecha de calculo
        /// Este valor es valido despues de ejecutar "Modelo_mensual_iterativo"
        /// </sumary>
        public Double RequisitoSC { get { return Requisito_SC; } }



        ///<sumary>
        /// Pensión Garantizada 
        /// Este valor es valido despues de ejecutar "Modelo_mensual_iterativo"
        /// </sumary>
        public Double PensionGarantizada { get { return Pension_Garantizada; } }


        /// <summary>
        /// IM01SMGVZB = IMSS salario mínimo Zona B
        /// </summary>
        public double IM01SMGVZB
        {
            get
            {
                if (this.Parametros == null)
                    return 0;

                if (this.Parametros.Count == 0)
                    return 0;

                var parametro = this.Parametros.Find(delegate (CLASESGenerales.Parametros par)
                {
                    return par.Id == PARAM8;
                });

                return parametro.Valor;
            }
        }

        /// <summary>
        /// IN02SMGVDF = INDEPENDIENTE salario mínimo Zona A (No implementado)
        /// </summary>

        /*  public double IN02SMGVDF
          {
              get
              {
                  if (this.Parametros == null)
                      return 0;

                  if (this.Parametros.Count == 0)
                      return 0;

                  var parametro = this.Parametros.Find(delegate(CLASESGenerales.Parametros par)
                  {
                      return par.Id == PARAM12;
                  });

                  return parametro.Valor;
              }
          }*/

        /// <summary>
        /// IN02SMGVZB = INDEPENDIENTE salario mínimo Zona B (No implementado)
        /// </summary>

        /*
        public double IN02SMGVZB
        {
            get
            {
                if (this.Parametros == null)
                    return 0;

                if (this.Parametros.Count == 0)
                    return 0;

                var parametro = this.Parametros.Find(delegate(CLASESGenerales.Parametros par)
                {
                    return par.Id == PARAM13;
                });

                return parametro.Valor;
            }
        }*/

        /// <summary>
        /// PMGBD = Pensión Mínima Garantizada (Base Datos)
        /// </summary>
        public double PMGBD
        {
            get
            {
                if (this.Parametros == null)
                    return 0;

                if (this.Parametros.Count == 0)
                    return 0;

                var parametro = this.Parametros.Find(delegate (CLASESGenerales.Parametros par)
                {
                    return par.Id == PARAM11;
                });

                return parametro.Valor;
            }
        }

        /// <summary>
        /// Tipo consulta IMMS, IND
        /// </summary>
        public TipoConsulta Tipo
        {
            get;
            set;
        }

        #endregion

        #region Enumeraciones

        /// <summary>
        ///  PGO enumeraciones para calculadora IMMS o INDEPENDIENTE
        /// </summary>
        public enum TipoConsulta
        {
            IMSS = 0,
            IND = 1
        };

        #endregion
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["DbConection"].ConnectionString;
        //public static string ConnectionString = @"Data Source=172.16.10.23\dwwebconsar;Initial Catalog=BD_SQL_CALCULADORA_AV;User ID=calculadora_av;Password=c41cu14d0r4_4v;";

        public enum Genero
        {
            Hombre,
            Mujer
        };


        /// <summary>
        /// Clase para la tabla de negativa de pension
        /// </summary>
        public class NegativaPension
        {
            private int _porcentajeSalario;
            private double _porcentajeSalarioPesos;
            private double _saldoAhorroVoluntario;
            private double _saldoAhorroObligatorio;
            private double _saldoAhorroTotal;
            //PGO
            private double _SCER;

            public int PorcentajeSalario
            {
                get { return _porcentajeSalario; }
                set { _porcentajeSalario = value; }
            }

            public double PorcentajeSalarioPesos
            {
                get { return _porcentajeSalarioPesos; }
                set { _porcentajeSalarioPesos = value; }
            }

            public double SaldoAhorroVoluntario
            {
                get { return _saldoAhorroVoluntario; }
                set { _saldoAhorroVoluntario = value; }
            }

            public double SaldoAhorroObligatorio
            {
                get { return _saldoAhorroObligatorio; }
                set { _saldoAhorroObligatorio = value; }
            }

            public double SaldoAhorroTotal
            {
                get { return _saldoAhorroTotal; }
                set { _saldoAhorroTotal = value; }
            }


        }

        public class CapturaCalculadora
        {
            double _comision;
            double _salarioBase;
            Genero _genero;
            DateTime _fechaNacimiento;
            int _edad;
            int _edadRetiro;
            int _aniosCotizados;
            double _rendimiento;
            double _densidadCotizacion;
            double _saldoAcumulado;
            List<object> _iData;
            double _tRendimientoMensual;

            public double Comision
            {
                get { return _comision; }
                set { _comision = value; }
            }

            public double SalarioBase
            {
                get { return _salarioBase; }
                set { _salarioBase = value; }
            }

            public Genero Genero
            {
                get { return _genero; }
                set { _genero = value; }
            }

            public DateTime FechaNacimiento
            {
                get { return _fechaNacimiento; }
                set { _fechaNacimiento = value; }
            }

            public int Edad
            {
                get { return _edad; }
                set { _edad = value; }
            }

            public int EdadRetiro
            {
                get { return _edadRetiro; }
                set { _edadRetiro = value; }
            }

            public int AniosCotizados
            {
                get { return _aniosCotizados; }
                set { _aniosCotizados = value; }
            }

            public double Rendimiento
            {
                get { return _rendimiento; }
                set { _rendimiento = value; }
            }

            public double DensidadCotizacion
            {
                get { return _densidadCotizacion; }
                set { _densidadCotizacion = value; }
            }

            public double SaldoAcumulado
            {
                get { return _saldoAcumulado; }
                set { _saldoAcumulado = value; }
            }

            public List<object> iData
            {
                get { return _iData; }
                set { _iData = value; }
            }

            public double TasaRendimientoMensual
            {
                get { return _tRendimientoMensual; }
                set { _tRendimientoMensual = value; }
            }
        }

        //Clase para el transporte de la validacion de la fecha        
        public class FechaValida
        {
            private Boolean _valida;
            private string _mensajeError;
            private int _edad;

            public Boolean Valida
            {
                get { return _valida; }
                set { _valida = value; }
            }

            public string MensajeError
            {
                get { return _mensajeError; }
                set { _mensajeError = value; }
            }

            public int Edad
            {
                get { return _edad; }
                set { _edad = value; }
            }

        }


        public Boolean Calcular(DateTime pFechaNacimiento, Double pEdadRetiro, Double nSemanasCotizadas, Double pSaldoActualAfore, Double pDensidadCotizacion, Double pSalarioBaseCotizacionMensual, out String MsgBox, out int pNegativaPension, int n)
        {
            DateTime pFechaCorte = DateTime.Now.Date;
            DateTime pFechaMaximaNacimiento = new DateTime(pFechaCorte.Year, pFechaCorte.Month, pFechaCorte.Day).AddYears(-Convert.ToInt32(pEdadRetiro));
            //SMGVDF = 67.29; // RARS SM Zona A
            //double SMGVZB = 63.77; // RARS SM Zona B

            SMGVDF = 0;
            double SMGVZB = 0;
            double valor_UMAMC = 0;

            // PGO Salarios Minimos si es calculadora IMSS
            //MC2
            if (this.Tipo == TipoConsulta.IMSS)
            {
                SMGVDF = this.IM01SMGVDF;
                SMGVZB = this.IM01SMGVZB;
                valor_UMAMC = this.IM01UMA;
            }

            // PGO Salarios Minimos si es calculadora INDEPENDIENTE (No implementado)
            /*  else if (this.Tipo == TipoConsulta.IND)
              {
                  SMGVDF = this.IN02SMGVDF;
                  SMGVZB = this.IN02SMGVZB;
              }*/

            TimeSpan dias1 = pFechaCorte.Subtract(pFechaNacimiento);
            Double pEdadActual = dias1.Days / 365;
            Double pMaximoAniosCotizados = pEdadActual - 14;
            //AJNP
            Double pMaxSemanasCotizadas = pMaximoAniosCotizados * 52;


            Double pSemanasCotizacion = 1250;
            Double pSemanasCotizacionAnios = pSemanasCotizacion / 52;

            DateTime pFechaRetiro = new DateTime(pFechaNacimiento.Year, pFechaNacimiento.Month, pFechaNacimiento.Day).AddYears(Convert.ToInt32(pEdadRetiro));

            TimeSpan tDias2 = pFechaRetiro.Subtract(pFechaCorte);
            Double dias2 = Convert.ToDouble(tDias2.Days);
            Double pAniosCotizacionVidaLaboral = ((pDensidadCotizacion * dias2) / 365) + nSemanasCotizadas;


            //PGO - ODT-07 - Obtener valor para Negativa de pensión
            pNegativaPension = 0;

            if (SCER >= RequisitoSC) { pNegativaPension = 0; } else if (SCER < RequisitoSC) { pNegativaPension = 1; }



            //  if (pAniosCotizacionVidaLaboral > pSemanasCotizacionAnios) { pNegativaPension = 1; }

            MsgBox = String.Empty;


            ///'''VALIDACIONES POR CONJUNTO (Visual)
            //Revisa si la edad de nacimiento no es de una persona más grande de la edad de retiro
            if (pFechaNacimiento < pFechaMaximaNacimiento)
            {
                MsgBox = "De acuerdo a la fecha de nacimiento introducida, tu edad actual es mayor a la edad de retiro deseada. Por lo que no es posible dar una estimación de tu pensión.";
            }

            //Valida que el número de años cotizados no exceda la edad menos 14 años
            //AJNP
            if (pSemanasCotizacion > (pMaxSemanasCotizadas * 52))
            {
                MsgBox = "De acuerdo a la fecha de nacimiento y años cotizados, existe una inconsistencia, ya que el máximo de años cotizados debe ser menor a la edad menos 14 años.";
            }

            //Revisa si el saldo y años cotizados son coherentes (saldo >0 y años cotizados=0)
            if (pSaldoActualAfore > 0 & nSemanasCotizadas == 0)
            {
                MsgBox = "Debido a que el saldo en la cuenta individual es mayor a cero, las semanas cotizadas deben ser mayores a cero.";
            }

            //Revisa si el saldo y años cotizados son coherentes (saldo =0 y años cotizados>0)
            if (pSaldoActualAfore == 0 & nSemanasCotizadas > 0)
            {
                MsgBox = "Debido a que las semanas cotizadas son mayores a cero, el saldo en la cuenta individual debe ser mayor a cero.";
            }

            if (pSalarioBaseCotizacionMensual == 0 || pSalarioBaseCotizacionMensual > Math.Round((30.4 * (25 * valor_UMAMC)), 2) || pSalarioBaseCotizacionMensual < Math.Round((30.4 * SMGVDF), 2))
            {
                MsgBox = "Esta cantidad debe de estar entre 1 salario mínimo y 25 veces el valor de la UMA.";
            }

            if (n <= 1 && MsgBox == "")
            {
                MsgBox = "De acuerdo a tu fecha de nacimiento, estás próximo a cumplir la edad de retiro que seleccionaste. Te recomendamos acudir al IMSS para obtener un dato más preciso de tu pensión.";
            }





            //if (pSalarioBaseCotizacionMensual == 0 || pSalarioBaseCotizacionMensual > Math.Round(((valor_UMAMC * 30) * 25 + 0.5), 2) || pSalarioBaseCotizacionMensual < Math.Round((SMGVDF * 30), 2))
            //{
            //    MsgBox = "Esta cantidad debe de estar entre 1 salario mínimo y 25 veces el valor de la UMA.";
            //}

            //Revisa si es negativa de pensión
            //Fecha de retiro = Fecha de nacimiento + 65 años
            //Años cotizados durante la vida laboral=(Densidad*(Fecha de retiro-Fecha de corte)) + Años cotizados previos a la fecha de corte
            //Si Años cotizados durante la vida laboral < Años para alcanzar pensión (1,250/52 semanas) => negativa de pensión
            //if (pNegativaPension == 1)
            //{
            //    MsgBox = "De acuerdo a la Ley del Seguro Social para acceder a una pensión debes tener un mínimo de 1,250 semanas cotizadas. Por ello, con los datos indicados no lograrás cumplir con las semanas requeridas  a la edad de retiro.";
            //}

            if (MsgBox == String.Empty) { return true; } else { return false; }

        }

        public Double MesesParaRetiro(DateTime FNacimiento, int EdadRetiro, DateTime FCorte)
        {
            DateTime pFechaRetiro = new DateTime(FNacimiento.Year, FNacimiento.Month, FNacimiento.Day).AddYears(Convert.ToInt32(EdadRetiro));

            Double Sal = 0;
            try
            {
                Sal = Fn_n(pFechaRetiro, FCorte);
                Sal = Sal < 1 ? 0 : Sal;
            }
            catch
            {
                Sal = 0;
            }
            return Sal;
        }

        //------Declaración de variables globales
        string[] ArgDesc = new string[8];
        int Tipo_de_Trabajador, Género_Trabajador, i, j, k, Edad_retiro; //'''''CAMBIO''''''
        Double Saldo_final, monto_PME, tasaR, valor_UMA, salario_diario, cuota_social, aportacion_mensual, scer, Requisito_SC, Pension_Garantizada;
        Double Saldo_inicial, Salario_mensual, Rendimiento_anual, Comision_anual, Nivel_Salarial, Bono_ISSSTE, SMGVDF, Aporta_mensual; //'''''CAMBIO''''''
        DateTime Fecha_corte, Fecha_nacimiento, Fecha_redencion, Fecha_retiro;
        Double Porc_Ao, Porc_Av, Porc_AS;

        //PGO ODT-07 - Acumulación por aportaciones 
        Double A1 = 0;
        Double A2 = 0;
        Double A3 = 0;
        Double CSE_t = 0;

        //PGO ODT-07 - α: Porcentaje correspondiente al margen de seguridad y que cuyo valor es equivalente a 2%
        Double margen_seg = 0.02;

        //------Declaración de variables globales


        //Calcula el saldo acumulado, pensión y tasa de reemplazo con la expresión mensual
        //Este modelo tiene como objeto ir acumulando el saldo al final de n meses (desde la edad actual a la fecha de retiro)
        public Double Modelo_mensual_iterativo(Double pSaldoActualAfore, Int32 Tipo_de_Trabajador, Double pGenero, Double pSalarioBaseCotizacionMensual
              , Double pRendimiento, Double pComisionAfore, DateTime pFechaNacimiento, Double Bono_ISSSTE, Int32 Fecha_redencion
              , DateTime pFechaCorte, Double pComisionAportacion, Double Porc_Av, Double Porc_AS, Double pDensidadCotizacion
              , Double pEdadRetiro, Int32 iResultado_funcion, Double aObligatoriaIniMensual, Double tRendimientoMensual, Int32 n1, Int32 n
              , DateTime fCambio1, DateTime fCambio2, Int32 n2, Int32 n2p, Double tRendimientoAnual, Int32 n3, Double pEdad, Double nSemanasCotizadas)
        {
            Double Aporta, Saldo_a, Saldo_b, Saldo_c, varModelo_mensual_iterativo;
            Aporta = 0;
            varModelo_mensual_iterativo = 0;

            Double Valor_URVe = URVe(pGenero, pEdadRetiro);


            DateTime pFechaRetiro = new DateTime(pFechaNacimiento.Year, pFechaNacimiento.Month, pFechaNacimiento.Day).AddYears(Convert.ToInt32(pEdadRetiro));

            //Valor del salario mínimo que será considerado para obtener el nivel salarial del trabajador
            //SMGVDF = 67.29; // RARS

            SMGVDF = 0;

            // PGO Salarios Minimos si es calculadora IMSS
            //MC3
            if (this.Tipo == TipoConsulta.IMSS)
                SMGVDF = this.IM01SMGVDF;

            //Cálculo para conocer el número de salarios mínimos que percibe el trabajador
            Nivel_Salarial = pSalarioBaseCotizacionMensual / (30 * SMGVDF);

            salario_diario = Math.Round((pSalarioBaseCotizacionMensual / 30.4) * 1000000) / 1000000;

            //salario_diario = pSalarioBaseCotizacionMensual / 30.4;

            //Cálculo de la fecha de retiro, en base a la fecha de nacimiento
            //Suponiendo una edad de retiro de 60 o 65 años '''''CAMBIO''''''
            Fecha_retiro = new DateTime(pFechaNacimiento.Year, pFechaNacimiento.Month, pFechaNacimiento.Day).AddYears(Convert.ToInt32(pEdadRetiro)); //'''''CAMBIO''''''


            //El saldo acumulado se compone de tres partes Saldo Inicial (Saldo_a) +  Aportaciones (Saldo_b) +  Bono de Pensión ISSSTE (Saldo_c)
            //Acumulación del Saldo Inicial hasta la fecha de retiro
            Double varfnk = Fn_n(Fecha_retiro, pFechaCorte);
            Int32 AnioRetiro = Fecha_retiro.Year;

            // Frank
            //for (i = 1; i <=varfnk; i++)
            //{
            //    pSaldoActualAfore = pSaldoActualAfore * (1 + Rendimiento_mensual(pRendimiento)) * (1 - Comision_mensual(pComisionAfore));
            //}
            Saldo_a = pSaldoActualAfore;

            //Acumulación de las Aportaciones y su respectiva acumulación de rendimientos y descuento de comisiones
            aportacion_mensual = Aportaciones(Tipo_de_Trabajador, pSalarioBaseCotizacionMensual, pComisionAportacion, Porc_Av, Porc_AS, pDensidadCotizacion);
            Double Cs = Cuota_Social_Mensual(Tipo_de_Trabajador, pDensidadCotizacion, pFechaCorte) / pDensidadCotizacion;

            Double CuotaSocial = Cuota_Social_Mensual(Tipo_de_Trabajador, pDensidadCotizacion, pFechaCorte);



            // Frank
            //for (j = 1; j <= varfnk; j++)
            //{
            //    Aporta = Aporta * (1 + Rendimiento_mensual(pRendimiento)) * (1 - Comision_mensual(pComisionAfore)) + apMensual;
            //}
            Saldo_b = Aporta; //Guarda el saldo acumulado de las aportaciones

            //Acumulación del Bono de Pensión ISSSTE desde la fecha de redención a la fecha de edad de retiro
            varfnk = Fn_k(pFechaRetiro, Fecha_redencion);
            for (k = 1; k <= varfnk; k++)
            {
                Bono_ISSSTE = Bono_ISSSTE * (1 + Rendimiento_mensual(pRendimiento)) * (1 - Comision_mensual(pComisionAfore));
            }
            Saldo_c = Bono_ISSSTE; //Se guarda el saldo acumulado del Bono de Pensión ISSSTE

            //El saldo final se compone de la suma de las tres anteriores componentes
            //Saldo_final = Saldo_a + Saldo_b + Saldo_c;

            Double apMensual = Aportaciones(Tipo_de_Trabajador, pSalarioBaseCotizacionMensual, pComisionAportacion, Porc_Av, Porc_AS, pDensidadCotizacion);

            //PGO - ODT-07 - Obtener valor de Acumulación de Aportaciones durante el periodo 1
            A1 = AcumulacionAP1(aObligatoriaIniMensual, CuotaSocial, tRendimientoMensual, n, n1, fCambio1);


            // PGO - ODT-07 - Variables para obtener A2
            //Aportación base a partir del año 2023 respeto al rango salarial k
            Double Ak = Aportacion_Base(Tipo_de_Trabajador);
            DateTime FechaInicioCSk = new DateTime(2023, 01, 01, 0, 0, 0);


            // PGO - ODT-07 - Determinación de la aportación AK inicial de acuerdo al año de corte
            int AnioFC1 = fCambio1.Year;
            int l = 0;

            if (AnioFC1 > 2022)
            {
                l = AnioFC1 - 2023;
            }
            else if (AnioFC1 == 2022)
            {
                l = 0;
            }

            // PGO - ODT-07 - Gradiente conforme al que crece la aportación base Ak
            Double gk = Gradiente(Tipo_de_Trabajador);

            // PGO - ODT-07 - Monto en pesos de la aportación base mensual
            Double AOk = 0;
            AOk = (Ak + l * gk) * pSalarioBaseCotizacionMensual;

            // PGO - ODT-07 - Gradiente gk por el salario de cotización
            Double G = 0;
            G = gk * pSalarioBaseCotizacionMensual;

            // PGO - ODT-07 - Monto mensual por concepto de cuota social a partir de 2023 (CSk)
            Double CSk = 0;
            DateTime Fcalculo = DateTime.Now;

            CSk = Cuota_Social_CSk(Tipo_de_Trabajador);
            CSk = CSk * 30.4;


            //if (Fcalculo >= FechaInicioCSk)
            //{
            //   CSk = Cuota_Social_CSk(Tipo_de_Trabajador);
            //   CSk = CSk * 30.4;
            //}



            // PGO - ODT-07 - Número de años en los que se dan los incrementos en la  aportación gradual (N2)
            double N2_icrementosAP = 0;
            DateTime fCambio1_aux;
            fCambio1_aux = fCambio1;
            fCambio1_aux = fCambio1_aux.AddDays(1);
            double n2_aux = (double)n2 / 24;


            if (n2_aux > 1)
            {
                N2_icrementosAP = (fCambio2.Year - ((fCambio1_aux.Year)) + 1);
            }
            else if (n2_aux <= 1)
            {
                N2_icrementosAP = 1;
            }




            // PGO - ODT-07 - Nivel del gradiente que se evalúa en el periodo despues del incremento en la aportación gradual
            double x = 0;
            fCambio1 = fCambio1.AddDays(1);
            x = (fCambio2.Year - (fCambio1.Year)) * G;
            //x = (fCambio2.Year - (fCambio1.Year + 1)) * G;

            //PGO - ODT-07 - Acumulación por aportaciones durante el periodo que comprende del 1 de enero de 2023 al 31 de diciembre de 2030
            DateTime FechaValidarA2 = new DateTime(2030, 12, 31, 0, 0, 0);

            if (fCambio2 <= FechaValidarA2)
            {
                A2 = ((((AOk + CSk) * ((Math.Pow(1 + tRendimientoMensual, n2) - 1) / tRendimientoMensual)) +
               (G / tRendimientoAnual) * ((Math.Pow(1 + tRendimientoMensual, 12) - 1) / tRendimientoMensual) * (((Math.Pow(1 + tRendimientoAnual, N2_icrementosAP - 1) - 1) / tRendimientoAnual) - (N2_icrementosAP - 1)) * (Math.Pow(1 + tRendimientoMensual, n2p)) +
               (x * ((Math.Pow(1 + tRendimientoMensual, n2p) - 1) / tRendimientoMensual))) *
               (Math.Pow(1 + tRendimientoMensual, n - (n1 + n2))));

            }
            else if (fCambio2 > FechaValidarA2)
            {
                A2 = 0;
            }


            // PGO - ODT-07 - Variables para obtener A3 - Anualidad del periodo n3, comprendido a partir de enero 2031 y la fecha de retiro


            A3 = (Ak * pSalarioBaseCotizacionMensual + 7 * G + CSk) * ((Math.Pow(1 + tRendimientoMensual, n3) - 1) / tRendimientoMensual);

            //if (Fcalculo > FechaValidarA2)
            //{
            //    A3 = (Ak * pSalarioBaseCotizacionMensual + 7 * G + CSk) * ((Math.Pow(1 + tRendimientoMensual, n3) - 1) / tRendimientoMensual);
            //}
            //else
            //{
            //    A3 = 0;
            //}


            // PGO - ODT-07
            // Cuota Social especial que sólo se entregará durante 2023 a las personas cuyo salario se encuentre dentro del rango salarial h
            Double CSEh = 0;
            if (Fecha_retiro.Year >= 2023)
            {
                CSEh = Cuota_Social_Especial(Tipo_de_Trabajador);
            }


            //PGO - ODT-07 - Fecha de cálculo de la Cuota Especial
            DateTime FechaValidarFCe = new DateTime(2023, 12, 31, 0, 0, 0);
            DateTime FCe = DateTime.Now;
            if (fCambio2 > FechaValidarFCe)
            {
                FCe = FechaValidarFCe;
            }
            else if (fCambio2 <= FechaValidarFCe)
            {
                FCe = fCambio2;
            }


            //PGO - ODT-07 - n’’: Número de meses que el trabajador obtendría la aportación de la cuota social especial, la cual sólo aplica durante el año 2023 
            double npp = 0;

            if (Fcalculo.Year <= 2023 && 2023 <= Fecha_retiro.Year)
            {
                if (fCambio1.Day > 15 && FCe.Day <= 15)
                {
                    npp = (((FCe.Year - fCambio1.Year) - 1) * 12 + 12 - fCambio1.Month + FCe.Month - 1);
                }
                else if (fCambio1.Day <= 15 && FCe.Day > 15)
                {
                    npp = (((FCe.Year - fCambio1.Year) - 1) * 12 + 12 - fCambio1.Month + FCe.Month + 1);
                }

                else
                {
                    npp = (((FCe.Year - fCambio1.Year) - 1) * 12 + 12 - fCambio1.Month + FCe.Month);
                }
            }
            else
            {
                npp = 0;
            }



            //PGO - ODT-07 - al saldo que se podría acumular por las aportaciones de la cuota social especial, las cuales solo se otorgan durante 2023 para trabajadores que coticen con entre 4.01 y 7.09 veces la UMA 
            double CSE = 0;

            CSE = (CSEh * 30.4) * ((Math.Pow(1 + tRendimientoMensual, npp) - 1) / tRendimientoMensual) * (Math.Pow(1 + tRendimientoMensual, n - (npp + n1)));
            //CSE = (CSEh * 30.4) * ((1 + Math.Pow(tRendimientoMensual, npp) - 1) / tRendimientoMensual) * (1 + Math.Pow(tRendimientoMensual, n - (npp + n1)));
            CSE_t = CSE;

            //PGO - ODT-07 - AV-Ahorro voluntario, para el anexo 1, el valor predeterminado es igual a cero
            double AV = 0;

            pRendimiento = Rendimiento_mensual(pRendimiento);
            pComisionAfore = pComisionAfore / 12;
            varfnk = Fn_n(Fecha_retiro, pFechaCorte);



            //PGO - ODT-07 - Saldo acumulado al retiro
            // 	Saldo de ahorro para el retiro en la cuenta individual proyectado al cumplir la Edad de retiro. En caso de que la Edad actual del trabajador sea mayor o igual a la Edad de retiro, Sf = Si 

            if (pEdad > pEdadRetiro)
            {
                Saldo_final = pSaldoActualAfore;
            }
            else
            {
                Saldo_final = pSaldoActualAfore * (Math.Pow(1 + tRendimientoMensual, n)) + pDensidadCotizacion * (A1 + A2 + A3 + CSE + AV);
            }



            //Saldo_final = Saldo_a * Math.Pow(1 + pRendimiento, varfnk) * Math.Pow(1 - pComisionAfore, varfnk) + (
            //              pDensidadCotizacion * (apMensual + Cs) * (
            //                  (Math.Pow(1 + pRendimiento, varfnk) * Math.Pow(1 - pComisionAfore, varfnk) - 1) /
            //                         ((1 + pRendimiento) * (1 - pComisionAfore) - 1)
            //              ));



            //PGO - ODT-07 - Variables para calcular la Estimación de Pensión Mensual

            //PGO - ODT-07 - Semanas Cotizadas indicadas por el trabajador
            double SemCotizadasAntFC = 0;
            SemCotizadasAntFC = Convert.ToInt32(nSemanasCotizadas);


            //PGO - ODT-07 - Semanas Cotizadas despues de la fecha de calculo
            double SemCotizadasDesFC = 0;
            TimeSpan DiferenciaSemCotizadas = Fecha_retiro - Fcalculo;
            SemCotizadasDesFC = (DiferenciaSemCotizadas.Days) + 1;

            SemCotizadasDesFC = Math.Round(SemCotizadasDesFC / 7);

            //      double diasDiff = ((Fecha_retiro - Fcalculo).Days);




            //PGO - ODT-07 - semanas cotizadas a la Fecha de Retiro 
            double SCER = 0;
            double SCER_AUX = 0;

            SCER_AUX = SemCotizadasAntFC + (SemCotizadasDesFC * pDensidadCotizacion);
            SCER = Math.Round(SCER_AUX);
            scer = SCER;


            // PGO - ODT-07 - Obtener rango de semanas cotizadas (tabla)
            double RangoSemanasCotizadas = Rango_Semanas_Cotizadas(AnioRetiro, SCER);


            double RequisitoSC = SemanasNegPension(AnioRetiro);
            Requisito_SC = Math.Round(RequisitoSC);



            //PGO - ODT-07 - Obtención del Valor de la Pensión Garantizada
            int RangoPG = 0;
            if (SMGVDF <= salario_diario && salario_diario < (2 * valor_UMA))
            {
                RangoPG = 1;
            }
            else if ((2 * valor_UMA) <= salario_diario && salario_diario < (3 * valor_UMA))
            {
                RangoPG = 2;
            }
            else if ((3 * valor_UMA) <= salario_diario && salario_diario < (4 * valor_UMA))
            {
                RangoPG = 3;
            }
            else if ((4 * valor_UMA) <= salario_diario && salario_diario < (5 * valor_UMA))
            {
                RangoPG = 4;
            }
            else if (salario_diario >= (5 * valor_UMA))
            {
                RangoPG = 5;
            }

            //PGO ODT-07 - Obtener valor del Monto de Pensión Garantizada
            double PGarantizada = 0;
            if (RangoSemanasCotizadas > 0)
            {
                PGarantizada = Monto_PGarantizada(RangoSemanasCotizadas, pEdadRetiro, RangoPG);
                Pension_Garantizada = PGarantizada;
            }




            //PGO - ODT-07 - Estimación de Pensión Mensual
            if (((Saldo_final) / ((1 + margen_seg) * 12 * Valor_URVe)) > PGarantizada)
            {
                monto_PME = Saldo_final / ((1 + margen_seg) * 12 * Valor_URVe);
            }
            else if (((Saldo_final) / ((1 + margen_seg) * 12 * Valor_URVe)) <= PGarantizada)
            {
                monto_PME = PGarantizada;
            }


            //PGO - ODT-07 - Tasa de reemplazo
            tasaR = monto_PME / pSalarioBaseCotizacionMensual;


            //monto_PME = Saldo_final / (URVe(pGenero, pEdadRetiro) * 12);
            //tasaR = (Saldo_final / (URVe(pGenero, pEdadRetiro) * 12)) / pSalarioBaseCotizacionMensual;

            switch (iResultado_funcion)
            {
                case 1: //Si Resultado_función = 1 entonces la función arrojará el saldo acumulado
                    varModelo_mensual_iterativo = Saldo_final;
                    break;
                case 2: //Si Resultado_función = 2. entonces la función arrojará monto de la pensíon mensual estimada
                    varModelo_mensual_iterativo = monto_PME;
                    break;
                case 3: //Si Resultado_función = 3 entonces la función arrojará la tasa de reemplazo
                    varModelo_mensual_iterativo = tasaR;
                    break;
            }

            return varModelo_mensual_iterativo;
        }

        public double GetSaldoFinal(Double pSaldoActualAfore,
                                                Int32 Tipo_de_Trabajador,
                                                Double pGenero,
                                                Double pSalarioBaseCotizacionMensual,
                                                Double pRendimiento,
                                                Double pComisionAfore,
                                                DateTime pFechaNacimiento,
                                                Double Bono_ISSSTE,
                                                Int32 Fecha_redencion,
                                                DateTime pFechaCorte,
                                                Double pComisionAportacion,
                                                Double Porc_Av,
                                                Double Porc_AS,
                                                Double pDensidadCotizacion,
                                                Double pEdadRetiro,
                                                Int32 iResultado_funcion,
                                                Double aObligatoriaIniMensual,
                                                Double tRendimientoMensual,
                                                Int32 n1,
                                                Int32 n,
                                                DateTime fCambio1,
                                                DateTime fCambio2,
                                                Int32 n2,
                                                Int32 n2p,
                                                Double tRendimientoAnual,
                                                Int32 n3,
                                                Double pEdad)




        //Double aObligatoriaIniMensual, Double tRendimientoMensual, Int32 n1, Int32 n
        //  , DateTime fCambio1, DateTime fCambio2)
        {
            Double Aporta, Saldo_a, Saldo_b, Saldo_c, varModelo_mensual_iterativo;
            Aporta = 0;
            varModelo_mensual_iterativo = 0;

            DateTime pFechaRetiro = new DateTime(pFechaNacimiento.Year, pFechaNacimiento.Month, pFechaNacimiento.Day).AddYears(Convert.ToInt32(pEdadRetiro));

            SMGVDF = 0;
            //MC4

            if (this.Tipo == TipoConsulta.IMSS)
                //SMGVDF = this.IM01SMGVDF;

                SMGVDF = getParametros();


            Nivel_Salarial = pSalarioBaseCotizacionMensual / (30 * SMGVDF);

            salario_diario = Math.Round((pSalarioBaseCotizacionMensual / 30.4) * 1000000) / 1000000;

            //salario_diario = pSalarioBaseCotizacionMensual / 30.4;

            Fecha_retiro = new DateTime(pFechaNacimiento.Year, pFechaNacimiento.Month, pFechaNacimiento.Day).AddYears(Convert.ToInt32(pEdadRetiro)); //'''''CAMBIO''''''

            int meses = (int)Fn_n(Fecha_retiro, pFechaCorte);


            //for (i = 1; i <= meses; i++)
            //{
            //    pSaldoActualAfore = pSaldoActualAfore * (1 + Rendimiento_mensual(pRendimiento)) * (1 - Comision_mensual(pComisionAfore));
            //}
            Saldo_a = pSaldoActualAfore;

            Double apMensual = Aportaciones(Tipo_de_Trabajador, pSalarioBaseCotizacionMensual, pComisionAportacion, Porc_Av, Porc_AS, pDensidadCotizacion);
            Double Cs = Cuota_Social_Mensual(Tipo_de_Trabajador, pDensidadCotizacion, pFechaCorte) / pDensidadCotizacion;
            //for (j = 1; j <= Fn_n(Fecha_retiro, pFechaCorte); j++)
            //{
            //   Aporta_mensual = Aportaciones(Tipo_de_Trabajador, pSalarioBaseCotizacionMensual, pComisionAportacion, Porc_Av, Porc_AS, pDensidadCotizacion) + Cuota_Social_Mensual(Tipo_de_Trabajador, pDensidadCotizacion, pFechaCorte);
            //    Aporta = Aporta * (1 + Rendimiento_mensual(pRendimiento)) * (1 - Comision_mensual(pComisionAfore)) + Aporta_mensual;
            //}
            Saldo_b = Aporta; //Guarda el saldo acumulado de las aportaciones

            //Double varfnk = Fn_k(pFechaRetiro, Fecha_redencion);
            //for (k = 1; k <= varfnk; k++)
            //{
            //    Bono_ISSSTE = Bono_ISSSTE * (1 + Rendimiento_mensual(pRendimiento)) * (1 - Comision_mensual(pComisionAfore));
            //}
            Saldo_c = Bono_ISSSTE; //Se guarda el saldo acumulado del Bono de Pensión ISSSTE


            Double CuotaSocial = Cuota_Social_Mensual(Tipo_de_Trabajador, pDensidadCotizacion, pFechaCorte);
            //PGO - ODT-07 - Obtener valor de Acumulación de Aportaciones durante el periodo 1
            A1 = AcumulacionAP1(aObligatoriaIniMensual, CuotaSocial, tRendimientoMensual, n, n1, fCambio1);

            //PGO - ODT-07 - Variables para obtener A2

            // PGO - ODT-07 - Aportación base a partir del año 2023 respeto al rango salarial k
            Double Ak = Aportacion_Base(Tipo_de_Trabajador);
            DateTime FechaInicioCSk = new DateTime(2023, 01, 01, 0, 0, 0);


            // PGO - ODT-07 - Determinación de la aportación AK inicial de acuerdo al año de corte
            int AnioFC1 = fCambio1.Year;
            int l = 0;

            if (AnioFC1 > 2022)
            {
                l = AnioFC1 - 2023;
            }
            else if (AnioFC1 == 2022)
            {
                l = 0;
            }

            // PGO - ODT-07 - Gradiente conforme al que crece la aportación base Ak
            Double gk = Gradiente(Tipo_de_Trabajador);

            // Monto en pesos de la aportación base mensual
            Double AOk = 0;
            AOk = (Ak + l * gk) * pSalarioBaseCotizacionMensual;

            // PGO - ODT-07 - Gradiente gk por el salario de cotización
            Double G = 0;
            G = gk * pSalarioBaseCotizacionMensual;


            // PGO - ODT-07 - Monto mensual por concepto de cuota social a partir de 2023 (CSk)
            Double CSk = 0;
            DateTime Fcalculo = DateTime.Now;

            CSk = Cuota_Social_CSk(Tipo_de_Trabajador);
            CSk = CSk * 30.4;

            //if (Fcalculo >= FechaInicioCSk)
            //{
            //    CSk = Cuota_Social_CSk(Tipo_de_Trabajador);
            //    CSk = CSk * 30.4;
            //}


            // PGO - ODT-07 - Número de años en los que se dan los incrementos en la  aportación gradual (N2)
            double N2_icrementosAP = 0;
            DateTime fCambio1_aux;
            fCambio1_aux = fCambio1;
            fCambio1_aux = fCambio1_aux.AddDays(1);
            double n2_aux = (double)n2 / 24;


            if (n2_aux > 1)
            {
                N2_icrementosAP = (fCambio2.Year - ((fCambio1_aux.Year)) + 1);
            }
            else if (n2_aux <= 1)
            {
                N2_icrementosAP = 1;
            }



            // PGO - ODT-07 - Nivel del gradiente que se evalúa en el periodo despues del incremento en la aportación gradual
            double x = 0;

            fCambio1 = fCambio1.AddDays(1);

            x = (fCambio2.Year - (fCambio1.Year)) * G;
            //x = (fCambio2.Year - (fCambio1.Year + 1)) * G;



            //PGO - ODT-07 - Acumulación por aportaciones durante el periodo que comprende del 1 de enero de 2023 al 31 de diciembre de 2030
            DateTime FechaValidarA2 = new DateTime(2030, 12, 31, 0, 0, 0);

            if (fCambio2 <= FechaValidarA2)
            {
                A2 = ((((AOk + CSk) * ((Math.Pow(1 + tRendimientoMensual, n2) - 1) / tRendimientoMensual)) +
               (G / tRendimientoAnual) * ((Math.Pow(1 + tRendimientoMensual, 12) - 1) / tRendimientoMensual) * (((Math.Pow(1 + tRendimientoAnual, N2_icrementosAP - 1) - 1) / tRendimientoAnual) - (N2_icrementosAP - 1)) * (Math.Pow(1 + tRendimientoMensual, n2p)) +
               (x * ((Math.Pow(1 + tRendimientoMensual, n2p) - 1) / tRendimientoMensual))) *
               (Math.Pow(1 + tRendimientoMensual, n - (n1 + n2))));
            }
            else if (fCambio2 > FechaValidarA2)
            {
                A2 = 0;
            }


            // PGO - ODT-07 - Variables para obtener A3 - Anualidad del periodo n3, comprendido a partir de enero 2031 y la fecha de retiro

            A3 = (Ak * pSalarioBaseCotizacionMensual + 7 * G + CSk) * ((Math.Pow(1 + tRendimientoMensual, n3) - 1) / tRendimientoMensual);

            //if (Fcalculo > FechaValidarA2)
            //{
            //    A3 = (Ak * pSalarioBaseCotizacionMensual + 7 * G + CSk) * ((1 + Math.Pow(tRendimientoMensual, n3) - 1) / tRendimientoMensual);
            //}
            //else
            //{
            //    A3 = 0;
            //}


            // PGO - ODT-07
            // Cuota Social especial que sólo se entregará durante 2023 a las personas cuyo salario se encuentre dentro del rango salarial h
            Double CSEh = 0;
            if (Fecha_retiro.Year >= 2023)
            {
                CSEh = Cuota_Social_Especial(Tipo_de_Trabajador);
            }


            //PGO - ODT-07 - Fecha de cálculo de la Cuota Especial
            DateTime FechaValidarFCe = new DateTime(2023, 12, 31, 0, 0, 0);
            DateTime FCe = DateTime.Now;
            if (fCambio2 > FechaValidarFCe)
            {
                FCe = FechaValidarFCe;
            }
            else if (fCambio2 <= FechaValidarFCe)
            {
                FCe = fCambio2;
            }


            //PGO - ODT-07 - n’’: Número de meses que el trabajador obtendría la aportación de la cuota social especial, la cual sólo aplica durante el año 2023 
            double npp = 0;

            if (Fcalculo.Year <= 2023 && 2023 <= Fecha_retiro.Year)
            {
                if (fCambio1.Day > 15 && FCe.Day <= 15)
                {
                    npp = (((FCe.Year - fCambio1.Year) - 1) * 12 + 12 - fCambio1.Month + FCe.Month - 1);
                }
                else if (fCambio1.Day <= 15 && FCe.Day > 15)
                {
                    npp = (((FCe.Year - fCambio1.Year) - 1) * 12 + 12 - fCambio1.Month + FCe.Month + 1);
                }

                else
                {
                    npp = (((FCe.Year - fCambio1.Year) - 1) * 12 + 12 - fCambio1.Month + FCe.Month);
                }
            }
            else
            {
                npp = 0;
            }


            //PGO - ODT-07 - al saldo que se podría acumular por las aportaciones de la cuota social especial, las cuales solo se otorgan durante 2023 para trabajadores que coticen con entre 4.01 y 7.09 veces la UMA 
            double CSE = 0;

            CSE = (CSEh * 30.4) * ((Math.Pow(1 + tRendimientoMensual, npp) - 1) / tRendimientoMensual) * (Math.Pow(1 + tRendimientoMensual, n - (npp + n1)));
            //CSE = (CSEh * 30.4) * ((1 + Math.Pow(tRendimientoMensual, npp) - 1) / tRendimientoMensual) * (1 + Math.Pow(tRendimientoMensual, n - (npp + n1)));
            CSE_t = CSE;

            //PGO - ODT-07 - AV-Ahorro voluntario, para el anexo 1, el valor predeterminado es igual a cero
            double AV = 0;

            pRendimiento = Rendimiento_mensual(pRendimiento);
            pComisionAfore = pComisionAfore / 12;



            //PGO - ODT-07 - Saldo acumulado al retiro
            // 	Saldo de ahorro para el retiro en la cuenta individual proyectado al cumplir la Edad de retiro. En caso de que la Edad actual del trabajador sea mayor o igual a la Edad de retiro, Sf = Si 

            Double sdo_final = 0;
            if (pEdad > pEdadRetiro)
            {
                sdo_final = pSaldoActualAfore;
            }
            else
            {
                sdo_final = pSaldoActualAfore * (Math.Pow(1 + tRendimientoMensual, n)) + pDensidadCotizacion * (A1 + A2 + A3 + CSE + AV);
            }





            /* Double sdo_final = Saldo_a * Math.Pow(1 + pRendimiento, meses) * Math.Pow(1 - pComisionAfore, meses) + (
                          pDensidadCotizacion * (apMensual + Cs) * (
                              (Math.Pow(1 + pRendimiento, meses) * Math.Pow(1 - pComisionAfore, meses) - 1) /
                                     ((1 + pRendimiento) * (1 - pComisionAfore) - 1)
                          ));*/

            SaldoFinal = sdo_final;
            return sdo_final;
        }

        public List<object> GetDataSetGraficaUno(Double pSaldoActualAfore,
                                                Int32 Tipo_de_Trabajador,
                                                Double pGenero,
                                                Double pSalarioBaseCotizacionMensual,
                                                Double pRendimiento,
                                                Double pComisionAfore,
                                                DateTime pFechaNacimiento,
                                                Double Bono_ISSSTE,
                                                Int32 Fecha_redencion,
                                                DateTime pFechaCorte,
                                                Double pComisionAportacion,
                                                Double Porc_Av,
                                                Double Porc_AS,
                                                Double pDensidadCotizacion,
                                                Double pEdadRetiro,
                                                Int32 iResultado_funcion,
                                                Double aObligatoriaIniMensual,
                                                Double tRendimientoMensual,
                                                Int32 n1,
                                                Int32 nMesesRetiro,
                                                DateTime fCambio1,
                                                DateTime fCambio2,
                                                Int32 n2,
                                                Int32 n2p,
                                                Double tRendimientoAnual,
                                                Int32 n3,
                                                Double pEdad)


        //    PME = oRNCalcularora.Modelo_mensual_iterativo(pSaldoActualAfore, 0, pGenero, pSalarioBaseCotizacionMensual, pRendimiento, pComisionAfore, pFechaNacimiento, 0, 0, pFechaCorte, pAportacion, 0, 0, pDensidadCotizacion, pEdadRetiro, 2, 
        //aObligatoriaIniMensual, tRendimientoMensual, n1, n, fCambio1, fCambio2, n2, N2_icrementosAP, n2p, tRendimientoAnual);

        {
            Double Aporta, Saldo_a, Saldo_b, Saldo_c, varModelo_mensual_iterativo;
            Aporta = 0;
            varModelo_mensual_iterativo = 0;

            DateTime pFechaRetiro = new DateTime(pFechaNacimiento.Year, pFechaNacimiento.Month, pFechaNacimiento.Day).AddYears(Convert.ToInt32(pEdadRetiro));

            SMGVDF = 0;
            //MC4

            if (this.Tipo == TipoConsulta.IMSS)
                //SMGVDF = this.IM01SMGVDF;

                SMGVDF = getParametros();


            Nivel_Salarial = pSalarioBaseCotizacionMensual / (30 * SMGVDF);


            salario_diario = Math.Round((pSalarioBaseCotizacionMensual / 30.4) * 1000000) / 1000000;
            //salario_diario = pSalarioBaseCotizacionMensual / 30.4;

            Fecha_retiro = new DateTime(pFechaNacimiento.Year, pFechaNacimiento.Month, pFechaNacimiento.Day).AddYears(Convert.ToInt32(pEdadRetiro)); //'''''CAMBIO''''''

            int meses = (int)Fn_n(Fecha_retiro, pFechaCorte);
            //for (i = 1; i <= meses; i++)
            //{
            //    pSaldoActualAfore = pSaldoActualAfore * (1 + Rendimiento_mensual(pRendimiento)) * (1 - Comision_mensual(pComisionAfore));
            //}
            Saldo_a = pSaldoActualAfore;

            Double apMensual = Aportaciones(Tipo_de_Trabajador, pSalarioBaseCotizacionMensual, pComisionAportacion, Porc_Av, Porc_AS, pDensidadCotizacion);
            Double Cs = Cuota_Social_Mensual(Tipo_de_Trabajador, pDensidadCotizacion, pFechaCorte) / pDensidadCotizacion;

            Double CuotaSocial = Cuota_Social_Mensual(Tipo_de_Trabajador, pDensidadCotizacion, pFechaCorte);
            //PGO - ODT-07 - Obtener valor de Acumulación de Aportaciones durante el periodo 1
            A1 = AcumulacionAP1(aObligatoriaIniMensual, CuotaSocial, tRendimientoMensual, nMesesRetiro, n1, fCambio1);


            //PGO - ODT-07 - Variables para obtener A2

            //Aportación base a partir del año 2023 respeto al rango salarial k
            Double Ak = Aportacion_Base(Tipo_de_Trabajador);
            DateTime FechaInicioCSk = new DateTime(2023, 01, 01, 0, 0, 0);


            // PGO - ODT-07 - Determinación de la aportación AK inicial de acuerdo al año de corte
            int AnioFC1 = fCambio1.Year;
            int l = 0;

            if (AnioFC1 > 2022)
            {
                l = AnioFC1 - 2023;
            }
            else if (AnioFC1 == 2022)
            {
                l = 0;
            }

            // PGO - ODT-07 - Gradiente conforme al que crece la aportación base Ak
            Double gk = Gradiente(Tipo_de_Trabajador);


            // PGO - ODT-07 - Monto en pesos de la aportación base mensual
            Double AOk = 0;
            AOk = (Ak + l * gk) * pSalarioBaseCotizacionMensual;

            // PGO - ODT-07 - Gradiente gk por el salario de cotización
            Double G = 0;
            G = gk * pSalarioBaseCotizacionMensual;


            // PGO - ODT-07 - Monto mensual por concepto de cuota social a partir de 2023 (CSk)
            Double CSk = 0;
            DateTime Fcalculo = DateTime.Now;

            CSk = Cuota_Social_CSk(Tipo_de_Trabajador);
            CSk = CSk * 30.4;

            //if (Fcalculo >= FechaInicioCSk)
            //{
            //    CSk = Cuota_Social_CSk(Tipo_de_Trabajador);
            //    CSk = CSk * 30.4;
            //}


            // PGO - ODT-07 - Número de años en los que se dan los incrementos en la  aportación gradual (N2)
            double N2_icrementosAP = 0;
            DateTime fCambio1_aux;
            DateTime fCambio1p;
            fCambio1p = fCambio1;
            fCambio1_aux = fCambio1;
            fCambio1_aux = fCambio1_aux.AddDays(1);
            double n2_aux = (double)n2 / 24;


            if (n2_aux > 1)
            {
                N2_icrementosAP = (fCambio2.Year - ((fCambio1_aux.Year)) + 1);
            }
            else if (n2_aux <= 1)
            {
                N2_icrementosAP = 1;
            }

            // PGO - ODT-07 - Nivel del gradiente que se evalúa en el periodo despues del incremento en la aportación gradual
            double x = 0;

            fCambio1 = fCambio1.AddDays(1);

            x = (fCambio2.Year - (fCambio1.Year)) * G;
            //x = (fCambio2.Year - (fCambio1.Year + 1)) * G;


            //PGO - ODT-07 - Acumulación por aportaciones durante el periodo que comprende del 1 de enero de 2023 al 31 de diciembre de 2030
            DateTime FechaValidarA2 = new DateTime(2030, 12, 31, 0, 0, 0);

            if (fCambio2 <= FechaValidarA2)
            {

                A2 = ((((AOk + CSk) * ((Math.Pow(1 + tRendimientoMensual, n2) - 1) / tRendimientoMensual)) +
              (G / tRendimientoAnual) * ((Math.Pow(1 + tRendimientoMensual, 12) - 1) / tRendimientoMensual) * (((Math.Pow(1 + tRendimientoAnual, N2_icrementosAP - 1) - 1) / tRendimientoAnual) - (N2_icrementosAP - 1)) * (Math.Pow(1 + tRendimientoMensual, n2p)) +
              (x * ((Math.Pow(1 + tRendimientoMensual, n2p) - 1) / tRendimientoMensual))) *
              (Math.Pow(1 + tRendimientoMensual, nMesesRetiro - (n1 + n2))));


            }
            else if (fCambio2 > FechaValidarA2)
            {
                A2 = 0;
            }


            // PGO - ODT-07 - Variables para obtener A3 - Anualidad del periodo n3, comprendido a partir de enero 2031 y hasta la fecha de retiro

            A3 = (Ak * pSalarioBaseCotizacionMensual + 7 * G + CSk) * ((Math.Pow(1 + tRendimientoMensual, n3) - 1) / tRendimientoMensual);

            //if (Fcalculo > FechaValidarA2)
            //{
            //    A3 = (Ak * pSalarioBaseCotizacionMensual + 7 * G + CSk) * ((1 + Math.Pow(tRendimientoMensual, n3) - 1) / tRendimientoMensual);
            //}
            //else
            //{
            //    A3 = 0;
            //}

            // PGO - ODT-07
            // Cuota Social especial que sólo se entregará durante 2023 a las personas cuyo salario se encuentre dentro del rango salarial h
            Double CSEh = 0;
            if (Fecha_retiro.Year >= 2023)
            {
                CSEh = Cuota_Social_Especial(Tipo_de_Trabajador);
            }

            //PGO - ODT-07 - Fecha de cálculo de la Cuota Especial
            DateTime FechaValidarFCe = new DateTime(2023, 12, 31, 0, 0, 0);
            DateTime FCe = DateTime.Now;
            if (fCambio2 > FechaValidarFCe)
            {
                FCe = FechaValidarFCe;
            }
            else if (fCambio2 <= FechaValidarFCe)
            {
                FCe = fCambio2;
            }


            //PGO - ODT-07 - n’’: Número de meses que el trabajador obtendría la aportación de la cuota social especial, la cual sólo aplica durante el año 2023 
            double npp = 0;

            if (Fcalculo.Year <= 2023 && 2023 <= Fecha_retiro.Year)
            {
                if (fCambio1.Day > 15 && FCe.Day <= 15)
                {
                    npp = (((FCe.Year - fCambio1.Year) - 1) * 12 + 12 - fCambio1.Month + FCe.Month - 1);
                }
                else if (fCambio1.Day <= 15 && FCe.Day > 15)
                {
                    npp = (((FCe.Year - fCambio1.Year) - 1) * 12 + 12 - fCambio1.Month + FCe.Month + 1);
                }

                else
                {
                    npp = (((FCe.Year - fCambio1.Year) - 1) * 12 + 12 - fCambio1.Month + FCe.Month);
                }
            }
            else
            {
                npp = 0;
            }



            //PGO - ODT-07 - al saldo que se podría acumular por las aportaciones de la cuota social especial, las cuales solo se otorgan durante 2023 para trabajadores que coticen con entre 4.01 y 7.09 veces la UMA 
            double CSE = 0;

            CSE = (CSEh * 30.4) * ((Math.Pow(1 + tRendimientoMensual, npp) - 1) / tRendimientoMensual) * (Math.Pow(1 + tRendimientoMensual, nMesesRetiro - (npp + n1)));
            //CSE = (CSEh * 30.4) * ((1 + Math.Pow(tRendimientoMensual, npp) - 1) / tRendimientoMensual) * (1 + Math.Pow(tRendimientoMensual, nMesesRetiro - (npp + n1)));
            CSE_t = CSE;

            //PGO - ODT-07 - AV-Ahorro voluntario, para el anexo 1, el valor predeterminado es igual a cero
            double AV = 0;


            //for (j = 1; j <= Fn_n(Fecha_retiro, pFechaCorte); j++)
            //{
            //   Aporta_mensual = Aportaciones(Tipo_de_Trabajador, pSalarioBaseCotizacionMensual, pComisionAportacion, Porc_Av, Porc_AS, pDensidadCotizacion) + Cuota_Social_Mensual(Tipo_de_Trabajador, pDensidadCotizacion, pFechaCorte);
            //    Aporta = Aporta * (1 + Rendimiento_mensual(pRendimiento)) * (1 - Comision_mensual(pComisionAfore)) + Aporta_mensual;
            //}
            Saldo_b = Aporta; //Guarda el saldo acumulado de las aportaciones

            //Double varfnk = Fn_k(pFechaRetiro, Fecha_redencion);
            //for (k = 1; k <= varfnk; k++)
            //{
            //    Bono_ISSSTE = Bono_ISSSTE * (1 + Rendimiento_mensual(pRendimiento)) * (1 - Comision_mensual(pComisionAfore));
            //}
            Saldo_c = Bono_ISSSTE; //Se guarda el saldo acumulado del Bono de Pensión ISSSTE

            pRendimiento = Rendimiento_mensual(pRendimiento);
            pComisionAfore = pComisionAfore / 12;

            //PGO - ODT-07 - Saldo acumulado al retiro
            // 	Saldo de ahorro para el retiro en la cuenta individual proyectado al cumplir la Edad de retiro. En caso de que la Edad actual del trabajador sea mayor o igual a la Edad de retiro, Sf = Si 

            Double sdo_final = 0;
            if (pEdad > pEdadRetiro)
            {
                sdo_final = pSaldoActualAfore;
            }
            else
            {
                sdo_final = pSaldoActualAfore * (Math.Pow(1 + tRendimientoMensual, nMesesRetiro)) + pDensidadCotizacion * (A1 + A2 + A3 + CSE + AV);
            }



            /*  Double sdo_final = Saldo_a * Math.Pow(1 + pRendimiento, meses) * Math.Pow(1 - pComisionAfore, meses) + (
                           pDensidadCotizacion * (apMensual + Cs) * (
                               (Math.Pow(1 + pRendimiento, meses) * Math.Pow(1 - pComisionAfore, meses) - 1) /
                                      ((1 + pRendimiento) * (1 - pComisionAfore) - 1)
                           ));*/

            //HttpContext.Current.Session["sdoAcumRetiro"] = sdo_final;

            Saldo_final = sdo_final;
            double n = meses;
            //int diaft = 0;
            int aa = 0;
            int anioft = 0;
            int mesft = 0;
            int diafct = 0;
            int aniofct = 0;
            int mmfct = 0;
            DateTime fCambio1pp;

            List<int> bloque0 = new List<int>();
            List<Double> bloqueR = new List<Double>();
            List<Double> bloqueA = new List<Double>();
            List<string> labels = new List<string>();
            List<object> iData = new List<object>();
            int[] tFC = new int[13];
            int[] mestFC = new int[13];
            string[] mestFCT = new string[13];
            string[] FtT = new string[13];
            int[] n2t = new int[13];
            string[] n2tT = new string[13];
            int[] Np2t = new int[13];
            string[] Np2tT = new string[13];
            string[] fcetT = new string[13];
            int[] Nptp = new int[13];
            string[] NptpT = new string[13];
            double[] XT = new double[13];
            string[] XTT = new string[13];
            double[] AT = new double[13];
            double[] SAt = new double[13];
            double[] RT = new double[13];
            int[] diaft = new int[13];

            //Fecha de cambio1 mas un dia
            fCambio1pp = fCambio1.AddDays(1);

            //Obtenemos an1
            double an1 = ((aObligatoriaIniMensual + CuotaSocial) * n1);
            //Obtenemos an2
            double an2 = (AOk + CSk) * n2 + 12 * (((N2_icrementosAP - 2) * (N2_icrementosAP - 1)) / 2) * G + (x * n2p + ((CSEh * 30.4) * npp));
            //Obtenemos an3
            double an3 = ((Ak + 7 * gk) * pSalarioBaseCotizacionMensual + CSk) * n3;

            if (n >= 13)
            {
                bloque0 = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

                for (int i = 0; i < bloque0.Count; i++)
                {
                    //1. -Calculamos t
                    //tFC[i] = Convert.ToInt16((double)(nMesesRetiro / 12) * bloque0[i]);
                    double var1 = (Convert.ToDouble(nMesesRetiro) / 12);
                    var1 = var1 * double.Parse(bloque0[i].ToString());
                    var1 = Math.Round(var1, 0);

                    tFC[i] = int.Parse(var1.ToString());

                    //2.- Calculamos mest 
                    if (n2 == 0)
                    {
                        mestFCT[i] = "";
                    }
                    else if (n1 < tFC[i] && tFC[i] <= (n1 + n2))
                    {
                        mestFC[i] = Convert.ToInt16(tFC[i] - n1);
                    }
                    else if (n1 >= tFC[i] || tFC[i] >= (n1 + n2))
                    {
                        mestFCT[i] = "";
                    }

                    //3.- Calculamos valor de la Fecha en t (Ft)   
                    // Regresar el resultado en dd/mm/aaaa
                    DateTime fechadeldia = DateTime.Today.AddMonths(Convert.ToInt32(tFC[i]));
                    //diaft = fechadeldia.Day;
                    if (mestFCT[i] == "")
                    {
                        FtT[i] = "";
                    }
                    else if (mestFC[i] < n2)
                    {
                        FtT[i] = Convert.ToString(fechadeldia.ToShortDateString().PadLeft(2, '0'));
                        //datetimeft = Convert.ToDateTime(FtT[i]);
                        diaft[i] = fechadeldia.Day;
                    }
                    else if (mestFC[i] == n2)
                    {
                        FtT[i] = fCambio2.ToShortDateString();
                        diaft[i] = fCambio2.Day;
                    }

                    //4.- Calculamos número de años N2t         
                    float n224 = n2;
                    float n224p = 24;
                    float n224pp = n224 / n224p;
                    // n224pp se realiza para comparar valor decimal con numero entero 1

                    if (mestFCT[i] == "")
                    {
                        n2tT[i] = "";
                    }
                    else if (n224pp > 1)
                    {
                        anioft = Convert.ToInt16(FtT[i].Substring(6, 4));
                        n2t[i] = (anioft - fCambio1pp.Year) + 1;
                    }
                    else if (n224pp <= 1)
                    {
                        n2t[i] = 1;
                    }

                    //5.- Calculamos número de periodos Np2t
                    if (mestFCT[i] == "")
                    {
                        Np2tT[i] = "";
                    }

                    else if (diaft[i] > 15)
                    {
                        mesft = Convert.ToInt16(FtT[i].Substring(3, 2));
                        Np2t[i] = mesft;
                    }
                    else if (diaft[i] < 15)
                    {
                        mesft = Convert.ToInt16(FtT[i].Substring(3, 2));
                        Np2t[i] = mesft - 1;
                    }

                    //6.- Calculamos valoe de la fecha FCEt
                    DateTime fechaac = Convert.ToDateTime("31/12/2023");
                    if (mestFCT[i] == "")
                    {
                        fcetT[i] = "";
                    }
                    else if (fechadeldia > fechaac)
                    {
                        fcetT[i] = "31/12/2023";
                    }
                    else if (fechadeldia <= fechaac)
                    {
                        fcetT[i] = fechadeldia.ToString();
                    }

                    //7.-numero de meses en que trabajador recibe su cuota nptp7
                    if (mestFCT[i] == "")
                    {
                        NptpT[i] = "";
                    }
                    else
                    {
                        diafct = Convert.ToInt16(fcetT[i].Substring(0, 2));
                        aniofct = Convert.ToInt16(fcetT[i].Substring(6, 4));
                        mmfct = Convert.ToInt16(fcetT[i].Substring(3, 2));
                        //Se debe cumplir que  Año(FC)≤2023≤Año(Ft), en otro caso, n't'=0
                        if (AnioFC1 <= 2023 && 2023 <= anioft)
                        {
                            if (fCambio1p.Day > 15 && diafct <= 15)
                            {
                                Nptp[i] = ((aniofct - AnioFC1) - 1) * 12 + 12 - fCambio1p.Month + mmfct - 1;
                            }
                            else if (fCambio1p.Day <= 15 && diafct > 15)
                            {
                                Nptp[i] = ((aniofct - AnioFC1) - 1) * 12 + 12 - fCambio1p.Month + mmfct + 1;
                            }
                            else
                            {
                                Nptp[i] = ((aniofct - AnioFC1) - 1) * 12 + 12 - fCambio1p.Month + mmfct;
                            }
                        }
                        else
                        {
                            Nptp[i] = 0;

                        }
                    }

                    //8.-nivel del gradiente para cada valor de t 
                    if (mestFCT[i] == "")
                    {
                        XTT[i] = "";
                    }
                    else
                    {
                        anioft = Convert.ToInt16(FtT[i].Substring(6, 4));
                        XT[i] = (anioft - fCambio1pp.Year) * G;
                    }

                    //9.-Para estimar los valores de la columna Aportaciones en t, 
                    double primeraformula = AOk + (CSk * n2);
                    double segundaformula = 12 * ((((N2_icrementosAP - 2) * (N2_icrementosAP - 1)) / 2) * G);
                    double terceraformula = x * n2p;

                    if (bloque0[i] == 0)
                    {
                        AT[i] = pSaldoActualAfore;
                    }
                    else if (tFC[i] <= n1)
                    {
                        AT[i] = (pSaldoActualAfore + pDensidadCotizacion * ((aObligatoriaIniMensual + CuotaSocial) * tFC[i]));
                    }
                    else if (n1 < tFC[i] && tFC[i] <= (n1 + n2))
                    {
                        double simasd = ((n2t[i] - 2) * (n2t[i] - 1)) / 2;
                        //Cuando mest sea nulo
                        if (mestFCT[i] == "")
                        {
                            AT[i] = pSaldoActualAfore + pDensidadCotizacion * (an1 + ((AOk + CSk) + 12 * simasd * G + (XT[i] * Np2t[i]) + ((CSEh * 30.4) * Nptp[i])));
                        }
                        else
                        {
                            AT[i] = pSaldoActualAfore + pDensidadCotizacion * (an1 + ((AOk + CSk) * mestFC[i] + 12 * simasd * G + (XT[i] * Np2t[i]) + ((CSEh * 30.4) * Nptp[i])));
                        }
                    }
                    else if (tFC[i] > (n1 + n2))
                    {
                        double aokmascsk = Ak + 7 * gk;
                        double san1y2 = an1 + an2;
                        double sn1y2 = n1 + n2;

                        AT[i] = pSaldoActualAfore + pDensidadCotizacion * (san1y2 + ((aokmascsk * pSalarioBaseCotizacionMensual + CSk) * (tFC[i] - sn1y2)));
                    }

                    //10.-Para estimar los valores de la columna Saldo acumulado en t, 
                    double Sn_1 = pSaldoActualAfore * Math.Pow(1 + tRendimientoMensual, n1) + (pDensidadCotizacion * (aObligatoriaIniMensual + CuotaSocial) * ((Math.Pow(1 + tRendimientoMensual, n1) - 1) / tRendimientoMensual));

                    double sn_2 = Sn_1 * (Math.Pow(1 + tRendimientoMensual, n2)) +
                       (pDensidadCotizacion * (

                       (AOk + CSk) * ((Math.Pow(1 + tRendimientoMensual, n2) - 1) / tRendimientoMensual) +

                       (G / tRendimientoAnual) *
                       ((Math.Pow(1 + tRendimientoMensual, 12) - 1) / tRendimientoMensual) *
                       (((Math.Pow(1 + tRendimientoAnual, N2_icrementosAP - 1) - 1) / tRendimientoAnual) - (N2_icrementosAP - 1)) *
                       (Math.Pow(1 + tRendimientoMensual, n2p)) +
                       x * ((Math.Pow(1 + tRendimientoMensual, n2p) - 1) / tRendimientoMensual) +
                       (CSEh * 30.4) * ((Math.Pow(1 + tRendimientoMensual, npp) - 1) / tRendimientoMensual) * (Math.Pow(1 + tRendimientoMensual, (n2 - npp)))));

                    if (bloque0[i] == 0)
                    {
                        SAt[i] = pSaldoActualAfore;
                    }
                    else if (tFC[i] <= n1)
                    {
                        SAt[i] = (pSaldoActualAfore * (Math.Pow(1 + tRendimientoMensual, tFC[i]))) + ((pDensidadCotizacion * (aObligatoriaIniMensual + CuotaSocial)) * ((Math.Pow(1 + tRendimientoMensual, tFC[i]) - 1) / tRendimientoMensual));

                    }
                    else if ((n1 < tFC[i] && tFC[i] <= (n1 + n2)))
                    {
                        SAt[i] = Sn_1 * (Math.Pow(1 + tRendimientoMensual, mestFC[i])) + (pDensidadCotizacion * ((((AOk + CSk) * ((Math.Pow(1 + tRendimientoMensual, mestFC[i]) - 1) / tRendimientoMensual)) +
                            (G / tRendimientoAnual) * ((Math.Pow(1 + tRendimientoMensual, 12) - 1) / tRendimientoMensual) * (((Math.Pow(1 + tRendimientoAnual, n2t[i] - 1) - 1) / tRendimientoAnual) - (n2t[i] - 1)) * (Math.Pow(1 + tRendimientoMensual, Np2t[i])) +
                            (XT[i] * ((Math.Pow(1 + tRendimientoMensual, Np2t[i]) - 1) / tRendimientoMensual))) +
                            (CSEh * 30.4) * ((Math.Pow(1 + tRendimientoMensual, Nptp[i]) - 1) / tRendimientoMensual) * (Math.Pow(1 + tRendimientoMensual, (mestFC[i] - Nptp[i])))));

                    }
                    else if (tFC[i] > (n1 + n2))
                    {
                        SAt[i] = sn_2 * (Math.Pow(1 + tRendimientoMensual, (tFC[i] - (n1 + n2)))) + (pDensidadCotizacion *
                           ((Ak * pSalarioBaseCotizacionMensual + 7 * G + CSk) *
                            (Math.Pow(1 + tRendimientoMensual, (tFC[i] - (n1 + n2))) - 1) / tRendimientoMensual));

                    }

                    // Rendimiento
                    RT[i] = SAt[i] - AT[i];

                    bloqueR.Add(RT[i]);
                    bloqueA.Add(AT[i]);
                    labels.Add(tFC[i].ToString());

                    if (i == bloque0.Count - 1)
                    {
                        iData.Add(labels);
                        iData.Add(bloqueA);
                        iData.Add(bloqueR);
                        iData.Add(Math.Round(AT[i], 0));
                        iData.Add((Math.Round(RT[i], 0)));
                    }
                }
            }
            else
            {
                List<int> Bloque1 = new List<int>();
                bloque0 = new List<int>() { 0, 12 };
                Bloque1 = new List<int>() { 0, nMesesRetiro };

                for (int i = 0; i < bloque0.Count; i++)
                {

                    if (bloque0[i] == 0)
                    {
                        AT[i] = pSaldoActualAfore;
                    }
                    else if (n2 > 0)
                    {
                        if (n1 > 0)
                        {
                            AT[i] = pSaldoActualAfore + pDensidadCotizacion * (an1 + an2);
                        }
                        else if (n3 > 0)
                        {
                            AT[i] = pSaldoActualAfore + pDensidadCotizacion * (an2 + an3);
                        }
                        else if (n1 == 0 && n3 == 0)
                        {
                            AT[i] = pSaldoActualAfore + pDensidadCotizacion * an2;
                        }
                    }
                    else if (n2 == 0)
                    {
                        if (n1 > 0)
                        {
                            AT[i] = pSaldoActualAfore + pDensidadCotizacion * an1;
                        }
                        else if (n3 > 0)
                        {
                            AT[i] = pSaldoActualAfore + pDensidadCotizacion * an3;
                        }
                    }
                    //3.- Cálculamos SAt
                    if (bloque0[i] == 0)
                    {
                        SAt[i] = pSaldoActualAfore;
                    }
                    else
                    {
                        SAt[i] = Saldo_final;
                    }

                    //4 .- Cálculamos RT
                    RT[i] = SAt[i] - AT[i];
                    bloqueR.Add(RT[i]);
                    bloqueA.Add(AT[i]);
                    labels.Add(Bloque1[i].ToString());

                    if (i == bloque0.Count - 1)
                    {
                        iData.Add(labels);
                        iData.Add(bloqueA);
                        iData.Add(bloqueR);
                        iData.Add(Math.Round(AT[i], 0));
                        iData.Add((Math.Round(RT[i], 0)));
                    }
                }
            }

            return iData;
        }

        public List<object> GetDataSetGraficaDos(Double pSaldoActualAfore,
                                             Int32 Tipo_de_Trabajador,
                                             Double pGenero,
                                             Double pSalarioBaseCotizacionMensual,
                                             Double pRendimiento,
                                             Double pComisionAfore,
                                             DateTime pFechaNacimiento,
                                             Double Bono_ISSSTE,
                                             Int32 Fecha_redencion,
                                             DateTime pFechaCorte,
                                             Double pComisionAportacion,
                                             Double Porc_Av,
                                             Double Porc_AS,
                                             Double pDensidadCotizacion,
                                             Double pEdadRetiro,
                                             Int32 iResultado_funcion,
                                             Double aObligatoriaIniMensual,
                                             Double tRendimientoMensual,
                                             Int32 n1,
                                             Int32 nMesesRetiro,
                                             DateTime fCambio1,
                                             DateTime fCambio2,
                                             Int32 n2,
                                             Int32 n2p,
                                             Double tRendimientoAnual,
                                             Int32 n3,
                                             Double nSemanasCotizadas,
                                             Double pEdad,
                                             Double nPensionDeseada)



        {
            Double Aporta, Saldo_a, Saldo_b, Saldo_c;
            Aporta = 0;

            DateTime pFechaRetiro = new DateTime(pFechaNacimiento.Year, pFechaNacimiento.Month, pFechaNacimiento.Day).AddYears(Convert.ToInt32(pEdadRetiro));

            SMGVDF = 0;

            if (this.Tipo == TipoConsulta.IMSS)
                SMGVDF = getParametros();

            Nivel_Salarial = pSalarioBaseCotizacionMensual / (30 * SMGVDF);
            salario_diario = Math.Round((pSalarioBaseCotizacionMensual / 30.4) * 1000000) / 1000000;

            Fecha_retiro = new DateTime(pFechaNacimiento.Year, pFechaNacimiento.Month, pFechaNacimiento.Day).AddYears(Convert.ToInt32(pEdadRetiro)); //'''''CAMBIO''''''

            int meses = (int)Fn_n(Fecha_retiro, pFechaCorte);
            Saldo_a = pSaldoActualAfore;

            Double apMensual = Aportaciones(Tipo_de_Trabajador, pSalarioBaseCotizacionMensual, pComisionAportacion, Porc_Av, Porc_AS, pDensidadCotizacion);
            Double Cs = Cuota_Social_Mensual(Tipo_de_Trabajador, pDensidadCotizacion, pFechaCorte) / pDensidadCotizacion;

            Double CuotaSocial = Cuota_Social_Mensual(Tipo_de_Trabajador, pDensidadCotizacion, pFechaCorte);
            A1 = AcumulacionAP1(aObligatoriaIniMensual, CuotaSocial, tRendimientoMensual, nMesesRetiro, n1, fCambio1);
            //PGO - ODT-07 - Variables para obtener A2

            //Aportación base a partir del año 2023 respeto al rango salarial k
            Double Ak = Aportacion_Base(Tipo_de_Trabajador);
            DateTime FechaInicioCSk = new DateTime(2023, 01, 01, 0, 0, 0);

            // PGO - ODT-07 - Determinación de la aportación AK inicial de acuerdo al año de corte
            int AnioFC1 = fCambio1.Year;
            int l = 0;

            if (AnioFC1 > 2022)
            {
                l = AnioFC1 - 2023;
            }
            else if (AnioFC1 == 2022)
            {
                l = 0;
            }

            // PGO - ODT-07 - Gradiente conforme al que crece la aportación base Ak
            Double gk = Gradiente(Tipo_de_Trabajador);


            // PGO - ODT-07 - Monto en pesos de la aportación base mensual
            Double AOk = 0;
            AOk = (Ak + l * gk) * pSalarioBaseCotizacionMensual;

            // PGO - ODT-07 - Gradiente gk por el salario de cotización
            Double G = 0;
            G = gk * pSalarioBaseCotizacionMensual;


            // PGO - ODT-07 - Monto mensual por concepto de cuota social a partir de 2023 (CSk)
            Double CSk = 0;
            DateTime Fcalculo = DateTime.Now;

            CSk = Cuota_Social_CSk(Tipo_de_Trabajador);
            CSk = CSk * 30.4;

            // PGO - ODT-07 - Número de años en los que se dan los incrementos en la  aportación gradual (N2)
            double N2_icrementosAP = 0;
            DateTime fCambio1_aux;
            DateTime fCambio1p;
            DateTime fCambioFei;
            fCambio1p = fCambio1;
            fCambio1_aux = fCambio1;
            fCambio1_aux = fCambio1_aux.AddDays(1);
            double n2_aux = (double)n2 / 24;

            if (n2_aux > 1)
            {
                N2_icrementosAP = (fCambio2.Year - ((fCambio1_aux.Year)) + 1);
            }
            else if (n2_aux <= 1)
            {
                N2_icrementosAP = 1;
            }

            // PGO - ODT-07 - Nivel del gradiente que se evalúa en el periodo despues del incremento en la aportación gradual
            double x = 0;

            fCambio1 = fCambio1.AddDays(1);

            x = (fCambio2.Year - (fCambio1.Year)) * G;

            //PGO - ODT-07 - Acumulación por aportaciones durante el periodo que comprende del 1 de enero de 2023 al 31 de diciembre de 2030
            DateTime FechaValidarA2 = new DateTime(2030, 12, 31, 0, 0, 0);

            if (fCambio2 <= FechaValidarA2)
            {

                A2 = ((((AOk + CSk) * ((Math.Pow(1 + tRendimientoMensual, n2) - 1) / tRendimientoMensual)) +
              (G / tRendimientoAnual) * ((Math.Pow(1 + tRendimientoMensual, 12) - 1) / tRendimientoMensual) * (((Math.Pow(1 + tRendimientoAnual, N2_icrementosAP - 1) - 1) / tRendimientoAnual) - (N2_icrementosAP - 1)) * (Math.Pow(1 + tRendimientoMensual, n2p)) +
              (x * ((Math.Pow(1 + tRendimientoMensual, n2p) - 1) / tRendimientoMensual))) *
              (Math.Pow(1 + tRendimientoMensual, nMesesRetiro - (n1 + n2))));

            }
            else if (fCambio2 > FechaValidarA2)
            {
                A2 = 0;
            }

            // PGO - ODT-07 - Variables para obtener A3 - Anualidad del periodo n3, comprendido a partir de enero 2031 y hasta la fecha de retiro
            A3 = (Ak * pSalarioBaseCotizacionMensual + 7 * G + CSk) * ((Math.Pow(1 + tRendimientoMensual, n3) - 1) / tRendimientoMensual);

            // PGO - ODT-07
            // Cuota Social especial que sólo se entregará durante 2023 a las personas cuyo salario se encuentre dentro del rango salarial h
            Double CSEh = 0;
            if (Fecha_retiro.Year >= 2023)
            {
                CSEh = Cuota_Social_Especial(Tipo_de_Trabajador);
            }

            //PGO - ODT-07 - Fecha de cálculo de la Cuota Especial
            DateTime FechaValidarFCe = new DateTime(2023, 12, 31, 0, 0, 0);
            DateTime FCe = DateTime.Now;
            if (fCambio2 > FechaValidarFCe)
            {
                FCe = FechaValidarFCe;
            }
            else if (fCambio2 <= FechaValidarFCe)
            {
                FCe = fCambio2;
            }

            //PGO - ODT-07 - n’’: Número de meses que el trabajador obtendría la aportación de la cuota social especial, la cual sólo aplica durante el año 2023 
            double npp = 0;

            if (Fcalculo.Year <= 2023 && 2023 <= Fecha_retiro.Year)
            {
                if (fCambio1.Day > 15 && FCe.Day <= 15)
                {
                    npp = (((FCe.Year - fCambio1.Year) - 1) * 12 + 12 - fCambio1.Month + FCe.Month - 1);
                }
                else if (fCambio1.Day <= 15 && FCe.Day > 15)
                {
                    npp = (((FCe.Year - fCambio1.Year) - 1) * 12 + 12 - fCambio1.Month + FCe.Month + 1);
                }

                else
                {
                    npp = (((FCe.Year - fCambio1.Year) - 1) * 12 + 12 - fCambio1.Month + FCe.Month);
                }
            }
            else
            {
                npp = 0;
            }

            //PGO - ODT-07 - al saldo que se podría acumular por las aportaciones de la cuota social especial, las cuales solo se otorgan durante 2023 para trabajadores que coticen con entre 4.01 y 7.09 veces la UMA 
            double CSE = 0;

            CSE = (CSEh * 30.4) * ((Math.Pow(1 + tRendimientoMensual, npp) - 1) / tRendimientoMensual) * (Math.Pow(1 + tRendimientoMensual, nMesesRetiro - (npp + n1)));
            //CSE = (CSEh * 30.4) * ((1 + Math.Pow(tRendimientoMensual, npp) - 1) / tRendimientoMensual) * (1 + Math.Pow(tRendimientoMensual, nMesesRetiro - (npp + n1)));
            CSE_t = CSE;

            //PGO - ODT-07 - AV-Ahorro voluntario, para el anexo 1, el valor predeterminado es igual a cero
            double AV = 0;
            Saldo_b = Aporta; //Guarda el saldo acumulado de las aportaciones
            Saldo_c = Bono_ISSSTE; //Se guarda el saldo acumulado del Bono de Pensión ISSSTE

            pRendimiento = Rendimiento_mensual(pRendimiento);
            pComisionAfore = pComisionAfore / 12;

            //PGO - ODT-07 - Saldo acumulado al retiro
            // 	Saldo de ahorro para el retiro en la cuenta individual proyectado al cumplir la Edad de retiro. En caso de que la Edad actual del trabajador sea mayor o igual a la Edad de retiro, Sf = Si 
            Double sdo_final = 0;
            if (pEdad > pEdadRetiro)
            {
                sdo_final = pSaldoActualAfore;
            }
            else
            {
                sdo_final = pSaldoActualAfore * (Math.Pow(1 + tRendimientoMensual, nMesesRetiro)) + pDensidadCotizacion * (A1 + A2 + A3 + CSE + AV);
            }

            Saldo_final = sdo_final;
            double n = meses;
            DateTime fechafc = DateTime.Today;

            int dif = 0;
            int asu = 0;
            
            DateTime af;
            int diafcep = 0;
            int mesfcep = 0;
            int aniofcep = 0;
            double alfa_a = 0.02;

            List<int> bloque0 = new List<int>();
            List<Double> bloqueR = new List<Double>();
            List<Double> bloqueA = new List<Double>();
            List<string> labels = new List<string>();
            List<object> iData = new List<object>();
            List<int> iT = new List<int>();

            iT = new List<int>() { 1, 2, 3, 4, 5, 6 };
            string[] fecT = new string[6];
            int[] p = new int[6];
            int[] mespFC = new int[6];
            string[] mespFCT = new string[6];
            string[] fpT = new string[6];
            int[] n2p1 = new int[6];
            string[] n2pT1 = new string[6];
            string[] np2pT = new string[6];
            int[] aniofp = new int[6];
            string[] n2pT = new string[6];
            int[] np2p = new int[6];
            int[] diafp = new int[6];
            DateTime[] fechavar = new DateTime[6];
            string[] FCEpT = new string[6];
            string[] resfcep = new string[6];
            int[] nppp = new int[6];
            string[] nppT = new string[6];
            string[] xpT = new string[6];
            double[] xp = new double[6];
            double[] SAei = new double[6];
            int[] nei = new int[6];
            string[] neiT = new string[6];
            int[] n2ei = new int[6];
            string[] n2eiT = new string[6];
            int[] nppei = new int[6];
            string[] nppeiT = new string[6];
            double[] xei = new double[6];
            string[] xeiT = new string[6];
            double[] iei = new double[6];
            string[] ieiT = new string[6];
            double[] aokei = new double[6];
            string[] aokeiT = new string[6];
            double[] spn2 = new double[6];
            double[] spn3 = new double[6];
            double[] sei = new double[6];
            double[] snei = new double[6];
            double[] dei = new double[6];
            int[] mesfp = new int[6];
            string[] fect2 = new string[6];
            string[] fect3 = new string[6];
            int[] fect4 = new int[6];

            iT[5] = Convert.ToInt16(pEdadRetiro);
            iT[0] = ((Convert.ToInt16(pEdad) / 10) + 1) * 10;
            dif = ((iT[5] - iT[0]) / 5);

            for (int i = 1; i < 5; i++)
            {
                iT[i] = iT[i - 1] + dif;
            }

            for (int i = 0; i < 6; i++)
            {
                //2.-Columna Fei
                if (iT[i] >= pEdad)
                {
                    asu = pFechaNacimiento.Year + iT[i];
                    fecT[i] = pFechaNacimiento.Day + "/" + pFechaNacimiento.Month + "/" + asu.ToString();

                }

                //3.- Columna p 
                // numero de periodos entre la Fecha de cálculo FC y Fei 
                if (fechafc.Day > 15 && pFechaNacimiento.Day <= 15)
                {
                    p[i] = (asu - fechafc.Year - 1) * 12 + 12 - fechafc.Month + pFechaNacimiento.Month - 1;
                }
                else if (fechafc.Day <= 15 && pFechaNacimiento.Day > 15)
                {
                    p[i] = (asu - fechafc.Year - 1) * 12 + 12 - fechafc.Month + pFechaNacimiento.Month + 1;
                }
                else
                {
                    p[i] = (asu - fechafc.Year - 1) * 12 + 12 - fechafc.Month + pFechaNacimiento.Month;
                }

                //4.- Columna Mesp 
                // corresponde al número de meses para cada valor p dentro del periodo n2
                if (n2 == 0)
                {
                    mespFCT[i] = "";
                }
                else if (n1 < p[i] && p[i] <= (n1 + n2))
                {
                    mespFC[i] = p[i] - n1;
                }
                else if (p[i] >= (n1 + n2))
                {
                    mespFCT[i] = "";
                }

                //5.- Columna Fp 
                // valor de la Fecha en p, la cual se obtiene de sumar p meses a  FC  

                if (mespFCT[i] == "")
                {
                    fpT[i] = "";
                }
                else if (mespFC[i] < n2)
                {
                    DateTime fechafp = DateTime.Today.AddMonths(Convert.ToInt32(p[i]));
                    fpT[i] = Convert.ToString(fechafp.ToShortDateString());

                    diafp[i] = DateTime.Today.Day;
                    mesfp[i] = Int16.Parse(fpT[i].Substring(3, 2));
                    aniofp[i] = Convert.ToInt32(fpT[i].Substring(6, 4));
                    fechavar[i] = Convert.ToDateTime(fpT[i]);
                }
                else if (mespFC[i] == n2)
                {
                    fpT[i] = fCambio2.Day + "/" + fCambio2.Month + "/" + fCambio2.Year;

                    diafp[i] = fCambio2.Day;
                    mesfp[i] = fCambio2.Month;
                    aniofp[i] = Convert.ToInt32(fpT[i].Substring(6, 4));
                    fechavar[i] = Convert.ToDateTime(fpT[i]);
                    
                }
                //6.-Columna N2p
                //número de años en los que se dan los incrementos en la aportación gradual 
                if (mespFCT[i] == "")
                {
                    n2pT[i] = "";
                }
                else if ((n2 / 24) > 1)
                {
                    af = fCambio1.AddDays(1);
                    n2p1[i] = (aniofp[i] - af.Year) + 1;
                }
                else if ((n2 / 24) <= 1)
                {
                    n2p1[i] = 1;
                }

                //7.- Columna Np2p 
                // número de periodos que se acumularía la aportación después del último incremento
                if (mespFCT[i] == "")
                {
                    np2pT[i] = "";
                }
                else if (diafp[i] > 15)
                {
                    np2p[i] = mesfp[i];
                }
                else
                {
                    np2p[i] = mesfp[i] - 1;
                }

                //8.- Columna FCEp
                //valor de la Fecha de cuota social especial en p
                DateTime fechadd = Convert.ToDateTime("31/12/2023");
                if (mespFCT[i] == "")
                {
                    FCEpT[i] = "";

                }
                else if (fechavar[i] > fechadd)
                {
                    resfcep[i] = Convert.ToString(fechadd.ToShortDateString());

                    diafcep = 31;
                    mesfcep = 12;
                    aniofcep = 2023;
                }
                else if (fechavar[i] <= fechadd)
                {
                    resfcep[i] = fpT[i];

                    diafcep = Int16.Parse(resfcep[i].Substring(0, 2));
                    mesfcep = Int16.Parse(resfcep[i].Substring(3, 2));
                    aniofcep = Int16.Parse(resfcep[i].Substring(6, 4));
                }

                //9.- Columna Nppp
                //número de meses que el trabajador obtendría la aportación de la Cuota Social Especial
                if (mespFCT[i] == "")
                {
                    nppT[i] = "";
                }
                else if (fechafc.Year <= 2023 && 2023 <= aniofp[i])
                {
                    if (fCambio1p.Day > 15 && diafcep <= 15)
                    {
                        nppp[i] = (aniofcep - fCambio1p.Year - 1) * 12 + 12 - fCambio1p.Month + mesfcep - 1;
                    }
                    else if (fCambio1p.Day <= 15 && diafcep > 15)
                    {
                        nppp[i] = (aniofcep - fCambio1p.Year - 1) * 12 + 12 - fCambio1p.Month + mesfcep + 1;
                    }

                    else
                    {
                        nppp[i] = (aniofcep - fCambio1p.Year - 1) * 12 + 12 - fCambio1p.Month + mesfcep;
                    }
                }
                else
                {
                    nppp[i] = 0;
                }

                //10.- Columna Xp
                // se refiere al nivel del gradiente para cada valor de 𝑝 
                if (mespFCT[i] == "")
                {
                    xpT[i] = "";
                }
                else
                {
                    xp[i] = (aniofp[i] - (fCambio1p.Year + 1)) * G;
                }

                //11.- Columna SAei
                // saldo acumulado
                double Sn_1 = pSaldoActualAfore * Math.Pow(1 + tRendimientoMensual, n1) + (pDensidadCotizacion * (aObligatoriaIniMensual + CuotaSocial) * ((Math.Pow(1 + tRendimientoMensual, n1) - 1) / tRendimientoMensual));

                double sn_2 = Sn_1 * (Math.Pow(1 + tRendimientoMensual, n2)) +
                     (pDensidadCotizacion * (
                     (AOk + CSk) * ((Math.Pow(1 + tRendimientoMensual, n2) - 1) / tRendimientoMensual) +
                     (G / tRendimientoAnual) *
                     ((Math.Pow(1 + tRendimientoMensual, 12) - 1) / tRendimientoMensual) *
                     (((Math.Pow(1 + tRendimientoAnual, N2_icrementosAP - 1) - 1) / tRendimientoAnual) - (N2_icrementosAP - 1)) *
                     (Math.Pow(1 + tRendimientoMensual, n2p)) +
                     x * ((Math.Pow(1 + tRendimientoMensual, n2p) - 1) / tRendimientoMensual) +
                     (CSEh * 30.4) * ((Math.Pow(1 + tRendimientoMensual, npp) - 1) / tRendimientoMensual) * (Math.Pow(1 + tRendimientoMensual, (n2 - npp)))));

                if (p[i] <= n1)
                {
                    SAei[i] = pSaldoActualAfore * (Math.Pow(1 + tRendimientoMensual, p[i])) + ((pDensidadCotizacion * (aObligatoriaIniMensual + CuotaSocial)) * (
                             (Math.Pow(1 + tRendimientoMensual, p[i]) - 1) / tRendimientoMensual));
                }
                else if (n1 < p[i] && p[i] <= (n1 + n2))
                {
                    //if (mespFCT[i] == "")
                    if (mespFCT[i] == "")
                    {
                        SAei[i] = Sn_1 * (1 + tRendimientoMensual) + (pDensidadCotizacion * ((AOk + CSk) * (
                        (1 + tRendimientoMensual - 1) / tRendimientoMensual) + (G / tRendimientoAnual) * (
                        (Math.Pow(1 + tRendimientoMensual, 12) - 1) / tRendimientoMensual) * (((Math.Pow(1 + tRendimientoAnual, n2p1[i] - 1) - 1) / tRendimientoAnual) - (
                        n2p1[i] - 1)) * (Math.Pow(1 + tRendimientoMensual, np2p[i])) + xp[i] * ((Math.Pow(1 + tRendimientoMensual, np2p[i]) - 1) / tRendimientoMensual) + (
                        CSEh * 30.4) * ((Math.Pow(1 + tRendimientoMensual, nppp[i]) - 1) / tRendimientoMensual) * (Math.Pow(1 + tRendimientoMensual, nppp[i]))
                        )
                        );
                    }
                    else
                    {
                        SAei[i] = Sn_1 * (Math.Pow(1 + tRendimientoMensual, mespFC[i])) + (pDensidadCotizacion * ((AOk + CSk) * (
                        (Math.Pow(1 + tRendimientoMensual, mespFC[i]) - 1) / tRendimientoMensual) + (G / tRendimientoAnual) * (
                        (Math.Pow(1 + tRendimientoMensual, 12) - 1) / tRendimientoMensual) * (((Math.Pow(1 + tRendimientoAnual, n2p1[i] - 1) - 1) / tRendimientoAnual) - (
                        n2p1[i] - 1)) * (Math.Pow(1 + tRendimientoMensual, np2p[i])) + xp[i] * ((Math.Pow(1 + tRendimientoMensual, np2p[i]) - 1) / tRendimientoMensual) + (
                        CSEh * 30.4) * ((Math.Pow(1 + tRendimientoMensual, nppp[i]) - 1) / tRendimientoMensual) * (Math.Pow(1 + tRendimientoMensual, (mespFC[i] - nppp[i])))
                        )
                        );
                    }
                }
                else if (p[i] > (n1 + n2))
                {
                    SAei[i] = sn_2 * (Math.Pow(1 + tRendimientoMensual, (p[i] - (n1 + n2)))) + (pDensidadCotizacion * ((Ak * pSalarioBaseCotizacionMensual + 7 * G + CSk) * (
                    ((Math.Pow(1 + tRendimientoMensual, (p[i] - (n1 + n2)))) - 1) / tRendimientoMensual)));
                }
                //12.- Columna nei
                // número de periodos 
                if (p[i] <= n1 || p[i] > (n1 + n2))
                {
                    neiT[i] = "";
                }
                else if (pFechaNacimiento.Day > 15 && fCambio2.Day <= 15)
                {
                    nei[i] = (fCambio2.Year - asu - 1) * 12 + 12 - pFechaNacimiento.Month + fCambio2.Month - 1;
                }
                else if (pFechaNacimiento.Day <= 15 && fCambio2.Day > 15)
                {
                    nei[i] = (fCambio2.Year - asu - 1) * 12 + 12 - pFechaNacimiento.Month + fCambio2.Month + 1;
                }
                else
                {
                    nei[i] = (fCambio2.Year - asu - 1) * 12 + 12 - pFechaNacimiento.Month + fCambio2.Month;
                }

                //13.- N2ei
                //número de años en los que se dan los incrementos en la aportación gradual
                DateTime pFechaNacimiento1 = pFechaNacimiento.AddDays(1);
                if (neiT[i] == "")
                {
                    n2eiT[i] = "";
                }
                
                else if ((n2 / 24) > 1)
                {
                    //Revisar
                    fect2[i] = pFechaNacimiento1.Day.ToString().PadLeft(2, '0') + "/" + pFechaNacimiento.Month.ToString().PadLeft(2, '0') + "/" + asu.ToString(); //fecha Fei más un dia
                    fect4[i] = Int32.Parse(fect2[i].Substring(6, 4)); //Solo tomar el año
                    n2ei[i] = (fCambio2.Year - fect4[i]) + 1;
                }
                else if ((n2 / 24) <= 1)
                {
                    n2ei[i] = 1;
                }

                // 14.- nppei
                // número de meses que el trabajador obtendría la aportación de la Cuota Social Especial 
                if (neiT[i] == "")
                {
                    nppeiT[i] = "";
                }
                else if (asu <= 2023 && 2023 <= fCambio2.Year)
                {
                    if (pFechaNacimiento.Day > 15 && FCe.Day <= 15)
                    {
                        nppei[i] = (FCe.Year - asu - 1) * 12 + 12 - pFechaNacimiento.Month + FCe.Month - 1;
                    }
                    else if (pFechaNacimiento.Day <= 15 && FCe.Day > 15)
                    {
                        nppei[i] = (FCe.Year - asu - 1) * 12 + 12 - pFechaNacimiento.Month + FCe.Month + 1;
                    }
                    else
                    {
                        nppei[i] = (FCe.Year - asu - 1) * 12 + 12 - pFechaNacimiento.Month + FCe.Month;
                    }
                }
                else
                {
                    nppei[i] = 0;
                }

                // 15.-Columna Xei
                // se refiere al nivel de aportación con el gradiente correspondiente
                if (neiT[i] == "")
                {
                    xeiT[i] = "";
                }
                else
                {
                    //Revisar 
                    fect2[i] = pFechaNacimiento1.Day.ToString().PadLeft(2, '0') + "/" + pFechaNacimiento.Month.ToString().PadLeft(2, '0') + "/" + asu.ToString(); //fecha Fei más un dia
                    fect4[i] = Int32.Parse(fect2[i].Substring(6, 4)); //Solo tomar el año
                    xei[i] = (fCambio2.Year - fect4[i]) * G;
                }
                // 16.- Columna Iei
                // se refiere al nivel del gradiente vigente
                if (neiT[i] == "")
                {
                    ieiT[i] = "";
                }
                else if (asu > 2022)
                {
                    iei[i] = asu - 2023;
                }
                else
                {
                    iei[i] = 0;
                }

                // 17.- columan AOkei
                // se refiere a la aportación con gradiente 
                if (neiT[i] == "")
                {
                    aokeiT[i] = "";
                }
                else
                {
                    aokei[i] = (Ak + (iei[i] * gk)) * pSalarioBaseCotizacionMensual;
                }

                // Calcular variable Sp,n2 Sp,n3
                spn2[i] = (pDensidadCotizacion * ((AOk + CSk) * ((Math.Pow(1 + tRendimientoMensual, n2) - 1) / tRendimientoMensual) + (
                         G / tRendimientoAnual) * ((Math.Pow(1 + tRendimientoMensual, 12) - 1) / tRendimientoMensual) * (
                         ((Math.Pow(1 + tRendimientoAnual, N2_icrementosAP - 1) - 1) / tRendimientoAnual) - (N2_icrementosAP - 1)) * (
                         Math.Pow(1 + tRendimientoMensual, n2p)) + x * ((Math.Pow(1 + tRendimientoMensual, n2p) - 1) / tRendimientoMensual) + (
                         CSEh * 30.4) * ((Math.Pow(1 + tRendimientoMensual, npp) - 1) / tRendimientoMensual) * (Math.Pow(1 + tRendimientoMensual, (n2 - npp)))));
                spn3[i] = (pDensidadCotizacion * ((Ak * pSalarioBaseCotizacionMensual + 7 * G + CSk) * (
                    (Math.Pow(1 + tRendimientoMensual, n3) - 1) / tRendimientoMensual)));

                // Calcular Sei
                // es el saldo acumulado a partir
                if (p[i] <= n1)
                {
                    sei[i] = ((pDensidadCotizacion * (aObligatoriaIniMensual + CuotaSocial)) * ((Math.Pow(1 + tRendimientoMensual, (n1 - p[i])) - 1) / tRendimientoMensual)) *
                       (Math.Pow(1 + tRendimientoMensual, (n2 + n3))) + spn2[i] * (Math.Pow(1 + tRendimientoMensual, n3)) + spn3[i];
                }
                else if (n1 < p[i] && p[i] <= (n1 + n2))
                {
                    sei[i] = pDensidadCotizacion * ((aokei[i] + CSk) * ((Math.Pow(1 + tRendimientoMensual, nei[i]) - 1) / tRendimientoMensual) +
                         (G / tRendimientoAnual) * ((Math.Pow(1 + tRendimientoMensual, 12) - 1) / tRendimientoMensual) * (((Math.Pow(1 + tRendimientoAnual, n2ei[i] - 1) - 1) / tRendimientoAnual) - (n2ei[i] - 1)) * (Math.Pow(1 + tRendimientoMensual, n2p)) +
                         xei[i] * ((Math.Pow(1 + tRendimientoMensual, n2p) - 1) / tRendimientoMensual) + (CSEh * 30.4) * ((Math.Pow(1 + tRendimientoMensual, nppei[i]) - 1) / tRendimientoMensual) * ((Math.Pow(1 + tRendimientoMensual, nei[i] - nppei[i])))) *
                         (Math.Pow(1 + tRendimientoMensual, n3)) + spn3[i];
                }
                else if (p[i] > (n1 + n2))
                {
                    sei[i] = (pDensidadCotizacion * ((Ak * pSalarioBaseCotizacionMensual + 7 * G + CSk) * (
                             (Math.Pow(1 + tRendimientoMensual, (nMesesRetiro - p[i])) - 1) / tRendimientoMensual)));
                }

                // 18.- Columna SNei
                Double Valor_URVe = URVe(pGenero, pEdadRetiro);
                snei[i] = ((Math.Round(nPensionDeseada) * Valor_URVe * 12 * (1 + alfa_a)) - sei[i]) / (Math.Pow(1 + tRendimientoMensual, (nMesesRetiro - p[i])));

                // déficit de ahorro
                dei[i] = snei[i] - SAei[i];

                bloqueR.Add(snei[i]);
                bloqueA.Add(SAei[i]);
                labels.Add(iT[i].ToString());

                if (i >= 5)
                {
                    iData.Add(labels);
                    iData.Add(bloqueA);
                    iData.Add(bloqueR);
                    iData.Add(Math.Round(snei[i], 0));
                    iData.Add((Math.Round(dei[i], 0)));
                }

            }

            return iData;

        }



        public static List<NegativaPension> CalculaSaldoAcumuladoNP(CapturaCalculadora objCapturaCalculadora, Int32 nMesesRetiro)
        {
            List<NegativaPension> lstobjNegativaPension = new List<NegativaPension>();

            DateTime Fecha_retiro = new DateTime(objCapturaCalculadora.FechaNacimiento.Year, objCapturaCalculadora.FechaNacimiento.Month, objCapturaCalculadora.FechaNacimiento.Day).AddYears(Convert.ToInt32(objCapturaCalculadora.EdadRetiro));

            int diasParaRetiro = (Fecha_retiro - DateTime.Now.Date).Days;

            double rm = 0;
            double cm = 0;
            double d = 0;
            double av = 0;
            


            for (int i = 1; i <= 5; i++)
            {
                NegativaPension objNegativaPension = new NegativaPension();

                //Rendimiento real mensual
                //rm = Math.Pow((1 + objCapturaCalculadora.Rendimiento), 1 / 12) - 1;
                rm = Math.Pow((1.0 + objCapturaCalculadora.Rendimiento), (1.0 / 12.0)) - 1.0;

                //Comisipon mensual
                cm = objCapturaCalculadora.Comision / 12;

                //Densidad de cotización del trabajador equivalente a 80%, se supone constante desde la Fecha de corte y hasta la Edad de retiro. 
                d = objCapturaCalculadora.DensidadCotizacion;

                //Ahorro voluntario
                av = ((double)i / 100.0) * objCapturaCalculadora.SalarioBase;

                objNegativaPension.PorcentajeSalario = i;
                objNegativaPension.PorcentajeSalarioPesos = Math.Round(av + 0.01); //se suma 0.01 por los casos de de .50

                // Sye - alejandro.guevara  
                //objNegativaPension.SaldoAhorroVoluntario = d * av * (((Math.Pow((1 + rm) * (1 - cm), n)) - 1) / (((1 + rm) * (1 - cm)) - 1));
                //objNegativaPension.SaldoAhorroVoluntario = d * av * (((Math.Pow((1 + objCapturaCalculadora.TasaRendimientoMensual), n)) - 1) / objCapturaCalculadora.TasaRendimientoMensual);
                objNegativaPension.SaldoAhorroVoluntario = d * av * ((Math.Pow(1 + objCapturaCalculadora.TasaRendimientoMensual, nMesesRetiro) - 1) / objCapturaCalculadora.TasaRendimientoMensual);

                objNegativaPension.SaldoAhorroObligatorio = objCapturaCalculadora.SaldoAcumulado; //(0.065 * objCapturaCalculadora.SalarioBase);

                objNegativaPension.SaldoAhorroTotal = objNegativaPension.SaldoAhorroVoluntario + objNegativaPension.SaldoAhorroObligatorio;

                lstobjNegativaPension.Add(objNegativaPension);
            }

            return lstobjNegativaPension;
        }

        public Double Av_mensual_IMSS(Double pSaldoActualAfore, Int32 Tipo_de_Trabajador, Double pGenero, Double pSalarioBaseCotizacionMensual, Double pRendimiento, Double pComisionAfore, DateTime pFechaNacimiento, Double Bono_ISSSTE, Int32 Fecha_redencion, DateTime pFechaCorte, Double pComisionAportacion, Double pDensidadCotizacion, Double pEdadRetiro, Double Tr, Int32 nMesesRetiro)
        {

            Double n, k;
            Double F, varURV, Cs_mensual, Av_mensual, Ao, Anualidad_aport;


            //Suponiendo una edad de retiro
            Fecha_retiro = new DateTime(pFechaNacimiento.Year, pFechaNacimiento.Month, pFechaNacimiento.Day).AddYears(Convert.ToInt32(pEdadRetiro));

            //Valor del salario mínimo que será considerado para obtener el nivel salarial del trabajador
            //SMGVDF = 67.29; // RARS

            SMGVDF = 0;

            // PGO Salarios Minimos si es calculadora IMSS
            //MC1
            if (this.Tipo == TipoConsulta.IMSS)
            {
                SMGVDF = this.IM01SMGVDF;
            }

            // PGO Salarios Minimos si es calculadora INDEPENDIENTE (No implementado)
            /*  else if (this.Tipo == TipoConsulta.IND)
              {
                  SMGVDF = this.IN02SMGVDF;
              }*/

            //Cálculo para conocer el número de salarios mínimos que percibe el trabajador
            Nivel_Salarial = pSalarioBaseCotizacionMensual / (30 * SMGVDF);

            //Meses que faltan para llegar a la edad de retiro
            n = Fn_n(Fecha_retiro, pFechaCorte);

            //Meses que hay desde la fecha de redención del Bono a la fecha de la edad de retiro
            k = Fn_k(Fecha_retiro, Fecha_redencion);

            //Es el factor de (1+ r_mensual)(1 - c_mensual)
            //F = (1 + Rendimiento_mensual(pRendimiento)) * (1 - Comision_mensual(pComisionAfore)) -1;
            double ComisionMensual = Comision_mensual(pComisionAfore);
            F = (1 + pRendimiento) * (1 - ComisionMensual) - 1;

            varURV = URVe(pGenero, pEdadRetiro);

            //Cuota Social mensual
            Cs_mensual = Cuota_Social_Mensual(Tipo_de_Trabajador, pDensidadCotizacion, pFechaCorte) / pDensidadCotizacion;

            //Aportación 6.5%
            Ao = pSalarioBaseCotizacionMensual * pComisionAportacion;

            //Anualidad de las aportaciones
            Anualidad_aport = (Math.Pow(F, n) - 1) / (F - 1);

            // Sye - alejandro.guevara 
            double SumaAportaciones = A1 + A2 + A3 + CSE_t;
            // Sye - alejandro.guevara | constante equivalente al 2%
            double alfa_a = 0.02;

            //Ahorro voluntario mensual
            //Av_mensual = ((((Tr) * (pSalarioBaseCotizacionMensual) * (12 * varURV)) - (Bono_ISSSTE * Math.Pow(F, k)) - (pSaldoActualAfore * Math.Pow(F, n))) / (pDensidadCotizacion * Anualidad_aport)) - Ao - Cs_mensual;
            //Sye - Alejandro.Guevara 
            Av_mensual = (((pSalarioBaseCotizacionMensual * Tr) * (1 + alfa_a) * varURV * 12) - pSaldoActualAfore * (Math.Pow(1 + F, nMesesRetiro)) - pDensidadCotizacion * (A1 + A2 + A3 + CSE_t)) / (pDensidadCotizacion * (((Math.Pow(1 + F, nMesesRetiro)) - 1) / F));

            return Av_mensual;

        }

        //Transforma el rendimiento anual en rendimiento mensual
        public double Rendimiento_mensual(double Rendimiento_anual)
        {
            Double varRendimientoMensual = 0;
            Double nolohace = 1.0 / 12;
            varRendimientoMensual = Math.Pow(1 + Rendimiento_anual, nolohace) - 1;

            return varRendimientoMensual;
        }

        //Función que transforma la comisión anual en comisión mensual, de acuerdo a lo que dice la circular 12-11
        public double Comision_mensual(double Comision_anual)
        {
            return Comision_anual / 12;
        }

        public double Tasa_rendimiento_mensual(double Rendimiento_mensual, double Comision_mensual)
        {

            return (1 + Rendimiento_mensual) * (1 - Comision_mensual) - 1;
        }

        //Funcion que calcula el número entero de meses que faltan para que el trabajador cumpla la edad de retiro, a partir de la fecha de corte ''''''CAMBIO''''''
        //Es el número de meses que se capitalizará el Saldo inicial y las aportaciones Aportaciones obligatorias, voluntarias y si aplica de ahorro solidario
        public Double Fn_n(DateTime Fecha_retiro, DateTime Fecha_corte)
        {
            Double varFn_n = 0;
            Util oUtil = new Util();

            TimeSpan tDias2 = Fecha_retiro.Subtract(Fecha_corte);
            Double dias2 = Convert.ToDouble(tDias2.Days);


            if (dias2 > 0)
            {
                varFn_n = Math.Round((dias2 * 12) / 365);
            }
            else
            {
                varFn_n = 0;
            }

            return varFn_n;
        }

        //Función que calcula el número entero de meses que faltan para que el trabajador cumpla la edad de retiro, a partir de la fecha de redención  '''''CAMBIO'''''
        //Es el número de meses que se capitalizará el monto correspondiente al Bono de Pensión ISSSTE
        public Double Fn_k(DateTime Fecha_retiro, Int32 Fecha_redencion)
        {
            Double varFn_k = 0;
            Util oUtil = new Util();

            if (Fecha_redencion != 0)
            {

                //TimeSpan tDias2 = Fecha_retiro.Subtract(Fecha_redencion);
                //Double dias2 = Convert.ToDouble(tDias2.Days);

                //if (dias2 > 0)
                //{
                //    varFn_k = oUtil.RoundDown((dias2 * 12) / 365, 0);
                //}
                //else
                //{
                //    varFn_k = 0;
                //}
            }
            else
            {
                varFn_k = 0;
            }
            return varFn_k;
        }

        //Valor de la Unidad de Renta Vitalicia a la edad de retiro dependiendo del género seleccionado '''''CAMBIO''''''
        public double URVe(Double Género_Trabajador, Double Edad_retiro)
        {
            Double varURV = 0;
            //URV = Application.WorksheetFunction.Index(Sheets("Parámetros").Range("B4:C54"), Application.WorksheetFunction.Match(Edad_retiro, Sheets("Parámetros").Range("A4:A54"), 0), Género_Trabajador + 1); //''''CAMBIO''''''
            DataTable dtAfores = new DataTable();
            //SqlConnection cn = new SqlConnection("Data Source=172.16.10.23\\dwwebconsar;Initial Catalog=BD_SQL_CALCULADORA_AV;User ID=calculadora_av;Password=c41cu14d0r4_4v;");
            SqlConnection cn = new SqlConnection(RNCalculadora.ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT VALOR FROM dbo.BDE_C_URENTA_VITALICIA WHERE EDAD = " + Edad_retiro + " AND ID_SEXO = " + Género_Trabajador, cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            da.Fill(dtAfores);
            varURV = Convert.ToDouble(dtAfores.Rows[0][0]);

            return varURV;
        }

        //Función que calcula las aportaciones de RCV + Porcentaje de Ahorro Voluntario + Porcentaje de Ahorro Solidario en base del instituto al que el trabajador cotiza
        //Este es un monto mensual
        public double Aportaciones(int Tipo_de_Trabajador, double Salario_mensual, double Porc_Ao, double Porc_Av, double Porc_AS, double densidad_cot)
        {
            Double varAportaciones = 0;
            switch (Tipo_de_Trabajador)
            {
                case 0: //Trabajador IMSS
                    varAportaciones = (Porc_Ao * Salario_mensual);
                    break;
                case 1: //Trabajador ISSSTE
                    varAportaciones = ((Porc_Ao * Salario_mensual) + (Porc_Av * Salario_mensual) + (Porc_AS * 4.25 * Salario_mensual)) * densidad_cot;
                    break;


            }
            return varAportaciones;
        }


        //PGO - ODT-07 Función para calcular la acumulación por aportaciones durante el periodo 1
        public double AcumulacionAP1(double aObligatoriaIniMensual, double CuotaSocial, double tRendimientoMensual, int n, int n1, DateTime fCambio1)
        {
            Double aCumulaAportaciones = 0;
            DateTime Fecha1 = new DateTime(2022, 12, 31, 0, 0, 0);

            if (fCambio1 <= Fecha1)
            {
                aCumulaAportaciones = (aObligatoriaIniMensual + cuota_social) * ((Math.Pow(1 + tRendimientoMensual, n1) - 1) / tRendimientoMensual) * (Math.Pow(1 + tRendimientoMensual, n - n1));
                //aCumulaAportaciones = (aObligatoriaIniMensual + cuota_social) * ((1 + Math.Pow(tRendimientoMensual, n1))/tRendimientoMensual) * (1 + Math.Pow((tRendimientoMensual), n-n1));
            }
            else
            {
                aCumulaAportaciones = 0;
            }


            return aCumulaAportaciones;
        }

        //Función que calcula el monto de la cuota social mensual en función al nivel salarial y tipo de trabajador
        public double Cuota_Social_Mensual(int Tipo_de_Trabajador, double densidad_cot, DateTime FCorte)
        {
            Double Cuota_Social = 0;
            Double CSI = 0;

            DataTable dtAfores = new DataTable();
            DataTable dtAforesUMA = new DataTable();
            DataTable dtAforesISSSTE = new DataTable();

            //SqlConnection cn = new SqlConnection(@"Data Source=172.16.10.23\dwwebconsar;Initial Catalog=BD_SQL_CALCULADORA_AV;User ID=calculadora_av;Password=c41cu14d0r4_4v;");
            SqlConnection cn = new SqlConnection(RNCalculadora.ConnectionString);
            //SqlCommand cmdUMA = new SqlCommand("SELECT T_VALOR_PAR FROM dbo.BDE_C_PARAMETROS WHERE '" + FCorte.ToString("yyyyMMdd") + "'>=FECH_INI_VIG and '" + FCorte.ToString("yyyyMMdd") + "'<=FECH_FIN_VIG", cn);
            SqlCommand cmdUMA = new SqlCommand("SELECT T_VALOR_PAR FROM dbo.BDE_C_PARAMETROS WHERE N_ID_PAR = 40", cn);
            SqlDataAdapter daUMA = new SqlDataAdapter(cmdUMA);
            daUMA.SelectCommand = cmdUMA;
            daUMA.Fill(dtAforesUMA);

            //SqlCommand cmd = new SqlCommand("SELECT T_VALOR_PAR FROM dbo.BDE_C_PARAMETROS WHERE VIG_FLAG = 1 AND N_ID_PAR IN (1,2,3,4,5)", cn);
            SqlCommand cmd = new SqlCommand("SELECT T_VALOR_PAR FROM dbo.BDS_C_CUOTA_SOCIAL WHERE VIG_FLAG = 1 AND N_ID_PAR IN (1,2,3,4,5,6)", cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            da.Fill(dtAfores);

            SqlCommand cmdISSSTE = new SqlCommand("SELECT T_VALOR_PAR FROM dbo.BDE_C_PARAMETROS WHERE VIG_FLAG = 1 AND N_ID_PAR IN (6)", cn);
            SqlDataAdapter daISSSTE = new SqlDataAdapter(cmdISSSTE);
            daISSSTE.SelectCommand = cmdISSSTE;
            daISSSTE.Fill(dtAforesISSSTE);

            if (dtAforesUMA.Rows.Count == 0)
                valor_UMA = 0;
            else
                valor_UMA = Double.Parse(dtAforesUMA.Rows[0][0].ToString());

            switch (Tipo_de_Trabajador)
            {
                //PGO - ODT- 07
                case 0: //Montos de cuota social diaria para Trabajador IMSS
                    if (salario_diario <= SMGVDF)
                    {
                        //Cuota_Social = 6.24636
                        Cuota_Social = Convert.ToDouble(dtAfores.Rows[0][0]);
                    }

                    else if (SMGVDF < salario_diario && salario_diario <= (4 * valor_UMA))
                    {
                        //Cuota_Social = 5.98609;
                        Cuota_Social = Convert.ToDouble(dtAfores.Rows[1][0]);
                    }

                    else if ((4 * valor_UMA) < salario_diario && salario_diario <= (7 * valor_UMA))
                    {
                        //Cuota_Social = 5.72583;
                        Cuota_Social = Convert.ToDouble(dtAfores.Rows[2][0]);
                    }

                    else if ((7 * valor_UMA) < salario_diario && salario_diario <= (10 * valor_UMA))
                    {
                        //Cuota_Social = 5.46556;
                        Cuota_Social = Convert.ToDouble(dtAfores.Rows[3][0]);
                    }

                    else if ((10 * valor_UMA) < salario_diario && salario_diario <= (15 * valor_UMA))
                    {
                        //Cuota_Social = 5.20530;
                        Cuota_Social = Convert.ToDouble(dtAfores.Rows[4][0]);
                    }
                    else if (salario_diario > (15 * valor_UMA))
                    {
                        //Cuota_Social = 0;
                        Cuota_Social = Convert.ToDouble(dtAfores.Rows[5][0]);
                    }
                    else
                    {
                        Cuota_Social = 0;
                    }

                    //CSI = Cuota_Social * 30 * densidad_cot;
                    CSI = Cuota_Social * 30.4;

                    break;
                case 1: //Montos de cuota social diaria para Trabajador ISSSTE
                    if (Nivel_Salarial > 0 & Nivel_Salarial <= 10)
                    {
                        //Cuota_Social = 3.95504;
                        Cuota_Social = Convert.ToDouble(dtAforesISSSTE.Rows[0][0]);
                    }
                    else
                    {
                        Cuota_Social = 0;
                    }

                    CSI = Cuota_Social * 30 * densidad_cot;

                    break;
            }


            cuota_social = CSI;

            return CSI;
        }


        //PGO ODT-07 - Función  para calcular la aportación base a partir del año 2023 respecto al rango salarial K
        public double Aportacion_Base(int Tipo_de_Trabajador)
        {
            Double AportacionBase = 0;

            DataTable dtApBase = new DataTable();
            //  DataTable dtAforesUMA = new DataTable();

            SqlConnection cn = new SqlConnection(RNCalculadora.ConnectionString);

            //SqlCommand cmdUMA = new SqlCommand("SELECT T_VALOR_PAR FROM dbo.BDE_C_PARAMETROS WHERE N_ID_PAR = 40", cn);
            //SqlDataAdapter daUMA = new SqlDataAdapter(cmdUMA);
            //daUMA.SelectCommand = cmdUMA;
            //daUMA.Fill(dtAforesUMA);

            SqlCommand cmd = new SqlCommand("SELECT T_VALOR_PAR FROM dbo.BDS_C_CUOTA_SOCIAL WHERE VIG_FLAG = 1 AND N_ID_PAR IN (7,8,9,10,11,12,13,14)", cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            da.Fill(dtApBase);


            //if (dtAforesUMA.Rows.Count == 0)
            //    valor_UMA = 0;
            //else
            //    valor_UMA = Double.Parse(dtAforesUMA.Rows[0][0].ToString());

            switch (Tipo_de_Trabajador)
            {
                //PGO - ODT- 07
                case 0: //Montos de cuota social diaria para Trabajador IMSS
                    if (salario_diario <= SMGVDF)
                    {
                        //Aportación base = 6.275000 %
                        AportacionBase = Convert.ToDouble(dtApBase.Rows[0][0]);
                    }

                    else if (((1 * SMGVDF) < salario_diario) && (salario_diario <= (1.50 * valor_UMA)))
                    {
                        //Aportación base = 6.406500 %
                        AportacionBase = Convert.ToDouble(dtApBase.Rows[1][0]);
                    }

                    else if (((1.50 * valor_UMA) < salario_diario) && (salario_diario <= (2.00 * valor_UMA)))
                    {
                        //Aportación base = 6.700250 %
                        AportacionBase = Convert.ToDouble(dtApBase.Rows[2][0]);
                    }

                    else if (((2.00 * valor_UMA) < salario_diario) && (salario_diario <= (2.50 * valor_UMA)))
                    {
                        //Aportación base = 6.876500 %
                        AportacionBase = Convert.ToDouble(dtApBase.Rows[3][0]);
                    }

                    else if (((2.50 * valor_UMA) < salario_diario) && (salario_diario <= (3.00 * valor_UMA)))
                    {
                        //Aportación base = 6.994000 %
                        AportacionBase = Convert.ToDouble(dtApBase.Rows[4][0]);
                    }
                    else if (((3.00 * valor_UMA) < salario_diario) && (salario_diario <= (3.50 * valor_UMA)))
                    {
                        //Aportación base = 7.077875%
                        AportacionBase = Convert.ToDouble(dtApBase.Rows[5][0]);
                    }
                    else if (((3.50 * valor_UMA) < salario_diario) && (salario_diario <= (4.00 * valor_UMA)))
                    {
                        //Aportación base = 7.140875 %
                        AportacionBase = Convert.ToDouble(dtApBase.Rows[6][0]);
                    }
                    else if (((4.00 * valor_UMA) < salario_diario) && (salario_diario <= (25 * valor_UMA)))
                    {
                        //Aportación base = 7.365625 %
                        AportacionBase = Convert.ToDouble(dtApBase.Rows[7][0]);
                    }

                    break;
            }

            return AportacionBase;
        }


        //PGO ODT-07 - Función  para calcular el Gradiente conforme al que crece la aportación base Ak de acuerdo al rango salarial definido por k 
        public double Gradiente(int Tipo_de_Trabajador)
        {
            Double ValorGradiente = 0;

            DataTable dtGradiente = new DataTable();
            //  DataTable dtAforesUMA = new DataTable();

            SqlConnection cn = new SqlConnection(RNCalculadora.ConnectionString);

            //SqlCommand cmdUMA = new SqlCommand("SELECT T_VALOR_PAR FROM dbo.BDE_C_PARAMETROS WHERE N_ID_PAR = 40", cn);
            //SqlDataAdapter daUMA = new SqlDataAdapter(cmdUMA);
            //daUMA.SelectCommand = cmdUMA;
            //daUMA.Fill(dtAforesUMA);

            SqlCommand cmd = new SqlCommand("SELECT T_VALOR_PAR FROM dbo.BDS_C_CUOTA_SOCIAL WHERE VIG_FLAG = 1 AND N_ID_PAR IN (15,16,17,18,19,20,21,22)", cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            da.Fill(dtGradiente);


            //if (dtAforesUMA.Rows.Count == 0)
            //    valor_UMA = 0;
            //else
            //    valor_UMA = Double.Parse(dtAforesUMA.Rows[0][0].ToString());

            switch (Tipo_de_Trabajador)
            {
                //PGO - ODT- 07
                case 0: //Montos de cuota social diaria para Trabajador IMSS
                    if (salario_diario <= SMGVDF)
                    {
                        // Gradiente = 0.000000 %
                        ValorGradiente = Convert.ToDouble(dtGradiente.Rows[0][0]);
                    }

                    else if (((1.00 * SMGVDF) < salario_diario) && (salario_diario <= (1.50 * valor_UMA)))
                    {
                        // Gradiente = 0.131500 %
                        ValorGradiente = Convert.ToDouble(dtGradiente.Rows[1][0]);
                    }

                    else if (((1.50 * valor_UMA) < salario_diario) && (salario_diario <= (2.00 * valor_UMA)))
                    {
                        // Gradiente = 0.425250 %
                        ValorGradiente = Convert.ToDouble(dtGradiente.Rows[2][0]);
                    }

                    else if (((2.00 * valor_UMA) < salario_diario) && (salario_diario <= (2.50 * valor_UMA)))
                    {
                        // Gradiente = 0.601500 %
                        ValorGradiente = Convert.ToDouble(dtGradiente.Rows[3][0]);
                    }

                    else if (((2.50 * valor_UMA) < salario_diario) && (salario_diario <= (3.00 * valor_UMA)))
                    {
                        // Gradiente = 0.719000 %
                        ValorGradiente = Convert.ToDouble(dtGradiente.Rows[4][0]);
                    }
                    else if (((3.00 * valor_UMA) < salario_diario) && (salario_diario <= (3.50 * valor_UMA)))
                    {
                        // Gradiente = 0.802875 %
                        ValorGradiente = Convert.ToDouble(dtGradiente.Rows[5][0]);
                    }
                    else if (((3.50 * valor_UMA) < salario_diario) && (salario_diario <= (4.00 * valor_UMA)))
                    {
                        // Gradiente = 0.865875 %
                        ValorGradiente = Convert.ToDouble(dtGradiente.Rows[6][0]);
                    }
                    else if (((4.00 * valor_UMA) < salario_diario) && (salario_diario <= (25 * valor_UMA)))
                    {
                        // Gradiente = 1.090625 %
                        ValorGradiente = Convert.ToDouble(dtGradiente.Rows[7][0]);
                    }

                    break;
            }

            return ValorGradiente;
        }


        //PGO ODT-07 - Función  para calcular el montyo mensual en pesos por concepto de cuota social que se otorgará a partir de 2023 de acuerdo al rango salarial k
        public double Cuota_Social_CSk(int Tipo_de_Trabajador)
        {
            Double CSk = 0;

            DataTable dtCSk = new DataTable();

            SqlConnection cn = new SqlConnection(RNCalculadora.ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT T_VALOR_PAR FROM dbo.BDS_C_CUOTA_SOCIAL WHERE VIG_FLAG = 1 AND N_ID_PAR IN (23,24,25,26,27,28,29,30)", cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            da.Fill(dtCSk);


            switch (Tipo_de_Trabajador)
            {
                //PGO - ODT- 07
                case 0: //Montos de cuota social diaria para Trabajador IMSS
                    if (salario_diario <= SMGVDF)
                    {
                        //Cuota social diaria = $10.75000
                        CSk = Convert.ToDouble(dtCSk.Rows[0][0]);
                    }

                    else if (((1 * valor_UMA) < salario_diario) && (salario_diario <= (1.50 * valor_UMA)))
                    {
                        //Cuota social diaria = $10.00000
                        CSk = Convert.ToDouble(dtCSk.Rows[1][0]);
                    }

                    else if (((1.50 * valor_UMA) < salario_diario) && (salario_diario <= (2.00 * valor_UMA)))
                    {
                        //Cuota social diaria = $9.25000
                        CSk = Convert.ToDouble(dtCSk.Rows[2][0]);
                    }

                    else if (((2.00 * valor_UMA) < salario_diario) && (salario_diario <= (2.50 * valor_UMA)))
                    {
                        //Cuota social diaria = $8.50000
                        CSk = Convert.ToDouble(dtCSk.Rows[3][0]);
                    }

                    else if (((2.50 * valor_UMA) < salario_diario) && (salario_diario <= (3.00 * valor_UMA)))
                    {
                        //Cuota social diaria = $7.75000
                        CSk = Convert.ToDouble(dtCSk.Rows[4][0]);
                    }
                    else if (((3.00 * valor_UMA) < salario_diario) && (salario_diario <= (3.50 * valor_UMA)))
                    {
                        //Cuota social diaria = $7.00000
                        CSk = Convert.ToDouble(dtCSk.Rows[5][0]);
                    }
                    else if (((3.50 * valor_UMA) < salario_diario) && (salario_diario <= (4.00 * valor_UMA)))
                    {
                        //Cuota social diaria = $6.25000
                        CSk = Convert.ToDouble(dtCSk.Rows[6][0]);
                    }
                    else if (((4.00 * valor_UMA) < salario_diario) && (salario_diario <= (25 * valor_UMA)))
                    {
                        //Cuota social diaria = $0
                        CSk = Convert.ToDouble(dtCSk.Rows[7][0]);
                    }

                    break;
            }

            return CSk;
        }


        //PGO ODT-07 - Cuota Social especial que sólo se entregará durante 2023 a las personas cuyo salario se encuentre dentro del rango salarial h
        public double Cuota_Social_Especial(int Tipo_de_Trabajador)
        {
            Double CuotaSocialEspecial = 0;

            DataTable dtApBase = new DataTable();
            SqlConnection cn = new SqlConnection(RNCalculadora.ConnectionString);


            SqlCommand cmd = new SqlCommand("SELECT T_VALOR_PAR FROM dbo.BDS_C_CUOTA_SOCIAL WHERE VIG_FLAG = 1 AND N_ID_PAR IN (31, 32, 33)", cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            da.Fill(dtApBase);



            switch (Tipo_de_Trabajador)
            {
                //PGO - ODT- 07
                case 0:
                    if (((4.00 * valor_UMA) < salario_diario) && (salario_diario <= (5.00 * valor_UMA)))
                    {
                        //Cuota social especial diaria = 2.45
                        CuotaSocialEspecial = Convert.ToDouble(dtApBase.Rows[0][0]);
                    }

                    else if (((5.00 * valor_UMA) < salario_diario) && (salario_diario <= (6.00 * valor_UMA)))
                    {
                        //Cuota social especial diaria = 1.8
                        CuotaSocialEspecial = Convert.ToDouble(dtApBase.Rows[1][0]);
                    }

                    else if (((6.00 * valor_UMA) < salario_diario) && (salario_diario <= (7.09 * valor_UMA)))
                    {
                        //Cuota social especial diaria = 1
                        CuotaSocialEspecial = Convert.ToDouble(dtApBase.Rows[2][0]);
                    }

                    break;
            }

            return CuotaSocialEspecial;
        }


        //PGO ODT-07 - Obtener Rango de semenas cotizadas que le corresponden a cada caso
        public double Rango_Semanas_Cotizadas(int AnioRetiro, double SCER)
        {
            Double RangoSemanas = 0;
            Double SemMinima = 0;
            Double SemMaxima = 0;

            DataTable dtApBase = new DataTable();
            SqlConnection cn = new SqlConnection(RNCalculadora.ConnectionString);
            SqlCommand cmd;

            if (AnioRetiro >= 2031)
            {
                cmd = new SqlCommand("SELECT ID_RANGO, T_SEM_MIN, T_SEM_MAX FROM dbo.BDS_C_RANGO_SEM_COTIZADAS WHERE VIG_FLAG = 1 AND ANIO_RETIRO =" + 2031, cn);
            }
            else
            {
                cmd = new SqlCommand("SELECT ID_RANGO, T_SEM_MIN, T_SEM_MAX FROM dbo.BDS_C_RANGO_SEM_COTIZADAS WHERE VIG_FLAG = 1 AND ANIO_RETIRO =" + AnioRetiro, cn);
            }


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            da.Fill(dtApBase);

            int contador = 0;
            foreach (DataRow row in dtApBase.Rows)
            {
                SemMinima = Convert.ToDouble(dtApBase.Rows[contador][1]);

                if (dtApBase.Rows[contador][2] != DBNull.Value)
                {
                    SemMaxima = Convert.ToDouble(dtApBase.Rows[contador][2]);
                }

                if (SemMinima <= SCER && SCER <= SemMaxima)
                {
                    RangoSemanas = Convert.ToDouble(dtApBase.Rows[contador][0]);
                }
                else if (SemMinima <= SCER && dtApBase.Rows[contador][2] == DBNull.Value)
                {
                    RangoSemanas = Convert.ToDouble(dtApBase.Rows[contador][0]);
                }

                contador++;

            }
            return RangoSemanas;
        }



        //PGO ODT-07 - Obtener Montos de Pensión Garantizada
        public double Monto_PGarantizada(double RangoSemanasCotizadas, double EdadRetiro, int RangoSalarialPG)
        {
            Double MontoPG = 0;


            DataTable dtApBase = new DataTable();
            SqlConnection cn = new SqlConnection(RNCalculadora.ConnectionString);
            SqlCommand cmd;

            if (EdadRetiro >= 65)
            {
                cmd = new SqlCommand("SELECT MONTO_PG FROM dbo.BDS_C_PENSION_GARANTIZADA WHERE ID_RANGO = " + RangoSemanasCotizadas + " AND BLOQUE_RANGO_SALARIAL = " + RangoSalarialPG + " and EDAD_RETIRO >= 65 and VIG_FLAG = 1", cn);
            }
            else
            {
                cmd = new SqlCommand("SELECT MONTO_PG FROM dbo.BDS_C_PENSION_GARANTIZADA WHERE ID_RANGO = " + RangoSemanasCotizadas + " AND BLOQUE_RANGO_SALARIAL = " + RangoSalarialPG + " and EDAD_RETIRO = " + EdadRetiro + " and VIG_FLAG = 1", cn);
            }


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            da.Fill(dtApBase);


            MontoPG = Convert.ToDouble(dtApBase.Rows[0][0]);

            return MontoPG;
        }


        //PGO ODT-07 - Determinar a los trabajadores con Negativa de Pensión
        public double SemanasNegPension(int AnioRetiro)
        {
            Double RequisitoSC = 0;

            DataTable dtApBase = new DataTable();
            SqlConnection cn = new SqlConnection(RNCalculadora.ConnectionString);
            SqlCommand cmd;

            if (AnioRetiro >= 2031)
            {
                cmd = new SqlCommand("SELECT REQ_SEMANAS FROM dbo.BDS_C_NEGATIVA_PENSION WHERE VIG_FLAG = 1 AND ANIO_RETIRO =" + 2031, cn);
            }
            else
            {
                cmd = new SqlCommand("SELECT REQ_SEMANAS FROM dbo.BDS_C_NEGATIVA_PENSION WHERE VIG_FLAG = 1 AND ANIO_RETIRO =" + AnioRetiro, cn);
            }


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            da.Fill(dtApBase);
            RequisitoSC = Convert.ToDouble(dtApBase.Rows[0][0]);

            return RequisitoSC;
        }





        /// <summary>
        /// Retorna la diferencia entre dos fechas en la forma x años, x meses, x días
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        private static bool GetDateFormat(DateTime startDate, DateTime endDate, string mensaje)
        {

            bool status = false;
            /*Valida fecha*/
            if (startDate.Date <= endDate.Date)
            {
                status = true;
                TimeSpan difference = endDate.Subtract(startDate.Date);

                StringBuilder sb = new StringBuilder();

                if (difference.Ticks == 0)
                {
                    sb.Append("0 días");
                }
                else if (difference.Ticks > 0)
                {
                    // This is to convert the timespan to datetime object
                    DateTime totalDate = DateTime.MinValue + difference;

                    int differenceInYears = totalDate.Year - 1;
                    int differenceInMonths = totalDate.Month - 1;
                    int differenceInDays = totalDate.Day - 1;

                    if (differenceInYears > 0)
                        sb.AppendFormat("{0} año(s)", differenceInYears);

                    if (differenceInMonths > 0)
                        if (differenceInMonths == 1)
                            sb.AppendFormat(" {0} mes", differenceInMonths);
                        else
                            sb.AppendFormat(" {0} meses", differenceInMonths);

                    if (differenceInDays > 0)
                        if (differenceInDays == 1)
                            sb.AppendFormat(" {0} día", differenceInDays);
                        else
                            sb.AppendFormat(" {0} días", differenceInDays);
                }

                mensaje = sb.ToString();
            }
            else
            {
                mensaje = "Error";
            }
            return status;

        }

        public static double getParametros()
        {
            var parametro = new Double();

            DataTable dt = new DataTable();
            try
            {
                SqlConnection cn = new SqlConnection(RNCalculadora.ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT T_VALOR_PAR FROM dbo.BDE_C_PARAMETROS WHERE N_ID_PAR = 7 and VIG_FLAG=1", cn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    parametro = Convert.ToDouble(row["T_VALOR_PAR"]);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return parametro;
        }

    }
}
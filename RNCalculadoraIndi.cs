using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CALCULADORA2014
{
    public class RNCalculadoraIndi
    {

        public static double RendimientoAnual = 0.04;

        private string _AhorroTotalEstimado;
        private string _PensionMensual;
        private List<LineaAhorro> _lstLineaAhorro;

        public string AhorroTotalEstimado
        {
            get { return _AhorroTotalEstimado; }
            set { _AhorroTotalEstimado = value; }
        }

        public string PensionMensual
        {
            get { return _PensionMensual; }
            set { _PensionMensual = value; }
        }
        public List<LineaAhorro> lstLineaAhorro
        {
            get { return _lstLineaAhorro; }
            set { _lstLineaAhorro = value; }
        }

        /// <summary>
        /// Valores de calculo de ahorro
        /// </summary>
        public class LineaAhorro
        {
            private double _AhorroAdicional;
            private string _AhorroTotalMensual;
            private string _AhorroAcumulado;
            private string _PensionMensual;


            public double AhorroAdicional
            {
                get { return _AhorroAdicional; }
                set { _AhorroAdicional = value; }
            }

            public string AhorroTotalMensual
            {
                get { return _AhorroTotalMensual; }
                set { _AhorroTotalMensual = value; }
            }

            public string AhorroAcumulado
            {
                get { return _AhorroAcumulado; }
                set { _AhorroAcumulado = value; }
            }
            public string PensionMensual
            {
                get { return _PensionMensual; }
                set { _PensionMensual = value; }
            }
        }

        /// <summary>
        /// Datos obtenidos de la captura
        /// </summary>
        public class FormularioCaptura
        {
            private int _EdadActual;
            private double _EdadRetiro;
            private double _AhorroMensual;
            private double _SaldoAFORE;
            private double _AhorroExtra;
            private double _SeleccionGenero;

            public int EdadActual
            {
                get { return _EdadActual; }
                set { _EdadActual = value; }
            }

            public double EdadRetiro
            {
                get { return _EdadRetiro; }
                set { _EdadRetiro = value; }
            }

            public double AhorroMensual
            {
                get { return _AhorroMensual; }
                set { _AhorroMensual = value; }
            }

            public double SaldoAFORE
            {
                get { return _SaldoAFORE; }
                set { _SaldoAFORE = value; }
            }

            public double AhorroExtra
            {
                get { return _AhorroExtra; }
                set { _AhorroExtra = value; }
            }
            public double SeleccionGenero
            {
                get { return _SeleccionGenero; }
                set { _SeleccionGenero = value; }
            }
        }

        /// <summary>
        /// Calcula una linea de ahorro con el formulario de captura
        /// </summary>
        /// <param name="objCaptura">Objeto formulario de captura</param>
        /// <returns></returns>
        public static LineaAhorro CalculaAhorroEstimado(FormularioCaptura objCaptura)
        {
            LineaAhorro objLineaAhorro = new LineaAhorro();

            double Sf = 0; //Saldo Acumulado
            double SaldoActual = 0; // Total del saldo actual,estimado a la edad de retiro
            double TotalAhorroMensual = 0; // Total del Ahorro mensual,estimado a la edad de retiro
            double Si = 0;  // Saldo actual de la cuenta individual
            double m = 0; // Número de meses que faltan para que el trabajador cumpla la edad de retiro
            double Avm = 0; // Monto en pesos de la aportación mensual realizada por el trabajador
            double rm = 0; // Rendimiento mensual previo al cobro de comisiones
            double cm = 0; //Comisión mensual vigente
            double varURV;   // Unidad de Renta Vitalicia

            Si = objCaptura.SaldoAFORE;
            m = (double)(objCaptura.EdadRetiro - objCaptura.EdadActual) * 12.0;
            Avm = objCaptura.AhorroMensual + objCaptura.AhorroExtra;
            rm = Math.Pow((1.0 + RendimientoAnual), (1.0/12.0)) - 1.0;
            cm = ComisionPromedioSIEFORES() / 12.0;
          

            TotalAhorroMensual = Avm * ((Math.Pow((1.0 + rm), m) * Math.Pow((1.0 - cm), m) - 1.0) / ((1.0 + rm) * (1.0 - cm) - 1.0) );

            SaldoActual = Si * Math.Pow((1.0 + rm), m) * Math.Pow((1.0 - cm), m);

            Sf = SaldoActual + TotalAhorroMensual;

            varURV = URV(objCaptura.SeleccionGenero, objCaptura.EdadRetiro); //.74

            objLineaAhorro.AhorroAdicional = Math.Round(objCaptura.AhorroExtra, 2);
            objLineaAhorro.AhorroTotalMensual = Math.Round(objCaptura.AhorroExtra + objCaptura.AhorroMensual, 2).ToString("C0");
            objLineaAhorro.AhorroAcumulado = Math.Round( Sf, 0).ToString("C0");
            objLineaAhorro.PensionMensual = Math.Round(Sf / ((12 * varURV))).ToString("C0");



            return objLineaAhorro;
        }

        //Valor de la Unidad de Renta Vitalicia a la edad de retiro dependiendo del género seleccionado '''''CAMBIO''''''
        public static double URV(Double Género_Trabajador, Double Edad_retiro)
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
        /// <summary>
        /// Realiza el calculo de las lineas de ahorro para lo capturado por el usuario
        /// Tambien se consideran incrementos de 50, 200 y 1000
        /// </summary>
        /// <param name="objCaptura"></param>
        /// <returns></returns>
        public static RNCalculadoraIndi CalculoTrabajadorIndependiente(FormularioCaptura objCaptura)
        {
            RNCalculadoraIndi objCalculoIndi = new RNCalculadoraIndi();

            objCalculoIndi.AhorroTotalEstimado = CalculaAhorroEstimado(objCaptura).AhorroAcumulado;
            objCalculoIndi.PensionMensual = CalculaAhorroEstimado(objCaptura).PensionMensual;
            objCalculoIndi.lstLineaAhorro = new List<LineaAhorro>();

            //objCalculoIndi.lstLineaAhorro.Add(CalculaAhorroEstimado(objCaptura));

            objCaptura.AhorroExtra = 100;
            objCalculoIndi.lstLineaAhorro.Add(CalculaAhorroEstimado(objCaptura));

            objCaptura.AhorroExtra = 300;
            objCalculoIndi.lstLineaAhorro.Add(CalculaAhorroEstimado(objCaptura));

            objCaptura.AhorroExtra = 1000;
            objCalculoIndi.lstLineaAhorro.Add(CalculaAhorroEstimado(objCaptura));

            return objCalculoIndi;
        }

        /// <summary>
        /// Obtiene el porcentaje de comision promedio de las SIEFORES
        /// </summary>
        /// <returns></returns>
        public static double ComisionPromedioSIEFORES()
        {
            Double ComisionPromedio = 0.0117;
            string strQuery = "SELECT PARAMETRO AS ComisionPromedio " +
                              " FROM BDS_C_PARAMETROS " +
                              " WHERE DESCRIPCION = 'Promedio_Comisiones_SIEFORES_Adicionales' " +
                              " AND VIG_FLAG = 1 ";

            SqlConnection cn;
            try
            {
                DataTable dtAfores = new DataTable();
                cn = new SqlConnection(RNCalculadora.ConnectionString);
                SqlCommand cmd = new SqlCommand(strQuery, cn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand = cmd;
                da.Fill(dtAfores);
                if (dtAfores.Rows.Count > 0)
                {
                    ComisionPromedio = Convert.ToDouble(dtAfores.Rows[0][0]);
                }
                cn.Close();
            }
            catch (Exception)
            {
                ComisionPromedio = 0.0117;
            }
            
            return ComisionPromedio;
        }



    }
}
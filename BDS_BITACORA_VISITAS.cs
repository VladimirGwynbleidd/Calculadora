using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CALCULADORA2014
{
    public class BDS_BITACORA_VISITAS
    {
        public Double ID_BITACORA_V;
        public String ID_SESION;
        public String IP;
        public Int32  CALCULADORA_ACTUAL;

        /// <summary>
        /// Lista de las calculadoras
        /// </summary>
        public const Int32 IMSS = 1;
        public const Int32 INDE = 2;
        public const Int32 ISSSTE = 3;

        private string ConnectionString;

        /// <summary>
        /// Llena la cadena de conexion y la calculadora actual
        /// </summary>
        /// <param name="piCalculadoraActual"></param>
        public BDS_BITACORA_VISITAS(int piCalculadoraActual)
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DbConection"].ConnectionString;
            this.CALCULADORA_ACTUAL = piCalculadoraActual;
        }

        public Int32 InsertarVisita()
        {
            String lsQuery = "";
            Int32 liRes = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(RNCalculadora.ConnectionString))
                {
                    cn.Open();
                    lsQuery = "Insert into BDS_BITACORA_VISITAS ([ID_SESION],[IP],[FECH_BITACORA],ID_CALCULADORA) values ('" + ID_SESION + "', '" + IP + "', GETDATE()," + this.CALCULADORA_ACTUAL.ToString() + ")";
                    SqlCommand cmd = new SqlCommand(lsQuery, cn);
                    liRes = cmd.ExecuteNonQuery();
                    cn.Close();
                    cn.Dispose();
                }
            }catch{
                liRes = 0;
            }

            return liRes;
        }

        public Int64 ObtieneTotalDeVisitas()
        {
            String lsQuery = "";
            Int64 liRes = 0;
            DataSet dt = new DataSet();
            try
            {
                using (SqlConnection cn = new SqlConnection(RNCalculadora.ConnectionString))
                {
                    cn.Open();

                    if (this.CALCULADORA_ACTUAL == BDS_BITACORA_VISITAS.IMSS)
                        lsQuery = "SELECT (COUNT(ID_BITACORA_V) + (CASE WHEN (SELECT T_VALOR_PAR FROM BDE_C_PARAMETROS WHERE T_DSC_PAR = 'CONTADOR_VISITAS_INICIAL_IMSS') IS NULL THEN 0 ELSE (SELECT CAST(T_VALOR_PAR AS NUMERIC) FROM BDE_C_PARAMETROS WHERE T_DSC_PAR = 'CONTADOR_VISITAS_INICIAL_IMSS') END)) CONT FROM BDS_BITACORA_VISITAS WHERE ID_CALCULADORA = " + this.CALCULADORA_ACTUAL.ToString();
                    else
                        lsQuery = "SELECT (COUNT(ID_BITACORA_V) + (CASE WHEN (SELECT T_VALOR_PAR FROM BDE_C_PARAMETROS WHERE T_DSC_PAR = 'CONTADOR_VISITAS_INICIAL_INDE') IS NULL THEN 0 ELSE (SELECT CAST(T_VALOR_PAR AS NUMERIC) FROM BDE_C_PARAMETROS WHERE T_DSC_PAR = 'CONTADOR_VISITAS_INICIAL_INDE') END)) CONT FROM BDS_BITACORA_VISITAS WHERE ID_CALCULADORA = " + this.CALCULADORA_ACTUAL.ToString();

                    SqlCommand cmd = new SqlCommand(lsQuery, cn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataTable ltTables in dt.Tables)
                    {
                        foreach (DataRow lrRow in ltTables.Rows)
                        {
                            Int64.TryParse(lrRow["CONT"].ToString(), out liRes);
                        }
                    }

                    cn.Close();
                    cn.Dispose();
                }
            }
            catch
            {
                liRes = 0;
            }

            return liRes;
        }

    }
}
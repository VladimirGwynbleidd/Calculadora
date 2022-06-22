using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace CALCULADORA2014
{
    public class BDS_BITACORA_VISITAS
    {
        public Double ID_BITACORA_V;
        public String ID_SESION;
        public String IP;
        public Int32 CALCULADORA_ACTUAL;

        /// <summary>
        /// Lista de las calculadoras
        /// </summary>
        public const Int32 IMSS = 1;
        public const Int32 INDE = 2;
        public const Int32 ISSSTE = 3;

        private string ConnectionString;

        public BDS_BITACORA_VISITAS()
        {
        }

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
            }
            catch
            {
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


        public Int32 InsertarDatosFormulario(double nSalarioBase, double nGenero, int nEdad, int nAnio)
        {
            String lsQuery = "";
            Int32 liRes = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(RNCalculadora.ConnectionString))
                {
                    cn.Open();
                    lsQuery = "Insert into BDS_GUARDAR_DATOS ([N_SALARIO_BASE],[N_ID_GENERO],[N_EDAD],[N_ANIO],[F_FECH_ACTUAL]) values (" + nSalarioBase + ", " + nGenero + ", " + nEdad + ", " + nAnio + ", GETDATE())";
                    SqlCommand cmd = new SqlCommand(lsQuery, cn);
                    liRes = cmd.ExecuteNonQuery();
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


        public int InsertarEncuestaSatisfaccionBuena(int ValorCarita, string chkInfoBuenaEncuesta1, string chkInfoBuenaEncuesta2, string chkInfoBuenaEncuesta3, string chkInfoBuenaEncuesta4, string txtArea)
        {
            String lsQuery = "";
            int liRes = 0;
            string N_ID_PREGUNTA = string.Empty;
            string T_DSC_ENCUESTA = string.Empty;
            try
            {

                if (chkInfoBuenaEncuesta1 != string.Empty && chkInfoBuenaEncuesta1 != null)
                {
                    N_ID_PREGUNTA = chkInfoBuenaEncuesta1;
                }
                else if (chkInfoBuenaEncuesta2 != string.Empty && chkInfoBuenaEncuesta2 != null)
                {
                    N_ID_PREGUNTA = chkInfoBuenaEncuesta2;
                }
                else if (chkInfoBuenaEncuesta3 != string.Empty && chkInfoBuenaEncuesta3 != null)
                {
                    N_ID_PREGUNTA = chkInfoBuenaEncuesta3;
                }
                else if (chkInfoBuenaEncuesta4 != string.Empty && chkInfoBuenaEncuesta4 != null)
                {
                    N_ID_PREGUNTA = chkInfoBuenaEncuesta4;
                }


                if (txtArea == string.Empty)
                    T_DSC_ENCUESTA = "NULL";
                else
                    T_DSC_ENCUESTA = txtArea;



                //T_DSC_ENCUESTA = txtArea == null && txtArea == string.Empty ? "NULL" : txtArea;

                using (SqlConnection cn = new SqlConnection(RNCalculadora.ConnectionString))
                {
                    cn.Open();
                    lsQuery = "INSERT INTO BDS_RESPUESTA_ENCUESTA VALUES (" + N_ID_PREGUNTA + ", " + ValorCarita + ", '" + T_DSC_ENCUESTA + "', GETDATE())";
                    SqlCommand cmd = new SqlCommand(lsQuery, cn);
                    liRes = cmd.ExecuteNonQuery();
                    cn.Close();
                    cn.Dispose();
                }
            }
            catch (Exception ex)
            {
                liRes = 0;
            }

            return liRes;
        }


        public int InsertarEncuestaSatisfaccionRegular(int ValorCarita, string chkInfoRegular1, string chkInfoRegular2, string chkInfoRegular3, string chkInfoRegular4, string idTxtAreaRegular)
        {
            String lsQuery = "";
            int liRes = 0;
            string N_ID_PREGUNTA = string.Empty;
            string T_DSC_ENCUESTA = string.Empty;
            try
            {

                if (chkInfoRegular1 != string.Empty && chkInfoRegular1 != null)
                {
                    N_ID_PREGUNTA = chkInfoRegular1;
                }
                else if (chkInfoRegular2 != string.Empty && chkInfoRegular2 != null)
                {
                    N_ID_PREGUNTA = chkInfoRegular2;
                }
                else if (chkInfoRegular3 != string.Empty && chkInfoRegular3 != null)
                {
                    N_ID_PREGUNTA = chkInfoRegular3;
                }
                else if (chkInfoRegular4 != string.Empty && chkInfoRegular4 != null)
                {
                    N_ID_PREGUNTA = chkInfoRegular4;
                }

                if (idTxtAreaRegular == string.Empty)
                    T_DSC_ENCUESTA = "NULL";
                else
                    T_DSC_ENCUESTA = idTxtAreaRegular;


                //T_DSC_ENCUESTA = idTxtAreaRegular == null && idTxtAreaRegular == string.Empty ? "NULL" : idTxtAreaRegular;

                using (SqlConnection cn = new SqlConnection(RNCalculadora.ConnectionString))
                {
                    cn.Open();
                    lsQuery = "INSERT INTO BDS_RESPUESTA_ENCUESTA VALUES (" + N_ID_PREGUNTA + ", " + ValorCarita + ", '" + T_DSC_ENCUESTA + "', GETDATE())";
                    SqlCommand cmd = new SqlCommand(lsQuery, cn);
                    liRes = cmd.ExecuteNonQuery();
                    cn.Close();
                    cn.Dispose();
                }
            }
            catch (Exception ex)
            {
                liRes = 0;
            }

            return liRes;
        }

        public int InsertarEncuestaSatisfaccionBad(int ValorCarita, string chkInfoBad1, string chkInfoBad2, string chkInfoBad3, string chkInfoBad4, string idTxtAreaBad)
        {
            String lsQuery = "";
            int liRes = 0;
            string N_ID_PREGUNTA = string.Empty;
            string T_DSC_ENCUESTA = string.Empty;
            try
            {

                if (chkInfoBad1 != string.Empty && chkInfoBad1 != null)
                {
                    N_ID_PREGUNTA = chkInfoBad1;
                }
                else if (chkInfoBad2 != string.Empty && chkInfoBad2 != null)
                {
                    N_ID_PREGUNTA = chkInfoBad2;
                }
                else if (chkInfoBad3 != string.Empty && chkInfoBad3 != null)
                {
                    N_ID_PREGUNTA = chkInfoBad3;
                }
                else if (idTxtAreaBad != string.Empty && idTxtAreaBad != null)
                {
                    N_ID_PREGUNTA = chkInfoBad4;
                }


                if (idTxtAreaBad == string.Empty)
                    T_DSC_ENCUESTA = "NULL";
                else
                    T_DSC_ENCUESTA = idTxtAreaBad;



                using (SqlConnection cn = new SqlConnection(RNCalculadora.ConnectionString))
                {
                    cn.Open();
                    lsQuery = "INSERT INTO BDS_RESPUESTA_ENCUESTA VALUES (" + N_ID_PREGUNTA + ", " + ValorCarita + ", '" + T_DSC_ENCUESTA + "', GETDATE())";
                    SqlCommand cmd = new SqlCommand(lsQuery, cn);
                    liRes = cmd.ExecuteNonQuery();
                    cn.Close();
                    cn.Dispose();
                }
            }
            catch (Exception ex)
            {
                liRes = 0;
            }

            return liRes;
        }

        public List<CLASESGenerales.Encuesta> LLamarOpcionSatisfaccion(CLASESGenerales.Encuesta encuesta)
        {
            List<CLASESGenerales.Encuesta> LstEncuesta = new List<CLASESGenerales.Encuesta>();
            String lsQuery = "";
            DataTable dtEncuestas = new DataTable();


            try
            {
                using (SqlConnection cn = new SqlConnection(RNCalculadora.ConnectionString))
                {
                    cn.Open();
                    lsQuery = "SELECT  * from [dbo].[BDS_C_CAMPOS_ENCUESTA] WHERE VIG_FLAG=1 AND N_ID_PREGUNTA=" + encuesta.N_ID_PREGUNTA;
                    SqlCommand cmd = new SqlCommand(lsQuery, cn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.SelectCommand = cmd;
                    da.Fill(dtEncuestas);
                }

                LstEncuesta = DataTableToList<CLASESGenerales.Encuesta>(dtEncuestas).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return LstEncuesta;
        }


        private readonly IDictionary<Type, ICollection<PropertyInfo>> _Properties =
      new Dictionary<Type, ICollection<PropertyInfo>>();


        public IEnumerable<T> DataTableToList<T>(DataTable table) where T : class, new()
        {
            try
            {
                var objType = typeof(T);
                ICollection<PropertyInfo> properties;

                lock (_Properties)
                {
                    if (!_Properties.TryGetValue(objType, out properties))
                    {
                        properties = objType.GetProperties().Where(property => property.CanWrite).ToList();
                        _Properties.Add(objType, properties);
                    }
                }

                var list = new List<T>(table.Rows.Count);

                foreach (var row in table.AsEnumerable())
                {
                    var obj = new T();

                    foreach (var prop in properties)
                    {
                        try
                        {
                            var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            var safeValue = row[prop.Name] == null ? null : Convert.ChangeType(row[prop.Name], propType);

                            prop.SetValue(obj, safeValue, null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return Enumerable.Empty<T>();
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CALCULADORA2014
{
    public class CLASESGenerales
    {
        public class CLASEAhorroVoluntario
        {
            private String PensionMensual = String.Empty;

            public String PensionMensual_
            {
                get { return PensionMensual; }
                set { PensionMensual = value; }
            }
            private String TasaReemplazo = String.Empty;

            public String TasaReemplazo_
            {
                get { return TasaReemplazo; }
                set { TasaReemplazo = value; }
            }
            private String AhorroMensual = String.Empty;

            public String AhorroMensual_
            {
                get { return AhorroMensual; }
                set { AhorroMensual = value; }
            }
        }

        public class CLASEPensionMensualTasa
        {
            private String Texto = String.Empty;

            public String Texto_
            {
                get { return Texto; }
                set { Texto = value; }
            }
            private String Numero = String.Empty;

            public String Numero_
            {
                get { return Numero; }
                set { Numero = value; }
            }

        }

        public class CLASETipoTrabajador 
        {
            private Int32 N_CVE_T_TRABAJADOR = 0;

            public Int32 N_CVE_T_TRABAJADOR_
            {
                get { return N_CVE_T_TRABAJADOR; }
                set { N_CVE_T_TRABAJADOR = value; }
            }
            private String T_DSC_T_TRABAJADOR = String.Empty;

            public String T_DSC_T_TRABAJADOR_
            {
                get { return T_DSC_T_TRABAJADOR; }
                set { T_DSC_T_TRABAJADOR = value; }
            }



        }

        //PGO PROPIEDADES PARA GUARDAR PARÁMETROS DE SALARIOS MÍNIMOS
        public class Parametros
        {
            public int Id
            {
                get;
                set;
            }

            public double Valor
            {
                get;
                set;
            }

            public string ValorFecha
            {
                get;
                set;
            }

            public string Descripcion
            {
                get;
                set;
            }

        }
    }
}
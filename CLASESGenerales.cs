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

        public class Encuesta
        {
            //Parámetros para consultar la encuesta de satisfacción
            public int N_ID_ENCUESTA { get; set; }
            public int N_ID_PREGUNTA { get; set; }
            public string T_DSC_ENCUESTA { get; set; }
            public int VIG_FLAG { get; set; }
            public string F_FECH_INI_VIG { get; set; }
            public string F_FECH_FIN_VIG { get; set; }
            public string MyProperty { get; set; }

        }


        public class CaritaFeliz
        {
            //Parámetros para consultar la encuesta de satisfacción
            public int ValorCarita { get; set; }
            public string chkInfoBuenaEncuesta1 { get; set; }
            public string chkInfoBuenaEncuesta2 { get; set; }
            public string chkInfoBuenaEncuesta3 { get; set; }
            public string chkInfoBuenaEncuesta4 { get; set; }
            public string txtArea { get; set; }


        }


    }
}
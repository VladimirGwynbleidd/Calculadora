using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CALCULADORA2014.Calculos
{
    public class NegocioEntities
    {

        public string Saldo
        {
            get
            {
                return saldo;
            }
            set
            {
                saldo = value;
            }
        }
        public string Pension
        {
            get
            {
                return pension;
            }
            set
            {
                pension = value;
            }
        }
        public string Porcentaje
        {
            get
            {
                return porcentaje;
            }
            set
            {
                porcentaje = value;
            }
        }
        public string Tipo
        {
            get
            {
                return tipo;
            }
            set
            {
                tipo = value;
            }
        }

         private string saldo;
         private string pension;
         private string porcentaje;
         private string tipo;
    }
}
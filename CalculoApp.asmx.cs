using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace CALCULADORA2014
{
    /// <summary>
    /// Descripción breve de CalculoApp
    /// </summary>
    [WebService(Namespace = "http://www.consar.gob.mx/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class CalculoApp : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld( int valor)
        {
            return "Hola a todos y el valor es " + valor;
        }

        [WebMethod]
        public List<CALCULADORA2014.Calculos.NegocioEntities> GetCalculo_Valores(DateTime pFechaNacimiento, Double pEdadRetiro, Double nSemanasCotizadas, Double pSaldoActualAfore, Double pDensidadCotizacion, Double pGenero, Double pSalarioBaseCotizacionMensual, Double pRendimiento, Double pComisionAfore, Double pEdad)
        {

            string sParametros_Calculo;

            sParametros_Calculo = pFechaNacimiento.ToShortDateString().ToString() + "|" + pEdadRetiro.ToString() + "|" + nSemanasCotizadas.ToString() + "|" + pSaldoActualAfore.ToString() + "|" + pDensidadCotizacion.ToString() + "|" + pGenero.ToString() + "|" + pSalarioBaseCotizacionMensual.ToString() + "|" + pRendimiento.ToString() + "|" + pComisionAfore.ToString() + "|" + "0.065" + '|' + pEdad.ToString(); 

            ExecuteQuery dbquery = new ExecuteQuery();
            List<CALCULADORA2014.Calculos.NegocioEntities> Result = new List<CALCULADORA2014.Calculos.NegocioEntities>();
            Result = dbquery.GetCalculo_Valores(sParametros_Calculo);
            return Result;
        }

    }
}

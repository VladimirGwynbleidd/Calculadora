using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.Script.Services;
using System.Timers;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;

namespace CALCULADORA2014
{
    public partial class CalculadoraIndi : System.Web.UI.Page
    {
        public String gsIdSesion
        {
            get
            {
                object o = ViewState["gsIdSesion"];
                return (o == null) ? String.Empty : (string)o;
            }

            set
            {
                ViewState["gsIdSesion"] = value;
            }
        }

        public String gsIp
        {
            get
            {
                object o = ViewState["gsIp"];
                return (o == null) ? String.Empty : (string)o;
            }

            set
            {
                ViewState["gsIp"] = value;
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Guardarmos la sesion la primera vz que entro el usuario
                if (gsIdSesion == String.Empty)
                {
                    BDS_BITACORA_VISITAS objBitacora = new BDS_BITACORA_VISITAS(BDS_BITACORA_VISITAS.INDE);
                    Int64 liRes = 0;
                    gsIdSesion = Session.SessionID;
                    gsIp = Request.UserHostAddress;

                    if (gsIp == "" || gsIp.Length < 6)
                    {
                        if (Request.ServerVariables["REMOTE_ADDR"] != null)
                            gsIp = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        if (Request.ServerVariables["HTTP_CLIENT-IP"] != null && (gsIp == "" || gsIp.Length < 6))
                            gsIp = Request.ServerVariables["HTTP_CLIENT-IP"].ToString();
                    }

                    objBitacora.ID_SESION = gsIdSesion;
                    objBitacora.IP = gsIp;
                    liRes = objBitacora.InsertarVisita();
                    liRes = objBitacora.ObtieneTotalDeVisitas();

                    lbContVisitas.Text = liRes.ToString("N0");
                }
            }
        }

       
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string Calculo(int EdadActual, int EdadRetiro, double AhorroMensual, double SaldoAFORE, int SeleccionGenero)
        {
            Thread.Sleep(3000);

            RNCalculadoraIndi.FormularioCaptura objCaptura = new RNCalculadoraIndi.FormularioCaptura();

            objCaptura.EdadActual = EdadActual;
            objCaptura.EdadRetiro = EdadRetiro;
            objCaptura.AhorroMensual = AhorroMensual;
            objCaptura.SaldoAFORE = SaldoAFORE;
            objCaptura.AhorroExtra = 0;
            objCaptura.SeleccionGenero = SeleccionGenero;
                       
            
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string theJson = serializer.Serialize(RNCalculadoraIndi.CalculoTrabajadorIndependiente(objCaptura));

            

            return theJson;
        }



        [System.Web.Services.WebMethod]
        public static string GetCurrentTime(string name)
        {
            return "Hello " + name + Environment.NewLine + "The Current Time is: "
                + DateTime.Now.ToString();
        }


    }
}
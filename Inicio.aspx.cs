using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CALCULADORA2014
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void imbTrabIMSS_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("CalculadoraIMSS.aspx", false);
        }

        protected void imbTrabIndi_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("CalculadoraIndi.aspx", false);
        }
    }


}
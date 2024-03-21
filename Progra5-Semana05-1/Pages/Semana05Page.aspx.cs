using Progra5_Semana05_1.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Progra5_Semana05_1.Pages
{
    public partial class Semana05Page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			try
			{
                //Implementacion de using asegura que el objeto que se crea 
                //sea destruido automaticamente, una vez que salimos 
                //del bloque de codigo using
                using (Progra5_Ejemplo1Entities1 db = new Progra5_Ejemplo1Entities1())
				{
					//usamos el procedimiento almacenado para cargar una lista que se
                    //mostrará en el gridview de la página

                    var Lista = db.SPUsuariosListar().ToList();

                    GvListaUsuarios.DataSource = Lista;
                    GvListaUsuarios.DataBind();
				}
			}
			catch (Exception)
			{
                Response.Redirect("~/Pages/Error.aspx");
			}

        }
    }
}
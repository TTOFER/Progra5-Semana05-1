using Progra5_Semana05_1.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Progra5_Semana05_1.Pages
{
    public partial class UsuarioAgregarPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            //Primero vamos a capturar en variables locales los valores
            //digitados en la página
            string nombre = TxtNombre.Text.Trim();
            string email = TxtEmail.Text.Trim();
            string telefono = TxtTelefono.Text.Trim();
            string contrasennia = TxtContrasennia.Text.Trim();

            //en este ejemplo será opcional
            //TO-DO: dar funcionalidad al combo para seleccionar el Rol

            try
            {
                using (Progra5_Ejemplo1Entities1 db = new Progra5_Ejemplo1Entities1())
                {
                    db.SPUsuarioAgregar(nombre, email, telefono, contrasennia, 2);
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/Pages/Error.aspx");
            }

            Response.Redirect("~/Pages/ExitoAgregarUsuario.aspx");
        }
    }
}
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
            if (!Page.IsPostBack)
            {
                LlenarListaRolesUsuario();
            } 
        }

        //Bloque de código con funcionalidad determinada
        //se encapsula en su propia función o método
        //para llenar dropdownlist lo vamos a incluir en un método

        private void LlenarListaRolesUsuario()
        {
            try
            {
                //lista que almacenará los datos del SP
                //para luego usarla en el dropdownlist
                var ListaRolesUsuario = new List<ListItem>();

                using (Progra5_Ejemplo1Entities1 db = new Progra5_Ejemplo1Entities1())
                {
                    //vamos a usar un linq para invocar al SP
                    //y asignar valores para que funcione el dropdownlist

                    //LINQ para consultas
                    //los clásico SELECT FROM de BD

                    var query = (from lr in db.SPUsuarioRolListar()
                                 select new ListItem
                                 {
                                     Value = lr.id.ToString(),
                                     Text = lr.descrip
                                 }
                                 ).ToList();
                    
                    //incorporar cada posible resultado a la variable
                    //que usamos para el datasource del dropdownlist
                    ListaRolesUsuario.AddRange( query );

                    //y ahora hacer el binding entre la lista el ddl
                    DdlRolesUsuario.DataTextField = "Text";
                    DdlRolesUsuario.DataValueField = "Value";

                    DdlRolesUsuario.DataSource = ListaRolesUsuario;
                    DdlRolesUsuario.DataBind();

                    DdlRolesUsuario.ClearSelection();

                }
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            //Primero vamos a capturar en variables locales los valores
            //digitados en la página
            string nombre = TxtNombre.Text.Trim();
            string email = TxtEmail.Text.Trim();
            string telefono = TxtTelefono.Text.Trim();
            string contrasennia = TxtContrasennia.Text.Trim();

            int idrol = Convert.ToInt32(DdlRolesUsuario.SelectedItem.Value);
            

            try
            {
                using (Progra5_Ejemplo1Entities1 db = new Progra5_Ejemplo1Entities1())
                {
                    db.SPUsuarioAgregar(nombre, email, telefono, contrasennia, idrol);
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
using Progra5_Semana05_1.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Progra5_Semana05_1.Pages
{
    public partial class UsuarioModificarPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {

                CargarInformacionDeUsuario();

                LlenarListaRolesUsuario();

            }
        }

        private void CargarInformacionDeUsuario()
        {
            int idUsuario = Convert.ToInt32(Request.QueryString["id"]);
            TxtUsuarioID.Text = idUsuario.ToString();

            try
            {
                using (Progra5_Ejemplo1Entities1 db = new Progra5_Ejemplo1Entities1())
                {
                    var datosUsuario = db.SPUsuarioConsultarPorID(idUsuario).FirstOrDefault();

                    if (datosUsuario != null)
                    {
                        TxtNombre.Text = datosUsuario.Nombre;
                        TxtTelefono.Text = datosUsuario.Telefono;
                        TxtEmail.Text = datosUsuario.Email;

                        //seleccionar rol que tiene el usuario actualmente
                        string idrol = datosUsuario.UsuarioRolID.ToString();

                        DdlRolesUsuario.Items.FindByValue(idrol).Selected = true;
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }

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
                    ListaRolesUsuario.AddRange(query);

                    //y ahora hacer el binding entre la lista el ddl
                    DdlRolesUsuario.DataTextField = "Text";
                    DdlRolesUsuario.DataValueField = "Value";

                    DdlRolesUsuario.DataSource = ListaRolesUsuario;
                    DdlRolesUsuario.DataBind();

                    //DdlRolesUsuario.ClearSelection();

                }
            }
            catch (Exception)
            {

                Response.Redirect("Error.aspx");
            }
        }
        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            try
            {

                int idUsuario = Convert.ToInt32(TxtUsuarioID.Text.Trim());
                string nombre = TxtNombre.Text.Trim();
                string email = TxtEmail.Text.Trim();
                string telefono = TxtEmail.Text.Trim();

                //string contrasennia = string.Empty;
                string contrasennia = "";

                int idrol = Convert.ToInt32(DdlRolesUsuario.SelectedItem.Value);

                // esto valida que se haya digitado info en el txt
                if (!string.IsNullOrEmpty(TxtContrasennia.Text.Trim()))
                {
                    contrasennia = TxtContrasennia.Text.Trim();
                }

                //llamamos al sp de modicar usuario
                using (Progra5_Ejemplo1Entities1 db = new Progra5_Ejemplo1Entities1())
                {
                    db.SPUsuarioModificar(idUsuario, nombre, email, telefono, contrasennia, idrol);

                }

            }
            catch (Exception)
            {
                Response.Redirect("~/Pages/Error.aspx");
            }

            Response.Redirect("ExitoModificarUsuario.aspx");
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idUsuario = Convert.ToInt32(TxtUsuarioID.Text.Trim());

                using (Progra5_Ejemplo1Entities1 db = new Progra5_Ejemplo1Entities1())
                {
                    db.SPUsuarioEliminar(idUsuario);
                }
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }

            Response.Redirect("ExitoEliminarUsuario.aspx");
        }
    }
}
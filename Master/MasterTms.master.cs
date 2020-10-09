using System;
using System.Linq;
using System.Web.UI;
using System.Data;

public partial class Master_MasterTms : System.Web.UI.MasterPage
{
    FuncionesGenerales funcion = new FuncionesGenerales();
    protected void Page_Init(object sender, EventArgs e)
    {

    //    Page.ClientScript.RegisterOnSubmitStatement(this.GetType(), "obsubumit", "progr_est()");
        if (Session["Usuario"] != null)
        {
            UsuarioBC usuario = new UsuarioBC();
            usuario = (UsuarioBC)Session["Usuario"];

        }
        else
        {
            Response.Redirect("~/inicioQYMS2.aspx");
        }
        if (!IsPostBack)
        {
            PaginaBC pagina = new PaginaBC();
            UsuarioBC usuario = (UsuarioBC)Session["Usuario"];
        }


    }
    public void btnCambioPass_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalCambioPass();", true);
    }

    public void btnConfirmar_Click(object sender, EventArgs e)
    {
        UsuarioBC usuario = (UsuarioBC)Session["Usuario"];
        llenarUsuario(usuario);

        if (txtPass.Text == "" || txtNuevaPass.Text == "" || txtConfirmaPass.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Ingrese todos los campos');", true);
        }

        else if (usuario.PASSWORD != funcion.Encriptar(txtPass.Text, usuario.USERNAME))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Password actual no es valida');", true);
        }

        else if ((txtNuevaPass.Text) != (txtConfirmaPass.Text))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Password no coinciden');", true);
        }
        else
        {
            usuario.CambioPass(usuario);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Password Actualizada');", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje2", "$('#modalConfirmaciopass').modal('hide');", true);
        }
    }

    private UsuarioBC llenarUsuario(UsuarioBC usuario)
    {
        usuario.PASSWORD2 = funcion.Encriptar(txtNuevaPass.Text, usuario.USERNAME);
        return usuario;
    }




}


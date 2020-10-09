using System;
using System.Web.UI;

public partial class _Default : System.Web.UI.Page
{
    static FuncionesGenerales funcion = new FuncionesGenerales();
    UtilsWeb utils = new UtilsWeb();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("~/InicioQYMS2.aspx");
        if (!IsPostBack)
        {
            this.txtUsername.Focus();
            Session.Abandon();

            txtUsername.Attributes.Add("onkeypress", "return clickButton(event,'" + btnEntrar.ClientID + "')");
            txtPassword.Attributes.Add("onkeypress", "return clickButton(event,'" + btnEntrar.ClientID + "')");
        }
    }

    protected void btnEntrar_Click(object sender, EventArgs e)
    {
        UsuarioBC usuario = new UsuarioBC();
        usuario.USERNAME = this.txtUsername.Text;
        usuario.PASSWORD = funcion.Encriptar(this.txtPassword.Text, this.txtUsername.Text.ToLower());
        usuario = usuario.Login(usuario);
        if (!usuario.ESTADO)
        {
            usuario.LOGUEADO = false;
            string texto = "El usuario se encuentra inactivo, por favor contacte al administrador del sistema!";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('" + texto + "');", true);
        }
        //else if (usuario.CAMBIO_PASS == 0 && usuario.ESTADO != null)
        //{
        //    Session["USUARIO"] = usuario;
        //    this.ModalPopupExtender1.Show();
        //}
        else
        {
            if (usuario.LOGUEADO)
            {
                Session["USUARIO"] = usuario;
                Response.Redirect("~/App/Inicio.aspx");
            }
            else
            {
                usuario.LOGUEADO = false;
                string texto = "Nombre de usuario o contraseña incorrecta, por favor intente nuevamente!";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('" + texto + "');", true);
            }
        }
    }


    protected void btnClientes_Click(object sender, EventArgs e)
    {
        Response.Redirect("AccesoClientes.aspx");
    }

    protected void btnClientesImg_Click(object sender, EventArgs e)
    {
        Response.Redirect("AccesoClientes.aspx");
    }


    protected void btnactivar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        UsuarioBC usuario = new UsuarioBC();
        usuario = (UsuarioBC)Session["Usuario"];
        string ps = funcion.Encriptar(this.tbnueva.Text, this.txtUsername.Text);
        usuario.PASSWORD = ps;
        if (tbnueva.Text == tbnuevar.Text)
        {
            if (usuario.ModificarPass(usuario))
            {
                Session["USUARIO"] = usuario;
                Response.Redirect("~/App/Inicio.aspx");
            }
        }
        else
        {
         //   utils.ShowPopUpMsg("Las contraseñas no coinciden, ingreselas correctamente!!", this.Page);
            this.ModalPopupExtender1.Show();
        }
    }
}

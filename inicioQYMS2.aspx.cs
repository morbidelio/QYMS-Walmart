using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;

partial class inicioQYMS2 : System.Web.UI.Page
{
    static FuncionesGenerales funcion = new FuncionesGenerales();
    UtilsWeb utils = new UtilsWeb();

    protected void BtnProveedor_Click(object sender, EventArgs e)
    {
        UsuarioBC usuario = new UsuarioBC();
        usuario.USERNAME = this.UsuarioT.Value;
        usuario.PASSWORD = funcion.Encriptar(this.ContrasenaT.Value, this.UsuarioT.Value.ToLower());
        usuario.PROVEEDOR = this.ProveedorT.Value;
        usuario = usuario.Loginproveedor(usuario);
        if (usuario.LOGUEADO)
        {
            if (!usuario.ESTADO)
            {
                usuario.LOGUEADO = false;
                string texto = "El usuario se encuentra inactivo, por favor contacte al administrador del sistema!";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('" + texto + "');", true);
            }
            else
            {
                Session["USUARIO"] = usuario;
                Response.Redirect("~/App/"+usuario.INICIO);
            }
        }
        else
        {
            usuario.LOGUEADO = false;
            string texto = "Nombre de usuario, proveedor o contraseña incorrectos, por favor intente nuevamente!";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('" + texto + "');", true);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.UsuarioL.Focus();
            Session.Abandon();

            UsuarioL.Attributes.Add("onkeypress", "return clickButton(event,'" + BtnLogin.ClientID + "')");
            ContrasenaL.Attributes.Add("onkeypress", "return clickButton(event,'" + BtnLogin.ClientID + "')");
        }
    }

    protected void btnEntrar_Click(object sender, EventArgs e)
    {
        UsuarioBC usuario = new UsuarioBC();
        usuario.USERNAME = this.UsuarioL.Value;
        usuario.PASSWORD = funcion.Encriptar(this.ContrasenaL.Value,this.UsuarioL.Value.ToLower());
        usuario = usuario.Login(usuario);
        if (usuario.LOGUEADO)
        {
            if (!usuario.ESTADO)
            {
                usuario.LOGUEADO = false;
                string texto = "El usuario se encuentra inactivo, por favor contacte al administrador del sistema!";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('" + texto + "');", true);
            }
            else
            {
                Session["USUARIO"] = usuario;
                Response.Redirect("~/App/"+usuario.INICIO);
            }
        }
        else
        {
            usuario.LOGUEADO = false;
            string texto = "Nombre de usuario o contraseña incorrecta, por favor intente nuevamente!";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('" + texto + "');", true);
        }
    }


    protected void btnClientes_Click(object sender, EventArgs e)
    {
        Response.Redirect("AccesoClientes.aspx");
    }

}

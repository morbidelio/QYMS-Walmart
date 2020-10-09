using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Web;

partial class inicioQMGPS : System.Web.UI.Page
{
    static FuncionesGenerales funcion = new FuncionesGenerales();
    UtilsWeb utils = new UtilsWeb();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            System.Web.SessionState.SessionIDManager Manager = new System.Web.SessionState.SessionIDManager(); 

            string NewID = Manager.CreateSessionID(Context);
            bool redirected = false;
            bool IsAdded = false;
            Manager.SaveSessionID(Context, NewID, out redirected, out IsAdded);

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
                Response.Redirect("~/App/Inicio.aspx");
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

    protected void btnClientesImg_Click(object sender, EventArgs e)
    {
        Response.Redirect("AccesoClientes.aspx");
    }
}

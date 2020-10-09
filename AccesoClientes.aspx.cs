using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using AjaxControlToolkit;

public partial class AccesoClientes : System.Web.UI.Page 
{
    static FuncionesGenerales funcion = new FuncionesGenerales();
    UtilsWeb utils = new UtilsWeb();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtUsername.Focus();
            Session.Abandon();
        }
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }

    protected void btnEntrar_Click(object sender, EventArgs e)
    {
        UsuarioBC usuario = new UsuarioBC();
        usuario.USERNAME=this.txtUsername.Text ;
        usuario.PASSWORD = funcion.Encriptar(this.txtPassword.Text, this.txtUsername.Text) ;
        //usuario = usuario.Login_Cli(usuario);
        if (!usuario.ESTADO)
        {
            usuario.LOGUEADO = false;
            ScriptManager.RegisterStartupScript(this.Page,this.GetType(),"mensaje","alert('El usuario se encuentra inactivo, por favor contacte al administrador del sistema')",true);
            
        }
        else
        {
            if (usuario.LOGUEADO)
            {
                Session["USUARIO"] = usuario;
                Session["USUARIO_CLIENTE"] = usuario.ID_TIPO;

                Response.Redirect("~/App/ConsultaGD_Cli.aspx");
            }
            else {
                usuario.LOGUEADO = false;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Nombre de usuario o contraseña incorrecta, por favor intente denuevo')", true);
            }
        }
    }
    

}

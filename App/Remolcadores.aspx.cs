using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class App_Remolcadores : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            DataTable ds = yms.ObteneSites(((UsuarioBC)Session["Usuario"]).ID);
            UsuarioBC usuario = new UsuarioBC();
            utils.CargaDrop(ddl_editUsuario, "ID", "USERNAME", usuario.ObtenerTodos());

            utils.CargaDropNormal(this.dropsite, "ID", "NOMBRE", ds);
            utils.CargaDrop(this.ddl_editSite, "ID", "NOMBRE", ds);
            ObtenerRemolcador(true);
        }
        else
        {
            ObtenerRemolcador(false);
        }
    }

    protected void drop_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObtenerRemolcador(true);
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {

        if (Session["Usuario"] != null)
        {
            UsuarioBC usuario = new UsuarioBC();
            usuario = (UsuarioBC)Session["Usuario"];

            if (usuario.numero_sites < 2)
            {
                this.SITE.Visible = false;
            }

        }

    }

    #region GridView
    
    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerRemolcador(false);
    }

    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ELIMINAR")
        {
            hf_idRemolcador.Value = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalConfirmacion();", true);
        }
    }

    protected void gv_listar_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RemolcadorBC remolcador = new RemolcadorBC();
        gv_listar.SelectedIndex = e.NewEditIndex;
        hf_idRemolcador.Value = this.gv_listar.SelectedDataKey.Value.ToString();
        remolcador = remolcador.obtenerXId(int.Parse(hf_idRemolcador.Value));
        txt_editCodigo.Text = remolcador.CODIGO;
        txt_editDesc.Text = remolcador.DESCRIPCION;
        ddl_editUsuario.SelectedValue = remolcador.ID_USUARIO.ToString();
        ddl_editSite.SelectedValue = remolcador.SITE_ID.ToString();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarRemolcador();", true);
    }

    protected void gv_listar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            gv_listar.PageIndex = e.NewPageIndex;
        }
        ObtenerRemolcador(false);
    }

    #endregion

    #region Buttons

    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        Limpiar();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarRemolcador();", true);
    }

    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        ObtenerRemolcador(true);
    }

    protected void btn_editGrabar_Click(object sender, EventArgs e)
    {
        RemolcadorBC remolcador = new RemolcadorBC();
        remolcador.DESCRIPCION = txt_editDesc.Text;
        remolcador.CODIGO = txt_editCodigo.Text;
        remolcador.ID_USUARIO = int.Parse(ddl_editUsuario.SelectedValue);
        remolcador.SITE_ID = int.Parse(ddl_editSite.SelectedValue);
        if (hf_idRemolcador.Value == "")
        {
            if (remolcador.Crear(remolcador))
            {
                ObtenerRemolcador(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Remolcador creado exitosamente');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalRemolcador')", true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al agregar remolcador. Intente nuevamente.');", true);
        }
        else
        {
            remolcador.ID = int.Parse(hf_idRemolcador.Value);
            if (remolcador.Modificar(remolcador))
            {
                ObtenerRemolcador(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Remolcador modificado exitosamente');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalRemolcador')", true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al modificar remolcador. Intente nuevamente.');", true);
        }
    }

    protected void btn_EliminarRemolcador_Click(object sender, EventArgs e)
    {
        RemolcadorBC remolcador = new RemolcadorBC();
        if (remolcador.Eliminar(int.Parse(hf_idRemolcador.Value)))
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "ShowAlert('Remolcador eliminado exitosamente');", true);
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al eliminar remolcador. Revise si tiene otros datos asociados');", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalRemolcador')", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar_confirmacion", "cerrarModal('modalConfirmacion')", true);
        ObtenerRemolcador(true);
    }

    #endregion

    private void ObtenerRemolcador(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            RemolcadorBC remolcador = new RemolcadorBC();
            int site_id = Convert.ToInt32(dropsite.SelectedValue);
            string codigo = txt_buscarCodigo.Text;
            string descripcion = txt_buscarDescripcion.Text;
            DataTable dt = remolcador.obtenerXParametro(site_id, codigo, descripcion);
            ViewState["lista"] = dt;
        }
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
            dw.Sort = (String)ViewState["sortExpresion"];
        this.gv_listar.DataSource = dw;
        this.gv_listar.DataBind();
    }

    private void Limpiar()
    {
        hf_idRemolcador.Value = "";
        txt_editDesc.Text = "";
        txt_editCodigo.Text = "";
        ddl_editUsuario.ClearSelection();
        ddl_editSite.ClearSelection();
    }
}
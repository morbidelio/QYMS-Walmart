using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Zona : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Tipo_ZonaBC zoti = new Tipo_ZonaBC();
            SiteBC site = new SiteBC();
            utils.CargaDropNormal(ddl_buscarTipoZona, "ID", "DESCRIPCION", zoti.ObtenerTodos());
           ddl_buscarTipoZona.Items.Insert(0, new ListItem("TODOS","-1"));
           ddl_buscarTipoZona.SelectedValue = "-1";
            // ddl_buscarTipoZona.Items[0].Value = "0";
            utils.CargaDropTodos(ddl_buscarSite, "ID", "NOMBRE", site.ObtenerTodos());
            utils.CargaDropDefaultValue(ddl_editTipoZona, "ID", "DESCRIPCION", zoti.ObtenerTodos());
            utils.CargaDrop(ddl_editSite, "ID", "NOMBRE", site.ObtenerTodos());
            ObtenerZona(true);
        }
    }

    #region Gridview

    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerZona(false);
    }

    protected void gv_listar_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ZonaBC zona = new ZonaBC();
        gv_listar.SelectedIndex = e.NewEditIndex;
        hf_idZona.Value = this.gv_listar.SelectedDataKey.Value.ToString();
        zona = zona.ObtenerXId(int.Parse(hf_idZona.Value));
        txt_editCodigo.Text = zona.CODIGO.ToString();
        txt_editDesc.Text = zona.DESCRIPCION;
        ddl_editSite.SelectedValue = zona.SITE_ID.ToString();
        ddl_editTipoZona.SelectedValue = zona.ZOTI_ID.ToString();
        //txt_editPosX.Text = zona.ZONA_X.ToString();
        //txt_editPosY.Text = zona.ZONA_Y.ToString();
        //txt_editRotacion.Text = zona.ROTACION.ToString();
        //txt_editAltura.Text = zona.ALTURA.ToString();
        //txt_editAnchura.Text = zona.ANCHURA.ToString();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarZona();", true);
    }

    protected void gv_listar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            gv_listar.PageIndex = e.NewPageIndex;
        }
        ObtenerZona(false);
    }

    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ELIMINAR")
        {
            hf_idZona.Value = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalConfirmacion();", true);
        }
        if (e.CommandName == "VIRTUAL")
        {
            hf_idZona.Value = e.CommandArgument.ToString();
            ZonaBC z = new ZonaBC();
            int index = Convert.ToInt32(hf_idZona.Value);
            if (z.Virtual(index))
            {
                ObtenerZona(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Todo OK');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Error');", true);
        }
    }

    protected void GoPage(object sender, System.EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;
        if (int.TryParse((oIraPag.Text), out iNumPag) && iNumPag > 0 && iNumPag <= gv_listar.PageCount)
        {
            if (int.TryParse(oIraPag.Text, out iNumPag) && iNumPag > 0 && iNumPag <= gv_listar.PageCount)
            {
                gv_listar.PageIndex = iNumPag - 1;
            }
            else
            {
                gv_listar.PageIndex = 0;
            }
        }
        ObtenerZona(true);
    }

    #endregion

    #region Buttons

    protected void btn_nuevoZona_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarZona(true);", true);
    }

    protected void btn_buscarZona_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["lista"];
        string filtros = "";
        bool previo = false;
        if (!string.IsNullOrEmpty(txt_buscarNombre.Text))
        {
            filtros += "DESCRIPCION LIKE '%" + txt_buscarNombre.Text + "%'";
            previo = true;
        }
        if (ddl_buscarSite.SelectedValue != "0")
        {
            if (previo) filtros += " AND";
            filtros += " SITE_ID = " + ddl_buscarSite.SelectedValue;
            previo = true;
        }
        if (ddl_buscarTipoZona.SelectedValue != "-1")
        {
            if (previo) filtros += " AND";
            filtros += " ZOTI_ID = " + ddl_buscarTipoZona.SelectedValue;
        }
        if (!string.IsNullOrEmpty(filtros))
        {
            DataView dw = dt.AsDataView();
            dw.RowFilter = filtros;
            ViewState["filtrados"] = dw.ToTable();
            ObtenerZona(true);
        }
        else
            ObtenerZona(true);
    }

    protected void btn_EliminarZona_Click(object sender, EventArgs e)
    {
        ZonaBC zona = new ZonaBC();
        if (zona.Eliminar(int.Parse(hf_idZona.Value)))
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Zona eliminada exitosamente');", true);
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al eliminar zona. Revise si tiene otros datos asociados');", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalConfirmacion');", true);
        ObtenerZona(true);
    }

    protected void btn_editGrabar_Click(object sender, EventArgs e)
    {
        ZonaBC zona = new ZonaBC();
        zona.CODIGO = txt_editCodigo.Text;
        zona.DESCRIPCION = txt_editDesc.Text;
        zona.SITE_ID = int.Parse(ddl_editSite.SelectedValue);
        zona.ZOTI_ID = int.Parse(ddl_editTipoZona.SelectedValue);
        if (string.IsNullOrEmpty(hf_idZona.Value))
        {
            if (zona.Crear(zona))
            {
                ObtenerZona(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Zona creada exitosamente');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalZona');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al agregar zona. Intente nuevamente.');", true);
        }
        else
        {
            zona.ID = int.Parse(hf_idZona.Value);
            if (zona.Modificar(zona))
            {
                ObtenerZona(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Zona modificado exitosamente');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalZona');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al modificar zona. Intente nuevamente.');", true);
        }
    }

    #endregion

    #region UtilsPagina

    private void ObtenerZona(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            ZonaBC z = new ZonaBC();
            z.DESCRIPCION = txt_buscarNombre.Text;
            z.SITE_ID = Convert.ToInt32(ddl_buscarSite.SelectedValue);
            z.ZOTI_ID = Convert.ToInt32(ddl_buscarTipoZona.SelectedValue);
            DataTable dt = z.ObtenerXParametrosMant(z);
            ViewState["lista"] = dt;
            ViewState.Remove("filtrados");
        }
        DataView dw;
        if ((DataTable)ViewState["filtrados"] == null)
            dw = new DataView((DataTable)ViewState["lista"]);
        else
            dw = new DataView((DataTable)ViewState["filtrados"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
            dw.Sort = (String)ViewState["sortExpresion"];
        this.gv_listar.DataSource = dw;
        this.gv_listar.DataBind();
    }

    #endregion
}
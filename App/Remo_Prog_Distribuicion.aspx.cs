// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Remo_Prog_Distribuicion : System.Web.UI.Page
{
    UsuarioBC u = new UsuarioBC();
    UtilsWeb utils = new UtilsWeb();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["usuario"] == null)
        {
            this.Response.Redirect("~/InicioQYMS.aspx");
        }

        this.u = (UsuarioBC)this.Session["usuario"];
        if (!this.IsPostBack)
        {
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            this.utils.CargaDropNormal(this.ddl_site, "ID", "NOMBRE", yms.ObteneSites(this.u.ID));
            this.utils.CargaDrop(this.ddl_editSite, "ID", "NOMBRE", yms.ObteneSites(this.u.ID));
        }
        this.ObtenerProgramacion(false);
    }

    #region Buttons

    protected void btn_Conf_Click(object sender, EventArgs e)
    {
        RemoProgDistribuicionBC r = new RemoProgDistribuicionBC();
        if (r.ActivarDesactivar(Convert.ToInt32(this.hf_idRPD.Value)))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Todo OK');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Todo mal!');", true);
        }
        this.ObtenerProgramacion(true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "$('#modalConfirmar').modal('hide');", true);
    }

    protected void btn_asignarGuardar_Click(object sender, EventArgs e)
    {
        RemoProgDistribuicionBC r = new RemoProgDistribuicionBC();
        DataTable dt = (DataTable)this.ViewState["seleccionados"];
        int id = Convert.ToInt32(this.hf_idRPD.Value);
        if (r.Asignar(dt, id))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Todo OK!');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Error!');", true);
        }
        ObtenerPlayas(true);
        ObtenerProgramacion(true);
    }

    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        this.LimpiarTodo();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalRemoProg();", true);
    }

    protected void btn_editGuardar_Click(object sender, EventArgs e)
    {
        RemoProgDistribuicionBC r = new RemoProgDistribuicionBC();
        r.SITE_ID = Convert.ToInt32(this.ddl_editSite.SelectedValue);
        r.DESCRIPCION = this.txt_editDesc.Text;
        if (string.IsNullOrEmpty(this.hf_idRPD.Value))
        {
            if (r.Crear(r))
            {
                this.ddl_site.SelectedValue = this.ddl_editSite.SelectedValue;
                this.ObtenerProgramacion(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Agregado Correctamente!');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje2", " $('#modalRemoProg').modal('hide')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Error!');", true);
            }
        }
        else
        {
            r.ID = Convert.ToInt32(this.hf_idRPD.Value);
            if (r.Modificar(r))
            {
                this.ObtenerProgramacion(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Modificado Correctamente!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Error!');", true);
            }
        }
    }

    #endregion

    #region GridView

    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ASIGNAR")
        {
            this.LimpiarTodo();
            RemoProgDistribuicionBC r = new RemoProgDistribuicionBC();
            this.hf_idRPD.Value = e.CommandArgument.ToString();
            int id = Convert.ToInt32(this.hf_idRPD.Value);
            r = r.ObtenerXId(id);
            this.hf_idSite.Value = r.SITE_ID.ToString();
            ZonaBC z = new ZonaBC();
            this.utils.CargaDrop(this.ddl_asignarZona, "ID", "DESCRIPCION", z.ObtenerXSite(r.SITE_ID, false));
            this.ObtenerSeleccionados(true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalAsignar();", true);
        }
        if (e.CommandName == "EDITAR")
        {
            this.LimpiarTodo();
            RemoProgDistribuicionBC r = new RemoProgDistribuicionBC();
            this.hf_idRPD.Value = e.CommandArgument.ToString();
            int id = Convert.ToInt32(this.hf_idRPD.Value);
            r = r.ObtenerXId(id);
            this.ddl_editSite.SelectedValue = r.SITE_ID.ToString();
            this.txt_editDesc.Text = r.DESCRIPCION;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalRemoProg();", true);
        }
        if (e.CommandName == "ACTIVAR")
        {
            this.hf_idRPD.Value = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalConfirmar();", true);
        }
    }

    protected void gv_seleccionados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int index = e.Row.RowIndex;
            LinkButton btnsubir = (LinkButton)e.Row.FindControl("btn_subir");
            LinkButton btnbajar = (LinkButton)e.Row.FindControl("btn_bajar");
            DataTable dt = (DataTable)this.ViewState["seleccionados"];
            if (index == 0)
            {
                btnsubir.Visible = false;
            }
            if (dt.Rows.Count == (index + 1))
            {
                btnbajar.Visible = false;
            }
            if (dt.Rows.Count <= 1)
            {
                btnsubir.Visible = false;
                btnbajar.Visible = false;
            }
        }
    }

    protected void gv_seleccionados_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ELIMINAR")
        {

            int index = Convert.ToInt32(e.CommandArgument);
            DataTable dt = (DataTable)this.ViewState["seleccionados"];
            int orden = Convert.ToInt32(dt.Rows[index]["ORDEN"]);
            for (int i = index; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["ORDEN"]) > orden)
                {
                    try
                    {
                        dt.Rows[i]["ORDEN"] = Convert.ToInt32(dt.Rows[i]["ORDEN"]) - 1;
                    }
                    catch (Exception ex)
                    {
                        dt.Rows[i]["ORDEN"] = 1;
                    }
                }
            }
            this.hf_seleccionados.Value = this.QuitarSeleccionado(dt.Rows[index]["PLAY_ID"].ToString());
            dt.Rows.RemoveAt(index);
            this.ViewState["seleccionados"] = dt;
            this.ObtenerPlayas(false);
        }
        if (e.CommandName == "ARRIBA")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            DataTable sel = (DataTable)this.ViewState["seleccionados"];
            int ordenO = Convert.ToInt32(sel.Rows[index]["ORDEN"].ToString());
            sel.Rows[index]["ORDEN"] = ordenO - 1;
            sel.Rows[index - 1]["ORDEN"] = ordenO;
            sel.DefaultView.Sort = "ORDEN ASC";
            this.ViewState["seleccionados"] = sel.DefaultView.ToTable();
            this.ObtenerSeleccionados(false);
        }
        if (e.CommandName == "ABAJO")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            DataTable sel = (DataTable)this.ViewState["seleccionados"];
            int ordenO = Convert.ToInt32(sel.Rows[index]["ORDEN"].ToString());
            sel.Rows[index]["ORDEN"] = ordenO + 1;
            sel.Rows[index + 1]["ORDEN"] = ordenO;
            sel.DefaultView.Sort = "ORDEN ASC";
            this.ViewState["seleccionados"] = sel.DefaultView.ToTable();
            this.ObtenerSeleccionados(false);
        }
    }

    protected void gv_noSeleccionados_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "SEL")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            DataTable sel = new DataTable();
            DataTable nosel = (DataTable)this.ViewState["nosel"];
            sel = (DataTable)this.ViewState["seleccionados"];
            string id = nosel.Rows[index]["PLAY_ID"].ToString();
            string descripcion = nosel.Rows[index]["DESCRIPCION"].ToString();
            string zona = nosel.Rows[index]["ZONA"].ToString();
            int orden = 1;
            try
            {
                orden = (int)sel.Compute("Max(ORDEN)", "");
                orden++;
            }
            catch
            {
                orden = 1;
            }
            sel.Rows.Add(Convert.ToInt32(this.hf_idRPD.Value), id,  null, orden, descripcion, zona);
            if (string.IsNullOrWhiteSpace(this.hf_seleccionados.Value))
            {
                this.hf_seleccionados.Value = id;
            }
            else
            {
                this.hf_seleccionados.Value += string.Format(",{0}", id);
            }
            this.ViewState["seleccionados"] = sel;
            this.ObtenerPlayas(false);
        }
    }

    #endregion

    #region DropDownList

    protected void ddl_site_IndexChanged(object sender, EventArgs e)
    {
        this.ObtenerProgramacion(true);
    }

    protected void ddl_asignarZona_IndexChanged(object sender, EventArgs e)
    {
        if (this.ddl_asignarZona.SelectedValue != "0")
        {
            this.ObtenerPlayas(false);
        }
        else
        {
            this.gv_noSeleccionados.DataSource = null;
            this.gv_noSeleccionados.DataBind();
        }
    }

    #endregion

    #region UtilsPagina

    private void LimpiarTodo()
    {
        DataTable dt = new DataTable();
        this.ViewState.Remove("playas");
        dt.Columns.Add("REPD_ID");
        dt.Columns.Add("PLAY_ID");
        dt.Columns.Add("ORDEN_OLD");
        dt.Columns.Add("ORDEN");
        dt.Columns.Add("DESCRIPCION");
        dt.Columns.Add("ZONA");
        this.ViewState["seleccionados"] = dt;
        this.hf_idRPD.Value = "";
        this.hf_seleccionados.Value = "";
        this.hf_idSite.Value = "";
        this.gv_noSeleccionados.DataSource = null;
        this.gv_seleccionados.DataSource = null;
        this.gv_seleccionados.DataBind();
        this.gv_noSeleccionados.DataBind();
        this.ddl_editSite.ClearSelection();
        this.txt_editDesc.Text = "";
    }

    private void ObtenerProgramacion(bool forzarBD)
    {
        if (this.ViewState["listado"] == null || forzarBD)
        {
            RemoProgDistribuicionBC rpd = new RemoProgDistribuicionBC();
            this.ViewState["listado"] = rpd.ObtenerTodo(Convert.ToInt32(this.ddl_site.SelectedValue));
            this.ViewState.Remove("filtrados");
        }
        DataTable dt;
        if (this.ViewState["filtrados"] == null)
        {
            dt = (DataTable)this.ViewState["listado"];
        }
        else
        {
            dt = (DataTable)this.ViewState["filtrados"];
        }
        this.gv_listar.DataSource = dt;
        this.gv_listar.DataBind();
    }

    private void ObtenerSeleccionados(bool forzarBD)
    {
        DataTable dt;
        if (this.ViewState["seleccionados"] == null || forzarBD)
        {
            RemoProgDistribuicionBC rpd = new RemoProgDistribuicionBC();
            this.ViewState["seleccionados"] = rpd.ObtenerPlayas(Convert.ToInt32(this.hf_idRPD.Value));
        }
        DataView dw = new DataView((DataTable)this.ViewState["seleccionados"]);
        dw.Sort = "ORDEN ASC";
        dt = dw.ToTable();
        string cadena = "";
        foreach (DataRow dr in dt.Rows)
        {
            if (!string.IsNullOrEmpty(cadena))
            {
                cadena += ",";
            }
            cadena += dr["PLAY_ID"].ToString();
        }
        this.hf_seleccionados.Value = cadena;
        this.gv_seleccionados.DataSource = dt;
        this.gv_seleccionados.DataBind();
    }

    private void ObtenerPlayas(bool forzarBD)
    {
        if (this.ViewState["playas"] == null || forzarBD)
        {
            PlayaBC p = new PlayaBC();
            DataTable playas = p.ObtenerXSite(Convert.ToInt32(this.hf_idSite.Value));
            this.ViewState["playas"] = playas;
        }
        DataView playasNS = new DataView((DataTable)this.ViewState["playas"]);
        string filtro = string.Format("ZONA_ID = {0}", this.ddl_asignarZona.SelectedValue);
        if (!string.IsNullOrEmpty(hf_seleccionados.Value))
            filtro += string.Format(" AND PLAY_ID NOT IN ({0})", hf_seleccionados.Value);
        playasNS.RowFilter = filtro;
        this.ViewState["nosel"] = playasNS.ToTable();
        this.gv_noSeleccionados.DataSource = this.ViewState["nosel"];
        this.gv_noSeleccionados.DataBind();
        ObtenerSeleccionados(false);
    }

    private string QuitarSeleccionado(string id)
    {
        string[] ids = this.hf_seleccionados.Value.Split(",".ToCharArray());
        string cadena = "";
        foreach (string i in ids)
        {
            if (i != id)
            {
                if (!string.IsNullOrEmpty(cadena))
                {
                    cadena += ",";
                }
                cadena += i;
            }
        }
        return cadena;
    }
    #endregion
}
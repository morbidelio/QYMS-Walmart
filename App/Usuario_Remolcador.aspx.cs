// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Usuario_Remolcador : System.Web.UI.Page
{
    UsuarioBC u = new UsuarioBC();
    UtilsWeb utils = new UtilsWeb();
    CargaDrops drops = new CargaDrops();

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
            JornadaBC j = new JornadaBC();
            this.drops.Site_Normal(this.dropsite, this.u.ID);
            this.drops.Site_Normal(this.ddl_siteEdit);
            this.hf_idsite.Value = this.ddl_siteEdit.SelectedValue;
            this.utils.CargaDrop(this.ddl_jornadaEdit, "ID", "NOMBRE", j.obtenerTodas());
            this.ddl_siteEdit_IndexChanged(null, null);
        }
        this.ObtenerUsuarioRemolcador(false);
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (this.Session["Usuario"] != null)
        {
            UsuarioBC usuario = new UsuarioBC();
            usuario = (UsuarioBC)this.Session["Usuario"];

            if (usuario.numero_sites < 2)
            {
                this.SITE.Visible = false;
                this.pnl_crearSite.Visible = false;
            }
        }
    }

    #region GridView

    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITAR")
        {
            this.hf_idremo.Value = e.CommandArgument.ToString();
            this.LlenarForm();
            this.ddl_remoEdit.Enabled = false;
            this.ddl_siteEdit.Enabled = false;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalUsuarioRemolcador();", true);
        }
    }

    protected void gv_jornadaUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
    }

    protected void gv_jornadaUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ELIMINAR")
        {
            Int32 index = Convert.ToInt32(e.CommandArgument);
            DataTable dt = (DataTable)this.ViewState["edit"];
            if (dt.Rows[index]["REPR_ID"].ToString() != "0")
            {
                if (string.IsNullOrEmpty(this.hf_eliminados.Value))
                {
                    this.hf_eliminados.Value = dt.Rows[index]["REPR_ID"].ToString();
                }
                else
                {
                    this.hf_eliminados.Value += string.Format(",{0}", dt.Rows[index]["REPR_ID"].ToString());
                }
            }
            dt.Rows.RemoveAt(index);
            DataView dw = new DataView();
            dw = dt.DefaultView;
            dw.Sort = "FECHA,JORN_ID asc";
            dt = dw.ToTable();
            this.gv_jornadaUsuario.DataSource = dt;
            this.gv_jornadaUsuario.DataBind();
            this.ViewState["edit"] = dt;
        }
    }

    #endregion

    #region Buttons

    protected void btn_Canc_Click(object sender, EventArgs e)
    {
        this.ddl_siteEdit.SelectedValue = this.hf_idsite.Value;
        this.ddl_remoEdit.SelectedValue = this.hf_idremo.Value;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal_cierra", "cerrarBorrarAsoc();", true);
    }

    protected void btn_Conf_Click(object sender, EventArgs e)
    {
        this.ViewState["edit"] = null;
        this.gv_jornadaUsuario.DataSource = null;
        this.gv_jornadaUsuario.DataBind();
        if (this.ddl_siteEdit.SelectedValue != this.ddl_siteEdit.SelectedValue)
        {
            this.cargarDropsForm();
        }
        this.hf_idsite.Value = this.ddl_siteEdit.SelectedValue;
        this.hf_idremo.Value = this.ddl_remoEdit.SelectedValue;
        //up_jornadaUsuario.Update();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal_cierra", "cerrarBorrarAsoc();", true);
    }

    public void btn_agregarEdit_Click(object sender, EventArgs e)
    {
        DataTable dt;
        if (this.ViewState["edit"] == null)
        {
            dt = new DataTable();
            dt.Columns.Add("REPR_ID");
            dt.Columns.Add("FECHA");
            dt.Columns.Add("USUA_ID");
            dt.Columns.Add("USUARIO");
            dt.Columns.Add("JORN_ID");
            dt.Columns.Add("JORNADA");
            dt.Columns.Add("REPD_ID");
            dt.Columns.Add("PROGRAMACION");
            dt.Columns.Add("AGREGAR");
        }
        else
        {
            dt = (DataTable)this.ViewState["edit"];
        }
        DataView dw = dt.AsDataView();
        dw.RowFilter = string.Format("JORN_ID = {0} AND FECHA = '{1}'", this.ddl_jornadaEdit.SelectedValue, this.txt_fechaEdit.Text);
        if (dw.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Jornada ya asignada para este día! No se puede asignar mas de una vez!');", true);
        }
        else
        {
            UsuarioRemolcadorBC ur = new UsuarioRemolcadorBC();
            ur.FECHA = DateTime.Parse(this.txt_fechaEdit.Text);
            ur.JORN_ID = int.Parse(this.ddl_jornadaEdit.SelectedValue);
            ur.SITE_ID = int.Parse(this.ddl_siteEdit.SelectedValue);
            ur.REMO_ID = int.Parse(this.ddl_remoEdit.SelectedValue);
            if (this.comprobarDatos(ur))
            {
                dt.Rows.Add("0", this.txt_fechaEdit.Text, this.ddl_usuaEdit.SelectedValue, this.ddl_usuaEdit.SelectedItem.Text, this.ddl_jornadaEdit.SelectedValue, this.ddl_jornadaEdit.SelectedItem.Text, this.ddl_programacionEdit.SelectedValue, this.ddl_programacionEdit.SelectedItem.Text, "1");
                dw = dt.DefaultView;
                dw.Sort = "FECHA,JORN_ID asc";
                dt = dw.ToTable();
                this.ViewState["edit"] = dt;
                this.gv_jornadaUsuario.DataSource = dt;
                this.gv_jornadaUsuario.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Datos redundantes. Compruebe e intente nuevamente.');", true);
            }
        }
    }

    public void btn_nuevo_Click(object sender, EventArgs e)
    {
        this.limpiarForm();
        this.txt_fechaEdit.Text = DateTime.Now.ToShortDateString();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalUsuarioRemolcador();", true);
    }

    public void btn_buscar_Click(object sender, EventArgs e)
    {
        this.ObtenerUsuarioRemolcador(false);
    }

    public void btn_editGrabar_Click(object sender, EventArgs e)
    {
        UsuarioRemolcadorBC u = new UsuarioRemolcadorBC();
        u.REMO_ID = int.Parse(this.ddl_remoEdit.SelectedValue);
        u.SITE_ID = int.Parse(this.ddl_siteEdit.SelectedValue);
        DataTable dt = (DataTable)this.ViewState["edit"];
        bool exito = true;
        if (!string.IsNullOrEmpty(this.hf_eliminados.Value))
        {
            if (!u.Eliminar(this.hf_eliminados.Value))
            {
                exito = false;
            }
        }
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["REPR_ID"].ToString() == "0")
            {
                u.FECHA = DateTime.Parse(dr["FECHA"].ToString());
                u.USUA_ID = int.Parse(dr["USUA_ID"].ToString());
                u.JORN_ID = int.Parse(dr["JORN_ID"].ToString());
                u.REPD_ID = int.Parse(dr["REPD_ID"].ToString());
                if (!u.Guardar(u))
                {
                    exito = false;
                }
            }
        }
        if (exito)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "showAlert('Asociación usuario remolcador guardada exitosamente!');", true);
            if (string.IsNullOrEmpty(this.hf_idremo.Value))
            {
                this.limpiarForm();
            }
            this.ObtenerUsuarioRemolcador(true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "$('#modalUsuarioRemolcador').modal('hide');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Error. No se guardaron/eliminaron todos los registros correctamente. Revise los datos.');", true);
        }
    }

    #endregion

    #region DropDownList

    public void drop_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ObtenerUsuarioRemolcador(true);
    }

    public void ddl_siteEdit_IndexChanged(object sender, EventArgs e)
    {
        if (this.ViewState["edit"] != null)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalBorrarAsoc();", true);
        }
        else
        {
            this.cargarDropsForm();
        }
    }

    #endregion

    #region UtilsPagina

    private bool comprobarDatos(UsuarioRemolcadorBC ur)
    {
        bool ok = false;
        if (ur.ComprobarRegistros(ur, this.hf_eliminados.Value))
        {
            ok = true;
        }
        return ok;
    }

    private void cargarDropsForm()
    {
        int site_id = int.Parse(this.ddl_siteEdit.SelectedValue);
        RemolcadorBC r = new RemolcadorBC();
        RemoProgDistribuicionBC rpd = new RemoProgDistribuicionBC();
        this.drops.Usuario(this.ddl_usuaEdit, site_id, true);
        this.utils.CargaDrop(this.ddl_programacionEdit, "ID", "DESCRIPCION", rpd.ObtenerTodo(site_id));
        this.utils.CargaDrop(this.ddl_remoEdit, "ID", "DESCRIPCION", r.obtenerTodos(site_id));
    }

    private void limpiarForm()
    {
        this.ViewState.Remove("edit");
        this.ddl_siteEdit.SelectedValue = this.dropsite.SelectedValue;
        this.ddl_siteEdit_IndexChanged(null, null);
        this.ddl_remoEdit.Enabled = true;
        this.ddl_siteEdit.Enabled = true;
        this.ddl_jornadaEdit.ClearSelection();
        this.txt_fechaEdit.Text = DateTime.Now.ToShortDateString();
        this.hf_idsite.Value = this.ddl_siteEdit.SelectedValue;
        this.hf_idremo.Value = this.ddl_remoEdit.SelectedValue;
        this.gv_jornadaUsuario.DataSource = null;
        this.gv_jornadaUsuario.DataBind();
    }

    private void ObtenerUsuarioRemolcador(bool forzarBD)
    {
        if (this.ViewState["lista"] == null || forzarBD)
        {
            UsuarioRemolcadorBC ur = new UsuarioRemolcadorBC();
            this.ViewState["lista"] = ur.obtenerTodosControl(int.Parse(this.dropsite.SelectedValue));
        }
        DataView dw = new DataView((DataTable)this.ViewState["lista"]);
        if (this.ViewState["sortExpresion"] != null && this.ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)this.ViewState["sortExpresion"];
        }
        this.gv_listar.DataSource = dw.ToTable();
        this.gv_listar.DataBind();
    }

    private void LlenarForm()
    {
        this.txt_fechaEdit.Text = DateTime.Now.ToShortDateString();
        RemolcadorBC r = new RemolcadorBC();
        r.ID = int.Parse(this.hf_idremo.Value);
        r = r.obtenerXId();
        this.hf_idsite.Value = r.SITE_ID.ToString();
        this.ddl_siteEdit.SelectedValue = r.SITE_ID.ToString();
        cargarDropsForm();
        UsuarioRemolcadorBC ur = new UsuarioRemolcadorBC();
        this.ViewState["edit"] = ur.obtenerUsuarioRemolcadorXRemoId(r.ID);
        this.gv_jornadaUsuario.DataSource = (DataTable)this.ViewState["edit"];
        this.gv_jornadaUsuario.DataBind();

        try
        {
            this.ddl_remoEdit.SelectedValue = r.ID.ToString();
        }
        catch (Exception)
        {
            this.ddl_remoEdit.ClearSelection();
        }
    }

    #endregion

    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 3; i < e.Row.Cells.Count; i++)
        {
            e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("(***)", "<br/>");
        }
    }
}
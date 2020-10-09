using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Timer_Procesos : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SiteBC s = new SiteBC();
            SolicitudBC sol = new SolicitudBC();
            sol.ObtenerEstados();
            //TrailerEstadoBC t = new TrailerEstadoBC();
            Tipo_ZonaBC tz = new Tipo_ZonaBC();
            utils.CargaDrop(ddl_buscaSite, "ID", "NOMBRE", s.ObtenerTodos());
            utils.CargaDrop(ddl_buscaEstadoSol, "ID", "DESCRIPCION", sol.ObtenerEstados());
            utils.CargaDropDefaultValue(ddl_buscaTipoZona, "ID", "DESCRIPCION", tz.ObtenerTodos());
            utils.CargaDrop(ddl_editSite, "ID", "NOMBRE", s.ObtenerTodos());
            utils.CargaDrop(ddl_editEstadoSoli, "ID", "DESCRIPCION", sol.ObtenerEstados());
            utils.CargaDropDefaultValue(ddl_editTipoZona, "ID", "DESCRIPCION", tz.ObtenerTodos());
            ObtenerTimers(true);
        }
    }

    #region Button

    protected void btn_eliminarTimer_Click(object sender, EventArgs e)
    {
        TimerProcesosBC tp = new TimerProcesosBC();
        if (tp.Eliminar(int.Parse(hf_idTimerProcesos.Value)))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Eliminado sin problemas');", true);
            ObtenerTimers(true);
        }
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Ocurrió un error!');", true);
    }

    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        string filtros = "";
        bool anterior = false;
        if (ddl_buscaSite.SelectedIndex > 0)
        {
            filtros = "SITE_ID = " + ddl_buscaSite.Text + " ";
            anterior = true;
        }
        if (!string.IsNullOrEmpty(txt_buscaNombre.Text))
        {
            if (anterior)
                filtros += "AND ";
            else
                anterior = true;
            filtros = "DESCRIPCION LIKE '%" + txt_buscaNombre.Text + "%' ";
            anterior = true;
        }
        if (ddl_buscaEstadoSol.SelectedIndex > 0)
        {
            if (anterior)
                filtros += "AND ";
            else
                anterior = true;
            filtros = "SOES_ID = " + ddl_buscaEstadoSol.SelectedValue + " ";
            anterior = true;
        }
        if (ddl_buscaTipoZona.SelectedIndex > 0)
        {
            if (anterior)
                filtros += "AND ";
            else
                anterior = true;
            filtros = "ZOTI_ID = " + ddl_buscaTipoZona.SelectedValue + " ";
            anterior = true;
        }
        if (ddl_buscaTipoPlaya.SelectedIndex > 0)
        {
            if (anterior)
                filtros += "AND ";
            else
                anterior = true;
            filtros = "PYTI_ID = " + ddl_buscaTipoPlaya.SelectedValue + " ";
            anterior = true;
        }
        if (string.IsNullOrEmpty(filtros))
            ObtenerTimers(true);
        else
        {
            dw.RowFilter = filtros;
            ViewState["filtrados"] = dw.ToTable();
            ObtenerTimers(false);
        }
    }

    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        Limpiar();
        hf_idTimerProcesos.Value = "";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalTimers();", true);
    }

    protected void btn_editGrabar_Click(object sender, EventArgs e)
    {
        TimerProcesosBC tp = new TimerProcesosBC();
        tp.CODIGO = txt_editCodigo.Text;
        tp.DESCRIPCION = txt_editDesc.Text;
        tp.TIEMPO_MAX = int.Parse(txt_editMins.Text);
        tp.COLOR = txt_editColor.Text;
        tp.SITE_ID = int.Parse(ddl_editSite.SelectedValue);
        tp.SOES_ID = int.Parse(ddl_editEstadoSoli.SelectedValue);
        tp.ZOTI_ID = int.Parse(ddl_editTipoZona.SelectedValue);
        tp.PYTI_ID = int.Parse(ddl_editTipoPlaya.SelectedValue);
        if (string.IsNullOrEmpty(hf_idTimerProcesos.Value))
        {
            if (tp.Crear(tp))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Timer procesos agregado correctamente');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "limpiar", "limpiarForm();", true);
                ObtenerTimers(true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Error. Revise que los datos sean correctos.');", true);
        }
        else
        {
            tp.ID = int.Parse(hf_idTimerProcesos.Value);
            if (tp.Modificar(tp))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Timer procesos agregado correctamente');", true);
                ObtenerTimers(true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Error. Revise que los datos sean correctos.');", true);
        }
    }

    #endregion

    #region Gridview

    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager && (gv_listar.DataSource != null))
        {
            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumberOfPages");
            _TotalPags.Text = gv_listar.PageCount.ToString();

            //LLENA LA LISTA CON EL NUMERO DE PAGINAS
            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(gv_listar.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = Convert.ToString(gv_listar.PageIndex + 1);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnEliminar = (LinkButton)e.Row.FindControl("btn_eliminarTimer");
            if (e.Row.Cells[7].Text == "Default" || string.IsNullOrWhiteSpace(e.Row.Cells[7].Text))
            {
                btnEliminar.Visible = false;
            }
            else
            {
                btnEliminar.Visible = true;
            }
            //int id_solicitud = int.Parse(gv_timerProcesos.DataKeys[e.Row.RowIndex].Values[0].ToString());
            string color = e.Row.Cells[10].Text;
            try
            {
                e.Row.Cells[10].BackColor = System.Drawing.ColorTranslator.FromHtml(color);
            }
            catch (Exception)
            {
                e.Row.Cells[10].BackColor = System.Drawing.Color.Empty;
            }
            e.Row.Cells[10].Text = "";
        }
    }

    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerTimers(false);
    }

    protected void gv_listar_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Limpiar();
        TimerProcesosBC tp = new TimerProcesosBC();
        gv_listar.SelectedIndex = e.NewEditIndex;
        hf_idTimerProcesos.Value = this.gv_listar.SelectedDataKey.Value.ToString();
        tp = tp.ObtenerXId(int.Parse(hf_idTimerProcesos.Value));
        txt_editCodigo.Text = tp.CODIGO;
        txt_editDesc.Text = tp.DESCRIPCION;
        txt_editMins.Text = tp.TIEMPO_MAX.ToString();
        txt_editColor.Text = tp.COLOR;
        ddl_editSite.SelectedValue = tp.SITE_ID.ToString();
        ddl_editSite.Enabled = false;
        ddl_editEstadoSoli.Enabled = true;
        ddl_editTipoZona.Enabled = true;
        ddl_editTipoPlaya.Enabled = true;
        ddl_editEstadoSoli.SelectedValue = tp.SOES_ID.ToString();
        ddl_editTipoZona.SelectedValue = tp.ZOTI_ID.ToString();
        ddl_editTipoPlaya.SelectedValue = tp.PYTI_ID.ToString();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalTimers();", true);
    }

    protected void gv_listar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            gv_listar.PageIndex = e.NewPageIndex;
        }
        ObtenerTimers(false);
    }

    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        TimerProcesosBC tp = new TimerProcesosBC();
        if (e.CommandName == "ELIMINAR")
        {
            hf_idTimerProcesos.Value = e.CommandArgument.ToString();
            tp = tp.ObtenerXId(int.Parse(hf_idTimerProcesos.Value));
            lblRazonEliminacion.Text = "Eliminar Tipo Playa";
            msjEliminacion.Text = "Se eliminará el tipo tp seleccionado, ¿desea continuar?";
            btnModalEliminar.Attributes.Remove("onclick");
            btnModalEliminar.Attributes.Add("onclick", "eliminarTimer();");
            btnModalEliminar.Text = "Eliminar";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalConfirmacion();", true);
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
        ObtenerTimers(false);
    }

    #endregion

    private void Limpiar()
    {
        txt_editCodigo.Text = "";
        txt_editDesc.Text = "";
        txt_editMins.Text = "";
        txt_editColor.Text = "";
        ddl_editSite.ClearSelection();
        ddl_editSite.Enabled = true;
        ddl_editEstadoSoli.ClearSelection();
        ddl_editEstadoSoli.Enabled = true;
        ddl_editTipoPlaya.ClearSelection();
        ddl_editTipoPlaya.Enabled = true;
        ddl_editTipoZona.ClearSelection();
        ddl_editTipoZona.Enabled = true;
    }

    private void ObtenerTimers(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            TimerProcesosBC tp = new TimerProcesosBC();
            ViewState["lista"] = tp.ObtenerTodos();
            ViewState.Remove("filtrados");
        }
        DataView dw;
        if (ViewState["filtrados"] == null)
            dw = new DataView((DataTable)ViewState["lista"]);
        else
            dw = new DataView((DataTable)ViewState["filtrados"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
            dw.Sort = (String)ViewState["sortExpresion"];
        this.gv_listar.DataSource = dw;
        this.gv_listar.DataBind();
    }
}
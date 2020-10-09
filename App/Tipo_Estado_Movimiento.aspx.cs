using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class App_Tipo_Estado_Movimiento : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();

    #region GridView

    protected void GoPage(object sender, System.EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;
        if (int.TryParse((oIraPag.Text), out iNumPag) && iNumPag > 0 && iNumPag <= gv_listaTipoEstadoMov.PageCount)
        {
            if (int.TryParse(oIraPag.Text, out iNumPag) && iNumPag > 0 && iNumPag <= gv_listaTipoEstadoMov.PageCount)
            {
                gv_listaTipoEstadoMov.PageIndex = iNumPag - 1;
            }
            else
            {
                gv_listaTipoEstadoMov.PageIndex = 0;
            }
        }
        ObtenerTipoEstadoMov(true);
    }

    public void gv_listaTipoEstadoMov_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager && (gv_listaTipoEstadoMov.DataSource != null))
        {
            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumberOfPages");
            _TotalPags.Text = gv_listaTipoEstadoMov.PageCount.ToString();

            //LLENA LA LISTA CON EL NUMERO DE PAGINAS
            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(gv_listaTipoEstadoMov.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = Convert.ToString(gv_listaTipoEstadoMov.PageIndex + 1);
        }
    }

    public void gv_listaTipoEstadoMov_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        DataView view = new DataView();
        if (ViewState["lista"] == null)
        {
            TipoEstadoMovBC tem = new TipoEstadoMovBC();
            view.Table = tem.obtenerTodoTipoEstadoMov();
        }
        else
        {
            view.Table = (DataTable)ViewState["lista"];
        }
        view.Sort = e.SortExpression + " " + direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ViewState["lista"] = view.Table;
        this.gv_listaTipoEstadoMov.DataSource = view;
        this.gv_listaTipoEstadoMov.DataBind();
    }

    public void gv_listaTipoEstadoMov_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        TipoEstadoMovBC tem = new TipoEstadoMovBC();
        if (e.CommandName == "ELIMINAR")
        {
            hf_idTipoEstadoMov.Value = e.CommandArgument.ToString();
            tem = tem.obtenerXID(int.Parse(hf_idTipoEstadoMov.Value));
            lblRazonEliminacion.Text = "Eliminar TipoEstadoMov";
            msjEliminacion.Text = "Se eliminará el tem seleccionado, ¿desea continuar?";
            btnModalEliminar.Attributes.Remove("onclick");
            btnModalEliminar.Attributes.Add("onclick", "eliminarTipoEstadoMov();");
            btnModalEliminar.Text = "Eliminar";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalConfirmacion();", true);
        }
    }

    public void gv_listaTipoEstadoMov_RowEditing(object sender, GridViewEditEventArgs e)
    {
        TipoEstadoMovBC tem = new TipoEstadoMovBC();
        gv_listaTipoEstadoMov.SelectedIndex = e.NewEditIndex;
        hf_idTipoEstadoMov.Value = this.gv_listaTipoEstadoMov.SelectedDataKey.Value.ToString();

        tem = tem.obtenerXID(int.Parse(this.gv_listaTipoEstadoMov.SelectedDataKey.Value.ToString()));
        txt_editDesc.Text = tem.DESCRIPCION;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarTipoEstadoMov();", true);
    }

    public void gv_listaTipoEstadoMov_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            gv_listaTipoEstadoMov.PageIndex = e.NewPageIndex;
        }
        ObtenerTipoEstadoMov(false);
    }

    protected void ObtenerTipoEstadoMov(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            TipoEstadoMovBC tem = new TipoEstadoMovBC();
            DataTable dt = tem.obtenerTodoTipoEstadoMov();
            ViewState["lista"] = dt;
        }
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
            dw.Sort = (String)ViewState["sortExpresion"];
        this.gv_listaTipoEstadoMov.DataSource = dw;
        this.gv_listaTipoEstadoMov.DataBind();
    }

    #endregion

    #region Buttons

    public void btn_nuevoTipoEstadoMov_Click(object sender, EventArgs e)
    {
        hf_idTipoEstadoMov.Value = "";
        txt_editDesc.Text = "";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarTipoEstadoMov();", true);
    }

    public void btn_buscarTipoEstadoMov_Click(object sender, EventArgs e)
    {
        TipoEstadoMovBC tem = new TipoEstadoMovBC();
        DataTable dt = tem.obtenerXParametro(txt_buscarNombre.Text);
        ViewState["lista"] = dt;
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        if (ViewState["sortExpresion"] != null)
        {
            String sortExp = (String)ViewState["sortExpresion"];
            if (sortExp != "")
                dw.Sort = (String)ViewState["sortExpresion"];
        }
        this.gv_listaTipoEstadoMov.DataSource = dw;
        this.gv_listaTipoEstadoMov.DataBind();
        this.txt_buscarNombre.Focus();
    }

    public void btn_editGrabar_Click(object sender, EventArgs e)
    {
        TipoEstadoMovBC tem = new TipoEstadoMovBC();
        tem.DESCRIPCION = txt_editDesc.Text;
        if (hf_idTipoEstadoMov.Value == "")
        {
            if (tem.Crear(tem))
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Tipo Estado Movimiento creado exitosamente');", true);
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al agregar tipo estado movimiento. Intente nuevamente.');", true);
            hf_idTipoEstadoMov.Value = "";
            txt_editDesc.Text = "";
        }
        else
        {
            tem.ID = int.Parse(hf_idTipoEstadoMov.Value);
            if (tem.Modificar(tem))
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Tipo Estado Movimiento modificado exitosamente');", true);
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al modificar tipo estado movimiento. Intente nuevamente.');", true);
        }
        ObtenerTipoEstadoMov(true);
    }

    public void btn_EliminarTipoEstadoMov_Click(object sender, EventArgs e)
    {
        TipoEstadoMovBC tem = new TipoEstadoMovBC();
        if (tem.Eliminar(int.Parse(hf_idTipoEstadoMov.Value)))
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Tipo Estado Movimiento eliminado exitosamente');", true);
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al eliminar tipo estado movimiento. Revise si tiene otros datos asociados');", true);
        ObtenerTipoEstadoMov(true);
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            ObtenerTipoEstadoMov(true);
    }
}
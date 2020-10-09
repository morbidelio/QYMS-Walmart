// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Tipo_Trailer : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ObtenerTipoTrailer(false);
    }

    #region GridView

    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                string color = e.Row.Cells[4].Text;
                e.Row.Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml(color);
                e.Row.Cells[4].Text = "";
            }
            catch (Exception)
            {
            }
        }
    }

    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = this.utils.ConvertSortDirectionToSql((String)this.ViewState["sortOrder"]);
        this.ViewState["sortOrder"] = direccion;
        this.ViewState["sortExpresion"] = string.Format("{0} {1}", e.SortExpression, direccion);
        this.ObtenerTipoTrailer(false);
    }

    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        TrailerTipoBC tipo_trailer = new TrailerTipoBC();
        if (e.CommandName == "ELIMINAR")
        {
            this.hf_idTipoTrailer.Value = e.CommandArgument.ToString();
            tipo_trailer = tipo_trailer.obtenerXID(int.Parse(this.hf_idTipoTrailer.Value));
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalConfirmacion();", true);
        }
    }

    protected void gv_listar_RowEditing(object sender, GridViewEditEventArgs e)
    {
        TrailerTipoBC tipo_trailer = new TrailerTipoBC();
        this.gv_listar.SelectedIndex = e.NewEditIndex;
        this.hf_idTipoTrailer.Value = this.gv_listar.SelectedDataKey.Value.ToString();

        tipo_trailer = tipo_trailer.obtenerXID(int.Parse(this.gv_listar.SelectedDataKey.Value.ToString()));
        this.txt_editDesc.Text = tipo_trailer.DESCRIPCION;
        this.txt_editColor.Text = tipo_trailer.COLOR;
        //rcp_editColor.SelectedColor = System.Drawing.ColorTranslator.FromHtml(tipo_trailer.COLOR);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarTipoTrailer();", true);
    }

    protected void gv_listar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            this.gv_listar.PageIndex = e.NewPageIndex;
        }
        this.ObtenerTipoTrailer(false);
    }

    #endregion

    #region Buttons

    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        this.hf_idTipoTrailer.Value = "";
        this.txt_editDesc.Text = "";
        this.txt_editColor.Text = "#000000";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarTipoTrailer();", true);
    }

    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(this.txt_buscarNombre.Text))
        {
            this.ObtenerTipoTrailer(true);
        }
        else
        {
            DataView dw = new DataView((DataTable)this.ViewState["lista"]);
            dw.RowFilter = string.Format("DESCRIPCION LIKE '% {0}%'", this.txt_buscarNombre.Text);
            this.ViewState["filtrados"] = dw.ToTable();
            this.ObtenerTipoTrailer(false);
        }
    }

    protected void btn_grabar_Click(object sender, EventArgs e)
    {
        TrailerTipoBC tipo_trailer = new TrailerTipoBC();
        tipo_trailer.DESCRIPCION = this.txt_editDesc.Text;
        tipo_trailer.COLOR = this.txt_editColor.Text;

        if (this.hf_idTipoTrailer.Value == "")
        {
            if (tipo_trailer.Crear(tipo_trailer))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Tipo trailer creado exitosamente');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al agregar tipo trailer. Intente nuevamente.');", true);
            }
            this.hf_idTipoTrailer.Value = "";
            this.txt_editDesc.Text = "";
        }
        else
        {
            tipo_trailer.ID = int.Parse(this.hf_idTipoTrailer.Value);
            if (tipo_trailer.Modificar(tipo_trailer))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Tipo trailer modificado exitosamente');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al modificar tipo trailer. Intente nuevamente.');", true);
            }
        }
        this.ObtenerTipoTrailer(true);
    }

    protected void btn_eliminar_Click(object sender, EventArgs e)
    {
        TrailerTipoBC tipo_trailer = new TrailerTipoBC();
        if (tipo_trailer.Eliminar(int.Parse(this.hf_idTipoTrailer.Value)))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAlert('Tipo trailer eliminado exitosamente');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al eliminar tipo trailer. Revise si tiene otros datos asociados');", true);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalTipoTrailer')", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar_confirmacion", "cerrarModal('modalConfirmacion')", true);

        this.ObtenerTipoTrailer(true);
    }

    #endregion

    #region UtilsPagina

    private void ObtenerTipoTrailer(bool forzarBD)
    {
        if (this.ViewState["lista"] == null || forzarBD)
        {
            TrailerTipoBC tipo_trailer = new TrailerTipoBC();
            string descripcion = this.txt_buscarNombre.Text;
            DataTable dt = tipo_trailer.obtenerXParametro(descripcion);
            this.ViewState["lista"] = dt;
        }
        DataView dw = new DataView((DataTable)this.ViewState["lista"]);
        if (this.ViewState["sortExpresion"] != null && this.ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)this.ViewState["sortExpresion"];
        }
        this.gv_listar.DataSource = dw.ToTable();
        this.gv_listar.DataBind();
    }

    #endregion
}
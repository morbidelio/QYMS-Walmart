// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Destino : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            DestinoTipoBC dtt = new DestinoTipoBC();
            DataTable dt = dtt.obtenerTodo();
            foreach (DataRow dr in dt.Rows)
            {
                if (dt.Rows[0]["CODIGO"].ToString() == "DLPR")
                {
                    dt.Rows.Remove(dr);
                    break;
                }
            }
            this.utils.CargaDrop(this.ddl_editTipo, "ID", "NOMBRE", dt);
        }
        this.ObtenerDestinos(false);
    }

    #region GridView

    protected void gv_listar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            this.gv_listar.PageIndex = e.NewPageIndex;
        }
        this.ObtenerDestinos(false);
    }

    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITAR")
        {
            DestinoBC d = new DestinoBC();
            this.hf_idDestino.Value = e.CommandArgument.ToString();
            d = d.ObtenerSeleccionado(int.Parse(this.hf_idDestino.Value), null);
            this.txt_editCodigo.Text = d.CODIGO;
            this.txt_editNombre.Text = d.NOMBRE;
            this.ddl_editTipo.SelectedValue = d.DETI_ID.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalDestino();", true);
        }
        if (e.CommandName == "ELIMINAR")
        {
            this.hf_idDestino.Value = e.CommandArgument.ToString();

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalConfirmar();", true);
        }
    }

    #endregion

    #region Buttons

    protected void btn_Eliminar_Click(object sender, EventArgs e)
    {
        DestinoBC d = new DestinoBC();
        if (d.Eliminar(int.Parse(this.hf_idDestino.Value)))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Destino eliminado correctamente');", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalConfirmar');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Error');", true);
        }
        this.ObtenerDestinos(true);
    }

    protected void btn_guardar_Click(object sender, EventArgs e)
    {
        DestinoBC d = new DestinoBC();
        d.CODIGO = this.txt_editCodigo.Text;
        d.NOMBRE = this.txt_editNombre.Text;
        d.DETI_ID = int.Parse(this.ddl_editTipo.SelectedValue);
        if (string.IsNullOrEmpty(this.hf_idDestino.Value))
        {
            if (d.Agregar(d))
            {
                this.ObtenerDestinos(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Destino agregado correctamente');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalDestino');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Error');", true);
            }
        }
        else
        {
            d.ID = int.Parse(this.hf_idDestino.Value);
            if (d.Modificar(d))
            {
                this.ObtenerDestinos(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Destino modificado correctamente');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalDestino');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Error');", true);
            }
        }
    }

    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        this.LimpiarTodo();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalDestino();", true);
    }

    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        this.ObtenerDestinos(true);
    }

    #endregion

    #region UtilsPagina

    private void ObtenerDestinos(bool forzarBD)
    {
        if (this.ViewState["lista"] == null || forzarBD)
        {
            DestinoBC d = new DestinoBC();
            string nombre = txt_buscarNombre.Text;
            DataTable dt = d.ObtenerXParametros(nombre);
            this.ViewState["lista"] = dt;
        }
        DataView dw = new DataView((DataTable)this.ViewState["lista"]);
        if (this.ViewState["sortExpresion"] != null && this.ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)this.ViewState["sortExpresion"];
        }
        this.gv_listar.DataSource = dw;
        this.gv_listar.DataBind();
    }

    private void LimpiarTodo()
    {
        this.hf_idDestino.Value = "";
        this.txt_editCodigo.Text = "";
        this.txt_editNombre.Text = "";
        this.ddl_editTipo.ClearSelection();
    }

    #endregion
}
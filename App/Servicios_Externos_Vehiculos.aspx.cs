// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Servicios_Externos_Vehiculos : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.CargarDrops();
        }
        this.ObtenerServicios(false);
    }

    #region Buttons

    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        ObtenerServicios(true);
    }

    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        this.LimpiarForm();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditar();", true);
    }

    protected void btn_Eliminar_Click(object sender, EventArgs e)
    {
        ServiciosExternosVehiculosBC s = new ServiciosExternosVehiculosBC();
        if (s.Eliminar(int.Parse(this.hf_id.Value)))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAlert('Vehículo eliminado exitosamente');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Error');", true);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalConfirmar');", true);
        this.ObtenerServicios(true);
    }

    protected void btn_editGrabar_Click(object sender, EventArgs e)
    {
        ServiciosExternosVehiculosBC s = new ServiciosExternosVehiculosBC();
        s = this.LlenarBC();
        TrailerBC t = new TrailerBC();
        t = t.obtenerXPlaca(s.PLACA);
        if (t.ID > 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Patente corresponde a Trailer');", true);
        }
        else if (this.utils.patentevalida(s.PLACA) == true)
        {
            if (string.IsNullOrEmpty(this.hf_id.Value))
            {
                if (s.ObtenerXPlaca(s.PLACA).SEVE_ID > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Patente existente');", true);
                }
                else if (s.Crear())
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "showAlert('Vehículo agregado correctamente');", true);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalEditar');", true);
                    this.ObtenerServicios(true); 
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Error');", true);
                }
            }
            else
            {
                s.SEVE_ID = Convert.ToInt32(this.hf_id.Value);
                if (s.ObtenerXPlaca(s.PLACA).SEVE_ID > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Patente existente');", true);
                }
                else if (s.Modificar())
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "showAlert('Vehículo modificado correctamente');", true);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalEditar');", true);
                    this.ObtenerServicios(true); 
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Error');", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Patente Invalida');", true);
        }
    }

    #endregion

    #region GridView

    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = this.utils.ConvertSortDirectionToSql((String)this.ViewState["sortOrder"]);
        this.ViewState["sortOrder"] = direccion;
        this.ViewState["sortExpresion"] = string.Format("{0} {1}", e.SortExpression, direccion);
        this.ObtenerServicios(false);
    }

    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITAR")
        {
            ServiciosExternosVehiculosBC s = new ServiciosExternosVehiculosBC();
            this.hf_id.Value = e.CommandArgument.ToString();
            this.LlenarForm();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditar();", true);
        }
        else if (e.CommandName == "ELIMINAR")
        {
            this.hf_id.Value = e.CommandArgument.ToString();
            this.lbl_tituloConfirmar.Text = "Eliminar servicio externo";
            this.lbl_msjConfirmar.Text = "Se eliminará el servicio externo seleccionado, ¿desea continuar?";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalConfirmar();", true);
        }
    }

    protected void gv_listar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            this.gv_listar.PageIndex = e.NewPageIndex;
        }
        this.ObtenerServicios(false);
    }

    #endregion

    #region UtilsPagina

    private void CargarDrops()
    {
        CargaDrops c = new CargaDrops();
        c.Proveedor(this.ddl_editProv);
    }

    private void ObtenerServicios(bool forzarBD)
    {
        if (this.ViewState["listar"] == null || forzarBD)
        {
            ServiciosExternosVehiculosBC s = new ServiciosExternosVehiculosBC();
            string codigo = txt_buscarCodigo.Text;
            string placa = txt_buscarPlaca.Text;
            DataTable dt = s.ObtenerXParametros(codigo,placa);
            this.ViewState["listar"] = dt;
        }
        DataView dw = new DataView((DataTable)this.ViewState["listar"]);
        if (this.ViewState["sortExpresion"] != null && this.ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)this.ViewState["sortExpresion"];
        }
        this.gv_listar.DataSource = dw.ToTable();
        this.gv_listar.DataBind();
    }

    private void LlenarForm()
    {
        ServiciosExternosVehiculosBC s = new ServiciosExternosVehiculosBC();
        s.SEVE_ID = Convert.ToInt32(this.hf_id.Value);
        s = s.ObtenerXId();
        this.txt_editCodigo.Text = s.CODIGO;
        this.txt_editPlaca.Text = s.PLACA;
        this.ddl_editProv.SelectedValue = s.PROV_ID.ToString();
    }

    private void LimpiarForm()
    {
        this.hf_id.Value = "";
        this.txt_editCodigo.Text = "";
        this.txt_editPlaca.Text = "";
        this.ddl_editProv.ClearSelection();
    }

    private ServiciosExternosVehiculosBC LlenarBC()
    {
        ServiciosExternosVehiculosBC s = new ServiciosExternosVehiculosBC();
        s.CODIGO = this.txt_editPlaca.Text;
        s.PLACA = this.txt_editPlaca.Text;
        s.PROV_ID = Convert.ToInt32(this.ddl_editProv.SelectedValue);
        return s;
    }
    #endregion
}
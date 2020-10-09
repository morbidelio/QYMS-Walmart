// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Conductor_Serv_Ext : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ObtenerConductores(false);
    }
    #region TextBox
    protected void txt_editRut_TextChanged(object sender, EventArgs e)
    {
        if (this.validarRut(txt_editRut.Text))
        {
            ServiciosExternosConductorBC c = new ServiciosExternosConductorBC();
            c = c.ObtenerXRut(this.txt_editRut.Text);
            if (c.ID != 0)
            {
                this.hf_id.Value = c.ID.ToString();
                this.txt_editNombre.Text = c.NOMBRE;
                this.txt_editRut.Enabled = false;
                utils.ShowMessage2(this, "crear", "warn_rutEncontrado");
            }
            this.txt_editNombre.Focus();
        }
        else
        {
            utils.ShowMessage2(this, "crear", "warn_rutNoValido");
            this.txt_editRut.Text = "";
            this.txt_editRut.Focus();
        }
    }
    #endregion
    #region Buttons
    protected void btn_Conf_Click(object sender, EventArgs e)
    {
        ServiciosExternosConductorBC c = new ServiciosExternosConductorBC();
        c.ID = Convert.ToInt32(this.hf_id.Value);
        if (c.Eliminar())
        {
            utils.ShowMessage2(this, "eliminar", "success");
        }
        else
        {
            utils.ShowMessage2(this, "eliminar", "error");
        }
        this.ObtenerConductores(true);
        utils.CerrarModal(this, "modalConf");
    }
    protected void btn_grabar_Click(object sender, EventArgs e)
    {
        ServiciosExternosConductorBC c = new ServiciosExternosConductorBC();
        c.RUT = this.txt_editRut.Text;
        c.NOMBRE = this.txt_editNombre.Text;
        if (string.IsNullOrEmpty(this.hf_id.Value))
        {
            if (c.Agregar(c))
            {
                utils.ShowMessage2(this, "crear", "success");
                utils.CerrarModal(this, "modalEdit");
            }
            else
            {
                utils.ShowMessage2(this, "crear", "error");
            }
        }
        else
        {
            c.ID = Convert.ToInt32(this.hf_id.Value);
            if (c.Modificar(c))
            {
                utils.ShowMessage2(this, "modificar", "success");
                utils.CerrarModal(this, "modalEdit");
            }
            else
            {
                utils.ShowMessage2(this, "modificar", "success");
            }
        }
        this.ObtenerConductores(true);
    }
    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        this.LimpiarTodo();
        utils.AbrirModal(this,"modalEdit");
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        ObtenerConductores(true);
    }
    #endregion
    #region GridView
    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = this.utils.ConvertSortDirectionToSql((String)this.ViewState["sortOrder"]);
        this.ViewState["sortOrder"] = direccion;
        this.ViewState["sortExpresion"] = string.Format("{0} {1}", e.SortExpression, direccion);
        this.ObtenerConductores(false);
    }
    protected void gv_listar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            this.gv_listar.PageIndex = e.NewPageIndex;
        }
        this.ObtenerConductores(false);
    }
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITAR")
        {
            this.hf_id.Value = e.CommandArgument.ToString();
            ServiciosExternosConductorBC c = new ServiciosExternosConductorBC();
            c = c.ObtenerXId(Convert.ToInt32(this.hf_id.Value));
            this.txt_editRut.Text = c.RUT;
            this.txt_editRut.Enabled = false;
            this.txt_editNombre.Text = c.NOMBRE;
            utils.AbrirModal(this, "modalEdit");
        }
        if (e.CommandName == "ELIM")
        {
            this.hf_id.Value = e.CommandArgument.ToString();
            this.hf_confirmar.Value = "ELIM";
            this.lbl_tituloConfirmar.Text = "Eliminar Conductor";
            this.lbl_mensajeConfirmar.Text = "Se eliminará el conductor seleccionado, ¿desea continuar?";
            utils.AbrirModal(this, "modalConf");
        }
    }
    #endregion
    #region UtilsPagina
    private bool validarRut(string rut)
    {
        bool validacion = false;
        try
        {
            rut = rut.ToUpper();
            rut = rut.Replace(".", "");
            rut = rut.Replace("-", "");
            int rutAux = Convert.ToInt32(rut.Substring(0, rut.Length - 1));

            char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

            int m = 0, s = 1;
            for (; rutAux != 0; rutAux /= 10)
            {
                s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
            }
            if (dv == (char)(s != 0 ? s + 47 : 75))
            {
                validacion = true;
            }
        }
        catch (Exception)
        {
            validacion = false;
        }
        return validacion;
    }
    private void ObtenerConductores(bool forzarBD)
    {
        if (this.ViewState["lista"] == null || forzarBD)
        {
            ServiciosExternosConductorBC c = new ServiciosExternosConductorBC();
            string rut = txt_buscarRut.Text;
            string nombre = txt_buscarNombre.Text;
            this.ViewState["lista"] = c.ObtenerXParametros(rut, nombre);
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
        this.hf_id.Value = "";
        this.txt_editNombre.Text = "";
        this.txt_editRut.Text = "";
        this.txt_editRut.Enabled = true;
    }
    #endregion
}
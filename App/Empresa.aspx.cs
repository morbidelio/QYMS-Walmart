using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class App_Empresa : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();
    protected void ddl_editRegionIndexChanged(object sender, EventArgs e)
    {
        if (ddl_editRegion.Items.Count > 1 && ddl_editRegion.SelectedIndex >= 1)
        {
            ComunaBC comuna = new ComunaBC();
            utils.CargaDrop(ddl_editComuna, "ID", "NOMBRE", comuna.obtenerComunasXRegion(Convert.ToInt32(ddl_editRegion.SelectedValue)));
            if (ddl_editComuna.Items.Count > 1)
                ddl_editComuna.Enabled = true;
            else
                ddl_editComuna.Enabled = false;
        }
        else
            ddl_editComuna.Enabled = false;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RegionBC region = new RegionBC();
            utils.CargaDrop(ddl_editRegion, "ID", "NOMBRE", region.obtenerTodoRegion());
            ddl_editRegionIndexChanged(null, null);
            ObtenerEmpresas(true);
        }
    }
    #region GridView
    protected void gv_listaEmpresas_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerEmpresas(false);
    }
    protected void gv_listaEmpresas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName == "EDITAR")
        {
            limpiarTodo();
            hf_idEmpresa.Value = e.CommandArgument.ToString();
            activarDesactivar(true);
            EmpresaBC empresa = new EmpresaBC();
            empresa = empresa.ObtenerTodoXId(Convert.ToInt32(hf_idEmpresa.Value));
            llenarDatos(empresa);
            utils.AbrirModal(this, "modalEdit");
        }
        if (e.CommandName == "VER")
        {
            limpiarTodo();
            EmpresaBC empresa = new EmpresaBC();
            hf_idEmpresa.Value = e.CommandArgument.ToString();
            empresa = empresa.ObtenerTodoXId(Convert.ToInt32(hf_idEmpresa.Value));
            llenarDatos(empresa);
            activarDesactivar(false);
            utils.AbrirModal(this, "modalEdit");
        }
        else if (e.CommandName == "ELIMINAR")
        {
            limpiarTodo();
            hf_idEmpresa.Value = e.CommandArgument.ToString();
            lblRazonEliminacion.Text = "Eliminar empresa";
            msjEliminacion.Text = "Se eliminará la empresa seleccionada. ¿Desea continuar?";
            utils.AbrirModal(this, "modalConf");
        }
    }
    #endregion
    #region Botones
    protected void btn_eliminarEmpresa_click(object sender, EventArgs e)
    {
        EmpresaBC empresa = new EmpresaBC();
        if (empresa.Eliminar(Convert.ToInt32(hf_idEmpresa.Value)))
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Empresa eliminada exitosamente');", true);
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al eliminar empresa. Revise si la empresa tiene otros datos asociados');", true);
        ObtenerEmpresas(true);
    }
    protected void btn_editGrabarNuevo_Click(object sender, EventArgs e)
    {
        activarDesactivar(true);
        string exito;
        EmpresaBC empresa = llenarEmpresa();
        if (hf_idEmpresa.Value == "")
        {
            empresa.DESCRIPCION = "";
            empresa.USR_CREACION = "";
            if (empresa.Crear(empresa))
                exito = "Empresa creada correctamente.";
            else
                exito = "Error: Empresa no pudo ser creada. Revise los datos.";
        }
        else
        {
            empresa.DESCRIPCION = "";
            empresa.ID = Convert.ToInt32(hf_idEmpresa.Value);
            empresa.USR_MODIFICACION = "";
            if (empresa.Modificar(empresa))
                exito = "Empresa modificada correctamente.";
            else
                exito = "Error: Empresa no pudo ser modificada. Revise los datos.";
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('" + exito + "');", true);
        ObtenerEmpresas(true);
    }
    protected void btn_nuevaEmpresa_Click(object sender, EventArgs e)
    {
        limpiarTodo();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarEmpresa();", true);
    }
    protected void btn_buscarEmpresa_Click(object sender, EventArgs e)
    {
        EmpresaBC empresa = new EmpresaBC();
        DataTable dt = empresa.ObtenerXParametro(txt_buscarRut.Text, txt_buscarRSocial.Text, txt_buscarNFantasia.Text);
        ViewState["lista"] = dt;
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
            dw.Sort = (String)ViewState["sortExpresion"];
        this.gv_listar.DataSource = dw;
        this.gv_listar.DataBind();
        this.gv_listar.Visible = true;
        this.txt_buscarRut.Focus();
    }
    #endregion
    #region TextBox
    protected void txt_editRut_TextChanged(object sender, EventArgs e)
    {
        if (this.txt_buscarRut.Text != "")
        {
            if (this.txt_buscarRut.Text != "")
            {
                EmpresaBC empresa = new EmpresaBC();
                empresa = empresa.ObtenerTodoXRut(txt_buscarRut.Text.Replace(".",""));
                if (validarRut(txt_buscarRut.Text) == false)
                {
                    this.txt_buscarRut.Text = "";
                    this.txt_buscarRut.Focus();
                    string texto = "El rut ingresado no es válido!";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "mensaje('" + texto + "');", true);
                }
                else
                {
                    //if (empresa.EXISTE)
                    //{
                    //    string texto = "El cliente ingresado ya existe, pero no lo puede modificar, ya que esta operacion no lo tiene asignado!";
                    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "mensaje('" + texto + "');", true);
                    //    limpiarTodo();
                    //    this.txt_buscarRut.Focus();
                    //}
                    if (empresa.EXISTE)
                    {
                        string texto = "El rut ya se encuentra registrado como cliente, por ello se han cargado sus datos!";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "mensaje('" + texto + "');", true);
                        this.txt_buscarRut.Focus();
                        empresa = empresa.ObtenerTodoXRut(txt_buscarRut.Text);
                        txt_editRut.Text = empresa.RUT;
                        txt_editRsocial.Text = empresa.RAZON_SOCIAL;
                        txt_editGiro.Text = empresa.GIRO;
                        txt_editNombreFantasia.Text = empresa.NOMBRE_FANTASIA;
                        txt_editTelefono.Text = empresa.TELEFONO;
                        txt_editContacto.Text = empresa.NOMBRE_CONTACTO;
                        txt_editEmail.Text = empresa.EMAIL;
                        this.txt_editRsocial.Focus();
                    }
                    else
                    {
                        this.hf_idEmpresa.Value = "";
                        this.txt_editRsocial.Focus();
                    }
                }
            }
        }
    }
    #endregion
    #region FuncionesPagina
    private void limpiarTodo()
    {
        hf_idEmpresa.Value = "";
        txt_editCodigo.Text = "";
        txt_editRut.Text = "";
        txt_editRsocial.Text = "";
        txt_editGiro.Text = "";
        txt_editNombreFantasia.Text = "";
        txt_editBodega.Text = "";
        txt_editTelefono.Text = "";
        txt_editContacto.Text = "";
        txt_editEmail.Text = "";
        ddl_editComuna.ClearSelection();
        ddl_editRegion.ClearSelection();
        txt_editDireccion.Text = "";
    }
    private void llenarDatos(EmpresaBC empresa)
    {
        ComunaBC comu = new ComunaBC();
        comu = comu.obtenerXID(empresa.ID_COMUNA);
        hf_idEmpresa.Value = empresa.ID.ToString();
        txt_editCodigo.Text = empresa.CODIGO;
        txt_editRut.Text = empresa.RUT;
        txt_editRsocial.Text = empresa.RAZON_SOCIAL;
        txt_editGiro.Text = empresa.GIRO;
        txt_editNombreFantasia.Text = empresa.NOMBRE_FANTASIA;
        txt_editBodega.Text = empresa.BODEGA;
        txt_editTelefono.Text = empresa.TELEFONO;
        txt_editContacto.Text = empresa.NOMBRE_CONTACTO;
        txt_editEmail.Text = empresa.EMAIL;
        if (empresa.ID_COMUNA != 0)
        {
            ddl_editRegion.SelectedValue = comu.ID_REGION.ToString();
            utils.CargaDrop(ddl_editComuna, "ID", "NOMBRE", comu.obtenerComunasXRegion(Convert.ToInt32(ddl_editRegion.SelectedValue)));
            ddl_editComuna.SelectedValue = empresa.ID_COMUNA.ToString();
            ddl_editComuna.Enabled = true;
        }
        else
        {
            ddl_editRegion.ClearSelection();
            ddl_editComuna.ClearSelection();
            ddl_editComuna.Enabled = false;
        }
        txt_editDireccion.Text = empresa.DIRECCION;
    }
    private void activarDesactivar(bool estado)
    {
        txt_editRut.Enabled = estado;
        txt_editRsocial.Enabled = estado;
        txt_editGiro.Enabled = estado;
        txt_editNombreFantasia.Enabled = estado;
        txt_editBodega.Enabled = estado;
        txt_editTelefono.Enabled = estado;
        txt_editContacto.Enabled = estado;
        txt_editEmail.Enabled = estado;
        ddl_editComuna.Enabled = estado;
        txt_editDireccion.Enabled = estado;
        btn_editGrabar.Visible = estado;
    }
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
        catch (Exception e)
        {
            throw e;
        }
        return validacion;
    }
    private EmpresaBC llenarEmpresa()
    {
        EmpresaBC empresa = new EmpresaBC();
        empresa.CODIGO = txt_editCodigo.Text;
        empresa.RUT = txt_editRut.Text;
        empresa.RAZON_SOCIAL = txt_editRsocial.Text;
        empresa.DIRECCION = txt_editDireccion.Text;
        empresa.ID_COMUNA = Convert.ToInt32(ddl_editComuna.SelectedValue);
        empresa.GIRO = txt_editGiro.Text;
        empresa.NOMBRE_FANTASIA = txt_editNombreFantasia.Text;
        empresa.LATITUD = 0;
        empresa.LONGITUD = 0;
        empresa.TELEFONO = txt_editTelefono.Text;
        empresa.EMAIL = txt_editEmail.Text;
        empresa.NOMBRE_CONTACTO = txt_editContacto.Text;
        empresa.ACTIVO = true;
        empresa.BODEGA = txt_editBodega.Text;
        empresa.CODIGO = empresa.GIRO.Substring(0, 2) + "_" + empresa.NOMBRE_FANTASIA.Substring(0, 2);
        return empresa;
    }
    private void ObtenerEmpresas(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            EmpresaBC empresa = new EmpresaBC();
            DataTable dt = empresa.ObtenerTodas();
            ViewState["lista"] = dt;
        }
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
            dw.Sort = (String)ViewState["sortExpresion"];
        this.gv_listar.DataSource = dw;
        this.gv_listar.DataBind();
    }
    #endregion

    protected void gv_listar_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.TableSection = TableRowSection.TableFooter;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class App_proveedores : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ObtenerProveedor(true);
        }
    }
    #region Gridview
    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = string.Format("{0} {1}", e.SortExpression, direccion);
        ObtenerProveedor(false);
    }
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName == "MODIFICAR")
        {
            Limpiar();
            ProveedorBC prov = new ProveedorBC();
            prov = prov.ObtenerXId(Convert.ToInt32(e.CommandArgument));
            LlenarForm(prov);
            utils.AbrirModal(this, "modalEdit");
        }
        if (e.CommandName == "ELIMINAR")
        {
            hf_id.Value = e.CommandArgument.ToString();
            utils.AbrirModal(this, "modalConfirmar");
        }
        if (e.CommandName == "VENDOR")
        {
            hf_id.Value = e.CommandArgument.ToString();
            ObtenerVendor();
            utils.AbrirModal(this, "modalVendor");
        }
    }
    protected void gv_listar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            gv_listar.PageIndex = e.NewPageIndex;
        }
        ObtenerProveedor(false);
    }
    #endregion
    #region Buttons
    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        Limpiar();
        utils.AbrirModal(this, "modalEdit");
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        this.ObtenerProveedor(true);
        this.txt_buscarNombre.Focus();
    }
    protected void btn_Eliminar_Click(object sender, EventArgs e)
    {
        ProveedorBC prov = new ProveedorBC();
        if (prov.Eliminar(Convert.ToInt32(hf_id.Value)))
        {
            utils.ShowMessage2(this, "eliminar", "success");
            utils.CerrarModal(this, "modalConfirmar");
        }
        else
        {
            utils.ShowMessage2(this, "eliminar", "error");
        }
        ObtenerProveedor(true);
    }
    protected void btn_editGrabar_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txt_editCodigo.Text)) { utils.ShowMessage2(this, "modificar", "warn_codigoVacio"); return; }
        if (string.IsNullOrEmpty(txt_editDesc.Text)) { utils.ShowMessage2(this, "modificar", "warn_descripcionVacio"); return; }
        if (string.IsNullOrEmpty(txt_editRut.Text)) { utils.ShowMessage2(this, "modificar", "warn_rutVacio"); return; }
        ProveedorBC prov = new ProveedorBC();
        prov.CODIGO = txt_editCodigo.Text;
        prov.DESCRIPCION = txt_editDesc.Text;
        prov.DIRECCION = txt_editDirec.Text;
        prov.RUT = txt_editRut.Text;
        if (string.IsNullOrEmpty(hf_id.Value))
        {
            if (prov.Crear(prov))
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
            prov.PROV_ID = Convert.ToInt32(hf_id.Value);
            if (prov.Modificar(prov))
            {
                utils.ShowMessage2(this, "modificar", "success");
                utils.CerrarModal(this, "modalEdit");
            }
            else
            {
                utils.ShowMessage2(this, "modificar", "error");
            }
        }
        ObtenerProveedor(true);
    }
    #endregion
    #region TextBox
    protected void txt_editCodigo_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txt_editCodigo.Text)) {
            ProveedorBC p = new ProveedorBC();
            if (!string.IsNullOrEmpty(hf_id.Value))
            {
                p.PROV_ID = Convert.ToInt32(hf_id.Value);
            }
            p.CODIGO = txt_editCodigo.Text;
            p = p.ComprobarCodigoExistente();
            if (p.PROV_ID != 0)
            {
                if (string.IsNullOrEmpty(hf_id.Value))
                {
                    Limpiar();
                    LlenarForm(p);
                    utils.ShowMessage2(this, "modificar", "warn_codigoExisteCarga");
                }
                else
                {
                    txt_editCodigo.Text = "";
                    txt_editCodigo.Focus();
                    utils.ShowMessage2(this, "modificar", "warn_codigoExiste");
                }
            }
            else
            {
                txt_editRut.Focus();
            }
        }
    }
    protected void txt_editRut_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txt_editRut.Text))
        {
            ProveedorBC p = new ProveedorBC();
            if (!string.IsNullOrEmpty(hf_id.Value))
            {
                p.PROV_ID = Convert.ToInt32(hf_id.Value);
            }
            p.RUT = txt_editRut.Text;
            p = p.ComprobarCodigoExistente();
            if (p.PROV_ID != 0)
            {
                if (string.IsNullOrEmpty(hf_id.Value))
                {
                    Limpiar();
                    LlenarForm(p);
                    utils.ShowMessage2(this, "modificar", "warn_codigoExisteCarga");
                }
                else
                {
                    txt_editRut.Text = "";
                    txt_editRut.Focus();
                    utils.ShowMessage2(this, "modificar", "warn_codigoExiste");
                }
            }
            else
            {
                txt_editDesc.Focus();
            }
        }
    }
    #endregion
    #region UtilsPagina
    private void ObtenerProveedor(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            ProveedorBC prov = new ProveedorBC();
            string codigo = txt_buscarCodigo.Text;
            string descripcion = txt_buscarNombre.Text;
            ViewState["lista"] = prov.ObtenerXParametros(codigo, descripcion);
        }
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)ViewState["sortExpresion"];
        }
        this.gv_listar.DataSource = dw.ToTable();
        this.gv_listar.DataBind();
    }
    private void ObtenerVendor()
    {
        int prov_id = Convert.ToInt32(hf_id.Value);
        DataTable dt = new ProveedorBC().obtenerVendorXParametros(prov_id, txt_vendorNro.Text);
        gv_vendor.DataSource = dt;
        gv_vendor.DataBind();
    }
    private void LlenarForm(ProveedorBC p)
    {
        hf_id.Value = p.PROV_ID.ToString();
        txt_editCodigo.Text = p.CODIGO;
        txt_editDesc.Text = p.DESCRIPCION;
        txt_editDirec.Text = p.DIRECCION;
        txt_editRut.Text = p.RUT;
    }
    private void Limpiar()
    {
        hf_id.Value = "";
        txt_editCodigo.Text = "";
        txt_editDesc.Text = "";
        txt_editDirec.Text = "";
    }
    #endregion
    // viewstate
    protected override void SavePageStateToPersistenceMedium(object state)
    {
        string file = this.GenerateFileName();

        FileStream filestream = new FileStream(file, FileMode.Create);

        LosFormatter formator = new LosFormatter();

        formator.Serialize(filestream, state);

        filestream.Flush();

        filestream.Close();

        filestream = null;
    }

    protected override object LoadPageStateFromPersistenceMedium()
    {
        object state = null;
        try
        {
            StreamReader reader = new StreamReader(this.GenerateFileName());

            LosFormatter formator = new LosFormatter();

            state = formator.Deserialize(reader);

            reader.Close();
        }
        catch (Exception)
        {
            this.Response.Redirect(string.Format("{0}.aspx", Path.GetFileNameWithoutExtension(this.Page.AppRelativeVirtualPath)));
        }
        return state;
    }

    private string GenerateFileName()
    {
        string pageName = Path.GetFileNameWithoutExtension(this.Page.AppRelativeVirtualPath);

        string file = string.Format("{0}{1}.txt", pageName, this.Session.SessionID.ToString());

        //       file = Path.Combine(Server.MapPath("~/ViewStateFiles") + "/" + file);  
        file = string.Format("{0}\\{1}", utils.pathviewstate(), file);

        return file;
    }

    protected void btn_vendorBuscar_Click(object sender, EventArgs e)
    {
        ObtenerVendor();
    }

    protected void btn_vendorAgregar_Click(object sender, EventArgs e)
    {
        try
        {
            int prov_id = Convert.ToInt32(hf_id.Value);
            int prve_numero = Convert.ToInt32(txt_vendorNro.Text);
            ProveedorBC p = new ProveedorBC();
            if (!p.ComprobarNroVendorExistente(prve_numero)) { utils.ShowMessage2(this, "crearVendor", "warn_nroExiste"); return; }
            if (p.AgregarVendor(prov_id, prve_numero))
            {
                utils.ShowMessage2(this, "crearVendor", "success");
            }
            else
            {
                utils.ShowMessage2(this, "crearVendor", "error");
            }
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            ObtenerVendor();
            ObtenerProveedor(true);
        }
    }

    protected void gv_vendor_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ELIMINAR")
        {
            try
            {
                int prve_id = Convert.ToInt32(e.CommandArgument);
                ProveedorBC p = new ProveedorBC();
                if (p.EliminarVendor(prve_id))
                {
                    utils.ShowMessage2(this, "eliminarvendor", "success");
                }
                else
                {
                    utils.ShowMessage2(this, "eliminarvendor", "error");
                }
            }
            catch (Exception ex)
            {
                utils.ShowMessage(this, ex.Message, "error", false);
            }
            finally
            {
                ObtenerVendor();
                ObtenerProveedor(true);
            }
        }
    }

    protected void gv_vendor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int prov_id = Convert.ToInt32(hf_id.Value);
            DataTable dt = new ProveedorBC().obtenerVendorXParametros(prov_id);
            if (dt.Rows.Count <= 1)
                ((LinkButton)e.Row.FindControl("btn_eliminarVendor")).Style.Add("visibility", "hidden");
        }
    }  
}
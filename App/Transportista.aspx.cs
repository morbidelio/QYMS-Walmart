// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class App_Transportista : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ObtenerTransportista(false);
    }

    #region GridView

    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = this.utils.ConvertSortDirectionToSql((String)this.ViewState["sortOrder"]);
        this.ViewState["sortOrder"] = direccion;
        this.ViewState["sortExpresion"] = string.Format("{0} {1}", e.SortExpression, direccion);
        this.ObtenerTransportista(false);
    }

    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ELIMINAR")
        {
            this.hf_id.Value = (e.CommandArgument.ToString());
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEliminar();", true);
        }
    }

    protected void gv_listar_RowEditing(object sender, GridViewEditEventArgs e)
    {
        TransportistaBC t = new TransportistaBC();
        this.gv_listar.SelectedIndex = e.NewEditIndex;
        this.hf_id.Value = this.gv_listar.SelectedDataKey.Value.ToString();
        t = t.ObtenerXId(int.Parse(this.hf_id.Value));
        this.txt_editRut.Enabled = false;
        this.llenarDatos(t);
        this.customRut.Enabled = false;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalTransportista();", true);
    }

    protected void gv_listar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            this.gv_listar.PageIndex = e.NewPageIndex;
        }
        this.ObtenerTransportista(false);
    }

    #endregion

    #region Botones

    protected void btn_eliminar_click(object sender, EventArgs e)
    {
        TransportistaBC transportista = new TransportistaBC();
        if (transportista.Eliminar(Convert.ToInt32(this.hf_id.Value)))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Transportista eliminado exitosamente');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Ocurrió un error al eliminar transportista. Revise si tiene otros datos asociados');", true);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalEliminar');", true);
        this.ObtenerTransportista(true);
    }

    protected void btn_grabar_Click(object sender, EventArgs e)
    {
        string exito;
        TransportistaBC transportista = this.llenarTransportista();
        if (this.hf_id.Value == "")
        {
            if (transportista.Crear(transportista))
            {
                exito = "Todo OK!";
            }
            else
            {
                exito = "Error!";
            }
        }
        else
        {
            transportista.ID = int.Parse(this.hf_id.Value);
            if (transportista.Modificar(transportista))
            {
                exito = "Todo OK!";
            }
            else
            {
                exito = "Error!";
            }
        }
        this.ObtenerTransportista(true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("alert('{0}');", exito), true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalTransportista');", true);
    }

    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        this.limpiarTodo();
        this.txt_editRut.Enabled = true;
        this.customRut.Enabled = true;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalTransportista();", true);
    }

    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        this.ObtenerTransportista(true);
    }

    #endregion

    #region TextBox

    protected void txt_editRut_TextChanged(object sender, EventArgs e)
    {
        if (this.txt_editRut.Text != "")
        {
            TransportistaBC t = new TransportistaBC();
            t = t.ObtenerTodoXRut(this.txt_editRut.Text.Replace(".", ""));
            if (!this.utils.validarRut(this.txt_editRut.Text))
            {
                this.txt_editRut.Focus();
                string texto = "El rut ingresado no es válido.";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("showAlert4('{0}');", texto), true);
                this.limpiarTodo();
            }
            else
            {
                if (string.IsNullOrEmpty(this.hf_id.Value))
                {
                    if (t.EXISTE)
                    {
                        string texto = "Transportista existe. Se han cargado los datos.";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("showAlert('{0}');", texto), true);
                        this.txt_buscarRut.Focus();
                        this.hf_id.Value = t.ID.ToString();
                        this.txt_editRut.Enabled = false;
                        this.txt_editNombre.Text = t.NOMBRE;
                        //txt_editPass.Text = transportista.PASS;
                        this.txt_editRol.Text = t.ROL.ToString();
                        this.txt_editNombre.Focus();
                    }
                    else
                    {
                        this.hf_id.Value = "";
                        this.txt_editNombre.Focus();
                    }
                }
            }
        }
    }

    #endregion

    #region FuncionesPagina

    private void limpiarTodo()
    {
        this.hf_id.Value = "";
        this.txt_editRut.Text = "";
        this.txt_editNombre.Text = "";
        this.txt_editRol.Text = "";
    }

    private void llenarDatos(TransportistaBC transportista)
    {
        this.hf_id.Value = transportista.ID.ToString();
        this.txt_editRut.Text = transportista.RUT;
        this.txt_editNombre.Text = transportista.NOMBRE;
        this.txt_editRol.Text = transportista.ROL.ToString();
    }

    private bool validarRut(string rut)
    {
        bool validacion = false;
        try
        {
            rut = rut.ToUpper();
            rut = rut.Replace(".", "");
            rut = rut.Replace("-", "");
            int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

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

    private TransportistaBC llenarTransportista()
    {
        TransportistaBC t = new TransportistaBC();
        t.RUT = this.txt_editRut.Text.Replace(".", "");
        t.NOMBRE = this.txt_editNombre.Text;
        t.ROL = int.Parse(this.txt_editRol.Text);
        return t;
    }

    private void ObtenerTransportista(bool forzarBD)
    {
        if (this.ViewState["lista"] == null || forzarBD)
        {
            TransportistaBC transportista = new TransportistaBC();
            string nombre = this.txt_buscarNombre.Text;
            string rut = this.txt_buscarRut.Text;
            DataTable dt = transportista.ObtenerXParametro(rut, nombre);
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


    // viewstate
    protected override void SavePageStateToPersistenceMedium(object state)
    {

        string file = GenerateFileName();

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



            StreamReader reader = new StreamReader(GenerateFileName());

            LosFormatter formator = new LosFormatter();

            state = formator.Deserialize(reader);

            reader.Close();


        }
        catch (Exception ex)
        {
            Response.Redirect(Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + ".aspx");
        }
        return state;
    }

    private string GenerateFileName()
    {

        string pageName = Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);

        string file = pageName + Session.SessionID.ToString() + ".txt";

        //       file = Path.Combine(Server.MapPath("~/ViewStateFiles") + "/" + file);  
        file = utils.pathviewstate() + "\\" + file;

        return file;

    }  
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Conductor : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    UsuarioBC usuario = new UsuarioBC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.usuario = (UsuarioBC)this.Session["Usuario"];
            TransportistaBC t = new TransportistaBC();
            ddl_editTran.DataSource = t.ObtenerTodos();
            ddl_editTran.DataTextField = "NOMBRE";

            ddl_editTran.DataValueField = "ID";
            ddl_editTran.DataBind();
            ddl_editTran.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("Seleccione", "0"));
            utils.CargaDropTodos(ddl_buscarTran, "ID", "NOMBRE", t.ObtenerTodos());
        }
        ObtenerConductores(false);
    }
    #region TextBox
    protected void txt_editRut_TextChanged(object sender, EventArgs e)
    {
        if (validarRut(txt_editRut.Text))
        {
            ConductorBC c = new ConductorBC();
            c = c.ObtenerXRut(txt_editRut.Text);
            if (c.ID != 0)
            {
                hf_id.Value = c.ID.ToString();
                txt_editNombre.Text = c.NOMBRE;
                ddl_editTran.SelectedValue = c.TRAN_ID.ToString();
                txt_editRut.Enabled = false;
                utils.ShowMessage2(this, "crear", "warn_rutEncontrado");
            }
            else if (!txt_editRut.Text.Contains("-"))
                txt_editRut.Text = txt_editRut.Text.Insert(txt_editRut.Text.Length - 1, "-");
            txt_editNombre.Focus();
        }
        else
        {
            utils.ShowMessage2(this, "crear", "warn_rutNoValido");
            txt_editRut.Text = "";
            txt_editRut.Focus();
        }
    }
    #endregion
    #region Buttons
    protected void btn_Conf_Click(object sender, EventArgs e)
    {
        if (hf_confirmar.Value == "ELIM")
        {
            ConductorBC c = new ConductorBC();
            c.ID = Convert.ToInt32(hf_id.Value);
            if (c.Eliminar())
            {
                utils.ShowMessage2(this, "eliminar", "success");
                utils.CerrarModal(this, "modalConf");
            }
            else
            {
                utils.ShowMessage2(this, "eliminar", "error");
            }
            ObtenerConductores(true);
        }
        if (hf_confirmar.Value == "BLOQUEAR")
        {
            ConductorBC c = new ConductorBC();
            c.ID = Convert.ToInt32(hf_id.Value);
            c.MOTIVO_BLOQUEO = txt_confirmarMotivo.Text;

            if (c.Bloquear(((UsuarioBC)this.Session["Usuario"]).ID ))
            {
                utils.ShowMessage2(this, "bloquear", "success");
                utils.CerrarModal(this, "modalConf");
            }
            else
            {
                utils.ShowMessage2(this, "bloquear", "error");
            }
            ObtenerConductores(true);
        }
        if (hf_confirmar.Value == "ACTIVAR")
        {
            ConductorBC c = new ConductorBC();
            c.ID = Convert.ToInt32(hf_id.Value);
            if (c.Activar())
            {
                utils.ShowMessage2(this, "activar", "success");
                utils.CerrarModal(this, "modalConf");
            }
            else
            {
                utils.ShowMessage2(this, "activar", "error");
            }
            ObtenerConductores(true);
        }
    }
    protected void btn_upload_Click(object sender, EventArgs e)
    {
        if (FileUpLoad1.HasFile)
        {
            string fileName = FileUpLoad1.FileName;
            string Ruta1;

            Ruta1 = Server.MapPath(ConfigurationManager.AppSettings["conductores_yms"]);
            if (!System.IO.Directory.Exists(Ruta1))
                System.IO.Directory.CreateDirectory(Ruta1);
            if (Path.GetExtension(FileUpLoad1.FileName) == ".png" || Path.GetExtension(FileUpLoad1.FileName) == ".jpg")
            {
                ConductorBC c = new ConductorBC();
                c = c.ObtenerXId(Convert.ToInt32(hf_id.Value));
                string sName = "COND_" + c.RUT + Path.GetExtension(FileUpLoad1.FileName);

                try
                {
                    FileUpLoad1.SaveAs(Ruta1 + sName);
                    c.IMAGEN = sName;
                    if (c.AgregarFoto())
                    {
                        utils.ShowMessage2(this, "subirFoto", "success");
                        ObtenerConductores(true);
                    }
                    else
                    {
                        utils.ShowMessage2(this, "subirFoto", "error");
                    }
                }
                catch (Exception ex)
                {
                    utils.ShowMessage(this, ex.Message, "error", false);
                }
            }
            else
            {
                utils.ShowMessage2(this,"subirFoto","warn_archivoInvalido");
            }
        }
    }
    protected void btn_editGrabar_Click(object sender, EventArgs e)
    {
        ConductorBC c = new ConductorBC();
        c.RUT = txt_editRut.Text;
        c.NOMBRE = txt_editNombre.Text;
        c.TRAN_ID = Convert.ToInt32(ddl_editTran.SelectedValue);
        c.COND_EXTRANJERO = chk_editExtranjero.Checked;
        if (string.IsNullOrEmpty(hf_id.Value))
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
            c.ID = Convert.ToInt32(hf_id.Value);
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
        ObtenerConductores(true);
    }
    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        LimpiarTodo();
        txt_editRut.Enabled = true;
        chk_editExtranjero.Enabled = true;
        utils.AbrirModal(this, "modalEdit");
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        ObtenerConductores(true);
    }
    #endregion
    #region GridView
    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string url = ConfigurationManager.AppSettings["conductores_yms"];

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Image img = (Image)e.Row.FindControl("img_foto");
            string imagen = DataBinder.Eval(e.Row.DataItem,"IMAGEN").ToString();
            if (string.IsNullOrEmpty(imagen))
                img.Visible = false;
            else
                img.ImageUrl=url+imagen;

        }
    }
    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerConductores(false);
    }
    protected void gv_listar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            gv_listar.PageIndex = e.NewPageIndex;
        }
        ObtenerConductores(false);
    }
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "FOTO")
        {
            hf_id.Value = e.CommandArgument.ToString();
            utils.AbrirModal(this, "modalFoto");
        }
        if (e.CommandName == "EDITAR")
        {
            hf_id.Value = e.CommandArgument.ToString();
            ConductorBC c = new ConductorBC();
            c = c.ObtenerXId(Convert.ToInt32(hf_id.Value));
            txt_editRut.Text = c.RUT;
            txt_editNombre.Text = c.NOMBRE;
            ddl_editTran.SelectedValue = c.TRAN_ID.ToString();
            chk_editExtranjero.Checked = c.COND_EXTRANJERO;
            txt_editRut.Enabled = false;
            chk_editExtranjero.Enabled = false;
            utils.AbrirModal(this, "modalEdit");
        }
        if (e.CommandName == "ELIM")
        {
            hf_id.Value = e.CommandArgument.ToString();
            hf_confirmar.Value = "ELIM";
            lbl_tituloConfirmar.Text = "Eliminar Conductor";
            lbl_mensajeConfirmar.Text = "Se eliminará el conductor seleccionado, ¿desea continuar?";
            pnl_confirmarObservacion.Visible = false;
            utils.AbrirModal(this, "modalConf");
        }
        if (e.CommandName == "ACTIVAR")
        {
            hf_id.Value = e.CommandArgument.ToString();
            hf_confirmar.Value = "ACTIVAR";
            lbl_tituloConfirmar.Text = "Activar/Desactivar Conductor";
            lbl_mensajeConfirmar.Text = "Se activará/desactivará el conductor seleccionado, ¿desea continuar?";
            pnl_confirmarObservacion.Visible = false;
            utils.AbrirModal(this, "modalConf");
        }
        if (e.CommandName == "BLOQUEAR")
        {
            hf_id.Value = e.CommandArgument.ToString();
            ConductorBC c = new ConductorBC().ObtenerXId(Convert.ToInt32(hf_id.Value));
            if (c.BLOQUEADO)
            {
                lbl_tituloConfirmar.Text = "Desbloquear Conductor";
                lbl_mensajeConfirmar.Text = "Se desbloqueará el conductor seleccionado.";
                txt_confirmarMotivo.Text = "";
                pnl_confirmarObservacion.Visible = false;
            }
            else
            {
                lbl_tituloConfirmar.Text = "Bloquear Conductor";
                lbl_mensajeConfirmar.Text = "Ingrese motivo de bloqueo:";
                txt_confirmarMotivo.Text = "";
                pnl_confirmarObservacion.Visible = true;
            }
            hf_confirmar.Value = "BLOQUEAR";
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
        catch (Exception e)
        {
            validacion = false;
        }
        return validacion;
    }
    private void ObtenerConductores(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            ConductorBC c = new ConductorBC();
            string nombre = txt_buscarNombre.Text;
            string rut = txt_buscarRut.Text;
            int tran_id = Convert.ToInt32(ddl_buscarTran.SelectedValue);
            ViewState["lista"] = c.ObtenerXParametro(rut, nombre, tran_id);
        }
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
            dw.Sort = (String)ViewState["sortExpresion"];
        this.gv_listar.DataSource = dw;
        this.gv_listar.DataBind();
    }
    private void LimpiarTodo()
    {
        hf_id.Value = "";
        txt_editNombre.Text = "";
        txt_editRut.Text = "";
        ddl_editTran.ClearSelection();
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
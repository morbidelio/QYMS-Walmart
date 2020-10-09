using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.IO;

public partial class App_Usuario : System.Web.UI.Page
{
    static FuncionesGenerales funcion = new FuncionesGenerales();
    static UtilsWeb utils = new UtilsWeb();
    Panel panel_ddl;
    Panel pnl_site;
    UsuarioBC user = new UsuarioBC();

    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        //Generar Pasword
        FuncionesGenerales fn;
        fn = new FuncionesGenerales();
        this.txt_editPassword.Text = fn.generarPassword(6);
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["panel"] != null)
        {
            panel_ddl = (Panel)Session["panel"];
            pnl_asignar.Controls.Add(panel_ddl);

            foreach (Control control in panel_ddl.Controls)
            {
                if (control is Panel)
                {
                    agregaevento((Panel)control);
                }
            }
        }
    }
    protected void agregaevento(Panel mipanel)
    {
        foreach (Control control in mipanel.Controls)
        {
            if (control is DropDownList)
            {
                DropDownList drop = (DropDownList)(control);
                if (drop.ID.Contains("ZONA"))
                {
                    drop.SelectedIndexChanged += new EventHandler(DDL_ZONA_INDEX_CHANGED);
                    //drop.AutoPostBack = true;
                }
                if (drop.ID.Contains("PLAYA"))
                {
                    drop.SelectedIndexChanged += new EventHandler(DDL_playa_INDEX_CHANGED);
                    //drop.AutoPostBack = true;
                }
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {
            Response.Redirect("~/InicioQYMS2.aspx");
        }
        user = (UsuarioBC)Session["usuario"];
        if (!IsPostBack)
        {
            Session["panel"] = null;

            SiteBC site = new SiteBC();
            EmpresaBC empresa = new EmpresaBC();
            PerfilBC perfil = new PerfilBC();
            ProveedorBC pr = new ProveedorBC();
            rlcli.DataSource = site.ObtenerTodos();
            rlcli.DataTextField = "DESCRIPCION";
            rlcli.DataValueField = "ID";
            rlcli.DataBind();
            utils.CargaDropNormal(this.ddl_editEmpresa, "ID", "NOMBRE_FANTASIA", empresa.ObtenerTodas());
            ddl_editEmpresa.Enabled = false;
            utils.CargaDrop(this.ddl_editTipoUsuario, "ID", "NOMBRE", user.ObtenerPerfilesAutorizados());
            utils.CargaDrop(this.ddl_buscarTipoUsuario, "ID", "NOMBRE", perfil.ObtenerTodo());
            utils.CargaDrop(this.ddl_editProveedores, "ID", "DESCRIPCION", pr.obtenerTodo());
            ObtenerUsuarios(true);
        }
    }
    #region TextBox
    protected void txt_editRut_TextChanged(object sender, EventArgs e)
    {
        if (validarRut(txt_editRut.Text))
            txt_editEmail.Focus();
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Rut no válido!');", true);
            txt_editRut.Text = "";
            txt_editRut.Focus();
        }
    }
    #endregion
    #region Botones
    protected void btn_asignarLugares_Click(object sender, EventArgs e)
    {
        //pnl_site.FindControl("")
        int id_usuario = Convert.ToInt32(hf_idUsuario.Value);
        UsuarioBC u = new UsuarioBC();
        if (u.AsignarLugar(id_usuario, hf_idLugares.Value))
        {
            utils.ShowMessage(this, "Lugares asignados correctamente.", "success", true);
            utils.CerrarModal(this, "modalAsignar");
        }
        else
        {
            utils.ShowMessage(this, "Ocurrió un error. Revise los datos", "success", true);
        }
        ObtenerUsuarios(true);
    }
    protected void btn_agregarUsuario_Click(object sender, EventArgs e)
    {
        limpiarTodo();
        utils.AbrirModal(this, "modalEdit");
    }
    protected void btn_buscarUsuario_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["lista"];
        DataView dw = dt.AsDataView();
        string filtros = "";
        bool segundo = false;
        if (ddl_buscarTipoUsuario.SelectedIndex != 0)
        {
            filtros += "ID_USUARIO_TIPO = " + ddl_buscarTipoUsuario.SelectedValue + " ";
            segundo = true;
        }
        if (!string.IsNullOrWhiteSpace(txt_buscarRUT.Text))
        {
            if (segundo)
                filtros += "AND ";
            segundo = true;
            filtros += "RUT LIKE '%" + txt_buscarRUT.Text + "%' ";
        }
        if (!string.IsNullOrWhiteSpace(txt_buscarNombre.Text))
        {
            if (segundo)
                filtros += "AND ";
            filtros += "NOMBRE LIKE '%" + txt_buscarNombre.Text + "%' ";
            segundo = true;
        }
        if (!string.IsNullOrWhiteSpace(txt_buscarApellido.Text))
        {
            if (segundo)
                filtros += "AND ";
            filtros += "APELLIDO LIKE '%" + txt_buscarApellido.Text + "%' ";
            segundo = true;
        }
        if (chk_buscarActivos.Checked)
        {
            if (segundo)
                filtros += "AND ";
            filtros += "ESTADO = 'Activo'";
            segundo = true;
        }

        if (!string.IsNullOrWhiteSpace(txt_buscarusername.Text))
        {
            if (segundo)
                filtros += "AND ";
            filtros += "USERNAME like '%" + txt_buscarusername.Text + "%' ";
            segundo = true;
        }
        if (string.IsNullOrWhiteSpace(filtros))
        {
            ObtenerUsuarios(true);
        }
        else
        {
            dw.RowFilter = filtros;
            dt = dw.ToTable();
            ViewState["filtrados"] = dt;
            ObtenerUsuarios(false);
        }
    }
    protected void btn_grabarUsuario_Click(object sender, EventArgs e)
    {
        try
        {
            UsuarioBC usuario = llenarUsuario();
            if (string.IsNullOrEmpty(hf_idUsuario.Value))
            {
                if (usuario.Crear(usuario))
                {
                    utils.ShowMessage(this, "Usuario creado correctamente.", "success", true);
                    utils.CerrarModal(this, "modalEdit");
                }
                else
                    utils.ShowMessage(this, "Ocurrió un error al crear usuario. Revise los datos.", "error", false);
            }
            else
            {

                if (usuario.Modificar(usuario))
                {
                    utils.ShowMessage(this, "Usuario modificado correctamente.", "success", true);
                    utils.CerrarModal(this, "modalEdit");
                }
                else
                    utils.ShowMessage(this, "Ocurrió un error al modificar usuario. Revise los datos.", "error", false);
            }
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            ObtenerUsuarios(true);
        }
    }
    #endregion
    #region Hidden
    protected void btn_Activar_click(object sender, EventArgs e)
    {
        try
        {
            UsuarioBC usuario = new UsuarioBC();
            usuario.ID = Convert.ToInt32(hf_idUsuario.Value);
            if (usuario.Activar())
            {
                utils.ShowMessage(this, "Usuario activado.", "success", true);
                utils.CerrarModal(this, "modalConf");
            }
            else
            {
                utils.ShowMessage(this, "Error al activar usuario. Revise los datos.", "error", false);
            }
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            ObtenerUsuarios(true);
        }
    }
    protected void btn_Desactivar_click(object sender, EventArgs e)
    {
        try
        {
            UsuarioBC usuario = new UsuarioBC();
            usuario.ID = Convert.ToInt32(hf_idUsuario.Value);
            if (usuario.Desactivar())
            {
                utils.ShowMessage(this, "Usuario desactivado.", "success", true);
                utils.CerrarModal(this, "modalConf");
            }
            else
            {
                utils.ShowMessage(this, "Error al desactivar usuario. Revise los datos.", "error", false);
            }
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            ObtenerUsuarios(true);
        }
    }
    #endregion
    #region DropDownList
    protected void ddl_editTipoUsuario_IndexChanged(object sender, EventArgs e)
    {
        if (ddl_editTipoUsuario.SelectedItem.Text.ToUpper() == "PROVEEDOR")
        {
            dv_proveedores.Style.Add("display", "block");
            CompareDropProveedor.Enabled = true;
        }
        else
        {
            dv_proveedores.Style.Add("display", "none");
            CompareDropProveedor.Enabled = false;
        }
        ddl_editEstado.Focus();
    }
    protected void creaddl(string nombre_site, string site_id)
    {
        pnl_site = new Panel();
        pnl_site.ID = site_id + "SITE_PANEL";
        pnl_site.CssClass = "col-xs-3";
        pnl_site.EnableViewState = true;
        Literal lsite = new Literal();
        lsite.Text = "<h4>" + nombre_site + "</h4>";
        Label lzona = new Label();
        lzona.Text = "Zona";
        DropDownList ddl_zona = new DropDownList();
        ddl_zona.ID = site_id + "ZONA_" + "_DDL";
        ddl_zona.SelectedIndexChanged += new EventHandler(DDL_ZONA_INDEX_CHANGED);
        ddl_zona.AutoPostBack = true;
        ddl_zona.CssClass = "form-control";
        pnl_site.Controls.Add(lsite);
        pnl_site.Controls.Add(lzona);
        pnl_site.Controls.Add(ddl_zona);

        Label lplaya = new Label();
        lplaya.Text = "Playa";
        DropDownList ddl_playa = new DropDownList();
        ddl_playa.ID = site_id + "PLAYA_" + "_DDL";
        ddl_playa.SelectedIndexChanged += new EventHandler(DDL_playa_INDEX_CHANGED);
        ddl_playa.AutoPostBack = true;
        ddl_playa.CssClass = "form-control";
        pnl_site.Controls.Add(lplaya);
        pnl_site.Controls.Add(ddl_playa);

        Label llugar = new Label();
        llugar.Text = "Posición";
        DropDownList ddl_lugar = new DropDownList();
        ddl_lugar.ID = site_id + "LUGAR_" + "_DDL";

        ddl_lugar.CssClass = "form-control droplugar";
        pnl_site.Controls.Add(llugar);
        pnl_site.Controls.Add(ddl_lugar);
        panel_ddl.Controls.Add(pnl_site);
    }
    protected void DDL_ZONA_INDEX_CHANGED(object SENDER, EventArgs E)
    {
        DropDownList ddl_zona = (DropDownList)SENDER;
        DropDownList ddl_playas = (DropDownList)panel_ddl.FindControl(ddl_zona.ID.Replace("ZONA", "PLAYA"));
        if (!string.IsNullOrEmpty(ddl_zona.SelectedValue) && ddl_zona.SelectedValue != "0")
        {
            ddl_playas.Enabled = true;
            PlayaBC p = new PlayaBC();
            utils.CargaDrop(ddl_playas, "ID", "DESCRIPCION", p.ObtenerXZona(Convert.ToInt32(ddl_zona.SelectedValue)));
        }
        else
        {
            ddl_playas.ClearSelection();
            ddl_playas.Enabled = false;
        }
        DDL_playa_INDEX_CHANGED(ddl_playas, null);
    }
    protected void DDL_playa_INDEX_CHANGED(object SENDER, EventArgs E)
    {
        DropDownList ddl_playa = (DropDownList)SENDER;
        DropDownList ddl_lugar = (DropDownList)panel_ddl.FindControl(ddl_playa.ID.Replace("PLAYA", "LUGAR"));
        if (!string.IsNullOrEmpty(ddl_playa.SelectedValue) && ddl_playa.SelectedValue != "0")
        {
            ddl_lugar.Enabled = true;
            LugarBC l = new LugarBC();
            utils.CargaDrop(ddl_lugar, "ID", "DESCRIPCION", l.obtenerLugaresXPlayaDrop(Convert.ToInt32(ddl_playa.SelectedValue)));
        }
        else
        {
            ddl_lugar.ClearSelection();
            ddl_lugar.Enabled = false;
        }
    }
    #endregion
    #region Grilla
    protected void gv_listar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            gv_listar.PageIndex = e.NewPageIndex;
        }
        ObtenerUsuarios(false);
    }
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Session["panel"] = null;
        UsuarioBC usuario = new UsuarioBC();
        if (e.CommandName == "EDITAR")
        {
            limpiarTodo();
            hf_idUsuario.Value = e.CommandArgument.ToString();
            usuario = usuario.ObtenerPorId(Convert.ToInt32(hf_idUsuario.Value));
            llenarForm(usuario);
            utils.AbrirModal(this, "modalEdit");
        }
        if (e.CommandName == "ACTIVAR")
        {
            hf_idUsuario.Value = e.CommandArgument.ToString();
            usuario = usuario.ObtenerPorId(Convert.ToInt32(hf_idUsuario.Value));
            lblRazonEliminacion.Text = "Activar/Desactivar Usuario";
            if (usuario.ESTADO)
            {
                msjEliminacion.Text = "Se desactivará el usuario seleccionado, ¿desea continuar?";
                btn_Activar.Visible = false;
                btn_Desactivar.Visible = true;
            }
            else
            {
                msjEliminacion.Text = "Se activará el usuario seleccionado, ¿desea continuar?";
                btn_Activar.Visible = true;
                btn_Desactivar.Visible = false;
            }
            utils.AbrirModal(this, "modalConf");
        }
        if (e.CommandName == "ASIGNAR")
        {
            hf_idUsuario.Value = e.CommandArgument.ToString();
            DataTable dt = usuario.ObtenerLugaresAsignados(Convert.ToInt32(hf_idUsuario.Value));
            if (dt.Rows.Count == 0)
            {
                utils.ShowMessage(this, "Usuario no tiene sites asignados.", "warn", true);
                return;
            }
            Session["sites"] = dt;
            ZonaBC z = new ZonaBC();
            PlayaBC p = new PlayaBC();
            LugarBC l = new LugarBC();

            if (pnl_asignar.FindControl("mipanel") == null)
            {
                panel_ddl = new Panel();
                panel_ddl.ID = "mipanel";
                panel_ddl.EnableViewState = true;
                pnl_asignar.Controls.Add(panel_ddl);
            }
            else
            {
                panel_ddl = (Panel)pnl_asignar.FindControl("mipanel");
                panel_ddl.Controls.Clear();
            }

            foreach (DataRow dr in dt.Rows)
            {
                creaddl(dr["NOMBRE_SITE"].ToString(), dr["SITE_ID"].ToString());
                DropDownList ddlzona = (DropDownList)panel_ddl.FindControl(dr["SITE_ID"].ToString() + "ZONA__DDL");    // new DropDownList();
                DropDownList ddlplaya = (DropDownList)panel_ddl.FindControl(dr["SITE_ID"].ToString() + "PLAYA__DDL"); // new DropDownList();
                DropDownList ddllugar = (DropDownList)panel_ddl.FindControl(dr["SITE_ID"].ToString() + "LUGAR__DDL");// new DropDownList();

                int site_id = Convert.ToInt32(dr["SITE_ID"].ToString());
                //  Panel pn = new Panel();
                //  pn.CssClass = "col-xs-3";
                utils.CargaDrop(ddlzona, "ID", "DESCRIPCION", z.ObtenerXSite(site_id, true));
                // DropDownList ddl2 = new DropDownList();
                if (!string.IsNullOrEmpty(dr["LUGA_ID"].ToString()))
                {
                    l = l.obtenerXID(Convert.ToInt32(dr["LUGA_ID"].ToString()));
                    ddlzona.SelectedValue = l.ID_ZONA.ToString();
                    DDL_ZONA_INDEX_CHANGED(ddlzona, null);
                    ddlplaya.SelectedValue = l.ID_PLAYA.ToString();
                    DDL_playa_INDEX_CHANGED(ddlplaya, null);
                    ddllugar.SelectedValue = l.ID.ToString();
                }
                else
                    DDL_ZONA_INDEX_CHANGED(ddlzona, null);
            }
            Session["panel"] = panel_ddl;
            utils.AbrirModal(this, "modalAsignar");
        }
    }
    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerUsuarios(false);
    }
    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btn_modificar = (LinkButton)e.Row.FindControl("btn_modificar");
            LinkButton btn_activar = (LinkButton)e.Row.FindControl("btn_activar");
            LinkButton btn_asignar = (LinkButton)e.Row.FindControl("btn_asignar");
            if (DataBinder.Eval(e.Row.DataItem, "NIVEL_PERMISOS") != DBNull.Value)
            {
                int nivel = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "NIVEL_PERMISOS"));
                int id = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ID_USUARIO_TIPO"));
                if (user.NIVEL_PERMISOS >= nivel && user.ID_TIPO != id)
                {
                    btn_modificar.Visible = false;
                    btn_activar.Visible = false;
                    btn_asignar.Visible = false;
                }
            }
        }
    }
    #endregion
    #region FuncionesPagina
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
    private void ObtenerUsuarios(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            UsuarioBC usuarioG = new UsuarioBC();
            DataTable dt = usuarioG.ObtenerTodos();
            ViewState["lista"] = dt;
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
    private void limpiarTodo()
    {
        hf_idUsuario.Value = "";
        txt_editNombre.Text = "";
        txt_editApellido.Text = "";
        txt_editRut.Text = "";
        txt_editEmail.Text = "";
        txt_editUsername.Text = "";
        txt_editPassword.Text = "";
        rlcli.ClearChecked();
        ddl_editTipoUsuario.ClearSelection();
        ddl_editEstado.ClearSelection();
        txt_editObservacion.Text = "";
        ddl_editEmpresa.ClearSelection();
        ddl_editProveedores.ClearSelection();
        ddl_editTipoUsuario_IndexChanged(null, null);
    }
    private void llenarForm(UsuarioBC usuario)
    {
        hf_idUsuario.Value = usuario.ID.ToString();
        txt_editNombre.Text = usuario.NOMBRE;
        txt_editApellido.Text = usuario.APELLIDO;
        txt_editRut.Text = usuario.RUT;
        txt_editEmail.Text = usuario.EMAIL;
        txt_editUsername.Text = usuario.USERNAME;
        try
        {
            this.txt_editPassword.Text = funcion.Desencriptar(usuario.PASSWORD, usuario.USERNAME);

        }
        catch (Exception e)
        {
            this.txt_editPassword.Text = "";
        }
        //txt_editPassword.Text.Insert(0, usuario.PASSWORD);
        ddl_editTipoUsuario.SelectedValue = usuario.ID_TIPO.ToString();
        ddl_editTipoUsuario_IndexChanged(null, null);
        if (usuario.ID_PROVEEDOR != 0 && usuario.ID_PROVEEDOR != null)
            ddl_editProveedores.SelectedValue = usuario.ID_PROVEEDOR.ToString();
        else
            ddl_editProveedores.ClearSelection();
        if (usuario.ESTADO)
            ddl_editEstado.SelectedIndex = 1;
        else
            ddl_editEstado.SelectedIndex = 2;
        txt_editObservacion.Text = usuario.OBSERVACION;
        ddl_editEmpresa.SelectedValue = usuario.ID_EMPRESA.ToString();
        utils.CargaItemsRadList(rlcli, usuario.SITE);
    }
    private UsuarioBC llenarUsuario()
    {
        UsuarioBC usuario = new UsuarioBC();
        usuario.CODIGO = ddl_editEmpresa.SelectedItem.Text + "_" + txt_editUsername.Text;
        usuario.NOMBRE = txt_editNombre.Text;
        usuario.APELLIDO = txt_editApellido.Text;
        usuario.RUT = txt_editRut.Text;
        usuario.EMAIL = txt_editEmail.Text;
        usuario.USERNAME = txt_editUsername.Text;
        usuario.PASSWORD = funcion.Encriptar(txt_editPassword.Text, txt_editUsername.Text.ToLower());
        usuario.ID_TIPO = Convert.ToInt32(ddl_editTipoUsuario.SelectedValue);
        if (ddl_editEstado.SelectedIndex == 1)
            usuario.ESTADO = true;
        else
            usuario.ESTADO = false;
        usuario.SITE = utils.concatenaId(rlcli);
        usuario.OBSERVACION = txt_editObservacion.Text;
        usuario.ID_EMPRESA = Convert.ToInt32(ddl_editEmpresa.SelectedValue);
        usuario.ID_PROVEEDOR = Convert.ToInt32(ddl_editProveedores.SelectedValue);
        if (hf_idUsuario.Value == "")
        {
            usuario.DESCRIPCION = "Usuario " + txt_editNombre.Text + " " + txt_editApellido.Text + "\nFecha de creación: " + System.DateTime.Now.ToString("dd/MM/yyyy");
        }
        else
        {
            usuario.DESCRIPCION = "Usuario " + txt_editNombre.Text + " " + txt_editApellido.Text + "\nFecha de modificación: " + System.DateTime.Now.ToString("dd/MM/yyyy");
            usuario.ID = Convert.ToInt32(hf_idUsuario.Value);
        }
        return usuario;
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

        //     utils.buscarArchivo

        //    file = Path.Combine(Server.MapPath("~/ViewStateFiles") + "/" + file);
        file = string.Format("{0}\\{1}", utils.pathviewstate(), file);
        return file;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class App_Local : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();
    Panel panel_caract;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["panel"] = null;
            Session.Remove("datosA");
            RegionBC region = new RegionBC();
            ComunaBC comuna = new ComunaBC();
            utils.CargaDrop(ddl_buscarRegion, "ID", "NOMBRE", region.obtenerTodoRegion());
            utils.CargaDrop(ddl_editRegion, "ID", "NOMBRE", region.obtenerTodoRegion());
            utils.CargaDrop(ddl_buscarComuna, "ID", "NOMBRE", comuna.obtenerTodoComuna());
            crearContenido();
        }
        ObtenerLocal(false);
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["panel"] != null)
        {
            panel_caract = (Panel)Session["panel"];
            pnl_contenidoCaract.Controls.Add(panel_caract);
        }
    }

    #region DropDownList

    protected void ddl_buscarRegion_IndexChanged(object sender, EventArgs e)
    {
        ComunaBC comuna = new ComunaBC();
        if (ddl_buscarRegion.SelectedValue != "0" && ddl_buscarRegion.SelectedValue != null)
        {
            utils.CargaDrop(ddl_buscarComuna, "ID", "NOMBRE", comuna.obtenerComunasXRegion(int.Parse(ddl_buscarRegion.SelectedValue)));
            ddl_buscarComuna.Enabled = true;
        }
        else
            utils.CargaDrop(ddl_buscarComuna, "ID", "NOMBRE", comuna.obtenerTodoComuna());
    }

    protected void ddl_editRegion_IndexChanged(object sender, EventArgs e)
    {
        ComunaBC comuna = new ComunaBC();
        if (ddl_editRegion.SelectedValue != "0" && ddl_editRegion.SelectedValue != null)
        {
            utils.CargaDrop(ddl_editComuna, "ID", "NOMBRE", comuna.obtenerComunasXRegion(int.Parse(ddl_editRegion.SelectedValue)));
            ddl_editComuna.Enabled = true;
        }
        else
        {
            ddl_editComuna.ClearSelection();
            ddl_editComuna.Enabled = false;
        }
    }

    #endregion

    #region GridView

    protected void gv_listaLocal_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerLocal(false);
    }

    protected void gv_listaLocal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ELIMINAR")
        {
            hf_idLocal.Value = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalConfirmacion();", true);
        }
    }

    protected void gv_listaLocal_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ViewState.Remove("datosA");
        LocalBC local = new LocalBC();
        ComunaBC comuna = new ComunaBC();
        CaractCargaBC caracteristica = new CaractCargaBC();
        DataTable dt = new DataTable();
        gv_listar.SelectedIndex = e.NewEditIndex;
        hf_idLocal.Value = this.gv_listar.SelectedDataKey.Value.ToString();
        local = local.obtenerXID(int.Parse(hf_idLocal.Value));
        txt_editCodigo.Text = local.CODIGO;
        txt_editDesc.Text = local.DESCRIPCION;
        txt_editDireccion.Text = local.DIRECCION;
        ddl_editRegion.SelectedValue = local.REGION_ID.ToString();
        hf_caracteristicasLocal.Value = local.EXCLUYENTES;
        hf_excluyentes.Value = local.EXCLUYENTES;
        hf_noexcluyentes.Value = local.NO_EXCLUYENTES;
        dt = caracteristica.obtenerXLocal(int.Parse(hf_idLocal.Value));
        ViewState["datosA"] = dt;
        if (local.INTERNOS == 0)
            chk_editInternos.Checked = false;
        else
            chk_editInternos.Checked = true;
        if (local.EXTERNOS == 0)
            chk_editExternos.Checked = false;
        else
            chk_editExternos.Checked = true;

        ddl_editRegion_IndexChanged(null, null);
        if (ddl_editRegion.SelectedIndex != 0)
        {
            utils.CargaDrop(ddl_editComuna, "ID", "NOMBRE", comuna.obtenerComunasXRegion(int.Parse(ddl_editRegion.SelectedValue)));
            ddl_editComuna.SelectedValue = local.COMUNA_ID.ToString();
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "llenarForm", "llenarForm();", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarLocal();", true);
    }

    protected void gv_listar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            gv_listar.PageIndex = e.NewPageIndex;
        }
        ObtenerLocal(false);
    }

    #endregion

    #region Buttons

    protected void btn_export_Click(object sender, EventArgs e)
    {
        DataView view = new DataView();
        view.Table = (DataTable)this.ViewState["lista"];

        view.Table.Columns.Remove(view.Table.Columns["NO_EXCLUYENTES"]);
        view.Table.Columns.Remove(view.Table.Columns["CARACTERISTICAS"]);
        view.Table.Columns.Remove(view.Table.Columns["EXCLUYENTES"]);
        view.Table.Columns.Remove(view.Table.Columns["COMU_ID"]);
        view.Table.Columns.Remove(view.Table.Columns["REGI_ID"]);
        if (view.Count > 0)
        {
            GridView gv = new GridView();
            gv.DataSource = view;
            gv.DataBind();

            string fileName = "Reporte_local.xls";
            string Extension = ".xls";
            if (Extension == ".xls")
            {
                PrepareControlForExport(gv);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AddHeader("Content-type", "application / xls");

                HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.Public);
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                try
                {
                    using (StringWriter sw = new StringWriter())
                    {
                        using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                        {
                            Table table = new Table();
                            table.GridLines = gv.GridLines;

                            if (gv.HeaderRow != null)
                            {
                                PrepareControlForExport(gv.HeaderRow);
                                table.Rows.Add(gv.HeaderRow);
                            }

                            foreach (GridViewRow row in gv.Rows)
                            {
                                PrepareControlForExport(row);
                                table.Rows.Add(row);
                            }

                            if (gv.FooterRow != null)
                            {
                                PrepareControlForExport(gv.FooterRow);
                                table.Rows.Add(gv.FooterRow);
                            }

                            gv.GridLines = GridLines.Both;
                            table.RenderControl(htw);
                            HttpContext.Current.Response.Write(sw.ToString());
                            HttpContext.Current.Response.End();
                        }
                    }
                }
                catch (HttpException ex)
                {
                    throw ex;
                }
            }
        }
        else
        {
            utils.ShowMessage2(this, "exportar", "warn_sinFilas");
        }
    }

    private static void PrepareControlForExport(Control control)
    {
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];
            if (current is LinkButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
            }
            else if (current is ImageButton)
            {
                control.Controls.Remove(current);
            }
            else if (current is HyperLink)
            {
                control.Controls.Remove(current);
            }
            else if (current is DropDownList)
            {
                control.Controls.Remove(current);
            }
            else if (current is CheckBox)
            {
                control.Controls.Remove(current);
            }
            else if (current is HiddenField)
            {
                control.Controls.Remove(current);
            }
            if (current.HasControls())
            {
                PrepareControlForExport(current);
            }
        }
    }

    protected void btn_editAgregarCaract_Click(object sender, EventArgs e)
    {
        //CaractCargaBC caract = new CaractCargaBC();
        DataTable dt = (DataTable)ViewState["datosA"];
        //dt.Rows.Add(ddl_editCaract.SelectedValue, ddl_editCaract.SelectedItem.Text);
        //gv_editCaracteristicas.DataSource = dt;
        //gv_editCaracteristicas.DataBind();
        ViewState["datosA"] = dt;
        hf_caracteristicasLocal.Value = generarStringId(dt);
        //utils.CargaDrop(ddl_editCaract, "ID", "DESCRIPCION", caract.obtenerTiposNoSeleccionados(hf_caracteristicasLocal.Value));
    }

    protected void btn_nuevoLocal_Click(object sender, EventArgs e)
    {
        ViewState.Remove("datosA");
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("DESCRIPCION");
        ViewState["datosA"] = dt;
        Limpiar();
        //gv_editCaracteristicas.DataSource = null;
        //gv_editCaracteristicas.DataBind();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarLocal();", true);
    }

    protected void btn_buscarLocal_Click(object sender, EventArgs e)
    {
        string filtros = "";
        bool primero = true;
        if (!string.IsNullOrEmpty(txt_buscarCodigo.Text))
        {
            filtros = "CODIGO LIKE '%" + txt_buscarCodigo.Text + "%' ";
            primero = false;
        }
        if (!string.IsNullOrEmpty(txt_buscarDescripcion.Text))
        {
            if (primero)
                filtros += "AND ";
            filtros = "DESCRIPCION LIKE '%" + txt_buscarDescripcion.Text + "%' ";
            primero = false;
        }
        if (ddl_buscarRegion.SelectedIndex > 0)
        {
            if (primero)
                filtros += "AND ";
            filtros = "REGI_ID = " + ddl_buscarRegion.SelectedValue + " ";
            primero = false;
        }
        if (ddl_buscarComuna.SelectedIndex > 0)
        {
            if (primero)
                filtros += "AND ";
            filtros = "COMU_ID = " + ddl_buscarComuna.SelectedValue + "";
        }
        if (string.IsNullOrEmpty(filtros))
            ObtenerLocal(true);
        else
        {
            DataView dw = new DataView((DataTable)ViewState["lista"]);
            dw.RowFilter = filtros;
            ViewState["filtrados"] = dw.ToTable();
            ObtenerLocal(false);
        }
    }

    protected void btn_editGrabar_Click(object sender, EventArgs e)
    {
        LocalBC local = new LocalBC();
        local.DESCRIPCION = txt_editDesc.Text;
        local.DIRECCION = txt_editDireccion.Text;
        local.COMUNA_ID = int.Parse(ddl_editComuna.SelectedValue);
        local.REGION_ID = int.Parse(ddl_editRegion.SelectedValue);
        local.CODIGO = txt_editCodigo.Text;
        if (chk_editInternos.Checked)
            local.INTERNOS = 1;
        else
            local.INTERNOS = 0;
        if (chk_editExternos.Checked)
            local.EXTERNOS = 1;
        else
            local.EXTERNOS = 0;
        local.EXCLUYENTES = hf_excluyentes.Value;
        local.NO_EXCLUYENTES = hf_noexcluyentes.Value;
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj1", "alert('Excluyentes: " + hf_excluyentes.Value + "');", true);
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj2", "alert('No Excluyentes: " + hf_noexcluyentes.Value + "');", true);

        if (hf_idLocal.Value == "")
        {
            if (local.Crear(local))
            {
                ObtenerLocal(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Local creado exitosamente');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalLocal');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al agregar local. Intente nuevamente.');", true);
        }
        else
        {
            local.ID = int.Parse(hf_idLocal.Value);
            if (local.Modificar(local))
            {
                ObtenerLocal(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Local modificado exitosamente');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalLocal');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al modificar local. Intente nuevamente.');", true);
        }
    }

    protected void btn_EliminarLocal_Click(object sender, EventArgs e)
    {
        LocalBC local = new LocalBC();
        if (local.Eliminar(int.Parse(hf_idLocal.Value)))
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Local eliminado exitosamente');", true);
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al eliminar local. Revise si tiene otros datos asociados');", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalConfirmacion');", true);
        ObtenerLocal(true);
    }

    #endregion

    #region UtilsPagina

    private void Limpiar()
    {
        hf_idLocal.Value = "";
        txt_editCodigo.Text = "";
        txt_editDesc.Text = "";
        txt_editDireccion.Text = "";
        ddl_editComuna.ClearSelection();
        ddl_editRegion.ClearSelection();
        chk_editExternos.Checked = false;
        chk_editInternos.Checked = false;
    }

    private void ObtenerLocal(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            LocalBC local = new LocalBC();
            DataTable dt = local.obtenerTodoLocal();
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

    private string generarStringId(DataTable dt)
    {
        string ids = "";
        bool primero = true;
        foreach (DataRow dr in dt.Rows)
        {
            if (primero)
            {
                ids += dr[0];
                primero = false;
            }
            else
                ids += "," + dr[0];
        }
        return ids;
    }

    internal void crearContenido()
    {
        CaractCargaBC c = new CaractCargaBC();
        DataTable caract = c.obtenerTodoYTipos();
        //System.Text.StringBuilder strb = new System.Text.StringBuilder();
        DataTable tipos = caract.DefaultView.ToTable(true, "CACT_ID", "CACT_DESC", "CACT_EXCLUYENTE");
        Panel p = new Panel();
        foreach (DataRow row in tipos.Rows)
        {
            Panel pnl_caract = new Panel();
            pnl_caract.ID = "menu_tipo_" + row["CACT_ID"].ToString();
            pnl_caract.Style.Add("margin-bottom", "5px");
            pnl_caract.CssClass = "col-xs-3";
            Literal ltl_caract = new Literal();
            ltl_caract.Text = row["CACT_DESC"].ToString() + "<br />";
            pnl_caract.Controls.Add(ltl_caract);



            //strb.Append("<div class='col-xs-3' id='menu_tipo_").
            //    Append(row["CACT_ID"].ToString()).
            //    Append("' style='margin-bottom:5px;'>").
            //    Append(row["CACT_DESC"].ToString()).
            //    Append("<br />");
            DataRow[] caracteristicas = caract.Select("CACT_ID = " + row["CACT_ID"].ToString());
            if (row["CACT_EXCLUYENTE"].ToString() == "True")
            {
                CheckBox chk_caract = new CheckBox();
                chk_caract.ID = "caractTipo_" + row["CACT_ID"].ToString();
                chk_caract.Attributes.Add("value", row["CACT_ID"].ToString());
                chk_caract.Attributes.Add("name", "check");
                chk_caract.CssClass = "check";
                chk_caract.ClientIDMode = ClientIDMode.Static;
                pnl_caract.Controls.Add(chk_caract);

                //strb.Append("<input name='check' id='caractTipo_").
                //    Append(row["CACT_ID"].ToString()).
                //    Append("' type='checkbox' value='").
                //    Append(row["CACT_ID"].ToString()).
                //    Append("'></input>");
            }
            else
            {
                DropDownList ddl_caract = new DropDownList();
                ddl_caract.ID = "caractTipo_" + row["CACT_ID"].ToString();
                ddl_caract.Attributes.Add("value", row["CACT_ID"].ToString());
                //ddl_caract.Attributes.Add("name", "drop");
                ddl_caract.CssClass = "drop form-control";
                ddl_caract.ClientIDMode = ClientIDMode.Static;
                ListItem li = new ListItem();
                li.Attributes.Add("id", "op_drop_0");
                li.Text = "No Aplica";
                li.Value = "0";

                if (row["CACT_ID"].ToString() != "0")
                {
                 
                    ddl_caract.Items.Add(li);
                }

                //strb.Append("<select name='drop' class='form-control' id='caractTipo_").
                //    Append(row["CACT_ID"].ToString()).
                //    Append("' value='").
                //    Append(row["CACT_ID"].ToString()).
                //    Append("' >").
                //    Append("<option value='0' >No Aplica</option>");
                foreach (DataRow c1 in caracteristicas)
                {
                    li = new ListItem();
                    li.Attributes.Add("id", "op_drop_" + c1["CACA_ID"].ToString());
                    li.Text = c1["CACA_DESC"].ToString();
                    li.Value = c1["CACA_ID"].ToString();
                    ddl_caract.Items.Add(li);

                    //strb.Append("<option value='").
                    //    Append(c1["CACA_ID"].ToString()).
                    //    Append("' id='op_drop_").
                    //    Append(c1["CACA_ID"].ToString()).
                    //    Append("'>").
                    //    Append(c1["CACA_DESC"].ToString()).
                    //    Append("</option>");
                }
                pnl_caract.Controls.Add(ddl_caract);
                //strb.Append("</select>");

            }
            p.Controls.Add(pnl_caract);
            //panel_caract.Controls.Add(pnl_caract);
            //strb.Append("</div>");
        }
        Session["panel"] = p;
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
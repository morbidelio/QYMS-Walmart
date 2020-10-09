// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web;

public partial class App_Trailer : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            CargaDrops();
            ltl_contenidoCaract.Text = this.crearContenido();
        }
        ObtenerTrailer(false);
    }

    #region GridView
    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnbloquear = (LinkButton)e.Row.FindControl("btn_BloquearTrailer");
            if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "TRUE_SITE_IN")) && Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TRBT_ID")) == 0)
            {
                btnbloquear.Style.Add("visibility", "visible");
            }
            else
            {
                btnbloquear.Style.Add("visibility", "hidden");
            }
        }
    }
    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = this.utils.ConvertSortDirectionToSql((String)this.ViewState["sortOrder"]);
        this.ViewState["sortOrder"] = direccion;
        this.ViewState["sortExpresion"] = string.Format("{0} {1}", e.SortExpression, direccion);
        this.ObtenerTrailer(false);
    }
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "MODIFICAR")
        {
            Limpiar();
            this.hf_idTrailer.Value = e.CommandArgument.ToString();
            TrailerBC trailer = new TrailerBC();
            trailer.ID = Convert.ToInt32(e.CommandArgument);
            trailer = trailer.obtenerXID();
            this.llenarForm(trailer);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "llenarForm", "llenarForm();", true);
            utils.AbrirModal(this, "modalEdit");
        }
        if (e.CommandName == "ELIMINAR")
        {
            ddTipoBloqueo.Visible = false;
            hf_idTrailer.Value = e.CommandArgument.ToString();
            lblRazonEliminacion.Text = "Eliminar Trailer";
            msjEliminacion.Text = "Se eliminará el trailer seleccionado, ¿desea continuar?";
            btnModalEliminar.Visible = true;
            btn_BloquearTrailer.Visible = false;
            utils.AbrirModal(this, "modalConfirmar");
        }
        if (e.CommandName == "BLOQUEAR")
        {
            SiteBC site = new SiteBC();
            utils.CargaDrop(this.ddl_buscarSite, "ID", "NOMBRE", site.ObtenerTodos());
            ddTipoBloqueo.Visible = true;
            hf_idTrailer.Value = e.CommandArgument.ToString();

            TrailerBC trailer = new TrailerBC();
            trailer = trailer.obtenerXID(Convert.ToInt32(this.hf_idTrailer.Value.ToString()));
            lblRazonEliminacion.Text = "Bloquear Trailer";
            ddl_buscarSite.SelectedValue = trailer.SITE_ID.ToString();
            ddl_buscarSite_onChange(null, null);
            msjEliminacion.Text = "Motivo de Bloqueo";
            btnModalEliminar.Visible = false;
            btn_BloquearTrailer.Visible = true;
            utils.AbrirModal(this, "modalConfirmar");
        }
    }
    protected void gv_listar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            this.gv_listar.PageIndex = e.NewPageIndex;
        }
        this.ObtenerTrailer(false);
    }
    #endregion
    #region DropDownList
    protected void ddl_buscarSite_onChange(object sender, EventArgs e)
    {
        ZonaBC zona = new ZonaBC();
        int site_id = Convert.ToInt32(this.ddl_buscarSite.SelectedValue);
        this.utils.CargaDrop(this.ddl_zona, "ID", "DESCRIPCION", zona.ObtenerXSite(site_id, true));
    }
    protected void ddl_zona_onChange(object sender, EventArgs e)
    {
        CargaDrops drops = new CargaDrops();
        int zona_id = Convert.ToInt32(this.ddl_zona.SelectedValue);
        drops.Playa(this.ddl_playa, zona_id);
    }
    protected void ddl_playa_onChange(object sender, EventArgs e)
    {
        int playa_id = Convert.ToInt32(this.ddl_playa.SelectedValue);
        CargaDrops drops = new CargaDrops();
        drops.Lugar1(this.ddl_lugar, 0, playa_id, -1, 1);
    }
    #endregion
    #region Buttons
    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        this.Limpiar();
        utils.AbrirModal(this, "modalEdit");
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        this.ObtenerTrailer(true);
        this.txt_buscarNombre.Focus();
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        DataView view = new DataView();
        view.Table = (DataTable)this.ViewState["lista"];

        if (view.Count > 0)
        {
            GridView gv = new GridView();
            gv.DataSource = view;
            gv.DataBind();

            string fileName = "Reporte_Trailer.xls";
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
    protected void btn_editGrabar_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txt_editPlaca.Text)) { utils.ShowMessage2(this, "modificar", "warn_placaVacia");return; }
        if (string.IsNullOrEmpty(txt_editNumero.Text) && !chk_editExterno.Checked) { utils.ShowMessage2(this, "modificar", "warn_numeroVacio");return; }
        if (ddl_editTran.SelectedIndex < 1) { utils.ShowMessage2(this, "modificar", "warn_transportistaVacio");return; }
        if (ddl_editTipo.SelectedIndex < 1) { utils.ShowMessage2(this, "modificar", "warn_tipoVacio");return; }
        TrailerBC trailer = new TrailerBC();
        trailer.PLACA = this.txt_editPlaca.Text;
        if (!this.chk_editExterno.Checked)
            trailer.NUMERO = this.txt_editNumero.Text;
        if (ddl_editTran.SelectedIndex > 0)
            trailer.TRAN_ID = Convert.ToInt32(ddl_editTran.SelectedValue);
        if (ddl_editTipo.SelectedIndex > 0)
            trailer.TRTI_ID = Convert.ToInt32(ddl_editTipo.SelectedValue);
        trailer.CODIGO = this.txt_editPlaca.Text;
        trailer.EXTERNO = this.chk_editExterno.Checked;
        trailer.NO_EXCLUYENTES = this.hf_noexcluyentes.Value;
        trailer.EXCLUYENTES = this.hf_excluyentes.Value;
        trailer.ID_SHORTEK = this.ddl_editShorteck.SelectedValue;
        trailer.REQ_SELLO = this.chk_editSello.Checked;
        if (this.hf_idTrailer.Value == "")
        {
            if (trailer.Crear())
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
            trailer.ID = Convert.ToInt32(this.hf_idTrailer.Value);
            if (trailer.Modificar())
            {
                utils.ShowMessage2(this, "modificar", "success");
                utils.CerrarModal(this, "modalEdit");
            }
            else
            {
                utils.ShowMessage2(this, "modificar", "error");
            }
        }
        this.ObtenerTrailer(true);
    }
    protected void btn_EliminarTrailer_Click(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();
        if (trailer.Eliminar(Convert.ToInt32(this.hf_idTrailer.Value)))
        {
            utils.ShowMessage2(this, "eliminar", "success");
            utils.CerrarModal(this, "modalConfirmar");
        }
        else
        {
            utils.ShowMessage2(this, "eliminar", "error");
        }
        this.ObtenerTrailer(true);
    }
    protected void btn_BloquearTrailer_Click(object sender, EventArgs e)
    {
        if (ddTipoBloqueo.SelectedIndex < 1) { utils.ShowMessage2(this, "bloquear", "warn_tipoVacio"); return; }
        TrailerBC trailer = new TrailerBC();
        string resultado;
        UsuarioBC usuario = new UsuarioBC();
        usuario = (UsuarioBC)this.Session["Usuario"];
        trailer.Bloquear(Convert.ToInt32(this.hf_idTrailer.Value), Convert.ToInt32(this.ddTipoBloqueo.SelectedValue), usuario.ID, out resultado);
        if (resultado == "")
        {
            utils.ShowMessage2(this, "bloquear", "success");
            utils.CerrarModal(this, "modalConfirmar");
        }
        else
        {
            utils.ShowMessage(this, resultado, "error", true);
        }
        this.ObtenerTrailer(true);
    }
    #endregion
    #region UtilsPagina
    private void CargaDrops()
    {
        TransportistaBC tran = new TransportistaBC();
        TrailerTipoBC tipo = new TrailerTipoBC();
        ShorteckBC s = new ShorteckBC();
        DataTable dt;
        dt = tran.ObtenerTodos();
        utils.CargaDrop(this.ddl_editTran, "ID", "NOMBRE", dt);
        utils.CargaDrop(this.ddl_buscarTransportista, "ID", "NOMBRE", dt);
        dt = tipo.obtenerTodo();
        utils.CargaDrop(this.ddl_editTipo, "ID", "DESCRIPCION", dt);
        utils.CargaDrop(this.ddl_buscarTipo, "ID", "DESCRIPCION", dt);
        utils.CargaDrop(this.ddTipoBloqueo, "ID", "NOMBRE", tran.ObtenerMotivoBloqueo("1"));
        utils.CargaDrop(this.ddl_editShorteck, "SHOR_ID", "SHOR_DESC", s.ObtenerTodos());

    }
    private void ObtenerTrailer(bool forzarBD)
    {
        if (this.ViewState["lista"] == null || forzarBD)
        {
            TrailerBC trailer = new TrailerBC();
            DataTable dt = trailer.obtenerXParametro(txt_buscarNombre.Text, txt_buscarNro.Text, chk_buscarInterno.Checked, Convert.ToInt32(ddl_buscarTipo.SelectedValue), Convert.ToInt32(this.ddl_buscarTransportista.SelectedValue));
            ViewState["lista"] = dt;
        }
        DataView dw = new DataView((DataTable)this.ViewState["lista"]);
        if (this.ViewState["sortExpresion"] != null && this.ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)this.ViewState["sortExpresion"];
        }
        this.gv_listar.DataSource = dw.ToTable();
        this.gv_listar.DataBind();
    }
    private void Limpiar()
    {
        this.hf_excluyentes.Value = "";
        this.hf_noexcluyentes.Value = "";
        this.hf_idTrailer.Value = "";
        this.txt_editPlaca.Text = "";
        this.chk_editExterno.Checked = false;
        this.txt_editNumero.Text = "";
        this.ddl_editTran.ClearSelection();
        this.ddl_editTipo.ClearSelection();
        this.ddl_editShorteck.ClearSelection();
    }
    private void llenarForm(TrailerBC trailer)
    {
        hf_idTrailer.Value = trailer.ID.ToString();
        txt_editPlaca.Text = trailer.PLACA;
        chk_editExterno.Checked = trailer.EXTERNO;
        cambia_interno(null, null);
        txt_editNumero.Text = trailer.NUMERO;
        ddl_editTran.SelectedValue = trailer.TRAN_ID.ToString();
        ddl_editTipo.SelectedValue = trailer.TRTI_ID.ToString();
        hf_excluyentes.Value = trailer.EXCLUYENTES;
        hf_noexcluyentes.Value = trailer.NO_EXCLUYENTES;
        ddl_editShorteck.SelectedValue = trailer.ID_SHORTEK;
    }
    internal string crearContenido()
    {
        CaractCargaBC c = new CaractCargaBC();
        DataTable caract = c.obtenerTodoYTipos();
        System.Text.StringBuilder strb = new System.Text.StringBuilder();
        DataTable tipos = caract.DefaultView.ToTable(true, "CACT_ID", "CACT_DESC", "CACT_EXCLUYENTE");
        foreach (DataRow row in tipos.Rows)
        {
            strb.Append("<div class='col-xs-3' id='menu_tipo_").
			Append(row["CACT_ID"].ToString()).
			Append("' style='margin-bottom:5px;'>").
			Append(row["CACT_DESC"].ToString()).
            Append("<br />");
            DataRow[] caracteristicas = caract.Select(string.Format("CACT_ID = {0}", row["CACT_ID"].ToString()));
            if (row["CACT_EXCLUYENTE"].ToString() == "True")
            {
                strb.Append("<input name='check' id='caractTipo_").Append(row["CACT_ID"].ToString()).Append("' type='checkbox' value='").Append(row["CACT_ID"].ToString()).
                Append("'></input>");
            }
            else
            {
                strb.Append("<select class='form-control' name='drop' id='caractTipo_").Append(row["CACT_ID"].ToString()).Append("' value='").Append(row["CACT_ID"].ToString()).
                Append("' >");
                foreach (DataRow c1 in caracteristicas)
                {
                    strb.Append("<option value='").Append(c1["CACA_ID"].ToString()).Append("' id='op_drop_").Append(c1["CACA_ID"].ToString()).Append("'>").Append(c1["CACA_DESC"].ToString()).
                    Append("</option>");
                }
                strb.Append("</select>");
            }
            strb.Append("</div>");
        }
        return strb.ToString();
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
    #endregion
    protected void cambia_interno(object sender, EventArgs e)
    {
        txt_editNumero.Enabled = !chk_editExterno.Checked;
    }

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
            Console.Write(ex);
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



    protected void txt_editPlaca_TextChanged(object sender, EventArgs e)
    {
        TrailerBC t = new TrailerBC();
        if (!string.IsNullOrEmpty(hf_idTrailer.Value))
            t.ID = Convert.ToInt32(hf_idTrailer.Value);
        t.PLACA = txt_editPlaca.Text;
        if (t.Comprobar())
        {
            txt_editPlaca.Text = "";
            txt_editPlaca.Focus();
            utils.ShowMessage2(this, "modificar", "warn_placaExiste");
            return;
        }
    }

    protected void txt_editNumero_TextChanged(object sender, EventArgs e)
    {
        TrailerBC t = new TrailerBC();
        if (!string.IsNullOrEmpty(hf_idTrailer.Value))
            t.ID = Convert.ToInt32(hf_idTrailer.Value);
        t.NUMERO = txt_editNumero.Text;
        if (t.Comprobar())
        {
            txt_editNumero.Text = "";
            txt_editNumero.Focus();
            utils.ShowMessage2(this, "modificar", "warn_nroExiste");
            return;
        }
        else
        {
            ddl_editTran.Focus();
        }
    }
}
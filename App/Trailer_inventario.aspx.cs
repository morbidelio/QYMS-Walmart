using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;

public partial class App_Trailerinventario : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    CargaDrops d = new CargaDrops();
    static FuncionesGenerales funcion = new FuncionesGenerales();

    protected void ddl_buscarZona_IndexChanged(object sender, EventArgs e)
    {
        if (ddl_buscarZona.SelectedIndex > 0)
        {
            d.Playa_Todos(ddl_buscarPlaya, Convert.ToInt32(ddl_buscarPlaya.SelectedValue));
        }
        else
        {
            if (ddl_buscarSite.SelectedIndex > 0)
            {
                d.Playa_Todos(ddl_buscarPlaya, 0, Convert.ToInt32(ddl_buscarSite.SelectedValue));
            }
            else
            {
                d.Playa_Todos(ddl_buscarPlaya);
            }
        }
    }
    protected void ddl_buscarSite_IndexChanged(object sender, EventArgs e)
    {
        if (ddl_buscarSite.SelectedIndex > 0)
        {
            d.Zona_Todos(ddl_buscarZona, Convert.ToInt32(ddl_buscarSite.SelectedValue));
        }
        else
        {
            d.Zona_Todos(ddl_buscarZona);
        }
        ddl_buscarZona_IndexChanged(null, null);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TransportistaBC tran = new TransportistaBC();
            TrailerTipoBC tipo = new TrailerTipoBC();
            ShorteckBC s = new ShorteckBC();
            //rlcli.DataSource = dt;
            //rlcli.DataTextField = "DESCRIPCION";
            //rlcli.DataValueField = "ID";
            //rlcli.DataBind();
            //utils.CargaCheck(this.chklst_editCaracteristicas, "ID", "DESCRIPCION", catt.obtenerTodo());
            d.Site_Todos(ddl_buscarSite);
            ddl_buscarSite_IndexChanged(null, null);
            d.Transportista_Todos(ddl_editTran);
            //utils.CargaDrop(ddl_editTran, "ID", "NOMBRE", tran.ObtenerTodos());
            utils.CargaDrop(ddl_editTipo, "ID", "DESCRIPCION", tipo.obtenerTodo());
            utils.CargaDrop(ddl_buscarTransportista, "ID", "NOMBRE", tran.ObtenerTodos());
            utils.CargaDrop(ddl_buscarTipo, "ID", "DESCRIPCION", tipo.obtenerTodo());
            utils.CargaDrop(ddTipoBloqueo, "ID", "NOMBRE", tran.ObtenerMotivoBloqueo("1"));
            utils.CargaDrop(ddl_editShorteck, "SHOR_ID", "SHOR_DESC", s.ObtenerTodos());
            ltl_contenidoCaract.Text = crearContenido();
            ObtenerTrailer(true);
        }
    }
    #region GridView
    protected void GoPage(object sender, System.EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;
        if (int.TryParse((oIraPag.Text), out iNumPag) && iNumPag > 0 && iNumPag <= gv_listar.PageCount)
        {
            if (int.TryParse(oIraPag.Text, out iNumPag) && iNumPag > 0 && iNumPag <= gv_listar.PageCount)
            {
                gv_listar.PageIndex = iNumPag - 1;
            }
            else
            {
                gv_listar.PageIndex = 0;
            }
        }
        ObtenerTrailer(false);
    }
    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager && (gv_listar.DataSource != null))
        {
            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumberOfPages");
            _TotalPags.Text = gv_listar.PageCount.ToString();

            //LLENA LA LISTA CON EL NUMERO DE PAGINAS
            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(gv_listar.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = Convert.ToString(gv_listar.PageIndex + 1);
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnbloquear = (LinkButton)e.Row.FindControl("btn_BloquearTrailer");

            if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "TRUE_SITE_IN")) && DataBinder.Eval(e.Row.DataItem, "TRBT_ID").ToString() == "0")
            {
                btnbloquear.Visible = true;
            }
            else //if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TRBT_ID")) == 1) //revision supervisor
            {
                btnbloquear.Visible = false;
            }
        }
    }
    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerTrailer(false);
    }
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "MODIFICAR")
        {
            TrailerBC trailer = new TrailerBC();
            hf_idTrailer.Value = e.CommandArgument.ToString();
            trailer = trailer.obtenerXID(int.Parse(hf_idTrailer.Value));
            llenarForm(trailer);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "llenarForm", "llenarForm();", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarTrailer();", true);
        }
        if (e.CommandName == "ELIMINAR")
        {
            ddTipoBloqueo.Visible = false;
            hf_idTrailer.Value = e.CommandArgument.ToString();
            lblRazonEliminacion.Text = "Eliminar Trailer";
            msjEliminacion.Text = "Se eliminará el trailer seleccionado, ¿desea continuar?";
            btnModalEliminar.Visible = true;
            btn_BloquearTrailer.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalConfirmar();", true);
        }
        if (e.CommandName == "BLOQUEAR")
        {
            SiteBC site = new SiteBC();
            utils.CargaDrop(ddl_buscarSite, "ID", "NOMBRE", site.ObtenerTodos());
            ddTipoBloqueo.Visible = true;
            hf_idTrailer.Value = e.CommandArgument.ToString();

            TrailerBC trailer = new TrailerBC();
            trailer = trailer.obtenerXID(int.Parse( hf_idTrailer.Value.ToString()));
            lblRazonEliminacion.Text = "Bloquear Trailer";
            ddl_buscarSite.SelectedValue = trailer.SITE_ID.ToString();
            ddl_buscarSite_onChange(null, null);
            msjEliminacion.Text = "Motivo de Bloqueo";
            btnModalEliminar.Visible = false;
            btn_BloquearTrailer.Visible = true;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalConfirmar();", true);
        }
        if (e.CommandName == "inventario")
        {
            TrailerBC trailer = new TrailerBC();
            hf_idTrailer.Value = e.CommandArgument.ToString();
            trailer.temporal_estado_cargado(int.Parse(hf_idTrailer.Value.ToString()));
            btn_buscarTrailer_Click(null, null);
        }
    }
    protected void gv_listaTrailer_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            gv_listar.PageIndex = e.NewPageIndex;
        }
        ObtenerTrailer(false);
    }
    #endregion
    #region DropDownList
    protected void ddl_buscarSite_onChange(object sender, EventArgs e)
    {
        ZonaBC zona = new ZonaBC();
        int site_id = int.Parse(ddl_buscarSite.SelectedValue);
        utils.CargaDrop(ddl_zona, "ID", "DESCRIPCION", zona.ObtenerXSite(site_id,false));

    }
    protected void ddl_zona_onChange(object sender, EventArgs e)
    {
        CargaDrops drops = new CargaDrops();
        int zona_id = int.Parse(ddl_zona.SelectedValue);
        drops.Playa(ddl_playa, zona_id);
    }
    protected void ddl_playa_onChange(object sender, EventArgs e)
    {
        int playa_id = int.Parse(ddl_playa.SelectedValue);
        CargaDrops drops = new CargaDrops();
        drops.Lugar1(ddl_lugar, 0, playa_id,-1,1);
        //utils.CargaDrop(ddl_lugar, "ID", "DESCRIPCION", l.ObtenerXPlaya(playa_id));

    }
    protected void ddl_siteinventario_onChange(object sender, EventArgs e)
    {
        ZonaBC zona = new ZonaBC();
        int site_id = int.Parse(ddl_siteinventario.SelectedValue);
        utils.CargaDrop(ddl_zonainventario, "ID", "DESCRIPCION", zona.ObtenerXSite(site_id,false));

    }
    protected void ddl_zonainventario_onChange(object sender, EventArgs e)
    {
        CargaDrops drops = new CargaDrops();
        int zona_id = int.Parse(ddl_zonainventario.SelectedValue);
        drops.Playa(ddl_playainventario, zona_id);
        ddl_playainventario_onChange(null, null);
    }
    protected void ddl_playainventario_onChange(object sender, EventArgs e)
    {
        int playa_id = int.Parse(ddl_playainventario.SelectedValue);
        CargaDrops drops = new CargaDrops();
        drops.Lugar1(ddl_lugarinventario, 0, playa_id,-1,1);
        //utils.CargaDrop(ddl_lugar, "ID", "DESCRIPCION", l.ObtenerXPlaya(playa_id));

    }
    #endregion
    #region Buttons
    protected void btn_nuevoTrailer_Click(object sender, EventArgs e)
    {
        limpiarForm();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarTrailer();", true);
    }
    protected void btn_buscarTrailer_Click(object sender, EventArgs e)
    {
        ObtenerTrailer(true);
        this.txt_buscarNombre.Focus();
    }
    protected void btn_editGrabar_Click(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();
        trailer.PLACA = txt_editPlaca.Text;
        if (!chk_editExterno.Checked)
            trailer.NUMERO = txt_editNumero.Text;
        trailer.CODIGO = txt_editPlaca.Text;
        trailer.EXTERNO = chk_editExterno.Checked;
        int tran_id;
        if (int.TryParse(ddl_editTran.SelectedValue, out tran_id))
            trailer.TRAN_ID = tran_id;
        else
            trailer.TRAN_ID = 0;
        trailer.TRTI_ID = int.Parse(ddl_editTipo.SelectedValue);
        trailer.NO_EXCLUYENTES = hf_noexcluyentes.Value;
        trailer.EXCLUYENTES = hf_excluyentes.Value;
        trailer.ID_SHORTEK = ddl_editShorteck.SelectedValue;
        trailer.REQ_SELLO = chk_editSello.Checked;
        if (hf_idTrailer.Value == "")
        {
            TrailerBC trai = new TrailerBC();
            trai = trai.obtenerXPlaca(txt_editPlaca.Text);
            if (!chk_editExterno.Checked)
                trai = trai.obtenerXNro(txt_editNumero.Text);

            if (trailer.Comprobar())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('número o placa de Trailer ya existe');", true);
            }
            else
            {
                if (trailer.Crear())
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAlert('Trailer creado exitosamente');", true);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalTrailer');", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al agregar trailer. Intente nuevamente.');", true);
            }
        }
        else
        {
            trailer.ID = int.Parse(hf_idTrailer.Value);
            if (trailer.Comprobar())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Número o placa de Trailer ya existe');", true);
                return;
            }
            else
            {
                if (trailer.Modificar())
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAlert('Trailer modificado exitosamente');", true);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalTrailer');", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al modificar trailer. Intente nuevamente.');", true);
            }
        }
        ObtenerTrailer(true);
    }
    protected void btn_EliminarTrailer_Click(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();
        if (trailer.Eliminar(int.Parse(hf_idTrailer.Value)))
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAlert('Todo OK.');", true);
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Error.');", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalConfirmar');", true);
        ObtenerTrailer(true);
    }
    protected void btn_BloquearTrailer_Click(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();
        string resultado;
        UsuarioBC usuario = new UsuarioBC();
        usuario = (UsuarioBC)Session["Usuario"];

        if (ddTipoBloqueo.SelectedItem.Text == "Seleccione...")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Debe Seleccionar un tipo de Bloqueo');", true);
        }
        else
        {

            if (trailer.BLOQUEADO == "False" || 1==1)
            {

                trailer = trailer.obtenerXID(int.Parse(hf_idTrailer.Value));

                trailer.Bloquear(int.Parse(hf_idTrailer.Value), int.Parse(ddTipoBloqueo.SelectedValue), usuario.ID, out resultado);
                if (resultado == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalConfirmar');", true);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "confirmar", "showAlert('Todo OK');", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Error');", true);
            }
            else
            {
                 ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Error');", true);
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalConfirmar');", true);
            ObtenerTrailer(true);
        }
    }
    protected void btn_inventario_Click(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();
        string resultado;
        UsuarioBC usuario = new UsuarioBC();
        usuario = (UsuarioBC)Session["Usuario"];

        if (ddTipoBloqueo.SelectedItem.Text == "Seleccione...")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Debe Seleccionar un tipo de Bloqueo');", true);
        }
        else
        {

            if (trailer.BLOQUEADO == "False" || 1 == 1)
            {

                trailer = trailer.obtenerXID(int.Parse(hf_idTrailer.Value));
                resultado = "";
             //   trailer.Bloquear(int.Parse(hf_idTrailer.Value), int.Parse(ddTipoBloqueo.SelectedValue), usuario.ID, out resultado);
                if (resultado == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalConfirmar');", true);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "confirmar", "showAlert('Todo OK');", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Error');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Error');", true);
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalConfirmar');", true);
            ObtenerTrailer(true);
        }
    }
    #endregion
    #region UtilsPagina
    private void ObtenerTrailer(bool forzarBD)
    {

        if (ViewState["lista"] == null || forzarBD)
        {
            TrailerBC t = new TrailerBC();
            t.PLACA = txt_buscarNombre.Text;
            t.NUMERO = txt_buscarNro.Text;
            t.EXTERNO = chk_buscarInterno.Checked;
            t.TRTI_ID = Convert.ToInt32(ddl_buscarTipo.SelectedValue);
            t.TRAN_ID = Convert.ToInt32(ddl_buscarTransportista.SelectedValue);
            t.SITE_ID = Convert.ToInt32(ddl_buscarSite.SelectedValue);
            int zona_id = Convert.ToInt32(ddl_buscarZona.SelectedValue);
            int play_id = Convert.ToInt32(ddl_buscarPlaya.SelectedValue);
            DataTable dt = t.obtenerXParametro(t,zona_id,play_id);
            ViewState["lista"] = dt;
        }
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
            dw.Sort = (String)ViewState["sortExpresion"];
        this.gv_listar.DataSource = dw;
        this.gv_listar.DataBind();
    }
    private void limpiarForm()
    {
        hf_excluyentes.Value = "";
        hf_noexcluyentes.Value = "";
        hf_idTrailer.Value = "";
        txt_editPlaca.Text = "";
        chk_editExterno.Checked = false;
        txt_editNumero.Text = "";
        ddl_editTran.ClearSelection();
        ddl_editTipo.ClearSelection();
        ddl_editShorteck.ClearSelection();
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
            strb.Append("<div class='col-xs-3' id='menu_tipo_").Append(row["CACT_ID"].ToString()).Append("' style='margin-bottom:5px;'>").
            Append(row["CACT_DESC"].ToString()).
            Append("<br />");
            DataRow[] caracteristicas = caract.Select("CACT_ID = " + row["CACT_ID"].ToString());
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
            //Append("</div>");
        }
        return strb.ToString();
    }
    #endregion
    protected void cambia_interno(object sender, EventArgs e)
    {
        if (chk_editExterno.Checked == false)
        {
            txt_editNumero.Enabled = true;
        }
        else
            txt_editNumero.Enabled = false;

        rfv_numero.Enabled = txt_editNumero.Enabled;
    }
}
// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Trailer_Bloqueo1 : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();
    UsuarioBC usuario = new UsuarioBC();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (this.Session["usuario"] == null)
        {
            this.Response.Redirect("~/InicioQYMS2.aspx");
        }
     
        this.usuario = (UsuarioBC)this.Session["usuario"];
     
        if (!this.IsPostBack)
        {
         
            TransportistaBC tran = new TransportistaBC();
            TrailerTipoBC tipo = new TrailerTipoBC();
            SiteBC site = new SiteBC();
            utils.CargaDrop(this.ddl_buscarSite, "ID", "NOMBRE", site.ObtenerTodos());
            this.utils.CargaDrop(this.ddl_editTran, "ID", "NOMBRE", tran.ObtenerTodos());
            this.utils.CargaDrop(this.ddl_editTipo, "ID", "DESCRIPCION", tipo.obtenerTodo());
            this.utils.CargaDrop(this.ddl_buscarTransportista, "ID", "NOMBRE", tran.ObtenerTodos());
            this.utils.CargaDrop(this.ddl_buscarTipo, "ID", "DESCRIPCION", tipo.obtenerTodo());
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            utils.CargaDropNormal(this.ddl_site, "ID", "NOMBRE", yms.ObteneSites(this.usuario.ID));
         

            this.utils.CargaDrop(this.ddl_buscarMotivo, "ID", "NOMBRE", tran.ObtenerMotivoBloqueo("1"));
            this.ltl_contenidoCaract.Text = this.crearContenido();
        }
        this.ObtenerTrailer(false);
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (this.Session["Usuario"] != null)
        {
            UsuarioBC usuario = new UsuarioBC();
            usuario = (UsuarioBC)this.Session["Usuario"];

            if (usuario.numero_sites < 2)
            {
                this.site.Visible = false;
            }
        }
    }

    #region GridView

    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnbloquear = (LinkButton)e.Row.FindControl("btn_bloquear");
            LinkButton btncontinuar = (LinkButton)e.Row.FindControl("btn_continuar");
            LinkButton btndesbloquear = (LinkButton)e.Row.FindControl("btn_desbloquear");
            LinkButton btnmover = (LinkButton)e.Row.FindControl("btn_mover");
            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TRBT_ID")) == 0)
            {
                btnbloquear.Visible = true;
                btndesbloquear.Visible = false;
                btncontinuar.Visible = false;
                btnmover.Visible = false;
            }
            else if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TRBT_ID")) == 1) //revision supervisor
            {
                btnbloquear.Visible = true;
                btndesbloquear.Visible = false;
                btncontinuar.Visible = true;
                btnmover.Visible = false;
            }
            else if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TRBT_ID")) == 100) //revision supervisor
            {
                btnbloquear.Visible = false;
                btndesbloquear.Visible = true;
                btncontinuar.Visible = false;
                btnmover.Visible = false;
            }
            else
            {
                btnbloquear.Visible = false;
                btndesbloquear.Visible = true;
                btncontinuar.Visible = false;
                btnmover.Visible = true;
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
        TrailerBC trailer = new TrailerBC();
        if (e.CommandName == "BLOQUEAR")
        {
            TransportistaBC tran = new TransportistaBC();
            this.utils.CargaDrop(this.ddTipoBloqueo, "ID", "NOMBRE", tran.ObtenerMotivoBloqueo("1"));

            this.hf_idTrailer.Value = e.CommandArgument.ToString();
            trailer = trailer.obtenerXID(int.Parse(this.hf_idTrailer.Value));

            this.lblRazonEliminacion.Text = "Bloquear Trailer";
            //msjEliminacion.Text = "Motivo de Bloqueo";
            this.ddl_buscarSite.SelectedValue = trailer.SITE_ID.ToString();
            this.dv_motivo.Visible = true;
            this.dv_zona.Visible = false;
            this.dv_playa.Visible = false;
            this.dv_lugar.Visible = false;
            this.btn_desbloquear.Visible = false;
            this.btn_bloquear.Visible = true;
            this.btn_mover.Visible = false;

            this.ddl_bloqPlaya.Items.Clear();
            this.ddl_bloqLugar.Items.Clear();

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalBloqueo();", true);
        }
        if (e.CommandName == "DESBLOQUEAR")
        {
            this.hf_idTrailer.Value = e.CommandArgument.ToString();
            trailer = trailer.obtenerXID(int.Parse(this.hf_idTrailer.Value));

            SiteBC site = new SiteBC();
            this.utils.CargaDrop(this.ddl_buscarSite, "ID", "NOMBRE", site.ObtenerTodos());
            this.lblRazonEliminacion.Text = "Desbloquear Trailer";
            //msjEliminacion.Text = "Motivo de Bloqueo";
            this.ddl_buscarSite.SelectedValue = trailer.SITE_ID.ToString();
            this.ddl_buscarSite_onChange(null, null);
            this.dv_motivo.Visible = false;
            this.dv_zona.Visible = true;
            this.dv_playa.Visible = true;
            this.dv_lugar.Visible = true;
            this.btn_bloquear.Visible = false;
            this.btn_desbloquear.Visible = true;
            this.btn_mover.Visible = false;
 
            this.ddl_bloqPlaya.Items.Clear();
            this.ddl_bloqLugar.Items.Clear();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalBloqueo();", true);
        }
        if (e.CommandName == "MOVER")
        {
            this.hf_idTrailer.Value = e.CommandArgument.ToString();
            trailer = trailer.obtenerXID(int.Parse(this.hf_idTrailer.Value));

            lblRazonEliminacion.Text = "Mover Trailer";
            ddl_buscarSite.SelectedValue = trailer.SITE_ID.ToString();
            ddl_buscarSite_onChange(null, null);
            dv_motivo.Visible = false;
            dv_zona.Visible = true;
            dv_playa.Visible = true;
            dv_lugar.Visible = true;
            btn_bloquear.Visible = false;
            btn_desbloquear.Visible = false;
            btn_mover.Visible = true;
            ddl_bloqPlaya.Items.Clear();
            ddl_bloqLugar.Items.Clear();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalBloqueo();", true);
        }
        if (e.CommandName == "CONTINUAR")
        {
            this.hf_idTrailer.Value = e.CommandArgument.ToString();
            trailer = trailer.obtenerXID(int.Parse(this.hf_idTrailer.Value));

            string resultado;
            UsuarioBC usuario = new UsuarioBC();
            usuario = (UsuarioBC)this.Session["Usuario"];

            trailer.continuar(int.Parse(this.hf_idTrailer.Value), usuario.ID, out resultado);
            if (string.IsNullOrEmpty(resultado))
            {
                this.ObtenerTrailer(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Todo OK!');", true);
                // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalCerrar('modalBloqueo');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", string.Format("alert('{0}');", resultado), true);
            }
        }
    }

    protected void gv_listar_RowEditing(object sender, GridViewEditEventArgs e)
    {
        TrailerBC t = new TrailerBC();
        this.gv_listar.SelectedIndex = e.NewEditIndex;

        this.hf_idTrailer.Value = this.gv_listar.DataKeys[this.gv_listar.SelectedIndex].Value.ToString();

        t = t.obtenerXID(int.Parse(this.hf_idTrailer.Value));
        this.llenarForm(t);

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "llenarForm", "llenarForm();", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarTrailer();", true);
    }

    protected void gv_listar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            this.gv_listar.PageIndex = e.NewPageIndex;
        }
        this.ObtenerTrailer(false);
    }


    public void gv_listar_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
    }
    #endregion

    #region Buttons

    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        this.limpiarForm();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarTrailer();", true);
    }

    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        this.ObtenerTrailer(true);
        this.txt_buscarNombre.Focus();
    }

    protected void btn_mover_Click(object sender, EventArgs e)
    {
        if (this.ddl_bloqLugar.SelectedIndex > 0)
        {
            TrailerBC t = new TrailerBC().obtenerXID(int.Parse(this.hf_idTrailer.Value));
            MovimientoBC m = new MovimientoBC();
            m.ID_DESTINO = int.Parse(this.ddl_bloqLugar.SelectedValue);
            m.ID_TRAILER = t.ID;
            string resultado;
            if (m.MOVIMIENTO(t.SITE_ID, this.usuario.ID, out resultado) && resultado == "")
            {
                this.ObtenerTrailer(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar4", "showAlert('Movimiento Exitoso');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar3", "cerrarModal('modalBloqueo');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", string.Format("alert('{0}');", resultado), true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Seleccione un lugar de destino');", true);
        }
    }

    protected void btn_bloquear_Click(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();

        if (this.ddTipoBloqueo.SelectedItem.Text == "Seleccione...")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Debe Seleccionar un tipo de Bloqueo');", true);
        }
        else
        {
            string resultado;

            trailer.Bloquear(int.Parse(this.hf_idTrailer.Value), int.Parse(this.ddTipoBloqueo.SelectedValue), this.usuario.ID, out resultado);
            if (string.IsNullOrEmpty(resultado))
            {
                this.ObtenerTrailer(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAlert('Bloqueado Correctamente!');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal2", "modalCerrar('modalBloqueo');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", string.Format("alert('{0}');", resultado), true);
            }
        }
    }

    protected void btn_desbloquear_Click(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();
        string resultado;
        int lugar;
        try
        {
            lugar = int.Parse(this.ddl_bloqLugar.SelectedValue);
        }
        catch (Exception)
        {
            lugar = 0;
        }

        if (trailer.Desbloquear(int.Parse(this.hf_idTrailer.Value), lugar, this.usuario.ID, out resultado))
        {
            this.ObtenerTrailer(true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAlert('Desbloqueado correctamente!');", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal2", "modalCerrar('modalBloqueo');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", string.Format("alert('{0}!');", resultado), true);
        }
    }

    protected void btn_grabar_Click(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();
        trailer.PLACA = this.txt_editPlaca.Text;
        trailer.CODIGO = this.txt_editPlaca.Text;
        trailer.EXTERNO = this.chk_editExterno.Checked;
        //trailer.CARACTERISTICAS = utils.concatenaId(rlcli);
        int tran_id;
        trailer.NUMERO = this.txt_editNumero.Text;
        if (int.TryParse(this.ddl_editTran.SelectedValue, out tran_id))
        {
            trailer.TRAN_ID = tran_id;
        }
        else
        {
            trailer.TRAN_ID = 0;
        }
        trailer.TRTI_ID = int.Parse(this.ddl_editTipo.SelectedValue);
        trailer.NO_EXCLUYENTES = this.hf_noexcluyentes.Value;
        trailer.EXCLUYENTES = this.hf_excluyentes.Value;

        if (this.hf_idTrailer.Value == "")
        {
            TrailerBC trai = new TrailerBC();
            trai = trai.obtenerXPlaca(this.txt_editPlaca.Text);
            if (trai.ID == 0)
            {
                trai = trai.obtenerXNro(this.txt_editNumero.Text);
            }

            if (trai.ID != 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('número o placa de Trailer ya existe');", true);
            }
            else
            {
                if (trailer.Crear(trailer))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Trailer creado exitosamente');", true);
                    this.limpiarForm();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al agregar trailer. Intente nuevamente.');", true);
                }
            }
        }
        else
        {
            trailer.ID = int.Parse(this.hf_idTrailer.Value);
            if (!trailer.Comprobar(trailer))
            {
                if (trailer.Modificar(trailer))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Trailer modificado exitosamente');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al modificar trailer. Intente nuevamente.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Número o placa de Trailer ya existe');", true);
            }
        }
        this.ObtenerTrailer(true);
    }

    #endregion

    #region DropDownList

    protected void ddl_buscarSite_onChange(object sender, EventArgs e)
    {
        ZonaBC zona = new ZonaBC();
        int site_id = int.Parse(this.ddl_buscarSite.SelectedValue);
        this.utils.CargaDrop(this.ddl_bloqZona, "ID", "DESCRIPCION", zona.ObtenerXSite(site_id, false));
    }

    protected void ddl_bloqZona_onChange(object sender, EventArgs e)
    {
        PlayaBC playa = new PlayaBC();
        int zona_id = int.Parse(this.ddl_bloqZona.SelectedValue);
        this.utils.CargaDrop(this.ddl_bloqPlaya, "ID", "DESCRIPCION", playa.ObtenerXZona(zona_id));
        if (this.ddl_bloqPlaya.Items.Count > 1)
        {
            this.ddl_bloqPlaya.Enabled = true;
            if (this.ddl_bloqPlaya.SelectedIndex > 0)
            {
                this.ddl_bloqPlaya_onChange(null, null);
                this.ddl_bloqPlaya.Enabled = true;
            }
        }
    }

    protected void ddl_bloqPlaya_onChange(object sender, EventArgs e)
    {
        int playa_id = int.Parse(this.ddl_bloqPlaya.SelectedValue);

        YMS_ZONA_BC yms = new YMS_ZONA_BC();
        DataTable ds1 = yms.Obtenerlugares_playa(playa_id, null, "0");
        this.utils.CargaDrop(this.ddl_bloqLugar, "id", "codigo", ds1);

        if (this.ddl_bloqLugar.Items.Count <= 1)
        {
            this.ddl_bloqLugar.Enabled = false;
        }
        else
        {
            this.ddl_bloqLugar.Enabled = true;
        }
    }

    #endregion

    #region UtilsPagina

    private void ObtenerTrailer(bool forzarBD)
    {
        if (this.ViewState["lista"] == null || forzarBD)
        {
            TrailerBC trailer = new TrailerBC();
            DataTable dt = trailer.obtenerXParametrotaller(this.txt_buscarNombre.Text, this.txt_buscarNro.Text, this.chk_buscarInterno.Checked, int.Parse(this.ddl_buscarTipo.SelectedValue), int.Parse(this.ddl_buscarTransportista.SelectedValue), int.Parse(this.ddl_site.SelectedValue), 1, int.Parse(ddl_buscarMotivo.SelectedValue));
            this.ViewState["lista"] = dt;
        }
        DataView dw = new DataView((DataTable)this.ViewState["lista"]);
        if (this.ViewState["sortExpresion"] != null && this.ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)this.ViewState["sortExpresion"];
        }
        this.gv_listar.DataSource = dw;
        this.gv_listar.DataBind();
    }

    private void limpiarForm()
    {
        this.hf_excluyentes.Value = "";
        this.hf_noexcluyentes.Value = "";
        this.hf_idTrailer.Value = "";
        //     txt_editCodigo.Text = "";
        this.txt_editPlaca.Text = "";
        this.chk_editExterno.Checked = false;
        this.txt_editNumero.Text = "";
        this.ddl_editTran.ClearSelection();
        this.ddl_editTipo.ClearSelection();
        //rlcli.ClearChecked();
    }

    private void llenarForm(TrailerBC trailer)
    {
        this.hf_idTrailer.Value = trailer.ID.ToString();
        //   txt_editCodigo.Text = trailer.CODIGO;
        this.txt_editPlaca.Text = trailer.PLACA;
        this.chk_editExterno.Checked = trailer.EXTERNO;
        this.txt_editNumero.Text = trailer.NUMERO;
        this.ddl_editTran.SelectedValue = trailer.TRAN_ID.ToString();
        this.ddl_editTipo.SelectedValue = trailer.TRTI_ID.ToString();
        this.hf_excluyentes.Value = trailer.EXCLUYENTES;
        this.hf_noexcluyentes.Value = trailer.NO_EXCLUYENTES;
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
            //Append("<div class='col-xs-6'>").
            Append(row["CACT_DESC"].ToString()).
            Append("<br />");
            //Append("</div>").
            //Append("<div class='col-xs-6'>");
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
            //Append("</div>");
        }
        return strb.ToString();
    }

    #endregion

    protected void cambia_interno(object sender, EventArgs e)
    {
        if (this.chk_editExterno.Checked == false)
        {
            this.txt_editNumero.Enabled = true;
        }
        else
        {
            this.txt_editNumero.Enabled = false;
        }

        this.rfv_numero.Enabled = this.txt_editNumero.Enabled;
    }




}
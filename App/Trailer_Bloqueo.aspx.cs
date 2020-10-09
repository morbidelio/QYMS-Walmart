﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;

public partial class App_Trailer_Bloqueo : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();
    UsuarioBC usuario = new UsuarioBC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../InicioQYMS.aspx");
            usuario = (UsuarioBC)Session["usuario"];
            CaractCargaBC catt = new CaractCargaBC();
            TransportistaBC tran = new TransportistaBC();
            TrailerTipoBC tipo = new TrailerTipoBC();
            SiteBC site = new SiteBC();
            DataTable dt = catt.obtenerTodo();
            //rlcli.DataSource = dt;
            //rlcli.DataTextField = "DESCRIPCION";
            //rlcli.DataValueField = "ID";
            //rlcli.DataBind();
            //utils.CargaCheck(this.chklst_editCaracteristicas, "ID", "DESCRIPCION", catt.obtenerTodo());
            utils.CargaDrop(ddl_editTran, "ID", "NOMBRE", tran.ObtenerTodos());
            utils.CargaDrop(ddl_editTipo, "ID", "DESCRIPCION", tipo.obtenerTodo());
            utils.CargaDrop(ddl_buscarTransportista, "ID", "NOMBRE", tran.ObtenerTodos());
            utils.CargaDrop(ddl_buscarTipo, "ID", "DESCRIPCION", tipo.obtenerTodo());
            utils.CargaDropNormal(ddl_site, "ID", "NOMBRE", site.ObtenerTodos());
            utils.CargaDrop(ddl_buscarMotivo, "ID", "NOMBRE", tran.ObtenerMotivoBloqueo("1"));
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

    protected void gv_listaTrailer_RowDataBound(object sender, GridViewRowEventArgs e)
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
            LinkButton btnbloquear = (LinkButton)e.Row.FindControl("btn_bloquear");
            LinkButton btncontinuar = (LinkButton)e.Row.FindControl("btn_continuar");
            LinkButton btndesbloquear = (LinkButton)e.Row.FindControl("btn_desbloquear");
            LinkButton btnmover = (LinkButton)e.Row.FindControl("btn_mover");
            LinkButton btndesbloquearnommov = (LinkButton)e.Row.FindControl("btn_desbloquear_nomov");
            
            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TRBT_ID")) == 0)
            {
                btnbloquear.Visible = true;
                btndesbloquear.Visible = false;
                btncontinuar.Visible = false;
                btnmover.Visible = false;
                btndesbloquearnommov.Visible=false;
            } else if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TRBT_ID")) == 1) //revision supervisor
            {
                btnbloquear.Visible = true;
                btndesbloquear.Visible = false;
                btncontinuar.Visible = true;
                btnmover.Visible = false;
                btndesbloquearnommov.Visible=false;
            }
           else
            {
                if (DataBinder.Eval(e.Row.DataItem, "TRBT_REQUIERE_ACTUALIZAR_LUGAR").ToString() == "True")

                {
                    btndesbloquear.Visible = true;
                    btndesbloquearnommov.Visible = false;
                }
                else
                {
                    btndesbloquear.Visible = false;
                    btndesbloquearnommov.Visible = true;
                }

                btnbloquear.Visible = false ;
               
                btncontinuar.Visible = false;
                btnmover.Visible = false;
                
            }

         //   btn_mover.Visible = false;
         //   btndesbloquearnommov.Visible = true;

        }
        
    }

    protected void gv_listaTrailer_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerTrailer(false);
    }

    protected void gv_listaTrailer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        TrailerBC trailer = new TrailerBC();
        if (e.CommandName == "BLOQUEAR")
        {
            TransportistaBC tran = new TransportistaBC();
            utils.CargaDrop(ddTipoBloqueo, "ID", "NOMBRE", tran.ObtenerMotivoBloqueo("1"));

            hf_idTrailer.Value = e.CommandArgument.ToString();
            trailer = trailer.obtenerXID(int.Parse(hf_idTrailer.Value));

            lblRazonEliminacion.Text = "Bloquear Trailer";
            //msjEliminacion.Text = "Motivo de Bloqueo";
            ddl_buscarSite.SelectedValue = trailer.SITE_ID.ToString();
            dv_motivo.Visible = true;
            dv_zona.Visible = false;
            dv_playa.Visible = false;
            dv_lugar.Visible = false;
            btn_desbloquear.Visible = false;
            btn_bloquear.Visible = true;
            btn_mover.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalBloqueo();", true);
        }
        if (e.CommandName == "DESBLOQUEAR")
        {
            hf_idTrailer.Value = e.CommandArgument.ToString();
            trailer = trailer.obtenerXID(int.Parse(hf_idTrailer.Value));

            SiteBC site = new SiteBC();
            utils.CargaDrop(ddl_buscarSite, "ID", "NOMBRE", site.ObtenerTodos());
            lblRazonEliminacion.Text = "Desbloquear Trailer";
            //msjEliminacion.Text = "Motivo de Bloqueo";
            ddl_buscarSite.SelectedValue = trailer.SITE_ID.ToString();
            ddl_buscarSite_onChange(null, null);
            btn_buscarTrailer.Visible = true;
            dv_motivo.Visible = false;
            dv_zona.Visible = true;
            dv_playa.Visible = true;
            dv_lugar.Visible = true;
            btn_bloquear.Visible = false;
            btn_desbloquear.Visible = true;
            btn_mover.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalBloqueo();", true);
        }
        if (e.CommandName == "MOVER")
        {
            hf_idTrailer.Value = e.CommandArgument.ToString();
            trailer = trailer.obtenerXID(int.Parse(hf_idTrailer.Value));

            SiteBC site = new SiteBC();
            utils.CargaDrop(ddl_buscarSite, "ID", "NOMBRE", site.ObtenerTodos());
            lblRazonEliminacion.Text = "Desbloquear Trailer";
            //msjEliminacion.Text = "Motivo de Bloqueo";
            btn_bloquear.Attributes.Remove("onclick");
            btn_bloquear.Attributes.Add("onclick", "BloqueaTrailer();");
            ddl_buscarSite.SelectedValue = trailer.SITE_ID.ToString();
            ddl_buscarSite_onChange(null, null);
            btn_buscarTrailer.Visible = true;
            dv_motivo.Visible = false;
            dv_zona.Visible = true;
            dv_playa.Visible = true;
            dv_lugar.Visible = true;
            btn_bloquear.Visible = false;
            btn_desbloquear.Visible = false;
            btn_mover.Visible = true;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalBloqueo();", true);
        }
        if (e.CommandName == "CONTINUAR")
        {

             hf_idTrailer.Value = e.CommandArgument.ToString();
            trailer = trailer.obtenerXID(int.Parse(hf_idTrailer.Value));

             string resultado;
            UsuarioBC usuario = new UsuarioBC();
            usuario = (UsuarioBC)Session["Usuario"];

            trailer.continuar(int.Parse(hf_idTrailer.Value),  usuario.ID, out resultado);
            if (string.IsNullOrEmpty(resultado))
            {
                ObtenerTrailer(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAlert('Solicitud realizada correctamente');", true);
               // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalCerrar('modalBloqueo');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('" + resultado + "');", true);
        }

        if (e.CommandName == "DESBLOQUEAR_nomov")
        {
            hf_idTrailer.Value = e.CommandArgument.ToString();
            trailer = trailer.obtenerXID(int.Parse(hf_idTrailer.Value));


            desbloquear1_Click();
        }


    }

    protected void gv_listaTrailer_RowEditing(object sender, GridViewEditEventArgs e)
    {
        TrailerBC trailer = new TrailerBC();
        gv_listar.SelectedIndex = e.NewEditIndex;

        hf_idTrailer.Value = gv_listar.DataKeys[gv_listar.SelectedIndex].Value.ToString();

        trailer = trailer.obtenerXID(int.Parse(hf_idTrailer.Value));
        llenarForm(trailer);

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "llenarForm", "llenarForm();", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarTrailer();", true);
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

    #region Buttons

    protected void btn_nuevoTrailer_Click(object sender, EventArgs e)
    {
        limpiarForm();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarTrailer();", true);
    }

    protected void btn_buscarTrailer_Click(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();
        DataTable dt = trailer.obtenerXParametroBloqueo(txt_buscarNombre.Text, txt_buscarNro.Text, chk_buscarInterno.Checked, int.Parse(ddl_buscarTipo.SelectedValue), int.Parse(ddl_buscarTransportista.SelectedValue), int.Parse(ddl_site.SelectedValue),0);
        ViewState["lista"] = dt;
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        if (ViewState["sortExpresion"] != null)
        {
            String sortExp = (String)ViewState["sortExpresion"];
            if (sortExp != "")
                dw.Sort = (String)ViewState["sortExpresion"];
        }
        this.gv_listar.DataSource = dw;
        this.gv_listar.DataBind();
        this.txt_buscarNombre.Focus();
    }

    protected void btn_mover_Click(object sender, EventArgs e)
    {
        if (ddl_bloqLugar.SelectedIndex > 0)
        {
            TrailerBC t = new TrailerBC().obtenerXID(int.Parse(hf_idTrailer.Value));
            MovimientoBC m = new MovimientoBC();
            m.ID_DESTINO = int.Parse(ddl_bloqLugar.SelectedValue);
            m.ID_TRAILER = t.ID;
            string resultado;
            if (m.MOVIMIENTO(t.SITE_ID, usuario.ID, out resultado))
            {
                ObtenerTrailer(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalBloqueo');", true);
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('" + resultado + "');", true);
        }
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Seleccione un lugar de destino');", true);
    }

    protected void btn_bloquear_Click(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();

        if (ddTipoBloqueo.SelectedItem.Text == "Seleccione...")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Debe Seleccionar un tipo de Bloqueo');", true);
        }
        else
        {
            string resultado;

            trailer.Bloquear(int.Parse(hf_idTrailer.Value), int.Parse(ddTipoBloqueo.SelectedValue), usuario.ID, out resultado);
            if (string.IsNullOrEmpty(resultado))
            {
                ObtenerTrailer(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAalert('Operacion Realizada Correctamente');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal1", "modalCerrar('modalBloqueo');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('" + resultado + "');", true);

        }
    }

    protected void btn_desbloquear_Click(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();
        string resultado;

        if (ddl_bloqLugar.SelectedValue == "" || ddl_bloqLugar.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mesnaje", "alert('seleccione lugar');", true);
          
        }
        else
        {

            trailer.Desbloquear(int.Parse(hf_idTrailer.Value), int.Parse(ddl_bloqLugar.SelectedValue), usuario.ID, out resultado);
            if (resultado=="")
            {
                ObtenerTrailer(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Solicitud realizada correctamente!');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalCerrar('modalBloqueo');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('"+ resultado+"!');", true);
        }
        }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (Session["Usuario"] != null)
        {
            UsuarioBC usuario = new UsuarioBC();
            usuario = (UsuarioBC)Session["Usuario"];

            if (usuario.numero_sites < 2)
            {
                this.site.Visible = false;
            }
        }
    }


    protected void desbloquear1_Click()
    {
        TrailerBC trailer = new TrailerBC();
        string resultado;
        trailer.Desbloquear(int.Parse(hf_idTrailer.Value), 0, usuario.ID, out resultado);
        if (resultado=="")
        {
            ObtenerTrailer(true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAlert('Solicitud realizada correctamente!');", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal1", "modalCerrar('modalBloqueo');", true);
        }
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('"+resultado+"');", true);
    }


    protected void btn_editGrabar_Click(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();
        trailer.PLACA = txt_editPlaca.Text;
        trailer.CODIGO = txt_editPlaca.Text;
        trailer.EXTERNO = chk_editExterno.Checked;
        //trailer.CARACTERISTICAS = utils.concatenaId(rlcli);
        int tran_id;
        trailer.NUMERO = txt_editNumero.Text;
        if (int.TryParse(ddl_editTran.SelectedValue, out tran_id))
            trailer.TRAN_ID = tran_id;
        else
            trailer.TRAN_ID = 0;
        trailer.TRTI_ID = int.Parse(ddl_editTipo.SelectedValue);
        trailer.NO_EXCLUYENTES = hf_noexcluyentes.Value;
        trailer.EXCLUYENTES = hf_excluyentes.Value;

        if (hf_idTrailer.Value == "")
        {
            TrailerBC trai = new TrailerBC();
            trai = trai.obtenerXPlaca(txt_editPlaca.Text);
            if (trai.ID == 0)
                trai = trai.obtenerXNro(txt_editNumero.Text);

            if (trai.ID != 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('número o placa de Trailer ya existe');", true);
            }
            else
            {
                if (trailer.Crear(trailer))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Trailer creado exitosamente');", true);
                    limpiarForm();
                }
                else
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al agregar trailer. Intente nuevamente.');", true);
            }
        }
        else
        {
            trailer.ID = int.Parse(hf_idTrailer.Value);
            if (!trailer.Comprobar())
            {
                if (trailer.Modificar(trailer))
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Trailer modificado exitosamente');", true);
                else
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al modificar trailer. Intente nuevamente.');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Número o placa de Trailer ya existe');", true);

        }
        ObtenerTrailer(true);
    }

    #endregion

    #region DropDownList

    protected void ddl_buscarSite_onChange(object sender, EventArgs e)
    {
        ZonaBC zona = new ZonaBC();
        int site_id = int.Parse(ddl_buscarSite.SelectedValue);
        utils.CargaDrop(ddl_bloqZona, "ID", "DESCRIPCION", zona.ObtenerXSite(site_id,false));

    }

    protected void ddl_bloqZona_onChange(object sender, EventArgs e)
    {
        PlayaBC playa = new PlayaBC();
        int zona_id = int.Parse(ddl_bloqZona.SelectedValue);
        utils.CargaDrop(ddl_bloqPlaya, "ID", "DESCRIPCION", playa.ObtenerXZona(zona_id));
        if (ddl_bloqPlaya.Items.Count > 1)
        {
            ddl_bloqPlaya.Enabled = true;
            if (ddl_bloqPlaya.SelectedIndex > 0)
            {
                ddl_bloqPlaya_onChange(null, null);
                ddl_bloqPlaya.Enabled = true;
            }
        }
    }

    protected void ddl_bloqPlaya_onChange(object sender, EventArgs e)
    {
        PlayaBC playa = new PlayaBC();
        int playa_id = int.Parse(ddl_bloqPlaya.SelectedValue);
        LugarBC lugar = new LugarBC();

        CargaDrops drops = new CargaDrops();
        drops.Lugar1(ddl_bloqLugar, 0, playa_id,-1,1);
        //utils.CargaDrop(ddl_bloqLugar, "ID", "DESCRIPCION", lugar.ObtenerXPlaya(playa_id));
        if (ddl_bloqLugar.Items.Count <= 1)
            ddl_bloqLugar.Enabled = false;
        else
            ddl_bloqLugar.Enabled = true;

    }

    #endregion

    #region UtilsPagina

    private void ObtenerTrailer(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            TrailerBC trailer = new TrailerBC();
            DataTable dt = trailer.obtenerXParametroBloqueo(txt_buscarNombre.Text, txt_buscarNro.Text, chk_buscarInterno.Checked, int.Parse(ddl_buscarTipo.SelectedValue), int.Parse(ddl_buscarTransportista.SelectedValue), int.Parse(ddl_site.SelectedValue),0);
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
        //     txt_editCodigo.Text = "";
        txt_editPlaca.Text = "";
        chk_editExterno.Checked = false;
        txt_editNumero.Text = "";
        ddl_editTran.ClearSelection();
        ddl_editTipo.ClearSelection();
        //rlcli.ClearChecked();
    }

    private void llenarForm(TrailerBC trailer)
    {
        hf_idTrailer.Value = trailer.ID.ToString();
        //   txt_editCodigo.Text = trailer.CODIGO;
        txt_editPlaca.Text = trailer.PLACA;
        chk_editExterno.Checked = trailer.EXTERNO;
        txt_editNumero.Text = trailer.NUMERO;
        ddl_editTran.SelectedValue = trailer.TRAN_ID.ToString();
        ddl_editTipo.SelectedValue = trailer.TRTI_ID.ToString();
        hf_excluyentes.Value = trailer.EXCLUYENTES;
        hf_noexcluyentes.Value = trailer.NO_EXCLUYENTES;
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
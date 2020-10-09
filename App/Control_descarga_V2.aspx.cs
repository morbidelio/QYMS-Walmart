// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class App_control_descarga_v2: System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();
    UsuarioBC usuario = new UsuarioBC();
    CargaDrops drop = new CargaDrops();

    protected void rb_CheckedChanged(object sender, EventArgs e)
    {
        //int site_id = Convert.ToInt32(dropsite.SelectedValue);
        if (rb_Nada.Checked)
        {
            hf_soli_tipo.Value = "0";
            dv_posicion.Visible = false;
            dv_solicitud.Visible = false;
        }
        if (rb_estacionamiento.Checked)
        {
            hf_soli_tipo.Value = "0";
            dv_posicion.Visible = false;
            dv_solicitud.Visible = false;
        }
        if (rb_estacionamientoMan.Checked)
        {
            hf_soli_tipo.Value = "0";

            if (hf_accion.Value.ToString() == "DESCARGA_COMPLETA_LI")
                CargaDropZona(3);

            else
                CargaDropZona(7);

                
            dv_posicion.Visible = true;
            dv_solicitud.Visible = true;
            int luga_trailer = Convert.ToInt32(hf_lugaid.Value);
            LugarBC luga = new LugarBC();
            luga = luga.obtenerXID(luga_trailer);

            try
            {
                ddl_zona.SelectedValue = luga.ID_ZONA.ToString();
                ddl_zona_SelectedIndexChanged(null, null);
                ddl_playa.SelectedValue = luga.ID_PLAYA.ToString();
                ddl_playa_SelectedIndexChanged(null, null);
                ddl_posicion.SelectedValue = luga.ID.ToString();
            }
            catch (Exception)
            {
                ddl_zona_SelectedIndexChanged(null, null);
            }


        }
        if (rb_Pallets.Checked)
        {
            hf_soli_tipo.Value = "3";
            CargaDropZona(2);
            dv_posicion.Visible = true;
            dv_solicitud.Visible = true;
            int luga_trailer = Convert.ToInt32(hf_lugaid.Value);
            LugarBC luga = new LugarBC();
            luga = luga.obtenerXID(luga_trailer);

            try
            {
                ddl_zona.SelectedValue = luga.ID_ZONA.ToString();
                ddl_zona_SelectedIndexChanged(null, null);
                ddl_playa.SelectedValue = luga.ID_PLAYA.ToString();
                ddl_playa_SelectedIndexChanged(null, null);
                ddl_posicion.SelectedValue = luga.ID.ToString();
            }
            catch (Exception)
            {
                ddl_zona_SelectedIndexChanged(null, null);
            }
        }
        if (rb_Desechos.Checked)
        {
            hf_soli_tipo.Value = "4";

            CargaDropZona(6);//, tipo_solicitud);
            dv_posicion.Visible = true;
            dv_solicitud.Visible = true;

            int luga_trailer = Convert.ToInt32(hf_lugaid.Value);
            LugarBC luga = new LugarBC();
            luga = luga.obtenerXID(luga_trailer);

            try
            {
                ddl_zona.SelectedValue = luga.ID_ZONA.ToString(); 
                ddl_zona_SelectedIndexChanged(null, null);
                ddl_playa.SelectedValue = luga.ID_PLAYA.ToString();
                ddl_playa_SelectedIndexChanged(null, null);
                ddl_posicion.SelectedValue = luga.ID.ToString();
            }
            catch (Exception)
            { 
                ddl_zona_SelectedIndexChanged(null, null);
            }
        }
        if (rb_descargar.Checked)
        {
            hf_soli_tipo.Value = "5";
            CargaDropZona(2);
            dv_posicion.Visible = true;
            dv_solicitud.Visible = true;
            int luga_trailer = Convert.ToInt32(hf_lugaid.Value);
            LugarBC luga = new LugarBC();
            luga = luga.obtenerXID(luga_trailer);

            try
            {
                ddl_zona.SelectedValue = luga.ID_ZONA.ToString();
                ddl_zona_SelectedIndexChanged(null, null);
                ddl_playa.SelectedValue = luga.ID_PLAYA.ToString();
                ddl_playa_SelectedIndexChanged(null, null);
                ddl_posicion.SelectedValue = luga.ID.ToString();
            }
            catch (Exception)
            {
                ddl_zona_SelectedIndexChanged(null, null);
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("~/InicioQYMS2.aspx");
        }
        usuario = (UsuarioBC)Session["Usuario"];
        if (!IsPostBack)
        {
            TrailerTipoBC tipo = new TrailerTipoBC();
            drop.Site_Normal(dropsite, usuario.ID);
            drop.Transportista(ddl_buscarTransportista);
            utils.CargaDrop(ddl_buscarTipo, "ID", "DESCRIPCION", tipo.obtenerTodo());
            drop_SelectedIndexChanged(null, null);
            ObtenerTrailer(true);
        }
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (Session["Usuario"] != null)
        {
            if (usuario.numero_sites < 2)
            {
                this.SITE.Visible = false;
            }
        }
    }

    #region GridView

    protected void gv_bloqueo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "QUITAR")
        {
            SolicitudAndenesBC s = new SolicitudAndenesBC();
            string resultado;
            s.SOLI_ID = Convert.ToInt32(hf_idSolicitud.Value);
            s.LUGA_ID = Convert.ToInt32(e.CommandArgument.ToString());
            if (s.Liberar(usuario.ID, out resultado))
            {
                gv_bloqueo.DataSource = s.ObtenerBloqueados();
                gv_bloqueo.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "showAlert('" + resultado + "');", true);

            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('" + resultado + "');", true);
        }
    }

    protected void gv_listar_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int soes_id = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SOES_ID"));
            int soti_id = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SOTI_ID"));
            int estado_trailer = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "estado_trailer"));
            int movi_id = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "movi_id"));
            //Botones Default
            LinkButton btn_solicitud_descarga = (LinkButton)e.Row.FindControl("btn_solicitud_descarga");
            LinkButton btn_solicitud_descarga_li = (LinkButton)e.Row.FindControl("btn_solicitud_descarga_li");
            LinkButton btn_solicitud_pallet = (LinkButton)e.Row.FindControl("btn_solicitud_pallet");
            LinkButton btn_solicitud_desechos = (LinkButton)e.Row.FindControl("btn_solicitud_desechos");
            LinkButton btn_mover = (LinkButton)e.Row.FindControl("btn_mover");
            //Botones Sol Descarga
            LinkButton btn_descargaCompleta = (LinkButton)e.Row.FindControl("btn_descargaCompleta");
            LinkButton btn_descargaEditarPos = (LinkButton)e.Row.FindControl("btn_descargaEditarPos");
            LinkButton btn_descargaBloquear = (LinkButton)e.Row.FindControl("btn_descargaBloquear");
            //Botones Sol Descarga LI
            LinkButton btn_descargaLiCompleta = (LinkButton)e.Row.FindControl("btn_descargaLiCompleta");
            LinkButton btn_descargaLiEditarPos = (LinkButton)e.Row.FindControl("btn_descargaLiEditarPos");
            LinkButton btn_descargaMover = (LinkButton)e.Row.FindControl("btn_descargaMover");
            //Botones Sol Pallet
            LinkButton btn_palletsTrasladoAnden = (LinkButton)e.Row.FindControl("btn_palletsTrasladoAnden");
            LinkButton btn_palletsTrasladoEst = (LinkButton)e.Row.FindControl("btn_palletsTrasladoEst");
            LinkButton btn_palletsReiniciar = (LinkButton)e.Row.FindControl("btn_palletsReiniciar");
            LinkButton btn_palletsCompletar = (LinkButton)e.Row.FindControl("btn_palletsCompletar");
            //Botones Sol Desechos
            //LinkButton btn_desechosTrasladoAnden = (LinkButton)e.Row.FindControl("btn_desechosTrasladoAnden");
            //LinkButton btn_desechosTrasladoEst = (LinkButton)e.Row.FindControl("btn_desechosTrasladoEst");
            //LinkButton btn_desechosReiniciar = (LinkButton)e.Row.FindControl("btn_desechosReiniciar");
            LinkButton btn_desechosCompletar = (LinkButton)e.Row.FindControl("btn_desechosCompletar");

            btn_solicitud_descarga.Visible = false;
            btn_solicitud_descarga_li.Visible = false;
            btn_solicitud_pallet.Visible = false;
            btn_solicitud_desechos.Visible = false;
            btn_mover.Visible = false;

            btn_descargaCompleta.Visible = false;
            btn_descargaMover.Visible = false;
            btn_descargaEditarPos.Visible = false;
            btn_descargaBloquear.Visible = false;

            btn_descargaLiCompleta.Visible = false;
            btn_descargaLiEditarPos.Visible = false;

            btn_palletsTrasladoAnden.Visible = false;
            btn_palletsTrasladoEst.Visible = false;
            btn_palletsReiniciar.Visible = false;
            btn_palletsCompletar.Visible = false;

            //btn_desechosTrasladoAnden.Visible = false;
            //btn_desechosTrasladoEst.Visible = false;
            //btn_desechosReiniciar.Visible = false;
            btn_desechosCompletar.Visible = false;

            if (movi_id == 0)
            {
                switch (soti_id)
                {
                    case 0:                                                     //sin solicitud
                        switch (estado_trailer)
                        {
                            case 100:                                           //descargado
                                btn_solicitud_pallet.Visible = true;
                                btn_solicitud_desechos.Visible = true;
                                btn_mover.Visible = true;
                                break;
                            case 400:                                           //cargado
                                btn_solicitud_descarga_li.Visible = true;
                                btn_solicitud_descarga.Visible = false;
                                btn_mover.Visible = true;
                                break;
                        }
                        break;
                    case 2:                                                     //solicitud descarga
                        switch (soes_id)
                        {
                            case 200: //Solicitud Creada
                                btn_descargaCompleta.Visible = false;
                                btn_descargaMover.Visible = false;
                                btn_descargaEditarPos.Visible = false;
                                btn_descargaBloquear.Visible = true;
                                break;
                            case 205: //casi descarga
                                btn_descargaCompleta.Visible = false;
                                btn_descargaMover.Visible = false;
                                btn_descargaEditarPos.Visible = false;
                                btn_descargaBloquear.Visible = true;
                                break;
                            case 210: //decargando
                                btn_descargaCompleta.Visible = true;
                                btn_descargaMover.Visible = true;
                                btn_descargaEditarPos.Visible = false;
                                btn_descargaBloquear.Visible = true;
                                break;

                            case 230: //Carga Completa
                                btn_descargaCompleta.Visible = false;
                                btn_descargaMover.Visible = false;
                                btn_descargaEditarPos.Visible = false;
                                btn_descargaBloquear.Visible = true;
                                break;
                        }
                        break;
                    case 5:                                                     //solicitud descarga LI
                        switch (soes_id)
                        {
                            case 500: //Solicitud Creada
                                btn_descargaLiEditarPos.Visible = false;
                                break;
                            case 505: //casi descarga
                                btn_descargaLiEditarPos.Visible = false;
                                break;
                            case 510: //decargando
                                btn_descargaLiCompleta.Visible = true;
                                btn_descargaLiEditarPos.Visible = false;
                                break;

                            case 530: //Descarga Completa
                                btn_descargaLiEditarPos.Visible = false;
                                break;
                        }
                        break;
                    case 3:                                         //solicitud pallet
                        switch (soes_id)
                        {
                            case 300:
                                break;
                            case 310:
                                btn_palletsTrasladoAnden.Visible = true;
                                btn_palletsTrasladoEst.Visible = true;
                                break;
                            case 325:
                                btn_palletsReiniciar.Visible = true;
                                break;
                            case 330:
                                btn_palletsCompletar.Visible = true;
                                break;
                        }
                        break;
                    case 4:                                         //solicitud desechos
                        switch (soes_id)
                        {
                            case 400:
                                break;
                                //case 410:
                                //    btn_desechosTrasladoAnden.Visible = true;
                                //    btn_desechosTrasladoEst.Visible = true;
                                //    break;
                                //case 425:
                                //    btn_desechosReiniciar.Visible = true;
                                //    break;
                            case 410:
                                btn_desechosCompletar.Visible = true;
                                break;
                        }
                        break;
                }
            }
        }
    }

    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = string.Format("{0} {1}", e.SortExpression, direccion);
        ObtenerTrailer(false);
    }

    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DEF_DESCARGA_LI" ||
            e.CommandName == "DEF_PALLETS" ||
            e.CommandName == "DEF_MOVER" ||
            e.CommandName == "DEF_DESECHOS" ||
            e.CommandName == "DESCARGA_EDITA" ||
            e.CommandName == "DESCARGA_BLOQUEAR" ||
            e.CommandName == "DESCARGA_COMPLETA_LI" ||
            e.CommandName == "DESCARGA_POSICION_LI" ||
            e.CommandName == "DESECHOS_COMPLETAR" ||
            e.CommandName == "PALLETS_COMPLETAR" ||
            e.CommandName == "PALLETS_TRASLADO_ANDEN" ||
            e.CommandName == "PALLETS_TRASLADO_EST" ||
            e.CommandName == "PALLETS_REINICIAR" ||
            e.CommandName == "DEF_DESCARGA" ||
            e.CommandName == "DESCARGA_COMPLETA" ||
            e.CommandName == "DESCARGA_POSICION" ||
            e.CommandName == "DESCARGA_MOVER")
        {
            int index = Convert.ToInt32(e.CommandArgument);

            hf_idTrailer.Value = gv_listar.DataKeys[index].Values[0].ToString();
            hf_idSolicitud.Value = gv_listar.DataKeys[index].Values[1].ToString();
            hf_lugaid.Value = gv_listar.DataKeys[index].Values[2].ToString();
            hf_soanorden.Value = gv_listar.DataKeys[index].Values[3].ToString();
            hf_timestamp.Value = gv_listar.DataKeys[index].Values[4].ToString();

            Limpiar();
            hf_accion.Value = e.CommandName;
            TituloModal(e.CommandName);
            hf_soli_tipo.Value = "0";


            rb_descargar.Checked = false;
            rb_Desechos.Checked = false;
            rb_estacionamiento.Checked = false;
            rb_estacionamientoMan.Checked = false;
            rb_Pallets.Checked = false;
            rb_Nada.Checked = false;

            switch (hf_accion.Value)
            {
                case "DEF_PALLETS":
                    rb_Pallets.Checked = true;
                    hf_soli_tipo.Value = "3";
                    Modal(false, true, true);

                    int luga_trailer = Convert.ToInt32(hf_lugaid.Value);
                    LugarBC luga = new LugarBC();
                    luga = luga.obtenerXID(luga_trailer);

                    try
                    {
                        ddl_zona.SelectedValue = luga.ID_ZONA.ToString(); 
                        ddl_zona_SelectedIndexChanged(null, null);
                        ddl_playa.SelectedValue = luga.ID_PLAYA.ToString();
                        ddl_playa_SelectedIndexChanged(null, null);
                        ddl_posicion.SelectedValue = luga.ID.ToString();
                    }
                    catch (Exception)
                    { 
                        ddl_zona_SelectedIndexChanged(null, null);
                    }
                    break;
                case "DEF_DESECHOS":
                    rb_Desechos.Checked = true;
                    hf_soli_tipo.Value = "4";
                    Modal(false, true, true);

                    luga_trailer = Convert.ToInt32(hf_lugaid.Value);
                    luga = new LugarBC();
                    luga = luga.obtenerXID(luga_trailer);

                    try
                    {
                        ddl_zona.SelectedValue = luga.ID_ZONA.ToString(); 
                        ddl_zona_SelectedIndexChanged(null, null);
                        ddl_playa.SelectedValue = luga.ID_PLAYA.ToString();
                        ddl_playa_SelectedIndexChanged(null, null);
                        ddl_posicion.SelectedValue = luga.ID.ToString();
                    }
                    catch (Exception)
                    { 
                        ddl_zona_SelectedIndexChanged(null, null);
                    }
                    break;
                case "DESCARGA_COMPLETA_LI":
                case "PALLETS_COMPLETAR":
                    Modal(true, false, false);
                    break;
                case "DESECHOS_COMPLETAR":
                    Modal(true, false, false);
                    break;
                case "DEF_MOVER":
                    Modal(false, true, true);
                    break;
                case "DEF_DESCARGA_LI":
                    hf_soli_tipo.Value = "5";
                    Modal(false, true, false);
                    break;
                case "DESCARGA_POSICION_LI":
                case "DESECHOS_TRASLADO_ANDEN":
                case "DESECHOS_TRASLADO_EST":
                case "DESECHOS_REINICIAR":
                case "PALLETS_TRASLADO_ANDEN":
                case "PALLETS_TRASLADO_EST":
                case "PALLETS_REINICIAR":
                case "DEF_DESCARGA":
                case "DESCARGA_POSICION":
                case "DESCARGA_MOVER":
                    Modal(false, true, false);
                    break;
                case "DESCARGA_COMPLETA":
                   // bool ext = Convert.ToBoolean(gv_listar.DataKeys[index].Values[5]);
                    //if (ext)
                    //{
                    //    Modal(false, true, false);


                    //}
                    //else
                    //{
                        string mensaje;
                        SolicitudBC s = new SolicitudBC();
                        bool error = s.DescargaCompleta(Convert.ToInt32(hf_idSolicitud.Value), out mensaje, usuario.ID);
                        if (error)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "showAlert('Se ha completado la descarga');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", string.Format("showAlert4('{0}');", mensaje), true);
                        }
                        ObtenerTrailer(true);
            //  }
                    break;
                case "DESCARGA_BLOQUEAR": 
                    SolicitudAndenesBC sa = new SolicitudAndenesBC();
                    sa.SOLI_ID = Convert.ToInt32(hf_idSolicitud.Value);
                    LugarBC l = new LugarBC();
                    int play_id = sa.ObtenerPlayaId();
                    drop.Lugar1(ddl_bloquearPos, Convert.ToInt32(dropsite.SelectedValue), play_id, 0, 1);// l.ObtenerXPlaya(play_id));
                    gv_bloqueo.DataSource = sa.ObtenerBloqueados();
                    gv_bloqueo.DataBind();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalBloqueoAnden();", true);
                    break;
            }
        }
    }

    #endregion

    #region DropDownList

    protected void drop_SelectedIndexChanged(object sender, EventArgs e)
    {
        PlayaBC p = new PlayaBC();
        p.SITE_ID = Convert.ToInt32(dropsite.SelectedValue);
        p.ID_TIPOPLAYA = 100;
        p.ID_TIPOZONA = 200;
        utils.CargaDropTodos(this.ddl_buscarPlaya, "ID", "DESCRIPCION", p.ObtenerTodas());
        ObtenerTrailer(true);
    }

    protected void ddl_zona_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_zona.SelectedIndex > 0)
        {
            //int tipo_solicitud = int.Parse(hf_soli_tipo.Value);
            ddl_playa.Enabled = true;

            if (rb_Desechos.Visible && rb_Desechos.Checked)
            {
                CargaDropPlaya(1);
            }
            else if (rb_Pallets.Visible && rb_Pallets.Checked)
            {
                CargaDropPlaya(1);
            }
            else if (rb_estacionamientoMan.Visible && rb_estacionamientoMan.Checked)
            {
                CargaDropPlaya(2);
            }
            else if (rb_descargar.Visible && rb_descargar.Checked)
            {
                CargaDropPlaya(1);
            }
            else
            {
                switch (hf_accion.Value)
                {
                    case "DESCARGA_POSICION_LI":
                        CargaDropPlaya(0);//;, tipo_solicitud);
                        break;
                    case "DEF_MOVER":
                        CargaDropPlaya(0);//, tipo_solicitud);
                        break;
                    case "DEF_PALLETS":
                        CargaDropPlaya(1);//, tipo_solicitud);
                        break;
                    case "DEF_DESECHOS":
                        CargaDropPlaya(1);//, tipo_solicitud);
                        break;
                    case "DEF_DESCARGA_LI":
                        CargaDropPlaya(1);//, tipo_solicitud);
                        break;
                    case "DESECHOS_TRASLADO_ANDEN":
                        CargaDropPlaya(1);//, tipo_solicitud);
                        break;
                    case "DESECHOS_REINICIAR":
                        CargaDropPlaya(1);//, tipo_solicitud);
                        break;
                    case "DESECHOS_TRASLADO_EST":
                        CargaDropPlaya(2);//, tipo_solicitud);
                        break;
                    case "PALLETS_TRASLADO_ANDEN":
                        CargaDropPlaya(1);//, tipo_solicitud);
                        break;
                    case "PALLETS_REINICIAR":
                        CargaDropPlaya(1);//, tipo_solicitud);
                        break;
                    case "PALLETS_TRASLADO_EST":
                        CargaDropPlaya(2);//, tipo_solicitud);
                        break;
                    case "DESCARGA_COMPLETA":
                        CargaDropPlaya(2);//, tipo_solicitud);
                        break;
                    case "DESCARGA_POSICION":
                        CargaDropPlaya(1);//, tipo_solicitud);
                        break;
                    case "DESCARGA_MOVER":
                        CargaDropPlaya(0);//, tipo_solicitud);
                        break;
                    case "DESCARGA_BLOQUEAR":
                        CargaDropPlaya(0);
                        break;
                    default:
                        CargaDropPlaya(0);//, tipo_solicitud);
                        break;
                }
            }
        }
        else
        {
            ddl_playa.ClearSelection();
            ddl_playa.Enabled = false;
        }
        ddl_playa_SelectedIndexChanged(null, null);
    }

    protected void ddl_playa_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_playa.SelectedIndex > 0)
        {
            LugarBC l = new LugarBC();
            int play_id = Convert.ToInt32(ddl_playa.SelectedValue);
            int trai_id = Convert.ToInt32(hf_idTrailer.Value);
            ddl_posicion.Enabled = true;
            //    DataTable dt = l.ObtenerXPlaya(play_id, 0, 1, trai_id);
            //    utils.CargaDrop(ddl_posicion, "ID", "DESCRIPCION", dt);
            int solicitudtipo = Convert.ToInt32(hf_soli_tipo.Value);
            drop.Lugar1(ddl_posicion, Convert.ToInt32(dropsite.SelectedValue), Convert.ToInt32(ddl_playa.SelectedValue), 0, 1, trai_id, solicitudtipo);
        }
        else
        {
            ddl_posicion.ClearSelection();
            ddl_posicion.Enabled = false;
        }
    }

    #endregion

    #region Buttons

    protected void btn_bloquearAgregar_Click(object sender, EventArgs e)
    {
        SolicitudAndenesBC s = new SolicitudAndenesBC();
        string resultado;
        s.LUGA_ID = Convert.ToInt32(ddl_bloquearPos.SelectedValue);
        s.SOLI_ID = Convert.ToInt32(hf_idSolicitud.Value);

        if (s.Bloquear(usuario.ID, out resultado))
        {
            gv_bloqueo.DataSource = s.ObtenerBloqueados();
            gv_bloqueo.DataBind();
            utils.ShowMessage(this, resultado, "success", true);
        }

        else
        {
            utils.ShowMessage(this, resultado, "error", false);
        }


    }

    protected void btn_guardar_Click(object sender, EventArgs e)
    {
        TrailerLogiBC t = new TrailerLogiBC();
        SolicitudBC s = new SolicitudBC();
        int soli_id = Convert.ToInt32(hf_idSolicitud.Value);
        int luga_trailer = 0;
        int soan_orden = 0;
        try
        {
            luga_trailer = Convert.ToInt32(hf_lugaid.Value);
            soan_orden = Convert.ToInt32(hf_soanorden.Value);
        }
        catch (Exception)
        {
        }

        int luga_id = 0;
        int site_id = 0;
        try
        {
            luga_id = Convert.ToInt32(ddl_posicion.SelectedValue);
            site_id = Convert.ToInt32(dropsite.SelectedValue);
        }
        catch (Exception)
        {
        }
        
        string mensaje = "";
        bool error = true;
        switch (hf_accion.Value)
        {
            case "DEF_PALLETS":
            case "DEF_DESECHOS":
                if (luga_id != 0)
                {
                    error = CrearSolicitud(luga_id, "", out mensaje);
                }
                else
                {
                    utils.ShowMessage(this, "Complete todos los datos", "warn", true);
                    return;
                }
                break;
            case "DEF_DESCARGA_LI":
                if (luga_id != 0)
                {
                    s = new SolicitudBC();
                    s.ID_SITE = site_id;
                    s.ID_USUARIO = usuario.ID;
                    s.DOCUMENTO = txt_doc.Text;
                    s.OBSERVACION = txt_obs.Text;
                    s.ID_TRAILER = Convert.ToInt32(hf_idTrailer.Value);

                    error = t.DescargaLI_Crear(s, luga_id, out mensaje);

                    if (string.IsNullOrEmpty(mensaje))
                    {
                        mensaje = "Se ha creado la solicitud.";
                    }
                }
                else
                {
                    utils.ShowMessage(this, "Complete todos los datos", "warn", true);
                    return;
                }
                break;
            case "DEF_MOVER":
                MovimientoBC mov = new MovimientoBC();
                mov.ID_TRAILER = Convert.ToInt32(hf_idTrailer.Value);
                mov.OBSERVACION = "";
                mov.ID_DESTINO = Convert.ToInt32(ddl_posicion.SelectedValue);
                mov.OBSERVACION = txt_obs.Text;
                mov.petroleo = null;
                error = mov.MOVIMIENTO(mov, site_id, usuario.ID, out mensaje);
                if (string.IsNullOrEmpty(mensaje))
                {
                    mensaje = "Se ha generado el movimiento";
                }
                break;
            case "DESCARGA_EDITA":
                break;
            case "DESCARGA_POSICION_LI":
                break;
            case "DESCARGA_COMPLETA_LI":
                error = t.DescargaLI_Completar(soli_id, luga_trailer, soan_orden, usuario.ID, out mensaje);
                if (string.IsNullOrEmpty(mensaje))
                {
                    string error_out;
                    mensaje = "Se ha completado la solicitud.";
                    error = CrearSolicitud(luga_id, mensaje, out error_out);
                    mensaje = error_out;
                }
                break;
            case "DESECHOS_COMPLETAR":
                error = t.Desechos_Completar(soli_id, luga_trailer, soan_orden, usuario.ID,  out mensaje);
                if (string.IsNullOrEmpty(mensaje) && !rb_Nada.Checked)
                {
                    mensaje = "Se ha completado la solicitud. ";
                   string error_out;
                    error = CrearSolicitud(luga_id, mensaje, out error_out);
                    mensaje = error_out;
                }
                break;
            case "PALLETS_COMPLETAR":
                error = t.PALLETS_Completar(soli_id, luga_trailer, soan_orden, usuario.ID, out mensaje);
                if (string.IsNullOrEmpty(mensaje) && !rb_Nada.Checked)
                {
                    mensaje = "Se ha completado la solicitud. ";
                    string error_out;
                    error = CrearSolicitud(luga_id, mensaje, out error_out);
                    mensaje = error_out;
                }
                break;

            case "PALLETS_TRASLADO_ANDEN":
                error = t.Pallets_Reiniciar(soli_id, luga_trailer, luga_id, usuario.ID, soan_orden, soan_orden + 1, out mensaje);
        
                if (string.IsNullOrEmpty(mensaje))
                {
                    mensaje = "Se ha generado el movimiento";
                }
                break;
            case "PALLETS_TRASLADO_EST":
    
                error = t.Pallets_TrasladoEst(soli_id, luga_trailer, luga_id, soan_orden, usuario.ID, out mensaje);
                if (string.IsNullOrEmpty(mensaje))
                {
                    mensaje = "Se ha generado el movimiento";
                }
                break;
            case "PALLETS_REINICIAR":
                error = t.Pallets_Reiniciar(soli_id, luga_trailer, luga_id, usuario.ID, 0, soan_orden + 1, out mensaje);
                if (string.IsNullOrEmpty(mensaje))
                {
                    mensaje = "Se ha reanudado la descarga";
                }
                break;
            case "DEF_DESCARGA":
            case "DESCARGA_COMPLETA":
                error = s.DescargaCompleta(soli_id, out mensaje,  usuario.ID, luga_id);
                if (string.IsNullOrEmpty(mensaje))
                {
                    mensaje = "Se ha completado la descarga";
                }
                break;
            case "DESCARGA_POSICION":
                s = new SolicitudBC();
                s.SOLI_ID = Convert.ToInt32(hf_idSolicitud.Value);
                s.OBSERVACION = "";
                s.ID_DESTINO = luga_id;
                s.TIMESTAMP = Convert.ToDateTime(hf_timestamp.Value);
                if (s.validarTimeStamp())
                {
                    error = s.ModificarDescarga(s);
                    if (string.IsNullOrEmpty(mensaje))
                    {
                        mensaje = "Se ha modificado la solicitud";
                    }
                    else
                    {
                        mensaje = "Error";
                    }
                }
                else
                {
                    mensaje = "No se pudo validar timestamp.";
                }
                break;
            case "DESCARGA_MOVER":
                MovimientoBC m = new MovimientoBC();
                s = new SolicitudBC();
                m.ID_DESTINO = luga_id;
                m.ID_TRAILER = Convert.ToInt32(hf_idTrailer.Value);
                m.ID_SOLICITUD = soli_id;
                m.OBSERVACION = "";

                error = s.DescargaMovimiento(m, site_id, usuario.ID, out mensaje);
                if (string.IsNullOrEmpty(mensaje))
                {
                    mensaje = "Se ha generado el movimiento";
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrarmodal", "cerrarModal('modalLogistica');", true);
                break;
        }

        if (error)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", string.Format("showAlert('{0}');", mensaje), true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalLogistica');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", string.Format("showAlert4('{0}');", mensaje), true);
        }
        ObtenerTrailer(true);
    }

    protected void btn_buscarTrailer_Click(object sender, EventArgs e)
    {
        ObtenerTrailer(true);
    }

    #endregion

    #region UtilsPagina

    private void CargaDropZona(int tipo)
    {
        ZonaBC z = new ZonaBC();
        int site_id = Convert.ToInt32(dropsite.SelectedValue);
        DataTable dt = new DataTable();
        switch (tipo)
        {
            case 1: // zonas no LI
                dt = z.ObtenerLI(site_id, 400, false, 0);
                break;
            case 2: // zonas LI
                dt = z.ObtenerLI(site_id, 400, true, 0);
                break;
            case 3: // zonas que contengan estacionamientos
                dt = z.ObtenerLI(site_id, 0, false, 200);
                break;
            case 4: // zonas que contengan andenes
                dt = z.ObtenerLI(site_id, 0, false, 100);
                break;
            case 5: // zonas LI que contengan andenes
                dt = z.ObtenerLI(site_id, 400, true, 100);
                break;
            case 6: // zonas no LI que contengan andenes
                dt = z.ObtenerLI(site_id, 400, false, 100);
                break;
            case 7: // zonas LI que contengan estacionamientos
                dt = z.ObtenerLI(site_id, 400, true, 200);
                break;
            case 8: // zonas no LI que contengan estacionamientos
                dt = z.ObtenerLI(site_id, 400, false, 200);
                break;
            default:
                dt = z.ObtenerLI(site_id);
                break;
        }

        utils.CargaDrop(ddl_zona, "ID", "DESCRIPCION", dt);
        ddl_zona_SelectedIndexChanged(null, null);
    }

    private void CargaDropPlaya(int tipo)
    {
        PlayaBC p = new PlayaBC();
        int zona_id = Convert.ToInt32(ddl_zona.SelectedValue);
        DataTable dt = new DataTable();
        switch (tipo)
        {
            case 1:
                dt = p.ObtenerXZona(zona_id, 100); //Selecciona playas tipo andén
                break;
            case 2:
                dt = p.ObtenerXZona(zona_id, 200); //Selecciona playas tipo estacionamiento
                break;
            case 3:
                dt = p.ObtenerXZona(zona_id, 400); //Selecciona playas tipo logística inversa
                break;
            default:
                dt = p.ObtenerXZona(zona_id);
                break;
        }
        utils.CargaDrop(ddl_playa, "ID", "DESCRIPCION", dt);
        ddl_playa_SelectedIndexChanged(null, null);
    }

    private void TituloModal(string command)
    {
        switch (command)
        {
            case "DEF_DESCARGA_LI":
                this.lbl_tituloLogistica.Text = "Solicitud Descarga";
                break;
            case "DEF_PALLETS":
                this.lbl_tituloLogistica.Text = "Solicitud Pallets";
                break;
            case "DEF_MOVER":
                this.lbl_tituloLogistica.Text = "Mover Trailer";
                break;
            case "DEF_DESECHOS":
                this.lbl_tituloLogistica.Text = "Devolución Pallets";
                break;
            case "DESCARGA_EDITA":
                this.lbl_tituloLogistica.Text = "Editar Solicitud Descarga";
                break;
            case "DESCARGA_COMPLETA_LI":
                this.lbl_tituloLogistica.Text = "Completar Solicitud Descarga";
                break;
            case "DESECHOS_COMPLETAR":
                this.lbl_tituloLogistica.Text = "Completar Devolución Pallets";
                break;
            case "PALLETS_COMPLETAR":
                this.lbl_tituloLogistica.Text = "Completar Solicitud Pallets";
                break;
            case "DESCARGA_POSICION_LI":
                this.lbl_tituloLogistica.Text = "Mover Trailer";
                break;
            case "DESECHOS_TRASLADO_ANDEN":
                this.lbl_tituloLogistica.Text = "Mover Trailer a Andén";
                break;
            case "DESECHOS_TRASLADO_EST":
                this.lbl_tituloLogistica.Text = "Mover Trailer a Estacionamiento";
                break;
            case "DESECHOS_REINICIAR":
                this.lbl_tituloLogistica.Text = "Continuar Devolución Pallets";
                break;
            case "PALLETS_TRASLADO_ANDEN":
                this.lbl_tituloLogistica.Text = "Mover Trailer a Andén";
                break;
            case "PALLETS_TRASLADO_EST":
                this.lbl_tituloLogistica.Text = "Mover Trailer a Estacionamiento";
                break;
            case "PALLETS_REINICIAR":
                this.lbl_tituloLogistica.Text = "Continuar Solicitud Pallets";
                break;
            case "DESCARGA_POSICION":
                this.lbl_tituloLogistica.Text = "EDITAR POSICION";
                break;
            case "DESCARGA_MOVER":
                this.lbl_tituloLogistica.Text = "Mover Trailer";
                break;
            case "DESCARGA_BLOQUEAR":
                this.lbl_tituloLogistica.Text = "Bloquear Andenes";
                break;
        }
    }

    private void Modal(bool tipo, bool posicion, bool solicitud)
    {
        this.dv_tipo.Visible = tipo;
        dv_posicion.Visible = posicion;
        dv_solicitud.Visible = solicitud;
        ddl_zona.Enabled = true;
        this.dv_descargadoPallets.Visible = false;
        this.dv_descargadoDevolucion.Visible = false;
        this.dv_movimiento_auto.Visible = false;
        this.dv_movimiento_manual.Visible = false;
        this.dv_descargar.Visible = false;

        switch (hf_accion.Value)
        {
            case "DEF_MOVER":
                CargaDropZona(0);//, tipo_solicitud);
                break;
            case "DESCARGA_POSICION_LI":
                CargaDropZona(0);//, tipo_solicitud);
                break;
            case "DEF_DESECHOS":
                rb_Desechos.Checked = true;
                CargaDropZona(6);//, tipo_solicitud);
                break;
            case "DEF_PALLETS":
                rb_Pallets.Checked = true;
                CargaDropZona(2);//;, tipo_solicitud);
                break;
            case "DEF_DESCARGA_LI":
                CargaDropZona(2);//, tipo_solicitud);
                this.dv_descargar.Visible = true;
                rb_descargar.Checked = true;
                break;
            case "DESECHOS_COMPLETAR":
                this.dv_movimiento_manual.Visible = true;
                rb_estacionamientoMan.Checked = true;
                this.rb_CheckedChanged(null, null);
            //    this.dv_cargado.Visible = true;
                break;
            case "PALLETS_TRASLADO_ANDEN":
                CargaDropZona(6);//;, tipo_solicitud);
                break;
            case "PALLETS_REINICIAR":
                CargaDropZona(6);//, tipo_solicitud);
                break;
            case "PALLETS_TRASLADO_EST":
                CargaDropZona(8);//;, tipo_solicitud);
                break;
            case "PALLETS_COMPLETAR":
                this.dv_descargadoDevolucion.Visible = true;
                this.dv_movimiento_auto.Visible = true;
               // this.dv_cargado.Visible = false;

                break;
            case "DESCARGA_POSICION":
                CargaDropZona(4);//;, tipo_solicitud);
                break;
            case "DESCARGA_COMPLETA":
                
          //      this.dv_movimiento_auto.Visible = true;
                this.dv_movimiento_manual.Visible = true;
                rb_estacionamientoMan.Checked = true;
               CargaDropZona(8);//;, tipo_solicitud);

                break;
            case "DESCARGA_COMPLETA_LI":

                this.dv_descargadoPallets.Visible = true;
                this.dv_movimiento_manual.Visible = true;
                CargaDropZona(3);//;, tipo_solicitud);
                break;
            case "DESCARGA_MOVER":
                CargaDropZona(0);//;, tipo_solicitud);
                btn_guardar.Visible = true;
                break;
            case "DESCARGA_BLOQUEO":
                CargaDropZona(0);
                break;
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalLogistica();", true);
    }

    private bool CrearSolicitud(int luga_id, string error_in, out string error_out)
    {
        bool error = true;
        error_out = error_in;
        int soli_id = Convert.ToInt32(hf_idSolicitud.Value);
        SolicitudBC s = new SolicitudBC();
        TrailerLogiBC t = new TrailerLogiBC();
        s.ID_SITE = Convert.ToInt32(dropsite.SelectedValue);
        s.SOLI_ID = soli_id;
        s.ID_USUARIO = usuario.ID;
        s.DOCUMENTO = txt_doc.Text;
        s.OBSERVACION = txt_obs.Text;
        s.ID_TRAILER = Convert.ToInt32(hf_idTrailer.Value);
        if (rb_Desechos.Checked)
        {
            error = t.Desechos_Crear(s, luga_id, out error_in);
            if (string.IsNullOrEmpty(error_in))
            {
                error_out += "Se ha creado la solicitud";
            }
            else
            {
                error_out += error_in;
            }
        }
        if (rb_Pallets.Checked)
        {
            error = t.Pallets_Crear(s, luga_id, out error_in);
            if (string.IsNullOrEmpty(error_in))
            {
                error_out += "Se ha creado la solicitud";
            }
            else
            {
                error_out += error_in;
            }
        }

        if (rb_estacionamiento.Checked)
        {
            MovimientoBC mov = new MovimientoBC();
            mov.ID_TRAILER = Convert.ToInt32(hf_idTrailer.Value);
            mov.OBSERVACION = "";

            mov.ID_DESTINO = 0;
            mov.OBSERVACION = txt_obs.Text;
            mov.petroleo = null;
            error = mov.MOVIMIENTO_automatico_estacinamiento(mov, Convert.ToInt32(dropsite.SelectedValue), usuario.ID, out error_in);

            if (string.IsNullOrEmpty(error_in))
            {
                error_out += "Trailer al estacionamiento";
            }
            else
            {
                error_out += error_in;
            }
        }

        if (rb_estacionamientoMan.Checked)
        {
            MovimientoBC mov = new MovimientoBC();
            mov.ID_TRAILER = Convert.ToInt32(hf_idTrailer.Value);
            mov.OBSERVACION = "";

            mov.ID_DESTINO = luga_id;
            mov.OBSERVACION = txt_obs.Text;
            mov.petroleo = null;
            error = mov.MOVIMIENTO(mov, Convert.ToInt32(dropsite.SelectedValue), usuario.ID, out error_in);

            if (string.IsNullOrEmpty(error_in))
            {
                error_out += "Trailer al estacionamiento";
            }
            else
            {
                error_out += error_in;
            }
        }

        if (rb_descargar.Checked)
        {
            s = new SolicitudBC();
            s.ID_SITE = Convert.ToInt32(dropsite.SelectedValue);
            s.ID_USUARIO = usuario.ID;
            s.DOCUMENTO = txt_doc.Text;
            s.OBSERVACION = txt_obs.Text;
            s.ID_TRAILER = Convert.ToInt32(hf_idTrailer.Value);

            error = t.DescargaLI_Crear(s, luga_id, out error_in);

            if (string.IsNullOrEmpty(error_in))
            {
                error_out = "Se ha creado la solicitud.";
            }
            else
            {
                error_out += error_in;
            }
        }

        return error;
    }

    private void Limpiar()
    {
        this.dv_tipo.Visible = false;
        dv_posicion.Visible = false;
        dv_solicitud.Visible = false;
        rb_Nada.Checked = false;
        rb_Desechos.Checked = false;
        rb_estacionamiento.Checked = false;
        rb_Pallets.Checked = false;
        ddl_zona.ClearSelection();
        ddl_zona_SelectedIndexChanged(null, null);
        ddl_posicion.ClearSelection();
    }

    private void ObtenerTrailer(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            TrailerLogiBC trailer = new TrailerLogiBC();
            int play_id = Convert.ToInt32(this.ddl_buscarPlaya.SelectedValue);


            DataTable dt = trailer.obtenerXParametroDesc(this.txt_buscarNombre.Text, this.txt_buscarNro.Text, this.chk_buscarInterno.Checked, Convert.ToInt32(ddl_buscarTipo.SelectedValue), Convert.ToInt32(ddl_buscarTransportista.SelectedValue), Convert.ToInt32(dropsite.SelectedValue), play_id);
            ViewState["lista"] = dt;
            ViewState.Remove("filtrados");
        }
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        if (ViewState["filtrados"] == null)
        {
            dw = new DataView((DataTable)ViewState["lista"]);
        }
        else
        {
            dw = new DataView((DataTable)ViewState["filtrados"]);
        }
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)ViewState["sortExpresion"];
        }
        gv_listar.DataSource = dw;
        gv_listar.DataBind();
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
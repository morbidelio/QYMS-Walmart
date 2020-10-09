// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_soli_logi : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();
    UsuarioBC usuario = new UsuarioBC();
    CargaDrops drop = new CargaDrops();

    protected void rb_CheckedChanged(object sender, EventArgs e)
    {
        //int site_id = Convert.ToInt32(dropsite.SelectedValue);
        if (this.rb_Nada.Checked)
        {
            this.hf_soli_tipo.Value = "0";
            this.dv_posicion.Visible = false;
            this.dv_solicitud.Visible = false;
        }
        if (this.rb_estacionamiento.Checked)
        {
            this.hf_soli_tipo.Value = "0";
            this.dv_posicion.Visible = false;
            this.dv_solicitud.Visible = false;
        }
        if (this.rb_estacionamientoMan.Checked)
        {
            this.hf_soli_tipo.Value = "0";

            if (this.hf_accion.Value.ToString() == "DESCARGA_COMPLETA_LI")
                this.CargaDropZona(3);

            else
                this.CargaDropZona(7);

                
            this.dv_posicion.Visible = true;
            this.dv_solicitud.Visible = true;
            int luga_trailer = Convert.ToInt32(this.hf_lugaid.Value);
            LugarBC luga = new LugarBC();
            luga = luga.obtenerXID(luga_trailer);

            try
            {
                this.ddl_zona.SelectedValue = luga.ID_ZONA.ToString();
                this.ddl_zona_SelectedIndexChanged(null, null);
                this.ddl_playa.SelectedValue = luga.ID_PLAYA.ToString();
                this.ddl_playa_SelectedIndexChanged(null, null);
                this.ddl_posicion.SelectedValue = luga.ID.ToString();
            }
            catch (Exception)
            {
                this.ddl_zona_SelectedIndexChanged(null, null);
            }


        }
        if (this.rb_Pallets.Checked)
        {
            this.hf_soli_tipo.Value = "3";
            this.CargaDropZona(2);
            this.dv_posicion.Visible = true;
            this.dv_solicitud.Visible = true;
            int luga_trailer = Convert.ToInt32(this.hf_lugaid.Value);
            LugarBC luga = new LugarBC();
            luga = luga.obtenerXID(luga_trailer);

            try
            {
                this.ddl_zona.SelectedValue = luga.ID_ZONA.ToString();
                this.ddl_zona_SelectedIndexChanged(null, null);
                this.ddl_playa.SelectedValue = luga.ID_PLAYA.ToString();
                this.ddl_playa_SelectedIndexChanged(null, null);
                this.ddl_posicion.SelectedValue = luga.ID.ToString();
            }
            catch (Exception)
            {
                this.ddl_zona_SelectedIndexChanged(null, null);
            }
        }
        if (this.rb_Desechos.Checked)
        {
            this.hf_soli_tipo.Value = "4";

            this.CargaDropZona(6);//, tipo_solicitud);
            this.dv_posicion.Visible = true;
            this.dv_solicitud.Visible = true;

            int luga_trailer = Convert.ToInt32(this.hf_lugaid.Value);
            LugarBC luga = new LugarBC();
            luga = luga.obtenerXID(luga_trailer);

            try
            {
                this.ddl_zona.SelectedValue = luga.ID_ZONA.ToString(); 
                this.ddl_zona_SelectedIndexChanged(null, null);
                this.ddl_playa.SelectedValue = luga.ID_PLAYA.ToString();
                this.ddl_playa_SelectedIndexChanged(null, null);
                this.ddl_posicion.SelectedValue = luga.ID.ToString();
            }
            catch (Exception)
            { 
                this.ddl_zona_SelectedIndexChanged(null, null);
            }
        }
        if (this.rb_descargar.Checked)
        {
            this.hf_soli_tipo.Value = "5";
            this.CargaDropZona(2);
            this.dv_posicion.Visible = true;
            this.dv_solicitud.Visible = true;
            int luga_trailer = Convert.ToInt32(this.hf_lugaid.Value);
            LugarBC luga = new LugarBC();
            luga = luga.obtenerXID(luga_trailer);

            try
            {
                this.ddl_zona.SelectedValue = luga.ID_ZONA.ToString();
                this.ddl_zona_SelectedIndexChanged(null, null);
                this.ddl_playa.SelectedValue = luga.ID_PLAYA.ToString();
                this.ddl_playa_SelectedIndexChanged(null, null);
                this.ddl_posicion.SelectedValue = luga.ID.ToString();
            }
            catch (Exception)
            {
                this.ddl_zona_SelectedIndexChanged(null, null);
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["Usuario"] == null)
        {
            this.Response.Redirect("~/InicioQYMS2.aspx");
        }
        this.usuario = (UsuarioBC)this.Session["Usuario"];
        if (!this.IsPostBack)
        {
            TrailerTipoBC tipo = new TrailerTipoBC();
            this.drop.Site_Normal(this.dropsite, this.usuario.ID);
            this.drop.Transportista(this.ddl_buscarTransportista);
            this.utils.CargaDrop(this.ddl_buscarTipo, "ID", "DESCRIPCION", tipo.obtenerTodo());
            this.ObtenerTrailer(true);
        }
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (this.Session["Usuario"] != null)
        {
            if (this.usuario.numero_sites < 2)
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
            s.SOLI_ID = int.Parse(hf_idSolicitud.Value);
            s.LUGA_ID = int.Parse(e.CommandArgument.ToString());
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
        string direccion = this.utils.ConvertSortDirectionToSql((String)this.ViewState["sortOrder"]);
        this.ViewState["sortOrder"] = direccion;
        this.ViewState["sortExpresion"] = string.Format("{0} {1}", e.SortExpression, direccion);
        this.ObtenerTrailer(false);
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

            this.hf_idTrailer.Value = this.gv_listar.DataKeys[index].Values[0].ToString();
            this.hf_idSolicitud.Value = this.gv_listar.DataKeys[index].Values[1].ToString();
            this.hf_lugaid.Value = this.gv_listar.DataKeys[index].Values[2].ToString();
            this.hf_soanorden.Value = this.gv_listar.DataKeys[index].Values[3].ToString();
            this.hf_timestamp.Value = this.gv_listar.DataKeys[index].Values[4].ToString();

            this.Limpiar();
            this.hf_accion.Value = e.CommandName;
            this.TituloModal(e.CommandName);
            this.hf_soli_tipo.Value = "0";


            this.rb_descargar.Checked = false;
            this.rb_Desechos.Checked = false;
            this.rb_estacionamiento.Checked = false;
            this.rb_estacionamientoMan.Checked = false;
            this.rb_Pallets.Checked = false;
            this.rb_Nada.Checked = false;

            switch (this.hf_accion.Value)
            {
                case "DEF_PALLETS":
                    this.rb_Pallets.Checked = true;
                    this.hf_soli_tipo.Value = "3";
                    this.Modal(false, true, true);

                    int luga_trailer = Convert.ToInt32(this.hf_lugaid.Value);
                    LugarBC luga = new LugarBC();
                    luga = luga.obtenerXID(luga_trailer);

                    try
                    {
                        this.ddl_zona.SelectedValue = luga.ID_ZONA.ToString(); 
                        this.ddl_zona_SelectedIndexChanged(null, null);
                        this.ddl_playa.SelectedValue = luga.ID_PLAYA.ToString();
                        this.ddl_playa_SelectedIndexChanged(null, null);
                        this.ddl_posicion.SelectedValue = luga.ID.ToString();
                    }
                    catch (Exception)
                    { 
                        this.ddl_zona_SelectedIndexChanged(null, null);
                    }
                    break;
                case "DEF_DESECHOS":
                    this.rb_Desechos.Checked = true;
                    this.hf_soli_tipo.Value = "4";
                    this.Modal(false, true, true);

                    luga_trailer = Convert.ToInt32(this.hf_lugaid.Value);
                    luga = new LugarBC();
                    luga = luga.obtenerXID(luga_trailer);

                    try
                    {
                        this.ddl_zona.SelectedValue = luga.ID_ZONA.ToString(); 
                        this.ddl_zona_SelectedIndexChanged(null, null);
                        this.ddl_playa.SelectedValue = luga.ID_PLAYA.ToString();
                        this.ddl_playa_SelectedIndexChanged(null, null);
                        this.ddl_posicion.SelectedValue = luga.ID.ToString();
                    }
                    catch (Exception)
                    { 
                        this.ddl_zona_SelectedIndexChanged(null, null);
                    }
                    break;
                case "DESCARGA_COMPLETA_LI":
                case "PALLETS_COMPLETAR":
                    this.Modal(true, false, false);
                    break;
                case "DESECHOS_COMPLETAR":
                    this.Modal(true, false, false);
                    break;
                case "DEF_MOVER":
                    this.Modal(false, true, true);
                    break;
                case "DEF_DESCARGA_LI":
                    this.hf_soli_tipo.Value = "5";
                    this.Modal(false, true, false);
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
                    this.Modal(false, true, false);
                    break;
                case "DESCARGA_COMPLETA":
                    bool ext = Convert.ToBoolean(this.gv_listar.DataKeys[index].Values[5]);
                    if (ext)
                    {
                        this.Modal(false, true, false);
                    }
                    else
                    {
                        string mensaje;
                        SolicitudBC s = new SolicitudBC();
                        bool error = s.DescargaCompleta(Convert.ToInt32(this.hf_idSolicitud.Value), out mensaje, 0);
                        if (error)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "showAlert('Se ha completado la descarga');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", string.Format("showAlert4('{0}');", mensaje), true);
                        }
                        this.ObtenerTrailer(true);
                    }
                    break;
                case "DESCARGA_BLOQUEAR": 
                    SolicitudAndenesBC sa = new SolicitudAndenesBC();
                    sa.SOLI_ID = Convert.ToInt32(hf_idSolicitud.Value);
                    LugarBC l = new LugarBC();
                    int play_id = sa.ObtenerPlayaId();
                    drop.Lugar1(this.ddl_bloquearPos, int.Parse(this.dropsite.SelectedValue), play_id, 0, 1);// l.ObtenerXPlaya(play_id));
                    //drops.Lugar(ddl_bloquearPos, 0, play_id);
                    //utils.CargaDrop(ddl_bloquearPos, "ID", "DESCRIPCION", l.ObtenerXPlaya(play_id));
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
        this.ObtenerTrailer(true);
    }

    protected void ddl_zona_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddl_zona.SelectedIndex > 0)
        {
            //int tipo_solicitud = int.Parse(this.hf_soli_tipo.Value);
            this.ddl_playa.Enabled = true;

            if (this.rb_Desechos.Visible == true && this.rb_Desechos.Checked == true)
            {
                this.CargaDropPlaya(1);
            }
            else if (this.rb_Pallets.Visible == true && this.rb_Pallets.Checked == true)
            {
                this.CargaDropPlaya(1);
            }
            else if (this.rb_estacionamientoMan.Visible == true && this.rb_estacionamientoMan.Checked == true)
            {
                this.CargaDropPlaya(2);
            }
            else if (this.rb_descargar.Visible == true && this.rb_descargar.Checked == true)
            {
                this.CargaDropPlaya(1);
            }
            else
            {
                switch (this.hf_accion.Value)
                {
                    case "DESCARGA_POSICION_LI":
                        this.CargaDropPlaya(0);//;, tipo_solicitud);
                        break;
                    case "DEF_MOVER":
                        this.CargaDropPlaya(0);//, tipo_solicitud);
                        break;
                    case "DEF_PALLETS":
                        this.CargaDropPlaya(1);//, tipo_solicitud);
                        break;
                    case "DEF_DESECHOS":
                        this.CargaDropPlaya(1);//, tipo_solicitud);
                        break;
                    case "DEF_DESCARGA_LI":
                        this.CargaDropPlaya(1);//, tipo_solicitud);
                        break;
                    case "DESECHOS_TRASLADO_ANDEN":
                        this.CargaDropPlaya(1);//, tipo_solicitud);
                        break;
                    case "DESECHOS_REINICIAR":
                        this.CargaDropPlaya(1);//, tipo_solicitud);
                        break;
                    case "DESECHOS_TRASLADO_EST":
                        this.CargaDropPlaya(2);//, tipo_solicitud);
                        break;
                    case "PALLETS_TRASLADO_ANDEN":
                        this.CargaDropPlaya(1);//, tipo_solicitud);
                        break;
                    case "PALLETS_REINICIAR":
                        this.CargaDropPlaya(1);//, tipo_solicitud);
                        break;
                    case "PALLETS_TRASLADO_EST":
                        this.CargaDropPlaya(2);//, tipo_solicitud);
                        break;
                    case "DESCARGA_COMPLETA":
                        this.CargaDropPlaya(2);//, tipo_solicitud);
                        break;
                    case "DESCARGA_POSICION":
                        this.CargaDropPlaya(1);//, tipo_solicitud);
                        break;
                    case "DESCARGA_MOVER":
                        this.CargaDropPlaya(0);//, tipo_solicitud);
                        break;
                    case "DESCARGA_BLOQUEAR":
                        this.CargaDropPlaya(0);
                        break;
                    default:
                        this.CargaDropPlaya(0);//, tipo_solicitud);
                        break;
                }
            }
        }
        else
        {
            this.ddl_playa.ClearSelection();
            this.ddl_playa.Enabled = false;
        }
        this.ddl_playa_SelectedIndexChanged(null, null);
    }

    protected void ddl_playa_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddl_playa.SelectedIndex > 0)
        {
            LugarBC l = new LugarBC();
            int play_id = Convert.ToInt32(this.ddl_playa.SelectedValue);
            int trai_id = Convert.ToInt32(this.hf_idTrailer.Value);
            this.ddl_posicion.Enabled = true;
            //    DataTable dt = l.ObtenerXPlaya(play_id, 0, 1, trai_id);
            //    utils.CargaDrop(ddl_posicion, "ID", "DESCRIPCION", dt);
            int solicitudtipo = int.Parse(this.hf_soli_tipo.Value);
            this.drop.Lugar1(this.ddl_posicion, int.Parse(this.dropsite.SelectedValue), Convert.ToInt32(this.ddl_playa.SelectedValue), 0, 1, trai_id, solicitudtipo);
        }
        else
        {
            this.ddl_posicion.ClearSelection();
            this.ddl_posicion.Enabled = false;
        }
    }

    #endregion

    #region Buttons

    protected void btn_bloquearAgregar_Click(object sender, EventArgs e)
    {
        SolicitudAndenesBC s = new SolicitudAndenesBC();
        string resultado;
        s.LUGA_ID = int.Parse(ddl_bloquearPos.SelectedValue);
        s.SOLI_ID = int.Parse(hf_idSolicitud.Value);

        if (s.Bloquear(usuario.ID, out resultado))
        {
            gv_bloqueo.DataSource = s.ObtenerBloqueados();
            gv_bloqueo.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "showAlert('" + resultado + "');", true);
        }

        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('" + resultado + "');", true);
        }


    }

    protected void btn_guardar_Click(object sender, EventArgs e)
    {
        TrailerLogiBC t = new TrailerLogiBC();
        SolicitudBC s = new SolicitudBC();
        int soli_id = Convert.ToInt32(this.hf_idSolicitud.Value);
        int luga_trailer = 0;
        int soan_orden = 0;
        try
        {
            luga_trailer = Convert.ToInt32(this.hf_lugaid.Value);
            soan_orden = Convert.ToInt32(this.hf_soanorden.Value);
        }
        catch (Exception)
        {
        }

        int luga_id = 0;
        int site_id = 0;
        try
        {
            luga_id = Convert.ToInt32(this.ddl_posicion.SelectedValue);
            site_id = Convert.ToInt32(this.dropsite.SelectedValue);
        }
        catch (Exception)
        {
        }
        
        string mensaje = "";
        bool error = true;
        switch (this.hf_accion.Value)
        {
            case "DEF_PALLETS":
            case "DEF_DESECHOS":
                if (luga_id != 0)
                {
                    error = this.CrearSolicitud(luga_id, "", out mensaje);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Complete todos los datos');", true);
                    return;
                }
                break;
            case "DEF_DESCARGA_LI":
                if (luga_id != 0)
                {
                    s = new SolicitudBC();
                    s.ID_SITE = site_id;
                    s.ID_USUARIO = this.usuario.ID;
                    s.DOCUMENTO = this.txt_doc.Text;
                    s.OBSERVACION = this.txt_obs.Text;
                    s.ID_TRAILER = Convert.ToInt32(this.hf_idTrailer.Value);

                    error = t.DescargaLI_Crear(s, luga_id, out mensaje);

                    if (string.IsNullOrEmpty(mensaje))
                    {
                        mensaje = "Se ha creado la solicitud.";
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Complete todos los datos');", true);
                    return;
                }
                break;
            case "DEF_MOVER":
                MovimientoBC mov = new MovimientoBC();
                mov.ID_TRAILER = int.Parse(this.hf_idTrailer.Value);
                mov.OBSERVACION = "";
                mov.ID_DESTINO = Convert.ToInt32(this.ddl_posicion.SelectedValue);
                mov.OBSERVACION = this.txt_obs.Text;
                mov.petroleo = null;
                error = mov.MOVIMIENTO(mov, site_id, this.usuario.ID, out mensaje);
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
                error = t.DescargaLI_Completar(soli_id, luga_trailer, soan_orden, this.usuario.ID, out mensaje);
                if (string.IsNullOrEmpty(mensaje))
                {
                    string error_out;
                    mensaje = "Se ha completado la solicitud.";
                    error = this.CrearSolicitud(luga_id, mensaje, out error_out);
                    mensaje = error_out;
                }
                break;
            case "DESECHOS_COMPLETAR":
                error = t.Desechos_Completar(soli_id, luga_trailer, soan_orden, this.usuario.ID,  out mensaje);
                if (string.IsNullOrEmpty(mensaje) && !this.rb_Nada.Checked)
                {
                    mensaje = "Se ha completado la solicitud. ";
                   string error_out;
                    error = this.CrearSolicitud(luga_id, mensaje, out error_out);
                    mensaje = error_out;
                }
                break;
            case "PALLETS_COMPLETAR":
                error = t.PALLETS_Completar(soli_id, luga_trailer, soan_orden, this.usuario.ID, out mensaje);
                if (string.IsNullOrEmpty(mensaje) && !this.rb_Nada.Checked)
                {
                    mensaje = "Se ha completado la solicitud. ";
                    string error_out;
                    error = this.CrearSolicitud(luga_id, mensaje, out error_out);
                    mensaje = error_out;
                }
                break;

            case "PALLETS_TRASLADO_ANDEN":
                error = t.Pallets_Reiniciar(soli_id, luga_trailer, luga_id, this.usuario.ID, soan_orden, soan_orden + 1, out mensaje);
        
                if (string.IsNullOrEmpty(mensaje))
                {
                    mensaje = "Se ha generado el movimiento";
                }
                break;
            case "PALLETS_TRASLADO_EST":
    
                error = t.Pallets_TrasladoEst(soli_id, luga_trailer, luga_id, soan_orden, this.usuario.ID, out mensaje);
                if (string.IsNullOrEmpty(mensaje))
                {
                    mensaje = "Se ha generado el movimiento";
                }
                break;
            case "PALLETS_REINICIAR":
                error = t.Pallets_Reiniciar(soli_id, luga_trailer, luga_id, this.usuario.ID, 0, soan_orden + 1, out mensaje);
                if (string.IsNullOrEmpty(mensaje))
                {
                    mensaje = "Se ha reanudado la descarga";
                }
                break;
            case "DEF_DESCARGA":
            case "DESCARGA_COMPLETA":
                error = s.DescargaCompleta(soli_id, out mensaje, luga_id);
                if (string.IsNullOrEmpty(mensaje))
                {
                    mensaje = "Se ha completado la descarga";
                }
                break;
            case "DESCARGA_POSICION":
                s = new SolicitudBC();
                s.SOLI_ID = int.Parse(this.hf_idSolicitud.Value);
                s.OBSERVACION = "";
                s.ID_DESTINO = luga_id;
                s.TIMESTAMP = DateTime.Parse(this.hf_timestamp.Value);
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
                m.ID_TRAILER = Convert.ToInt32(this.hf_idTrailer.Value);
                m.ID_SOLICITUD = soli_id;
                m.OBSERVACION = "";

                error = s.DescargaMovimiento(m, site_id, this.usuario.ID, out mensaje);
                if (string.IsNullOrEmpty(mensaje))
                {
                    mensaje = "Se ha generado el movimiento";
                }
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
        this.ObtenerTrailer(true);
    }

    protected void btn_buscarTrailer_Click(object sender, EventArgs e)
    {
        this.ObtenerTrailer(true);
    }

    #endregion

    #region UtilsPagina

    private void CargaDropZona(int tipo)
    {
        ZonaBC z = new ZonaBC();
        int site_id = Convert.ToInt32(this.dropsite.SelectedValue);
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

        this.utils.CargaDrop(this.ddl_zona, "ID", "DESCRIPCION", dt);
        this.ddl_zona_SelectedIndexChanged(null, null);
    }

    private void CargaDropPlaya(int tipo)
    {
        PlayaBC p = new PlayaBC();
        int zona_id = Convert.ToInt32(this.ddl_zona.SelectedValue);
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
        this.utils.CargaDrop(this.ddl_playa, "ID", "DESCRIPCION", dt);
        this.ddl_playa_SelectedIndexChanged(null, null);
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
        this.dv_posicion.Visible = posicion;
        this.dv_solicitud.Visible = solicitud;
        ddl_zona.Enabled = true;
        this.dv_descargadoPallets.Visible = false;
        this.dv_descargadoDevolucion.Visible = false;
        this.dv_movimiento_auto.Visible = false;
        this.dv_movimiento_manual.Visible = false;
        this.dv_descargar.Visible = false;
        this.dv_SolicitudWays.Visible = false;

        switch (this.hf_accion.Value)
        {
            case "DEF_MOVER":
                this.CargaDropZona(0);//, tipo_solicitud);
                break;
            case "DESCARGA_POSICION_LI":
                this.CargaDropZona(0);//, tipo_solicitud);
                break;
            case "DEF_DESECHOS":
                this.rb_Desechos.Checked = true;
                this.CargaDropZona(6);//, tipo_solicitud);
                break;
            case "DEF_PALLETS":
                this.rb_Pallets.Checked = true;
                this.CargaDropZona(2);//;, tipo_solicitud);
                break;
            case "DEF_DESCARGA_LI":
                this.CargaDropZona(2);//, tipo_solicitud);
                this.dv_descargar.Visible = true;
                this.rb_descargar.Checked = true;
                break;
            case "DESECHOS_COMPLETAR":
                this.dv_movimiento_manual.Visible = true;
                this.rb_estacionamientoMan.Checked = true;
                this.rb_CheckedChanged(null, null);
            //    this.dv_cargado.Visible = true;
                break;
            case "PALLETS_TRASLADO_ANDEN":
                this.CargaDropZona(6);//;, tipo_solicitud);
                break;
            case "PALLETS_REINICIAR":
                this.CargaDropZona(6);//, tipo_solicitud);
                break;
            case "PALLETS_TRASLADO_EST":
                this.CargaDropZona(3);//;, tipo_solicitud);
                break;
            case "PALLETS_COMPLETAR":
                this.dv_descargadoDevolucion.Visible = true;
                this.dv_movimiento_auto.Visible = true;
               // this.dv_cargado.Visible = false;

                break;
            case "DESCARGA_POSICION":
                this.CargaDropZona(4);//;, tipo_solicitud);
                break;
            case "DESCARGA_COMPLETA":
                
          //      this.dv_movimiento_auto.Visible = true;
                this.dv_movimiento_manual.Visible = true;
                this.rb_estacionamientoMan.Checked = true;
                
               this.CargaDropZona(8);//;, tipo_solicitud);

                break;
            case "DESCARGA_COMPLETA_LI":

                this.dv_descargadoPallets.Visible = true;
                this.dv_movimiento_auto.Visible = true;
                this.CargaDropZona(3);//;, tipo_solicitud);
                this.dv_SolicitudWays.Visible = false;
                break;
            case "DESCARGA_MOVER":
                this.CargaDropZona(0);//;, tipo_solicitud);
                break;
            case "DESCARGA_BLOQUEO":
                this.CargaDropZona(0);
                break;
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalLogistica();", true);
    }

    private bool CrearSolicitud(int luga_id, string error_in, out string error_out)
    {
        bool error = true;
        error_out = error_in;
        int soli_id = Convert.ToInt32(this.hf_idSolicitud.Value);
        SolicitudBC s = new SolicitudBC();
        TrailerLogiBC t = new TrailerLogiBC();
        s.ID_SITE = Convert.ToInt32(this.dropsite.SelectedValue);
        s.SOLI_ID = soli_id;
        s.ID_USUARIO = this.usuario.ID;
        s.DOCUMENTO = this.txt_doc.Text;
        s.OBSERVACION = this.txt_obs.Text;
        s.ID_TRAILER = Convert.ToInt32(this.hf_idTrailer.Value);
        if (this.rb_Desechos.Checked)
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
        if (this.rb_Pallets.Checked)
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

        if (this.rb_estacionamiento.Checked)
        {
            MovimientoBC mov = new MovimientoBC();
            mov.ID_TRAILER = int.Parse(this.hf_idTrailer.Value);
            mov.OBSERVACION = "";

            mov.ID_DESTINO = 0;
            mov.OBSERVACION = this.txt_obs.Text;
            mov.petroleo = null;
            error = mov.MOVIMIENTO_automatico_estacinamiento(mov, Convert.ToInt32(this.dropsite.SelectedValue), this.usuario.ID, out error_in);

            if (string.IsNullOrEmpty(error_in))
            {
                error_out += "Trailer al estacionamiento";
            }
            else
            {
                error_out += error_in;
            }
        }

        if (this.rb_estacionamientoMan.Checked)
        {
            MovimientoBC mov = new MovimientoBC();
            mov.ID_TRAILER = int.Parse(this.hf_idTrailer.Value);
            mov.OBSERVACION = "";

            mov.ID_DESTINO = luga_id;
            mov.OBSERVACION = this.txt_obs.Text;
            mov.petroleo = null;
            error = mov.MOVIMIENTO(mov, Convert.ToInt32(this.dropsite.SelectedValue), this.usuario.ID, out error_in);

            if (string.IsNullOrEmpty(error_in))
            {
                error_out += "Trailer al estacionamiento";
            }
            else
            {
                error_out += error_in;
            }
        }

        if (this.rb_descargar.Checked)
        {
            s = new SolicitudBC();
            s.ID_SITE = Convert.ToInt32(this.dropsite.SelectedValue);
            s.ID_USUARIO = this.usuario.ID;
            s.DOCUMENTO = this.txt_doc.Text;
            s.OBSERVACION = this.txt_obs.Text;
            s.ID_TRAILER = Convert.ToInt32(this.hf_idTrailer.Value);

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


        if (rb_solicitudWays.Checked == true)
        {
            string url = string.Format("Solicitud_Carga.aspx?type=new&trai_id={0}", this.hf_idTrailer.Value);
            this.Response.Redirect(url);
        
        }

        return error;
    }

    private void Limpiar()
    {
        this.dv_tipo.Visible = false;
        this.dv_posicion.Visible = false;
        this.dv_solicitud.Visible = false;
        this.rb_Nada.Checked = false;
        this.rb_Desechos.Checked = false;
        this.rb_estacionamiento.Checked = false;
        this.rb_Pallets.Checked = false;
        this.ddl_zona.ClearSelection();
        this.ddl_zona_SelectedIndexChanged(null, null);
        this.ddl_posicion.ClearSelection();
    }

    private void ObtenerTrailer(bool forzarBD)
    {
        if (this.ViewState["lista"] == null || forzarBD)
        {
            TrailerLogiBC trailer = new TrailerLogiBC();

            DataTable dt = trailer.obtenerXParametro(this.txt_buscarNombre.Text, this.txt_buscarNro.Text, this.chk_buscarInterno.Checked, int.Parse(this.ddl_buscarTipo.SelectedValue), int.Parse(this.ddl_buscarTransportista.SelectedValue), Convert.ToInt32(this.dropsite.SelectedValue));
            this.ViewState["lista"] = dt;
            this.ViewState.Remove("filtrados");
        }
        DataView dw = new DataView((DataTable)this.ViewState["lista"]);
        if (this.ViewState["filtrados"] == null)
        {
            dw = new DataView((DataTable)this.ViewState["lista"]);
        }
        else
        {
            dw = new DataView((DataTable)this.ViewState["filtrados"]);
        }
        if (this.ViewState["sortExpresion"] != null && this.ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)this.ViewState["sortExpresion"];
        }
        this.gv_listar.DataSource = dw;
        this.gv_listar.DataBind();
    }
    #endregion
}
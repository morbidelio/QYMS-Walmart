using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Solicitud_Descarga : System.Web.UI.Page
{

    static DataTable table = new DataTable();
    static UtilsWeb utils = new UtilsWeb();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CreaDataTable();

            if (Request.Params["type"] == null)
            {
                this.txt_buscarFecha.Text = DateTime.Now.ToShortDateString();
                this.txt_buscarHora.Text = DateTime.Now.ToShortTimeString();
                YMS_ZONA_BC yms = new YMS_ZONA_BC();
                DataTable ds = yms.ObteneSites(((UsuarioBC)Session["Usuario"]).ID);
                utils.CargaDropNormal(dropsite, "ID", "NOMBRE", ds);
                drop_SelectedIndexChanged(null, null);
            }
            else
            {
                YMS_ZONA_BC yms = new YMS_ZONA_BC();
                DataTable ds = yms.ObteneSites(((UsuarioBC)Session["Usuario"]).ID);
                utils.CargaDropNormal(dropsite, "ID", "NOMBRE", ds);
                llenarForm();
            }
        }
        if (table.Rows.Count > 0)
        {
            ddl_destinoZona.Enabled = false;
            ddl_destinoPlaya.Enabled = false;
        }
        else
        {
            ddl_destinoZona.Enabled = true;
         //   ddl_destinoZona_SelectedIndexChanged(null, null);
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
                this.SITE.Visible = false;
            }
        }
    }
    #region DropDownList
    protected void drop_SelectedIndexChanged(object sender, EventArgs e)
    {
        YMS_ZONA_BC yms = new YMS_ZONA_BC();

        utils.CargaDrop(ddl_destinoZona, "ID", "DESCRIPCION", yms.ObtenerZonasDescarga(Convert.ToInt32(dropsite.SelectedValue)));
        ddl_destinoZona_SelectedIndexChanged(null, null);
    }
    protected void ddl_destinoZona_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!(ddl_destinoZona.SelectedIndex == 0))
        {
            int id_zona = Convert.ToInt32(ddl_destinoZona.SelectedValue);
            utils.CargaDrop(ddl_destinoPlaya, "ID", "DESCRIPCION", new PlayaBC().ObtenerPlayasXCriterio(null, null, id_zona, false, "100"));
            ddl_destinoPlaya.Enabled = true;
            ddl_destinoPlaya_SelectedIndexChanged(null, null);
        }
        else
        {
            ddl_destinoPos.Enabled = false;
            ddl_bloquearPos.Enabled = false;
            ddl_destinoPlaya.Enabled = false;
            ddl_destinoPos.ClearSelection();
            ddl_bloquearPos.ClearSelection();
            ddl_destinoPlaya.ClearSelection();
        }
    }
    protected void ddl_destinoPlaya_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!(ddl_destinoPlaya.SelectedIndex == 0) || (ddl_destinoZona.SelectedIndex == 0))
        {
            int id_playa = Convert.ToInt32(ddl_destinoPlaya.SelectedValue);
            DataTable ds1 = new YMS_ZONA_BC().Obtenerlugares_playa(id_playa, null, "0");
            utils.CargaDrop(ddl_destinoPos, "ID", "DESCRIPCION", ds1);
            utils.CargaDrop(ddl_bloquearPos, "ID", "DESCRIPCION", ds1);
            ddl_destinoPos.Enabled = true;
            ddl_bloquearPos.Enabled = true;
        }
        else
        {
            ddl_destinoPos.ClearSelection();
            ddl_bloquearPos.ClearSelection();
            ddl_destinoPos.Enabled = false;
            ddl_bloquearPos.Enabled = false;
        }
    }
    #endregion
    #region Buttons
    protected void btn_confirmar_Click(object sender, EventArgs e)
    {
        DataView dw = table.AsDataView();
        dw.RowFilter = "POSICION1 = " + ddl_destinoPos.SelectedValue;
        if (dw.Count != 0)
        {
            utils.ShowMessage2(this, "guardar", "warn_destinoBloqueado");
            return;
        }
        try
        {
            string resultado;
            UsuarioBC usuario = new UsuarioBC();
            usuario = (UsuarioBC)Session["Usuario"];
            SolicitudBC solicitud = new SolicitudBC();
            TrailerUltEstadoBC traiue = new TrailerUltEstadoBC();
            int id = Convert.ToInt32(hf_trailerId.Value);
            traiue = traiue.CargaTrue(id);
            string fh = txt_buscarFecha.Text + " " + txt_buscarHora.Text;
            solicitud.ID_TIPO = 2;
            solicitud.ID_USUARIO = usuario.ID;   // Variable de sesión
            solicitud.FECHA_CREACION = DateTime.Now;
            solicitud.FECHA_PLAN_ANDEN = DateTime.Parse(fh);
            solicitud.DOCUMENTO = traiue.DOC_INGRESO;
            solicitud.OBSERVACION = "";
            solicitud.ID_TRAILER = id;
            solicitud.ID_DESTINO = Convert.ToInt32(ddl_destinoPos.SelectedValue);
            solicitud.ID_SITE = Convert.ToInt32(this.dropsite.SelectedValue);

            string bloqueados = "";

            for (int i = 0; i < table.Rows.Count; i++)
            {
                bloqueados = bloqueados + table.Rows[i]["POSICION1"].ToString();
                bloqueados += (i < table.Rows.Count - 1) ? "," : string.Empty;
            }


            bool ejecucion = solicitud.Descarga(solicitud, bloqueados, out resultado);
            if (ejecucion && resultado == "")
            {
                limpia(null, null);
                utils.ShowMessage2(this, "guardar", "success");
            }
            else
                utils.ShowMessage(this, resultado, "error", false);
        }
        catch (Exception EX)
        {
            utils.ShowMessage(this, EX.Message, "error", false);
        }
    }
    protected void limpia(object sender, EventArgs e)
    {
        hf_trailerId.Value = "";
        txt_nroFlota.Text = "";
        txt_buscarNro.Text = "";
        this.txt_buscarPatente.Text = "";
        ddl_destinoZona.ClearSelection();
        ddl_destinoZona.Enabled = true;
        this.drop_SelectedIndexChanged(null, null);
        ddl_destinoZona_SelectedIndexChanged(null, null);
        table.Rows.Clear();
        this.gv_Seleccionados.DataSource = null;
        this.gv_Seleccionados.DataBind();
        ViewState["lista"] = null;
        ddl_bloquearPos.Items.Clear();
        ddl_bloquearPos.Items.Add(new ListItem("Seleccione", "0"));
        ddl_bloquearPos.Enabled = false;

        pnl_detalleLugar.Style.Add("background-color", "#ffffff");
        pnl_detalleLugar.Style.Add("color", "black");
        pnl_detalleTrailer.Attributes.Add("style", string.Format("background-color:{0};", "#ffffff"));
        pnl_imgAlerta.BackColor = System.Drawing.Color.White;
        img_trailer.ImageUrl = "";
        lbl_origenZona.Text = "";
        lbl_origenPlaya.Text = "";
        txt_transporte.Text = "";
        lbl_lugar.Text = "";

    }
    protected void btn_AgregarListado_Click(object sender, EventArgs e)
    {
        
        DataView dw = table.AsDataView();
        try
        {
            dw.RowFilter = "POSICION1 = " + ddl_bloquearPos.SelectedValue;
        }
        catch (Exception ex)
        {
        }

        
  
        if (dw.ToTable().Rows.Count != 0)
        {
            utils.ShowMessage2(this, "bloquear", "warn_existe");
            return;
        }
        try
        {
            DataRow row = table.NewRow();
            row["ZONA1"] = ddl_destinoZona.SelectedValue;
            row["ZONA"] = ddl_destinoZona.SelectedItem.Text;
            row["PLAYA1"] = ddl_destinoPlaya.SelectedValue;
            row["PLAYA"] = ddl_destinoPlaya.SelectedItem.Text;
            row["POSICION1"] = ddl_bloquearPos.SelectedValue;
            row["POSICION"] = ddl_bloquearPos.SelectedItem.Text;
            table.Rows.Add(row);
            ViewState["lista"] = table;
            this.gv_Seleccionados.DataSource = table;
            this.gv_Seleccionados.DataBind();
        }
        catch (Exception EX)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Ocurrió un error!');", true);
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();
        LugarBC lugar = new LugarBC();
        bool error = true;
        trailer = (txt_buscarNro.Text != "") ? trailer.obtenerXNro(txt_buscarNro.Text): trailer.obtenerXPlaca(txt_buscarPatente.Text);
        if ((trailer.ID == 0)) //Trailer no existe
        {
            limpia(null, null);
            utils.ShowMessage2(this, "buscarTrailer", "warn_trailerNoExiste");
            return;
        }

        if (trailer.SITE_ID == 0 || !trailer.SITE_IN || trailer.LUGAR_ID == 0)
        {
            limpia(null, null);
            utils.ShowMessage2(this, "buscarTrailer", "warn_trailerFueraCD");
            return;
        }
        if (trailer.SITE_ID != Convert.ToInt32(this.dropsite.SelectedValue))
        {
            limpia(null, null);
            utils.ShowMessage2(this, "buscarTrailer", "warn_trailerOtroCD");
            return;
        }
        if (string.IsNullOrEmpty(hf_soliId.Value) && trailer.SOLI_ID != 0)
        {
            limpia(null, null);
            utils.ShowMessage2(this, "buscarTrailer", "warn_trailerSolPendiente");
            return;
        }
        if (!string.IsNullOrEmpty(hf_soliId.Value) && trailer.SOLI_ID.ToString() != hf_soliId.Value && trailer.SOLI_ID != 0)
        {
            limpia(null, null);
            utils.ShowMessage2(this, "buscarTrailer", "warn_trailerSolDistinta");
            return;
        }
        if (trailer.MOVI_ID != 0)
        {
            limpia(null, null);
            utils.ShowMessage2(this, "buscarTrailer", "warn_trailerMovPendiente");
            return;
        }
        else if (trailer.TRES_ID != 400)
        {
            switch (trailer.TRES_ID)
            {
                case 100:
                    utils.ShowMessage2(this, "buscarTrailer", "warn_trailerDescargado");
                    break;
                case 150:
                case 200:
                    utils.ShowMessage2(this, "buscarTrailer", "warn_trailerCargando");
                    break;
                case 300:
                case 310:
                    utils.ShowMessage2(this, "buscarTrailer", "warn_trailerDescargando");
                    break;
                case 500:
                    utils.ShowMessage2(this, "buscarTrailer", "warn_trailerRuta");
                    break;
                case 600:
                    utils.ShowMessage2(this, "buscarTrailer", "warn_trailerBloqueado");
                    break;
            }
            limpia(null, null);
            return;
        }

        table.Rows.Clear();
        gv_Seleccionados.DataSource = null;
        gv_Seleccionados.DataBind();
        lugar = lugar.obtenerXID(trailer.LUGAR_ID);
        DataTable dt_lugares = lugar.obtenerLugarEstado(lugar.ID_PLAYA, lugar.ID, lugar.ID_SITE);
        DataRow row_lugar = dt_lugares.Rows[0];
        hf_trailerId.Value = trailer.ID.ToString();
        pnl_detalleLugar.Style.Add("background-color", row_lugar["COLOR_LUGAR"].ToString());
        pnl_detalleLugar.Style.Add("color", "black");
        lbl_lugar.Text = row_lugar["LUGA_COD"].ToString();
        pnl_detalleTrailer.Attributes.Add("style", string.Format("background-color:{0};", row_lugar["TRTI_COLOR"].ToString()));
        int estado_sol = Convert.ToInt32(row_lugar["SOES_ID"].ToString());
        pnl_imgAlerta.BackColor = System.Drawing.ColorTranslator.FromHtml(row_lugar["COLOR"].ToString());
        if (row_lugar["tres_icono"].ToString() != "")
            img_trailer.ImageUrl = row_lugar["tres_icono"].ToString();
        lbl_origenZona.Text = lugar.ZONA;
        lbl_origenPlaya.Text = lugar.PLAYA;
        ddl_destinoZona.Enabled = true;
        txt_nroFlota.Text = trailer.NUMERO;
        txt_transporte.Text = trailer.TRANSPORTISTA;
        utils.ShowMessage2(this, "buscarTrailer", "success");
    }
    #endregion
    #region UtilsPagina
    private void CreaDataTable()
    {
        table.Columns.Clear();
        table.Columns.Add("ZONA1", typeof(String));
        table.Columns.Add("ZONA", typeof(String));
        table.Columns.Add("PLAYA1", typeof(String));
        table.Columns.Add("PLAYA", typeof(String));
        table.Columns.Add("POSICION1", typeof(String));
        table.Columns.Add("POSICION", typeof(String));


    }
    private void llenarForm()
    {
        int soli_id = Convert.ToInt32(Request.Params["soli_id"].ToString());
        hf_soliId.Value = soli_id.ToString();
        SolicitudBC solicitud = new SolicitudBC();
        SolicitudAndenesBC sa = new SolicitudAndenesBC();
        LugarBC l = new LugarBC();
        TrailerBC t = new TrailerBC();
        solicitud = solicitud.ObtenerXId(soli_id);
        sa = sa.ObtenerXId(soli_id, 1);
        txt_buscarFecha.Text = solicitud.FECHA_CREACION.ToString("dd/MM/yyyy");
        txt_buscarHora.Text = solicitud.FECHA_CREACION.ToString("hh:mm");
        hf_trailerId.Value = solicitud.ID_TRAILER.ToString();
        t = t.obtenerXID(solicitud.ID_TRAILER);
        txt_buscarPatente.Text = t.PLACA;
        btnBuscar_Click(null, null);
        l = l.obtenerXID(sa.LUGA_ID);
        dropsite.SelectedValue = l.ID_SITE.ToString();
        drop_SelectedIndexChanged(null, null);
        ddl_destinoZona.SelectedValue = l.ID_ZONA.ToString();
        ddl_destinoZona_SelectedIndexChanged(null, null);
        ddl_destinoPlaya.SelectedValue = l.ID_PLAYA.ToString();
        ddl_destinoPlaya_SelectedIndexChanged(null, null);
        ddl_destinoPos.SelectedValue = l.ID.ToString();
    }
    #endregion
    #region GridView
    protected void gv_Seleccionados_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.TableSection = TableRowSection.TableFooter;
        }
    }
    protected void gv_seleccionados_rowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument.ToString());
        table.Rows[index].Delete();
        this.gv_Seleccionados.DataSource = table;
        this.gv_Seleccionados.DataBind();
        if (table.Rows.Count > 0)
        {
            ddl_destinoZona.Enabled = false;
            ddl_destinoPlaya.Enabled = false;
        }
        else
        {
            ddl_destinoZona.Enabled = true;
            ddl_destinoZona_SelectedIndexChanged(null, null);
        }
    }
    #endregion
}
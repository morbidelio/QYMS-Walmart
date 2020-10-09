using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class App_Posicion : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();
    CargaDrops drops = new CargaDrops();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            drops.Site(ddl_buscarSite);
            drops.Site(ddl_editSite);
            ddl_buscarSite_onChange(null, null);
            ObtenerPosicion(true);
        }
        else
        { ObtenerPosicion(false); }
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
        ObtenerPosicion(false);
    }

    protected void gv_listaPosicion_RowDataBound(object sender, GridViewRowEventArgs e)
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
    }

    protected void gv_listaPosicion_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerPosicion(false);
    }

    protected void gv_listaPosicion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        LugarBC lugar = new LugarBC();
        if (e.CommandName == "ELIMINAR")
        {
            hf_idPosicion.Value = e.CommandArgument.ToString();
            lugar = lugar.obtenerXID(int.Parse(hf_idPosicion.Value));
            lblRazonEliminacion.Text = "Eliminar Posicion";
            msjEliminacion.Text = "Se eliminará la posición seleccionada, ¿desea continuar?";
            btnModalEliminar.Attributes.Remove("onclick");
            btnModalEliminar.Attributes.Add("onclick", "eliminarPosicion();");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalConfirmacion();", true);
        }
        if (e.CommandName == "HABILITAR")
        {
            hf_idPosicion.Value = e.CommandArgument.ToString();
            lugar = lugar.obtenerXID(int.Parse(hf_idPosicion.Value));
            lblRazonEliminacion.Text = "Habilitar/Deshabilitar Posicion";
            msjEliminacion.Text = "Se habilitará/deshabilitará la posición seleccionada, ¿desea continuar?";
            btnModalEliminar.Attributes.Remove("onclick");
            btnModalEliminar.Attributes.Add("onclick", "habilitarPosicion();");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalConfirmacion();", true);
        }
    }

    protected void gv_listaPosicion_RowEditing(object sender, GridViewEditEventArgs e)
    {
        LugarBC lugar = new LugarBC();
        gv_listar.SelectedIndex = e.NewEditIndex;
        hf_idPosicion.Value = this.gv_listar.SelectedDataKey.Value.ToString();

        lugar = lugar.obtenerXID(int.Parse(this.gv_listar.SelectedDataKey.Value.ToString()));
        llenarForm(lugar);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarPosicion();", true);
    }

    protected void gv_listaPosicion_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            gv_listar.PageIndex = e.NewPageIndex;
        }
        ObtenerPosicion(false);
    }

    #endregion

    #region Buttons

    protected void btn_habilitarPosicion_Click(object sender, EventArgs e)
    {
        LugarBC lugar = new LugarBC();
        string mensaje = "";
        if (lugar.Habilitar(int.Parse(hf_idPosicion.Value), out mensaje))
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Posicion habilitado/deshabilitado');", true);
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('"+mensaje+"');", true);
        ObtenerPosicion(true);
        btn_buscarPosicion_Click(null, null);
    }

    protected void btn_nuevoPosicion_Click(object sender, EventArgs e)
    {
        limpiarForm();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarPosicion();", true);
    }

    protected void btn_buscarPosicion_Click(object sender, EventArgs e)
    {
        string filtros = "";
        bool primero = true;
        if (!string.IsNullOrEmpty(txt_buscarCodigo.Text))
        {
            filtros += "CODIGO LIKE '%" + txt_buscarCodigo.Text + "%' ";
            primero = false;
        }

        if (!string.IsNullOrEmpty(txt_buscarNombre.Text))
        {
            if (!primero)
                filtros += "AND ";
            filtros += "descripcion LIKE '%" + txt_buscarNombre.Text + "%' ";
            primero = false;
        }

        if (ddl_buscarSite.SelectedIndex > 0)
        {
            if (!primero)
                filtros += "AND ";
            filtros += "SITE_ID = " + ddl_buscarSite.SelectedValue + " ";
            primero = false;
        }

        if (chk_buscarOcupado.Checked)
        {
            if (!primero)
                filtros += "AND ";
            filtros += "OCUPADO = 0 ";
            primero = false;
        }

        if (ddl_zona.SelectedIndex > 0)
        {
            if (!primero)
                filtros += "AND ";
            filtros += "ZONA_ID = " + ddl_zona.SelectedValue;
            primero = false;
        }

        if (ddl_playa.SelectedIndex > 0)
        {
            if (!primero)
                filtros += "AND ";
            filtros += "PLAY_ID = " + ddl_playa.SelectedValue;
        }
        if (string.IsNullOrEmpty(filtros))
            ObtenerPosicion(true);
        else
        {
            DataView dw = new DataView((DataTable)ViewState["lista"]);
            dw.RowFilter = filtros;
            ViewState["filtrados"] = dw.ToTable();
            ObtenerPosicion(false);
        }
    }

    protected void btn_editGrabar_Click(object sender, EventArgs e)
    {
        LugarBC lugar = llenarPosicion();
        LugarBC lugar2= new LugarBC();

        
        if (hf_idPosicion.Value == "" )
        {

            if (lugar2.obtenerXParametro(lugar.CODIGO, "", lugar.ID_SITE, false, 0, 0).Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Posicion ya existe en el site');", true);
            }
            else if (lugar.Crear(lugar)  )
            {
                ObtenerPosicion(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Posicion creado exitosamente');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalPosicion');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al agregar lugar. Intente nuevamente.');", true);
        }
        else
        {
            lugar.ID = int.Parse(hf_idPosicion.Value);
            string mensaje="";
            if (lugar.Modificar(lugar, out mensaje ))
            {
                ObtenerPosicion(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Posicion modificada exitosamente');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalPosicion');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('"+mensaje+"');", true);
        }
    }

    protected void btn_EliminarPosicion_Click(object sender, EventArgs e)
    {
        LugarBC lugar = new LugarBC();
        if (lugar.Eliminar(int.Parse(hf_idPosicion.Value)))
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Posicion eliminado exitosamente');", true);
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al eliminar lugar. Revise si tiene otros datos asociados');", true);
        ObtenerPosicion(true);
    }

    #endregion

    #region DropDownList

    public void ddl_buscarSite_onChange(object sender, EventArgs e)
    {
        if (ddl_buscarSite.SelectedIndex > 0)
        {
            drops.Zona(ddl_zona, int.Parse(ddl_buscarSite.SelectedValue));
            ddl_zona_onChange(null, null);
            if (ddl_zona.Items.Count > 1)
                ddl_zona.Enabled = true;
            else
            {
                ddl_zona.Enabled = false;
                ddl_playa.Enabled = false;
                ddl_zona.ClearSelection();
                ddl_playa.ClearSelection();
            }
        }
        else
        {
            ddl_zona.Enabled = false;
            ddl_playa.Enabled = false;
            ddl_zona.ClearSelection();
            ddl_playa.ClearSelection();
        }

    }

    public void ddl_zona_onChange(object sender, EventArgs e)
    {
        if (ddl_zona.SelectedIndex > 0)
        {
            int zona_id = int.Parse(ddl_zona.SelectedValue);
            drops.Playa(ddl_playa, zona_id);
            if (ddl_zona.Items.Count > 1)
                ddl_playa.Enabled = true;
            else
            {
                ddl_playa.Enabled = false;
                ddl_playa.ClearSelection();
            }
        }
        else
        {
            ddl_playa.Enabled = false;
            ddl_playa.ClearSelection();
        }
    }

    public void ddl_editZona_onChange(object sender, EventArgs e)
    {
        //PlayaBC playa = new PlayaBC();
        int zona_id = int.Parse(ddl_editZona.SelectedValue);
        if (zona_id > 0)
        {
            drops.Playa(ddl_editPlaya, zona_id);
            //utils.CargaDrop(ddl_editPlaya, "ID", "DESCRIPCION", playa.ObtenerXZona(zona_id));
            if (ddl_editPlaya.Items.Count > 1)
                ddl_editPlaya.Enabled = true;
            else
                ddl_editPlaya.Enabled = false;
        }
        else
            ddl_editPlaya.Enabled = false;
    }

    public void ddl_editSite_onChange(object sender, EventArgs e)
    {
        //ZonaBC zona = new ZonaBC();
        int site_id = int.Parse(ddl_editSite.SelectedValue);
        drops.Zona(ddl_editZona, site_id);
        //utils.CargaDrop(ddl_editZona, "ID", "DESCRIPCION", zona.ObtenerXSite(site_id));
        if (ddl_editZona.Items.Count > 1)
        {
            ddl_editZona.Enabled = true;
            ddl_editZona_onChange(null, null);
        }
        else
        {
            ddl_editPlaya.ClearSelection();
            ddl_editZona.ClearSelection();
            ddl_editPlaya.Enabled = false;
            ddl_editZona.Enabled = false;
        }
    }

    #endregion

    #region UtilsPagina

    protected void ObtenerPosicion(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            LugarBC lugar = new LugarBC();
            DataTable dt = lugar.ObtenerTodos1(-1,-1);
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

    private LugarBC llenarPosicion()
    {
        LugarBC lugar = new LugarBC();
        lugar.CODIGO = txt_editCodigo.Text;
        lugar.DESCRIPCION = txt_editDesc.Text;
        lugar.ID_SITE = int.Parse(ddl_editSite.SelectedValue);
        //lugar.LUGAR_X = double.Parse(txt_editPosX.Text);
        //lugar.LUGAR_Y = double.Parse(txt_editPosY.Text);
        //lugar.POSICION = double.Parse(txt_editPosicion.Text);
        //lugar.ORDEN = int.Parse(txt_editOrden.Text);
        //lugar.ANCHO = double.Parse(txt_editAncho.Text);
        //lugar.ALTO = double.Parse(txt_editAlto.Text);
        //lugar.ROTACION = int.Parse(txt_editRotacion.Text);
        lugar.ID_PLAYA = int.Parse(ddl_editPlaya.SelectedValue);
        lugar.ID_ZONA = int.Parse(ddl_editZona.SelectedValue);
        return lugar;
    }

    private void limpiarForm()
    {
        hf_idPosicion.Value = "";
        txt_editCodigo.Text = "";
        txt_editDesc.Text = "";
        ddl_editSite.ClearSelection();
        ddl_editZona.ClearSelection();
        ddl_editZona.Enabled = false;
        ddl_editPlaya.ClearSelection();
        ddl_editPlaya.Enabled = false;
        //txt_editPosX.Text = "";
        //txt_editPosY.Text = "";
        //txt_editPosicion.Text = "";
        //txt_editOrden.Text = "";
        //txt_editAncho.Text = "";
        //txt_editAlto.Text = "";
        //txt_editRotacion.Text = "";
    }

    private void llenarForm(LugarBC lugar)
    {
        txt_editCodigo.Text = lugar.CODIGO.ToString();
        txt_editDesc.Text = lugar.DESCRIPCION;
        ddl_editSite.SelectedValue = lugar.ID_SITE.ToString();
        ddl_editSite_onChange(null, null);
        try
        {
            ddl_editZona.SelectedValue = lugar.ID_ZONA.ToString();
  ddl_editZona_onChange(null, null);
        ddl_editPlaya.SelectedValue = lugar.ID_PLAYA.ToString();

        }
        catch (Exception ex)
        { }
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
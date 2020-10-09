using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;

public partial class App_stockTrailer : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargaTipoBC ct = new CargaTipoBC();
            ShorteckBC sh = new ShorteckBC();
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            DataTable ds = yms.ObteneSites(((UsuarioBC)Session["Usuario"]).ID);
            utils.CargaDropNormal(this.DDL_SITE, "ID", "NOMBRE", ds);
            TrailerEstadoBC estadobc = new TrailerEstadoBC();
            CaractCargaBC car = new CaractCargaBC();
            utils.CargaDropTodos(DDL_disponibilidad, "ID", "NOMBRE", estadobc.ObtenerTodosSTOCK());
            utils.CargaDropTodos(this.ddl_capacidad, "ID", "DESCRIPCION", car.obtenerXTipo(0));
            utils.CargaDropTodos(this.ddl_tipocarga, "ID", "DESCRIPCION", ct.obtenerTodo());
            utils.CargaDropTodos(this.ddl_shortec, "SHOR_ID", "SHOR_DESC", sh.ObtenerTodos());
        }
    }

    #region GridView

    protected void GoPage(object sender, System.EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;
        if (int.TryParse((oIraPag.Text), out iNumPag) && iNumPag > 0 && iNumPag <= gv_listaTrailer.PageCount)
        {
            if (int.TryParse(oIraPag.Text, out iNumPag) && iNumPag > 0 && iNumPag <= gv_listaTrailer.PageCount)
            {
                gv_listaTrailer.PageIndex = iNumPag - 1;
            }
            else
            {
                gv_listaTrailer.PageIndex = 0;
            }
        }
        ObtenerTrailer(true);
    }

    public void gv_listaTrailer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager && (gv_listaTrailer.DataSource != null))
        {
            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumberOfPages");
            _TotalPags.Text = gv_listaTrailer.PageCount.ToString();

            //LLENA LA LISTA CON EL NUMERO DE PAGINAS
            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(gv_listaTrailer.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = Convert.ToString(gv_listaTrailer.PageIndex + 1);
        }
    }

    public void gv_listaTrailer_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerTrailer(false);
    }

    public void gv_listaTrailer_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            gv_listaTrailer.PageIndex = e.NewPageIndex;
        }
        ObtenerTrailer(false);
    }

    #endregion

    #region Buttons

    protected void btn_export_Click(object sender, EventArgs e)
    {
        DataView view = new DataView();
        view.Table = (DataTable)ViewState["lista"];

        if (view.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Exportar", "Exportar();", true);
        }
        else
        {
            string texto = "Debe cargar datos antes de exportar!";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('" + texto + "');", true);
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        DataView view = new DataView();
        view.Table = (DataTable)ViewState["lista"];
        GridView gv = new GridView();
        gv.DataSource = view;
        gv.DataBind();

        string fileName = "reporte_StockTrailer.xls";
        string Extension = ".xls";
        if (Extension == ".xls")
        {
            PrepareControlForExport(gv);
            HttpContext.Current.Response.Clear();
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
                        System.Web.UI.WebControls.Table table = new System.Web.UI.WebControls.Table();
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

    public void btn_buscarTrailer_Click(object sender, EventArgs e)
    {
        ObtenerTrailer(true);
    }

    #endregion

    #region UtilsPagina

    protected void ObtenerTrailer(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            TrailerBC trailer = new TrailerBC();
            //          DataTable dt = trailer.obtenerXParametro(txt_buscarNombre.Text, txt_buscarNro.Text, chk_buscarInterno.Checked, int.Parse(ddl_buscarTipo.SelectedValue), int.Parse(ddl_buscarTransportista.SelectedValue));
            //      ViewState["lista"] = dt;
            DataTable dt = trailer.obtener_listado_trailer(int.Parse(DDL_SITE.SelectedValue), int.Parse(DDL_disponibilidad.SelectedValue), CHK_bloqeuado.Checked, int.Parse(ddl_capacidad.SelectedValue), chk_plancha.Checked, CHK_Asignado.Checked, Convert.ToInt32(ddl_tipocarga.SelectedValue), ddl_shortec.SelectedValue);
            ViewState["lista"] = dt;

        }
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
            dw.Sort = (String)ViewState["sortExpresion"];
        this.gv_listaTrailer.DataSource = dw;
        this.gv_listaTrailer.DataBind();
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

}
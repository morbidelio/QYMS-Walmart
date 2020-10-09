using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class App_Shorteck : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    public void btn_buscar_Click(object sender, EventArgs e)
    {
        bool primero = true;
        string query = "";
        if (!string.IsNullOrEmpty(txt_buscarCod.Text))
        {
            query += "SHOR_ID LIKE '%" + txt_buscarCod.Text + "%' ";
            primero = false;
        }
        if (!string.IsNullOrEmpty(txt_buscarDesc.Text))
        {
            if (!primero)
                query += "AND ";
            query += "SHOR_DESC LIKE '%" + txt_buscarDesc.Text + "%'";
        }
        if (string.IsNullOrEmpty(query))
            ObtenerShorteck(true);
        else
        {
            DataView dw = new DataView((DataTable)ViewState["lista"]);
            dw.RowFilter = query;
            ViewState["filtrados"] = dw.ToTable();
            ObtenerShorteck(false);
        }
    }

    public void btnModalEliminar_Click(object sender, EventArgs e)
    {
        ShorteckBC s = new ShorteckBC();
        if (s.Eliminar(hf_idShorteck.Value))
        {
            ObtenerShorteck(true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Todo OK');", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalConfirmacion');", true);
        }
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Error');", true);
    }

    public void btn_guardar_Click(object sender, EventArgs e)
    {
        ShorteckBC s = new ShorteckBC();
        s.ID = txt_editCodigo.Text;
        s.DESCRIPCION = txt_editDescripcion.Text;
        if (s.AgregarModificar())
        {
            ObtenerShorteck(true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Todo OK');", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalShorteck');", true);
        }
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Error');", true);
    }

    public void btn_nuevo_Click(object sender, EventArgs e)
    {
        txt_editCodigo.Text = "";
        txt_editCodigo.Enabled = true;
        txt_editDescripcion.Text = "";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalShorteck();", true);
    }

    public void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
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
            if (gv_listar.PageIndex == 0)
            {
                Button b1 = (Button)e.Row.FindControl("Button1");
                Button b4 = (Button)e.Row.FindControl("Button4");
                b1.Visible = false;
                b4.Visible = false;
            }

            if (gv_listar.PageIndex + 1 == gv_listar.PageCount)
            {
                Button b2 = (Button)e.Row.FindControl("Button2");
                Button b3 = (Button)e.Row.FindControl("Button3");
                b2.Visible = false;
                b3.Visible = false;
            }
             
        }
    }

    public void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerShorteck(false);
    }

    public void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITAR")
        {
            hf_idShorteck.Value = e.CommandArgument.ToString();
            ShorteckBC s = new ShorteckBC();
            s = s.ObtenerXId(hf_idShorteck.Value);
            txt_editCodigo.Text = s.ID;
            txt_editCodigo.Enabled = false;
            txt_editDescripcion.Text = s.DESCRIPCION;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalShorteck();", true);
        }
        if (e.CommandName == "ELIMINAR")
        {
            hf_idShorteck.Value = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalConfirmacion();", true);
        }
    }

    public void gv_listar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            gv_listar.PageIndex = e.NewPageIndex;
        }
        ObtenerShorteck(false);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ObtenerShorteck(true);
        }
    }

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
        ObtenerShorteck(false);
    }


    protected void ObtenerShorteck(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            ShorteckBC s = new ShorteckBC();
            ViewState["lista"] = s.ObtenerTodos();
            ViewState.Remove("filtrados");
        }
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        if (ViewState["filtrados"] == null)
            dw = new DataView((DataTable)ViewState["lista"]);
        else
            dw = new DataView((DataTable)ViewState["filtrados"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
            dw.Sort = (String)ViewState["sortExpresion"];
        this.gv_listar.DataSource = dw;
        this.gv_listar.DataBind();
    }

}
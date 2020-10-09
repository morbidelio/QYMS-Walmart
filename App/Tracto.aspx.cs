using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class App_Tracto : System.Web.UI.Page
{
    private static UtilsWeb utils = new UtilsWeb();
    private UsuarioBC user = new UsuarioBC();
    CargaDrops drop = new CargaDrops();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
            Response.Redirect("~/Inicio_QYMS2.aspx");
        user = (UsuarioBC)Session["usuario"];
        if (!IsPostBack)
        {
            drop.Transportista_Todos(ddl_buscarTrans);
            drop.Transportista(ddl_editTran);
            ObtenerTracto(true);
        }

    }

    #region GridView

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
    }

    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerTracto(false);
    }

    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITAR")
        {
            TractoBC t = new TractoBC();
            hf_id.Value = e.CommandArgument.ToString();
            t = t.ObtenerXId(int.Parse(hf_id.Value));
            txt_editPatente.Text = t.PATENTE;
            ddl_editTran.SelectedValue = t.TRAN_ID.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalTracto();", true);
        }
        if (e.CommandName == "ELIMINAR")
        {
            hf_id.Value = (e.CommandArgument.ToString());
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalConfirmar();", true);
        }
    }
    
    protected void gv_listar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            gv_listar.PageIndex = e.NewPageIndex;
        }
        ObtenerTracto(false);
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
        ObtenerTracto(false);
    }

    #endregion

    #region Botones

    protected void btn_eliminar_click(object sender, EventArgs e)
    {
        TractoBC t = new TractoBC();
        if (t.Eliminar(int.Parse(hf_id.Value)))
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Tracto eliminado exitosamente');", true);
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Ocurrió un error al eliminar Tracto');", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalConfirmar');", true);
        ObtenerTracto(true);
    }

    protected void btn_editGrabar_Click(object sender, EventArgs e)
    {
        string exito;
        TractoBC t = new TractoBC();
        t.PATENTE = txt_editPatente.Text;
        t.TRAN_ID = Convert.ToInt32(ddl_editTran.SelectedValue);
        if (hf_id.Value == "")
        {
            t.USUA_ID_CREACION = user.ID;
            if (t.Crear())
            {
                exito = "Todo OK!";
            }
            else
                exito = "Error!";
        }
        else
        {
            t.ID = int.Parse(hf_id.Value);
            if (t.Modificar(t))
                exito = "Todo OK!";
            else
                exito = "Error!";
        }
        ObtenerTracto(true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('" + exito + "');", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalTracto');", true);
    }

    public void btn_nuevo_Click(object sender, EventArgs e)
    {
        limpiarTodo();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalTracto();", true);
    }

    public void btn_buscar_click(object sender, EventArgs e)
    {
        string filtros = "";
        bool primero = true;

        if (!string.IsNullOrEmpty(txt_buscarPlaca.Text))
        {
            filtros += "PATENTE LIKE '%" + txt_buscarPlaca.Text + "%' ";
        }
        if (ddl_buscarTrans.SelectedIndex > 0)
        {
            filtros += "TRAN_ID = " + ddl_buscarTrans.SelectedValue;
        }
        if (string.IsNullOrEmpty(filtros))
            ObtenerTracto(true);
        else
        {
            DataView dw = new DataView((DataTable)ViewState["lista"]);
            dw.RowFilter = filtros;
            ViewState["filtrados"] = dw.ToTable();
            ObtenerTracto(false);
        }
    }

    #endregion

    #region FuncionesPagina

    private void limpiarTodo()
    {
        hf_id.Value = "";
        txt_editPatente.Text = "";
    }

    private bool validarRut(string rut)
    {
        bool validacion = false;
        try
        {
            rut = rut.ToUpper();
            rut = rut.Replace(".", "");
            rut = rut.Replace("-", "");
            int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

            char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

            int m = 0, s = 1;
            for (; rutAux != 0; rutAux /= 10)
            {
                s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
            }
            if (dv == (char)(s != 0 ? s + 47 : 75))
            {
                validacion = true;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
        return validacion;
    }

    private void ObtenerTracto(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            TractoBC t = new TractoBC();
            DataTable dt = t.ObtenerTodos();
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
  
}
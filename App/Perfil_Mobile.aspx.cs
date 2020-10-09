using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Perfil_Mobile : System.Web.UI.Page
{
    static FuncionesGenerales funcion = new FuncionesGenerales();
    static UtilsWeb utils = new UtilsWeb();
    UsuarioBC user = new UsuarioBC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {
            Response.Redirect("~/InicioQYMS2.aspx");
        }
        user = (UsuarioBC)Session["usuario"];
        if (!IsPostBack)
        {
            this.txt_buscarNombre.Focus();
            ObtenerPerfiles(true);
            MenuBC menu = new MenuBC();
            literarlMnu.Text = crearCheckBoxMenu(menu.ObtenerTodo(true));
            hf_checkMenus.Value = "";
        }
    }
    #region Botones
    protected void btn_grabarPerfil_Click(object sender, EventArgs e)
    {
        PerfilBC perfil = new PerfilBC();
        perfil.NOMBRE = txt_nombreEdita.Text;
        perfil.DESCRIPCION = txt_descripcionEdita.Text;
        perfil.MENU = this.hf_checkMenus.Value;
        perfil.MOBILE = true;
        perfil.NIVEL_PERMISOS = Convert.ToInt32(txt_editPermisos.Text);
        if (perfil.MENU != "")
        {
            if (this.hf_id.Value == "")
            {
                if (perfil.Ingresa(perfil))
                {
                    ObtenerPerfiles(true);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Perfil creado con éxito!');", true);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalPerfil');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Error!');", true);
                }
            }
            else
            {
                perfil.ID = int.Parse(hf_id.Value);
                if (perfil.Modifica(perfil))
                {
                    ObtenerPerfiles(true);
                    string texto = "Perfil modificado con éxito!";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('" + texto + "');", true);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalPerfil');", true);
                }
                else
                {
                    string texto = "Ha ocurrido un problema al intentar modificar el perfil!";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('" + texto + "');", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('No hay módulos seleccionados');", true);
        }
        hf_checkMenus.Value = "";
        hf_id.Value = "";
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txt_buscarNombre.Text))
        {
            DataView dw = new DataView((DataTable)ViewState["lista"]);
            dw.RowFilter = "NOMBRE LIKE '%" + txt_buscarNombre.Text + "%'";
            ViewState["filtrados"] = dw.ToTable();
            ObtenerPerfiles(false);
        }
        else
            ObtenerPerfiles(true);
    }
    protected void btn_Eliminar_click(object sender, EventArgs e)
    {
        PerfilBC perfil = new PerfilBC();
        if (perfil.Elimina(int.Parse(hf_id.Value)))
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Todo OK');", true);
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Error!');", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalEliminar');", true);
        ObtenerPerfiles(true);
    }
    protected void btn_nuevoPerfil_Click(object sender, EventArgs e)
    {
        Limpiar();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalPerfil();", true);
    }
    #endregion
    #region Grilla
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
        ObtenerPerfiles(false);
    }
    protected void gv_listaPerfiles_RowEditing(object sender, GridViewEditEventArgs e)
    {
        PerfilBC perfil = new PerfilBC();
        gv_listar.SelectedIndex = e.NewEditIndex;
        int id = int.Parse(this.gv_listar.SelectedDataKey.Value.ToString());
        perfil = perfil.ObtenerXId(id, true);
        if (perfil.ID > 0)
        {
            this.hf_id.Value = perfil.ID.ToString();
            this.txt_nombreEdita.Text = perfil.NOMBRE;
            this.txt_descripcionEdita.Text = perfil.DESCRIPCION;
            this.txt_editPermisos.Text = perfil.NIVEL_PERMISOS.ToString();
            this.hf_checkMenus.Value = perfil.MENU;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "checkSorting", "checkSorting();", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalPerfil();", true);
        }
        else
        {
            string texto = "No se encontró el perfil seleccionado!";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('" + texto + "');", true);
        }
    }
    protected void gv_listaPerfiles_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ELIMINAR")
        {
            hf_id.Value = e.CommandArgument.ToString();
            lblRazonEliminacion.Text = "Eliminar Perfil";
            msjEliminacion.Text = "Se eliminará el perfil seleccionado, ¿desea continuar?";
            btnModalEliminar.Attributes.Remove("onclick");
            btnModalEliminar.Attributes.Add("onclick", "eliminarPerfil();");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "confirmar", "modalConfirmar();", true);
        }
    }
    protected void gv_listaPerfiles_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerPerfiles(false);
    }
    protected void gv_listaPerfiles_RowDataBound(object sender, GridViewRowEventArgs e)
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
            LinkButton btn_modificar = (LinkButton)e.Row.FindControl("btn_modificar");
            LinkButton btn_eliminar = (LinkButton)e.Row.FindControl("btn_eliminar");
            if (DataBinder.Eval(e.Row.DataItem, "NIVEL_PERMISOS") != DBNull.Value)
            {
                int nivel = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "NIVEL_PERMISOS"));
                int id = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ID"));
                if (user.NIVEL_PERMISOS >= nivel && user.ID_TIPO != id)
                {
                    btn_modificar.Visible = false;
                    btn_eliminar.Visible = false;
                }
            }
        }
    }
    protected void gv_listaPerfiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            gv_listar.PageIndex = e.NewPageIndex;
        }
        ObtenerPerfiles(false);
    }
    #endregion
    #region FuncionesPagina
    private void Limpiar()
    {
        hf_checkMenus.Value = "";
        hf_id.Value = "";
        txt_nombreEdita.Text = "";
        txt_descripcionEdita.Text = "";
        txt_editPermisos.Text = "";
    }
    private void ObtenerPerfiles(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            PerfilBC perfil = new PerfilBC();
            DataTable dt = perfil.ObtenerTodo(true);
            ViewState["lista"] = dt;
            ViewState.Remove("filtrados");
        }
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        if (ViewState["filtrados"] == null)
            dw = new DataView((DataTable)ViewState["lista"]);
        else
            dw = new DataView((DataTable)ViewState["filtrados"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)ViewState["sortExpresion"];
        }
        this.gv_listar.DataSource = dw;
        this.gv_listar.DataBind();
    }
    internal string crearCheckBoxMenu(DataTable dataTable)
    {
        System.Text.StringBuilder strb = new System.Text.StringBuilder();
        strb.Append("<table width='100px'>");
        foreach (DataRow row in dataTable.Rows)
        {
            int orden = Convert.ToInt32(row["ORDEN"].ToString());
            String url = row["URL"].ToString();
            if (url == "")
                url = "#";
            strb.Append("<tr><td><input type='checkbox' onclick='check(this)' value='").
                Append(row["ORDEN"].ToString()).
                Append("' ").
                Append(" name='menu' id='menu_").
                Append(row["ID"].ToString() + "'></input></td>").
                Append("<td  valign='middle'><B>").
                Append(row["TITULO"].ToString()).
                Append("</B><input type='hidden' id='orden_").
                Append(orden.ToString()).
                Append("' value='").
                Append(row["ID"].ToString()).
                Append("' /></td></tr>");
            //else
            //{
            //    strb.Append("<tr><td>").
            //        Append("<input type='checkbox' name='menu' id='menu_").
            //        Append(row["ID"].ToString() + "' value='").
            //        Append(row["ORDEN"].ToString()).
            //        Append("' onclick='checkHijo(this)'></input></td>").
            //        Append("<td class='mnu' valign='middle'><B>").
            //        Append(row["TITULO"].ToString()).
            //        Append("</B><input type='hidden' id='orden_").
            //        Append(row["ORDEN"].ToString()).
            //        Append("' value='").
            //        Append(row["ID"].ToString()).
            //        Append("' /></td></tr>");
            //}
        }
        strb.Append("</table>");
        return strb.ToString();
    }
    #endregion
}
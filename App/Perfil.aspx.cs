using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class App_Perfil : System.Web.UI.Page
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
            literarlMnu.Text = crearCheckBoxMenu(menu.ObtenerTodo(false));
            hf_checkMenus.Value = "";
        }
    }
    #region Botones

    protected void btn_grabar_Click(object sender, EventArgs e)
    {
        int nivel = Convert.ToInt32(txt_perfilPermisos.Text);
        if (nivel >= user.NIVEL_PERMISOS)
        {
            if (!string.IsNullOrEmpty(this.hf_checkMenus.Value))
            {
                PerfilBC perfil = new PerfilBC();
                perfil.NOMBRE = txt_nombreEdita.Text;
                perfil.DESCRIPCION = txt_perfilDescripcion.Text;
                perfil.MENU = this.hf_checkMenus.Value;
                perfil.MOBILE = false;
                perfil.NIVEL_PERMISOS = nivel;
                string texto;
                if (this.hf_id.Value == "")
                {
                    if (perfil.Ingresa(perfil))
                    {
                        texto = "Perfil creado con éxito!";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalPerfil');", true);
                    }
                    else
                    {
                        texto = "Error!";
                    }
                }
                else
                {
                    perfil.ID = int.Parse(hf_id.Value);
                    if (perfil.Modifica(perfil))
                    {
                        texto = "Perfil modificado con éxito!";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalPerfil');", true);
                    }
                    else
                    {
                        texto = "Ha ocurrido un problema al intentar modificar el perfil!";
                    }
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('" + texto + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('No hay módulos seleccionados');", true);
            }
            ObtenerPerfiles(true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Nivel de permiso no autorizado.');", true);
        }
    }

    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txt_buscarNombre.Text))
        {
            DataView dw = new DataView((DataTable)ViewState["lista"]);
            dw.RowFilter = "NOMBRE LIKE '%" + txt_buscarNombre.Text + "%'";
            ViewState["filtrados"] = dw.ToTable();
            ObtenerPerfiles(false);
        }
        else
            ObtenerPerfiles(true);
    }

    protected void btn_eliminar_click(object sender, EventArgs e)
    {
        PerfilBC perfil = new PerfilBC();
        if (perfil.Elimina(int.Parse(hf_id.Value)))
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Todo OK');", true);
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Error!');", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalEliminar');", true);
        ObtenerPerfiles(true);
    }

    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        Limpiar();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalPerfil();", true);
    }

    #endregion
    #region Grilla
    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
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
        if (e.CommandName == "MODIFICAR")
        {
            hf_id.Value = e.CommandArgument.ToString();
            PerfilBC perfil = new PerfilBC().ObtenerXId(Convert.ToInt32(hf_id.Value));
            txt_nombreEdita.Text = perfil.NOMBRE;
            txt_perfilDescripcion.Text = perfil.DESCRIPCION;
            txt_perfilPermisos.Text = perfil.NIVEL_PERMISOS.ToString();
            hf_checkMenus.Value = perfil.MENU;
            txt_perfilPermisos.Text = perfil.NIVEL_PERMISOS.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "checkSorting", "checkSorting();modalPerfil();", true);
        }
    }
    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerPerfiles(false);
    }
    protected void gv_listar_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
        txt_perfilDescripcion.Text = "";
        txt_perfilPermisos.Text = "";
    }

    private void ObtenerPerfiles(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            PerfilBC perfil = new PerfilBC();
            DataTable dt = perfil.ObtenerTodo(false);
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
        strb.Append("<table style='width:100px;font-size:10px;'><tr><td valign='top'>");
        bool swInicioEncabezado = false;
        foreach (DataRow row in dataTable.Rows)
        {
            int orden = Convert.ToInt32(row["ORDEN"].ToString());
            if (orden % 100 == 0)
            {
                if (swInicioEncabezado)
                {
                    strb.Append("</table></td><td valign='top'>");
                    swInicioEncabezado = false;
                }
                String url = row["URL"].ToString();
                if (url == "")
                    url = "#";
                strb.Append("<table border='0' width='100px'><tr><td><input type='checkbox' value='").Append(row["ORDEN"].ToString()).
                Append("' disabled='true'");
                if (row["ORDEN"].ToString() == "0")
                    strb.Append("checked='true'");
                strb.Append(" name='menu' id='menu_").Append(row["ID"].ToString() + "'></input></td>").Append("<td style='font-size:10px;' valign='bottom'><B>").Append(row["TITULO"].ToString()).Append("</B><input type='hidden' id='orden_").Append(row["ORDEN"].ToString()).Append("' value='").Append(row["ID"].ToString()).
                Append("' /></td></tr>");
                swInicioEncabezado = true;
            }
            else
            {
                strb.Append("<tr><td>").Append("<input type='checkbox' name='menu' id='menu_").Append(row["ID"].ToString() + "' value='").Append(row["ORDEN"].ToString()).Append("' onclick='checkHijo(this)'></input></td>").Append("<td style='font-size:10px;' class='mnu' valign='bottom'><B>").Append(row["TITULO"].ToString()).Append("</B><input type='hidden' id='orden_").Append(row["ORDEN"].ToString()).Append("' value='").Append(row["ID"].ToString()).
                Append("' /></td></tr>");
            }
        }
        strb.Append("</table></td></tr></table>");
        return strb.ToString();
    }
    #endregion
}
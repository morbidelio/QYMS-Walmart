using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.Services;
using System.Web.Script.Services;
public class menuitem2
{
    public string ID { get; set; }

    public string Nombre { get; set; }
    public string icono { get; set; }
    public List<menuitem2> subitems { get; set; }
}


public partial class App_YMS_editorb : System.Web.UI.Page
{
    public int maxzoom = 16;
    public string url = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/');

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static menuitem2[] GetMenu(string usuario)
    {
        if (HttpContext.Current.Session["Counter"] == null)
        {
            YMS_ZONA_BC yms = new YMS_ZONA_BC();

            List<menuitem2> menuprincipal = new List<menuitem2>();
            menuprincipal.Add(new menuitem2() { ID = "0", Nombre = "Enviar a", icono = "2" });



            List<menuitem2> lst = new List<menuitem2>();
            DataTable ds = yms.Obtenermenu_lugar(Convert.ToInt32(usuario.Replace("lug_", "")));

            for (int value = 0; value <= ds.Rows.Count - 1; value++)

                lst.Add(new menuitem2() { ID = ds.Rows[value]["id"].ToString(), Nombre = ds.Rows[value]["nombre"].ToString(), icono = ds.Rows[value]["icono"].ToString() });
            menuprincipal[0].subitems = lst;

            return menuprincipal.ToArray();
        }
        else
            return null;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public static menuitem2[] graba(string usuario, string supl_id, string x, string y, string ancho, string alto, string orientacion)
    {
        if (HttpContext.Current.Session["Counter"] == null)
        {
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            yms.guarda_playa(Convert.ToInt32(usuario.Replace("play_", "")),  double.Parse(x.Replace(".", ",")), double.Parse(y.Replace(".", ",")), double.Parse(ancho.Replace(".", ",")), double.Parse(alto.Replace(".", ",")), Convert.ToInt32(orientacion));

            return null;
        }
        else
            return null;
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 3600;
        Server.ScriptTimeout = 36000;

        // agregazoom(1)
        // agregazoom(2)
        // agregazoom(3)
        // agregazoom(4)
        if (IsPostBack == false)
        {
            cargasites();
            cargaYMS();
        }

        // llenazona2()
        // llenazona3()


        string pagina;



        // Dim list As IList(Of imagen)
        // list = New List(Of imagen)
        // Dim Imagen As New imagen()
        // Imagen.ImageID = 1
        // Imagen.Title = "1"
        // Imagen.ImageSRC = "./images/01-login-qmgps-completo.jpg"
        // Imagen.ThumbImageSRC = "./images/01-login-qmgps-completo.jpg"
        // list.Add(Imagen)

        // Dim Imagen2 As New imagen()
        // Imagen2.ImageID = 2
        // Imagen2.Title = "2"
        // Imagen2.ImageSRC = "./images/01-login-qmgps-completo.jpg"
        // Imagen2.ThumbImageSRC = "./images/01-login-qmgps-completo.jpg"
        // list.Add(Imagen2)




        // Repeater1.DataSource = list
        // Repeater1.DataBind()


        if (IsPostBack)
        {
        }
    }



    protected void drop_SelectedIndexChanged(object sender, EventArgs e)
    {
        large.Controls.Clear();
        rlplayas.Items.Clear();
        cargaYMS();
    }

    protected void recagar(object sender, EventArgs e)
    {
        large.Controls.Clear();
        cargaYMS();
    }



    public void llenaPlaya(DataRow playa)
    {
        double top;
        double left;
        double width;
        double height;
        int desfasey;
        int desfasex;
        desfasey = 0;
        desfasex = 0;
        top = double.Parse(playa["Playa_Y"].ToString());
        left = double.Parse(playa["Playa_X"].ToString());
        width = double.Parse(playa["width"].ToString());
        height = double.Parse(playa["height"].ToString());

        Panel zona1l = new Panel();
        large.Controls.Add(zona1l);
        zona1l.ClientIDMode = ClientIDMode.Static;
        zona1l.Style["width"] = width.ToString().Replace(",", ".") + "%";
        zona1l.Style["height"] = height.ToString().Replace(",", ".") + "%";
        zona1l.ID = "play_" + playa["id"].ToString();
       // zona1l.Attributes.Add("SUPL_ID", playa["supl_id"].ToString());
        zona1l.Attributes.Add("rotacion", playa["rotacion"].ToString());
        zona1l.CssClass = "zona";

        zona1l.Style["top"] = (top + desfasey).ToString().Replace(",", ".") + "%";
        zona1l.Style["left"] = (left + desfasex).ToString().Replace(",", ".") + "%";

        // 
        // zona1l.Style.Item("top") = 50.ToString + "%"
        // zona1l.Style.Item("left") = 50.ToString + "%"
        int lugares = Convert.ToInt32(playa["lugares"].ToString());

        YMS_ZONA_BC yms = new YMS_ZONA_BC();
        DataTable ds = yms.Obtenerlugares_playa(Convert.ToInt32(playa["id"].ToString()), null/* TODO Change to default(_) if this is not a reference type */, null/* TODO Change to default(_) if this is not a reference type */);
        zona1l.Attributes.Add("playa", playa["CODIGO"].ToString());
        zona1l.Attributes.Add("lugares", lugares.ToString());

        for (int value = 0; value <= lugares - 1; value++)
        {

            Control anterior = zona1l.FindControl("lug_" + ds.Rows[value]["id"].ToString());
            if (anterior != null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alerta" + ds.Rows[value]["CODIGO"].ToString(), "console.log('" + ds.Rows[value]["CODIGO"] + " repetido: " + ds.Rows[value]["trai_placa"].ToString() + ", " + ((Image)(anterior)).Attributes["patente"] + " ' );", true);

                ((Image)anterior).Attributes.Add("repetido", "si");
                anterior = null;
            }
            else
            {


                Panel celda2 = new Panel();
                celda2.ClientIDMode = ClientIDMode.Static;
                Image imagen2 = new Image();
                imagen2.ID = "lug_" + ds.Rows[value]["id"].ToString();
                imagen2.ClientIDMode = ClientIDMode.Static;
                // celda2.Style.Add("display", "inline-grid")
                // celda2.CssClass = "icono1"
                // celda2.Width = ds.Rows(value)("ancho").ToString()
                // celda2.Height = ds.Rows(value)("alto").ToString()

                Label texto_trailer = new Label();
                texto_trailer.CssClass = "patente_" + ds.Rows[value]["rotacion"].ToString();
                texto_trailer.Text = ds.Rows[value]["trai_id"].ToString();
                // celda2.Controls.Add(texto_trailer)
                Image pallet = new Image();
                Image ottawa = new Image();
                // Dim tabla As New Table
                // tabla.Style.Add("border", "1px solid white")

                // tabla.CssClass = "tabla_lugar"
                imagen2.Attributes.Add("rotacion", ds.Rows[value]["rotacion"].ToString());

                if (ds.Rows[value]["trai_id"].ToString() == "0")
                {
                    imagen2.Attributes.Add("patente", "vacio");
                    imagen2.ImageUrl = "../images/yms_estacionamiento_" + ds.Rows[value]["rotacion"].ToString() + ".png";
                    imagen2.Width = Unit.Percentage(double.Parse(ds.Rows[value]["ancho"].ToString())); // ;(ds.Rows(value)("ancho") - 2).ToString() + "%"
                    imagen2.Height = Unit.Percentage(double.Parse(ds.Rows[value]["alto"].ToString())); // ; (ds.Rows(value)("alto") - 2).ToString() + "%"
                    imagen2.CssClass = "context-menu-one icono masterTooltip lugar";
                    imagen2.Attributes.Add("title1", "Estacionamiento Vacío");
                    texto_trailer.Text = ""; // ds.Rows(value)("trai_id").ToString()
                }
                else
                {
                    imagen2.Attributes.Add("patente", ds.Rows[value]["trai_id"].ToString());
                    imagen2.ImageUrl = "../images/yms_trailer_" + ds.Rows[value]["rotacion"].ToString() + ".png";
                    imagen2.Width = Unit.Percentage(double.Parse(ds.Rows[value]["ancho"].ToString())); // (ds.Rows(value)("ancho") - 2).ToString() + "%"
                    imagen2.Height = Unit.Percentage(double.Parse(ds.Rows[value]["alto"].ToString())); // ; (ds.Rows(value)("alto") - 2).ToString() + "%"
                    imagen2.CssClass = "context-menu-one icono masterTooltip lugar";
                    imagen2.Attributes.Add("title1", "Doc Entrada:" + ds.Rows[value]["TRUE_DOC_INGRESO"].ToString());
                    texto_trailer.Text = ds.Rows[value]["trai_id"].ToString();
                }

                imagen2.Attributes.Add("playa", ds.Rows[value]["PLAY_COD"].ToString());
                imagen2.Attributes.Add("posicion", ds.Rows[value]["orden"].ToString());
                imagen2.Attributes.Add("codigo_lugar", ds.Rows[value]["CODIGO"].ToString());
                imagen2.Attributes.Add("trai_id", ds.Rows[value]["TRAI_ID"].ToString());



                // prueba tabla
                if ((ds.Rows[value]["rotacion"].ToString() == "2") | (ds.Rows[value]["rotacion"].ToString() == "4"))
                {
                    // Dim fila As New TableRow
                    // Dim superior As New TableCell
                    // Dim celdaimages As New TableCell
                    // Dim inferios As New TableCell
                    // fila.Cells.Add(superior)
                    // superior.Controls.Add(ottawa)
                    // fila.Cells.Add(celdaimages)
                    // celdaimages.Controls.Add(texto_trailer)
                    // celdaimages.Controls.Add(imagen2)
                    // fila.Cells.Add(inferios)
                    // inferios.Controls.Add(pallet)
                    // tabla.Rows.Add(fila)
                    // celda2.CssClass = celda2.CssClass + " icono1_2"
                    // Dim topimagen As Double = ds.Rows(value)("LUGAR_y") '* value
                    // Dim leftimagen As Double = ds.Rows(value)("LUGAR_X") '* value

                    imagen2.Style["top"] = Unit.Percentage(double.Parse(ds.Rows[value]["LUGAR_y"].ToString())).ToString().Replace(",", "."); // topimagen.ToString() + "%"
                    imagen2.Style["left"] = Unit.Percentage(double.Parse(ds.Rows[value]["LUGAR_X"].ToString())).ToString().Replace(",", "."); // (leftimagen).ToString() + "%"
                }
                else if ((ds.Rows[value]["rotacion"].ToString() == "1") | (ds.Rows[value]["rotacion"].ToString() == "3"))
                {

                    // Dim fila1 As New TableRow
                    // Dim superior As New TableCell
                    // Dim fila2 As New TableRow
                    // Dim celdaimages As New TableCell
                    // Dim fila3 As New TableRow
                    // Dim inferios As New TableCell
                    // fila1.Cells.Add(superior)
                    // fila2.Cells.Add(celdaimages)
                    // fila3.Cells.Add(inferios)
                    // tabla.Rows.Add(fila1)
                    // tabla.Rows.Add(fila2)
                    // tabla.Rows.Add(fila3)
                    // celdaimages.Controls.Add(texto_trailer)
                    // superior.Controls.Add(ottawa)
                    // celdaimages.Controls.Add(imagen2)
                    // inferios.Controls.Add(pallet)
                    // Dim topimagen As Double = ds.Rows(value)("LUGAR_y") ' * value
                    // Dim leftimagen As Double = ds.Rows(value)("LUGAR_X") ' * value

                    imagen2.Style["top"] = Unit.Percentage(double.Parse(ds.Rows[value]["LUGAR_y"].ToString())).ToString().Replace(",", "."); // topimagen.ToString() + "%"
                    imagen2.Style["left"] = Unit.Percentage(double.Parse(ds.Rows[value]["LUGAR_X"].ToString())).ToString().Replace(",", "."); // (leftimagen).ToString() + "%"
                }



                // celda2.Controls.Add(tabla)


                celda2.Controls.Add(imagen2);

                if ((1 == 1))
                {
                    pallet.ImageUrl = "../images/yms_pallet_vacio.png";
                    pallet.Width = 3; // (ds.Rows(value)("ancho") - 2).ToString()
                    pallet.Height = 3; // (ds.Rows(value)("alto") - 2).ToString()
                }


                if ((ds.Rows[value]["ottawa"].ToString() != "0"))
                {
                    ottawa.ImageUrl = "../images/ottawa_" + ds.Rows[value]["rotacion"].ToString() + ".png";

                    celda2.Controls.Add(ottawa);
                }

                zona1l.Controls.Add(celda2);
            }
        }
    }

    // Sub llenazona3()

    // Dim top As Integer
    // Dim left As Integer
    // top = 330
    // left = 890
    // Dim desfasey As Integer
    // Dim desfasex As Integer
    // desfasey = 15
    // desfasex = 10

    // zona3l.Style.Item("top") = (top + desfasey).ToString + "px"
    // zona3l.Style.Item("left") = (left + desfasex).ToString + "px"

    // For value As Integer = 1 To 10
    // Dim celda As New Panel
    // Dim celda2 As New Panel
    // Dim imagen As New Image
    // celda.Width = 8
    // celda.Height = 25
    // celda2.CssClass = "icono"
    // '  celda.Style.Add("float", "left")
    // celda.Style.Add("display", "inline-grid")
    // celda2.Style.Add("display", "inline-grid")
    // celda2.Width = 8
    // celda2.Height = 25
    // imagen.ImageUrl = "images/yms_1.png"
    // imagen.CssClass = "context-menu-one"
    // celda.Controls.Add(imagen)

    // Dim imagen2 As New Image
    // imagen2.ImageUrl = "images/yms_1.png"
    // imagen2.CssClass = "context-menu-one icono"
    // celda2.Controls.Add(imagen2)
    // Me.zona3l.Controls.Add(celda2)
    // Next

    // End Sub

    // Sub llenazona2()

    // Dim top As Integer
    // Dim left As Integer
    // top = 330
    // left = 890
    // Dim desfasey As Integer
    // Dim desfasex As Integer
    // desfasey = 15
    // desfasex = 10

    // zona2l.Style.Item("top") = (top + desfasey).ToString + "px"
    // zona2l.Style.Item("left") = (left + desfasex).ToString + "px"

    // For value As Integer = 1 To 10
    // Dim celda As New Panel
    // Dim celda2 As New Panel
    // Dim imagen As New Image
    // celda.Width = 8
    // celda.Height = 25
    // celda2.CssClass = "icono"
    // '  celda.Style.Add("float", "left")
    // celda.Style.Add("display", "inline-grid")
    // celda2.Style.Add("display", "inline-grid")
    // celda2.Width = 8
    // celda2.Height = 25
    // imagen.ImageUrl = "images/yms_1.png"
    // imagen.CssClass = "context-menu-one icono"
    // celda.Controls.Add(imagen)

    // Dim imagen2 As New Image
    // imagen2.ImageUrl = "images/yms_1.png"
    // imagen2.CssClass = "context-menu-one"
    // celda2.Controls.Add(imagen2)
    // Me.zona2l.Controls.Add(celda2)
    // Next

    // End Sub



    // Sub carga_tracto_trailer2()

    // Dim transportista As String = ""

    // Try

    // transportista = drop_transportistas.SelectedValue
    // Catch ex As Exception
    // transportista = drop_transportistas.SelectedValue
    // End Try

    // Dim sql As String = "SP_INTEGRA_LISTAR_PATENTE_TRANSPORTE "


    // Dim conn_ As SqlConnection
    // Try
    // conn_ = New SqlConnection(HttpContext.Current.Session("usuario").connection_string.ToString())
    // Catch ex As Exception
    // conn_ = New SqlConnection(HttpContext.Current.Session("usuario").connection_string.ToString())
    // End Try

    // Dim comando As New SqlCommand(sql, conn_)
    // comando.CommandType = CommandType.StoredProcedure
    // comando.Parameters.AddWithValue("@TRANS_ID", transportista)

    // Dim ds As New DataTable
    // Dim lector As SqlDataReader

    // Try
    // conn_.Open()
    // lector = comando.ExecuteReader
    // ds.Load(lector)
    // conn_.Close()

    // drop_moviles.DataSource = ds
    // drop_moviles.DataTextField = "placa"
    // drop_moviles.DataValueField = "id_reg"
    // drop_moviles.DataBind()

    // Dim item As RadComboBoxItem
    // item = New RadComboBoxItem("Seleccione", "0")

    // drop_moviles.Items.Insert(0, item)

    // Catch ex As Exception

    // End Try
    // End Sub

    public void cargasites()
    {
        YMS_ZONA_BC yms = new YMS_ZONA_BC();
        DataTable ds = yms.ObteneSites(((UsuarioBC)Session["Usuario"]).ID);
        UtilsWeb utilidades = new UtilsWeb();
        utilidades.CargaDropNormal(dropsite, "ID", "NOMBRE", ds);
    }



    public void cargaYMS()
    {
        // aquí va el sp




        YMS_ZONA_BC yms = new YMS_ZONA_BC();
        var imagen = yms.ObteneSite(Convert.ToInt32(dropsite.SelectedValue)).Rows[0]["SITE_IMAGEN"];
        siteimage.ImageUrl = url + "/images/" + imagen;
        DataTable ds = yms.ObtenerPlayas_Site(Convert.ToInt32(dropsite.SelectedValue), null, null);

        try
        {
            int i = 0;
            while (i < ds.Rows.Count)
            {
                llenaPlaya(ds.Rows[i]);
                // If (IsPostBack = False) Then

                agregafiltroplaya(ds.Rows[i]);
                // End If
                i = i + 1;
            }
        }
        catch (Exception ex)
        {
        }
    }


    public void agregafiltroplaya(DataRow playa)
    {
        ListItem item = new ListItem(playa["CODIGO"].ToString(), "play_" + playa["id"].ToString());
        item.Selected = true;
        item.Attributes.Add("id", "chk_play_" + playa["id"].ToString());
        item.Attributes.Add("onclick", "chk_play_" + playa["id"].ToString());
        rlplayas.Items.Add(item);
    }

    //public string FechaAnsii(string Fecha)
    //{
    //    string xAño;
    //    string xMes;
    //    string xDia;

    //    xAño = DateTime.Year(Fecha).ToString();

    //    if (Strings.Len(DateTime.Month(Fecha).ToString()) == 1)
    //        xMes = "0" + DateTime.Month(Fecha).ToString();
    //    else
    //        xMes = DateTime .Month(Fecha).ToString();

    //    if (Strings.Len(DateTime.Day(Fecha).ToString()) == 1)
    //        xDia = "0" + DateTime.Day(Fecha).ToString();
    //    else
    //        xDia = DateTime.Day(Fecha).ToString();

    //    FechaAnsii = xAño + xMes + xDia;
    //}



    // Protected Sub drop_transportistas_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles drop_transportistas.SelectedIndexChanged
    // If drop_transportistas.SelectedValue = 0 Then
    // cargaMov()
    // ElseIf drop_transportistas.SelectedValue <> 0 Then
    // cargaMov2()
    // End If
    // End Sub

    private void MMensaje(string mensaje)
    {
        System.Text.StringBuilder scriptMsj = new System.Text.StringBuilder();
        scriptMsj.Append("<script language='javascript'>");
        scriptMsj.Append("alert('");
        scriptMsj.Append(mensaje);
        scriptMsj.Append("');</script>");
        this.ClientScript.RegisterStartupScript(this.GetType(), "MENSAJE", scriptMsj.ToString());
    }
}
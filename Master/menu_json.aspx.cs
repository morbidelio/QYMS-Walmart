using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Web;

public partial class Master_menu_json2 : System.Web.UI.Page
{
    FuncionesGenerales funciones = new FuncionesGenerales();
    UsuarioBC usuario = new UsuarioBC();
    protected void Page_Load(object sender, System.EventArgs e)
    {
       
    }

    public static DataSet retornoDS(string _Sql, string NombreTabla = "")
    {
        SqlConnection conn_ = new SqlConnection(ConfigurationManager.ConnectionStrings["CsString"].ConnectionString);
        SqlDataAdapter _adap;
        DataSet ds_ = new DataSet();
        _adap = new SqlDataAdapter(_Sql, conn_);
        try
        {
            _adap.SelectCommand.CommandTimeout = 300;
            _adap.Fill(ds_, NombreTabla);
        }
        catch (Exception ex) { }
        return ds_;
    }

    //public void json1(DataSet ds_)
    //{
    //    char c = (char)34;

    //    string outp = "";
    //    string id1, id_sub_menu, id_sub_sub_menu, link, icono, nombre, clase;


    //    try
    //    {
    //        foreach (DataRow row in ds_.Tables[0].Rows)
    //        {
    //            id1 = row["MENU_ID"].ToString();
    //            id_sub_menu = row["ID_PADRE"].ToString();
    //            link = row["LINK"].ToString();
    //            nombre = row["TITULO"].ToString();
    //            clase = row["CLASE"].ToString();
    //            DataBind();
    //            if (id_sub_menu == "")
    //            {
    //                outp = outp + "{" + c + "class" + c + ":" + c + clase + c + ",";
    //                outp = outp + c + "link" + c + ":" + c + link + c + ",";
    //                outp = outp + c + "texto" + c + ":" + c + nombre + c + ",";
    //                outp = outp + c + "id" + c + ":" + c + id1 + c + "},";
    //            }
    //        }
    //        outp = "{" + c + "records" + c + ":[" + outp.TrimEnd(',') + "],";
    //        Response.Write(outp);
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}

    //public void json2(DataSet ds_)
    //{
    //    char c = (char)34;

    //    string outp = "";
    //    string id1, id_sub_menu, id_sub_sub_menu, link, icono, nombre, clase;

    //    try
    //    {
    //        foreach (DataRow row in ds_.Tables[0].Rows)
    //        {
    //            id1 = row["MENU_ID"].ToString();
    //            id_sub_menu = row["ID_PADRE"].ToString();
    //            link = row["LINK"].ToString();
    //            nombre = row["TITULO"].ToString();
    //            clase = row["CLASE"].ToString();
    //            DataBind();
    //            if (id_sub_menu != "")
    //            {
    //                outp = outp + "{" + c + "class" + c + ":" + c + clase + c + ",";
    //                outp = outp + c + "link" + c + ":" + c + link + c + ",";
    //                outp = outp + c + "texto" + c + ":" + c + nombre + c + ",";
    //                outp = outp + c + "id2" + c + ":" + c + id1 + c + ",";
    //                outp = outp + c + "id" + c + ":" + c + id_sub_menu + c + "},";
    //            }
    //        }
    //        outp = "" + c + "records2" + c + ":[" + outp.TrimEnd(',') + "],";
    //        Response.Write(outp);
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //    finally
    //    {
    //    }
    //}

    //public void json3()
    //{
    //    string _Sql;

    //    _Sql = "EXEC dbo.QMGPS_MASTER_LISTAR_MENU " + System.Web.UI.Page.Session["usuario"].id + "";

    //    DataSet ds_ = new DataSet();
    //    ds_ = funciones.retornoDS(_Sql, "datos");

    //    string c = Strings.Chr(34);

    //    string outp = "";
    //    string id1, id_sub_menu, id_sub_sub_menu, link, icono, nombre, clase;


    //    try
    //    {
    //        foreach (DataRow row in ds_.Tables[0].Rows)
    //        {
    //            id1 = row["id"].ToString();
    //            id_sub_menu = row["id_sub_menu"].ToString();
    //            id_sub_sub_menu = row["id_sub_sub_menu"].ToString();
    //            link = row["link"].ToString();
    //            icono = row["icono"].ToString();
    //            nombre = row["nombre"].ToString();
    //            clase = row["class"].ToString();
    //            System.Web.UI.Control.DataBind();
    //            if (id_sub_menu == "" & id_sub_sub_menu != "")
    //            {
    //                outp = outp + "{" + c + "class" + c + ":" + c + clase + c + ",";
    //                outp = outp + c + "link" + c + ":" + c + link + c + ",";
    //                outp = outp + c + "icono" + c + ":" + c + icono + c + ",";
    //                outp = outp + c + "texto" + c + ":" + c + nombre + c + ",";
    //                outp = outp + c + "id2" + c + ":" + c + id_sub_sub_menu + c + "},";
    //            }
    //        }
    //        outp = "" + c + "records3" + c + ":[" + outp.TrimEnd(",") + "]}";
    //        System.Web.UI.Page.Response.Write(outp);
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //    finally
    //    {
    //    }
    //}

    [System.Web.Services.WebMethod(true)]
    public static string SendMessage()
    {
        UsuarioBC usuario = new UsuarioBC();
        usuario = (UsuarioBC)HttpContext.Current.Session["usuario"];
        string _Sql;
        _Sql = "EXEC dbo.LISTAR_MENU " + usuario.ID + "";
        DataSet ds_ = new DataSet();
        ds_ = retornoDS(_Sql, "datos");

        List<Menu_QMGPS> lst_menu = new List<Menu_QMGPS>();
        int i;

        if ((ds_.Tables[0].Rows.Count > 0))
        {
            for (i = 0; i <= ds_.Tables[0].Rows.Count - 1; i++)
            {
                Menu_QMGPS _menu = new Menu_QMGPS();

                _menu.Id = ds_.Tables[0].Rows[i]["MENU_ID"].ToString();
                _menu.Id_sub_menu = ds_.Tables[0].Rows[i]["ID_PADRE"].ToString();
                _menu.Link = ds_.Tables[0].Rows[i]["LINK"].ToString();
                _menu.Nombre = ds_.Tables[0].Rows[i]["TITULO"].ToString();
                _menu.Clase = ds_.Tables[0].Rows[i]["CLASE"].ToString();
                //_menu.Tiene_sub = ds_.Tables[0].Rows[i]["TIENE_SUB"].ToString();

                lst_menu.Add(_menu);
            }
        }

        return JsonConvert.SerializeObject(lst_menu);
    }
}

public class Menu_QMGPS
{
    private string _id;
    private string _id_sub_menu;
    private string _link;
    private string _nombre;
    private string _clase;
    private string _icono;

    public string Id
    {
        get
        {
            return _id;
        }
        set
        {
            _id = value;
        }
    }

    public string Id_sub_menu
    {
        get
        {
            return _id_sub_menu;
        }
        set
        {
            _id_sub_menu = value;
        }
    }

    public string Link
    {
        get
        {
            return _link;
        }
        set
        {
            _link = value;
        }
    }

    public string Nombre
    {
        get
        {
            return _nombre;
        }
        set
        {
            _nombre = value;
        }
    }

    public string Clase
    {
        get
        {
            return _clase;
        }
        set
        {
            _clase = value;
        }
    }

    //public string Tiene_sub
    //{
    //    get
    //    {
    //        return _tiene_sub;
    //    }
    //    set
    //    {
    //        _tiene_sub = value;
    //    }
    //}
}

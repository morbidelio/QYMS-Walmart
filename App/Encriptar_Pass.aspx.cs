using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class App_Encriptar_Pass : System.Web.UI.Page
{
    FuncionesGenerales f = new FuncionesGenerales();
    public void btn_encriptar_Click(object sender, EventArgs e)
    {
        UsuarioBC u = new UsuarioBC();
        DataView dw = u.ObtenerTodos().AsDataView();
        //dw.RowFilter = "PASS = USERNAME";
        //dw.RowFilter = "ID in (2212, 2261, 2273, 2311, 2343, 2414)";
        DataTable dt = dw.ToTable();
        DataTable dt1 = new DataTable();
        dt1.Columns.Add("ID");
        dt1.Columns.Add("PASS");
        string user, pass, newpass, original;
        StringBuilder s = new StringBuilder();
        StringBuilder t = new StringBuilder();
        foreach (DataRow dr in dt.Rows)
        {
            user = dr["USERNAME"].ToString();
            pass = dr["PASS"].ToString();
            newpass = f.Encriptar(pass, user);
            dt1.Rows.Add(user, newpass);
            s.Append("UPDATE USUARIOS SET  usua_estado='Activo', USUA_PASSWORD= '").
                Append(newpass).
                Append("' WHERE USUA_ID=").
                Append(dr["ID"].ToString() ).
                Append(" and USUA_USERNAME=USUA_PASSWORD and usua_estado='1' ").

                Append("<br />");



            //t.Append("ID: ").Append(dr["ID"].ToString()).Append(" Pass Original: ");
            try
            {
                original = f.Desencriptar(pass, user);
                //t.Append(original);
            }
            catch
            {
                t.Append("ID: ").
                    Append(dr["ID"].ToString()).
                    Append(" ERROR AL DESENCRIPTAR");
            }
            t.Append("<br />");
        }
        ltl.Text = s.ToString();
        desencriptar.Text = t.ToString();
        //gv1.DataSource = dt;
        //gv1.DataBind();
        //gv2.DataSource = dt1;
        //gv2.DataBind();
        //usuario.PASSWORD = funcion.Encriptar(this.txtPassword.Text, this.txtUsername.Text);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
}
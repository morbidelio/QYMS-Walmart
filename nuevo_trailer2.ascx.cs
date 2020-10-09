using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class nuevo_trailer2 : System.Web.UI.UserControl
{
    static UtilsWeb utils = new UtilsWeb();

    public event EventHandler ButtonClickDemo;

    public void Page_Load(object sender, EventArgs e)  
    {
        if (IsPostBack == false)
        {
            TransportistaBC tran = new TransportistaBC();
            TrailerTipoBC tipo = new TrailerTipoBC();
            utils.CargaDrop(ddl_editTran, "ID", "NOMBRE", tran.ObtenerTodos());
            utils.CargaDrop(ddl_editTipo, "ID", "DESCRIPCION", tipo.obtenerTodo());
            CaractCargaBC catt = new CaractCargaBC();
            DataTable dt = catt.obtenerTodo();
            //   rlcli.DataSource = dt;
            //   rlcli.DataTextField = "DESCRIPCION";
            //  rlcli.DataValueField = "ID";
            //  rlcli.DataBind();
        }
    }

    public void limpiarForm()
    {
        hf_excluyentes.Value = "";
        hf_noexcluyentes.Value = "";
        hf_idTrailer.Value = "";
        //     txt_editCodigo.Text = "";
        txt_editPlaca.Text = "";
        chk_editExterno.Checked = false;
        txt_editNumero.Text = "";
        ddl_editTran.ClearSelection();
        ddl_editTipo.ClearSelection();
        ltl_contenidoCaract.Text = crearContenido();
        //rlcli.ClearChecked();
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

    private void llenarForm(TrailerBC trailer)
    {
        //if (!String.IsNullOrEmpty(trailer.CARACTERISTICAS))
        //{
        //    string[] caracts = trailer.CARACTERISTICAS.Split(',');
        //    //rlcli.ClearChecked();
        //    //foreach (string c in caracts)
        //    //{
        //    //    //rlcli.FindItemByValue(c).Checked = true;
        //    //}
        //}
        //else
        //rlcli.ClearChecked();
        hf_idTrailer.Value = trailer.ID.ToString();
        //   txt_editCodigo.Text = trailer.CODIGO;
        txt_editPlaca.Text = trailer.PLACA;
        chk_editExterno.Checked = trailer.EXTERNO;
        txt_editNumero.Text = trailer.NUMERO;
        ddl_editTran.SelectedValue = trailer.TRAN_ID.ToString();
        ddl_editTipo.SelectedValue = trailer.TRTI_ID.ToString();
        hf_excluyentes.Value = trailer.EXCLUYENTES;
        hf_noexcluyentes.Value = trailer.NO_EXCLUYENTES;
    }

    protected void cambia_interno(object sender, EventArgs e)
    {
        if (chk_editExterno.Checked == false)
        {
            txt_editNumero.Enabled = true;
        }
        else
            txt_editNumero.Enabled = false;

        rfv_numero.Enabled = txt_editNumero.Enabled;
    }

    public void btn_editGrabar_Click(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();
        trailer.PLACA = txt_editPlaca.Text;
        trailer.CODIGO = txt_editPlaca.Text;
        trailer.EXTERNO = chk_editExterno.Checked;
        //trailer.CARACTERISTICAS = utils.concatenaId(rlcli);
        int tran_id;
        trailer.NUMERO = txt_editNumero.Text;
        if (int.TryParse(ddl_editTran.SelectedValue, out tran_id))
            trailer.TRAN_ID = tran_id;
        else
            trailer.TRAN_ID = 0;
        trailer.TRTI_ID = int.Parse(ddl_editTipo.SelectedValue);
        trailer.NO_EXCLUYENTES = hf_noexcluyentes.Value;
        trailer.EXCLUYENTES = hf_excluyentes.Value;

        if (hf_idTrailer.Value == "")
        {
            TrailerBC trai = new TrailerBC();
            trai = trai.obtenerXPlaca(txt_editPlaca.Text);
            if (trai.ID == 0 && txt_editNumero.Text!="")
                trai = trai.obtenerXNro(txt_editNumero.Text);

            if (trai.ID != 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('número o placa de Trailer ya existe');", true);
            }
            else
            {
                if (trailer.Crear(trailer))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Trailer creado exitosamente');", true);
                    limpiarForm();
                    ButtonClickDemo(trailer, null);
                }
                else
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al agregar trailer. Intente nuevamente.');", true);
            }
        }
        else
        {
            trailer.ID = int.Parse(hf_idTrailer.Value);
            if (trailer.Modificar(trailer))
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Trailer modificado exitosamente');", true);
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al modificar trailer. Intente nuevamente.');", true);
        }
    }
}
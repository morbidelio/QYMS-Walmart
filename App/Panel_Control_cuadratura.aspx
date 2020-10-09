<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Panel_Control_cuadratura.aspx.cs" Inherits="App_Panel_Control_cuadratura" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="server">
<div class="col-lg-12 separador"></div>
    <h2>Control Cuadratura</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" Runat="Server">
  <asp:UpdatePanel runat="server" ID="upfiltros" >
    <ContentTemplate>
      <asp:Panel ID="SITE" CssClass="col-xs-4" runat="server" >
        Site
        <asp:DropDownList ID="ddl_Site" runat="server"  ClientIDMode="Static"
                          onselectedindexchanged="ddl_Site_SelectedIndexChanged" AutoPostBack="true">
        </asp:DropDownList>
        <asp:Button ID="btn_recargar" CssClass="btn btn-primary" runat="server" 
                    Text="Recargar"  onclick="recargaplayas" />
        <button class="btn btn-primary" type="button" data-toggle="collapse" data-target="#collapseExample" aria-expanded="false" aria-controls="collapseExample">
          Mostrar Menú
        </button>
      </asp:Panel>
      <div class="col-xs-8">
        <asp:Literal ID="ltl_menuZonas" runat="server">
        </asp:Literal>
      </div>
      <div class="col-xs-12 separador"></div>
      <div class="collapse" id="collapseExample">
        <div class="col-xs-4" style="margin-left:5px;border: 2px solid; border-radius: 10px; width:34%;">
          <h5>
            Tipo de Trailer
          </h5>
          <asp:Panel ID="pnl_leyendaTipoTrailer" CssClass="row" style="padding-bottom: 10px; padding-left:10px;" runat="server">
          </asp:Panel>
        </div>
        <div class="col-xs-3" style="margin-left:5px;border: 2px solid; border-radius: 10px; width:20%; ">
          <h5>
            Estado Trailer
          </h5>
          <div class="row" style="padding-bottom: 10px; padding-left:10px;">
            <div>
              <div style="float:left; margin-right:2px; font-size:x-small">Vacío</div>
              <div style="float:left; margin-right:2px;">
                <img style="width:17px;" src="../Img/tra_vacio.png" />
              </div>
            </div>
            <div>
              <div style="float:left; margin-right:2px; font-size:x-small">
                Carga Parcial
              </div>
              <div style="float:left; margin-right:2px;">
                <img style="width:17px;" src="../Img/tra_semivacio.png" />
              </div>
            </div>
            <div>
              <div style="float:left; margin-right:2px; font-size:x-small">
                Carga Completa
              </div>
              <div style="float:left; margin-right:2px;">
                <img style="width:17px;" src="../Img/tra_ocupado.png" />
              </div>
            </div>
          </div>
        </div>
        <div class="col-xs-2" style="margin-left:5px;border: 2px solid; border-radius: 10px;">
          <h5>
            Estado Carga
          </h5>
          <div class="row" style="padding-bottom: 10px; padding-left:10px;">
            <div style="float:left; margin-right:2px; font-size:x-small">
              En hora
            </div>
            <div class="col-xs-6" style="height:20px;width:20px;padding:0;text-align:center;background-color:green;border-radius:20px;">
              <img src="../img/reloj.png" style="height:20px;width:20px;border-width:0px;position:absolute;top:0px;left:0px;">
            </div>
            <div style="float:left; margin-right:2px; font-size:x-small">Demorado</div>
            <div class="col-xs-6" style="height:20px;width:20px;padding:0;text-align:center;background-color:yellow;border-radius:20px;">
              <img src="../img/reloj.png" style="height:20px;width:20px;border-width:0px;position:absolute;top:0px;left:0px;">
            </div>
            <div style="float:left; margin-right:2px; font-size:x-small">Alarma</div>
            <div class="col-xs-6" style="height:20px;width:20px;padding:0;text-align:center;background-color:#ff0000;border-radius:20px;">
              <img src="../img/reloj.png" style="height:20px;width:20px;border-width:0px;position:absolute;top:0px;left:0px;">
            </div>
          </div>
        </div>
        <div id="dv_leyendaEstado" Visible="false" runat="server" class="col-xs-12" style="border: 2px solid; border-radius: 10px;">
          <h5>
            Estado Lugar
          </h5>
          <asp:Panel ID="pnl_leyendaEstado" CssClass="row" style="padding-bottom: 10px; padding-left:10px;" runat="server">
            <div style="float:left; margin-right:2px; width:17px; height:17px; background-color:#7FFFFF;"></div>
            <div style="float:left; margin-right:2px; font-size:x-small">
              Mov Pendiente
            </div>
            &nbsp
          </asp:Panel>
        </div>
      </div>
    </ContentTemplate> 
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" Runat="Server">
  <asp:UpdatePanel runat="server" UpdateMode="conditional" ID="upplayas">
    <ContentTemplate>
      <div style="width:100%;min-height:20vh; max-height:60vh;overflow-y:auto;">
        <asp:Panel runat="server" ID="playaslugares" CssClass="tab-content" >
        </asp:Panel>
      </div>
    </ContentTemplate>
    <Triggers>
      <asp:AsyncPostBackTrigger  ControlID="prueba" EventName="tick" />
    </Triggers>
  </asp:UpdatePanel>
  <asp:Timer ID="prueba" runat="server" OnTick="recargaplayas" Interval="60000"></asp:Timer>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" Runat="Server">
  <!-- Modal Pendientes-->
  <div id="modalPendientes" class="modal fade" data-backdrop="static" role="dialog">
    <div class="modal-dialog">
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <!-- Modal content-->
          <div class="modal-content">
            <div class="modal-header">
              <h4>
                <asp:Label ID="lblRazonEliminacion" runat="server"></asp:Label>
              </h4>
            </div>
            <div class="modal-body" style="overflow-y: auto;height: auto">
              <div class="col-lg-2">
                <asp:Panel ID="pnl_lugarOrigen" CssClass="lugar-especifico" runat="server" style="">
                  <asp:Panel ID="pnl_detalleLugar" CssClass="row columna-anden detalle-lugar-especifico" runat="server">
                    <asp:Label ID="lbl_lugar" runat="server"></asp:Label>
                  </asp:Panel>
                  <asp:Panel ID="pnl_detalleTrailer" CssClass="row columna-anden detalle-trailer-especifico" runat="server">
                    <asp:Panel style="width:25px;border-radius:25px;" ID="pnl_imgAlerta" runat="server">
                      <img id="img_alerta" runat="server" style="width:25px;border-radius:25px;" src="../img/reloj.png" />
                    </asp:Panel>
                    <div class="col-lg-6" style="padding-left:5px;padding:0px">
                    <asp:Image ID="img_trailer" Width="25px" runat="server" />
                        <span id="spn_tracto" runat="server">T</span>
                    </div>
                  </asp:Panel>
                </asp:Panel>
              </div>
              <div class="col-lg-5">
              <br />
                Zona:
                <asp:Label ID="lbl_origenZona" runat="server"></asp:Label>
              </div>
              <div class="col-lg-5">
              <br />
                Playa:
                <asp:Label ID="lbl_origenPlaya" runat="server"></asp:Label>
              </div>
              <div class="col-lg-12 separador"></div>
              <div class="row">
                <div class="col-lg-12">
                  <asp:RadioButton ID="rb_vacio" OnCheckedChanged="rb_estado_CheckedChanged" AutoPostBack="true" GroupName="estado_lugar" runat="server" />
                  Posición Vacía
                </div>
              </div>
              <div class="col-xs-12 separador"></div>
              <div class="row">
                <div class="col-lg-3">
                  <br />
                  <asp:RadioButton ID="rb_trailer" OnCheckedChanged="rb_estado_CheckedChanged" AutoPostBack="true" GroupName="estado_lugar" runat="server" />
                  Trailer
                </div>
                <div class="col-lg-3">
                  Placa Trailer
                  <br />
                  <asp:TextBox ID="txt_placaTrailer" OnTextChanged="txt_placaTrailer_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                </div>
                <div class="col-lg-3">
                  Flota
                  <br />
                  <asp:TextBox ID="txt_nroTrailer" OnTextChanged="txt_nroTrailer_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                </div>
                <div class="col-lg-3" >
                  Estado
                  <br />
                  <asp:DropDownList ID="ddl_estadoTrailer" OnSelectedIndexChanged="ddl_estadoTrailer_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server" enabled="false">
                    <asp:ListItem Value="0" Text="Vacío" />
                    <asp:ListItem Value="1" Text="Cargado" />
                  </asp:DropDownList>
                </div>
              </div>
              <div class="col-xs-12 separador"></div>
              <div class="row">
                <div class="col-lg-3">
                  <br />
                  <asp:RadioButton ID="rb_tracto" OnCheckedChanged="rb_estado_CheckedChanged" AutoPostBack="true" GroupName="estado_lugar" runat="server" />
                  Tracto
                </div>
                <div class="col-lg-3">
                  Placa Tracto
                  <br />
                  <asp:TextBox ID="txt_placaTracto" CssClass="form-control" runat="server" OnTextChanged="txt_placaTracto_TextChanged" AutoPostBack="true" />
                </div>
                <div class="col-lg-3 invisible" >
                  Transportista
                  <br />
                  <asp:DropDownList ID="ddl_transTracto" CssClass="form-control" runat="server">
                  </asp:DropDownList>
                </div>
              </div>
              <div class="col-xs-12 separador"></div>
              <div class="col-xs-12">
                <center>
                  <asp:LinkButton ID="btn_guarda_cambios" runat="server" OnClick="btn_guarda_cambios_click" Enabled="false" CssClass="btn btn-primary" >
                    <span class="glyphicon glyphicon-floppy-disk"></span>
                  </asp:LinkButton>
                </center>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" data-dismiss="modal" class="btn btn-primary">
                <span class="glyphicon glyphicon-remove"></span>
              </button>
            </div>
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" Runat="Server">
  <asp:UpdatePanel runat="server">
    <ContentTemplate>
      <asp:Button ID="btn_pendiente" runat="server" OnClick="btn_pendienteClick" Text="Button" />
      <%--<asp:Button ID="btn_mostrarDatos" runat="server" OnClick="btn_mostrarDatosClick" Text="Button" />--%>
      <asp:HiddenField ClientIDMode="Static" runat="server" id="tabactivo" />
      <asp:HiddenField ClientIDMode="Static" runat="server" id="div_position" />
  <asp:HiddenField ID="hf_idTrailer" runat="server" />
    </ContentTemplate>
  </asp:UpdatePanel>
  <asp:HiddenField ID="hf_idLugar" runat="server" />
  <asp:HiddenField ID="hf_idPlaya" runat="server" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" Runat="Server">
  <script type="text/javascript">
    function modalPendientes(i) {
        $("#<%= this.hf_idLugar.ClientID %>").val(i);
        var clickButton = document.getElementById("<%= this.btn_pendiente.ClientID %>");
        //        document.getElementById("<%= this.hf_idLugar.ClientID %>").Value = i;
        clickButton.click();
    }
    function mostrarDatos(mensaje, a) {
        $("#msj_play_" + a).text(mensaje);
    }

    $(document).ready(function () {
        graba_tab();
    });

    function graba_tab() {
        $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
            var target = $(e.target).attr("id"); // activated tab
            $("#tabactivo").val($(e.target).attr("id"));
            guardascroll();

            // alert(target);
        });
    }

    function reactivatab() {
        $('#' + $("#tabactivo").val())[0].click();
    }

    function guardascroll() {
        var div = document.getElementById("scrolls");
        var div_position = document.getElementById("div_position");
        var position = parseFloat(div_position.value);
        if (isNaN(position)) {
            position = 0;
        }
        div.scrollTop = position;
        div.onscroll = function () {
            div_position.value = div.scrollTop;
        };
    }

    window.onload = function () {

    };
  </script>
  <style type="text/css">
    .cpHeader
    {
    color: white;
    background-color: #719DDB;
    font: bold 11px auto "Trebuchet MS", Verdana;
    font-size: 12px;
    cursor: pointer;
    width:100%;
    height:18px;
    padding: 4px;           
    }
    .cpBody
    {
    background-color: #DCE4F9;
    font: normal 11px auto Verdana, Arial;
    border: 1px gray;               
    width:100%;
    padding: 4px;
    padding-top: 7px;
    }      


    a
    {
    color:Black;
    }
  </style>
</asp:Content>
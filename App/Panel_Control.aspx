<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Panel_Control.aspx.cs" Inherits="App_Panel_Control" %>

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
        <div class="col-xs-3" style="margin-left:5px;border: 2px solid; border-radius: 10px; width:20%;">
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
  <asp:Timer ID="prueba" runat="server" OnTick="recargaplayas" Interval="90000"></asp:Timer>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" Runat="Server">
  <!-- Modal Pendientes-->
  <div id="modalPendientes" class="modal fade" data-backdrop="static" role="dialog">
    <div class="modal-dialog modal-lg" style="width: 1300px">
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <!-- Modal content-->
          <div class="modal-content">
            <div class="modal-header">
              <h4 class="modal-title">
                Solicitudes de carga y mantenimiento pendientes
              </h4>
            </div>
            <div class="modal-body" style="height: auto; width: auto; overflow-y: auto;">
              <div class="col-xs-12">
                <asp:GridView ID="gv_solicitudesPendientes" runat="server" EmptyDataText="No hay solicitudes pendientes!" Width="100%" 
                              CssClass="table table-bordered table-hover tablita" BorderWidth="2" BorderColor="Black" AutoGenerateColumns="False" >
                  <Columns>
                    <asp:BoundField ReadOnly="True" HeaderText="Solicitud N°" DataField="ID_SOLICITUD" SortExpression="ID_SOLICITUD" />
                    <asp:BoundField ReadOnly="True" HeaderText="Fecha Límite" DataField="FECHA_LIMITE" SortExpression="FECHA_LIMITE" />
                    <asp:BoundField ReadOnly="True" HeaderText="Fecha Cumplimiento" DataField="FECHA_CUMPLIMIENTO" SortExpression="FECHA_CUMPLIMIENTO" />
                    <asp:BoundField ReadOnly="True" HeaderText="Fecha Solicitud" DataField="FECHA_SOLICITUD" SortExpression="FECHA_SOLICITUD" />
                    <%--<asp:BoundField ReadOnly="True" HeaderText="Nombre" DataField="MODELO" SortExpression="MODELO" />--%>
                    <%--<asp:BoundField ReadOnly="True" HeaderText="Nombre" DataField="RUTA" SortExpression="RUTA" />--%>
                    <asp:BoundField ReadOnly="True" HeaderText="Capacidad" DataField="CAPACIDAD" SortExpression="CAPACIDAD" />
                    <asp:BoundField ReadOnly="True" HeaderText="Frío" DataField="FRIO" SortExpression="FRIO" />
                    <%--<asp:BoundField ReadOnly="True" HeaderText="Nombre" DataField="MULTIFRIO" SortExpression="MULTIFRIO" />--%>
                    <asp:BoundField ReadOnly="True" HeaderText="Temperatura" DataField="TEMPERATURA" SortExpression="TEMPERATURA" />
                    <asp:BoundField ReadOnly="True" HeaderText="Plancha" DataField="PLANCHA" SortExpression="PLANCHA" />
                    <asp:BoundField ReadOnly="True" HeaderText="Patente" DataField="PATENTE" SortExpression="PATENTE" />
                    <asp:BoundField ReadOnly="True" HeaderText="Nro Flota" DataField="FLOTA" SortExpression="FLOTA" />
                    <asp:BoundField ReadOnly="True" HeaderText="Locales" DataField="LOCALES" SortExpression="LOCALES" />
                    <asp:BoundField ReadOnly="True" HeaderText="Observaciones" DataField="OBSERVACION" SortExpression="OBSERVACION" />
                  </Columns>
                </asp:GridView>
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
        </ContentTemplate>
  </asp:UpdatePanel>
  <asp:HiddenField ID="hf_idLugar" runat="server" />
  <asp:HiddenField ID="hf_idPlaya" runat="server" />
    </asp:Content>
        <asp:Content ID="Content6" ContentPlaceHolderID="scripts" Runat="Server">
          <script type="text/javascript">
            function modalPendientes(i) {
    $("#<%= hf_idLugar.ClientID %>").val(i);
            var clickButton = document.getElementById("<%= btn_pendiente.ClientID %>");
        //        document.getElementById("<%= hf_idLugar.ClientID %>").Value = i;
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
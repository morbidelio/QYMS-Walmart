<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Reporte_Movimientos.aspx.cs" Inherits="App_Reporte_Movimientos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-xs-12 separador"></div>
  <h2>Reporte Movimientos</h2>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-xs-12 separador"></div>
  <div class="col-xs-2">
    Site
    <br />
    <asp:DropDownList ID="ddl_site" CssClass="form-control" OnSelectedIndexChanged="ddl_site_SelectedIndexChanged" runat="server">
    </asp:DropDownList>
  </div>
  <div class="col-xs-1">
    Desde
    <br />
    <asp:TextBox ID="txt_desde" CssClass="form-control input-fecha" runat="server" />
  </div>
  <div class="col-xs-1">
    Hasta
    <br />
    <asp:TextBox ID="txt_hasta" CssClass="form-control input-fecha" runat="server" />
  </div>

  <div class="col-xs-1">
    Confirmación No Ottawa
    <br />
    <asp:checkbox ID="No_ottawa" runat="server" Checked="false" />  </div>
  <div class="col-xs-1">
    <br />
    <asp:LinkButton id="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" runat="server">
      <span class="glyphicon glyphicon-search"></span>
    </asp:LinkButton>
    <asp:LinkButton id="btn_exportar" CssClass="btn btn-primary" OnClick="btn_export_Click" runat="server">
      <span class="glyphicon glyphicon-export"></span>
    </asp:LinkButton>
  </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" Runat="Server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <asp:GridView ID="gv_listar" onsorting="gv_listar_Sorting" OnRowCreated="gv_listar_RowCreated" AllowSorting="true"
                    emptydatatext="No hay ingresos!" Width="100%"
                    CssClass="table table-bordered table-hover tablita" AutoGenerateColumns="false" runat="server" >
        <Columns>
          <asp:BoundField DataField="FH_CREA_MOV" SortExpression="FH_CREA_MOV" HeaderText="FH Creación Mov"  />
          <asp:BoundField DataField="FH_CUMPLIMIENTO" SortExpression="FH_CUMPLIMIENTO" HeaderText="FH Cumplimiento" />
          <asp:BoundField DataField="USUARIO_CREACION" SortExpression="USUARIO_CREACION" HeaderText="Usuario Creación Mov" />
          <asp:BoundField DataField="USUARIO_REMOLCADOR" SortExpression="USUARIO_REMOLCADOR" HeaderText="Usuario Ottawa" />
          <asp:BoundField DataField="USUARIO_FINALIZACION" SortExpression="USUARIO_FINALIZACION" HeaderText="Usuario confirmacion Mov." />
          <asp:BoundField DataField="PLAYA_ORIGEN" SortExpression="PLAYA_ORIGEN" HeaderText="Playa Origen" />
          <asp:BoundField DataField="POS_ORIGEN" SortExpression="POS_ORIGEN" HeaderText="Posición Origen" />
          <asp:BoundField DataField="PLAYA_DESTINO" SortExpression="PLAYA_DESTINO" HeaderText="Playa Destino" />
          <asp:BoundField DataField="POS_DESTINO" SortExpression="POS_DESTINO" HeaderText="Posición Destino" />
          <asp:BoundField DataField="TIPO_MOV" SortExpression="TIPO_MOV" HeaderText="Tipo Mov" />
          <asp:BoundField DataField="PATENTE" SortExpression="PATENTE" HeaderText="Placa Trailer" />
          <asp:BoundField DataField="NRO_FLOTA" SortExpression="NRO_FLOTA" HeaderText="Nro Flota" />
          <asp:BoundField DataField="SOLICITADO_VS_CUMPLIDO" SortExpression="SOLICITADO_VS_CUMPLIDO" HeaderText="Tiempo Solicitado v/s Tiempo Cumplido" />
        </Columns>
        <HeaderStyle CssClass="header-color2" />
      </asp:GridView>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
      <asp:Button ID="btnExportar" runat="server" CssClass="ocultar" OnClick="btnExportar_Click" />
        </ContentTemplate>
    <Triggers >
      <asp:PostBackTrigger ControlID="btnExportar"  />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" Runat="Server">
  <script type="text/javascript" src="../js2/jquery.dataTables.min.js"></script>
  <link rel="stylesheet" href="../js2/css/defaultTheme.css" media="screen" />
  <script type="text/javascript">
      function EndRequestHandler(sender, args) {
          setTimeout(tabla, 100);
      }
      Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);
      Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

      function Exportar() {
          $get("<%=this.btnExportar.ClientID %>").click();
      }


      var calcDataTableHeight = function () {
          //   alert($(window).height() - $("#scrolls").offset().top);
          return $(window).height() - $("#scrolls").offset().top - 100;
      };

      function reOffset1() {

          $('div.dataTables_scrollBody').height(calcDataTableHeight());
      }


      window.onresize = function (e) { reOffset1(); }


      function tabla() {
          if ($('#<%= this.gv_listar.ClientID %>')[0] != undefined && $('#<%= this.gv_listar.ClientID %>')[0].rows.length > 1) {
              $('#<%= this.gv_listar.ClientID %>').DataTable({
                  "scrollY": calcDataTableHeight(),
                  "scrollX": true,
                  "paging": false,
                  "ordering": false,
                  "searching": false,
                  "lengthChange": false,
                  "info": false
              });
          }
      }
  </script>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Reporte_Entrada.aspx.cs" Inherits="App_Reporte_Entrada" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-xs-12 separador"></div>
  <h2>
    Reporte Entradas
  </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-xs-12 separador"></div>
  <div class="container-fluid filtro">
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
      <br />
      <asp:LinkButton id="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" runat="server">
        <span class="glyphicon glyphicon-search"></span>
      </asp:LinkButton>
      <asp:LinkButton id="btn_exportar" CssClass="btn btn-primary" OnClick="btn_export_Click" runat="server">
        <span class="glyphicon glyphicon-import"></span>
      </asp:LinkButton>
    </div>
  </div>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server" class="container-fluid col-xs-12 cuerpo">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <asp:GridView ID="gv_listar" onsorting="gv_listar_Sorting" OnRowCreated="gv_listar_RowCreated" AllowSorting="true"
                    emptydatatext="No hay ingresos!" CssClass="table table-bordered table-hover tablita" AutoGenerateColumns="false" runat="server" >
        <Columns>
          <asp:BoundField DataField="PATENTE" SortExpression="PATENTE" HeaderText="Patente Trailer" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
          <asp:BoundField DataField="TRANSPORTISTA" SortExpression="TRANSPORTISTA" HeaderText="Transportista Trailer" HeaderStyle-Width="300px" ItemStyle-Width="300px" />
          <asp:BoundField DataField="PATENTE_TRACTO" SortExpression="PATENTE_TRACTO" HeaderText="Patente Tracto" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
          <asp:BoundField DataField="TRANSPORTISTA_TRACTO" SortExpression="TRANSPORTISTA_TRACTO" HeaderText="Transportista Tracto" HeaderStyle-Width="300px" ItemStyle-Width="300px" />
          <asp:BoundField DataField="SITE" SortExpression="SITE" HeaderText="Site" HeaderStyle-Width="80px" ItemStyle-Width="80px"  />
          <asp:BoundField DataField="ZONA" SortExpression="ZONA" HeaderText="Zona" HeaderStyle-Width="140px" ItemStyle-Width="140px"  />
          <asp:BoundField DataField="PLAYA" SortExpression="PLAYA" HeaderText="Playa" HeaderStyle-Width="250px" ItemStyle-Width="250px"  />
          <asp:BoundField DataField="LUGAR" SortExpression="LUGAR" HeaderText="Posición" HeaderStyle-Width="70px" ItemStyle-Width="70px"  />
          <asp:BoundField DataField="ESTADO" SortExpression="ESTADO" HeaderText="Estado" HeaderStyle-Width="155px" ItemStyle-Width="155px" />
          <asp:BoundField DataField="FECHA" SortExpression="FECHA" HeaderText="Fecha"  />
          <asp:BoundField DataField="HORA" SortExpression="HORA" HeaderText="Hora" />
          <asp:BoundField DataField="DIAS" SortExpression="DIAS" HeaderText="Días en CD" />
          <asp:BoundField DataField="USUARIO" SortExpression="USUARIO" HeaderText="Usuario Ingreso" />
          <asp:BoundField DataField="CONDUCTOR_RUT" SortExpression="CONDUCTOR_RUT" HeaderText="RUT Conductor" />
          <asp:BoundField DataField="CONDUCTOR_NOMBRE" SortExpression="CONDUCTOR_NOMBRE" HeaderText="Nombre Conductor" />
          <asp:BoundField DataField="CITA" SortExpression="CITA" HeaderText="N° Cita" />
          <asp:BoundField DataField="DOCUMENTO" SortExpression="DOCUMENTO" HeaderText="Documento" />
          <asp:BoundField DataField="PROVEEDOR" SortExpression="PROVEEDOR" HeaderText="Proveedor" />
          <asp:BoundField DataField="SELLO" SortExpression="SELLO" HeaderText="Sello" />
          <asp:BoundField DataField="TIPO_CARGA" SortExpression="TIPO_CARGA" HeaderText="Tipo Carga" />
          <asp:BoundField DataField="MOTIVO" SortExpression="MOTIVO" HeaderText="Motivo" />
          <asp:BoundField DataField="REFERENCIA" SortExpression="REFERENCIA" HeaderText="Referencia" />
          <asp:BoundField DataField="OBSERVACIONES" SortExpression="OBSERVACIONES" HeaderText="Observaciones" />
          <asp:BoundField DataField="PRING_FONO" SortExpression="PRING_FONO" HeaderText="Fono contacto" />
        </Columns>
        <EditRowStyle HorizontalAlign="Center" />
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
      <script type="text/javascript">
          Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);

          var pageScrollPos = 0;
          var pageScrollPosleft = 0;

          function BeginRequestHandler(sender, args) {
              pageScrollPos = $('div.dataTables_scrollBody').scrollTop();
              pageScrollPosleft = $('div.dataTables_scrollBody').scrollLeft();
          }
          var calcDataTableHeight = function () {
              return $(window).height() - $("#scrolls").offset().top - 80;
          };

          function reOffset1() {

              $('div.dataTables_scrollBody').height(calcDataTableHeight());
          }

          window.onresize = function (e) {
              reOffset1();
          }

    function EndRequestHandler(sender, args) {
        setTimeout(tabla, 200);
    }
    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);
    
    function Exportar() {
    $get("<%=this.btnExportar.ClientID %>").click();
    }
    
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


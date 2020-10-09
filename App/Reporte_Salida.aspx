<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Reporte_Salida.aspx.cs" Inherits="App_Reporte_Salida" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">

  <div class="col-xs-12 separador"></div>
  <h2>Reporte Salidas</h2>
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
          <asp:BoundField DataField="FECHA_EGRESO" SortExpression="FECHA_EGRESO" HeaderText="Fecha Egreso"  />
          <asp:BoundField DataField="HORA_EGRESO" SortExpression="HORA_EGRESO" HeaderText="Hora Egreso" />
          <asp:BoundField DataField="TIPO_DESTINO" SortExpression="TIPO_DESTINO" HeaderText="Tipo Destino" />
          <asp:BoundField DataField="DESTINO" SortExpression="DESTINO" HeaderText="Destino" />
          <asp:BoundField DataField="SELLO" SortExpression="SELLO" HeaderText="Sello" />
          <asp:BoundField DataField="RUTA" SortExpression="RUTA" HeaderText="Ruta" />
          <asp:BoundField DataField="PATENTE_TRACTO" SortExpression="PATENTE_TRACTO" HeaderText="Patente Tracto" />
          <asp:BoundField DataField="TRANSPORTISTA" SortExpression="TRANSPORTISTA" HeaderText="Transportista" />
          <asp:BoundField DataField="PATENTE" SortExpression="PATENTE" HeaderText="Patente" />
          <asp:BoundField DataField="NRO_FLOTA" SortExpression="NRO_FLOTA" HeaderText="Nro Flota" />
          <asp:BoundField DataField="CONDUCTOR_RUT" SortExpression="CONDUCTOR_RUT" HeaderText="Rut Conductor" />
          <asp:BoundField DataField="CONDUCTOR_NOMBRE" SortExpression="CONDUCTOR_NOMBRE" HeaderText="Conductor" />
          <asp:BoundField DataField="USUARIO_SALIDA" SortExpression="USUARIO_SALIDA" HeaderText="Usuario" />
          <asp:BoundField DataField="TEMPERATURA" SortExpression="TEMPERATURA" HeaderText="Temperatura" />
          <asp:BoundField DataField="GPS" SortExpression="GPS" HeaderText="GPS" />
          <asp:BoundField DataField="ESTADO_CARGA" SortExpression="ESTADO_CARGA" HeaderText="Estado Carga" />
          <%--<asp:BoundField DataField="RUT_PEONETA1" SortExpression="RUT_PEONETA1" HeaderText="RUT Peoneta 1" />
          <asp:BoundField DataField="NOMBRE_PEONETA1" SortExpression="NOMBRE_PEONETA1" HeaderText="Nombre Peoneta 1" />
          <asp:BoundField DataField="RUT_PEONETA2" SortExpression="RUT_PEONETA2" HeaderText="Rut Peoneta 2" />
          <asp:BoundField DataField="NOMBRE_PEONETA2" SortExpression="NOMBRE_PEONETA2" HeaderText="Nombre Peoneta 2" />--%>
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

      function Exportar() {
          $get("<%=this.btnExportar.ClientID %>").click();
      }

      function tabla() {
          if ($('#<%= this.gv_listar.ClientID %>')[0] != undefined && $('#<%= this.gv_listar.ClientID %>')[0].rows.length > 1) {
              $('#<%= this.gv_listar.ClientID %>').DataTable({
                  "scrollY": "320px",
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


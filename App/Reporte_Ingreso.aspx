<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Reporte_Ingreso.aspx.cs" Inherits="App_Reporte_Ingreso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-xs-12 separador"></div>
  <h2>Reporte Entrada BH y LI</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-xs-12 separador"></div>

  <div class="col-xs-2">
    Site
    <br />
    <asp:DropDownList ID="ddl_site" CssClass="form-control" OnSelectedIndexChanged="ddl_site_SelectedIndexChanged" runat="server">
    </asp:DropDownList>
  </div>
  <div class="col-xs-2">
    Tipo Ingreso
    <br />
    <asp:DropDownList ID="ddl_tipo" OnSelectedIndexChanged="ddl_site_SelectedIndexChanged" CssClass="form-control" runat="server">
      <asp:ListItem Value="3" Text="Ingresos Backhaul" />
      <asp:ListItem Value="8" Text="Ingresos Logística Inversa" />
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
  <asp:UpdatePanel runat="server">
    <ContentTemplate>
      <asp:GridView ID="gv_listar" onsorting="gv_listar_Sorting" OnRowCreated="gv_listar_RowCreated" AllowSorting="true"
                    emptydatatext="No hay ingresos!" Width="100%"
                    CssClass="table table-bordered table-hover tablita" AutoGenerateColumns="false" runat="server" >
        <Columns>
          <asp:BoundField DataField="FECHA_LLEGADA" SortExpression="FECHA_LLEGADA" HeaderText="Fecha Llegada"  />
          <asp:BoundField DataField="TIEMPO_EN_CD" SortExpression="TIEMPO_EN_CD" HeaderText="Tiempo en CD" />
          <asp:BoundField DataField="TRUE_GUIA_DESPACHO_INGRESO" SortExpression="TRUE_GUIA_DESPACHO_INGRESO" HeaderText="Guía" />
          <asp:BoundField DataField="DESCARGA" SortExpression="DESCARGA" HeaderText="Tiempo de descarga" />
          <asp:BoundField DataField="PATENTE" SortExpression="PATENTE" HeaderText="Patente" />
          <asp:BoundField DataField="NRO_FLOTA" SortExpression="NRO_FLOTA" HeaderText="Número Flota" />
          <asp:BoundField DataField="PLAYA" SortExpression="PLAYA" HeaderText="Playa" />
          <asp:BoundField DataField="ANDEN" SortExpression="ANDEN" HeaderText="Andén" />
          <asp:BoundField DataField="PROVEEDOR" SortExpression="PROVEEDOR" HeaderText="Proveedor" />
          <asp:BoundField DataField="OBSERVACIONES" SortExpression="OBSERVACIONES" HeaderText="Observaciones" />
        </Columns>
        <HeaderStyle CssClass="header-color2" />
      </asp:GridView>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
          setTimeout(tabla,100);
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
                  "lengthChange": false
              });
          }
      }
  </script>
</asp:Content>


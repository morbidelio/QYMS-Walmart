<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Reporte_Ult_Est_Trailer.aspx.cs" Inherits="App_Reporte_Ult_Est_Trailer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-lg-12 separador"></div>
  <h2>
    Reporte Estados Trailer
  </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-lg-12 separador"></div>
  <asp:UpdatePanel runat="server">
    <ContentTemplate>
      <div class="col-lg-1">
        N° Flota
        <br />
        <asp:TextBox id="txt_nroFlota" CssClass="form-control" runat="server" />

      </div>
      <div class="col-lg-1">
        Placa
        <br />
        <asp:TextBox ID="txt_placa" CssClass="form-control" runat="server" />
      </div>
      <div class="col-lg-1">
        Site
        <br />
        <asp:DropDownList ID="ddl_site" CssClass="form-control" runat="server">
        </asp:DropDownList>
      </div>
      <div class="col-lg-1">
        Desde
        <br />
        <asp:TextBox id="txt_desde" CssClass="input-fecha form-control" runat="server" />
      </div>
      <div class="col-lg-1">
        Hasta
        <br />
        <asp:TextBox id="txt_hasta" CssClass="input-fecha form-control" runat="server" />
      </div>
      <div class="col-lg-1">
        <br />
        <asp:LinkButton id="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" runat="server">
          <span class="glyphicon glyphicon-search"></span>
        </asp:LinkButton>
        <asp:LinkButton id="btn_exportar" CssClass="btn btn-primary" OnClick="btn_exportar_Click" runat="server">
          <span class="glyphicon glyphicon-export"></span>
        </asp:LinkButton>
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" Runat="Server">
  <asp:UpdatePanel runat="server">
    <ContentTemplate>
      <asp:GridView ID="gv_listar" CssClass="table table-bordered table-hover tablita" UseAccessibleHeader="true" Width="100%" AutoGenerateColumns="false"
                    OnRowCreated="gv_listar_RowCreated" EmptyDataText="No hay movimientos para los filtros seleccionados" runat="server" >
        <Columns>
          <asp:BoundField DataField="PLACA" SortExpression="PLACA" HeaderText="Placa" />
          <asp:BoundField DataField="FH_MODIFICACIONES" SortExpression="FH_MODIFICACIONES" HeaderText="FH modificaciones" />
          <asp:BoundField DataField="ACCION" SortExpression="ACCION" HeaderText="Accion" />
          <asp:BoundField DataField="soli_id" SortExpression="soli_id" HeaderText="Solicitud" />
          <asp:BoundField DataField="movi_id" SortExpression="movi_id" HeaderText="Movimiento" />
           <asp:BoundField DataField="tipo_movimiento" SortExpression="tipo_movimiento" HeaderText="Tipo Movimiento" />
          <asp:BoundField DataField="SITE" SortExpression="SITE" HeaderText="Site" />
          <asp:BoundField DataField="SITE_IN" SortExpression="SITE_IN" HeaderText="Site in" />
          <asp:BoundField DataField="POSICION" SortExpression="POSICION" HeaderText="Posicion" />
          <asp:BoundField DataField="CARGADO" SortExpression="CARGADO" HeaderText="Cargado" />
          <asp:BoundField DataField="PROVEEDOR" SortExpression="PROVEEDOR" HeaderText="Proveedor" />
          <asp:BoundField DataField="TEMPERATURA" SortExpression="TEMPERATURA" HeaderText="Temperatura" />
          <asp:BoundField DataField="NIVEL_PETROLEO" SortExpression="NIVEL_PETROLEO" HeaderText="Nivel petroleo" />
          <asp:BoundField DataField="FH_INGRESO" SortExpression="FH_INGRESO" HeaderText="FH ingreso" />
          <asp:BoundField DataField="FH_RETIRO" SortExpression="FH_RETIRO" HeaderText="FH retiro" />
          <asp:BoundField DataField="DOC_INGRESO" SortExpression="DOC_INGRESO" HeaderText="Doc ingreso" />
          <asp:BoundField DataField="GUIA_DESPACHO_INGRESO" SortExpression="GUIA_DESPACHO_INGRESO" HeaderText="Guia despacho ingreso" />
          <asp:BoundField DataField="SELLO_INGRESO" SortExpression="SELLO_INGRESO" HeaderText="Sello ingreso" />
          <asp:BoundField DataField="SELLO_CARGA" SortExpression="SELLO_CARGA" HeaderText="Sello carga" />
          <asp:BoundField DataField="ACOMP_RUT" SortExpression="ACOMP_RUT" HeaderText="Acomp rut" />
          <asp:BoundField DataField="TRUE_PATENTE_TRACTO" SortExpression="TRUE_PATENTE_TRACTO" HeaderText="True patente tracto" />
          <asp:BoundField DataField="OBS" SortExpression="OBS" HeaderText="Obs" />
          <asp:BoundField DataField="COD_INTERNO_IN" SortExpression="COD_INTERNO_IN" HeaderText="Cod interno in" />
          <asp:BoundField DataField="ESTADO_TRAILER" SortExpression="ESTADO_TRAILER" HeaderText="Estado trailer" />
          <asp:BoundField DataField="USUARIO" SortExpression="USUARIO" HeaderText="Usuario" />
          <asp:BoundField DataField="TIPO_INGRESO_CARGA" SortExpression="TIPO_INGRESO_CARGA" HeaderText="Tipo ingreso carga" />
          <asp:BoundField DataField="MOTIVO_INGRESO_CARGA" SortExpression="MOTIVO_INGRESO_CARGA" HeaderText="Motivo ingreso carga" />
          <asp:BoundField DataField="DESTINO" SortExpression="DESTINO" HeaderText="Destino" />
          <asp:BoundField DataField="LOCAL" SortExpression="LOCAL" HeaderText="Local" />
          <asp:BoundField DataField="RUT_CONDUCTOR" SortExpression="RUT_CONDUCTOR" HeaderText="Rut conductor" />
          <asp:BoundField DataField="NOMBRE_CONDUCTOR" SortExpression="NOMBRE_CONDUCTOR" HeaderText="Nombre conductor" />
        </Columns>
      </asp:GridView>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" Runat="Server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <asp:Button ID="btn_ExportarPost" runat="server" CssClass="ocultar" OnClick="btn_ExportarPost_Click" />
    </ContentTemplate>
    <Triggers >
      <asp:PostBackTrigger ControlID="btn_ExportarPost"  />
    </Triggers>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" Runat="Server">
  <script type="text/javascript">
    function EndRequestHandler(sender, args) {
        setTimeout(tabla, 100);
    }
    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);

    function Exportar() {
        $get("<%=this.btn_ExportarPost.ClientID %>").click();
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


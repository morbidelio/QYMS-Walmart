<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Lavado.aspx.cs" Inherits="App_Lavado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-lg-12 separador">
  </div>
  <h2>Lavado</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-lg-12 separador"></div>
  <div class="col-lg-1">
    Site
    <br />
    <asp:DropDownList ID="ddl_buscarSite" CssClass="form-control" runat="server">
    </asp:DropDownList>
  </div>
  <div class="col-lg-1">
    Desde
    <br />
    <asp:TextBox ID="txt_buscarDesde" CssClass="form-control input-fecha" runat="server" />
  </div>
  <div class="col-lg-1">
    Hasta
    <br />
    <asp:TextBox ID="txt_buscarHasta" CssClass="form-control input-fecha" runat="server" />
  </div>
  <div class="col-lg-1">
  <br />
    <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" runat="server">
      <span class="glyphicon glyphicon-search"></span>
    </asp:LinkButton>
  </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" Runat="Server">
  <asp:UpdatePanel runat="server">
    <ContentTemplate>
      <asp:GridView ID="gv_listar" OnRowCreated="gv_listar_RowCreated" 
      CssClass="table table-hover table-bordered tablita" width="100%"
      UseAccessibleHeader="true" AutoGenerateColumns="false" runat="server">
        <Columns>
          <asp:TemplateField>
            <ItemTemplate>
              <asp:LinkButton id="btn_lavado" CssClass="btn btn-sm btn-primary" runat="server">
                <span class="glyphicon glyphicon-play"></span>
              </asp:LinkButton>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:BoundField HeaderText="Placa" DataField="PLACA" />
          <asp:BoundField HeaderText="Número" DataField="NUMERO" />
          <asp:BoundField HeaderText="Tipo" DataField="TIPO" />
          <asp:BoundField HeaderText="Site" DataField="SITE" />
          <asp:BoundField HeaderText="Playa" DataField="PLAYA" />
          <asp:BoundField HeaderText="Posición" DataField="POSICION" />
          <asp:BoundField HeaderText="FH Último Lavado" DataField="FH_ULTIMO_LAVADO" />
        </Columns>
      </asp:GridView>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" Runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" Runat="Server">
  <script type="text/javascript">
      function EndRequestHandler(sender, args) {
          setTimeout(tabla, 100);
      }
      Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);

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
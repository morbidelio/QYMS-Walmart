<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Reporte_yardtag_ult_dato.aspx.cs" Inherits="App_Reporte_yardtag_ult_dato" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-xs-12 separador"></div>
  <h2>Reporte Yardtag ult dato</h2>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-xs-12 separador"></div>
  <div class="col-xs-2">
    Site
    <br />
    <asp:DropDownList ID="ddl_site" CssClass="form-control" OnSelectedIndexChanged="ddl_site_SelectedIndexChanged" runat="server" AutoPostBack="true">
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
    placa
    <br />
    <asp:TextBox ID="txt_placa" CssClass="form-control" runat="server" />  </div>

    
      <div class="col-lg-2">
                Playa
        <br />
                <asp:DropDownList ID="ddl_playa" CssClass="form-control" runat="server" />
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
      <asp:GridView ID="gv_listar" onsorting="gv_listar_Sorting" OnRowCreated="gv_listar_RowCreated" AllowSorting="true" AutoGenerateColumns="true"
                    emptydatatext="No hay Datos!" Width="100%"
                    CssClass="table table-bordered table-hover tablita"  runat="server" >
        <Columns>

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


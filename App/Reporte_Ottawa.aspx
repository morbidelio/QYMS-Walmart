<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Reporte_Ottawa.aspx.cs" Inherits="App_Reporte_Ottawa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-lg-12 separador"></div>
  <h2>
    Reporte Ottawa
  </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-lg-12 separador"></div>
  <div class="col-xs-2">
    Site
    <br />
    <asp:DropDownList ID="ddl_buscarSite" CssClass="form-control" runat="server" onselectedindexchanged="ddl_buscarSite_SelectedIndexChanged" AutoPostBack="true">
    </asp:DropDownList>
  </div>
  <div class="col-lg-2">
    Tipo Movimientos
    <br />
    <asp:DropDownList id="ddl_moti" CssClass="form-control" runat="server">
      <asp:ListItem Value="0" Text="Todos" />
    </asp:DropDownList>
  </div>
  <div class="col-lg-1">
    Desde
    <br />
    <asp:TextBox id="txt_desde" CssClass="form-control" runat="server" />
  </div>
  <div class="col-lg-1">
    Hasta
    <br />
    <asp:TextBox id="txt_hasta" CssClass="form-control" runat="server" />
  </div>
  <div class="col-lg-2">
    Usuario
    <br />
    <asp:DropDownList id="ddl_usuario" CssClass="form-control" runat="server">
      <asp:ListItem Value="0" Text="Todos" />
    </asp:DropDownList>
  </div>
  <div class="col-lg-2">
    Remolcador
    <br />
    <asp:DropDownList id="ddl_remo" CssClass="form-control" runat="server">
      <asp:ListItem Value="0" Text="Todos" />
    </asp:DropDownList>
  </div>
  <div class="col-lg-1">
    <br />
    <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" cssclass="btn btn-primary" runat="server">
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
      <div class="col-lg-12" style="overflow:auto;height:60vh">
        <asp:GridView ID="gv_listar" OnRowDataBound="gv_listar_RowDataBound" CssClass="table table-bordered table-hover tablita" Width="100%" runat="server">
        </asp:GridView>
      </div>
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
  <style type="text/css">
    th
    {
    padding: 0px !important;
    }
    td
    {
    padding: 0px !important;
    }
  </style>

  <script type="text/javascript">
 
      function Exportar() {
          $get("<%=this.btnExportar.ClientID %>").click();
      }

  </script>

</asp:Content>


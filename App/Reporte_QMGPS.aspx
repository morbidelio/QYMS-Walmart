<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Reporte_QMGPS.aspx.cs" Inherits="App_Reporte_QMGPS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-lg-12 separador"></div>
  <h2>
    Reporte QMGPS
  </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-lg-12 separador"></div>
  <asp:Panel ID="SITE" runat="server" >
    <div class="col-lg-1">
      Site
      <br />
      <asp:DropDownList ID="ddl_buscarSite" CssClass="form-control" runat="server" onselectedindexchanged="ddl_buscarSite_SelectedIndexChanged" AutoPostBack="true">
      </asp:DropDownList>
    </div>
  </asp:Panel>
  <div class="col-lg-2">
    Solo fuera de site
    <br />
    <asp:CheckBox id="chk_sitein" runat="server" />
  </div>
  <div class="col-lg-1">
    N° Flota
    <br />
      <asp:TextBox ID="txt_buscarNro" CssClass="form-control" runat="server" />
  </div>
  <div class="col-lg-1">
    Patente
    <br />
      <asp:TextBox ID="txt_buscarPlaca" CssClass="form-control" runat="server" />
  </div>
  <div class="col-lg-1">
    <br />
    <asp:LinkButton id="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" runat="server">
      <span class="glyphicon glyphicon-search"></span>
    </asp:LinkButton>
  </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" Runat="Server">
  <asp:UpdatePanel runat="server">
    <ContentTemplate>
      <div class="col-lg-12" style="height:60vh;overflow:auto;" >
        <asp:GridView ID="gv_listar" EmptyDataText="No hay trailers seleccionados" CssClass="table table-bordered table-hover tablita" Width="100%" runat="server" >
        </asp:GridView>
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" Runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" Runat="Server">
</asp:Content>
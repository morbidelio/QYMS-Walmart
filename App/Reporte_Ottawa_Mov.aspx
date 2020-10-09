<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Reporte_Ottawa_Mov.aspx.cs" Inherits="App_Reporte_Ottawa_Mov" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-xs-12 separador"></div>
  <h2>
    Reporte Ottawa por Movimiento
  </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-lg-12 separador"></div>

   <div class="col-xs-2">
    Site
    <br />
    <asp:DropDownList ID="ddl_buscarSite" CssClass="form-control" runat="server">
    </asp:DropDownList>
  </div>

  <div class="col-lg-1">
    Desde
    <br />
    <asp:TextBox id="txt_desde" CssClass="form-control input-fecha" runat="server" />
  </div>
  <div class="col-lg-1">
    Hasta
    <br />
    <asp:TextBox id="txt_hasta" CssClass="form-control input-fecha" runat="server" />
  </div>
  <div class="col-lg-1">
    <br />
    <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" cssclass="btn btn-primary" runat="server">
      <span class="glyphicon glyphicon-search"></span>
    </asp:LinkButton>
    <asp:LinkButton id="btn_exportar" OnClick="btn_export_Click" CssClass="btn btn-primary" runat="server">
      <span class="glyphicon glyphicon-export"></span>
    </asp:LinkButton>
  </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" Runat="Server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <div class="col-lg-12" style="overflow:auto;height:60vh">
        <asp:GridView ID="gv_listar" CssClass="table table-bordered table-hover tablita" AutoGenerateColumns="false" Width="100%" runat="server">
          <Columns>
           <asp:BoundField DataField="CODIGO" SortExpression="CODIGO" HeaderText="Código" />
            <asp:BoundField DataField="OTTAWA" SortExpression="OTTAWA" HeaderText="Ottawa" />
            <asp:BoundField DataField="JORNADA" SortExpression="JORNADA" HeaderText="Jornada" />
            <asp:BoundField DataField="CANT_MOV" SortExpression="CANT_MOV" HeaderText="Movimientos" />
            <asp:BoundField DataField="TIEMPO_CONECTADO" SortExpression="TIEMPO_CONECTADO" HeaderText="Tiempo Realizando Movimientos" />
            <asp:BoundField DataField="TIEMPO_LIBRE" SortExpression="TIEMPO_LIBRE" HeaderText="Tiempo libre" />
          </Columns>
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


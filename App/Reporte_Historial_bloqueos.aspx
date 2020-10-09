<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Reporte_Historial_bloqueos.aspx.cs" Inherits="App_Reporte_historia_bloqueos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-xs-12 separador"></div>
  <h2>
    Reporte Historial Bloqueos
  </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-lg-12 separador"></div>


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
           <asp:BoundField DataField="COHB_FECHA_BLOQUEO" SortExpression="COHB_FECHA_BLOQUEO" HeaderText="Fecha" />
            <asp:BoundField DataField="COHB_COND_MOTIVO_BLOQUEO" SortExpression="COHB_COND_MOTIVO_BLOQUEO" HeaderText="Motivo" />
             <asp:BoundField DataField="COHB_COND_BLOQUEADO" SortExpression="COHB_COND_BLOQUEADO" HeaderText="Bloqueado" />
            <asp:BoundField DataField="COND_RUT" SortExpression="COND_RUT" HeaderText="RUT Conductor" />
                      <asp:BoundField DataField="COND_NOMBRE" SortExpression="COND_NOMBRE" HeaderText="Nombre Conductor" />
            <asp:BoundField DataField="USUA_USERNAME" SortExpression="USUA_USERNAME" HeaderText="Usuario Bloqueo" />
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


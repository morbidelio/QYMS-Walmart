<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Limpiar_Solicitudes.aspx.cs" Inherits="App_Limpiar_Solicitudes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-lg-12 separador">
  </div>
  <h2>
    Limpiar solicitudes
  </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-lg-12 separador"></div>
  <div class="col-lg-1">
    <br />
  </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" Runat="Server">
  <asp:UpdatePanel runat="server">
    <ContentTemplate>
      <asp:GridView ID="gv_listar" CssClass="table table-hover table-bordered tablita" OnRowCommand="gv_listar_RowCommand" runat="server">
        <Columns>
          <asp:TemplateField>
            <ItemTemplate>
              <asp:LinkButton ID="btn_limpiar" CommandArgument='<%# Eval("SOLI_ID") %>' CommandName="BORRAR" cssclass="btn btn-primary" runat="server">
                <span class="glyphicon glyphicon-erase"></span>
              </asp:LinkButton>
            </ItemTemplate>
          </asp:TemplateField>
            
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
</asp:Content>
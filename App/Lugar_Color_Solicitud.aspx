<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Lugar_Color_Solicitud.aspx.cs" Inherits="App_Lugar_Color_Solicitud" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-xs-12 separador"></div>
  <h2>
    Maestro Color Estado Solicitud
  </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-xs-12 separador"></div>
  <div class="col-xs-12">
  </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" Runat="Server">
  <asp:UpdatePanel runat="server">
    <ContentTemplate>
      <center>
        <asp:GridView ID="gv_listar" CssClass="table table-hover table-bordered tablita" EmptyDataText="No existen estados-solicitud" AutoGenerateColumns="false"
                      OnRowCommand="gv_listar_RowCommand" OnRowDataBound="gv_listar_RowDataBound" OnPageIndexChanging="gv_listar_PageIndexChanging" 
                      pagesize="6" AllowPaging="true" runat="server" >
          <Columns>
            <asp:TemplateField ShowHeader="False">
              <ItemTemplate>
                <asp:LinkButton ID="btn_modificar" CssClass="btn btn-primary" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ID") %>'
                                CommandName="EDITAR" ToolTip="Modificar">
                  <span class="glyphicon glyphicon-pencil"></span>
                </asp:LinkButton>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
            <asp:BoundField DataField="DESCRIPCION" HeaderText="Tipo Solicitud" />
          </Columns>
          <PagerTemplate>
            Página
            <asp:DropDownList ID="paginasDropDownList" Font-Size="12px" AutoPostBack="true" runat="server"
                              OnSelectedIndexChanged="GoPage">
            </asp:DropDownList>
            de
            <asp:Label ID="lblTotalNumberOfPages" runat="server" />
            <asp:Button ID="Button4" runat="server" CommandName="Page" ToolTip="Prim. Pag" CommandArgument="First"
                        CssClass="pagfirst" />
            <asp:Button ID="Button1" runat="server" CommandName="Page" ToolTip="Pág. anterior"
                        CommandArgument="Prev" CssClass="pagprev" />
            <asp:Button ID="Button2" runat="server" CommandName="Page" ToolTip="Sig. página"
                        CommandArgument="Next" CssClass="pagnext" />
            <asp:Button ID="Button3" runat="server" CommandName="Page" ToolTip="Últ. Pag" CommandArgument="Last"
                        CssClass="paglast" />
          </PagerTemplate>
          <PagerStyle ForeColor="#BBB" HorizontalAlign="Right" BackColor="White" />
        </asp:GridView>
      </center>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" Runat="Server">
  <div id="modalColor" class="modal fade" data-backdrop="static" role="dialog">
    <div class="modal-dialog modal-lg">
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <div class="modal-content">
            <div class="modal-header">
              <h4 class="modal-title">
                Asignar Color Estado
              </h4>
            </div>
            <div class="modal-body" style="height: auto; width: auto; overflow-y: auto;">
              <%--<div class="col-xs-12">
              <asp:Literal ID="ltl_menu" runat="server"></asp:Literal>
              </div>
              <div class="col-xs-12 separador">
              </div>--%>
              <div class="col-xs-12">
                <asp:Panel ID="pnl_Sites" runat="server">
                </asp:Panel>
                <%--<asp:Literal ID="ltl_color" runat="server"></asp:Literal>--%>
              </div>
              <div class="col-xs-12 separador">
              </div>
              <div class="col-xs-12">
                <center>
                  <asp:LinkButton ID="btn_grabar" runat="server" OnClick="btn_grabar_Click"
                                  CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk"></span>
                  </asp:LinkButton>
                </center>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-primary" data-dismiss="modal">
                <span class="glyphicon glyphicon-remove"></span></button>
            </div>
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" Runat="Server">
  <asp:UpdatePanel runat="server">
    <ContentTemplate>
      <asp:HiddenField ID="hf_id" runat="server" />
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" Runat="Server">
    <link rel="stylesheet" media="screen" type="text/css" href="../css2/spectrum.css" />
    <script type="text/javascript" src="../js2/spectrum.js"></script>
  <script type="text/javascript">
    function modalColor() {
        $("#modalColor").modal();
    }
    function EndRequestHandler(sender, args) {
        $(".color").spectrum({
            change: function (color) {
                $(this).val(color.toHexString());
            }
        });
    }
    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);
  </script>
</asp:Content>
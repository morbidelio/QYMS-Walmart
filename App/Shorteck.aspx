<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Shorteck.aspx.cs" Inherits="App_Shorteck" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-xs-12 separador"></div>
  <h2>
    Maestro Shortec
  </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-xs-12 separador"></div>
  <div class="col-xs-12">
    <div class="col-xs-2">
      Código
      <br />
      <asp:TextBox id="txt_buscarCod" CssClass="form-control" runat="server" />
    </div>
    <div class="col-xs-2">
      Descripción
      <br />
      <asp:TextBox ID="txt_buscarDesc" CssClass="form-control" runat="server" />
    </div>
    <div class="col-xs-1">
    </div>
    <br />
    <asp:LinkButton ID="btn_buscar" cssclass="btn btn-primary" OnClick="btn_buscar_Click" runat="server" >
      <span class="glyphicon glyphicon-search"></span>
    </asp:LinkButton>
    <asp:LinkButton ID="btn_nuevo" cssclass="btn btn-primary" OnClick="btn_nuevo_Click" runat="server" >
      <span class="glyphicon glyphicon-plus"></span>
    </asp:LinkButton>
  </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" Runat="Server">
  <asp:UpdatePanel runat="server">
    <ContentTemplate>
      <div class="col-xs-12">
        <center>
          <asp:GridView ID="gv_listar" runat="server" AllowPaging="True" AllowSorting="True"
                        BorderColor="Black" CellPadding="8" DataKeyNames="SHOR_ID" BackColor="White" GridLines="Horizontal"
                        EnableSortingAndPagingCallbacks="True" OnPageIndexChanging="gv_listar_PageIndexChanging"
                        OnRowCommand="gv_listar_RowCommand" PageSize="6" OnSorting="gv_listar_Sorting" EmptyDataText="No se encontraron códigos Shortec!"
                        AutoGenerateColumns="False" Width="60%" PagerSettings-Mode="NumericFirstLast"
                        CssClass="table table-bordered table-hover tablita" OnRowDataBound="gv_listar_RowDataBound">
            <Columns>
              <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                 HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:LinkButton ID="btn_modificar" runat="server" CausesValidation="false" CommandArgument='<%# Eval("SHOR_ID") %>'
                                  CommandName="EDITAR" CssClass="btn btn-sm btn-primary" ToolTip="Modificar">
                    <span class="glyphicon glyphicon-pencil"></span>
                  </asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              </asp:TemplateField>
              <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                 HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:LinkButton ID="btn_eliminar" runat="server" CausesValidation="false" CommandArgument='<%# Eval("SHOR_ID") %>'
                                  CommandName="ELIMINAR" CssClass="btn btn-sm btn-primary" ToolTip="Modificar">
                    <span class="glyphicon glyphicon-remove"></span>
                  </asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              </asp:TemplateField>
              <asp:BoundField HeaderText="Código" DataField="SHOR_ID" SortExpression="SHOR_ID" />
              <asp:BoundField HeaderText="Descripción" DataField="SHOR_DESC" SortExpression="SHOR_DESC" />
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
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" Runat="Server">
  <div class="modal fade" id="modalShorteck" data-backdrop="static" role="dialog">
    <div class="modal-dialog" role="dialog">
      <asp:UpdatePanel runat="server">
        <ContentTemplate>
          <div class="modal-content">
            <div class="modal-header">
              <h4>
                Eliminar Shortec
              </h4>
            </div>
            <div class="modal-body" style="overflow-y:auto; height:auto">
              <div class="col-xs-12">
                <div class="col-xs-6">
                  Código
                  <br />
                  <asp:TextBox ID="txt_editCodigo" CssClass="form-control" runat="server" />
                </div>
                <div class="col-xs-6">
                  Descripción
                  <br />
                  <asp:TextBox ID="txt_editDescripcion" CssClass="form-control" runat="server" />
                </div>
              </div>
              <div class="col-xs-12 separador"></div>
              <div class="col-xs-12">
                <center>
                  <asp:LinkButton ID="btn_guardar" OnClick="btn_guardar_Click" runat="server" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk"></span>
                  </asp:LinkButton>
                </center>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" data-dismiss="modal" class="btn btn-primary">
                <span class="glyphicon glyphicon-remove"></span>
              </button>
            </div>
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </div>
  <div class="modal fade" id="modalConfirmacion" data-backdrop="static" role="dialog">
    <div class="modal-dialog" role="dialog">
      <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
          <div class="modal-content">
            <div class="modal-header">
              <h4>
                Eliminar Shortec
              </h4>
            </div>
            <div class="modal-body" style="overflow-y:auto; height:auto">
              El registro seleccionado será eliminado de la base de datos ¿Desea continuar?
            </div>
            <div class="modal-footer">
              <asp:LinkButton ID="btnModalEliminar" OnClick="btnModalEliminar_Click" runat="server" CssClass="btn btn-primary">
                <span class="glyphicon glyphicon-ok"></span>
              </asp:LinkButton>
              <button type="button" data-dismiss="modal" class="btn btn-primary">
                <span class="glyphicon glyphicon-remove"></span>
              </button>
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
      <asp:HiddenField ID="hf_idShorteck" runat="server" />
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" Runat="Server">
  <script type="text/javascript">
    function modalShorteck() {
        $("#modalShorteck").modal();
    }

    function modalConfirmacion() {
        $("#modalConfirmacion").modal();
    }
  </script>
</asp:Content>
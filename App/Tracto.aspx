<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Tracto.aspx.cs" Inherits="App_Tracto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-lg-12 separador"></div>
    <h2>Maestro Tracto
    </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-lg-12 separador"></div>
    <div class="col-lg-1">
        Placa
    <br />
        <asp:TextBox ID="txt_buscarPlaca" CssClass="form-control" runat="server" />
    </div>
    <div class="col-lg-3">
        Transportista
    <br />
        <asp:DropDownList ID="ddl_buscarTrans" CssClass="form-control" runat="server">
            <asp:ListItem Text="Todos" />
        </asp:DropDownList>
    </div>
    <div class="col-lg-1">
        <br />
        <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_click" CssClass="btn btn-primary"
            ToolTip="Buscar Tracto" runat="server">
      <span class="glyphicon glyphicon-search" />
        </asp:LinkButton>
        <asp:LinkButton ID="btn_nuevo" CssClass="btn btn-primary" ToolTip="Nuevo Tracto"
            runat="server" OnClick="btn_nuevo_Click">
      <span class="glyphicon glyphicon-plus" />
        </asp:LinkButton>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-xs-12" style="text-align:center;">
          <asp:GridView ID="gv_listar" runat="server" AllowPaging="True" AllowSorting="True" PageSize="10" Width="70%" 
                        OnPageIndexChanging="gv_listar_PageIndexChanging" OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting" OnRowDataBound="gv_listar_RowDataBound"
                        EmptyDataText="No se encontraron Tractos!" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tablita">
            <Columns>
              <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center"
                                 HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:LinkButton ID="btn_modificar" runat="server" CommandName="EDITAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Modificar">
                    <span class="glyphicon glyphicon-pencil" />
                  </asp:LinkButton>
                </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center"
                                 HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:LinkButton ID="btn_eliminar" runat="server" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Eliminar">
                    <span class="glyphicon glyphicon-remove" />
                  </asp:LinkButton>
                </ItemTemplate>
              </asp:TemplateField>
              <asp:BoundField HeaderText="Patente" DataField="PATENTE" SortExpression="PATENTE" />
              <asp:BoundField HeaderText="Estado" DataField="ESTADO" SortExpression="ESTADO" />
              <asp:BoundField HeaderText="Transportista" DataField="TRANSPORTISTA" SortExpression="TRANSPORTISTA" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
    <div class="modal fade" id="modalTracto" data-backdrop="static" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Datos Tracto
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-xs-12">
                                Patente
                <br />
                                <asp:TextBox ID="txt_editPatente" runat="server" CssClass="form-control input-mayus"></asp:TextBox>
                            </div>
                            <div class="col-xs-12">
                                Transportista
                <br />
                                <asp:DropDownList ID="ddl_editTran" CssClass="form-control input-mayus" runat="server">
                                    <asp:ListItem Value="0" Text="Seleccione..." />
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-12 separador">
                            </div>
                            <div class="col-xs-12">
                                <center>
                  <asp:LinkButton ID="btn_editGrabarNuevo" runat="server" OnClick="btn_editGrabar_Click"
                                  ValidationGroup="nuevoCliente" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk" />
                  </asp:LinkButton>
                </center>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-primary">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- modal eliminación -->
    <div class="modal fade" id="modalConfirmar" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Eliminar Tracto
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            Se eliminará el tracto seleccionado ¿Desea continuar?
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btnModalEliminar" runat="server" ToolTip="Eliminar" CssClass="btn btn-primary"
                                OnClick="btn_eliminar_click">
                <span class="glyphicon glyphicon-ok" />
                            </asp:LinkButton>
                            <button type="button" data-dismiss="modal" class="btn btn-primary">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_id" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        function modalTracto() {
            $("#modalTracto").modal();
        }

        function modalConfirmar() {
            $("#modalConfirmar").modal();
        }
    </script>
</asp:Content>


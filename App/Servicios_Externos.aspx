<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Servicios_Externos.aspx.cs" Inherits="App_Servicios_Externos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-lg-12 separador"></div>
  <h2>
    Maestro Servicios Externos
  </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-lg-12 separador"></div>
  <div class="col-lg-2">
    Código
    <br />
    <asp:TextBox ID="txt_buscarCodigo" CssClass="form-control" runat="server" />
  </div>
  <div class="col-lg-1">
  <br />
    <asp:LinkButton id="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" runat="server">
      <span class="glyphicon glyphicon-search"></span>
    </asp:LinkButton>
    <asp:LinkButton id="btn_nuevo" OnClick="btn_nuevo_Click" CssClass="btn btn-primary" runat="server">
      <span class="glyphicon glyphicon-plus"></span>
    </asp:LinkButton>
  </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" Runat="Server">
  <asp:UpdatePanel runat="server">
    <ContentTemplate>
      <div class="col-lg-12">
        <center>
          <asp:GridView ID="gv_listar" runat="server" AllowPaging="True" AllowSorting="True" enableViewState="false"
                        OnPageIndexChanging="gv_listar_PageIndexChanging" OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting"
                        EmptyDataText="No se encontraron servicios externos!" AutoGenerateColumns="False" Width="50%" CssClass="table table-bordered table-hover tablita">
            <Columns>
              <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                  <asp:LinkButton ID="btn_modificarEmpresa" runat="server" CausesValidation="false" CommandName="EDITAR" CommandArgument='<%# Eval("SEEX_ID") %>'
                                  CssClass="btn btn-sm btn-primary" ToolTip="Modificar">
                    <span class="glyphicon glyphicon-pencil"></span>
                  </asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              </asp:TemplateField>
              <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                  <asp:LinkButton ID="btn_eliminarEmpresa" runat="server" CausesValidation="False" CommandName="ELIMINAR" CommandArgument='<%# Eval("SEEX_ID") %>'
                                  CssClass="btn btn-sm btn-primary" ToolTip="Eliminar">
                    <span class="glyphicon glyphicon-remove"></span>
                  </asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              </asp:TemplateField>
              <asp:BoundField DataField="SEEX_ID" visible="false" />
              <asp:BoundField DataField="CODIGO" SortExpression="CODIGO" HeaderText="Código" />
            </Columns>
          <PagerStyle CssClass="pagination-ys" />
          </asp:GridView>
        </center>
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" Runat="Server">
  <!-- modal Empresa  ---->
  <div class="modal fade" id="modalEditar" data-backdrop="static" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
          <ContentTemplate>
            <div class="modal-header">
              <h4 class="modal-title">
                Datos Empresa
              </h4>
            </div>
            <div class="modal-body" style="height: auto; overflow: auto">
              <div class="col-xs-12">
                Código
                <br />
                <asp:TextBox ID="txt_editCodigo" CssClass="form-control" runat="server" />
              </div>
              <div class="col-xs-12 separador">
              </div>
              <div class="col-xs-12">
                <center>
                  <asp:LinkButton ID="btn_editGrabar" runat="server" OnClick="btn_editGrabar_Click" ValidationGroup="edit"
                                  CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk"></span>
                </asp:LinkButton>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-primary" data-dismiss="modal">
                <span class="glyphicon glyphicon-remove"></span>
              </button>
            </div>
            <asp:RequiredFieldValidator ID="rfv_codigo" runat="server" ControlToValidate="txt_editCodigo"
                                        Display="None" ErrorMessage="* Requerido" ValidationGroup="nuevoCliente" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" PopupPosition="BottomLeft"
                                          runat="server" Enabled="True" TargetControlID="rfv_codigo">
            </asp:ValidatorCalloutExtender>
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div>
  </div>
  <!-- modal eliminación -->
  <div class="modal fade" id="modalConfirmar" data-backdrop="static" role="dialog" >
    <div class="modal-dialog" role="dialog">
      <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
          <div class="modal-content">
            <div class="modal-header">
              <h4>
                <asp:Label ID="lbl_tituloConfirmar" runat="server"></asp:Label>
              </h4>
            </div>
            <div class="modal-body">
              <asp:Label ID="lbl_msjConfirmar" runat="server"></asp:Label>
            </div>
            <div class="modal-footer">
              <asp:LinkButton ID="btnModalEliminar" runat="server" onclick="btn_Eliminar_Click"
                              CssClass="btn btn-primary">
                <span class="glyphicon glyphicon-ok"></span>
              </asp:LinkButton>
              <button type="button" class="btn btn-primary" data-dismiss="modal">
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
      <asp:HiddenField ID="hf_id" runat="server" />
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" Runat="Server">
  <script type="text/javascript">
    function modalEditar() {
        $("#modalEditar").modal();
    }
    function modalConfirmar() {
        $("#modalConfirmar").modal();
    }
  </script>
</asp:Content>
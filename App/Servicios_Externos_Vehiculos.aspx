<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Servicios_Externos_Vehiculos.aspx.cs" Inherits="App_Servicios_Externos_Vehiculos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-lg-12 separador"></div>
  <h2>
    Maestro Vehículos Servicios Externos
  </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-lg-12 separador"></div>
  <div class="col-lg-1">
    Código
    <br />
    <asp:TextBox ID="txt_buscarCodigo" CssClass="form-control" runat="server" />

  </div>
  <div class="col-lg-1">
    Placa
    <br />
    <asp:TextBox ID="txt_buscarPlaca" CssClass="form-control" runat="server" />

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
          <asp:GridView ID="gv_listar" runat="server" AllowPaging="True" AllowSorting="True" pagesize="8" enableviewstate="false"
                        OnPageIndexChanging="gv_listar_PageIndexChanging" OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting"
                        EmptyDataText="No se encontraron vehiculos!" AutoGenerateColumns="False" Width="50%" CssClass="table table-bordered table-hover tablita">
            <Columns>
              <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                  <asp:LinkButton ID="btn_modificar" runat="server" CausesValidation="false" CommandName="EDITAR" CommandArgument='<%# Eval("SEVE_ID") %>'
                                  CssClass="btn btn-sm btn-primary" ToolTip="Modificar">
                    <span class="glyphicon glyphicon-pencil"></span>
                  </asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              </asp:TemplateField>
              <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                  <asp:LinkButton ID="btn_eliminar" runat="server" CausesValidation="False" CommandName="ELIMINAR" CommandArgument='<%# Eval("SEVE_ID") %>'
                                  CssClass="btn btn-sm btn-primary" ToolTip="Eliminar">
                    <span class="glyphicon glyphicon-remove"></span>
                  </asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              </asp:TemplateField>
              <asp:BoundField DataField="SEVE_ID" visible="false" />
              <asp:BoundField DataField="CODIGO" SortExpression="CODIGO" HeaderText="Código" />
              <asp:BoundField DataField="PLACA" SortExpression="PLACA" HeaderText="Placa" />
              <asp:BoundField DataField="PROVEEDOR" SortExpression="PROVEEDOR" HeaderText="Proveedor" />
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
    <div class="modal-dialog ">
      <div class="modal-content">
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
          <ContentTemplate>
            <div class="modal-header">
              <h4 class="modal-title">
                Datos Vehículo Externo
              </h4>
            </div>
            <div class="modal-body" style="height: auto; overflow: auto">
              <div class="col-xs-3 invisible"  style="display:none; visibility:hidden;">
                Código
                <br />
                <asp:TextBox ID="txt_editCodigo" CssClass="form-control" runat="server" />
              </div>
              <div class="col-xs-4">
                Placa
                <br />
                <asp:TextBox ID="txt_editPlaca" CssClass="form-control" runat="server" />
              </div>
              <div class="col-xs-6">
                Proveedor
                <br />
                  <asp:DropDownList ID="ddl_editProv" CssClass="form-control" runat="server">
                      <asp:ListItem Value="0" Text="Seleccione..." />
                  </asp:DropDownList>
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


    function EndRequestHandler(sender, args) {

        $('#<%= txt_buscarPlaca.ClientID %>').on('input', function () {
            this.value = this.value.replace(/[^0-9a-zA-Zk]/g, '');
        });
    }



    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);
  </script>


</asp:Content>
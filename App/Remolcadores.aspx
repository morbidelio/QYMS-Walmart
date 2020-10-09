<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Remolcadores.aspx.cs" Inherits="App_Remolcadores" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-lg-12 separador">
  </div>
  <h2>
    Maestro Remolcador
  </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-lg-12 separador">
  </div>
  <asp:Panel ID="SITE" runat="server" >
    <div class="col-lg-2">
      Site
      <br />
      <asp:DropDownList runat="server" CssClass="form-control" id="dropsite" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="drop_SelectedIndexChanged" ></asp:DropDownList>
    </div>
  </asp:Panel>
  <div class="col-lg-2">
    Código
    <br />
    <asp:TextBox ID="txt_buscarCodigo" CssClass="form-control" runat="server"></asp:TextBox>
  </div>
  <div class="col-lg-2">
    Descripción
    <br />
    <asp:TextBox ID="txt_buscarDescripcion" CssClass="form-control" runat="server"></asp:TextBox>
  </div>
  <div class="col-lg-2">
    <br />
    <asp:LinkButton ID="btn_buscarRemolcador" OnClick="btn_buscar_Click" CssClass="btn btn-primary"
                    ToolTip="Buscar Remolcador" runat="server">
      <span class="glyphicon glyphicon-search"></span>
    </asp:LinkButton>
    <asp:LinkButton ID="btn_nuevoRemolcador" CssClass="btn btn-primary" ToolTip="Nuevo Remolcador"
                    runat="server" OnClick="btn_nuevo_Click">
      <span class="glyphicon glyphicon-plus"></span>
    </asp:LinkButton>
  </div>

</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" Runat="Server">
  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
      <div class="col-lg-12">
        <center>
          <asp:GridView ID="gv_listar" runat="server" AllowPaging="True" AllowSorting="True" DataKeyNames="ID" PageSize="8" EnableViewState="false"
                        OnRowCommand="gv_listar_RowCommand" OnRowEditing="gv_listar_RowEditing" OnSorting="gv_listar_Sorting" OnPageIndexChanging="gv_listar_PageIndexChanging"
                        EmptyDataText="No se encontraron remolcadores!" AutoGenerateColumns="False" Width="70%" CssClass="table table-bordered table-hover tablita" >
            <Columns>
              <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%"
                                 ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:LinkButton ID="btn_modificarRemolcador" runat="server" CausesValidation="false" CommandName="Edit"
                                  CssClass="btn btn-sm btn-primary" ToolTip="Modificar">
                    <span class="glyphicon glyphicon-pencil"></span>
                  </asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              </asp:TemplateField>
              <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%"
                                 ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:LinkButton ID="btn_eliminarRemolcador" runat="server" CausesValidation="False" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID") %>'
                                  CssClass="btn btn-sm btn-primary" ToolTip="Eliminar">
                    <span class="glyphicon glyphicon-remove"></span>
                  </asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              </asp:TemplateField>
              <asp:BoundField ReadOnly="True" HeaderText="Id" DataField="ID" SortExpression="ID"
                              ItemStyle-Height="30px" Visible="false"></asp:BoundField>
              <asp:BoundField ReadOnly="True" HeaderText="Código" DataField="CODIGO" SortExpression="CODIGO"
                              ItemStyle-Width="5%" ItemStyle-Height="30px"></asp:BoundField>
              <asp:BoundField ReadOnly="True" HeaderText="Descripción" DataField="DESCRIPCION" SortExpression="DESCRIPCION"
                              ItemStyle-Width="30%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                              HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
            </Columns>
            <PagerStyle CssClass="pagination-ys" />
          </asp:GridView>
        </center>
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" Runat="Server">
  <!-- modal Remolcador  ---->
  <div class="modal fade" id="modalRemolcador" data-backdrop="static" role="dialog">

    <div class="modal-dialog modal-lg">
      <div class="modal-content">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
          <ContentTemplate>
            <div class="modal-header">
              <h4 class="modal-title">
                Datos Remolcador
              </h4>
            </div>
            <div class="modal-body" style="height: auto; overflow: auto">
              <div class="col-lg-2">
                Site
                <br />
                <asp:DropDownList ID="ddl_editSite" CssClass="form-control" runat="server">
                  <asp:ListItem Value="0" Text="Seleccione..." />
                </asp:DropDownList>
              </div>
              <div class="col-lg-2">
                Código
                <br />
                <asp:TextBox ID="txt_editCodigo" CssClass="form-control input-mayus" runat="server"></asp:TextBox>
              </div>
              <div class="col-lg-5">
                Descripción
                <br />
                <asp:TextBox ID="txt_editDesc" CssClass="form-control input-mayus" runat="server"></asp:TextBox>
              </div>
              <div class="col-lg-3" style="display:none; visibility:hidden">
                Usuario
                <br />
                <asp:DropDownList ID="ddl_editUsuario" Visible="false"
                                  AutoPostBack="true" runat="server" CssClass="form-control">
                  <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
                </asp:DropDownList>
              </div>
              <div class="col-lg-12 separador"></div>
              <div class="col-lg-12">
                <center>
                  <asp:LinkButton ID="btn_editGrabar" runat="server" OnClick="btn_editGrabar_Click" ValidationGroup="nuevoRemolcador"
                                  CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk"></span>
                  </asp:LinkButton>
                </center>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-primary" data-dismiss="modal">
                <span class="glyphicon glyphicon-remove"></span>
              </button>
            </div>
            <asp:RequiredFieldValidator ID="rfv_codigo" runat="server" ControlToValidate="txt_editCodigo"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="nuevoRemolcador">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_codigo">
            </asp:ValidatorCalloutExtender>

            <asp:RequiredFieldValidator ID="rfv_txt_editDesc" runat="server" ControlToValidate="txt_editDesc"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="nuevoRemolcador">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_txt_editDesc">
            </asp:ValidatorCalloutExtender>

            <asp:RequiredFieldValidator ID="rfv_ddl_editSite" runat="server" ControlToValidate="ddl_editSite"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" InitialValue="0"
                                        ValidationGroup="nuevoRemolcador">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_ddl_editSite">
            </asp:ValidatorCalloutExtender>
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div>
  </div>
  <!-- Modal Eliminación -->
  <div class="modal fade" id="modalConfirmacion" data-backdrop="static" role="dialog" >
    <div class="modal-dialog" role="dialog">
      <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
          <div class="modal-content">
            <div class="modal-content">
              <div class="modal-header">
                <h4>
                  Eliminar Remolcador
                </h4>
              </div>
              <div class="modal-body">
                Se eliminará el remolcador seleccionado, ¿desea continuar?
              </div>
              <div class="modal-footer">
                <asp:LinkButton ID="btnModalEliminar" OnClick="btn_EliminarRemolcador_Click" runat="server" 
                                CssClass="btn btn-primary" ToolTip="Eliminar" >
                  <span class="glyphicon glyphicon-ok"></span>
                </asp:LinkButton>
                <button type="button" data-dismiss="modal" class="btn btn-primary" >
                  <span class="glyphicon glyphicon-remove"></span>
                </button>
              </div>
          </div>

        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </div>
</asp:Content>
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" Runat="Server">
  <asp:UpdatePanel ID="UpdatePanel4" runat="server">
    <ContentTemplate>
      <asp:HiddenField ID="hf_idRemolcador" runat="server" />
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" Runat="Server">
  <script type="text/javascript">
        function modalEditarRemolcador() {
    $("#modalRemolcador").modal();
    }

        function modalConfirmacion() {
    $("#modalConfirmacion").modal();
    }
  </script>
</asp:Content>
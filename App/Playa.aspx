<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Playa.aspx.cs" Inherits="App_Playa" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-xs-12 separador">
  </div>
  <h2>
    Maestro Playa
  </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-xs-12">
    <div class="col-xs-1">
        Site
        <br />
        <asp:DropDownList ID="ddl_site" OnSelectedIndexChanged="ddl_site_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
        </asp:DropDownList>
    </div>
    <div class="col-xs-1">
      Código
      <br />
      <asp:TextBox ID="txt_buscarCodigo" runat="server" CssClass="form-control" Width="90%"></asp:TextBox>
    </div>
    <div class="col-xs-2">
      Nombre
      <br />
      <asp:TextBox ID="txt_buscarNombre" runat="server" CssClass="form-control" Width="90%"></asp:TextBox>
    </div>
    <div class="col-xs-2">
      Zona
      <br />
      <asp:DropDownList ID="ddl_buscarZona" CssClass="form-control" runat="server">
      </asp:DropDownList>
    </div>
    <div class="col-xs-2">
      Excepto virtuales
      <br />
      <asp:CheckBox ID="chk_buscarVirtual" runat="server" />
    </div>
    <div class="col-xs-2">
      <br />
      <asp:LinkButton ID="btn_buscarPlaya" OnClick="btn_buscarPlaya_Click" CssClass="btn btn-primary"
                      ToolTip="Buscar Playa" runat="server">
        <span class="glyphicon glyphicon-search"></span>
      </asp:LinkButton>
      <asp:LinkButton ID="btn_nuevaPlaya" CssClass="btn btn-primary" ToolTip="Nueva Playa"
                      runat="server" OnClick="btn_nuevaPlaya_Click">
        <span class="glyphicon glyphicon-plus"></span>
      </asp:LinkButton>
    </div>
  </div>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" Runat="Server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <div class="col-xs-12" style="max-height: 60vh; min-height: 60vh; padding-left: 0px;
           padding-right: 0px;">
        <asp:GridView ID="gv_listaPlaya" runat="server" AllowPaging="True" AllowSorting="True"
                      BorderColor="Black" CellPadding="8" DataKeyNames="ID" BackColor="White" GridLines="Horizontal"
                      EnableSortingAndPagingCallbacks="True" OnPageIndexChanging="gv_listaPlaya_PageIndexChanging1"
                      OnRowCommand="gv_listaPlaya_RowCommand" OnRowEditing="gv_listaPlaya_RowEditing" PageSize="6"
                      OnSorting="gv_listaPlaya_Sorting" EmptyDataText="No se encontraron Clientes!" AutoGenerateColumns="False"
                      Width="100%" PagerSettings-Mode="NumericFirstLast" CssClass="table table-bordered table-hover tablita"
                      OnRowDataBound="gv_listaPlaya_RowDataBound">
          <Columns>
            <asp:TemplateField HeaderText="Modificar" ShowHeader="False" ItemStyle-Width="5%"
                               ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
              <ItemTemplate>
                <asp:LinkButton ID="btn_modificarPlaya" runat="server" CausesValidation="false" CommandName="Edit"
                                CssClass="btn btn-primary" ToolTip="Modificar">
                  <span class="glyphicon glyphicon-pencil"></span>
                </asp:LinkButton>
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Eliminar" ShowHeader="False" ItemStyle-Width="5%"
                               Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
              <ItemTemplate>
                <asp:LinkButton ID="btn_eliminarPlaya" runat="server" CausesValidation="False" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID") %>'
                                CssClass="btn btn-primary" ToolTip="Eliminar">
                  <span class="glyphicon glyphicon-remove"></span>
                </asp:LinkButton>
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:BoundField ReadOnly="True" HeaderText="ID" DataField="ID" SortExpression="ID"
                            ItemStyle-Height="30px" Visible="false"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Código" DataField="CODIGO" SortExpression="CODIGO"
                            ItemStyle-Height="30px" ItemStyle-Width="10%" HeaderStyle-Width="10%" ></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Descripción" DataField="DESCRIPCION" SortExpression="DESCRIPCION"
                            ItemStyle-Width="10%" HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Virtual" DataField="VIRTUAL" SortExpression="VIRTUAL"
                            ItemStyle-Width="10%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Zona" DataField="ZONA" SortExpression="ZONA"
                            ItemStyle-Width="10%" HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Tipo" DataField="TIPO" SortExpression="TIPO"
                            ItemStyle-Width="10%" HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
            <%--                    <asp:BoundField ReadOnly="True" HeaderText="Posición X" DataField="PLAYA_X" SortExpression="PLAYA_X"
            ItemStyle-Width="10%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Posición Y" DataField="PLAYA_Y" SortExpression="PLAYA_Y"
            ItemStyle-Width="10%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Rotación" DataField="ROTACION" SortExpression="ROTACION"
            ItemStyle-Width="10%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>--%>
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
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" Runat="Server">
  <!-- modal Playa  ---->
  <div class="modal fade" id="modalPlaya" data-backdrop="static" role="dialog">
    <div class="modal-dialog">
      <div class="modal-content">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
          <ContentTemplate>
            <div class="modal-header">
              <h4 class="modal-title">
                Datos Playa
              </h4>
            </div>
            <div class="modal-body" style="height: auto; overflow: auto">
              <div class="col-xs-12">
                <div class="col-xs-2">
                  Código
                  <br />
                  <asp:TextBox ID="txt_editCodigo" CssClass="form-control input-mayus" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-8">
                  Descripción
                  <br />
                  <asp:TextBox ID="txt_editDesc" CssClass="form-control input-mayus" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-4">
                  Tipo
                  <br />
                  <asp:DropDownList ID="ddl_editTipo" CssClass="form-control" runat="server">
                    <asp:ListItem Value="0" Text="Seleccione..."></asp:ListItem>
                    <asp:ListItem Value="100" Text="Andén"></asp:ListItem>
                    <asp:ListItem Value="200" Text="Estacionamiento"></asp:ListItem>
                  </asp:DropDownList>                    
                </div>
              </div>
              <div class="col-xs-12 separador">
              </div>
              <div class="col-xs-12">
                <div class="col-xs-3">
                  Site
                  <br />
                  <asp:DropDownList ID="ddl_editSite" AutoPostBack="true" OnSelectedIndexChanged="ddl_editSite_SelectedIndexChanged" CssClass="form-control" runat="server">
                  </asp:DropDownList>
                </div>
                <div class="col-xs-6">
                  Zona
                  <br />
                  <asp:DropDownList ID="ddl_editZona" CssClass="form-control" runat="server">
                  </asp:DropDownList>
                </div>
                <div class="col-xs-2">
                  Virtual
                  <br />
                  <asp:CheckBox ID="chk_editVirtual" runat="server" />
                </div>
              </div>
              <div class="col-xs-12 separador">
              </div>
              <div class="col-xs-12">
                <center>
                  <asp:LinkButton ID="btn_editGrabar" runat="server" OnClick="btn_editGrabar_Click" ValidationGroup="nuevoPlaya"
                                  CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk"></span>
                  </asp:LinkButton>
                </center>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-default" data-dismiss="modal">
                Cerrar</button>
            </div>
            <asp:RequiredFieldValidator ID="rfv_Codigo" runat="server" ControlToValidate="txt_editCodigo"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="nuevoPlaya">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_Codigo">
            </asp:ValidatorCalloutExtender>
            <asp:RequiredFieldValidator ID="rfv_txt_editDesc" runat="server" ControlToValidate="txt_editDesc"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="nuevoPlaya">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_txt_editDesc">
            </asp:ValidatorCalloutExtender>
            <asp:RequiredFieldValidator ID="rfv_ddl_editZona" runat="server" ControlToValidate="ddl_editZona"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" InitialValue="0"
                                        ValidationGroup="nuevoPlaya">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_ddl_editZona">
            </asp:ValidatorCalloutExtender>
            <asp:RequiredFieldValidator ID="rfv_ddl_editTipo" runat="server" ControlToValidate="ddl_editTipo"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" InitialValue="0"
                                        ValidationGroup="nuevoPlaya">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_ddl_editTipo">
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
            <div class="modal-header">
              <h4>
                Eliminar Playa
              </h4>
            </div>
            <div class="modal-body">
              Se eliminará la playa seleccionada, ¿desea continuar?
            </div>
            <div class="modal-footer">
              <asp:LinkButton ID="btnModalEliminar" runat="server" tooltip="Eliminar"
                              CssClass="btn btn-primary" OnClick="btn_EliminarPlaya_Click">
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
  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
      <asp:HiddenField ID="hf_idPlaya" runat="server" />
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" Runat="Server">
  <script type="text/javascript">
      function modalEditarPlaya() {
        $("#modalPlaya").modal();
    }
    
    function modalConfirmacion() {
        $("#modalConfirmacion").modal();
    }

  </script>
</asp:Content>

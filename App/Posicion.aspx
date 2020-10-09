<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true"
CodeFile="Posicion.aspx.cs" Inherits="App_Posicion" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
  <div class="col-xs-12 separador">
  </div>
  <h2>
    Maestro Posición
  </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
  <div class="col-xs-15">
    <div class="col-xs-1">
      Código
      <br />
      <asp:TextBox ID="txt_buscarCodigo" runat="server" CssClass="form-control" Width="90%"></asp:TextBox>
    </div>
    <div class="col-xs-1">
      Nombre
      <br />
      <asp:TextBox ID="txt_buscarNombre" runat="server" CssClass="form-control" Width="90%"></asp:TextBox>
    </div>
    <asp:UpdatePanel runat="server">
      <ContentTemplate>
        <div class="col-xs-2">
          Site
          <br />
          <asp:DropDownList ID="ddl_buscarSite" CssClass="form-control" AutoPostBack="true"
                            OnSelectedIndexChanged="ddl_buscarSite_onChange" runat="server">
          </asp:DropDownList>
        </div>
        <div class="col-xs-2">
          Zona
          <br />
          <asp:DropDownList ID="ddl_zona" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_zona_onChange"
                            runat="server">
          </asp:DropDownList>
        </div>
        <div class="col-xs-2">
          Playa
          <br />
          <asp:DropDownList ID="ddl_playa" CssClass="form-control" runat="server">
          </asp:DropDownList>
        </div>
      </ContentTemplate>
    </asp:UpdatePanel>
    <div class="col-xs-1">
      Solo libres
      <br />
      <asp:CheckBox ID="chk_buscarOcupado" runat="server" />
    </div>
    <div class="col-xs-2">
      <br />
      <asp:LinkButton ID="btn_buscarPosicion" OnClick="btn_buscarPosicion_Click" CssClass="btn btn-primary"
                      ToolTip="Buscar Posicion" runat="server">
        <span class="glyphicon glyphicon-search"></span>
      </asp:LinkButton>
      <asp:LinkButton ID="btn_nuevoPosicion" CssClass="btn btn-primary" ToolTip="Nuevo Posicion"
                      runat="server" OnClick="btn_nuevoPosicion_Click">
        <span class="glyphicon glyphicon-plus"></span>
      </asp:LinkButton>
    </div>
  </div>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <div class="col-xs-12">
        <asp:GridView ID="gv_listar" runat="server" AllowPaging="True" AllowSorting="True" EnableViewState="false"
                      DataKeyNames="ID" OnPageIndexChanging="gv_listaPosicion_PageIndexChanging1"
                      OnRowCommand="gv_listaPosicion_RowCommand" OnRowEditing="gv_listaPosicion_RowEditing"
                      PageSize="8" OnSorting="gv_listaPosicion_Sorting" EmptyDataText="No se encontraron lugares!"
                      AutoGenerateColumns="False" Width="100%" PagerSettings-Mode="NumericFirstLast"
                      CssClass="table table-bordered table-hover tablita" OnRowDataBound="gv_listaPosicion_RowDataBound">
          <Columns>
            <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%">
              <ItemTemplate>
                <asp:LinkButton ID="btn_modificarPosicion" runat="server" CausesValidation="false"
                                CommandName="Edit" CssClass="btn btn-sm btn-primary" ToolTip="Modificar">
                  <span class="glyphicon glyphicon-pencil"></span>
                </asp:LinkButton>
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%">
              <ItemTemplate>
                <asp:LinkButton ID="btn_eliminarPosicion" runat="server" CausesValidation="False"
                                CommandName="ELIMINAR" CommandArgument='<%#Eval("ID")%>' CssClass="btn btn-sm btn-primary"
                                ToolTip="Eliminar">
                  <span class="glyphicon glyphicon-remove"></span>
                </asp:LinkButton>
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%">
              <ItemTemplate>
                <asp:LinkButton ID="btn_habilitarPosicion" runat="server" CausesValidation="False"
                                CommandName="HABILITAR" CommandArgument='<%#Eval("ID")%>' CssClass="btn btn-sm btn-primary"
                                ToolTip="Habilitar/Deshabilitar">
                  <span class="glyphicon glyphicon-off"></span>
                </asp:LinkButton>
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:BoundField ReadOnly="True" DataField="ID" Visible="false"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Código" DataField="CODIGO" SortExpression="CODIGO"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Descripción" DataField="DESCRIPCION" SortExpression="DESCRIPCION"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Estado" DataField="ESTADO" SortExpression="ESTADO" Visible="false"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Site" DataField="SITE" SortExpression="SITE"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Zona" DataField="ZONA" SortExpression="ZONA"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Playa" DataField="PLAYA" SortExpression="PLAYA"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Ocupado" DataField="OCUPADO" SortExpression="OCUPADO"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Estado" DataField="ESTADO_LUGAR" SortExpression="ESTADO_LUGAR"></asp:BoundField>
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
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
  <!-- modal Posicion  ---->
  <div class="modal fade" id="modalPosicion" data-backdrop="static" role="dialog">
    <div class="modal-dialog">
      <div class="modal-content">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
          <ContentTemplate>
            <div class="modal-header">
              <h4 class="modal-title">
                Datos Posición
              </h4>
            </div>
            <div class="modal-body" style="height: auto; overflow: auto">
              <div class="col-xs-12">
                <div class="col-xs-2">
                  Código
                  <br />
                  <asp:TextBox ID="txt_editCodigo" CssClass="form-control input-mayus" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-10">
                  Descripción
                  <br />
                  <asp:TextBox ID="txt_editDesc" CssClass="form-control input-mayus" runat="server"></asp:TextBox>
                </div>
              </div>
              <div class="col-xs-12 separador">
              </div>
              <div class="col-xs-12">
                <div class="col-xs-4">
                  Site
                  <br />
                  <asp:DropDownList ID="ddl_editSite" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_editSite_onChange"
                                    runat="server">
                  </asp:DropDownList>
                </div>
                <div class="col-xs-4">
                  Zona
                  <br />
                  <asp:DropDownList ID="ddl_editZona" CssClass="form-control" Enabled="false" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddl_editZona_onChange" runat="server">
                  </asp:DropDownList>
                </div>
                <div class="col-xs-4">
                  Playa
                  <br />
                  <asp:DropDownList CssClass="form-control" ID="ddl_editPlaya" Enabled="false" runat="server">
                    <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
                  </asp:DropDownList>
                </div>
              </div>
              <div class="col-xs-12 separador">
              </div>
              <%--
              <div class="col-xs-12">
              <div class="col-xs-3">
              Posición X
              <br />
              <asp:TextBox ID="txt_editPosX" CssClass="form-control input-double" runat="server"></asp:TextBox>
              <asp:RequiredFieldValidator ID="rfv_txt_editPosX" runat="server" ControlToValidate="txt_editPosX"
              Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
              ValidationGroup="nuevoPosicion">
              </asp:RequiredFieldValidator>
              <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
              Enabled="True" TargetControlID="rfv_txt_editPosX">
              </asp:ValidatorCalloutExtender>
              </div>
              <div class="col-xs-3">
              Posición Y
              <br />
              <asp:TextBox ID="txt_editPosY" CssClass="form-control input-double" runat="server"></asp:TextBox>
              <asp:RequiredFieldValidator ID="rfv_txt_editPosY" runat="server" ControlToValidate="txt_editPosY"
              Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
              ValidationGroup="nuevoPosicion">
              </asp:RequiredFieldValidator>
              <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
              Enabled="True" TargetControlID="rfv_txt_editPosY">
              </asp:ValidatorCalloutExtender>
              </div>
              <div class="col-xs-3">
              Posición
              <br />
              <asp:TextBox ID="txt_editPosicion" CssClass="form-control input-number" runat="server"></asp:TextBox>
              <asp:RequiredFieldValidator ID="rfv_txt_editPosicion" runat="server" ControlToValidate="txt_editPosicion"
              Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
              ValidationGroup="nuevoPosicion">
              </asp:RequiredFieldValidator>
              <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
              Enabled="True" TargetControlID="rfv_txt_editPosicion">
              </asp:ValidatorCalloutExtender>
              </div>
              </div>
              <div class="col-xs-12 separador">
              </div>
              <div class="col-xs-12">
              <div class="col-xs-3">
              Orden
              <br />
              <asp:TextBox ID="txt_editOrden" CssClass="form-control input-number" runat="server"></asp:TextBox>
              <asp:RequiredFieldValidator ID="rfv_txt_editOrden" runat="server" ControlToValidate="txt_editOrden"
              Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
              ValidationGroup="nuevoPosicion">
              </asp:RequiredFieldValidator>
              <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
              Enabled="True" TargetControlID="rfv_txt_editOrden">
              </asp:ValidatorCalloutExtender>
              </div>
              <div class="col-xs-3">
              Ancho
              <br />
              <asp:TextBox ID="txt_editAncho" CssClass="form-control input-double" runat="server"></asp:TextBox>
              <asp:RequiredFieldValidator ID="rfv_txt_editAncho" runat="server" ControlToValidate="txt_editAncho"
              Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
              ValidationGroup="nuevoPosicion">
              </asp:RequiredFieldValidator>
              <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
              Enabled="True" TargetControlID="rfv_txt_editAncho">
              </asp:ValidatorCalloutExtender>
              </div>
              <div class="col-xs-3">
              Alto
              <br />
              <asp:TextBox ID="txt_editAlto" CssClass="form-control input-double" runat="server"></asp:TextBox>
              <asp:RequiredFieldValidator ID="rfv_txt_editAlto" runat="server" ControlToValidate="txt_editAlto"
              Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
              ValidationGroup="nuevoPosicion">
              </asp:RequiredFieldValidator>
              <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
              Enabled="True" TargetControlID="rfv_txt_editAlto">
              </asp:ValidatorCalloutExtender>
              </div>
              <div class="col-xs-3">
              Rotación
              <br />
              <asp:TextBox ID="txt_editRotacion" CssClass="form-control input-number" Width="50px" runat="server"></asp:TextBox>
              <asp:RequiredFieldValidator ID="rfv_txt_editRotacion" runat="server" ControlToValidate="txt_editRotacion"
              Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
              ValidationGroup="nuevoPosicion">
              </asp:RequiredFieldValidator>
              <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server"
              Enabled="True" TargetControlID="rfv_txt_editRotacion">
              </asp:ValidatorCalloutExtender>
              </div>
              </div>
              <div class="col-xs-12 separador">
              </div>--%>
              <div class="col-xs-12">
                <center>
                  <asp:LinkButton ID="btn_editGrabar" runat="server" OnClick="btn_editGrabar_Click"
                                  ValidationGroup="nuevoPosicion" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk"></span>
                  </asp:LinkButton>
                </center>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" data-dismiss="modal" class="btn btn-primary" >
                <span class="glyphicon glyphicon-remove"></span>
              </button>
            </div>
            <asp:RequiredFieldValidator ID="rfv_Codigo" runat="server" ControlToValidate="txt_editCodigo"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" ValidationGroup="nuevoPosicion">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_Codigo">
            </asp:ValidatorCalloutExtender>
            <asp:RequiredFieldValidator ID="rfv_txt_editDesc" runat="server" ControlToValidate="txt_editDesc"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" ValidationGroup="nuevoPosicion">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_txt_editDesc">
            </asp:ValidatorCalloutExtender>
            <asp:RequiredFieldValidator ID="rfv_ddl_editPlaya" runat="server" ControlToValidate="ddl_editPlaya"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" InitialValue="0"
                                        ValidationGroup="nuevoPosicion">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_ddl_editPlaya">
            </asp:ValidatorCalloutExtender>
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div>
  </div>
  <!-- Modal Eliminación -->
  <div class="modal fade" id="modalConfirmacion" data-backdrop="static" role="dialog">
    <div class="modal-dialog" role="dialog">
      <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
          <div class="modal-content">
            <div class="modal-header">
              <h4>
                <asp:Label ID="lblRazonEliminacion" runat="server"></asp:Label>
              </h4>
            </div>
            <div class="modal-body">
              <asp:Label ID="msjEliminacion" runat="server"></asp:Label>
            </div>
            <div class="modal-footer">
              <asp:LinkButton ID="btnModalEliminar" runat="server" data-dismiss="modal" CssClass="btn btn-primary">
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
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" runat="Server">
  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
      <asp:HiddenField ID="hf_idPosicion" runat="server" />
      <asp:Button ID="btn_eliminarPosicion" OnClick="btn_EliminarPosicion_Click" runat="server"
                  Text="Button" />
      <asp:Button ID="btn_habilitarPosicion" OnClick="btn_habilitarPosicion_Click" runat="server"
                  Text="Button" />
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
  <script type="text/javascript">
    function modalEditarPosicion() {
        $("#modalPosicion").modal();
    }

    function modalConfirmacion() {
        $("#modalConfirmacion").modal();
    }

    function eliminarPosicion() {
        var clickButton = document.getElementById("<%= this.btn_eliminarPosicion.ClientID %>");
        clickButton.click();
    }

    function habilitarPosicion() {
        var clickButton = document.getElementById("<%= this.btn_habilitarPosicion.ClientID %>");
        clickButton.click();
    }
  </script>
</asp:Content>
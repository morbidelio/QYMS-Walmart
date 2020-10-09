<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Playa_Origen_Destino.aspx.cs" Inherits="Playa_Origen_Destino" %>

<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-lg-12 separador">
    </div>
    <h2>Asignación Origen/Destino por Playa
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-lg-12">
        <asp:Panel ID="SITE" runat="server">
            <div class="col-lg-2">
                Site
        <br />
                <asp:DropDownList ID="ddl_site" OnSelectedIndexChanged="ddl_site_IndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                </asp:DropDownList>
            </div>
        </asp:Panel>
        <div class="col-lg-1">
            Código
      <br />
            <asp:TextBox ID="txt_buscarCodigo" runat="server" CssClass="form-control" Width="90%" />
        </div>
        <div class="col-lg-2">
            Descripción
      <br />
            <asp:TextBox ID="txt_buscarDescripcion" runat="server" CssClass="form-control" Width="90%" />
        </div>
        <div class="col-lg-2">
            Zona
      <br />
            <asp:DropDownList ID="ddl_buscarZona" CssClass="form-control" runat="server">
            </asp:DropDownList>
        </div>
        <div class="col-lg-1">
            Solo Físicos
      <br />
            <asp:CheckBox ID="chk_buscarVirtual" runat="server" />
        </div>
        <div class="col-lg-1">
            <br />
            <asp:LinkButton ID="btn_buscarPlaya" OnClick="btn_buscarPlaya_Click" CssClass="btn btn-primary" ToolTip="Buscar Playa" runat="server">
        <span class="glyphicon glyphicon-search" />
            </asp:LinkButton>
            <asp:LinkButton ID="btn_nuevaPlaya" CssClass="btn btn-primary" ToolTip="Nueva Playa" runat="server" OnClick="btn_nuevaPlaya_Click">
        <span class="glyphicon glyphicon-plus" />
            </asp:LinkButton>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12">
                <asp:GridView ID="gv_listar" AllowPaging="True" AllowSorting="True" DataKeyNames="ID" runat="server"
                    PageSize="6" EmptyDataText="¡No se encontraron playas!" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-hover tablita"
                    OnRowCommand="gv_listar_RowCommand" OnRowEditing="gv_listar_RowEditing" OnSorting="gv_listar_Sorting" OnPageIndexChanging="gv_listar_PageIndexChanging1" OnRowDataBound="gv_listar_RowDataBound">
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_modificarPlaya" runat="server" CommandName="Edit" CssClass="btn btn-sm btn-primary" ToolTip="Modificar">
                  <span class="glyphicon glyphicon-pencil" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_destinosPlaya" runat="server" CommandName="DESTINO" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-sm btn-primary" ToolTip="Asignar Destinos">
                  <span class="glyphicon glyphicon-map-marker" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_eliminarPlaya" runat="server" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-sm btn-primary" ToolTip="Eliminar">
                  <span class="glyphicon glyphicon-remove" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:BoundField ReadOnly="True" HeaderText="ID" DataField="ID" Visible="false" />
                        <asp:BoundField ReadOnly="True" HeaderText="Código" DataField="CODIGO" SortExpression="CODIGO" />
                        <asp:BoundField ReadOnly="True" HeaderText="Descripción" DataField="DESCRIPCION" SortExpression="DESCRIPCION" />
                        <asp:BoundField ReadOnly="True" HeaderText="Virtual" DataField="VIRTUAL" SortExpression="VIRTUAL" />
                        <asp:BoundField ReadOnly="True" HeaderText="Zona" DataField="ZONA" SortExpression="ZONA" />
                        <asp:BoundField ReadOnly="True" HeaderText="Tipo" DataField="TIPO" SortExpression="TIPO" />
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
    <div class="modal fade" id="modalPlaya" data-backdrop="static" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Datos Tipo Playa
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-lg-2">
                                Código
                  <br />
                                <asp:TextBox ID="txt_editCodigo" CssClass="form-control input-mayus" runat="server" />
                            </div>
                            <div class="col-lg-10">
                                Descripción
                  <br />
                                <asp:TextBox ID="txt_editDesc" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-lg-12 separador">
                            </div>
                            <div class="col-lg-6">
                                Zona
                  <br />
                                <asp:DropDownList ID="ddl_editZona" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-4">
                                Tipo
                  <br />
                                <asp:DropDownList ID="ddl_editTipo" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0" Text="Seleccione..."></asp:ListItem>
                                    <asp:ListItem Value="100" Text="Andén"></asp:ListItem>
                                    <asp:ListItem Value="200" Text="Estacionamiento"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-2">
                                Virtual
                  <br />
                                <asp:CheckBox ID="chk_editVirtual" runat="server" />
                            </div>
                            <div class="col-lg-12 separador">
                            </div>
                            <div class="col-lg-12" style="text-align: center">
                                <asp:LinkButton ID="btn_editGrabar" runat="server" OnClick="btn_editGrabar_Click" ValidationGroup="nuevoPlaya" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-primary">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                        <asp:RequiredFieldValidator ID="rfv_Codigo" runat="server" ControlToValidate="txt_editCodigo"
                            Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" ValidationGroup="nuevoPlaya" />
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" PopupPosition="BottomLeft" Enabled="True" TargetControlID="rfv_Codigo" />
                        <asp:RequiredFieldValidator ID="rfv_txt_editDesc" runat="server" ControlToValidate="txt_editDesc"
                            Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" ValidationGroup="nuevoPlaya" />
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" PopupPosition="BottomLeft" Enabled="True" TargetControlID="rfv_txt_editDesc" />
                        <asp:RequiredFieldValidator ID="rfv_ddl_editZona" runat="server" ControlToValidate="ddl_editZona"
                            Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" InitialValue="0" ValidationGroup="nuevoPlaya" />
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" PopupPosition="BottomLeft" Enabled="True" TargetControlID="rfv_ddl_editZona" />
                        <asp:RequiredFieldValidator ID="rfv_ddl_editTipo" runat="server" ControlToValidate="ddl_editTipo"
                            Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" InitialValue="0" ValidationGroup="nuevoPlaya" />
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" PopupPosition="BottomLeft" Enabled="True" TargetControlID="rfv_ddl_editTipo" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalConfirmacion" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>
                                <asp:Label ID="lblRazonEliminacion" runat="server" />
                            </h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="msjEliminacion" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btnModalEliminar" runat="server" data-dismiss="modal" CssClass="btn btn-primary">
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

    <div class="modal fade" id="modalDestino" data-backdrop="static" role="dialog">
        <div class="modal-dialog" style="width: 1200px" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Destinos Playa
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-lg-3">
                                Zona
                <br />
                                <asp:DropDownList ID="ddl_destinoZona" OnSelectedIndexChanged="ddl_destinoZona_IndexChanged" CssClass="form-control" AutoPostBack="true" runat="server">
                                    <asp:ListItem Value="0" Text="Seleccione..." />
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-3">
                                Tipo Carga
                <br />
                                <asp:DropDownList ID="ddl_destinoTipoCarga" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0" Text="Sin Carga" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-12 separador"></div>
                            <div class="col-lg-6" style="overflow-y: scroll; height: 30vh">
                                <asp:GridView ID="gv_noSeleccionados" CssClass="table table-bordered table-hover tablita" AutoGenerateColumns="false"
                                    Width="100%" OnRowCommand="gv_noSeleccionados_RowCommand" runat="server" EmptyDataText="No hay playas para seleccionar!">
                                    <Columns>
                                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Playa" />
                                        <asp:BoundField DataField="ZONA" HeaderText="Zona" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_agregar" CommandName="SEL" CommandArgument='<%# Container.DataItemIndex %>' runat="server">
                            <span class="glyphicon glyphicon-menu-right" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-lg-6" style="overflow-y: scroll; height: 30VH">
                                <asp:GridView ID="gv_seleccionados" CssClass="table table-bordered table-hover tablita" AutoGenerateColumns="false" EmptyDataText="No hay playas seleccionadas!"
                                    Width="100%" OnRowCommand="gv_seleccionados_RowCommand" OnRowDataBound="gv_seleccionados_RowDataBound" runat="server">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_quitar" CommandName="SEL" CssClass="btn btn-primary btn-sm" CommandArgument='<%# Container.DataItemIndex %>' runat="server">
                            <span class="glyphicon glyphicon-erase" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_subir" CommandName="ARRIBA" CommandArgument='<%# Container.DataItemIndex %>' runat="server">
                            <span class="glyphicon glyphicon-menu-up" />
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btn_bajar" CommandName="ABAJO" CommandArgument='<%# Container.DataItemIndex %>' runat="server">
                            <span class="glyphicon glyphicon-menu-down" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DESTINO" HeaderText="Playa Destino" />
                                        <asp:BoundField DataField="ZONA_DESTINO" HeaderText="Zona Destino" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-lg-12 separador"></div>
                        <div class="col-lg-12" style="text-align: center">
                            <asp:LinkButton ID="btn_asignarGuardar" runat="server" CssClass="btn btn-primary" OnClick="btn_asignarGuardar_Click">
                    <span class="glyphicon glyphicon-floppy-disk" />
                            </asp:LinkButton>
                        </div>
                        <div class="modal-footer">
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
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_idPlaya" runat="server" />
            <asp:HiddenField ID="hf_seleccionados" runat="server" />
            <asp:HiddenField ID="hf_idSite" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        function modalPlaya() {
            $("#modalPlaya").modal();
        }

        function modalConfirmacion() {
            $("#modalConfirmacion").modal();
        }

        function modalDestino() {
            $("#modalDestino").modal();
        }

    </script>
</asp:Content>

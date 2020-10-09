<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="SiteV2.aspx.cs" Inherits="App_Site" %>

<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-lg-12 separador">
    </div>
    <h2>Maestro Site
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-lg-2">
        Nombre
    <br />
        <asp:TextBox ID="txt_buscarNombre" runat="server" CssClass="form-control" Width="100px" />
    </div>
    <div class="col-lg-2">
        Empresa
    <br />
        <asp:DropDownList ID="ddl_buscarEmpresa" runat="server" CssClass="form-control">
        </asp:DropDownList>
    </div>
    <div class="col-lg-1">
        <br />
        <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" ToolTip="Buscar Cliente" runat="server">
      <span class="glyphicon glyphicon-search" />
        </asp:LinkButton>
        <asp:LinkButton ID="btn_nuevo" CssClass="btn btn-primary" ToolTip="Nuevo Site" runat="server" OnClick="btn_nuevo_Click">
      <span class="glyphicon glyphicon-plus" />
        </asp:LinkButton>
    </div>

</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-8 col-lg-push-2">
                <asp:GridView ID="gv_listar" runat="server" AllowPaging="True" AllowSorting="True" PageSize="8" EmptyDataText="No se encontraron Clientes!"
                    OnPageIndexChanging="gv_listar_PageIndexChanging1" OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting" OnRowDataBound="gv_listar_RowDataBound"
                    AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-hover tablita">
                    <Columns>
                        <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_modificarSite" runat="server" CommandName="EDITAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Modificar">
                    <span class="glyphicon glyphicon-pencil" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_virtualSite" runat="server" CommandName="VIRTUAL" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Asignar Virtual">
                    <span class="glyphicon glyphicon-alert" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_eliminarSite" runat="server" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Eliminar">
                    <span class="glyphicon glyphicon-remove" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_trailerAuto" runat="server" CommandName="ACTIVAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Auto/Manual">
                    <span class="glyphicon glyphicon-random" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Nombre" DataField="NOMBRE" SortExpression="NOMBRE" />
                        <asp:BoundField HeaderText="Descripción" DataField="DESCRIPCION" SortExpression="DESCRIPCION" />
                        <asp:BoundField HeaderText="Empresa" DataField="EMPRESA" SortExpression="EMPRESA" />
                        <asp:BoundField HeaderText="Trailer Automático" DataField="TRAILER_AUTO" SortExpression="TRAILER_AUTO" />
                        <asp:BoundField HeaderText="Codigo SAP" DataField="COD_SAP" SortExpression="COD_SAP" />
                    </Columns>
                    <PagerTemplate>
                        Página
            <asp:DropDownList ID="paginasDropDownList" Font-Size="12px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="GoPage">
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
    <div class="modal fade" id="modalSite" data-backdrop="static" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">
							Datos Site
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-lg-3">
                                Nombre
                <br />
                                <asp:TextBox ID="txt_editNombre" CssClass="form-control input-mayus" runat="server" />
                            </div>
                            <div class="col-lg-3">
                                Descripción
                <br />
                                <asp:TextBox ID="txt_editDesc" CssClass="form-control input-mayus" runat="server" />
                            </div>
                            <div class="col-lg-3">
                                Empresa
                <br />
                                <asp:DropDownList ID="ddl_editEmpresa" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-3">
                                Codigo SAP
                <br />
                                <asp:TextBox ID="txt_editCodSap" CssClass="form-control input-number" runat="server" />
                            </div>
                            <div class="col-lg-12 separador">
                            </div>
                            <div class="col-lg-12" style="text-align: center">
                                <asp:LinkButton ID="btn_editGrabar" runat="server" OnClick="btn_editGrabar_Click" ValidationGroup="nuevo" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-primary">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                        <asp:RequiredFieldValidator ID="rfv_txtEditNombre" runat="server" ControlToValidate="txt_editNombre"
                            Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" ValidationGroup="nuevo" />
                        <asp:ValidatorCalloutExtender ID="txt_nombreEdita_ValidatorCalloutExtender" runat="server" PopupPosition="BottomLeft" TargetControlID="rfv_txtEditNombre" />
                        <asp:RequiredFieldValidator ID="rfv_ddlEditEmpresa" runat="server" ControlToValidate="ddl_editEmpresa" InitialValue="0"
                            Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" ValidationGroup="nuevo" />
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" PopupPosition="BottomLeft" TargetControlID="rfv_ddlEditEmpresa" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalVirtual" data-backdrop="static" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Definir playa virtual
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-lg-3">
                                Zona
                <br />
                                <asp:DropDownList ID="ddl_virtualZona" OnSelectedIndexChanged="ddl_virtualZona_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true" runat="server">
                                    <asp:ListItem Value="0" Text="Seleccione..." />
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-3">
                                Playa
                <br />
                                <asp:DropDownList ID="ddl_virtualPlaya" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0" Text="Seleccione..." />
                                </asp:DropDownList>
                            </div>

                            <div class="col-lg-12 separador">
                            </div>
                            <div class="col-lg-12" style="text-align: center;">
                                <asp:LinkButton ID="btn_virtualGuardar" runat="server" OnClick="btn_virtualGuardar_Click" ValidationGroup="gvirtual" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-primary">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                        <asp:RequiredFieldValidator ID="rfv_playa" runat="server" ControlToValidate="ddl_virtualPlaya"
                            Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" ValidationGroup="gvirtual" />
                        <asp:ValidatorCalloutExtender ID="vce_playa" runat="server" PopupPosition="BottomLeft" TargetControlID="rfv_playa" />
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
                            <asp:LinkButton ID="btnModalEliminar" OnClick="btn_EliminarSite_Click" runat="server" ToolTip="Eliminar" CssClass="btn btn-primary">
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
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_idSite" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        function modalSite() {
            $("#modalSite").modal();
        }

        function modalConfirmacion() {
            $("#modalConfirmacion").modal();
        }

        function modalVirtual() {
            $("#modalVirtual").modal();
        }

    </script>
</asp:Content>

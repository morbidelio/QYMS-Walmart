<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Remo_Prog_Distribuicion.aspx.cs" Inherits="App_Remo_Prog_Distribuicion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-lg-12 separador"></div>
    <h2>Asignación Zonas/Playas
    </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-lg-12 separador"></div>
    <div class="col-lg-2">
        Site
    <br />
        <asp:DropDownList ID="ddl_site" CssClass="form-control" OnSelectedIndexChanged="ddl_site_IndexChanged" AutoPostBack="true" runat="server">
        </asp:DropDownList>
    </div>
    <div class="col-lg-1">
        <br />
        <asp:LinkButton ID="btn_nuevo" OnClick="btn_nuevo_Click" CssClass="btn btn-primary" runat="server" ToolTip="Agregar">
      <span class="glyphicon glyphicon-plus" />
        </asp:LinkButton>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <div class="col-lg-12 separador"></div>
    <div class="col-lg-push-2 col-lg-8">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:GridView ID="gv_listar" CssClass="table table-hover table-bordered tablita" EmptyDataText="No existen remolcadores asignados" EnableViewState="false"
                    OnRowCommand="gv_listar_RowCommand" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_editar" runat="server" CommandName="EDITAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-primary" ToolTip="Modificar">
                    <span class="glyphicon glyphicon-pencil" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_activar" runat="server" CommandName="ACTIVAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-primary" ToolTip="Activar / Desactivar">
                    <span class="glyphicon glyphicon-off" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_asignar" runat="server" CommandName="ASIGNAR" CssClass="btn btn-primary" ToolTip="Asignar Playas" CommandArgument='<%# Eval("ID") %>'>
                    <span class="glyphicon glyphicon-tags" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción" />
                        <asp:BoundField DataField="ACTIVA" HeaderText="Activo" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
    <div class="modal fade" id="modalRemoProg" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Datos Programación Remolcador
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-lg-4">
                                Site
                  <br />
                                <asp:DropDownList ID="ddl_editSite" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0" Text="Seleccione..." />
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-6">
                                Descripción
                  <br />
                                <asp:TextBox ID="txt_editDesc" CssClass="form-control input-mayus" runat="server" />
                            </div>
                            <div class="col-lg-12 separador"></div>
                            <div class="col-lg-12" style="text-align: center">
                                <asp:LinkButton ID="btn_editGuardar" CssClass="btn btn-primary" ToolTip="Guardar"
                                    runat="server" OnClick="btn_editGuardar_Click">
                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                            </div>
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

    <div class="modal fade" id="modalAsignar" data-backdrop="static" role="dialog">
        <div class="modal-dialog" style="width: 1300px" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Asignar Playas Remolcador
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-lg-5">
                                Zona
                  <br />
                                <asp:DropDownList ID="ddl_asignarZona" CssClass="form-control" Width="300" OnSelectedIndexChanged="ddl_asignarZona_IndexChanged" AutoPostBack="true" runat="server">
                                    <asp:ListItem Value="0" Text="Seleccione..." />
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-12 separador"></div>
                            <div class="col-lg-5" style="overflow-y: auto; min-height: 20vh; max-height: 50vh">
                                <asp:GridView ID="gv_noSeleccionados" CssClass="table table-bordered table-hover tablita" AutoGenerateColumns="false"
                                    Width="100%" OnRowCommand="gv_noSeleccionados_RowCommand" runat="server" EmptyDataText="No hay playas para seleccionar!">
                                    <Columns>
                                        <%--<asp:BoundField DataField="PLAY_ID" visible="false" />--%>
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
                            <div class="col-lg-7" style="overflow-y: auto; min-height: 20vh; max-height: 50vh">
                                <asp:GridView ID="gv_seleccionados" CssClass="table table-bordered table-hover tablita" AutoGenerateColumns="false" EmptyDataText="No hay playas seleccionadas!"
                                    Width="100%" OnRowCommand="gv_seleccionados_RowCommand" OnRowDataBound="gv_seleccionados_RowDataBound" runat="server">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_quitar" CommandName="ELIMINAR" CssClass="btn btn-primary btn-sm" CommandArgument='<%# Container.DataItemIndex %>' runat="server">
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
                                        <%--<asp:BoundField DataField="ORDEN" HeaderText="Orden" />--%>
                                        <%--<asp:BoundField DataField="ORDEN_OLD" HeaderText="Orden Old" />--%>
                                        <asp:BoundField DataField="PLAYA" HeaderText="Playa" />
                                        <asp:BoundField DataField="ZONA" HeaderText="Zona" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-lg-12 separador"></div>
                            <div class="col-lg-12" style="text-align: center">
                                <asp:LinkButton ID="btn_asignarGuardar" runat="server" CssClass="btn btn-primary" OnClick="btn_asignarGuardar_Click">
                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                            </div>
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
    <div class="modal fade" id="modalConfirmar" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Activar / Desactivar
                            </h4>
                        </div>
                        <div class="modal-body">
                            Se activará/desactivará la programación/distribuición remolcador seleccionada. ¿Desea continuar?
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btn_Conf" runat="server" OnClick="btn_Conf_Click" CssClass="btn btn-primary">
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
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_idRPD" runat="server" />
            <asp:HiddenField ID="hf_seleccionados" runat="server" />
            <asp:HiddenField ID="hf_idSite" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        function modalRemoProg() {
            $("#modalRemoProg").modal();
        }
        function modalAsignar() {
            $("#modalAsignar").modal();
        }
        function modalConfirmar() {
            $("#modalConfirmar").modal();
        }
    </script>
</asp:Content>

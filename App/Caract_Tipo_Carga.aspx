<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Caract_Tipo_Carga.aspx.cs" Inherits="App_Caract_Tipo_Carga" %>

<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-xs-12 separador">
    </div>
    <h2>Maestro Características de tipo Carga
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-xs-2">
        Descripción
      <br />
        <asp:TextBox ID="txt_buscarNombre" runat="server" CssClass="form-control" Width="100px" />
    </div>
    <div class="col-xs-2">
        <br />
        <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" ToolTip="Buscar CaractTipoCarga" runat="server">
        <span class="glyphicon glyphicon-search" />
        </asp:LinkButton>
        <asp:LinkButton ID="btn_nuevo" OnClick="btn_nuevo_Click" CssClass="btn btn-primary" ToolTip="Nuevo CaractTipoCarga" runat="server">
        <span class="glyphicon glyphicon-plus" />
        </asp:LinkButton>
    </div>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-xs-12">
                <asp:GridView ID="gv_listar" runat="server" AllowSorting="True" DataKeyNames="ID" Width="100%"
                    OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting" OnRowCreated="gv_listar_RowCreated"
                    EmptyDataText="No se encontraron tipos de características trailer!" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tablita">
                    <Columns>
                        <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_modificar" runat="server" CommandName="MODIFICAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Modificar">
                  <span class="glyphicon glyphicon-pencil" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_eliminar" runat="server" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Eliminar">
                  <span class="glyphicon glyphicon-remove" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="ID" DataField="ID" SortExpression="ID" />
                        <asp:BoundField HeaderText="Excluyente" DataField="EXCLUYENTE" SortExpression="EXCLUYENTE" />
                        <asp:BoundField HeaderText="Descripción" DataField="DESCRIPCION" SortExpression="DESCRIPCION" />
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
    <!-- modal CaractTipoCarga  ---->
    <div class="modal fade" id="modalEdit" data-backdrop="static" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Datos Tipo Características Trailer
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-xs-2">
                                ID
                  <br />
                                <asp:TextBox ID="txt_editId" runat="server" CssClass="form-control input-number" />
                            </div>
                            <div class="col-xs-2">
                                Excluyente
                  <br />
                                <asp:CheckBox ID="chk_editExcluyente" runat="server" />
                            </div>
                            <div class="col-xs-8">
                                Descripción
                <br />
                                <asp:TextBox ID="txt_editDesc" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-xs-12 separador">
                            </div>
                            <div class="col-xs-12" style="text-align: center">
                                <asp:LinkButton ID="btn_editGrabar" runat="server" OnClick="btn_editGrabar_Click" ValidationGroup="nuevoCaractTipoCarga" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-primary"><span class="glyphicon glyphicon-remove" /></button>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Modal Eliminación -->
    <div class="modal fade" id="modalConf" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Eliminar características de tipo carga
                            </h4>
                        </div>
                        <div class="modal-body">
                            Se eliminarán características de tipo carga seleccionado, ¿desea continuar?
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btnModalEliminar" runat="server" CssClass="btn btn-primary" OnClick="btn_EliminarCaractTipoCarga_Click">
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
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_idCaractTipoCarga" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);

        var pageScrollPos = 0;
        var pageScrollPosleft = 0;

        function BeginRequestHandler(sender, args) {
            pageScrollPos = $('div.dataTables_scrollBody').scrollTop();
            pageScrollPosleft = $('div.dataTables_scrollBody').scrollLeft();
        }
        var calcDataTableHeight = function () {
            return $(window).height() - $("#scrolls").offset().top - 100;
        };

        function reOffset1() {

            $('div.dataTables_scrollBody').height(calcDataTableHeight());
        }

        window.onresize = function (e) {
            reOffset1();
        }

        function tabla() {
            if ($('#<%= this.gv_listar.ClientID %>')[0] != undefined && $('#<%= this.gv_listar.ClientID %>')[0].rows.length > 1) {
                $('#<%= this.gv_listar.ClientID %>').DataTable({
                    "scrollY": calcDataTableHeight(),
                    "scrollX": true,
                    "scrollCollapse": true,
                    "paging": false,
                    "ordering": false,
                    "searching": false,
                    "lengthChange": false,
                    "info": false
                });
                $('div.dataTables_scrollBody').scrollTop(pageScrollPos);
                $('div.dataTables_scrollBody').scrollLeft(pageScrollPosleft);
            }
        }

        function EndRequestHandler1(sender, args) {
            $("#<%= btn_editGrabar.ClientID %>").click(function () {
                if ($("#<%=this.txt_editId.ClientID %>").val() == '') {
                    showAlertClass('crear', 'warn_idVacio');
                    return false;
                }
                if ($("#<%=this.txt_editDesc.ClientID %>").val() == '') {
                    showAlertClass('crear', 'warn_descripcionVacio');
                    return false;
                }
            });
            tabla();
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler1);
    </script>
</asp:Content>

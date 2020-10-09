<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Reporte_Preingreso.aspx.cs" Inherits="App_Reporte_Preingreso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-xs-12 separador">
  </div>
  <h2>
    Reporte Pre-Ingreso
  </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-xs-12 separador">
    </div>
    <div class="col-xs-1">
        Site
    <br />
        <asp:DropDownList ID="ddl_site" CssClass="form-control" runat="server">
        </asp:DropDownList>
    </div>
    <div class="col-xs-1">
        Fecha Desde
    <br />
        <asp:TextBox ID="txt_buscarDesde" CssClass="form-control input-fecha" runat="server" />
    </div>
    <div class="col-xs-1">
        Fecha Hasta
    <br />
        <asp:TextBox ID="txt_buscarHasta" CssClass="form-control input-fecha" runat="server" />
    </div>
    <div class="col-xs-2">
        Proveedor
    <br />
        <asp:DropDownList ID="ddl_buscarProveedor" CssClass="form-control" runat="server">
            <asp:ListItem Value="0" Text="Todos..." />
        </asp:DropDownList>
    </div>
    <div class="col-xs-2">
        Número
    <br />
        <asp:TextBox ID="txt_numero" CssClass="form-control" runat="server" />
    </div>
    <div class="col-xs-1">
        <br />
        <asp:LinkButton ID="btn_buscar" CssClass="btn btn-primary" OnClick="btn_buscar_Click" runat="server">
      <span class="glyphicon glyphicon-search" />
        </asp:LinkButton>
        <asp:LinkButton ID="btn_exportar" CssClass="btn btn-primary" OnClick="btn_export_Click" runat="server">
      <span class="glyphicon glyphicon-export" />
        </asp:LinkButton>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-xs-12">
                <asp:GridView ID="gv_listar" UseAccessibleHeader="true" OnRowCreated="gv_listar_OnRowCreated" OnSorting="gv_listar_Sorting" EmptyDataText="No hay preingresos!"
                    CssClass="table table-bordered table-hover tablita" AllowSorting="true" AutoGenerateColumns="false" runat="server"
                    OnRowCommand="gv_listaTrailer_RowCommand" ViewStateMode="Disabled" OnRowDataBound="gv_listaTrailer_RowDataBound">
                    <Columns>
                        <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_eliminarRemolcador" runat="server" CausesValidation="False"
                                    CommandName="ELIMINAR" CommandArgument='<%# Eval("PRING_ID") %>' CssClass="btn btn-xs btn-primary"
                                    ToolTip="Eliminar">
                  <span class="glyphicon glyphicon-remove" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PRING_ID" SortExpression="PRING_ID" HeaderText="ID" />
                        <asp:BoundField DataField="SITE" SortExpression="SITE" HeaderText="SITE" ItemStyle-Width="20%" HeaderStyle-Width="20%" />
                        <asp:BoundField DataField="CITA" SortExpression="CITA" HeaderText="N° Cita" />
                        <asp:BoundField DataField="FH_PREINGRESO" SortExpression="FH_PREINGRESO" HeaderText="Fecha/Hora" />
                        <asp:BoundField DataField="PROVEEDOR" SortExpression="PROVEEDOR" HeaderText="Proveedor" />
                        <asp:BoundField DataField="TIPO_CARGA" SortExpression="TIPO_CARGA" HeaderText="Tipo Carga" />
                        <asp:BoundField DataField="PATENTE" SortExpression="PATENTE" HeaderText="Patente" />
                        <asp:BoundField DataField="TRACTO" SortExpression="TRACTO" HeaderText="TRACTO" />
                        <asp:BoundField DataField="RUT_CONDUCTOR" SortExpression="RUT_CONDUCTOR" HeaderText="Rut Conductor" />
                        <asp:BoundField DataField="NOMBRE_CONDUCTOR" SortExpression="NOMBRE_CONDUCTOR" HeaderText="Nombre Conductor" />
                    </Columns>
                    <HeaderStyle CssClass="header-color2" />
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="modal fade" id="modalConfirmar" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Eliminar Pre-Ingreso
                            </h4>
                        </div>
                        <div class="modal-body">
                            Se eliminará el Pre-Ingreso seleccionado, ¿desea continuar?
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btnModalEliminar" runat="server" OnClick="btn_Eliminar_Click"
                                CssClass="btn btn-primary">
                <span class="glyphicon glyphicon-ok" />
                            </asp:LinkButton>
                            <button type="button" class="btn btn-primary" data-dismiss="modal">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnExportar" runat="server" CssClass="ocultar" OnClick="btnExportar_Click" />
            <asp:HiddenField ID="hf_preingreso" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportar" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript" src="../js2/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" href="../js2/css/defaultTheme.css" media="screen" />
    <script type="text/javascript">

        var calcDataTableHeight = function () {
            return $(window).height() - $("#scrolls").offset().top - 90;
        };
        function modalConfirmacion() {
            $("#modalConfirmar").modal();
        }
        function EndRequestHandler(sender, args) {
            setTimeout(tabla, 100);
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);

        function Exportar() {
            $get("<%=this.btnExportar.ClientID %>").click();
        }

        function tabla() {
            if ($('#<%= this.gv_listar.ClientID %>')[0] != undefined && $('#<%= this.gv_listar.ClientID %>')[0].rows.length > 1) {
                $('#<%= this.gv_listar.ClientID %>').DataTable({
                    "scrollY": calcDataTableHeight(),
                    "scrollX": true,
                    "paging": false,
                    "ordering": false,
                    "searching": false,
                    "lengthChange": false,
                    "info": false
                });
            }
        }
    </script>
</asp:Content>


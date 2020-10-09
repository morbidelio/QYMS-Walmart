<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true"
    CodeFile="Ver_PreIngresos.aspx.cs" Inherits="App_Ver_PreIngreso" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-lg-12 separador">
    </div>
    <h2>Listado Pre-Ingresos
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-lg-12 separador">
    </div>
    <div class="container-fluid filtro">

        <div class="col-lg-2">
            SITE
      <br />
            <asp:DropDownList ID="ddl_buscarSite" CssClass="form-control" Width="90%" runat="server">
            </asp:DropDownList>
        </div>
        <div class="col-lg-2">
            PROVEEDOR
      <br />
            <asp:DropDownList ID="ddl_buscarProveedor" CssClass="form-control" Width="90%" runat="server">
            </asp:DropDownList>
        </div>
        <div class="col-lg-1">
            Fecha Desde
      <br />
            <asp:TextBox ID="txt_buscarDesde" CssClass="form-control input-fecha" runat="server" />
        </div>
        <div class="col-lg-1">
            Fecha Hasta
      <br />
            <asp:TextBox ID="txt_buscarHasta" CssClass="form-control input-fecha" runat="server" />
        </div>
        <div class="col-lg-2">
            <br />
            <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary"
                ToolTip="Buscar Trailer" runat="server">
        <span class="glyphicon glyphicon-search" />
            </asp:LinkButton>
            <asp:LinkButton ID="btn_export" ToolTip="Exportar a Excel" CssClass="btn btn-primary"
                OnClick="btn_export_Click" runat="server">
        <span class="glyphicon glyphicon-import" />
            </asp:LinkButton>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server" class="container-fluid col-lg-12 cuerpo">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12">
                <asp:GridView ID="gv_listar" runat="server" AllowPaging="false" AllowSorting="true" UseAccessibleHeader="true" DataKeyNames="numero"
                    OnRowCommand="gv_listar_RowCommand" OnRowCreated="gv_listar_RowCreated" OnRowDataBound="gv_listar_RowDataBound" OnSorting="gv_listar_Sorting"
                    EmptyDataText="No se encontraron pre-ingresos!" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-hover tablita">
                    <Columns>
                        <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_pdf" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="pdf" CssClass="btn btn-xs btn-primary" ToolTip="Descargar" ClientIDMode="AutoID">
                  <span class="glyphicon glyphicon-arrow-down" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_eliminar" runat="server" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Eliminar">
                  <span class="glyphicon glyphicon-remove" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Id" DataField="NUMERO" SortExpression="ID" Visible="false" />
                        <asp:BoundField HeaderText="N° PRE-INGRESO" DataField="NUMERO" SortExpression="NUMERO" ItemStyle-Width="10%" />
                        <asp:BoundField HeaderText="SITE" DataField="SITE" SortExpression="SITE" ItemStyle-Width="20%" HeaderStyle-Width="20%" />
                        <asp:BoundField HeaderText="PROVEEDOR" DataField="PROVEEDOR" SortExpression="PROVEEDOR" ItemStyle-Width="10%" />
                        <asp:BoundField HeaderText="TRAILER" DataField="TRAILER" SortExpression="TRAILER" ItemStyle-Width="10%" HeaderStyle-Width="10%" Visible="false" />
                        <asp:BoundField HeaderText="FECHA" DataField="FECHA_PREINGRESO" SortExpression="FECHA_PREINGRESO" ItemStyle-Width="10%" HeaderStyle-Width="10%" />
                        <asp:BoundField HeaderText="ESTADO" DataField="ESTADO" SortExpression="ESTADO" ItemStyle-Width="30%" HeaderStyle-Width="10%" />
                        <asp:BoundField HeaderText="SELLO CARGA" DataField="SELLO_CARGA" SortExpression="SELLO_CARGA" ItemStyle-Width="20%" HeaderStyle-Width="10%" />
                        <asp:BoundField HeaderText="RUT CHOFER" DataField="RUT_CHOFER" SortExpression="RUT_CHOFER" ItemStyle-Width="20%" HeaderStyle-Width="10%" />
                        <asp:BoundField HeaderText="NOMBRE CHOFER" DataField="NOMBRE_CHOFER" SortExpression="NOMBRE_CHOFER" ItemStyle-Width="20%" HeaderStyle-Width="10%" />
                        <asp:BoundField HeaderText="RUT ACOMPAÑANTE" DataField="RUT_ACOMPAÑANTE" SortExpression="RUT_ACOMPAÑANTE" ItemStyle-Width="20%" HeaderStyle-Width="10%" />
                        <asp:BoundField HeaderText="TRACTO" DataField="TRACTO" SortExpression="TRACTO" ItemStyle-Width="20%" HeaderStyle-Width="10%" />
                        <asp:BoundField HeaderText="OBSERVACION" DataField="OBSERVACION" SortExpression="OBSERVACION" Visible="false" ItemStyle-Width="20%" HeaderStyle-Width="10%" />
                        <asp:BoundField HeaderText="TIPO INGRESO CARGA" DataField="TIPO_INGRESO_CARGA" SortExpression="TIPO_INGRESO_CARGA" ItemStyle-Width="20%" HeaderStyle-Width="10%" />
                        <asp:BoundField HeaderText="CONDUCTOR" DataField="CONDUCTOR" SortExpression="CONDUCTOR" ItemStyle-Width="20%" HeaderStyle-Width="10%" />
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="display: none">
        <asp:Panel ID="pnlReport" runat="server" Visible="false">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="BarCode 128" Font-Size="24pt"
                InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="BarCode 128" WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="App\reportes\PreIngreso.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </asp:Panel>
    </div>
    <!-- Modal Eliminación -->
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
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_idTrailer" runat="server" />
            <asp:HiddenField ID="hf_excluyentes" Value="" runat="server" />
            <asp:HiddenField ID="hf_noexcluyentes" Value="" runat="server" />
            <asp:HiddenField ID="hf_preingreso" runat="server" />
            <asp:HiddenField ID="hf_id" runat="server" />
            <asp:Button ID="btnExportar" runat="server" CssClass="ocultar" OnClick="btnExportar_Click" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportar" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript" src="../js2/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" href="../js2/css/defaultTheme.css" media="screen" />
    <script type="text/javascript">
        function EndRequestHandler(sender, args) {
            tabla();
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);

        function tabla() {
            if ($('#<%= this.gv_listar.ClientID %>')[0] != undefined && $('#<%= this.gv_listar.ClientID %>')[0].rows.length > 1) {
              $('#<%= this.gv_listar.ClientID %>').DataTable({
                    "scrollY": "60vh",
                    "scrollX": true,
                    "scrollCollapse": true,
                    "paging": false,
                    "ordering": false,
                    "searching": false,
                    "lengthChange": false
                });
            }
        }

        function modalEditarTrailer() {
            $("#modalTrailer").modal();
        }

        function modalConfirmacion() {
            $("#modalConfirmar").modal();
        }

        function Exportar() {
            $get("<%=this.btnExportar.ClientID %>").click();
        }

        $(document).ready(function () {
            $(window).keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    var button = document.getElementById("<% =this.btn_buscar.ClientID %>");
                button.click();
                return true;
            }
        });
    });
    </script>
</asp:Content>

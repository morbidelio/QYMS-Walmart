<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Preingreso_Reserva.aspx.cs" Inherits="App_Preingreso_Reserva" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-lg-12 separador"></div>
    <h2>Maestro Reservas Pre-Ingreso
    </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-lg-12 separador"></div>
    <div class="col-lg-1">
        CD
        <br />
        <asp:DropDownList ID="ddl_buscarSite" CssClass="form-control" runat="server" />
    </div>
    <div class="col-lg-1">
        N° Cita
        <br />
        <asp:TextBox ID="txt_buscarNumCita" CssClass="form-control" runat="server" />
    </div>
    <div class="col-lg-1">
        Desde
        <br />
        <asp:TextBox ID="txt_buscarDesde" CssClass="form-control input-fecha" runat="server" />
    </div>
    <div class="col-lg-1">
        Hasta
        <br />
        <asp:TextBox ID="txt_buscarHasta" CssClass="form-control input-fecha" runat="server" />
    </div>
    <div class="col-lg-2">
        Proveedor
        <br />
        <asp:DropDownList ID="ddl_buscarProveedor" CssClass="form-control" runat="server" />
    </div>
    <div class="col-lg-2">
        Tipo Carga
        <br />
        <asp:DropDownList ID="ddl_buscarTipoCarga" CssClass="form-control" runat="server" />
    </div>
    <div class="col-lg-2">
        Solo con preingreso
        <br />
        <asp:CheckBox ID="chk_buscarPreIngreso" runat="server" />
    </div>
    <div class="col-lg-1">
        <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" runat="server">
            <span class="glyphicon glyphicon-search" />
        </asp:LinkButton>
        <asp:LinkButton ID="btn_nuevo" OnClick="btn_nuevo_Click" CssClass="btn btn-primary" runat="server">
            <span class="glyphicon glyphicon-plus" />
        </asp:LinkButton>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <div class="col-lg-12 separador"></div>
    <div class="col-lg-12">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:GridView ID="gv_listar" CssClass="table table-hover table-bordered tablita" UseAccessibleHeader="true" AutoGenerateColumns="false" EmptyDataText="No hay preingresos" runat="server"
                    OnRowCommand="gv_listar_RowCommand" OnRowDataBound="gv_listar_RowDataBound" OnRowCreated="gv_listar_RowCreated">
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="5px">
                            <HeaderTemplate>
                                <span class="glyphicon glyphicon-remove" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_eliminar" CommandName="ELIMINAR" CommandArgument='<%# Eval("NUM_CITA") %>' CssClass="btn btn-xs btn-primary" runat="server">
                                    <span class="glyphicon glyphicon-remove" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="5px">
                            <HeaderTemplate>
                                <span class="glyphicon glyphicon-pencil" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_modificar" CommandName="MODIFICAR" CommandArgument='<%# Eval("NUM_CITA") %>' CssClass="btn btn-xs btn-primary" runat="server">
                                    <span class="glyphicon glyphicon-pencil" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="5px">
                            <HeaderTemplate>
                                <span class="glyphicon glyphicon-file" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_pdf" CommandName="PDF" CommandArgument='<%# Eval("PRING_ID") %>' CssClass="btn btn-xs btn-primary" runat="server">
                                    <span class="glyphicon glyphicon-file" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PROVEEDOR" SortExpression="PROVEEDOR" HeaderText="PROVEEDOR" />
                        <asp:BoundField DataField="NUM_VENDOR" SortExpression="NUM_VENDOR" HeaderText="NUM_VENDOR" />
                        <asp:BoundField DataField="FECHA_CITA" SortExpression="FECHA_CITA" HeaderText="FECHA_CITA" />
                        <asp:BoundField DataField="NUM_CITA" SortExpression="NUM_CITA" HeaderText="NUM_CITA" />
                        <asp:BoundField DataField="SITE" SortExpression="SITE" HeaderText="SITE" />
                        <asp:BoundField DataField="TIPO_CARGA" SortExpression="TIPO_CARGA" HeaderText="TIPO_CARGA" />
                        <asp:BoundField DataField="TRAILER" SortExpression="TRAILER" HeaderText="TRAILER" />
                        <asp:BoundField DataField="TRACTO" SortExpression="TRACTO" HeaderText="TRACTO" />
                        <asp:BoundField DataField="CONDUCTOR" SortExpression="CONDUCTOR" HeaderText="CONDUCTOR" />
                        <asp:BoundField DataField="ESTADO_PREINGRESO" SortExpression="ESTADO_PREINGRESO" HeaderText="ESTADO_PREINGRESO" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
    <div class="modal fade" id="modalEdit" data-backdrop="static" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Datos Característica Trailer
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-lg-4">
                                Proveedor
                  <br />
                                <asp:DropDownList ID="ddl_editProveedor" OnSelectedIndexChanged="ddl_editProveedor_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0" Text="Seleccione..." />
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-4">
                                Vendor
                  <br />
                                <asp:DropDownList ID="ddl_editVendor" Enabled="false" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0" Text="Seleccione..." />
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-2">
                                Fecha
                  <br />
                                <asp:TextBox ID="txt_editFecha" CssClass="form-control input-fecha" runat="server" />
                            </div>
                            <div class="col-lg-2">
                                Hora
                  <br />
                                <asp:TextBox ID="txt_editHora" CssClass="form-control input-hora" runat="server" />
                            </div>
                            <div class="col-lg-2">
                                N° Cita
                  <br />
                                <asp:TextBox ID="txt_editNro" CssClass="form-control input-number" runat="server" />
                            </div>
                            <div class="col-lg-3">
                                Site
                  <br />
                                <asp:DropDownList ID="ddl_editSite" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0" Text="Seleccione..." />
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-3">
                                Tipo Carga
                  <br />
                                <asp:DropDownList ID="ddl_editTipoCarga" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0" Text="Seleccione..." />
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-12 separador"></div>
                            <div class="col-lg-12" style="text-align: center;">
                                <asp:LinkButton ID="btn_editGrabar" runat="server" OnClick="btn_editGrabar_Click" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-primary">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalConf" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Eliminar Reserva
                            </h4>
                        </div>
                        <div class="modal-body">
                            Se eliminará la reserva seleccionada ¿Desea continuar?
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btnModalEliminar" runat="server" CssClass="btn btn-primary" ToolTip="Eliminar" OnClick="btnModalEliminar_Click">
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
            <asp:HiddenField ID="hf_id" runat="server" />
            <asp:Button ID="btn_pdf" OnClick="btn_pdf_Click" CssClass="ocultar" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_pdf" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlReport" runat="server" Visible="false">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="BarCode 128" Font-Size="24pt"
            InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="BarCode 128" WaitMessageFont-Size="14pt">
            <LocalReport ReportPath="App\reportes\PreIngreso.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        var calcDataTableHeight = function () {
            return $(window).height() - $("#scrolls").offset().top - 90;
        };

        function EndRequestHandler(sender, args) {
            if ($('#<%= gv_listar.ClientID %>')[0] != undefined && $('#<%= gv_listar.ClientID %>')[0].rows.length > 1)
                $('#<%= gv_listar.ClientID %>').DataTable({
                    "scrollY": calcDataTableHeight(),
                    "scrollCollapse": true,
                    "paging": false,
                    "ordering": true,
                    "searching": false,
                    "lengthChange": false,
                    "scrollX": true,
                    "info": false
                });
        }

        function btn_pdf() {
            $("#<%= btn_pdf.ClientID %>").click();
        }

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Proceso_PreEntradav2_ex.aspx.cs" Inherits="App_Proceso_PreEntradav2_ex" %>

<%@ Register Src="~/nuevo_trailer.ascx" TagName="uc" TagPrefix="uc2" %>
<%@ Reference Control="~/nuevo_trailer.ascx" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-lg-12 separador"></div>
    <h2>Pre-Ingreso CD Walmart
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <asp:UpdatePanel ID="upfiltros" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 separador"></div>
            <div class="col-lg-2">
                Centro de Distribución
          <br />
                <asp:DropDownList runat="server" ID="dropsite" ClientIDMode="Static" CssClass="form-control" Width="150px"
                    AutoPostBack="true" OnSelectedIndexChanged="drop_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="col-lg-2">
                <h4>
                    <asp:Label ID="lbl_site" runat="server" />
                </h4>
            </div>
            <div class="col-lg-1 oculta">
                N° CITA
          <br />
                <asp:TextBox ID="txt_buscarDoc" CssClass="form-control input-number" runat="server" Visible="false" />
                <asp:LinkButton ID="btn_buscarDoc" CssClass="btn btn-primary" Visible="false" ToolTip="Buscar doc" runat="server" OnClick="txt_buscarDoc_TextChanged">
          <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
            </div>
            <div class="ocultar">
                <div class="col-lg-1">
                    Propio
            <br />
                    <asp:RadioButton ID="rb_propio" runat="server" GroupName="trai_externo" Enabled="false" />
                </div>
                <div class="col-lg-1">
                    Externo
            <br />
                    <asp:RadioButton ID="rb_externo" runat="server" GroupName="trai_externo" Enabled="false" />
                </div>
                <div class="col-lg-1">
                    Cargado
            <br />
                    <asp:RadioButton ID="rb_ingresoCargado" runat="server" TextAlign="Left" Checked="true" AutoPostBack="true"
                        OnCheckedChanged="chk_ingresoCargado_CheckedChanged" GroupName="trai_cargado" />
                </div>
                <div class="col-lg-1">
                    Vacío
            <br />
                    <asp:RadioButton ID="rb_ingresoVacio" runat="server" TextAlign="Left" AutoPostBack="true"
                        OnCheckedChanged="chk_ingresoCargado_CheckedChanged" GroupName="trai_cargado" />
                </div>
            </div>
            <div class="col-lg-1">
                Fecha
          <br />
                <asp:TextBox CssClass="form-control input-fecha" ID="txt_ingresoFecha" Width="100px" runat="server" />
            </div>
            <div class="col-lg-1">
                Hora
          <br />
                <asp:TextBox CssClass="form-control input-hora" ID="txt_ingresoHora" Width="100px" MaxLength="5" runat="server" />

            </div>
            <div class="col-lg-2">
                Teléfono Conductor
          <br />
                <asp:TextBox ID="txt_ingresoFono" CssClass="form-control input-number" runat="server" />
            </div>
            <div class="col-lg-1" style="text-align: center">
                Importado
          <br />
                <asp:CheckBox runat="server" ID="chk_vehiculoImportado" Checked="false" Enabled="true" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-4">
                <div class="col-lg-5 text-right">
                    Patente Tráiler/Rampla
                </div>
                <div class="col-lg-6">
                    <asp:TextBox ID="txt_placaTrailer" CssClass="form-control input-word" runat="server" MaxLength="8" />
                </div>
                <div class="col-lg-1">
                    <asp:LinkButton ID="btnBuscarTrailer" CssClass="btn btn-primary" runat="server" OnClick="btnBuscarTrailer_Click">
            <span class="glyphicon glyphicon-search" />
                    </asp:LinkButton>
                </div>
                <div class="col-lg-12 separador"></div>
                <asp:DropDownList ID="ddl_transportista" CssClass="form-control" runat="server" Enabled="false" Visible="false">
                    <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
                </asp:DropDownList>
                <div class="col-lg-5 text-right">
                    Patente Tracto
                </div>
                <div class="col-lg-6">
                    <asp:TextBox ID="txt_placaTracto" CssClass="form-control input-word" runat="server" Enabled="false" MaxLength="8"  />
                </div>
                <div class="col-lg-12 separador"></div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="col-lg-5 text-right">
                            <b>Extranjero</b>
                        </div>
                        <div class="col-xs-6">
                            <asp:CheckBox ID="chk_conductorExtranjero" OnCheckedChanged="chk_conductorExtranjero_CheckedChanged" AutoPostBack="true" runat="server" Enabled="false" />
                        </div>
                        <div class="col-lg-12 separador"></div>
                        <div class="col-lg-5 text-right">
                            Conductor RUT
                        </div>
                        <div class="col-lg-6">
                            <asp:TextBox ID="txt_conductorRut" OnTextChanged="txt_conductorRut_TextChanged" CssClass="form-control input-word" runat="server" AutoPostBack="true" Enabled="false" />
                        </div>
                        <div class="col-lg-12 separador"></div>
                        <div class="col-lg-5 text-right">
                            Conductor Nombre
                        </div>
                        <div class="col-lg-6">
                            <asp:TextBox ID="txt_conductorNombre" CssClass="form-control input-word" runat="server" Enabled="false" />
                        </div>
                        <div class="col-lg-12 separador"></div>
                        <div class="col-lg-5 text-right">
                            Acompañante RUT
                        </div>
                        <div class="col-lg-6">
                            <asp:TextBox ID="txt_acomRut" CssClass="form-control input-word" runat="server" Enabled="false" />
                        </div>
                        <div class="col-lg-12 separador"></div>
                        <div class="col-lg-5 text-right">
                            Proveedor
                        </div>
                        <div class="col-lg-6">
                            <asp:DropDownList ID="ddl_proveedor" runat="server" CssClass="form-control" Enabled="false">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-12 separador"></div>
                        <div class="col-lg-5 text-right">
                            ID Sello
                        </div>
                        <div class="col-lg-6">
                            <asp:TextBox ID="txt_idSello" CssClass="form-control" runat="server" Enabled="false" />
                        </div>
                        <div class="col-lg-12 separador"></div>
                        <div class="col-lg-5 text-right">
                            Sello referencia
                        </div>
                        <div class="col-lg-6">
                            <asp:TextBox ID="txt_selloReferencia" CssClass="form-control" runat="server" Enabled="false" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="col-lg-12 separador"></div>
            </div>
            <div class="col-lg-7 col-lg-push-1" style="height: 50vh; border: solid 2px; border-radius: 10px">
                <h4>Numeros de Cita asociados
                </h4>
                <div class="col-lg-3">
                    <asp:TextBox ID="txt_doc2" CssClass="form-control input-letrasnumeros" runat="server" Enabled="false" />
                </div>
                <div class="col-lg-3">
                    <asp:DropDownList ID="ddl_tipo_carga" CssClass="form-control" runat="server" Enabled="false">
                        <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-lg-3">
                    <asp:DropDownList ID="ddl_motivo" CssClass="form-control" runat="server" Enabled="false" Visible="false">
                        <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-lg-3">
                    <asp:LinkButton ID="btn_AgregarListado" CssClass="btn btn-primary" runat="server" ToolTip="Agregar A reservados"
                        OnClick="btn_AgregarListado_Click" Visible="true" ValidationGroup="citas">
            <span class="glyphicon glyphicon-calendar" /> Agregar Cita
                    </asp:LinkButton>
                </div>
                <div class="col-lg-12 separador"></div>
                <div class="col-lg-12" style="height: 40vh; overflow-y: auto;">
                    <asp:GridView ID="gv_Seleccionados" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" Width="100%" runat="server"
                        EmptyDataText="Sin citas Seleccionadas" OnRowCommand="gv_seleccionados_rowCommand" CssClass="table table-bordered table-hover tablita">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btn_quitar" CssClass="btn btn-primary btn-xs" CommandArgument='<%# Eval("NUM_CITA") %>' CommandName="NOSEL" runat="server">
                    <span class="glyphicon glyphicon-erase" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="N°" DataField="NUM_CITA" HeaderStyle-Width="25%" />
                            <asp:BoundField HeaderText="Tipo Carga" DataField="TIPO_CARGA" HeaderStyle-Width="25%" />
                            <asp:BoundField HeaderText="Motivo" DataField="MOTIVO_CARGA" HeaderStyle-Width="25%" />
                            <asp:BoundField HeaderText="Fecha cita" DataField="FECHA_HORA" HeaderStyle-Width="25%" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="col-lg-1">
                <!-- Para motivos de prueba no se usa -->
                <asp:LinkButton ID="btn_buscarConductor" CssClass="btn btn-primary" Visible="false"
                    ToolTip="Buscar Entrada" runat="server">
          <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
            </div>
            <div class="col-lg-12 separador"></div>
            <div class="col-lg-2">
                Observaciones
            </div>
            <div class="col-lg-8">
                <asp:TextBox ID="txt_obs" CssClass="form-control" runat="server" />
            </div>
            <div class="col-lg-12 separador"></div>
            <div class="col-lg-12" style="text-align: center;">
                <asp:LinkButton ID="btn_confirmar" CssClass="btn btn-primary" runat="server" OnClick="btn_confirmar_Click">
            Confirmar Ficha de Seguridad
                </asp:LinkButton>
                <asp:LinkButton ID="btn_limpiar" CssClass="btn btn-primary" runat="server" OnClick="btn_limpiar_Click">
            <span class="glyphicon glyphicon-erase" />
                </asp:LinkButton>
            </div>
            <div style="display: none">
                <asp:Panel ID="pnlReport" runat="server" Visible="false">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="BarCode 128" Font-Size="24pt"
                        InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="BarCode 128" WaitMessageFont-Size="14pt">
                        <LocalReport ReportPath="App\reportes\PreIngreso.rdlc">
                        </LocalReport>
                    </rsweb:ReportViewer>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
    <div class="modal fade" id="modalTransportista" data-backdrop="static" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Datos Transportista
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                                <div class="col-lg-3">
                                    Rut
                  <br />
                                    <asp:TextBox ID="txt_editRutTran" runat="server" Width="100px" CssClass="form-control" OnTextChanged="txt_editRutTran_TextChanged"
                                        AutoPostBack="True">
                                    </asp:TextBox>
                                </div>
                                <div class="col-lg-5">
                                    Nombre
                  <br />
                                    <asp:TextBox ID="txt_editNombre" runat="server" CssClass="form-control input-mayus" />
                                </div>
                                <div class="col-lg-4">
                                    Rol
                  <br />
                                    <asp:TextBox ID="txt_editRol" runat="server" CssClass="input-number form-control" />
                                </div>
                            <div class="col-lg-12 separador">
                            </div>
                            <div class="col-lg-12" style="text-align: center;">
                                <asp:LinkButton ID="btn_tranGrabar" runat="server" OnClick="btn_tranGrabar_Click" ValidationGroup="nuevoCliente"
                                    CssClass="btn btn-primary">
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
    <div class="modal fade" id="modalConfirmar" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Patente no encontrada
                            </h4>
                        </div>
                        <div class="modal-body">
                            La patente ingresada no existe, ¿desea agregar un nuevo trailer?
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btn_Conf" runat="server" OnClick="btn_Conf_Click"
                                CssClass="btn btn-primary">
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
    <div class="modal fade" id="modalTrailer" data-backdrop="static" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Datos Trailer
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-lg-2">
                                Placa
                  <br />
                                <asp:TextBox ID="txt_editPlaca" CssClass="form-control input-mayus" runat="server" OnTextChanged="txt_editPlaca_TextChanged" AutoPostBack="true" />
                            </div>
                            <div class="col-lg-5 ocultar">
                                Transportista
                  <br />
                                <asp:DropDownList ID="ddl_editTran" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-12 separador"></div>
                            <div class="col-lg-12" style="text-align: center;">
                                <asp:LinkButton ID="btn_editGrabar" runat="server" OnClick="btn_editGrabar_Click" ValidationGroup="nuevoTrailer"
                                    CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_idTrailer" runat="server" />
            <asp:HiddenField ID="hf_id" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btn_pdf" CssClass="" runat="server" Style="visibility: hidden; display: none;" OnClick="generaPDF" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_pdf" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        $.datetimepicker.setLocale('es');
        function EndRequestHandler(sender, args) {
            $('#<%= txt_placaTrailer.ClientID %>').change(function () {
                if (!validaPatente($(this).val()) &&
                    !$('#<%= chk_vehiculoImportado.ClientID %>').prop('checked')) {
                    showAlertClass('trailer', 'warn_placaInvalida');
                    $(this).val('');
                    return false;
                }
            });
            $('#<%= txt_placaTracto.ClientID %>').change(function () {
                if (!validaPatente($(this).val()) &&
                    !$('#<%= chk_vehiculoImportado.ClientID %>').prop('checked')) {
                    showAlertClass('tracto', 'warn_placaInvalida');
                    $(this).val('');
                    return false;
                }
            });
            $('#<%= btn_confirmar.ClientID %>').click(function () {
                if ($('#<%= dropsite.ClientID %>').val() == '0') {
                    showAlertClass('confirmar', 'warn_siteVacio');
                    return false;
                }
                if ($('#<%= hf_idTrailer.ClientID %>').val() == '') {
                    showAlertClass('confirmar', 'warn_trailerVacio');
                    return false;
                }
                if ($('#<%= txt_placaTrailer.ClientID %>').val() == '') {
                    showAlertClass('confirmar', 'warn_placaVacia');
                    return false;
                }
                if ($('#<%= txt_conductorRut.ClientID %>').val() == '') {
                    showAlertClass('confirmar', 'warn_conductorRutVacio');
                    return false;
                }
                if ($('#<%= txt_conductorNombre.ClientID %>').val() == '') {
                    showAlertClass('confirmar', 'warn_conductorNombreVacio');
                    return false;
                }
            });
            $('#<%= btnBuscarTrailer.ClientID %>').click(function () {
                if ($('#<%= txt_placaTrailer.ClientID %>').val() == '') {
                    showAlertClass('trailer', 'warn_placaVacia');
                    return false;
                }
            });
        }
        function generaPDF() {
            $("#<%= this.btn_pdf.ClientID %>").click();
            $("#<%= this.btn_limpiar.ClientID %>").click();
        }

        $(document).ready(function () {
            $(window).keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    var button = document.getElementById("<% =this.btn_buscarDoc.ClientID %>");
                //                __doPostBack("<% =this.btn_buscarDoc.ClientID %>".replace("_","$") ,'');
                //                $("#<% =this.btn_buscarDoc.ClientID %>").click();
                    button.click();
                    return true;
                }
            });
        });

        function modalTrailer() {
            $('#modalConfirmar').modal('hide');
            $("#modalTrailer").modal();
        }

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Proceso_Entradav2.aspx.cs" Inherits="App_Proceso_Entradav2" %>

<%@ Register Src="~/nuevo_trailer.ascx" TagName="uc" TagPrefix="uc2" %>
<%@ Reference Control="~/nuevo_trailer.ascx" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-xs-12 separador"></div>
    <h2>Proceso de Entrada
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <asp:UpdatePanel ID="upfiltros" runat="server">
        <ContentTemplate>
            <div class="col-xs-12 separador"></div>
            <div class="col-xs-1">
                N° Cita
          <br />
                <asp:TextBox ID="txt_buscarDoc" CssClass="form-control input-number" runat="server" AutoPostBack="true" OnTextChanged="txt_buscarDoc_TextChanged" />
                <asp:RequiredFieldValidator ID="rfv_doc" runat="server" ControlToValidate="txt_buscarDoc"
                    Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" InitialValue="0"
                    ValidationGroup="buscar">
                </asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                    Enabled="True" TargetControlID="rfv_doc">
                </asp:ValidatorCalloutExtender>
            </div>
            <div class="col-xs-1">
                Placa Trailer
          <br />
                <asp:TextBox CssClass="form-control input-word" ID="txt_buscarPatente" runat="server" Width="100" MaxLength="8" Enabled="false" />
            </div>
            <div class="col-xs-1">
                <br />
                <asp:LinkButton ID="btnBuscarTrailer" CssClass="btn btn-primary" ValidationGroup="buscar" ToolTip="Buscar Entrada" runat="server" OnClick="btnBuscarTrailer_Click">
                    <span class="glyphicon glyphicon-search" />
                    <asp:LinkButton ID="btn_nuevoTrailer" CssClass="btn btn-primary" ToolTip="Nuevo Trailer" Style="margin-left: 10px" runat="server" OnClick="btn_nuevoTrailer_Click" Visible="false">
              <span class="glyphicon glyphicon-plus" />
                    </asp:LinkButton>

                </asp:LinkButton>
            </div>
            <div class="col-xs-1">
                Propio
          <br />
                <asp:RadioButton ID="rb_propio" runat="server" GroupName="trai_externo" Enabled="false" />
            </div>
            <div class="col-xs-1">
                Externo
          <br />
                <asp:RadioButton ID="rb_externo" runat="server" GroupName="trai_externo" Enabled="false" />
            </div>
            <div class="col-xs-1">
                Cargado
          <br />

                <asp:RadioButton ID="rb_ingresoCargado" runat="server" TextAlign="Left" AutoPostBack="true" Enabled="false"
                    OnCheckedChanged="chk_ingresoCargado_CheckedChanged" GroupName="trai_cargado" />
            </div>
            <div class="col-xs-1">
                Vacío
          <br />
                <asp:RadioButton ID="rb_ingresoVacio" runat="server" TextAlign="Left" AutoPostBack="true" Checked="false" Enabled="false"
                    OnCheckedChanged="chk_ingresoCargado_CheckedChanged" GroupName="trai_cargado" />
            </div>
            <div class="col-xs-1">
                Fecha
          <br />
                <asp:TextBox CssClass="form-control input-fecha" ID="txt_ingresoFecha" Width="100px" runat="server" />
            </div>
            <div class="col-xs-1">
                Hora
          <br />
                <asp:TextBox CssClass="form-control input-hora" ID="txt_ingresoHora" Width="100px" MaxLength="4" runat="server" />
            </div>
            <asp:Panel ID="SITE" runat="server">
                <div class="col-xs-1">
                    Site
            <br />
                    <asp:DropDownList runat="server" ID="dropsite" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="drop_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-xs-12 separador"></div>
            <div class="col-xs-3" style="visibility: hidden; display: none;">
                Proveedor
          <br />
                <asp:RadioButton ID="rb_proveedor" runat="server" GroupName="trai_externo" Enabled="false" Visible="false" />
            </div>
            <div class="col-xs-3" style="visibility: hidden; display: none;">
                Mantenim. Externo
          <br />
                <asp:RadioButton ID="rb_mantExterno" runat="server" GroupName="trai_externo" Enabled="false" Visible="false" />
            </div>
            <div class="col-xs-12 separador"></div>
            <div class="col-xs-4">
                <div class="col-lg-5 col-xs-6">
                    Transporte
                </div>
                <div class="col-xs-6">
                    <asp:DropDownList ID="ddl_transportista" CssClass="form-control" runat="server" Enabled="false">
                        <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-12 separador"></div>
                <div class="col-lg-5 col-xs-6">
                    Tracto Ext Patente
                </div>
                <div class="col-xs-6">
                    <asp:TextBox ID="txt_traExtPat" CssClass="form-control input-word" runat="server" Enabled="false" />
                </div>
                <div class="col-xs-12 separador"></div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="col-lg-5 col-xs-6">
                            <b>Extranjero</b>
                        </div>
                        <div class="col-xs-6">
                            <asp:CheckBox ID="chk_conductorExtranjero" OnCheckedChanged="chk_conductorExtranjero_CheckedChanged" AutoPostBack="true" runat="server" Enabled="false" />
                        </div>
                        <div class="col-xs-12 separador"></div>
                        <div class="col-lg-5 col-xs-6">
                            Conductor RUT
                        </div>
                        <div class="col-xs-6">
                            <asp:TextBox ID="txt_conductorRut" OnTextChanged="txt_conductorRut_TextChanged" CssClass="form-control input-word" runat="server" AutoPostBack="true" Enabled="false" />
                        </div>
                        <div class="col-xs-12 separador"></div>
                        <div class="col-lg-5 col-xs-6">
                            Conductor Nombre
                        </div>
                        <div class="col-xs-6">
                            <asp:TextBox ID="txt_conductorNombre" CssClass="form-control input-word" runat="server" Enabled="false" />
                        </div>
                        <div class="col-lg-12 separador"></div>
                        <div class="col-lg-5 col-xs-6">
                            Acompañante RUT
                        </div>
                        <div class="col-xs-6">
                            <asp:TextBox ID="txt_acomRut" CssClass="form-control input-word" runat="server" Enabled="false" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="col-xs-12 separador"></div>
                <div class="col-xs-12" style="visibility: hidden; display: none">
                    <div class="col-xs-4 text-right">
                        Origen
                    </div>
                    <div class="col-xs-6">
                        <asp:DropDownList ID="ddl_trailerOrigen" CssClass="form-control" runat="server" Visible="false">
                            <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-xs-12 separador"></div>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="col-xs-3 text-right">Zona</div>
                        <div class="col-xs-6">
                            <asp:DropDownList ID="ddl_zona" runat="server" AutoPostBack="true" Enabled="false"
                                OnSelectedIndexChanged="ddl_zona_SelectedIndexChanged" CssClass="form-control">
                                <asp:ListItem>Seleccione...</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-12 separador"></div>
                        <div class="col-xs-3 text-right">Playa</div>
                        <div class="col-xs-6">
                            <asp:DropDownList ID="ddl_playa" Enabled="false" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="ddl_playa_SelectedIndexChanged" CssClass="form-control">
                                <asp:ListItem>Seleccione...</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-12 separador"></div>
                        <div class="col-xs-3 text-right">Posición</div>
                        <div class="col-xs-6">
                            <asp:DropDownList ID="ddl_posicion" Enabled="false" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-xs-4">
                <div class="col-lg-5 col-xs-6">
                    Tipo de Carga
                </div>
                <div class="col-xs-6">
                    <asp:DropDownList ID="ddl_tipo_carga" CssClass="form-control" runat="server" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="tipo_carga_SelectedIndexChanged">
                        <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-12 separador"></div>
                <div class="col-lg-5 col-xs-6">
                    Motivo
                </div>
                <div class="col-xs-6">
                    <asp:DropDownList ID="ddl_motivo" CssClass="form-control" runat="server" Enabled="false">
                        <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-12 separador"></div>

                <div class="col-lg-5 col-xs-6">
                    Proveedor
                </div>
                <div class="col-xs-6">
                    <asp:DropDownList ID="ddl_proveedor" runat="server" CssClass="form-control" Enabled="false">
                    </asp:DropDownList>
                </div>
                <div class="col-xs-12 separador"></div>
                <div class="col-lg-5 col-xs-6">
                    sello
                </div>
                <div class="col-xs-6">
                    <asp:TextBox ID="txt_idSello" CssClass="form-control" runat="server" Enabled="false" />
                </div>
            </div>
            <div class="col-xs-1">
                <!-- Para motivos de prueba no se usa -->
                <asp:LinkButton ID="btn_buscarConductor" CssClass="btn btn-primary" Visible="false" ToolTip="Buscar Entrada" runat="server">
            <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
            </div>
            <div class="col-xs-12 separador"></div>
            <div class="col-xs-2">
                Observaciones
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="txt_obs" CssClass="form-control" runat="server" />
            </div>
            <div class="col-xs-12 separador"></div>
            <div class="col-xs-12" style="text-align: center">
                <asp:Button ID="btn_limpiar" CssClass="btn btn-primary" runat="server" Text="Limpiar Datos" OnClick="btn_limpiar_Click" />
                <asp:Button ID="btn_confirmar" CssClass="btn btn-primary" runat="server" Text="Confirmar Ingreso" OnClick="btn_confirmar_Click" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_confirmar" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
    <uc2:uc runat="server" ID="nuevo_trailer" Visible="true" />
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
                            <button type="button" class="btn btn-default" data-dismiss="modal">
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
                            <div class="col-xs-12">
                                <div class="col-xs-2">
                                    Placa
                  <br />
                                    <asp:TextBox ID="txt_editPlaca" CssClass="form-control" runat="server" OnTextChanged="txt_editPlaca_TextChanged" AutoPostBack="true" />
                                </div>
                                <div class="col-xs-5">
                                    Transportista
                  <br />
                                    <asp:DropDownList ID="ddl_editTran" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12 separador"></div>
                            <div class="col-xs-12" style="text-align: center">
                                <asp:LinkButton ID="btn_editGrabar" runat="server" OnClick="btn_editGrabar_Click" ValidationGroup="nuevoTrailer" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>

                        <asp:RequiredFieldValidator ID="rfv_txt_editPlaca" runat="server" ControlToValidate="txt_editPlaca"
                            Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                            ValidationGroup="nuevoTrailer">
                        </asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" PopupPosition="BottomLeft"
                            Enabled="True" TargetControlID="rfv_txt_editPlaca">
                        </asp:ValidatorCalloutExtender>

                        <asp:RequiredFieldValidator ID="rfv_tran" runat="server" ControlToValidate="ddl_editTran" InitialValue="0"
                            Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                            ValidationGroup="nuevoTrailer">
                        </asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" PopupPosition="BottomLeft"
                            Enabled="True" TargetControlID="rfv_tran">
                        </asp:ValidatorCalloutExtender>
                    </ContentTemplate>
                    <%--          <Triggers>
          <asp:PostBackTrigger ControlID="btn_Conf" />
          </Triggers>--%>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_idTrailer" runat="server" />
            <asp:HiddenField ID="hf_idCond" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        function EndRequestHandler(sender, args) {
            $('#<%= txt_buscarPatente.ClientID %>').on('input', function () {
                this.value = this.value.replace(/[^0-9a-zA-Zk]/g, '');
            });

            $('#<%= txt_traExtPat.ClientID %>').on('input', function () {
                this.value = this.value.replace(/[^0-9a-zA-Zk]/g, '');
            });

            $('#<%= txt_acomRut.ClientID %>').on('input', function () {
                this.value = this.value.replace(/[^0-9kK]/g, '');
            });
            $('#<%= btnBuscarTrailer.ClientID %>').click(function () {
                if ($('#<%= txt_buscarPatente.ClientID %>').val() == '' &&
                    ($('#<%= txt_buscarDoc.ClientID %>').val() == '0' ||
                    $('#<%= txt_buscarDoc.ClientID %>').val() == '') {
                    $("#<%= ddl_zona.ClientID %>").val('0');
                    $("#<%= ddl_zona.ClientID %>").prop("disabled", true);
                    $("#<%= hf_idTrailer.ClientID %>").val('0');
                    showAlertClass("trailer", "warn_vacio");
                    return false;
                }
            });
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);
    </script>
</asp:Content>

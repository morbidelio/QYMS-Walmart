<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Proceso_Entrada.aspx.cs" Inherits="App_Proceso_Entrada" %>

<%@ Register Src="~/nuevo_trailer.ascx" TagName="uc" TagPrefix="uc2" %>
<%@ Reference Control="~/nuevo_trailer.ascx" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-lg-12 separador"></div>
    <h2>Proceso de Entrada
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <asp:UpdatePanel ID="upfiltros" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 separador"></div>
            <div class="col-lg-1 col-xs-4">
                Número Flota
            <br />
                <asp:TextBox ID="txt_buscarNro" CssClass="form-control input-number" runat="server" />
            </div>
            <div class="col-lg-1 col-xs-4">
                Placa Trailer
            <br />
                <asp:TextBox ID="txt_buscarPatente" CssClass="form-control input-word" MaxLength="8" runat="server" />
            </div>
            <div class="col-lg-1 col-xs-4">
                <br />
                <asp:LinkButton ID="btnBuscarTrailer" CssClass="btn btn-primary" ToolTip="Buscar Trailer" runat="server" OnClick="btnBuscarTrailer_Click">
                            <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_nuevoTrailer" CssClass="btn btn-primary" ToolTip="Nuevo Trailer" Style="margin-left: 10px" runat="server" OnClick="btn_nuevoTrailer_Click">
                <span class="glyphicon glyphicon-plus" />
                </asp:LinkButton>
            </div>
            <div class="col-lg-1 col-xs-3">
                Propio
            <br />
                <asp:RadioButton ID="rb_propio" runat="server" GroupName="trai_externo" Enabled="false" />
            </div>
            <div class="col-lg-1 col-xs-3">
                Externo
            <br />
                <asp:RadioButton ID="rb_externo" runat="server" GroupName="trai_externo" Enabled="false" />
            </div>
            <div class="col-lg-1 col-xs-3">
                Cargado
            <br />

                <asp:RadioButton ID="rb_ingresoCargado" runat="server" TextAlign="Left" AutoPostBack="true" OnCheckedChanged="chk_ingresoCargado_CheckedChanged" GroupName="trai_cargado" />
            </div>
            <div class="col-lg-1 col-xs-3">
                Vacío
            <br />
                <asp:RadioButton ID="rb_ingresoVacio" runat="server" TextAlign="Left" AutoPostBack="true" Checked="false"
                    OnCheckedChanged="chk_ingresoCargado_CheckedChanged" GroupName="trai_cargado" />
            </div>
            <div class="col-lg-1 col-xs-4">
                Fecha
            <br />
                <asp:TextBox CssClass="form-control input-fecha" ID="txt_ingresoFecha" Width="90px" runat="server" />
            </div>
            <div class="col-lg-1 col-xs-3">
                Hora
            <br />
                <asp:TextBox CssClass="form-control input-hora" ID="txt_ingresoHora" Width="60px" MaxLength="4" runat="server" />
            </div>
            <asp:Panel ID="SITE" runat="server">
                <div class="col-lg-2 col-xs-5">
                    Site
                    <br />
                    <asp:DropDownList CssClass="form-control" runat="server" ID="dropsite" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="drop_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container-fluid" style="height: 45vh; overflow-y: auto">
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

                <div class="col-xs-12 col-lg-4">
                    <div class="col-lg-5 col-xs-6">
                        Transporte
                    </div>
                    <div class="col-xs-6">
                        <asp:DropDownList ID="ddl_transportista" CssClass="form-control" runat="server" Enabled="false">
                            <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-12 separador"></div>
                    <div class="col-lg-5 col-xs-6">
                        Tracto Ext Patente
                    </div>
                    <div class="col-xs-6">
                        <asp:TextBox ID="txt_tracto" CssClass="form-control input-word" runat="server" Enabled="false" />
                    </div>
                    <div class="col-lg-12 separador"></div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="col-lg-5 col-xs-6">
                                <b>Extranjero</b>
                            </div>
                            <div class="col-xs-6">
                                <asp:CheckBox id="chk_conductorExtranjero" OnCheckedChanged="chk_conductorExtranjero_CheckedChanged" AutoPostBack="true" Enabled="false" runat="server" />
                            </div>
                            <div class="col-lg-12 separador"></div>
                            <div class="col-lg-5 col-xs-6">
                                Conductor RUT
                            </div>
                            <div class="col-xs-6">
                                <asp:TextBox ID="txt_conductorRut" OnTextChanged="txt_conductorRut_TextChanged" CssClass="form-control input-word" runat="server" AutoPostBack="true" Enabled="false" />
                            </div>
                            <div class="col-lg-12 separador"></div>
                            <div class="col-lg-5 col-xs-6">
                                Conductor Nombre
                            </div>
                            <div class="col-xs-6">
                                <asp:TextBox ID="txt_conductorNombre" CssClass="form-control input-word" Enabled="false" runat="server" />
                            </div>
                            <div class="col-lg-12 separador"></div>
                            
                            <div class="col-lg-5 col-xs-6">
                                Acompañante RUT
                            </div>
                            <div class="col-xs-6">
                                <asp:TextBox ID="txt_acomRut" CssClass="form-control input-word" Enabled="false" runat="server" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="col-lg-12 separador"></div>
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
                </div>
                <div class="col-lg-4 col-xs-12">
                    <div class="col-lg-5 col-xs-6">
                        Tipo de Carga
                    </div>
                    <div class="col-xs-6">
                        <asp:DropDownList ID="ddl_tipo_carga" CssClass="form-control" runat="server" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="tipo_carga_SelectedIndexChanged">
                            <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-12 separador"></div>
                    <div class="col-lg-5 col-xs-6">
                        Motivo
                    </div>
                    <div class="col-xs-6">
                        <asp:DropDownList ID="ddl_motivo" CssClass="form-control" runat="server" Enabled="false">
                            <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-lg-12 separador"></div>

                    <div class="col-lg-5 col-xs-6">
                        Proveedor
                    </div>
                    <div class="col-xs-6">
                        <asp:DropDownList ID="ddl_proveedor" runat="server" CssClass="form-control" Enabled="false">
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-12 separador"></div>
                    <asp:Panel ID="Pnl_guia" runat="server">
                        <div class="col-lg-5 col-xs-6">
                            Guía 
                        </div>
                        <div class="col-xs-6">
                            <asp:TextBox ID="guia" CssClass="form-control" runat="server" Enabled="false" />
                            <asp:CustomValidator ID="customValidator1" runat="server" ControlToValidate="guia" ErrorMessage="Ingrese Guía" Text="" ForeColor="Red" ClientValidationFunction="validaGuia" ValidateEmptyText="true" Display="None" />
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                Enabled="True" TargetControlID="customValidator1">
                            </asp:ValidatorCalloutExtender>
                        </div>
                    </asp:Panel>
                    <div class="col-lg-12 separador"></div>
                    <asp:Panel ID="PNL_CITA" runat="server">
                        <div class="col-lg-5 col-xs-6">
                            Cita 
                        </div>
                        <div class="col-xs-6">
                            <asp:TextBox ID="txt_doc" CssClass="form-control" runat="server" Enabled="false" />
                        </div>
                    </asp:Panel>
                    <div class="col-lg-12 separador"></div>
                    <div class="col-lg-5 col-xs-6">
                        sello
                    </div>
                    <div class="col-xs-6">
                        <asp:TextBox ID="txt_idSello" CssClass="form-control" runat="server" Enabled="false" />
                    </div>
                    <div class="col-lg-12 separador"></div>
                    <div class="col-lg-12 separador"></div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="col-lg-4 col-xs-12">
                            <div class="col-xs-4">
                                Posición Auto
                <br />
                                <asp:RadioButton ID="rb_posAuto" OnCheckedChanged="rb_pos_CheckedChanged" AutoPostBack="true" GroupName="rb_pos" runat="server" />
                            </div>
                            <div class="col-xs-4">
                                Posición Manual
                <br />
                                <asp:RadioButton ID="rb_posManual" OnCheckedChanged="rb_pos_CheckedChanged" AutoPostBack="true" GroupName="rb_pos" runat="server" />
                            </div>
                            <div class="col-xs-3">
                                Mantenimiento
                <br />
                                <asp:RadioButton ID="rb_mantenimiento" OnCheckedChanged="rb_pos_CheckedChanged" AutoPostBack="true" GroupName="rb_pos" runat="server" />
                            </div>
                            <div class="col-lg-12 separador"></div>
                            <div class="col-lg-3 col-xs-6">Zona</div>
                            <div class="col-xs-6">
                                <asp:DropDownList ID="ddl_zona" runat="server" AutoPostBack="true" Enabled="false"
                                    OnSelectedIndexChanged="ddl_zona_SelectedIndexChanged" CssClass="form-control">
                                    <asp:ListItem>Seleccione...</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-lg-12 separador"></div>

                            <div class="col-lg-3 col-xs-6">Playa</div>
                            <div class="col-xs-6">
                                <asp:DropDownList ID="ddl_playa" Enabled="false" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddl_playa_SelectedIndexChanged" CssClass="form-control">
                                    <asp:ListItem>Seleccione...</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-12 separador"></div>
                            <div class="col-lg-3 col-xs-6">
                                Posición
                  <asp:LinkButton ID="refrescar" runat="server" OnClick="refrescarpos">   
                  <span class="glyphicon glyphicon-refresh" /> 
                  </asp:LinkButton>
                            </div>
                            <div class="col-xs-6">
                                <asp:DropDownList ID="ddl_posicion" Enabled="false" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="col-lg-12 separador"></div>
                <div class="col-xs-12">
                    <div class="col-lg-2 col-xs-6">
                        Observaciones
                    </div>
                    <div class="col-lg-8 col-xs-6">
                        <asp:TextBox ID="txt_obs" CssClass="form-control" runat="server" />
                    </div>
                </div>
            </div>
            <div class="col-lg-12 separador"></div>
            <div class="col-xs-12" style="text-align: center;">
                <asp:LinkButton ID="btn_limpiar" CssClass="btn btn-primary" runat="server"
                    Text="Limpiar Datos" OnClick="btn_limpiar_Click" />
                <asp:LinkButton ID="btn_confirmar" CssClass="btn btn-primary" runat="server"
                    Text="Confirmar Ingreso" OnClick="btn_confirmar_Click" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
    <uc2:uc runat="server" ID="nuevo_trailer" />

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
        function EndRequestHandler1(sender, args) {
            $('#<%= txt_buscarPatente.ClientID %>').keyup(function () {
                $('#<%= txt_buscarNro.ClientID %>').val('');
            });
            $('#<%= txt_buscarNro.ClientID %>').keyup(function () {
                $('#<%= txt_buscarPatente.ClientID %>').val('');
            });
            $('#<%= btnBuscarTrailer.ClientID %>').click(function () {
                if ($('#<%= txt_buscarPatente.ClientID %>').val() == '' &&
                    ($('#<%= txt_buscarNro.ClientID %>').val() == '' ||
                     $('#<%= txt_buscarNro.ClientID %>').val() == '0')) {
                    $("#<%= ddl_zona.ClientID %>").val('0');
                    $("#<%= ddl_zona.ClientID %>").prop("disabled", true);
                    $("#<%= hf_idTrailer.ClientID %>").val('0');
                    showAlertClass("trailer", "warn_vacio");
                    return false;
                }
            });
            $('#<%= btn_confirmar.ClientID %>').click(function () {
                if ($('#<%= hf_idTrailer.ClientID %>').val() == '0' ||
                    $('#<%= hf_idTrailer.ClientID %>').val() == '') {
                    showAlertClass("confirmar", "warn_trailerVacio");
                    return false;
                }
                if ($('#<%= txt_conductorRut.ClientID %>').val() == '') {
                    showAlertClass("confirmar", "warn_conductorVacio");
                    return false;
                }
                if ($('#<%= ddl_posicion.ClientID %>').val() == '0' ||
                    $('#<%= ddl_posicion.ClientID %>').val() == '') {
                    showAlertClass("confirmar", "warn_posicionVacia");
                    return false;
                }
            });
            $('#<%= txt_buscarPatente.ClientID %>').change(function () {
                if (!validaPatente($(this).val())) {
                    $("#<%= ddl_zona.ClientID %>").val('0');
                    $("#<%= ddl_zona.ClientID %>").prop("disabled", true);
                    $("#<%= hf_idTrailer.ClientID %>").val('0');
                    $("#<%= txt_buscarPatente.ClientID %>").val('');
                    showAlertClass("trailer", "warn_patenteInvalida");
                    return false;
                }
            });
            $('#<%= txt_tracto.ClientID %>').change(function () {
                if (!validaPatente($(this).val())) {
                    $("#<%= txt_tracto.ClientID %>").val('');
                    showAlertClass("tracto", "warn_patenteInvalida");
                    return false;
                }
            });
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler1);

        function validaGuia(sender, args) {
            var from = document.getElementById('<%= ddl_tipo_carga.ClientID %>').value;
            var to = document.getElementById('<%= guia.ClientID %>').value;
            var valido = true;
            if (from == 8 && to == '') valido = false;

            args.IsValid = valido;
        }

    </script>
</asp:Content>

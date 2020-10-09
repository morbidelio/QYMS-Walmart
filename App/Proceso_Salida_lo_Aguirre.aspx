<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Proceso_Salida_lo_Aguirre.aspx.cs" Inherits="App_Proceso_Salida_lo_Aguirre" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-xs-12 separador"></div>
    <h2>Proceso de Salida Lo Aguirre
    </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <asp:UpdatePanel runat="server" ID="upfiltro">
        <ContentTemplate>
            <div class="col-lg-2 col-xs-4">
                Nº Viaje:
    <br />
                <asp:TextBox ID="txt_nroViaje" runat="server" CssClass="tb1 textbox form-control input-number" />
            </div>
            <div class="col-lg-2 col-xs-4">
                Patente:
    <br />
                <asp:TextBox ID="txt_Patente" runat="server" CssClass="tb1 textbox form-control" />
            </div>
            <div class="col-lg-2 col-xs-4">
                Flota:
    <br />
                <asp:TextBox ID="txt_NroFlota" runat="server" CssClass="tb1 textbox form-control" />
            </div>
            <div class="form-group col-lg-1 col-md-1 col-sm-1 col-xs-1">
                <br />
                <asp:LinkButton ID="btn_buscar" CssClass="btn btn-primary" Text="" runat="server" OnClick="btn_buscar_Click">
              <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
            </div>
            <asp:Panel ID="SITE" runat="server">
                <div class="col-lg-1 col-xs-4">
                    Site
                    <br />
                    <asp:DropDownList runat="server" ID="dropsite" ClientIDMode="Static" />
                </div>
            </asp:Panel>
            <div class="form-group col-lg-4 col-md-4 col-sm-2">
                <asp:GridView ID="Gridviajes" runat="server" Width="100%" AutoGenerateColumns="false"
                    Style="background-color: White; border-color: Black; width: 100%; border-collapse: collapse;"
                    EmptyDataText="No existen viajes" CssClass="table table-hover table-bordered tablita">
                    <Columns>
                        <asp:BoundField DataField="Viaje" HeaderText="Viaje" />
                        <asp:BoundField DataField="FH_Viaje" HeaderText="FH Viaje" />
                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                        <asp:BoundField DataField="Estado" HeaderText="Estado" />
                    </Columns>
                </asp:GridView>
                <asp:Literal ID="TablaInicial_Viaje1" Text="" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel runat="server" ID="up_dvcontenido">
        <ContentTemplate>
            <div id="dv_contenido" runat="server" style="display: none;">
                <div class="col-xs-12" style="height: 55vh; overflow-y: auto">
                    <div class="col-xs-5 col-md-5 col-lg-2 text-right">
                        <img alt="Tracto" src="../images/iconos/tracto.png" />
                    </div>
                    <div class="col-xs-7 col-lg-4 text-left">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <center>
                                    <table class="table table-bordered table-hover" runat="server" id="tbl_datosTracto">
                                        <tr>
                                        <td class="tablita">
                                            PATENTE TRACTO:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_patenteTracto" runat="server" />
                                        </td>
                                        </tr>
                                        <tr>
                                        <td class="tablita">
                                            RUT CONDUCTOR:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_conductorRut"  runat="server" OnTextChanged="txt_rutChofer_TextChanged" AutoPostBack="true"/>
                                        </td>
                                        </tr>
                                        <tr>
                                            <td class="tablita">EXTRANJERO:
                                            </td>
                                            <td>
                                                <asp:CheckBox id="chk_conductorExtranjero" OnCheckedChanged="chk_conductorExtranjero_CheckedChanged" AutoPostBack="true" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                        <td class="tablita">
                                            NOMBRE CONDUCTOR:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_conductorNombre" runat="server" />
                                        </td>
                                        </tr>
                                        <tr>
                                        <td class="tablita">
                                            TRANSPORTISTA:
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_tran" runat="server" />
                                        </td>
                                        </tr>
                                        <tr>
                                        <td class="tablita">
                                            GPS ACTIVO:
                                        </td>
                                        <td>
                                            <asp:Label ID="txt_gpsActivoTracto" runat="server" />
                                        </td>
                                        </tr>
                                         <tr>
                                        <td class="tablita">
                                            Oservacion:
                                        </td>
                                        <td>
                                            <asp:Label ID="txt_obs" runat="server" />
                                        </td>
                                        </tr>
                                    </table>
                                </center>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-xs-5 col-md-5 col-lg-2 text-right">
                        <img src="../images/iconos/trailer.png" />
                    </div>
                    <div class="col-xs-7 col-lg-4 text-left">
                        <center>
                            <table class="table table-bordered table-hover" runat="server" id="tblTrailer">
                            <tr>
                                <td class="tablita">
                                PLACA:
                                </td>
                                <td>
                                <asp:Label ID="lblPlacaTrailer" runat="server" CssClass="resp" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tablita">
                                FLOTA:
                                </td>
                                <td>
                                <asp:Label ID="lblFlotaTrailer" runat="server" CssClass="resp" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tablita">
                                F/H DATOS:
                                </td>
                                <td>
                                <asp:Label ID="lblFechaDatos" runat="server" CssClass="resp" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tablita">
                                ESTADO:
                                </td>
                                <td>
                                <asp:Label ID="lblEstado" runat="server" CssClass="resp" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tablita">
                                TRANSPORTISTA:
                                </td>
                                <td>
                                <asp:Label ID="lblTransportista" runat="server" CssClass="resp" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tablita">
                                TIPO VEHÍCULO:
                                </td>
                                <td>
                                <asp:Label ID="lblTipo" runat="server" CssClass="resp" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tablita">
                                UBICACIÓN ACTUAL:
                                </td>
                                <td>
                                <asp:Label ID="lblUbicacion" runat="server" CssClass="resp" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tablita">
                                AUDITORIA:
                                </td>
                                <td>
                                <asp:Label ID="lblauditoria" runat="server" CssClass="resp" />
                                </td>
                            </tr>
                            </table>
                        </center>
                    </div>
                    <div class="col-xs-12 separador">
                    </div>
                    <div id="dv_locales" class="col-lg-12 col-md-12 col-sm-12 col-xs-12" runat="server">
                        <asp:GridView ID="gilllocal" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="LOCA_COD"
                            Style="background-color: White; border-color: Black; width: 100%; border-collapse: collapse;"
                            EmptyDataText="Solicitud no posee locales seleccionados" CssClass="table table-hover table-bordered tablita">
                            <Columns>
                                <asp:BoundField DataField="LOCA_COD" HeaderText="Codigo" />
                                <asp:BoundField DataField="LOCAL" />
                                <asp:BoundField DataField="SECUENCIA" />
                                <asp:TemplateField HeaderText="Sello">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_sello" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sobre">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_sobre" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Carro">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_carro" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Embarque">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_embarque" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-xs-12 separador">
                    </div>
                    <div id="dv_destino" class="col-lg-12 col-md-12 col-sm-12 col-xs-12" runat="server">
                        <div class="col-xs-6">
                            Tipo Destino
                                <br />
                            <asp:DropDownList ID="ddl_tipoDestino" CssClass="form-control" OnSelectedIndexChanged="ddl_tipoDestino_SelectedIndexChanged"
                                AutoPostBack="true" runat="server">
                                <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-6">
                            Destino
                                <br />
                            <asp:DropDownList ID="ddl_destino" CssClass="form-control" Enabled="false" runat="server">
                                <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-xs-12 separador">
                    </div>
                    <div class="col-lg-4">
                        <center>
                            <table class="table table-bordered table-hover" runat="server" id="Table2">
                                <tr>
                                    <td class="tablita">
                                        MARCAS PROPIAS:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_mmpp" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tablita">
                                        CAJAS DEVOLUCIÓN:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_cajas" CssClass="input-number" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tablita">
                                        MOTIVO DEVOLUCIÓN:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_devolucion" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tablita">
                                        SELLO:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_sello" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tablita">
                                        LI PALLETS AZULES:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_liAzules" CssClass="input-number" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tablita">
                                        LI PALLETS ROJOS:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_liRojos" CssClass="input-number" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tablita">
                                        LI PALLETS BLANCOS:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_liBlancos" CssClass="input-number" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tablita">
                                        LI PALLETS LEÑA:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_liLeña" CssClass="input-number" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tablita">
                                        GUIA DESPACHO:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_gdNro" CssClass="input-number" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </center>
                    </div>
                    <div class="col-lg-push-2 col-lg-6">
                        <div class="col-xs-5 col-md-5 col-lg-4 text-right">
                            <img alt="puertas" src="../images/iconos/puertas.png" />
                        </div>
                        <div class="col-xs-7 col-lg-8 text-left">
                            <center>
                                <table class="table table-bordered table-hover" runat="server" id="tblSolicitud">
                                    <tr style="visibility:hidden; display:none">
                                        <td class="tablita">
                                        ESTADO:
                                        </td>
                                        <td>
                                        <asp:Label ID="lblEstadoSol" runat="server" CssClass="resp" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tablita">
                                        F/H DATOS:
                                        </td>
                                        <td>
                                        <asp:Label ID="lblFechaDatos2" runat="server" CssClass="resp" />
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </div>
                        <div class="col-xs-12 separador">
                        </div>
                        <div class="col-xs-5 col-md-5 col-lg-4 text-right">
                            <img alt="temperatura" src="../images/iconos/temperatura.png" />
                        </div>
                        <div class="col-xs-7 col-lg-8 text-left">
                            <center>
                                <table class="table table-bordered table-hover" runat="server" id="Table1">
                                <tr>
                                    <td class="tablita">
                                    TEMPERATURA:
                                    </td>
                                    <td>
                                    <asp:Label ID="lblTemperatura" runat="server" CssClass="resp" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tablita">
                                    GPS ACTIVO:
                                    </td>
                                    <td>
                                    <asp:Label ID="lblGPS" runat="server" CssClass="resp" />
                                    </td>
                                </tr>
                                </table>
                            </center>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 separador">
                </div>
                <div class="col-xs-12" style="text-align:center">
                        <asp:LinkButton ID="btn_confirmar" CssClass="btn btn-primary" runat="server" onclick="btn_confirmar_Click">
                            <span class="glyphicon glyphicon-ok" />
                        </asp:LinkButton>   
                        <asp:LinkButton ID="btn_limpiar" CssClass="btn btn-primary" runat="server" onclick="btn_limpiar_Click">
                            <span class="glyphicon glyphicon-erase" />
                        </asp:LinkButton> 
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_idTrailer" runat="server" />
            <asp:HiddenField ID="hf_idCond" runat="server" />
            <asp:HiddenField ID="hf_idTran" runat="server" />
            <asp:HiddenField ID="hf_trueCodInterno" runat="server" />
            <asp:HiddenField ID="estado_yms" runat="server" />
            <asp:HiddenField ID="locales_YMS" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">

        function EndRequestHandler(sender, args) {
            $('.input-number').on('input', function () {
                this.value = this.value.replace(/[^0-9]/g, '');
            });
            $('#<%= chk_conductorExtranjero.ClientID %>').change(function () {
                $('#<%= txt_conductorRut.ClientID %>').val('');
                $('#<%= txt_conductorNombre.ClientID %>').val('');
                $('#<%= hf_idCond.ClientID %>').val('');
            });
            $('#<%= btn_confirmar.ClientID %>').click(function () {
                if ($('#<%= hf_idTrailer.ClientID %>').val() == '0' ||
                    $('#<%= hf_idTrailer.ClientID %>').val() == '') {
                    showAlertClass("confirmar", "warn_trailerVacio");
                    return false;
                }
                if ($('#<%= txt_conductorRut.ClientID %>').val() == '') {
                    showAlertClass("confirmar", "warn_conductorRutVacio");
                    return false;
                }
            });
            $('#<%= txt_Patente.ClientID %>').change(function () {
                if (!validaPatente($(this).val())) {
                    showAlertClass("trailer", "warn_placaInvalida");
                    return false;
                }
            });
            $('#<%= txt_patenteTracto.ClientID %>').change(function () {
                if (!validaPatente($(this).val())) {
                    showAlertClass("tracto", "warn_placaInvalida");
                    return false;
                }
            });
            $('#<%= btn_buscar.ClientID %>').click(function () {
                if ($('#<%= txt_Patente.ClientID %>').val() == '' &&
                    ($('#<%= txt_NroFlota.ClientID %>').val() == '' ||
                        $('#<%= txt_NroFlota.ClientID %>').val() == '0') &&
                    ($('#<%= txt_nroViaje.ClientID %>').val() == '' ||
                        $('#<%= txt_nroViaje.ClientID %>').val() == '0')) {
                    $("#<%= hf_idTrailer.ClientID %>").val('');
                    showAlertClass("trailer", "warn_vacio");
                    return false;
                }
            });
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);
    </script>
</asp:Content>

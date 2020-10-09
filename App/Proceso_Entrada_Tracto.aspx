<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Proceso_Entrada_Tracto.aspx.cs" Inherits="App_Entrada_Tracto" %>

<%@ Register Src="~/nuevo_trailer.ascx" TagName="uc" TagPrefix="uc2" %>
<%@ Reference Control="~/nuevo_trailer.ascx" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-xs-12 separador"></div>
    <h2>Proceso de Entrada Tracto
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <asp:UpdatePanel ID="upfiltros" runat="server">
        <ContentTemplate>
            <div class="col-xs-12 separador"></div>
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
                    <asp:DropDownList CssClass="form-control" runat="server" ID="dropsite" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="drop_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </asp:Panel>
            <div class="col-lg-1 col-xs-6">
                Patente
        <br />
                <asp:TextBox ID="txt_buscarPatente" CssClass="form-control input-word" runat="server" />
            </div>
            <div class="col-lg-1 col-xs-6">
                <br />
                <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" runat="server">
            <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container-fluid" style="height: 45vh; overflow-y: auto">
                <div class="col-lg-4 col-xs-12">
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
                        <asp:TextBox ID="txt_conductorRut" OnTextChanged="txt_conductorRut_TextChanged" CssClass="form-control input-word"
                            runat="server" AutoPostBack="true" Enabled="false" />
                    </div>
                    <div class="col-xs-12 separador"></div>
                    <div class="col-lg-5 col-xs-6">
                        Conductor Nombre
                    </div>
                    <div class="col-xs-6">
                        <asp:TextBox ID="txt_conductorNombre" CssClass="form-control input-word" runat="server" Enabled="false" />
                    </div>
                    <div class="col-xs-12 separador"></div>
                    <div class="col-lg-5 col-xs-6">
                        Acompañante RUT
                    </div>
                    <div class="col-xs-6">
                        <asp:TextBox ID="txt_acomRut" CssClass="form-control input-word" runat="server" Enabled="false" />
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" Visible="false">
                    <ContentTemplate>
                        <div class="col-lg-4 col-xs-12">
                            <div class="col-xs-12 separador"></div>
                            <div class="col-lg-5 col-xs-6">Zona</div>
                            <div class="col-xs-6">
                                <asp:DropDownList ID="ddl_zona" runat="server" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="ddl_zona_SelectedIndexChanged" CssClass="form-control">
                                    <asp:ListItem>Seleccione...</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-12 separador"></div>
                            <div class="col-lg-5 col-xs-6">Playa</div>
                            <div class="col-xs-6">
                                <asp:DropDownList ID="ddl_playa" Enabled="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_playa_SelectedIndexChanged" CssClass="form-control">
                                    <asp:ListItem>Seleccione...</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-12 separador"></div>
                            <div class="col-lg-5 col-xs-6">Posición</div>
                            <div class="col-xs-6">
                                <asp:DropDownList ID="ddl_posicion" Enabled="false" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_posicion" runat="server" ControlToValidate="ddl_posicion"
                                    Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" InitialValue="0"
                                    ValidationGroup="entrada">
                                </asp:RequiredFieldValidator>
                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                    Enabled="True" TargetControlID="rfv_posicion">
                                </asp:ValidatorCalloutExtender>
                            </div>
                            <div class="col-xs-12 separador"></div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="col-xs-12 separador"></div>
                    <div class="col-lg-2 col-xs-6">
                        Observaciones
                    </div>
                    <div class="col-lg-8 col-xs-6">
                        <asp:TextBox ID="txt_obs" CssClass="form-control" runat="server" />
                    </div>
                <div class="col-xs-12 separador"></div>
                <div class="col-xs-12" style="text-align:center">
            <asp:Button ID="btn_limpiar" CssClass="btn btn-primary" runat="server" 
            Text="Limpiar Datos" onclick="btn_limpiar_click" />
            <asp:Button ID="btn_confirmar" CssClass="btn btn-primary" runat="server" 
            Text="Confirmar Ingreso" onclick="btn_confirmar_Click" />
                </div>
        </ContentTemplate>
        <%--<Triggers>
    <asp:PostBackTrigger ControlID="btn_confirmar" />
    </Triggers>--%>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
</asp:Content>
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_pring_id" runat="server" />
            <asp:HiddenField ID="hf_idCond" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        function EndRequestHandler(sender, args) {
            $('#<%= btn_confirmar.ClientID %>').click(function () {
                if ($('#<%= ddl_transportista.ClientID %>').val() == '0' ||
                    $('#<%= ddl_transportista.ClientID %>').val() == '') {
                    showAlertClass("confirmar", "warn_tranVacio");
                    return false;
                }
                if ($('#<%= txt_conductorRut.ClientID %>').val() == '') {
                    showAlertClass("confirmar", "warn_conductorVacio");
                    return false;
                }
                if ($('#<%= txt_buscarPatente.ClientID %>').val() == '') {
                    showAlertClass("confirmar", "warn_tractoVacio");
                    return false;
                }
            });
            $('#<%= txt_buscarPatente.ClientID %>').change(function () {
                if (!validaPatente($(this).val())) {
                    $("#<%= txt_buscarPatente.ClientID %>").val('');
                    showAlertClass("tracto", "warn_patenteInvalida");
                    return false;
                }
            });
            $('#<%= btn_buscar.ClientID %>').click(function () {
                if ($('#<%= txt_buscarPatente.ClientID %>').val() == '') {
                    showAlertClass("tracto", "warn_placaVacio");
                    return false;
                }
            });
        }

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);
    </script>
</asp:Content>

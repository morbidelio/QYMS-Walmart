<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="proceso_Entrada_Servicios_Externos.aspx.cs" Inherits="App_Entrada_Servicios_Externos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-lg-12 separador"></div>
    <h2>Proceso de Entrada Servicios Externos</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-xs-12 separador"></div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-lg-1 col-xs-5">
                Placa
        <br />
                <asp:TextBox ID="txt_placa" CssClass="form-control input-word" runat="server" />
            </div>
            <div class="col-lg-1">
                <br />
                <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" runat="server">
          <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
            </div>
            <div class="col-lg-3 invisible" style="display: none; visibility: hidden">
                Proveedor
        <br />
                <asp:DropDownList ID="ddl_prov" Enabled="false" CssClass="form-control" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv_prov" runat="server" ControlToValidate="ddl_prov" InitialValue="0"
                    Display="None" ErrorMessage="* Requerido" ValidationGroup="entrada" SetFocusOnError="true">
                </asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" PopupPosition="BottomLeft"
                    runat="server" Enabled="True" TargetControlID="rfv_prov">
                </asp:ValidatorCalloutExtender>
            </div>

            <div class="col-lg-2 col-xs-5">
                Site
        <br />
                <asp:DropDownList ID="ddl_site" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="col-lg-1 col-xs-5">
                Fecha
        <br />
                <asp:TextBox ID="txt_fecha" CssClass="form-control input-fecha" runat="server" />
                <asp:RequiredFieldValidator ID="rfv_fecha" runat="server" ControlToValidate="txt_fecha"
                    Display="None" ErrorMessage="* Requerido" ValidationGroup="entrada" SetFocusOnError="true">
                </asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" PopupPosition="BottomLeft"
                    runat="server" Enabled="True" TargetControlID="rfv_fecha">
                </asp:ValidatorCalloutExtender>
            </div>
            <div class="col-lg-1 col-xs-5">
                Hora
        <br />
                <asp:TextBox ID="txt_hora" Width="55px" CssClass="form-control input-hora" runat="server" />
                <asp:RequiredFieldValidator ID="rfv_hora" runat="server" ControlToValidate="txt_hora"
                    Display="None" ErrorMessage="* Requerido" ValidationGroup="entrada" SetFocusOnError="true">
                </asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" PopupPosition="BottomLeft"
                    runat="server" Enabled="True" TargetControlID="rfv_hora">
                </asp:ValidatorCalloutExtender>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel UpdateMode="Always" runat="server">
        <ContentTemplate>
            <div class="col-lg-6">
                <div class="col-lg-4 col-xs-6">
                    <b>Extranjero</b>
                </div>
                <div class="col-lg-6 col-xs-6">
                    <asp:CheckBox id="chk_conductorExtranjero" OnCheckedChanged="chk_conductorExtranjero_CheckedChanged" AutoPostBack="true" runat="server" />
                </div>
                <div class="col-lg-12 separador"></div>
                <div class="col-lg-4">
                    Rut conductor
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="txt_conductorRut" CssClass="form-control input-word" OnTextChanged="txt_rutCond_TextChanged" runat="server" AutoPostBack="true" />
                </div>
                <div class="col-lg-12 separador"></div>
                <div class="col-lg-4">
                    Nombre conductor
                </div>
                <div class="col-lg-6">
                    <asp:TextBox ID="txt_conductorNombre" CssClass="form-control input-word" runat="server" />
                </div>
                <div class="col-lg-12 separador"></div>

                <div class="col-lg-4">
                    Servicio Externo
                </div>
                <div class="col-lg-6">
                    <asp:DropDownList ID="ddl_servExt" CssClass="form-control" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-lg-5">
                <div class="col-lg-3">
                    Observaciones
                </div>
                <div class="col-lg-9">
                    <asp:TextBox ID="txt_obs" CssClass="form-control" runat="server" />
                </div>
            </div>
            <div class="col-lg-12 separador"></div>
            <div class="col-lg-12" style="text-align:center">
          <asp:LinkButton id="btn_guardar" ValidationGroup="entrada" CssClass="btn btn-primary" OnClick="btn_guardar_Click" runat="server">
            <span class="glyphicon glyphicon-floppy-disk" />
          </asp:LinkButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        function EndRequestHandler(sender, args) {

            $('#<%= txt_placa.ClientID %>').on('input', function () {
                this.value = this.value.replace(/[^0-9a-zA-Zk]/g, '');
            });

            $('#<%= btn_buscar.ClientID %>').click(function () {
                if ($('#<%= txt_placa.ClientID %>').val() == '') {
                    showAlertClass("buscar", "warn_patenteVacio");
                    $(this).val('');
                    return false;
                }
            });
            $('#<%= txt_placa.ClientID %>').change(function () {
                if (!validaPatente($(this).val())) {
                    $(this).val('');
                    showAlertClass("buscar", "warn_patenteInvalida");
                    return false;
                }
            });
            $('#<%= btn_guardar.ClientID %>').click(function () {
                if ($('#<%= txt_conductorRut.ClientID %>').val() == '') {
                    showAlertClass("confirmar", "warn_conductorVacio");
                    return false;
                }
                if ($('#<%= ddl_servExt.ClientID %>').val() == '0' ||
                    $('#<%= ddl_servExt.ClientID %>').val() == '') {
                    showAlertClass("confirmar", "warn_servExtVacio");
                    return false;
                }
            });
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);

    </script>

</asp:Content>


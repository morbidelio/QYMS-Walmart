<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="proceso_Salida_tracto.aspx.cs" Inherits="App_Salida_tracto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-xs-12 separador"></div>
    <h2>Proceso de Salida Tracto
    </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-xs-12 separador"></div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-lg-1 col-xs-5">
                Placa
        <br />
                <asp:TextBox ID="txt_placa" CssClass="form-control" runat="server" />
            </div>
            <div class="col-lg-1">
                <br />
                <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" runat="server">
          <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="col-lg-1 col-xs-5">
        Fecha
    <br />
        <asp:TextBox ID="txt_fecha" CssClass="form-control input-fecha" runat="server" />
        <asp:RequiredFieldValidator ID="rfv_fecha" runat="server" ControlToValidate="txt_fecha"
            Display="None" ErrorMessage="* Requerido" ValidationGroup="salida" SetFocusOnError="true">
        </asp:RequiredFieldValidator>
        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" PopupPosition="BottomLeft"
            runat="server" Enabled="True" TargetControlID="rfv_fecha">
        </asp:ValidatorCalloutExtender>
    </div>
    <div class="col-lg-1 col-xs-5">
        Hora
    <br />
        <asp:TextBox ID="txt_hora" Width="55px" CssClass="form-control input-hora" runat="server" />
        <asp:RequiredFieldValidator ID="rfv_hora" runat="server" ControlToValidate="txt_hora"
            Display="None" ErrorMessage="* Requerido" ValidationGroup="salida" SetFocusOnError="true">
        </asp:RequiredFieldValidator>
        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" PopupPosition="BottomLeft"
            runat="server" Enabled="True" TargetControlID="rfv_hora">
        </asp:ValidatorCalloutExtender>
    </div>
    <asp:Panel ID="SITE" runat="server">
        <div class="col-lg-2 col-xs-5">
            Site
      <br />
            <asp:DropDownList ID="ddl_site" CssClass="form-control" runat="server"></asp:DropDownList>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel9" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <div id="dv_contenido" runat="server" style="display: none;">
                <div class="col-lg-3 col-xs-12 col-md-5 text-right">
                    <img alt="Tracto" src="../images/iconos/tracto.png" />
                </div>
                <div class="col-lg-4 col-xs-12 text-left">
                    <center>
                        <table class="table table-bordered table-hover">
                          <tr>
                            <td class="tablita">
                              PATENTE TRACTO:
                            </td>
                            <td>
                              <asp:Label id="lbl_placa" runat="server" />
                            </td>
                          </tr>
                          <tr>
                            <td class="tablita">
                              TRANSPORTISTA:
                            </td>
                            <td>
                              <asp:Label id="lbl_transportista" runat="server" />
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
                              RUT CONDUCTOR:
                            </td>
                            <td>
                                <asp:TextBox ID="txt_conductorRut" AutoPostBack="true" OnTextChanged="txt_rutConductor_TextChanged" runat="server" />
                            </td>
                          </tr>
                          <tr>
                            <td class="tablita">
                              CONDUCTOR:
                            </td>
                            <td>
                                <asp:Label ID="lbl_nombreConductor" runat="server" />
                            </td>
                          </tr>             </table>
                      </center>
                </div>
                <div class="col-lg-12 separador"></div>
                <div class="col-lg-12" style="text-align: center">
                    <asp:LinkButton ID="btn_guardar" CssClass="btn btn-primary" ValidationGroup="salida" OnClick="btn_guardar_Click" runat="server">
              <span class="glyphicon glyphicon-floppy-disk" />
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
            <asp:HiddenField ID="hf_id" runat="server" />
            <asp:HiddenField ID="hf_idCond" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        function EndRequestHandler1(sender, args) {
            $('#<%= btn_buscar.ClientID %>').click(function () {
                if ($('#<%= txt_placa.ClientID %>').val() == '') {
                    $("#<%= hf_id.ClientID %>").val('');
                    showAlertClass("tracto", "warn_vacio");
                    return false;
                }
            });
            $('#<%= btn_guardar.ClientID %>').click(function () {
                if ($('#<%= hf_id.ClientID %>').val() == '0' ||
                    $('#<%= hf_id.ClientID %>').val() == '') {
                    showAlertClass("guardar", "warn_placaVacio");
                    return false;
                }
                if ($('#<%= hf_idCond.ClientID %>').val() == '') {
                    showAlertClass("guardar", "warn_conductorRutVacio");
                    return false;
                }
            });
            $('#<%= txt_placa.ClientID %>').change(function () {
                if (!validaPatente($(this).val())) {
                    showAlertClass("tracto", "warn_placaInvalida");
                    return false;
                }
            });
            $('#<%= chk_conductorExtranjero.ClientID %>').change(function () {
                $('#<%= txt_conductorRut.ClientID %>').val('');
                $('#<%= lbl_nombreConductor.ClientID %>').val('');
                $('#<%= hf_idCond.ClientID %>').val('');
            });
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler1);

    </script>
</asp:Content>


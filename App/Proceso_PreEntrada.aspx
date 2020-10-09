<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Proceso_PreEntrada.aspx.cs" Inherits="App_Proceso_PreEntrada" %>

<%@ Register Src="~/nuevo_trailer.ascx" TagName="uc" TagPrefix="uc2" %>
<%@ Reference Control="~/nuevo_trailer.ascx" %>

<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-xs-12 separador"></div>
    <h2>Pre-Ingreso CD Walmart
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <asp:UpdatePanel ID="upfiltros" runat="server">
        <ContentTemplate>
            <div class="col-xs-12 separador"></div>
            <div class="col-xs-2">
                Site
          <br />
                <asp:DropDownList runat="server" CssClass="form-control" ID="dropsite" ClientIDMode="Static" Width="150px"
                    AutoPostBack="true" OnSelectedIndexChanged="drop_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="col-xs-2">
                <h4>
                    <asp:Label ID="lbl_site" runat="server"></asp:Label>
                </h4>
            </div>
            <div class="col-xs-1 ocultar" style="width: 150px">
                Número Flota
          <br />
                <asp:TextBox ID="txt_buscarNro" CssClass="form-control input-number" runat="server" Width="100" />
            </div>
            <div class="ocultar">
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
                    <asp:RadioButton ID="rb_ingresoCargado" runat="server" TextAlign="Left" Checked="true" AutoPostBack="true"
                        OnCheckedChanged="chk_ingresoCargado_CheckedChanged" GroupName="trai_cargado" />
                </div>
                <div class="col-xs-1">
                    Vacío
            <br />
                    <asp:RadioButton ID="rb_ingresoVacio" runat="server" TextAlign="Left" AutoPostBack="true"
                        OnCheckedChanged="chk_ingresoCargado_CheckedChanged" GroupName="trai_cargado" />
                </div>
            </div>
            <div class="col-xs-1">
                Fecha
          <br />
                <asp:TextBox CssClass="form-control input-fecha" ID="txt_ingresoFecha" Width="100px" runat="server" />
            </div>
            <div class="col-xs-1">
                Hora
          <br />
                <asp:TextBox CssClass="form-control input-hora" ID="txt_ingresoHora" Width="100px" MaxLength="5" runat="server" />
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                <div class="col-lg-5 col-xs-6">
                    Patente Tracto
                </div>
                <div class="col-xs-6">
                    <asp:TextBox ID="txt_placaTracto" CssClass="form-control input-word" runat="server" Enabled="false" />
                </div>
                <div class="col-lg-5 col-xs-6">
                    Patente Tráiler
                </div>
                <div class="col-xs-5">
                    <asp:TextBox ID="txt_placaTrailer" CssClass="form-control input-word" runat="server" MaxLength="8" />
                </div>
                <div class="col-xs-1">
                    <asp:LinkButton ID="btnBuscarTrailer" CssClass="btn btn-primary" ValidationGroup="trailer1"
                        ToolTip="Buscar Entrada" runat="server" OnClick="btnBuscarTrailer_Click">
                <span class="glyphicon glyphicon-search" />

                    </asp:LinkButton>
                </div>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="col-lg-5 col-xs-6">
                            <b>Extranjero</b>
                        </div>
                        <div class="col-xs-6">
                            <asp:CheckBox ID="chk_conductorExtranjero" OnCheckedChanged="chk_conductorExtranjero_CheckedChanged" AutoPostBack="true" runat="server" Enabled="false" />
                        </div>
                        <div class="col-lg-12 separador"></div>
                        <div class="col-lg-5 col-xs-6">
                            Conductor RUT
                        </div>
                        <div class="col-xs-6">
                            <asp:TextBox ID="txt_conductorRut" OnTextChanged="txt_conductorRut_TextChanged" CssClass="form-control input-word" runat="server" AutoPostBack="true" Enabled="false" />
                        </div>
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
            </div>
            <div class="col-xs-4">
                <div class="col-lg-5 col-xs-6">
                    Tipo de Carga
                </div>
                <div class="col-xs-6">
                    <asp:DropDownList ID="ddl_tipo_carga" CssClass="form-control" runat="server" Enabled="true" AutoPostBack="true" OnSelectedIndexChanged="tipo_carga_SelectedIndexChanged">
                        <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-12 ocultar">
                    <div class="col-lg-5 col-xs-6">
                        Motivo
                    </div>
                    <div class="col-xs-6">
                        <asp:DropDownList ID="ddl_motivo" CssClass="form-control" runat="server" Enabled="false">
                            <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-lg-5 col-xs-6">
                    Proveedor
                </div>
                <div class="col-xs-6">
                    <asp:DropDownList ID="ddl_proveedor" runat="server" CssClass="form-control" Enabled="false">
                    </asp:DropDownList>
                </div>
                <div class="col-lg-5 col-xs-6">
                    Cita
                </div>
                <div class="col-xs-6">
                    <asp:TextBox ID="txt_doc" CssClass="form-control" runat="server" Enabled="false" />
                </div>
                <div class="col-lg-5 col-xs-6">
                    ID Sello
                </div>
                <div class="col-xs-6">
                    <asp:TextBox ID="txt_idSello" CssClass="form-control" runat="server" Enabled="false" />
                </div>
            </div>
            <div class="col-xs-1">
                <!-- Para motivos de prueba no se usa -->
                <asp:LinkButton ID="btn_buscarConductor" CssClass="btn btn-primary" Visible="false"
                    ToolTip="Buscar Entrada" runat="server">
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
                <asp:LinkButton ID="btn_confirmar" CssClass="btn btn-primary" runat="server"
                    OnClick="btn_confirmar_Click">
            <span class="glyphicon glyphicon-ok" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_limpiar" CssClass="btn btn-primary" runat="server"
                    OnClick="btn_limpiar_Click">
            <span class="glyphicon glyphicon-erase" />
                </asp:LinkButton>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_confirmar" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
    <uc2:uc runat="server" ID="nuevo_trailer" Visible="true" />

</asp:Content>
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_idTrailer" runat="server" />
            <asp:HiddenField ID="hf_idCond" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">

        function EndRequestHandler(sender, args) {
            $('#<%= txt_placaTrailer.ClientID %>').change(function () {
                if (!validaPatente($(this).val())) {
                    showAlertClass('trailer', 'warn_placaInvalida');
                    $('#<%= txt_placaTrailer.ClientID %>').val('');
                    return false;
                }
            });
            $('#<%= txt_placaTracto.ClientID %>').change(function () {
                if (!validaPatente($(this).val())) {
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
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);
    </script>
</asp:Content>

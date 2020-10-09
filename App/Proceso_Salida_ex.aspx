<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Proceso_Salida_ex.aspx.cs" Inherits="App_Proceso_Salidaex" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-xs-12 separador"></div>
    <h2>Proceso de Salida Importado
    </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <asp:UpdatePanel runat="server" ID="upfiltro">
        <ContentTemplate>
            <div class="col-lg-1 col-xs-4">
                Patente:
    <br />
                <asp:TextBox ID="txt_Patente" runat="server" CssClass="tb1 textbox form-control" MaxLength="14" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="form-group col-lg-1 col-md-1 col-sm-1 col-xs-1">
        <br />
        <asp:LinkButton ID="btn_buscar" CssClass="btn btn-primary" Text="" runat="server" OnClick="btn_buscar_Click">
      <span class="glyphicon glyphicon-search"></span>
        </asp:LinkButton>
    </div>
    <asp:Panel ID="SITE" runat="server">
        <div class="col-lg-2 col-xs-4">
            Site
      <br />
            <asp:DropDownList CssClass="form-control" runat="server" ID="dropsite" ClientIDMode="Static" AutoPostBack="true"></asp:DropDownList>
        </div>
    </asp:Panel>
    <div class="form-group col-xs-12 col-lg-4 col-md-4 col-sm-2">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <asp:GridView ID="Gridviajes" runat="server" Width="100%" AutoGenerateColumns="false" Style="background-color: White; border-color: Black; width: 100%; border-collapse: collapse;"
                    EmptyDataText="No existen viajes" CssClass="table table-hover table-bordered tablita">
                    <Columns>
                        <asp:BoundField DataField="Viaje" HeaderText="Viaje" />
                        <asp:BoundField DataField="FH_Viaje" HeaderText="FH Viaje" />
                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                        <asp:BoundField DataField="Estado" HeaderText="Estado" />

                    </Columns>
                </asp:GridView>
                <asp:Literal ID="TablaInicial_Viaje1" Text="" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel runat="server" ID="up_dvcontenido">
        <ContentTemplate>


            <div id="dv_contenido" runat="server" style="display: none;">
                <div class="col-xs-12" style="height: 60vh; overflow: auto">
                    <div class="col-lg-6 col-xs-12">
                        <div class="col-xs-12">
                            <div class="col-lg-5 col-xs-12 col-md-5 text-right">
                                <img alt="Tracto" src="../images/iconos/tracto.png" />
                            </div>
                            <div class="col-lg-7 col-xs-12 text-left">
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
                                                <asp:TextBox ID="txt_conductorRut" OnTextChanged="txt_rutChofer_TextChanged" AutoPostBack="true" runat="server" />
                                              </td>
                                            </tr>
                                            <tr>
                                              <td class="tablita">
                                                NOMBRE CONDUCTOR:
                                              </td>
                                              <td>
                                                <asp:TextBox ID="txt_conductorNombre" Enabled="false" runat="server" />
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
                                          </table>
                                        </center>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-xs-12 separador"></div>
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <div id="dv_locales" class="col-lg-12 col-md-12 col-sm-12 col-xs-12" runat="server">

                                    <asp:GridView ID="gilllocal" runat="server" Width="100%" AutoGenerateColumns="false" Style="background-color: White; border-color: Black; width: 100%; border-collapse: collapse;"
                                        EmptyDataText="Solicitud no posee locales seleccionados" CssClass="table table-hover table-bordered tablita">
                                        <Columns>
                                            <asp:BoundField DataField="LOCA_COD" HeaderText="Codigo" />
                                            <asp:BoundField DataField="LOCAL" />
                                            <asp:BoundField DataField="SECUENCIA" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div class="col-xs-12 separador"></div>
                        <div id="dv_destino" class="col-lg-12 col-md-12 col-sm-12 col-xs-12" runat="server">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="col-xs-6">
                                        Tipo Destino
                <br />
                                        <asp:DropDownList ID="ddl_tipoDestino" CssClass="form-control" OnSelectedIndexChanged="ddl_tipoDestino_SelectedIndexChanged" AutoPostBack="true" runat="server">
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="col-xs-6">
                        <div class="col-xs-12">
                            <div class="col-lg-5 col-xs-12 col-md-5 text-right">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <img src="../images/iconos/trailer.png" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-xs-7 text-left">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <center>
                                          <table class="table table-bordered table-hover" runat="server" id="tblTrailer">
                                            <tr>
                                              <td class="tablita">
                                                PLACA:
                                              </td>
                                              <td>
                                                <asp:Label ID="lblPlacaTrailer" runat="server" CssClass="resp"></asp:Label>
                                              </td>
                                            </tr>
                                            <tr>
                                              <td class="tablita">
                                                FLOTA:
                                              </td>
                                              <td>
                                                <asp:Label ID="lblFlotaTrailer" runat="server" CssClass="resp"></asp:Label>
                                              </td>
                                            </tr>
                                            <tr>
                                              <td class="tablita">
                                                F/H DATOS:
                                              </td>
                                              <td>
                                                <asp:Label ID="lblFechaDatos" runat="server" CssClass="resp"></asp:Label>
                                              </td>
                                            </tr>
                                            <tr>
                                              <td class="tablita">
                                                ESTADO:
                                              </td>
                                              <td>
                                                <asp:Label ID="lblEstado" runat="server" CssClass="resp"></asp:Label>
                                              </td>
                                            </tr>
                                            <tr>
                                              <td class="tablita">
                                                TRANSPORTISTA:
                                              </td>
                                              <td>
                                                <asp:Label ID="lblTransportista" runat="server" CssClass="resp"></asp:Label>
                                              </td>
                                            </tr>
                                            <tr>
                                              <td class="tablita">
                                                TIPO VEHÍCULO:
                                              </td>
                                              <td>
                                                <asp:Label ID="lblTipo" runat="server" CssClass="resp"></asp:Label>
                                              </td>
                                            </tr>
                                            <tr>
                                              <td class="tablita">
                                                UBICACIÓN ACTUAL:
                                              </td>
                                              <td>
                                                <asp:Label ID="lblUbicacion" runat="server" CssClass="resp"></asp:Label>
                                              </td>
                                            </tr>
                                            <tr>
                                              <td class="tablita">
                                                AUDITORIA:
                                              </td>
                                              <td>
                                                <asp:Label ID="lblauditoria" runat="server" CssClass="resp"></asp:Label>
                                              </td>
                                            </tr>
                                          </table>
                                        </center>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-xs-12 separador"></div>
                        <div class="col-xs-12">
                            <div class="col-lg-5 col-xs-12 col-md-5 text-right">
                                <img alt="puertas" src="../images/iconos/puertas.png" />
                            </div>
                            <div class="col-xs-7 text-left">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <center>
                  <table class="table table-bordered table-hover" runat="server" id="tblSolicitud">
                    <tr>
                      <td class="tablita">
                        ESTADO:
                      </td>
                      <td>
                        <asp:Label ID="lblEstadoSol" runat="server" CssClass="resp"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="tablita">
                        F/H DATOS:
                      </td>
                      <td>
                        <asp:Label ID="lblFechaDatos2" runat="server" CssClass="resp"></asp:Label>
                      </td>
                    </tr>
                  </table>
                </center>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-xs-12 separador"></div>
                        <div class="col-xs-12">
                            <div class="col-lg-5 col-xs-12 col-md-5 text-right">
                                <img alt="temperatura" src="../images/iconos/temperatura.png" />
                            </div>
                            <div class="col-xs-7 text-left">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <center>
                  <table class="table table-bordered table-hover" runat="server" id="Table1">
                    <tr>
                      <td class="tablita">
                        TEMPERATURA:
                      </td>
                      <td>
                        <asp:Label ID="lblTemperatura" runat="server" CssClass="resp"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="tablita">
                        GPS ACTIVO:
                      </td>
                      <td>
                        <asp:Label ID="lblGPS" runat="server" CssClass="resp"></asp:Label>
                      </td>
                    </tr>
                  </table>
                </center>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 separador"></div>
                <div class="col-xs-12">
                    <center>
            <asp:UpdatePanel runat="server" ID="botones">
      <ContentTemplate>

        <asp:LinkButton ID="btn_confirmar" CssClass="btn btn-primary" runat="server" 
                        onclick="btn_confirmar_Click">
                        <span class="glyphicon glyphicon-ok"></span>
        </asp:LinkButton>   
        <asp:LinkButton ID="btn_limpiar" CssClass="btn btn-primary" runat="server" 
                        onclick="btn_limpiar_Click">
                        <span class="glyphicon glyphicon-erase"></span>
        </asp:LinkButton>   

        </ContentTemplate>
      </asp:UpdatePanel>

      </center>
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
            <asp:HiddenField ID="hf_idTran" runat="server" />
            <asp:HiddenField ID="hf_idCond" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
    <style>
        @media only screen and (max-width: 750px) {
            .table tbody tr td {
                font-size: 10px;
                height: auto;
                width: auto;
                padding: 2px;
                vertical-align: middle;
            }
        }
    </style>
    <script type="text/javascript">

        function EndRequestHandler(sender, args) {
            $('.input-number').on('input', function () {
                this.value = this.value.replace(/[^0-9]/g, '');
            });
            $('#<%= btn_buscar.ClientID %>').click(function () {
            if ($('#<%= txt_Patente.ClientID %>').val() == '') {
                $("#<%= hf_idTrailer.ClientID %>").val('');
                showAlertClass("trailer", "warn_placaVacia");
                return false;
            }
            });
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);
    </script>
</asp:Content>

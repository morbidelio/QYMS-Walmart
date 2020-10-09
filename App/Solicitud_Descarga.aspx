<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true"
    CodeFile="Solicitud_Descarga.aspx.cs" Inherits="App_Solicitud_Descarga" %>

<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-xs-12 separador">
    </div>
    <h2>Solicitud de Descarga
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <asp:UpdatePanel runat="server" ID="upfiltros">
        <ContentTemplate>
            <asp:Panel ID="SITE" runat="server">
                <div class="col-lg-1 col-md-2 col-sm-2 col-xs-4">
                    Site
              <br />
                    <asp:DropDownList runat="server" ID="dropsite" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="drop_SelectedIndexChanged" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </asp:Panel>
            <div class="col-lg-1 col-md-1 col-sm-2 col-xs-4">
                Fecha
            <br />
                <asp:TextBox CssClass="form-control input-fecha" ID="txt_buscarFecha" runat="server" />
            </div>
            <div class="col-lg-1 col-md-1 col-sm-1 col-xs-4">
                Hora
            <br />
                <asp:TextBox CssClass="form-control input-hora" ID="txt_buscarHora" runat="server" />
            </div>
            <div class="col-xs-12 hidden-sm hidden-md hidden-lg separador"></div>
            <div class="col-lg-1 col-md-2 col-sm-2 col-xs-3">
                N° Flota
            <br />
                <asp:TextBox ID="txt_buscarNro" CssClass="form-control input-number" runat="server" />
                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txt_buscarNro" Operator="GreaterThanEqual" ValidationGroup="trailer1" ValueToCompare="0" Type="Integer"
                    ErrorMessage="ingrese un Número mayor a 0" Display="Dynamic" Text="*" />
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" PopupPosition="BottomLeft" Enabled="True" TargetControlID="CompareValidator2">
                </asp:ValidatorCalloutExtender>
            </div>
            <div class="col-lg-1 col-md-2 col-sm-2 col-xs-4">
                Patente
            <br />
                <asp:TextBox CssClass="form-control" ID="txt_buscarPatente" runat="server" />
            </div>
            <div class="col-lg-1 col-md-1 col-sm-2 col-xs-3">
                Extranjero
            <br />
                <asp:CheckBox runat="server" ID="extranjero" Checked="false" />
            </div>
            <div class="col-lg-1 col-md-1 col-sm-1 col-xs-2">
                <br />
                <asp:LinkButton ID="btnBuscar" CssClass="btn btn-primary" ValidationGroup="trailer1" ToolTip="Buscar Descarga" runat="server" OnClick="btnBuscar_Click">
              <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="dv_contenedor">
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                    <div class="col-lg-3 col-md-4 col-sm-3 col-xs-4">
                        N° Flota
            <br />
                        <asp:TextBox ID="txt_nroFlota" Enabled="false" CssClass="form-control" runat="server" />
                    </div>
                    <div class="col-lg-6 col-md-8 col-sm-6 col-xs-8">
                        Transporte
            <br />
                        <asp:TextBox ID="txt_transporte" Enabled="false" CssClass="form-control" runat="server" />
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 separador"></div>
                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                        <h4>Origen
                        </h4>
                        <div class="col-lg-3 col-md-4 col-sm-4 col-xs-4">
                            <asp:Panel ID="pnl_lugarOrigen" CssClass="lugar-especifico" runat="server" Style="">
                                <asp:Panel ID="pnl_detalleLugar" CssClass="row columna-anden detalle-lugar-especifico" runat="server">
                                    <asp:Label ID="lbl_lugar" runat="server" />
                                </asp:Panel>
                                <asp:Panel ID="pnl_detalleTrailer" CssClass="row columna-anden detalle-trailer-especifico" runat="server">
                                    <asp:Panel Style="width: 20px; border-radius: 20px;" ID="pnl_imgAlerta" runat="server">
                                        <img style="width: 20px; border-radius: 20px;" src="../img/reloj.png" />
                                    </asp:Panel>
                                    <asp:Image ID="img_trailer" Width="20px" runat="server" />
                                </asp:Panel>
                            </asp:Panel>
                        </div>
                        <div id="dv_origen" class="col-lg-9 col-md-8 col-sm-8 col-xs-8">
                            Zona:
            <asp:Label ID="lbl_origenZona" runat="server" />
                            <br />
                            Playa:
            <asp:Label ID="lbl_origenPlaya" runat="server" />
                        </div>
                    </div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <h4>Destino
                                </h4>
                                <div class="col-xs-4">
                                    Zona
                                </div>
                                <div class="col-xs-8">
                                    <asp:DropDownList ID="ddl_destinoZona" CssClass="form-control" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddl_destinoZona_SelectedIndexChanged" Enabled="False">
                                        <asp:ListItem>Seleccione...</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-12 separador">
                                </div>
                                <div class="col-xs-4">
                                    Playa
                                </div>
                                <div class="col-xs-8">
                                    <asp:DropDownList ID="ddl_destinoPlaya" CssClass="form-control" runat="server" Enabled="false"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddl_destinoPlaya_SelectedIndexChanged">
                                        <asp:ListItem>Seleccione...</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-12 separador">
                                </div>
                                <div class="col-xs-4">
                                    Posición
                                </div>
                                <div class="col-xs-8">
                                    <asp:DropDownList ID="ddl_destinoPos" CssClass="form-control" Enabled="false" runat="server">
                                        <asp:ListItem>Seleccione...</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="hidden-lg hidden-md col-xs-12 col-sm-12 separador"></div>
                <div id="dv_bloqueo" class="col-xs-12 col-md-6 col-lg-6" style="height:70%">
                    <h4>Destinos Bloqueados
                    </h4>
                    <div class="col-xs-5">
                        <asp:DropDownList ID="ddl_bloquearPos" CssClass="form-control" Enabled="false" runat="server">
                            <asp:ListItem>Seleccione...</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-1">
                        <asp:LinkButton ID="btn_AgregarListado" CssClass="btn btn-primary" runat="server" ToolTip="Agregar A reservados" OnClick="btn_AgregarListado_Click">
            <span class="glyphicon glyphicon-calendar" />
                        </asp:LinkButton>
                    </div>
                    <div class="col-xs-12 separador"></div>
                    <div id="dv_grillaBloqueados" class="col-xs-12">
                        <asp:GridView ID="gv_Seleccionados" CssClass="table table-bordered table-hover tablita" runat="server" EmptyDataText="Sin lugares reservados"
                            AutoGenerateColumns="False" Width="100%" OnRowCommand="gv_seleccionados_rowCommand" OnRowCreated="gv_Seleccionados_RowCreated">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_quitar" CssClass="btn btn-primary btn-xs" CommandArgument="<%# Container.DataItemIndex %>" CommandName="QUITAR" runat="server">
                    <span class="glyphicon glyphicon-erase" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="ZONA" DataField="ZONA" SortExpression="ZONA" />
                                <asp:BoundField HeaderText="PLAYA" DataField="PLAYA" SortExpression="PLAYA" />
                                <asp:BoundField HeaderText="POSICION" DataField="POSICION" SortExpression="POSICION" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                 <div class="col-xs-12 separador"></div>
                          <div class="col-xs-12" style="text-align: center">
                <asp:LinkButton ID="btn_limpiar" CssClass="btn btn-primary" runat="server" ToolTip="Limpiar" OnClick="limpia">
            <span class="glyphicon glyphicon-erase" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_anular" CssClass="btn btn-primary" runat="server" ToolTip="Anular Solicitud" Visible="false">
            <span class="glyphicon glyphicon-trash" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_confirmar" CssClass="btn btn-primary" runat="server" ToolTip="Confirmar Movimiento" OnClick="btn_confirmar_Click">
            <span class="glyphicon glyphicon-ok" />
                </asp:LinkButton>
            </div>

            </div>
           
  
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
</asp:Content>
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_trailerId" runat="server" />
            <asp:HiddenField ID="hf_soliId" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
    <style>
        #dv_contenedor {
            overflow-y: auto;
        }

        #dv_bloqueo {
            border: solid 2px;
            border-radius: 10px;
            height: 100%;
        }

        @media (min-width: 1441px) {
            #dv_contenedor {
                height: 70vh;
            }

            .dataTables_scrollBody {
                height: 58vh;
            }
        }

        @media (min-width: 1281px) and (max-width: 1440px) {
            #dv_contenedor {
                height: 55vh;
            }

            .dataTables_scrollBody {
                height: 32vh;
            }
        }

        /* 
            ##Device = Laptops, Desktops
            ##Screen = B/w 1025px to 1280px
        */

        @media (min-width: 1025px) and (max-width: 1280px) {
            #dv_contenedor {
                height: 50vh;
            }

            .dataTables_scrollBody {
                height: 32vh;
            }
        }

        /* 
            ##Device = Tablets, Ipads (portrait)
            ##Screen = B/w 768px to 1024px
        */

        @media (min-width: 768px) and (max-width: 1024px) {
            #dv_contenedor {
                height: 50vh;
            }

            .dataTables_scrollBody {
                height: 25vh;
            }
        }

        /* 
            ##Device = Tablets, Ipads (landscape)
            ##Screen = B/w 768px to 1024px
        */

        @media (min-width: 768px) and (max-width: 1024px) and (orientation: landscape) {
            #dv_contenedor {
                height: 50vh;
            }

            .dataTables_scrollBody {
                height: 25vh;
            }
        }

        /* 
            ##Device = Low Resolution Tablets, Mobiles (Landscape)
            ##Screen = B/w 481px to 767px
        */

        @media (max-width: 767px) {
            #dv_contenedor {
                height: 45vh;
            }

            .dataTables_scrollBody {
                height: 20vh;
            }
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(window).keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    var button = document.getElementById("<% =this.btnBuscar.ClientID %>");
                    button.click();
                    return true;
                }
            });
        });

        function tabla() {
            if ($('#<%= gv_Seleccionados.ClientID %>')[0] != undefined && $('#<%= gv_Seleccionados.ClientID %>')[0].rows.length > 1) {
                $('#<%= gv_Seleccionados.ClientID %>').DataTable({
                    "scrollX": true,
                    "scrollCollapse": true,
                    "paging": false,
                    "ordering": false,
                    "searching": false,
                    "lengthChange": false,
                    "info": false
                });
            }
        }
        function EndRequestHandler1(sender, args) {
            $('#<%= btn_confirmar.ClientID %>').click(function () {
                if ($('#<%=ddl_destinoPos.ClientID%>').val() == '' ||
                    $('#<%=ddl_destinoPos.ClientID%>').val() == '0' ||
                    $('#<%=ddl_destinoPos.ClientID%>').val() == null) {
                    showAlertClass('guardar', 'warn_destinoVacio');
                    return false;
                }
            });
            $('#<%= btn_AgregarListado.ClientID %>').click(function () {
                if ($('#<%=ddl_bloquearPos.ClientID%>').val() == '' ||
                    $('#<%=ddl_bloquearPos.ClientID%>').val() == '0' ||
                    $('#<%=ddl_bloquearPos.ClientID%>').val() == null) {
                    showAlertClass('bloquear', 'warn_destinoVacio');
                    return false;
                }
            });
            $('#<%= btnBuscar.ClientID %>').click(function () {
                if ($('#<%=txt_buscarNro.ClientID%>').val() == '' &&  $('#<%=txt_buscarPatente.ClientID%>').val() == '') {
                    showAlertClass('buscarTrailer', 'warn_datosVacio');
                    return false;
                }
                else if ($('#<%=extranjero.ClientID %>').prop('checked') &&
                        $('#<%=txt_buscarPatente.ClientID%>').val() != '') {
                    if (!validaPatente($('#<%=txt_buscarPatente.ClientID%>').val())) {
                        showAlertClass('buscarTrailer', 'warn_placaInvalida');
                        return false;
                    }
                }
            });
            tabla();
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler1);
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterTms.master" AutoEventWireup="true" CodeFile="KPI_Site.aspx.cs" Inherits="App_KPI_Site" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-lg-12 separador"></div>
            <div class="col-lg-1" style="text-align: right">
                Site
            </div>
            <div class="col-lg-2">
                <asp:DropDownList ID="ddl_site" OnSelectedIndexChanged="ddl_site_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-lg-1">
                <asp:Button ID="btn_recargar" CssClass="btn btn-primary" runat="server" Text="Recargar" OnClick="recargaplayas" />
            </div>
            <div class="col-lg-1">
                <button class="btn btn-primary" type="button" onclick="javascript:window.location.href = './Panel_control.aspx'">
                    Panel Control
                </button>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Timer ID="prueba" runat="server" OnTick="recargaplayas" Interval="60000"></asp:Timer>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div style="margin: 2px;" class="panel panel-primary">
                    <div class="panel-heading" style="text-align: center">
                        <h2 class="panel-title">
                        KPI CENTRO DE DISTRIBUCION</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-2" style="min-width: 120px">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title" style="min-height: 30px">Trailers en site </h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-lg-6" style="text-align: center">
                                        <img data-toggle="tooltip" title="Vacios" width="50" src="../images/yms/trailer_vacio.png" />
                                        <br />
                                        <asp:Label ID="lbl_trailerVacio" Text="text" runat="server" />
                                    </div>
                                    <div class="col-lg-6" style="text-align: center">
                                        <img data-toggle="tooltip" title="Cargados" width="50" src="../images/yms/trailer_cargado.png" />
                                        <br />
                                        <asp:Label ID="lbl_trailerCargado" Text="text" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2" style="min-width: 240px">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title" style="min-height: 30px">Tiempo en andén carga</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-lg-6">
                                        <img data-toggle="tooltip" data-placement="right" title="Promedio" width="50" src="../images/yms/reloj_normal.png" />
                                    </div>
                                    <div class="col-lg-6">
                                <br />
                                        <asp:Label ID="lbl_tiempoPromedioCarga" Font-Bold="true" Text="text" runat="server" />
                                    </div>
                                    <div class="col-lg-12 separador"></div>
                                    <div class="col-lg-6">
                                        <img data-toggle="tooltip" data-placement="right" title="Mínimo" width="50" src="../images/yms/reloj_atiempo.png" />
                                    </div>
                                    <div class="col-lg-6">
                                <br />
                                        <asp:Label ID="lbl_tiempoMinCarga" Font-Bold="true" Text="text" runat="server" />
                                    </div>
                                    <div class="col-lg-12 separador"></div>
                                    <div class="col-lg-6">
                                        <img data-toggle="tooltip" data-placement="right" title="Máximo" width="50" src="../images/yms/reloj_atrasado.png" />
                                    </div>
                                    <div class="col-lg-6">
                                <br />
                                        <asp:Label ID="lbl_tiempoMaxCarga" Font-Bold="true" Text="text" runat="server" />
                                    </div>
                                    <div class="col-xs-12 separador"></div>
                                    <div class="col-lg-6">
                                        <img data-toggle="tooltip" data-placement="right" title="Cantidad" width="50" src="../images/yms/trailer_vacio.png" />
                                    </div>

                                    <div class="col-lg-6">
                                <br />
                                        <asp:Label ID="lbl_cantidad_vacio" Font-Bold="true" Text="text" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2" style="min-width: 260px">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title" style="min-height: 30px">Tiempo en andén descarga</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-lg-6">
                                        <img data-toggle="tooltip" title="Promedio" data-placement="right" width="50" src="../images/yms/reloj_normal.png" runat="server" />
                                    </div>
                                    <div class="col-lg-6">
                                <br />
                                        <asp:Label ID="lbl_tiempoPromedioDescarga" Font-Bold="true" Text="text" runat="server" />
                                    </div>
                                    <div class="col-lg-12 separador"></div>
                                    <div class="col-lg-6">
                                        <img data-toggle="tooltip" title="Mínimo" data-placement="right" width="50" src="../images/yms/reloj_atiempo.png" />
                                    </div>
                                    <div class="col-lg-6">
                                <br />
                                        <asp:Label ID="lbl_tiempoMinDescarga" Font-Bold="true" Text="text" runat="server" />
                                    </div>
                                    <div class="col-lg-12 separador"></div>
                                    <div class="col-lg-6">
                                        <img data-toggle="tooltip" title="Máximo" data-placement="right" width="50" src="../images/yms/reloj_atrasado.png" />
                                    </div>
                                    <div class="col-lg-6">
                                <br />
                                        <asp:Label ID="lbl_tiempoMaxDescarga" Font-Bold="true" Text="text" runat="server" />
                                    </div>
                                    <div class="col-xs-12 separador"></div>
                                    <div class="col-lg-6">
                                        <img data-toggle="tooltip" title="Cantidad" data-placement="right" width="50" src="../images/yms/trailer_cargado.png" />
                                    </div>

                                    <div class="col-lg-6">
                                <br />
                                        <asp:Label ID="lbl_cantidad_cargado" Font-Bold="true" Text="text" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2" style="min-width: 260px;">
                            <div class="panel panel-primary" style="min-height: 320px;">
                                <div class="panel-heading">
                                    <h3 class="panel-title" style="min-height: 30px">Tiempo en estacionamiento</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-lg-6">
                                        <img data-toggle="tooltip" title="Promedio" data-placement="right" width="50" src="../images/yms/reloj_normal.png" />
                                    </div>
                                    <div class="col-lg-6">
                                <br />
                                        <asp:Label ID="lbl_tiempoPromedioEstacionamiento" Font-Bold="true" Text="text" runat="server" />
                                    </div>
                                    <div class="col-xs-12 separador"></div>
                                    <div class="col-lg-6">
                                        <img data-toggle="tooltip" title="Vacíos" data-placement="right" width="50" src="../images/yms/trailer_vacio.png" />
                                    </div>

                                    <div class="col-lg-6">
                                        <br />
                                        <asp:Label ID="lbl_cantidadEstacionamiento_vacio" Font-Bold="true" Text="text" runat="server" />
                                    </div>

                                    <div class="col-xs-12 separador"></div>

                                    <div class="col-lg-6">
                                        <img data-toggle="tooltip" title="Cargados" data-placement="right" width="50" src="../images/yms/trailer_cargado.png" />
                                    </div>

                                    <div class="col-lg-6">

                                        <br />
                                        <asp:Label ID="lbl_cantidadEstacionamiento_cargado" Font-Bold="true" Text="text" runat="server" />
                                    </div>
                                    <div class="col-xs-12 separador"></div>
                                    <div class="col-xs-12 separador"></div>
                                    <div class="col-lg-6">
                                        <asp:Label ID="Label1" Font-Bold="true" Text="Cantidad" runat="server" />
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:Label ID="lbl_cantidadEstacionamiento" Font-Bold="true" Text="text" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title" style="min-height: 30px">Andenes carga/descarga</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="GaugeMeter" id="GaugeMeter_2" data-append="%" data-style="Arch" data-width="10" data-size="285">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
    <script src="../js2/GaugeMeter.js" type="text/javascript"></script>
    <script type="text/javascript">
        function cargaGauge(ocupados, vacios) {
            var total = ocupados + vacios;
            var porcentaje = ocupados * 100 / total;
            var lbl = 'Ocupados: ' + ocupados + ' Vacíos: ' + vacios;
            $("#GaugeMeter_2").gaugeMeter({ percent: porcentaje, label: lbl });
        }
    </script>
    <style type="text/css">
        .GaugeMeter {
            Position: Relative;
            Text-Align: Center;
            Overflow: Hidden;
            Cursor: Default;
        }

            .GaugeMeter SPAN,
            .GaugeMeter B {
                Margin: 0 23%;
                width: 54%;
                Position: Absolute;
                Text-align: Center;
                Display: Inline-Block;
                Color: RGBa(0,0,0,.8);
                Font-Weight: 100;
                Overflow: Hidden;
                White-Space: NoWrap;
                Text-Overflow: Ellipsis;
            }

            .GaugeMeter[data-style="Semi"] B {
                Margin: 0 10%;
                width: 80%;
            }

            .GaugeMeter S,
            .GaugeMeter U {
                Text-Decoration: None;
                Font-Size: .5em;
                Opacity: .5;
            }

            .GaugeMeter B {
                Color: Black;
                Font-Weight: 300;
                Font-Size: .7em;
                Opacity: .8;
            }
    </style>
</asp:Content>


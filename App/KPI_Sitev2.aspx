<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterTms.master" AutoEventWireup="true" CodeFile="KPI_Sitev2.aspx.cs" Inherits="App_KPI_Site" %>

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
                        KPI CENTRO DE DISTRIBUCION</h2>
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-2" style="width:15%">
                            <div class="panel panel-info" style="height:306px">
                                <div class="panel-heading">
                                    <h1 class="panel-title" style="font-size:18px;text-align:center;height:30px">Trailers en site</h1>
                                </div>
                                <div class="panel-body">
                                    <div class="col-lg-6" style="text-align: center">
                                        <img data-toggle="tooltip" title="Trailers vacios" width="80" src="../images/yms/trailer_vacio.png" />
                                    </div>
                                    <div class="col-lg-6" style="text-align: center">
                                        <asp:Label ID="lbl_trailerVacio" style="font-size:36px;" runat="server" />
                                        <br />
                                        <span style="font-size:12px" class="label label-primary">Vacios</span>
                                    </div>
                                    <div class="col-lg-12 separador" style="height:40px;"></div>
                                    <div class="col-lg-6" style="text-align: center">
                                        <img data-toggle="tooltip" title="Trailers cargados" width="80" src="../images/yms/trailer_cargado.png" />
                                    </div>
                                    <div class="col-lg-6" style="text-align: center">
                                        <asp:Label ID="lbl_trailerCargado" style="font-size:36px;" runat="server" />
                                        <br />
                                        <span style="font-size:12px" class="label label-primary">Cargados</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3" style="width:20%">
                            <div class="panel panel-info" style="height:306px">
                                <div class="panel-heading">
                                    <h1 class="panel-title" style="font-size:18px;text-align:center;height:30px">Andén carga</h1>
                                </div>
                                <div class="panel-body">
                                    <div class="col-lg-6" style="text-align:center">
                                        <img data-toggle="tooltip" data-placement="right" title="Cantidad trailers" width="80" src="../images/yms/lugar_carga.png" />
                                    </div>
                                    <div class="col-lg-6" style="text-align:center">
                                        <asp:Label ID="lbl_cantidad_vacio" style="font-size:36px;" runat="server" />
                                        <br />
                                        <span style="font-size:12px" class="label label-primary">Trailers</span>
                                    </div>
                                    <div class="col-lg-12 separador"></div>
                                    <hr style="width:100%;margin-left:0px;margin-right:0px;" />
                                    <div class="col-lg-4" style="text-align:center;color:rgb(245,179,65)">
                                        <img data-toggle="tooltip" data-placement="right" title="Tiempo promedio" width="50" src="../images/yms/tiempo-atrasado.png" />
                                        <br />
                                        Promedio
                                        <br />
                                        <asp:Label ID="lbl_tiempoPromedioCarga" Font-Bold="true" Text="text" runat="server" />
                                    </div>
                                    <div class="col-lg-4" style="text-align:center;color:rgb(157,185,54)">
                                        <img data-toggle="tooltip" data-placement="right" title="Tiempo mínimo" width="50" src="../images/yms/tiempo-ok.png" />
                                        <br />
                                        Mínimo
                                        <br />
                                        <asp:Label ID="lbl_tiempoMinCarga" Font-Bold="true" Text="text" runat="server" />
                                    </div>
                                    <div class="col-lg-4" style="text-align:center;color:rgb(205,17,24)">
                                        <img data-toggle="tooltip" data-placement="right" title="Tiempo máximo" width="50" src="../images/yms/tiempo-alarma.png" />
                                        <br />
                                        Máximo
                                        <br />
                                        <asp:Label ID="lbl_tiempoMaxCarga" Font-Bold="true" Text="text" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3" style="width:20%">
                            <div class="panel panel-info" style="height:306px">
                                <div class="panel-heading">
                                    <h3 class="panel-title" style="font-size:18px;text-align:center;height:30px">Andén descarga</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-lg-6" style="text-align:center">
                                        <img data-toggle="tooltip" title="Cantidad trailers" data-placement="right" width="80" src="../images/yms/lugar_ocupado.png" />
                                    </div>
                                    <div class="col-lg-6" style="text-align:center">
                                        <asp:Label ID="lbl_cantidad_cargado" style="font-size:36px;" runat="server" />
                                        <br />
                                        <span style="font-size:12px" class="label label-primary">Trailers</span>
                                    </div>
                                    <div class="col-xs-12 separador"></div>
                                    <hr style="width:100%;margin-left:0px;margin-right:0px;" />
                                    <div class="col-lg-4" style="color:rgb(245,179,65);text-align:center">
                                        <img data-toggle="tooltip" title="Tiempo promedio" data-placement="right" width="50" src="../images/yms/tiempo-atrasado.png" runat="server" />
                                <br />
                                        Promedio
                                <br />
                                        <asp:Label ID="lbl_tiempoPromedioDescarga" Font-Bold="true" Text="text" runat="server" />
                                    </div>
                                    <div class="col-lg-4" style="color:rgb(157,185,54);text-align:center">
                                        <img data-toggle="tooltip" title="Tiempo mínimo" data-placement="right" width="50" src="../images/yms/tiempo-ok.png" />
                                <br />
                                        Mínimo
                                <br />
                                        <asp:Label ID="lbl_tiempoMinDescarga" Font-Bold="true" Text="text" runat="server" />
                                    </div>
                                    <div class="col-lg-4" style="color:rgb(205,17,24);text-align:center">
                                        <img data-toggle="tooltip" title="Tiempo máximo" data-placement="right" width="50" src="../images/yms/tiempo-alarma.png" />
                                <br />
                                        Máximo
                                <br />
                                        <asp:Label ID="lbl_tiempoMaxDescarga" Font-Bold="true" Text="text" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4" style="width:45%">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <h3 class="panel-title" style="font-size:18px;text-align:center;height:30px">En estacionamiento</h3>
                                </div>
                                <div class="panel-body">
                                        <div class="col-lg-1" style="text-align:center;">
                                            <img data-toggle="tooltip" title="Estacionamientos ocupados" data-placement="right" width="40" src="../images/yms/estacionamiento.png" />
                                        </div>
                                        <div class="col-lg-2" style="text-align:center;">
                                            <asp:Label ID="lbl_cantidadEstacionamiento" Font-Bold="true" Text="text" runat="server" />
                                            <br />
                                            <span style="font-size:12px" class="label label-primary">Cantidad</span>
                                        </div>
                                        <div class="col-lg-1" style="text-align:center;">
                                            <img data-toggle="tooltip" title="Trailers vacíos" data-placement="right" width="40" src="../images/yms/trailer_vacio.png" />
                                        </div>
                                        <div class="col-lg-2" style="text-align:center;">
                                            <asp:Label ID="lbl_cantidadEstacionamiento_vacio" Font-Bold="true" Text="text" runat="server" />
                                            <br />
                                            <span style="font-size:12px" class="label label-primary">Vacíos</span>
                                        </div>
                                        <div class="col-lg-1" style="text-align:center;">
                                            <img data-toggle="tooltip" title="Trailers cargados" data-placement="right" width="40" src="../images/yms/trailer_cargado.png" />
                                        </div>
                                        <div class="col-lg-2" style="text-align:center;">
                                            <asp:Label ID="lbl_cantidadEstacionamiento_cargado" Font-Bold="true" Text="text" runat="server" />
                                            <br />
                                            <span style="font-size:12px" class="label label-primary">Cargados</span>
                                        </div>
                                        <div class="col-lg-1" style="text-align:center;color:rgb(245,179,65)">
                                            <img data-toggle="tooltip" title="Tiempo en estacionamiento" data-placement="right" width="40" src="../images/yms/tiempo-atrasado.png" />
                                        </div>
                                        <div class="col-lg-2" style="text-align:center;color:rgb(245,179,65)">
                                            Promedio
                                    <br />
                                            <asp:Label ID="lbl_tiempoPromedioEstacionamiento" Font-Bold="true" Text="text" runat="server" />
                                        </div>
                                </div>
                            </div>
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <h3 class="panel-title" style="font-size:18px;text-align:center;height:30px">Andenes carga/descarga</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-lg-2">
                                        <img data-toggle="tooltip" title="Libres" data-placement="right" width="70" src="../images/yms/lugar_vacio.png" />
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Label ID="lbl_andenesLibres" style="font-size:36px;" runat="server" />
                                        <br />
                                        <span style="font-size:12px" class="label label-primary">Libres</span>
                                    </div>
                                    <div class="col-lg-2">
                                        <img data-toggle="tooltip" title="Ocupados" data-placement="right" width="70" src="../images/yms/lugar_ocupado.png" />
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Label ID="lbl_andenesOcupados" style="font-size:36px;" runat="server" />
                                        <br />
                                        <span style="font-size:12px" class="label label-primary">Ocupados</span>
                                    </div>
                                    <div class="col-lg-3 col-lg-push-1">
                                        <div class="GaugeMeter" id="GaugeMeter_2" data-append="%" data-style="Full" data-width="6" data-size="80">
                                        </div>
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
            $("#GaugeMeter_2").gaugeMeter({ percent: porcentaje});
        }
    </script>
    <style type="text/css">
        .panel-body{
            padding-left:0px;
            padding-right:0px;
        }
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
                Margin: 0 0;
                width: 80%;
            }

            .GaugeMeter S,
            .GaugeMeter U {
                Text-Decoration: None;
                Font-Size: .6em;
                Opacity: .5;
            }

            .GaugeMeter B {
                Color: Black;
                Font-Weight: 300;
                Font-Size: .8em;
                Opacity: .8;
            }
    </style>
</asp:Content>


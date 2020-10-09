<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inicioQYMS2.aspx.cs" Inherits="inicioQYMS2" %>

<meta name="viewport" content="width=device-width, initial-scale=1.0">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
      ::: QYMS :::
      </title>
    <link rel="SHORTCUT ICON" href="img/img_icono_q/q.ico" />
    <link href="css2/Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css2/Font.css" rel="stylesheet" type="text/css" />
    <link href="css2/Progresbar.css" rel="stylesheet" type="text/css" />
    <link href="css2/Login.css" rel="stylesheet" type="text/css" />
    <script src="js2/jquery-3.2.1.js" type="text/javascript"></script>
    <script src="js2/Jquery.ui.1.12.1.js" type="text/javascript"></script>
    <script src="js2/Bootstrap.js" type="text/javascript"></script>
    <style type="text/css">
        #myCarousel
        {
            min-height: 120vh;
            position: absolute;
            filter:brightness(0.5);
            width: 100%;
        }
        
        #IMGPie
        {
            position: absolute;
            filter: blur(0.5px);
        }
        body
        {
            overflow: hidden;
            
        }
    </style>
</head>
<body>
    <form runat="server">
    <asp:ScriptManager ID="SCM" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid" style="width: 100%!important; padding-left:0px!important">
        <%--<img id="IMGFondo" src="./img/img_fondo/fondo_login_blanco2.jpg" class="img-responsive" />--%>
        <div id="myCarousel" class="carousel slide" data-ride="carousel" style="border: 0px!important">
            <!-- Indicators -->
            <ol class="carousel-indicators">
                <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                <li data-target="#myCarousel" data-slide-to="1"></li>
                <li data-target="#myCarousel" data-slide-to="2"></li>
                <li data-target="#myCarousel" data-slide-to="3"></li>
            </ol>
            <!-- Wrapper for slides -->
            <div class="carousel-inner">
                <div class="item active">
                    <img src="./img/img_fondo/CAMION1.jpg" alt="Patente" style="width: 100%; height: 120vh"
                        class="img-responsive" />
                </div>
                <div class="item">
                    <img src="./img/img_fondo/BODEGA1.jpg" alt="Bodega" style="width: 100%; height: 120vh"
                        class="img-responsive" />
                </div>
                <div class="item">
                    <img src="./img/img_fondo/BARCO1.jpg" alt="Barco" style="width: 100%; height: 120vh"
                        class="img-responsive" />
                </div>
                <div class="item">
                    <img src="./img/img_fondo/MOBILE1.jpg" alt="Mobile" style="width: 100%; height: 120vh"
                        class="img-responsive" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                <center>
                    <img id="IMG1" class="img-responsive" src="img/img_icono_q/MMPFQ.png" />
                </center>
            </div>
            <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                <center>
                    <img id="IMGL" class="img-responsive" src="img/img_icono_q/logo_login.png" />
                    <img id="IMGD" class="img-responsive" src="img/img_icono_q/logo_loginCliente.png" />
                    <img id="IMGT" class="img-responsive" src="img/img_icono_q/logo_loginTrans.png" />
                    <img id="IMGA" class="img-responsive" src="img/img_icono_q/logo_loginAcceso.png" />
                </center>
            </div>
            <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                <center>
                    <img id="IMG2" class="img-responsive" src="img/img_icono_q/Logo_h.png" />
                </center>
            </div>
        </div>
        <div class="row">
            <p>
            </p>
        </div>
        <div class="row">
            <div class="col-xs-1 col-sm-2 col-md-3 col-lg-4">
            </div>
            <div class="col-xs-10 col-sm-8 col-md-6 col-lg-4">
                <asp:Panel ID="PL" runat="server" DefaultButton="BtnLogin">
                    <div id="LUsuario" class="jumbotron" style="background: rgba(29,81,143, 0.6)!important;">
                        <div class="container">
                            <div class="form-group">
                                <label for="Usuario">
                                    Usuario:</label>
                                <input type="text" class="form-control" id="UsuarioL" runat="server" />
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 linea">
                            </div>
                            <div class="form-group">
                                <label for="Contraseña">
                                    Contraseña:</label>
                                <input type="password" class="form-control" id="ContrasenaL" runat="server" />
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 linea">
                            </div>
                            <div class="form-group">
                                <p>
                                </p>
                            </div>
                            <center>
                                <asp:UpdatePanel ID="UPL" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="BtnLogin" runat="server" CssClass="btn btn-primary ButtonColor" Text="Ingresar" OnClick="btnEntrar_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </center>
                            <p>
                            </p>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PD" runat="server" DefaultButton="BtnDestino">
                    <div id="LDestino" class="jumbotron" style="background: rgba(29,81,143, 0.6)!important;">
                        <div class="container">
                            <div class="form-group">
                                <label for="Usuario">
                                    Codigo:</label>
                                <input type="text" class="form-control" id="UsuarioD" runat="server" />
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 linea">
                            </div>
                            <div class="form-group">
                                <label for="Contraseña">
                                    Contraseña:</label>
                                <input type="password" class="form-control" id="ContrasenaD" runat="server" />
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 linea">
                            </div>
                            <div class="form-group">
                                <label for="Contraseña">
                                    Cliente:</label>
                                <input type="text" class="form-control" id="ClienteD" runat="server" onkeyup="javascript:this.value=this.value.toUpperCase();" />
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 linea">
                            </div>
                            <div class="checkbox">
                                <label>
                                    <input type="checkbox" value="" id="CHKM" runat="server" />Mercado</label>
                            </div>
                            <center>
                                <asp:UpdatePanel ID="UPD" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="BtnDestino" runat="server" CssClass="btn btn-primary ButtonColor"
                                            Text="Ingresar" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </center>
                            <p>
                            </p>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PT" runat="server" DefaultButton="BtnProveedor">
                    <div id="LTransporte" class="jumbotron" style="background: rgba(29,81,143, 0.6)!important;">
                        <div class="container">
                            <div class="form-group">
                                <label for="Usuario">
                                    Usuario:</label>
                                <input type="text" class="form-control" id="UsuarioT" runat="server" />
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 linea">
                            </div>
                            <div class="form-group">
                                <label for="Contraseña">
                                    Contraseña:</label>
                                <input type="password" class="form-control" id="ContrasenaT" runat="server" />
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 linea">
                            </div>
                            <div class="form-group">
                                <label for="Contraseña">
                                    Proveedor:</label>
                                <input type="text" class="form-control" id="ProveedorT" runat="server" onkeyup="javascript:this.value=this.value.toUpperCase();" />
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 linea">
                            </div>
                            <center>
                                <asp:UpdatePanel ID="UPT" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="BtnProveedor" runat="server" CssClass="btn btn-primary ButtonColor" OnClick="BtnProveedor_Click"
                                            Text="Ingresar" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </center>
                            <p>
                            </p>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PC" runat="server" DefaultButton="BtnAcceso">
                    <div id="LContrasena" class="jumbotron" style="background: rgba(29,81,143, 0.6)!important;">
                        <div class="container">
                            <div class="form-group">
                                <label for="Usuario">
                                    Usuario:</label>
                                <input type="text" class="form-control" id="UsuarioC" runat="server" />
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 linea">
                            </div>
                            <div class="form-group">
                                <label for="Contraseña">
                                    Contraseña Actual:</label>
                                <input type="password" class="form-control" id="ContrasenaC" runat="server" />
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 linea">
                            </div>
                            <div class="form-group">
                                <label for="Contraseña">
                                    Contraseña Nueva:</label>
                                <input type="password" class="form-control" id="ContrasenanC" runat="server" />
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 linea">
                            </div>
                            <center>
                                <asp:UpdatePanel ID="UPA" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="BtnAcceso" runat="server" CssClass="btn btn-primary ButtonColor"
                                            Text="Cambiar" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </center>
                            <p>
                            </p>
                        </div>
                    </div>
                </asp:Panel>
                <div>
                    <div class="btn-group btn-group-justified">
                        <a id="Login" href="#" class="btn btn-primary AColor">Login</a>
                        <a id="Transporte" href="#" class="btn btn-primary AColor">Acceso Proveedores</a> 
                        <%--<a id="Contrasena" href="#" class="btn btn-primary AColor">Contraseña</a>--%>
                    </div>
                </div>

                <div id="myModal" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header modal-header-error">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">
                                    Alerta!</h4>
                            </div>
                            <div class="modal-body">
                                <p>
                                    <asp:UpdatePanel ID="UE" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="LME" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </p>
                            </div>
                            <div class="modal-footer modal-header-error">
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myModal2" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header modal-header-ok">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">
                                    Ok</h4>
                            </div>
                            <div class="modal-body">
                                <p>
                                    <asp:UpdatePanel ID="UO" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="LMO" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </p>
                            </div>
                            <div class="modal-footer modal-header-ok">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-1 col-sm-2 col-md-3 col-lg-4">
            </div>
        </div>
        <%--<br />
        <br />
        <div class="row">
            <div class="col-xs-1 col-sm-2 col-md-3 col-lg-4">
            </div>
            <div class="col-xs-10 col-sm-8 col-md-6 col-lg-4">
                <img id="IMGPie" class="img-responsive" src="img/img_fondo/operacionQ2.png" /></div>
            <div class="col-xs-1 col-sm-2 col-md-3 col-lg-4">
            </div>
        </div>--%> 
    </div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UPL">
        <ProgressTemplate>
            <div class="overlay">
                <div class="overlayContent">
                    <center>
                        <img src="img/progress_mmpfq.gif" alt="" />
                    </center>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UPD">
        <ProgressTemplate>
            <div class="overlay">
                <div class="overlayContent">
                    <center>
                        <img src="img/progress_mmpfq.gif" alt="" />
                    </center>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress3" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UPT">
        <ProgressTemplate>
            <div class="overlay">
                <div class="overlayContent">
                    <center>
                        <img src="img/progress_mmpfq.gif" alt="" />
                    </center>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress4" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UPA">
        <ProgressTemplate>
            <div class="overlay">
                <div class="overlayContent">
                    <center>
                        <img src="img/progress_mmpfq.gif" alt="" />
                    </center>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <script type="text/javascript">
        $("#LTransporte").hide();
        $("#LContrasena").hide();
        $("#LDestino").hide();

        $("#IMGT").hide();
        $("#IMGA").hide();
        $("#IMGD").hide();

        $("#Login").on("click", function () {
            $("#LUsuario").fadeIn('slow');
            $("#LTransporte").hide();
            $("#LContrasena").hide();
            $("#LTransporte").hide();
            $("#LDestino").hide();

            $("#IMGL").fadeIn('slow');
            $("#IMGT").hide();
            $("#IMGA").hide();
            $("#IMGD").hide();

        });

        $("#Destino").on("click", function () {
            $("#LUsuario").hide();
            $("#LTransporte").hide();
            $("#LContrasena").hide();
            $("#LTransporte").hide();
            $("#LDestino").fadeIn('slow');

            $("#IMGL").hide();
            $("#IMGT").hide();
            $("#IMGA").hide();
            $("#IMGD").fadeIn('slow');
        });

        $("#Transporte").on("click", function () {
            $("#LUsuario").hide();
            $("#LTransporte").hide();
            $("#LContrasena").hide();
            $("#LTransporte").fadeIn('slow');
            $("#LDestino").hide();

            $("#IMGL").hide();
            $("#IMGT").fadeIn('slow');
            $("#IMGA").hide();
            $("#IMGD").hide();
        });

        $("#Contrasena").on("click", function () {
            $("#LUsuario").hide();
            $("#LTransporte").hide();
            $("#LContrasena").fadeIn('slow');
            $("#LTransporte").hide();
            $("#LDestino").hide();

            $("#IMGL").hide();
            $("#IMGT").hide();
            $("#IMGA").fadeIn('slow');
            $("#IMGD").hide();
        });

        function MSGError(x) {
            document.getElementById('LME').innerHTML = x;
            $("#myModal").modal()
        }

        function MSGOk(x) {
            document.getElementById('LMO').innerHTML = x;
            $("#myModal2").modal()
        }
    </script>
    </form>
</body>
</html>

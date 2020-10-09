<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="Css/LoginTms.css" rel="stylesheet" type="text/css" />
    <link href="Css/fonts.css" rel="stylesheet" type="text/css" />
    <title>::: QYMS :::</title>
    <link rel="SHORTCUT ICON" href="../img/Nueva/q.ico"/>
    <style type="text/css">
        #effect
        {
            height: 164px;
        }
    </style>
    <script type="text/javascript">
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
        <div id="abc" class="abc">
            <asp:UpdatePanel ID="logo" runat="server">
                <ContentTemplate>
                    <asp:Image ID="Img1" runat="server" src="img/Nuevas/logo_login_qmtms.jpg" alt="QMGPS"
                        class="logoQ" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="dialog"  style="display: none; font-size:small" align="center">
            </div>
            <div id="effect" class="effect">
                <table class="tableLoginCliente">
                    <tr>
                        <td class="td">
                            <asp:Label ID="Label1" runat="server" Text="Usuario" CssClass="lblCliente"></asp:Label><br />
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="txtLoginCliente"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername"
                                ErrorMessage="*Requerido" ValidationGroup="login">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="td">
                            <asp:Label ID="Label2" runat="server" Text="Contraseña" CssClass="lblCliente"></asp:Label><br />
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="txtLoginCliente"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                ErrorMessage="*Requerido" ValidationGroup="login">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; height: 70px;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:ImageButton ID="btnEntrar" runat="server"
                                        ImageUrl="~/Img/Nuevas/login_ingresar.png" onclick="btnEntrar_Click"/>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnlpass" runat="server" CssClass="popupConfirmation" Width="40%" style="display:none">
                    <div class="popup_Container">
                        <div class="popup_Titlebar" id="PopupHeader">
                            <div class="TitlebarLeft">
                                Confirmación clave de usuario
                            </div>  
                        </div>
                        <div class="popup_Body">
                            <table width="100%">
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Label Text="Activación usuario" runat="server" CssClass="tituloNavCentro"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="width:40%">
                                        <asp:Label Text="Ingrese contraseña nueva:" runat="server" CssClass="label"></asp:Label>
                                    </td>
                                    <td align="center" style="width:60%">
                                        <asp:TextBox ID="tbnueva" runat="server" TextMode="Password" CssClass="textBox"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbnueva"
                                            Display="None" ErrorMessage="* Requerido" ValidationGroup="activar" SetFocusOnError="true">
                                        </asp:RequiredFieldValidator>
                                        <asp:ValidatorCalloutExtender ID="RequiredFieldValidator2_ValidatorCalloutExtender"
                                            runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                        </asp:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="width:40%">
                                        <asp:Label ID="Label3" Text="Ingrese contraseña nuevamente:" runat="server" CssClass="label"></asp:Label>
                                    </td>
                                    <td align="center" style="width:60%">
                                        <asp:TextBox ID="tbnuevar" runat="server" TextMode="Password" CssClass="textBox"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbnuevar"
                                            Display="None" ErrorMessage="* Requerido" ValidationGroup="activar" SetFocusOnError="true">
                                        </asp:RequiredFieldValidator>
                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1"
                                            runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                        </asp:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:ImageButton ID="btnactivar" runat="server" ValidationGroup="activar" 
                                            ImageUrl="~/Img/Nuevas/activar.png" 
                                            Width="100px" onclick="btnactivar_Click"/>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
                <asp:HiddenField ID="hidenalgo2" runat="server" />
                <asp:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="ModalPopupBG" runat="server"
                                        TargetControlID="hidenalgo2" PopupControlID="pnlpass">
                </asp:ModalPopupExtender>
            </div>
        </div>
    </form>
</body>
</html>

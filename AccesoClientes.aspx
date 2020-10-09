<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccesoClientes.aspx.cs" Inherits="AccesoClientes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/Css/login.css" rel="stylesheet" type="text/css" />
    <title>::: TRACKING SOTRASER :::</title>
    <link rel="SHORTCUT ICON" href="../img/beot.png"/>
    <style type="text/css">
        .style1
        {
            width: 335px;
        }
        .style2
        {
            width: 145px;
        }
        .style3
        {
            text-align: left;
        }
        .style4
        {
            width: 116px;
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <div class="banner">
            <div class="titulo">
            </div>
            <div class="logo">
            </div>
            <div class="tituloNav">
                <asp:Label ID="lblTituloNav" runat="server" Text="Acceso Clientes"></asp:Label>
            </div>
        </div>
        <br />
        <br />
        <br />
        <br />
        <div class="lineaFondo">
        </div>
        <br />
        <table width="100%">
            <tr>
                <td style="width: 20%">
                    &nbsp;
                </td>
                <td style="width: 60%">
                    <asp:Panel ID="Panel1" runat="server" BackImageUrl="~/Img/fondoLogin.jpg" Height="319px"
                        Width="100%" BorderColor="White" BorderWidth="2">
                        <table style="width: 100%;">
                            <tr>
                                <td class="style1">
                                    &nbsp;
                                </td>
                                <td class="style4">
                                    &nbsp;
                                </td>
                                <td class="style2">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp;
                                </td>
                                <td class="style4">
                                    &nbsp;
                                </td>
                                <td class="style2">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                   
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp;
                                </td>
                                <td class="style4">
                                    &nbsp;
                                </td>
                                <td class="style2">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                            </tr>
                             <tr>
                                <td class="style1">
                                    &nbsp;
                                </td>
                                <td class="style4">
                                    &nbsp;
                                </td>
                                <td class="style2">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                            </tr>
                             <tr>
                                <td class="style1">
                                    &nbsp;
                                </td>
                                <td class="style4">
                                    &nbsp;
                                </td>
                                <td class="style2">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp;
                                </td>
                                <td class="style4">
                                    &nbsp;<asp:Label ID="Label1" runat="server" Style="text-align: right" Text="Usuario :  "></asp:Label>
                                </td>
                                <td class="style2">
                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="textBox" Width="150px" Font-Bold="False"></asp:TextBox>
                                </td>
                                <td class="style3">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername"
                                        ErrorMessage="*" ValidationGroup="login"></asp:RequiredFieldValidator>
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp;
                                </td>
                                <td class="style4">
                                    <asp:Label ID="Label2" runat="server" Text="Password : "></asp:Label>
                                </td>
                                <td class="style2">
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textBox"
                                        Width="150px"></asp:TextBox>
                                </td>
                                <td class="style3">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                        ErrorMessage="*" ValidationGroup="login"></asp:RequiredFieldValidator>
                                </td>
                                <td class="style3" valign="top">
                                    <asp:Button ID="btnEntrar" runat="server" CssClass="botonNormal" OnClick="btnEntrar_Click"
                                        Text="Entrar" ValidationGroup="login" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp;
                                </td>
                                <td class="style4">
                                    &nbsp;
                                </td>
                                <td class="style2">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp;
                                </td>
                                <td class="style4">
                                    &nbsp;
                                </td>
                                <td class="style2">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp;
                                </td>
                                <td class="style4">
                                    &nbsp;
                                </td>
                                <td class="style2">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp;
                                </td>
                                <td class="style4">
                                    &nbsp;
                                </td>
                                <td class="style2">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp;
                                </td>
                                <td class="style4">
                                    &nbsp;
                                </td>
                                <td class="style2">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp;
                                </td>
                                <td class="style4">
                                    &nbsp;
                                </td>
                                <td class="style2">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp;
                                </td>
                                <td class="style4">
                                    &nbsp;
                                </td>
                                <td class="style2">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp;
                                </td>
                                <td class="style4">
                                    &nbsp;
                                </td>
                                <td class="style2">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                      <asp:Button ID="btnVolver" runat="server" CssClass="botonNormal" OnClick="btnVolver_Click"
                                        Text="Volver" Visible="false" />
                                </td>
                                <td class="style4">
                                    &nbsp;
                                </td>
                                <td class="style2">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td style="width: 20%">
                    &nbsp;
                </td>
            </tr>
        </table>
        <div class="lineaFondo">
        </div>
        <br />
        <br />
        <br />
        <br />
    </div>
    </form>
</body>
</html>

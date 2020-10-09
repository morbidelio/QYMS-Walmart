<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Encriptar_Pass.aspx.cs" Inherits="App_Encriptar_Pass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css2/Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../css2/Font.css" rel="stylesheet" type="text/css" />
    <link href="../css2/Estilo-Nuevo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:LinkButton ID="btn_encriptar" OnClick="btn_encriptar_Click" CssClass="btn btn-primary" runat="server">
            <span class="glyphicon glyphicon-lock"></span>
        </asp:LinkButton>
        <div class="col-xs-6" style="height:60vh;overflow-y:auto">
            <asp:Literal id="ltl" runat="server" />
            <asp:GridView ID="gv1" AutoGenerateColumns="false" runat="server" >
                <Columns>
                    <asp:BoundField DataField="ID" />
                    <asp:BoundField DataField="USERNAME" />
                    <asp:BoundField DataField="PASS" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="col-xs-6" style="height:60vh;overflow-y:auto">
            <asp:Literal id="desencriptar" runat="server" />
            <asp:GridView ID="gv2" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>


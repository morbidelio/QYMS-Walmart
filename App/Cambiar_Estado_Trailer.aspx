<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cambiar_Estado_Trailer.aspx.cs" Inherits="Cambiar_Estado_Trailer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css2/Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../css2/Font.css" rel="stylesheet" type="text/css" />
    <link href="../css2/Estilo-Nuevo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="col-xs-2">
            SITE
            <br />
            <asp:DropDownList id="ddl_site" OnSelectedIndexChanged="site_indexChanged" AutoPostBack="true" runat="server">
                <asp:ListItem Text="text1" />
                <asp:ListItem Text="text2" />
            </asp:DropDownList>
        </div>
        <div class="col-xs-2">
            trailer
            <br />
            <asp:DropDownList id="ddl_trailer" runat="server">
                <asp:ListItem Text="text1" />
                <asp:ListItem Text="text2" />
            </asp:DropDownList>
        </div>
    </div>
    </form>
</body>
</html>

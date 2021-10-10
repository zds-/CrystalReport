<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="WebApplication1.WebForm2" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <asp:Button ID="Button1" runat="server" Text="SP Norwind" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="SP Norwind2" OnClick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" Text="SP Norwind2 and export to pdf" OnClick="Button3_Click" />
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
             
        </div>
    </form>
</body>
</html>

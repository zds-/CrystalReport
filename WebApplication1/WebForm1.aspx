<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/WebForm2.aspx">Page2</asp:HyperLink>
            <asp:Button ID="Button1" runat="server" Text="Norwind" OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" Text="Norwind2" OnClick="Button1_Click" />
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
            <CR:CrystalReportViewer ID="CrystalReportViewer" runat="server" AutoDataBind="true" />
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeView2.aspx.cs" Inherits="FineUIPro.Web.Notice.NoticeView2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table id="Table1" runat="server" width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" valign="middle" style="font-size: 15pt; font-weight: bold">
                    <asp:Label runat="server" ID="lbTitle"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" valign="middle" style="font-size: 11pt;">
                    <asp:Label runat="server" ID="lbMainContent"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

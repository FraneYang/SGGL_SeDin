<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintDesigner2.aspx.cs"
    Inherits="Web.ReportPrint.PrintDesigner2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function CheckDropDownList(source, args) {
            if (args.Value == "0")
                args.IsValid = false;
            else
                args.IsValid = true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" runat="server" width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="height: 10px" colspan="3">
            </td>
        </tr>
        <tr>
            <td align="right" style="height: 32px" width="15%">
                <asp:Label ID="Label3" runat="server" Text="选择设计报表"></asp:Label>&nbsp;
            </td>
            <td align="left" style="height: 32px" width="40%">
                <asp:DropDownList ID="drpPrintReport" runat="server" Height="22" Width="81%" CssClass="textboxStyle">
                </asp:DropDownList>
                <asp:CustomValidator ID="CustomValidator3" runat="server" Display="Dynamic" ErrorMessage="&quot;请选择所要设计的报表！&quot;"
                    ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpPrintReport" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
            </td>
            <td align="left" style="height: 32px" width="45%">
                <asp:Button ID="BtnReportDesigner" runat="server" Text="报表设计" OnClick="BtnReportDesigner_Click"
                    ValidationGroup="Save" />&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" Style="z-index: 101; left: 7px; position: absolute;
        top: -5px" runat="server" HeaderText="请注意！" ShowMessageBox="True" ShowSummary="False"
        ValidationGroup="Save" />
    </form>
</body>
</html>

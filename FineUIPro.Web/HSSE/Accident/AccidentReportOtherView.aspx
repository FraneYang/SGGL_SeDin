<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccidentReportOtherView.aspx.cs"
    ValidateRequest="false" Inherits="FineUIPro.Web.HSSE.Accident.AccidentReportOtherView" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看事故调查处理报告</title>
    <link href="../Styles/Style.css" rel="stylesheetasp" type="text/css" />
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style type="text/css">
        .BackColor
        {
            color: Red;
            background-color: Silver;
        }
        .titler
        {
            color: Black;
            font-size: large;
        }
        .itemTitle
        {
            color: Black;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel8" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Toolbars>
            <f:Toolbar ID="Toolbar3" Position="Top" runat="server">
                <Items>
                    <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                        EnableAjax="false" DisableControlBeforePostBack="false">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:ContentPanel runat="server" AutoScroll="true" Height="500px" ShowHeader="false"
                ID="ContentPanel2">
                <table id="Table1" runat="server" width="100%" cellpadding="0" cellspacing="0" align="center">
                    <tr>
                        <td>
                            <table id="Table2" runat="server" align="center" valign="top" width="100%" cellpadding="0"
                                cellspacing="0" class="table" style="border-collapse: collapse;" border="1">
                                <tr>
                                    <td rowspan="2" align="center" style="width: 30%;">
                                        <img id="image" runat="server" alt="" src="../Images/Null.jpg" />
                                    </td>
                                    <td colspan="2" align="center" style="width: 40%;">
                                        <asp:Label ID="lblProjectName" runat="server" Font-Size="11pt"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 30%;">
                                        &nbsp;项目号：
                                        <asp:Label ID="lblProjectCode" runat="server" Width="65%" CssClass="textboxnoneborder"
                                            ReadOnly="True" Font-Size="Small"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Label ID="Label6" runat="server" Width="100%" Font-Bold="True" Font-Size="X-Large"
                                            Text="事故调查处理报告"></asp:Label>
                                    </td>
                                    <td align="left" style="border: 1px solid #000000; border-top: none; border-left: none;">
                                        &nbsp;事故编号：<asp:Label ID="txtAccidentReportOtherCode" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table id="Table3" runat="server" align="center" valign="top" width="100%" cellpadding="0"
                                cellspacing="0" class="table" style="border-collapse: collapse;" border="1">
                                <tr style="height: 32px;">
                                    <td align="right">
                                        事故类型：
                                    </td>
                                    <td align="left">
                                        <asp:Label runat="server" ID="txtAccidentTypeName"></asp:Label>
                                    </td>
                                    <td align="right">
                                        提要：
                                    </td>
                                    <td align="left" colspan="3">
                                        <asp:Label runat="server" ID="txtAbstract"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="right">
                                        发生区域：
                                    </td>
                                    <td align="left">
                                        <asp:Label runat="server" ID="txtWorkAreaName"></asp:Label>
                                    </td>
                                    <td align="right">
                                        发生时间：
                                    </td>
                                    <td align="left">
                                        <asp:Label runat="server" ID="txtAccidentDate"></asp:Label>
                                    </td>
                                    <td align="right">
                                        人数：
                                    </td>
                                    <td align="left">
                                        <asp:Label runat="server" ID="txtPeopleNum"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="right" style="width: 10%;">
                                        事故责任单位：
                                    </td>
                                    <td align="left" style="width: 30%;">
                                        <asp:Label runat="server" ID="txtUnitName"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 10%;">
                                        直接经济损失：
                                    </td>
                                    <td align="left" style="width: 20%;">
                                        <asp:Label runat="server" ID="txtEconomicLoss"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 10%;">
                                        间接经济损失：
                                    </td>
                                    <td align="left" style="width: 20%;">
                                        <asp:Label runat="server" ID="txtEconomicOtherLoss"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="right">
                                        报告人单位：
                                    </td>
                                    <td align="left">
                                        <asp:Label runat="server" ID="txtReporterUnit"></asp:Label>
                                    </td>
                                    <td align="right">
                                        报告人：
                                    </td>
                                    <td align="left">
                                        <asp:Label runat="server" ID="txtReportMan"></asp:Label>
                                    </td>
                                    <td align="right">
                                        报告时间：
                                    </td>
                                    <td align="left">
                                        <asp:Label runat="server" ID="txtReportDate"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="right">
                                        事故过程描述：
                                    </td>
                                    <td align="left" colspan="5">
                                        <asp:Label runat="server" ID="txtProcessDescription"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="right">
                                        紧急措施：
                                    </td>
                                    <td align="left" colspan="5">
                                        <asp:Label runat="server" ID="txtEmergencyMeasures"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="right">
                                        直接原因：
                                    </td>
                                    <td align="left" colspan="5">
                                        <asp:Label runat="server" ID="txtImmediateCause"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="right">
                                        间接原因：
                                    </td>
                                    <td align="left" colspan="5">
                                        <asp:Label runat="server" ID="txtIndirectReason"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="right">
                                        整改及预防措施：
                                    </td>
                                    <td align="left" colspan="5">
                                        <asp:Label runat="server" ID="txtCorrectivePreventive"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="right">
                                        报告编制：
                                    </td>
                                    <td align="left">
                                        <asp:Label runat="server" ID="txtCompileManName"></asp:Label>
                                    </td>
                                    <td align="right">
                                        日期：
                                    </td>
                                    <td align="left" colspan="3">
                                        <asp:Label runat="server" ID="txtCompileDate"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="6">
                                        调查组成员
                                    </td>
                                </tr>
                                <tr id="Tr1" runat="server" style="height: 32px;">
                                    <td id="Td1" runat="server" align="left" colspan="10">
                                        <asp:GridView ID="Grid1" runat="server" AllowSorting="True" PageSize="100" ShowFooter="true"
                                            AutoGenerateColumns="False" HorizontalAlign="Left" Width="100%">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="false" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="单位">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# ConvertUnit(Eval("UnitId")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="40%" Height="32px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="姓名">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# ConvertPerson(Eval("PersonId")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="20%" Height="32px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="职务">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# ConvertPosition(Eval("PositionId")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="25%" Height="32px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </f:ContentPanel>
        </Items>
        <Toolbars>
            <f:Toolbar ID="Toolbar4" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="Label22">
                    </f:Label>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill3" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Panel>
    </form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
</body>
</html>

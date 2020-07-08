<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccidentReportView.aspx.cs"
    EnableEventValidation="false" Inherits="FineUIPro.Web.HSSE.Accident.AccidentReportView"
    ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查看事故调查报告</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel8" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
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
                                        <asp:Label ID="Label2" runat="server" Width="100%" Font-Bold="True" Font-Size="X-Large"
                                            Text="事故调查报告"></asp:Label>
                                    </td>
                                    <td align="left" style="border: 1px solid #000000; border-top: none; border-left: none;">
                                        &nbsp;事故编号：<asp:Label ID="txtAccidentReportCode" runat="server"></asp:Label>
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
                                        是否待定：
                                    </td>
                                    <td align="left">
                                        <asp:Label runat="server" ID="txtIsNotConfirm"></asp:Label>
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
                                    <td align="left" colspan="3">
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
                                        工时损失：
                                    </td>
                                    <td align="left" style="width: 20%;">
                                        <asp:Label runat="server" ID="txtWorkingHoursLoss"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 10%;">
                                        直接经济损失：
                                    </td>
                                    <td align="left" style="width: 5%;">
                                        <asp:Label runat="server" ID="txtEconomicLoss"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 10%;">
                                        间接经济损失：
                                    </td>
                                    <td align="left" style="width: 5%;">
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
                                    <td align="left" colspan="3">
                                        <asp:Label runat="server" ID="txtReportDate"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="right">
                                        事故过程描述：
                                    </td>
                                    <td align="left" colspan="7">
                                        <asp:Label runat="server" ID="txtProcessDescription"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="right">
                                        紧急措施：
                                    </td>
                                    <td align="left" colspan="7">
                                        <asp:Label runat="server" ID="txtEmergencyMeasures"></asp:Label>
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
                                    <td align="left" colspan="5">
                                        <asp:Label runat="server" ID="txtCompileDate"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </f:ContentPanel>
        </Items>
        <Toolbars>
        <f:Toolbar ID="Toolbar2" Position="Bottom" ToolbarAlign="Right" runat="server">
            <items>
            <f:Label runat="server" ID="lbTemp">
            </f:Label>
            <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
            </f:Button>
            <f:ToolbarFill ID="ToolbarFill1" runat="server">
            </f:ToolbarFill>
            <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
            </f:Button>
        </items>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RiskAnalysisChart.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.HiddenInspection.RiskAnalysisChart" %>

<%@ Register Src="~/Controls/ChartControl.ascx" TagName="ChartControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>巡检分析(图表)</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" AjaxAspnetControls="divAccidentUnit,divAccidentTime" />
    <f:Panel ID="Panel3" CssClass="blockpanel" runat="server" EnableCollapse="false"
        BodyPadding="10px" ShowBorder="true" ShowHeader="false">
        <Items>
            <f:Form ID="Form2" ShowHeader="false" ShowBorder="false" runat="server">
                <Rows>
                    <f:FormRow ColumnWidths="20% 3% 20% 30% 15% 10%">
                        <Items>
                            <f:DatePicker ID="txtStartRectificationTime" runat="server" Label="检查时间" LabelAlign="Right"
                                LabelWidth="80px">
                            </f:DatePicker>
                            <f:Label ID="Label3" runat="server" Text="至" Width="5px">
                            </f:Label>
                            <f:DatePicker ID="txtEndRectificationTime" runat="server">
                            </f:DatePicker>
                            <f:DropDownList runat="server" ID="drpType" >
                                <f:ListItem Text="按责任单位" Value="0" Selected="true"/>
                                <f:ListItem Text="按问题类型" Value="1" />
                                <f:ListItem Text="按危害因素" Value="2" />
                                <f:ListItem Text="按作业内容" Value="3" />
                                <f:ListItem Text="按导致伤害/事故" Value="4" />
                                <f:ListItem Text="按责任单位" Value="0" Selected="true"/>
                                <f:ListItem Text="按单位工程" Value="5" />
                            </f:DropDownList>
                            <f:Button ID="BtnAnalyse" Text="统计" Icon="ChartPie" runat="server" OnClick="BtnAnalyse_Click">
                            </f:Button>
                        </Items>
                    </f:FormRow>
                </Rows>
            </f:Form>
        </Items>
    </f:Panel>
    <f:Panel ID="Panel4" CssClass="blockpanel" runat="server"  EnableCollapse="false"
        BodyPadding="10px" ShowBorder="true" ShowHeader="false" >
        <Items>
            <f:ContentPanel ShowHeader="false" runat="server" ID="cpAccidentTime" Margin="0 0 0 0" AutoScroll="true">
                <div id="divAccidentTime" style="height:100%;width:100%">
                    <uc1:ChartControl ID="ChartAccidentTime" runat="server" />
                </div>
            </f:ContentPanel>
        </Items>
    </f:Panel>
    </form>
</body>
</html>

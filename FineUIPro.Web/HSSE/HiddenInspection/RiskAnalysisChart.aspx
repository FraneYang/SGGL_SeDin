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
                            <f:DropDownList ID="drpChartType" runat="server" Label="图形类型" AutoPostBack="true"
                                OnSelectedIndexChanged="drpChartType_SelectedIndexChanged" Width="300px" LabelWidth="80px">
                                <f:ListItem Value="Column" Text="柱形图"></f:ListItem>
                                <f:ListItem Value="Line" Text="折线图"></f:ListItem>
                                <f:ListItem Value="Pie" Text="饼形图"></f:ListItem>
                                <f:ListItem Value="StackedArea" Text="堆积面积图"></f:ListItem>
                                <f:ListItem Value="Spline" Text="样条图"></f:ListItem>
                                <f:ListItem Value="SplineArea" Text="样条面积图"></f:ListItem>
                                <f:ListItem Value="StepLine" Text="阶梯线图"></f:ListItem>
                                <f:ListItem Value="Stock" Text="股价图"></f:ListItem>
                                <f:ListItem Value="Radar" Text="雷达图"></f:ListItem>
                            </f:DropDownList>
                            <f:CheckBox ID="ckbShow" runat="server" LabelWidth="80px" Label="三维效果" AutoPostBack="true"
                                OnCheckedChanged="ckbShow_CheckedChanged" Width="110px">
                            </f:CheckBox>
                            <f:Button ID="BtnAnalyse" Text="统计" Icon="ChartPie" runat="server" OnClick="BtnAnalyse_Click">
                            </f:Button>
                        </Items>
                    </f:FormRow>
                    <f:FormRow >
                        <Items>
                            <f:RadioButtonList ID="rblState" runat="server" Label="分析类型" LabelWidth="80px" Width="250px"
                                AutoPostBack="true" OnSelectedIndexChanged="drpChartType_SelectedIndexChanged">
                                <f:RadioItem Value="0" Selected="true" Text="按责任单位" />
                                <f:RadioItem Value="1" Text="按检查项" />
                            </f:RadioButtonList>
                            <f:Label ID="aa" runat="server">
                            </f:Label>
                        </Items>
                    </f:FormRow>
                </Rows>
            </f:Form>
        </Items>
    </f:Panel>
    <f:Panel ID="Panel4" CssClass="blockpanel" runat="server"  EnableCollapse="false"
        BodyPadding="10px" ShowBorder="true" ShowHeader="false">
        <Items>
            <f:TabStrip ID="TabStrip1" CssClass="f-tabstrip-theme-simple" Height="360px" ShowBorder="true"
                TabPosition="Top" MarginBottom="5px" EnableTabCloseMenu="false" runat="server">
                <Tabs>
                    <f:Tab ID="Tab2" Title="按类别" BodyPadding="5px" Layout="Fit" IconFont="Bookmark" runat="server"
                        TitleToolTip="按类别统计">
                        <Items>
                            <f:ContentPanel ShowHeader="false" runat="server" ID="cpAccidentTime" Margin="0 0 0 0">
                                <div id="divAccidentTime">
                                    <uc1:ChartControl ID="ChartAccidentTime" runat="server" />
                                </div>
                            </f:ContentPanel>
                        </Items>
                    </f:Tab>
                </Tabs>
            </f:TabStrip>
        </Items>
    </f:Panel>
    </form>
</body>
</html>

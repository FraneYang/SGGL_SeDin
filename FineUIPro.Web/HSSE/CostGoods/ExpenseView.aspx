<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpenseView.aspx.cs" Inherits="FineUIPro.Web.HSSE.CostGoods.ExpenseView"
    ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查看安全费用计划</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="安全费用计划" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:Label ID="txtMonths" runat="server" Label="月份" LabelWidth="100px">
                    </f:Label>
                    <f:TextBox ID="txtExpenseCode" runat="server" Label="编号" LabelAlign="Right" MaxLength="50"
                        LabelWidth="230px" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="drpUnitId" runat="server" Label="填报单位" LabelAlign="Right" MaxLength="50"
                        LabelWidth="100px" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtReportDate" runat="server" Label="填报日期" LabelAlign="Right" MaxLength="50"
                        LabelWidth="230px" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <%--<f:FormRow>
                <Items>
                    <f:TextBox ID="txtCompileMan" runat="server" Label="编制人" Readonly="true" LabelAlign="Right" MaxLength="50" LabelWidth="200px"
                       >
                    </f:TextBox>
                    <f:TextBox ID="txtCompileDate" runat="server" Label="编制日期" LabelAlign="Right" MaxLength="50" LabelWidth="230px"
                        Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>--%>
            <%-- <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCheckMan" runat="server" Label="审核人" Readonly="true" LabelAlign="Right" MaxLength="50" LabelWidth="200px"
                        >
                    </f:TextBox>
                    <f:TextBox ID="txtCheckDate" runat="server" Label="审核日期" LabelAlign="Right" MaxLength="50" LabelWidth="230px"
                        Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtApproveMan" runat="server" Label="批准人" Readonly="true" LabelAlign="Right" MaxLength="50" LabelWidth="200px"
                        >
                    </f:TextBox>
                    <f:TextBox ID="txtApproveDate" runat="server" Label="批准日期" LabelAlign="Right" MaxLength="50" LabelWidth="230px"
                        Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>--%>
            <f:FormRow>
                <Items>
                    <f:Panel ID="Panel2" runat="server" ShowBorder="false" Layout="Table" TableConfigColumns="5"
                        ShowHeader="false" BodyPadding="1px">
                        <Items>
                            <f:Panel ID="Panel1" Title="Panel1" MarginRight="0" Width="100px"
                                runat="server" BodyPadding="1px" ShowBorder="false" ShowHeader="false">
                                <Items>
                                    <f:Label runat="server" ID="Label58" Text="编号">
                                    </f:Label>
                                </Items>
                            </f:Panel>
                            <f:Panel ID="Panel3" Title="Panel1" runat="server" BodyPadding="1px" Width="200px"
                                ShowBorder="false" ShowHeader="false" MarginLeft="50">
                                <Items>
                                    <f:Label runat="server" ID="lblAccidentType11" Text="类别">
                                    </f:Label>
                                </Items>
                            </f:Panel>
                            <f:Panel ID="Panel5" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                Width="200px" ShowHeader="false" MarginLeft="60px">
                                <Items>
                                    <f:Label runat="server" ID="Label1" Text="当期费用（元）">
                                    </f:Label>
                                </Items>
                            </f:Panel>
                            <f:Panel ID="Panel6" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                Width="250px" ShowHeader="false" MarginLeft="115px">
                                <Items>
                                    <f:Label runat="server" ID="Label3" Text="项目累计费用（元）">
                                    </f:Label>
                                </Items>
                            </f:Panel>
                            <f:Panel ID="Panel4" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                Width="300px" ShowHeader="false" MarginLeft="180px">
                                <Items>
                                    <f:Label runat="server" ID="Label2" Text="备注">
                                    </f:Label>
                                </Items>
                            </f:Panel>
                        </Items>
                    </f:Panel>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 22% 25% 25% 23%">
                <Items>
                    <f:Label runat="server" ID="Label5" Text="A1">
                    </f:Label>
                    <f:Label runat="server" ID="Label6" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;基础管理">
                    </f:Label>
                    <f:NumberBox ID="nbA1" NoDecimal="false" NoNegative="false" MinValue="0" runat="server"
                        Readonly="true" Label="">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectA1" NoDecimal="false" NoNegative="false" Readonly="true"
                        MinValue="0" runat="server" Label="">
                    </f:NumberBox>
                    <f:TextBox runat="server" ID="txtDefA1" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 22% 25% 25% 23%">
                <Items>
                    <f:Label runat="server" ID="Label7" Text="A2">
                    </f:Label>
                    <f:Label runat="server" ID="Label8" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;安全技术">
                    </f:Label>
                    <f:NumberBox ID="nbA2" NoDecimal="false" NoNegative="false" MinValue="0" runat="server"
                        Readonly="true" Label="">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectA2" NoDecimal="false" NoNegative="false" Readonly="true"
                        MinValue="0" runat="server" Label="">
                    </f:NumberBox>
                    <f:TextBox runat="server" ID="txtDefA2" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 22% 25% 25% 23%">
                <Items>
                    <f:Label runat="server" ID="Label9" Text="A3">
                    </f:Label>
                    <f:Label runat="server" ID="Label10" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;职业健康">
                    </f:Label>
                    <f:NumberBox ID="nbA3" NoDecimal="false" NoNegative="false" MinValue="0" runat="server"
                        Readonly="true" Label="">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectA3" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="">
                    </f:NumberBox>
                    <f:TextBox runat="server" ID="txtDefA3" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 22% 25% 25% 23%">
                <Items>
                    <f:Label runat="server" ID="Label11" Text="A4">
                    </f:Label>
                    <f:Label runat="server" ID="Label12" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;安全防护">
                    </f:Label>
                    <f:NumberBox ID="nbA4" NoDecimal="false" NoNegative="false" MinValue="0" runat="server"
                        Readonly="true" Label="">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectA4" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="">
                    </f:NumberBox>
                    <f:TextBox runat="server" ID="txtDefA4" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 22% 25% 25% 23%">
                <Items>
                    <f:Label runat="server" ID="Label13" Text="A5">
                    </f:Label>
                    <f:Label runat="server" ID="Label14" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;化工试车">
                    </f:Label>
                    <f:NumberBox ID="nbA5" NoDecimal="false" NoNegative="false" MinValue="0" runat="server"
                        Readonly="true" Label="">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectA5" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="">
                    </f:NumberBox>
                    <f:TextBox runat="server" ID="txtDefA5" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 22% 25% 25% 23%">
                <Items>
                    <f:Label runat="server" ID="Label15" Text="A6">
                    </f:Label>
                    <f:Label runat="server" ID="Label16" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;教育培训">
                    </f:Label>
                    <f:NumberBox ID="nbA6" NoDecimal="false" NoNegative="false" MinValue="0" runat="server"
                        Readonly="true" Label="">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectA6" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="">
                    </f:NumberBox>
                    <f:TextBox runat="server" ID="txtDefA6" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 22% 25% 25% 23%">
                <Items>
                    <f:Label runat="server" ID="Label17" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;A安全生产费用合计">
                    </f:Label>
                    <f:NumberBox ID="nbA" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectA" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="">
                    </f:NumberBox>
                    <f:Label runat="server" ID="Label18" Text="">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 22% 25% 25% 23%">
                <Items>
                    <f:Label runat="server" ID="Label19" Text="B1">
                    </f:Label>
                    <f:Label runat="server" ID="Label20" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;文明施工">
                    </f:Label>
                    <f:NumberBox ID="nbB1" NoDecimal="false" NoNegative="false" MinValue="0" runat="server"
                        Readonly="true" Label="">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectB1" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="">
                    </f:NumberBox>
                    <f:TextBox runat="server" ID="txtDefB1" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 22% 25% 25% 23%">
                <Items>
                    <f:Label runat="server" ID="Label21" Text="B2">
                    </f:Label>
                    <f:Label runat="server" ID="Label22" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;临时设施">
                    </f:Label>
                    <f:NumberBox ID="nbB2" NoDecimal="false" NoNegative="false" MinValue="0" runat="server"
                        Readonly="true" Label="">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectB2" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="">
                    </f:NumberBox>
                    <f:TextBox runat="server" ID="txtDefB2" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 22% 25% 25% 23%">
                <Items>
                    <f:Label runat="server" ID="Label23" Text="B3">
                    </f:Label>
                    <f:Label runat="server" ID="Label24" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;环境保护">
                    </f:Label>
                    <f:NumberBox ID="nbB3" NoDecimal="false" NoNegative="false" MinValue="0" runat="server"
                        Readonly="true" Label="">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectB3" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="">
                    </f:NumberBox>
                    <f:TextBox runat="server" ID="txtDefB3" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 22% 25% 25% 23%">
                <Items>
                    <f:Label runat="server" ID="Label25" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;B文明施工措施费合计">
                    </f:Label>
                    <f:NumberBox ID="nbB" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectB" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="">
                    </f:NumberBox>
                    <f:Label runat="server" ID="Label26" Text="">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 22% 25% 25% 23%">
                <Items>
                    <f:Label runat="server" ID="Label27" Text="A+B安全生产及文明施工措施费总计">
                    </f:Label>
                    <f:NumberBox ID="nbAB" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectAB" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="">
                    </f:NumberBox>
                    <f:Label runat="server" ID="Label28" Text="">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true"
                        BodyPadding="0px">
                        <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server" Margin="0 50 30 50">
                <Items>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>

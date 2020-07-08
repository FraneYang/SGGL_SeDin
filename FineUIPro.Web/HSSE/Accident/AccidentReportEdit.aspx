<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccidentReportEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Accident.AccidentReportEdit" ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑事故调查报告</title>
    <link href="../../Styles/Style.css" rel="stylesheetasp" type="text/css" />
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
            font-size: medium;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Panel ID="Panel2" runat="server" Height="80px"  ShowBorder="false"
        Title="事故调查报告表头" Layout="Table" TableConfigColumns="3" ShowHeader="false" BodyPadding="1px">
        <Items>
            <f:Panel ID="Panel1" Title="Panel1" Width="350px" Height="80px" MarginRight="0" TableRowspan="3"
                runat="server" BodyPadding="1px" ShowBorder="false" ShowHeader="false">
                <Items>
                    <f:Image ID="Image1" runat="server" ImageUrl="../../Images/Null.jpg" LabelAlign="right">
                    </f:Image>
                </Items>
            </f:Panel>
            <f:Panel ID="Panel3" Title="Panel1" Width="350px" Height="30px" runat="server" BodyPadding="1px"
                ShowBorder="false" ShowHeader="false">
                <Items>
                    <f:Label ID="lblProjectName" runat="server" CssClass="titler" Margin="5 0 0 20">
                    </f:Label>
                </Items>
            </f:Panel>
            <f:Panel ID="Panel5" Title="Panel1" Width="350px" Height="30px" runat="server" BodyPadding="1px"
                ShowBorder="false" ShowHeader="false">
                <Items>
                    <f:TextBox ID="txtAccidentReportName" runat="server" LabelAlign="Right" Width="250px">
                    </f:TextBox>
                </Items>
            </f:Panel>
            <f:Panel ID="Panel4" Title="Panel1" Width="300px" Height="50px" TableRowspan="2"
                runat="server" BodyPadding="1px" ShowBorder="false" ShowHeader="false">
                <Items>
                    <f:Label ID="Label3" runat="server" Text="事故报告登记" CssClass="titler" Margin="5 0 0 40">
                    </f:Label>
                </Items>
            </f:Panel>
            <f:Panel ID="Panel7" Title="Panel1" Width="250px" Height="30px" runat="server" BodyPadding="1px"
                ShowBorder="false" ShowHeader="false">
                <Items>
                    <f:TextBox ID="txtAccidentReportCode" runat="server" Label="事故编号" LabelAlign="Right"
                        LabelWidth="75px" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    <f:TabStrip ID="TabStrip1" Height="220px" ShowBorder="true" TabPosition="Top"
        EnableTabCloseMenu="false" ActiveTabIndex="0" runat="server">
        <Tabs>
            <f:Tab ID="Tab1" Title="标签一" BodyPadding="5px" Layout="Fit" runat="server">
                <Items>
                    <f:Form ID="Form2" runat="server" ShowHeader="false" AutoScroll="true">
                        <Items>
                            <f:FormRow>
                                <Items>
                                    <f:DropDownList ID="drpAccidentTypeId" runat="server" Label="事故类型" LabelAlign="Right"
                                        AutoPostBack="true" OnSelectedIndexChanged="drpAccidentTypeId_SelectedIndexChanged">
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow runat="server" ID="frIsNotConfirm" Hidden="true">
                                <Items>
                                     <f:RadioButtonList runat="server" ID="rbIsNotConfirm" Label="是否待定" Width="60px"
                                        LabelAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="rbIsNotConfirm_SelectedIndexChanged">
                                        <f:RadioItem Value="True" Text="是" />
                                        <f:RadioItem Value="False" Selected="true" Text="否" />
                                    </f:RadioButtonList>
                                    <f:Label runat="server" ID="lb1"></f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow runat="server" ID="frAbstract">
                                <Items>
                                    <f:TextBox ID="txtAbstract" runat="server" Label="提要" LabelAlign="Right" ShowRedStar="true"
                                        Required="true" MaxLength="20">
                                    </f:TextBox>
                                 </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:DatePicker ID="txtAccidentDate" runat="server" Label="发生时间" LabelAlign="Right"
                                        EnableEdit="true" ShowRedStar="true" Required="true">
                                    </f:DatePicker>
                                    <f:TextBox ID="txtWorkArea" runat="server" Label="发生区域" LabelAlign="Right" MaxLength="200">
                                    </f:TextBox>
                                    <f:NumberBox ID="txtPeopleNum" runat="server" Label="人数" ShowRedStar="true" Required="true"
                                        LabelAlign="Right" NoDecimal="true" NoNegative="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow runat="server" ID="trConfirm" ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:DropDownList ID="drpUnitId" runat="server"  Label="事故责任单位" LabelAlign="Right" LabelWidth="120px">
                                    </f:DropDownList>
                                    <f:NumberBox ID="txtWorkingHoursLoss" runat="server" Label="工时损失" LabelAlign="Right"
                                        NoDecimal="false" MinValue="0" NoNegative="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtEconomicLoss" runat="server" Label="直接经济损失" LabelAlign="Right" LabelWidth="120px"
                                        NoDecimal="false" MinValue="0" NoNegative="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtEconomicOtherLoss" runat="server" Label="间接经济损失" LabelAlign="Right" LabelWidth="120px"
                                        NoDecimal="false" MinValue="0" NoNegative="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow runat="server" ID="trNotConfirm" Hidden="true">
                                <Items>
                                    <f:DropDownList ID="drpUnitId2" runat="server" Label="事故责任单位" LabelAlign="Right" > 
                                    </f:DropDownList>
                                    <f:TextBox ID="txtNotConfirmWorkingHoursLoss" runat="server" Label="工时损失" LabelAlign="Right" 
                                        MaxLength="100">
                                    </f:TextBox>
                                    <f:TextBox ID="txtNotConfirmEconomicLoss" runat="server" Label="直接经济损失" LabelAlign="Right"
                                        MaxLength="100">
                                    </f:TextBox>
                                    <f:TextBox ID="txtNotConfirmEconomicOtherLoss" runat="server" Label="间接经济损失" LabelAlign="Right" 
                                        MaxLength="100">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtReportMan" runat="server" Label="报告人" LabelAlign="Right" MaxLength="50">
                                    </f:TextBox>
                                    <f:TextBox ID="txtReporterUnit" runat="server" Label="报告人单位" LabelAlign="Right" MaxLength="50">
                                    </f:TextBox>
                                    <f:DatePicker ID="txtReportDate" runat="server" Label="报告时间" LabelAlign="Right" EnableEdit="true">
                                    </f:DatePicker>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtProcessDescription" runat="server" Label="事故过程描述" LabelAlign="Right"
                                        MaxLength="500" Height="80px">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtEmergencyMeasures" runat="server" Label="紧急措施" LabelAlign="Right"
                                        MaxLength="500" Height="64px">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:DropDownList ID="drpCompileMan" runat="server" Label="报告编制" LabelAlign="Right"
                                        EnableEdit="true">
                                    </f:DropDownList>
                                    <f:DatePicker ID="txtCompileDate" runat="server" Label="日期" LabelAlign="Right" EnableEdit="true">
                                    </f:DatePicker>
                                </Items>
                            </f:FormRow>
                        </Items>
                    </f:Form>
                </Items>
            </f:Tab>
            <f:Tab ID="Tab2" Title="标签二" BodyPadding="5px" runat="server">
                <Items>
                    <f:HtmlEditor runat="server" Label="事故调查报告" ID="txtFileContents" ShowLabel="false"
                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="260" LabelAlign="Right">
                    </f:HtmlEditor>
                </Items>
            </f:Tab>
        </Tabs>
    </f:TabStrip>
    <f:Panel ID="Panel6" Title="Panel1"  Height="160px" runat="server" BodyPadding="1px"
        ShowBorder="false" ShowHeader="false">
        <Items>
            <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true"
                BodyPadding="0px">
                <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
            </f:ContentPanel>
        </Items>
    </f:Panel>
    <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
        <Items>
            <f:Label runat="server" ID="lbTemp">
            </f:Label>
            <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
            </f:Button>
            <f:ToolbarFill ID="ToolbarFill1" runat="server">
            </f:ToolbarFill>
            <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                OnClick="btnSave_Click">
            </f:Button>
            <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                OnClick="btnSubmit_Click">
            </f:Button>
            <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
            </f:Button>
        </Items>
    </f:Toolbar>
    </form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
</body>
</html>

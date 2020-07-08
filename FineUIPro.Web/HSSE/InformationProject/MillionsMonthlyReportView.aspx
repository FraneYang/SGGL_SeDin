<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MillionsMonthlyReportView.aspx.cs"
    Inherits="FineUIPro.Web.InformationProject.MillionsMonthlyReportView" ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查看百万工时安全统计月报</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel1"/>
    <f:Panel ID="Panel1" BodyPadding="5px" runat="server" ShowHeader="false" EnableCollapse="True"
        Title="百万工时安全统计月报" AutoScroll="true">
        <Items>
            <f:Form ID="Form2" runat="server" ShowHeader="false" BodyPadding="5px" RedStarPosition="BeforeText">
                <Items>
                    <f:FormRow>
                        <Items>
                            <f:TextBox ID="txtProjectName" runat="server" Label="项目名称" LabelAlign="Right" Readonly="true">
                            </f:TextBox>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>
                            <f:TextBox ID="txtYear" runat="server" Label="年度" LabelAlign="Right" Readonly="true">
                            </f:TextBox>
                            <f:TextBox ID="txtMonth" runat="server" Label="月份" LabelAlign="Right" Readonly="true">
                            </f:TextBox>
                             <f:TextBox ID="txtTotalWorkNum" runat="server" Label="总工时数（万）" Readonly="true" LabelWidth="140px" LabelAlign="Right" EmptyText="0">
                    </f:TextBox>
                        </Items>
                    </f:FormRow>
                    <%-- <f:FormRow>
                        <Items>
                            <f:TextBox ID="txtAffiliation" runat="server" Label="所属单位" Readonly="true" LabelAlign="Right">
                            </f:TextBox>
                            <f:TextBox ID="txtName" runat="server" Label="名称" LabelAlign="Right" Readonly="true">
                            </f:TextBox>
                            <f:NumberBox ID="txtTotalWorkNum" runat="server" Label="总工时数（万）" LabelAlign="Right"
                                LabelWidth="140px" Readonly="true">
                            </f:NumberBox>
                        </Items>
                    </f:FormRow>--%>
                </Items>
            </f:Form>
            <f:GroupPanel runat="server" Title="员工总数" BodyPadding="5px" ID="GroupPanel1" EnableCollapse="True"
                Collapsed="false">
                <Items>
                    <f:Form ID="Form4" ShowBorder="false" ShowHeader="false" runat="server">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:NumberBox ID="txtPostPersonNum" runat="server" Label="在岗员工" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSnapPersonNum" runat="server" Label="临时员工" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtContractorNum" runat="server" Label="承包商" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:GroupPanel>
            <f:GroupPanel runat="server" Title="重伤事故" BodyPadding="5px" ID="GroupPanel2" EnableCollapse="True"
                Collapsed="false">
                <Items>
                    <f:Form ID="Form5" ShowBorder="false" ShowHeader="false" runat="server">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:NumberBox ID="txtSeriousInjuriesNum" runat="server" Label="起数" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSeriousInjuriesPersonNum" runat="server" Label="人数" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSeriousInjuriesLossHour" runat="server" Label="损失工时" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:GroupPanel>
            <f:GroupPanel runat="server" Title="轻伤事故" BodyPadding="5px" ID="GroupPanel3" EnableCollapse="True"
                Collapsed="false">
                <Items>
                    <f:Form ID="Form6" ShowBorder="false" ShowHeader="false" runat="server">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:NumberBox ID="txtMinorAccidentNum" runat="server" Label="起数" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtMinorAccidentPersonNum" runat="server" Label="人数" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtMinorAccidentLossHour" runat="server" Label="损失工时" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:GroupPanel>
            <f:GroupPanel runat="server" Title="其它事故" BodyPadding="5px" ID="GroupPanel4" EnableCollapse="True"
                Collapsed="false">
                <Items>
                    <f:Form ID="Form7" ShowBorder="false" ShowHeader="false" runat="server">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:NumberBox ID="txtOtherAccidentNum" runat="server" Label="起数" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtOtherAccidentPersonNum" runat="server" Label="人数" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtOtherAccidentLossHour" runat="server" Label="损失工时" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:GroupPanel>
            <f:GroupPanel runat="server" Title="工作受限" BodyPadding="5px" ID="GroupPanel5" EnableCollapse="True"
                Collapsed="false">
                <Items>
                    <f:Form ID="Form8" ShowBorder="false" ShowHeader="false" runat="server">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:NumberBox ID="txtRestrictedWorkPersonNum" runat="server" Label="人数" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtRestrictedWorkLossHour" runat="server" Label="损失工时" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:Label ID="Label1" runat="server">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:GroupPanel>
            <f:GroupPanel runat="server" Title="医疗处置" BodyPadding="5px" ID="GroupPanel6" EnableCollapse="True"
                Collapsed="false">
                <Items>
                    <f:Form ID="Form9" ShowBorder="false" ShowHeader="false" runat="server">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:NumberBox ID="txtMedicalTreatmentPersonNum" runat="server" Label="人数" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtMedicalTreatmentLossHour" runat="server" Label="损失工时" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:Label ID="Label2" runat="server">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:GroupPanel>
            <f:GroupPanel runat="server" Title="无伤害事故" BodyPadding="5px" ID="GroupPanel7" EnableCollapse="True"
                Collapsed="false">
                <Items>
                    <f:Form ID="Form10" ShowBorder="false" ShowHeader="false" runat="server">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:NumberBox ID="txtFireNum" runat="server" Label="火灾" LabelAlign="Right" Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtExplosionNum" runat="server" Label="爆炸" LabelAlign="Right" Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtTrafficNum" runat="server" Label="交通" LabelAlign="Right" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:NumberBox ID="txtEquipmentNum" runat="server" Label="机械设备" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtQualityNum" runat="server" Label="质量" LabelAlign="Right" Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtOtherNum" runat="server" Label="其它" LabelAlign="Right" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:GroupPanel>
            <f:Form ID="Form11" runat="server" ShowHeader="false" BodyPadding="5px">
                <Items>
                    <f:FormRow>
                        <Items>
                            <f:NumberBox ID="txtFirstAidDressingsNum" runat="server" Label="急救包扎" LabelAlign="Right"
                                Readonly="true">
                            </f:NumberBox>
                            <f:NumberBox ID="txtAttemptedEventNum" runat="server" Label="未遂事件" LabelAlign="Right"
                                Readonly="true">
                            </f:NumberBox>
                            <f:NumberBox ID="txtLossDayNum" runat="server" Label="损失工日" LabelAlign="Right" Readonly="true">
                            </f:NumberBox>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>
                            <f:TextBox ID="txtCompileManName" runat="server" Label="填报人" LabelAlign="Right" Readonly="true">
                            </f:TextBox>
                            <f:DatePicker ID="txtCompileDate" runat="server" Label="填报日期" LabelAlign="Right"
                                Readonly="true">
                            </f:DatePicker>
                            <f:Label ID="Label4" runat="server">
                            </f:Label>
                        </Items>
                    </f:FormRow>
                </Items>
            </f:Form>
            <f:Form ID="Form3" runat="server" ShowHeader="false" BodyPadding="5px">
                <Items>
                    <f:FormRow>
                        <Items>
                            <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true"
                                BodyPadding="0px">
                                <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
                            </f:ContentPanel>
                        </Items>
                    </f:FormRow>
                </Items>
            </f:Form>
        </Items>
        <Toolbars>
            <f:Toolbar ID="Toolbar2" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:ToolbarFill ID="ToolbarFill2" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Panel>
    </form>
</body>
</html>

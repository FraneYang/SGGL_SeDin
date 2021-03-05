<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectSysSet.aspx.cs"
    Inherits="FineUIPro.Web.common.ProjectSet.ProjectSysSet" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server" />
        <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
            <Regions>
                <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox"
                    BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server">
                    <Items>
                        <f:GroupPanel ID="GroupPanel2" Layout="Anchor" Title="焊接环境设置" runat="server">
                            <Items>
                                <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="true" Layout="VBox"
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Rows>
                                        <f:FormRow>
                                            <Items>
                                                <f:CheckBox ID="ckbPdms" runat="server" Label="引用PDMS模板" Text="是否引用PDMS导出模板" LabelWidth="200">
                                                </f:CheckBox>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:CheckBox ID="ckbJointB" runat="server" Label="焊口编号" Text="对接焊缝是否带B前缀" LabelWidth="200">
                                                </f:CheckBox>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:CheckBox ID="ckbDayReport" runat="server" Label="焊接日报编号" Text="是否自动生成" LabelWidth="200">
                                                </f:CheckBox>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:CheckBox ID="ckbPoint" runat="server" Label="点口编号" Text="是否自动生成" LabelWidth="200">
                                                </f:CheckBox>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:RadioButtonList ID="robStandard" runat="server" Label="无损检测委托单" LabelWidth="200">
                                                    <f:RadioItem Value="1" Text="3543-G401(1)" />
                                                    <f:RadioItem Value="2" Text="3543-G401(2)" />
                                                    <f:RadioItem Value="3" Text="第三方委托单" />
                                                    <f:RadioItem Value="4" Text="第三方委托单(神化)" />
                                                </f:RadioButtonList>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:Label ID="Label4" runat="server" Label="说明" LabelWidth="200" Text="(1)需监理和总包签字；(2)需总包、监理、施工、检测单位签字">
                                                </f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ColumnWidths="22% 13% 13% 13% 13% 13% 13%">
                                            <Items>
                                                <f:Label ID="Label1" runat="server" Label="组批条件设置" LabelWidth="200"></f:Label>
                                                <f:CheckBox ID="cb1" runat="server" Text="单位工程" Checked="true" Enabled="false"></f:CheckBox>
                                                <f:CheckBox ID="cb2" runat="server" Text="施工单位" Checked="true" Enabled="false"></f:CheckBox>
                                                <f:CheckBox ID="cb3" runat="server" Text="探伤类型" Checked="true" Enabled="false"></f:CheckBox>
                                                <f:CheckBox ID="cb4" runat="server" Text="探伤比例" Checked="true" Enabled="false"></f:CheckBox>
                                                <f:CheckBox ID="cb5" runat="server" Text="管线等级"></f:CheckBox>
                                                <f:CheckBox ID="cb6" runat="server" Text="管线"></f:CheckBox>
                                                <f:CheckBox ID="cb7" runat="server" Text="焊工"></f:CheckBox>
                                            </Items>
                                        </f:FormRow>
                                    </Rows>
                                </f:Form>
                            </Items>
                        </f:GroupPanel>
                        <f:GroupPanel ID="GroupPanel1" Layout="Anchor" Title="质量环境设置" runat="server">
                            <Items>
                                <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                    Layout="VBox" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
                                    LabelAlign="Right">
                                    <Rows>
                                        <f:FormRow runat="server">
                                            <Items>
                                                <f:NumberBox ID="txtRemindDay" Label="检试验设备到期提醒天数" runat="server"
                                                    LabelWidth="180px" DecimalPrecision="1" NoNegative="true">
                                                </f:NumberBox>
                                                <f:Label runat="server"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow runat="server">
                                            <Items>
                                                <f:NumberBox ID="txtStarTime" runat="server" NoDecimal="true" NoNegative="false" Label="月报开始日期" MinValue="1" MaxValue="31"
                                                    LabelWidth="180" Width="210">
                                                </f:NumberBox>
                                                <f:NumberBox ID="txtEndTime" NoDecimal="true" NoNegative="false" Label="月报结束日期" LabelWidth="180" Width="210" runat="server" MinValue="1" MaxValue="31">
                                                </f:NumberBox>
                                            </Items>
                                        </f:FormRow>
                                    </Rows>
                                </f:Form>
                            </Items>
                        </f:GroupPanel>
                    </Items>
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                            <Items>
                                <f:Button ID="btnSave" Icon="SystemSave" runat="server" Text="提交数据" ValidateForms="SimpleForm1"
                                    Hidden="true" OnClick="btnSave_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                </f:Region>
            </Regions>
        </f:RegionPanel>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectSysSet.aspx.cs" Inherits="FineUIPro.Web.CQMS.BaseInfo.ProjectSysSet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server" />
        <f:GroupPanel ID="GroupPanel1" Layout="Anchor" Title="项目环境设置" runat="server">
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
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                            <Items>
                                <f:HiddenField ID="hdCheckerId" runat="server">
                                </f:HiddenField>
                                <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1" OnClick="btnSave_Click">
                                </f:Button>
                                <%--<f:Button ID="btnClose" EnablePostBack="false"
                                    runat="server" Icon="SystemClose">
                                </f:Button>--%>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                </f:Form>
            </Items>
        </f:GroupPanel>
    </form>
</body>
</html>

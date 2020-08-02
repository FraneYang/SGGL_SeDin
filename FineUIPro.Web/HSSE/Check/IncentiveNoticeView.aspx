<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IncentiveNoticeView.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Check.IncentiveNoticeView" ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查看奖励通知单</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="奖励通知单" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtIncentiveNoticeCode" runat="server" Label="编号" LabelAlign="Right"
                        Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtUnitName" runat="server" Label="受奖单位" LabelAlign="Right"
                        Readonly="true" LabelWidth="140px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtTeamGroup" runat="server" Label="受奖班组" LabelAlign="Right" Readonly="true"
                        LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtPerson" runat="server" Label="受奖个人" LabelAlign="Right" Readonly="true"
                        LabelWidth="140px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtIncentiveDate" runat="server" Label="奖励时间" LabelAlign="Right" Readonly="true"
                        LabelWidth="120px">
                    </f:TextBox>
                     <f:TextBox ID="txtRewardType" runat="server" Label="奖励类型" LabelAlign="Right"
                        Readonly="true" LabelWidth="140px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtBasicItem" runat="server" Label="奖励根据 " LabelAlign="Right" LabelWidth="120px"
                        Readonly="true">
                    </f:TextBox>
                   
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Panel ID="Panel19" runat="server" Height="100px" Width="750px" ShowBorder="false"
                        Layout="Table" TableConfigColumns="2" ShowHeader="false" BodyPadding="1px">
                        <Items>
                            <f:Panel ID="Panel14" Title="Panel1" Width="150px" Height="100px" MarginRight="0"
                                MarginTop="40" TableRowspan="3" runat="server" BodyPadding="1px" ShowBorder="false"
                                ShowHeader="false" MarginLeft="45px">
                                <Items>
                                    <f:Label runat="server" ID="Label30" Text="奖励方式：">
                                    </f:Label>
                                </Items>
                            </f:Panel>
                            <f:Panel ID="Panel16" Title="Panel1" Width="850px" Height="30px" runat="server" BodyPadding="1px"
                                ShowBorder="false" ShowHeader="false">
                                <Items>
                                    <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="true" runat="server"
                                        RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow ColumnWidths="10% 25% 25% 50%">
                                                <Items>
                                                    <f:CheckBox runat="server" ID="rbtnIncentiveWay1" Enabled="false">
                                                    </f:CheckBox>                                                   
                                                    <f:NumberBox runat="server" ID="txtPayMoney" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:TextBox runat="server" ID="txtCurrency" Label="币种" Readonly="true" LabelWidth="60px">
                                                    </f:TextBox>
                                                    <f:TextBox runat="server" ID="txtBig" Label="大写" Readonly="true" LabelWidth="60px">
                                                    </f:TextBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:Panel>
                            <f:Panel ID="Panel1" Title="Panel1" Width="850px" Height="30px" runat="server" BodyPadding="1px"
                                ShowBorder="false" ShowHeader="false">
                                <Items>
                                    <f:Form ID="Form3" ShowBorder="false" ShowHeader="false" AutoScroll="true" runat="server"
                                        RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow ColumnWidths="5% 5% 80% 10%">
                                                <Items>
                                                    <f:CheckBox runat="server" ID="rbtnIncentiveWay2" Enabled="false">
                                                    </f:CheckBox>
                                                    <f:Label runat="server" ID="Label33" Text="授予">
                                                    </f:Label>
                                                    <f:TextBox runat="server" ID="txtTitleReward" Readonly="true">
                                                    </f:TextBox>
                                                    <f:Label runat="server" ID="Label34" Text="称号">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:Panel>
                            <f:Panel ID="Panel2" Title="Panel1" Width="850px" Height="30px" runat="server" BodyPadding="1px"
                                ShowBorder="false" ShowHeader="false">
                                <Items>
                                    <f:Form ID="Form4" ShowBorder="false" ShowHeader="false" AutoScroll="true" runat="server"
                                        RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow ColumnWidths="5% 5% 80% 10%">
                                                <Items>
                                                    <f:CheckBox runat="server" ID="rbtnIncentiveWay3" Enabled="false">
                                                    </f:CheckBox>
                                                    <f:Label runat="server" ID="Label35" Text="给予">
                                                    </f:Label>
                                                    <f:TextBox runat="server" ID="txtMattleReward" Readonly="true">
                                                    </f:TextBox>
                                                    <f:Label runat="server" ID="Label36" Text="物质奖励">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:Panel>
                        </Items>
                    </f:Panel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:ContentPanel ID="ContentPanel2" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="附件">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divFile" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label runat="server" ID="Label37" Text="有关证据（被奖罚人姓名、岗位、证件编号、有关文字描述或照片等）：">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:HtmlEditor runat="server" Label="有关证据" ID="txtFileContents" ShowLabel="false"
                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="260" LabelAlign="Right">
                    </f:HtmlEditor>
                </Items>
            </f:FormRow>
            <f:FormRow>
                    <Items>
                        <f:GroupPanel runat="server" Title="审批步骤" BodyPadding="1px" ID="GroupPanel3"
                            EnableCollapse="True" Collapsed="false" EnableCollapseEvent="true"
                            EnableExpandEvent="true">
                            <Items>
                                <f:Grid ID="gvFlowOperate" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server" DataIDField="FlowOperateId" AllowSorting="true" SortField="OperateTime" SortDirection="ASC" EnableTextSelection="True">
                                    <Columns>
                                        <f:TemplateField ColumnID="tfNumber" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                                            TextAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# gvFlowOperate.PageIndex * gvFlowOperate.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </f:TemplateField>
                                        <f:RenderField Width="200px" ColumnID="OperateName" DataField="OperateName" FieldType="String" HeaderText="步骤" HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:RenderField Width="200px" ColumnID="UserName" DataField="UserName" FieldType="String" HeaderText="操作人" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="150px" ColumnID="Opinion" DataField="Opinion" FieldType="string" HeaderText="意见" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                        </f:RenderField>
                                        <f:RenderField Width="80px" ColumnID="IsAgree" DataField="IsAgree" FieldType="String"
                                            HeaderText="是否同意" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="150px" ColumnID="OperateTime" DataField="OperateTime" FieldType="string" HeaderText="时间" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>

                                    </Columns>
                                </f:Grid>

                            </Items>
                        </f:GroupPanel>
                    </Items>
                </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>

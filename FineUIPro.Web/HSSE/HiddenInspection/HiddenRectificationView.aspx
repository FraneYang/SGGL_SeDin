<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HiddenRectificationView.aspx.cs" Inherits="FineUIPro.Web.HSSE.HiddenInspection.HiddenRectificationView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看日常巡检</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />   
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtWorkAreaName" runat="server" Label="区域" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtResponsibilityUnitName" runat="server" Label="责任单位" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtRegisterTypesName" runat="server" Label="检查项" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtProblemDescription" runat="server" Label="问题描述" Readonly="true" Height="64px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtTakeSteps" runat="server" Label="采取措施" Readonly="true" Height="50px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtResponsibilityManName" runat="server" Label="责任人" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtRectificationPeriod" runat="server" Label="整改期限" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCheckManName" runat="server" Label="检查人" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtCheckTime" runat="server" Label="检查时间" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtRectificationTime" runat="server" Label="整改时间" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtStates" runat="server" Label="状态" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="8% 92%">
                <Items>
                    <f:Label runat="server" ID="lblImageUrl" Label="整改前图片">
                    </f:Label>
                    <f:ContentPanel ID="ContentPanel2" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="整改前图片">
                        <table style="width">
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divImageUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="8% 92%">
                <Items>
                    <f:Label runat="server" ID="lblRectificationImageUrl" Label="整改后图片">
                    </f:Label>
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="整改后图片">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divRectificationImageUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
            <%--<f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" ForceFit="true"
                        DataIDField="PunishRecordId" DataKeyNames="PunishRecordId" 
                        ShowGridHeader="true" Height="400px" EnableColumnLines="true" >
                        <Columns>
                            <f:TemplateField ColumnID="tfNumber" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="labNumber" runat="server" Text=' <%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1%>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField HeaderText="卡号" ColumnID="CardNo" DataField="CardNo" SortField="CardNo"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="100px">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="PersonName" DataField="PersonName" SortField="PersonName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="人员姓名">
                            </f:RenderField>
                            <f:RenderField HeaderText="岗位名称" ColumnID="PostName" DataField="PostName" SortField="PostName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="120px">
                            </f:RenderField>
                            <f:RenderField HeaderText="所在班组" ColumnID="TeamGroupName" DataField="TeamGroupName"
                                SortField="TeamGroupName" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="140px">
                            </f:RenderField>
                            <f:RenderField HeaderText="处罚内容" ColumnID="PunishItemContent" DataField="PunishItemContent"
                                SortField="PunishItemContent" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="200px">
                            </f:RenderField>
                            <f:RenderField HeaderText="处罚原因" ColumnID="PunishReason" DataField="PunishReason"
                                SortField="PunishReason" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="150px">
                            </f:RenderField>
                            <f:RenderField HeaderText="扣分" ColumnID="Deduction" DataField="Deduction" SortField="Deduction"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="70px">
                            </f:RenderField>
                            <f:RenderField HeaderText="罚款" ColumnID="PunishMoney" DataField="PunishMoney" SortField="PunishMoney"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="70px">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="PunishDate" DataField="PunishDate" SortField="PunishDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="处罚日期"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>--%>
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

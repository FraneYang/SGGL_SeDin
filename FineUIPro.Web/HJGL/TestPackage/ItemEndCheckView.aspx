<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemEndCheckView.aspx.cs" Inherits="FineUIPro.Web.HJGL.TestPackage.ItemEndCheckView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="试压包明细" EnableCollapse="true" Collapsed="false" runat="server" BoxFlex="1" DataKeyNames="PipelineId,ItemCheckId" AllowCellEditing="true" EnableColumnLines="true" ClicksToEdit="1" DataIDField="ItemCheckId" AllowSorting="true" SortField="PipelineId" SortDirection="ASC" PageSize="100" Height="360px">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:Label ID="txtTestPackageNo" runat="server" Label="试压包编号" LabelAlign="Right" LabelWidth="120px" Readonly="true"></f:Label>
                                <f:Label ID="txtTestPackageName" runat="server" Label="试压包名称" LabelAlign="Right" LabelWidth="120px" Readonly="true"></f:Label>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:TemplateField ColumnID="PipelineId" Width="110px" HeaderText="管线编号" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# ConvertCarryPipeline(Eval("PipelineId")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField Width="120px" ColumnID="Content" DataField="Content"
                            HeaderTextAlign="Center" HeaderText="尾项内容" TextAlign="Left" ExpandUnusedSpace="true">
                        </f:RenderField>
                        <f:RenderField Width="120px" ColumnID="ItemType" DataField="ItemType" HeaderTextAlign="Center" HeaderText="检查类别" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="120px" ColumnID="Result" DataField="Result" HeaderTextAlign="Center" HeaderText="消项结果" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="120px" ColumnID="Remark" DataField="Remark" HeaderTextAlign="Center" HeaderText="备注" TextAlign="Left">
                        </f:RenderField>
                    </Columns>

                </f:Grid>
            </Items>
            <Items>
                <f:Grid ID="gvFlowOperate" ShowBorder="true" ShowHeader="true" Title="审批步骤" EnableCollapse="true" runat="server" DataIDField="ApproveId" AllowSorting="true" SortField="ApproveDate" SortDirection="ASC" EnableTextSelection="True" EnableColumnLines="true">
                    <Columns>
                        <f:TemplateField ColumnID="tfNumber" Width="55px" HeaderText="序号" HeaderTextAlign="Center"
                            TextAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# gvFlowOperate.PageIndex * gvFlowOperate.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:TemplateField ColumnID="ApproveType" Width="200px" HeaderText="办理类型" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# ConvertApproveType(Eval("ApproveType")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField Width="200px" ColumnID="UserName" DataField="UserName"
                            FieldType="String" HeaderText="办理人员" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="150px" ColumnID="Opinion" DataField="Opinion"
                            FieldType="string" HeaderText="办理意见" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                        </f:RenderField>
                        <f:RenderField Width="160px" ColumnID="ApproveDate" DataField="ApproveDate"
                            FieldType="Date" RendererArgument="yyyy-MM-dd" HeaderText="时间" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>

                    </Columns>
                </f:Grid>

            </Items>

        </f:Panel>
    </form>
</body>
</html>

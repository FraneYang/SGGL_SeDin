<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectManageRule.aspx.cs" Inherits="FineUIPro.Web.HSSE.ActionPlan.ProjectManageRule" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>项目安全管理制度</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch" Height="550px ">
            <Items>
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="公司安全管理制度发布" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="ManagerRuleId" DataIDField="ManagerRuleId" AllowSorting="true" SortField="IssueDate,CompileDate" SortDirection="DESC" EnableColumnLines="true"
                    OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" PageSize="10" ForceFit="true"
                    OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                    OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                            <Items>
                                <f:CheckBoxList runat="server" ID="cbIssue" ShowLabel="false" Width="150px" LabelAlign="Right">
                                    <f:CheckItem Text="未发布" Value="0" Selected="true" />
                                    <f:CheckItem Text="已发布" Value="1" />
                                </f:CheckBoxList>
                                <f:TextBox runat="server" Label="编号" ID="txtManageRuleCode" EmptyText="输入查询条件"
                                    Width="210px" LabelWidth="60px"
                                    LabelAlign="right">
                                </f:TextBox>
                                <f:TextBox runat="server" Label="名称" ID="txtManageRuleName" EmptyText="输入查询条件"
                                    Width="210px" LabelWidth="60px"
                                    LabelAlign="right">
                                </f:TextBox>
                                <f:TextBox runat="server" Label="分类" ID="txtManageRuleTypeName" EmptyText="输入查询条件"
                                    Width="210px" LabelWidth="60px"
                                    LabelAlign="right">
                                </f:TextBox>
                                <f:ToolbarFill runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="btnQuery" ToolTip="查询" Icon="SystemSearch" EnablePostBack="true" OnClick="TextBox_TextChanged" runat="server">
                                </f:Button>
                                <f:Button ID="btnCompile" ToolTip="编制" Icon="Add" Hidden="true" runat="server" OnClick="btnCompile_Click">
                                </f:Button>
                                <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                    EnableAjax="false" DisableControlBeforePostBack="false">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:RenderField Width="80px" ColumnID="ManageRuleCode" DataField="ManageRuleCode"
                            SortField="ManageRuleCode" FieldType="String" HeaderText="文件编号" HeaderTextAlign="Center"
                            TextAlign="Left">
                        </f:RenderField>
                        <f:TemplateField ColumnID="tfManageRuleName" Width="250px" HeaderText="文件名称" HeaderTextAlign="Center" TextAlign="Left"
                            SortField="ManageRuleName">
                            <ItemTemplate>
                                <asp:Label ID="lblManageRuleName" runat="server" Text='<%# Bind("ManageRuleName") %>'
                                    ToolTip='<%#Bind("ManageRuleName") %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                         <f:RenderField Width="160px" ColumnID="ManageRuleTypeName" DataField="ManageRuleTypeName"
                            SortField="ManageRuleTypeName" FieldType="String" HeaderText="分类" HeaderTextAlign="Center"
                            TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="IssueDate" DataField="IssueDate" SortField="IssueDate"
                            FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="发布时间"
                            HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:TemplateField ColumnID="tfState" Width="130px" HeaderText="状态" HeaderTextAlign="Center" TextAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblState" runat="server" Text='<%# Bind("FlowOperateName") %>' ToolTip='<%#  Bind("FlowOperateName") %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                    </Columns>
                    <Listeners>
                        <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                    </Listeners>
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                        </f:ToolbarText>
                        <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                        </f:DropDownList>
                    </PageItems>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="管理规定选择" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="1100px"
            OnClose="Window1_Close" Height="500px">
        </f:Window>
        <f:Window ID="Window2" Title="管理规定编辑" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" OnClose="Window1_Close"
            Width="1024px" Height="550px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
                runat="server" Text="编辑" Icon="Pencil" Hidden="true">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuIssuance" Icon="BookNext" OnClick="btnMenuIssuance_Click"
                EnablePostBack="true" Hidden="true" runat="server" Text="发布">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDelete" Icon="Delete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
                Hidden="true" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除">
            </f:MenuButton>
        </f:Menu>
    </form>
    <script type="text/javascript">
        var menuID = '<%= Menu1.ClientID %>';
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>

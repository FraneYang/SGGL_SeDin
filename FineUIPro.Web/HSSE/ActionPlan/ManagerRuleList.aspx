<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagerRuleList.aspx.cs" Inherits="FineUIPro.Web.HSSE.ActionPlan.ManagerRuleList" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>管理规定清单</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="管理规定清单" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="ManagerRuleId" DataIDField="ManagerRuleId" AllowSorting="true"
                    SortField="CompileDate" SortDirection="ASC" EnableColumnLines="true" OnSort="Grid1_Sort" ForceFit="true"
                    AllowPaging="true" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"
                    IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:RadioButtonList runat="server" ID="rblType" ShowRedStar="true" AutoPostBack="true" Width="210px" OnSelectedIndexChanged="rblType_SelectedIndexChanged">
                                    <f:RadioItem Text="集团" Value="集团" Selected="true" />
                                    <f:RadioItem Text="公司" Value="公司" />
                                    <f:RadioItem Text="项目" Value="项目" />
                                </f:RadioButtonList>
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
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="btnQuery" ToolTip="查询" Icon="SystemSearch" EnablePostBack="true"
                                    OnClick="TextBox_TextChanged" runat="server">
                                </f:Button>
                                <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                    EnableAjax="false" DisableControlBeforePostBack="false">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:TemplateField ColumnID="tfManageRuleCode" Width="80px" HeaderText="编号" HeaderTextAlign="Center" TextAlign="Left"
                            SortField="ManageRuleCode">
                            <ItemTemplate>
                                <asp:Label ID="lblManageRuleCode" runat="server" Text='<%# Bind("ManageRuleCode") %>'
                                    ToolTip='<%#Bind("ManageRuleCode") %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField Width="220px" ColumnID="OldManageRuleCode" DataField="OldManageRuleCode"
                            SortField="OldManageRuleCode" FieldType="String" HeaderText="原编号" HeaderTextAlign="Center"
                            TextAlign="Left">
                        </f:RenderField>
                        <f:TemplateField ColumnID="tfManageRuleName" Width="250px" HeaderText="文件名称" HeaderTextAlign="Center" TextAlign="Left"
                            SortField="ManageRuleName">
                            <ItemTemplate>
                                <asp:Label ID="lblManageRuleName" runat="server" Text='<%# Bind("ManageRuleName") %>'
                                    ToolTip='<%#Bind("ManageRuleName") %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:TemplateField ColumnID="tfManageRuleTypeName" Width="150px" HeaderText="分类" HeaderTextAlign="Center" TextAlign="Left"
                            SortField="ManageRuleTypeName">
                            <ItemTemplate>
                                <asp:Label ID="lblManageRuleTypeName" runat="server" Text='<%# Bind("ManageRuleTypeName") %>'
                                    ToolTip='<%#Bind("ManageRuleTypeName") %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:TemplateField ColumnID="tfVersionNo" Width="60px" HeaderText="版本" HeaderTextAlign="Center" TextAlign="Left"
                            SortField="VersionNo">
                            <ItemTemplate>
                                <asp:Label ID="lblVersionNo" runat="server" Text='<%# Bind("VersionNo") %>'
                                    ToolTip='<%#Bind("VersionNo") %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <%-- <f:TemplateField ColumnID="tfRemark" Width="300px" HeaderText="摘要" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true"
                        SortField="Remark">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("ShortRemark") %>' ToolTip='<%#Bind("Remark") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>--%>
                        <%--<f:WindowField TextAlign="Center" Width="120px" WindowID="WindowAtt" HeaderText="附件"
                        Text="附件上传查看" ToolTip="附件上传查看" DataIFrameUrlFields="ManagerRuleId" DataIFrameUrlFormatString="../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ActionPlanManagerRule&menuId=775EFCF4-DE5C-46E9-8EA3-B16270E2F6A6"
                         />
                    <f:WindowField TextAlign="Center" Width="60px" WindowID="Window6" HeaderText="内容"
                        Text="编辑" ToolTip="编辑内容" DataIFrameUrlFields="ManagerRuleId" DataIFrameUrlFormatString="../ShowDialog/ShowSeeFile.aspx?toKeyId={0}&type=ActionPlanManageRule"
                        Title="编辑内容"  />--%>
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
        <f:Window ID="Window1" Title="编辑内容" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" OnClose="Window1_Close" IsModal="true"
            Width="1024px" Height="650px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true" Hidden="true"
                ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除" Icon="Delete">
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

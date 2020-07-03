<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowWBS.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.ShowWBS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>项目施工WBS</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <style>
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }

          .Yellow  {
            background-color: #FFFF93;
        }

           .f-grid-colheader-text {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
       <f:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server" />
        <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
            <Regions>
                <f:Region ID="Region1" ShowBorder="false" ShowHeader="false" RegionPosition="Left"
                    Title="项目施工WBS定制" BodyPadding="0 5 0 0" Width="300px" Layout="Fit" runat="server"
                    EnableCollapse="true">
                    <Items>
                        <f:Panel runat="server" ID="panel2" RegionPosition="Left" RegionSplit="true" EnableCollapse="true"
                            Width="400" Title="项目施工WBS定制" TitleToolTip="项目施工WBS定制" ShowBorder="true" ShowHeader="true"
                            BodyPadding="5px" IconFont="ArrowCircleLeft">
                            <Items>
                                <f:Tree ID="trWBS" Width="290" Height="580px" EnableCollapse="true" ShowHeader="true"
                                    OnNodeCommand="trWBS_NodeCommand" OnNodeExpand="trWBS_NodeExpand" AutoLeafIdentification="true"
                                    runat="server">
                                    <%--<Toolbars>
                                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                                            <Items>
                                                <f:Button ID="btnLevel1" ToolTip="显示第一级" Text="1级" runat="server" OnClick="btnLevel1_Click">
                                                </f:Button>
                                                <f:Button ID="btnLevel2" ToolTip="显示第二级" Text="2级" runat="server" OnClick="btnLevel2_Click">
                                                </f:Button>
                                                <f:Button ID="btnLevel3" ToolTip="显示第三级" Text="3级" runat="server" OnClick="btnLevel3_Click">
                                                </f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>--%>
                                    <Listeners>
                                        <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                                    </Listeners>
                                </f:Tree>
                                <f:HiddenField runat="server" ID="hdSelectId">
                                </f:HiddenField>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Region>
                <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox"
                    BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server">
                    <Items>
                        <f:Grid ID="Grid1" Width="870px" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                            runat="server" BoxFlex="1" DataKeyNames="ControlItemAndCycleId,InitControlItemCode" AllowSorting="true" EnableColumnLines="true"
                            SortField="ControlItemAndCycleCode" SortDirection="ASC" AllowCellEditing="true" ClicksToEdit="1" ForceFit="true"
                            ShowSelectedCell="true" DataIDField="ControlItemAndCycleId" AllowPaging="true" IsDatabasePaging="true"
                            PageSize="100" OnPageIndexChange="Grid1_PageIndexChange" AllowFilters="true" OnFilterChange="Grid1_FilterChange">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                                    <Items>
                                        <f:CheckBox runat="server" ID="cbAll" Label="全选" LabelWidth="50px" AutoPostBack="true" OnCheckedChanged="cbAll_CheckedChanged"></f:CheckBox>
                                        <f:Label runat="server" Width="100px"></f:Label>
                                        <f:Label runat="server" ID="lb1" Text="注：禁用项为已验收完成，黄色禁选项为控制点等级不属于当前流程的工作包"></f:Label>
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                        <f:Button ID="btnSave" ToolTip="保存" Icon="SystemSave" runat="server" Text=""
                                            OnClick="btnSave_Click">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:TemplateField ColumnID="Check" Width="50px" HeaderText="选择" HeaderTextAlign="Center" TextAlign="Center"
                                    EnableLock="true" Locked="False">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="cbSelect" />
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:RenderField Width="120px" ColumnID="ControlItemContent" DataField="ControlItemContent" FieldType="String"
                                    HeaderText="工作包" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="95px" ColumnID="ControlPoint" DataField="ControlPoint" FieldType="String"
                                    HeaderText="控制点等级" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="65px" ColumnID="Weights" DataField="Weights" FieldType="String"
                                    HeaderText="权重%" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField HeaderText="控制点内容描述" ColumnID="ControlItemDef" DataField="ControlItemDef" SortField="ControlItemDef"
                                    HeaderTextAlign="Center" TextAlign="Center" Width="250px" FieldType="String" >
                                </f:RenderField>
                                <f:RenderField Width="160px" ColumnID="HGForms" DataField="HGForms" FieldType="String"
                                    HeaderText="对应的化工资料表格" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="160px" ColumnID="SHForms" DataField="SHForms" FieldType="String"
                                    HeaderText="对应的石化资料表格" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="PlanCompleteDate" DataField="PlanCompleteDate" FieldType="Date"
                                Renderer="Date" RendererArgument="yyyy-MM-dd" TextAlign="Center" HeaderText="计划完成时间" HeaderTextAlign="Center">
                            </f:RenderField>
                                <f:RenderField HeaderText="质量验收规范" ColumnID="Standard" DataField="Standard" SortField="Standard"
                                    HeaderTextAlign="Center" TextAlign="Center" Width="110px" FieldType="String">
                                </f:RenderField>
                                <f:RenderField HeaderText="条款号" ColumnID="ClauseNo" DataField="ClauseNo" SortField="ClauseNo"
                                    HeaderTextAlign="Center" TextAlign="Center" Width="80px" FieldType="String">
                                </f:RenderField>
                                <f:RenderField HeaderText="检查次数" ColumnID="CheckNum" DataField="CheckNum"
                                    SortField="CheckNum" HeaderTextAlign="Center" TextAlign="Center" Width="80px"
                                    FieldType="String">
                                </f:RenderField>
                                <f:RenderField Width="20px" ColumnID="ControlItemAndCycleId" DataField="ControlItemAndCycleId" FieldType="String"
                                    HeaderText="工作包Id" HeaderTextAlign="Center" TextAlign="Center" Hidden="true">
                                </f:RenderField>
                            </Columns>
                            <PageItems>
                                <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                </f:ToolbarSeparator>
                                <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                                </f:ToolbarText>
                                <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                    <f:ListItem Text="100" Value="100" />
                                    <f:ListItem Text="500" Value="500" />
                                </f:DropDownList>
                            </PageItems>
                        </f:Grid>
                    </Items>
                </f:Region>
            </Regions>
        </f:RegionPanel>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuMore" OnClick="btnMenuMore_Click" EnablePostBack="true" runat="server" Icon="ZoomIn"
                Text="展开全部">
            </f:MenuButton>
            <%--<f:MenuButton ID="btnMenuAdd" OnClick="btnMenuAdd_Click" EnablePostBack="true" runat="server" Hidden="true" Icon="Add"
                Text="定制">
            </f:MenuButton>--%>
            <%--<f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true" Hidden="true" Icon="Delete"
                ConfirmText="确认删除选中项？" ConfirmTarget="Top" runat="server" Text="删除">
            </f:MenuButton>--%>
        </f:Menu>
    </form>
    <script type="text/javascript">
        var treeID = '<%= trWBS.ClientID %>';
        var menuID = '<%= Menu1.ClientID %>';
        // 保存当前菜单对应的树节点ID
        var currentNodeId;

        // 返回false，来阻止浏览器右键菜单
        function onTreeNodeContextMenu(event, nodeId) {
            currentNodeId = nodeId;
            F(menuID).show();
            return false;
        }

        // 设置所有菜单项的禁用状态
        function setMenuItemsDisabled(disabled) {
            var menu = F(menuID);
            $.each(menu.items, function (index, item) {
                item.setDisabled(disabled);
            });
        }

        // 显示菜单后，检查是否禁用菜单项
        function onMenuShow() {
            if (currentNodeId) {
                var tree = F(treeID);
                var nodeData = tree.getNodeData(currentNodeId);
                if (nodeData.leaf) {
                    setMenuItemsDisabled(true);
                } else {
                    setMenuItemsDisabled(false);
                }
            }
        }
    </script>
</body>
</html>

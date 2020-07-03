<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlItemInitSet.aspx.cs" Inherits="FineUIPro.Web.CQMS.WBS.ControlItemInitSet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>施工WBS基础数据库</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <style>
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
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
                Title="施工WBS基础数据库" BodyPadding="0 5 0 0" Width="300px" Layout="Fit" runat="server"
                EnableCollapse="true">
                <Items>
                    <f:Panel runat="server" ID="panel2" RegionPosition="Left" RegionSplit="true" EnableCollapse="true"
                        Width="400" Title="施工WBS基础数据库" TitleToolTip="施工WBS基础数据库" ShowBorder="true" ShowHeader="true"
                        BodyPadding="5px" IconFont="ArrowCircleLeft">
                        <Items>
                            <f:Tree ID="trWBS" Width="290" Height="600px" EnableCollapse="true" ShowHeader="true"
                                OnNodeCommand="trWBS_NodeCommand" OnNodeExpand="trWBS_NodeExpand" AutoLeafIdentification="true"
                                runat="server">
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
                        runat="server" BoxFlex="1" DataKeyNames="ControlItemCode" AllowSorting="true" EnableColumnLines="true"
                        SortField="ControlItemCode" SortDirection="ASC" AllowCellEditing="true" ClicksToEdit="1"
                        EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" ForceFit="true"
                        ShowSelectedCell="true" DataIDField="ControlItemCode" AllowPaging="true" IsDatabasePaging="true"
                        PageSize="100" OnPageIndexChange="Grid1_PageIndexChange" AllowFilters="true" OnFilterChange="Grid1_FilterChange">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                    <f:Button ID="btnNew" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNew_Click"
                                    Hidden="true">
                                </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RenderField Width="90px" ColumnID="ControlItemContent" DataField="ControlItemContent" FieldType="String"
                                HeaderText="工作包" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="70px" ColumnID="ControlPoint" DataField="ControlPoint" FieldType="String"
                                HeaderText="控制点等级" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="65px" ColumnID="Weights" DataField="Weights" FieldType="String"
                                HeaderText="权重%" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="280px" ColumnID="ControlItemDef" DataField="ControlItemDef" FieldType="String"
                                HeaderText="控制点内容描述" HeaderTextAlign="Center" TextAlign="Center" >
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="HGForms" DataField="HGForms" FieldType="String"
                                HeaderText="对应的化工资料表格" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="SHForms" DataField="SHForms" FieldType="String"
                                HeaderText="对应的石化资料表格" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="Standard" DataField="Standard" FieldType="String"
                                HeaderText="质量验收规范" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="ClauseNo" DataField="ClauseNo" FieldType="String"
                                HeaderText="条款号" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
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
                                <f:ListItem Text="100" Value="100" />
                                <f:ListItem Text="500" Value="500" />
                            </f:DropDownList>
                        </PageItems>
                    </f:Grid>
                </Items>
            </f:Region>
        </Regions>
    </f:RegionPanel>
    <f:Window ID="Window1" Title="编辑" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="800px" Height="300px">
    </f:Window>
    <f:Window ID="Window2" Title="增加" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="true"
        Width="800px" Height="300px">
    </f:Window>
    <f:Window ID="Window3" Title="编辑" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window3_Close" IsModal="true"
        Width="1000px" Height="600px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuAdd" OnClick="btnMenuAdd_Click" EnablePostBack="true" runat="server" Hidden="true" Icon="Add"
            Text="增加">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true" Hidden="true" Icon="Pencil"
            runat="server" Text="修改">
        </f:MenuButton>
        <%--        <f:MenuButton ID="btnMenuCopy" OnClick="btnMenuCopy_Click" EnablePostBack="true"
            runat="server" Text="拷贝" >
        </f:MenuButton>--%>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true" Hidden="true" Icon="Delete"
            ConfirmText="确认删除选中项？" ConfirmTarget="Top" runat="server" Text="删除">
        </f:MenuButton>
    </f:Menu>
        <f:Menu ID="Menu2" runat="server">
            <Items>
                <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Hidden="true" Text="修改" Icon="Pencil"
                    OnClick="btnMenuModify_Click">
                </f:MenuButton>
                <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server" Hidden="true" Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？"
                    OnClick="btnMenuDel_Click">
                </f:MenuButton>
            </Items>
        </f:Menu>
    </form>
    <script type="text/javascript">
        var treeID = '<%= trWBS.ClientID %>';
        var menuID = '<%= Menu1.ClientID %>';
        var menuID2 = '<%= Menu2.ClientID %>';
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

         

        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID2).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>
</body>
</html>

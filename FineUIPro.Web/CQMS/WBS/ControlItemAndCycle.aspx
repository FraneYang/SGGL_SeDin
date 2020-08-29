<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlItemAndCycle.aspx.cs" Inherits="FineUIPro.Web.CQMS.WBS.ControlItemAndCycle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>项目施工WBS定制</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <style>
        .f-grid-row .f-grid-cell-inner {
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
      <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" CssClass="blockpanel" Margin="5px" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
            <Items>
                <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true" EnableCollapse="true" Layout="Fit"
                    RegionPercent="20%" Title="项目WBS基础数据库" TitleToolTip="项目WBS基础数据库" ShowBorder="true" ShowHeader="true"
                    BodyPadding="10px">
                    <Items>
                        <f:Tree ID="trWBS"  EnableCollapse="true" ShowHeader="true" 
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
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" Layout="Fit"
                    Title="中间面板" ShowBorder="true" ShowHeader="false" BodyPadding="10px">
                    <Items>
                        <f:Grid ID="Grid1" Width="870px" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                            runat="server" BoxFlex="1" DataKeyNames="ControlItemAndCycleId,InitControlItemCode" AllowSorting="true" EnableColumnLines="true"
                            SortField="ControlItemAndCycleCode" SortDirection="ASC" AllowCellEditing="true" ClicksToEdit="1"
                            EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"  ForceFit="true"
                            ShowSelectedCell="true" DataIDField="ControlItemAndCycleId" AllowPaging="true" IsDatabasePaging="true"
                            PageSize="100" OnPageIndexChange="Grid1_PageIndexChange" AllowFilters="true" OnFilterChange="Grid1_FilterChange">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                                    <Items>
                                        <%--<f:Button ID="btnNew" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNew_Click"
                                            Hidden="true">
                                        </f:Button>--%>
                                        <f:Label runat="server" Width="380px" ID="lbHd"></f:Label>
                                        <f:CheckBox runat="server" ID="cbAllHGForms" Label="全选对应的化工资料表格" LabelWidth="175px" AutoPostBack="true" OnCheckedChanged="cbAllHGForms_CheckedChanged"></f:CheckBox>
                                        <f:CheckBox runat="server" ID="cbAllSHForms" Label="全选对应的石化资料表格" LabelWidth="175px" AutoPostBack="true" OnCheckedChanged="cbAllSHForms_CheckedChanged"></f:CheckBox>
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                        <f:Button ID="btnIn" ToolTip="批量导入计划完成时间" Icon="ArrowDown" runat="server" Text="" Hidden="true"
                                            OnClick="btnIn_Click">
                                        </f:Button>
                                        <f:Button ID="btnSave" ToolTip="保存" Icon="SystemSave" runat="server" Text="" Hidden="true"
                                            OnClick="btnSave_Click">
                                        </f:Button>
                                         <%--<f:Button ID="btnRset"  OnClick="btnRset_Click" ToolTip="恢复默认" Hidden="true" Icon="ArrowUndo" runat="server" >
                                </f:Button>--%>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:RenderField Width="70px" ColumnID="ControlItemContent" DataField="ControlItemContent" FieldType="String"
                                    HeaderText="工作包" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField HeaderText="控制点等级" ColumnID="ControlPoint" DataField="ControlPoint"
                                    SortField="ControlPoint" HeaderTextAlign="Center" TextAlign="Center" Width="70px"
                                    FieldType="String">
                                    <Editor>
                                        <f:DropDownList ID="drpControlPoint" runat="server">
                                            <f:ListItem Text="A" Value="A" />
                                            <f:ListItem Text="AR" Value="AR" />
                                            <f:ListItem Text="B" Value="B" />
                                            <f:ListItem Text="BR" Value="BR" />
                                            <f:ListItem Text="C" Value="C" />
                                            <f:ListItem Text="CR" Value="CR" />
                                        </f:DropDownList>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField HeaderText="权重%" ColumnID="Weights" DataField="Weights"
                                    SortField="Weights" HeaderTextAlign="Center" TextAlign="Center" Width="85px"
                                    FieldType="String">
                                    <Editor>
                                        <f:NumberBox ID="txtWeights" runat="server" NoNegative="true" NoDecimal="true">
                                        </f:NumberBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField HeaderText="控制点内容描述" ColumnID="ControlItemDef" DataField="ControlItemDef" SortField="ControlItemDef"
                                    HeaderTextAlign="Center" TextAlign="Center" Width="300px" FieldType="String" >
                                    <Editor>
                                        <f:TextArea runat="server" ID="txtControlItemDef" AutoGrowHeight="true" Height="70px">
                                        </f:TextArea>
                                    </Editor>
                                </f:RenderField>
                                <f:TemplateField ColumnID="HGForms" Width="180px" HeaderText="对应的化工资料表格" HeaderTextAlign="Center" TextAlign="Left">
                                    <ItemTemplate>
                                        <asp:CheckBoxList runat="server" ID="cblHGForms" RepeatDirection="Vertical" >
                                        </asp:CheckBoxList>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:TemplateField ColumnID="SHForms" Width="180px" HeaderText="对应的石化资料表格" HeaderTextAlign="Center" TextAlign="Left">
                                    <ItemTemplate>
                                        <asp:CheckBoxList runat="server" ID="cblSHForms" RepeatDirection="Vertical" >
                                        </asp:CheckBoxList>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:RenderField Width="120px" ColumnID="PlanCompleteDate" DataField="PlanCompleteDate" FieldType="Date"
                                Renderer="Date" RendererArgument="yyyy-MM-dd" TextAlign="Center" HeaderText="计划完成时间" HeaderTextAlign="Center">
                                <Editor>
                                    <f:DatePicker ID="DatePicker2" Required="true" runat="server">
                                    </f:DatePicker>
                                </Editor>
                            </f:RenderField>
                                <f:RenderField HeaderText="质量验收规范" ColumnID="Standard" DataField="Standard" SortField="Standard"
                                    HeaderTextAlign="Center" TextAlign="Center" Width="140px" FieldType="String">
                                    <Editor>
                                        <f:TextArea runat="server" ID="txtStandard" AutoGrowHeight="true" Height="70px">
                                        </f:TextArea>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField HeaderText="条款号" ColumnID="ClauseNo" DataField="ClauseNo" SortField="ClauseNo" Hidden="true"
                                    HeaderTextAlign="Center" TextAlign="Center" Width="60px" FieldType="String">
                                    <Editor>
                                        <f:TextArea runat="server" ID="txtClauseNo" AutoGrowHeight="true" Height="70px">
                                        </f:TextArea>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField HeaderText="检查次数" ColumnID="CheckNum" DataField="CheckNum"
                                    SortField="CheckNum" HeaderTextAlign="Center" TextAlign="Center" Width="50px"
                                    FieldType="String">
                                    <Editor>
                                        <f:NumberBox ID="txtCheckNum" runat="server" NoNegative="true" NoDecimal="false">
                                        </f:NumberBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField Width="20px" ColumnID="ControlItemAndCycleId" DataField="ControlItemAndCycleId" FieldType="String"
                                    HeaderText="工作包Id" HeaderTextAlign="Center" TextAlign="Center" Hidden="true">
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
                </f:Panel>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="定制" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
            Width="1300px" Height="656px">
        </f:Window>
        <f:Window ID="Window2" Title="增加" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Self" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="true"
            Width="1300px" Height="656px">
        </f:Window>
        <f:Window ID="Window3" Title="编辑" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Self" EnableResize="true" runat="server" OnClose="Window3_Close" IsModal="true"
            Width="1300px" Height="656px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuAdd" OnClick="btnMenuAdd_Click" EnablePostBack="true" runat="server" Hidden="true" Icon="Add"
                Text="定制">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuMore" OnClick="btnMenuMore_Click" EnablePostBack="true" runat="server" Icon="ZoomIn"
                Text="展开全部">
            </f:MenuButton>
            <%--<f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true" Hidden="true" Icon="Delete"
                ConfirmText="确认删除选中项？" ConfirmTarget="Top" runat="server" Text="删除">
            </f:MenuButton>--%>
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

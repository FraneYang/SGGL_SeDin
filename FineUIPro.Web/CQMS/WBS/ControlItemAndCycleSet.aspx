<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlItemAndCycleSet.aspx.cs" Inherits="FineUIPro.Web.CQMS.WBS.ControlItemAndCycleSet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server" />
        <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
            <Regions>
                <f:Region ID="Region1" ShowBorder="false" ShowHeader="false" RegionPosition="Left"
                    Title="项目WBS基础数据库" BodyPadding="0 5 0 0" Width="870px" Layout="Fit" runat="server"
                    EnableCollapse="true">
                    <Items>
                        <f:Grid ID="Grid1" Width="870px" ShowBorder="true" ShowHeader="false" EnableCollapse="true" ForceFit="true"
                            runat="server" BoxFlex="1" DataKeyNames="ControlItemAndCycleId,ControlItemAndCycleCode" AllowSorting="true" EnableColumnLines="true"
                            SortField="ControlItemAndCycleCode" SortDirection="ASC" AllowCellEditing="true" ClicksToEdit="1"
                            ShowSelectedCell="true" DataIDField="ControlItemAndCycleId" AllowPaging="false" IsDatabasePaging="false"
                            PageSize="200" OnRowCommand="Grid1_RowCommand">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                                    <Items>
                                        <f:CheckBox runat="server" ID="cbAll" Label="全选" LabelWidth="50px" AutoPostBack="true" OnCheckedChanged="cbAll_CheckedChanged"></f:CheckBox>
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                        <f:Label runat="server" ID="lbWeights" Label="累计权重"></f:Label>
                                        <f:Label runat="server" Width="90px"></f:Label>
                                        <f:Button ID="btnSave" ToolTip="保存" Icon="SystemSave" runat="server" Text=""
                                            OnClick="btnSave_Click">
                                        </f:Button>
                                        <f:Button runat="server" ID="btnDel" OnClick="btnDel_Click" Hidden="true"></f:Button>
                                        <f:Button runat="server" ID="btnReCheck" OnClick="btnReCheck_Click" Hidden="true"></f:Button>
                                        <f:HiddenField runat="server" ID="hdId"></f:HiddenField>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:TemplateField ColumnID="Check" Width="100px" HeaderText="选择" HeaderTextAlign="Center" TextAlign="Center"
                                    EnableLock="true" Locked="False">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="cbSelect" AutoPostBack="true" OnCheckedChanged="cbSelect_CheckedChanged" />
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:RenderField Width="200px" ColumnID="ControlItemContent" DataField="ControlItemContent" FieldType="String"
                                    HeaderText="工作包" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField HeaderText="定制" ColumnID="AttachUrl" DataField="AttachUrl" SortField="AttachUrl"
                                    HeaderTextAlign="Center" TextAlign="Center" Width="250px" FieldType="String" >
                                    <Editor>
                                        <f:TextBox runat="server" ID="txtName">
                                        </f:TextBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField HeaderText="权重%" ColumnID="Weights" DataField="Weights"
                                    SortField="Weights" HeaderTextAlign="Center" TextAlign="Center" Width="65px"
                                    FieldType="String">
                                    <Editor>
                                        <f:NumberBox ID="txtWeights" runat="server" NoNegative="true" TrimEndZero="false" NoDecimal="false">
                                        </f:NumberBox>
                                    </Editor>
                                </f:RenderField>
                                <%--<f:TemplateField ColumnID="PlanCompleteDate" Width="120px" HeaderText="计划完成时间" HeaderTextAlign="Center" TextAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPlanCompleteDate" runat="server" Text='<%# ConvertDate(Eval("PlanCompleteDate")) %>'
                                            Width="98%" CssClass="Wdate" Style="width: 98%; cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"
                                            BorderStyle="None">
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </f:TemplateField>--%>
                                <f:RenderField Width="120px" ColumnID="PlanCompleteDate" DataField="PlanCompleteDate" FieldType="Date"
                                Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="计划完成时间" TextAlign="Center" HeaderTextAlign="Center">
                                <Editor>
                                    <f:DatePicker ID="DatePicker2" Required="true" runat="server">
                                    </f:DatePicker>
                                </Editor>
                            </f:RenderField>
                                <f:LinkButtonField Width="60px" TextAlign="Center" HeaderText="增加" ToolTip="增加" CommandName="add"
                                    Icon="Add" />
                                <f:LinkButtonField Width="60px" TextAlign="Center" HeaderText="删除" ToolTip="删除" CommandName="del"
                                    Icon="Delete" />
                            </Columns>
                            <Listeners>
                                <f:Listener Event="afteredit" Handler="onGridAfterEdit" />
                            </Listeners>
                        </f:Grid>
                    </Items>
                </f:Region>
                <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox"
                    BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server" BodyPadding="0 5 0 0">
                    <Items>
                        <f:Panel runat="server" ID="panel2" RegionPosition="Left" RegionSplit="true" EnableCollapse="true"
                            Width="400" Title="项目WBS基础数据库" TitleToolTip="项目WBS基础数据库" ShowBorder="true" ShowHeader="true"
                            BodyPadding="5px" IconFont="ArrowCircleLeft">
                            <Items>
                                <f:Tree ID="trWBS" Width="380" Height="550px" EnableCollapse="true" ShowHeader="true"
                                    AutoLeafIdentification="true"
                                    runat="server">
                                    <Toolbars>
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
                                    </Toolbars>
                                </f:Tree>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Region>
            </Regions>
        </f:RegionPanel>
    </form>
    <script type="text/javascript">
        var treeID = '<%= trWBS.ClientID %>';
        var btnDelID = '<%= btnDel.ClientID %>';
        var btnReCheckID = '<%= btnReCheck.ClientID %>';
        var hdId = '<%= hdId.ClientID %>';
        // 保存当前菜单对应的树节点ID
        var currentNodeId;

        // 设置所有菜单项的禁用状态
        function setMenuItemsDisabled(disabled) {
            var menu = F(menuID);
            $.each(menu.items, function (index, item) {
                item.setDisabled(disabled);
            });
        }

        function onGridAfterEdit(event, value, params) {
              var me = this, columnId = params.columnId, rowId = params.rowId;
            if (columnId === 'Weights') {
                var Weights = me.getCellValue(rowId, 'Weights');
                if (Weights.toString() != "") {
                    me.updateCellValue(rowId, 'Weights', parseFloat(Weights).toFixed(2));
                }
            }
            // 回发到后台更新
            __doPostBack('', 'UPDATE_SUMMARY');
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

        var grid1ClientID = '<%= Grid1.ClientID %>';


        // 设置所有菜单项的禁用状态
        function ShowDel() {
            var result = confirm("确定删除该条信息吗?");
            if (result == true) {
                document.getElementById(btnDelID).click();
            }
            else {
                 document.getElementById(btnReCheckID).click();
            }
        }
    </script>
</body>
</html>

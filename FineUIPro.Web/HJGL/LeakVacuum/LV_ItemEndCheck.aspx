<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LV_ItemEndCheck.aspx.cs" Inherits="FineUIPro.Web.HJGL.LeakVacuum.LV_ItemEndCheck" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>试压尾项检查</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
            <Items>
                <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                    EnableCollapse="true" Width="290px" Title="试压尾项检查" ShowBorder="true" Layout="VBox"
                    ShowHeader="true" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                            <Items>
                                <f:DatePicker ID="txtSearchDate" runat="server" EmptyText="输入查询条件" AutoPostBack="true" OnTextChanged="Tree_TextChanged"
                                    DateFormatString="yyyy-MM" Label="按月份" LabelWidth="70px">
                                </f:DatePicker>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>
                        <f:Tree ID="tvControlItem" ShowHeader="false" Title="尾工录入节点树" OnNodeCommand="tvControlItem_NodeCommand"
                            runat="server" ShowBorder="false" EnableCollapse="true" EnableSingleClickExpand="true" AutoLeafIdentification="true"
                            EnableSingleExpand="true" EnableTextSelection="true">
                        </f:Tree>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                    Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="试压尾项检查"
                    TitleToolTip="试压尾项检查" AutoScroll="true">
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="试压尾项检查录入(双击编辑)" EnableCollapse="false" runat="server"
                            BoxFlex="1" DataKeyNames="LVItemEndCheckId" AllowCellEditing="true" EnableColumnLines="true"
                            ClicksToEdit="2" DataIDField="LVItemEndCheckId" AllowSorting="true" SortField="CheckDate" OnPreDataBound="Grid1_PreDataBound"
                            OnSort="Grid1_Sort" EnableTextSelection="True" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar5" runat="server" ToolbarAlign="Right">
                                    <Items>
                                        <f:Button ID="btnNew" Text="新增数据" Icon="Add" runat="server" OnClick="btnNew_Click">
                                        </f:Button>
                                        <f:Button ID="btnDelete" Text="删除选中行" Icon="Delete" OnClick="btnDelete_Click" runat="server">
                                        </f:Button>
                                        <%--<f:Button ID="btnSave" Icon="SystemSave" runat="server" Text="保存" OnClick="btnSave_Click">
                                        </f:Button>--%>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:RenderField Width="120px" ColumnID="ItemType" DataField="ItemType"
                                    HeaderTextAlign="Center" HeaderText="检查项" TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="Remark" DataField="Remark"
                                    HeaderTextAlign="Center" HeaderText="项检查内容" TextAlign="Left" ExpandUnusedSpace="true">
                                </f:RenderField>
                                <f:RenderField Width="110px" ColumnID="CheckName" DataField="CheckName"
                                    HeaderTextAlign="Center" HeaderText="检查人" TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="110px" ColumnID="CheckDate" DataField="CheckDate"
                                    HeaderTextAlign="Center" FieldType="Date" TextAlign="Left"
                                    Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="检查日期">
                                </f:RenderField>
                                <f:RenderField Width="110px" ColumnID="DealName" DataField="DealName"
                                    HeaderTextAlign="Center" HeaderText="处理人" TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="110px" ColumnID="DealDate" DataField="DealDate"
                                    HeaderTextAlign="Center" FieldType="Date" TextAlign="Left"
                                    Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="处理日期">
                                </f:RenderField>
                                <f:LinkButtonField ColumnID="Delete" Width="50px" EnablePostBack="false" Icon="Delete"
                                    HeaderTextAlign="Center" HeaderText="删除" Hidden="true" />
                            </Columns>
                            <Listeners>
                                <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                            </Listeners>
                        </f:Grid>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
            Width="800px" Height="460px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <Items>
                <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Text="编辑" Icon="Pencil" OnClick="btnMenuModify_Click">
                </f:MenuButton>
            </Items>
        </f:Menu>
    </form>
    <script type="text/javascript">
        // 返回false，来阻止浏览器右键菜单
        var menuID = '<%= Menu1.ClientID %>';
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

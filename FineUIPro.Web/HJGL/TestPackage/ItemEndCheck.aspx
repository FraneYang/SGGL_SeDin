<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemEndCheck.aspx.cs" Inherits="FineUIPro.Web.HJGL.TestPackage.ItemEndCheck" %>

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
                            BoxFlex="1" DataKeyNames="PTP_ID" AllowCellEditing="true" EnableColumnLines="true"
                            ClicksToEdit="2" DataIDField="PTP_ID" AllowSorting="true" SortField="CheckDate"
                            OnSort="Grid1_Sort" EnableTextSelection="True" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">
                            <Columns>
                                <f:RenderField Width="120px" ColumnID="TestPackageNo" DataField="TestPackageNo"
                                    HeaderTextAlign="Center" HeaderText="试压包编号" TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="TestPackageName" DataField="TestPackageName"
                                    HeaderTextAlign="Center" HeaderText="试压包名称" TextAlign="Left" ExpandUnusedSpace="true">
                                </f:RenderField>
                                <f:RenderField Width="110px" ColumnID="UserName" DataField="UserName"
                                    HeaderTextAlign="Center" HeaderText="建档人" TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="110px" ColumnID="TableDate" DataField="TableDate"
                                    HeaderTextAlign="Center" FieldType="Date" TextAlign="Left"
                                    Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="建档日期">
                                </f:RenderField>
                                <f:RenderField Width="110px" ColumnID="Remark" DataField="Remark"
                                    HeaderTextAlign="Center" HeaderText="备注" TextAlign="Left">
                                </f:RenderField>
                                <f:TemplateField ColumnID="State" Width="110px" HeaderText="审批状态" HeaderTextAlign="Center" TextAlign="Center"
                                    EnableLock="true" Locked="False">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# ConvertState(Eval("State")) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:TemplateField ColumnID="AuditMan" Width="100px" HeaderText="办理人" HeaderTextAlign="Center" TextAlign="Center"
                                    EnableLock="true" Locked="False">
                                    <ItemTemplate>
                                        <asp:Label ID="Label41" runat="server" Text='<%# ConvertMan(Eval("PTP_ID")) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
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
            Target="Parent" EnableResize="false" runat="server" IsModal="true" OnClose="Window1_Close"
            Width="900px" Height="900px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <Items>
                <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Text="编辑" Icon="Pencil" OnClick="btnMenuModify_Click">
                </f:MenuButton>
                <f:MenuButton ID="btnMenuView" EnablePostBack="true" runat="server" Text="查看" Icon="ApplicationViewIcons" OnClick="btnMenuView_Click">
                </f:MenuButton>
                <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server" Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？"
                    OnClick="btnMenuDel_Click" >
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

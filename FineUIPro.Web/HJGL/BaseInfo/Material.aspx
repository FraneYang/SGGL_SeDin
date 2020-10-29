<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Material.aspx.cs" Inherits="FineUIPro.Web.HJGL.BaseInfo.Material" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>材质定义</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="材质定义"
                EnableCollapse="true" runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="MaterialId"
                AllowCellEditing="true" ClicksToEdit="2" DataIDField="MaterialId" AllowSorting="true" ForceFit="true"
                SortField="MaterialCode" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox ID="txtMaterialCode" runat="server" Label="型号、牌号、级别"
                                EmptyText="输入查询条件" Width="320px" LabelWidth="200px" LabelAlign="Right">
                            </f:TextBox>
                            <f:DropDownList ID="drpSteType" runat="server" Label="材质类型"
                                EnableEdit="true" Width="300px" LabelWidth="140px" LabelAlign="Right">
                            </f:DropDownList>
                            <f:Button ID="btnQuery" ToolTip="查询" Icon="SystemSearch"
                                EnablePostBack="true" OnClick="btnQuery_Click" runat="server">
                            </f:Button>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="true"
                                runat="server" OnClick="btnNew_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RenderField Width="200px" ColumnID="MaterialCode" DataField="MaterialCode" FieldType="String"
                        HeaderText="型号、牌号、级别" HeaderTextAlign="Center"
                        TextAlign="Left" SortField="MaterialCode">
                    </f:RenderField>
                    <f:TemplateField Width="180px" HeaderText="材质类型"
                        HeaderTextAlign="Center" TextAlign="Center" SortField="SteelType">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# ConvertSteelType(Eval("SteelType")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="200px" ColumnID="MaterialType" DataField="MaterialType" FieldType="String"
                        HeaderText="材料标准" HeaderTextAlign="Center"
                        TextAlign="Left" SortField="MaterialType">
                    </f:RenderField>
                   <f:RenderField Width="150px" ColumnID="MetalType" DataField="MetalType" FieldType="String"
                        HeaderText="金属类别代号" HeaderTextAlign="Center"
                        TextAlign="Left" SortField="MetalType">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="MaterialClass" DataField="MaterialClass" FieldType="String"
                        HeaderText="材质类别" HeaderTextAlign="Center"
                        TextAlign="Left" SortField="MaterialClass">
                    </f:RenderField>
                    <f:RenderField Width="160px" ColumnID="MaterialGroup" DataField="MaterialGroup" FieldType="String"
                        HeaderText="材质组别" HeaderTextAlign="Center"
                        TextAlign="Left" SortField="MaterialGroup">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="Remark" DataField="Remark" FieldType="String"
                        HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left">
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
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="false" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="700px" Height="420px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="BulletEdit" EnablePostBack="true"
            runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Top"
            runat="server" Text="删除">
        </f:MenuButton>
        <f:MenuButton ID="btnView" OnClick="btnView_Click" Icon="Find" EnablePostBack="true"
            runat="server" Text="查看">
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

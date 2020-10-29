<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Weld.aspx.cs" Inherits="FineUIPro.Web.HJGL.BaseInfo.Weld" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊缝类型</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="焊缝类型"
                EnableCollapse="true" runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="WeldTypeId"
                AllowCellEditing="true" ClicksToEdit="2" DataIDField="WeldTypeId" AllowSorting="true"
                SortField="WeldTypeCode" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox ID="txtWeldTypeCode" runat="server" Label="焊缝类型代号"
                                EmptyText="输入查询条件" Width="350px" LabelWidth="180px" LabelAlign="Right">
                            </f:TextBox>
                            <f:TextBox ID="txtWeldTypeName" runat="server" Label="焊缝类型名称"
                                EmptyText="输入查询条件" Width="350px" LabelWidth="180px" LabelAlign="Right">
                            </f:TextBox>
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
                    <f:RenderField Width="250px" ColumnID="WeldTypeCode" DataField="WeldTypeCode" FieldType="String"
                        HeaderText="焊缝类型代号" HeaderTextAlign="Center"
                        TextAlign="Left" SortField="WeldTypeCode">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="WeldTypeName" DataField="WeldTypeName" FieldType="String"
                        HeaderText="焊缝类型名称" HeaderTextAlign="Center"
                        TextAlign="Left" SortField="WeldTypeName">
                    </f:RenderField>
                    <f:TemplateField Width="200px" HeaderText="探伤类型" HeaderTextAlign="Center"
                        TextAlign="Center" SortField="DetectionType">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# ConvertDetectionType(Eval("DetectionType")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="200px" ColumnID="Thickness" DataField="Thickness" FieldType="String"
                        HeaderText="壁厚" HeaderTextAlign="Center"
                        TextAlign="Left" SortField="Thickness">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="Remark" DataField="Remark" FieldType="String"
                        HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left"
                        ExpandUnusedSpace="true">
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
                        <f:ListItem Text="10" Value="10" />
                        <f:ListItem Text="15" Value="15" />
                        <f:ListItem Text="20" Value="20" />
                        <f:ListItem Text="25" Value="25" />
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="false" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="700px" Height="400px">
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

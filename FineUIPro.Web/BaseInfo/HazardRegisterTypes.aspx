<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HazardRegisterTypes.aspx.cs" Inherits="FineUIPro.Web.BaseInfo.HazardRegisterTypes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全巡检类型</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全巡检类型" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="RegisterTypesId" DataIDField="RegisterTypesId" ForceFit="true"
                AllowSorting="true" SortField="TypeCode" SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" 
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableColumnLines="true"
                 EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false"  runat="server">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:TemplateField Width="50px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                   <f:RenderField Width="100px" ColumnID="TypeCode" DataField="TypeCode" SortField="TypeCode"
                        FieldType="String" HeaderText="编号" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="550px" ColumnID="RegisterTypesName" DataField="RegisterTypesName" SortField="RegisterTypesName"
                        FieldType="String" HeaderText="名称" EnableFilter="true" HeaderTextAlign="Center"
                        TextAlign="Left">                        
                    </f:RenderField>
                    <f:CheckBoxField Width="100px" RenderAsStaticField="true" TextAlign="Left"  DataField="IsPunished" HeaderText="是否处罚" /> 
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
    <f:Window ID="Window1" Title="编辑安全巡检类型" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="false" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="500px" Height="230px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑" Icon="Pencil">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true" Icon="Delete"
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

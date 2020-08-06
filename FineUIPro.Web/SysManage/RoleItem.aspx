<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleItem.aspx.cs" Inherits="FineUIPro.Web.SysManage.RoleItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="历史角色" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="RoleItemId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="RoleItemId" AllowSorting="true" SortField="RoleItemId"
                SortDirection="ASC"  AllowPaging="true" IsDatabasePaging="true" EnableRowDoubleClickEvent="true"
                    OnRowDoubleClick="Grid1_RowDoubleClick" PageSize="10" ForceFit="true"  EnableTextSelection="True">
                <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:TextBox runat="server" ID="txtProjectName" Label="项目"  Width="300px" LabelWidth="70px"
                                    OnTextChanged="txtProjectName_Blur" AutoPostBack="true"></f:TextBox>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="btnNew" ToolTip="新增" Icon="Add" runat="server"
                                    OnClick="btnNew_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                    <f:RenderField Width="180px" ColumnID="ProjectName" DataField="ProjectName" EnableFilter="true"
                        SortField="ProjectName" FieldType="String" HeaderText="项目名称" HeaderTextAlign="Center"
                        TextAlign="Left">                      
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="UserName" DataField="UserName" EnableFilter="true"
                        SortField="UserName" FieldType="String" HeaderText="姓名" HeaderTextAlign="Center"
                        TextAlign="Left">                      
                    </f:RenderField>
                    <f:TemplateField ColumnID="RoleId" Width="180px" HeaderText="角色名称" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# ConvertRoleName(Eval("RoleId")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                    <f:RenderField Width="100px" ColumnID="入场时间" DataField="IntoDate" EnableFilter="true" Renderer="Date"
                        SortField="IntoDate" FieldType="Date" HeaderText="入场时间" HeaderTextAlign="Center"
                        TextAlign="Left">                      
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="出场时间" DataField="OutDate" EnableFilter="true" Renderer="Date"
                        SortField="OutDate" FieldType="Date" HeaderText="出场时间" HeaderTextAlign="Center"
                        TextAlign="Left">                      
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
        <f:Window ID="Window1" Title="编辑历史记录" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
            Height="420px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
                 runat="server" Text="编辑" Icon="Pencil">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
                ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除"
                Icon="Delete">
            </f:MenuButton>
        </f:Menu>
    </form>
 </body>
    <script type="text/jscript">
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
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleList.aspx.cs" Inherits="FineUIPro.Web.SysManage.RoleList" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>角色管理</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />   
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="角色管理" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="RoleId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="RoleId" AllowSorting="true" SortField="RoleCode"
                SortDirection="ASC" OnSort="Grid1_Sort"   AllowPaging="true" IsDatabasePaging="true"
                PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" ForceFit="true"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"  EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                            <f:TextBox runat="server" Label="名称" ID="txtRoleName" EmptyText="输入查询条件" 
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px">
                             </f:TextBox>
                             <f:TextBox runat="server" Label="类型" ID="txtRoleTypeName" EmptyText="输入查询条件" 
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px">
                             </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server" Hidden ="true">
                            </f:Button>                           
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>                   
                    <f:RenderField Width="140px" ColumnID="RoleCode" DataField="RoleCode" 
                        SortField="RoleCode" FieldType="String" HeaderText="编码" HeaderTextAlign="Center"
                        TextAlign="Left">                      
                    </f:RenderField>
                    <f:RenderField Width="140px" ColumnID="RoleName" DataField="RoleName" EnableFilter="true"
                        SortField="RoleName" FieldType="String" HeaderText="名称" HeaderTextAlign="Center"
                        TextAlign="Left">                      
                    </f:RenderField>
                      <f:RenderField Width="90px" ColumnID="RoleTypeName" DataField="RoleTypeName" 
                        SortField="RoleTypeName" FieldType="String" HeaderText="类型" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField>
                    <f:TemplateField ColumnID="CNCodes" Width="180px" HeaderText="对口专业" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# ConvertCNCodes(Eval("CNCodes")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                    <f:CheckBoxField Width="80px" RenderAsStaticField="true" TextAlign="Center"  DataField="IsAuditFlow" HeaderText="参与审批" />
                    <f:CheckBoxField Width="75px" RenderAsStaticField="true" TextAlign="Center"  DataField="IsSystemBuilt" HeaderText="内置" />
                    <f:RenderField  ColumnID="Def" DataField="Def" SortField="Def" FieldType="String" Width="80px"
                        HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left" >
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
    <f:Window ID="Window1" Title="角色管理" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" 
        Width="700px" Height="280px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            runat="server" Text="编辑" Hidden ="true" Icon="Pencil">
        </f:MenuButton>       
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"  Icon="Delete"
            ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除" Hidden ="true">
        </f:MenuButton>        
    </f:Menu>
    </form>
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
</body>
</html>

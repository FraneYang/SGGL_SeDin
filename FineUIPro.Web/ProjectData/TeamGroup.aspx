<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamGroup.aspx.cs" Inherits="FineUIPro.Web.ProjectData.TeamGroup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>班组信息</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="班组信息" EnableCollapse="true"
                runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="TeamGroupId"
                AllowCellEditing="true" ClicksToEdit="2" DataIDField="TeamGroupId" AllowSorting="true"
                SortField="TeamGroupCode" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox runat="server" Label="班组名称" ID="txtTeamGroupName" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px" LabelAlign="right">
                            </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" Hidden="true"
                                runat="server">
                            </f:Button>
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>                    
                    <f:TemplateField ColumnID="tfNumber" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                        TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="120px" ColumnID="TeamGroupCode" DataField="TeamGroupCode" SortField="TeamGroupCode"
                        FieldType="String" HeaderText="班组编号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="TeamGroupName" DataField="TeamGroupName" SortField="TeamGroupName"
                        FieldType="String" HeaderText="班组名称" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:TemplateField ColumnID="tfUnitId" Width="300px" HeaderText="单位名称" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitName" runat="server" Text='<%#ConvertUnitName(Eval("UnitId")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField ColumnID="tfGroupLeader" Width="120px" HeaderText="班组长" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lbGroupLeader" runat="server" Text='<%#ConvertGroupLeader(Eval("GroupLeaderId")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                         <f:TemplateField ColumnID="tfPersonNum" Width="100px" HeaderText="人数" HeaderTextAlign="Center" TextAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lbPersonNum" runat="server" Text='<%#ConvertPersonNum(Eval("TeamGroupId")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="150px" ColumnID="Remark" DataField="Remark" SortField="Remark"
                        FieldType="String" HeaderText="备注" ExpandUnusedSpace="true" HeaderTextAlign="Center"
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
    <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="800px" Height="330px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="BulletEdit" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server"
            Text="删除">
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="FineUIPro.Web.SysManage.UserList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户信息</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="用户信息" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="UserId" AllowCellEditing="true" EnableColumnLines="true"
                    ClicksToEdit="2" DataIDField="UserId" AllowSorting="true" SortField="UnitName,UserName" ForceFit="true"
                    SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                    PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                    OnRowDoubleClick="Grid1_RowDoubleClick" Width="980px" EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                            <Items>                             
                                <f:TextBox runat="server" Label="姓名" ID="txtUserName" EmptyText="输入查询条件" AutoPostBack="true"
                                    OnTextChanged="TextBox_TextChanged" Width="210px" LabelWidth="50px">
                                </f:TextBox>
                                <f:TextBox runat="server" Label="单位" ID="txtUnitName" EmptyText="输入查询条件" AutoPostBack="true"
                                    OnTextChanged="TextBox_TextChanged" Width="210px" LabelWidth="50px">
                                </f:TextBox>
                                <f:DropDownList ID="drpDepart" runat="server" Label="部门" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged"
                            EnableEdit="true" Width="190px" LabelWidth="50px">
                        </f:DropDownList>
                                <f:TextBox runat="server" Label="角色" ID="txtRoleName" EmptyText="输入查询条件" AutoPostBack="true"
                                    OnTextChanged="TextBox_TextChanged" Width="210px" LabelWidth="50px">
                                </f:TextBox>
                                <f:ToolbarFill runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                    Hidden="true">
                                </f:Button>
                                   <f:Button ID="btnImport" ToolTip="导入" Icon="ApplicationAdd" Hidden="true" runat="server"
                                    OnClick="btnImport_Click">
                            </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="45px" HeaderTextAlign="Center"
                            TextAlign="Center" />
                        <f:RenderField Width="100px" ColumnID="UserName" DataField="UserName" SortField="UserName"
                            FieldType="String" HeaderText="姓名" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="250px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                            FieldType="String" HeaderText="单位" HeaderTextAlign="Center" TextAlign="Left" >
                        </f:RenderField>
                        <f:RenderField Width="120px" ColumnID="DepartName" DataField="DepartName" SortField="DepartName"
                            FieldType="String" HeaderText="部门" HeaderTextAlign="Center" TextAlign="Left" >
                        </f:RenderField>
                        <f:RenderField Width="120px" ColumnID="Account" DataField="Account" SortField="Account"
                            FieldType="String" HeaderText="账号" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                       <%-- <f:RenderField Width="120px" ColumnID="IdentityCard" DataField="IdentityCard" SortField="IdentityCard"
                            FieldType="String" HeaderText="身份证号码" HeaderTextAlign="Center" TextAlign="Right" Hidden="true">
                        </f:RenderField>--%>
                        <f:TemplateField Width="150px" ColumnID="RoleName" HeaderText="本部角色" TextAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# ConvertRoleName(Eval("UserId")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:TemplateField Width="200px" ColumnID="ProjectRoleName" HeaderText="项目角色" TextAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lbtnPro" runat="server" Text='<%# ConvertProjectRoleName(Eval("UserId")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField Width="120px" ColumnID="Telephone" DataField="Telephone" SortField="Telephone"
                            FieldType="String" HeaderText="手机号码" HeaderTextAlign="Center" TextAlign="Right" Hidden="true">
                        </f:RenderField>
                        <f:CheckBoxField Width="50px" SortField="IsPost" RenderAsStaticField="true" DataField="IsPost"
                            HeaderText="在岗" HeaderTextAlign="Center" TextAlign="Center">
                        </f:CheckBoxField>
                 <%--       <f:CheckBoxField Width="50px" SortField="IsOffice" RenderAsStaticField="true" DataField="IsOffice"
                            HeaderText="本部" HeaderTextAlign="Center" TextAlign="Center">
                        </f:CheckBoxField>
                        <f:TemplateField Width="250px" ColumnID="UserId" HeaderText="参与项目" TextAlign="Left">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPro" runat="server" Text='<%# ConvertProject(Eval("UserId")) %>' OnClick="lbtnPro_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </f:TemplateField>--%>
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
        <f:Window ID="Window1" Title="用户信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="1000px"
            Height="420px">
        </f:Window>
        <f:Window ID="Window2" Title="导入人员信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" OnClose="Window2_Close" IsModal="false"
            CloseAction="HidePostBack" Width="1000px" Height="560px">
    </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
                Hidden="true" runat="server" Text="编辑" Icon="Pencil">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
                Hidden="true" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除"
                Icon="Delete">
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonSet.aspx.cs" Inherits="FineUIPro.Web.Person.PersonSet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>员工信息</title>
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
                    ClicksToEdit="2" DataIDField="UserId" AllowSorting="true" SortField="UserCode" ForceFit="true"
                    SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                    PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                    OnRowDoubleClick="Grid1_RowDoubleClick" Width="980px" EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                            <Items>
                                <f:TextBox runat="server" Label="用户" ID="txtUserName" EmptyText="输入查询条件" AutoPostBack="true"
                                    OnTextChanged="TextBox_TextChanged" Width="210px" LabelWidth="80px">
                                </f:TextBox>
                                <f:TextBox runat="server" Label="角色" ID="txtRoleName" EmptyText="输入查询条件" AutoPostBack="true"
                                    OnTextChanged="TextBox_TextChanged" Width="210px" LabelWidth="80px">
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
                        <f:RenderField Width="120px" ColumnID="Account" DataField="Account" SortField="Account"
                            FieldType="String" HeaderText="账号" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="200px" ColumnID="RoleName" DataField="RoleName" SortField="RoleName"
                            FieldType="String" HeaderText="角色" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="120px" ColumnID="Telephone" DataField="Telephone" SortField="Telephone"
                            FieldType="String" HeaderText="手机号码" HeaderTextAlign="Center" TextAlign="Right" Hidden="true">
                        </f:RenderField>
                        <f:CheckBoxField Width="50px" SortField="IsPost" RenderAsStaticField="true" DataField="IsPost"
                            HeaderText="在岗" HeaderTextAlign="Center" TextAlign="Center">
                        </f:CheckBoxField>
                        <f:CheckBoxField Width="50px" SortField="IsOffice" RenderAsStaticField="true" DataField="IsOffice"
                            HeaderText="本部" HeaderTextAlign="Center" TextAlign="Center">
                        </f:CheckBoxField>
                        <f:TemplateField Width="250px" ColumnID="UserId" HeaderText="参与项目" TextAlign="Left">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPro" runat="server" Text='<%# ConvertProject(Eval("UserId")) %>' OnClick="lbtnPro_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </f:TemplateField>
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
            Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="800px"
            Height="650px">
        </f:Window>
        <f:Window ID="Window2" Title="导入人员信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="false"
            CloseAction="HidePostBack" Width="1000px" Height="560px">
    </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
                Hidden="true" runat="server" Text="编辑" Icon="Pencil">
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

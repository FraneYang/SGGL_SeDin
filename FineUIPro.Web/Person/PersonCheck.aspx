<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonCheck.aspx.cs" Inherits="FineUIPro.Web.Person.PersonCheck" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>


                <f:Grid ID="Grid1" ShowBorder="true" EnableAjax="false" ShowHeader="false" Title="员工绩效考核"
                    EnableCollapse="true" runat="server" BoxFlex="1" EnableColumnLines="true" DataIDField="QuarterCheckId"
                     DataKeyNames="QuarterCheckId" AllowCellEditing="true" ClicksToEdit="2" SortDirection="DESC"
                    AllowPaging="true" IsDatabasePaging="true" PageSize="15" OnRowDoubleClick="Grid1_RowDoubleClick" EnableRowDoubleClickEvent="true" EnableTextSelection="True" OnRowCommand="Grid1_RowCommand">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="BtnCreat" Text="生成考核表" Icon="ChartPie"
                                    runat="server" OnClick="BtnCreat_Click" Hidden="true">
                                </f:Button>

                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:TemplateField ColumnID="tfNumber" HeaderText="序号"
                            Width="60px" HeaderTextAlign="Center" TextAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="labNumber" runat="server" Text=' <%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1%>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField HeaderText="姓名" ColumnID="UserName"
                            DataField="UserName" SortField="UserName" FieldType="String" HeaderTextAlign="Center"
                            Width="200px">
                        </f:RenderField>
                        <f:RenderField HeaderText="岗位" ColumnID="RoleName"
                            DataField="RoleName" SortField="RoleName" FieldType="String" HeaderTextAlign="Center"
                            Width="200px">
                        </f:RenderField>
                        <f:RenderField HeaderText="项目名称" ColumnID="ProjectName" DataField="ProjectName"
                            SortField="ProjectName" FieldType="String" HeaderTextAlign="Center" Width="350px">
                        </f:RenderField>
                        <f:RenderField HeaderText="开始时间" ColumnID="StartTime" DataField="StartTime"
                            SortField="StartTime" FieldType="Date" RendererArgument="yyyy-MM-dd" HeaderTextAlign="Center" Width="150px">
                        </f:RenderField>
                        <f:RenderField HeaderText="结束时间" ColumnID="EndTime"
                            DataField="EndTime" SortField="EndTime" FieldType="Date" HeaderTextAlign="Center"
                            Width="180px" RendererArgument="yyyy-MM-dd">
                        </f:RenderField>
                        <f:TemplateField Width="110px" ColumnID="UserId" HeaderText="考核状态" TextAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lbtnPro" runat="server" Text='<%# ConvertApprove(Eval("State")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:LinkButtonField ColumnID="export" HeaderText="导出" Width="60px" Icon="ArrowUp" CommandName="export" Hidden="true"/>
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
        <f:Window ID="Window1" Title="绩效考核" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="1000px"
            Height="1000px" OnClose="Window1_Close">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true" 
                runat="server" Text="编辑" Icon="Pencil">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDelete" ConfirmText="确定删除当前数据？" OnClick="btnMenuDelete_Click" EnablePostBack="true"
                Hidden="true" runat="server" Text="删除" Icon="Delete">
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

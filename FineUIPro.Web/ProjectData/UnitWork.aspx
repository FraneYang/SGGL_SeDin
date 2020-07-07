<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitWork.aspx.cs" Inherits="FineUIPro.Web.ProjectData.UnitWork" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>单位工程</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
       <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>

                <f:Grid ID="Grid1" ShowBorder="true" EnableAjax="false" ShowHeader="false" Title="单位工程设置" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="UnitWorkId" AllowCellEditing="true" ForceFit="true"
                    ClicksToEdit="2" DataIDField="UnitWorkId" AllowSorting="true" SortField="UnitWorkCode"
                    SortDirection="ASC" EnableColumnLines="true"
                    AllowPaging="true" IsDatabasePaging="true" PageSize="10"
                    EnableRowDoubleClickEvent="true" AllowFilters="true" EnableTextSelection="True" OnRowDoubleClick="Grid1_RowDoubleClick" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:TextBox ID="txtUnitWorkCode" runat="server" Label="单位工程编号" Width="250px" LabelWidth="110px"
                                    LabelAlign="Right">
                                </f:TextBox>
                                <f:TextBox ID="txtUnitWorkName" runat="server" Label="单位工程名称" Width="250px" LabelWidth="120px"
                                    LabelAlign="Right">
                                </f:TextBox>
                                <f:DropDownList ID="drpProjectType" runat="server" Label="所属工程" Width="210px" LabelAlign="Right" EnableEdit="true">
                                    <f:ListItem Text="建筑工程" Value="1" />
                                    <f:ListItem Text="安装工程" Value="2" />
                                </f:DropDownList>
                                <f:ToolbarFill runat="server"></f:ToolbarFill>
                                <f:Button ID="btnSearch" Icon="SystemSearch"
                                    EnablePostBack="true" runat="server" OnClick="btnSearch_Click">
                                </f:Button>
                                <f:Button ID="btnRset" OnClick="btnRset_Click" ToolTip="重置" Icon="ArrowUndo" EnablePostBack="true" runat="server">
                                </f:Button>
                                <f:Button ID="btnNew" Icon="Add" EnablePostBack="true" Hidden="true"
                                    runat="server">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:TemplateField ColumnID="tfPageIndex" Width="55px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField Width="180px" ColumnID="UnitWorkCode" DataField="UnitWorkCode"
                            SortField="UnitWorkCode" FieldType="String" HeaderText="单位工程编号" TextAlign="center"
                            HeaderTextAlign="Center">
                        </f:RenderField>

                        <f:RenderField Width="180px" ColumnID="UnitWorkName" DataField="UnitWorkName"
                            SortField="UnitWorkName" FieldType="String" HeaderText="单位工程名称" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="180px" ColumnID="ProjectType" DataField="ProjectType" SortField="ProjectType"
                            FieldType="String" HeaderText="所属工程" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField HeaderText="权重%" ColumnID="Weights" DataField="Weights"
                            SortField="Weights" HeaderTextAlign="Center" TextAlign="Center" Width="100px"
                            FieldType="String">
                        </f:RenderField>
                        <f:RenderField HeaderText="施工单位" ColumnID="UnitId" DataField="UnitId"
                            SortField="UnitId" HeaderTextAlign="Center" TextAlign="Center" Width="100px"
                            FieldType="String">
                        </f:RenderField>
                        <f:RenderField HeaderText="监理单位" ColumnID="SupervisorUnitId" DataField="SupervisorUnitId"
                            SortField="SupervisorUnitId" HeaderTextAlign="Center" TextAlign="Center" Width="100px"
                            FieldType="String">
                        </f:RenderField>
                        <f:RenderField HeaderText="检测单位" ColumnID="NDEUnit" DataField="NDEUnit"
                            SortField="NDEUnit" HeaderTextAlign="Center" TextAlign="Center" Width="100px"
                            FieldType="String">
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
                        <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                            <f:ListItem Text="10" Value="10" />
                            <f:ListItem Text="15" Value="15" />
                            <f:ListItem Text="20" Value="20" />
                            <f:ListItem Text="25" Value="25" />
                            <f:ListItem Text="所有行" Value="100000" />
                        </f:DropDownList>
                        <f:ToolbarFill runat="server">
                        </f:ToolbarFill>

                    </PageItems>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="单位工程" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
            Width="600px" Height="300px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <Items>
                <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Hidden="true" Text="修改" Icon="Pencil"
                    OnClick="btnMenuModify_Click">
                </f:MenuButton>
                <%--<f:MenuButton ID="btnMenuView" EnablePostBack="true" runat="server" Text="查看" Icon="ApplicationViewIcons" OnClick="btnMenuView_Click">
                </f:MenuButton>--%>
                <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server" Hidden="true" Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？"
                    OnClick="btnMenuDel_Click">
                </f:MenuButton>
            </Items>
        </f:Menu>
    </form>
    <script type="text/javascript">
        var menuID = '<%= Menu1.ClientID %>';

        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>
</body>
</html>

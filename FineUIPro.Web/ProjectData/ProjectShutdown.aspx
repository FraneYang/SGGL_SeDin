<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectShutdown.aspx.cs" Inherits="FineUIPro.Web.ProjectData.ProjectShutdown" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>项目状态</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="项目状态" EnableCollapse="true"
                runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="ProjectStateId"
                DataIDField="ProjectStateId" AllowSorting="true" ForceFit="true"
                SortField="ShutdownDate" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>                         
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
                    <f:RenderField Width="300px" ColumnID="ProjectName" DataField="ProjectName" 
                        SortField="ProjectName" FieldType="String" HeaderText="项目名称" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:TemplateField Width="90px" HeaderText="原状态" ColumnID="OldProjectState">
                        <ItemTemplate>                                       
                            <asp:Label ID="Label3" runat="server" Text='<%# GetProjectState(Eval("OldProjectState")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                        <f:TemplateField Width="90px" HeaderText="申请状态" ColumnID="ProjectState">
                        <ItemTemplate>                                       
                            <asp:Label ID="Label1" runat="server" Text='<%# GetProjectState(Eval("ProjectState")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="100px" ColumnID="CompileDate" DataField="CompileDate"
                        SortField="CompileDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                        HeaderText="申请日期" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                     <f:RenderField Width="100px" ColumnID="CompileManName" DataField="CompileManName"
                        SortField="CompileManName" FieldType="String" HeaderText="申请人" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="140px" ColumnID="ShutdownDate" DataField="ShutdownDate"
                        SortField="ShutdownDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                        HeaderText="状态生效日期" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="160px" ColumnID="FlowOperateName" DataField="FlowOperateName"
                        SortField="FlowOperateName" FieldType="String" HeaderText="状态" HeaderTextAlign="Center"
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
    <f:Window ID="Window1" Title="项目状态及软件关闭" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="900px" Height="450px">
    </f:Window>   
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="TableEdit" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除">
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmergencyList.aspx.cs" Inherits="FineUIPro.Web.HSSE.Emergency.EmergencyList" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>应急预案管理清单</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="应急预案管理清单" EnableCollapse="true"
                runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="EmergencyListId" ForceFit="true"
                DataIDField="EmergencyListId" AllowSorting="true" SortField="EmergencyCode" SortDirection="DESC"                 
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" OnSort="Grid1_Sort" 
                AllowPaging="true" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox runat="server" Label="编号" ID="txtEmergencyCode" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="90px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:DropDownList ID="drpUnitId" runat="server" Label="单位" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged"  LabelWidth="70px" Width="250px">
                            </f:DropDownList>
                            <f:TextBox runat="server" Label="文件名称" ID="txtEmergencyName" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="90px"
                                LabelAlign="right">
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
                    <f:RenderField Width="100px" ColumnID="EmergencyCode" DataField="EmergencyCode"
                        SortField="EmergencyCode" FieldType="String" HeaderText="编号" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>    
                    <f:RenderField Width="200px" ColumnID="EmergencyName" DataField="EmergencyName"
                        SortField="EmergencyName" FieldType="String" HeaderText="文件名称" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>  
                    <f:RenderField Width="100px" ColumnID="EmergencyTypeName" DataField="EmergencyTypeName"
                        SortField="EmergencyTypeName" FieldType="String" HeaderText="预案类型" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="75px" ColumnID="VersionCode" DataField="VersionCode" 
                        SortField="VersionCode" FieldType="String" HeaderText="版次" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="240px" ColumnID="UnitName" DataField="UnitName" 
                        SortField="UnitName" FieldType="String" HeaderText="单位名称" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="90px" ColumnID="CompileManName" DataField="CompileManName"
                        SortField="CompileManName" FieldType="String" HeaderText="整理人" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="110px" ColumnID="CompileDate" DataField="CompileDate"
                        SortField="CompileDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                        HeaderText="整理日期" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>                    
             <%--        <f:RenderField Width="90px" ColumnID="AuditMan" DataField="AuditMan"
                        SortField="AuditMan" FieldType="String" HeaderText="审核人" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>                    
                     <f:RenderField Width="90px" ColumnID="ApproveMan" DataField="ApproveMan"
                        SortField="ApproveMan" FieldType="String" HeaderText="批准人" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>--%>
                    <f:RenderField Width="150px" ColumnID="FlowOperateName" DataField="FlowOperateName"
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
    <f:Window ID="Window1" Title="应急预案管理清单" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="1100px" Height="600px">
    </f:Window>    
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="Pencil" EnablePostBack="true"
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

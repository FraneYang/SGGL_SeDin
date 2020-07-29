<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HazardList.aspx.cs" Inherits="FineUIPro.Web.HSSE.Hazard.HazardList" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>职业健康安全危险源辨识与评价</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="职业健康安全危险源辨识与评价" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="HazardListId" DataIDField="HazardListId" AllowSorting="true" SortField="CompileDate"
                SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" ForceFit="true"
                EnableColumnLines="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableTextSelection="True"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                            <f:TextBox runat="server" Label="编号" ID="txtHazardListCode" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" LabelWidth="50px" LabelAlign="right">
                            </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNew_Click"
                                Hidden="true">
                            </f:Button>
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                            <f:Button ID="btnHelp" runat="server" ToolTip="点击下载本页面使用说明" Text="帮助" Icon="Help">
                                <Listeners>
                                    <f:Listener Event="click" Handler="onToolSourceCodeClick" />
                                </Listeners>
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RenderField Width="100px" ColumnID="HazardListCode" DataField="HazardListCode"
                        SortField="HazardListCode" FieldType="String" HeaderText="清单编号" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:TemplateField ColumnID="tfVersionNo" Width="70px" HeaderText="版本号" HeaderTextAlign="Center"
                        TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblVersionNo" runat="server" Text='<%# Bind("VersionNo") %>' ToolTip='<%#Bind("VersionNo") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField ColumnID="tfWorkStage" Width="300px" HeaderText="工作阶段" HeaderTextAlign="Center"
                        TextAlign="Left" >
                        <ItemTemplate>
                            <asp:Label ID="lblWorkStage" runat="server" Text='<%# ConvertWorkStage(Eval("WorkStage")) %>'
                                ToolTip='<%# ConvertWorkStage(Eval("WorkStage")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="130px" ColumnID="WorkAreaName" DataField="WorkAreaName" 
                        SortField="WorkAreaName" FieldType="String" HeaderText="单位工程" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="IdentificationDate" DataField="IdentificationDate"
                        SortField="IdentificationDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                        HeaderText="辨识时间" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="ControllingPersonName" DataField="ControllingPersonName"
                        SortField="ControllingPersonName" FieldType="String" HeaderText="控制责任人" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="UserName" DataField="UserName" SortField="UserName"
                        FieldType="String" HeaderText="编制人" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="CompileDate" DataField="CompileDate" SortField="CompileDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="编制日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="FlowOperateName" DataField="FlowOperateName"
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
    <f:Window ID="Window1" Title="职业健康安全危险源辨识与评价" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1200px" Height="800px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuAllOut" EnablePostBack="true" runat="server" Text="导出危险源"
            Icon="FolderUp" Hidden="true">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuImportantOut" EnablePostBack="true" runat="server" Text="导出重大危险源"
            Icon="FolderPage" Hidden="true">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Hidden="true"
            Text="修改" Icon="Pencil" OnClick="btnMenuModify_Click">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server" Hidden="true"
            Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？" OnClick="btnMenuDel_Click">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuAllPrint" EnablePostBack="true" runat="server" Hidden="true"
            Text="打印危险源" Icon="Printer" OnClick="btnMenuAllPrint_Click">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuImportantPrint" EnablePostBack="true" runat="server" Hidden="true"
            Text="打印重大危险源" Icon="Printer" OnClick="btnMenuImportantPrint_Click">
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

        // 点击标题栏工具图标 - 查看源代码
        function onToolSourceCodeClick(event) {
            window.open('../Doc/危险源选择及评价方法.doc', '_blank');
        }
    </script>
</body>
</html>

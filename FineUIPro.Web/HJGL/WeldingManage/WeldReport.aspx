<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeldReport.aspx.cs" Inherits="FineUIPro.Web.HJGL.WeldingManage.WeldReport" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊接日报</title>
    <meta name="sourcefiles" content="~/HJGL/WeldingManage/GetWdldingDailyItem.ashx" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="220px" Title="焊接日报"
                ShowBorder="true" Layout="VBox" ShowHeader="true" AutoScroll="true" BodyPadding="5px"
                IconFont="ArrowCircleLeft">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:DatePicker ID="txtMonth" runat="server" Label="月份" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="Tree_TextChanged" Width="220px" LabelWidth="50px" DisplayType="Month" DateFormatString="yyyy-MM">
                            </f:DatePicker>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                    <f:Tree ID="tvControlItem" ShowHeader="false" Height="500px" Title="焊接日报"
                        OnNodeCommand="tvControlItem_NodeCommand" runat="server" ShowBorder="false" EnableCollapse="true"
                        EnableSingleClickExpand="true" AutoLeafIdentification="true" EnableSingleExpand="true"
                        EnableTextSelection="true">
                    </f:Tree>

                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="焊接日报"
                TitleToolTip="焊接日报" AutoScroll="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar3" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="true"
                                runat="server" OnClick="btnNew_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>
                            <f:DatePicker ID="txtWeldingDate" Label="焊接日期" runat="server"
                                DateFormatString="yyyy-MM-dd" LabelWidth="90px" Width="210px" LabelAlign="Right">
                            </f:DatePicker>
                             <f:TextBox ID="txtWeldingDailyCode" runat="server" Label="日报编号"
                                EmptyText="日报编号查询" Width="220px" LabelWidth="100px" LabelAlign="Right">
                            </f:TextBox>
                            <f:TextBox ID="txtPipelineCode" runat="server" Label="管线号"
                                EmptyText="输入查询条件" Width="220px" LabelWidth="80px" LabelAlign="Right">
                            </f:TextBox>
                            <f:TextBox ID="txtWeldJointCode" runat="server" Label="焊口号"
                                EmptyText="输入查询条件" Width="180px" LabelWidth="80px" LabelAlign="Right">
                            </f:TextBox>
                            <f:Button ID="btnQuery" ToolTip="查询" Icon="SystemSearch"
                                EnablePostBack="true" OnClick="btnQuery_Click" runat="server">
                            </f:Button>
                            <f:ToolbarFill ID="ToolbarFill2" runat="server">
                            </f:ToolbarFill>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="焊接日报"
                        EnableCollapse="true" runat="server" BoxFlex="1" DataKeyNames="WeldingDailyId"
                        AllowCellEditing="true" AllowColumnLocking="true" EnableColumnLines="true" ClicksToEdit="2"
                        DataIDField="WeldingDailyId" AllowSorting="true" SortField="WeldingDailyCode"
                        SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                        PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableTextSelection="True"
                        EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">
                        <Columns>
                            <f:RenderField Width="120px" ColumnID="WeldingDailyCode" DataField="WeldingDailyCode"
                                FieldType="String" HeaderText="焊接日报编号" HeaderTextAlign="Center"
                                TextAlign="Left" SortField="WeldingDailyCode">
                            </f:RenderField>
                             <f:RenderField Width="200px" ColumnID="UnitName" DataField="UnitName" FieldType="String"
                                HeaderText="单位名称" HeaderTextAlign="Center" TextAlign="Left" SortField="UnitName">
                            </f:RenderField>
                             <f:RenderField Width="150px" ColumnID="InstallationCode" DataField="InstallationCode" FieldType="String"
                                HeaderText="装置号" HeaderTextAlign="Center" TextAlign="Left" SortField="InstallationCode">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="WeldingDate" DataField="WeldingDate" SortField="WeldingDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="焊接日期"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="UserName" DataField="UserName" FieldType="String"
                                HeaderText="填报人" HeaderTextAlign="Center" TextAlign="Left" SortField="UserName">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="TableDate" DataField="TableDate" SortField="TableDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="填报日期"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="Remark" DataField="Remark" FieldType="String"
                                HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left"
                                ExpandUnusedSpace="true">
                            </f:RenderField>
                            <f:TemplateField ColumnID="expander" RenderAsRowExpander="true">
                                <ItemTemplate>
                                </ItemTemplate>
                            </f:TemplateField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                            <f:Listener Event="rowexpanderexpand" Handler="onRowExpanderExpand" />
                            <f:Listener Event="rowexpandercollapse" Handler="onRowExpanderCollapse" />
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
                                <f:ListItem Text="所有行" Value="10000" />
                            </f:DropDownList>
                        </PageItems>
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true"
        EnableMaximize="true" Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close"
        IsModal="true" Width="1200px" Height="650px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="Pencil" EnablePostBack="true"
            runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Top"
            runat="server" Text="删除">
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

        var grid1 = '<%= Grid1.ClientID %>';
        function onRowExpanderExpand(event, rowId) {

            var grid = this, rowEl = grid.getRowEl(rowId), rowData = grid.getRowData(rowId);

            var tplEl = rowEl.find('.f-grid-rowexpander-details .f-grid-tpl');
            if (!tplEl.text()) {

                F.create({
                    type: 'grid',
                    renderTo: tplEl,
                    header: false,
                    columnMenu: false,
                    columnResizing: false,
                    cls: 'gridinrowexpander',
                    fields: ['Num', 'PipelineCode', 'WeldJointCode', 'CoverWelderCode', 'BackingWelderCode', 'WeldTypeCode', 'WeldingLocationCode', 'JointAttribute', 'Size', 'Dia', 'Thickness', 'WeldingMethodCode'],
                    columns: [{
                        text: '序号', field: 'Num', width: 50
                    }, {
                        text: '管线编号', field: 'PipelineCode', width: 240
                    }, {
                        text: '焊口号', field: 'WeldJointCode', width: 70
                    }, {
                        text: '盖面焊工', field: 'CoverWelderCode', width: 80
                    }, {
                        text: '打底焊工', field: 'BackingWelderCode', width: 80
                    }, {
                        text: '焊缝类型', field: 'WeldTypeCode', width: 80
                    },{
                        text: '焊接位置', field: 'WeldingLocationCode', width: 80
                    }, {
                        text: '焊口属性', field: 'JointAttribute', width: 80
                    }, {
                        text: '达因', field: 'Size', width: 60
                    }, {
                        text: '外径', field: 'Dia', width: 60
                    }, {
                        text: '壁厚', field: 'Thickness', width: 60
                    }, {
                        text: '焊接方法', field: 'WeldingMethodCode', width: 120
                    }],
                    dataUrl: 'GetWdldingDailyItem.ashx?WeldingDailyId=' + rowId, // 这里可传递行中任意数据（rowData）
                    listeners: {
                        dataload: function () {
                            rowExpandersDoLayout();
                        }
                    }
                });
            }
        }

        function onRowExpanderCollapse(event, rowId) {
            rowExpandersDoLayout();
        }

        // 重新布局表格和行扩展列中的表格（解决出现纵向滚动条时的布局问题）
        function rowExpandersDoLayout() {
            var grid1Cmp = F(grid1);

            grid1Cmp.doLayout();

            $('.f-grid-row:not(.f-grid-rowexpander-collapsed) .gridinrowexpander').each(function () {
                var gridInside = F($(this).attr('id'));
                gridInside.doLayout();
            });
        }
    </script>
</body>
</html>

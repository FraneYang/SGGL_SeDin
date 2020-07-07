<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NDTBatch.aspx.cs" Inherits="FineUIPro.Web.HJGL.NDT.NDTBatch" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>检测单</title>
    <style>
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="280px" Title="检测单" ShowBorder="true"
                Layout="VBox" ShowHeader="false" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:DatePicker ID="txtNDEDateMonth" runat="server" Label="月份"
                                EmptyText="输入查询条件" AutoPostBack="true" OnTextChanged="Tree_TextChanged"
                                Width="265px" LabelWidth="100px" DisplayType="Month" DateFormatString="yyyy-MM" LabelAlign="Right">
                            </f:DatePicker>
                        </Items>
                    </f:Toolbar>
                    <f:Toolbar ID="Toolbar5" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox ID="txtSearchCode" runat="server" EmptyText="输入查询条件"
                                AutoPostBack="true" Label="检测单号" LabelWidth="100px"
                                OnTextChanged="Tree_TextChanged" LabelAlign="Right">
                            </f:TextBox>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                    <f:Tree ID="tvControlItem" ShowHeader="false" Title="检测单节点树" OnNodeCommand="tvControlItem_NodeCommand"
                        Height="500px" runat="server" ShowBorder="false" EnableCollapse="true" EnableSingleClickExpand="true"
                        AutoLeafIdentification="true" EnableSingleExpand="true" EnableTextSelection="true" OnNodeExpand="tvControlItem_TreeNodeExpanded">
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="检测单"
                TitleToolTip="检测单" AutoScroll="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:HiddenField runat="server" ID="hdNDEID">
                            </f:HiddenField>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" Text="新增" ToolTip="新增"
                                Icon="Add" runat="server" OnClick="btnNew_Click">
                            </f:Button>
                            <f:Button ID="btnImport" Text="导入" ToolTip="导入" Icon="ApplicationGet" runat="server"
                                        OnClick="btnImport_Click" Hidden="true">
                                    </f:Button>
                            <f:Button ID="btnEdit" Text="编辑" ToolTip="编辑"
                                Icon="TableEdit" runat="server" OnClick="btnEdit_Click">
                            </f:Button>
                            <f:Button ID="btnAudit" Text="审核" ToolTip="审核" Icon="TableKey" runat="server"
                                OnClick="btnAudit_Click">
                            </f:Button>
                            <f:Button ID="btnDelete" Text="删除" ToolTip="删除"
                                ConfirmText="确认删除此检测单？" ConfirmTarget="Top" Icon="Delete"
                                runat="server" OnClick="btnDelete_Click">
                            </f:Button>
                            <f:Button ID="BtnRepairRecord" Text="生成返修通知单" ToolTip="选择检测不合格的焊口生成返修通知单"  runat="server"
                                OnClick="BtnRepairRecord_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                    <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" BodyPadding="2px"
                        runat="server" RedStarPosition="BeforeText" LabelAlign="Left">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                     <f:Label ID="txtTrustBatchCode" Label="委托单号" runat="server"
                                        LabelWidth="130px" LabelAlign="Right">
                                    </f:Label>
                                    <f:Label ID="txtUnitName" Label="单位名称" runat="server" LabelWidth="130px"
                                        LabelAlign="Right">
                                    </f:Label>
                                    <f:Label ID="txtUnitWork" Label="单位工程编号" runat="server"
                                        LabelWidth="130px" LabelAlign="Right">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="txtNDEDate" Label="检测日期" runat="server" LabelWidth="130px"
                                        LabelAlign="Right">
                                    </f:Label>
                                    <f:Label ID="txtCheckUnit" Label="检测单位" runat="server"
                                        LabelWidth="130px" LabelAlign="Right">
                                    </f:Label>
                                    <f:Label ID="txtDetectionTypeCode" Label="检测方法" runat="server"
                                        LabelWidth="130px" LabelAlign="Right">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="检测单明细" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="NDEItemID" AllowCellEditing="true" AllowColumnLocking="true"
                        EnableColumnLines="true" ClicksToEdit="2" DataIDField="NDEItemID" AllowSorting="true"
                        SortField="PipelineCode,WeldJointCode" SortDirection="ASC" OnSort="Grid1_Sort" OnRowCommand="Grid1_RowCommand"
                        AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                        EnableTextSelection="True">
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号"
                                Width="50px" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:RenderField HeaderText="管线号" ColumnID="PipelineCode"
                                DataField="PipelineCode" SortField="PipelineCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="180px">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊口号" ColumnID="WeldJointCode"
                                DataField="WeldJointCode" SortField="WeldJointCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="单位工程" ColumnID="UnitWorkCode" DataField="UnitWorkCode"
                                SortField="UnitWorkCode" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊工号" ColumnID="WelderCode"
                                DataField="WelderCode" SortField="WelderCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="检测日期" ColumnID="FilmDate" DataField="FilmDate"
                                SortField="FilmDate" FieldType="Date" Renderer="Date" HeaderTextAlign="Center"
                                TextAlign="Left" Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="报告日期" ColumnID="ReportDate"
                                DataField="ReportDate" SortField="ReportDate" FieldType="Date" Renderer="Date"
                                HeaderTextAlign="Center" TextAlign="Left" Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="检测总数" ColumnID="TotalFilm" DataField="TotalFilm"
                                SortField="TotalFilm" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="合格数" ColumnID="PassFilm" DataField="PassFilm"
                                SortField="PassFilm" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="是否合格" ColumnID="CheckResultStr"
                                DataField="CheckResultStr" SortField="CheckResultStr" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="评定级别" ColumnID="JudgeGrade"
                                DataField="JudgeGrade" SortField="JudgeGrade" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="100px">
                            </f:RenderField>
                            <f:TemplateField Width="150px" HeaderText="缺陷" HeaderTextAlign="Center"
                                TextAlign="Center" SortField="CheckDefects">
                                <ItemTemplate>
                                    <asp:Label ID="lbCheckDefects" runat="server" Text='<%# ConvertCheckDefects(Eval("CheckDefects")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField HeaderText="返修位置" ColumnID="RepairLocation"
                                DataField="RepairLocation" SortField="RepairLocation" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="探伤报告编号" ColumnID="NDEReportNo"
                                DataField="NDEReportNo" SortField="NDEReportNo" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="240px">
                            </f:RenderField>
                            <f:RenderField HeaderText="备注" ColumnID="Remark" DataField="Remark"
                                SortField="Remark" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="100px">
                            </f:RenderField>
                            <f:LinkButtonField Width="100px" HeaderText="取消审核" ConfirmTarget="Top"
                                CommandName="CancelAudit" TextAlign="Center" Text="取消审核"
                                ToolTip="取消审核" />
                        </Columns>
                        <Listeners>
                            <f:Listener Event="dataload" Handler="onGridDataLoad" />
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
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true"
        EnableMaximize="true" Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close"
        IsModal="true" Width="1200px" Height="660px">
    </f:Window>
    <f:Window ID="Window2" Title="弹出窗体" Hidden="true" EnableIFrame="true"
        EnableMaximize="true" Target="Parent" EnableResize="true" runat="server" IsModal="false" OnClose="Window2_Close" 
        CloseAction="HidePostBack" Width="1024px" Height="640px">
    </f:Window>
    <f:Window ID="WindowRepair" Title="弹出窗体" Hidden="true"
        EnableIFrame="true" EnableMaximize="true" Target="Top" EnableResize="true" runat="server"
        IsModal="true" Width="1280px" Height="800px">
    </f:Window>
    </form>
    <script type="text/javascript">
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            // F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
        function onGridDataLoad(event) {
            this.mergeColumns(['PipelineCode']);
        }
    </script>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HardReport.aspx.cs" Inherits="FineUIPro.Web.HJGL.HotProcessHard.HardReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>硬度报告</title>
    <style>
        .f-grid-row .f-grid-cell-inner {
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
                    EnableCollapse="true" Width="280px" Title="硬度报告"
                    ShowBorder="true" Layout="VBox" ShowHeader="false" AutoScroll="true" BodyPadding="5px"
                    IconFont="ArrowCircleLeft">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:TextBox ID="txtSearchNo" runat="server" EmptyText="输入查询条件"
                                    AutoPostBack="true" Label="委托单号" LabelWidth="100px"
                                    OnTextChanged="Tree_TextChanged" Width="260px" LabelAlign="Right">
                                </f:TextBox>
                                <f:HiddenField runat="server" ID="hdHardTrustID">
                                </f:HiddenField>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>
                        <f:Tree ID="tvControlItem" ShowHeader="false" Title="硬度委托节点树" OnNodeCommand="tvControlItem_NodeCommand"
                            Height="470px" runat="server" ShowBorder="false" EnableCollapse="true"
                            AutoLeafIdentification="true" EnableSingleExpand="true" EnableTextSelection="true">
                            <Listeners>
                                <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                            </Listeners>
                        </f:Tree>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                    Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="硬度委托"
                    TitleToolTip="硬度委托" AutoScroll="true">
                    <Items>
                        <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                            runat="server" RedStarPosition="BeforeText" LabelAlign="Left">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                        <f:Label ID="txtInspectionNum" Label="报告编号" runat="server"
                                            LabelWidth="180px" LabelAlign="Right">
                                        </f:Label>
                                        <f:Label ID="txtHardnessMethod" Label="检测方法" runat="server"
                                            LabelWidth="180px" LabelAlign="Right">
                                        </f:Label>
                                        <f:Label ID="txtEquipmentModel" Label="设备型号" runat="server"
                                            LabelWidth="180px" LabelAlign="Right">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="硬度委托明细" EnableCollapse="true"
                            runat="server" BoxFlex="1" DataKeyNames="HardTrustItemID" AllowCellEditing="true"
                            AllowColumnLocking="true" EnableColumnLines="true" ClicksToEdit="2" DataIDField="HardTrustItemID"
                            AllowSorting="true" SortField="WeldJointCode" SortDirection="ASC" OnSort="Grid1_Sort"
                            AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                            EnableTextSelection="True" ForceFit="true">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar3" Position="Top" runat="server" ToolbarAlign="Right">
                                    <Items>
                                        <f:TextBox ID="txtPipelineCode" runat="server" Label="管线号"
                                            EmptyText="输入查询条件" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"
                                            Width="290px" LabelWidth="120px" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:TextBox ID="txtWeldJointCode" runat="server" Label="总焊口量"
                                            EmptyText="输入查询条件" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"
                                            Width="360px" LabelWidth="160px" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                        </f:ToolbarFill>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
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
                                <f:RenderField HeaderText="焊工号" ColumnID="WelderCode"
                                    DataField="WelderCode" SortField="WelderCode" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="规格" ColumnID="Specification"
                                    DataField="Specification" SortField="Specification" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="材质" ColumnID="MaterialCode"
                                    DataField="MaterialCode" SortField="MaterialCode" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="90px">
                                </f:RenderField>

                                <f:RenderField HeaderText="检测结果" ColumnID="checkResult" DataField="checkResult"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="80px">
                                </f:RenderField>
                                <f:WindowField HeaderTextAlign="Center" TextAlign="Center" Width="90px" WindowID="WindowHardReport"
                                    DataIFrameUrlFields="HardTrustItemID" DataIFrameUrlFormatString="HardReportItem.aspx?HardTrustItemID={0}"
                                    Text="硬度报告" HeaderText="硬度报告">
                                </f:WindowField>
                                <f:RenderField HeaderText="备注" ColumnID="Remark" DataField="Remark"
                                    SortField="Remark" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                    Width="90px" ExpandUnusedSpace="true">
                                </f:RenderField>
                            </Columns>
                            <Listeners>
                                <f:Listener Event="dataload" Handler="onGridDataLoad" />
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
            IsModal="true" Width="1090px" Height="660px">
        </f:Window>
        <f:Window ID="Window2" Title="打印" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Top" EnableResize="true" runat="server" IsModal="true"
            Width="1024px" Height="640px">
        </f:Window>
        <f:Window ID="WindowHardReport" Title="硬度报告" Hidden="true"
            EnableIFrame="true" EnableMaximize="true" Target="Top" EnableResize="true" runat="server"
            IsModal="true" Width="1000px" Height="520px" OnClose="WindowHardReport_Close">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Text="编辑" Icon="Pencil" OnClick="btnMenuModify_Click">
            </f:MenuButton>
        </f:Menu>
    </form>
    <script type="text/javascript">
        var menuID2 = '<%= Menu1.ClientID %>';
        function onTreeNodeContextMenu(event, rowId) {
            F(menuID2).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
        function onGridDataLoad(event) {
            this.mergeColumns(['PipelineCode']);
            this.mergeColumns(['WeldJointCode', 'STE_Code', 'JOT_JointDesc'], {
                depends: true
            });
        }
    </script>
</body>
</html>

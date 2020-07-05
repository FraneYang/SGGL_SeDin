<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepairAndExpand.aspx.cs" Inherits="FineUIPro.Web.HJGL.RepairAndExpand.RepairAndExpand" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>返修扩透</title>
    <style>
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
         .colorredRed {
            font-weight: bold;
            color: red;
        }
        .colorredBlue {
            font-weight: bold;
            color: blue;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="290px" Title="检测单" ShowBorder="true"
                Layout="VBox" ShowHeader="false" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:DatePicker ID="txtRepairMonth" runat="server" Label="月份"
                                EmptyText="输入查询条件" AutoPostBack="true" OnTextChanged="Tree_TextChanged"
                                Width="265px" LabelWidth="100px" DisplayType="Month" DateFormatString="yyyy-MM" LabelAlign="Right">
                            </f:DatePicker>
                        </Items>
                    </f:Toolbar>
                    <f:Toolbar ID="Toolbar5" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox ID="txtSearchCode" runat="server" EmptyText="输入查询条件"
                                AutoPostBack="true" Label="返修单" LabelWidth="100px"
                                OnTextChanged="Tree_TextChanged" Width="265px" LabelAlign="Right">
                            </f:TextBox>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                    <f:Tree ID="tvControlItem" ShowHeader="false" Title="返修单节点树" OnNodeCommand="tvControlItem_NodeCommand"
                        Height="470px" runat="server" ShowBorder="false" EnableCollapse="true" EnableSingleClickExpand="true"
                        AutoLeafIdentification="true" EnableSingleExpand="true" EnableTextSelection="true">
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
                            <f:Button ID="btnSave" Text="保存" ToolTip="保存"
                                Icon="SystemSave" runat="server" OnClick="btnSave_Click">
                            </f:Button>
                            <f:Button ID="btnPointAudit" Text="扩透口审核" ToolTip="监理审核后才可生成委托单" Icon="ArrowNsew" runat="server"
                                OnClick="btnPointAudit_Click">
                            </f:Button>
                            <f:Button ID="btnGenerate" Text="生成" ToolTip="生成委托单" Icon="Pencil" runat="server"
                                       OnClick="btnGenerate_Click" >
                            </f:Button>
                            <f:Button ID="btnSee"  Text="查看底片"  OnClick="btnSee_Click" runat="server"></f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                    <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" BodyPadding="2px"
                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="txtPipeCode" Label="管线号" runat="server" LabelWidth="90px">
                                    </f:Label>
                                    <f:Label ID="txtWeldJointCode" Label="焊口号" runat="server" LabelWidth="90px">
                                    </f:Label>
                                    <f:Label ID="txtRepairLocation" Label="底片号" runat="server" LabelWidth="90px">
                                    </f:Label> 
                                    <f:Label ID="txtWelder" Label="施焊焊工" runat="server" LabelWidth="90px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="txtJudgeGrade" Label="合格等级" runat="server" LabelWidth="90px">
                                    </f:Label>
                                    <f:Label ID="txtCheckDefects" Label="缺陷" runat="server" LabelWidth="90px">
                                    </f:Label> 
                                    <f:DropDownList ID="drpRepairWelder" runat="server" Label="返修焊工" LabelWidth="90px" ></f:DropDownList>
                                    <f:DatePicker ID="txtRepairDate" Label="返修日期" runat="server"
                                        DateFormatString="yyyy-MM-dd" LabelWidth="90px" LabelAlign="Right">
                                    </f:DatePicker>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:CheckBox ID="ckbIsCut" runat="server" Label="是否切除" LabelAlign="Right"   AutoPostBack="true" ></f:CheckBox>
                                    <f:Label ID="lbIsAudit" Label="是否审核" runat="server"></f:Label> 
                                    <f:Label runat="server"></f:Label>
                                    <f:Label runat="server"></f:Label>
                                   
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="点口管理" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="PointBatchItemId" AllowCellEditing="true"
                        AllowColumnLocking="true" EnableColumnLines="true" ClicksToEdit="2" DataIDField="PointBatchItemId"
                        AllowSorting="true" SortField="PipelineCode,WeldJointCode" SortDirection="ASC"
                        OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" PageSize="100"
                        OnPageIndexChange="Grid1_PageIndexChange" EnableTextSelection="True" 
                        KeepCurrentSelection="true" EnableCheckBoxSelect="true">
                        <Toolbars>
                           <f:Toolbar ID="Toolbar3" Position="Top" runat="server">
                              <Items>
                                 <f:Label runat="server" Text="选择扩透口："></f:Label>
                                <f:CheckBox ID="ckbWelder" runat="server" Label="同焊工" LabelAlign="Right"  Checked="true"  AutoPostBack="true"  LabelWidth="70px" OnCheckedChanged="ckbWelder_CheckedChanged"></f:CheckBox>
                                <f:CheckBox ID="ckbPipe" runat="server" Label="同管线" LabelAlign="Right" Checked="true" LabelWidth="70px" AutoPostBack="true" OnCheckedChanged="ckbPipe_CheckedChanged"></f:CheckBox>
                                <f:CheckBox ID="ckbdaily" runat="server" Label="同一天" LabelAlign="Right" Checked="true" LabelWidth="70px" AutoPostBack="true" OnCheckedChanged="ckbDaily_CheckedChanged"></f:CheckBox>
                                <f:CheckBox ID="ckbRepairBefore" runat="server" Label="返修前所焊" LabelAlign="Right" LabelWidth="100px"  AutoPostBack="true" OnCheckedChanged="ckbRepairBefore_CheckedChanged"></f:CheckBox>
                                <f:CheckBox ID="ckbMat" runat="server" Label="同材质" LabelAlign="Right" AutoPostBack="true" LabelWidth="70px" OnCheckedChanged="ckbMat_CheckedChanged"></f:CheckBox>
                                <f:CheckBox ID="ckbSpec" runat="server" Label="同规格" LabelAlign="Right" AutoPostBack="true"  LabelWidth="70px" OnCheckedChanged="ckbSpec_CheckedChanged"></f:CheckBox>
                              </Items>
                           </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号"
                                Width="45px" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:RenderField HeaderText="批号" ColumnID="PointBatchCode" DataField="PointBatchCode"
                                SortField="PointBatchCode" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="200px" Locked="true">
                            </f:RenderField>
                            <f:RenderField HeaderText="管线号" ColumnID="PipelineCode" DataField="PipelineCode"
                                SortField="PipelineCode" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="180px" Locked="true">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊口代号" ColumnID="WeldJointCode" DataField="WeldJointCode"
                                SortField="WeldJointCode" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="80px" Locked="true">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊接日期" ColumnID="WeldingDate" DataField="WeldingDate" SortField="WeldingDate"
                                FieldType="Date" Renderer="Date" HeaderTextAlign="Center" TextAlign="Left" Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊接区域" ColumnID="JointArea" DataField="JointArea" SortField="JointArea"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Center" Width="80px">
                            </f:RenderField>
                            <f:RenderField HeaderText="点口类型" ColumnID="PointState" DataField="PointState" SortField="PointState"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Center" Width="80px">
                            </f:RenderField>
                            <f:RenderField HeaderText="点口日期" ColumnID="PointDate" DataField="PointDate" SortField="PointDate"
                                FieldType="Date" Renderer="Date" HeaderTextAlign="Center" TextAlign="Left" Width="100px">
                            </f:RenderField>
                           
                            <f:RenderField HeaderText="实际寸径" ColumnID="Size" DataField="Size" SortField="Size"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">
                            </f:RenderField>
                            <f:RenderField HeaderText="管道等级" ColumnID="PipingClassName" DataField="PipingClassName"
                                SortField="PipingClassName" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="80px">
                            </f:RenderField>
                            <f:RenderField HeaderText="备注" ColumnID="Remark" DataField="Remark" SortField="Remark"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="60px">
                            </f:RenderField>
                             <f:RenderField HeaderText="点口审核" ColumnID="PointIsAudit" DataField="PointIsAudit"
                                SortField="PointIsAudit" FieldType="String" HeaderTextAlign="Center" TextAlign="Center"
                                Width="90px">
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
        EnableMaximize="true" Target="Top" EnableResize="true" runat="server" 
        IsModal="true" Width="1200px" Height="660px">
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PointBatch.aspx.cs" Inherits="FineUIPro.Web.HJGL.PointTrust.PointBatch" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>点口管理</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
     <style>
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
                EnableCollapse="true" Width="280px" Title="点口管理" ShowBorder="true" Layout="VBox"
                ShowHeader="false" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Toolbars>
                    <f:Toolbar ID="Toolbar4" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox ID="txtWelderCode" runat="server" EmptyText="输入查询条件"
                                AutoPostBack="true" Label="焊工号" LabelWidth="90px"
                                OnTextChanged="Tree_TextChanged" Width="300px" LabelAlign="Right">
                            </f:TextBox>
                        </Items>
                    </f:Toolbar>
                    <f:Toolbar ID="Toolbar5" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:DatePicker runat="server" DateFormatString="yyyy-MM" Label="批开始日期" ID="txtStartTime"
                                DisplayType="Month" ShowTodayButton="false" AutoPostBack="true" 
                                OnTextChanged="Tree_TextChanged"  LabelWidth="90px" LabelAlign="Right" Width="300px">
                            </f:DatePicker>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                    <f:Tree ID="tvControlItem" ShowHeader="false" Title="点口管理节点树" OnNodeCommand="tvControlItem_NodeCommand"
                        Height="500px" runat="server" ShowBorder="false" EnableCollapse="true" EnableSingleClickExpand="true"
                        EnableSingleExpand="true" AutoLeafIdentification="true" 
                        EnableTextSelection="true" OnNodeExpand="tvControlItem_TreeNodeExpanded">
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="点口管理"
                TitleToolTip="点口管理" AutoScroll="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar3" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>  
                            <f:Button ID="btnAutoPoint" Text="自动点口" ToolTip="生成" Icon="cmy" runat="server"
                                OnClick="btnAutoPoint_Click">
                            </f:Button>
                            <f:Button ID="btnbtnOpenResetPoint" Text="点口调整" ToolTip="点口调整" Icon="ArrowUndo"
                                runat="server" OnClick="btnbtnOpenResetPoint_Click">
                            </f:Button> 
                            <f:Button ID="btnPointAudit" Text="生成委托单" ToolTip="监理点口审核并生成委托单" Icon="ArrowNsew" runat="server"
                                OnClick="btnPointAudit_Click" Hidden="true">
                            </f:Button> 
                            <f:Button ID="btnGenerate" Text="生成" ToolTip="生成委托单" Icon="TableEdit" runat="server"
                                OnClick="btnGenerate_Click"  Hidden="true">
                            </f:Button>
                            <f:Button ID="btnSelectExpandPoint" Text="重新选择扩口" ToolTip="重新选择扩口" Icon="ArrowRefresh"
                                runat="server" OnClick="btnSelectExpandPoint_Click" Hidden="true">
                            </f:Button>
                            <f:Button ID="btnPrint" Text="打印" ToolTip="打印" Icon="Printer" runat="server" OnClick="btnPrint_Click"  Hidden="true">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                  
                </Toolbars>
                <Items>
                    <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" BodyPadding="2px"
                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="txtStartDate" Label="批次开始时间" runat="server" LabelAlign="Right" LabelWidth="110px">
                                    </f:Label>
                                    <f:Label ID="txtEndDate" Label="批次关闭日期" runat="server" LabelAlign="Right" LabelWidth="110px">
                                    </f:Label>
                                    <f:Label ID="txtState" Label="关闭状态" runat="server" LabelAlign="Right" LabelWidth="110px">
                                    </f:Label>
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
                        OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" PageSize="20"
                        OnPageIndexChange="Grid1_PageIndexChange" EnableTextSelection="True" Height="500px">
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号"
                                Width="45px" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:RenderField HeaderText="管线号" ColumnID="PipelineCode" DataField="PipelineCode"
                                SortField="PipelineCode" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="220px" Locked="true">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊口代号" ColumnID="WeldJointCode" DataField="WeldJointCode"
                                SortField="WeldJointCode" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="80px" Locked="true">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊接日期" ColumnID="WeldingDate" DataField="WeldingDate" SortField="WeldingDate"
                                FieldType="Date" Renderer="Date" HeaderTextAlign="Center" TextAlign="Left" Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊口属性" ColumnID="JointAttribute" DataField="JointAttribute" SortField="JointAttribute"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Center" Width="80px">
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
                            <f:RenderField HeaderText="返修日期" ColumnID="RepairDate" DataField="RepairDate" SortField="RepairDate"
                                FieldType="Date" Renderer="Date" HeaderTextAlign="Center" TextAlign="Left" Width="100px" Hidden="true">
                            </f:RenderField>
                            <f:RenderField HeaderText="切除日期" ColumnID="CutDate" DataField="CutDate" SortField="CutDate"
                                FieldType="Date" Renderer="Date" HeaderTextAlign="Center" TextAlign="Left" Width="100px" Hidden="true">
                            </f:RenderField>
                            <f:RenderField HeaderText="实际寸径" ColumnID="Size" DataField="Size" SortField="Size"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">
                            </f:RenderField>
                            <f:RenderField HeaderText="管道等级" ColumnID="PipingClassName" DataField="PipingClassName"
                                SortField="PipingClassName" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="80px">
                            </f:RenderField>
                            <f:RenderField HeaderText="是否焊工首件" ColumnID="IsWelderFirst" DataField="IsWelderFirst"
                                SortField="IsWelderFirst" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="80px">
                            </f:RenderField>
                             <f:RenderField HeaderText="是否委托" ColumnID="IsBuildTrust" DataField="IsBuildTrust"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Center"
                                Width="90px">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊缝类型" ColumnID="WeldTypeCode" DataField="WeldTypeCode"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Center"
                                Width="90px">
                            </f:RenderField>
                            <f:RenderField HeaderText="打底焊工" ColumnID="BackingWelderCode" DataField="BackingWelderCode"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Center"
                                Width="90px">
                            </f:RenderField>
                            <f:RenderField HeaderText="盖面焊工" ColumnID="CoverWelderCode" DataField="CoverWelderCode"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Center"
                                Width="90px">
                            </f:RenderField>
                             <f:RenderField HeaderText="备注" ColumnID="Remark" DataField="Remark" SortField="Remark"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="60px">
                            </f:RenderField>
                        </Columns>
                        
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
        EnableMaximize="true" Target="Top" EnableResize="false" runat="server" OnClose="Window1_Close"
        IsModal="true" Width="1200px" Height="680px">
    </f:Window>
   
    </form>
    <script>
       
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>

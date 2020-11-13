<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PipelineList.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.WeldingManage.PipelineList" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>管线信息</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
            <Items>
                <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                    EnableCollapse="true" Width="300px" Title="管线信息"
                    ShowBorder="true" Layout="VBox" ShowHeader="true" AutoScroll="true" BodyPadding="5px"
                    IconFont="ArrowCircleLeft">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:TextBox ID="txtWorkArea" runat="server" Label="区域" EmptyText="输入查询条件"
                                    AutoPostBack="true" OnTextChanged="Tree_TextChanged" Width="280px" LabelWidth="50px">
                                </f:TextBox>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>
                        <f:Tree ID="tvControlItem" ShowHeader="false" Height="560px" Title="装置区域"
                            OnNodeCommand="tvControlItem_NodeCommand" runat="server" ShowBorder="false" EnableCollapse="true"
                            EnableSingleClickExpand="true" AutoLeafIdentification="true" EnableSingleExpand="true"
                            EnableTextSelection="true">
                        </f:Tree>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                    Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="管线信息"
                    TitleToolTip="管线信息" AutoScroll="true">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar3" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:TextBox ID="txtWorkAreaCode" runat="server" Label="区域"
                                    EmptyText="输入查询条件"
                                    Width="180px" LabelWidth="50px" LabelAlign="Right">
                                </f:TextBox>
                                <f:TextBox ID="txtPipelineCode" runat="server" Label="管线号"
                                    EmptyText="输入查询条件"
                                    Width="240px" LabelWidth="100px" LabelAlign="Right">
                                </f:TextBox>
                                <f:TextBox ID="txtSingleNumber" runat="server" Label="单线图号"
                                    EmptyText="输入查询条件"
                                    Width="240px" LabelWidth="100px" LabelAlign="Right">
                                </f:TextBox>
                                <f:Button ID="btnQuery" ToolTip="查询" Icon="SystemSearch"
                                    EnablePostBack="true" OnClick="btnQuery_Click" runat="server">
                                </f:Button>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="btnSelectColumn" Text="选择显示列" Icon="ShapesManySelect"
                                    runat="server" OnClick="btnSelectColumn_Click" Hidden="true">
                                </f:Button>
                                <f:Button ID="btnNew" Text="新增" Icon="Add" runat="server" OnClick="btnNew_Click">
                                </f:Button>
                                <f:Button ID="btnImport" Text="导入" ToolTip="导入" Icon="PackageIn" runat="server" OnClick="btnImport_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="管线信息"
                            EnableCollapse="true" runat="server" BoxFlex="1" DataKeyNames="PipelineId" AllowCellEditing="true"
                            AllowColumnLocking="true" EnableColumnLines="true" ClicksToEdit="2" DataIDField="PipelineId"
                            AllowSorting="true" SortField="PipelineCode" SortDirection="ASC" OnSort="Grid1_Sort"
                            AllowPaging="true" IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                            EnableTextSelection="True" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"
                            EnableSummary="true" SummaryPosition="Bottom">
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true" HeaderText="序号"
                                    Width="60px" HeaderTextAlign="Center" TextAlign="Center" />
                                <f:WindowField ColumnID="PipelineCode" HeaderTextAlign="Center" TextAlign="Left"
                                    Width="220px" WindowID="Window1" HeaderText="管线号" DataTextField="PipelineCode"
                                    DataIFrameUrlFields="PipelineId" DataIFrameUrlFormatString="PipelineEdit.aspx?PipelineId={0}"
                                    Title="管线号" DataToolTipField="PipelineCode" SortField="PipelineCode"
                                    Locked="true">
                                </f:WindowField>
                                <f:RenderField Width="90px" ColumnID="TotalDin" DataField="TotalDin" FieldType="Double"
                                    HeaderText="总达因数" HeaderTextAlign="Center" TextAlign="Right">
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="JointCount" DataField="JointCount" FieldType="Int"
                                    HeaderText="总焊口量" HeaderTextAlign="Center"
                                    TextAlign="Right">
                                </f:RenderField>
                                <f:RenderField Width="200px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                                    FieldType="String" HeaderText="单位名称" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:TemplateField Width="130px" HeaderText="无损检测类型" HeaderTextAlign="Center"
                                    TextAlign="Center" SortField="DetectionType">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# ConvertDetectionType(Eval("DetectionType")) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:RenderField Width="100px" ColumnID="DetectionRateCode" DataField="DetectionRateCode" SortField="DetectionRateCode"
                                    FieldType="String" HeaderText="探伤比例" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>

                                <f:RenderField Width="100px" ColumnID="MediumCode" DataField="MediumCode" SortField="MediumCode"
                                    FieldType="String" HeaderText="介质代号" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField HeaderText="管道等级" ColumnID="PipingClassCode"
                                    DataField="PipingClassCode" SortField="PipingClassCode" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="100px">
                                </f:RenderField>

                                <f:RenderField Width="100px" ColumnID="SingleNumber" DataField="SingleNumber" SortField="SingleNumber"
                                    FieldType="String" HeaderText="单线图号" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="130px" ColumnID="DesignPress" DataField="DesignPress" SortField="DesignPress"
                                    FieldType="Double" HeaderText="设计压力 MPa(g)" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="130px" ColumnID="DesignTemperature" DataField="DesignTemperature" SortField="DesignTemperature"
                                    FieldType="Double" HeaderText="设计温度℃" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="130px" ColumnID="TestMediumCode" DataField="TestMediumCode" SortField="TestMediumCode"
                                    FieldType="String" HeaderText="压力试验介质" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="160px" ColumnID="TestPressure" DataField="TestPressure" SortField="TestPressure"
                                    FieldType="Double" HeaderText="压力试验压力 MPa(g)" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="130px" ColumnID="PressurePipingClassCode" DataField="PressurePipingClassCode" SortField="PressurePipingClassCode"
                                    FieldType="String" HeaderText="压力管道级别" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="PipeLenth" DataField="PipeLenth" SortField="PipeLenth"
                                    FieldType="Double" HeaderText="管线长度(m)" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="130px" ColumnID="LeakMediumName" DataField="LeakMediumName" SortField="LeakMediumName"
                                    FieldType="String" HeaderText="泄露试验介质" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="160px" ColumnID="LeakPressure" DataField="LeakPressure" SortField="LeakPressure"
                                    FieldType="Double" HeaderText="泄露试验压力 MPa(g)" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="PCMediumName" DataField="PCMediumName" SortField="PCMediumName"
                                    FieldType="String" HeaderText="吹洗要求" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="160px" ColumnID="VacuumPressure" DataField="VacuumPressure" SortField="VacuumPressure"
                                    FieldType="Double" HeaderText="真空试验压力 KPa(a)" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="Remark" DataField="Remark" SortField="Remark"
                                    FieldType="String" HeaderText="备注" HeaderTextAlign="Center"
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
            IsModal="true" Width="800px" Height="560px">
        </f:Window>
        <f:Window ID="Window2" Title="选择显示列" Hidden="true"
            EnableIFrame="true" EnableMaximize="false" Target="Top" EnableResize="false" runat="server"
            IsModal="true" Width="700px" Height="500px" OnClose="Window2_Close">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
                runat="server" Text="编辑" Icon="TableEdit">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
                ConfirmText="删除选中行？" ConfirmTarget="Top" runat="server"
                Text="删除" Icon="Delete">
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

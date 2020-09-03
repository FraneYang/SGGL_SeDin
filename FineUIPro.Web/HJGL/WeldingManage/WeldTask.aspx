<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeldTask.aspx.cs" Inherits="FineUIPro.Web.HJGL.WeldingManage.WeldTask" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊接任务单</title>
    <meta name="sourcefiles" content="~/HJGL/WeldingManage/GetWdldingDailyItem.ashx" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
            <Items>
                <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                    EnableCollapse="true" Width="220px" Title="焊接任务单"
                    ShowBorder="true" Layout="VBox" ShowHeader="true" AutoScroll="true" BodyPadding="5px"
                    IconFont="ArrowCircleLeft">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:DatePicker ID="txtMonth" runat="server" Label="月份" EmptyText="输入查询条件" AutoPostBack="true" Width="220px" LabelWidth="50px" DisplayType="Month" DateFormatString="yyyy-MM">
                                </f:DatePicker>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>
                        <f:Tree ID="tvControlItem" ShowHeader="false" Height="500px" Title="焊接任务单" runat="server" ShowBorder="false" EnableCollapse="true"
                            EnableSingleClickExpand="true" AutoLeafIdentification="true" EnableSingleExpand="true"
                            EnableTextSelection="true" OnNodeCommand="tvControlItem_NodeCommand" EnableExpandEvent="true">
                        </f:Tree>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                    Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="焊接任务单"
                    TitleToolTip="焊接任务单" AutoScroll="true">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:DatePicker ID="txtTaskDate" Label="任务日期" runat="server"
                                    DateFormatString="yyyy-MM-dd" LabelAlign="Left">
                                </f:DatePicker>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="焊接任务单"
                            EnableCollapse="true" runat="server" BoxFlex="1" DataKeyNames="WeldJointId" EnableColumnLines="true"
                            AllowCellEditing="true" ClicksToEdit="1" DataIDField="WeldJointId" AllowSorting="true"
                            SortField="PipelineCode,WeldJointCode" SortDirection="ASC" OnSort="Grid1_Sort"
                            AllowPaging="false" IsDatabasePaging="true" PageSize="10000" EnableTextSelection="True">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar4" Position="Top" runat="server" ToolbarAlign="Right">
                                    <Items>
                                        <f:HiddenField runat="server" ID="hdItemsString"></f:HiddenField>
                                        <f:DropDownList runat="server" EnableEdit="true" Label="可焊焊口"></f:DropDownList>
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                        <f:Button runat="server" ID="CreatWeldableWeldJoint" Icon="ChartPie" Text="生成可焊焊口"></f:Button>
                                        <f:Button runat="server" ID="ckSelect" Icon="Find" ToolTip="查找" Text="查找" OnClick="ckSelect_Click">
                                        </f:Button>
                                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" Text="保存" OnClick="btnSave_Click">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true" HeaderText="序号"
                                    Width="50px" HeaderTextAlign="Center" TextAlign="Center" />
                                <f:RenderField HeaderText="管线号" ColumnID="PipelineCode"
                                    DataField="PipelineCode" SortField="PipelineCode" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="180px" ExpandUnusedSpace="true">
                                </f:RenderField>
                                <f:RenderField HeaderText="焊口号" ColumnID="WeldJointCode"
                                    DataField="WeldJointCode" SortField="WeldJointCode" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="70px">
                                </f:RenderField>
                                <f:RenderField HeaderText="可焊焊口" ColumnID="WeldableWeldJoint"
                                    DataField="WeldableWeldJoint" SortField="WeldableWeldJoint" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="70px">
                                    <Editor>
                                        <f:TextBox runat="server" ID="txtWeldableWeldJoint"></f:TextBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField HeaderText="盖面焊工" ColumnID="CoverWelderId"
                                    DataField="CoverWelderCode" SortField="CoverWelderCode" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="110px">
                                    <Editor>
                                        <f:DropDownList ID="drpCoverWelderId" EnableEdit="true" Required="true" runat="server"
                                            ShowRedStar="true">
                                        </f:DropDownList>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField HeaderText="打底焊工" ColumnID="BackingWelderId"
                                    DataField="BackingWelderCode" SortField="BackingWelderCode" FieldType="String"
                                    HeaderTextAlign="Center" TextAlign="Left" Width="110px">
                                    <Editor>
                                        <f:DropDownList ID="drpBackingWelderId" EnableEdit="true" Required="true" runat="server"
                                            ShowRedStar="true">
                                        </f:DropDownList>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField HeaderText="焊口属性" ColumnID="JointAttribute"
                                    DataField="JointAttribute" SortField="JointAttribute" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="100px">
                                    <Editor>
                                        <f:DropDownList ID="drpJointAttribute" Required="true" runat="server" ShowRedStar="true">
                                        </f:DropDownList>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField HeaderText="机动化程度" ColumnID="WeldingMode"
                                    DataField="WeldingMode" SortField="WeldingMode" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="100px">
                                    <Editor>
                                        <f:DropDownList ID="drpWeldingMode" Required="true" runat="server" ShowRedStar="true">
                                        </f:DropDownList>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField HeaderText="焊接类型" ColumnID="WeldTypeCode"
                                    DataField="WeldTypeCode" SortField="WeldTypeCode" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="70px">
                                </f:RenderField>
                                <f:RenderField HeaderText="达因" ColumnID="Size"
                                    DataField="Size" SortField="Size" FieldType="Double" HeaderTextAlign="Center" TextAlign="Left"
                                    Width="70px">
                                </f:RenderField>
                                <f:RenderField HeaderText="外径" ColumnID="Dia"
                                    DataField="Dia" SortField="Dia" FieldType="Double" HeaderTextAlign="Center" TextAlign="Left"
                                    Width="70px">
                                </f:RenderField>
                                <f:RenderField HeaderText="壁厚" ColumnID="Thickness"
                                    DataField="Thickness" SortField="Thickness" FieldType="Double" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="70px">
                                </f:RenderField>
                                <f:RenderField HeaderText="焊接方法" ColumnID="WeldingMethodCode"
                                    DataField="WeldingMethodCode" SortField="WeldingMethodCode" FieldType="String"
                                    HeaderTextAlign="Center" TextAlign="Left" Width="120px">
                                </f:RenderField>
                                <f:RenderField HeaderText="WeldJointId" ColumnID="WeldJointId" DataField="WeldJointId"
                                    FieldType="String" Hidden="true">
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
            IsModal="true" Width="1200px" Height="650px" OnClose="Window1_Close">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuDelete"
                EnablePostBack="true" Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Top" runat="server" Text="删除" OnClick="btnMenuDelete_Click">
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

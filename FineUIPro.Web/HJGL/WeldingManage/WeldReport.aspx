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
                            <Listeners>
                                <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                            </Listeners>
                        </f:Tree>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                    Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="焊接日报"
                    TitleToolTip="焊接日报" AutoScroll="true">
                    <Items>
                        <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                            runat="server" RedStarPosition="BeforeText" LabelAlign="Left">
                            <Rows>
                                <f:FormRow ColumnWidths="25% 15% 18% 18% 24% ">
                                    <Items>
                                        <f:Label ID="txtUnitName" Label="单位名称" runat="server"
                                            LabelWidth="90px" LabelAlign="Right">
                                        </f:Label>
                                        <f:Label ID="txtTabler" Label="填报人" runat="server"
                                            LabelWidth="90px" LabelAlign="Right">
                                        </f:Label>
                                        <f:Label ID="txtTableDate" Label="填报日期" runat="server"
                                            LabelWidth="90px" LabelAlign="Right">
                                        </f:Label>
                                        <f:Label ID="txtWeldingDate" Label="焊接日期" runat="server"
                                            LabelWidth="90px" LabelAlign="Right">
                                        </f:Label>
                                        <f:Label ID="txtRemark" Label="备注" runat="server"
                                            LabelWidth="90px" LabelAlign="Right">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="焊接日报明细" EnableCollapse="true"
                            runat="server" BoxFlex="1" DataKeyNames="WeldJointId" AllowCellEditing="true"
                            AllowColumnLocking="true" EnableColumnLines="true" ClicksToEdit="2" DataIDField="WeldJointId"
                            AllowSorting="true" SortField="PipelineCode,WeldJointCode" SortDirection="ASC" OnSort="Grid1_Sort"
                            AllowPaging="true" IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange">
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true" HeaderText="序号"
                                    Width="50px" HeaderTextAlign="Center" TextAlign="Center" />
                                <f:RenderField HeaderText="管线号" ColumnID="PipelineCode"
                                    DataField="PipelineCode" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="180px">
                                </f:RenderField>
                                <f:RenderField HeaderText="焊口号" ColumnID="WeldJointCode"
                                    DataField="WeldJointCode" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Center" Width="100px">
                                </f:RenderField>
                                <f:RenderField HeaderText="盖面焊工" ColumnID="CoverWelderCode"
                                    DataField="CoverWelderCode" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Center" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="打底焊工" ColumnID="BackingWelderCode"
                                    DataField="BackingWelderCode" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Center" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="焊口机动化程度" ColumnID="WeldingMode" DataField="WeldingMode"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Center" Width="150px">
                                </f:RenderField>
                                <f:RenderField HeaderText="材质1" ColumnID="Material1Code"
                                    DataField="Material1Code" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Center" Width="120px">
                                </f:RenderField>
                                <f:RenderField HeaderText="材质2" ColumnID="Material2Code"
                                    DataField="Material2Code" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Center" Width="120px">
                                </f:RenderField>
                                <f:RenderField HeaderText="外径" ColumnID="Dia"
                                    DataField="Dia" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Center" Width="80px">
                                </f:RenderField>
                                <f:RenderField HeaderText="壁厚" ColumnID="Thickness"
                                    DataField="Thickness" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Center" Width="80px">
                                </f:RenderField>

                                <f:RenderField HeaderText="焊缝类型" ColumnID="WeldTypeCode"
                                    DataField="WeldTypeCode" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Center" Width="80px">
                                </f:RenderField>
                                <f:RenderField HeaderText="焊接方法" ColumnID="WeldingMethodCode"
                                    DataField="WeldingMethodCode" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Center" Width="160px">
                                </f:RenderField>

                                <f:RenderField HeaderText="焊丝" ColumnID="WeldingWireCode"
                                    DataField="WeldingWireCode" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Center" Width="150px">
                                </f:RenderField>
                                <f:RenderField HeaderText="焊条" ColumnID="WeldingRodCode" DataField="WeldingRodCode"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Center" Width="150px">
                                </f:RenderField>

                            </Columns>
                            <PageItems>
                                <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                </f:ToolbarSeparator>
                                <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                                </f:ToolbarText>
                                <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                    <f:ListItem Text="15" Value="10" />
                                    <f:ListItem Text="25" Value="15" />
                                    <f:ListItem Text="50" Value="20" />
                                    <f:ListItem Text="100" Value="25" />
                                </f:DropDownList>
                            </PageItems>
                        </f:Grid>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Top" EnableResize="false" runat="server" OnClose="Window1_Close"
            IsModal="true" Width="1200px" Height="650px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuAdd" EnablePostBack="true" runat="server" Text="新增" Icon="Add" OnClick="btnMenuAdd_Click">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="BulletEdit" EnablePostBack="true"
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
        function onTreeNodeContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>

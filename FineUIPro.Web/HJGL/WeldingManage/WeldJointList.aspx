<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeldJointList.aspx.cs" Inherits="FineUIPro.Web.HJGL.WeldingManage.WeldJointList" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊口信息</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
            <Items>
                <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                    EnableCollapse="true" Width="300px" Title="焊口号"
                    ShowBorder="true" Layout="VBox" ShowHeader="true" AutoScroll="true" BodyPadding="5px"
                    IconFont="ArrowCircleLeft">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:TextBox ID="txtPipelineCode" runat="server" Label="管线"
                                    EmptyText="输入查询条件" AutoPostBack="true" OnTextChanged="Tree_TextChanged"
                                    Width="280px" LabelWidth="60px">
                                </f:TextBox>
                                <%--<f:Button ID="btnOut" runat="server" Text="导出" ToolTip="导出焊口情况信息" Icon="FolderUp" OnClick="btnOut_Click">
                            </f:Button>--%>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>
                        <f:Tree ID="tvControlItem" ShowHeader="false" Height="560px" Title="单位工程"
                            OnNodeCommand="tvControlItem_NodeCommand" runat="server" ShowBorder="false" EnableCollapse="true"
                            EnableSingleClickExpand="true" AutoLeafIdentification="true"
                            EnableTextSelection="true">
                        </f:Tree>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                    Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="焊口信息"
                    TitleToolTip="焊口信息" AutoScroll="true">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar3" Position="Top" runat="server" ToolbarAlign="Right">
                            <Items>
                                <f:TextBox ID="txtWeldJointCode" runat="server" Label="焊口号"
                                    EmptyText="输入查询条件" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"
                                    Width="240px" LabelWidth="80px" LabelAlign="Right">
                                </f:TextBox>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="btnSelectColumn" Text="选择显示列" Icon="ShapesManySelect"
                                    runat="server" OnClick="btnSelectColumn_Click">
                                </f:Button>
                                <f:Button ID="btnNew" Text="新增" Icon="Add" runat="server" OnClick="btnNew_Click">
                                </f:Button>
                                <f:Button ID="btnBatchAdd" Text="批量增加" Icon="TableAdd"
                                    runat="server" OnClick="btnBatchAdd_Click">
                                </f:Button>
                                <f:Button ID="btnOut2" runat="server" Text="导出" ToolTip="导出焊口初始数据信息"
                                    Icon="FolderUp" EnableAjax="false" DisableControlBeforePostBack="false" OnClick="btnOut2_Click">
                                </f:Button>
                                <f:Button ID="btnPrint" Text="打印" Icon="Printer" runat="server"
                                    OnClick="btnPrint_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>

                    </Toolbars>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="焊口信息"
                            EnableCollapse="true" runat="server" BoxFlex="1" DataKeyNames="WeldJointId" AllowCellEditing="true"
                            AllowColumnLocking="true" EnableColumnLines="true" ClicksToEdit="2" DataIDField="WeldJointId"
                            AllowSorting="true" SortField="WeldJointCode" SortDirection="ASC" OnSort="Grid1_Sort"
                            AllowPaging="true" IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                            EnableTextSelection="True" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true" HeaderText="序号"
                                    Width="60px" HeaderTextAlign="Center" TextAlign="Center" ColumnID="tfNumber" />
                                <f:GroupField HeaderText="设计基础数据" TextAlign="Center">
                                    <Columns>
                                        <f:WindowField ColumnID="WeldJointCode" HeaderTextAlign="Center" TextAlign="Left"
                                            Width="100px" WindowID="Window1" HeaderText="焊口号"
                                            DataTextField="WeldJointCode" DataIFrameUrlFields="WeldJointId" DataIFrameUrlFormatString="WeldJointEdit.aspx?WeldJointId={0}"
                                            Title="焊口号" DataToolTipField="WeldJointCode"
                                            SortField="WeldJointCode" Locked="true">
                                        </f:WindowField>
                                        <f:RenderField HeaderText="材质1" ColumnID="Material1Code" DataField="Material1Code" SortField="Material1Code" FieldType="String" HeaderTextAlign="Center"
                                            TextAlign="Left" Width="90px">
                                        </f:RenderField>
                                        <f:RenderField HeaderText="材质2" ColumnID="Material2Code"
                                            DataField="Material2Code" SortField="Material2Code" FieldType="String" HeaderTextAlign="Center"
                                            TextAlign="Left" Width="90px">
                                        </f:RenderField>
                                        <f:RenderField HeaderText="达因" ColumnID="Size" DataField="Size"
                                            SortField="Size" FieldType="Double" HeaderTextAlign="Center" TextAlign="Left"
                                            Width="90px">
                                        </f:RenderField>
                                        <f:RenderField HeaderText="外径" ColumnID="Dia"
                                            DataField="Dia" SortField="Dia" FieldType="Double" HeaderTextAlign="Center" TextAlign="Left"
                                            Width="90px">
                                        </f:RenderField>
                                        <f:RenderField HeaderText="壁厚" ColumnID="Thickness"
                                            DataField="Thickness" SortField="Thickness" FieldType="Double" HeaderTextAlign="Center"
                                            TextAlign="Left" Width="90px">
                                        </f:RenderField>
                                        <f:RenderField HeaderText="规格" ColumnID="Specification"
                                            DataField="Specification" SortField="Specification" FieldType="String" HeaderTextAlign="Center"
                                            TextAlign="Left" Width="90px">
                                        </f:RenderField>
                                        <f:RenderField HeaderText="焊缝类型" ColumnID="WeldTypeCode"
                                            DataField="WeldTypeCode" SortField="WeldTypeCode" FieldType="String" HeaderTextAlign="Center"
                                            TextAlign="Left" Width="90px">
                                        </f:RenderField>
                                        <f:RenderField HeaderText="组件1号" ColumnID="ComponentsCode1"
                                            DataField="ComponentsCode1" SortField="ComponentsCode1" FieldType="String" HeaderTextAlign="Center"
                                            TextAlign="Left" Width="90px">
                                        </f:RenderField>
                                        <f:RenderField HeaderText="组件2号" ColumnID="ComponentsCode2"
                                            DataField="ComponentsCode2" SortField="ComponentsCode2" FieldType="String" HeaderTextAlign="Center"
                                            TextAlign="Left" Width="90px">
                                        </f:RenderField>
                                    </Columns>
                                </f:GroupField>
                                <f:GroupField HeaderText="施工基础数据" TextAlign="Center">
                                    <Columns>
                                        <f:RenderField HeaderText="对应WPS" ColumnID="WPQCode"
                                            DataField="WPQCode" SortField="WPQCode" FieldType="String" HeaderTextAlign="Center"
                                            TextAlign="Left" Width="100px">
                                        </f:RenderField>
                                        <f:RenderField HeaderText="坡口类型" ColumnID="GrooveTypeCode"
                                            DataField="GrooveTypeCode" SortField="GrooveTypeCode" FieldType="String" HeaderTextAlign="Center"
                                            TextAlign="Left" Width="90px">
                                        </f:RenderField>
                                        <f:RenderField HeaderText="焊接方法" ColumnID="WeldingMethodCode"
                                            DataField="WeldingMethodCode" SortField="WeldingMethodCode" FieldType="String"
                                            HeaderTextAlign="Center" TextAlign="Left" Width="90px">
                                        </f:RenderField>
                                        <f:RenderField HeaderText="焊丝" ColumnID="WeldingWireCode"
                                            DataField="WeldingWireCode" SortField="WeldingWireCode" FieldType="String" HeaderTextAlign="Center"
                                            TextAlign="Left" Width="90px">
                                        </f:RenderField>
                                        <f:RenderField HeaderText="焊条" ColumnID="WeldingRodCode"
                                            DataField="WeldingRodCode" SortField="WeldingRodCode" FieldType="String" HeaderTextAlign="Center"
                                            TextAlign="Left" Width="90px">
                                        </f:RenderField>
                                        <f:RenderField HeaderText="预热温度" ColumnID="PreTemperature"
                                            DataField="PreTemperature" SortField="PreTemperature" FieldType="String" HeaderTextAlign="Center"
                                            TextAlign="Left" Width="90px">
                                        </f:RenderField>
                                        <f:RenderField HeaderText="是否热处理" ColumnID="IsHotProessStr"
                                            DataField="IsHotProessStr" FieldType="String" HeaderTextAlign="Center"
                                            TextAlign="Left" Width="110px">
                                        </f:RenderField>
                                    </Columns>
                                </f:GroupField>
                                <f:RenderField HeaderText="备注" ColumnID="Remark" DataField="Remark"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
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
            EnableMaximize="true" Target="Top" EnableResize="false" runat="server" OnClose="Window1_Close"
            IsModal="true" Width="1200px" Height="620px">
        </f:Window>

        <f:Window ID="Window2" Title="弹出窗体" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Top" EnableResize="false" runat="server" IsModal="true"
            Width="1024px" Height="620px">
        </f:Window>
        <f:Window ID="Window3" Title="弹出窗体" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Top" EnableResize="false" runat="server" IsModal="true"
            Width="1200px" Height="620px">
        </f:Window>
        <f:Window ID="Window4" Title="选择显示列" Hidden="true"
            EnableIFrame="true" EnableMaximize="false" Target="Top" EnableResize="false" runat="server"
            IsModal="true" Width="700px" Height="470px" OnClose="Window4_Close">
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

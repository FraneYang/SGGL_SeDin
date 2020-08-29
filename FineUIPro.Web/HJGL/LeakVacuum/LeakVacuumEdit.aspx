<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeakVacuumEdit.aspx.cs" Inherits="FineUIPro.Web.HJGL.LeakVacuum.LeakVacuumEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>泄露性/真空包</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
            <Items>
                <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                    EnableCollapse="true" Width="230px" Title="试压包" ShowBorder="true" Layout="VBox"
                    ShowHeader="true" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                    <Items>
                        <f:Tree ID="tvControlItem" ShowHeader="false" Title="试压包节点树" OnNodeCommand="tvControlItem_NodeCommand"
                            runat="server" ShowBorder="false" EnableCollapse="true" EnableSingleClickExpand="true" AutoLeafIdentification="true"
                            EnableSingleExpand="true" EnableTextSelection="true">
                            <Listeners>
                                <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                            </Listeners>
                        </f:Tree>
                        <f:HiddenField runat="server" ID="hdLeakVacuumId"></f:HiddenField>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                    Layout="VBox" ShowHeader="false" BodyPadding="2px" IconFont="PlusCircle" Title="试压包"
                    TitleToolTip="试压包" AutoScroll="true">
                    <Items>
                        <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true"
                            BodyPadding="2px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                        <f:Label ID="txtSysNo" Label="系统号" runat="server" LabelWidth="130px">
                                        </f:Label>
                                        <f:Label ID="txtSysName" Label="系统名称" runat="server" LabelWidth="130px">
                                        </f:Label>
                                        <f:Label ID="drpTabler" Label="创建人" runat="server" LabelWidth="130px">
                                        </f:Label>
                                        <f:Label ID="txtTableDate" Label="创建日期" runat="server" LabelWidth="130px">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ColumnWidths="25% 25% 50%">
                                    <Items>
                                        <f:Label ID="drpAuditer" Label="审核人" runat="server" LabelWidth="130px">
                                        </f:Label>
                                        <f:Label ID="txtAduditDate" Label="审核日期" runat="server" LabelWidth="130px">
                                        </f:Label>
                                        <f:Label ID="txtRemark" Label="备注" runat="server" LabelWidth="130px">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                    <Items>
                        <f:Panel runat="server" ID="panel2" RegionPosition="Center" ShowBorder="true" Layout="VBox"
                            BodyPadding="2px" IconFont="PlusCircle" Title="试压前条件确认"
                            ShowHeader="true" AutoScroll="true" EnableCollapse="true" Collapsed="false" Height="200px">
                            <Items>
                                <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" Title="试压包" AutoScroll="true"
                                    EnableCollapse="true" Collapsed="false" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
                                    LabelAlign="Left">
                                    <Rows>
                                        <f:FormRow>
                                            <Items>
                                                <f:TextBox ID="txtCheck1" runat="server" Label="1.管道压力试验合格"
                                                    LabelAlign="Right" LabelWidth="350px">
                                                </f:TextBox>
                                                <f:TextBox ID="txtCheck2" runat="server" Label="2.压力试验采用的临时法兰、螺栓、垫片等均己更换" LabelAlign="Right"
                                                    LabelWidth="350px">
                                                </f:TextBox>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:TextBox ID="txtCheck3" runat="server" Label="3.未参与压力试验的安全附件及仪表等己安装复位" LabelAlign="Right"
                                                    LabelWidth="350px">
                                                </f:TextBox>
                                                <f:TextBox ID="txtCheck4" runat="server" Label="4.试验管道系统的阀门已全部开启"
                                                    LabelAlign="Right" LabelWidth="350px">
                                                </f:TextBox>
                                            </Items>
                                        </f:FormRow>

                                        <f:FormRow>
                                            <Items>
                                                <f:TextBox ID="txtCheck5" runat="server" Label="5.试压用压力表、精度等级、检定状态符合规范要求"
                                                    LabelAlign="Right" LabelWidth="350px">
                                                </f:TextBox>
                                                <f:Label runat="server"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                    </Rows>
                                </f:Form>
                            </Items>
                        </f:Panel>
                    </Items>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="试压包明细" EnableCollapse="true" Collapsed="false"
                            runat="server" BoxFlex="1" DataKeyNames="LV_PipeId" AllowCellEditing="true"
                            EnableColumnLines="true" ClicksToEdit="2" DataIDField="LV_PipeId" AllowSorting="true"
                            SortField="PipelineCode" SortDirection="ASC" OnSort="Grid1_Sort" EnableTextSelection="True"
                            AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange">
                            <Columns>
                                <f:RenderField HeaderText="管线编号" ColumnID="PipelineCode" DataField="PipelineCode" SortField="PipelineCode"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="160px" ExpandUnusedSpace="true">
                                </f:RenderField>
                                <f:RenderField HeaderText="设计压力" ColumnID="DesignPress" DataField="DesignPress" SortField="DesignPress"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="设计温度" ColumnID="DesignTemperature" DataField="DesignTemperature" SortField="DesignTemperature"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="试验环境温度" ColumnID="AmbientTemperature" DataField="AmbientTemperature" SortField="AmbientTemperature" FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="110px">
                                </f:RenderField>
                                <f:RenderField HeaderText="试验介质温度" ColumnID="TestMediumTemperature" DataField="TestMediumTemperature" SortField="TestMediumTemperature" FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="110px">
                                </f:RenderField>
                                <f:GroupField HeaderText="泄露性试验" TextAlign="Center">
                                    <Columns>
                                        <f:RenderField Width="100px" ColumnID="LeakPressure" DataField="LeakPressure"
                                            FieldType="String" HeaderText="压力" TextAlign="Center" HeaderTextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="110px" ColumnID="LeaMediumName" DataField="LeaMediumName"
                                            FieldType="String" HeaderText="介质" TextAlign="Center" HeaderTextAlign="Center">
                                        </f:RenderField>
                                    </Columns>
                                </f:GroupField>
                                <f:GroupField HeaderText="真空试验" TextAlign="Center">
                                    <Columns>
                                        <f:RenderField Width="100px" ColumnID="VacuumPressure" DataField="VacuumPressure"
                                            FieldType="String" HeaderText="压力" TextAlign="Center" HeaderTextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="110px" ColumnID="VacuumMediumName" DataField="VacuumMediumName"
                                            FieldType="String" HeaderText="介质" TextAlign="Center" HeaderTextAlign="Center">
                                        </f:RenderField>
                                    </Columns>
                                </f:GroupField>
                            </Columns>
                            <PageItems>
                                <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                </f:ToolbarSeparator>
                                <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                                </f:ToolbarText>
                                <f:DropDownList runat="server" ID="ddlPageSize" Width="90px" AutoPostBack="true"
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
        <f:Window ID="Window1" Title="泄露性/真空包维护" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
            Width="1280px" Height="900px">
        </f:Window>
        <f:Window ID="Window2" Title="泄露性/真空包打印" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Top" EnableResize="true" runat="server"
            IsModal="true" Width="1024px" Height="640px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <Items>
                <f:MenuButton ID="btnMenuNew" EnablePostBack="true" runat="server" Text="新增" Icon="Add" OnClick="btnMenuNew_Click">
                </f:MenuButton>
                <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Text="编辑" Icon="Pencil" OnClick="btnMenuModify_Click">
                </f:MenuButton>
                <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server" Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？"
                    OnClick="btnMenuDel_Click">
                </f:MenuButton>
                <f:MenuButton ID="btnMenuPrint" EnablePostBack="true" runat="server" Text="编辑" Icon="Pencil" OnClick="btnMenuPrint_Click">
                </f:MenuButton>
            </Items>
        </f:Menu>
    </form>
    <script type="text/javascript">
        // 返回false，来阻止浏览器右键菜单
        var menuID = '<%= Menu1.ClientID %>';
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

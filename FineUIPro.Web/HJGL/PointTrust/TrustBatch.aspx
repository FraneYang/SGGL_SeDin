<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrustBatch.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.PointTrust.TrustBatch" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无损委托单</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
            <Items>
                <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                    EnableCollapse="true" Width="320px" Title="无损委托单"
                    ShowBorder="true" Layout="VBox" ShowHeader="false" AutoScroll="true" BodyPadding="5px"
                    IconFont="ArrowCircleLeft">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar4" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox ID="txtWelderCode" runat="server" EmptyText="输入查询条件"
                                AutoPostBack="true" Label="焊工号" LabelWidth="90px"
                                OnTextChanged="Tree_TextChanged" Width="300px" LabelAlign="Right">
                            </f:TextBox>
                        </Items>
                    </f:Toolbar>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:DatePicker ID="txtTrustDateMonth" runat="server" Label="月份"
                                    EmptyText="输入查询条件" AutoPostBack="true" OnTextChanged="Tree_TextChanged"
                                    Width="265px" LabelWidth="100px" DisplayType="Month" DateFormatString="yyyy-MM" LabelAlign="Right">
                                </f:DatePicker>
                            </Items>
                        </f:Toolbar>
                        <%--<f:Toolbar ID="Toolbar5" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:TextBox ID="txtSearchCode" runat="server" EmptyText="输入查询条件"
                                    AutoPostBack="true" Label="委托单号" LabelWidth="100px"
                                    OnTextChanged="Tree_TextChanged" Width="265px" LabelAlign="Right">
                                </f:TextBox>
                            </Items>
                        </f:Toolbar>--%>
                    </Toolbars>
                    <Items>
                        <f:Tree ID="tvControlItem" ShowHeader="false" Height="500px" Title="无损委托单"
                            OnNodeCommand="tvControlItem_NodeCommand" runat="server" ShowBorder="false" EnableCollapse="true"
                            EnableSingleClickExpand="true" AutoLeafIdentification="true" EnableSingleExpand="true"
                            EnableTextSelection="true" OnNodeExpand="tvControlItem_TreeNodeExpanded">
                            <Listeners>
                                <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                            </Listeners>
                        </f:Tree>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                    Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="无损委托单"
                    TitleToolTip="无损委托单" AutoScroll="true">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar3" Position="Top" runat="server" ToolbarAlign="Right" Hidden="true">
                            <Items>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server" >
                                </f:ToolbarFill>
                                <f:Button ID="btnAudit" Text="审核" ToolTip="审核后才可进行检测" Icon="Accept" runat="server"
                                    OnClick="btnAudit_Click" Hidden="true">
                                </f:Button>
                                <%--<f:Button ID="btnPointAudit" Text="生成委托单" ToolTip="监理点口审核并生成委托单" Icon="ArrowNsew" runat="server"
                                OnClick="btnPointAudit_Click">
                            </f:Button>--%>
                                <f:Button ID="btnDelete" Text="删除" Icon="Delete" runat="server" Hidden="true"
                                    OnClick="btnDelete_Click">
                                </f:Button>
                                <f:Button ID="btnPrint" Text="打印" Icon="Printer" runat="server"
                                    OnClick="btnPrint_Click" Hidden="true">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>
                        <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" BodyPadding="2px"
                            runat="server" RedStarPosition="BeforeText" LabelAlign="Left">
                            <Rows>
                                <f:FormRow ColumnWidths="46% 27% 27%">
                                    <Items>
                                        <f:Label ID="txtTrustBatchCode" Label="委托单号" runat="server"
                                            LabelWidth="170px" LabelAlign="Right">
                                        </f:Label>
                                        <f:Label ID="txtTrustDate" Label="委托日期" runat="server"
                                            LabelWidth="105px" LabelAlign="Right">
                                        </f:Label>
                                        <f:Label ID="txtDetectionTypeCode" Label="检测方法" runat="server"
                                            LabelWidth="105px" LabelAlign="Right">
                                        </f:Label>

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ColumnWidths="46% 27% 27%">
                                    <Items>
                                        <f:Label ID="lbNDEUnit" Label="检测单位" runat="server"
                                            LabelWidth="170px" LabelAlign="Right">
                                        </f:Label>
                                        
                                        <f:Label ID="lbIsTrust" Label="是否委托" runat="server"
                                            LabelWidth="105px" LabelAlign="Right">
                                        </f:Label>
                                        <f:Label ID="lbIsAudit" Label="是否审核" runat="server" Hidden="true"
                                            LabelWidth="105px" LabelAlign="Right">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="无损委托单"
                            EnableCollapse="true" runat="server" BoxFlex="1" DataKeyNames="PointBatchItemId"
                            AllowCellEditing="true" AllowColumnLocking="true" EnableColumnLines="true" ClicksToEdit="2"
                            DataIDField="PointBatchItemId" AllowSorting="true" SortField="PipelineCode,WeldJointCode"
                            SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                            PageSize="25" OnPageIndexChange="Grid1_PageIndexChange" EnableTextSelection="True">
                            <Columns>
                                <f:RenderField Width="160px" ColumnID="PipelineCode" DataField="PipelineCode" FieldType="String"
                                    HeaderText="管线号" HeaderTextAlign="Center" TextAlign="Left"
                                    SortField="PipelineCode">
                                </f:RenderField>
                                <f:RenderField Width="80px" ColumnID="PipingClassCode" DataField="PipingClassCode"
                                    FieldType="String" HeaderText="管道等级" HeaderTextAlign="Center"
                                    TextAlign="Left" SortField="PipingClassCode">
                                </f:RenderField>
                                <f:RenderField Width="80px" ColumnID="WeldJointCode" DataField="WeldJointCode" FieldType="String"
                                    HeaderText="焊口号" HeaderTextAlign="Center"
                                    TextAlign="Left" SortField="WeldJointCode">
                                </f:RenderField>
                                <f:RenderField Width="80px" ColumnID="JointArea" DataField="JointArea" FieldType="String"
                                    HeaderText="焊接区域" HeaderTextAlign="Center" TextAlign="Left"
                                    SortField="JointArea">
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="WelderCode" DataField="WelderCode" FieldType="String"
                                    HeaderText="焊工号" HeaderTextAlign="Center" TextAlign="Left"
                                    SortField="WelderCode">
                                </f:RenderField>
                                <f:RenderField Width="80px" ColumnID="WeldTypeCode" DataField="WeldTypeCode" FieldType="String"
                                    HeaderText="焊缝类型" HeaderTextAlign="Center" TextAlign="Left"
                                    SortField="WeldTypeCode">
                                </f:RenderField>
                                <f:RenderField Width="70px" ColumnID="Dia" DataField="Dia" FieldType="Double" HeaderText="外径"
                                    HeaderTextAlign="Center" TextAlign="Left" SortField="Dia">
                                </f:RenderField>
                                <f:RenderField Width="70px" ColumnID="Thickness" DataField="Thickness" FieldType="Double"
                                    HeaderText="壁厚" HeaderTextAlign="Center" TextAlign="Left"
                                    SortField="Thickness">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="PointDate" DataField="PointDate" SortField="PointDate"
                                    FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="点口日期"
                                    HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="80px" ColumnID="AcceptLevel" DataField="AcceptLevel" FieldType="String" HeaderText="合格等级"
                                    HeaderTextAlign="Center" TextAlign="Left">
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
            IsModal="true" Width="1050px" Height="650px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnPointAudit" EnablePostBack="true" runat="server" Text="委托" Icon="ArrowNsew" OnClick="btnPointAudit_Click">
            </f:MenuButton>
        </f:Menu>
    </form>
    <script type="text/javascript">
        var menuID1 = '<%= Menu1.ClientID %>';
        function onTreeNodeContextMenu(event, rowId) {
            F(menuID1).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckNotice.aspx.cs" Inherits="FineUIPro.Web.OfficeCheck.Check.CheckNotice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>检查通知单</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <script src="../res/js/jquery-3.3.1.min.js" type="text/javascript"></script>
    <style type="text/css">
        .labcenter {
            text-align: center;
        }

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
                    EnableCollapse="true" Width="260px" Title="检查通知" TitleToolTip="检查通知" ShowBorder="true"
                    ShowHeader="false" BodyPadding="5px" IconFont="ArrowCircleLeft" Layout="VBox" AutoScroll="true">
                    <Items>
                        <f:DatePicker ID="txtCheckStartTimeS" runat="server" Label="开始时间" AutoPostBack="true" OnTextChanged="Tree_TextChanged"
                            DateFormatString="yyyy-MM-dd" LabelWidth="80px">
                        </f:DatePicker>
                        <f:DatePicker ID="txtCheckEndTimeS" runat="server" Label="结束时间" AutoPostBack="true" OnTextChanged="Tree_TextChanged"
                            DateFormatString="yyyy-MM-dd" LabelWidth="80px">
                        </f:DatePicker>
                    </Items>
                    <Items>
                        <f:Tree ID="tvControlItem" EnableCollapse="true" ShowHeader="true" Title="检查通知节点树"
                            OnNodeCommand="tvControlItem_NodeCommand" AutoLeafIdentification="true"
                            runat="server" EnableTextSelection="true" AutoScroll="true" Height="580px">
                        </f:Tree>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                    Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="检查通知"
                    TitleToolTip="检查通知" AutoScroll="true">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                            <Items>
                                <%--<f:Button ID="btnFind" Text="安全监督检查管理办法" Icon="Find" runat="server" OnClick="btnFind_Click">
                            </f:Button>--%>
                                <f:HiddenField runat="server" ID="hdCheckNoticeId"></f:HiddenField>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                <f:Button ID="btnNew" ToolTip="增加检查通知" Icon="Add" runat="server" OnClick="btnNew_Click" Hidden="true">
                                </f:Button>
                                <f:Button ID="btnEdit" ToolTip="修改检查通知" Icon="TableEdit" runat="server" OnClick="btnEdit_Click" Hidden="true">
                                </f:Button>
                                <f:Button ID="btnDelete" ToolTip="删除检查通知" ConfirmText="确认删除此检查通知?" ConfirmTarget="Top" Hidden="true"
                                    Icon="Delete" runat="server" OnClick="btnDelete_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>
                        <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true"
                            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Left">
                            <Rows>
                                <f:FormRow ColumnWidths="40% 60%">
                                    <Items>
                                        <f:Label ID="drpSubjectUnit" Label="受检单位" runat="server" LabelWidth="80px">
                                        </f:Label>
                                        <f:Label ID="txtSubjectObject" ShowLabel="false" runat="server" MarginLeft="140px"></f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ColumnWidths="40% 60%">
                                    <Items>
                                        <f:Label ID="txtSubjectUnitMan" Label="受检单位负责人" runat="server" LabelWidth="120px">
                                        </f:Label>
                                        <f:Label ID="txtSubjectUnitTel" Label="受检单位负责人电话" runat="server" LabelWidth="150px">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ColumnWidths="40% 60%">
                                    <Items>
                                        <f:Label ID="txtCheckTeamLeader" Label="检查组长" runat="server" LabelWidth="80px">
                                        </f:Label>
                                        <f:Label ID="txtSubjectUnitAdd" Label="受检单位地址" runat="server" LabelWidth="120px">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ColumnWidths="40% 60%">
                                    <Items>
                                        <f:Label ID="txtCheckStartTime" Label="检查开始日期" runat="server" LabelWidth="110px">
                                        </f:Label>
                                        <f:Label ID="txtCheckEndTime" Label="检查结束日期" runat="server" LabelWidth="110px">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ColumnWidths="40% 60%">
                                    <Items>
                                        <f:Label ID="txtCompileMan" Label="编制人" runat="server" LabelWidth="100px">
                                        </f:Label>
                                        <f:Label ID="txtCompileDate" Label="编制日期" runat="server" LabelWidth="100px">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                    <Items>
                        <f:Grid ID="Grid2" Width="870px" ShowBorder="true" ShowHeader="false" EnableCollapse="true" Title="检查组成员"
                            runat="server" BoxFlex="1" DataKeyNames="CheckTeamId" AllowSorting="true" IsDatabasePaging="true" PageSize="10"
                            OnSort="Grid2_Sort" SortField="SortIndex" SortDirection="ASC" AllowCellEditing="true" Collapsed="false" EnableCollapseEvent="true"
                            ClicksToEdit="2" EnableColumnLines="true" DataIDField="CheckTeamId" AllowPaging="true" EnableExpandEvent="true"
                            EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid2_RowDoubleClick" EnableTextSelection="True">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar3" Position="Top" runat="server" ToolbarAlign="Right">
                                    <Items>
                                        <f:Label runat="server" Text="检查组成员"></f:Label>
                                        <f:ToolbarFill ID="ToolbarFill3" runat="server"></f:ToolbarFill>
                                        <f:Button ID="btnCheckTeamAdd" ToolTip="增加检查组成员" Icon="Add" runat="server" Hidden="true" OnClick="btnCheckTeamAdd_Click">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:RenderField Width="70px" ColumnID="SortIndex" DataField="SortIndex"
                                    SortField="SortIndex" FieldType="Int" HeaderText="序号"
                                    HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="80px" ColumnID="UserName" DataField="UserName"
                                    SortField="UserName" FieldType="String" HeaderText="姓名"
                                    HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="65px" ColumnID="SexName" DataField="SexName"
                                    SortField="SexName" FieldType="String" HeaderText="性别"
                                    HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="UnitName" DataField="UnitName"
                                    SortField="UnitName" FieldType="String" HeaderText="所在单位" ExpandUnusedSpace="true"
                                    HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="80px" ColumnID="PostName" DataField="PostName"
                                    SortField="PostName" FieldType="String" HeaderText="单位职务"
                                    HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="80px" ColumnID="WorkTitle" DataField="WorkTitle"
                                    SortField="WorkTitle" FieldType="String" HeaderText="职称"
                                    HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="90px" ColumnID="CheckPostName" DataField="CheckPostName"
                                    SortField="CheckPostName" FieldType="String" HeaderText="工作组职务"
                                    HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="90px" ColumnID="CheckDate" DataField="CheckDate"
                                    SortField="CheckDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                                    HeaderText="检查日期" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                            </Columns>
                            <Listeners>
                                <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu2" />
                            </Listeners>
                        </f:Grid>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="检查通知维护" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
            Width="800px" Height="450px">
        </f:Window>
        <f:Window ID="WindowTeam" Title="维护检查工作组" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Self" EnableResize="true" runat="server" OnClose="WindowTeam_Close" IsModal="true"
            Width="800px" Height="350px">
        </f:Window>
        <%--<f:Window ID="Window3" Title="查看" ShowHeader="false" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true"
        Width="900px" Height="520px">
   </f:Window>--%>
        <f:Menu ID="Menu2" runat="server">
            <f:MenuButton ID="btnCheckTeamEdit" EnablePostBack="true" OnClick="btnCheckTeamEdit_Click"
                runat="server" Text="编辑" Hidden="true" Icon="TableEdit">
            </f:MenuButton>
            <f:MenuButton ID="btnCheckTeamDelete" EnablePostBack="true" Icon="Delete" OnClick="btnCheckTeamDelete_Click"
                ConfirmText="删除选中行？" ConfirmTarget="Top" runat="server" Text="删除" Hidden="true">
            </f:MenuButton>
        </f:Menu>

    </form>
    <script type="text/javascript">
        var menuID2 = '<%= Menu2.ClientID %>';
        function onRowContextMenu2(event, rowId) {
            F(menuID2).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
    <%--<script src="../res/js/hook.js" type="text/javascript"></script>--%>
</body>
</html>

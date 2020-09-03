<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotProessReportItem.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.HotProcessHard.HotProessReportItem" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>热处理报告</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="热处理报告"
                    EnableCollapse="true" runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="HotProessReportId,WeldJointId"
                    AllowCellEditing="true" ClicksToEdit="1" DataIDField="HotProessReportId" AllowSorting="true"
                    SortField="HotProessReportId" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                    IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange" OnRowCommand="Grid1_RowCommand" EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:Label runat="server" ID="lbPipeLineCode" Label="管线号" LabelAlign="Right"></f:Label>
                                <f:Label runat="server" ID="lbWeldJointCode" Label="焊口号"  LabelAlign="Right"></f:Label>
                                <f:DropDownList ID="drpIsCompleted" runat="server" Label="是否完成" LabelWidth="80px">
                                    <f:ListItem Value="True" Text="完成" Selected="true" />
                                    <f:ListItem Value="False" Text="未完成" />
                                </f:DropDownList>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                <f:Button ID="btnSave" ToolTip="保存该焊口热处理是否完成" Text="保存" Icon="SystemSave" EnablePostBack="true"
                                    runat="server" OnClick="btnSave_Click">
                                </f:Button>
                                <f:Button ID="btnNew" ToolTip="新增" Text="新增" Icon="Add" EnablePostBack="true"
                                    runat="server" OnClick="btnNew_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:RenderField Width="120px" ColumnID="PointCount" DataField="PointCount" FieldType="Int"
                            HeaderText="测温点编号" HeaderTextAlign="Center" TextAlign="Left"
                            SortField="PointCount">
                            <Editor>
                                <f:NumberBox ID="txtPointCount" runat="server" NoDecimal="true">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="150px" ColumnID="RequiredT" DataField="RequiredT" FieldType="String"
                            HeaderText="热处理温度℃(要求)" HeaderTextAlign="Center" TextAlign="Left">
                            <Editor>
                                <f:NumberBox ID="txtRequiredT" runat="server">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="150px" ColumnID="ActualT" DataField="ActualT" FieldType="String"
                            HeaderText="热处理温度℃(实际)" HeaderTextAlign="Center" TextAlign="Left">
                            <Editor>
                                <f:NumberBox ID="txtActualT" runat="server">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="150px" ColumnID="RequestTime" DataField="RequestTime" FieldType="String"
                            HeaderText="恒温时间h（要求）" HeaderTextAlign="Center" TextAlign="Left">
                            <Editor>
                                <f:NumberBox ID="txtRequestTime" runat="server">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="150px" ColumnID="ActualTime" DataField="ActualTime" FieldType="String"
                            HeaderText="恒温时间h（实际）" HeaderTextAlign="Center" TextAlign="Left">
                            <Editor>
                                <f:NumberBox ID="txtActualTime" runat="server">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="150px" ColumnID="RecordChartNo" DataField="RecordChartNo" FieldType="String"
                            HeaderText="记录曲线图编号" HeaderTextAlign="Center" TextAlign="Left"
                            ExpandUnusedSpace="true">
                            <Editor>
                                <f:NumberBox ID="txtRecordChartNo" runat="server">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:LinkButtonField Width="100px" HeaderText="热处理曲线" ConfirmTarget="Top" CommandName="attchUrl"
                            TextAlign="Center" ToolTip="上传/查看" Text="热处理曲线" />
                        <f:LinkButtonField Width="60px" TextAlign="Center" HeaderText="删除" ToolTip="删除" CommandName="del"
                            Icon="Delete" />
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
                        </f:DropDownList>
                    </PageItems>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close"
            IsModal="true" Width="900px" Height="380px">
        </f:Window>
        <f:Window ID="Window2" Title="文件查看" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="680px"
            Height="480px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
                Icon="Delete" ConfirmText="删除选中行" ConfirmTarget="Top"
                runat="server" Text="删除">
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

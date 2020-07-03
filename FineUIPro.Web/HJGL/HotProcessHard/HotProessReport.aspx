<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotProessReport.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.HotProcessHard.HotProessReport" %>

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
                EnableCollapse="true" runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="HotProessReportId"
                AllowCellEditing="true" ClicksToEdit="2" DataIDField="HotProessReportId" AllowSorting="true"
                SortField="WeldJointCode" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange" OnRowCommand="Grid1_RowCommand"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="true"
                                runat="server" OnClick="btnNew_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RenderField Width="120px" ColumnID="WeldJointCode" DataField="WeldJointCode" FieldType="String"
                        HeaderText="总焊口量" HeaderTextAlign="Center"
                        TextAlign="Left" SortField="WeldJointCode">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="PointCount" DataField="PointCount" FieldType="Int"
                        HeaderText="测温点编号" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="PointCount">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="RequiredT" DataField="RequiredT" FieldType="String"
                        HeaderText="热处理温度℃(要求)" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="ActualT" DataField="ActualT" FieldType="String"
                        HeaderText="热处理温度℃(实际)" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="RequestTime" DataField="RequestTime" FieldType="String"
                        HeaderText="恒温时间h（要求）" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="ActualTime" DataField="ActualTime" FieldType="String"
                        HeaderText="恒温时间h（实际）" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="RecordChartNo" DataField="RecordChartNo" FieldType="String"
                        HeaderText="记录曲线图编号" HeaderTextAlign="Center" TextAlign="Left"
                        ExpandUnusedSpace="true">
                    </f:RenderField>
                    <f:LinkButtonField Width="100px" HeaderText="附件查看" ConfirmTarget="Top" CommandName="attchUrl"
                        TextAlign="Center" ToolTip="上传/查看" Text="附件查看" />
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
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="BulletEdit" EnablePostBack="true"
            runat="server" Text="编辑">
        </f:MenuButton>
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

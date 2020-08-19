<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSEDiary.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Manager.HSEDiary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HSE日志</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="HSE日志" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="HSEDiaryId" DataIDField="HSEDiaryId" AllowSorting="true" ForceFit="true"
                    SortField="DiaryDate,UserName" SortDirection="DESC" EnableColumnLines="true" OnSort="Grid1_Sort" AllowPaging="true"
                    IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                    EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:DropDownList ID="drpUser" runat="server" Label="工程师" Width="350px" LabelWidth="80px"
                                    EnableEdit="true" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                                </f:DropDownList>
                                <f:DatePicker runat="server" ID="txtDiaryDate" Label="日期" LabelWidth="80px"
                                    AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                                </f:DatePicker>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                    EnableAjax="false" DisableControlBeforePostBack="false">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:TemplateField ColumnID="tfNumber" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                            TextAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField Width="110px" ColumnID="DiaryDate" DataField="DiaryDate" SortField="DiaryDate"
                            FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="日期"
                            HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="UserName" DataField="UserName"
                            SortField="UserName" FieldType="String" HeaderText="工程师" HeaderTextAlign="Center"
                            TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="220px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                            FieldType="String" HeaderText="申请单位" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="300px" ColumnID="DailySummary" DataField="DailySummary" SortField="DailySummary"
                            FieldType="String" HeaderText="当日小结" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="300px" ColumnID="TomorrowPlan" DataField="TomorrowPlan" SortField="TomorrowPlan"
                            FieldType="String" HeaderText="明日计划" HeaderTextAlign="Center" TextAlign="Left">
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
                        </f:DropDownList>
                    </PageItems>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="HSE日志" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
            Width="1000px" Height="650px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuView" OnClick="btnMenuView_Click" EnablePostBack="true"
                runat="server" Text="查看" Icon="Find">
            </f:MenuButton>
            <f:MenuButton ID="btnPrinter" EnablePostBack="true" runat="server"
                Text="导出" Icon="Printer" OnClick="btnPrinter_Click" EnableAjax="false" DisableControlBeforePostBack="false">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
                Hidden="true" Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server"
                Text="删除">
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

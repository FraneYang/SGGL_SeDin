<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthReport.aspx.cs" Inherits="FineUIPro.Web.HSSE.SitePerson.MonthReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人工时月报</title>
  <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="人工时月报" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="CompileDate" DataIDField="CompileDate" 
                AllowSorting="true" SortField="CompileDate" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"  ForceFit="true"
                SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" EnableColumnLines="true"                
                EnableTextSelection="True" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                            <f:DatePicker runat="server"  DateFormatString="yyyy-MM" Label="月报月份" EmptyText="请选择年月" 
                                ID="txtDate" LabelAlign="right" DisplayType="Month" ShowTodayButton="false" 
                                AutoPostBack="true"  OnTextChanged="txtDate_TextChanged">
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
                  <f:TemplateField ColumnID="tfPageIndex" Width="100px" HeaderText="序号" HeaderTextAlign="Center"
                        TextAlign="Center" EnableLock="true" Locked="False">
                        <ItemTemplate>
                            <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="150px" ColumnID="CompileDate" DataField="CompileDate" SortField="CompileDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM" HeaderText="日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>      
                    <f:RenderField Width="250px" ColumnID="DayWorkTime" DataField="DayWorkTime" SortField="DayWorkTime"
                        FieldType="Int"  HeaderText="当月人工时" HeaderTextAlign="Center" TextAlign="Right">
                    </f:RenderField>
                        <f:RenderField Width="250px" ColumnID="TotalPersonWorkTime" DataField="TotalPersonWorkTime" SortField="TotalPersonWorkTime"
                        FieldType="Int"  HeaderText="项目累计人工时" HeaderTextAlign="Center" TextAlign="Right">
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
    <f:Window ID="Window1" Title="人工时月报" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server"  IsModal="true"
        Width="1000px" Height="560px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnView" EnablePostBack="true" runat="server" Text="查看" Icon="Find" OnClick="btnView_Click">
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

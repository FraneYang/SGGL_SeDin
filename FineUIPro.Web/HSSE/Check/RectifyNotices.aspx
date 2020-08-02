<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RectifyNotices.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Check.RectifyNotices" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="~/Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>  
    <title>隐患整改通知单</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="隐患整改通知单" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="RectifyNoticesId" DataIDField="RectifyNoticesId" AllowSorting="true" 
                    SortField="RectifyNoticesCode" SortDirection="DESC" OnSort="Grid1_Sort" EnableColumnLines="true" AllowPaging="true"
                    IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" ForceFit="true"
                    EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:TextBox runat="server" Label="单位" ID="txtUnitName" EmptyText="输入查询条件"
                                    AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="50px"
                                    LabelAlign="right">
                                </f:TextBox>
                                <f:TextBox runat="server" Label="单位工程" ID="txtWorkAreaName" EmptyText="输入查询条件"
                                    AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="200px" LabelWidth="80px"
                                    LabelAlign="right">
                                </f:TextBox>
                                <f:TextBox runat="server" Label="编号" ID="txtRectifyNoticesCode" EmptyText="输入查询条件" Width="200px"
                                    AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelWidth="50px" LabelAlign="right">
                                </f:TextBox>
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:RadioButtonList runat="server" ID="rbStates" Width="500px" LabelWidth="60px"
                                    AutoPostBack="true" OnSelectedIndexChanged="rbStates_SelectedIndexChanged">
                                    <f:RadioItem Text="全部" Value="-1" Selected="true" />
                                    <f:RadioItem Text="待提交" Value="0" />
                                    <f:RadioItem Text="待签发" Value="1" />
                                    <f:RadioItem Text="待整改" Value="2" />
                                    <f:RadioItem Text="待审核" Value="3" />
                                    <f:RadioItem Text="待复审" Value="4" />
                                    <f:RadioItem Text="已完成" Value="5" />
                                </f:RadioButtonList>
                                <f:ToolbarSeparator ID="ToolbarFill2" runat="server">
                                </f:ToolbarSeparator>
                                <f:RadioButtonList runat="server" ID="rbrbHiddenHazardType" Width="300px" Label="隐患类别" LabelWidth="80px"
                                    AutoPostBack="true" OnSelectedIndexChanged="rbStates_SelectedIndexChanged">
                                    <f:RadioItem Text="全部" Value="-1" Selected="true" />
                                    <f:RadioItem Text="一般" Value="1" />
                                    <f:RadioItem Text="较大" Value="2" />
                                    <f:RadioItem Text="重大" Value="3" />
                                </f:RadioButtonList>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                    Hidden="true">
                                </f:Button>
                              <%--  <f:Button ID="btnPrint" ToolTip="打印" Icon="Printer" Hidden="true" runat="server"
                                    OnClick="btnPrint_Click">
                                </f:Button>--%>
                                <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                    EnableAjax="false" DisableControlBeforePostBack="false">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:RenderField Width="100px" ColumnID="RectifyNoticesCode" DataField="RectifyNoticesCode"
                            SortField="RectifyNoticesCode" FieldType="String" HeaderText="单号" TextAlign="Left"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="150px" ColumnID="WorkAreaName" DataField="WorkAreaName"
                            SortField="WorkAreaName" FieldType="String" HeaderText="单位工程" TextAlign="Left"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="250px" ColumnID="UnitName" DataField="UnitName" ExpandUnusedSpace="true"
                            SortField="UnitName" FieldType="String" HeaderText="受检单位名称" TextAlign="Left" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:TemplateField ColumnID="CheckManNames" Width="180px" HeaderText="检查人" HeaderTextAlign="Center" TextAlign="Left"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# ConvertCheckPerson(Eval("CheckManIds")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField Width="100px" ColumnID="CheckedDate" DataField="CheckedDate" SortField="CheckedDate"
                            FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="检查日期"
                            HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="110px" ColumnID="DutyPersonName" DataField="DutyPersonName"
                            SortField="DutyPersonName" FieldType="String" HeaderText="整改责任人"
                            TextAlign="Left" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="CompleteDate" DataField="CompleteDate" SortField="CompleteDate"
                            FieldType="Date" Renderer="Date" HeaderText="整改日期"
                            HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="145px" ColumnID="StatesName" DataField="StatesName"
                            SortField="StatesName" FieldType="String" HeaderText="状态"
                            TextAlign="Left" HeaderTextAlign="Center">
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
        <f:Window ID="Window1" Title="编辑隐患整改通知单" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
            Width="1100px" Height="560px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <Items>
		        <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Hidden="true"
                    Text="编辑" Icon="Pencil" OnClick="btnMenuModify_Click">
                </f:MenuButton>
                 <f:MenuButton ID="btnView" EnablePostBack="true" runat="server" 
                    Text="查看" Icon="Find" OnClick="btnView_Click">
                </f:MenuButton>
                 <f:MenuButton ID="btnPrinter" EnablePostBack="true" runat="server" 
                    Text="导出[通知单]" Icon="Printer" OnClick="btnPrinter_Click" EnableAjax="false" DisableControlBeforePostBack="false">
                </f:MenuButton>
                <f:MenuButton ID="btnPrinterReturn" EnablePostBack="true" runat="server" 
                    Text="导出[反馈单]" Icon="Printer" OnClick="btnPrinterReturn_Click" EnableAjax="false" DisableControlBeforePostBack="false">
                </f:MenuButton>
                <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server" Hidden="true"
                    Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？" OnClick="btnMenuDel_Click">
                </f:MenuButton>
            </Items>
        </f:Menu>
    </form>
    <script type="text/javascript">
        var menuID = '<%= Menu1.ClientID %>';

        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>
</body>
</html>

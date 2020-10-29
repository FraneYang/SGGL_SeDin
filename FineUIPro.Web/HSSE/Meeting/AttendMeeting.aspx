<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttendMeeting.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Meeting.AttendMeeting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>其他会议记录</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="其他会议记录" EnableCollapse="true"
                runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="AttendMeetingId"
                 DataIDField="AttendMeetingId" AllowSorting="true" ForceFit="true"
                SortField="AttendMeetingCode" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox runat="server" Label="编号" ID="txtAttendMeetingCode" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:DropDownList ID="drpUnitId" runat="server" Label="单位" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged"  LabelWidth="70px" Width="250px">
                            </f:DropDownList>  
                            <f:TextBox runat="server" Label="名称" ID="txtAttendMeetingName" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" Hidden="true"
                                runat="server">
                            </f:Button>                   
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:TemplateField ColumnID="tfNumber" Width="55px" HeaderText="序号" HeaderTextAlign="Center"
                        TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="140px" ColumnID="AttendMeetingCode" DataField="AttendMeetingCode"
                        SortField="AttendMeetingCode" FieldType="String" HeaderText="编号" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="180px" ColumnID="AttendMeetingName" DataField="AttendMeetingName"
                        SortField="AttendMeetingName" FieldType="String" HeaderText="名称" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="250px" ColumnID="UnitName" DataField="UnitName"
                        SortField="UnitName" FieldType="String" HeaderText="单位" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="AttendMeetingDate" DataField="AttendMeetingDate"
                        SortField="AttendMeetingDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                        HeaderText="会议日期" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:TemplateField ColumnID="tfCompileMan" Width="120px" HeaderText="整理人" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblCompileMan" runat="server" Text='<%# ConvertCompileMan(Eval("CompileMan")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="140px" ColumnID="FlowOperateName" DataField="FlowOperateName"
                        SortField="FlowOperateName" FieldType="String" HeaderText="状态" HeaderTextAlign="Center"
                        TextAlign="Left">
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
    <f:Window ID="Window1" Title="其他会议记录" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1024px" Height="620px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="Pencil" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑">
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

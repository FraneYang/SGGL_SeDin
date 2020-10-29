<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpecialMeeting.aspx.cs" Inherits="FineUIPro.Web.HSSE.Meeting.SpecialMeeting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全专题会议</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />  
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全专题会议" EnableCollapse="true"
                runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="SpecialMeetingId"
                 DataIDField="SpecialMeetingId" AllowSorting="true" ForceFit="true"
                SortField="SpecialMeetingCode" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox runat="server" Label="编号" ID="txtSpecialMeetingCode" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:DropDownList ID="drpUnitId" runat="server" Label="单位" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged"  LabelWidth="70px" Width="250px">
                            </f:DropDownList>  
                            <f:TextBox runat="server" Label="名称" ID="txtSpecialMeetingName" EmptyText="输入查询条件"
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
                    <f:RenderField Width="100px" ColumnID="SpecialMeetingCode" DataField="SpecialMeetingCode"
                        SortField="SpecialMeetingCode" FieldType="String" HeaderText="编号" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="SpecialMeetingName" DataField="SpecialMeetingName"
                        SortField="SpecialMeetingName" FieldType="String" HeaderText="名称" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="UnitName" DataField="UnitName"
                        SortField="UnitName" FieldType="String" HeaderText="单位" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="SpecialMeetingDate" DataField="SpecialMeetingDate"
                        SortField="SpecialMeetingDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                        HeaderText="会议日期" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="MeetingHours" DataField="MeetingHours"
                        SortField="MeetingHours" FieldType="Int" HeaderText="时数" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="MeetingHostMan" DataField="MeetingHostMan"
                        SortField="MeetingHostMan" FieldType="String" HeaderText="主持人" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="AttentPersonNum" DataField="AttentPersonNum"
                        SortField="AttentPersonNum" FieldType="Int" HeaderText="参会人数" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                 <%--   <f:RenderField Width="90px" ColumnID="AttentPerson" DataField="AttentPerson"
                        SortField="AttentPerson" FieldType="String" HeaderText="参会人员" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>--%>
              <%--      <f:TemplateField ColumnID="tfCompileMan" Width="80px" HeaderText="整理人" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblCompileMan" runat="server" Text='<%# ConvertCompileMan(Eval("CompileMan")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>--%>
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
    <f:Window ID="Window1" Title="安全专题会议" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1024px" Height="630px">
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

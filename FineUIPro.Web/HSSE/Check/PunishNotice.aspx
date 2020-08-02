<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PunishNotice.aspx.cs" Inherits="FineUIPro.Web.HSSE.Check.PunishNotice" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>处罚通知单</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="处罚通知单" EnableCollapse="true"
                    runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="PunishNoticeId" ForceFit="true"
                    DataIDField="PunishNoticeId" AllowSorting="true" SortField="PunishNoticeCode" SortDirection="DESC" OnSort="Grid1_Sort"
                    AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                    EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:RadioButtonList runat="server" ID="rbStates" Width="450px" LabelWidth="60px"
                                    AutoPostBack="true" OnSelectedIndexChanged="rbStates_SelectedIndexChanged">
                                    <f:RadioItem Text="全部" Value="-1" Selected="true" />
                                    <f:RadioItem Text="待提交" Value="0" />
                                    <f:RadioItem Text="待签发" Value="1" />
                                    <f:RadioItem Text="待批准" Value="2" />
                                    <f:RadioItem Text="待回执" Value="3" />
                                    <f:RadioItem Text="已闭环" Value="4" />
                                </f:RadioButtonList>
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:DropDownList ID="drpUnitId" runat="server" Label="单位" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged" LabelWidth="70px" Width="350px">
                                </f:DropDownList>
                                <f:TextBox runat="server" Label="编号" ID="txtPunishNoticeCode" EmptyText="输入查询条件"
                                    AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="210px" LabelWidth="60px"
                                    LabelAlign="right">
                                </f:TextBox>
                                <f:DatePicker ID="txtStartDate" runat="server" Label="处罚时间" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"
                                    Width="220px" EmptyText="开始时间" LabelAlign="Right">
                                </f:DatePicker>
                                <f:Label ID="lblTo" runat="server" Text="至">
                                </f:Label>
                                <f:DatePicker ID="txtEndDate" runat="server" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"
                                    Width="120px" EmptyText="结束时间">
                                </f:DatePicker>
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
                        <f:TemplateField ColumnID="tfNumber" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                            TextAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField Width="100px" ColumnID="PunishNoticeCode" DataField="PunishNoticeCode"
                            SortField="PunishNoticeCode" FieldType="String" HeaderText="编号" HeaderTextAlign="Center"
                            TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="240px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                            FieldType="String" HeaderText="处罚单位" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="PunishNoticeDate" DataField="PunishNoticeDate"
                            SortField="PunishNoticeDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                            HeaderText="处罚时间" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="220px" ColumnID="BasicItem" DataField="BasicItem" SortField="BasicItem"
                            FieldType="String" HeaderText="处罚依据" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="90px" ColumnID="PunishMoney" DataField="PunishMoney" SortField="PunishMoney"
                            FieldType="Float" HeaderText="处罚金额" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <%--   <f:WindowField TextAlign="Center" Width="70px" WindowID="WindowAtt" Text="附件" HeaderText="通知单"
                        ToolTip="通知单上传查看" DataIFrameUrlFields="PunishNoticeId" DataIFrameUrlFormatString="~/AttachFile/webuploader.aspx?toKeyId={0}&type=-1&path=FileUpload/PunishNoticeStatistics&menuId=CCD0E55B-300A-454B-8559-155ADAD386AE"/>       
                    <f:RenderField Width="110px" ColumnID="FlowOperateName" DataField="FlowOperateName"
                        SortField="FlowOperateName" FieldType="String" HeaderText="状态" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:WindowField TextAlign="Center" Width="70px" WindowID="WindowAtt" Text="附件" HeaderText="回执单"
                        ToolTip="回执单上传查看" DataIFrameUrlFields="PunishNoticeId" DataIFrameUrlFormatString="~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PunishNotice&menuId=755F1C1D-2178-47D8-9F82-A501B53A2436"/>
                        --%>
                        <f:RenderField Width="130px" ColumnID="RetrunSateName" DataField="RetrunSateName"
                            SortField="RetrunSateName" FieldType="String" HeaderText="状态" HeaderTextAlign="Center"
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
        <f:Window ID="Window1" Title="处罚通知单" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="1000px"
            Height="600px" OnClose="Window1_Close">
        </f:Window>
        <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Self" EnableResize="true" runat="server" IsModal="true" Width="700px" OnClose="WindowAtt_Close"
            Height="500px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="Pencil" EnablePostBack="true"
                Hidden="true" runat="server" Text="编辑">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuView" EnablePostBack="true" runat="server" Text="查看" Icon="ApplicationViewIcons" OnClick="btnMenuView_Click">
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

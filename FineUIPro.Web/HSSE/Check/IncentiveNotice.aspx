<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IncentiveNotice.aspx.cs" Inherits="FineUIPro.Web.HSSE.Check.IncentiveNotice" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>奖励通知单</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="奖励通知单" EnableCollapse="true"
                    runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="IncentiveNoticeId" ForceFit="true"
                    DataIDField="IncentiveNoticeId" AllowSorting="true" SortField="IncentiveNoticeCode" SortDirection="DESC"
                    OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                    EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:TextBox runat="server" Label="编号" ID="txtIncentiveNoticeCode" EmptyText="输入查询条件"
                                    AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="210px" LabelWidth="60px"
                                    LabelAlign="right">
                                </f:TextBox>
                                <f:DropDownList ID="drpUnitId" runat="server" Label="单位" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged" LabelWidth="70px" Width="250px">
                                </f:DropDownList>
                                <f:DatePicker ID="txtStartDate" runat="server" Label="奖励时间" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"
                                    Width="250px" EmptyText="开始时间" LabelAlign="Right">
                                </f:DatePicker>
                                <f:Label ID="lblTo" runat="server" Text="至">
                                </f:Label>
                                <f:DatePicker ID="txtEndDate" runat="server" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"
                                    Width="150px" EmptyText="结束时间">
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
                        <f:RenderField Width="100px" ColumnID="IncentiveNoticeCode" DataField="IncentiveNoticeCode"
                            SortField="IncentiveNoticeCode" FieldType="String" HeaderText="编号" HeaderTextAlign="Center"
                            TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="250px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                            FieldType="String" HeaderText="受奖单位" HeaderTextAlign="Center" TextAlign="Left" >
                        </f:RenderField>
                        <f:RenderField Width="150px" ColumnID="RewardTypeName" DataField="RewardTypeName" SortField="RewardTypeName"
                            FieldType="String" HeaderText="奖励类型" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="IncentiveMoney" DataField="IncentiveMoney" SortField="IncentiveMoney"
                            FieldType="Float" HeaderText="奖励金额" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="70px" ColumnID="Currency" DataField="Currency" SortField="Currency"
                            FieldType="String" HeaderText="币种" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="IncentiveDate" DataField="IncentiveDate"
                            SortField="IncentiveDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                            HeaderText="奖励时间" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="150px" ColumnID="FlowOperateName" DataField="FlowOperateName"
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
        <f:Window ID="Window1" Title="奖励通知单" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="1100px"
            Height="520px">
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

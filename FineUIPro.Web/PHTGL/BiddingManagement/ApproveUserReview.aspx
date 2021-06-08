<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproveUserReview.aspx.cs" Inherits="FineUIPro.Web.PHTGL.BiddingManagement.ApproveUserReview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="评标小组名单审批" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="ApproveUserReviewID" AllowCellEditing="true"
                    ClicksToEdit="2" DataIDField="ApproveUserReviewID" AllowSorting="true" OnSort="Grid1_Sort"
                    SortDirection="DESC" EnableColumnLines="true" OnRowDoubleClick="Grid1_RowDoubleClick" OnPageIndexChange="Grid1_PageIndexChange"
                    AllowPaging="true" IsDatabasePaging="true" PageSize="10" ForceFit="true" OnRowCommand="Grid1_RowCommand"
                    EnableRowDoubleClickEvent="true"
                    EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:TextBox runat="server" Label="招标文件编号" ID="txtBidDocumentsCode" EmptyText="输入查询条件" Width="300px" LabelWidth="140px"
                                    LabelAlign="left">
                                </f:TextBox>
                                <f:DropDownList runat="server" ID="DropState" Label="审批状态"></f:DropDownList>

                                <f:ToolbarFill runat="server"></f:ToolbarFill>
                                <f:Button ID="btnQuery" OnClick="btnSearch_Click" ToolTip="查询" Text="查询" Icon="SystemSearch" EnablePostBack="true" runat="server">
                                </f:Button>
                                <f:Button ID="btnRset" OnClick="btnRset_Click" ToolTip="重置" Text="重置" Icon="ArrowUndo" EnablePostBack="true" runat="server">
                                </f:Button>
                                <f:Button ID="btnNew" ToolTip="新增" Icon="Add" Text="新增" EnablePostBack="false" runat="server"
                                    Hidden="true">
                                </f:Button>
                                <f:Button ID="btnMenuDelete" EnablePostBack="true" runat="server" Hidden="true" Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？"
                                    OnClick="btnMenuDelete_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar ID="Toolbar3" Position="Top" runat="server" ToolbarAlign="Right">
                            <Items>
                                <f:Button ID="btnPrinter" EnablePostBack="true" runat="server"
                                    Text="导出" Icon="Printer" OnClick="btnPrinter_Click" EnableAjax="false" DisableControlBeforePostBack="true">
                                </f:Button>
                                <f:Button ID="btnMenuEdit" EnablePostBack="true" runat="server" Hidden="true" Text="编辑审批流" Icon="Pencil"
                                    OnClick="btnMenuEdit_Click">
                                </f:Button>
                                <f:Button ID="btnQueryApprove" OnClick="btnQueryApprove_Click" ToolTip="查询/进行审批" Text="查询/进行审批" Icon="SystemSearch" EnablePostBack="true" runat="server">
                                </f:Button>


                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:TemplateField ColumnID="tfPageIndex" Width="55px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField ColumnID="BidDocumentsCode" DataField="BidDocumentsCode" Width="120px" FieldType="String" HeaderText="招标文件编号" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                         <f:RenderField ColumnID="BidProject" DataField="BidProject" Width="120px" FieldType="String" HeaderText="招标项目" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="ProjectName" DataField="ProjectName" Width="120px" FieldType="String" HeaderText="项目名称" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="ProjectCode" DataField="ProjectCode" Width="120px" FieldType="String" HeaderText="项目编号" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="State" DataField="State" Width="120px" FieldType="String" HeaderText="审批状态" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="CreateUser" DataField="CreateUser" Width="180px" FieldType="String" HeaderText="创建人" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:LinkButtonField HeaderText="查看" ColumnID="LooK" Width="60px" Icon="Zoom" CommandName="LooK" />

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
                            <f:ListItem Text="所有行" Value="100000" />
                        </f:DropDownList>
                    </PageItems>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="评标小组名单审批" Hidden="true" EnableIFrame="true" EnableMaximize="true" Maximized="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" OnClose="Window1_Close"
            Width="1000px" Height="420px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <Items>
                <%--     <f:MenuButton ID="btnMenuEdit" EnablePostBack="true" runat="server" Hidden="true" Text="编辑" Icon="Pencil"
                    OnClick="btnMenuEdit_Click">
                </f:MenuButton>
                <f:MenuButton ID="btnMenuDelete" EnablePostBack="true" runat="server" Hidden="true" Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？"
                    OnClick="btnMenuDelete_Click">
                </f:MenuButton>
                <f:MenuButton ID="btnPrinter" EnablePostBack="true" runat="server"
                Text="导出" Icon="Printer" OnClick="btnPrinter_Click" EnableAjax="false" DisableControlBeforePostBack="false">
            </f:MenuButton>--%>
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

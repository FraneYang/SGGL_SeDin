﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActionPlanFormation.aspx.cs" Inherits="FineUIPro.Web.PHTGL.BiddingManagement.ActionPlanFormation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>实施计划编制</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="实施计划编制" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="ActionPlanID" AllowCellEditing="true"
                    ClicksToEdit="2" DataIDField="ActionPlanID" AllowSorting="true" SortField="CreateTime" OnSort="Grid1_Sort"
                    SortDirection="DESC" EnableColumnLines="true" OnRowDoubleClick="Grid1_RowDoubleClick" OnPageIndexChange="Grid1_PageIndexChange"
                    AllowPaging="true" IsDatabasePaging="true" PageSize="10" ForceFit="true" OnRowCommand="Grid1_RowCommand" OnRowClick="Grid1_RowClick" EnableRowClickEvent="true"
                    EnableRowDoubleClickEvent="true"
                    EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:TextBox runat="server" Label="实施计划编号" ID="txtActionPlanCode" EmptyText="输入查询条件" Width="300px" LabelWidth="140px"
                                    LabelAlign="left">
                                </f:TextBox>
                                 <f:DropDownList runat="server" ID="DropState" Label="审批状态"></f:DropDownList>

                                <f:ToolbarFill runat="server"></f:ToolbarFill>
                                <f:Button ID="btnQuery" OnClick="btnSearch_Click" ToolTip="查询" Text="查询" Icon="SystemSearch" EnablePostBack="true" runat="server">
                                </f:Button>
                                <f:Button ID="btnRset" OnClick="btnRset_Click" ToolTip="重置" Text="重置" Icon="ArrowUndo" EnablePostBack="true" runat="server">
                                </f:Button>
                                <f:Button ID="btnNew" ToolTip="新增" Icon="Add" Text="新增"  EnablePostBack="false" runat="server"
                                    Hidden="true">
                                </f:Button>
                                <f:Button ID="btnDelete" EnablePostBack="true" runat="server"  Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？"
                                    OnClick="btnMenuDelete_Click">
                                </f:Button>
                             </Items>
                        </f:Toolbar>
                        <f:Toolbar ID="Toolbar1" runat="server" ToolbarAlign="Right">
                            <Items>
                                 <f:Button ID="MenuButton3" EnablePostBack="true" runat="server" Hidden="true"
                                    Text="导出" Icon="Printer" OnClick="btnPrinter_Click" EnableAjax="false" DisableControlBeforePostBack="true">
                                </f:Button>
                                 <f:Button ID="btnEditAgain" EnablePostBack="true" runat="server" Hidden="true"   Text="重新编辑" Icon="Pencil" ConfirmText="是否重新编辑"
                                    OnClick="btnEditAgain_Click">
                                </f:Button>
                                <f:Button ID="btnEdit" EnablePostBack="true" runat="server"    Text="编辑" Icon="Pencil"
                                    OnClick="btnMenuEdit_Click">
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
                        <f:RenderField ColumnID="ActionPlanCode" DataField="ActionPlanCode" Width="220px" FieldType="String" HeaderText="实施计划编号" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="Name" DataField="Name" Width="200px" FieldType="String" HeaderText="项目名称" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="EPCCode" DataField="EPCCode" Width="200px" FieldType="String" HeaderText="总承包合同编号" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="State" DataField="State" Width="120px" FieldType="String" HeaderText="状态" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="BidProject" DataField="BidProject" Width="200px" FieldType="String" HeaderText="招标工程" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="BidType" DataField="BidType" Width="120px" FieldType="String" HeaderText="招标方式" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="PriceType" DataField="PriceType" Width="180px" FieldType="String" HeaderText="计价方式" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="CreatUser" DataField="CreatUser" Width="150px" FieldType="String" HeaderText="创建人" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                      <%--  <f:RenderField Width="120px" ColumnID="CreateTime" DataField="CreateTime" FieldType="Date"
                            Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="创建日期">
                        </f:RenderField>--%>
                        <f:LinkButtonField HeaderText="查看" ColumnID="LooK" Width="60px" Icon="Zoom" CommandName="LooK" />

<%--                        <f:LinkButtonField ColumnID="export" HeaderText="导出" Width="60px" Icon="ArrowUp" CommandName="export" />--%>
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
        <f:Window ID="Window1" Title="招标实施计划" Hidden="true" EnableIFrame="true" EnableMaximize="true" Maximized="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="false" OnClose="Window1_Close"
            Width="1200px" Height="650px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
           <%-- <Items>
                <f:MenuButton ID="btnMenuEdit" EnablePostBack="true" runat="server" Hidden="true" Text="编辑" Icon="Pencil"
                    OnClick="btnMenuEdit_Click">
                </f:MenuButton>
                <f:MenuButton ID="btnMenuDelete" EnablePostBack="true" runat="server" Hidden="true" Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？"
                    OnClick="btnMenuDelete_Click">
                </f:MenuButton>
                <f:MenuButton ID="btnPrinter" EnablePostBack="true" runat="server"
                    Text="导出" Icon="Printer" OnClick="btnPrinter_Click" EnableAjax="false" DisableControlBeforePostBack="false">
                </f:MenuButton>
            </Items>--%>
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

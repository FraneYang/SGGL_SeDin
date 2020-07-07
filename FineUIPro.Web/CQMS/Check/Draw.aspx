<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Draw.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.Draw" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .f-grid-colheader-text {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>

                <f:Grid ID="Grid1" ShowBorder="true" EnableAjax="false" ShowHeader="false" Title="施工图纸" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="DrawId" AllowCellEditing="true"
                    ClicksToEdit="2" DataIDField="DrawId" AllowSorting="true" SortField="DrawId"
                    SortDirection="DESC" EnableColumnLines="true" ForceFit="true"
                    AllowPaging="true" IsDatabasePaging="true" PageSize="10"
                    EnableRowDoubleClickEvent="true" AllowFilters="true" EnableTextSelection="True" OnRowDoubleClick="Grid1_RowDoubleClick" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:TextBox ID="txtDrawCode" runat="server" Label="图号" Width="230px" LabelWidth="70px"
                                    LabelAlign="Right">
                                </f:TextBox>
                                <f:TextBox ID="txtDrowName" runat="server" Label="图纸名称" Width="270px" LabelWidth="100px"
                                    LabelAlign="Right">
                                </f:TextBox>
                                

                            </Items>
                        </f:Toolbar>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:DropDownList ID="drpMainItem" runat="server" Label="主项号" Width="230px" LabelAlign="Right" EnableEdit="true" LabelWidth="70px">
                                </f:DropDownList>
                                <f:DropDownList ID="drpDesignCN" runat="server" Label="设计专业" Width="270px" LabelAlign="Right" EnableEdit="true" LabelWidth="100px">
                                </f:DropDownList>
                                <f:TextBox ID="txtEdition" runat="server" Label="版次" Width="220px" LabelWidth="50px"
                                    LabelAlign="Right">
                                </f:TextBox>
                                <f:ToolbarFill runat="server"></f:ToolbarFill>
                                <f:Button ID="btnSearch" Icon="SystemSearch"
                                    EnablePostBack="true" runat="server" OnClick="btnSearch_Click">
                                </f:Button>
                                <f:Button ID="btnRset" OnClick="btnRset_Click" ToolTip="重置" Icon="ArrowUndo" EnablePostBack="true" runat="server">
                                </f:Button>
                                <f:Button ID="btnNew" Icon="Add" EnablePostBack="true"
                                    runat="server">
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
                        <%--<f:WindowField ColumnID="DrawCode" HeaderTextAlign="Center" TextAlign="Center"
                            Width="90px" WindowID="Window1" HeaderText="图号"
                            DataTextField="DrawCode" DataIFrameUrlFields="DrawId" DataIFrameUrlFormatString="DrawEdit.aspx?DrawId={0}"
                            Title="图号" DataToolTipField="DrawCode" SortField="DrawCode" Locked="true">
                        </f:WindowField>--%>
                        <f:RenderField Width="180px" ColumnID="DrawCode" DataField="DrawCode"
                            FieldType="String" HeaderText="图纸号" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="180px" ColumnID="DrawName" DataField="DrawName"
                            FieldType="String" HeaderText="图纸名称" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="180px" ColumnID="MainItem" DataField="MainItem"
                            FieldType="String" HeaderText="主项" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="180px" ColumnID="DesignCN" DataField="DesignCN"
                            FieldType="String" HeaderText="设计专业" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="180px" ColumnID="Edition" DataField="Edition"
                            FieldType="String" HeaderText="版次" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="180px" ColumnID="AcceptDate" DataField="AcceptDate" SortField="AcceptDate" FieldType="Date" HeaderText="接收日期" TextAlign="Center" HeaderTextAlign="Center" RendererArgument="yyyy-MM-dd">
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
                        <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                            <f:ListItem Text="10" Value="10" />
                            <f:ListItem Text="15" Value="15" />
                            <f:ListItem Text="20" Value="20" />
                            <f:ListItem Text="25" Value="25" />
                            <f:ListItem Text="所有行" Value="100000" />
                        </f:DropDownList>
                        <f:ToolbarFill runat="server">
                        </f:ToolbarFill>

                    </PageItems>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="施工图纸" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
            Width="800px" Height="460px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <Items>
                <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Text="修改" Icon="Pencil" OnClick="btnMenuModify_Click" Hidden="true">
                </f:MenuButton>
                <f:MenuButton ID="btnMenuView" EnablePostBack="true" runat="server" Text="查看" Icon="ApplicationViewIcons" OnClick="btnMenuView_Click">
                </f:MenuButton>
                <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server" Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？"
                    OnClick="btnMenuDel_Click" Hidden="true">
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

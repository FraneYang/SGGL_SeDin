<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileCabinet.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.FileCabinet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>文件柜</title>
    <style>
        .item {
            display: inline-block;
            width: 33.3333%;
            box-sizing: border-box;
        }

        .x-grid-tpl {
            white-space: normal;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Title="重要文件" ShowBorder="false" EnableCollapse="false"
            Layout="VBox" Margin="5px" BodyPadding="5px" ShowHeader="false">
            <Items>
                <f:Grid ID="gvFile" ShowBorder="true" EnableAjax="false" ShowHeader="false" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="FileCabinetId" AllowCellEditing="true"
                    ClicksToEdit="2" DataIDField="FileCabinetId" AllowSorting="true" SortField="FileCode"
                    SortDirection="DESC" EnableColumnLines="true" OnRowCommand="gvFile_RowCommand" ForceFit="true"
                    AllowPaging="true" IsDatabasePaging="true" PageSize="10" EnableTextSelection="True">
                    <Toolbars>

                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                            <Items>
                                <f:Button ID="btnNew" Icon="Add" Hidden="true" runat="server" ToolTip="增加">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:TemplateField ColumnID="tfPageIndex" Width="55px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="lblPageIndex" runat="server" Text='<%# gvFile.PageIndex * gvFile.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField Width="100px" ColumnID="FileDate" DataField="FileDate" SortField="FileDate"
                            FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="日期" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>

                        <f:RenderField Width="200px" ColumnID="FileCode" DataField="FileCode"
                            SortField="FileCode" FieldType="String" HeaderText="编号" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>

                        <f:RenderField Width="120px" ColumnID="FileContent" DataField="FileContent"
                            SortField="FileContent" FieldType="String" HeaderText="事由" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="90px" ColumnID="CreateManName" DataField="CreateManName"
                            SortField="CreateManName" FieldType="String" HeaderText="管理人" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:LinkButtonField HeaderText="附件" ColumnID="download" Width="60px" Icon="ArrowDown" CommandName="download" />

                    </Columns>
                    <Listeners>
                        <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                    </Listeners>
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                        </f:ToolbarText>
                        <f:DropDownList runat="server" ID="ddlPageSize" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" Width="80px" AutoPostBack="true">
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

        <f:Menu ID="Menu1" runat="server">
            <Items>

                <f:MenuButton ID="btnMenuView" EnablePostBack="true" runat="server" Text="查看" Icon="ApplicationViewIcons"
                    OnClick="btnMenuView_Click">
                </f:MenuButton>
                <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Hidden="true" Text="修改" Icon="Pencil"
                    OnClick="btnMenuModify_Click">
                </f:MenuButton>
                <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server" Hidden="true" Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？"
                    OnClick="btnMenuDel_Click">
                </f:MenuButton>
            </Items>
        </f:Menu>

    </form>
    <f:Window ID="Windowtt" Title="文件柜" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true"
        Width="700px" Height="500px">
    </f:Window>
    <f:Window ID="Window1" Title="文件柜" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true"
        Width="700px" Height="400px" OnClose="Window1_Close">
    </f:Window>
    <f:Window ID="windows_tt" Title="文件柜" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true"
        Width="1300px" Height="400px">
    </f:Window>
    <script>
        var menuID = '<%= Menu1.ClientID %>';
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

    </script>
</body>
</html>

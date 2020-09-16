<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PauseNotice.aspx.cs" Inherits="FineUIPro.Web.HSSE.Check.PauseNotice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>工程暂停令</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="工程暂停令" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="PauseNoticeId" ForceFit="true"
                DataIDField="PauseNoticeId" AllowSorting="true" SortField="PauseNoticeCode"
                SortDirection="DESC" OnSort="Grid1_Sort" EnableColumnLines="true" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                              <f:RadioButtonList runat="server" ID="rbStates" Width="450px" 
                                 AutoPostBack="true" OnSelectedIndexChanged="rbStates_SelectedIndexChanged">
                                <f:RadioItem Text="全部" Value="-1" Selected="true" />
                                <f:RadioItem Text="待提交" Value="0" />
                                <f:RadioItem Text="待签发" Value="1" />
                                <f:RadioItem Text="待批准" Value="2" />
                                <f:RadioItem Text="待接收" Value="3" />
                                <f:RadioItem Text="已闭环" Value="4" />
                            </f:RadioButtonList>  
                            <f:TextBox runat="server" Label="编号" ID="txtPauseNoticeCode" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelWidth="50px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                Hidden="true">
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
                    <f:RenderField Width="140px" ColumnID="PauseNoticeCode" DataField="PauseNoticeCode"
                        SortField="PauseNoticeCode" FieldType="String" HeaderText="编号" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="260px" ColumnID="UnitName" DataField="UnitName"
                        SortField="UnitName" FieldType="String" HeaderText="单位名称" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="ProjectPlace" DataField="ProjectPlace" ExpandUnusedSpace="true"
                        SortField="ProjectPlace" FieldType="String" HeaderText="单位工程" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="PauseTime" DataField="PauseTime" SortField="PauseTime"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd HH:mm" HeaderText="停工日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
     <%--               <f:RenderField Width="150px" ColumnID="IsConfirmStr" DataField="IsConfirmStr" FieldType="String"
                        HeaderText="是否签字确认" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>--%>
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
    <f:Window ID="Window1" Title="编辑工程暂停令" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
        Width="1000px" Height="550px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <Items>
            <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Hidden="true"
                Text="修改" Icon="Pencil" OnClick="btnMenuModify_Click">
            </f:MenuButton>
            <%--<f:MenuButton ID="btnMenuConfirm" EnablePostBack="true" runat="server" Hidden="true"
                Text="确认" Icon="Accept" OnClick="btnMenuConfirm_Click">
            </f:MenuButton>--%>
            <f:MenuButton ID="btnMenuView" EnablePostBack="true" runat="server" Text="查看" Icon="ApplicationViewIcons" OnClick="btnMenuView_Click">
                </f:MenuButton>
            <f:MenuButton ID="btnPrinter" EnablePostBack="true" runat="server"
                Text="导出" Icon="Printer" OnClick="btnPrinter_Click" EnableAjax="false" DisableControlBeforePostBack="false">
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

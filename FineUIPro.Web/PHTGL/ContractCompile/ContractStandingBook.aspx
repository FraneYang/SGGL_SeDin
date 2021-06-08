<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractStandingBook.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.ContractStandingBook" %>

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
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="台账" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="ContractId" AllowCellEditing="true"
                    ClicksToEdit="2" DataIDField="ContractId" AllowSorting="true" SortField="ContractId" OnSort="Grid1_Sort"
                    SortDirection="DESC" EnableColumnLines="true" OnPageIndexChange="Grid1_PageIndexChange"
                    AllowPaging="true" IsDatabasePaging="true" PageSize="10"
                    EnableRowDoubleClickEvent="true"
                    EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:TextBox runat="server" Label="总承包合同编号" ID="txtProjectCode" EmptyText="输入查询条件" Width="300px" LabelWidth="120px"
                                    LabelAlign="left">
                                </f:TextBox>
                                <f:TextBox runat="server" Label="项目简称" ID="txtShortName" EmptyText="输入查询条件" Width="300px" LabelWidth="90px"
                                    LabelAlign="left">
                                </f:TextBox>
                                <%-- <f:TextBox runat="server" Label="项目代码" ID="txtProjectCode2" EmptyText="输入查询条件" Width="300px" LabelWidth="90px"
                                    LabelAlign="right">
                                </f:TextBox>--%>
                                <f:TextBox runat="server" Label="合同编号" ID="txtContractNum" EmptyText="输入查询条件" Width="300px" LabelWidth="90px"
                                    LabelAlign="left">
                                </f:TextBox>
                                <f:TextBox runat="server" Label="施工合同名称" ID="txtContractName" EmptyText="输入查询条件" Width="300px" LabelWidth="110px"
                                    LabelAlign="left">
                                </f:TextBox>
                                <f:TextBox runat="server" Label="施工分包商" ID="txtSubConstruction" EmptyText="输入查询条件" Width="300px" LabelWidth="90px"
                                    LabelAlign="left">
                                </f:TextBox>
                                <%-- <f:DropDownList ID="drpStates" runat="server" Label="状态" AutoPostBack="true" OnSelectedIndexChanged="btnSearch_Click"
                                    LabelWidth="70px" LabelAlign="Right" Width="170px">
                                </f:DropDownList>--%>
                                <f:Button ID="btnSearch" ToolTip="查询" Icon="SystemSearch" runat="server" OnClick="btnSearch_Click">
                                </f:Button>
                                <f:ToolbarFill runat="server"></f:ToolbarFill>

                                <f:Button ID="Button1" ToolTip="重置" Icon="ArrowUndo" runat="server" OnClick="btnReset_Click">
                                </f:Button>
                                <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                    EnableAjax="false" DisableControlBeforePostBack="false">
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
                        <f:RenderField ColumnID="ProjectCode" DataField="ProjectCode" Width="120px" FieldType="String" HeaderText="总承包合同编号" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="ShortName" DataField="ShortName" Width="180px" FieldType="String" HeaderText="项目简称" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="ProjectCode" DataField="ProjectCode" Width="180px" FieldType="String" HeaderText="项目代码" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="ContractNum" DataField="ContractNum" Width="180px" FieldType="String" HeaderText="合同编号" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="ContractName" DataField="ContractName" Width="180px" FieldType="String" HeaderText="施工合同名称" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="SubConstruction" DataField="SubConstruction" Width="120px" FieldType="String" HeaderText="施工分包商" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="Currency" DataField="Currency" Width="100px" FieldType="String" HeaderText="合同价格类型" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="ContractAmount" DataField="ContractAmount" Width="120px" FieldType="String" HeaderText="合同价格（元）" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="DepartName" DataField="DepartName" Width="120px" FieldType="String" HeaderText="签订日期" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="Bank1" DataField="Bank1" Width="120px" FieldType="String" HeaderText="公司开户行" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="Account1" DataField="Account1" Width="150px" FieldType="String" HeaderText="开户行账号" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>

                    </Columns>
                    <%--     <Listeners>
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                        </Listeners>--%>
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
        <f:Window ID="Window1" Title="基本信息" Hidden="true" EnableIFrame="true" EnableMaximize="true" Maximized="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" OnClose="Window1_Close"
            Width="1000px" Height="420px">
        </f:Window>
        <%--<f:Menu ID="Menu1" runat="server">
            <Items>
                <f:MenuButton ID="btnMenuEdit" EnablePostBack="true" runat="server" Hidden="true" Text="重新提交" Icon="Pencil"
                    OnClick="btnMenuEdit_Click">
                </f:MenuButton>
                <f:MenuButton ID="btnMenuDelete" EnablePostBack="true" runat="server" Hidden="true" Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？"
                    OnClick="btnMenuDelete_Click">
                </f:MenuButton>
            </Items>
        </f:Menu>--%>
    </form>
    <script type="text/javascript">
       <%-- var menuID = '<%= Menu1.ClientID %>';

        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }--%>
</script>
</body>
</html>

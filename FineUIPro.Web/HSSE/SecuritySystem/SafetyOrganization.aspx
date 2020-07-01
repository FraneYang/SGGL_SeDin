<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SafetyOrganization.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.SecuritySystem.SafetyOrganization" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>安全管理机构</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                Layout="HBox" EnableCollapse="true" Width="300" Title="安全管理机构" TitleToolTip="安全管理机构"
                ShowBorder="true" ShowHeader="true" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Items>
                    <f:Tree ID="trSafetyOrganization" EnableCollapse="true" ShowHeader="false" ShowBorder="false"
                        EnableIcons="true" Title="安全管理机构" OnNodeCommand="trSafetyOrganization_NodeCommand"
                        AutoLeafIdentification="true" AutoScroll="true" runat="server" EnableTextSelection="True"
                        EnableSingleClickExpand="true">
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="安全管理机构"
                TitleToolTip="安全管理机构" AutoScroll="true">
                <Items>
                    <f:Grid ID="Grid1" Width="870px" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="SafetyOrganizationId" AllowSorting="true"
                        OnSort="Grid1_Sort" SortField="SortIndex" SortDirection="ASC" AllowCellEditing="true"
                        ClicksToEdit="2" EnableColumnLines="true" DataIDField="SafetyOrganizationId" ForceFit="true"
                        AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                        EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                    <f:Button ID="btnNewItem" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewItem_Click"
                                        Hidden="true">
                                    </f:Button>
                                    <f:Button ID="btnEditItem" ToolTip="编辑" Icon="Pencil" runat="server" OnClick="btnEditItem_Click"
                                        Hidden="true">
                                    </f:Button>
                                    <f:Button ID="btnDeleteItem" ToolTip="删除" Icon="Delete" ConfirmText="确定删除当前数据？" OnClick="btnDeleteItem_Click"
                                        Hidden="true" runat="server">
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
                            <f:RenderField Width="150px" ColumnID="Post" DataField="Post" SortField="Post" FieldType="String"
                                HeaderTextAlign="Center" TextAlign="Left" HeaderText="职务">
                            </f:RenderField>
                            <f:RenderField Width="110px" ColumnID="Names" DataField="Names" SortField="Names"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="姓名">
                            </f:RenderField>
                  <%--          <f:RenderField Width="120px" ColumnID="Telephone" DataField="Telephone" SortField="Telephone"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Right" HeaderText="电话">
                            </f:RenderField>--%>
                            <f:RenderField Width="110px" ColumnID="MobilePhone" DataField="MobilePhone" SortField="MobilePhone"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Right" HeaderText="手机">
                            </f:RenderField>
                            <f:RenderField Width="110px" ColumnID="EMail" DataField="EMail" SortField="EMail"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="邮箱">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="Duty" DataField="Duty" SortField="Duty" FieldType="String"
                                HeaderTextAlign="Center" TextAlign="Center" HeaderText="职责">
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
        </Items>
    </f:Panel>
    <f:Window ID="Window2" Title="编辑" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="true"
        Width="800px" Height="400px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            runat="server" Text="编辑" Hidden="true" Icon="Pencil">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除"
            Hidden="true">
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

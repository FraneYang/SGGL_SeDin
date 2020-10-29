<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EquipmentOut.aspx.cs" Inherits="FineUIPro.Web.HSSE.InApproveManager.EquipmentOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>特种设备机具出场报批</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="特种设备机具出场报批" EnableCollapse="true"
                    runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="EquipmentOutId" DataIDField="EquipmentOutId"
                    AllowSorting="true" SortField="EquipmentOutCode" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                    IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" ForceFit="true"
                    EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:TextBox runat="server" Label="编号" ID="txtEquipmentOutCode" EmptyText="输入查询条件"
                                    AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="330px" LabelAlign="right">
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
                        <f:TemplateField ColumnID="tfNumber" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                            TextAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField Width="100px" ColumnID="EquipmentOutCode" DataField="EquipmentOutCode"
                            SortField="EquipmentOutCode" FieldType="String" HeaderText="编号" HeaderTextAlign="Center"
                            TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="240px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                            FieldType="String" HeaderText="申请单位" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="ApplicationDate" DataField="ApplicationDate"
                            SortField="ApplicationDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                            HeaderText="申请日期" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="120px" ColumnID="CarNum" DataField="CarNum" SortField="CarNum"
                            FieldType="String" HeaderText="车牌号" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="CarModel" DataField="CarModel" SortField="CarModel"
                            FieldType="String" HeaderText="车型" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="90px" ColumnID="DriverName" DataField="DriverName" SortField="DriverName"
                            FieldType="String" HeaderText="驾驶员姓名" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="DriverNum" DataField="DriverNum" SortField="DriverNum"
                            FieldType="String" HeaderText="驾驶证号" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="140px" ColumnID="FlowOperateName" DataField="FlowOperateName"
                            SortField="FlowOperateName" FieldType="String" HeaderText="状态" HeaderTextAlign="Center"
                            TextAlign="Left">
                        </f:RenderField>
                        <%--<f:WindowField TextAlign="Left" Width="160px" WindowID="WindowAtt" HeaderText="附件"
                            Text="上传查看" ToolTip="附件上传查看" DataIFrameUrlFields="EquipmentOutId" DataIFrameUrlFormatString="~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/EquipmentOutAttachUrl&menuId=A4832598-E3D4-4906-88E5-A3886A85FC5A"
                            IFrameUrl="~/alert.aspx" ExpandUnusedSpace="True" HeaderTextAlign="Center" />--%>
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
        <f:Window ID="Window1" Title="特种设备机具出场报批" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" OnClose="Window1_Close" IsModal="true"
            Width="1024px" Height="600px">
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubUnitQuality.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.QualityAudit.SubUnitQuality" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>分包商资质</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="分包商资质" EnableCollapse="true"
                runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="UnitId"  DataIDField="UnitId" 
                AllowSorting="true" SortField="UnitCode" OnRowCommand="Grid1_RowCommand" ForceFit="true"
                SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
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
                    <f:RenderField Width="100px" ColumnID="UnitCode" DataField="UnitCode" SortField="UnitCode"
                        FieldType="String" HeaderText="单位编号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="240px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                        FieldType="String" HeaderText="分包单位" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="SubUnitQualityName" DataField="SubUnitQualityName"  ExpandUnusedSpace="true"
                        SortField="SubUnitQualityName" FieldType="String" HeaderText="资质" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                 <%--   <f:RenderField Width="120px" ColumnID="Telephone" DataField="Telephone" SortField="Telephone"
                        FieldType="String" HeaderText="联系电话" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>--%>
                 <%--   <f:RenderField Width="120px" ColumnID="EMail" DataField="EMail" SortField="EMail"
                        FieldType="String" HeaderText="电子邮箱" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>--%>
                 <%--   <f:RenderField Width="170px" ColumnID="BusinessLicense" DataField="BusinessLicense"
                        SortField="BusinessLicense" FieldType="String" HeaderText="营业执照" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>--%>
           <%--         <f:RenderField Width="120px" ColumnID="BL_EnableDate" DataField="BL_EnableDate" SortField="BL_EnableDate"
                        FieldType="Date" Renderer="Date" HeaderText="营业执照有效期" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>--%>
                   <%-- <f:RenderField Width="120px" ColumnID="OrganCode" DataField="OrganCode" SortField="OrganCode"
                        FieldType="String" HeaderText="机构代码" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="O_EnableDate" DataField="O_EnableDate" SortField="O_EnableDate"
                        FieldType="Date" Renderer="Date" HeaderText="机构代码有效期" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>--%>
                    <%--<f:RenderField Width="120px" ColumnID="Certificate" DataField="Certificate" SortField="Certificate"
                        FieldType="String" HeaderText="资质证书" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="C_EnableDate" DataField="C_EnableDate" SortField="C_EnableDate"
                        FieldType="Date" Renderer="Date" HeaderText="资质证书有效期" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="160px" ColumnID="QualityLicense" DataField="QualityLicense"
                        SortField="QualityLicense" FieldType="String" HeaderText="质量体系认证证书" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="170px" ColumnID="QL_EnableDate" DataField="QL_EnableDate" SortField="QL_EnableDate"
                        FieldType="Date" Renderer="Date" HeaderText="质量体系认证证书有效期" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="HSELicense" DataField="HSELicense" SortField="HSELicense"
                        FieldType="String" HeaderText="HSE体系认证证书" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="180px" ColumnID="H_EnableDate" DataField="H_EnableDate" SortField="H_EnableDate"
                        FieldType="Date" Renderer="Date" HeaderText="HSE体系认证证书有效期" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="SecurityLicense" DataField="SecurityLicense"
                        SortField="SecurityLicense" FieldType="String" HeaderText="安全生产许可证" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="SL_EnableDate" DataField="SL_EnableDate" SortField="SL_EnableDate"
                        FieldType="Date" Renderer="Date" HeaderText="安全生产许可证有效期" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>--%>
                    <f:LinkButtonField Width="80px" HeaderText="审查记录" ConfirmTarget="Parent" CommandName="auditDetail"
                        TextAlign="Center" ToolTip="审查记录" Text="详细"/>
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
    <f:Window ID="Window1" Title="分包商资质" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="1200px"
        Height="540px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="Pencil" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑">
        </f:MenuButton>
        <%--<f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除">
        </f:MenuButton>--%>
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

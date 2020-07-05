<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SafePersonQuality.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.QualityAudit.SafePersonQuality" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全人员资质</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch" AutoScroll="true">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全人员资质" EnableCollapse="true"
                runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="PersonId" DataIDField="PersonId" AllowSorting="true"
                SortField="UnitCode,WorkPostCode,PersonName" SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" ForceFit="true"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox runat="server" Label="编号" ID="txtCardNo" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="200px" LabelWidth="70px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:DropDownList ID="drpUnitId" runat="server" Label="单位" AutoPostBack="true" 
                                OnSelectedIndexChanged="TextBox_TextChanged"  LabelWidth="70px" Width="250px">
                            </f:DropDownList>
                            <f:TextBox runat="server" Label="人员姓名" ID="txtPersonName" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="200px" LabelWidth="70px" LabelAlign="right">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="岗位" ID="txtWorkPostName" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="200px" LabelWidth="70px" LabelAlign="right">
                            </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>                                                     
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
               <%--     <f:RenderField Width="90px" ColumnID="UnitCode" DataField="UnitCode"
                        SortField="UnitCode" FieldType="String" HeaderText="单位代码" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>--%>
                    <f:RenderField Width="240px" ColumnID="UnitName" DataField="UnitName"
                        SortField="UnitName" FieldType="String" HeaderText="单位名称" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>  
                    <f:RenderField Width="90px" ColumnID="PersonName" DataField="PersonName" 
                        SortField="PersonName" FieldType="String" HeaderText="人员姓名" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>  
                    <f:RenderField Width="120px" ColumnID="WorkPostName" DataField="WorkPostName" 
                        SortField="WorkPostName" FieldType="String" HeaderText="岗位" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="170px" ColumnID="CertificateName" DataField="CertificateName"
                         SortField="CertificateName" FieldType="String" HeaderText="特岗证书"
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="CertificateNo" DataField="CertificateNo" 
                        SortField="CertificateNo" FieldType="String" HeaderText="证书编号" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="SendDate" DataField="SendDate" SortField="SendDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="发证时间"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="LimitDate" DataField="LimitDate" SortField="LimitDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="有效期至"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                   <%-- <f:RenderField Width="100px" ColumnID="Grade" DataField="Grade" 
                        SortField="Grade" FieldType="String" HeaderText="操作类别" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>--%>
                    <%--<f:RenderField Width="220px" ColumnID="SendUnit" DataField="SendUnit" 
                        SortField="SendUnit" FieldType="String" HeaderText="发证单位" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                       <f:RenderField Width="100px" ColumnID="AuditorName" DataField="AuditorName" 
                        SortField="AuditorName" FieldType="String" HeaderText="审核人" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="AuditDate" DataField="AuditDate" SortField="AuditDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="审核时间"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:WindowField TextAlign="Center" Width="70px" WindowID="WindowAtt" HeaderText="证书"
                        Text="查看" ToolTip="附件上传查看" DataIFrameUrlFields="SafePersonQualityId" DataIFrameUrlFormatString="~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SafePersonQualityAttachUrl&menuId=750F5074-45B9-470E-AE1E-6204957421E6&type=-1"
                        HeaderTextAlign="Center" ColumnID="attWindow" />--%>
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
    <f:Window ID="Window1" Title="安全人员资质" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="800px"
        Height="500px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="附件页面" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="Pencil" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑">
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
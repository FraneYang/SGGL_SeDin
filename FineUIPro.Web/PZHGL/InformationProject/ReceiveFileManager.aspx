<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceiveFileManager.aspx.cs" Inherits="FineUIPro.Web.PZHGL.InformationProject.ReceiveFileManager" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>一般来文管理</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="一般来文" EnableCollapse="true"
                runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="ReceiveFileManagerId"
                DataIDField="ReceiveFileManagerId" AllowSorting="true" SortField="ReceiveFileCode" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" ForceFit="true"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:RadioButtonList runat="server" ID="rbFileType" Width="250px"
                                AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                                <f:RadioItem Value="0" Text="项目发文" Selected="true"/>
                                <f:RadioItem Value="1" Text="公司来文"/>
                            </f:RadioButtonList>
                            <f:TextBox runat="server" Label="编号" ID="txtReceiveFileCode" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="70px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="名称" ID="txtReceiveFileName" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="70px"
                                LabelAlign="right">
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
                    <f:RenderField Width="100px" ColumnID="ReceiveFileCode" DataField="ReceiveFileCode"
                        SortField="ReceiveFileCode" FieldType="String" HeaderText="来文编号" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="350px" ColumnID="ReceiveFileName" DataField="ReceiveFileName" 
                        SortField="ReceiveFileName" FieldType="String" HeaderText="文件名称" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField  ColumnID="UnitName" DataField="UnitName" Width="220px"
                        SortField="UnitName" FieldType="String" HeaderText="来文单位" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="GetFileDate" DataField="GetFileDate"
                        SortField="GetFileDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                        HeaderText="来文日期" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="140px" ColumnID="FlowOperateName" DataField="FlowOperateName"
                        SortField="FlowOperateName" FieldType="String" HeaderText="状态" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
             <%--        <f:WindowField TextAlign="Center" Width="70px" WindowID="WindowAtt" HeaderText="文件" Text="附件" ToolTip="附件上传查看" 
                        DataIFrameUrlFields="ReceiveFileManagerId" DataIFrameUrlFormatString="~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ReceiveFileManagerAttachUrl&menuId=4F5C00F3-DA7D-4B2D-B1EF-310DFFCA77DD&type=-1"
                        HeaderTextAlign="Center" >
                     </f:WindowField>--%>
                  <%--  <f:WindowField TextAlign="Center" Width="70px" WindowID="WindowAtt" HeaderText="回复" Text="附件" ToolTip="附件上传查看" 
                        DataIFrameUrlFields="ReReceiveFileManagerId" DataIFrameUrlFormatString="~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ReceiveFileManagerAttachUrl&menuId=4F5C00F3-DA7D-4B2D-B1EF-310DFFCA77DD"
                        HeaderTextAlign="Center" >
                     </f:WindowField>
                     <f:RenderField Width="80px" ColumnID="RetrunSateName" DataField="RetrunSateName"
                        SortField="RetrunSateName" FieldType="String" HeaderText="回复状态" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>--%>
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
    <f:Window ID="Window1" Title="一般来文管理" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="1024px" Height="600px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="附件页面" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px" Height="500px" OnClose="WindowAtt_Close">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="Pencil" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnIssue" OnClick="btnIssue_Click" Icon="NoteGo" EnablePostBack="true"
            Hidden="true" runat="server" Text="下发">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除">
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

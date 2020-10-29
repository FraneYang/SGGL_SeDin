<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoIssuedNotice.aspx.cs" Inherits="FineUIPro.Web.Notice.NoIssuedNotice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>通知管理</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="Fit" BoxConfigAlign="Stretch">
        <Items>
          <f:Grid ID="SGrid" ShowBorder="true" ShowHeader="false" Title="发出通知" EnableCollapse="true" runat="server" BoxFlex="1" 
              EnableColumnLines="true" DataKeyNames="SNoticeId" DataIDField="SNoticeId" AllowSorting="true" SortField="NoticeCode" ForceFit="true"
              SortDirection="DESC" OnSort="SGrid_Sort" AllowPaging="true" IsDatabasePaging="true" PageSize="10"  EnableTextSelection="True"
              OnPageIndexChange="SGrid_PageIndexChange" EnableRowDoubleClickEvent="true" OnRowDoubleClick="SGrid_RowDoubleClick" >
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:TextBox runat="server" Label="编号" ID="txtNoticeCode" EmptyText="输入查询条件" 
                                    Width="250px" LabelWidth="60px" LabelAlign="right">
                                </f:TextBox>
                                <f:TextBox runat="server" Label="名称" ID="txtNoticeTitle" EmptyText="输入查询条件" 
                                    Width="250px" LabelWidth="60px" LabelAlign="right">
                                </f:TextBox>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                    <f:Button ID="btnQuery" ToolTip="查询" Icon="SystemSearch" 
                                        EnablePostBack="true" OnClick="STextBox_TextChanged" runat="server" >
                                </f:Button>
                                <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" Hidden="true"
                                    runat="server">
                                </f:Button>
                                <f:Button ID="btnOutS" OnClick="btnOutS_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                    EnableAjax="false" DisableControlBeforePostBack="false">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>                                    
                        <f:RenderField Width="80px" ColumnID="NoticeCode" DataField="NoticeCode" SortField="NoticeCode"
                            FieldType="String" HeaderText="编号" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="350px" ColumnID="NoticeTitle" DataField="NoticeTitle" SortField="NoticeTitle"
                            FieldType="String" HeaderText="标题" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:TemplateField Width="400px" HeaderText="接收对象" ColumnID="AccessProjectText" HeaderTextAlign="Center"
                            TextAlign="Left" ExpandUnusedSpace="true">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("SortAccessProjectText") %>' ToolTip='<%# Bind("AccessProjectText") %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <%--    <f:RenderField Width="150px" ColumnID="ReleaseDate" DataField="ReleaseDate" SortField="ReleaseDate"
                            HeaderText="发布日期" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>--%>
                        <f:RenderField Width="130px" ColumnID="StateName" DataField="StateName" SortField="StateName"
                            FieldType="String" HeaderText="状态" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                    <%--    <f:RenderField Width="70px" ColumnID="IsReleaseName" DataField="IsReleaseName" SortField="IsReleaseName"
                            FieldType="String" HeaderText="发布" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>--%>
                    <%--    <f:WindowField TextAlign="Left" Width="60px" WindowID="WindowAtt" HeaderText="附件" Text="详细" ToolTip="附件上传查看"
                            DataIFrameUrlFields="SNoticeId" DataIFrameUrlFormatString="../AttachFile/webuploader.aspx?toKeyId={0}&type=-1&path=FileUpload/NoticeAttachUrl"
                            HeaderTextAlign="Center" />--%>
                    </Columns>
                    <Listeners>
                        <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                    </Listeners>
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                        </f:ToolbarText>
                        <f:DropDownList runat="server" ID="SddlPageSize" Width="80px" AutoPostBack="true"
                            OnSelectedIndexChanged="SddlPageSize_SelectedIndexChanged">
                        </f:DropDownList>
                    </PageItems>
                </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="管理通知" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="1100px"
        Height="560px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="Pencil" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuIssuance" OnClick="btnMenuIssuance_Click" Icon="BookNext"
            EnablePostBack="true" Hidden="true" runat="server" Text="发布">
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

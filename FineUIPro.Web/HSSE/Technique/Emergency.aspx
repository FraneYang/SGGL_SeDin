<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Emergency.aspx.cs" Inherits="FineUIPro.Web.HSSE.Technique.Emergency" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>应急预案</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="应急预案" EnableCollapse="true" ForceFit="true"
                runat="server" BoxFlex="1" DataKeyNames="EmergencyId" DataIDField="EmergencyId" AllowSorting="true" SortField="EmergencyCode"
                SortDirection="ASC" OnSort="Grid1_Sort"  AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True" EnableColumnLines="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar3" Position="Top" runat="server">
                        <Items>
                            <f:TextBox ID="EmergencyCode" runat="server" Label="编号" EmptyText="输入查询条件"
                                Width="210px" LabelWidth="60px"
                                LabelAlign="Right">
                            </f:TextBox>
                            <f:TextBox ID="EmergencyName" runat="server" Label="名称" EmptyText="输入查询条件"
                                Width="210px" LabelWidth="60px"
                                LabelAlign="Right">
                            </f:TextBox>
                            <f:TextBox ID="EmergencyTypeName" runat="server" Label="类型" EmptyText="输入查询条件"
                                Width="210px" LabelWidth="60px"
                                LabelAlign="Right">
                            </f:TextBox>
                            <f:ToolbarFill runat="server"></f:ToolbarFill>
                            <f:Button ID="btnQuery" ToolTip="查询" Icon="SystemSearch" EnablePostBack="true" 	
                                OnClick="TextBox_TextChanged" runat="server" >
                            </f:Button>
                             <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                Hidden="true">
                            </f:Button>
                            <f:Button ID="btnSelectColumns" runat="server" ToolTip="导出" Icon="FolderUp" EnablePostBack="false" Hidden="true">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>                
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                    <f:TemplateField Width="120px" HeaderText="编号" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="EmergencyCode">
                        <ItemTemplate>
                            <asp:Label ID="lblEmergencyCode" runat="server" Text='<%# Bind("EmergencyCode") %>'
                                ToolTip='<%#Bind("EmergencyCode") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="200px" HeaderText="名称" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="EmergencyName">
                        <ItemTemplate>
                            <asp:Label ID="lblEmergencyName" runat="server" Text='<%# Bind("EmergencyName") %>'
                                ToolTip='<%#Bind("EmergencyName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="180px" HeaderText="类型" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="EmergencyTypeName">
                        <ItemTemplate>
                            <asp:Label ID="lblEmergencyTypeName" runat="server" Text='<%# Bind("EmergencyTypeName") %>'
                                ToolTip='<%#Bind("EmergencyTypeName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="220px" HeaderText="摘要" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="Summary">
                        <ItemTemplate>
                            <asp:Label ID="lblSummary" runat="server" Text='<%# Bind("Summary") %>' ToolTip='<%#Bind("Summary") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:WindowField TextAlign="Left" Width="80px" WindowID="WindowAtt" Text="详细" HeaderText="附件"
                        ToolTip="附件上传查看" DataIFrameUrlFields="EmergencyId" DataIFrameUrlFormatString="../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Emergency&menuId=D11BFC83-BE64-457D-B8FA-11C37D35CD72" />                  
                    <f:RenderField Width="80px" ColumnID="IsBuildName" DataField="IsBuildName" SortField="IsBuildName"
                        FieldType="String" HeaderText="来源" HeaderTextAlign="Center" TextAlign="Left">
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
    <f:Window ID="Window1" Title="编辑应急预案" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="800px" Height="460px">
    </f:Window>
    <f:Window ID="Window4" Title="选择需要导出的列" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" OnClose="Window4_Close" IsModal="true"
        Width="450px" Height="250px" EnableAjax="false">
    </f:Window>
    <f:Window ID="Window6" Title="请点击下方保存下载附件到本地" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" Hidden="true" IsModal="true"
        Width="400px" Height="10px" EnableAjax="false">
    </f:Window>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true" Icon="Pencil"
            runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true" Icon="Delete"
            ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/jscript">
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

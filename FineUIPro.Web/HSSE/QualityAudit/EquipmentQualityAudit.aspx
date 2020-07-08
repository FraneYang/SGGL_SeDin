<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EquipmentQualityAudit.aspx.cs" Inherits="FineUIPro.Web.HSSE.QualityAudit.EquipmentQualityAudit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>审查记录</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" DataIDField="AuditDetailId" DataKeyNames="AuditDetailId" 
                        ShowGridHeader="true" Height="400px" EnableColumnLines="true" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                    <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                Hidden="true">
                            </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RowNumberField HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:RenderField Width="220px" ColumnID="AuditContent" DataField="AuditContent" SortField="AuditContent"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="审查内容"
                                ExpandUnusedSpace="true">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="AuditManName" DataField="AuditManName" SortField="AuditManName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="审查人">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="AuditDate" DataField="AuditDate" SortField="AuditDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="审查时间"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="260px" ColumnID="AuditResult" DataField="AuditResult" SortField="AuditResult"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="审查结果">
                            </f:RenderField>
                            <f:WindowField TextAlign="Left" Width="60px" WindowID="WindowAtt" HeaderTextAlign="Center"
                                Text="附件" HeaderText="附件" ToolTip="附件上传查看" DataIFrameUrlFields="AuditDetailId" 
                                DataIFrameUrlFormatString="~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/EquipmentQualityAuditAttachUrl&menuId=2DEDD752-8BAF-43CD-933D-932AF9AF2F58&type=-1" />
                        </Columns>
                        <Listeners>
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                        </Listeners>
                    </f:Grid>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    <f:Window ID="Window1" Title="编辑审查记录明细" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="800px" Height="400px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true" Hidden="true"
            Icon="Pencil" runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true" Hidden="true"
            Icon="Delete" ConfirmText="确定删除当前数据？" ConfirmTarget="Parent" runat="server" Text="删除">
        </f:MenuButton>
    </f:Menu>
    </form>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LawRegulationList.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Law.LawRegulationList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>法律法规</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />    
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全法律法规" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="LawRegulationId"  EnableColumnLines="true" ClicksToEdit="2" DataIDField="LawRegulationId" AllowSorting="true"
                SortField="EffectiveDate" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" ForceFit="true"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                        <Items>
                            <f:TextBox ID="txtLawRegulationName" runat="server" Label="名称" EmptyText="输入查询名称"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="60px"
                                LabelAlign="Right">
                            </f:TextBox>
                            <f:TextBox ID="txtLawsRegulationsTypeName" runat="server" Label="类别" EmptyText="输入查询类别"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="60px"
                                LabelAlign="Right">
                            </f:TextBox>
                            <f:ToolbarFill runat="server"></f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                Hidden="true">
                            </f:Button>
                            <f:Button ID="btnSelectColumns" runat="server" ToolTip="导出" Icon="FolderUp" EnablePostBack="false" Hidden="true">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>               
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center" Locked="true"/>
                    <f:TemplateField Width="250px" HeaderText="名称" HeaderTextAlign="Center" TextAlign="Left"   SortField="LawRegulationName">
                        <ItemTemplate>
                            <asp:Label ID="lblLawRegulationName" runat="server" Text='<%# Bind("LawRegulationName") %>'
                                ToolTip='<%#Bind("LawRegulationName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="90px" HeaderText="类别" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="LawsRegulationsTypeName">
                        <ItemTemplate>
                            <asp:Label ID="lblLawsRegulationsTypeName" runat="server" Text='<%# Bind("LawsRegulationsTypeName") %>'
                                ToolTip='<%#Bind("LawsRegulationsTypeName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="100px" ColumnID="ApprovalDate" DataField="ApprovalDate" SortField="ApprovalDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="批准日"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="EffectiveDate" DataField="EffectiveDate" SortField="EffectiveDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="生效日"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:TemplateField Width="400px" HeaderText="简介及重点关注条款" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="Description">
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("ShortDescription") %>' ToolTip='<%#Bind("Description") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:WindowField TextAlign="Center" Width="80px" WindowID="WindowAtt" 
                        Text="详细" ToolTip="附件上传查看" DataIFrameUrlFields="LawRegulationId" DataIFrameUrlFormatString="../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/LawRegulation&menuId=F4B02718-0616-4623-ABCE-885698DDBEB1"/>                                       
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
    <f:Window ID="Window1" Title="编辑" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="760px" Height="380px">
    </f:Window>
    <f:Window ID="Window5" Title="选择需要导出的列" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window5_Close" IsModal="true"
        Width="350px" Height="250px" EnableAjax="false">
    </f:Window>
       <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Parent" EnableResize="true" runat="server"
            IsModal="true" Width="700px" Height="500px">
       </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            runat="server" Text="编辑" Icon="Pencil" Hidden="true">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除" Icon="Delete"
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

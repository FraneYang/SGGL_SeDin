<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RulesRegulations.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Law.RulesRegulations" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全规章制度</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />   
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全规章制度" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="RulesRegulationsId"  EnableColumnLines="true" DataIDField="RulesRegulationsId" AllowSorting="true"
                SortField="RulesRegulationsCode" SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" ForceFit="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                        <Items>
                            <f:TextBox ID="txtRulesRegulationsName" runat="server" Label="名称" EmptyText="输入查询名称"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"
                                LabelAlign="Right">
                            </f:TextBox>
                            <f:TextBox ID="txtRulesRegulationsTypeName" runat="server" Label="分类" EmptyText="输入查询分类"
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
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                    <f:TemplateField Width="100px" HeaderText="编号" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="RulesRegulationsCode">
                        <ItemTemplate>
                            <asp:Label ID="lblRulesRegulationsCode" runat="server" Text='<%# Bind("RulesRegulationsCode") %>'
                                ToolTip='<%#Bind("RulesRegulationsCode") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="300px" HeaderText="名称" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="RulesRegulationsName">
                        <ItemTemplate>
                            <asp:Label ID="lblRulesRegulationsName" runat="server" Text='<%# Bind("RulesRegulationsName") %>'
                                ToolTip='<%#Bind("RulesRegulationsName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="90px" HeaderText="分类" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="RulesRegulationsTypeName">
                        <ItemTemplate>
                            <asp:Label ID="lblRulesRegulationsTypeName" runat="server" Text='<%# Bind("RulesRegulationsTypeName") %>'
                                ToolTip='<%#Bind("RulesRegulationsTypeName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="100px" ColumnID="CustomDate" DataField="CustomDate" SortField="CustomDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="订制时间"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:TemplateField Width="220px" HeaderText="适用范围" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="ApplicableScope">
                        <ItemTemplate>
                            <asp:Label ID="lblApplicableScope" runat="server" Text='<%# Bind("ApplicableScope") %>'
                                ToolTip='<%#Bind("ApplicableScope") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                  <%--  <f:TemplateField Width="300px" HeaderText="摘要" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="Remark">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("Remark") %>' ToolTip='<%#Bind("Remark") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>--%>
                    <%--<f:WindowField ColumnID="AttachUrl" Width="120px" WindowID="Window6" HeaderText="附件"
                        DataToolTipField="AttachUrlName" DataTextField="AttachUrlName" DataTextFormatString="{0}"
                        DataIFrameUrlFields="AttachUrl,AttachUrlName" DataIFrameUrlFormatString="~/common/ShowUpFile.aspx?fileUrl={0}"
                        DataWindowTitleFormatString="编辑 - {1}" />--%>
                        <f:WindowField TextAlign="Center" Width="70px" WindowID="WindowAtt" HeaderText="附件" Text="详细"
                       ToolTip="附件上传查看" DataIFrameUrlFields="RulesRegulationsId" DataIFrameUrlFormatString="../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/RulesRegulations&menuId=DF1413F3-4CE5-40B3-A574-E01CE64FEA25"
                          />
                    <f:RenderField Width="70px" ColumnID="IsBuildName" DataField="IsBuildName" SortField="IsBuildName"
                        FieldType="String" HeaderText="来源" HeaderTextAlign="Center" TextAlign="Center">
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
    <f:Window ID="Window1" Title="编辑安全规章制度" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="750px" Height="320px">
    </f:Window>
    <f:Window ID="Window5" Title="选择需要导出的列" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window5_Close" IsModal="true"
        Width="350px" Height="180px" EnableAjax="false">
    </f:Window>
    <%--<f:Window ID="Window6" Title="请点击下方保存下载附件到本地" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" Hidden="true" IsModal="true"
        Width="400px" Height="10px" EnableAjax="false">
    </f:Window>--%>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Self" EnableResize="true" runat="server"
            IsModal="true" Width="800px" Height="500px">
       </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            runat="server" Text="编辑" Hidden="true" Icon="Pencil">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true" Icon="Delete"
            ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除" Hidden="true">
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

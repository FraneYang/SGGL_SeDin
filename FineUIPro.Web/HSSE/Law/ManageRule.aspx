<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageRule.aspx.cs" Inherits="FineUIPro.Web.HSSE.Law.ManageRule" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>管理规定</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全管理规定" EnableCollapse="true"
                  runat="server" BoxFlex="1" DataKeyNames="ManageRuleId" EnableColumnLines="true" DataIDField="ManageRuleId"
                AllowSorting="true" SortField="ApprovalDate" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" 
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                        <Items>
                            <f:TextBox ID="txtManageRuleCode" runat="server" Label="编号" EmptyText="输入查询文件编号"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"
                                LabelAlign="Right">
                            </f:TextBox>
                            <f:TextBox ID="txtManageRuleName" runat="server" Label="名称" EmptyText="输入查询文件名称"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"
                                LabelAlign="Right">
                            </f:TextBox>
                                  <f:DropDownList ID="drpType" Label="类型" AutoPostBack="true"  OnSelectedIndexChanged="TextBox_TextChanged"
                                 Width="250px" LabelWidth="60px" LabelAlign="Right" runat="server">
                            </f:DropDownList>
                            <f:ToolbarFill runat="server"></f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" Hidden="true" runat="server">
                            </f:Button> 
                          <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                     <f:RenderField Width="100px" ColumnID="ReleaseStatesName" DataField="ReleaseStatesName" 
                        FieldType="String" HeaderText="状态" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                      <f:RenderField Width="150px" ColumnID="ManageRuleName" DataField="ManageRuleName" 
                        FieldType="String" HeaderText="名称" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="ManageRuleCode" DataField="ManageRuleCode" 
                        FieldType="String" HeaderText="编号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="140px" ColumnID="ManageRuleTypeName" DataField="ManageRuleTypeName" 
                        FieldType="String" HeaderText="类型" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                      <f:RenderField Width="100px" ColumnID="ReleaseUnit" DataField="ReleaseUnit" 
                        FieldType="String" HeaderText="发布机构" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="ApprovalDate" DataField="ApprovalDate" SortField="ApprovalDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="发布日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="EffectiveDate" DataField="EffectiveDate" SortField="EffectiveDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="生效日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="AbolitionDate" DataField="AbolitionDate" SortField="AbolitionDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="废止日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                     <f:RenderField Width="260px" ColumnID="ReplaceInfo" DataField="ReplaceInfo" 
                        FieldType="String" HeaderText="替换信息" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                    <f:RenderField Width="300px" ColumnID="Description" DataField="Description" 
                        FieldType="String" HeaderText="简介及重点关注条款" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="200px" ColumnID="IndexesNames" DataField="IndexesNames"  SortField="IndexesNames"
                        FieldType="String" HeaderText="索引" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                      <f:RenderField Width="100px" ColumnID="CompileMan" DataField="CompileMan" 
                        FieldType="String" HeaderText="上传人" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="CompileDate" DataField="CompileDate" SortField="CompileDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="上传时间"
                        HeaderTextAlign="Center" TextAlign="Left">
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
    <f:Window ID="Window1" Title="编辑安全管理规定" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1024px" Height="560px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="670px"
        Height="460px">
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

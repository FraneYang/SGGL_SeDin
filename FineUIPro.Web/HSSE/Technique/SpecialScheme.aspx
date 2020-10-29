<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpecialScheme.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Technique.SpecialScheme" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>专项方案</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="专项方案" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="SpecialSchemeId" EnableColumnLines="true"
                DataIDField="SpecialSchemeId" AllowSorting="true" SortField="SpecialSchemeCode" ForceFit="true"
                SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" 
                PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar3" Position="Top" runat="server">
                        <Items>
                            <f:TextBox ID="UnitName" runat="server" Label="单位" EmptyText="输入查询单位" 
                                Width="250px" LabelWidth="70px">
                            </f:TextBox>
                            <f:TextBox ID="SpecialSchemeName" runat="server" Label="名称" EmptyText="输入查询名称" 
                                Width="250px" LabelWidth="70px">
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
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center" />                    
                     <f:RenderField Width="160px" ColumnID="SpecialSchemeCode" DataField="SpecialSchemeCode" SortField="SpecialSchemeCode"
                        FieldType="String" HeaderText="编号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField> 
                     <f:RenderField Width="220px" ColumnID="SpecialSchemeName" DataField="SpecialSchemeName" SortField="SpecialSchemeName"
                        FieldType="String" HeaderText="名称" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField> 
                    <f:RenderField Width="110px" ColumnID="SpecialSchemeTypeName" DataField="SpecialSchemeTypeName" SortField="SpecialSchemeTypeName"
                        FieldType="String" HeaderText="类型" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>  
                    <f:RenderField Width="250px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                        FieldType="String" HeaderText="单位" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>  
                      <f:RenderField Width="150px" ColumnID="Summary" DataField="Summary" SortField="Summary"
                        FieldType="String" HeaderText="摘要" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>  
                    <f:WindowField TextAlign="Left" Width="70px" WindowID="WindowAtt" Text="详细"
                        ToolTip="上传查看" DataIFrameUrlFields="SpecialSchemeId" DataIFrameUrlFormatString="../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SpecialScheme&menuId=3E2F2FFD-ED2E-4914-8370-D97A68398814"/>             
                    <f:RenderField Width="80px" ColumnID="IsBuildName" DataField="IsBuildName" SortField="IsBuildName"
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
    <f:Window ID="Window1" Title="编辑专项方案" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="750px" Height="420px">
    </f:Window>
    <f:Window ID="Window4" Title="选择需要导出的列" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" OnClose="Window4_Close" IsModal="true"
        Width="450px" Height="250px" EnableAjax="false">
    </f:Window>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="false" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑" Icon="Pencil">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true" Icon="Delete"
            Hidden="true" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除">
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

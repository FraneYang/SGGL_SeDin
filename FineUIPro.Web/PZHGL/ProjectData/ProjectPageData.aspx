<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectPageData.aspx.cs" Inherits="FineUIPro.Web.PZHGL.ProjectData.ProjectPageData" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>移动端首页</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />  
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1"  runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false" 
       ShowHeader="false"  Layout="VBox" BoxConfigAlign="Stretch" >
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="移动端首页" 
                EnableCollapse="true" runat="server" BoxFlex="1"  EnableColumnLines="true" 
                DataKeyNames="PageDataId" DataIDField="PageDataId" AllowSorting="true" SortField="CreatDate" SortDirection="DESC"  OnSort="Grid1_Sort"                
                AllowPaging="true" IsDatabasePaging="true"  PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" 
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:ToolbarFill runat="server"></f:ToolbarFill>                        
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add"  OnClick="btnNew_Click"
                                Hidden="true" runat="server">
                            </f:Button> 
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
                      <f:RenderField Width="100px" ColumnID="StartDate" DataField="CreatDate" SortField="CreatDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="日期"
                         HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="SafeHours" DataField="SafeHours" FieldType="Int"
                        HeaderText="安全人工时">
                    </f:RenderField> 
                    <f:RenderField Width="110px" ColumnID="SitePersonNum" DataField="SitePersonNum" FieldType="Int"
                        HeaderText="当日现场人数">
                    </f:RenderField>
                    <f:RenderField Width="130px" ColumnID="SpecialEquipmentNum" DataField="SpecialEquipmentNum" FieldType="Int"
                        HeaderText="大型及特种设备">
                    </f:RenderField>
                     <f:RenderField Width="140px" ColumnID="EntryTrainingNum" DataField="EntryTrainingNum" FieldType="Int"
                        HeaderText="累计入场培训人次">
                    </f:RenderField>
                       <f:RenderField Width="140px" ColumnID="HiddenDangerNum" DataField="HiddenDangerNum" FieldType="Int"
                        HeaderText="隐患整改单总数">
                    </f:RenderField>
                       <f:RenderField Width="140px" ColumnID="RectificationNum" DataField="RectificationNum" FieldType="Int"
                        HeaderText="隐患整改单整改数">
                    </f:RenderField>
                      <f:RenderField Width="100px" ColumnID="RiskI" DataField="RiskI" FieldType="Int"
                        HeaderText="一级风险数">
                    </f:RenderField>
                      <f:RenderField Width="100px" ColumnID="RiskII" DataField="RiskII" FieldType="Int"
                        HeaderText="二级风险数">
                    </f:RenderField>
                      <f:RenderField Width="100px" ColumnID="RiskIII" DataField="RiskIII" FieldType="Int"
                        HeaderText="三级风险数">
                    </f:RenderField>
                      <f:RenderField Width="100px" ColumnID="RiskIV" DataField="RiskIV" FieldType="Int"
                        HeaderText="四级风险数">
                    </f:RenderField>
                      <f:RenderField Width="100px" ColumnID="RiskV" DataField="RiskV" FieldType="Int"
                        HeaderText="五级风险数">
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
    <f:Window ID="Window1" Title="编辑项目信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true"
        Width="900px" Height="400px">
    </f:Window>       
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="Pencil" EnablePostBack="true" Hidden="true"
            runat="server" Text="编辑">
        </f:MenuButton>
<%--        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true" Hidden="true" Icon="Delete"
            ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除">
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

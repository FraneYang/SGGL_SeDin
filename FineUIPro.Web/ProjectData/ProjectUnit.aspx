<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectUnit.aspx.cs" Inherits="FineUIPro.Web.ProjectData.ProjectUnit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>项目单位</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />  
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }     
        
         .f-grid-row.red
        {
            background-color: #FF7575;
            background-image: none;
        }
        
        .fontred
        {
            color: #FF7575;
            background-image: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1"  runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false" 
       ShowHeader="false"  Layout="VBox" BoxConfigAlign="Stretch" >
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="项目单位" 
                EnableCollapse="true" runat="server" BoxFlex="1"  EnableColumnLines="true"
                DataKeyNames="ProjectUnitId" AllowCellEditing="true" ClicksToEdit="2" DataIDField="ProjectUnitId"
                AllowSorting="true" SortField="UnitType,UnitCode" SortDirection="ASC"  OnSort="Grid1_Sort"                
                AllowPaging="true" IsDatabasePaging="true"  PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" 
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                             <f:DropDownList ID="drpProject" runat="server" Label="项目" Width="300px" LabelWidth="60px" EmptyText="请选择项目"
                                EnableEdit="true" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                            </f:DropDownList>
                            <f:TextBox runat="server" Label="单位名称" ID="txtUnitName" EmptyText="输入查询条件" 
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"></f:TextBox>                       
                            <f:Label runat="server" Label="说明" CssClass="fontred" Text="请双击填报单位类型" LabelAlign="right"></f:Label>
                            <f:ToolbarFill runat="server"></f:ToolbarFill>                        
                            <f:Button ID="btnSelect" ToolTip="新增" Icon="Add" Hidden="true" runat="server" 
                                 OnClick="btnSelect_Click">
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
                     <f:RenderField Width="150px" ColumnID="UnitTypeName" DataField="UnitTypeName" SortField="UnitTypeName"
                        FieldType="String" HeaderText="单位类型"  HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="180px" ColumnID="UnitCode" DataField="UnitCode" SortField="UnitCode" FieldType="String"
                        HeaderText="单位编号">
                    </f:RenderField>
                    <f:RenderField Width="250px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                        FieldType="String" HeaderText="单位名称"  HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                    </f:RenderField>
                    <f:RenderField Width="110px" ColumnID="InTime" DataField="InTime" SortField="InTime"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="入场时间"
                         HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="110px" ColumnID="OutTime" DataField="OutTime" SortField="OutTime"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="出场时间"
                       HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>           
                </Columns>
                <Listeners>
                    <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                    <f:Listener Event="dataload" Handler="onGridDataLoad" />
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
    <f:Window ID="Window1" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true">
    </f:Window>                  
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true" Hidden="true"
           Icon="BulletEdit" runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true" Hidden="true"
            Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除">
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
        function onGridDataLoad(event) {
            this.mergeColumns(['UnitTypeName']);
        }

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectUnitSelect.aspx.cs" Inherits="FineUIPro.Web.ProjectData.ProjectUnitSelect" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>单位信息</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />  
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }     
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1"  runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false" 
       ShowHeader="false"  Layout="VBox" BoxConfigAlign="Stretch" >
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="单位选择" 
                EnableCollapse="true" runat="server" BoxFlex="1"  EnableColumnLines="true"
                DataKeyNames="UnitId" AllowCellEditing="true" ClicksToEdit="2" DataIDField="UnitId"
                AllowSorting="true" SortField="IsThisUnit,UnitTypeName" SortDirection="DESC" OnSort="Grid1_Sort"                
                AllowPaging="true" IsDatabasePaging="true"  PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" 
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>                           
                            <f:TextBox runat="server" Label="单位名称" ID="txtUnitName" EmptyText="输入查询条件" 
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"></f:TextBox>                       
                            <f:ToolbarFill runat="server"></f:ToolbarFill> 
                             <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server">
                            </f:Button>  
                            <f:Button ID="btnSure" ToolTip="确定按钮" Icon="Accept" runat="server" OnClick="btnSure_Click">
                            </f:Button> 
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                   <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                    <f:RenderField Width="100px" ColumnID="UnitCode" DataField="UnitCode" SortField="UnitCode" FieldType="String"
                        HeaderText="单位编号">
                    </f:RenderField>
                    <f:RenderField ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                        FieldType="String" HeaderText="单位名称"  HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                    </f:RenderField>
                     <f:RenderField ColumnID="UnitTypeName" DataField="UnitTypeName" SortField="UnitTypeName" Width="120px"
                        FieldType="String" HeaderText="单位类型"  HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="300px" ColumnID="ProjectRange" DataField="ProjectRange" SortField="ProjectRange"
                        FieldType="String" HeaderText="工程范围"  HeaderTextAlign="Center" TextAlign="Left">
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
    <f:Window ID="Window1" Title="单位设置" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server"  IsModal="true"
        Width="1000px" Height="350px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuSure" OnClick="btnSure_Click" EnablePostBack="true" Icon="Accept" runat="server" Text="确定">
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

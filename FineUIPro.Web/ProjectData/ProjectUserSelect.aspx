<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectUserSelect.aspx.cs" Inherits="FineUIPro.Web.ProjectData.ProjectUserSelect" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户信息</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="用户选择" 
                EnableCollapse="true" runat="server" BoxFlex="1"  EnableColumnLines="true"
                DataKeyNames="UserId" AllowCellEditing="true" ClicksToEdit="2" DataIDField="UserId"
                AllowSorting="true" SortField="UserCode" SortDirection="ASC" OnSort="Grid1_Sort"                
                AllowPaging="true" IsDatabasePaging="true"  PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" 
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>      
                            <f:DropDownList ID="drpUnit" runat="server" Label="单位" Width="350px" LabelWidth="60px" EmptyText="请选择项目"
                                 EnableEdit="true" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                            </f:DropDownList>                     
                            <f:TextBox runat="server" Label="用户名称" ID="txtUserName" EmptyText="输入查询条件" 
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"></f:TextBox>                       
                            <f:ToolbarFill runat="server"></f:ToolbarFill>        
                             <f:Button ID="btnNew" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNew_Click">
                            </f:Button>
                            <f:Button ID="btnSure" ToolTip="确定按钮" Icon="Accept" runat="server" OnClick="btnSure_Click">
                            </f:Button> 
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                   <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                     <f:RenderField Width="100px" ColumnID="UserCode" DataField="UserCode" SortField="UserCode" FieldType="String"
                        HeaderText="用户编号">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="UserName" DataField="UserName" SortField="UserName"
                        FieldType="String" HeaderText="用户名称"  HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField> 
                     <f:RenderField ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                        FieldType="String" HeaderText="单位名称"  HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
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
    <f:Window ID="Window1" Title="用户信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="1000px"
        Height="400px">
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

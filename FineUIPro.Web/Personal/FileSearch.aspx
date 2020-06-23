<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileSearch.aspx.cs" Inherits="FineUIPro.Web.Personal.FileSearch" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>数据检索</title>
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
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true" Layout="VBox"
                 ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="数据检索" TitleToolTip="操作日志">
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        BoxFlex="1" DataKeyNames="NewID" EnableColumnLines="true" DataIDField="NewID" AllowSorting="true"
                        SortField="MenuTypeName,PageName,ProjectName,DataCode" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" 
                        IsDatabasePaging="true" PageSize="15"  OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true" 
                        AllowFilters="true" EnableTextSelection="True"  OnRowDoubleClick="Grid1_RowDoubleClick">
                         <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                                <Items>
                                    <f:TextBox runat="server"  ID="txtName" EmptyText="请输入检索内容" FocusOnPageLoad="true" Width="500px">
                                    </f:TextBox>  
                                    <f:Button runat="server" ID="btnSearch" IconFont="Search" OnClick="btnSearch_Click" EnablePostBack="true"></f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="45px" HeaderTextAlign="Center" TextAlign="Center"/>
                            <f:RenderField Width="110px" ColumnID="MenuTypeName" DataField="MenuTypeName" SortField="MenuTypeName"
                                FieldType="String" HeaderText="模块" HeaderTextAlign="Center" TextAlign="Left">                              
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="PageName" DataField="PageName" SortField="PageName"
                                FieldType="String" HeaderText="页面" HeaderTextAlign="Center" TextAlign="Left">                               
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="ProjectName" DataField="ProjectName" SortField="ProjectName"
                                FieldType="String" HeaderText="项目" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>  
                            <f:RenderField Width="130px" ColumnID="DataCode" DataField="DataCode" SortField="DataCode" 
                                HeaderText="编码" HeaderTextAlign="Center" 
                                TextAlign="Left" >
                            </f:RenderField>
                            <f:RenderField ColumnID="Contents" DataField="Contents" HeaderTextAlign="Center" TextAlign="Left"
                                FieldType="String" HeaderText="内容" ExpandUnusedSpace="true">
                            </f:RenderField>
                            <f:RenderField ColumnID="hdUrl" Hidden="true" DataField="Url" FieldType="String"></f:RenderField>
                        </Columns>
                         <Listeners>
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                        </Listeners>
                        <PageItems >
                            <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                            </f:ToolbarSeparator>
                            <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                            </f:ToolbarText>
                            <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                <f:ListItem Text="10" Value="10" />
                                <f:ListItem Text="15" Value="15" Selected="true"/>
                                <f:ListItem Text="20" Value="20" />
                                <f:ListItem Text="25" Value="25" />
                                <f:ListItem Text="所有行" Value="100000" />
                            </f:DropDownList>
                        </PageItems>
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="查看" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true"
        Width="1200px" Height="600px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuView" OnClick="btnMenuView_Click" EnablePostBack="true"
            runat="server" Text="查看" Icon="Pencil" >
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

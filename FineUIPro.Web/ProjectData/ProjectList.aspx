<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectList.aspx.cs" Inherits="FineUIPro.Web.ProjectData.ProjectList" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>项目清单</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="项目信息" 
                EnableCollapse="true" runat="server" BoxFlex="1"  EnableColumnLines="true" 
                DataKeyNames="ProjectId" DataIDField="ProjectId" AllowSorting="true" SortField="ProjectCode" SortDirection="DESC"  
                OnSort="Grid1_Sort"  AllowPaging="true" IsDatabasePaging="true"  PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" 
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:RadioButtonList runat="server" ID="ckState" AutoPostBack="true" 
                                OnSelectedIndexChanged="TextBox_TextChanged" Width="280px">
                                <f:RadioItem Text="全部" Value="0" />
                                <f:RadioItem Text="施工" Value="1" Selected="true" />
                                <f:RadioItem Text="完工/暂停" Value="2" />
                            </f:RadioButtonList>
                            <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>                            
                            <f:TextBox runat="server" Label="名称" ID="txtProjectName" EmptyText="输入查询条件" 
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="200px" LabelWidth="50px"></f:TextBox>                             
                            <f:ToolbarFill runat="server"></f:ToolbarFill>
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                   <f:TemplateField ColumnID="tfNumber" Width="55px" HeaderText="序号" HeaderTextAlign="Center"
                        TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="100px" ColumnID="ProjectCode" DataField="ProjectCode" SortField="ProjectCode" FieldType="String"
                        HeaderText="项目代号">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="ProjectName" DataField="ProjectName" SortField="ProjectName"
                        FieldType="String" HeaderText="项目名称"  HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="ProjectTypeName" DataField="ProjectTypeName" SortField="ProjectTypeName"
                        FieldType="String" HeaderText="项目类型"  HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                    <f:RenderField Width="110px" ColumnID="ProjectMoney" DataField="ProjectMoney" 
                        FieldType="String" HeaderText="合同额(万元)"  HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:TemplateField ColumnID="tfCM" Width="85px" HeaderText="施工经理" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblCM" runat="server" Text='<%# ConvertConstructionManager(Eval("ProjectId")) %>'
                                ToolTip='<%# ConvertConstructionManager(Eval("ProjectId")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField ColumnID="tfSubcontractor" Width="400px" HeaderText="施工分包商" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# ConvertSubcontractor(Eval("ProjectId")) %>'
                                ToolTip='<%# ConvertSubcontractor(Eval("ProjectId")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                      <f:TemplateField ColumnID="tfOwn" Width="150px" HeaderText="建设单位" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# ConvertOwn(Eval("ProjectId")) %>'
                                ToolTip='<%# ConvertOwn(Eval("ProjectId")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                     <f:RenderField Width="150px" ColumnID="ProjectAddress" DataField="ProjectAddress" SortField="ProjectAddress" FieldType="String"
                        HeaderText="项目地址"  HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>    
                     <f:RenderField Width="100px" ColumnID="StartDate" DataField="StartDate" SortField="StartDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="开工日期"
                         HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="EndDate" DataField="EndDate" SortField="EndDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="竣工日期"
                       HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="DayCount" DataField="DayCount"
                        FieldType="String" HeaderText="工程总日历天数" HeaderTextAlign="Center" TextAlign="Right">
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
    <f:Window ID="Window1" Title="项目" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" 
        Width="900px" Height="500px">
    </f:Window>       
    <f:Menu ID="Menu1" runat="server">
          <f:MenuButton ID="btnEnter" EnablePostBack="true" runat="server"  Icon="Outline" Text="进入项目"
                OnClick="btnEnter_Click">
            </f:MenuButton>
         <f:MenuButton ID="btnView" EnablePostBack="true" runat="server"  Icon="Find" Text="查看"
                OnClick="btnView_Click">
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

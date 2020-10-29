<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HazardPrompt.aspx.cs" Inherits="FineUIPro.Web.HSSE.Hazard.HazardPrompt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>风险提示</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .LabelColor
        {
            color: Red;
            font-size:small;
        }   
         .f-grid-row.Yellow
        {
            background-color: Yellow;
            color:blue;
        }              
        .f-grid-row.Red
        {
            background-color:Red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="风险评价清单列表" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="HazardListId"  DataIDField="HazardListId" AllowSorting="true" SortField="CompileDate"
                SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" ForceFit="true"
                PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableColumnLines="true" EnableTextSelection="True">
                 <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>      
                            <f:Label ID="Label1" runat="server" Text="列表颜色状态说明：1、未提醒，黄色；2、已提醒未响应，红色；3、已响应，白色。"  CssClass="LabelColor"></f:Label>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
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
                    <f:RenderField Width="250px" ColumnID="HazardListCode" DataField="HazardListCode"
                        SortField="HazardListCode" FieldType="String" HeaderText="编号" EnableFilter="true"
                        HeaderTextAlign="Center" TextAlign="Center">                        
                    </f:RenderField>
                    <f:TemplateField ColumnID="tfVersionNo" Width="100px" HeaderText="版本号" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblVersionNo" runat="server" Text='<%# Bind("VersionNo") %>' ToolTip='<%#Bind("VersionNo") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField ColumnID="tfWorkStage" Width="220px" HeaderText="工作阶段" HeaderTextAlign="Center" TextAlign="Left"
                        ExpandUnusedSpace="true">
                        <ItemTemplate>
                            <asp:Label ID="lblWorkStage" runat="server" Text='<%# ConvertWorkStage(Eval("WorkStage")) %>'
                                ToolTip='<%# ConvertWorkStage(Eval("WorkStage")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="100px" ColumnID="UserName" DataField="UserName" SortField="UserName"
                        FieldType="String" HeaderText="编制人" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="95px" ColumnID="CompileDate" DataField="CompileDate" SortField="CompileDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="编制日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="FlowOperateName" DataField="FlowOperateName"
                        SortField="FlowOperateName" FieldType="String" HeaderText="状态" HeaderTextAlign="Center"
                        TextAlign="Left">
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
    <f:Window ID="Window1" Title="提示" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="1200px"
        Height="600px">
    </f:Window>
    <f:Window ID="Window2" Title="响应" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="900px"
        Height="600px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuToolTip" OnClick="btnMenuToolTip_Click" EnablePostBack="true" Hidden="true"
            runat="server" Text="提示" Icon="Information">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuResponse" OnClick="btnMenuResponse_Click" EnablePostBack="true" Hidden="true"
            runat="server" Text="响应" Icon="CommentPlay">
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

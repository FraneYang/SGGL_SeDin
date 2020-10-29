<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagerMonth_SeDin.aspx.cs" Inherits="FineUIPro.Web.HSSE.Manager.ManagerMonth_SeDin" %>

<!DOCTYPE html>   
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>月报</title>
     <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style type="text/css">       
        .LabelColor
        {
            color: Red;
            font-size:small;
        } 
        .f-grid-row.Green
        {
            background-color: LightGreen;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="月报" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="MonthReportId" DataIDField="MonthReportId" 
                AllowSorting="true" SortField="ReporMonth" SortDirection="DESC" OnSort="Grid1_Sort"  ForceFit="true"
                EnableColumnLines="true" AllowPaging="true" IsDatabasePaging="true" PageSize="10"  OnPageIndexChange="Grid1_PageIndexChange"                 
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True" >
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox runat="server" Label="月份" ID="txtReporMonth" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelWidth="50px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill> 
                              <f:DatePicker runat="server"  DateFormatString="yyyy-MM" Label="月报月份" EmptyText="请选择年月" 
                                ID="txtMonth" LabelAlign="right" DisplayType="Month" ShowTodayButton="false">
                            </f:DatePicker>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" runat="server"
                                OnClick="btnNew_Click" Hidden="true">
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
                    <f:RenderField Width="85px" ColumnID="ReporMonth" DataField="ReporMonth" SortField="ReporMonth"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM" HeaderText="月份"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>      
                     <f:RenderField Width="100px" ColumnID="StartDate" DataField="StartDate" SortField="StartDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="开始日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="EndDate" DataField="EndDate" SortField="EndDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="结束日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                      <f:RenderField Width="100px" ColumnID="DueDate" DataField="DueDate" SortField="DueDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="截止日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="CompileManName" DataField="CompileManName" 
                        SortField="ReportManName" FieldType="String" HeaderText="编制人" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="AuditManName" DataField="AuditManName" 
                        SortField="ReportManName" FieldType="String" HeaderText="审核人" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="ApprovalManName" DataField="ApprovalManName" 
                        SortField="ReportManName" FieldType="String" HeaderText="批准人" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                      <f:RenderField Width="90px" ColumnID="StateName" DataField="StateName" 
                        SortField="StateName" FieldType="String" HeaderText="状态" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                 <%--    <f:RenderField Width="250px" ColumnID="ThisSummary" DataField="ThisSummary" 
                         FieldType="String" HeaderText="本月HSE活动综述" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>--%>
             <%--         <f:RenderField Width="250px" ColumnID="NextPlan" DataField="NextPlan" ExpandUnusedSpace="true"
                         FieldType="String" HeaderText="下月HSE工作计划" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>--%>
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
                    <f:ToolbarFill ID="ToolbarFill2" runat="server">
                    </f:ToolbarFill>
                    <%-- <f:Label ID="Label2" runat="server" Text="说明：绿色-未冻结；白色-已冻结。"  CssClass="LabelColor"></f:Label>--%>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="编辑月报" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" OnClose="Window1_Close"
        Width="1300px" Height="600px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <Items>
            <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Hidden="true"
                Text="编辑" Icon="Pencil" OnClick="btnMenuModify_Click">
            </f:MenuButton>
               <f:MenuButton ID="btnPrinter" EnablePostBack="true" runat="server" 
                    Text="导出" Icon="Printer" OnClick="btnPrinter_Click" EnableAjax="false" DisableControlBeforePostBack="false">
                </f:MenuButton>
            <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server" Hidden="true"
                Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？" OnClick="btnMenuDel_Click">
            </f:MenuButton>
        </Items>
    </f:Menu>
    </form>
    <script type="text/javascript">
        var menuID = '<%= Menu1.ClientID %>';
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SafetyQuarterlyReport.aspx.cs" Inherits="FineUIPro.Web.HSSE.InformationProject.SafetyQuarterlyReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>安全生产数据季报</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
   <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全生产数据季报" EnableCollapse="true"
                runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="SafetyQuarterlyReportId"
                DataIDField="SafetyQuarterlyReportId" AllowSorting="true" ForceFit="true"
                SortField="CompileDate" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                           <f:DropDownList ID="drpYear" runat="server" Label="年度" LabelAlign="Right" Width="250px"
                                AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                            </f:DropDownList>
                            <f:DropDownList ID="drpQuarters" runat="server" Label="季度" LabelAlign="Right" Width="250px"
                                AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                            </f:DropDownList>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" Hidden="true"
                                runat="server">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                        TextAlign="Center" />
                    <f:RenderField Width="250px" ColumnID="UnitName" DataField="UnitName" 
                        SortField="UnitName" FieldType="String" HeaderText="填报单位" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="YearAndQuarters" DataField="YearAndQuarters" 
                        SortField="YearAndQuarters" FieldType="String" HeaderText="季报时间" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="100px" ColumnID="CompileManName" DataField="CompileManName"
                        SortField="CompileManName" FieldType="String" HeaderText="填报人" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="110px" ColumnID="CompileDate" DataField="CompileDate"
                        SortField="CompileDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                        HeaderText="填报日期" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="FlowOperateName" DataField="FlowOperateName"
                        SortField="FlowOperateName" FieldType="String" HeaderText="状态" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>                  
                    <f:TemplateField HeaderText="打印" Width="80px">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnPrint" runat="server" Text="打印" OnClick="btnPrint_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </f:TemplateField>
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
    <f:Window ID="Window1" Title="编辑安全生产数据季报" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="1000px" Height="560px">
    </f:Window>
    <f:Window ID="Window2" Title="打印安全生产数据季报" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="1000px" Height="560px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="Pencil" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除">
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

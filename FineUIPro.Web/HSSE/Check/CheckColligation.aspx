<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckColligation.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Check.CheckColligation" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="~/Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <title>综合检查</title>
    <style type="text/css">
        .labcenter
        {
            text-align: center;
        }
          .LabelColor
        {
            color: Red;
            font-size:small;
        } 
         .f-grid-row.Yellow
        {
            background-color: Yellow;
        } 
        .f-grid-row.Green
        {
            background-color: LightGreen;
        }
         .f-grid-row.Red
        {
            background-color: Red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="综合检查" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="NewChcekId"  DataIDField="NewChcekId" ForceFit="true"
                AllowSorting="true" SortField="CheckTime" SortDirection="DESC" OnSort="Grid1_Sort" 
                EnableColumnLines="true" AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox runat="server" Label="单位" ID="txtUnitName" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="50px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="区域" ID="txtWorkAreaName" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="200px" LabelWidth="50px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="开始日期" ID="txtStartTime"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelAlign="right" Width="210px" LabelWidth="80px">
                            </f:DatePicker>
                            <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="结束日期" ID="txtEndTime"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelAlign="right" Width="210px" LabelWidth="80px">
                            </f:DatePicker>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                Hidden="true">
                            </f:Button>
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:TemplateField ColumnID="tfPageIndex" Width="55px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                        EnableLock="true" Locked="False">
                        <ItemTemplate>
                            <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="120px" ColumnID="CheckColligationCode" DataField="CheckColligationCode"
                        SortField="CheckColligationCode" FieldType="String" HeaderText="检查编号" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                     <f:RenderField Width="80px" ColumnID="CheckCount" DataField="CheckCount"
                        SortField="CheckCount" FieldType="Int" HeaderText="不合格数" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>                   
                    <f:RenderField Width="120px" ColumnID="WorkArea" DataField="WorkArea" SortField="WorkArea"
                        FieldType="String" HeaderText="检查区域" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="UnitName" DataField="UnitName"
                        SortField="UnitName" FieldType="String" HeaderText="责任单位" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="280px" ColumnID="Unqualified" DataField="Unqualified"
                        SortField="Unqualified" FieldType="String" HeaderText="不合格项描述" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                     <f:RenderField Width="70px" ColumnID="CompleteStatusName" DataField="CompleteStatusName"
                        SortField="CompleteStatusName" FieldType="String" HeaderText="整改" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="CompletedDate" DataField="CompletedDate" SortField="CompletedDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="闭环时间"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="CheckPersonName" DataField="CheckPersonName"
                        SortField="CheckPersonName" FieldType="String" HeaderText="检查人" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="CheckTime" DataField="CheckTime" SortField="CheckTime"
                        FieldType="String" HeaderText="检查日期" TextAlign="Center" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="FlowOperateName" DataField="FlowOperateName"
                        SortField="FlowOperateName" FieldType="String" HeaderText="状态" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                </Columns>
                <Listeners>
                    <f:Listener Event="dataload" Handler="onGridDataLoad" />
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
                     <f:ToolbarFill runat="server">
                    </f:ToolbarFill>
                     <f:Label ID="Label2" runat="server" Text="说明：绿色-未审核完成；黄色-未闭环；白色-已闭环。"  CssClass="LabelColor"></f:Label>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="编辑综合检查" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
        Width="1300px" Height="660px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <Items>
            <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Hidden="true"
                Text="修改" Icon="Pencil" OnClick="btnMenuModify_Click" >
            </f:MenuButton>
       <%--     <f:MenuButton ID="btnMenuRectify" EnablePostBack="true" runat="server" Hidden="true"
                Text="生成整改单" Icon="ScriptLightning" OnClick="btnMenuRectify_Click">
            </f:MenuButton>--%>
            <f:MenuButton ID="btnCompletedDate" EnablePostBack="true" runat="server" Hidden="true"
                Text="闭环" Icon="TimeGreen" OnClick="btnCompletedDate_Click" >
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

        function onGridDataLoad(event) {
            this.mergeColumns(['CheckColligationCode', 'CheckCount', 'CheckPersonName', 'CheckTime', 'FlowOperateName'], { depends: true });
//            this.mergeColumns(['CheckCount']);
//            this.mergeColumns(['CheckPersonName']);
//            this.mergeColumns(['CheckTime']);
//            this.mergeColumns(['FlowOperateName']);
        }
    </script>
</body>
</html>

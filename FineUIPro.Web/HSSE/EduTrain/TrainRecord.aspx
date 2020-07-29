<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainRecord.aspx.cs" Inherits="FineUIPro.Web.HSSE.EduTrain.TrainRecord" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>培训记录</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="培训记录" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="TrainingId" EnableColumnLines="true" DataIDField="TrainingId" AllowSorting="true"
                    SortField="TrainingCode" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" ForceFit="true"
                    IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                    EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"
                    EnableTextSelection="True" EnableSummary="true" SummaryPosition="Flow">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                            <Items>
                                <f:DropDownList ID="drpUnitId" runat="server" Label="单位" AutoPostBack="true" OnSelectedIndexChanged="Text_TextChanged"
                                    LabelAlign="Right" LabelWidth="50px" Width="260px">
                                </f:DropDownList>
                                <f:DropDownList ID="drpTrainType" runat="server" Label="培训类别" Width="210px" LabelWidth="80px" LabelAlign="Right"
                                    AutoPostBack="true" OnSelectedIndexChanged="Text_TextChanged">
                                </f:DropDownList>
                                <f:DropDownList ID="drpTrainLevel" runat="server" Label="培训级别" Width="180px" LabelWidth="80px" LabelAlign="Right"
                                    AutoPostBack="true" OnSelectedIndexChanged="Text_TextChanged">
                                </f:DropDownList>
                                <f:DatePicker ID="txtStartDate" runat="server" Label="日期" Width="160px" LabelWidth="60px" LabelAlign="Right"
                                    AutoPostBack="true" OnTextChanged="Text_TextChanged">
                                </f:DatePicker>
                                <f:Label ID="lblTo" runat="server" Text="至" Width="20px">
                                </f:Label>
                                <f:DatePicker ID="txtEndDate" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="Text_TextChanged">
                                </f:DatePicker>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="btnNew" ToolTip="增加" Icon="Add" runat="server" Hidden="true" OnClick="btnNew_Click">
                                </f:Button>
                                <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                    EnableAjax="false" DisableControlBeforePostBack="false">
                                </f:Button>
                                   <f:Button ID="btnRefresh" ToolTip="刷明细" Icon="ArrowRefresh" runat="server" Hidden="true" OnClick="btnRefresh_Click">
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
                        <%--<f:RenderField Width="120px" ColumnID="TrainingCode" DataField="TrainingCode" SortField="TrainingCode"
                        FieldType="String" HeaderText="培训编号" HeaderTextAlign="Center" TextAlign="Left">                        
                    </f:RenderField>--%>
                        <f:TemplateField ColumnID="tfTrainingCode" Width="100px" HeaderText="培训编号" HeaderTextAlign="Center"
                            TextAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblTrainingCode" runat="server" Text='<%# Bind("TrainingCode") %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField Width="150px" ColumnID="TrainTitle" DataField="TrainTitle" SortField="TrainTitle"
                            FieldType="String" HeaderText="标题" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:TemplateField ColumnID="tfUnitIds" Width="300px" HeaderText="单位名称" HeaderTextAlign="Center" 
                            TextAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblUnitId" runat="server" Text='<%# ConvertUnitName(Eval("UnitIds")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField Width="110px" ColumnID="TrainTypeName" DataField="TrainTypeName" SortField="TrainTypeName"
                            FieldType="String" HeaderText="培训类型" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <%--<f:RenderField Width="120px" ColumnID="TrainLevelName" DataField="TrainLevelName" SortField="TrainLevelName"
                            FieldType="String" HeaderText="培训级别" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>--%>
                        <f:RenderField Width="100px" ColumnID="TrainStartDate" DataField="TrainStartDate"
                            SortField="TrainStartDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                            HeaderText="培训日期" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <%--  <f:RenderField Width="65px" ColumnID="TeachHour" DataField="TeachHour" SortField="TeachHour"
                        FieldType="Float" HeaderText="学时" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="TeachMan" DataField="TeachMan" SortField="TeachMan"
                        FieldType="String" HeaderText="授课人" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>--%>
                        <f:RenderField Width="80px" ColumnID="TrainPersonNum" DataField="TrainPersonNum"
                            SortField="TrainPersonNum" FieldType="String" HeaderText="人数" HeaderTextAlign="Center"
                            TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="140px" ColumnID="FlowOperateName" DataField="FlowOperateName"
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
        <f:Window ID="Window1" Title="编制培训记录" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
            Width="1200px" Height="800px">
        </f:Window>
        <f:Window ID="Window2" Title="打印培训记录" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="1024px"
            Height="768px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
             <f:MenuButton ID="btnMenuView" OnClick="btnMenuView_Click" Icon="Find" EnablePostBack="true"
                runat="server" Text="查看">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
                runat="server" Icon="Pencil" Hidden="true" Text="编辑">
            </f:MenuButton>
            <f:MenuButton ID="btnPrint" OnClick="btnPrint_Click" EnablePostBack="true"
                runat="server" Icon="Printer" Text="打印">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
                ConfirmText="删除选中行？" Icon="Delete" Hidden="true" ConfirmTarget="Parent" runat="server"
                Text="删除">
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

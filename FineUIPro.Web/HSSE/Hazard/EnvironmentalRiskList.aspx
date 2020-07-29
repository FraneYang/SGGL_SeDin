<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnvironmentalRiskList.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Hazard.EnvironmentalRiskList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>环境危险源辨识与评价</title>
      <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />   
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="环境危险源辨识与评价" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="EnvironmentalRiskListId" DataIDField="EnvironmentalRiskListId" 
                AllowSorting="true" SortField="CompileDate" SortDirection="DESC" OnSort="Grid1_Sort" 
                AllowPaging="true" IsDatabasePaging="true"  EnableColumnLines="true" PageSize="10" 
                OnPageIndexChange="Grid1_PageIndexChange" EnableTextSelection="True" ForceFit="true"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                            <f:TextBox runat="server" Label="单位工程" ID="txtWorkAreaName" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" LabelWidth="80px" LabelAlign="right" Width="210">
                            </f:TextBox>
                            <f:DropDownList ID="drpUnitId" runat="server" Label="单位" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged"
                                LabelWidth="70px" Width="250px">
                            </f:DropDownList>
                            <f:DatePicker runat="server" Label="辨识日期" ID="txtStartDate" LabelWidth="100px" AutoPostBack="true"
                                Width="210px" OnTextChanged="TextBox_TextChanged" LabelAlign="right">
                            </f:DatePicker>
                            <f:DatePicker runat="server" Label="至" ID="txtEndDate" LabelWidth="40px" AutoPostBack="true"
                                Width="160px" OnTextChanged="TextBox_TextChanged" LabelAlign="right">
                            </f:DatePicker>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNew_Click"
                                Hidden="true">
                            </f:Button>
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
                    <f:RenderField Width="150px" ColumnID="EnvironmentalRiskListCode" DataField="EnvironmentalRiskListCode"
                        SortField="EnvironmentalRiskListCode" FieldType="String" HeaderText="编号" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="WorkAreaName" DataField="WorkAreaName" ExpandUnusedSpace="true"
                        SortField="WorkAreaName" FieldType="String" HeaderText="单位工程" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="IdentificationDate" DataField="IdentificationDate"
                        SortField="IdentificationDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                        HeaderText="辨识时间" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="ControllingPersonName" DataField="ControllingPersonName"
                        SortField="ControllingPersonName" FieldType="String" HeaderText="控制责任人" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="CompileManName" DataField="CompileManName"
                        SortField="CompileManName" FieldType="String" HeaderText="编制人" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="CompileDate" DataField="CompileDate" SortField="CompileDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="编制日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>                    
                    <f:WindowField TextAlign="Center" Width="90px" WindowID="WindowAtt" HeaderText="附件"
                        Text="详细" ToolTip="附件上传查看" DataIFrameUrlFields="EnvironmentalRiskListId" DataIFrameUrlFormatString="../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckDay&menuId=762F0BF9-471B-4115-B35E-03A26C573877"
                        HeaderTextAlign="Center" ColumnID="attWindow" />
                    <f:RenderField Width="150px" ColumnID="FlowOperateName" DataField="FlowOperateName"
                        ExpandUnusedSpace="true" SortField="FlowOperateName" FieldType="String" HeaderText="状态"
                        HeaderTextAlign="Center" TextAlign="Left">
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
    <f:Window ID="Window1" Title="环境危险源辨识与评价" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1200px" Height="700px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="附件页面" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Hidden="true"
            Text="修改" Icon="Pencil" OnClick="btnMenuModify_Click">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server" Hidden="true"
            Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？" OnClick="btnMenuDel_Click">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuAllOut" EnablePostBack="true" runat="server" Text="导出危险源"
            Icon="FolderUp" Hidden="true">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuImportantOut" EnablePostBack="true" runat="server" Text="导出重要危险源"
            Icon="FolderPage" Hidden="true">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuAllPrint" EnablePostBack="true" runat="server" Hidden="true"
            Icon="Printer" Text="打印危险源" OnClick="btnMenuAllPrint_Click">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuImportantPrint" EnablePostBack="true" runat="server" Hidden="true"
            Icon="Printer" Text="打印重要危险源" OnClick="btnMenuImportantPrint_Click">
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

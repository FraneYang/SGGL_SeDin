<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HiddenRectificationList.aspx.cs" Inherits="FineUIPro.Web.HSSE.HiddenInspection.HiddenRectificationList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全巡检</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全巡检" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="HazardRegisterId"  DataIDField="HazardRegisterId" 
                AllowSorting="true" SortField="CheckTime" SortDirection="DESC" OnSort="Grid1_Sort" 
                EnableColumnLines="true" AllowPaging="true" IsDatabasePaging="true" PageSize="10" 
                OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true" 
                OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">              
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox runat="server" Label="检查人" ID="txtCheckMan" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" LabelAlign="right" Width="210px" LabelWidth="80px">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="问题类型" ID="txtType" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" LabelAlign="right" Width="210px" LabelWidth="80px">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="单位工程" ID="txtWorkAreaName" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" LabelAlign="right" Width="210px" LabelWidth="80px">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="责任单位" ID="txtResponsibilityUnitName" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelAlign="right" Width="250px"
                                LabelWidth="80px">
                            </f:TextBox>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:DatePicker ID="txtStartTime" runat="server" Label="检查时间" LabelAlign="Right"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="200px" LabelWidth="80px">
                            </f:DatePicker>
                            <f:Label ID="Label3" runat="server" Text="至">
                            </f:Label>
                            <f:DatePicker ID="txtEndTime" runat="server" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"
                                Width="130px">
                            </f:DatePicker>
                            <f:DatePicker ID="txtStartRectificationTime" runat="server" Label="整改时间" LabelAlign="Right"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="200px" LabelWidth="80px">
                            </f:DatePicker>
                            <f:Label ID="Label1" runat="server" Text="至">
                            </f:Label>
                            <f:DatePicker ID="txtEndRectificationTime" runat="server" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"
                                Width="130px">
                            </f:DatePicker>
                            <f:DropDownList ID="drpStates" runat="server" Label="状态" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged"
                                LabelWidth="70px" LabelAlign="Right" Width="170px">
                            </f:DropDownList>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:HiddenField runat="server" ID="hdRemark">
                            </f:HiddenField>
                            <f:Button ID="btnNew" Icon="Add" runat="server" OnClick="btnNew_Click" ToolTip="编制" Hidden="true">
                            </f:Button>
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:TemplateField ColumnID="tfPageIndex" Width="55px" HeaderText="序号" HeaderTextAlign="Center"
                        TextAlign="Center" EnableLock="true" Locked="False">
                        <ItemTemplate>
                            <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>   
                        <f:RenderField Width="200px" ColumnID="RegisterDef" DataField="RegisterDef" SortField="RegisterDef"
                        FieldType="String" HeaderText="问题描述" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="CheckTime" DataField="CheckTime" SortField="CheckTime"
                        HeaderText="检查时间" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                      <f:RenderField Width="100px" ColumnID="RegisterTypes2Name" DataField="RegisterTypes2Name" SortField="RegisterTypes2Name"
                        FieldType="String" HeaderText="危害因素" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                       <f:RenderField Width="120px" ColumnID="RegisterTypesName" DataField="RegisterTypesName"
                        SortField="RegisterTypesName" FieldType="String" HeaderText="问题类型" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>     
                    <f:RenderField Width="100px" ColumnID="RegisterTypes3Name" DataField="RegisterTypes3Name" SortField="RegisterTypes3Name"
                        FieldType="String" HeaderText="作业内容" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="HazardValue" DataField="HazardValue" SortField="HazardValue"
                        FieldType="String" HeaderText="风险值" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                       <f:RenderField Width="120px" ColumnID="RegisterTypes4Name" DataField="RegisterTypes4Name" SortField="RegisterTypes4Name"
                        FieldType="String" HeaderText="导致伤害/事故" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="110px" ColumnID="WorkAreaName" DataField="WorkAreaName" SortField="WorkAreaName"
                        FieldType="String" HeaderText="单位工程" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>                  
                   <f:RenderField Width="200px" ColumnID="ResponsibilityUnitName" DataField="ResponsibilityUnitName"
                        SortField="ResponsibilityUnitName" FieldType="String" HeaderText="责任单位" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="ResponsibilityManName" DataField="ResponsibilityManName"
                        SortField="ResponsibilityManName" FieldType="String" HeaderText="责任人" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                      <f:RenderField Width="120px" ColumnID="Rectification" DataField="Rectification" SortField="Rectification"
                        FieldType="String" HeaderText="整改要求" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="RectificationPeriod" DataField="RectificationPeriod"
                        SortField="RectificationPeriod" FieldType="Date" Renderer="Date" HeaderText="整改期限"
                        TextAlign="Center" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="CCManNames" DataField="CCManNames"
                        FieldType="String" HeaderText="抄送" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="RectificationTime" DataField="RectificationTime"
                        SortField="RectificationTime" HeaderText="整改时间" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                     <f:RenderField Width="90px" ColumnID="CheckManName" DataField="CheckManName" SortField="CheckManName"
                        FieldType="String" HeaderText="检查人" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField> 
                    <f:RenderField Width="150px" ColumnID="RegisterDate" DataField="RegisterDate"
                        SortField="RegisterDate" HeaderText="检查时间" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <%--<f:TemplateField ColumnID="tfImageUrl1" Width="120px" HeaderText="整改前" HeaderTextAlign="Center"
                        TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lbImageUrl" runat="server" Text='<%# ConvertImageUrlByImage(Eval("HazardRegisterId")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField ColumnID="tfImageUrl2" Width="120px" HeaderText="整改后" HeaderTextAlign="Center"
                        TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# ConvertImgUrlByImage(Eval("HazardRegisterId")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>            --%>
                    <f:RenderField Width="90px" ColumnID="StatesStr" DataField="StatesStr" SortField="StatesStr"
                        FieldType="String" HeaderText="状态" HeaderTextAlign="Center" TextAlign="Center">
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
    <f:Window ID="Window1" Title="安全巡检" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" OnClose="Window1_Close"
        Width="900px" Height="600px">
    </f:Window>
    <f:Window ID="Window2" Title="违章处罚通知单" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" OnClose="Window1_Close"
        Width="1024px" Height="600px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <Items>
            <f:MenuButton ID="btnModify" EnablePostBack="true" runat="server" Text="编辑" 
                OnClick="btnMenuModify_Click" Hidden="true" Icon="Pencil">
            </f:MenuButton>
            <f:MenuButton ID="btnRectify" EnablePostBack="true" runat="server" Text="整改" 
                OnClick="btnMenuRectify_Click"  Hidden="true" Icon="TableEdit">
            </f:MenuButton>
            <f:MenuButton ID="btnConfirm" EnablePostBack="true" runat="server" Text="确认" 
                OnClick="btnMenuConfirm_Click"  Hidden="true" Icon="Accept">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuSee" EnablePostBack="true" runat="server" Text="查看" 
                OnClick="btnMenuSee_Click" Icon="Find">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"  Icon="Delete"
                ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除"  Hidden="true">
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

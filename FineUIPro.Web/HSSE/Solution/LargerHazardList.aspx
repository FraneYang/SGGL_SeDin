<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LargerHazardList.aspx.cs" Inherits="FineUIPro.Web.HSSE.Solution.LargerHazardList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>危险性较大的工程清单</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .labcenter
        {
            text-align: center;
        }        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="危险性较大的工程清单" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="HazardId" DataIDField="HazardId" AllowSorting="true" SortField="RecordTime"
                SortDirection="DESC" OnSort="Grid1_Sort" EnableColumnLines="true"  ForceFit="true"
                AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"  EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox runat="server" Label="编号" ID="txtHazardCode" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelWidth="50px"
                                LabelAlign="right" Width="180px">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="类型" ID="txtTypeName" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="180px" LabelWidth="50px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="地点" ID="txtAddress" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="200px" LabelWidth="50px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:DatePicker runat="server" Label="预计时间" ID="txtStartDate" LabelWidth="90px" AutoPostBack="true" Width="200px"
                                OnTextChanged="TextBox_TextChanged" LabelAlign="right">
                            </f:DatePicker>
                            <f:Label ID="lblTO" runat="server" Text="至"></f:Label>
                            <f:DatePicker runat="server" ID="txtEndDate" LabelWidth="40px" AutoPostBack="true" Width="110px"
                                OnTextChanged="TextBox_TextChanged" LabelAlign="right">
                            </f:DatePicker>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
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
                    <f:TemplateField ColumnID="tfNumber" Width="55px" HeaderText="序号" HeaderTextAlign="Center"
                        TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="250px" ColumnID="HazardCode" DataField="HazardCode"
                        SortField="HazardCode" FieldType="String" HeaderText="文件编号" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="180px" ColumnID="TypeName" DataField="TypeName" 
                        SortField="TypeName" FieldType="String" HeaderText="类型" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="180px" ColumnID="Address" DataField="Address" 
                        SortField="Address" FieldType="String" HeaderText="地点" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="RecordTime" DataField="RecordTime" SortField="RecordTime"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="编制时间"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="125px" ColumnID="IsArgumentStr" DataField="IsArgumentStr" SortField="IsArgumentStr"
                        FieldType="String" HeaderText="专家论证" TextAlign="Center" HeaderTextAlign="Center">
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
    <f:Window ID="Window1" Title="编辑危险性较大的工程清单" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" OnClose="Window1_Close"
        Width="1000px" Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <Items>
            <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Hidden="true" Text="修改" Icon="Pencil"
                OnClick="btnMenuModify_Click">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server" Hidden="true" Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？"
                OnClick="btnMenuDel_Click">
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

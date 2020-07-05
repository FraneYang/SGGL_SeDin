<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HazardList.aspx.cs" Inherits="FineUIPro.Web.HSSE.Technique.HazardList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>危险源清单</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="250" Title="危险源清单" TitleToolTip="危险源清单" ShowBorder="true"
                ShowHeader="false" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Center" runat="server">
                        <Items>
                            <f:TextBox ID="txtHazardListType" runat="server" LabelAlign="Right" Width="120px" 
                               EmptyText="输入查询条件">
                            </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnQuery" ToolTip="查询" Icon="SystemSearch" EnablePostBack="true" 	
                                OnClick="Text_TextChanged" runat="server" >
                            </f:Button>
                             <f:Button ID="btnAllOut" ToolTip="导出全部" Icon="FolderUp" runat="server" Hidden="true"
                                OnClick="btnAllOut_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                    <f:Tree ID="trHazardListType" Width="220px" EnableCollapse="true" ShowHeader="true"
                        Title="危险源清单" OnNodeCommand="trHazardListType_NodeCommand" AutoLeafIdentification="true"
                        runat="server" EnableTextSelection="True">
                        <Listeners>
                           <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                           </Listeners>
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" AutoScroll="true"
                Layout="VBox">
                <Items>
                    <f:Grid ID="Grid1" Width="860px" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="HazardId"  ForceFit="true"
                        DataIDField="HazardId" AllowSorting="true" SortField="HazardCode" SortDirection="ASC"
                        OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" PageSize="10" EnableColumnLines="true"
                        OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"
                        EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar3" Position="Top" runat="server">
                                <Items>
                                    <f:TextBox ID="HazardCode" runat="server" Label="代码" EmptyText="输入查询条件"  Width="210px" LabelWidth="80px" LabelAlign="Right">
                                    </f:TextBox>
                                    <f:TextBox ID="HazardListTypeCode" runat="server" Label="类别编号" EmptyText="输入查询条件"
                                       Width="210px" LabelWidth="90px"
                                        LabelAlign="Right">
                                    </f:TextBox>
                                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                                    <f:Button ID="btnQuery2" ToolTip="查询" Icon="SystemSearch" EnablePostBack="true" 	
                                        OnClick="TextBox_TextChanged" runat="server" >
                                    </f:Button>
                                     <f:Button ID="btnNewDetail" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewDetail_Click"
                                        Hidden="true">
                                    </f:Button>
                                    <f:Button ID="btnImport" ToolTip="导入" Icon="ApplicationGet" Hidden="true" runat="server"
                                        OnClick="btnImport_Click">
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
                                    <asp:Label ID="lbNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="120px" HeaderText="代码" HeaderTextAlign="Center" TextAlign="Left" ColumnID="tfHazardCode"
                                SortField="HazardCode">
                                <ItemTemplate>
                                    <asp:Label ID="lbHazardCode" runat="server" Text='<%# Bind("HazardCode") %>' ToolTip='<%#Bind("HazardCode") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="180px" HeaderText="危险因素明细" HeaderTextAlign="Center" TextAlign="Left" ColumnID="tfHazardItems">
                                <ItemTemplate>
                                    <asp:Label ID="lbHazardItems" runat="server" Text='<%# Bind("HazardItems") %>' ToolTip='<%#Bind("HazardItems") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="120px" HeaderText="缺陷类型" HeaderTextAlign="Center" TextAlign="Left" ColumnID="tfDefectsType"
                                SortField="DefectsType">
                                <ItemTemplate>
                                    <asp:Label ID="lbDefectsType" runat="server" Text='<%# Bind("DefectsType") %>' ToolTip='<%#Bind("DefectsType") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="120px" HeaderText="可导致事故" HeaderTextAlign="Center" TextAlign="Left" ColumnID="tfMayLeadAccidents"
                                SortField="MayLeadAccidents">
                                <ItemTemplate>
                                    <asp:Label ID="lbMayLeadAccidents" runat="server" Text='<%# Bind("MayLeadAccidents") %>'
                                        ToolTip='<%#Bind("MayLeadAccidents") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="80px" HeaderText="辅助方法" HeaderTextAlign="Center" TextAlign="Left" ColumnID="tfHelperMethod"
                                SortField="HelperMethod">
                                <ItemTemplate>
                                    <asp:Label ID="lbHelperMethod" runat="server" Text='<%# Bind("HelperMethod") %>'
                                        ToolTip='<%#Bind("HelperMethod") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="60px" ColumnID="HazardJudge_L" DataField="HazardJudge_L" SortField="HazardJudge_L"
                                FieldType="Float" HeaderTextAlign="Center" TextAlign="Center" HeaderText="L">
                            </f:RenderField>
                            <f:RenderField Width="60px" ColumnID="HazardJudge_E" DataField="HazardJudge_E" SortField="HazardJudge_E"
                                FieldType="Float" HeaderTextAlign="Center" TextAlign="Center" HeaderText="E">
                            </f:RenderField>
                            <f:RenderField Width="60px" ColumnID="HazardJudge_C" DataField="HazardJudge_C" SortField="HazardJudge_C"
                                FieldType="Float" HeaderTextAlign="Center" TextAlign="Center" HeaderText="C">
                            </f:RenderField>
                            <f:RenderField Width="60px" ColumnID="HazardJudge_D" DataField="HazardJudge_D" SortField="HazardJudge_D"
                                FieldType="Float" HeaderTextAlign="Center" TextAlign="Center" HeaderText="D">
                            </f:RenderField>
                            <f:RenderField Width="95px" ColumnID="HazardLevel" DataField="HazardLevel" SortField="HazardLevel"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="危险级别">
                            </f:RenderField>
                            <f:TemplateField Width="180px" HeaderText="控制措施" HeaderTextAlign="Center" TextAlign="Left" ColumnID="tfControlMeasures"
                                SortField="ControlMeasures">
                                <ItemTemplate>
                                    <asp:Label ID="lbControlMeasures" runat="server" Text='<%# Bind("ControlMeasures") %>'
                                        ToolTip='<%#Bind("ControlMeasures") %>'></asp:Label>
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
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="编辑危险源与评价清单" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="700px" Height="360px">
    </f:Window>
    <f:Window ID="Window2" Title="编辑危险源清单" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="true"
        Width="800px" Height="460px">
    </f:Window>
    <f:Window ID="Window6" Title="危险源与评价清单" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close" 
        Width="1000px" Height="600px" EnableAjax="false">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑" Icon="Pencil">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"  Icon="Delete"
            Hidden="true" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除">
        </f:MenuButton>
    </f:Menu>
    <f:Menu ID="Menu2" runat="server">
        <f:MenuButton ID="btnMenuNew2" OnClick="btnMenuNew2_Click" EnablePostBack="true"
            Hidden="true" runat="server" Icon="Add" Text="增加">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuEdit2" OnClick="btnMenuEdit2_Click" EnablePostBack="true"
            Hidden="true" runat="server" Icon="Pencil" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete2" OnClick="btnMenuDelete2_Click" EnablePostBack="true"
            Hidden="true" ConfirmText="删除选中行？" Icon="Delete" ConfirmTarget="Parent" runat="server" Text="删除">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/jscript">
        var menuID = '<%= Menu1.ClientID %>';
        var menuID2 = '<%= Menu2.ClientID %>';
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }

        // 保存当前菜单对应的树节点ID
        var currentNodeId;

        // 返回false，来阻止浏览器右键菜单
        function onTreeNodeContextMenu(event, nodeId) {
            currentNodeId = nodeId;
            F(menuID2).show();
            return false;
        }
    </script>
</body>
</html>

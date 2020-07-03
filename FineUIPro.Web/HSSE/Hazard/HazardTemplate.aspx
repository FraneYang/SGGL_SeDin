<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HazardTemplate.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Hazard.HazardTemplate" %>

<!DOCTYPE html>
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
                <Items>
                    <f:Tree ID="trHazardListType" Width="240px" EnableCollapse="true" ShowHeader="false"
                        Title="危险源清单" OnNodeCommand="trHazardListType_NodeCommand" AutoLeafIdentification="true"
                        runat="server" EnableTextSelection="True">
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" AutoScroll="true"
                Layout="VBox">
                <Items>
                    <f:Grid ID="Grid1" Width="870px" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="HazardId" DataIDField="HazardId" AllowSorting="true" SortField="HazardCode" SortDirection="ASC"
                        OnRowCommand="Grid1_RowCommand" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                        PageSize="10" EnableColumnLines="true" OnPageIndexChange="Grid1_PageIndexChange" 
                        EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar3" Position="Top" runat="server">
                                <Items>
                                    <f:DropDownList ID="drpWorkStages" runat="server" Label="工作阶段" EnableMultiSelect="true"
                                        EnableCheckBoxSelect="true" LabelAlign="Right" Width="600px" EmptyText="-请选择-"
                                        AutoPostBack="true" AutoSelectFirstItem="false" OnSelectedIndexChanged="TextBox_TextChanged">
                                    </f:DropDownList>
                                    <f:RadioButtonList ID="rblIsCompany" runat="server" Width="150px" 
                                        AutoPostBack="true" OnSelectedIndexChanged="rblIsCompany_SelectedIndexChanged" >
                                        <f:RadioItem Value="0" Text="内置" Selected="true" />
                                        <f:RadioItem Value="1" Text="本公司" />
                                    </f:RadioButtonList>
                                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                    </f:ToolbarFill>
                                    <f:Button ID="btnNewDetail" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewDetail_Click"
                                        Hidden="true">
                                    </f:Button>
                                    <f:Button ID="btnSave" ToolTip="确认" Icon="Accept" runat="server" OnClick="btnSave_Click">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:CheckBoxField ColumnID="ckbIsSelected" Width="60px" RenderAsStaticField="false"
                                AutoPostBack="true" CommandName="IsSelected" HeaderText="选择" HeaderTextAlign="Center" />
                            <f:TemplateField Width="130px" HeaderText="代码" HeaderTextAlign="Center" TextAlign="Left"
                                SortField="HazardCode">
                                <ItemTemplate>
                                    <asp:Label ID="lblHazardCode" runat="server" Text='<%# Bind("HazardCode") %>' ToolTip='<%#Bind("HazardCode") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="110px" HeaderText="类别编号" HeaderTextAlign="Center" TextAlign="Left"
                                SortField="HazardListTypeCode">
                                <ItemTemplate>
                                    <asp:Label ID="lblHazardListTypeCode" runat="server" Text='<%# Bind("HazardListTypeCode") %>'
                                        ToolTip='<%#Bind("HazardListTypeCode") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="200px" HeaderText="危险因素明细" HeaderTextAlign="Center" TextAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblHazardItems" runat="server" Text='<%# Bind("HazardItems") %>' ToolTip='<%#Bind("HazardItems") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="100px" HeaderText="缺陷类型" HeaderTextAlign="Center" TextAlign="Left"
                                SortField="DefectsType">
                                <ItemTemplate>
                                    <asp:Label ID="lblDefectsType" runat="server" Text='<%# Bind("DefectsType") %>' ToolTip='<%#Bind("DefectsType") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="120px" HeaderText="可能导致的事故" HeaderTextAlign="Center" TextAlign="Left"
                                SortField="MayLeadAccidents">
                                <ItemTemplate>
                                    <asp:Label ID="lblMayLeadAccidents" runat="server" Text='<%# Bind("MayLeadAccidents") %>'
                                        ToolTip='<%#Bind("MayLeadAccidents") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="80px" HeaderText="辅助方法" HeaderTextAlign="Center" TextAlign="Left"
                                SortField="HelperMethod">
                                <ItemTemplate>
                                    <asp:Label ID="lblHelperMethod" runat="server" Text='<%# Bind("HelperMethod") %>'
                                        ToolTip='<%#Bind("HelperMethod") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="95px" ColumnID="HazardJudge_L" DataField="HazardJudge_L" SortField="HazardJudge_L"
                                FieldType="Float" HeaderTextAlign="Center" TextAlign="Center" HeaderText="危险评价(L)">
                            </f:RenderField>
                            <f:RenderField Width="95px" ColumnID="HazardJudge_E" DataField="HazardJudge_E" SortField="HazardJudge_E"
                                FieldType="Float" HeaderTextAlign="Center" TextAlign="Center" HeaderText="危险评价(E)">
                            </f:RenderField>
                            <f:RenderField Width="95px" ColumnID="HazardJudge_C" DataField="HazardJudge_C" SortField="HazardJudge_C"
                                FieldType="Float" HeaderTextAlign="Center" TextAlign="Center" HeaderText="危险评价(C)">
                            </f:RenderField>
                            <f:RenderField Width="95px" ColumnID="HazardJudge_D" DataField="HazardJudge_D" SortField="HazardJudge_D"
                                FieldType="Float" HeaderTextAlign="Center" TextAlign="Center" HeaderText="危险评价(D)">
                            </f:RenderField>
                            <f:RenderField Width="95px" ColumnID="HazardLevel" DataField="HazardLevel" SortField="HazardLevel"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="危险级别">
                            </f:RenderField>
                            <f:TemplateField Width="180px" HeaderText="控制措施" HeaderTextAlign="Center" TextAlign="Left"
                                SortField="ControlMeasures">
                                <ItemTemplate>
                                    <asp:Label ID="lblControlMeasures" runat="server" Text='<%# Bind("ControlMeasures") %>'
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
    <f:Window ID="Window2" Title="编辑危险源清单" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="true"
        Width="700px" Height="350px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            runat="server" Text="编辑" Hidden="true" Icon="Pencil">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true" Icon="Delete"
            ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除" Hidden="true">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/jscript">
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

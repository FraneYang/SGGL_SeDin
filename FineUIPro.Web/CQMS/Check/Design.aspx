<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Design.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.Design" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .labcenter {
            text-align: center;
        }

        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }

        .f-grid-row.red {
            background-color: #FF7575;
            background-image: none;
        }

        .f-grid-colheader-text {
            white-space: normal;
            word-break: break-all;
        }

        .fontred {
            color: #FF7575;
            background-image: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Grid ID="Grid1" Title="设计变更记录" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" runat="server" EnableCollapse="true" DataIDField="DesignId" SortField="DesignDate"  SortDirection="DESC"  BoxFlex="1"  AllowCellEditing="true"
                    DataKeyNames="DesignId" EnableColumnLines="true" ClicksToEdit="2" AllowSorting="true" AllowPaging="true" IsDatabasePaging="true" PageSize="10" ForceFit="true"
                    EnableRowDoubleClickEvent="true" AllowFilters="true" EnableTextSelection="True"  OnRowDoubleClick="Grid1_RowDoubleClick" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:DropDownList ID="drpDesignType" runat="server" Label="变更类型" EnableEdit="true" LabelAlign="right" LabelWidth="110px">
                                </f:DropDownList>
                                <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="开始日期" ID="txtStartTime" LabelAlign="right" LabelWidth="110px">
                                </f:DatePicker>
                                <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="结束日期" ID="txtEndTime" LabelAlign="right" LabelWidth="110px">
                                </f:DatePicker>
                                <f:DropDownList ID="drpState" runat="server" Label="审批状态" LabelAlign="Right" EnableEdit="true">
                                        <f:ListItem Text="已闭合" Value="1" />
                                        <f:ListItem Text="未闭合" Value="0" />
                                </f:DropDownList>
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar ID="Toolbar3" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:DropDownList ID="drpUnit" runat="server" Label="实施单位" LabelAlign="Right" EnableEdit="true" LabelWidth="110px">
                                </f:DropDownList>
                                <f:DropDownList ID="drpMainItem" runat="server" Label="主项" LabelAlign="Right" EnableEdit="true" LabelWidth="110px">
                                </f:DropDownList>
                                <f:DropDownList ID="drpCNProfessional" runat="server" Label="专业" LabelAlign="Right" EnableEdit="true" LabelWidth="110px">
                                </f:DropDownList>
                                
                                <f:ToolbarFill runat="server"></f:ToolbarFill>
                                <f:Button ID="btnSearch" Icon="SystemSearch"
                                    EnablePostBack="true" runat="server" OnClick="btnSearch_Click">
                                </f:Button>
                                <f:Button ID="btnRset" OnClick="btnRset_Click" ToolTip="重置" Icon="ArrowUndo" EnablePostBack="true" runat="server">
                                </f:Button>
                                <f:Button ID="btnNew" Icon="Add" EnablePostBack="true"
                                    runat="server">
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
                        <f:RenderField Width="130px" ColumnID="MainItemName" DataField="MainItemName"
                            FieldType="String" HeaderText="主项" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="130px" ColumnID="ProfessionalName" DataField="ProfessionalName"
                            FieldType="String" HeaderText="专业" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:GroupField HeaderText="变更信息" TextAlign="Center">
                            <Columns>
                                <f:RenderField Width="100px" ColumnID="DesignType" DataField="DesignType"
                                    FieldType="String" HeaderText="变更类型" TextAlign="Center" HeaderTextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="110px" ColumnID="DesignCode" DataField="DesignCode"
                                    FieldType="String" HeaderText="变更编号" TextAlign="Center" HeaderTextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="110px" ColumnID="DesignContents" DataField="DesignContents"
                                    FieldType="String" HeaderText="变更内容" TextAlign="Center" HeaderTextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="DesignDate" DataField="DesignDate" FieldType="Date" HeaderText="变更日期" TextAlign="Center" HeaderTextAlign="Center" RendererArgument="yyyy-MM-dd">
                                </f:RenderField>
                            </Columns>
                        </f:GroupField>
                        <f:GroupField HeaderText="变更分析" TextAlign="Center">
                            <Columns>
                                <f:TemplateField ColumnID="CarryUnit" Width="110px" HeaderText="实施单位" HeaderTextAlign="Center" TextAlign="Center"
                                    EnableLock="true" Locked="False" >
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# ConvertCarryUnit(Eval("CarryUnitIds")) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:RenderField Width="80px" ColumnID="IsNoChange" DataField="IsNoChange"
                                    FieldType="String" HeaderText="是否已按原图纸施工" TextAlign="Center" HeaderTextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="80px" ColumnID="IsNeedMaterial" DataField="IsNeedMaterial"
                                    FieldType="String" HeaderText="是否需要增补材料" TextAlign="Center" HeaderTextAlign="Center">
                                </f:RenderField>
                                <f:TemplateField ColumnID="BuyMaterialUnit" Width="110px" HeaderText="增补材料采购方" HeaderTextAlign="Center" TextAlign="Center"
                                    EnableLock="true" Locked="False">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# ConvertCarryUnit(Eval("BuyMaterialUnitIds")) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:RenderField Width="100px" ColumnID="MaterialPlanReachDate" DataField="MaterialPlanReachDate"
                                    FieldType="Date" HeaderText="材料预计到齐时间" TextAlign="Center" HeaderTextAlign="Center" RendererArgument="yyyy-MM-dd">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="PlanDay" DataField="PlanDay"
                                    FieldType="String" HeaderText="预计施工周期(天)" TextAlign="Center" HeaderTextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="PlanCompleteDate" DataField="PlanCompleteDate"
                                    FieldType="Date" HeaderText="计划完成时间" TextAlign="Center" HeaderTextAlign="Center" RendererArgument="yyyy-MM-dd">
                                </f:RenderField>
                            </Columns>
                        </f:GroupField>
                        <f:GroupField HeaderText="变更实施" TextAlign="Center">
                            <Columns>
                                <f:RenderField Width="100px" ColumnID="MaterialRealReachDate" DataField="MaterialRealReachDate"
                                    FieldType="Date" HeaderText="增补材料到齐时间" TextAlign="Center" HeaderTextAlign="Center" RendererArgument="yyyy-MM-dd">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="RealCompleteDate" DataField="RealCompleteDate"
                                    FieldType="Date" HeaderText="施工完成时间" TextAlign="Center" HeaderTextAlign="Center " RendererArgument="yyyy-MM-dd">
                                </f:RenderField>
                            </Columns>
                        </f:GroupField>
                        <f:TemplateField ColumnID="State" Width="110px" HeaderText="审批状态" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# ConvertState(Eval("State")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:TemplateField ColumnID="AuditMan" Width="100px" HeaderText="办理人" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="Label41" runat="server" Text='<%# ConvertMan(Eval("DesignId")) %>'></asp:Label>
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
                        <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                            <f:ListItem Text="10" Value="10" />
                            <f:ListItem Text="15" Value="15" />
                            <f:ListItem Text="20" Value="20" />
                            <f:ListItem Text="25" Value="25" />
                            <f:ListItem Text="所有行" Value="100000" />
                        </f:DropDownList>
                        <f:ToolbarFill runat="server">
                        </f:ToolbarFill>

                    </PageItems>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="设计变更" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" OnClose="Window1_Close"
            Width="1300px" Height="620px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <Items>
                <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Text="修改" Icon="Pencil" OnClick="btnMenuModify_Click" Hidden="true">
                </f:MenuButton>
                <f:MenuButton ID="btnMenuView" EnablePostBack="true" runat="server" Text="查看" Icon="ApplicationViewIcons" OnClick="btnMenuView_Click">
                </f:MenuButton>
                <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server" Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？"
                    OnClick="btnMenuDel_Click" Hidden="true">
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

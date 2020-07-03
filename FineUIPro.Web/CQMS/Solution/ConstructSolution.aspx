<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConstructSolution.aspx.cs" Inherits="FineUIPro.Web.CQMS.Solution.ConstructSolution" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>施工方案</title>
    <style type="text/css">
        .labcenter {
            text-align: center;
        }

        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }

        .LabelColor {
            color: Red;
            font-size: small;
        }

        /*.f-grid-row.LightRedRow {
            background-color: #FF60AF;
        }

        .f-grid-row.LightYellowRow {
            background-color: #FFFF93;
        }

        .f-grid-row.LightGreenRow {
            background-color: #79FF79;
        }*/

        .Green {
            background-color: Green;
        }

        .Yellow {
            background-color: #FFFF93;
        }

        .HotPink {
            background-color: HotPink;
        }

        .LightGreen {
            background-color: LightGreen
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>

                <f:Grid ID="Grid1" ShowBorder="true" EnableAjax="false" ShowHeader="false" Title="施工方案" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="ConstructSolutionId" AllowCellEditing="true"
                    ClicksToEdit="2" DataIDField="ConstructSolutionId" AllowSorting="true" SortField="code"
                    SortDirection="DESC" EnableColumnLines="true"  OnPageIndexChange="Grid1_PageIndexChange"
                    AllowPaging="true" IsDatabasePaging="true" PageSize="10"  ForceFit="true"
                    EnableRowDoubleClickEvent="true" AllowFilters="true" OnRowDoubleClick="Grid1_RowDoubleClick"
                    EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:DropDownList ID="drpModelType" runat="server" Width="350px" Label="模板类别" LabelAlign="Right" EnableEdit="true">
                                    <f:ListItem Text="施工组织设计模板" Value="1" />
                                    <f:ListItem Text="施工方案模板" Value="3" />
                                </f:DropDownList>
                                   <f:DropDownList ID="drpUnitWork" runat="server" Label="单位工程" LabelAlign="Right" EnableEdit="true">
                                </f:DropDownList>
                                    <f:DropDownList ID="drpCNProfessional" runat="server" Label="专业" Width="350px" LabelAlign="Right" EnableEdit="true">
                                </f:DropDownList>
                                <f:Button runat="server" ID="btnDownLoad" Icon="ArrowDown" ToolTip="模板下载">
                                    <Listeners>
                                        <f:Listener Event="click" Handler="ButtonClick" />
                                    </Listeners>
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>

                                <f:DropDownList ID="drpProposeUnit" runat="server" Width="350px" Label="单位名称" LabelAlign="Right" EnableEdit="true">
                                </f:DropDownList>

                                <f:DropDownList ID="drpSolutionType" runat="server" Width="350px" Label="方案类别" LabelAlign="Right" EnableEdit="true">
                                </f:DropDownList>
                                  <f:DropDownList ID="drpState" runat="server" Label="审批状态" LabelAlign="Right" EnableEdit="true" >
                                        <f:ListItem Text="已闭合" Value="1" />
                                        <f:ListItem Text="未闭合" Value="0" />
                                </f:DropDownList>
                                <f:ToolbarFill runat="server"></f:ToolbarFill>

                                <f:Button ID="btnQuery" ToolTip="查询" OnClick="btnQuery_Click" Icon="SystemSearch" EnablePostBack="true" runat="server">
                                </f:Button>
                                <f:Button ID="btnRset" ToolTip="重置" OnClick="btnRset_Click" Icon="ArrowUndo" EnablePostBack="true" runat="server">
                                </f:Button>
                                <f:Button ID="btnNew" ToolTip="编制" Icon="Add" EnablePostBack="false" runat="server">
                                </f:Button>
                            </Items>
                        </f:Toolbar>

                    </Toolbars>
                    <Columns>
                        <f:TemplateField ColumnID="tfPageIndex" Width="50px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField ColumnID="code" DataField="code" Width="50px"
                            SortField="code" FieldType="String" HeaderText="编号" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="SolutionName" DataField="SolutionName" Width="130px"
                            SortField="SolutionName" FieldType="String" HeaderText="方案名称" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>

                        <f:RenderField ColumnID="UnitName" DataField="UnitName" Width="160px" 
                            SortField="UnitName" FieldType="String" HeaderText="施工单位" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                          <f:RenderField ColumnID="UnitWorkIds" Width="120px" DataField="UnitWorkIds"
                            SortField="UnitWorkIds" FieldType="String" HeaderText="单位工程" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="120px" ColumnID="CNProfessionalCodes"  DataField="CNProfessionalCodes" SortField="CNProfessionalCodes"
                            FieldType="String" HeaderText="专业" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>

                        <f:TemplateField ColumnID="SolutionType" Width="100px" HeaderText="方案类别" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# BLL.CQMSConstructSolutionService.ConvertSolutionType(Eval("SolutionType")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>


                        <f:RenderField ColumnID="CompileManName" Width="70px" DataField="CompileManName"
                            SortField="CompileManName" FieldType="String" HeaderText="编制人" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>

                        <f:RenderField Width="100px" ColumnID="CompileDate" DataField="CompileDate" SortField="CompileDate"
                            FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="编制时间" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:TemplateField ColumnID="State" Width="60px" HeaderText="状态" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# BLL.CQMSConstructSolutionService.ConvertState(Eval("State")) %>'></asp:Label>
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
        <f:Window ID="Window1" Title="施工方案" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
            Width="850px" Height="500px">
        </f:Window>
        <f:Window ID="window_tt" Title="施工方案" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" OnClose="window_tt_Close"
            Width="1300px" Height="660px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <Items>
                <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Hidden="true" Text="修改" Icon="Pencil"
                    OnClick="btnMenuModify_Click">
                </f:MenuButton>
                <f:MenuButton ID="btnMenuView" EnablePostBack="true" runat="server" Text="查看" Icon="ApplicationViewIcons"
                    OnClick="btnMenuView_Click">
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

        function ButtonClick(event) {
            // 第一个参数 false 用来指定当前不是AJAX请求
            __doPostBack(false, '', 'ButtonClick');
        }
    </script>
</body>
</html>

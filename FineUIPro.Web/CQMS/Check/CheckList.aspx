<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckList.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.CheckList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <title>质量巡检</title>
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

        .Green  {
            background-color: Green;
            color:white;
        }
        .Yellow  {
            background-color: #FFFF93;
        }

        .Red {
            background-color: red;
        }

        .LightGreen {
            background-color: LightGreen
        }
        .f-grid-colheader-text {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>

                <f:Grid ID="Grid1" ShowBorder="true" EnableAjax="false" ShowHeader="false" Title="质量巡检记录" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="CheckControlCode" AllowCellEditing="true"
                    ClicksToEdit="2" DataIDField="CheckControlCode" AllowSorting="true" SortField="CheckDate"
                    SortDirection="DESC" OnSort="Grid1_Sort" EnableColumnLines="true" ForceFit="true"
                    AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                    EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" AllowFilters="true"
                    OnFilterChange="Grid1_FilterChange" OnRowDataBound="Grid1_RowDataBound" EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:DropDownList ID="drpSponsorUnit" runat="server" Label="施工单位" EnableEdit="true" EmptyText="请选择查询条件"
                                     LabelAlign="right">
                                </f:DropDownList>

                                <f:DropDownList ID="drpUnitWork" runat="server" Label="单位工程" LabelAlign="Right" EnableEdit="true">
                                </f:DropDownList>
                                <f:DropDownList ID="drpCNProfessional" runat="server" Label="专业" LabelAlign="Right" EnableEdit="true">
                                </f:DropDownList>
                                 <f:DropDownList ID="drpQuestionType" runat="server" Label="问题类别" Width="210px" LabelAlign="Right" EnableEdit="true">
                                   </f:DropDownList>
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
              
                                <f:DropDownList ID="dpHandelStatus" runat="server" Label="整改状态" LabelAlign="Right" EnableEdit="true">
                                        <f:ListItem Text="未确认" Value="1" />
                                        <f:ListItem Text="已闭环" Value="2" />
                                        <f:ListItem Text="超期未整改" Value="3" />
                                        <f:ListItem Text="未整改" Value="4" />
                                </f:DropDownList>
                                  <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="开始日期" ID="txtStartTime"
                                      LabelAlign="right" >
                                </f:DatePicker>
                                  <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="结束日期" ID="txtEndTime"
                                      LabelAlign="right"  >
                                </f:DatePicker>
                                   
                                 <f:ToolbarFill runat="server"></f:ToolbarFill>
                                 <f:Button ID="btnQuery" OnClick="btnQuery_Click" ToolTip="查询"    Icon="SystemSearch" EnablePostBack="true" runat="server" >
                                </f:Button>
                                 <f:Button ID="btnRset"  OnClick="btnRset_Click" ToolTip="重置"    Icon="ArrowUndo" EnablePostBack="true" runat="server" >
                                </f:Button>
                                <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                    Hidden="true">
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
                        <f:RenderField  ColumnID="DocCode" DataField="DocCode" 
                            SortField="DocCode" FieldType="String" HeaderText="质量巡检编号" TextAlign="Left" MinWidth="140px"
                            HeaderTextAlign="Center">
                        </f:RenderField>

                        <f:RenderField Width="100px" ColumnID="UnitWorkName" DataField="UnitWorkName"
                            SortField="UnitWorkName" FieldType="String" HeaderText="单位工程名称" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="UnitName" DataField="UnitName"
                            SortField="UnitName" FieldType="String" HeaderText="施工单位" TextAlign="Left" 
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="ProfessionalName" DataField="ProfessionalName" SortField="ProfessionalName"
                            FieldType="String" HeaderText="专业" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="90px" ColumnID="QuestionType" DataField="QuestionType" SortField="QuestionType"
                            FieldType="String" HeaderText="问题类别" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="70px" ColumnID="CheckSite" DataField="CheckSite" SortField="CheckSite"
                            FieldType="String" HeaderText="部位" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="95px" ColumnID="CheckDate" DataField="CheckDate" SortField="CheckDate"
                            FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="巡检日期" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="95px" ColumnID="userName" DataField="userName" SortField="userName"
                            FieldType="String" HeaderText="检查人" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:TemplateField ColumnID="State" Width="100px" HeaderText="审批状态" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# ConvertState(Eval("State")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:TemplateField ColumnID="AuditMan" Width="80px" HeaderText="办理人" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="Label41" runat="server" Text='<%# ConvertMan(Eval("CheckControlCode")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:TemplateField ColumnID="State2" Width="80px" HeaderText="整改状态" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Convertstatus(Eval("CheckControlCode")) %>'></asp:Label>
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
        <f:Window ID="Window1" Title="质量巡检" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" OnClose="Window1_Close"
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
    </script>
</body>
</html>

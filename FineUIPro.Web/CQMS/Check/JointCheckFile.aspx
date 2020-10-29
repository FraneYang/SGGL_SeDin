<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JointCheckFile.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.JointCheckFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <title>施工质量检查记录</title>
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

        .f-grid-row.Yellow {
            background-color: Yellow;
        }

        .f-grid-row.Green {
            background-color: LightGreen;
        }

        .f-grid-row.Red {
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
                <f:Grid ID="GvJoinCheck" ShowBorder="true" EnableAjax="false" ShowHeader="false" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="JointCheckId" AllowCellEditing="true" Title="质量共检"
                    ClicksToEdit="2" DataIDField="JointCheckId" AllowSorting="true" SortField="CheckDate"
                    SortDirection="DESC" EnableColumnLines="true" OnRowCommand="GvJoinCheck_RowCommand" ForceFit="true"
                    AllowPaging="true" IsDatabasePaging="true" PageSize="10" EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:DropDownList ID="drpSponsorUnit" runat="server" Label="受检施工单位" EnableEdit="true" EmptyText="请选择查询条件" Width="380px"
                                    LabelAlign="right" LabelWidth="110px">
                                </f:DropDownList>
                                <f:DropDownList ID="drpCheckType" runat="server" Label="检查类别" EnableEdit="true" EmptyText="请选择查询条件" Width="220px"
                                    LabelAlign="right" LabelWidth="100px">
                                </f:DropDownList>
                                <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="开始日期" ID="txtStartTime"
                                    LabelAlign="right" Width="200px" LabelWidth="80px">
                                </f:DatePicker>
                                <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="结束日期" ID="txtEndTime"
                                    LabelAlign="right" Width="200px" LabelWidth="80px">
                                </f:DatePicker>
                                <f:DropDownList ID="drpState" runat="server" Label="审批状态" EnableEdit="true" EmptyText="请选择查询条件" Width="200px"
                                    LabelAlign="right" LabelWidth="100px">
                                </f:DropDownList>
                                <f:ToolbarFill runat="server"></f:ToolbarFill>
                                <f:Button ID="Button1" OnClick="btnQuery_Click" ToolTip="查询" Icon="SystemSearch" EnablePostBack="true" runat="server">
                                </f:Button>
                                <f:Button ID="Button2" OnClick="btnRset_Click" ToolTip="重置" Icon="ArrowUndo" EnablePostBack="true" runat="server">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:TemplateField ColumnID="tfPageIndex" Width="55px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="lblPageIndex" runat="server" Text='<%# GvJoinCheck.PageIndex * GvJoinCheck.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField Width="200px" ColumnID="JointCheckCode" DataField="JointCheckCode"
                            SortField="JointCheckCode" FieldType="String" HeaderText="编号" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="230px" ColumnID="UnitName" DataField="UnitName"
                            SortField="UnitName" FieldType="String" HeaderText="受检施工单位" TextAlign="Center"
                            HeaderTextAlign="Center" >
                        </f:RenderField>
                        <f:TemplateField ColumnID="CheckType" Width="100px" HeaderText="检查类别" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# ConvertCheckType(Eval("CheckType")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField ColumnID="CheckName" DataField="CheckName" Width="180px"
                            SortField="CheckName" FieldType="String" HeaderText="检查名称" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>

                        <f:RenderField ColumnID="CheckDate" DataField="CheckDate"
                            SortField="CheckDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="共检日期" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="CheckMan" DataField="CheckMan"
                            SortField="CheckMan" FieldType="String" HeaderText="发起人" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:LinkButtonField ColumnID="export" HeaderText="导出" Width="60px" Icon="ArrowUp" CommandName="export" />
                        <f:LinkButtonField HeaderText="附件" ColumnID="download" Width="60px" Icon="ArrowDown" CommandName="download" />
                    </Columns>
                    <Listeners>
                        <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                    </Listeners>
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText2" runat="server" Text="每页记录数：">
                        </f:ToolbarText>
                        <f:DropDownList runat="server" ID="ddlPageSize" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" Width="80px" AutoPostBack="true">
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
        <f:Window ID="Window1" Title="质量共检" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" OnClose="Window1_Close"
            Width="1300px" Height="660px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <Items>

                <f:MenuButton ID="btnMenuView" EnablePostBack="true" runat="server" Text="查看" Icon="ApplicationViewIcons"
                    OnClick="btnMenuView_Click">
                </f:MenuButton>

            </Items>
        </f:Menu>
        <f:Window ID="Windowtt" Title="文件柜" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true"
            Width="700px" Height="500px">
        </f:Window>
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

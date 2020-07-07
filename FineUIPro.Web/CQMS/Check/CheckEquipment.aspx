<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckEquipment.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.CheckEquipment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
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

                <f:Grid ID="Grid1" ShowBorder="true" EnableAjax="false" ShowHeader="false" Title="检试验设备及测量器具" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="CheckEquipmentId" AllowCellEditing="true"
                    ClicksToEdit="2" DataIDField="CheckEquipmentId" AllowSorting="true" SortField="CheckDay"
                    SortDirection="DESC" EnableColumnLines="true" ForceFit="true"
                    AllowPaging="true" IsDatabasePaging="true" PageSize="10"
                    EnableRowDoubleClickEvent="true" AllowFilters="true" EnableTextSelection="True" OnRowDoubleClick="Grid1_RowDoubleClick" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:DropDownList ID="drpUserUnit" runat="server" Label="使用单位" EnableEdit="true" EmptyText="请选择查询条件"
                                    LabelAlign="right" Width="330px" LabelWidth="90px">
                                </f:DropDownList>
                                <f:TextBox runat="server" ID="txtEquipmentName" Label="设备器具名称" LabelWidth="120px" LabelAlign="Right"></f:TextBox>
                                <f:RadioButtonList runat="server" ID="rblIsBeOverdue" Label="是否过期" AutoPostBack="true" LabelWidth="80px" Width="200px">
                                    <f:RadioItem Text="是" Value="true" />
                                    <f:RadioItem Text="否" Value="false" />
                                </f:RadioButtonList>
                                <f:ToolbarFill runat="server"></f:ToolbarFill>
                                <f:Button ID="btnQuery" OnClick="btnQuery_Click" ToolTip="查询" Icon="SystemSearch" EnablePostBack="true" runat="server">
                                </f:Button>
                                <f:Button ID="btnRset" OnClick="btnRset_Click" ToolTip="重置" Icon="ArrowUndo" EnablePostBack="true" runat="server">
                                </f:Button>
                                <f:Button ID="btnNew" Icon="Add" EnablePostBack="true"
                                    runat="server" OnClick="btnNew_Click">
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
                        <f:RenderField Width="110px" ColumnID="EquipmentName" DataField="EquipmentName"
                            FieldType="String" HeaderText="设备器具名称" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="UserUnit" DataField="UserUnit" 
                            FieldType="String" HeaderText="使用单位" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="60px" ColumnID="Format" DataField="Format"
                            FieldType="String" HeaderText="规格型号" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="50px" ColumnID="SetAccuracyGrade" DataField="SetAccuracyGrade"
                            FieldType="String" HeaderText="规定精度等级" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="50px" ColumnID="RealAccuracyGrade" DataField="RealAccuracyGrade"
                            FieldType="String" HeaderText="实际精度等级" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="50px" ColumnID="CheckCycle" DataField="CheckCycle"
                            FieldType="String" HeaderText="检定周期(年)" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="CheckDay" DataField="CheckDay" FieldType="Date" HeaderText="检定日" TextAlign="Center" HeaderTextAlign="Center" RendererArgument="yyyy-MM-dd">
                        </f:RenderField>
                        <f:RenderField Width="60px" ColumnID="IsIdentification" DataField="IsIdentification"
                            FieldType="String" HeaderText="是否有标识" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="60px" ColumnID="IsCheckCertificate" DataField="IsCheckCertificate"
                            FieldType="String" HeaderText="是否有检定证书" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:TemplateField ColumnID="IsbeOverdue" Width="80px" HeaderText="是否过期" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# ConvertIsBeOverdue(Eval("CheckCycle"),Eval("CheckDay")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField Width="80px" ColumnID="Isdamage" DataField="Isdamage"
                            FieldType="String" HeaderText="状态" TextAlign="Center" HeaderTextAlign="Center">
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
                                <asp:Label ID="Label41" runat="server" Text='<%# ConvertMan(Eval("CheckEquipmentId")) %>'></asp:Label>
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
        <f:Window ID="Window1" Title="编辑检试验设备" Hidden="true" EnableIFrame="true" EnableMaximize="true" OnClose="Window1_Close"
            Target="Parent" EnableResize="true" runat="server" IsModal="true"
            Width="1000px" Height="700px">
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WelderManage.aspx.cs" Inherits="FineUIPro.Web.HJGL.PersonManage.WelderManage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊工管理</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />

     <style type="text/css">
        .f-grid-row.color1,
        .f-grid-row.color1 .f-icon,
        .f-grid-row.color1 a {
            background-color: orange;
            color: #fff;
        }

        .f-grid-row.color3,
        .f-grid-row.color3 .f-icon,
        .f-grid-row.color3 a {
            background-color: red;
            color: #fff;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="焊工管理"
                EnableCollapse="true" runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="PersonId"
                AllowCellEditing="true" ClicksToEdit="2" DataIDField="PersonId" AllowSorting="true"
                SortField="PersonId" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True"
                OnRowDataBound="Grid1_RowDataBound">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>
                            <f:DropDownList ID="drpUnit" runat="server" Label="单位" LabelAlign="Right" LabelWidth="70px"
                                Width="280px">
                             </f:DropDownList>
                            <f:TextBox ID="txtWelderCode" runat="server" Label="焊工号"
                                EmptyText="输入查询条件" Width="240px" LabelWidth="100px"
                                LabelAlign="Right">
                            </f:TextBox>
                            <f:TextBox ID="txtWelderName" runat="server" Label="焊工姓名"
                                EmptyText="输入查询条件" Width="240px" LabelWidth="100px"
                                LabelAlign="Right">
                            </f:TextBox>
                            <f:Button ID="btnQuery" ToolTip="查询" Icon="SystemSearch"
                                EnablePostBack="true" OnClick="btnQuery_Click" runat="server">
                            </f:Button>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="true"
                                runat="server" OnClick="btnNew_Click" Hidden="true">
                            </f:Button>
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出"
                                Icon="TableGo" EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RenderField Width="180px" ColumnID="UnitName" DataField="UnitName" FieldType="String"
                        HeaderText="单位名称" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="UnitName">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="WelderCode" DataField="WelderCode" FieldType="String"
                        HeaderText="焊工号" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="WelderCode">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="PersonName" DataField="PersonName" FieldType="String"
                        HeaderText="焊工姓名" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="WelderName">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="Sex" DataField="Sex" FieldType="String"
                        HeaderText="性别" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="Birthday" DataField="Birthday" FieldType="Date"
                        HeaderText="出生日期" HeaderTextAlign="Center" TextAlign="Left"
                        Renderer="Date">
                    </f:RenderField>
                    <f:RenderField Width="180px" ColumnID="CertificateCode" DataField="CertificateCode"
                        FieldType="String" HeaderText="证书编号" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="CertificateLimitTime" DataField="CertificateLimitTime"
                        FieldType="Date" HeaderText="有效期" HeaderTextAlign="Center"
                        TextAlign="Left" Renderer="Date">
                    </f:RenderField>
                    <f:RenderField Width="110px" ColumnID="WelderLevel" DataField="WelderLevel" FieldType="String"
                        HeaderText="焊工等级" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="90px" ColumnID="IsUsed" DataField="IsUsed" FieldType="String"
                        HeaderText="是否在岗" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:WindowField HeaderTextAlign="Center" TextAlign="Center" Width="150px" WindowID="Window2"
                        DataIFrameUrlFields="PersonId" DataIFrameUrlFormatString="WelderItem.aspx?PersonId={0}"
                        Text="焊工资质" HeaderText="焊工资质">
                    </f:WindowField>
                    <f:WindowField HeaderTextAlign="Center" TextAlign="Center" Width="80px" WindowID="Window3" Hidden="true"
                        DataIFrameUrlFields="PersonId" DataIFrameUrlFormatString="WelderPrint.aspx?PersonId={0}"
                        Text="焊工资质" ToolTip="焊工技能考核证书" HeaderText="焊工资质">
                    </f:WindowField>
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
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true"
        EnableMaximize="true" Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close"
        IsModal="true" Width="960px" Height="500px">
    </f:Window>
    <f:Window ID="Window2" Title="弹出窗体" Hidden="true" EnableIFrame="true"
        EnableMaximize="true" Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close"
        IsModal="true" Width="1200px" Height="600px">
    </f:Window>
    <f:Window ID="Window3" Title="弹出窗体" Hidden="true" EnableIFrame="true"
        EnableMaximize="true" Target="Top" EnableResize="true" runat="server" IsModal="true"
        Width="600px" Height="650px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="BulletEdit" EnablePostBack="true"
            runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Top"
            runat="server" Text="删除">
        </f:MenuButton>
        <f:MenuButton ID="btnView" OnClick="btnView_Click" Icon="Find" EnablePostBack="true"
            runat="server" Text="查看">
        </f:MenuButton>
        <f:MenuButton ID="btnPrint" OnClick="btnPrint_Click" Icon="Printer" EnablePostBack="true"
            runat="server" Text="焊工资质">
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

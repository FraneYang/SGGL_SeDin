<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckerManage.aspx.cs" Inherits="FineUIPro.Web.HJGL.PersonManage.CheckerManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无损检测工信息</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager runat="server" ID="PageManager1" AutoSizePanelID="Panel1" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false" ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="无损检测工信息"
                    EnableCollapse="true" runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="PersonId"
                    AllowCellEditing="true" ClicksToEdit="2" DataIDField="PersonId" AllowSorting="true"
                    SortField="PersonId" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                    IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                    EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"  OnRowCommand="Grid1_RowCommand" EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar runat="server">
                            <Items>
                                <f:DropDownList ID="drpUnit" runat="server" Label="单位" LabelAlign="Right" LabelWidth="70px"
                                    Width="280px">
                                </f:DropDownList>
                                <f:TextBox ID="txtCheckerCode" runat="server" Label="检测工号"
                                    EmptyText="输入查询条件" Width="240px" LabelWidth="100px"
                                    LabelAlign="Right">
                                </f:TextBox>
                                <f:TextBox ID="txtCHeckerName" runat="server" Label="检测工姓名"
                                    EmptyText="输入查询条件" Width="240px" LabelWidth="100px"
                                    LabelAlign="Right">
                                </f:TextBox>
                                <f:Button ID="btnSearch" ToolTip="查询" Icon="SystemSearch"
                                    EnablePostBack="true" runat="server" OnClick="btnSearch_Click">
                                </f:Button>
                                <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="true"
                                    runat="server" OnClick="btnNew_Click">
                                </f:Button>
                                <f:Button ID="btnOut"  runat="server" ToolTip="导出"
                                    Icon="TableGo" EnableAjax="false" DisableControlBeforePostBack="false" OnClick="btnOut_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:RenderField Width="180px" ColumnID="UnitName" DataField="UnitName" FieldType="String"
                            HeaderText="单位名称" HeaderTextAlign="Center" TextAlign="Left"
                            SortField="UnitName">
                        </f:RenderField>
                        <f:RenderField Width="180px" ColumnID="WelderCode" DataField="WelderCode" FieldType="String"
                            HeaderText="检测工号" HeaderTextAlign="Center" TextAlign="Left"
                            SortField="CheckerCode">
                        </f:RenderField>
                        <f:RenderField Width="180px" ColumnID="PersonName" DataField="PersonName" FieldType="String"
                            HeaderText="检测工姓名" HeaderTextAlign="Center" TextAlign="Left"
                            SortField="PersonName">
                        </f:RenderField>
                        <f:RenderField Width="180px" ColumnID="Sex" DataField="Sex" FieldType="String"
                            HeaderText="性别" HeaderTextAlign="Center" TextAlign="Left"
                            SortField="Sex">
                        </f:RenderField>
                        <f:RenderField Width="180px" ColumnID="Birthday" DataField="Birthday" FieldType="Date"
                            HeaderText="生日" HeaderTextAlign="Center" TextAlign="Left"
                            SortField="Birthday" Renderer="Date">
                        </f:RenderField>
                        <f:RenderField Width="180px" ColumnID="IdentityCard" DataField="IdentityCard" FieldType="String"
                            HeaderText="身份证号" HeaderTextAlign="Center" TextAlign="Left"
                            SortField="IdentityCard">
                        </f:RenderField>
                        <f:RenderField Width="180px" ColumnID="IsUsed" DataField="IsUsed" FieldType="String"
                            HeaderText="是否在岗" HeaderTextAlign="Center" TextAlign="Left"
                            SortField="IsOnDuty">
                        </f:RenderField>
                        <f:LinkButtonField ID="btnCheckerQualification" runat="server" Text="资质"
                        HeaderText="资质" Width="180px" HeaderTextAlign="Center" 
                        TextAlign="Center" CommandName="CheckerQualification" CommandArgument="PipingClassId">
                    </f:LinkButtonField>
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
            IsModal="true" Width="700px" Height="330px">
        </f:Window>
        <f:Window ID="Window2" Title="弹出窗体" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close"
            IsModal="true" Width="700px" Height="330px">
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

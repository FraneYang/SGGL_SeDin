﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeldingLocation.aspx.cs" Inherits="FineUIPro.Web.HJGL.BaseInfo.WeldingLocation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊接位置</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="焊接位置"
                EnableCollapse="true" runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="WeldingLocationId"
                AllowCellEditing="true" ClicksToEdit="2" DataIDField="WeldingLocationId" AllowSorting="true"
                SortField="WeldingLocationCode" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox ID="txtWeldingLocationCode" runat="server" Label="焊接位置代号"
                                EmptyText="输入查询条件" Width="300px" LabelWidth="120px"
                                LabelAlign="Right">
                            </f:TextBox>
                            <f:TextBox ID="txtWeldingLocationName" runat="server" Label="焊接位置名称"
                                EmptyText="输入查询条件" Width="300px" LabelWidth="120px"
                                LabelAlign="Right">
                            </f:TextBox>
                            <f:Button ID="btnQuery" ToolTip="查询" Icon="SystemSearch"
                                EnablePostBack="true" OnClick="btnQuery_Click" runat="server">
                            </f:Button>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="true"
                                runat="server" OnClick="btnNew_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RenderField Width="200px" ColumnID="WeldingLocationCode" DataField="WeldingLocationCode"
                        FieldType="String" HeaderText="焊接位置代号" HeaderTextAlign="Center"
                        TextAlign="Left" SortField="PipingClassCode">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="WeldingLocationName" DataField="WeldingLocationName"
                        FieldType="String" HeaderText="焊接位置名称" HeaderTextAlign="Center"
                        TextAlign="Left" SortField="WeldingLocationName">
                    </f:RenderField>
                    <f:RenderField Width="350px" ColumnID="Remark" DataField="Remark" FieldType="String"
                        HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left"
                        ExpandUnusedSpace="true">
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
        IsModal="true" Width="1000px" Height="600px">
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

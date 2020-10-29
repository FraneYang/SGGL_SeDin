<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Control.aspx.cs" Inherits="FineUIPro.Web.HJGL.BaseInfo.Control" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>直径寸径对照</title>
    <style type="text/css">
        .color1 {
            background-color: #f62854;
            color: #fff;
        }
    </style>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="直径寸经对照"
                    EnableCollapse="true" runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="DNCompareId"
                    AllowCellEditing="true" ClicksToEdit="2" DataIDField="DNCompareId" AllowSorting="true"
                    SortField="PipeSize" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                    IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                    EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True" OnRowDataBound="Grid1_RowDataBound">

                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:Panel runat="server" ShowHeader="false" ShowBorder="false">
                                    <Items>
                                        <f:Label runat="server" Text="注：1 壁厚系列号(Sch.No.)后缀加S者,仅用于不锈钢管，其单位长度的理论质量是以碳素钢钢管给出"></f:Label>
                                        <f:Label runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2 尽可能不选用标注颜色的规则"></f:Label>
                                        <f:Label runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3 本表可用于无缝或焊接钢管"></f:Label>
                                    </Items>
                                </f:Panel>

                                <f:TextBox ID="txtOutSizeDia" runat="server" Label="外径" EmptyText="输入查询条件"
                                    Width="350px" LabelWidth="180px" LabelAlign="Right" Hidden="true">
                                </f:TextBox>
                                <f:Button ID="btnQuery" ToolTip="查询" Icon="SystemSearch"
                                    EnablePostBack="true" OnClick="btnQuery_Click" runat="server" Hidden="true">
                                </f:Button>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="true" Margin="0 0 50 0"
                                    runat="server" OnClick="btnNew_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:RenderField Width="140px" ColumnID="DN" DataField="DN" FieldType="String"
                            HeaderText="公称尺寸(DN)" HeaderTextAlign="Center" TextAlign="Left" SortField="DN">
                        </f:RenderField>
                        <f:RenderField Width="150px" ColumnID="PipeSize" DataField="PipeSize" FieldType="String"
                            HeaderText="公称尺寸(NPS)" HeaderTextAlign="Center" TextAlign="Left" SortField="PipeSize">
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="OutSizeDia" DataField="OutSizeDia" FieldType="Float"
                            HeaderText="外径" HeaderTextAlign="Center" TextAlign="Left" SortField="OutSizeDia">
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="SCH10" DataField="SCH10" FieldType="Float"
                            HeaderText="SCH10" HeaderTextAlign="Center" TextAlign="Left" SortField="SCH10">
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="SCH20" DataField="SCH20" FieldType="Float"
                            HeaderText="SCH20" HeaderTextAlign="Center" TextAlign="Left" SortField="SCH20">
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="SCH30" DataField="SCH30" FieldType="Float"
                            HeaderText="SCH30" HeaderTextAlign="Center" TextAlign="Left" SortField="SCH30">
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="STD" DataField="STD" FieldType="Float" HeaderText="STD"
                            HeaderTextAlign="Center" TextAlign="Left" SortField="STD">
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="SCH40" DataField="SCH40" FieldType="Float"
                            HeaderText="SCH40" HeaderTextAlign="Center" TextAlign="Left" SortField="SCH40">
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="SCH60" DataField="SCH60" FieldType="Float"
                            HeaderText="SCH60" HeaderTextAlign="Center" TextAlign="Left" SortField="SCH60">
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="XS" DataField="XS" FieldType="Float" HeaderText="XS"
                            HeaderTextAlign="Center" TextAlign="Left" SortField="XS">
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="SCH80" DataField="SCH80" FieldType="Float"
                            HeaderText="SCH80" HeaderTextAlign="Center" TextAlign="Left" SortField="SCH80">
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="SCH100" DataField="SCH100" FieldType="Float"
                            HeaderText="SCH100" HeaderTextAlign="Center" TextAlign="Left" SortField="SCH100">
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="SCH120" DataField="SCH120" FieldType="Float"
                            HeaderText="SCH120" HeaderTextAlign="Center" TextAlign="Left" SortField="SCH120">
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="SCH140" DataField="SCH140" FieldType="Float"
                            HeaderText="SCH140" HeaderTextAlign="Center" TextAlign="Left" SortField="SCH140">
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="SCH160" DataField="SCH160" FieldType="Float"
                            HeaderText="SCH160" HeaderTextAlign="Center" TextAlign="Left" SortField="SCH160">
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="XXS" DataField="XXS" FieldType="Float" HeaderText="XXS"
                            HeaderTextAlign="Center" TextAlign="Left" SortField="XXS">
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="size" DataField="size" FieldType="Float" HeaderText="尺寸系列"
                            HeaderTextAlign="Center" TextAlign="Left" SortField="size">
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="thickness" DataField="thickness" FieldType="Float" HeaderText="壁厚"
                            HeaderTextAlign="Center" TextAlign="Left" SortField="thickness">
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
            EnableMaximize="true" Target="Top" EnableResize="false" runat="server" OnClose="Window1_Close"
            IsModal="true" Width="900px" Height="350px">
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

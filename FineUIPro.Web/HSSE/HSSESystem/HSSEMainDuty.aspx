<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSSEMainDuty.aspx.cs" Inherits="FineUIPro.Web.HSSE.HSSESystem.HSSEMainDuty" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>安全主体责任</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" />
    <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
        <Regions>
            <f:Region ID="Region1" ShowBorder="false" ShowHeader="false" RegionPosition="Left"
                BodyPadding="0 5 0 0" Width="300px" Layout="Fit" runat="server">
                <Items>
                    <f:Grid ID="Grid2" ShowBorder="true" ShowHeader="true" Title="岗位" runat="server"
                        DataKeyNames="WorkPostId,WorkPostName" DataIDField="WorkPostId" EnableMultiSelect="false" EnableRowSelectEvent="true"
                        OnRowSelect="Grid2_RowSelect" ShowGridHeader="false" EnableTextSelection="True">
                        <Columns>
                            <f:BoundField ExpandUnusedSpace="true" ColumnID="WorkPostName" DataField="WorkPostName" DataFormatString="{0}"
                                HeaderText="岗位名称" />
                        </Columns>
                    </f:Grid>
                </Items>
            </f:Region>
            <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox"
                BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server">
                <Items>
                        <f:Grid ID="Grid1" Width="870px" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="HSSEMainDutyId"  ForceFit="true"
                        DataIDField="HSSEMainDutyId"  AllowPaging="true" IsDatabasePaging="true"  AllowSorting="true" OnSort="Grid1_Sort"
                        SortField="SortIndex" SortDirection="ASC" EnableColumnLines="true"
                        PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                        OnRowDoubleClick="Grid1_RowDoubleClick"  EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                    <f:Button ID="btnNew" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNew_Click" Hidden="true">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                          <f:RenderField Width="80px" ColumnID="SortIndex" DataField="SortIndex" SortField="SortIndex"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="序号">
                            </f:RenderField>
                            <f:RenderField Width="500px" ColumnID="Duties" DataField="Duties" SortField="Duties" 
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="职责">
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="Remark" DataField="Remark" SortField="Remark"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="备注">
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
                            </f:DropDownList>
                        </PageItems>
                    </f:Grid>
                </Items>
            </f:Region>
        </Regions>
    </f:RegionPanel>
    <f:Window ID="Window1" Title="编辑安全主体责任" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="800px" Height="360px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true" Hidden="true"
            runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true" Hidden="true"
            ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除">
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

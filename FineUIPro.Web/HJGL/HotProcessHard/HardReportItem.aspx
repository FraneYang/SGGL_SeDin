<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HardReportItem.aspx.cs" Inherits="FineUIPro.Web.HJGL.HotProcessHard.HardReportItem" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>硬度报告</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="硬度报告"
                    EnableCollapse="true" runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="HardReportId,WeldJointId"
                    AllowCellEditing="true" ClicksToEdit="1" DataIDField="HardReportId" AllowSorting="true"
                    SortField="HardReportNo,TestingPointNo" SortDirection="DESC" OnSort="Grid1_Sort" OnRowCommand="Grid1_RowCommand"
                    AllowPaging="true" IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange" EnableTextSelection="True" ForceFit="true">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:Label runat="server" ID="lbUnitWork" Label="单位工程" LabelAlign="Right"></f:Label>
                                <f:Label runat="server" ID="lbPipeLineCode" Label="管线号" LabelAlign="Right"></f:Label>
                                <f:Label runat="server" ID="lbWeldJointCode" Label="焊口号" LabelAlign="Right"></f:Label>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:DropDownList ID="drpIsPass" runat="server" Label="检测结果" LabelAlign="Right">
                                    <f:ListItem Text="待检测" Value="2" />
                                    <f:ListItem Text="合格" Value="1" />
                                    <f:ListItem Text="不合格" Value="0" />
                                </f:DropDownList>
                                <f:Button ID="btnSave" ToolTip="保存该焊口硬度检测是否合格" Text="保存" Icon="SystemSave" EnablePostBack="true"
                                    runat="server" OnClick="btnSave_Click">
                                </f:Button>
                                <f:Button ID="btnNew" ToolTip="新增" Text="新增" Icon="Add" EnablePostBack="true"
                                    runat="server" OnClick="btnNew_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:RowNumberField EnablePagingNumber="true" HeaderText="序号"
                            Width="60px" HeaderTextAlign="Center" TextAlign="Center" />
                        <f:RenderField Width="90px" ColumnID="TestingPointNo" DataField="TestingPointNo"
                            FieldType="String" HeaderText="检测部位编号" HeaderTextAlign="Center"
                            TextAlign="Left" SortField="TestingPointNo">
                            <Editor>
                                <f:TextBox runat="server" ID="txtTestingPointNo"></f:TextBox>
                            </Editor>
                        </f:RenderField>
                        <f:GroupField EnableLock="true" HeaderText="硬度值" TextAlign="Center">
                            <Columns>
                                <f:RenderField Width="80px" ColumnID="HardNessValue1" DataField="HardNessValue1"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Center">
                                    <Editor>
                                        <f:NumberBox ID="txtHardNessValue1" runat="server">
                                        </f:NumberBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField Width="80px" ColumnID="HardNessValue2" DataField="HardNessValue2"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Center">
                                    <Editor>
                                        <f:NumberBox ID="txtHardNessValue2" runat="server">
                                        </f:NumberBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField Width="80px" ColumnID="HardNessValue3" DataField="HardNessValue3"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Center">
                                    <Editor>
                                        <f:NumberBox ID="txtHardNessValue3" runat="server">
                                        </f:NumberBox>
                                    </Editor>
                                </f:RenderField>
                            </Columns>
                        </f:GroupField>
                        <f:RenderField Width="100px" ColumnID="Remark" DataField="Remark" FieldType="String"
                            HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left">
                            <Editor>
                                <f:TextBox runat="server" ID="txtRemark"></f:TextBox>
                            </Editor>
                        </f:RenderField>
                        <f:LinkButtonField Width="100px" HeaderText="检测部位示意图" ConfirmTarget="Top" CommandName="attchUrl"
                            TextAlign="Center" ToolTip="上传/查看" Text="检测部位示意图" />
                        <f:LinkButtonField Width="60px" TextAlign="Center" HeaderText="删除" ToolTip="删除" CommandName="del"
                            Icon="Delete" />
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
            IsModal="true" Width="700px" Height="430px">
        </f:Window>
        <f:Window ID="Window2" Title="文件查看" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="680px"
            Height="480px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
                Icon="Delete" ConfirmText="删除选中行" ConfirmTarget="Top"
                runat="server" Text="删除">
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

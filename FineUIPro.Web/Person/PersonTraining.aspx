<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonTraining.aspx.cs" Inherits="FineUIPro.Web.Person.PersonTraining" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>员工信息</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
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
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="员工培训" EnableCollapse="true"
                    runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="TrainingPlanId"
                    DataIDField="TrainingPlanId" AllowSorting="true" SortField="CompileTime" ForceFit="true"
                    SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                    PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                    OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                            <Items>
                                <f:ToolbarFill runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                    Hidden="true">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="45px" HeaderTextAlign="Center"
                            TextAlign="Center" />
                        <f:RenderField Width="100px" ColumnID="TrainingPlanCode" DataField="TrainingPlanCode" 
                            FieldType="String" HeaderText="编号" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="120px" ColumnID="TrainingPlanTitle" DataField="TrainingPlanTitle"
                            FieldType="String" HeaderText="标题" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="120px" ColumnID="TrainingPlanContent" DataField="TrainingPlanContent"
                            FieldType="String" HeaderText="内容" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="120px" ColumnID="StartTime" DataField="StartTime" SortField="StartTime"
                            FieldType="Date"  HeaderText="开始时间" HeaderTextAlign="Center" TextAlign="Left" RendererArgument="yyyy-MM-dd">
                        </f:RenderField>
                        <f:RenderField Width="120px" ColumnID="EndTime" DataField="EndTime" SortField="EndTime"
                            FieldType="Date"  HeaderText="结束时间" HeaderTextAlign="Center" TextAlign="Left" RendererArgument="yyyy-MM-dd">
                        </f:RenderField>
                        <f:RenderField Width="120px" ColumnID="UserName" DataField="UserName"
                            FieldType="String" HeaderText="制定人" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="120px" ColumnID="State" DataField="State" SortField="State"
                            FieldType="String"  HeaderText="状态" HeaderTextAlign="Center" TextAlign="Left">
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
        </f:Panel>
        <f:Window ID="Window1" Title="员工培训计划" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="1000px"
            Height="600px" OnClose="Window1_Close">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
                Hidden="true" runat="server" Text="编辑" Icon="Pencil">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDelete" ConfirmText="确定删除当前数据？" OnClick="btnMenuDelete_Click" EnablePostBack="true"
                Hidden="true" runat="server" Text="删除" Icon="Delete">
            </f:MenuButton>
            <f:MenuButton ID="MenuView"  EnablePostBack="true" runat="server" Text="查看" Icon="find" OnClick="MenuView_Click">
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

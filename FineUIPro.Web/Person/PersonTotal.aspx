<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonTotal.aspx.cs" Inherits="FineUIPro.Web.Person.PersonTotal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>员工总结</title>
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
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="用户信息" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="UserId" AllowCellEditing="true" EnableColumnLines="true"
                    ClicksToEdit="2" DataIDField="PersonTotalId" AllowSorting="true"  ForceFit="true"
                    SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                    PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                    OnRowDoubleClick="Grid1_RowDoubleClick" Width="980px" EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                            <Items>
                                <f:TextBox runat="server" Label="用户" ID="txtUserName" EmptyText="输入查询条件" AutoPostBack="true"
                                    OnTextChanged="TextBox_TextChanged" Width="210px" LabelWidth="80px" LabelAlign="Right">
                                </f:TextBox>
                                 <f:DatePicker runat="server" Label="开始日期" ID="txtStartTime" LabelWidth="110px" AutoPostBack="true"
                                    OnTextChanged="TextBox_TextChanged" LabelAlign="Right"></f:DatePicker>
                        <f:DatePicker runat="server" Label="结束日期" ID="txtEndTime" LabelWidth="110px" AutoPostBack="true"
                                    OnTextChanged="TextBox_TextChanged" LabelAlign="Right"></f:DatePicker>
                                <f:ToolbarFill runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                    Hidden="true">
                                </f:Button>
                                <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" Text="批量导出" ToolTip="批量导出" Icon="TableGo" Hidden="true"
                            AjaxLoadingType="Mask" ShowAjaxLoadingMaskText="true" AjaxLoadingMaskText="正在导出数据到服务器，请稍候"
                                    EnableAjax="false" DisableControlBeforePostBack="false">
                                    </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="45px" HeaderTextAlign="Center"
                            TextAlign="Center" />
                        <f:RenderField Width="100px" ColumnID="UserName" DataField="UserName" SortField="UserName"
                            FieldType="String" HeaderText="姓名" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="250px" ColumnID="RoleName" DataField="RoleName" SortField="RoleName"
                            FieldType="String" HeaderText="岗位" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="120px" ColumnID="StartTime" DataField="StartTime" SortField="StartTime"
                            FieldType="String" Renderer="Date"  HeaderText="开始日期" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="200px" ColumnID="EndTime" DataField="EndTime" SortField="EndTime"
                            FieldType="String" Renderer="Date" HeaderText="结束日期" HeaderTextAlign="Center" TextAlign="Center">
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
        <f:Window ID="Window1" Title="人员总结" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="800px"
            Height="650px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
                Hidden="true" runat="server" Text="编辑" Icon="Pencil">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDelete" ConfirmText="确定删除当前数据？" OnClick="btnMenuDelete_Click" EnablePostBack="true"
                Hidden="true" runat="server" Text="删除" Icon="Delete">
            </f:MenuButton>
            <f:MenuButton ID="MenuView" OnClick="MenuView_Click" EnablePostBack="true" runat="server" Text="查看" Icon="ApplicationViewIcons">
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

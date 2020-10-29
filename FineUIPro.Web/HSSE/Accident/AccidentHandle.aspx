<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccidentHandle.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Accident.AccidentHandle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HSE事故(含未遂)处理</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="HSE事故(含未遂)处理" EnableCollapse="true"
                runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="AccidentHandleId"
                 DataIDField="AccidentHandleId" AllowSorting="true" ForceFit="true"
                SortField="AccidentHandleCode" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" 
                EnableTextSelection="True" OnRowCommand="Grid1_RowCommand">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox runat="server" Label="名称" ID="txtAccidentHandleName" EmptyText="输入查询条件"
                                 Width="250px" LabelWidth="80px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:DatePicker ID="txtStartDate" runat="server" Label="发生时间" 
                                Width="250px" EmptyText="开始时间" LabelAlign="Right">
                            </f:DatePicker>
                            <f:Label ID="lblTo" runat="server" Text="至">
                            </f:Label>
                            <f:DatePicker ID="txtEndDate" runat="server"
                                Width="150px" EmptyText="结束时间">
                            </f:DatePicker>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnQuery" ToolTip="查询" Icon="SystemSearch" 
                                 EnablePostBack="true" OnClick="btnQuery_Click" runat="server" >
                            </f:Button>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" Hidden="true"
                                runat="server">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                        TextAlign="Center" />
                    <f:RenderField Width="100px" ColumnID="AccidentHandleCode" DataField="AccidentHandleCode"
                        SortField="AccidentHandleCode" FieldType="String" HeaderText="编号" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="160px" ColumnID="AccidentHandleName" DataField="AccidentHandleName"
                        SortField="AccidentHandleName" FieldType="String" HeaderText="名称" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>      
                    <f:RenderField Width="250px" ColumnID="UnitName" DataField="UnitName"
                        SortField="UnitName" FieldType="String" HeaderText="事故单位" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="AccidentDate" DataField="AccidentDate" SortField="AccidentDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="发生时间"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                  <%--  <f:RenderField Width="350px" ColumnID="AccidentDef" DataField="AccidentDef" SortField="AccidentDef"
                        FieldType="String" HeaderText="事故摘要" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>--%>
                    <f:RenderField Width="120px" ColumnID="Death" DataField="Death" SortField="Death"
                        FieldType="String" HeaderText="伤亡情况" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="MoneyLoss" DataField="MoneyLoss" SortField="MoneyLoss"
                        FieldType="String" HeaderText="经济损失" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
               <%--     <f:RenderField Width="400px" ColumnID="AccidentHandle" DataField="AccidentHandle"
                        SortField="AccidentHandle" FieldType="String" HeaderText="善后处理" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="Remark" DataField="Remark" SortField="Remark"
                        FieldType="String" HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>--%>
                    <f:LinkButtonField Width="90px" HeaderText="四不放过" Text="四不放过" HeaderTextAlign="Center"
                        TextAlign="Left"  DataCommandArgumentField="AccidentHandleId" CommandName="NoFourLetoff">
                    </f:LinkButtonField>
                    <f:RenderField Width="150px" ColumnID="FlowOperateName" DataField="FlowOperateName"
                        SortField="FlowOperateName" FieldType="String" HeaderText="状态" HeaderTextAlign="Center"
                        TextAlign="Left">
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
    <f:Window ID="Window1" Title="HSE事故(含未遂)处理" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="1024px"
        Height="550px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="Pencil" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server"
            Text="删除">
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreWeldReportAudit.aspx.cs" Inherits="FineUIPro.Web.HJGL.WeldingManage.PreWeldReportAudit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>预提交日报审核</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="280px" Title="预提交日报审核" ShowBorder="true" Layout="VBox"
                ShowHeader="true" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Items>
                     <f:Tree ID="tvControlItem" ShowHeader="false" Height="500px" Title="预提交日报审核节点树"
                        OnNodeCommand="tvControlItem_NodeCommand" runat="server" ShowBorder="false" EnableCollapse="true"
                        EnableSingleClickExpand="true" AutoLeafIdentification="true" EnableSingleExpand="true"
                        EnableTextSelection="true">
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="焊接记录预提交"
                TitleToolTip="预提交日报审核" AutoScroll="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Center">
                        <Items>
                            <f:RadioButtonList ID="IsAudit" runat="server" AutoPostBack="true" OnSelectedIndexChanged="IsAudit_SelectedIndexChanged">
                                <f:RadioItem Value="0"  Text="未提交" Selected="true"/>
                                <f:RadioItem Value="1"  Text="已提交"/>
                            </f:RadioButtonList>
                             <f:DatePicker runat="server" Label="焊接日期" ID="txtWeldingDate" LabelAlign="Right"
                                LabelWidth="100px" Width="260px" AutoPostBack="true" OnTextChanged="WeldingDate_OnTextChanged">
                            </f:DatePicker>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnAudit" Text="审核提交" ToolTip="审核" Icon="ApplicationEdit" runat="server"
                                OnClick="btnAudit_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="焊接记录预提交" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="PreWeldingDailyId,WeldJointId" AllowCellEditing="true"
                        AllowColumnLocking="true" EnableColumnLines="true" ClicksToEdit="2" DataIDField="PreWeldingDailyId"
                        AllowSorting="true" SortField="PipelineCode,WeldJointCode" SortDirection="ASC" OnSort="Grid1_Sort"
                        AllowPaging="true" IsDatabasePaging="true" PageSize="30" OnPageIndexChange="Grid1_PageIndexChange"
                        EnableTextSelection="True"  EnableCheckBoxSelect="true" KeepCurrentSelection="true">
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="60px" HeaderTextAlign="Center"
                                TextAlign="Center" />
                             <f:RenderField HeaderText="管线号" ColumnID="PipelineCode" DataField="PipelineCode" SortField="PipelineCode"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="200px" Locked="true">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊口号" ColumnID="WeldJointCode" DataField="WeldJointCode" SortField="WeldJointCode"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px" Locked="true">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊接日期" ColumnID="WeldingDate" DataField="WeldingDate"
                                SortField="WeldingDate" FieldType="Date" Renderer="Date" HeaderTextAlign="Center"
                                TextAlign="Left" Width="90px" Locked="true">
                            </f:RenderField>
                            <f:RenderField HeaderText="打底焊工" ColumnID="BackingWelderCode" DataField="BackingWelderCode"
                                SortField="BackingWelderCode" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="80px" Locked="true">
                            </f:RenderField>
                             <f:RenderField HeaderText="盖面焊工" ColumnID="CellWelderCode" DataField="CellWelderCode"
                                SortField="CellWelderCode" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="80px" Locked="true">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊接区域" ColumnID="JointArea" DataField="JointArea" SortField="JointArea"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="80px">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊口属性" ColumnID="JointAttribute" DataField="JointAttribute"
                                SortField="JointAttribute" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="80px">
                            </f:RenderField>
                            <f:RenderField HeaderText="寸径" ColumnID="Size" DataField="Size" FieldType="Float"
                                HeaderTextAlign="Center" TextAlign="Left" Width="80px">
                            </f:RenderField>
                            <f:RenderField HeaderText="外径" ColumnID="Dia" DataField="Dia" FieldType="Float"
                                HeaderTextAlign="Center" TextAlign="Left" Width="80px">
                            </f:RenderField>
                            <f:RenderField HeaderText="壁厚" ColumnID="Thickness" DataField="Thickness" FieldType="Float"
                                HeaderTextAlign="Center" TextAlign="Left" Width="80px">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊接方法" ColumnID="WeldMethod" DataField="WeldMethod" FieldType="String"
                                HeaderTextAlign="Center" TextAlign="Left" Width="80px">
                            </f:RenderField>
                            <f:RenderField HeaderText="审核人" ColumnID="AuditManName" DataField="AuditManName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="80px">
                            </f:RenderField>
                            <f:RenderField HeaderText="审核时间" ColumnID="AuditDate" DataField="AuditDate" FieldType="Date"
                                Renderer="Date" HeaderTextAlign="Center" TextAlign="Left" Width="80px">
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
                                <f:ListItem Text="30" Value="30" />
                                <f:ListItem Text="50" Value="50" />
                            </f:DropDownList>
                        </PageItems>
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    </form>
    <script type="text/javascript">
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            // F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }

    </script>
</body>
</html>
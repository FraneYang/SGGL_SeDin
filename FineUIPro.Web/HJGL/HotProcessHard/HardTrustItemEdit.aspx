<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HardTrustItemEdit.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.HotProcessHard.HardTrustItemEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查询焊口</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="250px" Title="管线" ShowBorder="true"
                Layout="VBox" ShowHeader="false" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox ID="txtPipelineCode" runat="server" Label="管线"
                                EmptyText="输入查询条件" Width="190px" LabelWidth="80px"
                                LabelAlign="Right">
                            </f:TextBox>
                            <f:Button ID="btnQuery" ToolTip="查询" Icon="SystemSearch"
                                EnablePostBack="true" OnClick="btnQuery_Click" runat="server">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                    <f:Tree ID="tvControlItem" ShowHeader="false" Height="450px" Title="管线列表" OnNodeCommand="tvControlItem_NodeCommand"
                        runat="server" ShowBorder="false" EnableCollapse="true" EnableSingleClickExpand="true"
                        AutoLeafIdentification="true" EnableTextSelection="true" Expanded="true">
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="焊口信息"
                TitleToolTip="焊口信息" AutoScroll="true">
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="焊口信息"
                        EnableCollapse="true" KeepCurrentSelection="true" runat="server" BoxFlex="1"
                        DataKeyNames="WeldJointId,HotProessTrustItemId" AllowColumnLocking="true" EnableColumnLines="true"
                        DataIDField="WeldJointId" EnableTextSelection="True" AllowSorting="true" SortField="WeldJointCode"
                        SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="false" IsDatabasePaging="true"
                        PageSize="1000" EnableCheckBoxSelect="true">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                    </f:ToolbarFill>
                                    <f:Button ID="btnAccept" Icon="Accept" runat="server" ToolTip="确定"
                                        OnClick="btnAccept_Click">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RenderField HeaderText="总焊口量" ColumnID="WeldJointCode"
                                DataField="WeldJointCode" SortField="WeldJointCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊工号" ColumnID="WelderCode"
                                DataField="WelderCode" SortField="WelderCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="规格" ColumnID="Specification"
                                DataField="Specification" SortField="Specification" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="材质" ColumnID="MaterialCode"
                                DataField="MaterialCode" SortField="MaterialCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="单线图号" ColumnID="SingleNumber"
                                DataField="SingleNumber" SortField="SingleNumber" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="备注" ColumnID="Remark" DataField="Remark"
                                SortField="Remark" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="100px" ExpandUnusedSpace="true">
                            </f:RenderField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    </form>
    <script>
        function renderIsProess(value) {
            return value == "1" ? '是' : '否';
        }
    </script>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectDailyWeldJoint.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.WeldingManage.SelectDailyWeldJoint" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查询焊口</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="250px" Title="管线" ShowBorder="true" Layout="VBox"
                ShowHeader="false" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
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
                    <f:Tree ID="tvControlItem" ShowHeader="false" Height="430px" Title="管线列表" OnNodeCommand="tvControlItem_NodeCommand"
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
                        DataKeyNames="WeldJointId" AllowColumnLocking="true" EnableColumnLines="true"
                        DataIDField="WeldJointId" EnableTextSelection="True" AllowSorting="true" SortField="WeldJointCode"
                        SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="false" IsDatabasePaging="true"
                        PageSize="1000" EnableCheckBoxSelect="true">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                    <f:DropDownList ID="drpCoverWelderId" Label="盖面焊工"
                                        runat="server" ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="100px"
                                        AutoPostBack="true" OnSelectedIndexChanged="drpCoverWelderId_OnSelectedIndexChanged"
                                        Width="200px" LabelAlign="Right">
                                    </f:DropDownList>
                                    <f:DropDownList ID="drpBackingWelderId" Label="打底焊工"
                                        runat="server" ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="100px"
                                        Width="200px" LabelAlign="Right">
                                    </f:DropDownList>
                                       <f:DropDownList ID="drpWeldingLocation" Label="焊接位置"
                                        runat="server"   LabelWidth="100px" Hidden="true"
                                        Width="180px" LabelAlign="Right">
                                    </f:DropDownList>
                                     <f:DropDownList ID="drpJointAttribute" Label="焊口属性"
                                        runat="server" ShowRedStar="true" Required="true" LabelWidth="100px"
                                        Width="180px" LabelAlign="Right">
                                    </f:DropDownList>
                                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                    </f:ToolbarFill>
                                    <f:Button ID="btnAccept" Icon="Accept" runat="server" ToolTip="确定"
                                        OnClick="btnAccept_Click">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RenderField HeaderText="焊口号" ColumnID="WeldJointCode"
                                DataField="WeldJointCode" SortField="WeldJointCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="140px" Locked="true">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊缝类型" ColumnID="WeldTypeCode"
                                DataField="WeldTypeCode" SortField="WeldTypeCode" FieldType="String"
                                HeaderTextAlign="Center" TextAlign="Left" Width="120px" ExpandUnusedSpace="true">
                            </f:RenderField> 
                            <f:RenderField HeaderText="焊接方法" ColumnID="WeldingMethodCode"
                                DataField="WeldingMethodCode" SortField="WeldingMethodCode" FieldType="String"
                                HeaderTextAlign="Center" TextAlign="Left" Width="140px" ExpandUnusedSpace="true">
                            </f:RenderField>
                            <f:RenderField HeaderText="外径" ColumnID="Dia"
                                DataField="Dia" SortField="Dia" FieldType="Double" HeaderTextAlign="Center" TextAlign="Left"
                                Width="120px">
                            </f:RenderField>
                            <f:RenderField HeaderText="壁厚" ColumnID="Thickness"
                                DataField="Thickness" SortField="Thickness" FieldType="Double" HeaderTextAlign="Center"
                                TextAlign="Left" Width="120px">
                            </f:RenderField>
                            <f:RenderField HeaderText="是否热处理" ColumnID="IsHotProessStr"
                                DataField="IsHotProessStr" SortField="IsHotProessStr" FieldType="String"
                                HeaderTextAlign="Center" TextAlign="Left" Width="120px" ExpandUnusedSpace="true">
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotProessTrustItemEdit.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.HotProcessHard.HotProessTrustItemEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查找管线焊口信息</title>
    <base target="_self" />
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
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
                            <f:TextBox ID="txtIsono" runat="server" Label="管线号"
                                EmptyText="输入查询条件" AutoPostBack="true" OnTextChanged="Tree_TextChanged"
                                Width="230px" LabelWidth="110px" LabelAlign="Right">
                            </f:TextBox>
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
                        DataKeyNames="WeldJointId" EnableColumnLines="true" DataIDField="WeldJointId"
                        EnableTextSelection="True" AllowSorting="true" SortField="WeldJointCode" SortDirection="ASC"
                        OnSort="Grid1_Sort" AllowPaging="false" 
                        EnableCheckBoxSelect="true">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                    <f:TextBox ID="txtJointNo" runat="server" Label="总焊口量"
                                        EmptyText="输入查询条件" AutoPostBack="true" OnTextChanged="Tree_TextChanged"
                                        Width="300px" LabelWidth="160px" LabelAlign="Right">
                                    </f:TextBox>
                                    <f:Label ID="ww" runat="server" Width="60px">
                                    </f:Label>
                                    <f:RadioButtonList runat="server" ID="rblIsWeld" Width="400px" AutoPostBack="true"
                                        OnSelectedIndexChanged="rblIsWeld_SelectedIndexChanged">
                                        <f:RadioItem Value="2" Text="全部" />
                                        <f:RadioItem Selected="true" Value="1" Text="已焊接" />
                                        <f:RadioItem Value="0" Text="未焊接" />
                                    </f:RadioButtonList>
                                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                    </f:ToolbarFill>
                                    <f:Button ID="btnAccept" Icon="Accept" runat="server" Text="确定"
                                        OnClick="btnAccept_Click">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RenderField HeaderText="焊口号" ColumnID="WeldJointCode"
                                DataField="WeldJointCode" SortField="WeldJointCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="180px">
                            </f:RenderField>
                            <f:RenderField HeaderText="规格" ColumnID="Specification"
                                DataField="Specification" SortField="Specification" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="180px">
                            </f:RenderField>
                            <f:RenderField HeaderText="材质代号" ColumnID="MaterialCode"
                                DataField="MaterialCode" SortField="MaterialCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="220px">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊接日期" ColumnID="WeldingDate"
                                DataField="WeldingDate" SortField="WeldingDate" FieldType="Date" Renderer="Date"
                                HeaderTextAlign="Center" TextAlign="Left" Width="120px">
                            </f:RenderField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    </form>
    <script type="text/javascript">
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            //  F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
                
    </script>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WelderManage.aspx.cs" Inherits="FineUIPro.Web.HJGL.PersonManage.WelderManage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊工管理</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .f-grid-row.color1,
        .f-grid-row.color1 .f-icon,
        .f-grid-row.color1 a {
            background-color: orange;
            color: #fff;
        }

        .f-grid-row.color3,
        .f-grid-row.color3 .f-icon,
        .f-grid-row.color3 a {
            background-color: red;
            color: #fff;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
            <Items>
                <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                    EnableCollapse="true" Width="300px" Title="单位名称"
                    ShowBorder="true" Layout="VBox" ShowHeader="true" AutoScroll="true" BodyPadding="5px"
                    IconFont="ArrowCircleLeft">
                    <Items>
                        <f:Tree ID="tvControlItem" ShowHeader="false" Height="560px" runat="server" ShowBorder="false" EnableCollapse="true"
                            EnableSingleClickExpand="true" AutoLeafIdentification="true" EnableSingleExpand="true"
                            EnableTextSelection="true" OnNodeCommand="tvControlItem_NodeCommand">
                        </f:Tree>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                    Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" AutoScroll="true">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                            <Items>
                                <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>
                                <f:Button ID="btnEdit" Text="编辑" Icon="TableEdit" runat="server" OnClick="btnEdit_Click" Hidden="true">
                                </f:Button>
                                <f:Button ID="btnDelete" Text="删除" ConfirmText="删除选中行？" ConfirmTarget="Top" Hidden="true"
                                    Icon="Delete" runat="server" OnClick="btnDelete_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>
                        <f:Form ID="SimpleForm1" LabelAlign="Right" MessageTarget="Qtip" RedStarPosition="BeforeText"
                            LabelWidth="90px" BodyPadding="5px" ShowBorder="false" ShowHeader="false" runat="server"
                            AutoScroll="false">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                        <f:Label ID="drpUnitId" runat="server" Label="单位名称" LabelWidth="120px">
                                        </f:Label>
                                        <f:Label ID="txtWelderName" runat="server" Label="焊工姓名" LabelWidth="120px">
                                        </f:Label>
                                        <f:Label ID="txtWelderCode" runat="server" Label="焊工号" LabelWidth="120px">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:Label ID="rblSex" runat="server" Label="性别" LabelWidth="120px">
                                        </f:Label>
                                        <f:Label ID="txtBirthday" runat="server" Label="出生日期"
                                            LabelWidth="120px">
                                        </f:Label>
                                        <f:Label ID="txtCertificateCode" runat="server" Label="证书编号" LabelWidth="120px">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:Label ID="txtCertificateLimitTime" runat="server" Label="有效期"
                                            LabelWidth="120px">
                                        </f:Label>
                                        <f:Label ID="txtWelderLevel" runat="server" Label="焊工等级" LabelWidth="120px">
                                        </f:Label>
                                        <f:CheckBox ID="cbIsOnDuty" runat="server" Label="是否在岗"
                                            LabelWidth="120px">
                                        </f:CheckBox>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="焊工资质" EnableCollapse="true" runat="server"
                            BoxFlex="1" EnableColumnLines="true" DataKeyNames="WelderQualifyId" AllowCellEditing="true"
                            ClicksToEdit="2" DataIDField="WelderQualifyId" AllowSorting="true" SortField="LimitDate"
                            SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                            PageSize="15" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                            OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                                    <Items>
                                        <f:TextBox ID="txtQualificationItem" runat="server" Label="合格项目"
                                            EmptyText="输入查询条件" Width="280px" LabelWidth="120px"
                                            LabelAlign="Right">
                                        </f:TextBox>
                                        <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                        </f:ToolbarFill>
                                        <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="true"
                                            runat="server" OnClick="btnNew_Click" Hidden="true">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:RenderField Width="280px" ColumnID="QualificationItem" DataField="QualificationItem"
                                    FieldType="String" HeaderText="合格项目" HeaderTextAlign="Center"
                                    TextAlign="Left" SortField="QualificationItem">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="CheckDate" DataField="CheckDate" FieldType="Date"
                                    HeaderText="批准日期" HeaderTextAlign="Center" TextAlign="Left"
                                    Renderer="Date">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="LimitDate" DataField="LimitDate" FieldType="Date"
                                    HeaderText="有效日期" HeaderTextAlign="Center" TextAlign="Left"
                                    Renderer="Date">
                                </f:RenderField>
                                <f:RenderField Width="130px" ColumnID="WeldingMethod" DataField="WeldingMethod"
                                    FieldType="String" HeaderText="焊接方法" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="170px" ColumnID="MaterialType" DataField="MaterialType" FieldType="String"
                                    HeaderText="型号、牌号、级别" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="90px" ColumnID="WeldingLocation" DataField="WeldingLocation"
                                    FieldType="String" HeaderText="焊接位置" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:GroupField EnableLock="true" HeaderText="对接焊缝" TextAlign="Center">
                                    <Columns>
                                        <f:RenderField Width="120px" ColumnID="Thickness" DataField="Thickness" FieldType="String"
                                            HeaderText="壁厚覆盖范围" HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:RenderField Width="120px" ColumnID="Sizes" DataField="Sizes" FieldType="String" HeaderText="管径覆盖范围"
                                            HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                    </Columns>
                                </f:GroupField>
                                <f:GroupField EnableLock="true" HeaderText="角焊缝" TextAlign="Center">
                                    <Columns>
                                        <f:RenderField Width="120px" ColumnID="Thickness2" DataField="Thickness2" FieldType="String"
                                            HeaderText="壁厚覆盖范围" HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:RenderField Width="120px" ColumnID="Sizes2" DataField="Sizes2" FieldType="String" HeaderText="管径覆盖范围"
                                            HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                    </Columns>
                                </f:GroupField>
                                <f:RenderField Width="160px" ColumnID="WeldType" DataField="WeldType" FieldType="String"
                                    HeaderText="可焊焊缝类型" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderCheckField Width="120px" ColumnID="IsCanWeldG" DataField="IsCanWeldG" HeaderText="是否可焊固定口"
                                    HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderCheckField>
                                <f:RenderField Width="110px" ColumnID="Remark" DataField="Remark" FieldType="String"
                                    HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left">
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
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Top" EnableResize="false" runat="server" OnClose="Window1_Close"
            IsModal="true" Width="960px" Height="620px">
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

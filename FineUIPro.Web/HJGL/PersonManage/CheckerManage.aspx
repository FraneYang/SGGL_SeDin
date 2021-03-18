<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckerManage.aspx.cs" Inherits="FineUIPro.Web.HJGL.PersonManage.CheckerManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无损检测工管理</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager runat="server" ID="PageManager1" AutoSizePanelID="Panel1" />
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
                        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                        <f:Label ID="drpUnitId" runat="server" Label="所属单位" LabelWidth="120px">
                                        </f:Label>
                                        <f:Label ID="txtCheckerCode" runat="server" Label="检测工编号" LabelWidth="120px">
                                        </f:Label>
                                        <f:Label ID="txtCheckerName" runat="server" Label="检测工姓名" LabelWidth="120px">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:Label ID="txtSex" runat="server" Label="性别" LabelWidth="120px">
                                        </f:Label>
                                        <f:Label ID="txtBirthday" runat="server" Label="出生日期"
                                            LabelWidth="120px">
                                        </f:Label>
                                        <f:Label ID="txtIdentityCard" runat="server" Label="身份证号" LabelWidth="120px">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:Label ID="txtCertificateCode" runat="server" Label="证书编号" LabelWidth="120px">
                                        </f:Label>
                                        <f:CheckBox ID="cbIsOnDuty" runat="server" Label="是否在岗" Enabled="false"
                                            LabelWidth="120px" Readonly="true">
                                        </f:CheckBox>
                                        <f:Label runat="server"></f:Label>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="检测工资质" EnableCollapse="true" runat="server"
                            BoxFlex="1" EnableColumnLines="true" DataKeyNames="WelderQualifyId" AllowCellEditing="true"
                            ClicksToEdit="2" DataIDField="WelderQualifyId" AllowSorting="true" SortField="LimitDate"
                            SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" ForceFit="true"
                            PageSize="15" OnRowDoubleClick="Grid1_RowDoubleClick" EnableRowDoubleClickEvent="true"
                            EnableTextSelection="True">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                                    <Items>
                                        <f:TextBox ID="txtQualificationItem" runat="server" Label="合格项目"
                                            EmptyText="输入查询条件" Width="280px" LabelWidth="120px"
                                            LabelAlign="Right" AutoPostBack="true" OnTextChanged="txtQualificationItem_TextChanged">
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
                                    FieldType="String" HeaderText="合格项目代号" HeaderTextAlign="Center"
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
                                <f:RenderField Width="100px" ColumnID="Level" DataField="Level"
                                    FieldType="String" HeaderText="等级" HeaderTextAlign="Center"
                                    TextAlign="Left" SortField="Level">
                                </f:RenderField>
                            </Columns>
                            <Listeners>
                                <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                            </Listeners>
                            <PageItems>
                                <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                                </f:ToolbarSeparator>
                                <f:ToolbarText ID="ToolbarText2" runat="server" Text="每页记录数：">
                                </f:ToolbarText>
                                <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
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
            IsModal="true" Width="700px" Height="330px">
        </f:Window>
        <f:Window ID="Window2" Title="弹出窗体" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Top" EnableResize="false" runat="server" OnClose="Window1_Close"
            IsModal="true" Width="700px" Height="330px">
        </f:Window>

        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuEdit1" OnClick="btnMenuEdit_Click" Icon="BulletEdit" EnablePostBack="true"
                runat="server" Text="编辑">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDelete1" OnClick="btnMenuDelete_Click" EnablePostBack="true"
                Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Top"
                runat="server" Text="删除">
            </f:MenuButton>
            <f:MenuButton ID="btnView" OnClick="btnView_Click" Icon="Find" EnablePostBack="true"
                runat="server" Text="查看">
            </f:MenuButton>
        </f:Menu>
    </form>
    <script type="text/javascript">

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
        var menuID = '<%= Menu1.ClientID %>';
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>
</body>
</html>

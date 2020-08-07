<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmergencyProcessEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Emergency.EmergencyProcessEdit" ValidateRequest="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑应急流程</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="应急流程" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtProcessSteps" runat="server" Label="步骤" LabelAlign="Right"
                            MaxLength="50" Readonly="true">
                        </f:TextBox>
                        <f:TextBox ID="txtProcessName" runat="server" Label="名称" Required="true" ShowRedStar="true"
                            LabelAlign="Right" MaxLength="50" FocusOnPageLoad="true">
                        </f:TextBox>
                        <f:TextBox runat="server" ID="txtStepOperator" Label="操作者" MaxLength="50"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow ID="State1" Hidden="true">
                    <Items>
                        <f:Form ID="Form2" ShowBorder="true" ShowHeader="true" Title="内容" AutoScroll="true"
                            runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                            <Rows>
                                <f:FormRow ColumnWidths="30% 30% 30% 5%">
                                    <Items>
                                        <f:HiddenField runat="server" ID="hdEmergencyProcessId"></f:HiddenField>
                                        <f:TextBox ID="txtSortId" runat="server" Label="序号" LabelAlign="Right" MaxLength="50">
                                        </f:TextBox>
                                        <f:TextBox ID="txtContent" runat="server" Label="内容" LabelAlign="Right" MaxLength="50">
                                        </f:TextBox>
                                        <f:Button ID="btnSure" Icon="Accept" runat="server" ValidateForms="SimpleForm1"
                                            OnClick="btnSure_Click" ToolTip="确认">
                                        </f:Button>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" EnableColumnLines="true"
                                            EnableColumnMove="true" runat="server" BoxFlex="1" DataKeyNames="EmergencyProcessItemId"
                                            DataIDField="EmergencyProcessItemId" AllowSorting="true" SortField="SortId"
                                            SortDirection="ASC" EnableTextSelection="True" Height="200px"
                                            EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">
                                            <Columns>
                                                <f:RenderField MinWidth="100px" ColumnID="SortId" DataField="SortId"
                                                    FieldType="String" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center" >
                                                </f:RenderField>
                                                <f:RenderField MinWidth="250px" ColumnID="Content" DataField="Content"
                                                    FieldType="String" HeaderText="内容" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                </f:RenderField>
                                            </Columns>
                                            <Listeners>
                                                <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                                            </Listeners>
                                        </f:Grid>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                </f:FormRow>
                <f:FormRow ID="State2">
                    <Items>
                        <f:DropDownList ID="drpTeam" runat="server" Label="队伍" EnableEdit="true" ForceSelection="false" EnableMultiSelect="true" MaxLength="500" EnableCheckBoxSelect="true" AutoPostBack="true" OnSelectedIndexChanged="drpTeam_SelectedIndexChanged">
                        </f:DropDownList>
                    </Items> 
                </f:FormRow>
                <f:FormRow ID="State2Person">
                    <Items>
                        <f:Label runat="server" ID="txtUser" Label="人员"></f:Label>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click" Hidden="true">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
                runat="server" Text="修改" Icon="Pencil">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
                ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除"
                Icon="Delete">
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

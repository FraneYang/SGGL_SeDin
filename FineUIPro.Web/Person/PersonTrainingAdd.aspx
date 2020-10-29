<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonTrainingAdd.aspx.cs" Inherits="FineUIPro.Web.Person.PersonTrainingAdd" %>

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
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="培训计划" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtTrainingPlanCode" runat="server" Label="编号" LabelAlign="Right" 
                            Required="true" ShowRedStar="true"
                            MaxLength="50" >
                        </f:TextBox>
                        <f:TextBox ID="txtTrainingPlanTitle" runat="server" Label="标题" Required="true" ShowRedStar="true"
                            LabelAlign="Right" MaxLength="50">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DatePicker runat="server" Label="开始时间" ID="txtStartTime" EnableEdit="false"></f:DatePicker>
                        <f:DatePicker runat="server" Label="结束时间" ID="txtEndTime" EnableEdit="false"></f:DatePicker>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" ID="txtTrainingPlanContent" Label="主要内容" MaxLength="50"></f:TextBox>
                        <f:DropDownList ID="drpHandleMan" runat="server" Label="审核人" EnableEdit="true" ForceSelection="false" ShowRedStar="true">
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:LinkButton ID="UploadAttach" runat="server" Label="培训课件" Text="上传和查看" OnClick="btnAttachUrl_Click" LabelAlign="Right">
                                    </f:LinkButton>
                        <f:Label runat="server" ></f:Label>
                        <f:HiddenField runat="server" ID="hdTrainingPlanId"></f:HiddenField>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Form ID="Form2" ShowBorder="true" ShowHeader="true" Title="员工" AutoScroll="true" BodyPadding="5px"
                            runat="server"  LabelAlign="Right">
                            <Rows>
                                <f:FormRow ColumnWidths="30% 30% 30% 5%">
                                    <Items>
                                        <f:HiddenField runat="server" ID="hdTrainingPersonId"></f:HiddenField>
                                        <f:DropDownList ID="drpTrainingPerson" runat="server" Label="员工" EnableEdit="true" AutoPostBack="true" OnSelectedIndexChanged="drpTrainingPerson_SelectedIndexChanged">
                                        </f:DropDownList>
                                        <f:Label ID="txtDepart" runat="server" Label="部门" Readonly="true">
                                        </f:Label>
                                         <f:Label ID="txtWorkPost" runat="server" Label="岗位" Readonly="true">
                                        </f:Label>
                                        <f:Button ID="btnSure" Icon="Accept" runat="server" ValidateForms="SimpleForm1"
                                            OnClick="btnSure_Click" ToolTip="确认">
                                        </f:Button>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:Grid ID="gvPerson" ShowBorder="true" ShowHeader="false" EnableCollapse="true" EnableColumnLines="true"
                                            EnableColumnMove="true" runat="server" BoxFlex="1" DataKeyNames="TrainingPersonId,TrainingUserId,TrainingPersonDepartId,TrainingPersonWorkPostId"
                                            DataIDField="TrainingPersonId" AllowSorting="true" 
                                            SortDirection="ASC" EnableTextSelection="True" Height="200px"
                                            EnableRowDoubleClickEvent="true" OnRowDoubleClick="gvPerson_RowDoubleClick">
                                            <Columns>
                                                <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                                                <f:RenderField MinWidth="250px" ColumnID="UserName" DataField="UserName"
                                                    FieldType="String" HeaderText="姓名" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                </f:RenderField>
                                                <f:RenderField MinWidth="250px" ColumnID="DepartName" DataField="DepartName"
                                                    FieldType="String" HeaderText="部门" HeaderTextAlign="Center" TextAlign="Left">
                                                </f:RenderField>
                                                <f:RenderField MinWidth="250px" ColumnID="WorkPostName" DataField="WorkPostName"
                                                    FieldType="String" HeaderText="岗位" HeaderTextAlign="Center" TextAlign="Left">
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
                <f:FormRow>
                    <Items>
                        <f:Form ID="Form3" ShowBorder="true" ShowHeader="true" Title="教材" AutoScroll="true" BodyPadding="5px"
                            runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                            <Rows>
                                <f:FormRow ColumnWidths="30% 30% 30% 5%">
                                    <Items>
                                        <f:HiddenField runat="server" ID="hdTrainingCompanyId"></f:HiddenField>
                                        <f:DropDownList ID="drpCompanyTraining" runat="server" Label="教材类型" EnableEdit="true" >
                                        </f:DropDownList>
                                        <f:Button ID="btn_SureComPany" Icon="Accept" runat="server" 
                                            OnClick="btn_SureComPany_Click" ToolTip="确认">
                                        </f:Button>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:Grid ID="gvCompany" ShowBorder="true" ShowHeader="false" EnableCollapse="true" EnableColumnLines="true"
                                            EnableColumnMove="true" runat="server" BoxFlex="1" DataKeyNames="TrainingCompanyId,CompanyTrainingId"
                                            DataIDField="TrainingCompanyId" AllowSorting="true" 
                                            SortDirection="ASC" EnableTextSelection="True" Height="200px"
                                            EnableRowDoubleClickEvent="true" OnRowDoubleClick="gvCompany_RowDoubleClick">
                                            <Columns>
                                                <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                                                <f:RenderField MinWidth="250px" ColumnID="CompanyTrainingName" DataField="CompanyTrainingName"
                                                    FieldType="String" HeaderText="教材类型" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                </f:RenderField>
                                            </Columns>
                                            <Listeners>
                                                <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu2" />
                                            </Listeners>
                                        </f:Grid>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                            OnClick="btnSubmit_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
        <f:Window ID="Window1" Title="文件上传" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="680px"
        Height="480px">
    </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuDelete" EnablePostBack="true"
                ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除"
                Icon="Delete" OnClick="btnMenuDelete_Click">
            </f:MenuButton>
        </f:Menu>
         <f:Menu ID="Menu2" runat="server">
            <f:MenuButton ID="btnMenuDelete1" EnablePostBack="true"
                ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除"
                Icon="Delete" OnClick="btnMenuDelete1_Click">
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
         var menuID2 = '<%= Menu2.ClientID %>';
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu2(event, rowId) {
            F(menuID2).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>
</body>
</html>

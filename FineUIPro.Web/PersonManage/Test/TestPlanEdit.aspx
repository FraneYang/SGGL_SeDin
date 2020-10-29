<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPlanEdit.aspx.cs" Inherits="FineUIPro.Web.PersonManage.Test.TestPlanEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>规则制定</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
          .f-grid-row.Red
        {
            background-color: red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="10px" ShowBorder="true" ShowHeader="false" AutoScroll="true">
                <Items>
                    <f:Form ID="SimpleForm2" ShowBorder="false" ShowHeader="false" Title="规则制定" AutoScroll="true"
                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right" >
                        <Rows>
                            <f:FormRow>
                            <Items>
                                <f:TextBox ID="txtPlanCode" runat="server" Label="编号" FocusOnPageLoad="true"
                                    LabelWidth="90px" MaxLength="50">
                                </f:TextBox>
                                <f:TextBox ID="txtPlanName" runat="server" Label="名称" Required="true" MaxLength="50"
                                    ShowRedStar="true" LabelWidth="90px">
                                </f:TextBox>
                                    <f:DatePicker runat="server" Required="true" DateFormatString="yyyy-MM-dd HH:mm:ss" 
                                        Label="开考时间" EmptyText="请选择开考时间" LabelWidth="90px"
                                        ID="txtTestStartTime" ShowRedStar="true" ShowTime="true">
                                    </f:DatePicker>
                                    <f:DatePicker runat="server" Required="true" DateFormatString="yyyy-MM-dd HH:mm:ss" 
                                        Label="结束时间" EmptyText="请选择结束时间" LabelWidth="90px"
                                        ID="txtTestEndTime" ShowRedStar="true" ShowTime="true">
                                    </f:DatePicker> 
                            </Items>
                        </f:FormRow>
                            <f:FormRow>
                            <Items> 
                                <f:NumberBox ID="txtDuration" runat="server" Label="时长"  ShowRedStar="true"
                                    NoDecimal="true" NoNegative="true"  LabelWidth="90px"  >
                                </f:NumberBox> 
                                <f:TextBox ID="txtTestPalce" runat="server" Label="考试地点" LabelWidth="90px" MaxLength="500">
                                </f:TextBox>
                                <f:DatePicker ID="txtPlanDate" runat="server" Label="制定日期" LabelWidth="90px"
                                        EnableEdit="true">
                                </f:DatePicker>
                                <f:DropDownList ID="drpPlanMan" runat="server" Label="制定人" EnableEdit="true" LabelWidth="90px">
                                </f:DropDownList>
                            </Items>
                        </f:FormRow>
                            <f:FormRow>
                                <Items>                                   
                                    <f:NumberBox ID="txtSValue" runat="server" Label="单选题(分值)"  ShowRedStar="true"
                                        NoDecimal="true" NoNegative="true"  LabelWidth="110px"   >
                                    </f:NumberBox> 
                                    <f:NumberBox ID="txtMValue" runat="server" Label="多选题(分值)"  ShowRedStar="true"
                                        NoDecimal="true" NoNegative="true"  LabelWidth="110px"  >
                                    </f:NumberBox> 
                                    <f:NumberBox ID="txtJValue" runat="server" Label="判断题(分值)"  ShowRedStar="true"
                                        NoDecimal="true" NoNegative="true"  LabelWidth="110px" >
                                    </f:NumberBox> 
                                    <f:Label runat="server" ID="lbm"></f:Label>
                                </Items>
                            </f:FormRow>
                    </Rows>
                       
                    </f:Form>
                    <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="true" Title="题型" AutoScroll="true"
                       runat="server" RedStarPosition="BeforeText" LabelAlign="Right" EnableTableStyle="true">
                         <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Bottom" ToolbarAlign="Right" runat="server">
                                <Items>
                                   <%-- <f:Label runat="server" ID="lbQuestionCount" Label="题目数量">
                                    </f:Label>
                                     <f:Label runat="server" ID="lbTotalScore" Label="总分值">
                                    </f:Label>--%>
                                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                    </f:ToolbarFill>
                                      <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm2"
                                        OnClick="btnSave_Click">
                                    </f:Button> 
                                    <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="发布" ValidateForms="SimpleForm2"
                                        OnClick="btnSubmit_Click">
                                    </f:Button>
                                     <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                                       </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:HiddenField runat="server" ID="hdTestPlanTrainingId"></f:HiddenField>
                                <f:DropDownList runat="server" ID="drpUserType" Label="人员" LabelWidth="90px">
                                    <f:ListItem Text="管理人员" Value="1" />
                                    <f:ListItem Text="临时用户" Value="2" />
                                    <f:ListItem Text="作业人员" Value="3" />
                                </f:DropDownList>
                                <f:DropDownList runat="server" ID="drpTraining" EnableEdit="true"
                                    Label="题目类型" LabelWidth="90px" ShowRedStar="true">
                                </f:DropDownList>
                                <f:NumberBox ID="txtTestType1Count" runat="server" Label="单选题"  ShowRedStar="true"
                                    NoDecimal="true" NoNegative="true"  LabelWidth="90px">
                                </f:NumberBox>
                                <f:NumberBox ID="txtTestType2Count" runat="server" Label="多选题"  ShowRedStar="true"
                                    NoDecimal="true" NoNegative="true"  LabelWidth="90px">
                                </f:NumberBox>
                                <f:NumberBox ID="txtTestType3Count" runat="server" Label="判断题"  ShowRedStar="true"
                                    NoDecimal="true" NoNegative="true"  LabelWidth="90px">
                                </f:NumberBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false"  EnableCollapse="true" EnableColumnLines="true" 
                                    EnableColumnMove="true" runat="server" BoxFlex="1" DataKeyNames="TestPlanTrainingId" 
                                    DataIDField="TestPlanTrainingId" AllowSorting="true" SortField="UserType,TrainingCode" 
                                    SortDirection="ASC" EnableTextSelection="True"  Height="260px"
                                    EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">   
                                        <Toolbars>                                   
                                            <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                                                <Items>
                                                <f:ToolbarFill runat="server"></f:ToolbarFill>    
                                                <f:Button ID="btnSure" Icon="Accept" runat="server"  ValidateForms="SimpleForm1" 
                                                    OnClick="btnSure_Click" ToolTip="确认">
                                                </f:Button>                                                
                                                </Items>
                                            </f:Toolbar>
                                    </Toolbars>
                                    <Columns>                        
                                        <f:RenderField Width="200px" ColumnID="UserTypeName" DataField="UserTypeName" 
                                            FieldType="String" HeaderText="考生类型"  HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:RenderField Width="200px" ColumnID="TrainingName" DataField="TrainingName" ExpandUnusedSpace="true"
                                            FieldType="String" HeaderText="题目类型"  HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:RenderField Width="150px" ColumnID="TestType1Count" DataField="TestType1Count" 
                                            FieldType="Int" HeaderText="单选题"  HeaderTextAlign="Center" TextAlign="Right">
                                        </f:RenderField>
                                        <f:RenderField Width="150px" ColumnID="TestType2Count" DataField="TestType2Count" 
                                            FieldType="Int" HeaderText="多选题"  HeaderTextAlign="Center" TextAlign="Right">
                                        </f:RenderField>
                                        <f:RenderField Width="150px" ColumnID="TestType3Count" DataField="TestType3Count" 
                                            FieldType="Int" HeaderText="判断题"  HeaderTextAlign="Center" TextAlign="Right">
                                        </f:RenderField>   
                                        <f:RenderField  HeaderText="UserType" ColumnID="UserType" DataField="UserType" 
                                            FieldType="String" Hidden="true"></f:RenderField>
                                        <f:RenderField  HeaderText="TrainingId" ColumnID="TrainingId" DataField="TrainingId" 
                                            FieldType="String" Hidden="true"></f:RenderField>
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
        </f:Panel>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
        runat="server" Text="修改" Icon="TableEdit">
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

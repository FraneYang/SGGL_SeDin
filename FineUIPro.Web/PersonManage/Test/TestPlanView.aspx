<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPlanView.aspx.cs" Inherits="FineUIPro.Web.PersonManage.Test.TestPlanView" %>

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
                                    <f:TextBox ID="txtPlanCode" runat="server" Label="编号" LabelWidth="90px"  Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtPlanName" runat="server" Label="名称"  LabelWidth="90px" Readonly="true">
                                    </f:TextBox>
                                        <f:TextBox runat="server" Label="开考时间"  ID="txtTestStartTime" LabelWidth="90px" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox runat="server"  Label="结束时间" ID="txtTestEndTime" LabelWidth="90px" Readonly="true">
                                    </f:TextBox> 
                            </Items>
                        </f:FormRow>
                            <f:FormRow>
                            <Items> 
                                <f:TextBox ID="txtDuration" runat="server" Label="时长" LabelWidth="90px"  Readonly="true">
                                </f:TextBox> 
                                <f:TextBox ID="txtTestPalce" runat="server" Label="考试地点" LabelWidth="90px" Readonly="true">
                                </f:TextBox>
                                <f:TextBox ID="txtPlanDate" runat="server" Label="制定日期" LabelWidth="90px" Readonly="true">
                                </f:TextBox>
                                <f:TextBox ID="drpPlanMan" runat="server" Label="制定人" LabelWidth="90px" Readonly="true">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                            <f:FormRow>
                                <Items>                                   
                                    <f:TextBox ID="txtSValue" runat="server" Label="单选题(分值)" LabelWidth="110px"   Readonly="true">
                                    </f:TextBox> 
                                    <f:TextBox ID="txtMValue" runat="server" Label="多选题(分值)" LabelWidth="110px"  Readonly="true">
                                    </f:TextBox> 
                                    <f:TextBox ID="txtJValue" runat="server" Label="判断题(分值)" LabelWidth="110px" Readonly="true">
                                    </f:TextBox> 
                                   <f:TextBox ID="txtActualTime" runat="server" Label="实际结束时间" LabelWidth="110px" Readonly="true">
                                </f:TextBox>
                                </Items>
                            </f:FormRow>
                    </Rows>
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                                <Items>
                                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                    </f:ToolbarFill>
                                     <f:Button ID="btnSave" Icon="BookEdit" runat="server" ToolTip="开始考试" ValidateForms="SimpleForm2"
                                        OnClick="btnSave_Click" Hidden="true">
                                    </f:Button> 
                                    <f:Button ID="btnSubmit" Icon="BookKey" runat="server" ToolTip="结束考试" ValidateForms="SimpleForm2"
                                        OnClick="btnSubmit_Click" Hidden="true">
                                    </f:Button>
                                     <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose" Hidden="true">
                                     </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                    </f:Form>
                    <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" Title="题型" AutoScroll="true"
                       runat="server" RedStarPosition="BeforeText" LabelAlign="Right" EnableTableStyle="true">
                    <Rows>                        
                        <f:FormRow>
                            <Items>
                                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false"  EnableCollapse="true" EnableColumnLines="true" 
                                    EnableColumnMove="true" runat="server" BoxFlex="1" DataKeyNames="TestPlanTrainingId" 
                                    DataIDField="TestPlanTrainingId" AllowSorting="true" SortField="UserType,TrainingCode" 
                                    SortDirection="ASC" EnableTextSelection="True"  Height="300px" >                                         
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

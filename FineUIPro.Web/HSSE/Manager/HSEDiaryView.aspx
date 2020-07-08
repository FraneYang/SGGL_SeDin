<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSEDiaryView.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Manager.HSEDiaryView" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HSE日志</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        .formtitle .f-field-body {
            text-align: center;           
            margin: 10px 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" TitleAlign="Center"
        BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" EnableTableStyle="true">
        <Rows>
             <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtDiaryDate" runat="server" Label="日期" Readonly="true" LabelWidth="90px">
                    </f:TextBox>
                      <f:TextBox ID="txtUserName" runat="server" Label="工程师" Readonly="true" LabelWidth="90px">
                    </f:TextBox>                       
                 </Items>
             </f:FormRow>
            <f:FormRow ColumnWidths="40% 30% 30%"> 
                 <Items>
                    <f:TextArea ID="TextBox1" runat="server" Text="1HSE检查情况及检查次数" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>
                      <f:TextArea ID="txtValue1" runat="server" Text="0" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>   
                     <f:TextArea ID="TextBox3" runat="server" Text="来源于“安全检查管理”中各类型检查。" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>   
                 </Items>
             </f:FormRow>
             <f:FormRow ColumnWidths="40% 30% 30%"> 
                 <Items>
                    <f:TextArea ID="TextBox2" runat="server" Text="2隐患整改情况及隐患整改数量" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>
                      <f:TextArea ID="txtValue2" runat="server" Text="0" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>   
                     <f:TextArea ID="TextBox5" runat="server" Text="来源于“安全检查管理”中各类型检查。" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>   
                 </Items>
             </f:FormRow>
            <f:FormRow ColumnWidths="40% 30% 30%"> 
                 <Items>
                    <f:TextArea  ID="TextBox4" runat="server" Text="3作业许可情况及作业票数量" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>
                      <f:TextArea ID="txtValue3" runat="server" Text="0" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>   
                     <f:TextArea ID="TextBox7" runat="server" Text="来源于“作业许可管理”中现场作业许可证。" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>   
                 </Items>
             </f:FormRow>
            <f:FormRow ColumnWidths="40% 30% 30%"> 
                 <Items>
                    <f:TextArea ID="TextBox6" runat="server" Text="4施工机具、安全设施检查、验收情况及检查验收数量" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>
                      <f:TextArea ID="txtValue4" runat="server" Text="0" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>   
                     <f:TextArea ID="TextBox9" runat="server" Text="来源于“施工机具”中施工机具、安全设施检查验收。" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>   
                 </Items>
             </f:FormRow>
            <f:FormRow ColumnWidths="40% 30% 30%"> 
                 <Items>
                    <f:TextArea ID="TextBox8" runat="server" Text="5危险源辨识工作情况及次数" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>
                      <f:TextArea ID="txtValue5" runat="server" Text="0" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>   
                     <f:TextArea ID="TextBox11" runat="server" Text="来源于“危险源辨识与评价。”" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>   
                 </Items>
             </f:FormRow>
            <f:FormRow ColumnWidths="40% 30% 30%"> 
                 <Items>
                    <f:TextArea ID="TextBox10" runat="server" Text="6应急计划修编、演练及物资准备情况及次数" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>
                      <f:TextArea ID="txtValue6" runat="server" Text="0" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>   
                     <f:TextArea ID="TextBox13" runat="server" Text="来源于“应急响应管理。”" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>   
                 </Items>
             </f:FormRow>
            <f:FormRow ColumnWidths="40% 30% 30%"> 
                 <Items>
                    <f:TextArea ID="TextBox12" runat="server" Text="7教育培训情况及人次" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>
                      <f:TextArea ID="txtValue7" runat="server" Text="0" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>   
                     <f:TextArea ID="TextBox15" runat="server" Text="来源于“教育培训”中“培训记录”。" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>   
                 </Items>
             </f:FormRow>
             <f:FormRow ColumnWidths="40% 30% 30%"> 
                 <Items>
                    <f:TextArea ID="TextBox14" runat="server" Text="8 HSE会议情况及次数" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>
                      <f:TextArea ID="txtValue8" runat="server" Text="0" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>   
                     <f:TextArea ID="TextBox17" runat="server" Text="来源于“会议管理”" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>   
                 </Items>
             </f:FormRow>
            <f:FormRow ColumnWidths="40% 30% 30%"> 
                 <Items>
                    <f:TextArea ID="TextBox16" runat="server" Text="9 HSE宣传工作情况" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>
                      <f:TextArea ID="txtValue9" runat="server" Text="0" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>   
                     <f:TextArea ID="TextBox19" runat="server" Text="来源于“信息管理”“HSE宣传活动”" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>   
                 </Items>
             </f:FormRow>
            <f:FormRow ColumnWidths="40% 30% 30%"> 
                 <Items>
                    <f:TextArea ID="TextBox18" runat="server" Text="10 HSE奖惩工作情况、HSE奖励次数、HSE处罚次数" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>
                      <f:TextArea ID="txtValue10" runat="server" Text="0" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>   
                     <f:TextArea ID="TextBox21" runat="server" Text="来源于奖惩管理。" Readonly="true" LabelWidth="90px" Height="35px">
                    </f:TextArea>   
                 </Items>
             </f:FormRow>
             <f:FormRow>
                 <Items>
                    <f:TextArea ID="txtDailySummary" runat="server" Label="今日小结" Readonly="true" LabelWidth="90px" Height="64px">
                    </f:TextArea>                    
                 </Items>
             </f:FormRow> 
             <f:FormRow>
                 <Items>
                    <f:TextArea ID="txtTomorrowPlan" runat="server" Label="明日计划" Readonly="true" LabelWidth="90px" Height="64px">
                    </f:TextArea>                    
                 </Items>
             </f:FormRow> 
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose" MarginRight="10px">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>

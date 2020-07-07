<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPlanView.aspx.cs" Inherits="FineUIPro.Web.EduTrain.TestPlanView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考试计划</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        Layout="VBox" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
        LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtPlanCode" runat="server" Label="编号" Readonly="true" LabelAlign="Right">
                    </f:TextBox>
                    <f:TextBox ID="txtPlanName" runat="server" Label="名称" Readonly="true" LabelAlign="Right">
                    </f:TextBox>
                    <f:TextBox ID="txtStates" runat="server" Label="状态" Readonly="true" LabelAlign="Right">
                    </f:TextBox>
                </Items>
            </f:FormRow>  
            <f:FormRow>
                <Items>
                     <f:TextBox ID="txtWorkPostNames" runat="server" Label="考生岗位" Readonly="true" LabelAlign="Right">
                    </f:TextBox>
                </Items>
            </f:FormRow>
              <f:FormRow>
                <Items>
                    <f:TextBox ID="txtTestStartTime" runat="server" Label="扫码开始时间" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtTestEndTime" runat="server" Label="扫码结束时间" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                     <f:TextBox ID="txtTestPalce" runat="server" Label="考试地点" Readonly="true" LabelAlign="Right">
                    </f:TextBox>  
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtDuration" runat="server" Label="时长" Readonly="true" LabelAlign="Right">
                    </f:TextBox>
                    <f:TextBox ID="txtTotalScore" runat="server" Label="总分值" Readonly="true" LabelAlign="Right">
                    </f:TextBox>
                    <f:TextBox ID="txtQuestionCount" runat="server" Label="试题数量" Readonly="true" LabelAlign="Right">
                    </f:TextBox>
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>                  
                    <f:TextBox ID="txtTestType1" runat="server" Label="单选题数" Readonly="true" LabelAlign="Right">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                     <f:TextBox ID="txtTestType2" runat="server" Label="多选题数" Readonly="true" LabelAlign="Right">
                     </f:TextBox>                   
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtTestType3" runat="server" Label="判断题数" Readonly="true" LabelAlign="Right">
                    </f:TextBox>
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>                  
                    <f:TextArea ID="txtTrainingName" runat="server" Label="试题类型" Height="50px" Readonly="true" LabelAlign="Right">
                    </f:TextArea>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>   
    </form>
</body>
</html>

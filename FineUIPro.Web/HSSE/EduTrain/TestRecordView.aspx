<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestRecordView.aspx.cs" Inherits="FineUIPro.Web.HSSE.EduTrain.TestRecordView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看考试记录</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
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
                    <f:TextBox ID="txtPlanName" runat="server" Label="考试名称" Readonly="true" LabelAlign="Right">
                    </f:TextBox>
                    <f:TextBox ID="txtTestManName" runat="server" Label="考生姓名" Readonly="true" LabelAlign="Right">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtAbstracts" runat="server" Label="题目内容" Height="50px" Readonly="true"
                        LabelAlign="Right">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtTestType" runat="server" Label="题型" Readonly="true" LabelAlign="Right">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtAItem" runat="server" Label="答案项A" Readonly="true" LabelAlign="Right">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtBItem" runat="server" Label="答案项B" Readonly="true" LabelAlign="Right">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCItem" runat="server" Label="答案项C" Readonly="true" LabelAlign="Right">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtDItem" runat="server" Label="答案项D" Readonly="true" LabelAlign="Right">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtEItem" runat="server" Label="答案项E" Readonly="true" LabelAlign="Right">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtAnswerItems" runat="server" Label="正确答案" Readonly="true" LabelAlign="Right">
                    </f:TextBox>
                    <f:TextBox ID="txtScore" runat="server" Label="题目分值" Readonly="true" LabelAlign="Right">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>                   
                    <f:TextBox ID="txtSelectedItem" runat="server" Label="回答项" Readonly="true" LabelAlign="Right">
                    </f:TextBox>
                    <f:TextBox ID="txtSubjectScore" runat="server" Label="得分" Readonly="true" LabelAlign="Right">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow runat="server" ID="trUrl" ColumnWidths="10% 90%">
                <Items>                   
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="图片">
                        <table>
                            <tr style="height: 25px">
                                <td align="left">
                                    <div id="divFile" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    </form>
</body>
</html>

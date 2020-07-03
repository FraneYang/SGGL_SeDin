<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestTrainingItemSave.aspx.cs" Inherits="FineUIPro.Web.HSSE.EduTrain.TestTrainingItemSave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" AjaxAspnetControls="divFile,divBeImageUrl" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow ColumnWidths="40% 40% 20%">
                <Items>
                    <f:TextBox ID="txtTrainingItemCode" runat="server" Label="编号" Required="true" ShowRedStar="true"
                        FocusOnPageLoad="true" MaxLength="50">
                    </f:TextBox>
                    <f:RadioButtonList runat="server" ID="rblTestType" Label="题型" 
                        AutoPostBack="true" OnSelectedIndexChanged="rblTestType_SelectedIndexChanged">
                         <f:RadioItem Text="单选题" Value="1" Selected="true"/>
                         <f:RadioItem Text="多选题" Value="2" />
                         <f:RadioItem Text="判断题" Value="3" />
                    </f:RadioButtonList>
                    <f:Label runat="server" ID="lbScore" Label="分值"></f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                 <Items>
                  <f:DropDownBox runat="server" Label="适合岗位" ID="DropDownBox1" 
                      DataControlID="RadioButtonList1" EnableMultiSelect="true" >
                    <PopPanel>
                        <f:SimpleForm ID="SimpleForm2" BodyPadding="10px" runat="server" AutoScroll="true"
                            ShowBorder="True" ShowHeader="false" Hidden="true">
                           <Items>
                               <f:Label ID="Label1" runat="server" Text="请选择适合的岗位"></f:Label>
                                <f:CheckBoxList ID="RadioButtonList1" ColumnNumber="3" runat="server"> 
                                </f:CheckBoxList>
                           </Items>
                        </f:SimpleForm>
                    </PopPanel>
                </f:DropDownBox>
                </Items> 
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtAbstracts" runat="server" Label="内容" Required="true" ShowRedStar="true"
                        MaxLength="2000" Height="64px"></f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtAItem" runat="server" Label="答案项A" MaxLength="500" ShowRedStar="true" Required="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtBItem" runat="server" Label="答案项B" MaxLength="500" ShowRedStar="true" Required="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow runat="server" ID="trC">
                <Items>
                    <f:TextBox ID="txtCItem" runat="server" Label="答案项C" MaxLength="500">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow runat="server" ID="trD">
                <Items>
                    <f:TextBox ID="txtDItem" runat="server" Label="答案项D" MaxLength="500">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow runat="server" ID="trE">
                <Items>
                    <f:TextBox ID="txtEItem" runat="server" Label="答案项E" MaxLength="500">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownBox runat="server" Label="正确答案项" ID="DropDownBox2" DataControlID="RadioButtonList2" EnableMultiSelect="true">
                    <PopPanel>
                        <f:SimpleForm ID="SimpleForm3" BodyPadding="10px" runat="server" AutoScroll="true"
                            ShowBorder="True" ShowHeader="false" Hidden="true">
                           <Items>
                               <f:Label ID="Label2" runat="server" Text="请选择正确答案项："></f:Label>
                                <f:CheckBoxList ID="RadioButtonList2" ColumnNumber="1" runat="server">                                 
                                </f:CheckBoxList>
                           </Items>
                        </f:SimpleForm>
                    </PopPanel>
                </f:DropDownBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:FileUpload runat="server" ID="btnFile" EmptyText="请选择图片/视频" OnFileSelected="btnFile_Click"
                        AutoPostBack="true" Label="图片/视频" >
                    </f:FileUpload>
                    <f:ContentPanel ID="ContentPanel2" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="附件">
                        <table>
                            <tr style="height: 25px">
                                <td align="left">
                                    <div id="divFile" runat="server">
                                    </div>
                                </td>
                            </tr>
                             <tr style="height:130px" runat="server" id="trImageUrl" visible="false">
                                <td style="text-align:left">
                                    <div id="divBeImageUrl" runat="server">
                                    </div>
                                </td>                               
                            </tr>
                        </table>
                    </f:ContentPanel>
                     <f:Button ID="btnDelete" Icon="Delete" runat="server" OnClick="btnDelete_Click" ToolTip="删除">
                    </f:Button>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp"></f:Label>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click" Hidden="true">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" runat="server" Icon="SystemClose">
                    </f:Button>
                    <f:HiddenField ID="hdCompileMan" runat="server">
                    </f:HiddenField>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>

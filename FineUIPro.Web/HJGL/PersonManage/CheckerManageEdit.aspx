<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckerManageEdit.aspx.cs" Inherits="FineUIPro.Web.HJGL.PersonManage.CheckerManageEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="drpUnitId" runat="server" Label="所属单位" Enabled="false"
                            AutoPostBack="true" OnSelectedIndexChanged="drpUnitId_SelectedIndexChanged1" LabelWidth="120px" Required="true" ShowRedStar="true">
                        </f:DropDownList>
                        <f:TextBox ID="txtCheckerCode" runat="server" Label="检测工编号"
                            MaxLength="50" FocusOnPageLoad="true" LabelWidth="120px">
                        </f:TextBox>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtCheckerName" runat="server" Label="检测工姓名" Enabled="false"
                            Required="true" MaxLength="50" ShowRedStar="true" LabelWidth="120px">
                        </f:TextBox>
                        <f:DatePicker ID="txtBirthday" runat="server" Label="出生日期" Enabled="false"
                            LabelWidth="120px">
                        </f:DatePicker>


                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtIdentityCard" runat="server" Label="身份证号" Enabled="false"
                            MaxLength="18" LabelWidth="120px" Required="true" ShowRedStar="true">
                        </f:TextBox>
                        <f:TextBox ID="txtCertificateCode" runat="server" Label="证书编号"
                            MaxLength="50" LabelWidth="120px" Required="true" ShowRedStar="true">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:RadioButtonList ID="rblSex" runat="server" Label="性别" LabelWidth="120px" Enabled="false">
                            <f:RadioItem Selected="true" Value="1" Text="男" />
                            <f:RadioItem Value="2" Text="女" />
                        </f:RadioButtonList>
                        <f:CheckBox ID="cbIsUsed" runat="server" Label="是否在岗" Enabled="false"
                            LabelWidth="120px">
                        </f:CheckBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:LinkButton ID="UploadAttach" runat="server" Label="证书附件"
                            Text="上传和查看" OnClick="btnAttachUrl_Click" LabelWidth="120px">
                        </f:LinkButton>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:HiddenField ID="hdCheckerId" runat="server">
                        </f:HiddenField>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1" OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭"
                            runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
        <f:Window ID="Window1" Title="文件上传" Hidden="true" EnableIFrame="true"
        EnableMaximize="true" Target="Parent" EnableResize="false" runat="server" IsModal="true"
        Width="680px" Height="480px">
    </f:Window>
    </form>
</body>
</html>

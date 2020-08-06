<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonSetEdit.aspx.cs" Inherits="FineUIPro.Web.Person.PersonSetEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑员工</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtUserCode" runat="server" Label="编号" MaxLength="20" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelWidth="110px">
                        </f:TextBox>
                        <f:TextBox ID="txtUserName" runat="server" Label="姓名" Required="true" ShowRedStar="true" MaxLength="20"
                            FocusOnPageLoad="true" LabelWidth="110px">
                        </f:TextBox>
                        <f:TextBox ID="txtAccount" runat="server" Label="工号/账号" Required="true" ShowRedStar="true" MaxLength="50" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelWidth="110px">
                        </f:TextBox>
                     
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                            <f:TextBox ID="txtIdentityCard" runat="server" Label="身份证号" MaxLength="50" ShowRedStar="true" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelWidth="110px">
                            <%--RegexPattern="IDENTITY_CARD"--%>
                        </f:TextBox>
                        <f:DatePicker ID="txtBirthday" runat="server" Label="出生日期"
                            LabelWidth="110px">
                        </f:DatePicker>
                           <f:RadioButtonList ID="rblSex" runat="server" Label="性别" LabelWidth="110px">
                            <f:RadioItem Selected="true" Value="1" Text="男" />
                            <f:RadioItem Value="2" Text="女" />
                        </f:RadioButtonList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="drpUnit" runat="server" Label="单位" EnableEdit="true" ForceSelection="false"
                            Required="true" ShowRedStar="true" LabelWidth="110px" Readonly="true">
                        </f:DropDownList>
                        <f:DropDownList ID="drpDepart" runat="server" Label="部门" EnableEdit="true" Readonly="true"
                            ForceSelection="false" LabelWidth="110px">
                        </f:DropDownList>
                        <f:TextBox ID="txtTelephone" runat="server" Label="手机号码" MaxLength="50" LabelWidth="110px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                           <f:DropDownList ID="drpIsOffice" runat="server" Label="本部人员" EnableEdit="true" ForceSelection="false" LabelWidth="110px">
                        </f:DropDownList>
                        <f:DropDownList ID="drpRole" runat="server" Label="本部角色" EnableEdit="true" ForceSelection="false" LabelWidth="110px">
                        </f:DropDownList>                   
                        <f:DropDownList ID="drpIsPost" runat="server" Label="在岗" EnableEdit="true" ForceSelection="false"
                            Required="true" ShowRedStar="true" LabelWidth="110px">
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtPoliticalstatus" runat="server" Label="政治面貌" MaxLength="50" LabelWidth="110px">
                        </f:TextBox>
                        <f:TextBox ID="txtHometown" runat="server" Label="籍贯" MaxLength="50" LabelWidth="110px">
                        </f:TextBox>
                        <f:TextBox ID="txtEducation" runat="server" Label="学历" MaxLength="50" LabelWidth="110px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtGraduate" runat="server" Label="毕业院校" MaxLength="50" LabelWidth="110px">
                        </f:TextBox>
                        <f:TextBox ID="txtMajor" runat="server" Label="所学专业" MaxLength="50" LabelWidth="110px">
                        </f:TextBox>
                        <f:DropDownList ID="drpPostTitle" runat="server" Label="职称" MaxLength="50" LabelWidth="110px">
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DatePicker runat="server" Label="入院日期" ID="txtIntoDate" LabelWidth="110px"></f:DatePicker>
                        <f:DropDownList ID="drpCertificate" runat="server" Label="职业资格证书" MaxLength="500" LabelWidth="110px" EnableCheckBoxSelect="true" EnableMultiSelect="true">
                        </f:DropDownList>
                        <f:TextBox ID="txtProjectId" runat="server" Label="当前所在项目" MaxLength="50" LabelWidth="110px" Readonly="true">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtProjectRoleId" runat="server" Label="当前项目角色" MaxLength="50" LabelWidth="110px" Readonly="true">
                        </f:TextBox>
                        <f:DatePicker runat="server" Label="进入项目时间" ID="txtIntoProjectDate" LabelWidth="110px" Readonly="true"></f:DatePicker>
                        <f:DatePicker runat="server" Label="离开项目时间" ID="txtOutProjectDate" LabelWidth="110px" Readonly="true"></f:DatePicker>
                    </Items>
                </f:FormRow>

                <f:FormRow>
                    <Items>
                        <f:DatePicker runat="server" Label="合同到期时间" ID="txtValidityDate" LabelWidth="110px"></f:DatePicker>
                        <f:DropDownList ID="drpWorkPost" runat="server" Label="岗位" MaxLength="500" LabelWidth="110px" EnableCheckBoxSelect="true" EnableMultiSelect="true">
                        </f:DropDownList>
                        <f:Button ID="BtnRole" Text="历史角色" ToolTip="查看" Icon="TableCell" runat="server"
                            OnClick="BtnRole_Click" >
                        </f:Button>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                         <f:Image ID="Image2" ImageUrl="~/res/images/Signature0.png" runat="server" Height="35px" Width="110px"
                                    BoxFlex="1" Label="签名" LabelWidth="110px">
                                </f:Image>
                                <f:FileUpload runat="server" ID="fileSignature" EmptyText="请选择"
                                    OnFileSelected="btnSignature_Click" AutoPostBack="true" Width="150px" LabelWidth="110px">
                                </f:FileUpload>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1" Hidden="true"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
        <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="880px"
            Height="500px">
        </f:Window>
    </form>
</body>
</html>

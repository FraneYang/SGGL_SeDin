<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WelderManageEdit.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.PersonManage.WelderManageEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑焊工管理</title>
    <base target="_self" />
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        .userphoto .f-field-label
        {
            margin-top: 0;
        }
        
        .userphoto img
        {
            width: 150px;
            height: 180px;            
        }
        
        .uploadbutton .f-btn
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Panel ID="Panel2" runat="server" ShowHeader="false" ShowBorder="false" ColumnWidth="100%"
        MarginRight="5px">
        <Items>
            <f:Form ID="SimpleForm1" LabelAlign="Right" MessageTarget="Qtip" RedStarPosition="BeforeText"
                LabelWidth="90px" BodyPadding="5px" ShowBorder="false" ShowHeader="false" runat="server"
                AutoScroll="false">
                <Items>
                    <f:Panel ID="Panel3" runat="server" ShowBorder="false" ShowHeader="false" Layout="HBox"
                        BoxConfigAlign="StretchMax">
                        <Items>
                            <f:Panel ID="Panel1" Title="面板1" BoxFlex="5" MarginRight="5px" runat="server" ShowBorder="false"
                                Width="200px" ShowHeader="false">
                                <Items> 
                                    <f:DropDownList ID="drpUnitId" runat="server" Label="单位名称" Enabled="false" 
                                        AutoPostBack="true" OnSelectedIndexChanged="drpUnitId_SelectedIndexChanged"  LabelWidth="120px" Required="true" ShowRedStar="true">
                                    </f:DropDownList>
                                   
                                    
                                     <f:TextBox ID="txtWelderName" runat="server" Label="焊工姓名" Enabled="false" 
                                        Required="true" MaxLength="50" ShowRedStar="true" LabelWidth="120px">
                                    </f:TextBox>
                                    <f:TextBox ID="txtCertificateCode" runat="server" Label="证书编号"
                                        MaxLength="50" LabelWidth="120px" Required="true" ShowRedStar="true">
                                    </f:TextBox>
                                    <f:DatePicker ID="txtBirthday" runat="server" Label="出生日期" Enabled="false"
                                        LabelWidth="120px">
                                    </f:DatePicker>
                                    <f:CheckBox ID="cbIsOnDuty" runat="server" Label="是否在岗" Enabled="false"
                                        LabelWidth="120px">
                                    </f:CheckBox>
                                </Items>
                            </f:Panel>
                            <f:Panel ID="Panel4" runat="server" BoxFlex="5" ShowBorder="false" ShowHeader="false"
                                Width="200px" MarginRight="5px" Layout="VBox">
                                <Items>
                                    <f:TextBox ID="txtWelderCode" runat="server" Label="焊工号"
                                        Required="true" MaxLength="50" ShowRedStar="true" FocusOnPageLoad="true" LabelWidth="120px">
                                    </f:TextBox>
                                    <f:RadioButtonList ID="rblSex" runat="server" Label="性别" LabelWidth="120px" Enabled="false">
                                        <f:RadioItem Selected="true" Value="1" Text="男" />
                                        <f:RadioItem Value="2" Text="女" />
                                    </f:RadioButtonList>
                                    <f:TextBox ID="txtIdentityCard" runat="server" Label="身份证号" Enabled="false"
                                        MaxLength="18" LabelWidth="120px" Required="true" ShowRedStar="true">
                                    </f:TextBox>
                                    <f:DatePicker ID="txtCertificateLimitTime" runat="server" Label="有效期"
                                        LabelWidth="120px" Required="true" ShowRedStar="true">
                                    </f:DatePicker>
                                    <f:TextBox ID="txtWelderLevel" runat="server" Label="焊工等级"
                                        MaxLength="50" LabelWidth="120px">
                                    </f:TextBox> 
                                </Items>
                            </f:Panel>
                            <f:Panel ID="Panel5" Title="面板1" BoxFlex="3" runat="server" ShowBorder="false" ShowHeader="false"
                                Layout="VBox">
                                <Items>
                                    <f:Image ID="Image1" CssClass="userphoto" ImageUrl="~/res/images/blank_180.png" runat="server"
                                        BoxFlex="1">
                                    </f:Image>
                                </Items>
                                <%--<Items>
                                    <f:FileUpload runat="server" ID="filePhoto" EmptyText="请选择照片" OnFileSelected="btnPhoto_Click"
                                        AutoPostBack="true">
                                    </f:FileUpload>
                                </Items>--%>
                            </f:Panel>
                        </Items>
                    </f:Panel>
                </Items>
                <Items>
                    <f:Form ID="Form7" ShowBorder="false" ShowHeader="false" runat="server">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtRemark" runat="server" Label="备注" MaxLength="500"
                                        LabelWidth="140px">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:LinkButton ID="UploadAttach" runat="server" Label="焊工证附件"
                                        Text="上传和查看" OnClick="btnAttachUrl_Click" LabelWidth="180px">
                                    </f:LinkButton>
                                    <f:Button runat="server" ID="btnQR" OnClick="btnQR_Click" Text="二维码生成" MarginLeft="100px"></f:Button>
                                    <f:Button runat="server" ID="btnSee" OnClick="btnSee_Click" Text="二维码查看" MarginLeft="5px" ></f:Button>
                                </Items>
                            </f:FormRow>
                        </Rows>
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                                <Items>
                                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1"
                                        OnClick="btnSave_Click">
                                    </f:Button>
                                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭"
                                        runat="server" Icon="SystemClose">
                                    </f:Button>
                                     
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                    </f:Form>
                </Items>
            </f:Form>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="文件上传" Hidden="true" EnableIFrame="true"
        EnableMaximize="true" Target="Parent" EnableResize="false" runat="server" IsModal="true"
        Width="680px" Height="480px">
    </f:Window>
    <f:Window ID="Window2" Title="文件上传" Hidden="true" EnableIFrame="true"
        EnableMaximize="true" Target="Parent" EnableResize="false" runat="server" IsModal="true"
        Width="480px" Height="480px">
    </f:Window>
    </form>
</body>
</html>

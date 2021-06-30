<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserListEdit.aspx.cs" Inherits="FineUIPro.Web.SysManage.UserListEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑用户</title>
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
                        <f:TextBox ID="txtUserCode" runat="server" Label="编号" MaxLength="20"  LabelWidth="90px">
                        </f:TextBox>
                        <f:TextBox ID="txtUserName" runat="server" Label="姓名" Required="true" ShowRedStar="true" MaxLength="20"
                            FocusOnPageLoad="true" LabelWidth="90px">
                        </f:TextBox>
                          <f:TextBox ID="txtAccount" runat="server" Label="登录账号" Required="true" ShowRedStar="true" MaxLength="50" 
                              LabelWidth="90px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                          <f:DropDownList ID="drpUnit" runat="server" Label="单位" EnableEdit="true" ForceSelection="false" AutoPostBack="true" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged"
                            Required="true" ShowRedStar="true" LabelWidth="90px">
                        </f:DropDownList>
                        <f:TextBox ID="txtIdentityCard" runat="server" Label="身份证号" MaxLength="50" ShowRedStar="true" Required="true"
                            LabelWidth="90px" RegexPattern="IDENTITY_CARD">
                        </f:TextBox>
                        <f:TextBox ID="txtTelephone" runat="server" Label="手机号码" MaxLength="50" LabelWidth="90px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow runat="server" ID="trServer">
                    <Items>                      
                        <f:DropDownList ID="drpDepart" runat="server" Label="部门" ShowRedStar="true" Required="true"
                            EnableEdit="true" ForceSelection="false" LabelWidth="90px">
                        </f:DropDownList>
                        <f:DropDownList ID="drpIsOffice" runat="server" Label="本部人员"  
                            EnableEdit="true" ForceSelection="false" LabelWidth="90px">
                        </f:DropDownList>
                          <f:DropDownList ID="drpRole" runat="server" Label="本部角色" EnableEdit="true" EnableMultiSelect="true" EnableCheckBoxSelect="true" ForceSelection="false" LabelWidth="90px">
                        </f:DropDownList>
                    </Items>
                </f:FormRow>                
                <f:FormRow ColumnWidths="33% 33% 24% 10%">
                    <Items>
                          <f:DropDownList ID="drpIsPost" runat="server" Label="在岗" EnableEdit="true" ForceSelection="false"
                            Required="true" ShowRedStar="true" LabelWidth="90px">
                        </f:DropDownList>
                         <f:Image ID="Image2" ImageUrl="~/res/images/Signature0.png" runat="server" Height="35px" Width="90px"
                                BoxFlex="1" Label="签名" LabelWidth="90px">
                            </f:Image>
                            <f:FileUpload runat="server" ID="fileSignature" EmptyText="请选择"
                                OnFileSelected="btnSignature_Click" AutoPostBack="true" Width="150px" LabelWidth="90px">
                            </f:FileUpload>
                            <f:Button runat="server" ID="btnRet" Icon="Delete" OnClick="btnRet_Click"></f:Button>
                    </Items>
                </f:FormRow>                
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                         <f:Button ID="btnArrowRefresh" Text="重置密码" Icon="ArrowRefresh" ConfirmText="确定恢复当前用户原始密码？" OnClick="btnArrowRefresh_Click"
                                runat="server" Hidden="true">
                            </f:Button>  
                        <f:ToolbarFill runat="server"></f:ToolbarFill>
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
            Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
    </form>
</body>
</html>

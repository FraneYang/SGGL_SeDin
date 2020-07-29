<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdatePasswordEdit.aspx.cs" Inherits="FineUIPro.Web.SysManage.UpdatePasswordEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改密码</title>
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
                    <f:Label ID="txtUserName" runat="server" Label="姓名">
                    </f:Label>
                </Items>
             </f:FormRow>
            <f:FormRow>
                <Items>
                     <f:Label ID="txtAccount" runat="server" Label="账号">
                    </f:Label>
                </Items> 
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtOldPassword" Label="原密码" TextMode="Password" FocusOnPageLoad="true"
                                EmptyText="输入原密码" Required="true" ShowRedStar="true" runat="server">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtNewPassword" Label="新密码" TextMode="Password"
                                EmptyText="输入新密码" Required="true" ShowRedStar="true" runat="server">
                    </f:TextBox>
                </Items>  
            </f:FormRow>
           
             <f:FormRow>
                <Items>
                  <f:TextBox ID="txtConfirmPassword" Label="确认密码" TextMode="Password"
                            EmptyText="再次输入新密码" Required="true" ShowRedStar="true" runat="server" EnableBlurEvent="true" OnBlur="txtConfirmPassword_Blur">
                    </f:TextBox>
                </Items>               
            </f:FormRow>                    
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SafetyOrganizationEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.SecuritySystem.SafetyOrganizationItemEdit" Async="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑安全组织机构明细</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                     <f:TextBox ID="txtSortIndex" runat="server" Label="序号" MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtPost" runat="server" Label="职务" Required="true" ShowRedStar="true"
                        MaxLength="50" FocusOnPageLoad="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtNames" runat="server" Label="姓名" Required="true" ShowRedStar="true"
                        MaxLength="10">
                    </f:TextBox>
                      <f:TextBox ID="txtTelephone" runat="server" Label="电话" MaxLength="20">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtMobilePhone" runat="server" Label="手机" MaxLength="20">
                    </f:TextBox>
                       <f:TextBox ID="txtEMail" runat="server" Label="邮箱" MaxLength="50">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtDuty" runat="server" Label="职责" MaxLength="2000" Height="72px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
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

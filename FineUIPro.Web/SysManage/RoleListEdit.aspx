<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleListEdit.aspx.cs" Inherits="FineUIPro.Web.SysManage.RoleListEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑角色</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" EnableTableStyle="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtRoleCode" runat="server" Label="编码" MaxLength="50" LabelWidth="90px" FocusOnPageLoad="true">
                        </f:TextBox>
                        <f:TextBox ID="txtRoleName" runat="server" Label="名称" Required="true" ShowRedStar="true" MaxLength="50"
                            AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelWidth="90px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:RadioButtonList runat="server" ID="rbIsOfficce" Label="类型" LabelWidth="90px">
                            <f:RadioItem  Text="本部角色" Value="1" />
                            <f:RadioItem  Text="项目角色" Value="0" Selected ="true"/>                            
                        </f:RadioButtonList>
                        <f:TextBox ID="txtDef" runat="server" Label="备注" MaxLength="100" LabelWidth="90px"></f:TextBox>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click" Hidden="true">
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

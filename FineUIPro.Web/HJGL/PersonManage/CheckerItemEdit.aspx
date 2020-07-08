<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckerItemEdit.aspx.cs" Inherits="FineUIPro.Web.HJGL.PersonManage.CheckerItemEdit" %>

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
                        <f:TextBox ID="txtWelderCode" runat="server" Label="无损检测工号"
                            Readonly="true" LabelWidth="140px">
                        </f:TextBox>
                        <f:TextBox ID="txtQualificationItem" runat="server" Label="合格项目" LabelWidth="140px"
                            Required="true" ShowRedStar="true" EnableBlurEvent="true">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DatePicker ID="txtCheckDate" runat="server" Label="批准日期"
                            LabelWidth="140px" Required="true" ShowRedStar="true">
                        </f:DatePicker>
                        <f:DatePicker ID="txtLimitDate" runat="server" Label="截止日期"
                            LabelWidth="140px" Required="true" ShowRedStar="true">
                        </f:DatePicker>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" ID="txtlevel" Label="等级" LabelWidth="140px"></f:TextBox>
                        <f:Label runat="server"></f:Label>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:HiddenField ID="hdWelderId" runat="server">
                        </f:HiddenField>
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
    </form>
</body>
</html>

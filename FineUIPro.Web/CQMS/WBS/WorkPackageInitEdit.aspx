<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkPackageInitEdit.aspx.cs" Inherits="FineUIPro.Web.CQMS.WBS.WorkPackageInitEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
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
                        <f:TextBox ID="txtWorkPackageCode" runat="server" Label="编号" LabelWidth="130px" Readonly="true" FocusOnPageLoad="true"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtWorkPackageName" runat="server" Label="名称" LabelWidth="130px" Required="true" ShowRedStar="true"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="drpIsChild" ShowRedStar="true" runat="server"  EmptyText="--请选择--"  AutoSelectFirstItem="false" Required="true" LabelWidth="130px" Label="是否末级" LabelAlign="Right" EnableEdit="true">
                            <f:ListItem Text="是" Value="False" />
                            <f:ListItem Text="否" Value="True" />
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" Text="" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
</body>
</html>

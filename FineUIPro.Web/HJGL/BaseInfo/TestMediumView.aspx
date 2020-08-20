<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestMediumView.aspx.cs" Inherits="FineUIPro.Web.HJGL.BaseInfo.TestMediumView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>查看试验介质</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtMediumCode" runat="server" Label="介质代号"
                         Readonly="true" LabelWidth="160px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtMediumName" runat="server" Label="介质名称"
                        Readonly="true" LabelWidth="160px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpTestType" runat="server" Label="试验介质类型" LabelWidth="180px">
                         <f:ListItem  Value="1" Text="试压介质"/>
                         <f:ListItem  Value="2" Text="泄漏性试验介质"/>
                         <f:ListItem  Value="3" Text="真空试验介质"/>
                         <f:ListItem  Value="4" Text="吹扫介质"/>
                         <f:ListItem  Value="5" Text="清洗介质"/>
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtRemark" runat="server" Label="备注"  LabelWidth="160px"
                        Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
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

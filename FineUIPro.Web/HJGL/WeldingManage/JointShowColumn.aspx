<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JointShowColumn.aspx.cs" Inherits="FineUIPro.Web.HJGL.WeldingManage.JointShowColumn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择显示列</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
            runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                    <Items>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" Text="提交" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:CheckBox ID="ckAll" runat="server" Label="全选" LabelWidth="55px" AutoPostBack="true"
                            OnCheckedChanged="ckAll_CheckedChanged">
                        </f:CheckBox>
                    </Items>
                </f:FormRow>
                <f:FormRow ColumnWidths="15% 85%">
                    <Items>
                        <f:Label ID="Label1" runat="server" Text="显示列：" LabelAlign="Right">
                        </f:Label>
                        <f:CheckBoxList ID="cblColumn" runat="server" DataTextField="Text" DataValueField="Value"
                            AutoColumnWidth="true" ColumnNumber="4" ColumnVertical="true">
                        </f:CheckBoxList>
                    </Items>
                </f:FormRow>
            </Rows>
        </f:Form>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SafetyMeasuresEdit.aspx.cs" Inherits="FineUIPro.Web.BaseInfo.SafetyMeasuresEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑安全措施</title>
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
                    <f:DropDownList ID="drpLicenseType" runat="server" Label="作业类型" EnableEdit="true" LabelWidth="100px">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                     <f:NumberBox ID="txtSortIndex" runat="server" Label="编号"  NoDecimal="false" NoNegative="false"
                         FocusOnPageLoad="true" LabelWidth="100px">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                     <f:TextArea ID="txtSafetyMeasures" runat="server" Label="名称" Required="true" ShowRedStar="true"  MaxLength="2000"
                        AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelWidth="100px" Height="150px">
                    </f:TextArea>
                </Items>
            </f:FormRow>           
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1" Hidden="true"
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

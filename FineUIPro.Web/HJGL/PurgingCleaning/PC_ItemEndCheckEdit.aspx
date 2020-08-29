<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PC_ItemEndCheckEdit.aspx.cs" Inherits="FineUIPro.Web.HJGL.PurgingCleaning.PC_ItemEndCheckEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow runat="server">
                    <Items>
                        <f:DropDownList ID="drpType" runat="server" Label="检查项" LabelWidth="110px" EmptyText="-请选择-" LabelAlign="Right" AutoSelectFirstItem="false" EnableEdit="true" Required="true" >
                            <f:ListItem Text="A项检查" Value="1" />
                            <f:ListItem Text="B项检查" Value="2" />
                        </f:DropDownList>
                        <f:TextBox ID="txtRemark" runat="server" Label="检查内容" MaxLength="70" LabelWidth="110px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow runat="server">
                    <Items>
                        <f:DropDownList ID="drpCheckMan" runat="server" Label="检察人" LabelWidth="110px" LabelAlign="Right" Required="true" ShowRedStar="true" AutoSelectFirstItem="false" EnableEdit="true"  EmptyText="-请选择-">
                        </f:DropDownList>
                        <f:DatePicker ID="txtCheckDate" Required="true" runat="server" Label="检查日期">
                        </f:DatePicker>
                    </Items>
                </f:FormRow>
                <f:FormRow runat="server">
                    <Items>
                         <f:DropDownList ID="drpDealMan" runat="server" Label="处理人" LabelWidth="110px" LabelAlign="Right" Required="true" ShowRedStar="true" AutoSelectFirstItem="false" EnableEdit="true"  EmptyText="-请选择-">
                        </f:DropDownList>
                        <f:TextBox runat="server" ID="txtOpinion" Label="处理意见" Hidden="true"></f:TextBox>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:HiddenField ID="hdCheckerId" runat="server">
                        </f:HiddenField>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1" OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false"
                            runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
</body>
</html>

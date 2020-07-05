<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReadWriteCard.aspx.cs" Inherits="FineUIPro.Web.HSSE.SitePerson.ReadWriteCard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>发卡</title>
     <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />   
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCardNo" runat="server" Label="所发卡号" MaxLength="50" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                    </f:TextBox>
                    <%--<f:TextBox ID="TextBox2" runat="server" OnBlur="txtblur()">
                    </f:TextBox>
                    <f:Button ID="imgbtnFocus" runat="server" Hidden="true" 
                        OnClick="imgbtnFocus_Click"></f:Button>--%>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtPersonName" runat="server" Label="人员姓名" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtUnitName" runat="server" Label="所属单位" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSendCard" Icon="SystemSave" runat="server" ToolTip="发卡" ValidateForms="SimpleForm1" OnClick="btnSendCard_Click">
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

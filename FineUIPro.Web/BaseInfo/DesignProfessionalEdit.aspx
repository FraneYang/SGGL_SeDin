<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DesignProfessionalEdit.aspx.cs" Inherits="FineUIPro.Web.BaseInfo.DesignProfessionalEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>设计专业</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow runat="server">
                    <Items>
                        <f:TextBox ID="txtDesignProfessionalCode" runat="server" Label="专业编号"
                            Required="true" MaxLength="70" ShowRedStar="true" LabelWidth="110px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow runat="server">
                    <Items>
                        <f:TextBox ID="txtProfessionalName" runat="server" Label="专业名称"
                            Required="true" MaxLength="70" ShowRedStar="true" LabelWidth="110px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:NumberBox runat="server" ID="txtSortIndex" Label="排序" LabelWidth="110px" NoDecimal="true" NoNegative="true"></f:NumberBox>
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

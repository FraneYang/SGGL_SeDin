<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QualityQuestionTypeView.aspx.cs" Inherits="FineUIPro.Web.BaseInfo.QualityQuestionTypeView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>质量问题类别</title>
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
                        <f:TextBox ID="txtQualityQuestionType" runat="server" Label="质量问题类别" Readonly="true"
                            Required="true" MaxLength="70" ShowRedStar="true" LabelWidth="110px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtSortIndex" runat="server" Label="排序"
                            LabelWidth="110px" Readonly="true">
                        </f:TextBox>
                   </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:HiddenField ID="hdCheckerId" runat="server">
                        </f:HiddenField>
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

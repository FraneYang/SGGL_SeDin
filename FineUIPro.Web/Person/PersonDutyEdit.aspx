<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonDutyEdit.aspx.cs" Inherits="FineUIPro.Web.Person.PersonDutyEdit"  ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑人员总结</title>
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
                        <f:DropDownList ID="drpSEDINUser" runat="server" Label="员工" EnableEdit="true" ForceSelection="false"
                            Required="true" ShowRedStar="true" LabelWidth="110px" AutoPostBack="true"  Readonly="true">
                        </f:DropDownList>
                        <f:DropDownList ID="drpWorkPost" runat="server" Label="岗位"  Readonly="true">
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:HtmlEditor runat="server" Label="岗位模板" ID="txtTemplate" ShowLabel="false"
                            Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="600" LabelAlign="Right">
                        </f:HtmlEditor>
                    </Items>
                </f:FormRow>
                <f:FormRow ID="IsAgree" Hidden="true">
                    <Items>
                        <f:RadioButtonList runat="server" ID="rdbIsAgree" Label="是否同意" ShowRedStar="true" LabelWidth="110px" AutoPostBack="true" OnSelectedIndexChanged="rdbIsAgree_SelectedIndexChanged">
                            <f:RadioItem Text="同意" Value="true" Selected="true" />
                            <f:RadioItem Text="不同意" Value="false" />
                        </f:RadioButtonList>
                    </Items>
                </f:FormRow>
                <f:FormRow ID="BackMan">
                    <Items>
                        <f:DropDownList runat="server" ID="drpHandleMan" Label="办理人员" LabelWidth="110px" ShowRedStar="true"></f:DropDownList>
                        <f:Label runat="server"></f:Label>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                            OnClick="btnSubmit_Click">
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
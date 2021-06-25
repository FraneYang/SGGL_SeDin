<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachUrl3.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.AttachUrl3" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑附件2</title>
    <style>
        .widthBlod {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:Panel ID="Panel4" IsFluid="true" CssClass="blockpanel" BodyPadding="10" Layout="VBox" MaxHeight="550" BoxConfigChildMargin="0 0 5 0" AutoScroll="true"
            EnableCollapse="true" Title="附件3    工程价格清单" runat="server">
            <Items>
                <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" Title="" AutoScroll="true"
                    runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                    <Items>
                        <f:FormRow>
                            <Items>
                                <f:Label ID="Label1" runat="server" Text="附件3    工程价格清单" CssClass="widthBlod"></f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:HtmlEditor runat="server" Label="" ID="txtAttachUrlContent" ShowLabel="false"
                                    Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="500px" LabelAlign="Right" Text="">
                                </f:HtmlEditor>
                            </Items>
                        </f:FormRow>

                    </Items>
                </f:Form>

            </Items>
            
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:HiddenField ID="hdAttachUrlItemId" runat="server" ></f:HiddenField>
                        <f:ToolbarFill runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" Text ="保存" Size="Medium" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" Text="关闭" Size="Medium" runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Panel>

    </form>
</body>
</html>

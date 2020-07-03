<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignManageEdit.aspx.cs" Inherits="FineUIPro.Web.HSSE.Resources.SignManageEdit"   ValidateRequest="false"%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑标牌</title>
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
                    <f:TextBox ID="txtSignCode" runat="server" Label="标牌编号"  MaxLength="50" FocusOnPageLoad="true">
                    </f:TextBox>
                    <f:TextBox ID="txtSignName" runat="server" Label="标牌名称" Required="true" ShowRedStar="true"  MaxLength="50">
                    </f:TextBox>
                    <f:DropDownList runat="server" ID="drpSignType" Label="标牌类型" EnableEdit="true" Required="true" ShowRedStar="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSignLen" runat="server" Label="标牌长度"  MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtSignWide" runat="server" Label="标牌宽度" MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtSignHigh" runat="server" Label="标牌高度" MaxLength="50">
                    </f:TextBox>
                </Items>
            </f:FormRow> 
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSignThick" runat="server" Label="标牌厚度"  MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtMaterial" runat="server" Label="标牌材质" MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtSignArea" runat="server" Label="适合位置" MaxLength="200">
                    </f:TextBox>
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                    <f:HtmlEditor runat="server" Label="标牌内容" ID="txtSignImage" ShowLabel="false"
                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="300" LabelAlign="Right">
                    </f:HtmlEditor>
                </Items>
            </f:FormRow> 
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1" MarginLeft="10px">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server"> </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
     <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>

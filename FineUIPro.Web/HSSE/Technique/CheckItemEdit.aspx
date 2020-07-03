<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckItemEdit.aspx.cs" Inherits="FineUIPro.Web.HSSE.Technique.CheckItemEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCheckItemName" runat="server" Label="名称" LabelWidth="90px" Required="true" ShowRedStar="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtMapCode" runat="server" Label="编码" LabelWidth="90px" MaxLength="2"  MaxLengthMessage="检查项目编码不能超过2位！"  Required="true" ShowRedStar="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>      
            <f:FormRow >
                <Items>
                     <f:NumberBox ID="txtSortIndex" runat="server" Label="排列序号" LabelWidth="90px" Required="true" ShowRedStar="true"></f:NumberBox>
                </Items>
                <Items>
                    <f:CheckBox  ID="chkIsEndLevel" MarginLeft="40px" runat="server" Text="末级" LabelWidth="90px">
                    </f:CheckBox>
                </Items>
            </f:FormRow>    
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
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

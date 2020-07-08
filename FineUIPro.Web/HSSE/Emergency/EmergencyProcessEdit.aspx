<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmergencyProcessEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Emergency.EmergencyProcessEdit" ValidateRequest="false" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑应急流程</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="应急流程" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtProcessSteps" runat="server" Label="步骤" LabelAlign="Right"
                        MaxLength="50" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtProcessName" runat="server" Label="名称" Required="true" ShowRedStar="true"
                        LabelAlign="Right" MaxLength="50" FocusOnPageLoad="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtStepOperator" Label="操作者" MaxLength="50"></f:TextBox>
                </Items>
            </f:FormRow>                        
            <f:FormRow>
                <Items>
                          <f:TextArea runat="server" ID="txtRemark" Label="提示内容" MaxLength="800" Height="200px"></f:TextArea>       
                </Items>
            </f:FormRow>         
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>                   
                    <f:ToolbarFill runat="server"> </f:ToolbarFill>
                     <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click" Hidden="true">
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

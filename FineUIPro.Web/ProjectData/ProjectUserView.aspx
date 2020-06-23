<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectUserView.aspx.cs" Inherits="FineUIPro.Web.ProjectData.ProjectUserView" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <rows>
            <f:FormRow>
                <Items>                 
                   <f:Label ID="lbProjectName" runat="server" Label="项目名称"></f:Label>
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>                 
                   <f:Label ID="lbUnitName" runat="server" Label="单位名称"></f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>         
                    <f:Label ID="lbUserCode" runat="server" Label="用户编码"></f:Label>        
                   <f:Label ID="lbUserName" runat="server" Label="用户名称"></f:Label>
                </Items>
            </f:FormRow>            
            <f:FormRow>
                <Items>
                    <f:TextBox ID="drpRole" runat="server" Label="角色" Readonly="true">
                    </f:TextBox>
                     <f:TextBox ID="drpIsPost" runat="server" Label="在岗" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>                
        </rows>
        <toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>                 
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </toolbars>
    </f:Form>
    </form>
</body>
</html>

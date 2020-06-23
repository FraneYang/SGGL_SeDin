<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectUserSave.aspx.cs" Inherits="FineUIPro.Web.ProjectData.ProjectUserSave" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
     <style type="text/css">      
        .label
        {
            color: Red;
            font-size: small;
        }
    </style>
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
                    <f:DropDownList ID="drpRole" runat="server" Label="角色" EnableEdit="true" ForceSelection="false"
                        Required="true" ShowRedStar="true" EnableMultiSelect="true" MaxLength="500" EnableCheckBoxSelect="true">
                    </f:DropDownList>
                    <f:DropDownList ID="drpIsPost" runat="server" Label="在岗" EnableEdit="true" ForceSelection="false"
                        Required="true" ShowRedStar="true">
                    </f:DropDownList> 
                </Items>
            </f:FormRow> 
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpWorkPost" runat="server" Label="项目岗位" EnableEdit="true" ForceSelection="false">
                    </f:DropDownList>
                    
                    <f:Label ID="Temp" runat="server" Margin="0 0 0 50"  Text="说明：项目岗位通过身份证号码与项目现场人员关联。" CssClass="label" ></f:Label>
                </Items>
            </f:FormRow>                
        </rows>
        <toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </toolbars>
    </f:Form>
    </form>
</body>
</html>

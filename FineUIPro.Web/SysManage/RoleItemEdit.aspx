<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleItemEdit.aspx.cs" Inherits="FineUIPro.Web.SysManage.RoleItemEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
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
                        <f:TextBox ID="txtProjectName" runat="server" Label="项目名称" MaxLength="200"  >
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                         <f:DropDownList ID="drpRole" runat="server" Label="角色" EnableEdit="true" ForceSelection="false"
                        Required="true" ShowRedStar="true" EnableMultiSelect="true" MaxLength="500" EnableCheckBoxSelect="true">
                    </f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                         <f:DatePicker runat="server" Label="入场时间" ID="txtIntoDate"></f:DatePicker>
                     <f:DatePicker runat="server" Label="出场时间" ID="txtOutDate"></f:DatePicker>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1" 
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>          
        <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
    </form>
</body>
</html>

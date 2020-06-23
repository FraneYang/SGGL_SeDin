<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamGroupEdit.aspx.cs"
    Inherits="FineUIPro.Web.ProjectData.TeamGroupEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑班组信息</title>
    <base target="_self" />
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
                    <f:TextBox ID="txtTeamGroupCode" runat="server" Label="班组编号" Required="true" MaxLength="50"
                        ShowRedStar="true" FocusOnPageLoad="true" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                    </f:TextBox>
                       <f:TextBox ID="txtTeamGroupName" runat="server" Label="班组名称" Required="true" MaxLength="50"
                        ShowRedStar="true" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                    </f:TextBox>
                </Items>
            </f:FormRow>           
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpUnitId" runat="server" Label="单位名称"  EnableEdit="true" ShowRedStar="true" Required="true"
                        OnSelectedIndexChanged="drpUnitId_SelectedIndexChanged" AutoPostBack="true">
                    </f:DropDownList>
                     <f:DropDownList ID="drpGroupLeader" runat="server" Label="班组长" EnableEdit="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtRemark" runat="server" Label="备注" MaxLength="500">
                    </f:TextArea>
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

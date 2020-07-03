<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckerManageView.aspx.cs" Inherits="FineUIPro.Web.HJGL.PersonManage.CheckerManageView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpUnitId" runat="server" Label="所属单位" 
                                        AutoPostBack="true"  LabelWidth="120px" Required="true" ShowRedStar="true">
                                    </f:DropDownList>
                    <f:TextBox ID="txtCheckerCode" runat="server" Label="检测工编号"
                        Required="true" MaxLength="50" ShowRedStar="true" FocusOnPageLoad="true" LabelWidth="120px">
                    </f:TextBox>
                    
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCheckerName" runat="server" Label="检测工姓名"
                        Required="true" MaxLength="50" ShowRedStar="true" LabelWidth="120px">
                    </f:TextBox>
                    <f:DatePicker ID="txtBirthday" runat="server" Label="出生日期"
                                        LabelWidth="120px">
                                    </f:DatePicker>
                    
                    
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtIdentityCard" runat="server" Label="身份证号"
                                        MaxLength="18" LabelWidth="120px" Required="true" ShowRedStar="true">
                                    </f:TextBox>
                    <f:RadioButtonList ID="rblSex" runat="server" Label="性别" LabelWidth="120px">
                                        <f:RadioItem Selected="true" Value="1" Text="男" />
                                        <f:RadioItem Value="2" Text="女" />
                                    </f:RadioButtonList>
                   
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
             <f:CheckBox ID="cbIsOnDuty" runat="server" Label="是否在岗"
                                        LabelWidth="120px">
                                    </f:CheckBox></Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭"
                        runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
       
    </form>
</body>
</html>

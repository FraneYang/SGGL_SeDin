<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckNoticeEdit.aspx.cs" Inherits="FineUIPro.Web.OfficeCheck.Check.CheckNoticeEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>编辑检查通知单</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <script src="../res/js/jquery-3.3.1.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>        
                     <f:TextBox ID="txtCheckTeamLeaderName" runat="server" Label="检查组长" LabelWidth="120px"
                        ShowRedStar="true" Required="true" AutoPostBack="true" OnTextChanged="txtCheckTeamLeaderName_TextChanged">
                    </f:TextBox>
                     <f:DropDownList ID="drpSex" runat="server" Label="性别"  LabelWidth="120px" >
                        <f:ListItem Value="男" Text="男" Selected="true"/>
                        <f:ListItem Value="女" Text="女"/>
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items> 
                    <f:DropDownList ID="drpUnit" runat="server" Label="检查组长单位" 
                       EnableEdit="true"  Required="true" ShowRedStar="true"  LabelWidth="120px" >
                    </f:DropDownList>
                   
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items> 
                    <f:DropDownList ID="drpSubjectProject" runat="server" Label="受检项目" 
                       EnableEdit="true"  Required="true" ShowRedStar="true"  LabelWidth="120px" 
                       AutoPostBack="true" OnSelectedIndexChanged="drpSubjectProject_OnSelectedIndexChanged">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
              <%--<f:FormRow>
                <Items>
                    <f:TextBox ID="txtSubjectObject" runat="server" MarginLeft="120px" Height="40px"  EmptyText="项目部名称" MaxLength="500">
                    </f:TextBox>
                </Items>
            </f:FormRow>--%>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtSubjectUnitAdd" runat="server" Label="受检项目地址"  LabelWidth="120px" Height="40px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSubjectUnitMan" runat="server" Label="受检项目负责人"  LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtSubjectUnitTel" runat="server" Label="受检项目负责人电话"  LabelWidth="150px">
                    </f:TextBox> 
                </Items>
            </f:FormRow>          
            <f:FormRow>
                <Items>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Required="true" ShowRedStar="true"
                         Label="检查日期" ID="txtCheckStartTime" LabelWidth="120px">
                    </f:DatePicker>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Required="true" ShowRedStar="true"
                        Label="至" ID="txtCheckEndTime"  LabelWidth="50px">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:HiddenField runat="server" ID="hdUserId"></f:HiddenField>
                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" Text="保存数据" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click" Hidden="true">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
      <%--<script src="../res/js/hook.js" type="text/javascript"></script>--%>
</body>
</html>

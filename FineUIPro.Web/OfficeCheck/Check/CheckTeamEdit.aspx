<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckTeamEdit.aspx.cs" Inherits="FineUIPro.Web.OfficeCheck.Check.CheckTeamEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>检查工作组维护</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <script src="../res/js/jquery-3.3.1.min.js" type="text/javascript"></script>
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
             <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtSortIndex" runat="server" Label="序号" LabelWidth="120px"  MinValue="1" NoDecimal="true" NoNegative="true">
                    </f:NumberBox>
                    <f:TextBox ID="txtUserName" runat="server" Label="组成员" LabelWidth="100px" FocusOnPageLoad="true"
                        ShowRedStar="true" Required="true" AutoPostBack="true" OnTextChanged="txtUserName_TextChanged">
                    </f:TextBox>
                </Items>
            </f:FormRow>
           <f:FormRow>
                <Items> 
                    <f:DropDownList ID="drpUnit" runat="server" Label="所在单位" 
                       EnableEdit="true"  Required="true" ShowRedStar="true"  LabelWidth="120px" >
                    </f:DropDownList>
                    <f:DropDownList ID="drpSex" runat="server" Label="性别"  LabelWidth="100px" >
                        <f:ListItem Value="男" Text="男" Selected="true"/>
                        <f:ListItem Value="女" Text="女"/>
                    </f:DropDownList>
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                    <f:TextBox ID="txtPostName" runat="server" Label="所在单位职务" LabelWidth="120px"  MaxLength="200">
                    </f:TextBox>
                    <f:TextBox ID="txtWorkTitle" runat="server" Label="职称" LabelWidth="100px"  MaxLength="200">
                    </f:TextBox>
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCheckPostName" runat="server" Label="评价小组职务" LabelWidth="120px"  MaxLength="200">
                    </f:TextBox>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="评价日期" ID="txtCheckDate"
                        EmptyText="请选择评价日期">
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
                        OnClick="btnSave_Click" >
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" Text="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
      <%--<script src="../res/js/hook.js" type="text/javascript"></script>--%>
</body>
</html>

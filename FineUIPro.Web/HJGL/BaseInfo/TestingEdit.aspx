<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestingEdit.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.BaseInfo.TestingEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑探伤类型</title>
    <base target="_self" />
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
                    <f:TextBox ID="txtDetectionTypeCode" runat="server" Label="检测方法代号"
                        Required="true" MaxLength="50" ShowRedStar="true" FocusOnPageLoad="true" LabelWidth="150px">
                    </f:TextBox>
                    <f:TextBox ID="txtDetectionTypeName" runat="server" Label="检测方法名称"
                        Required="true" MaxLength="50" ShowRedStar="true" LabelWidth="150px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                   <%-- <f:DropDownList ID="drpSysType" Label="系统类型" runat="server"
                        LabelAlign="right" ShowRedStar="true" Required="true" LabelWidth="150px">
                    </f:DropDownList>--%>
                    <f:NumberBox ID="txtSecuritySpace" Label="安全距离" runat="server"
                        LabelAlign="right" NoNegative="true" DecimalPrecision="4" LabelWidth="150px">
                    </f:NumberBox>   
                    <f:TextBox ID="txtInjuryDegree" runat="server" Label="伤害程度"
                        MaxLength="100" LabelWidth="150px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtRemark" runat="server" Label="备注" MaxLength="500" LabelWidth="150px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HazardListEdit.aspx.cs"
    Async="true" Inherits="FineUIPro.Web.HSSE.Technique.HazardListEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑危险源清单</title>
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
                    <f:TextBox ID="txtHazardCode" runat="server" Label="危险源代码" Required="true" ShowRedStar="true"
                        MaxLength="50" LabelWidth="120px" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                    </f:TextBox>
                    <f:TextBox ID="txtHazardItems" runat="server" Label="危险因素明细" Required="true" ShowRedStar="true"
                        MaxLength="150" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtDefectsType" runat="server" Label="缺陷类型" LabelWidth="120px" MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtMayLeadAccidents" runat="server" Label="可能导致的事故" LabelWidth="120px"
                        MaxLength="100">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="ddlHelperMethod" runat="server" Label="辅助方法" LabelWidth="120px">
                    </f:DropDownList>
                    <f:TextBox ID="txtHazardJudge_L" runat="server" Label="作业条件危险性评价(L)" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtHazardJudge_E" runat="server" Label="作业条件危险性评价(E)" LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtHazardJudge_C" runat="server" Label="作业条件危险性评价(C)" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtHazardJudge_D" runat="server" Label="作业条件危险性评价(D)" LabelWidth="120px">
                    </f:TextBox>
                    <f:DropDownList ID="ddlHazardLevel" runat="server" Label="危险级别" LabelWidth="120px">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtControlMeasures" runat="server" Label="控制措施" LabelWidth="120px"
                        Height="70px" MaxLength="200">
                    </f:TextArea>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        Hidden="true" OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                    <f:HiddenField ID="hdCompileMan" runat="server">
                    </f:HiddenField>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>

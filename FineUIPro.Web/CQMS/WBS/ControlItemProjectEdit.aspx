<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlItemProjectEdit.aspx.cs" Inherits="FineUIPro.Web.CQMS.WBS.ControlItemProjectEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
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
                        <f:TextBox ID="txtControlItemCode" runat="server" Label="编号" LabelWidth="150px" Readonly="true" FocusOnPageLoad="true"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtControlItemContent" runat="server" Label="工作包" LabelWidth="150px" Required="true" ShowRedStar="true"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="drpControlPoint" ShowRedStar="true" runat="server" Required="true" LabelWidth="150px" Label="控制点等级" LabelAlign="Right" EnableEdit="true">
                            <f:ListItem Text="A" Value="A" />
                            <f:ListItem Text="AR" Value="AR" />
                            <f:ListItem Text="B" Value="B" />
                            <f:ListItem Text="BR" Value="BR" />
                            <f:ListItem Text="C" Value="C" />
                            <f:ListItem Text="CR" Value="CR" />
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtControlItemDef" runat="server" Label="控制点内容描述" LabelWidth="150px" Height="80px" Required="true" ShowRedStar="true"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:NumberBox runat="server" ID="txtWeights" Label="权重%" LabelWidth="150px" NoDecimal="false" NoNegative="true"></f:NumberBox>
                   </Items>
                </f:FormRow>
                <f:FormRow runat="server" ID="tr5">
                    <Items>
                        <f:TextArea ID="txtHGForms" runat="server" Label="对应的化工资料表格" LabelWidth="150px" Height="80px" ></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow runat="server" ID="tr6">
                    <Items>
                        <f:TextArea ID="txtSHForms" runat="server" Label="对应的石化资料表格" LabelWidth="150px" Height="80px" ></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtStandard" runat="server" Label="质量验收规范" LabelWidth="150px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtClauseNo" runat="server" Label="条款号" LabelWidth="150px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:NumberBox runat="server" ID="txtCheckNum" Label="检查次数" LabelWidth="150px" NoDecimal="true" NoNegative="true"></f:NumberBox>
                   </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" Text="" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotProessReportEdit.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.HotProcessHard.HotProessReportEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑热处理报告</title>
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
                    <f:TextBox ID="txtWeldJointCode" runat="server" Label="总焊口量"
                        LabelWidth="150px" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:NumberBox ID="txtPointCount" runat="server" Label="测温点编号"
                        LabelWidth="150px" LabelAlign="Right" NoDecimal="true" NoNegative="true">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtRequiredT" runat="server" Label="热处理温度℃(要求)"
                        MaxLength="20" LabelWidth="150px" LabelAlign="Right">
                    </f:TextBox>
                    <f:TextBox ID="txtActualT" runat="server" Label="热处理温度℃(实际)" MaxLength="20"
                        LabelWidth="150px" LabelAlign="Right">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtRequestTime" runat="server" Label="恒温时间h（要求）"
                        MaxLength="20" LabelWidth="150px" LabelAlign="Right">
                    </f:TextBox>
                    <f:TextBox ID="txtActualTime" runat="server" Label="恒温时间h（实际）"
                        MaxLength="20" LabelWidth="150px" LabelAlign="Right">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtRecordChartNo" runat="server" Label="记录曲线图编号"
                        MaxLength="20" LabelWidth="150px" LabelAlign="Right">
                    </f:TextBox>
                    <f:HiddenField ID="hdWeldJointId" runat="server">
                    </f:HiddenField>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:LinkButton ID="UploadAttach" runat="server" Label="附件" Text="上传和查看" OnClick="btnAttachUrl_Click"
                        LabelAlign="Right" LabelWidth="150px">
                    </f:LinkButton>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click" ToolTip="保存">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭"
                        runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="Window1" Title="文件上传" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="680px"
        Height="480px">
    </f:Window>
    </form>
</body>
</html>

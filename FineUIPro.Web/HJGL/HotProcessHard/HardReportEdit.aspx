<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HardReportEdit.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.HotProcessHard.HardReportEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑硬度报告</title>
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
                    <f:Label runat="server" ID="txtPipelineCode" Label="管线号"
                        LabelWidth="110px">
                    </f:Label>
                    <f:Label runat="server" ID="txtWeldJointCode" Label="总焊口量">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtHardReportNo" runat="server" Label="报告编号"
                        Required="true" MaxLength="50" ShowRedStar="true" FocusOnPageLoad="true" LabelWidth="110px">
                    </f:TextBox>
                    <f:TextBox ID="txtTestingPointNo" runat="server" Label="试验部位"
                        >
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="nbHardNessValue1" NoDecimal="true" NoNegative="false" MinValue="0"
                        Label="硬度值1" runat="server" LabelWidth="110px">
                    </f:NumberBox>
                    <f:NumberBox ID="nbHardNessValue2" NoDecimal="true" NoNegative="false" MinValue="0"
                        LabelWidth="110px" Label="硬度值2" runat="server">
                    </f:NumberBox>
                    <f:NumberBox ID="nbHardNessValue3" NoDecimal="true" NoNegative="false" MinValue="0"
                        LabelWidth="110px" Label="硬度值3" runat="server">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtRemark" runat="server" Label="备注" MaxLength="500"
                        LabelWidth="110px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:LinkButton ID="UploadAttach" runat="server" Label="附件" Text="上传和查看" OnClick="btnAttachUrl_Click"
                        LabelAlign="Right" LabelWidth="110px">
                    </f:LinkButton>
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
    <f:Window ID="Window1" Title="文件上传" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="680px"
        Height="480px">
    </f:Window>
    </form>
</body>
</html>

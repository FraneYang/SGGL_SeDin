<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonQualityEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.QualityAudit.PersonQualityEdit" ValidateRequest="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑人员资质</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="人员资质" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtUnitCode" runat="server" Label="单位代码" LabelAlign="Right" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtUnitName" runat="server" Label="单位名称" LabelAlign="Right" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                   <f:TextBox ID="txtPersonName" runat="server" Label="姓名" LabelAlign="Right" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtWorkPostName" runat="server" Label="岗位" Readonly="true" LabelAlign="Right"
                        LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>           
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpCertificate" runat="server" Label="特岗证书"
                     LabelAlign="Right" LabelWidth="120px" EnableEdit="true" Required="true" ShowRedStar="true" >
                     </f:DropDownList>
                    <f:TextBox ID="txtCertificateNo" runat="server" Label="证书编号" LabelAlign="Right"
                        MaxLength="50" Required="true" ShowRedStar="true" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtGrade" runat="server" Label="操作类别" LabelAlign="Right" MaxLength="50"
                        LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtSendUnit" runat="server" Label="发证单位" LabelAlign="Right" MaxLength="50"
                        Required="true" ShowRedStar="true" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DatePicker ID="txtSendDate" Label="发证时间" runat="server" LabelAlign="Right" EnableEdit="true"
                        Required="true" ShowRedStar="true" LabelWidth="120px">
                    </f:DatePicker>
                    <f:DatePicker ID="txtLimitDate" Label="有效期至" runat="server" LabelAlign="Right" EnableEdit="true"
                        Required="true" ShowRedStar="true" LabelWidth="120px">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpAuditor" runat="server" Label="审核人" LabelAlign="Right" EnableEdit="true"
                        LabelWidth="120px">
                    </f:DropDownList>
                    <f:DatePicker ID="txtAuditDate" Label="审核日期" runat="server" LabelAlign="Right" EnableEdit="true"
                        LabelWidth="120px">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DatePicker ID="txtLateCheckDate" Label="最近复查日期" runat="server" LabelAlign="Right"
                        EnableEdit="true" LabelWidth="120px">
                    </f:DatePicker>
                    <f:Label ID="Label1" runat="server">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtRemark" runat="server" Label="备注" LabelAlign="Right" MaxLength="500"
                        LabelWidth="120px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnAttachUrl" Text="证书扫描件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>

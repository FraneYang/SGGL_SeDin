<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EquipmentQualityEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.QualityAudit.EquipmentQualityEdit" ValidateRequest="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑特殊机具设备资质</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="特殊机具设备资质" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpUnitId" runat="server" Label="单位名称" LabelAlign="Right" EnableEdit="true"
                        Required="true" ShowRedStar="true" LabelWidth="110px">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                     <f:TextBox ID="txtEquipmentQualityCode" runat="server" Label="编号" LabelAlign="Right"
                        MaxLength="50" Readonly="true" LabelWidth="110px">
                    </f:TextBox>
                    <f:DropDownList ID="drpSpecialEquipmentId" runat="server" Label="类型" LabelAlign="Right" LabelWidth="110px"
                        EnableEdit="true" Required="true" ShowRedStar="true"  FocusOnPageLoad="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtEquipmentQualityName" runat="server" Label="设备名称" LabelAlign="Right" LabelWidth="110px"
                        MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtSizeModel" runat="server" Label="规格型号" LabelAlign="Right" MaxLength="50" LabelWidth="110px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtFactoryCode" runat="server" Label="出厂编号" LabelAlign="Right"  LabelWidth="110px"
                        MaxLength="50" ShowRedStar="true" Required="true">
                    </f:TextBox>
                    <f:TextBox ID="txtCertificateCode" runat="server" Label="合格证编号" LabelAlign="Right"
                        MaxLength="50" LabelWidth="110px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DatePicker ID="txtCheckDate" Label="最近检验时间" runat="server" 
                        LabelAlign="Right" EnableEdit="true" LabelWidth="110px">
                    </f:DatePicker>
                    <f:DatePicker ID="txtLimitDate" Label="有效期至" runat="server" LabelAlign="Right" EnableEdit="true"
                        Required="true" ShowRedStar="true" LabelWidth="110px">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DatePicker ID="txtInDate" Label="入场时间" runat="server" LabelWidth="110px" 
                        LabelAlign="Right" EnableEdit="true">
                    </f:DatePicker>
                    <f:DatePicker ID="txtOutDate" Label="出场时间" runat="server" 
                        LabelAlign="Right" EnableEdit="true" LabelWidth="110px">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtApprovalPerson" runat="server" Label="审批人" LabelWidth="110px"
                        LabelAlign="Right" MaxLength="20">
                    </f:TextBox>
                    <f:TextBox ID="txtCarNum" runat="server" Label="车牌号" LabelAlign="Right" MaxLength="50" LabelWidth="110px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtRemark" runat="server" Label="备注" LabelAlign="Right" MaxLength="500" LabelWidth="110px">
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
                    <f:Button runat="server" ID="btnQR" OnClick="btnQR_Click" Text="二维码生成" MarginLeft="10px">
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
    <f:Window ID="Window1" runat="server" Hidden="true" IsModal="false" Target="Parent"
        EnableMaximize="true" EnableResize="false" Title="弹出框" CloseAction="HidePostBack"
        EnableIFrame="true">
    </f:Window>
    </form>
</body>
</html>

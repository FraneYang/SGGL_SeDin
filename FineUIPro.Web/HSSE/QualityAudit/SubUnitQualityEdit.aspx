<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubUnitQualityEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.QualityAudit.SubUnitQualityEdit" ValidateRequest="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑分包商资质</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="分包商资质" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" EnableTableStyle="false">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:Label ID="txtUnitName" runat="server" Label="分包单位" LabelAlign="Right"  LabelWidth="120px">
                    </f:Label>
                    <f:Label ID="txtTelephone" runat="server" Label="联系电话" LabelAlign="Right" >
                    </f:Label>
                    <f:Label ID="txtEmail" runat="server" Label="电子邮箱" LabelAlign="Right" >
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow >
                <Items>                   
                    <f:TextBox ID="txtSubUnitQualityName" runat="server" Label="资质" LabelAlign="Right" LabelWidth="120px"
                        MaxLength="100">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="25% 25% 30% 20%">
                <Items>
                    <f:TextBox ID="txtBusinessLicense" runat="server" Label="营业执照" LabelAlign="Right" LabelWidth="120px"
                        MaxLength="50">
                    </f:TextBox>
                    <f:DatePicker ID="txtBL_EnableDate" runat="server" Label="营业执照有效期" LabelAlign="Right"
                        EnableEdit="true" LabelWidth="120px">
                    </f:DatePicker>
                    <f:FileUpload runat="server" ID="btnBL_ScanUrl" EmptyText="请选择附件" OnFileSelected="btnBL_ScanUrl_Click"
                        AutoPostBack="true" Label="营业执照扫描件" LabelWidth="120px">
                    </f:FileUpload>
                    <f:ContentPanel ID="cpBL" runat="server" ShowHeader="false" ShowBorder="false" Title="营业执照扫描件">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divBL_ScanUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="25% 25% 30% 20%" runat="server" Hidden="true">
                <Items>
                    <f:TextBox ID="txtOrganCode" runat="server" Label="机构代码" LabelAlign="Right" MaxLength="50" LabelWidth="120px">
                    </f:TextBox>
                    <f:DatePicker ID="txtO_EnableDate" runat="server" Label="机构代码有效期" LabelAlign="Right"
                        EnableEdit="true" LabelWidth="120px">
                    </f:DatePicker>
                    <f:FileUpload runat="server" ID="btnO_ScanUrl" EmptyText="请选择附件" OnFileSelected="btnO_ScanUrl_Click"
                        AutoPostBack="true" Label="机构代码扫描件" LabelWidth="120px">
                    </f:FileUpload>
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="机构代码扫描件">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divO_ScanUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="25% 25% 30% 20%">
                <Items>
                    <f:TextBox ID="txtCertificate" runat="server" Label="资质证书" LabelAlign="Right" MaxLength="50" LabelWidth="120px">
                    </f:TextBox>
                    <f:DatePicker ID="txtC_EnableDate" runat="server" Label="资质证书有效期" LabelAlign="Right"
                        EnableEdit="true" LabelWidth="120px">
                    </f:DatePicker>
                    <f:FileUpload runat="server" ID="btnC_ScanUrl" EmptyText="请选择附件" OnFileSelected="btnC_ScanUrl_Click"
                        AutoPostBack="true" Label="资质证书扫描件" LabelWidth="120px">
                    </f:FileUpload>
                    <f:ContentPanel ID="ContentPanel2" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="资质证书扫描件">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divC_ScanUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="25% 25% 30% 20%">
                <Items>
                    <f:TextBox ID="txtQualityLicense" runat="server" Label="质量体系</br>认证证书" LabelAlign="Right"
                        MaxLength="50" LabelWidth="120px">
                    </f:TextBox>
                    <f:DatePicker ID="txtQL_EnableDate" runat="server" Label="质量--有效期" LabelAlign="Right"
                        EnableEdit="true" LabelWidth="120px">
                    </f:DatePicker>
                    <f:FileUpload runat="server" ID="btnQL_ScanUrl" EmptyText="请选择附件" OnFileSelected="btnQL_ScanUrl_Click"
                        AutoPostBack="true" Label="质量--扫描件" LabelWidth="120px">
                    </f:FileUpload>
                    <f:ContentPanel ID="ContentPanel3" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="质量--扫描件">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divQL_ScanUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="25% 25% 30% 20%">
                <Items>
                    <f:TextBox ID="txtHSELicense" runat="server" Label="HSE体系认证</br>证书(环保)" LabelAlign="Right"
                        MaxLength="50" LabelWidth="120px">
                    </f:TextBox>
                    <f:DatePicker ID="txtH_EnableDate" runat="server" Label="有效期" LabelAlign="Right"
                        EnableEdit="true" LabelWidth="120px">
                    </f:DatePicker>
                    <f:FileUpload runat="server" ID="btnH_ScanUrl" EmptyText="请选择附件" OnFileSelected="btnH_ScanUrl_Click"
                        AutoPostBack="true" Label="扫描件" LabelWidth="120px">
                    </f:FileUpload>
                    <f:ContentPanel ID="ContentPanel4" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="扫描件">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divH_ScanUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="25% 25% 30% 20%">
                <Items>
                    <f:TextBox ID="txtHSELicense2" runat="server" Label="HSE体系认证</br>证书(职业健康)" LabelAlign="Right"
                        MaxLength="50" LabelWidth="120px">
                    </f:TextBox>
                    <f:DatePicker ID="txtH_EnableDate2" runat="server" Label="有效期" LabelAlign="Right"
                        EnableEdit="true" LabelWidth="120px">
                    </f:DatePicker>
                    <f:FileUpload runat="server" ID="btnH_ScanUrl2" EmptyText="请选择附件" OnFileSelected="btnH_ScanUrl2_Click"
                        AutoPostBack="true" Label="扫描件" LabelWidth="120px">
                    </f:FileUpload>
                    <f:ContentPanel ID="ContentPanel6" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="扫描件">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divH_ScanUrl2" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="25% 25% 30% 20%">
                <Items>
                    <f:TextBox ID="txtSecurityLicense" runat="server" Label="安全生产许可证" LabelAlign="Right"
                        MaxLength="50" LabelWidth="120px">
                    </f:TextBox>
                    <f:DatePicker ID="txtSL_EnableDate" runat="server" Label="有效期" LabelAlign="Right"
                        EnableEdit="true" LabelWidth="120px">
                    </f:DatePicker>
                    <f:FileUpload runat="server" ID="btnSL_ScanUrl" EmptyText="请选择附件" OnFileSelected="btnSL_ScanUrl_Click"
                        AutoPostBack="true" Label="扫描件" LabelWidth="120px">
                    </f:FileUpload>
                    <f:ContentPanel ID="ContentPanel5" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="安全生产许可证扫描件">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divSL_ScanUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1" Hidden="true"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>

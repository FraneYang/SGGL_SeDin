<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneralEquipmentQualityEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.QualityAudit.GeneralEquipmentQualityEdit" ValidateRequest="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>一般机具设备资质</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="一般机具设备资质" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtGeneralEquipmentQualityCode" runat="server" Label="编号" LabelAlign="Right"
                        MaxLength="50" Readonly="true">
                    </f:TextBox>
                    <f:DatePicker ID="txtInDate" Label="入场时间" runat="server" LabelAlign="Right" LabelWidth="120px" EnableEdit="true">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpUnitId" runat="server" Label="单位名称" LabelAlign="Right" EnableEdit="true"
                        Required="true" ShowRedStar="true">
                    </f:DropDownList>
                    <f:DropDownList ID="drpSpecialEquipmentId" runat="server" Label="机具设备类型" LabelAlign="Right"
                        EnableEdit="true" Required="true" ShowRedStar="true" LabelWidth="120px">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtEquipmentCount" runat="server" Label="数量" LabelAlign="Right"
                        NoDecimal="true" NoNegative="true">
                    </f:NumberBox>
                    <f:DropDownList ID="drpIsQualified" runat="server" Label="是否合格" LabelAlign="Right"
                        EnableEdit="true" LabelWidth="120px">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtRemark" runat="server" Label="备注" LabelAlign="Right" MaxLength="500">
                    </f:TextArea>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnAttachUrl" Text="附件上传" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
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

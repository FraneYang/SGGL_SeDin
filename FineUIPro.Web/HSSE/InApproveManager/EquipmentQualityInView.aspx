<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EquipmentQualityInView.aspx.cs" Inherits="FineUIPro.Web.HSSE.InApproveManager.EquipmentQualityInView" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查看特种设备审批</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .customlabel span
        {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow ColumnWidths="75% 25%">
                <Items>
                    <f:TextBox ID="txtUnitName" runat="server" Label="单位名称" LabelAlign="Right" Readonly="true"  LabelWidth="90px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtDriverName" runat="server" Label="司机名称" LabelAlign="Right" Readonly="true" LabelWidth="90px">
                    </f:TextBox>
                    <f:TextBox ID="txtCarNum" runat="server" Label="车牌号" LabelAlign="Right" Readonly="true" LabelWidth="90px">
                    </f:TextBox>
                    <f:TextBox ID="txtCarType" runat="server" Label="车辆类型" LabelAlign="Right" Readonly="true" LabelWidth="90px">
                    </f:TextBox>
                    <f:TextBox ID="txtDutyMan" runat="server" Label="负责人" LabelAlign="Right" Readonly="true" LabelWidth="90px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow Height="40px">
                <Items>
                    <f:Label ID="lblT" runat="server" Text="检查内容" CssClass="customlabel span">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 25% 15% 10% 25% 15%">
                <Items>
                    <f:Label ID="Label1" runat="server" Text="序号">
                    </f:Label>
                    <f:Label ID="Label2" runat="server" Text="检查项目">
                    </f:Label>
                    <f:Label ID="Label3" runat="server" Text="检查状况">
                    </f:Label>
                    <f:Label ID="Label4" runat="server" Text="序号">
                    </f:Label>
                    <f:Label ID="Label5" runat="server" Text="检查项目">
                    </f:Label>
                    <f:Label ID="Label6" runat="server" Text="检查状况">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 25% 15% 10% 25% 15%">
                <Items>
                    <f:Label ID="Label7" runat="server" Text="1">
                    </f:Label>
                    <f:Label ID="Label8" runat="server" Text="保险证明（附复印件）">
                    </f:Label>
                    <f:CheckBox ID="ckbCheckItem1" runat="server">
                    </f:CheckBox>
                    <f:Label ID="Label10" runat="server" Text="11">
                    </f:Label>
                    <f:Label ID="Label11" runat="server" Text="轮胎良好">
                    </f:Label>
                    <f:CheckBox ID="ckbCheckItem11" runat="server">
                    </f:CheckBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 25% 15% 10% 25% 15%">
                <Items>
                    <f:Label ID="Label9" runat="server" Text="2">
                    </f:Label>
                    <f:Label ID="Label12" runat="server" Text="年检证明（附复印件）">
                    </f:Label>
                    <f:CheckBox ID="ckbCheckItem2" runat="server">
                    </f:CheckBox>
                    <f:Label ID="Label13" runat="server" Text="12">
                    </f:Label>
                    <f:Label ID="Label14" runat="server" Text="无过量排气噪音">
                    </f:Label>
                    <f:CheckBox ID="ckbCheckItem12" runat="server">
                    </f:CheckBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 25% 15% 10% 25% 15%">
                <Items>
                    <f:Label ID="Label15" runat="server" Text="3">
                    </f:Label>
                    <f:Label ID="Label16" runat="server" Text="行驶证（附复印件)">
                    </f:Label>
                    <f:CheckBox ID="ckbCheckItem3" runat="server">
                    </f:CheckBox>
                    <f:Label ID="Label17" runat="server" Text="13">
                    </f:Label>
                    <f:Label ID="Label18" runat="server" Text="备有安全带和灭火器">
                    </f:Label>
                    <f:CheckBox ID="ckbCheckItem13" runat="server">
                    </f:CheckBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 25% 15% 10% 25% 15%">
                <Items>
                    <f:Label ID="Label19" runat="server" Text="4">
                    </f:Label>
                    <f:Label ID="Label20" runat="server" Text="驾驶证（附复印件）">
                    </f:Label>
                    <f:CheckBox ID="ckbCheckItem4" runat="server">
                    </f:CheckBox>
                    <f:Label ID="Label21" runat="server" Text="14">
                    </f:Label>
                    <f:Label ID="Label22" runat="server" Text="外观良好">
                    </f:Label>
                    <f:CheckBox ID="ckbCheckItem14" runat="server">
                    </f:CheckBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 25% 15% 10% 25% 15%">
                <Items>
                    <f:Label ID="Label23" runat="server" Text="5">
                    </f:Label>
                    <f:Label ID="Label24" runat="server" Text="操作资格证">
                    </f:Label>
                    <f:CheckBox ID="ckbCheckItem5" runat="server">
                    </f:CheckBox>
                    <f:Label ID="Label25" runat="server" Text="15">
                    </f:Label>
                    <f:Label ID="Label26" runat="server" Text="无明显渗漏">
                    </f:Label>
                    <f:CheckBox ID="ckbCheckItem15" runat="server">
                    </f:CheckBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 25% 15% 10% 25% 15%">
                <Items>
                    <f:Label ID="Label27" runat="server" Text="6">
                    </f:Label>
                    <f:Label ID="Label28" runat="server" Text="前后车牌">
                    </f:Label>
                    <f:CheckBox ID="ckbCheckItem6" runat="server">
                    </f:CheckBox>
                    <f:Label ID="Label29" runat="server" Text="16">
                    </f:Label>
                    <f:Label ID="Label30" runat="server" Text="安全装置齐全有效">
                    </f:Label>
                    <f:CheckBox ID="ckbCheckItem16" runat="server">
                    </f:CheckBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 25% 15% 10% 25% 15%">
                <Items>
                    <f:Label ID="Label31" runat="server" Text="7">
                    </f:Label>
                    <f:Label ID="Label32" runat="server" Text="刹车功能良好">
                    </f:Label>
                    <f:CheckBox ID="ckbCheckItem7" runat="server">
                    </f:CheckBox>
                    <f:Label ID="Label33" runat="server" Text="17">
                    </f:Label>
                    <f:Label ID="Label34" runat="server" Text="吊钩完好">
                    </f:Label>
                    <f:CheckBox ID="ckbCheckItem17" runat="server">
                    </f:CheckBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 25% 15% 10% 25% 15%">
                <Items>
                    <f:Label ID="Label35" runat="server" Text="8">
                    </f:Label>
                    <f:Label ID="Label36" runat="server" Text="灯光可用、完好">
                    </f:Label>
                    <f:CheckBox ID="ckbCheckItem8" runat="server">
                    </f:CheckBox>
                    <f:Label ID="Label37" runat="server" Text="18">
                    </f:Label>
                    <f:Label ID="Label38" runat="server" Text="钢丝绳完好">
                    </f:Label>
                    <f:CheckBox ID="ckbCheckItem18" runat="server">
                    </f:CheckBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 25% 15% 10% 25% 15%">
                <Items>
                    <f:Label ID="Label39" runat="server" Text="9">
                    </f:Label>
                    <f:Label ID="Label40" runat="server" Text="挡风玻璃雨刷完好">
                    </f:Label>
                    <f:CheckBox ID="ckbCheckItem9" runat="server">
                    </f:CheckBox>
                    <f:Label ID="Label41" runat="server" Text="19">
                    </f:Label>
                    <f:Label ID="Label42" runat="server" Text="液压系统完好">
                    </f:Label>
                    <f:CheckBox ID="ckbCheckItem19" runat="server">
                    </f:CheckBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 25% 15% 10% 25% 15%">
                <Items>
                    <f:Label ID="Label43" runat="server" Text="10">
                    </f:Label>
                    <f:Label ID="Label44" runat="server" Text="反光镜完好">
                    </f:Label>
                    <f:CheckBox ID="ckbCheckItem10" runat="server">
                    </f:CheckBox>
                    <f:Label ID="Label45" runat="server" Text="20">
                    </f:Label>
                    <f:Label ID="Label46" runat="server" Text="倒车警报完好">
                    </f:Label>
                    <f:CheckBox ID="ckbCheckItem20" runat="server">
                    </f:CheckBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true"
                        BodyPadding="0px">
                        <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp">
                    </f:Label>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
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

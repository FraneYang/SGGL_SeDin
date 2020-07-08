<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccidentHandleEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Accident.AccidentHandleEdit" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑HSE事故(含未遂)处理</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="HSE事故(含未遂)处理"
        AutoScroll="true" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
        LabelAlign="Right">
        <Rows>
            <f:FormRow ColumnWidths="20% 20% 20% 40%">
                <Items>
                    <f:TextBox ID="txtAccidentHandleCode" runat="server" Label="编号" LabelAlign="Right"
                        MaxLength="50" Readonly="true" LabelWidth="80px">
                    </f:TextBox>
                    <f:TextBox ID="txtAccidentHandleName" runat="server" Label="名称" LabelAlign="Right"
                        Required="true" ShowRedStar="true" MaxLength="50" FocusOnPageLoad="true"  LabelWidth="80px">
                    </f:TextBox>
                       <f:DatePicker ID="txtAccidentDate" runat="server" Label="时间" LabelAlign="Right"
                        EnableEdit="true" Required="true" ShowRedStar="true"  LabelWidth="80px">
                    </f:DatePicker>
                    <f:DropDownList ID="drpUnitId" runat="server" Label="事故单位" EnableEdit="true"
                        LabelAlign="Right"  LabelWidth="80px">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtMoneyLoss" runat="server" Label="经济损失"   LabelWidth="80px"
                        NoNegative="True" EmptyText="0" DecimalPrecision="0" NoDecimal="False">
                    </f:NumberBox>
                    <f:NumberBox ID="txtWorkHoursLoss" runat="server" Label="工时损失"   LabelWidth="80px"
                        NoNegative="True" EmptyText="0" DecimalPrecision="0" NoDecimal="False">
                    </f:NumberBox>           
                    <f:NumberBox ID="txtMinorInjuriesPersonNum" runat="server" Label="轻伤人数"   LabelWidth="80px"
                        NoNegative="True" EmptyText="0" DecimalPrecision="0" NoDecimal="False">
                    </f:NumberBox>
                    <f:NumberBox ID="txtInjuriesPersonNum" runat="server" Label="重伤人数"   LabelWidth="80px"
                        NoNegative="True" EmptyText="0" DecimalPrecision="0" NoDecimal="False">
                    </f:NumberBox>
                    <f:NumberBox ID="txtDeathPersonNum" runat="server" Label="死亡人数"  LabelWidth="80px"
                        NoNegative="True" EmptyText="0" DecimalPrecision="0" NoDecimal="False">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtAccidentDef" runat="server" Label="事故摘要" LabelAlign="Right"
                        MaxLength="500" Height="90px"  LabelWidth="80px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtAccidentHandle" runat="server" Label="善后处理" LabelAlign="Right"
                        MaxLength="500"  Height="70px"  LabelWidth="80px">
                    </f:TextArea>
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
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                        OnClick="btnSubmit_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>

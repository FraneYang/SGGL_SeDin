<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpenseEdit.aspx.cs" Inherits="FineUIPro.Web.HSSE.CostGoods.ExpenseEdit"
    ValidateRequest="false" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑安全费用计划</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="安全费用计划" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" 
        Layout="VBox">
        <Rows>
            <f:FormRow ColumnWidths="30% 20% 50%">
                <Items>
                    <f:DropDownList ID="drpYear" runat="server" Label="月份" LabelWidth="200px" Required="true"
                        ShowRedStar="true">
                    </f:DropDownList>
                    <f:DropDownList ID="drpMonths" runat="server" Required="true">
                    </f:DropDownList>
                    <f:TextBox ID="txtExpenseCode" runat="server" Label="编号" LabelAlign="Right" MaxLength="50"
                        Readonly="true" Width="200px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="50% 50%">
                <Items>
                    <f:DropDownList ID="drpUnitId" runat="server" Label="填报单位" LabelAlign="Right" EnableEdit="true"
                        Required="true" LabelWidth="200px" ShowRedStar="true" AutoPostBack="true" OnSelectedIndexChanged="drpUnitId_SelectedIndexChanged">
                    </f:DropDownList>
                    <f:DatePicker ID="txtReportDate" runat="server" Label="填报日期" LabelAlign="Right">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Panel ID="Panel2" runat="server" ShowBorder="false" Layout="Table" TableConfigColumns="5"
                        ShowHeader="false" BodyPadding="1px">
                        <Items>
                            <f:Panel ID="Panel1" Title="Panel1" TableRowspan="2" MarginRight="0" Width="200px"
                                runat="server" BodyPadding="1px" ShowBorder="false" ShowHeader="false">
                                <Items>
                                    <f:Label runat="server" ID="Label58" Text="编号">
                                    </f:Label>
                                </Items>
                            </f:Panel>
                            <f:Panel ID="Panel3" Title="Panel1" runat="server" BodyPadding="1px" Width="250px"
                                TableRowspan="2" ShowBorder="false" ShowHeader="false" MarginLeft="50">
                                <Items>
                                    <f:Label runat="server" ID="lblAccidentType11" Text="类别">
                                    </f:Label>
                                </Items>
                            </f:Panel>
                            <f:Panel ID="Panel5" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                TableColspan="2" Width="400px" ShowHeader="false" MarginLeft="150px">
                                <Items>
                                    <f:Label runat="server" ID="Label1" Text="费用（元）">
                                    </f:Label>
                                </Items>
                            </f:Panel>
                            <f:Panel ID="Panel4" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                TableRowspan="2" Width="450px" ShowHeader="false" MarginLeft="80px">
                                <Items>
                                    <f:Label runat="server" ID="Label2" Text="备注">
                                    </f:Label>
                                </Items>
                            </f:Panel>
                            <f:Panel ID="Panel6" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                Width="200px" ShowHeader="false" MarginLeft="50px">
                                <Items>
                                    <f:Label runat="server" ID="Label3" Text=" 当期">
                                    </f:Label>
                                </Items>
                            </f:Panel>
                            <f:Panel ID="Panel7" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                Width="200px" ShowHeader="false" MarginLeft="50px">
                                <Items>
                                    <f:Label runat="server" ID="Label4" Text=" 项目累计">
                                    </f:Label>
                                </Items>
                            </f:Panel>
                        </Items>
                    </f:Panel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label runat="server" ID="Label5" Text="A1" Width="200px">
                    </f:Label>
                    <f:Label runat="server" ID="Label6" Text="基础管理" Width="250px">
                    </f:Label>
                    <f:NumberBox ID="nbA1" NoDecimal="false" NoNegative="false" MinValue="0" runat="server"
                        AutoPostBack="true" Label="" OnTextChanged="nbA1_TextChanged" Width="200px">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectA1" NoDecimal="false" NoNegative="false" Readonly="true"
                        MinValue="0" runat="server" Label="" Width="200px">
                    </f:NumberBox>
                    <f:TextBox runat="server" ID="txtDefA1" Width="450px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label runat="server" ID="Label7" Text="A2" Width="200px">
                    </f:Label>
                    <f:Label runat="server" ID="Label8" Text="安全技术" Width="250px">
                    </f:Label>
                    <f:NumberBox ID="nbA2" NoDecimal="false" NoNegative="false" MinValue="0" runat="server"
                        AutoPostBack="true" Label="" OnTextChanged="nbA2_TextChanged" Width="200px">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectA2" NoDecimal="false" NoNegative="false" Readonly="true"
                        MinValue="0" runat="server" Label="" Width="200px">
                    </f:NumberBox>
                    <f:TextBox runat="server" ID="txtDefA2" Width="450px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label runat="server" ID="Label9" Text="A3" Width="200px">
                    </f:Label>
                    <f:Label runat="server" ID="Label10" Text="职业健康" Width="250px">
                    </f:Label>
                    <f:NumberBox ID="nbA3" NoDecimal="false" NoNegative="false" MinValue="0" runat="server"
                        AutoPostBack="true" Label="" OnTextChanged="nbA3_TextChanged" Width="200px">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectA3" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="" Width="200px">
                    </f:NumberBox>
                    <f:TextBox runat="server" ID="txtDefA3" Width="450px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label runat="server" ID="Label11" Text="A4" Width="200px">
                    </f:Label>
                    <f:Label runat="server" ID="Label12" Text="安全防护" Width="250px">
                    </f:Label>
                    <f:NumberBox ID="nbA4" NoDecimal="false" NoNegative="false" MinValue="0" runat="server"
                        AutoPostBack="true" Label="" OnTextChanged="nbA4_TextChanged" Width="200px">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectA4" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="" Width="200px">
                    </f:NumberBox>
                    <f:TextBox runat="server" ID="txtDefA4" Width="450px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label runat="server" ID="Label13" Text="A5" Width="200px">
                    </f:Label>
                    <f:Label runat="server" ID="Label14" Text="化工试车" Width="250px">
                    </f:Label>
                    <f:NumberBox ID="nbA5" NoDecimal="false" NoNegative="false" MinValue="0" runat="server"
                        AutoPostBack="true" Label="" OnTextChanged="nbA5_TextChanged" Width="200px">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectA5" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="" Width="200px">
                    </f:NumberBox>
                    <f:TextBox runat="server" ID="txtDefA5" Width="450px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label runat="server" ID="Label15" Text="A6" Width="200px">
                    </f:Label>
                    <f:Label runat="server" ID="Label16" Text="教育培训" Width="250px">
                    </f:Label>
                    <f:NumberBox ID="nbA6" NoDecimal="false" NoNegative="false" MinValue="0" runat="server"
                        AutoPostBack="true" Label="" OnTextChanged="nbA6_TextChanged" Width="200px">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectA6" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="" Width="200px">
                    </f:NumberBox>
                    <f:TextBox runat="server" ID="txtDefA6" Width="450px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label runat="server" ID="Label29" Width="200px">
                    </f:Label>
                    <f:Label runat="server" ID="Label17" Text="A安全生产费用合计" Width="250px">
                    </f:Label>
                    <f:NumberBox ID="nbA" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="" Width="200px">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectA" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="" Width="200px">
                    </f:NumberBox>
                    <f:Label runat="server" ID="Label18" Text="" Width="450px">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label runat="server" ID="Label19" Text="B1" Width="200px">
                    </f:Label>
                    <f:Label runat="server" ID="Label20" Text="文明施工" Width="250px">
                    </f:Label>
                    <f:NumberBox ID="nbB1" NoDecimal="false" NoNegative="false" MinValue="0" runat="server"
                        AutoPostBack="true" Label="" OnTextChanged="nbB1_TextChanged" Width="200px">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectB1" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="" Width="200px">
                    </f:NumberBox>
                    <f:TextBox runat="server" ID="txtDefB1" Width="450px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label runat="server" ID="Label21" Text="B2" Width="200px">
                    </f:Label>
                    <f:Label runat="server" ID="Label22" Text="临时设施" Width="250px">
                    </f:Label>
                    <f:NumberBox ID="nbB2" NoDecimal="false" NoNegative="false" MinValue="0" runat="server"
                        AutoPostBack="true" Label="" OnTextChanged="nbB2_TextChanged" Width="200px">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectB2" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="" Width="200px">
                    </f:NumberBox>
                    <f:TextBox runat="server" ID="txtDefB2" Width="450px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label runat="server" ID="Label23" Text="B3" Width="200px">
                    </f:Label>
                    <f:Label runat="server" ID="Label24" Text="环境保护" Width="250px">
                    </f:Label>
                    <f:NumberBox ID="nbB3" NoDecimal="false" NoNegative="false" MinValue="0" runat="server"
                        AutoPostBack="true" Label="" OnTextChanged="nbB3_TextChanged" Width="200px">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectB3" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="" Width="200px">
                    </f:NumberBox>
                    <f:TextBox runat="server" ID="txtDefB3" Width="450px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label runat="server" ID="Label30" Width="200px">
                    </f:Label>
                    <f:Label runat="server" ID="Label25" Text="B文明施工措施费合计" Width="250px">
                    </f:Label>
                    <f:NumberBox ID="nbB" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="" Width="200px">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectB" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="" Width="200px">
                    </f:NumberBox>
                    <f:Label runat="server" ID="Label26" Text="" Width="450px">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label runat="server" ID="Label31" Width="200px">
                    </f:Label>
                    <f:Label runat="server" ID="Label27" Text="A+B安全生产及文明施工措施费总计" Width="250px">
                    </f:Label>
                    <f:NumberBox ID="nbAB" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="" Width="200px">
                    </f:NumberBox>
                    <f:NumberBox ID="nbProjectAB" NoDecimal="false" NoNegative="false" MinValue="0" Readonly="true"
                        runat="server" Label="" Width="200px">
                    </f:NumberBox>
                    <f:Label runat="server" ID="Label28" Text="" Width="450px">
                    </f:Label>
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
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server" Margin="0 50 30 50">
                <Items>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                    </f:Button>
                    <f:HiddenField runat="server" ID="hdProjectA1">
                    </f:HiddenField>
                    <f:HiddenField runat="server" ID="hdProjectA2">
                    </f:HiddenField>
                    <f:HiddenField runat="server" ID="hdProjectA3">
                    </f:HiddenField>
                    <f:HiddenField runat="server" ID="hdProjectA4">
                    </f:HiddenField>
                    <f:HiddenField runat="server" ID="hdProjectA5">
                    </f:HiddenField>
                    <f:HiddenField runat="server" ID="hdProjectA6">
                    </f:HiddenField>
                    <f:HiddenField runat="server" ID="hdProjectB1">
                    </f:HiddenField>
                    <f:HiddenField runat="server" ID="hdProjectB2">
                    </f:HiddenField>
                    <f:HiddenField runat="server" ID="hdProjectB3">
                    </f:HiddenField>
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

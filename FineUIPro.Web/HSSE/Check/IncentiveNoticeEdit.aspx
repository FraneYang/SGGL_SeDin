<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IncentiveNoticeEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Check.IncentiveNoticeEdit" ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑奖励通知单</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="奖励通知单" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtIncentiveNoticeCode" runat="server" Label="编号" LabelAlign="Right"
                        MaxLength="50" Readonly="true" LabelWidth="90px">
                    </f:TextBox>
                    <f:DropDownList ID="drpUnit" runat="server" Label="受奖单位" LabelAlign="Right" EnableEdit="true" LabelWidth="90px"
                        Required="true" ShowRedStar="true" AutoPostBack="true" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged">
                    </f:DropDownList>
                    <f:DropDownList ID="drpTeamGroup" runat="server" Label="受奖班组" LabelAlign="Right" EnableEdit="true" LabelWidth="90px">
                    </f:DropDownList>
                    <f:DropDownList ID="drpPerson" runat="server" Label="受奖个人" LabelAlign="Right" EnableEdit="true"  LabelWidth="90px">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DatePicker ID="txtIncentiveDate" runat="server" Label="奖励时间" LabelAlign="Right"
                        EnableEdit="true" LabelWidth="90px">
                    </f:DatePicker>
                    <f:DropDownList ID="drpRewardType" runat="server" Label="奖励类型" LabelAlign="Right" LabelWidth="90px"
                        Required="true" ShowRedStar="true">
                     </f:DropDownList>
                        <f:DropDownList ID="drpSignMan" runat="server" Label="签发人" LabelAlign="Right" LabelWidth="90px">
                    </f:DropDownList>
                    <f:DropDownList ID="drpApproveMan" runat="server" Label="批准人" LabelAlign="Right"
                        LabelWidth="90px">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtBasicItem" runat="server" Label="奖励根据 " LabelAlign="Right" MaxLength="300"
                        LabelWidth="90px">
                    </f:TextBox>                    
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Panel ID="Panel19" runat="server" Height="100px" ShowBorder="false"
                        Layout="Table" TableConfigColumns="2" ShowHeader="false" BodyPadding="1px">
                        <Items>
                            <f:Panel ID="Panel14" Title="Panel1" Width="150px" Height="100px" MarginRight="0"
                                MarginTop="40" TableRowspan="3" runat="server" BodyPadding="1px" ShowBorder="false"
                                ShowHeader="false" MarginLeft="45px">
                                <Items>
                                    <f:Label runat="server" ID="Label30" Text="奖励方式：" LabelWidth="90px">
                                    </f:Label>
                                </Items>
                            </f:Panel>
                            <f:Panel ID="Panel16" Title="Panel1" Width="800px" Height="30px" runat="server" BodyPadding="1px"
                                ShowBorder="false" ShowHeader="false">
                                <Items>
                                    <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="true" runat="server"
                                        RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow ColumnWidths="10% 25% 25% 40%">
                                                <Items>
                                                    <f:CheckBox runat="server" ID="rbtnIncentiveWay1" Checked="true" AutoPostBack="true"
                                                        OnCheckedChanged="rbtnIncentiveWay1_CheckedChanged">
                                                    </f:CheckBox>
                                                    <f:NumberBox runat="server" ID="txtPayMoney" OnBlur="txtPayMoney_Blur" EnableBlurEvent="true">
                                                    </f:NumberBox>
                                                    <f:TextBox runat="server" ID="txtCurrency" Label="币种" MaxLength="50" LabelWidth="60px">
                                                    </f:TextBox>
                                                    <f:TextBox runat="server" ID="txtBig" Label="大写" Readonly="true" LabelWidth="60px">
                                                    </f:TextBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:Panel>
                            <f:Panel ID="Panel1" Title="Panel1" Width="800px" Height="30px" runat="server" BodyPadding="1px"
                                ShowBorder="false" ShowHeader="false">
                                <Items>
                                    <f:Form ID="Form3" ShowBorder="false" ShowHeader="false" AutoScroll="true" runat="server"
                                        RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow ColumnWidths="5% 5% 80% 10%">
                                                <Items>
                                                    <f:CheckBox runat="server" ID="rbtnIncentiveWay2" AutoPostBack="true" OnCheckedChanged="rbtnIncentiveWay2_CheckedChanged">
                                                    </f:CheckBox>
                                                    <f:Label runat="server" ID="Label33" Text="授予">
                                                    </f:Label>
                                                    <f:TextBox runat="server" ID="txtTitleReward">
                                                    </f:TextBox>
                                                    <f:Label runat="server" ID="Label34" Text="称号">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:Panel>
                            <f:Panel ID="Panel2" Title="Panel1" Width="800px" Height="30px" runat="server" BodyPadding="1px"
                                ShowBorder="false" ShowHeader="false">
                                <Items>
                                    <f:Form ID="Form4" ShowBorder="false" ShowHeader="false" AutoScroll="true" runat="server"
                                        RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow ColumnWidths="5% 5% 80% 10%">
                                                <Items>
                                                    <f:CheckBox runat="server" ID="rbtnIncentiveWay3" AutoPostBack="true" OnCheckedChanged="rbtnIncentiveWay3_CheckedChanged">
                                                    </f:CheckBox>
                                                    <f:Label runat="server" ID="Label35" Text="给予">
                                                    </f:Label>
                                                    <f:TextBox runat="server" ID="txtMattleReward">
                                                    </f:TextBox>
                                                    <f:Label runat="server" ID="Label36" Text="物质奖励">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:Panel>
                        </Items>
                    </f:Panel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:FileUpload runat="server" ID="btnFile" EmptyText="请选择附件" OnFileSelected="btnFile_Click"
                        AutoPostBack="true" Label="附件" LabelWidth="90px">
                    </f:FileUpload>
                    <f:ContentPanel ID="ContentPanel2" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="附件">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divFile" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label runat="server" ID="Label37" Text="有关证据（被奖罚人姓名、岗位、证件编号、有关文字描述或照片等）：">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:HtmlEditor runat="server" Label="有关证据" ID="txtFileContents" ShowLabel="false" 
                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="200px" LabelAlign="Right">
                    </f:HtmlEditor>
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
                    <f:HiddenField ID="hdUnitId" runat="server">
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
    </form>
</body>
</html>

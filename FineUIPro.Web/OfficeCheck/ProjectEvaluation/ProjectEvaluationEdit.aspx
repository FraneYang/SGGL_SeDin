<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectEvaluationEdit.aspx.cs"
    Inherits="FineUIPro.Web.OfficeCheck.ProjectEvaluation.ProjectEvaluationEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>项目评价</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AjaxAspnetControls="divFile1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="项目绩效评价" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtPerfomanceRecordCode" runat="server" Label="评价编号" LabelAlign="Right"
                        MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtProjectName" runat="server" Label="项目名称" LabelAlign="Right"
                        MaxLength="50" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DatePicker ID="txtEvaluationDate" runat="server" Label="评价时间" LabelAlign="Right"
                        EnableEdit="true">
                    </f:DatePicker>
                    <f:Label runat="server" ID="lb"></f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TabStrip ID="TabStrip1" Width="850px" Height="380px" ShowBorder="true" TabPosition="Top"
                        EnableTabCloseMenu="false" runat="server">
                        <Tabs>
                            <f:Tab ID="Tab1" Title="模式一" BodyPadding="5px" Layout="Fit" runat="server">
                                <Items>
                                    <f:Form ID="Form2" runat="server" ShowHeader="false" BodyPadding="5px">
                                        <Items>
                                            <f:FormRow>
                                                <Items>
                                                    <f:TextArea ID="txtEvaluationDef" runat="server" Label="评价描述" MaxLength="3000" Height="230">
                                                    </f:TextArea>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:DropDownList ID="drpRewardOrPunish" runat="server" Label="奖/罚">
                                                    </f:DropDownList>
                                                    <f:NumberBox ID="txtRPMoney" runat="server" Label="金额" NoNegative="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:TextBox ID="txtAssessmentGroup" runat="server" Label="考评组" MaxLength="200">
                                                    </f:TextBox>
                                                </Items>
                                            </f:FormRow>
                                        </Items>
                                    </f:Form>
                                </Items>
                            </f:Tab>
                            <f:Tab ID="Tab2" Title="模式二" BodyPadding="5px" runat="server" AutoScroll="true">
                                <Items>
                                    <f:Form ID="Form3" runat="server" ShowHeader="false" BodyPadding="5px">
                                        <Items>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label1" runat="server" Text="序号">
                                                    </f:Label>
                                                    <f:Label ID="Label2" runat="server" Text="评价内容">
                                                    </f:Label>
                                                    <f:Label ID="Label3" runat="server" Text="表现情况">
                                                    </f:Label>
                                                    <f:Label ID="Label4" runat="server" Text="标准分">
                                                    </f:Label>
                                                    <f:Label ID="Label5" runat="server" Text="实得分">
                                                    </f:Label>
                                                    <f:Label ID="Label66" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="lblSort_1" runat="server" Text="1">
                                                    </f:Label>
                                                    <f:Label ID="Label6" runat="server" Text="体系文件、各种管理规定的针对性">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_1" runat="server" MaxLength="50">
                                                    </f:TextBox>
                                                    <f:Label ID="Label7" runat="server" Text="5">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_1" runat="server" NoNegative="true" MaxValue="5" MinValue="0" AutoPostBack="true" OnTextChanged="TextBox_OnTextChanged">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label68" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label8" runat="server" Text="2">
                                                    </f:Label>
                                                    <f:Label ID="Label9" runat="server" Text="组织机构、岗位职责">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_2" runat="server" MaxLength="50">
                                                    </f:TextBox>
                                                    <f:Label ID="Label10" runat="server" Text="5">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_2" runat="server" NoNegative="true" MaxValue="5" MinValue="0" AutoPostBack="true" OnTextChanged="TextBox_OnTextChanged">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label69" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label11" runat="server" Text="3">
                                                    </f:Label>
                                                    <f:Label ID="Label12" runat="server" Text="现场管理体系运行情况">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_3" runat="server" MaxLength="50">
                                                    </f:TextBox>
                                                    <f:Label ID="Label13" runat="server" Text="5">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_3" runat="server" NoNegative="true" MaxValue="5" MinValue="0" AutoPostBack="true" OnTextChanged="TextBox_OnTextChanged">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label70" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label14" runat="server" Text="4">
                                                    </f:Label>
                                                    <f:Label ID="Label15" runat="server" Text="人员培训">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_4" runat="server" MaxLength="50">
                                                    </f:TextBox>
                                                    <f:Label ID="Label16" runat="server" Text="5">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_4" runat="server" NoNegative="true" MaxValue="5" MinValue="0" AutoPostBack="true" OnTextChanged="TextBox_OnTextChanged">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label71" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label17" runat="server" Text="5">
                                                    </f:Label>
                                                    <f:Label ID="Label18" runat="server" Text="资源配备">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_5" runat="server" MaxLength="50">
                                                    </f:TextBox>
                                                    <f:Label ID="Label19" runat="server" Text="5">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_5" runat="server" NoNegative="true" MaxValue="5" MinValue="0" AutoPostBack="true" OnTextChanged="TextBox_OnTextChanged">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label72" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label20" runat="server" Text="6">
                                                    </f:Label>
                                                    <f:Label ID="Label21" runat="server" Text="HSSE方案、措施报批">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_6" runat="server" MaxLength="50">
                                                    </f:TextBox>
                                                    <f:Label ID="Label22" runat="server" Text="5">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_6" runat="server" NoNegative="true" MaxValue="5" MinValue="0" AutoPostBack="true" OnTextChanged="TextBox_OnTextChanged">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label73" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label23" runat="server" Text="7">
                                                    </f:Label>
                                                    <f:Label ID="Label24" runat="server" Text="方案、措施执行">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_7" runat="server" MaxLength="50">
                                                    </f:TextBox>
                                                    <f:Label ID="Label25" runat="server" Text="5">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_7" runat="server" NoNegative="true" MaxValue="5" MinValue="0" AutoPostBack="true" OnTextChanged="TextBox_OnTextChanged">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label74" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label26" runat="server" Text="8">
                                                    </f:Label>
                                                    <f:Label ID="Label27" runat="server" Text="危害辨识、风险评价和过程控制">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_8" runat="server" MaxLength="50">
                                                    </f:TextBox>
                                                    <f:Label ID="Label28" runat="server" Text="5">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_8" runat="server" NoNegative="true" MaxValue="5" MinValue="0" AutoPostBack="true" OnTextChanged="TextBox_OnTextChanged">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label75" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label29" runat="server" Text="9">
                                                    </f:Label>
                                                    <f:Label ID="Label30" runat="server" Text="HSSE会议">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_9" runat="server" MaxLength="50">
                                                    </f:TextBox>
                                                    <f:Label ID="Label31" runat="server" Text="5">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_9" runat="server" NoNegative="true" MaxValue="5" MinValue="0" AutoPostBack="true" OnTextChanged="TextBox_OnTextChanged">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label76" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label32" runat="server" Text="10">
                                                    </f:Label>
                                                    <f:Label ID="Label33" runat="server" Text="施工作业HSSE管理">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_10" runat="server" MaxLength="50">
                                                    </f:TextBox>
                                                    <f:Label ID="Label34" runat="server" Text="5">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_10" runat="server" NoNegative="true" MaxValue="5" MinValue="0" AutoPostBack="true" OnTextChanged="TextBox_OnTextChanged">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label77" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label35" runat="server" Text="11">
                                                    </f:Label>
                                                    <f:Label ID="Label36" runat="server" Text="现场违章">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_11" runat="server" MaxLength="50">
                                                    </f:TextBox>
                                                    <f:Label ID="Label37" runat="server" Text="5">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_11" runat="server" NoNegative="true" MaxValue="5" MinValue="0" AutoPostBack="true" OnTextChanged="TextBox_OnTextChanged">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label78" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label38" runat="server" Text="12">
                                                    </f:Label>
                                                    <f:Label ID="Label39" runat="server" Text="宣传、警示">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_12" runat="server" MaxLength="50">
                                                    </f:TextBox>
                                                    <f:Label ID="Label40" runat="server" Text="5">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_12" runat="server" NoNegative="true" MaxValue="5" MinValue="0" AutoPostBack="true" OnTextChanged="TextBox_OnTextChanged">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label79" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label41" runat="server" Text="13">
                                                    </f:Label>
                                                    <f:Label ID="Label42" runat="server" Text="文明施工与环境卫生">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_13" runat="server" MaxLength="50">
                                                    </f:TextBox>
                                                    <f:Label ID="Label43" runat="server" Text="5">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_13" runat="server" NoNegative="true" MaxValue="5" MinValue="0" AutoPostBack="true" OnTextChanged="TextBox_OnTextChanged">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label80" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label44" runat="server" Text="14">
                                                    </f:Label>
                                                    <f:Label ID="Label45" runat="server" Text="消防安全">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_14" runat="server" MaxLength="50">
                                                    </f:TextBox>
                                                    <f:Label ID="Label46" runat="server" Text="5">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_14" runat="server" NoNegative="true" MaxValue="5" MinValue="0" AutoPostBack="true" OnTextChanged="TextBox_OnTextChanged">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label81" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label47" runat="server" Text="15">
                                                    </f:Label>
                                                    <f:Label ID="Label48" runat="server" Text="治安保卫">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_15" runat="server" MaxLength="50">
                                                    </f:TextBox>
                                                    <f:Label ID="Label49" runat="server" Text="5">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_15" runat="server" NoNegative="true" MaxValue="5" MinValue="0" AutoPostBack="true" OnTextChanged="TextBox_OnTextChanged">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label82" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label50" runat="server" Text="16">
                                                    </f:Label>
                                                    <f:Label ID="Label51" runat="server" Text="奖惩情况">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_16" runat="server" MaxLength="50">
                                                    </f:TextBox>
                                                    <f:Label ID="Label52" runat="server" Text="5">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_16" runat="server" NoNegative="true" MaxValue="5" MinValue="0" AutoPostBack="true" OnTextChanged="TextBox_OnTextChanged">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label83" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label53" runat="server" Text="17">
                                                    </f:Label>
                                                    <f:Label ID="Label54" runat="server" Text="应急准备及应急演练">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_17" runat="server" MaxLength="50">
                                                    </f:TextBox>
                                                    <f:Label ID="Label55" runat="server" Text="5">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_17" runat="server" NoNegative="true" MaxValue="5" MinValue="0" AutoPostBack="true" OnTextChanged="TextBox_OnTextChanged">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label84" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label56" runat="server" Text="18">
                                                    </f:Label>
                                                    <f:Label ID="Label57" runat="server" Text="法律、法规、管理规范清单">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_18" runat="server" MaxLength="50">
                                                    </f:TextBox>
                                                    <f:Label ID="Label58" runat="server" Text="5">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_18" runat="server" NoNegative="true" MaxValue="5" MinValue="0" AutoPostBack="true" OnTextChanged="TextBox_OnTextChanged">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label85" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label59" runat="server" Text="19">
                                                    </f:Label>
                                                    <f:Label ID="Label60" runat="server" Text="事故、事件调查处理情况">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_19" runat="server" MaxLength="50">
                                                    </f:TextBox>
                                                    <f:Label ID="Label61" runat="server" Text="5">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_19" runat="server" NoNegative="true" MaxValue="5" MinValue="0" AutoPostBack="true" OnTextChanged="TextBox_OnTextChanged">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label86" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label62" runat="server" Text="20">
                                                    </f:Label>
                                                    <f:Label ID="Label63" runat="server" Text="过程文件的规范性">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_20" runat="server" MaxLength="50">
                                                    </f:TextBox>
                                                    <f:Label ID="Label64" runat="server" Text="5">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_20" runat="server" NoNegative="true" MaxValue="5" MinValue="0" AutoPostBack="true" OnTextChanged="TextBox_OnTextChanged">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label87" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="10% 60% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label65" runat="server" Text="综合评语">
                                                    </f:Label>
                                                    <f:TextBox ID="txtTotalJudging" runat="server" MaxLength="100">
                                                    </f:TextBox>
                                                    <f:Label ID="Label67" runat="server" Text="最终得分">
                                                    </f:Label>
                                                    <f:TextBox ID="txtTotalScore" runat="server" Readonly="true">
                                                    </f:TextBox>
                                                    <f:Label ID="Label88" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                        </Items>
                                    </f:Form>
                                </Items>
                            </f:Tab>
                        </Tabs>
                    </f:TabStrip>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
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
     <f:Window ID="WindowAtt" Title="附件上传查看" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>

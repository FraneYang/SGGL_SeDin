<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SafetyQuarterlyReportEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.InformationProject.SafetyQuarterlyReportEdit" ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑安全生产数据季报</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="ddlYearId" runat="server" Label="年度" Required="True" ShowRedStar="True" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged">
                    </f:DropDownList>
                    <f:DropDownList ID="ddlQuarter" runat="server" Label="季度" Required="True" ShowRedStar="True" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtTotalInWorkHours" runat="server" Label="总投入工时数" LabelWidth="250px"
                        NoNegative="True" EmptyText="0" DecimalPrecision="0" NoDecimal="True">
                    </f:NumberBox>
                    <f:TextBox ID="txtTotalInWorkHoursRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtTotalOutWorkHours" runat="server" Label="总损失工时数" LabelWidth="250px"
                        NoNegative="True" DecimalPrecision="0" NoDecimal="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtTotalOutWorkHoursRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtWorkHoursLossRate" runat="server" Label="百万工时损失率" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtWorkHoursLossRateRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtWorkHoursAccuracy" runat="server" Label="工时统计准确率（%）" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtWorkHoursAccuracyRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtMainBusinessIncome" runat="server" Label="主营业务收入/亿元" LabelWidth="250px"
                        NoNegative="True" EmptyText="0" DecimalPrecision="4">
                    </f:NumberBox>
                    <f:TextBox ID="txtMainBusinessIncomeRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtConstructionRevenue" runat="server" Label="施工收入/亿元" LabelWidth="250px"
                        NoNegative="True" EmptyText="0" DecimalPrecision="4">
                    </f:NumberBox>
                    <f:TextBox ID="txtConstructionRevenueRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtUnitTimeIncome" runat="server" Label="单位工时收入/元" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtUnitTimeIncomeRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtBillionsOutputMortality" runat="server" Label="百亿产值死亡率" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtBillionsOutputMortalityRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtMajorFireAccident" runat="server" Label="重大火灾事故报告数" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtMajorFireAccidentRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtMajorEquipAccident" runat="server" Label="重大机械设备事故报告数" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtMajorEquipAccidentRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtAccidentFrequency" runat="server" Label="事故发生频率（占总收入之比）" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtAccidentFrequencyRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtSeriousInjuryAccident" runat="server" Label="重伤以上事故报告数" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtSeriousInjuryAccidentRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtFireAccident" runat="server" Label="火灾事故统计报告数" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtFireAccidentRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtEquipmentAccident" runat="server" Label="装备事故统计报告数" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtEquipmentAccidentRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtPoisoningAndInjuries" runat="server" Label="中毒及职业伤害报告数" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtPoisoningAndInjuriesRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtProductionSafetyInTotal" runat="server" Label="安全生产投入总额/元" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtProductionSafetyInTotalRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtProtectionInput" runat="server" Label="安全防护投入/元" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtProtectionInputRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtLaboAndHealthIn" runat="server" Label="劳动保护及职业健康投入/元" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtLaboAndHealthInRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtTechnologyProgressIn" runat="server" Label="安全技术进步投入/元" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtTechnologyProgressInRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtEducationTrainIn" runat="server" Label="安全教育培训投入/元" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtEducationTrainInRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtProjectCostRate" runat="server" Label="工程造价占比（%）" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtProjectCostRateRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtProductionInput" runat="server" Label="百万工时安全生产投入额/万元" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtProductionInputRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtRevenue" runat="server" Label="安全生产投入占施工收入之比（%）" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtRevenueRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtFullTimeMan" runat="server" Label="安全专职人员总数（附名单）" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtFullTimeManRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtPMMan" runat="server" Label="项目经理人员总数（附名单）" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtPMManRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtCorporateDirectorEdu" runat="server" Label="企业负责人安全生产继续教育数" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtCorporateDirectorEduRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtProjectLeaderEdu" runat="server" Label="项目负责人安全生产继续教育数" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtProjectLeaderEduRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtFullTimeEdu" runat="server" Label="安全专职人员安全生产继续教育数" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtFullTimeEduRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtThreeKidsEduRate" runat="server" Label="安全生产三类人员继续教育覆盖率（%）" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtThreeKidsEduRateRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtUplinReportRate" runat="server" Label="上行报告(施工现场安全生产动态季报、专项活动总结上报、生产事故按时限上报)履行率（%）"
                        LabelWidth="250px" NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtUplinReportRateRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtKeyEquipmentTotal" runat="server" Label="重点装备总数" LabelWidth="250px"
                        NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtKeyEquipmentTotalRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtKeyEquipmentReportCount" runat="server" Label="重点装备安全控制检查报告数"
                        LabelWidth="250px" NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtKeyEquipmentReportCountRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtChemicalAreaProjectCount" runat="server" Label="化工界区施工作业项目数"
                        LabelWidth="250px" NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtChemicalAreaProjectCountRemark" runat="server" MaxLength="500"
                        Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtHarmfulMediumCoverCount" runat="server" Label="化工界区施工作业有害介质检测复测覆盖数"
                        LabelWidth="250px" NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtHarmfulMediumCoverCountRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtHarmfulMediumCoverRate" runat="server" Label="施工作业安全技术交底覆盖率（%）"
                        LabelWidth="250px" NoNegative="True" EmptyText="0">
                    </f:NumberBox>
                    <f:TextBox ID="txtHarmfulMediumCoverRateRemark" runat="server" MaxLength="500" Label="备注">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtRemark" runat="server" Label="备注" MaxLength="500" LabelWidth="250px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:FileUpload runat="server" ID="btnFullTimeManAttachUrl" EmptyText="请上传安全专职人员名单附件"
                        OnFileSelected="btnFullTimeManAttachUrl_Click" AutoPostBack="true" Label="安全专职人员名单附件"
                        LabelWidth="250px">
                    </f:FileUpload>
                    <f:ContentPanel ID="ContentPanel2" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="安全专职人员名单附件">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divFullTimeManAttachUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:FileUpload runat="server" ID="btnPMManAttachUrl" EmptyText="请上传项目经理人员名单附件" OnFileSelected="btnPMManAttachUrl_Click"
                        AutoPostBack="true" Label="项目经理人员名单附件" LabelWidth="250px">
                    </f:FileUpload>
                    <f:ContentPanel ID="ContentPanel3" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="项目经理人员名单附件">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divPMManAttachUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
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
            <f:Toolbar ID="Toolbar2" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:ToolbarFill ID="ToolbarFill2" runat="server">
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

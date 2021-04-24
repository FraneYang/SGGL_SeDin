<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpecialTermsConditions.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.SpecialTermsConditions" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>专用条款</title>
    <style>
        .formtitle .f-field-body {
            text-align: center;
            font-size: 20px;
            line-height: 1.2em;
            margin: 10px 0;
        }

        text {
            height: 30px;
            margin: 6px;
            /*width:300px;*/
            border-top-width: 0px;
            border-left-width: 0px;
            border-right-width: 0px;
            border-bottom-width: 1px;
        }
    </style>

    <script type="text/javascript">
        window.onload = function () {
            var otxt = document.getElementsByClassName("txt");
            otxt.onkeyup = function () {
                this.size = (this.value.length > 4 ? this.value.length : 4);
            }
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" Title="分包合同协议书"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label1" runat="server" Text="专用合同条款" CssClass="formtitle f-widget-header"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Panel ID="Panel1" runat="server" ShowHeader="false" ShowBorder="false">

                            <Items>
                                <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" ShowBorder="false">
                                    <f:Label runat="server" ID="Labe00" Text="11&nbsp;一般约定"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="lable" Text="1.1&nbsp;词语定义与解释  "></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label2" Text="1.1.1总包合同是指："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="TotalPackageContract" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label3" Text="1.1.2其他分包合同文件包括："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="OtherSubDocuments" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label4" Text="1.1.28作为施工场地组成部分的其他场所包括："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="OtherPlaces" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label5" Text="1.1.49分包合同适用的其他规范性文件："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="NormativeDocument" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label6" Text="1.3标准和规范"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label7" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;适用于分包工程的需要特别指出的标准和规范包括："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="StandardSpecification" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label8" Text="1.4图纸"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label9" Text="1.4.1总承包商向施工分包商提供图纸的期限："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="DrawingPeriod" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label10" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;总承包商向施工分包商提供图纸的数量："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="DrawingCount" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label11" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;总承包商向施工分包商提供图纸的内容："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="DrawingContents" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label12" Text="1.4.3深化设计"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label13" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;总承包商委托施工分包商进行深化设计的范围及费用承担："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="DeepeningDesign" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label14" Text="1.5施工分包商文件"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label15" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;施工分包商向总承包商提供施工文件的数量：一式肆份；（示例性描述）"></f:Label>
                                    <f:TextBox runat="server" Label="" ID="ConstructionSubFileCount" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label16" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;施工分包商向总承包商提供施工文件的形式:"></f:Label>
                                    <f:TextBox runat="server" Label="" ID="ConstructionSubFileForm" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label17" Text="1.6联络"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label18" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;总承包商接收文件的地点："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="GeneralContractorAddress" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label19" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;总承包商指定的接收人："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="GeneralContractorMan" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label20" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;施工分包商接收文件的地点："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="SubAddress" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label21" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;施工分包商指定的接收人："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="SubMan" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label22" Text="2总承包商"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label23" Text="2.2提供基础资料、施工条件"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label24" Text="2.2.2（5）总承包商负责提供的其他设施和条件："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="FacilitiesConditions" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label25" Text="2.3总承包商项目经理"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label26" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 姓名："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="ProjectManagerName" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label27" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 职称："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="ProjectManagerTitle" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label28" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 建造师执业资格等级："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="BuilderQualificationLevel" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label29" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 建造师注册证书号："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="BuilderRegistrationCertificate" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label30" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 联系电话："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="ProjectManagerTel" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label31" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 电子信箱："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="ProjectManagerEmail" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label32" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 通信地址："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="ProjectManagerAddress" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:TextArea runat="server" ID="ScopeAuthorization" EmptyText="" LabelWidth="300" Label="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 总承包商对项目经理的授权范围"
                                        AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                                    </f:TextArea>
                                    <br />
                                    <f:Label runat="server" ID="Label34" Text="3施工分包商"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label35" Text="3.1施工分包商的一般义务"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label36" Text="3.1.1由施工分包商办理的许可和批准手续包括："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="PermitsApprovals" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label37" Text="3.2施工分包商项目经理和其他主要项目管理人员"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label38" Text="3.2.1施工分包商项目经理信息："></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label39" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 姓名："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="SubProjectManagerName" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label40" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 职称："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="SubProjectManagerTitle" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label41" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 建造师执业资格等级："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="SubBuilderQualificationLevel" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label42" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 建造师注册证书号："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="SubBuilderRegistrationCertificate" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label43" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 联系电话："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="SubProjectManagerTel" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label44" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;电子信箱："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="SubProjectManagerEmail" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label45" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 通信地址："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="SubProjectManagerAddress" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:TextArea runat="server" ID="SubScopeAuthorization" EmptyText="" LabelWidth="330" Label="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 施工分包商对施工分包商项目经理的授权范围"
                                        AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                                    </f:TextArea>
                                    <br />
                                    <f:Label runat="server" ID="Label47" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 施工分包商组织机构配置和关键人员名单详见附件。"></f:Label>
                                    <f:TextBox runat="server" Label="" ID="AttachmentName" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label48" Text="3.2.2施工分包商项目经理每月在施工现场时间不得少于"></f:Label>
                                    <f:TextBox runat="server" Label="" ID="DaysNum" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <f:Label runat="server" ID="Label33" Text="  天 "></f:Label>
                                    <br />
                                    <f:TextArea runat="server" ID="DefaultResponsibility" EmptyText="" LabelWidth="400" Label="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 施工分包商项目经理兼任其他项目的项目经理的违约责任"
                                        AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                                    </f:TextArea>
                                    <br />
                                    <f:TextArea runat="server" ID="LeaveSiteResponsibility" EmptyText="" LabelWidth="320" Label="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 3.2.3施工分包商项目经理和其他主要项目管理人员擅自离开施工场地的违约责任"
                                        AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                                    </f:TextArea>
                                    <br />
                                    <f:Label runat="server" ID="Label51" Text="3.2.4施工分包商擅自更换施工分包商项目经理或其他主要项目管理人员的违约责任：详见附件。"></f:Label>
                                    <br />
                                    <f:TextArea runat="server" ID="RefusedChangeResponsibility" EmptyText="" LabelWidth="400" Label="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;施工分包商无正当理由拒绝更换施工分包商项目经理或其他主要项目管理人员的违约责任"
                                        AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                                    </f:TextArea>
                                    <br />
                                    <f:Label runat="server" ID="Label53" Text="3.3特殊工种上岗作业要求"></f:Label>
                                    <br />
                                    <f:TextArea runat="server" ID="WithoutCardMountGuard" EmptyText="" LabelWidth="320" Label="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;特殊工种作业人员无证上岗的违约责任"
                                        AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                                    </f:TextArea>
                                    <br />
                                    <f:Label runat="server" ID="Label55" Text="3.4禁止转包和再分包"></f:Label>
                                    <br />
                                    <f:TextArea runat="server" ID="SubcontractWorks" EmptyText="" LabelWidth="320" Label="3.4.1施工分包商转包分包工程的违约责任"
                                        AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                                    </f:TextArea>
                                    <br />
                                    <f:TextArea runat="server" ID="IllegalSubcontracting" EmptyText="" LabelWidth="320" Label="3.4.2施工分包商违法分包工程的违约责任"
                                        AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                                    </f:TextArea>
                                    <br />
                                    <f:Label runat="server" ID="Label58" Text="3.5履约担保"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label59" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;履约担保的方式："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="PerformanceWay" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label60" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 履约担保的金额："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="PerformanceMoney" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label61" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 履约担保的期限："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="PerformanceTimelimit" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label62" Text="3.6联合体"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label63" Text="3.6.2经总承包商确认的联合体协议详见附件。"></f:Label>
                                    <f:TextBox runat="server" Label="" ID="AssociationAgreementAttachUrl" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label64" Text="5分包工程质量"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label65" Text="5.1质量要求"></f:Label>
                                    <br />
                                    <f:TextArea runat="server" ID="SpecialStandards" EmptyText="" LabelWidth="320" Label="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 对于分包工程质量的特殊标准或要求"
                                        AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                                    </f:TextArea>
                                    <br />
                                    <f:Label runat="server" ID="Label67" Text="6HSE管理"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label68" Text="6.1安全文明施工"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label69" Text="6.1.1安全文明施工"></f:Label>
                                    <br />
                                    <f:TextArea runat="server" ID="ConstructionMeasures" EmptyText="" LabelWidth="320" Label="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 总承包商的安全文明施工措施包括"
                                        AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                                    </f:TextArea>
                                    <br />
                                    <f:TextArea runat="server" ID="SubMeasures" EmptyText="" LabelWidth="320" Label="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 施工分包商的安全文明施工措施包括"
                                        AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                                    </f:TextArea>
                                    <br />
                                    <f:Label runat="server" ID="Label72" Text="6.3职业健康"></f:Label>
                                    <br />
                                    <f:TextArea runat="server" ID="LabourCost" EmptyText="" LabelWidth="320" Label="6.3.3施工分包商拖欠其雇佣人员的劳动报酬或劳务分包单位的劳务费用应承担的违约责任"
                                        AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                                    </f:TextArea>
                                    <br />
                                    <f:Label runat="server" ID="Label74" Text="6.3.4施工分包商劳资专管员"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label75" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 姓名："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="LaborSupervisorName" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label76" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;职称："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="LaborSupervisorTitle" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label77" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;联系电话："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="LaborSupervisorTel" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label78" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;电子信箱："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="LaborSupervisorEmail" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label79" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;通信地址："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="LaborSupervisorAddress" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label80" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;劳务费用发放台账的格式及要求按照总承包商的要求执行，在项目现场确定。"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label81" Text="7工期和进度"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label82" Text="7.1施工组织"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label83" Text="7.1.1施工组织设计的编制和批准"></f:Label>
                                    <br />
                                    <f:TextArea runat="server" ID="OrganizationalDesign" EmptyText="" LabelWidth="380" Label="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 施工分包商未按时提交详细施工组织设计的违约责任"
                                        AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                                    </f:TextArea>
                                    <br />
                                    <f:TextArea runat="server" ID="Amendments" EmptyText="" LabelWidth="380" Label="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 总承包商对施工组织设计未按时确认或未按时提出修改意见的违约责任"
                                        AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                                    </f:TextArea>
                                    <br />
                                    <f:Label runat="server" ID="Label86" Text="7.1.3劳动力保障"></f:Label>
                                    <br />
                                    <f:TextArea runat="server" ID="Labour" EmptyText="" LabelWidth="380" Label="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 施工分包商未按时补足劳动力的违约责任"
                                        AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                                    </f:TextArea>
                                    <br />
                                    <f:Label runat="server" ID="Label88" Text="7.4工期延误"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label89" Text="7.4.2因施工分包商原因导致工期延误"></f:Label>
                                    <br />
                                    <f:TextArea runat="server" ID="WithinTimeLimit" EmptyText="" LabelWidth="380" Label=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;因施工分包商原因造成工期延误的，施工分包商应承担的逾期竣工违约责任："
                                        AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                                    </f:TextArea>
                                    <br />
                                    <f:Label runat="server" ID="Label91" Text="7.5不利物质条件"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label92" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;不利物质条件的其他情形："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="AdverseMaterialConditions" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label93" Text="7.6异常恶劣的气候条件"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label94" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 异常恶劣的气候条件包括："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="TextBox96" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label95" Text="8材料与设备"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label96" Text="8.1总承包商和施工分包商的供应范围："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="MaterialEquipmentSupplyRange" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label97" Text="8.3总承包商供应的材料与工程设备"></f:Label>
                                    <f:TextBox runat="server" Label="" ID="TextBox99" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label98" Text="8.3.2总承包商供应的材料设备，由施工分包商负责卸至指定地点的卸车费标准："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="UnloadingRateStandard" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label99" Text="8.3.3总承包商供应的其它材料设备从总承包商现场仓库出库到施工作业地点的二次搬运费标准："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="SecondaryHandlingCharges" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label100" Text="8.6样品"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label101" Text="8.6.1需要施工分包商报送样品的材料或工程设备，样品的种类、名称、规格、数量要求："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="SampleRequirements" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label102" Text="8.7材料与工程设备的替代"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label103" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 关于材料与工程设备替代的约定："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="AlternativeAgreed" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label104" Text="8.8施工设备和临时设施"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label105" Text="8.8.1总承包商提供的施工设备和临时设施包括："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="Equipment" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label106" Text="10分包合同变更"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label107" Text="10.1分包合同变更的范围"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label108" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;属于分包合同变更的其他情形："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="OtherCircumstances" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label109" Text="10.3分包合同变更估价"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label110" Text="10.3.1变更估价的原则"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label111" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;变更估价的其他原则："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="ChangeValuation" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label112" Text="10.4施工分包商的合理化建议"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label113" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;施工分包商提出的合理化建议降低了合同价格或者提高了工程经济效益的，奖励的方法和金额为："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="Reward" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label114" Text="10.5变更引起的工期调整"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label115" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 因变更引起工期变化的，确定增减工期天数的方法："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="IncreaseDecreasePeriod" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label116" Text="11合同价格"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label117" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（1）单价合同"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label118" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;合同单价包含的风险范围："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="RiskRange" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label119" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;风险范围以外合同价格的调整方法："></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label120" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;a. "></f:Label>
                                    <f:TextBox runat="server" Label="" ID="AdjustmentMethodA" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label121" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;b. "></f:Label>
                                    <f:TextBox runat="server" Label="" ID="AdjustmentMethodB" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label122" Text=" （2）总价合同"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label123" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;总价包含的风险范围："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="TotalPriceRiskRange" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label124" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;风险范围以外合同价格的调整方法："></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label125" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;a. "></f:Label>
                                    <f:TextBox runat="server" Label="" ID="TotalAdjustmentMethodA" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label126" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;b. "></f:Label>
                                    <f:TextBox runat="server" Label="" ID="TotalAdjustmentMethodB" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label127" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（3）其他价格形式："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="OtherPriceForms" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label128" Text="12价格调整"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label129" Text="12.1市场价格波动引起的调整"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label130" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;市场价格的波动范围："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="MarketPriceRange" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label131" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;市场价格波动超过约定，需要调差的工程设备和材料的范围："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="DifferenceRange" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label132" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;因市场价格波动，调整合同价格的方式："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="PricingWay" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label133" Text="13计量"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label134" Text="13.1计量原则和计量周期"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label135" Text="13.1.1工程量计算规则："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="QuantityCalculationRules" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label136" Text="14工程款支付"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label137" Text="14.1预付款"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label138" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;预付款支付比例或金额："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="AdvancePayment" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label139" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;预付款支付期限："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="AdvancePaymentPeriod" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label140" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;总承包商逾期支付预付款的违约责任："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="LatePaymentAdvance" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label141" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;关于预付款扣回的约定："></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label142" Text="14.3工程进度款支付"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label143" Text="14.3.2进度付款申请单应包括的内容："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="ProgressPaymentContents" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label144" Text="14.3.3进度款审核与支付（此部分是填空，但需要固定化）"></f:Label>
                                    <f:TextBox runat="server" Label="" ID="TextBox146" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:TextArea runat="server" ID="ProgressPaymentConvention" EmptyText="" LabelWidth="380" Label="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 关于进度款支付的其他特殊约定："
                                        AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                                    </f:TextArea>
                                    <br />

                                    <f:Label runat="server" ID="Label147" Text="16试车"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label148" Text="16.1试车的组织和配合（此部分是填空，但需要固定化）"></f:Label>
                                    <f:TextBox runat="server" Label="" ID="TextBox150" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label149" Text="（3）保运工作范围的约定："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="GuaranteedScopeWork" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label150" Text="16.3试车费用承担（枚举选择）"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label151" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;保运工作的费用标准（在所选项前打√）：□已包括在合同价款中；□双方另签工程保运协议；□本合同工程不涉及保运工作。"></f:Label>
                                    <f:TextBox runat="server" Label="" ID="GuaranteedCostStandard" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label152" Text="17竣工验收"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label153" Text="17.1竣工验收条件"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label154" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（4）申请竣工验收应具备的其他条件："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="AcceptanceCondition" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label155" Text="17.3竣工日期"></f:Label>
                                    <br />
                                    <f:TextArea runat="server" ID="UnqualifiedResponsibility" EmptyText="" LabelWidth="380" Label="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;分包工程自竣工验收合格至移交总承包商的期间未能保持质量合格状态的，施工分包商应承担的违约责任："
                                        AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                                    </f:TextArea>
                                    <br />
                                    <f:Label runat="server" ID="Label157" Text="17.4竣工退场"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label158" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;施工分包商对施工场地进行清理并退场的期限："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="CleanExitTimeLimit" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label159" Text="18分包工程移交"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label160" Text="18.1分包工程移交时间"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label161" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;合同当事人完成全部工程资料移交的期限："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="DataTransferTimeLimit" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label162" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;工程资料的套数、内容："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="DataNumContents" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label163" Text="19结算"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label164" Text="19.1结算申请"></f:Label>
                                    <br />
                                    <f:TextArea runat="server" ID="DataListing" EmptyText="" LabelWidth="380" Label="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;分包工程结算申请单需要的资料清单和份数："
                                        AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                                    </f:TextArea>
                                    <br />
                                    <f:Label runat="server" ID="Label166" Text="19.3最终结清"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label167" Text="19.3.1分包工程最终结清申请单需要的份数："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="FinalSettlementNum" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label168" Text="20缺陷责任期与保修期"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label169" Text="20.1缺陷责任期"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label170" Text="20.1.1缺陷责任期的起算日："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="DefectLiabilityDate" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label171" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;缺陷责任期的具体期限："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="DefectLiabilityPeriod" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label172" Text="20.2保修期"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label173" Text="20.2.1保修期的起算日："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="WarrantyPeriodDate" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label174" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 保修期具体期限："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="WarrantyPeriodPeriod" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label175" Text="20.3质量保证金"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label176" Text="20.3.1质量保证金的扣留金额和扣留方式："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="MarginDetainWay" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label177" Text="21违约"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label178" Text="21.1总承包商违约"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label179" Text="21.1.1总承包商违约时应支付的违约金或违约金的计算方法："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="DefaultMethod" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label180" Text="21.1.3因总承包商违约解除合同"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label181" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 因总承包商违约解除合同的，关于已完工程估价、清算和付款的约定："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="TerminationContract" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label182" Text="21.2施工分包商违约"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label183" Text="21.2.1施工分包商违约责任"></f:Label>
                                    <br />
                                    <f:TextArea runat="server" ID="DefaultLiability" EmptyText="" LabelWidth="380" Label="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;施工分包商违约时应支付的违约金或违约金的计算方法："
                                        AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                                    </f:TextArea>
                                    <br />
                                    <f:Label runat="server" ID="Label188" Text="21.2.3因施工分包商违约解除合同"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label189" Text=" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;因施工分包商违约解除合同的，关于分包工程估价、清算和付款的约定："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="SubDefaultCancelContract" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label190" Text="22不可抗力"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label191" Text="22.1不可抗力的确认和通知"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label192" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;不可抗力的其他情形："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="ForceMajeure" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label193" Text="22.2不被认为是不可抗力的情形"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label194" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（6）不被认为不可抗力的其他情形："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="NotConsideredForceMajeure" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label195" Text="22.5因不可抗力解除合同"></f:Label>
                                    <f:TextBox runat="server" Label="" ID="GeneralContractorShallPay" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label196" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;因不可抗力解除合同，总承包商应支付款项包括：双方就已经完成的工程进行工程竣工结算，总承包商按照通用合同条款19款【结算】的有关规定向施工分包商支付相应工程款。"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label197" Text="23保险"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label198" Text="23.4施工分包商负责提供的保险"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label199" Text="23.4.2施工分包商负责提供的保险包括但不限于："></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label200" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（2）施工分包商投保意外伤害保险的赔偿限额："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="LimitIndemnity" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label201" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（3）施工机具保险的保险金额："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="InsuredAmount" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label202" Text="23.5保险凭证"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label203" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;施工分包商提交保险凭证和保险单复印件的期限："></f:Label>
                                    <f:TextBox runat="server" Label="" ID="CertificateInsurance" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <br />
                                    <f:Label runat="server" ID="Label204" Text="25争议解决"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label205" Text="25.2仲裁或诉讼"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label206" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;因合同及合同有关事项发生的争议，按下列第1种方式解决："></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label207" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（1）向"></f:Label>
                                    <f:TextBox runat="server" Label="" ID="ArbitrationCommission" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <f:Label runat="server" ID="Label209" Text="仲裁委员会申请仲裁。"></f:Label>
                                    <br />
                                    <f:Label runat="server" ID="Label208" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（2）向"></f:Label>
                                    <f:TextBox runat="server" Label="" ID="PeopleCourt" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                                    <f:Label runat="server" ID="Label210" Text="人民法院起诉。（示例性描述）"></f:Label>
                                    <br />

                                </f:ContentPanel>
                            </Items>

                        </f:Panel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true"  ShowHeader="true" Title="专用合同条款附件" EnableCollapse="true"
                            runat="server" BoxFlex="1" DataKeyNames="AttachUrlId" AllowCellEditing="true"
                            ClicksToEdit="2" DataIDField="AttachUrlId" EnableColumnLines="true"
                            EnableTextSelection="True" OnPreRowDataBound="Grid1_PreRowDataBound" OnRowCommand="Grid1_RowCommand">
                            <Columns>
                                <f:CheckBoxField ColumnID="cbIsSelected" Width="100px" RenderAsStaticField="false"
                                    DataField="IsSelected" TextAlign="Center" CommandName="isSelected" />
                                <f:TemplateField ColumnID="tfPageIndex" Width="55px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                                    EnableLock="true" Locked="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:RenderField ColumnID="AttachUrlCode" DataField="AttachUrlCode" Width="150px" FieldType="String" HeaderText="附件编号" TextAlign="Center"
                                    HeaderTextAlign="Center">
                                </f:RenderField>
                                <f:RenderField ColumnID="AttachUrlName" DataField="AttachUrlName" Width="320px" FieldType="String" HeaderText="附件名称" TextAlign="Left"
                                    HeaderTextAlign="Center" ExpandUnusedSpace="true">
                                </f:RenderField>
                                <f:BoundField ColumnID="IsBuild" DataField="IsBuild" Hidden="true"></f:BoundField>
                                <f:LinkButtonField ColumnID="lbfEdit" Width="90px" TextAlign="Center" CommandName="edit" HeaderText="编辑" ToolTip="编辑附件" Icon="Pencil" />
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>

                        <f:Button ID="btnSave" ToolTip="保存" Icon="SystemSave" runat="server"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
        <f:Window ID="Window1" Title="编辑附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true"
            Width="1000px" Height="620px">
        </f:Window>
    </form>
</body>
</html>

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
         .lab {
            font-weight: bolder;

            background-color: aliceblue;
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
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" Title="专用合同条款"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label51" runat="server" Text="专用合同条款" CssClass="formtitle f-widget-header"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Labe00" Text="11&nbsp;一般约定" CssClass="lab"></f:Label>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="lable" Text="1.1&nbsp;词语定义与解释  "></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="1.1.1总包合同是指" LabelWidth="300" ID="TotalPackageContract" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="1.1.2其他分包合同文件包括" LabelWidth="300" ID="OtherSubDocuments" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="1.1.28作为施工场地组成部分的其他场所包括" LabelWidth="300" ID="OtherPlaces" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="1.1.49分包合同适用的其他规范性文件" LabelWidth="300" ID="NormativeDocument" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label56" Text="1.3标准和规范"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="适用于分包工程的需要特别指出的标准和规范包括" LabelWidth="300" ID="StandardSpecification" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label58" Text="1.4图纸"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="1.4.1总承包商向施工分包商提供图纸的期限" LabelWidth="300" ID="DrawingPeriod" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="总承包商向施工分包商提供图纸的数量" LabelWidth="300" ID="DrawingCount" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="总承包商向施工分包商提供图纸的内容" LabelWidth="300" ID="DrawingContents" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label62" Text="1.4.3深化设计"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="总承包商委托施工分包商进行深化设计的范围及费用承担" LabelWidth="300" ID="DeepeningDesign" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label64" Text="1.5施工分包商文件"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="施工分包商向总承包商提供施工文件的数量" LabelWidth="300" ID="ConstructionSubFileCount" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="施工分包商向总承包商提供施工文件的形式" LabelWidth="300" ID="ConstructionSubFileForm" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label67" Text="1.6联络"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="总承包商接收文件的地点" LabelWidth="300" ID="GeneralContractorAddress" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="总承包商指定的接收人" LabelWidth="300" ID="GeneralContractorMan" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="施工分包商接收文件的地点" LabelWidth="300" ID="SubAddress" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="施工分包商指定的接收人" LabelWidth="300" ID="SubMan" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label72" Text="2总承包商" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label73" Text="2.2提供基础资料、施工条件"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="2.2.2（5）总承包商负责提供的其他设施和条件" LabelWidth="300" ID="FacilitiesConditions" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label75" Text="2.3总承包商项目经理"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="姓名" LabelWidth="300" ID="ProjectManagerName" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="职称" LabelWidth="300" ID="ProjectManagerTitle" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="建造师执业资格等级" LabelWidth="300" ID="BuilderQualificationLevel" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="建造师注册证书号" LabelWidth="300" ID="BuilderRegistrationCertificate" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="联系电话" LabelWidth="300" ID="ProjectManagerTel" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="电子信箱" LabelWidth="300" ID="ProjectManagerEmail" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="通信地址" LabelWidth="300" ID="ProjectManagerAddress" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea runat="server" ID="ScopeAuthorization" EmptyText="" LabelWidth="300" Label="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 总承包商对项目经理的授权范围"
                            AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                        </f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label83" Text="3施工分包商" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label84" Text="3.1施工分包商的一般义务"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="3.1.1由施工分包商办理的许可和批准手续包括" LabelWidth="300" ID="PermitsApprovals" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label86" Text="3.2施工分包商项目经理和其他主要项目管理人员"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label87" Text="3.2.1施工分包商项目经理信息："></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="姓名" LabelWidth="300" ID="SubProjectManagerName" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="职称" LabelWidth="300" ID="SubProjectManagerTitle" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="建造师执业资格等级" LabelWidth="300" ID="SubBuilderQualificationLevel" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="建造师注册证书号" LabelWidth="300" ID="SubBuilderRegistrationCertificate" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="联系电话" LabelWidth="300" ID="SubProjectManagerTel" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="电子信箱" LabelWidth="300" ID="SubProjectManagerEmail" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="通信地址" LabelWidth="300" ID="SubProjectManagerAddress" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea runat="server" ID="SubScopeAuthorization" EmptyText="" LabelWidth="300" LabelAlign="right" Label="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 施工分包商对施工分包商项目经理的授权范围"
                            AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                        </f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="施工分包商组织机构配置和关键人员名单详见附件" LabelWidth="300" ID="AttachmentName" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="3.2.2施工分包商项目经理每月在施工现场时间不得少于" LabelWidth="300" ID="DaysNum" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                        <f:Label runat="server" ID="Label97" Text="  天 "></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea runat="server" ID="DefaultResponsibility" EmptyText="" LabelWidth="300" LabelAlign="Right" Label="施工分包商项目经理兼任其他项目的项目经理的违约责任"
                            AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                        </f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea runat="server" ID="LeaveSiteResponsibility" EmptyText="" LabelWidth="300" LabelAlign="Left" Label=" 3.2.3施工分包商项目经理和其他主要项目管理人员擅自离开施工场地的违约责任"
                            AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                        </f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label98" Text="3.2.4施工分包商擅自更换施工分包商项目经理或其他主要项目管理人员的违约责任：详见附件。"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea runat="server" ID="RefusedChangeResponsibility" EmptyText="" LabelWidth="300" LabelAlign="Right" Label="施工分包商无正当理由拒绝更换施工分包商项目经理或其他主要项目管理人员的违约责任"
                            AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                        </f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label99" Text="3.3特殊工种上岗作业要求"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea runat="server" ID="WithoutCardMountGuard" EmptyText="" LabelWidth="300" LabelAlign="Right" Label="特殊工种作业人员无证上岗的违约责任"
                            AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                        </f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label100" Text="3.4禁止转包和再分包"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea runat="server" ID="SubcontractWorks" EmptyText="" LabelWidth="300" LabelAlign="Left" Label="3.4.1施工分包商转包分包工程的违约责任"
                            AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                        </f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea runat="server" ID="IllegalSubcontracting" EmptyText="" LabelWidth="300" LabelAlign="Left" Label="3.4.2施工分包商违法分包工程的违约责任"
                            AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                        </f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label101" Text="3.5履约担保"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="履约担保的方式" LabelWidth="300" ID="PerformanceWay" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="履约担保的金额" LabelWidth="300" ID="PerformanceMoney" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="履约担保的期限" LabelWidth="300" ID="PerformanceTimelimit" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label105" Text="3.6联合体"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="3.6.2经总承包商确认的联合体协议详见附件" LabelWidth="300" ID="AssociationAgreementAttachUrl" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label107" Text="5分包工程质量" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label108" Text="5.1质量要求"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea runat="server" ID="SpecialStandards" EmptyText="" LabelWidth="300" LabelAlign="Right" Label=" 对于分包工程质量的特殊标准或要求"
                            AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                        </f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label109" Text="6HSE管理" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label110" Text="6.1安全文明施工"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label111" Text="6.1.1安全文明施工"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea runat="server" ID="ConstructionMeasures" EmptyText="" LabelWidth="300" LabelAlign="Right" Label="总承包商的安全文明施工措施包括"
                            AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                        </f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea runat="server" ID="SubMeasures" EmptyText="" LabelWidth="300" LabelAlign="Right" Label="施工分包商的安全文明施工措施包括"
                            AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                        </f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label112" Text="6.3职业健康"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea runat="server" ID="LabourCost" EmptyText="" LabelWidth="300" LabelAlign="Left" Label="6.3.3施工分包商拖欠其雇佣人员的劳动报酬或劳务分包单位的劳务费用应承担的违约责任"
                            AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                        </f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label113" Text="6.3.4施工分包商劳资专管员"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="姓名" LabelWidth="300" ID="LaborSupervisorName" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="职称" LabelWidth="300" ID="LaborSupervisorTitle" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="联系电话" LabelWidth="300" ID="LaborSupervisorTel" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="电子信箱" LabelWidth="300" ID="LaborSupervisorEmail" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="通信地址" LabelWidth="300" ID="LaborSupervisorAddress" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label119" Text="劳务费用发放台账的格式及要求按照总承包商的要求执行，在项目现场确定。"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label120" Text="7工期和进度" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label121" Text="7.1施工组织" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label122" Text="7.1.1施工组织设计的编制和批准"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea runat="server" ID="OrganizationalDesign" EmptyText="" LabelWidth="380" Label="施工分包商未按时提交详细施工组织设计的违约责任"
                            AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                        </f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea runat="server" ID="Amendments" EmptyText="" LabelWidth="380" Label="  总承包商对施工组织设计未按时确认或未按时提出修改意见的违约责任"
                            AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                        </f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label123" Text="7.1.3劳动力保障"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea runat="server" ID="Labour" EmptyText="" LabelWidth="380" Label=" 施工分包商未按时补足劳动力的违约责任"
                            AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                        </f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label124" Text="7.4工期延误"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label125" Text="7.4.2因施工分包商原因导致工期延误"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea runat="server" ID="WithinTimeLimit" EmptyText="" LabelWidth="380" Label=" 因施工分包商原因造成工期延误的，施工分包商应承担的逾期竣工违约责任："
                            AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                        </f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label126" Text="7.5不利物质条件"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="不利物质条件的其他情形" LabelWidth="300" ID="AdverseMaterialConditions" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label128" Text="7.6异常恶劣的气候条件"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="异常恶劣的气候条件包括" LabelWidth="300" ID="TextBox96" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label130" Text="8材料与设备" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="8.1总承包商和施工分包商的供应范围" LabelWidth="300" ID="MaterialEquipmentSupplyRange" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label52" Text="8.3总承包商供应的材料与工程设备" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="8.3.2总承包商供应的材料设备，由施工分包商负责卸至指定地点的卸车费标准" LabelWidth="300" ID="UnloadingRateStandard" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="8.3.3总承包商供应的其它材料设备从总承包商现场仓库出库到施工作业地点的二次搬运费标准" LabelWidth="300" ID="SecondaryHandlingCharges" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label135" Text="8.6样品"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label136" Text="8.6.1需要施工分包商报送样品的材料或工程设备，样品的种类、名称、规格、数量要求："></f:Label>
                        <f:TextBox runat="server" Label="" LabelWidth="300" ID="SampleRequirements" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label137" Text="8.7材料与工程设备的替代"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="关于材料与工程设备替代的约定" LabelWidth="300" ID="AlternativeAgreed" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label139" Text="8.8施工设备和临时设施"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="8.8.1总承包商提供的施工设备和临时设施包括" LabelWidth="300" ID="Equipment" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label141" Text="10分包合同变更" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label142" Text="10.1分包合同变更的范围"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="属于分包合同变更的其他情形" LabelWidth="300" ID="OtherCircumstances" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label144" Text="10.3分包合同变更估价"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label145" Text="10.3.1变更估价的原则"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="变更估价的其他原则" LabelWidth="300" ID="ChangeValuation" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label147" Text="10.4施工分包商的合理化建议"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="施工分包商提出的合理化建议降低了合同价格或者提高了工程经济效益的，奖励的方法和金额为" LabelWidth="300" ID="Reward" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label149" Text="10.5变更引起的工期调整"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="因变更引起工期变化的，确定增减工期天数的方法" LabelWidth="300" ID="IncreaseDecreasePeriod" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label151" Text="11合同价格" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label152" Text=" （1）单价合同"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="合同单价包含的风险范围" LabelWidth="300" ID="RiskRange" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label154" Text=" 风险范围以外合同价格的调整方法："></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="a" LabelWidth="300" ID="AdjustmentMethodA" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="b" LabelWidth="300" ID="AdjustmentMethodB" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label157" Text=" （2）总价合同"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="总价包含的风险范围" LabelWidth="300" ID="TotalPriceRiskRange" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label159" Text="  风险范围以外合同价格的调整方法："></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="a" LabelWidth="300" ID="TotalAdjustmentMethodA" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="b" LabelWidth="300" ID="TotalAdjustmentMethodB" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="（3）其他价格形式" LabelWidth="300" ID="OtherPriceForms" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label163" Text="12价格调整" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label164" Text="12.1市场价格波动引起的调整"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="市场价格的波动范围" LabelWidth="300" ID="MarketPriceRange" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="市场价格波动超过约定，需要调差的工程设备和材料的范围" LabelWidth="300" ID="DifferenceRange" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="因市场价格波动，调整合同价格的方式" LabelWidth="300" ID="PricingWay" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label168" Text="13计量" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label169" Text="13.1计量原则和计量周期"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="13.1.1工程量计算规则" LabelWidth="300" ID="QuantityCalculationRules" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label171" Text="14工程款支付" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label172" Text="14.1预付款"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="预付款支付比例或金额" LabelWidth="300" ID="AdvancePayment" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="预付款支付期限" LabelWidth="300" ID="AdvancePaymentPeriod" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="总承包商逾期支付预付款的违约责任" LabelWidth="300" ID="LatePaymentAdvance" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label176" Text="关于预付款扣回的约定："></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label177" Text="14.3工程进度款支付"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="14.3.2进度付款申请单应包括的内容" LabelWidth="300" ID="ProgressPaymentContents" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="14.3.3进度款审核与支付（此部分是填空，但需要固定化）" LabelWidth="300" ID="TextBox146" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea runat="server" ID="ProgressPaymentConvention" EmptyText="" LabelWidth="300" Label="关于进度款支付的其他特殊约定："
                            AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                        </f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>

                        <f:Label runat="server" ID="Label180" Text="16试车" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="16.1试车的组织和配合（此部分是填空，但需要固定化）" LabelWidth="300" ID="TextBox150" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="（3）保运工作范围的约定" LabelWidth="300" ID="GuaranteedScopeWork" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label183" Text="16.3试车费用承担（枚举选择）"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label184" Text=" 保运工作的费用标准（在所选项前打√）：□已包括在合同价款中；□双方另签工程保运协议；□本合同工程不涉及保运工作。"></f:Label>
                        <f:TextBox runat="server" Label="" LabelWidth="300" ID="GuaranteedCostStandard" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label185" Text="17竣工验收" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label186" Text="17.1竣工验收条件"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="（4）申请竣工验收应具备的其他条件" LabelWidth="300" ID="AcceptanceCondition" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label188" Text="17.3竣工日期"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea runat="server" ID="UnqualifiedResponsibility" EmptyText="" LabelWidth="300" Label="分包工程自竣工验收合格至移交总承包商的期间未能保持质量合格状态的，施工分包商应承担的违约责任："
                            AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                        </f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label189" Text="17.4竣工退场"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="施工分包商对施工场地进行清理并退场的期限" LabelWidth="300" ID="CleanExitTimeLimit" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label191" Text="18分包工程移交" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label192" Text="18.1分包工程移交时间"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="合同当事人完成全部工程资料移交的期限" LabelWidth="300" ID="DataTransferTimeLimit" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="工程资料的套数、内容" LabelWidth="300" ID="DataNumContents" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label195" Text="19结算" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label196" Text="19.1结算申请"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea runat="server" ID="DataListing" EmptyText="" LabelWidth="380" Label="分包工程结算申请单需要的资料清单和份数："
                            AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                        </f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label197" Text="19.3最终结清"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="19.3.1分包工程最终结清申请单需要的份数" LabelWidth="300" ID="FinalSettlementNum" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label199" Text="20缺陷责任期与保修期" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label200" Text="20.1缺陷责任期"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="20.1.1缺陷责任期的起算日" LabelWidth="300" ID="DefectLiabilityDate" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="缺陷责任期的具体期限" LabelWidth="300" ID="DefectLiabilityPeriod" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label203" Text="20.2保修期"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="20.2.1保修期的起算日" LabelWidth="300" ID="WarrantyPeriodDate" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="保修期具体期限" LabelWidth="300" ID="WarrantyPeriodPeriod" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label206" Text="20.3质量保证金"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="20.3.1质量保证金的扣留金额和扣留方式" LabelWidth="300" ID="MarginDetainWay" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label208" Text="21违约" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label209" Text="21.1总承包商违约"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="21.1.1总承包商违约时应支付的违约金或违约金的计算方法" LabelWidth="300" ID="DefaultMethod" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label211" Text="21.1.3因总承包商违约解除合同"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="因总承包商违约解除合同的，关于已完工程估价、清算和付款的约定" LabelWidth="300" ID="TerminationContract" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label213" Text="21.2施工分包商违约"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label214" Text="21.2.1施工分包商违约责任"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea runat="server" ID="DefaultLiability" EmptyText="" LabelWidth="380" Label="施工分包商违约时应支付的违约金或违约金的计算方法："
                            AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                        </f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label215" Text="21.2.3因施工分包商违约解除合同"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="因施工分包商违约解除合同的，关于分包工程估价、清算和付款的约定" LabelWidth="300" ID="SubDefaultCancelContract" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label217" Text="22不可抗力" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label218" Text="22.1不可抗力的确认和通知"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="不可抗力的其他情形" LabelWidth="300" ID="ForceMajeure" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label220" Text="22.2不被认为是不可抗力的情形"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="（6）不被认为不可抗力的其他情形" LabelWidth="300" ID="NotConsideredForceMajeure" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="22.5因不可抗力解除合同" LabelWidth="300" ID="GeneralContractorShallPay" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="left">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label223" Text="因不可抗力解除合同，总承包商应支付款项包括：双方就已经完成的工程进行工程竣工结算，总承包商按照通用合同条款19款【结算】的有关规定向施工分包商支付相应工程款。"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label224" Text="23保险" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label225" Text="23.4施工分包商负责提供的保险"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label226" Text="23.4.2施工分包商负责提供的保险包括但不限于："></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="（2）施工分包商投保意外伤害保险的赔偿限额" LabelWidth="300" ID="LimitIndemnity" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="（3）施工机具保险的保险金额" LabelWidth="300" ID="InsuredAmount" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label229" Text="23.5保险凭证"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="施工分包商提交保险凭证和保险单复印件的期限" LabelWidth="300" ID="CertificateInsurance" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label231" Text="25争议解决" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label232" Text="25.2仲裁或诉讼"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="Label233" Text="因合同及合同有关事项发生的争议，按下列第1种方式解决："></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="（1）向" LabelWidth="100" ID="ArbitrationCommission" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                        <f:Label runat="server" ID="Label235" Text="仲裁委员会申请仲裁。"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" Label="（2）向" LabelWidth="100" ID="PeopleCourt" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                            Width="120px" LabelAlign="right">
                        </f:TextBox>
                        <f:Label runat="server" ID="Label237" Text="人民法院起诉。（示例性描述）"></f:Label>
                    </Items>
                </f:FormRow>

                <f:FormRow>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" EnableAjax="false" ShowHeader="true" Title="专用合同条款附件" EnableCollapse="true"
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

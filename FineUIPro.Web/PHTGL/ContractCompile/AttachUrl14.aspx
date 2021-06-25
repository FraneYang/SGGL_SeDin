<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachUrl14.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.AttachUrl14" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑附件</title>
    <style>
        .widthBlod {
            padding-left:500px;   
        }
                .formtitle .f-field-body {
            text-align: center;
            font-size: 20px;
            line-height: 1.2em;
            margin: 10px 0;
        }
 
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="编辑附件14" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label1" runat="server" Text="附件14   施工安全管理协议书 " CssClass="formtitle f-widget-header" ></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" ShowBorder="false">
                            <f:Label runat="server" ID="Label3" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;为了贯彻“安全第一、预防为主、综合治理”的安全生产方针，明确总承包商与施工分包商双方的安全生产责任，确保施工的顺利进行，根据《中华人民共和国民法典》、《中华人民共和国安全生产法》、《中华人民共和国环境保护法》以及有关职业健康、安全、环境保护的法律、法规及总承包商相关管理规定，双方按照平等互利、协商一致的原则，订立本协议。 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label2" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 1工程项目概况"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label4" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.1工程名称: "></f:Label>
                             <f:TextBox  runat="server"  ID="txtProjectName"></f:TextBox>
                            <br />
                             <f:Label runat="server" ID="Label5" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.2工程内容:见主合同 "></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label6" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 2项目HSE目标"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label7" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 死亡事故“零指标”；"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label8" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 安全管理“零盲区”；"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label9" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 人身安全“零伤害” "></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label10" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 规章制度“零缺项”；"></f:Label>
                            <br />

                             <f:Label runat="server" ID="Label11" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;操作行为“零违章”； "></f:Label>
                            <br />

                             <f:Label runat="server" ID="Label12" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 生产设备“零隐患”。"></f:Label>
                            <br />

                             <f:Label runat="server" ID="Label13" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 3 安全组织机构与职责"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label14" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 3.1在合同实施过程中，合同双方应遵守国家、地方政府及行业颁布的各项法律法规、标准规范要求，按照《职业健康安全管理体系要求》（ ISO45001-2018）和《环境管理体系要求及使用指南》（GB/T24001-2016）的要求建立职业健康安全、环境管理体系，总承包商有权对施工分包商的职业健康安全管理体系、环境管理体系的运行情况随时进行审查。"></f:Label>
                            <br />

                             <f:Label runat="server" ID="Label15" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 3.2施工分包商应严格遵守业主及总承包商的职业健康安全、环境管理规定，并遵守总承包商发出的职业健康安全、环境指令。施工分包商有权拒绝业主、总承包商及监理人强令施工分包商违章作业、冒险施工的任何指令。"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label16" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 3.3总承包商负责项目职业健康安全、环境总体协调和管理，施工分包商负责本合同约定工作范围内职业健康安全、环境管理工作的具体实施。施工分包商应在项目开工后的一个月内将其《项目安全生产策划书》报总承包商审核、批准，项目实施过程中进行动态更新，同时应建立其安全生产责任制、治安保卫、安全培训、安全会议等制度，并对各项制度的执行情况如实进行记录，接受总承包商、业主监理人及政府安全监督部门的检查与监督。制度需覆盖其项目职业健康安全、环境管理的全部内容。"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label17" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 3.4总承包商负责组织建立项目安全生产委员会作为本项目职业健康安全、环境管理的最高管理机构，施工分包商项目部的主要管理人员（至少包含项目经理、施工经理、安全经理）应加入项目安全生产委员会，参与项目安全生产管理。"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label18" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 3.5施工分包商项目部安全管理人员的配置不少于"></f:Label>
                            <f:NumberBox ID="txtPersonAmount" runat="server"></f:NumberBox>
                             <f:Label runat="server" ID="Label33" Text="人（其中：安全经理  "></f:Label>
                           <f:NumberBox ID="txtSafetyManagerNumber" runat="server"></f:NumberBox>
                            <f:Label runat="server" ID="Label34" Text="名， "></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label19" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;专职负责安全培训及安全生产信息化管理系统管理人员 "></f:Label>
                            <f:NumberBox ID="txtSystemManagerNumber" runat="server"></f:NumberBox>
                            <f:Label runat="server" ID="Label175" Text="名）， "></f:Label>
                             <f:Label runat="server" ID="Label35" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 施工现场专职安全管理人员的数量按照作业人员与安全管理人员50：1人数要求配置，并按专业配备安全生产管理人员。"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label20" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4风险管理 "></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label21" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 4.1施工分包商应当按照《国务院安委会办公室关于实施遏制重特大事故工作指南构建双重预防机制的意见》（安委办〔2016〕11号）建立完善风险辨识分级管控和生产安全事故隐患排查治理双重预防机制。对本项目施工过程中存在的风险进行辨识、分析，按不同施工阶段建立危险源、环境因素识别与风险控制清单，项目开工后的一个月内提交总承包商查。"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label22" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 4.2对于重大危险源、重要环境因素，施工分包商应在施工方案中明确专项控制措施，总承包商应对控制措施的正确性和适宜性进行审查，检测控制措施的执行情况。同时应在现场设置重大危险源、重要环境因素告知牌，并对作业人员进行专项培训。"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label23" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 4.3施工分包商应于每日下班前对次日的施工作业进行风险研判并提交总承包商审查，总承包商审查后将在安全生产信息化管理系统发布。"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label24" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 5 主要职业健康安全、环境管理制度"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label25" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 5.1 安全教育培训"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label26" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 5.1.1 总承包商负责施工分包商人员的入场安全培训。施工分包商人员进入现场前，应提前通知总承包商，并提交相关人员资料（身份证复印件、体检报告、人员保险、特种作业人员资格证件等），总承包商在资料审查合格后应尽快对施工分包商人员进行入场培训，施工分包商人员不得在未进行入场安全培训的情况下进入现场工作。"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label27" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.1.2施工分包商应确保做好项目级、班组级安全教育，并保留书面记录，确保各岗位员工都具备从事该岗位所须的安全生产知识和技能。 "></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label28" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 5.1.3施工分包商在项目实施过程中应按总承包商的要求开展不间断的专项安全培训，并要求相应岗位的员工参加，不得因此提出关于工时损失的索赔要求。"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label29" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.2 安全会议 "></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label30" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 5.2.1施工分包商的项目经理、施工经理、安全经理应参加由总承包商组织召开的每月项目安全生产委员会会议，不得无故缺席。"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label31" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.2.2施工分包商安全经理（必要时邀请施工经理或项目经理）应参加由总承包商组织召开的周安全会议，不得无故缺席。 "></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label32" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 5.2.3 施工分包商应按照业主、监理、总承包商的要求参加安全专项会议。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label36" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.2.4施工分包商应当以班组为单位在每日上午施工前组织召开当日班前会，会上就当日的工作进行危害分析和风险告知，明确安全控制措施和要求，并应当由每位员工签字确认。班前会开会照片、会议内容及签到表须当日上传至总承包商项目安全信息管理平台。 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label37" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 5.2.5施工分包商应对上述各类型会议提出的问题按会议要求采取相应措施，总承包商应对此展开跟踪行动。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label38" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 5.3安全检查"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label39" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 5.3.1总承包商与施工分包商应当开展定期、不定期的职业健康安全、环境检查活动，以确保及时发现隐患。施工分包商应根据总承包商的检查要求指派相应专业人员参加。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label40" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.3.2对于检查中发现的问题、事故隐患，施工分包商必须在规定期限内完成整改，并向总承包商提交整改报告，总承包商应核实其整改情况。 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label41" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 5.3.3总承包商有权随时对施工分包商的各类职业健康安全、环境管理记录、统计数据及其他相关资料进行审查，对审查中发现的问题，施工分包商应当予以及时改进。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label42" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.4 安全报告 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label43" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 5.4.1施工分包商应当按照总承包商的要求于每月24日前向总承包商提交安全管理月报、安全费用月报。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label44" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 5.4.2 施工分包商应按照总承包商的要求提交各类有关职业健康安全、环境的报告（如安全月总结、年度安全管理总结、应急演练计划及总结等）。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label45" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.4.3 报告的内容应当客观、真实、准确。 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label46" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6 施工作业安全管理"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label47" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.1危大工程管理"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label48" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.1.1 施工分包商应按照《危险性较大的分部分项工程安全管理规定》（住建部〔2018〕37号令）及《危险性较大的分部分项工程安全管理规定》有关问题的通知（建办质〔2018〕31号）编制专项施工方案，专项施工方案应当由施工单位技术负责人审核签字、加盖单位公章，对于超过一定规模的危大工程，施工单位应当组织召开专家论证会对专项施工方案进行论证。 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label49" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.1.2施工分包商应当在施工现场显著位置公告危大工程名称、施工时间和具体责任人员，并在危险区域设置安全警示标志。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label50" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.1.3专项施工方案实施前，编制人员或者项目技术负责人应当向施工现场管理人员进行方案交底；施工现场管理人员应当向作业人员进行安全技术交底，并由双方和项目专职安全生产管理人员共同签字确认。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label51" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.1.4施工分包商应当按照规定对危大工程进行施工监测和安全巡视，发现危及人身安全的紧急情况，应当立即组织作业人员撤离危险区域。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label52" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.2作业许可管理 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label53" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.2.1本项目施工阶段，施工分包商应严格执行总承包商作业许可管理制度，包括但不局限于以下作业： "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label54" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1）高处作业 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label55" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 2）动火作业"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label56" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 3）受限空间作业"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label57" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 4）动土作业"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label58" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 5）吊装作业"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label59" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6）射线探伤作业"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label60" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 7）吹扫，试压作业"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label61" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 8）夜间施工作业"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label62" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 9）断路（占道）作业"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label63" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.2.2施工作业前施工分包商应向总承包商申请作业许可，经总承包商批准后方可施工。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label64" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.2.3 在项目试运行阶段、试生产阶段或到业主生产装置区进行施工作业时应执行业主的相关制度、要求。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label65" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.3 特种（特种设备）作业人员管理"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label66" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.3.1施工分包商的电工、登高架设工、起重工、焊接切割人员、叉车操作人员、吊车操作人员、起重机械安装拆卸人员、高处作业吊篮安装拆卸人员等特种作业人员进场施工前应将其操作证报总承包商处审查，特种（特种设备）作业人员操作证能通过全国统一证书查询系统（进入应急管理部政府网站http://www.chinasafety.gov.cn,依次点击 “服务”—“服务大厅”—“特种作业操作证及安全生产知识和管理能力考核合格信息查询”，进入查询平台）。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label67" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.3.2 特种（特种设备）作业人员应在其操作证许可范围内进行作业，不得超范围、无证作业。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label68" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.4临时用电管理"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label69" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.4.1临时用电按照《施工现场临时用电安全技术规范》JGJ 46-2005及中国化学工程集团有限公司《工程项目现场临时用电安全技术规范》编制临时用电组织设计，经具有法人资格的企业技术负责人审批后实施。临时用电设施安装完成后送电前，应由总承包商安全经理组织相关人员进行验收，验收合格之后方可送电。未经验收不得私自接送电。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label70" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.4.2施工分包商应定期检查用电设施，以确保其状态完好。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label71" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.4.3临时用电配电房及各级配电箱的配置还需符合业主单位要求。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label72" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.5脚手架管理"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label73" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.5.1 扣件式钢管脚手架材料、构配件应符合《建筑施工扣件式钢管脚手架安全技术规范》（JGJ 130-2011）要求，脚手架钢管应有产品质量合格证、质量检验报告。进场时，总承包商可对钢管的外径、壁厚、端面等的偏差进行复检，不符合规范要求不得使用。钢管应涂刷防锈油漆（用于搭设脚手架的钢管应涂成黄色、临边防护的脚手架钢管应涂刷红白相间漆）；扣件应有产品合格证、抽样复试报告。冲压钢脚手板应有质量合格证；木脚手板的宽度、厚度等应符合规范要求。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label74" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.5.2脚手架搭设前，施工分包商应将脚手架施工方案报总承包商审核、批准，属于危大工程范围的脚手架工程，按照6.1相关规定执行。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label75" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.5.3施工分包商应建立脚手架管理制度，对脚手架的搭设、修改、拆除、检查验收进行管理，脚手架必须悬挂脚手架标识牌，用于标识脚手架的状况，标识牌分为红牌、绿牌两种类型。 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label76" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.6 施工机械设备管理 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label77" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.6.1 施工分包商在施工机械设备进场前应将合格证、检验检测证明文件、保险等相关资料报总承包商审查，审查合格后由总承包商组织进场前的验收检查，验收合格后粘贴《机械设备检查合格标签》。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label78" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.6.2施工分包商应在施工机械设备使用过程中，指定专人每月进行检查验收并更换色标，设备的操作手应每日作业前对设备进行检查。发现故障时，必须立即停止使用，进行维修或报废，严禁设备设施带病运行。 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label79" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.7 职业健康与环境保护"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label80" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.7.1施工分包商应为其员工提供必要的膳宿条件和生活设施，并保证环境优雅舒适，消防设施完善。施工作业区、材料存放区与办公、生活区应划分清晰，并应采取相应的隔离措施；在建工程不得兼做宿舍；宿舍、办公用房的防火等级应符合规范要求；宿舍应设置可开启式窗户，床铺不得超过2 层，通道宽度不应小于0.9m；宿舍内住宿人员人均面积不应小于2.5 ㎡，且不得超过16 人。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label81" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.7.2施工分包商应采取有效措施预防传染病，保证施工人员的健康，并定期对施工现场、施工人员生活基地和工程进行防疫和卫生的专业检查和处理。 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label82" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.7.3施工分包商应按照法律规定保障现场施工人员的职业健康，并应按国家有关劳动保护的规定，采取有效的防止粉尘、降低噪声、控制有害气体和保障高温、高寒作业安全等劳动保护措施。施工分包商应于现场配备必要的伤病防治药品和设施。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label83" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.7.4施工分包商须在施工组织设计中列明环境保护的具体措施。在工程实施过程中，施工分包商应采取合理措施保护施工现场环境。对施工作业过程中可能引起的大气、水、噪音以及固体废物污染采取具体可行的防范措施。 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label84" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.7.5施工分包商负责承担因其原因引起的环境污染侵权损害赔偿责任，因上述环境污染引起纠纷而导致暂停施工的，由此增加的费用和（或）延误的工期由施工分包商承担。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label85" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.7.6 施工分包商应在现场设置足够数量的垃圾箱，施工垃圾及生活垃圾应分类存放，及时清运出现场。 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label86" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.8消防管理 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label87" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.8.1施工分包商应严格遵守《建设工程施工现场消防安全技术规范》GB 50720-2011、《石油化工建设工程施工安全技术规范》GB50484-2008及业主的有关规定。 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label88" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.8.2施工分包商应于施工区域及生活区内设置消防通道并保持畅通，配备消防设施，指派专人检查和维护，确保消防设施随时可以投入使用。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label89" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.8.3施工分包商在进行动火作业时，必须清除作业点周边可燃易燃物或采取充分的隔离措施。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label90" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.8.4动火作业区域必须配灭火器材。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label97" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.9文明施工  "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label91" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.9.1施工分包商应按照批准的施工平面布置图对进行施工机具设备安装、材料存放、道路规划，主要道路及材料加工区地面应进行硬化处理，道路应畅通，路面应平整坚实，施工机具、材料存放整齐、有序，材料标识牌应标明名称、规格等，并应采取防火、防雨、防锈蚀等措施。材料标识牌规格为400mmX260mm，铝塑板材质，牌上需注明材料名称、规格型号、材质、数量、生产日期、进场时间、使用单位、检验状况、生产厂家、负责人等。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label92" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.9.2施工分包商应每日做到“工完、料尽、场地清”。如施工分包商不能按总承包商要求及时清理施工区域内的垃圾、费料时，总承包商有权采取措施代为清理，所发生的费用将从施工分包商工程进度款中扣除，这种情况下，并不因此免除施工分包商的义务和责任。   "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label93" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.9.3施工分包商应制定具体的施工扬尘污染防治实施方案，并于开工后一个月内提交总承包商审核、批准。施工现场大门口或办公临建区域需设置扬尘在线监测系统，对颗粒物PM10、PM2.5、颗粒物、温度、风速、风向等数据进行实时监测，并在LED屏幕进行数据显示。现场围档及土方作业区域布置雾化喷淋装置，喷头间距不得大于6米，喷淋装置覆盖不到的区域设置移动喷雾装置。喷淋装置能够手动控制，也能与检测系统联动，当监测数据超过设定预警值时，能够自动开启喷淋降尘系统，降低粉尘深度，改善现场环境。施工现裸露场地和堆放的土方应采取覆盖、固化、绿化等措施。如采用盖土网覆盖，应采用6针盖土网进行全面覆盖。"></f:Label>                             <br />
                            <br />  <br />
                            <f:Label runat="server" ID="Label94" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.9.4运输易产生扬尘的物料例如土方和建筑垃圾时，应采用封闭式运输车辆或采取覆盖措施。水泥和其他易飞扬的细颗粒建筑材料应密闭存放或采取覆盖等措施。建筑物内施工垃圾的清运，应采用器具和管道运输，严禁随间抛掷"></f:Label>                             <br />
                            <br /> 
                            <f:Label runat="server" ID="Label95" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.9.5 施工现场产生的泥浆和污水未经处理不得直接排入排水系统。 "></f:Label>                             <br />
                            <br /> 
                            <f:Label runat="server" ID="Label96" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.9.6施工现场严禁焚烧各类废弃物。"></f:Label>                             <br />
                            <br /> 
                            <f:Label runat="server" ID="Label98" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.9.7项目现场大门口处设置五牌一图，即工程概况牌、管理人员名单及监督电话牌、消防安全制度牌、安全生产制度牌、文明和环保制度牌以及施工现场总平面图。五牌一图的尺寸按照7.1款所列的标准执行，材质为不锈钢管、铝塑板、铝芯板、工业PVC板、铁板。 "></f:Label>                             <br />
                            <br />  
                            <f:Label runat="server" ID="Label99" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.9.8钢筋、木工加工棚要求地面混凝土硬化或石子覆盖，搭设具体做法见中国化学标准化工地建设有关文件。"></f:Label> 
                            <br />
                            <f:Label runat="server" ID="Label100" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.9.9办公临建区域需设立旗台，悬挂国旗、中国化学集团公司旗。办公临建区域外部及内部需设绿化带，门口设对外宣传牌。 "></f:Label>  
                            <br />
                            <f:Label runat="server" ID="Label101" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 6.9.10施工现场应设置不少于一个吸烟亭及茶水房。 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label102" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.9.11在工程移交之前，施工分包商应当从施工现场清除施分包商的全部施工机具设备、多余的材料、垃圾及其他各种临时设施，并保持施工现场清洁整齐。   "></f:Label> 
                            <br />
                            <f:Label runat="server" ID="Label103" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 7安全标准化管理   "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label104" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;7.1现场施工要严格遵守《建筑施工安全检查标准》（JG59）、《石油化工建设工程施工安全技术规范》（GB50484）、《工程项目现场安全防护设施标准》（中国化学工程集团有限公司）、《工程项目现场临建设施标准》（中国化学工程集团有限公司）、业主管理手册等有关文明施工管理的相关规定。   "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label105" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;7.2施工现场实行封闭管理，工地与周边应设置高度不小于1.8米的彩板封闭围档（需要时，外侧立面满铺仿真植物绿毯，或文化宣传牌）。见下图：   "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label106" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></f:Label>                             <br />
                            <f:Label runat="server" ID="Label107" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;7.3施工分包商应为进入现场人员应配备安全帽、工作服、劳保鞋。安全帽的颜色要求：管理人员为白色、安全管理人员为红色、作业人员为黄色。工作服的颜色要求：专职安全管理人员为橘红色、其他管理人员为灰色、作业人员为蓝色。安全鞋应具备防砸、防穿刺、防滑、耐油等性能并符合《个体防护装备职业鞋》（GB21146）的要求。 "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label108" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 7.4项目现场要求使用五点式、双大钩、带缓冲包的安全带。   "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label109" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 8治安保卫  "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label110" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.1总承包商负责装置界区内整体治安保卫工作的整体策划，施工分包商负责******区域封闭围档、门楼、保安室、门禁系统、监控设施安装及日常维护工作，并设置不少于****名保安人员。施工分包商负责本单位施工区域及生活区内的治安保卫工作。围档具体做法要求参见7.2条，保安室不得小于3.6米X2.5米，具体做法见中国化学工程集团有限公司《工程项目现场安全防护设施标准》P281。"></f:Label>                             <br />
                            <f:Label runat="server" ID="Label111" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 人流大门与物流大门分开。人流大门门禁需采用人脸识别系统，门禁不少于三道，由****标段施工分包商指派专员进行人员录入及日常维护，未经识别人员严禁进入施工现场。物流大门采用电闸门，施工用车辆经总承包商处审核后方可进入施工现场。私人车辆严禁入场。  "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label112" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.2总承包商的治安保卫措施不能降低或取代施工分包商自身的治安保卫，合同双方应各自保管好自身的财产，并对自身管理不力造成的财产损失负责。   "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label113" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 9智慧化工地 "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label114" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 总承包商负责智慧化工地的总体策划，如视频监控系统、安全管理人员定位系统、塔机安全监控系统、现场广播系统、环境监测、易检系统、门禁系统等。施工分包商应按照总包的总体策划要求，配置其范围内的设备、设施，并进行日常维护管理。  "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label115" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;总承包商负责智慧化工地的总体策划。施工分包商在本项目的管理人员须全员参与招标人组织的智慧工地技术应用中，并安排专职人员对接招标人进行智慧工地管理的软件技术应用，主要包含但不限于：视频监控系统、安全管理人员定位系统、塔机安全监控系统（若需）、现场广播系统、环境监测和控制系统、易检系统、门禁系统；及施工分包商项目施工管理过程中应用的施工质量管理、施工HSE管理、焊接管理软件及APP程序应用。"></f:Label>                             <br />
                            <f:Label runat="server" ID="Label116" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 施工分包商须配合总承包商在项目施工管理过程中，安排督促所负责标段施工人员落实执行，安装应用相关施工质量管理、施工HSE管理、焊接管理PC端和手机端程序，并服从招标人对项目的软件应用统一管理。"></f:Label>                             <br />
                            <f:Label runat="server" ID="Label117" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 9.1 视频监控系统 "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label118" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 在施工界区内安装一套有效的高清视频监控系统，视频监控应覆盖主要施工作业区域、材料堆放区域、大门出入口，并可以对现场的视频监控进行远程控制。现场设置视频监控室，视频信号应传至现场视频监控室，监控室内配置一台不小于 55 寸的液晶电视实时显示监控画面，视频数据存储必须满足一周的保存时间，能够按照图像通道、日期等检索条件进行回放查询。  "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label119" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 9.2 安全管理人员定位系统 "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label120" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 所有安全管理人员应配备人员定位设备，该设备应具备人员实时定位，移动轨迹回放等功能，该设备能够与总包方建立的人员定位系统完美兼容。   "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label121" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 9.3 塔机安全监控系统（若需）   "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label122" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 设置一套塔机安全监控系统，应具备以下主要功能："></f:Label>                             <br />
                            <f:Label runat="server" ID="Label123" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; （1）塔机运行数据采集：通过精密传感器实时采集吊重、变幅、高度、回转、环境风速等多项安全作业工况数据；"></f:Label>                             <br />
                            <f:Label runat="server" ID="Label124" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; （2）工作状态实时显示：通过显示屏以图形数值方式实时显示当前实际工作参数和塔机额定工作能力参数；   "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label125" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; （3）单机运行状态监控：监控单台塔机的运行安全指标，在临近额定限值时发出声光预警和报警；   "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label126" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; （4）群塔防碰撞监控：塔司可直观全面地掌握周边塔机与本机当前干涉情况，并在发现碰撞危险时自动进行声光预警和报警；   "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label127" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; （5）远程可视化监控平台：塔机运行数据和报警信息通过无线网络实时传送回监控平台，实现塔机安全运行可视化远程监控；   "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label128" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; （6）塔司身份识别：支持 IC 卡、虹膜、人脸、指纹等识别方式，认证成功后方可操作塔机；   "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label129" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; （7）作业面可视化：实时监控塔吊作业区域的工作情况，使吊钩下方所钓重物的视频图像清晰地呈现在塔吊驾驶舱内的显示器上，从而指导司机的吊物操作。 "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label130" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 9.4 现场广播系统 "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label131" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 能够将需要公告的讯号无阻碍地传送给工人，不受工人主动性的影响。系统由遥控寻呼话筒，调谐器，前置放大器，贮备切换器，双通道功放，外置音响等组成。能够实现安全教育、应急广播等。 "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label132" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 9.5环境监测、控制系统   "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label133" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 实时采集气象数据，监测项目施工现场环境。为建筑工地提供工地环境监测服务，对颗粒物 PM10、PM2.5、颗粒物、温度、风速、风向等数据进行实时监测，现场大门处设置 LED 屏幕进行数据显示。"></f:Label>                             <br />
                            <f:Label runat="server" ID="Label134" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 现场围挡及土方作业区域布置雾化喷淋装置，喷头间距不得大于 6m。喷淋装置覆盖不到的区域设置移动喷雾装置。喷淋装置能够手动控制，也能与监测系统联动，当监测数据超过设定预警值时，能够自动开启喷淋降尘系统，降低粉尘浓度，改善现场环境。 "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label135" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 9.6易检系统"></f:Label>                             <br />
                            <f:Label runat="server" ID="Label136" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 检查人员能够通过移动端发布项目需整改的安全检查事项，实时跟进项目的整改情况，对问题的整改过程进行跟踪、指导以及最终确定闭环。该系统能够接入至业主方提供的项目管理平台。"></f:Label>                             <br />
                            <f:Label runat="server" ID="Label137" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 9.7门禁系统"></f:Label>                             <br />
                            <f:Label runat="server" ID="Label138" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 在施工界区出入口设置实名制考勤通道，采用人脸识别形式对进场人员进行识别，确保进场人员身份唯一。设置 LED 显示屏（屏幕尺寸不小于2m×3m），能够同步、准确显示各类进出人员及人数等信息。该系统除了对进场人员进行出入控制外，可以记录人员出入现场的次数、时间等信息，同时支持多种条件的查询方式，可以定期生成报表进行归档。实现人员进出的自动打卡考勤，统计现场资源情况。 "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label139" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 该系统应能够接入至总承包方提供的项目管理平台。"></f:Label>                             <br />
                            <f:Label runat="server" ID="Label140" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 9.8施工质量管理软件（招标人提供） "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label141" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 施工质量管理建立PC端和手机端软件应用，其主要功能包括：①工序共检；②质量巡检；③质量不合格管理；④工程联系单；⑤设计变更；⑥人员信息管理；⑦入场材料设备复检。   "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label142" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 9.9施工HSE管理软件（招标人提供）"></f:Label>                             <br />
                            <f:Label runat="server" ID="Label143" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 施工HSE管理建立PC端和手机端软件应用，其主要功能包括：①安全检查；②安全会议；③作业许可；④入场培训；⑤安全巡检等功能。 "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label144" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 9.10焊接管理软件（招标人提供）  "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label145" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 焊接管理主要功能涉及人脸识别领料，需投标人配合安排相关焊接人员进行人脸信息录入和焊工考试等工作。   "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label146" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 10应急管理 "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label147" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 10.1 施工分包商应参照总承包商综合应急预案编制自身的综合应急预案、专项应急预案，并在项目开工后的一个月内报总承包商审核、批准，项目实施过程中进行动态更新。"></f:Label>                             <br />
                            <f:Label runat="server" ID="Label148" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 10.2 当紧急情况发生时，施工分包商应立即启动应急预案，同时向总承包商报告。"></f:Label>                             <br />
                            <f:Label runat="server" ID="Label149" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 10.3 项目实施过程中，施工分包商应每半年开展不少于1次的应急演练活动，并提前7天向总承包商提交演练计划，经批准后方可进行应急演练。"></f:Label>                             <br />
                            <f:Label runat="server" ID="Label150" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 11安全激励与处罚  "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label151" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 11.1安全奖励   "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label152" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 11.1.1按照总承包商的项目安全奖惩管理规定，项目实施的安全奖励类型有安全目标实现奖、安全工时奖、竞赛奖励、绩效考核奖励、安全生产先进个人奖等。  "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label153" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 11.2 安全处罚  "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label154" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 11.2.1总承包商有权对施工分包商的安全违章行为和管理不足进行不同程度的处罚。施工分包商在本项目实施过程中发生安全生产事故，总承包商有权根据事故严重程度、事故责任认定及事故预防措施的执行情况等，对施工分包商处以清除人员出场、罚款、通报批评等处罚措施，直至终止本合同，施工分包商不得因此提出任何索赔要求。   "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label155" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 11.2.2施工分包商的管理人员安全工作不尽责导致作业现场混乱、险情频发、工作不力，总承包商有权提出更换，施工分包商须在5日内更换。 "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label156" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 11.2.3对于施工分包商作业人员的严重违章或一般违章3次以上的，总承包商有权将其清除出现场。"></f:Label>                             <br />
                            <f:Label runat="server" ID="Label157" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 11.2.4施工分包商发生死亡事故，按照国家和地方政府有关法律法规予以责任追究和相应赔偿。对于因施工分包商管理缺陷造成的死亡事故，每死亡一人，从施工分包商工程进度款中扣除人民币30万元作为罚款。  "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label158" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 11.2.5 施工分包商发生火灾、爆炸、设备损毁、财产损失、环境污染以及人员伤害等事故，直接导致本项目安全管理目标无法实现的，核算包括直接损失、误工、医疗等在内的总损失额后依事故分析及责任认定结果处以10万元以上30万元以下的罚款。   "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label159" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 11.3其它奖惩措施根据项目关于安全奖惩的规定执行。 "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label160" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 12社会责任 "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label161" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 12.1合同双方在本项目实施过程中应坚持依法经营诚实守信。模范遵守法律法规和社会公德、商业道德以及行业规则，及时足额纳税，维护投资者和债权人权益，保护知识产权，忠实履行合同，恪守商业信用，反对不正当竞争，杜绝商业活动中的腐败行为。"></f:Label>                             <br />
                            <f:Label runat="server" ID="Label162" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 12.2合同双方在本项目中应尊重项目所在地居民生活，积极参与社区建设，鼓励职工志愿服务社会。在发生重大自然灾害和突发事件的情况下，积极提供财力、物力和人力等方面的支持和援助。"></f:Label>                             <br />
                            <f:Label runat="server" ID="Label163" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 12.3施工分包商应避免在工程实施过程中所造成的各种扰民、干扰公共道路通行、破坏当地习俗以及影响其它单位的正常工作。 "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label164" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 12.4施工分包商应当依法与职工签订并履行劳动合同，坚持按劳分配、同工同酬。尊重职工人格，公平对待职工，杜绝性别、民族、宗教、年龄等各种歧视。关心职工现生活，为工排忧解难。 "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label165" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 13协议的履行期限  "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label166" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 本协议的履行期限与主合同保持一致。如果主合同因故需要变更期限，本协议与之变更至相同期限。   "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label167" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 14协议的变更、解除或终止  "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label168" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 本协议与主合同具有同等的法律效力，本协议随主合同的变更、解除或终止而变更、解除或终止。"></f:Label>                             <br />
                            <f:Label runat="server" ID="Label169" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 15解决争议的方法  "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label170" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 本协议在履行过程中发生争议，按照主合同约定的争议解决方式处理。"></f:Label>                             <br />
                            <f:Label runat="server" ID="Label171" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 16附则   "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label172" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 16.1本协议与主合同分数相同，每份具有同等法律效力。"></f:Label>                             <br />
                            <f:Label runat="server" ID="Label173" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 16.2本协议与主合同同时生效，并作为主合同的组成部分。   "></f:Label>                             <br />
                            <f:Label runat="server" ID="Label174" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></f:Label>
                            <br />
                            <br />
                            <f:Label runat="server" ID="Label176" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;总承包商：                                          施工分包商："></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label177" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（公章或合同专用章）                                         （公章或合同专用章）"></f:Label>
                            <f:Label runat="server" ID="Label178" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;法定代表人或其委托代理人：                                         法定代表人或其委托代理人："></f:Label>
                            <br />
                            <br />
                            <f:Label runat="server" ID="Label179" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日期：    年    月    日" LabelAlign="Left"></f:Label>


                        </f:ContentPanel>
                    </Items>
   
                </f:FormRow>
                
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" Text="保存" ValidateForms="SimpleForm1" Size="Medium"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" Text ="关闭" runat="server" Icon="SystemClose" Size="Medium">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
</body>
</html>

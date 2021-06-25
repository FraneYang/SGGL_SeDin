<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractAgreementEdit.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.ContractAgreementEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>合同协议书</title>
    <style>
        .formtitle .f-field-body {
            text-align: center;
            font-size: 20px;
            line-height: 1.2em;
            margin: 10px 0;
        }

        .speciallabel {
            color: red;
        }

        .widthBlod {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="true" Title="分包合同协议书"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label2" runat="server" Text="分包合同协议书" CssClass="formtitle f-widget-header"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="tab2_txtGeneralContractor" runat="server" Label="总承包商（全称）" CssClass="widthBlod" LabelWidth="160px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="tab2_txtSubConstruction" runat="server" Label="施工分包商（全称）" CssClass="widthBlod" LabelWidth="160px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow ColumnWidths="70% 30%">
                    <Items>
                        <f:Label ID="Label44" runat="server" Text="根据《中华人民共和国民法典》、《中华人民共和国建筑法》及有关法律规定，遵循平等、自愿、公平和诚实信用的原则，双方就"></f:Label>
                        <f:TextBox ID="tab2_txtContents" runat="server"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label47" runat="server" Text="分包工程施工及有关事项协商一致，共同达成如下协议："></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label3" runat="server" Text="1	分包工程概况" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="tab2_txtContractProject" runat="server" Label="总包工程名称" LabelWidth="160px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="tab2_txtContractProjectOwner" runat="server" Label="总包工程业主名称" LabelWidth="160px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="tab2_txtSubProject" runat="server" Label="分包工程名称" LabelWidth="160px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="tab2_txtSubProjectAddress" runat="server" Label="分包工程地点" LabelWidth="160px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="tab2_txtFundingSources" runat="server" Label="资金来源" LabelWidth="160px" Text="业主依据总包合同支付给总承包商的工程款。"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label4" runat="server" Text="2	工程承包范围及施工内容" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="tab2_txtSubProjectContractScope" runat="server" Label="分包工程承包范围" LabelWidth="160px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="tab2_txtSubProjectContent" runat="server" Label="分包工程内容" LabelWidth="160px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Panel ID="Panel3" runat="server" ShowHeader="false" ShowBorder="false">
                            <Items>
                                <f:ContentPanel ID="ContentPanel3" runat="server" ShowHeader="false" ShowBorder="false">
                                    施工分包商的进度、质量、安全等达不到合同要求，总承包商有权对工程承包范围内的工作内容进行局部增减的调整，施工分包商不得拒绝。
                                </f:ContentPanel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label5" runat="server" Text="3	分包合同工期" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow ColumnWidths="24% 3% 10% 3% 10% 50%">
                    <Items>
                        <f:TextBox ID="tab2_txtPlanStartYear" runat="server" Label="计划开工日期" LabelWidth="150px"></f:TextBox>
                        <f:Label ID="Label6" runat="server" Text="年"></f:Label>
                        <f:TextBox ID="tab2_txtPlanStartMonth" runat="server"></f:TextBox>
                        <f:Label ID="Label7" runat="server" Text="月"></f:Label>
                        <f:TextBox ID="tab2_txtPlanStartDay" runat="server"></f:TextBox>
                        <f:Label ID="Label8" runat="server" Text="日"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow ColumnWidths="24% 3% 10% 3% 10% 50%">
                    <Items>
                        <f:TextBox ID="tab2_txtPlanEndYear" runat="server" Label="计划竣工日期" LabelWidth="150px"></f:TextBox>
                        <f:Label ID="Label45" runat="server" Text="年"></f:Label>
                        <f:TextBox ID="tab2_txtPlanEndMonth" runat="server"></f:TextBox>
                        <f:Label ID="Label46" runat="server" Text="月"></f:Label>
                        <f:TextBox ID="tab2_txtPlanEndDay" runat="server"></f:TextBox>
                        <f:Label ID="Label48" runat="server" Text="日"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow ColumnWidths="20% 80%">
                    <Items>
                        <f:TextBox ID="tab2_txtLimit" runat="server" Label="工期总日历天数" LabelWidth="150px"></f:TextBox>
                        <f:Label ID="Label9" runat="server" Text="天。工期总日历天数与根据前述计划开工竣工日期计算的工期天数不一致的，以工期总日历天数为准。"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Panel ID="Panel4" runat="server" ShowHeader="false" ShowBorder="false">
                            <Items>
                                <f:ContentPanel ID="ContentPanel4" runat="server" ShowHeader="false" ShowBorder="false">
                                    注：上述合同工期为合同工程的最早开工日期和最晚竣工时间，部分单项/单位工程应根据总承包商的要求或主控进度计划提前进行中交。工期总日历天数作为对施工分包商的进度考核日期，不因实际开工日期的变化而调整。
                                </f:ContentPanel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label10" runat="server" Text="4	质量标准、HSE标准" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Panel ID="Panel5" runat="server" ShowHeader="false" ShowBorder="false">
                            <Items>
                                <f:ContentPanel ID="ContentPanel5" runat="server" ShowHeader="false" ShowBorder="false">
                                    （1）	质量标准：适用总包合同中业主的质量管理要求、设计图纸要求、合同基准日期国家及行业标准的有效版本的合格标准，但不低于工程建设标准强制性条文要求，以上标准、规范、要求、图纸如有相抵触时，则遵循较为严格的标准。
                                </f:ContentPanel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="tab2_txtQualityStandards" runat="server" Label="依据总承包商与业主签订的总承包合同约定，具体的质量标准及目标为" LabelWidth="500px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Panel ID="Panel6" runat="server" ShowHeader="false" ShowBorder="false">
                            <Items>
                                <f:ContentPanel ID="ContentPanel6" runat="server" ShowHeader="false" ShowBorder="false">
                                    （2）   施工HSE管理要求：满足通用合同条款及专用合同条款附件之《施工安全管理协议书》相关要求。
                                </f:ContentPanel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="tab2_txtHSEManageStandards" runat="server" Label="依据总承包商与业主签订的总承包合同约定，具体的HSE管理标准及目标为" LabelWidth="500px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label11" runat="server" Text="5  安全标准化和智慧化工地建设" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Panel ID="Panel1" runat="server" ShowHeader="false" ShowBorder="false">
                            <Items>
                                <f:ContentPanel ID="ContentPanel7" runat="server" ShowHeader="false" ShowBorder="false">
                                    为预防生产安全事故的发生，保障施工人员的安全和健康，提高施工管理水平，实现施工现场安全工作的标准化，实现项目管理的“数字化、可视化、精细化、智慧化”。具体要求详见：合同附件《施工安全管理协议书》第7条。
                                </f:ContentPanel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label12" runat="server" Text="6	签约合同价格形式与合同价格" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="tab2_txtSubcontractPriceForm" runat="server" Label="（1）分包合同价格形式" LabelWidth="180px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow ColumnWidths="20% 10% 5% 10% 55%">
                    <Items>
                        <f:Label ID="Label13" runat="server" Text="（2）签约合同价为：人民币（大写" LabelWidth="180px"></f:Label>
                        <f:TextBox ID="tab2_txtContractPriceCapital" runat="server"></f:TextBox>
                        <f:Label ID="Label14" runat="server" Text="）(¥"></f:Label>
                        <f:TextBox ID="tab2_txtContractPriceCNY" runat="server"></f:TextBox>
                        <f:Label ID="Label15" runat="server" Text="元)。合同价格详见附件。 "></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="tab2_txtContractPriceDesc" runat="server" EmptyText="（非必填区域）"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Panel ID="Panel7" runat="server" ShowHeader="false" ShowBorder="false">
                            <Items>
                                <f:ContentPanel ID="ContentPanel8" runat="server" ShowHeader="false" ShowBorder="false">
                                    （3）	安全文明施工费不少于合同结算总价的1.5%，已包含在合同价格中。
                                </f:ContentPanel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:FormRow>
                <f:FormRow ColumnWidths="30% 10% 60%">
                    <Items>
                        <f:Label ID="Label16" runat="server" Text="（4）施工分包商按照国家有关规定向总承包商开具"></f:Label>
                        <f:TextBox ID="tab2_txtInvoice" runat="server"></f:TextBox>
                        <f:Label ID="Label17" runat="server" Text="建筑业增值税专用发票。如国家相关税率政策发生变化，则按照最新的税率政策相应进行价格的调整。"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label18" runat="server" Text="7	合同文件构成及解释顺序" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Panel ID="Panel8" runat="server" ShowHeader="false" ShowBorder="false">
                            <Items>
                                <f:ContentPanel ID="ContentPanel9" runat="server" ShowHeader="false" ShowBorder="false">
                                    <p>（1）	本合同协议书；</p>
                                    <p>（2）	合同附件；</p>
                                    <p>（3）	专用合同条款；</p>
                                    <p>（4）	通用合同条款；</p>
                                    <p>（5）	中标通知书（如果有）；</p>
                                    <p>（6）	投标函及其附录（如果有）；</p>
                                    <p>（7）	图纸；</p>
                                    <p>（8）	技术标准和要求；</p>
                                    <p>（9）	招标书及附件、澄清文件（前后不一致时，以最终版为准）；</p>
                                    <p>（10）	已标价工程量清单、综合单价分析表或预算书；</p>
                                    <p>（11）	其他分包合同文件。</p>
                                    <p>以上组成合同的各项文件，应当相互一致，互作解释和说明。如果文件之间有表述不清或互相矛盾之处原则上按上述顺序依次适用，总承包商有权按照最有利于分包工程的原则做出指示，施工分包商应当执行。</p>
                                    <p>前述各项分包合同文件包括合同当事人就该项分包合同文件作出的补充和修改，属于同一类内容的文件，应以最新签署的为准。</p>
                                    <p>在合同订立及履行过程中形成的与合同有关的文件均构成合同文件组成部分，并根据其性质依据上述列出的合同文件顺序确定优先解释顺序。</p>
                                </f:ContentPanel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label19" runat="server" Text="8	承诺" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Panel ID="Panel9" runat="server" ShowHeader="false" ShowBorder="false">
                            <Items>
                                <f:ContentPanel ID="ContentPanel10" runat="server" ShowHeader="false" ShowBorder="false">
                                    <p>（1）	总承包商向施工分包商承诺业主同意总承包商将分包工程分包给施工分包商，并承诺按照分包合同约定的期限和方式支付合同价款。</p>
                                    <p>（2）	施工分包商承诺具有合同要求的施工资质，按照法律规定及合同约定组织完成工程施工，确保工程质量和安全，不进行转包及违法分包，并在缺陷责任期及保修期内承担相应的工程维修责任。</p>
                                    <p>（3）	施工分包商向总承包商承诺履行总包合同中与分包工程有关的总承包商的所有义务。施工分包商向总承包商承诺就分包工程质量和安全与总承包商一起向业主承担连带责任。</p>
                                    <p>（4）	合同当事人通过招投标形式签订分包合同的，双方理解并承诺不再另行签订与分包合同实质性内容相背离的合同。</p>
                                    <p>（5）	施工分包商承诺保证总承包商免于承受因分包工程、或因施工分包商、其代理人和其雇员的任何行为或疏忽引起的索赔、损害、身体损害或财产损失。</p>
                                    <p>（6）	施工分包商知晓并同意：总承包商向其支付的费用需与业主就此工作内容向总承包商支付的费用进度相一致；如业主延期付款，总承包商付款时间顺延。如因业主未能及时支付相应工程款，总承包商按照合同约定催促业主支付工程款后，业主仍未支付工程款的，总承包商不构成违约，如有争议，应通过协商方式解决。</p>
                                </f:ContentPanel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label20" runat="server" Text="9	法律" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Panel ID="Panel10" runat="server" ShowHeader="false" ShowBorder="false">
                            <Items>
                                <f:ContentPanel ID="ContentPanel11" runat="server" ShowHeader="false" ShowBorder="false">
                                    本合同遵循中华人民共和国法律，指中华人民共和国（为本合同之目的，不含中国香港、澳门和台湾地区）法律、行政法规、部门规章以及工程所在地的地方性法规、自治条例、单行条例和地方政府规章。需要明示的国家和地方的具体适用法律的名称:
                                </f:ContentPanel>
                                <f:TextBox ID="tab2_txtLaw" runat="server" Width="600px"></f:TextBox>
                                <f:ContentPanel ID="ContentPanel12" runat="server" ShowHeader="false" ShowBorder="false">
                                    本合同项下合同当事人的权利义务与部门规章以及工程所在地的地方法规、自治条例、单行条例和地方政府规章不一致的，本合同条款优先适用。法律和行政法规的非效力性强制性规定不对本合同效力产生影响，除非被特别约定为当事人的合同义务。法律、行政法规、部门规章以及工程所在地的地方法规、自治条例、单行条例和地方政府规章中规定的当事人行政义务不会自动被作为当事人的合同义务所适用。
                                </f:ContentPanel>
                            </Items>
                        </f:Panel>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label21" runat="server" Text="10	其他" CssClass="lab"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Panel ID="Panel11" runat="server" ShowHeader="false" ShowBorder="false">
                            <Items>
                                <f:ContentPanel ID="ContentPanel13" runat="server" ShowHeader="false" ShowBorder="false">
                                    （1）	本协议书中有关词语含义与本合同第二部分《通用合同条款》中分别赋予它们的定义相同。
                                </f:ContentPanel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:FormRow>
                <f:FormRow ColumnWidths="12% 8% 3% 8% 5% 20% 44%">
                    <Items>
                        <f:Label ID="Label22" runat="server" Text="（2）本协议书于"></f:Label>
                        <f:TextBox ID="tab2_txtSignedYear" runat="server"></f:TextBox>
                        <f:Label ID="Label23" runat="server" Text="年"></f:Label>
                        <f:TextBox ID="tab2_txtSignedMonth" runat="server"></f:TextBox>
                        <f:Label ID="Label24" runat="server" Text="月在"></f:Label>
                        <f:TextBox ID="tab2_txtSignedAddress" runat="server"></f:TextBox>
                        <f:Label ID="Label25" runat="server" Text="签订，自双方签字且盖章之日起生效。"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow ColumnWidths="12% 6% 24% 8% 12% 8% 48%">
                    <Items>
                        <f:Label ID="Label26" runat="server" Text="（3）本协议书一式"></f:Label>
                        <f:TextBox ID="tab2_txtAgreementNum" runat="server"></f:TextBox>
                        <f:Label ID="Label27" runat="server" Text="份，均具有同等法律效力，总承包商执"></f:Label>
                        <f:TextBox ID="tab2_txtGeneralContractorNum" runat="server"></f:TextBox>
                        <f:Label ID="Label28" runat="server" Text="份，施工分包商执"></f:Label>
                        <f:TextBox ID="tab2_txtSubContractorNum" runat="server"></f:TextBox>
                        <f:Label ID="Label29" runat="server" Text="份。"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Panel ID="Panel12" runat="server" ShowHeader="false" ShowBorder="false">
                            <Items>
                                <f:ContentPanel ID="ContentPanel14" runat="server" ShowHeader="false" ShowBorder="false">
                                    （4）	合同终止：总承包商、施工分包商履行完毕合同全部义务，竣工结算价款支付完毕，施工分包商向总承包商交付竣工工程及相关资料及质保期届满后，本合同即告终止。合同终止后，总承包商、施工分包商应当遵循诚实信用原则，履行通知、协助、保密等义务。
                                </f:ContentPanel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Panel ID="Panel13" runat="server" ShowHeader="false" ShowBorder="false">
                            <Items>
                                <f:ContentPanel ID="ContentPanel15" runat="server" ShowHeader="false" ShowBorder="false">
                                    签字页：
                                </f:ContentPanel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label30" runat="server" Text="总承包商：赛鼎工程有限公司" CssClass="widthBlod"></f:Label>
                        <f:TextBox ID="tab2_txtSub" runat="server" Label="施工分包商" CssClass="widthBlod"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label31" runat="server" Text="（公章或合同专用章）" CssClass="widthBlod"></f:Label>
                        <f:Label ID="Label32" runat="server" Text="（公章或合同专用章）" CssClass="widthBlod"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label33" runat="server" Text="法定代表人或其委托代理人：" CssClass="widthBlod"></f:Label>
                        <f:Label ID="Label34" runat="server" Text="法定代表人或其委托代理人：" CssClass="widthBlod"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label35" runat="server" Text="（签字）" CssClass="widthBlod"></f:Label>
                        <f:Label ID="Label36" runat="server" Text="（签字）" CssClass="widthBlod"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow ColumnWidths="10% 5% 5% 30% 10% 5% 5% 30%">
                    <Items>
                        <f:Label ID="Label37" runat="server" Text="签订日期：" CssClass="widthBlod"></f:Label>
                        <f:Label ID="Label38" runat="server" Text="年" CssClass="widthBlod"></f:Label>
                        <f:Label ID="Label39" runat="server" Text="月" CssClass="widthBlod"></f:Label>
                        <f:Label ID="Label40" runat="server" Text="日" CssClass="widthBlod"></f:Label>
                        <f:Label ID="Label41" runat="server" Text="签订日期： " CssClass="widthBlod"></f:Label>
                        <f:Label ID="Label42" runat="server" Text="年" CssClass="widthBlod"></f:Label>
                        <f:Label ID="Label43" runat="server" Text="月" CssClass="widthBlod"></f:Label>
                        <f:Label ID="Label49" runat="server" Text="日" CssClass="widthBlod"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="tab2_txtSocialCreditCode1" runat="server" Label="统一社会信用代码" LabelWidth="150px" Text="9114 0100 4057 4708 1B"></f:TextBox>
                        <f:TextBox ID="tab2_txtSocialCreditCode2" runat="server" Label="统一社会信用代码" LabelWidth="150px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="tab2_txtAddress1" runat="server" Label="地址" LabelWidth="150px" Text="山西综改示范区太原学府园晋阳街赛鼎路1号"></f:TextBox>
                        <f:TextBox ID="tab2_txtAddress2" runat="server" Label="地址" LabelWidth="150px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="tab2_txtZipCode1" runat="server" Label="邮政编码" LabelWidth="150px" Text="030032"></f:TextBox>
                        <f:TextBox ID="tab2_txtZipCode2" runat="server" Label="邮政编码" LabelWidth="150px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="tab2_txtLegalRepresentative1" runat="server" Label="法定代表人" LabelWidth="150px" Text="李缠乐"></f:TextBox>
                        <f:TextBox ID="tab2_txtLegalRepresentative2" runat="server" Label="法定代表人" LabelWidth="150px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="tab2_txtEntrustedAgent1" runat="server" Label="委托代理人" LabelWidth="150px" Text="马建国"></f:TextBox>
                        <f:TextBox ID="tab2_txtEntrustedAgent2" runat="server" Label="委托代理人" LabelWidth="150px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="tab2_txtTelephone1" runat="server" Label="电  话" LabelWidth="150px" Text="0351-2179017"></f:TextBox>
                        <f:TextBox ID="tab2_txtTelephone2" runat="server" Label="电  话" LabelWidth="150px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="tab2_txtFax1" runat="server" Label="传  真" LabelWidth="150px"></f:TextBox>
                        <f:TextBox ID="tab2_txtFax2" runat="server" Label="传  真" LabelWidth="150px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="tab2_txtEmail1" runat="server" Label="电子信箱" LabelWidth="150px"></f:TextBox>
                        <f:TextBox ID="tab2_txtEmail2" runat="server" Label="电子信箱" LabelWidth="150px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="tab2_txtBank1" runat="server" Label="开户银行" LabelWidth="150px" Text="建行太原市康乐街支行"></f:TextBox>
                        <f:TextBox ID="tab2_txtBank2" runat="server" Label="开户银行" LabelWidth="150px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="tab2_txtAccount1" runat="server" Label="账  号" LabelWidth="150px" Text="14001826208050011009"></f:TextBox>
                        <f:TextBox ID="tab2_txtAccount2" runat="server" Label="账  号" LabelWidth="150px"></f:TextBox>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server" Position="Top" ToolbarAlign="Right">
                    <Items>
                        <f:Button ID="btnSave" runat="server" Icon="SystemSave" ToolTip="保存" Text="保存" OnClick="btnSave_Click"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
</body>
</html>

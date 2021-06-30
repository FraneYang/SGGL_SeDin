<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractFormationEdit.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.ContractFormationEdit" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>系统环境设置</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }

        .LabelColor {
            color: Red;
            font-size: small;
        }

        .lab {
            font-weight: bolder;
            background-color: aliceblue;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:Form ID="Form4" ShowBorder="false" ShowHeader="false" AutoScroll="true" Title="审批信息"   
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Items>
                <f:Panel ID="Panel2" runat="server" ShowHeader="false" ShowBorder="false" ColumnWidth="100%" AutoScroll="true"   MarginRight="5px"   BoxConfigPadding="5px">
                    <Items>
                        <f:Panel ID="Panel16" runat="server" CssStyle="text-align: right;" ShowBorder="false" ShowHeader="false" AutoScroll="true"  >
                            <Items>
                                <f:Button runat="server" Text="提交" ValidateForms="Form1"  Size="Medium" ConfirmText="是否提交"
                                    ID="btnSubmitForm1" OnClick="btnSubmitForm1_Click">
                                </f:Button>
                                <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" Text="关闭" runat="server" Icon="SystemClose" Size="Medium">
                                </f:Button>
                            </Items>
                        </f:Panel>
                        <f:TabStrip ID="TabStrip1" CssClass="f-tabstrip-theme-simple" Height="700" ShowBorder="true" AutoScroll="true" 
                            TabPosition="Top" MarginBottom="5px" EnableTabCloseMenu="false" runat="server" BoxConfigPadding="5px"
                            ActiveTabIndex="0">
                            <Tabs>
                                <f:Tab ID="Tab1" Title="基本信息" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">
                                    <Items>
                                        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="false" Title="基本信息"
                                            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                            <Rows>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:CheckBoxList ID="CBIsPassBid" Label="是否通过招标确定" runat="server" ColumnWidth="50%" LabelWidth="120px" OnSelectedIndexChanged="CBIsAgree_SelectedIndexChanged">
                                                            <Items>
                                                                <f:CheckItem Text="是" Value="1" />
                                                                <f:CheckItem Text="否" Value="2" />
                                                            </Items>
                                                            <Listeners>
                                                                <f:Listener Event="change" Handler="onCheckBoxListChange" />
                                                            </Listeners>
                                                        </f:CheckBoxList>
                                                        <f:Label runat="server"></f:Label>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:RadioButtonList ID="IsUseStandardtxt" Label="是否使用标准文本" runat="server" ShowRedStar="true" AutoPostBack="true"  Required="true" OnSelectedIndexChanged="IsUseStandardtxt_SelectedIndexChanged">
                                                            <f:RadioItem Text="是" Value="1" />
                                                            <f:RadioItem Text="否" Value="2" />
                                                         </f:RadioButtonList>
                                                          <f:TextArea ID="NoUseStandardtxtRemark" runat="server" Label="说明" LabelAlign="Right"   ShowRedStar="true" Required="true" AutoGrowHeightMax="50px"  Hidden="true"   AutoGrowHeight="true" EmptyText="不使用标准文本原因"></f:TextArea>
                                                         <f:Button ID="btnAttachUrl" Text="合同文本及说明" ToolTip="附件" Icon="TableCell" runat="server" Hidden="true"
                                                            OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                                                        </f:Button>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:DropDownList ID="DropSetSubReviewCode" runat="server" Label="确定中标人审批编号" LabelAlign="Right" AutoPostBack="true" ShowRedStar="true" Required="true" AutoSelectFirstItem="false"   LabelWidth="140px" Hidden="true" OnSelectedIndexChanged="DropSetSubReviewCode_SelectedIndexChanged"></f:DropDownList>
                                                        <f:DropDownList ID="DropActionPlanCode" runat="server" Label="实施计划编号" LabelAlign="Right" AutoPostBack="true" ShowRedStar="true" Required="true"  AutoSelectFirstItem="false" LabelWidth="140px" Hidden="true" OnSelectedIndexChanged="DropActionPlanCode_SelectedIndexChanged"></f:DropDownList>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:TextBox ID="tab1_txtEPCCode" runat="server" Label="总承包合同编号"  ShowRedStar="true" Required="true" LabelAlign="Right" AutoPostBack="true"   LabelWidth="120px"></f:TextBox>
                                                        <f:TextBox ID="tab1_txtProjectName" runat="server" Label="项目名称"  ShowRedStar="true" Required="true" LabelAlign="Right"   LabelWidth="140px"></f:TextBox>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:TextBox ID="tab1_txtContractName" runat="server" Label="合同名称"  ShowRedStar="true" Required="true" LabelAlign="Right" MaxLength="200" LabelWidth="120px"></f:TextBox>
                                                        <f:TextBox ID="tab1_txtContractNum" runat="server" Label="合同编号"  ShowRedStar="true" Required="true" LabelAlign="Right" MaxLength="30" LabelWidth="140px"></f:TextBox>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow ColumnWidths="50% 30% 20%">
                                                    <Items>
                                                        <f:TextBox ID="tab1_txtParties" runat="server" Label="签约方"  ShowRedStar="true" Required="true" LabelAlign="Right" MaxLength="100" LabelWidth="120px"></f:TextBox>
                                                        <f:DropDownList ID="drpCurrency" runat="server" Label="（预计）合同金额"  ShowRedStar="true" Required="true" LabelAlign="Right" LabelWidth="140px"></f:DropDownList>
                                                        <f:NumberBox ID="tab1_txtContractAmount" runat="server" LabelAlign="Right"  ShowRedStar="true" Required="true" NoNegative="true"></f:NumberBox>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:DropDownList ID="drpDepartId" runat="server" Label="主办部门" LabelAlign="Right" LabelWidth="120px"></f:DropDownList>
                                                        <f:DropDownList ID="drpAgent" runat="server" Label="经办人" ShowRedStar="true" Required="true" AutoSelectFirstItem="false"  LabelAlign="Right" LabelWidth="140px"  AutoPostBack="true" EnableEdit="true"></f:DropDownList>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:DropDownList ID="drpContractType" runat="server" Label="合同类型" ShowRedStar="true" Required="true" AutoSelectFirstItem="false"  LabelAlign="Right" LabelWidth="120px"></f:DropDownList>
                                                        <f:TextBox ID="tab1_BuildUnit" runat="server" Label="建设单位"  ShowRedStar="true" Required="true" LabelAlign="Right" MaxLength="100" LabelWidth="120px"></f:TextBox>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:TextArea ID="tab1_txtRemark" runat="server" Label="合同摘要"  ShowRedStar="true" Required="true" LabelAlign="Right" MaxLength="1000" LabelWidth="120px" AutoGrowHeightMax="150px"  AutoGrowHeight="true"></f:TextArea>
                                                    </Items>
                                                </f:FormRow>
                                            </Rows>
                                            <Toolbars>
                                                <f:Toolbar ID="Toolbar5" Position="Bottom" ToolbarAlign="Right" runat="server">
                                                </f:Toolbar>
                                            </Toolbars>
                                        </f:Form>

                                    </Items>
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                                            <Items>
                                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                                </f:ToolbarFill>
                                                <f:Button ID="btnSave_Tab1" Icon="SystemSave" runat="server" Text="保存" ToolTip="保存" ValidateForms="SimpleForm1"
                                                    OnClick="btnSave_Tab1_Click" Size="Medium">
                                                </f:Button>
                                                <%--       <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                                                </f:Button>--%>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                </f:Tab>
                                <f:Tab ID="Tab2" Title="合同协议书" BodyPadding="5px" Layout="Fit" IconFont="Bookmark" runat="server">
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
                                            <Items>
                                                <f:Button ID="btnSave" runat="server" Icon="SystemSave" Text="保存" ToolTip="保存" OnClick="btnSave_Tab_2_Click" Size="Medium"></f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Items>
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

                                        </f:Form>
                                    </Items>
                                </f:Tab>
                                <f:Tab ID="Tab3" Title="通用条款" BodyPadding="5px" Layout="Fit" IconFont="Bookmark" runat="server">
                                    <Items>
                                        <f:Form ID="Form3" ShowBorder="false" ShowHeader="false" AutoScroll="true" Title="分包合同协议书"
                                            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                            <Rows>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:Label ID="Label50" runat="server" Text="通用合同条款" CssClass="formtitle f-widget-header"></f:Label>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:Panel ID="Panel14" runat="server" ShowHeader="false" ShowBorder="false">
                                                            <Items>
                                                                <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" ShowBorder="false">
                                                                    <p>1	一般约定</p>
                                                                    <p>1.1	词语定义与解释</p>
                                                                    <p>分包合同协议书、通用合同条款、专用合同条款中的下列词语具有本款所赋予的含义：</p>
                                                                    <p>1.1.1	总包合同：是指业主和总承包商就总包工程签订的且在分包合同专用合同条款中指明的总承包合同。</p>
                                                                    <p>1.1.2	分包合同：是指根据法律规定和合同当事人约定具有约束力的文件，包括分包合同协议书、合同附件、中标通知书（如果有）、投标函及其附录（如果有）、专用合同条款、通用合同条款、技术标准和要求、图纸、已标价工程量清单或预算书以及专用合同条款约定的其他分包合同文件。</p>
                                                                    <p>1.1.3	合同协议书：是指构成合同的由总承包商和施工分包商共同签署的称为“分包合同协议书”的书面文件。</p>
                                                                    <p>1.1.4	通用合同条款：是根据法律、行政法规规定及建设工程施工的需要订立，通用于分包工程施工的条款。</p>
                                                                    <p>1.1.5	专用合同条款：是总承包商与施工分包商根据法律、行政法规规定，结合具体工程实际，经协商达成一致意见的条款，是对通用条款的具体化、补充或修改。</p>
                                                                    <p>1.1.6	分包合同当事人：是指总承包商和（或）施工分包商。</p>
                                                                    <p>1.1.7	中标通知书：是指构成合同的由总承包商通知施工分包商中标的书面文件。</p>
                                                                    <p>1.1.8	投标函：是指构成合同的由施工分包商填写并签署的用于投标的称为“投标函”的文件。</p>
                                                                    <p>1.1.9	投标函附录：是指构成合同的附在投标函后的称为“投标函附录”的文件。</p>
                                                                    <p>1.1.10	已标价工程量清单：是指构成合同的由施工分包商按照规定的格式和要求填写并标明价格的工程量清单，包括说明和表格。</p>
                                                                    <p>1.1.11	预算书：是指构成合同的由施工分包商按照总承包商规定的格式和要求编制的工程预算文件。</p>
                                                                    <p>1.1.12	其他合同文件：是指经合同当事人约定的与工程施工有关的具有合同约束力的文件或书面协议。</p>
                                                                    <p>1.1.13	总承包商：是指与业主签订总包合同，并经业主同意与施工分包商签订分包合同的，具有总包工程承包资质的当事人及取得该当事人资格的合法继承人。</p>
                                                                    <p>1.1.14	施工分包商：是指承包分包工程并与总承包商签订分包合同的，具有分包工程施工承包资质的当事人及取得该当事人资格的合法继承人，当施工分包商是由两个或两个以上成员组成的联合体，施工分包商包括联合体所有成员。</p>
                                                                    <p>1.1.15	业主：是指与总承包商签订总包合同的当事人及取得该当事人资格的合法继承人。</p>
                                                                    <p>1.1.16	监理单位：是指在总包合同中指明的，受业主委托按照法律规定对总包工程进行监督管理的法人或其他组织。</p>
                                                                    <p>1.1.17	设计人：是指在总包合同中指明的，受业主委托负责总包工程设计并具备相应工程设计资质的法人或其他组织。</p>
                                                                    <p>1.1.18	工程师：指在总包合同中约定的由工程监理单位委派的工程师或业主指定的履行总包合同的代表，其具体身份和职权由业主和总承包商在总包合同专用条款中约定。</p>
                                                                    <p>1.1.19	总承包商项目经理：是指由总承包商任命并派驻施工现场，在总承包商授权范围内负责分包合同履行，且按照法律规定具有相应资格的项目负责人。</p>
                                                                    <p>1.1.20	施工分包商项目经理：是指由施工分包商任命并派驻施工现场，在施工分包商授权范围内负责分包合同履行的项目负责人。</p>
                                                                    <p>1.1.21	总包工程：是指业主和总承包商在总包合同中约定的承包范围内的工程。</p>
                                                                    <p>1.1.22	分包工程：是指总承包商和施工分包商在分包合同中约定的施工分包商承包范围内的工程。</p>
                                                                    <p>1.1.23	永久工程：是指施工分包商按分包合同约定建造并移交给总承包商的工程，包括工程设备。</p>
                                                                    <p>1.1.24	临时工程：是指施工分包商为完成分包合同约定的永久工程所修建的各类临时性工程，不包括施工设备。</p>
                                                                    <p>1.1.25	单位工程：是指在合同中指明的，具备独立施工条件并能形成独立使用功能的永久工程。</p>
                                                                    <p>1.1.26	工程设备：是指构成永久工程的机电设备、金属结构设备、仪器及其他类似的设备和装置。</p>
                                                                    <p>1.1.27	施工设备：是指为完成分包合同约定的各项工作所需的设备、器具和其他物品，但不包括工程设备、临时工程和材料。</p>
                                                                    <p>1.1.28	施工场地：是指用于分包工程施工的场所，以及在专用合同条款中指明作为分包工程施工场地组成部分的其他场所。</p>
                                                                    <p>1.1.29	临时设施：是指为完成分包合同约定的各项工作服务的临时性生产和生活设施。</p>
                                                                    <p>1.1.30	开工日期：包括计划开工日期和实际开工日期。计划开工日期是指合同协议书约定的开工日期；实际开工日期是指总承包商按照第7.2款【开工】发出的开工通知中载明的开工日期。</p>
                                                                    <p>1.1.31	竣工日期：包括计划竣工日期和实际竣工日期。计划竣工日期是指合同协议书约定的竣工日期；实际竣工日期按照第17.3款【竣工日期】确定。</p>
                                                                    <p>1.1.32	工期：是指在分包合同协议书约定的施工分包商完成分包工程所需的期限，包括按照分包合同约定所作的期限变更。</p>
                                                                    <p>1.1.33	缺陷责任期：是指施工分包商按照分包合同约定履行缺陷修复义务且总承包商预留质量保证金（已提供履约担保的除外）的期限。提前使用的分包工程自开始使用之日起计算，其他分包工程自总包工程实际竣工之日起计算。</p>
                                                                    <p>1.1.34	保修期：是指施工分包商按照分包合同约定履行保修义务的期限。提前使用的分包工程自开始使用之日起计算，其他分包工程自总包工程验收合格之日起计算。</p>
                                                                    <p>1.1.35	基准日期：招标分包工程以投标截止日前第28天的日期为基准日期，直接分包工程以分包合同签订日前第28天的日期为基准日期。</p>
                                                                    <p>1.1.36	天：除特别指明外，均指日历天。合同中按天计算时间的，开始当天不计入，从次日开始计算，期限最后一天的截止时间为当天24:00时。</p>
                                                                    <p>1.1.37	签约合同价：是指总承包商和施工分包商在分包合同协议书中确定的总金额。</p>
                                                                    <p>1.1.38	分包合同价格：是指总承包商用于支付施工分包商按照分包合同约定完成承包范围内全部工作的金额，包括分包合同履行过程中按分包合同约定发生的价格变化。</p>
                                                                    <p>1.1.39	费用：是指为履行分包合同所发生的或将要发生的所有必需的开支，包括管理费和应分摊的其他费用，但不包括利润。</p>
                                                                    <p>1.1.40	暂估价：是指总承包商在工程量清单或预算书中提供的用于支付必然发生但暂时不能确定价格的材料、工程设备的单价、专业工程以及服务工作的金额。</p>
                                                                    <p>1.1.41	暂列金额：是指总承包商在工程量清单或预算书中暂定并包括在合同价格中的一笔款项，用于工程合同签订时尚未确定或者不可预见的所需材料、工程设备、服务的采购，施工中可能发生的工程变更、合同约定调整因素出现时的合同价格调整以及发生的索赔、现场签证确认等的费用。</p>
                                                                    <p>1.1.42	计日工：是指分包合同履行过程中，施工分包商完成总承包商提出的零星工作或需要采用计日工计价的变更工作时，按分包合同中约定的单价计价的一种方式。</p>
                                                                    <p>1.1.43	质量保证金：是指施工分包商按照第20.3款【质量保证金】用于保证其在缺陷责任期内履行缺陷修补义务的担保。</p>
                                                                    <p>1.1.44	总价项目：是指在现行国家、行业以及地方的计量规则中无工程量计算规则，在已标价工程量清单或预算书中以总价或以费率形式计算的项目。</p>
                                                                    <p>1.1.45	书面形式：指文件、信件和数据电文（包括电报、电传、传真、电子数据交换和电子邮件）等可以有形地表现所载内容的形式。</p>
                                                                    <p>1.1.46	深化设计：是指施工分包商在总承包商提供的图纸基础上，结合现场实际情况，对图纸进行完善、补充并绘制直接指导施工的图纸的活动。</p>
                                                                    <p>1.1.47	图纸：是指构成合同的图纸，包括由总承包商按照合同约定提供或经总承包商批准的设计文件、施工图、鸟瞰图及模型等，以及在合同履行过程中形成的图纸文件，图纸应当按照法律规定审查合格。</p>
                                                                    <p>1.1.48	技术标准和要求：是指构成合同的施工应当遵守的或指导施工的国家、行业或地方的技术标准和要求，以及合同约定的技术标准和要求。</p>
                                                                    <p>1.1.49	法律：是指中华人民共和国法律、行政法规、部门规章，以及工程所在地的地方性法规、自治条例、单行条例和地方政府规章等。分包合同当事人可以在专用合同条款中约定分包合同适用的其他规范性文件。</p>
                                                                    <p>1.1.50	单价合同：是指合同当事人约定以工程量清单及其综合单价进行合同价格计算、调整和确认的建设工程分包合同，在约定的风险范围内合同单价不作调整。</p>
                                                                    <p>1.1.51	总价合同：是指合同当事人约定以图纸、已标价工程量清单或预算书及有关条件进行合同价格计算、调整和确认的建设工程分包合同，在约定的风险范围内合同总价不作调整。</p>
                                                                    <p>1.1.52	单机试车：现场安装的驱动装置空负荷运转或单台机器、机组以水、空气等为介质进行的负荷试车，以检验其除受介质影响外的机械性能和制造、安装质量。</p>
                                                                    <p>1.1.53	联动试车：对规定范围内的机器、设备、管道、电气、自动控制系统等在各处达到性能标准后，以水、空气、部分实物料等为介质所进行的模拟试运行，以检验其除受介质影响外的机械性能和制造、安装质量，电气系统的试车称为电气系统试运行。</p>
                                                                    <p>1.1.54	化工投料试车：对工厂的全部生产装置按设计文件规定的介质打通生产流程，进行各装置之间首尾衔接的试运行，以检验其除经济指标外的全部性能，并生产出合格产品。</p>
                                                                    <p>1.1.55	中间交接：单项工程或部分装置按设计文件所规定的范围全部完成，并经管道系统和设备的内部处理、电气和仪表调试及单机试车合格后，总承包商、施工分包商和业主之间所做的交接工作，标志着工程施工结束，由单机试车转入联动试车。</p>
                                                                    <p>1.1.56	机械竣工：完成所有单项工程、生产单元/装置的中间交接工作。</p>
                                                                    <p>1.1.57	联合体：经总承包商同意由两个或两个以上法人或者其它组织组成的施工分包商的临时机构，联合体各方向总承包商承担连带责任。</p>
                                                                    <p>1.1.58	合同终止：指合同的权利义务终止。</p>
                                                                    <p>1.2	语言文字</p>
                                                                    <p>分包合同以中国的汉语简体文字编写、解释和说明。合同当中约定使用两种以上语言时，汉语为优先解释和说明合同的语言。</p>
                                                                    <p>1.3	标准和规范</p>
                                                                    <p>适用于分包工程的标准和规范包括国家标准、行业标准、工程所在地的地方标准以及总包合同中约定的适用于分包工程的标准和规范等。合同条款和有关技术文件中未有规定的，适用基准日期前国家的相关标准、规范的有效版本；没有国家标准、规范的，适用基准日期前行业相关标准、规范的有效版本；没有行业标准、规范的，适用工程所在地地方政府基准日期前发布的相关标准、规范。当所适用的标准、规范未能涵盖工程所需时，应由施工分包商提出要求，由总承包商指定或确认。分包合同当事人有特别要求的，应在专用合同条款中约定。</p>
                                                                    <p>除专用合同条款另有约定外，施工分包商必须配备齐全合同工程所需的所有标准、规范、非总承包商颁布的标准图集及相应的法律、法规，费用自理，并在合同工程结束前保留在现场使用。施工分包商在签订分包合同前已充分了解前述标准和要求的复杂程度，签约合同价中已包含由此产生的费用。</p>
                                                                    <p>总承包商和施工分包商各自行准备本合同专用条款约定的标准、规范。</p>
                                                                    <p>1.4	图纸</p>
                                                                    <p>1.4.1	图纸的提供</p>
                                                                    <p>总承包商应按照专用合同条款约定的期限、数量和内容向施工分包商免费提供图纸。施工分包商需要增加约定以外图纸套数的，总承包商可代为复制，复制费用由施工分包商承担。总承包商至迟不得晚于实际开工日期前7天向施工分包商提供图纸。因总承包商未按分包合同约定提供图纸导致施工分包商费用增加和（或）工期延误的，按照第7.4.1项的约定办理。除专用合同条款另有约定外，施工分包商应在施工场地保存一套完整的图纸，供总承包商和有关人员进行工程检查时使用</p>
                                                                    <p>1.4.2	图纸的错误</p>
                                                                    <p>施工分包商在收到图纸后，发现图纸存在差错、遗漏或缺陷的，应及时通知总承包商。总承包商应及时向施工分包商提供修改补充后的图纸或处理意见。</p>
                                                                    <p>图纸需要修改和补充的，应经总承包商审批同意，并由总承包商在工程或工程相应部位施工前将修改后的图纸或补充图纸提交给施工分包商，施工分包商应按修改或补充后的图纸施工。</p>
                                                                    <p>1.4.3	深化设计</p>
                                                                    <p>总承包商委托施工分包商完成分包工程的施工图深化设计的，深化设计需要相应的设计资质的，施工分包商应在其设计资质等级和业务允许的范围内，在总承包商提供图纸的基础上，根据国家有关工程建设标准进行深化设计；如施工分包商不具备相应的设计资质，应由施工分包商委托具有相应资质的单位进行深化设计。深化设计不需要相应设计资质的，施工分包商在总承包商提供图纸的基础上根据国家有关工程建设标准自行完成深化设计。</p>
                                                                    <p>施工分包商的深化设计须经过总承包商确认后方可进行施工。施工分包商对自行或委托设计的图纸负有全部的法律责任。关于总承包商委托施工分包商进行深化设计的范围及发生的费用，双方应在专用合同条款中约定。</p>
                                                                    <p>1.5	施工分包商文件</p>
                                                                    <p>施工分包商应按照专用合同条款的约定提供应当由其编制的与工程施工有关的文件，并按照专用合同条款约定的数量和形式提交总承包商。</p>
                                                                    <p>除专用合同条款另有约定外，总承包商应在收到施工分包商文件后7天内审查完毕，总承包商对施工分包商文件有异议的，施工分包商应予以修改，并重新报送总承包商。总承包商的审查并不减轻或免除施工分包商根据合同约定应当承担的责任。</p>
                                                                    <p>1.6	联络</p>
                                                                    <p>与分包合同有关的通知、指令等，均应采用书面形式，并应在分包合同约定的期限内送达接收人和送达地点。</p>
                                                                    <p>总承包商和施工分包商应在专用合同条款中约定各自的送达接收人和送达地点。任何一方合同当事人指定的接收人或送达地点发生变动的，应提前3天以书面形式通知对方。</p>
                                                                    <p>总承包商和施工分包商应当及时签收另一方送达至送达地点和指定接收人的来往信函。拒不签收的，由此增加的费用和（或）延误的工期由拒绝接收一方承担。</p>
                                                                    <p>1.7	化石、文物和地下障碍物</p>
                                                                    <p>在施工现场发掘的所有文物、古迹以及具有地质研究或考古价值的其他遗迹、化石、钱币或物品属于国家所有。总承包商应根据总包合同将施工场地内需要保护的化石、文物等通知施工分包商，施工分包商应予保护，因采取保护措施发生的费用由总承包商承担。</p>
                                                                    <p>施工分包商在施工过程中发现化石、文物的，施工分包商应立即以书面形式通知总承包商，通知中应载明化石或文物的数量、需要采取的保护措施以及因此发生的费用。总承包商收到通知后应指示施工分包商采取合理有效的保护措施，防止任何人员移动或损坏前述物品，因施工分包商采取保护措施增加的费用和（或）延误的工期由总承包商承担。</p>
                                                                    <p>施工分包商发现文物后不及时报告或隐瞒不报，致使文物丢失或损坏的，应赔偿损失，并承担相应的法律责任。</p>
                                                                    <p>施工中发现了影响施工的地下障碍物时，施工分包商应于8小时内以书面形式通知总承包商，同时提出处置方案，总承包商收到处置方案后24小时内会同有关单位予以认可或提出修正方案。总承包商承担由此发生的费用，顺延延误的工期。</p>
                                                                    <p>1.8	严禁贿赂</p>
                                                                    <p>合同当事人不得以贿赂或变相贿赂的方式，谋取非法利益或损害对方权益。因一方合同当事人的贿赂造成对方损失的，应赔偿损失，并承担相应的法律责任。</p>
                                                                    <p>施工分包商不得与总承包商有利害关系的第三方串通损害总承包商利益。未经总承包商书面同意，施工分包商不得为总承包商提供合同约定以外的通讯设备、交通工具及其他任何形式的利益，不得向总承包商及其有关人员支付报酬。</p>
                                                                    <p>1.9	知识产权</p>
                                                                    <p>总承包商提供给施工分包商的图纸、总承包商为实施工程自行编制或委托编制的技术规范以及反映总承包商要求的或其他类似性质的文件的著作权属于总承包商，施工分包商可以为实现合同目的而复制、使用此类文件，但不能用于与合同无关的其他事项。未经总承包商书面同意，施工分包商不得为了合同以外的目的而复制、使用上述文件或将之提供给任何第三方。</p>
                                                                    <p>除专用合同条款另有约定外，施工分包商为实施工程所编制的文件，除署名权以外的著作权属于总承包商，施工分包商可因实施工程的运行、调试、维修、改造等目的而复制、使用此类文件，但不能用于与合同无关的其他事项。未经总承包商书面同意，施工分包商不得为了合同以外的目的而复制、使用上述文件或将之提供给任何第三方。</p>
                                                                    <p>分包合同当事人保证在履行分包合同过程中不侵犯对方及第三方的知识产权。施工分包商在深化设计、使用材料、施工设备、工程设备或采用施工工艺时，因侵犯他人的专利权或其他知识产权所引起的责任，由施工分包商承担；因使用总承包商提供的材料、施工设备、工程设备或施工工艺导致侵权的，由总承包商承担责任。</p>
                                                                    <p>除专用合同条款另有约定外，施工分包商在合同签订前和签订时已确定采用的专利、专有技术、技术秘密的使用费已包含在签约合同价中。</p>
                                                                    <p>1.10	保密</p>
                                                                    <p>除法律规定或合同另有约定外，未经总承包商同意，施工分包商不得将总承包商提供的图纸、文件以及声明需要保密的资料信息等商业秘密泄露给第三方，并且不得用于履行本合同之外的其它用途。工程竣工结算后，施工分包商应将全部图纸退还给总承包商。</p>
                                                                    <p>施工分包商可以把从总承包商处得到的相关文件、数据和其他资料在其履行分包合同时将分包工程范围内所需要的相关文件、数据和其他资料提供给其分包单位，同时施工分包商应从其分包单位处获得类似的保密保证，且施工分包商对其分包单位的保密义务承担连带责任。</p>
                                                                    <p>以下情形，不属于违反保密义务：</p>
                                                                    <p>（1）	非分包合同当事人过失，现在或以后公开的资料；</p>
                                                                    <p>（2）	能够证明在泄密出现时已为一方所有，并且不是以前直接或间接从其他方获得的资料；</p>
                                                                    <p>（3）	在无保密义务下从第三方合法转到另一方的资料。</p>
                                                                    <p>分包合同解除或终止的，保密条款继续有效。</p>
                                                                    <p>2	总承包商</p>
                                                                    <p>2.1	总承包商的一般义务</p>
                                                                    <p>2.1.1	总承包商应向施工分包商提供履行分包合同所需的相应资料。</p>
                                                                    <p>2.1.2	总承包商应按法律规定和总包合同的约定对施工分包商和分包工程进行管理并承担总包管理责任。总承包商负责协调施工分包商与其他施工分包商之间的交叉施工作业。</p>
                                                                    <p>2.1.3	总承包商应保证施工分包商免于承担因总承包商、其他施工分包商的行为或疏忽造成的人员伤亡、财产损失、或与此有关的任何索赔。</p>
                                                                    <p>2.2	提供基础资料、施工条件</p>
                                                                    <p>2.2.1	总承包商应当在移交施工场地前向施工分包商提供分包工程施工所必需的地下管线资料、地质勘察资料、相邻建筑物、构筑物和地下工程等有关基础资料。施工分包商应对依据前述基础资料所做出的解释和推断负责，但因基础资料存在错误、遗漏导致施工分包商解释或推断失实的，由总承包商承担责任。</p>
                                                                    <p>2.2.2	除专用合同条款另有约定外，总承包商应最迟于实际开工日期3天前向施工分包商移交施工场地并提供以下施工条件：</p>
                                                                    <p>（1）	项目场区控制网基准点；</p>
                                                                    <p>（2）	水、电接驳点；</p>
                                                                    <p>（3）	正常施工所需要的进入施工场地的交通条件；</p>
                                                                    <p>（4）	正常施工所需要的作业面；</p>
                                                                    <p>（5）	按照专用合同条款约定应提供的其他设施和条件。</p>
                                                                    <p>总承包商向施工分包商移交施工场地时，总承包商和施工分包商应对全部施工条件和分包工程施工前的工程质量进行检查，并在检查记录上签字确认。施工条件不满足或分包工程施工前的工程质量不合格的，总承包商负责完善施工条件或修复相关工程直至质量合格。</p>
                                                                    <p>因总承包商原因未能按分包合同约定及时向施工分包商提供施工场地、施工条件、基础资料的，由总承包商承担由此增加的费用和（或）延误的工期。</p>
                                                                    <p>2.3	总承包商项目经理</p>
                                                                    <p>总承包商应在专用合同条款中明确其派驻施工场地的总承包商项目经理的姓名、职称、注册执业证书编号、联系方式及授权范围等事项。总承包商项目经理在总承包商的授权范围内负责处理分包合同履行过程中与总承包商有关的具体事宜。总承包商项目经理在授权范围内的行为由总承包商承担责任。</p>
                                                                    <p>总承包商可不经施工分包商同意更换项目经理，更换后7日内应通知施工分包商，自总承包商向施工分包商发出书面通知后生效。除发出变更指令外，被重新指定的项目经理无权漠视或推翻前任项目经理发出的任何证书或指示，并将继续行使合同文件约定的前任的职权，履行前任的义务。</p>
                                                                    <p>根据工程需要，总承包商项目经理可书面委派其授权代表，行使合同约定的自己的职权，并可在认为必要时撤回委派。委派和撤回委派均将在向施工分包商发出书面通知后生效。总承包商项目经理的授权代表在其授权范围和授权时限内的任何指令、签字与总承包商项目经理具有同等效力。</p>
                                                                    <p>2.4	指令和决定</p>
                                                                    <p>2.4.1	总承包商指令</p>
                                                                    <p>就分包工程范围内的工作，总承包商随时可以向施工分包商发出指令，施工分包商应执行总承包商发出的指令，总承包商指令违反法律或强制性标准的除外。如果施工分包商在收到总承包商指令后3日内未向总承包商提出异议且未执行指令，或者施工分包商提出异议但在总承包商再次确认指令后2日内仍未执行指令的，总承包商可委托其它施工单位完成该指令事项，因此发生的费用由施工分包商承担。</p>
                                                                    <p>总承包商的指令应以书面形式发出。紧急情况下，为了保证施工人员的安全或避免工程受损，总承包商可以口头形式发出指令，该指令与书面形式的指令具有同等法律效力，但总承包商应在口头指令发出后48小时内补发书面指令，补发的书面指令应与口头指令一致。</p>
                                                                    <p>2.4.2	业主或监理单位指令</p>
                                                                    <p>就分包工程范围内的工作，施工分包商应接受并执行经总承包商确认并转发的业主或监理单位发出的指令。施工分包商不应接受或执行未经总承包商确认的业主或监理单位发出的指令。施工分包商一旦收到了业主或监理单位直接向施工分包商发出的指令，应立即将此类指令通知总承包商。如施工分包商与业主或工程师发生直接工作联系，将被视为违约，并承担违约责任。</p>
                                                                    <p>3	施工分包商</p>
                                                                    <p>3.1	施工分包商的一般义务</p>
                                                                    <p>3.1.1	施工分包商应按照法律和当地行政法规或条例及专用合同条款等的要求，缴纳各项税费、办理并领取所需的全部许可、执照和批准，并将办理结果书面报送总承包商留存；施工分包商应保障和保持总承包商免受因施工分包商未能完成上述工作带来的伤害。</p>
                                                                    <p>3.1.2	按法律规定和分包合同约定完成分包工程，编制施工组织设计和施工措施计划，对所有施工作业和施工方法的完备性和安全可靠性负责，并在保修期内履行保修义务。</p>
                                                                    <p>3.1.3	对施工场地进行查勘，并充分了解分包工程所在地的气象条件、交通条件、风俗习惯以及与履行分包合同有关的其他情况。因施工分包商未能充分查勘、了解前述情况或未能充分估计前述情况所可能产生后果的，施工分包商承担由此增加的费用和（或）延误的工期。</p>
                                                                    <p>3.1.4	施工分包商应被认为已对施工现场的进入通路的适宜性和可用性表示满意，因进场通路对施工分包商的使用要求不适宜、不能用而发生的费用由施工分包商承担。施工分包商应尽合理努力，防止任何道路或桥梁因施工分包商的通行或施工分包商人员受到损坏。除专用合同条款另有约定外，施工分包商应负责施工进场道路所需要的任何维护。总承包商不对施工分包商任何进场通路的使用或其他原因引起的索赔负责。</p>
                                                                    <p>3.1.5	施工分包商应允许总承包商、业主、工程师及其三方中任何一方授权的人员在工作时间内，合理进入分包工程施工场地或材料存放的地点，以及施工场地以外与分包合同有关的施工分包商的任何工作或准备的地点，施工分包商应提供方便。</p>
                                                                    <p>3.1.6	施工分包商设备运到现场后，应视为准备为工程施工专用。未经总承包商同意，施工分包商不得从现场运走任何施工分包商设备。</p>
                                                                    <p>3.1.7	按法律规定和合同约定采取施工安全和环境保护措施，办理工伤保险和意外伤害保险，确保工程及人员、材料、设备和设施的安全，承担因自身管理不善所造成事故或处罚的一切责任。</p>
                                                                    <p>3.1.8	在进行合同约定的各项工作时，不得侵害总承包商与他人使用公用道路、水源、市政管网等公共设施的权利，做好施工场地周围地下管线和邻近建筑物、构筑物（包括文物保护建筑）、古树名木的保护工作，避免对邻近的公共设施产生干扰。施工分包商占用或使用他人的施工场地，影响他人作业或生活的，应承担相应责任；</p>
                                                                    <p>3.1.9	按照第6.2款【环境保护】约定负责施工场地及其周边环境与生态的保护工作；</p>
                                                                    <p>3.1.10	按第6.1款【安全文明施工】约定采取施工安全措施，确保工程及其人员、材料、设备和设施的安全，防止因工程施工造成的人身伤害和财产损失；</p>
                                                                    <p>3.1.11	将总承包商按合同约定支付的各项价款专用于合同工程，且应及时支付其雇用人员工资，并及时向劳务分包人支付合同价款，严格遵守专用合同条款附件之《落实施工作业人员待遇承诺书》的相关承诺。</p>
                                                                    <p>3.1.12	按照法律规定完成分包工程资料的编写、管理和归档，并按专用合同条款约定的竣工资料的套数、内容、时间等要求移交总承包商，确保分包工程资料的准确完整，包含电子版及过程中的影像资料。</p>
                                                                    <p>3.1.13	针对采用单价计价的工程，施工分包商在收到完整的施工图后60天内向总承包商报送施工图预算和工程量计算书，双方完成费用核对后，将该部分工程费用以总价形式明确作为后续工程款支付和工程结算的依据。</p>
                                                                    <p>3.1.14	施工分包商未履行各项义务，造成总承包商损失的，施工分包商赔偿总承包商有关所有损失或总承包商有权从其应付给施工分包商的任何款项中扣除。</p>
                                                                    <p>3.2	施工分包商项目经理和其他主要项目管理人员</p>
                                                                    <p>3.2.1	除专用合同条款另有约定外，施工分包商应在分包合同签署后7天内向总承包商提交施工分包商项目管理机构及施工人员安排的报告。施工分包商项目管理机构包括施工分包商项目经理和其他主要项目管理人员（应包括施工、技术、质量、HSE等），项目经理及主要管理人员应是施工分包商正式聘用的员工。</p>
                                                                    <p>分包合同当事人应在专用合同条款中约定施工分包商项目经理的姓名、职称、注册执业证书编号、联系方式及授权范围等事项。其他主要项目管理人员的姓名和岗位详见专用合同条款附件之《施工分包商组织机构人员配置表及关键人员名单》。</p>
                                                                    <p>合同当事人通过招投标方式订立分包合同的，专用合同条款或其附件中载明的施工分包商项目经理和其他主要项目管理人员应与施工分包商投标文件保持一致。</p>
                                                                    <p>施工分包商应在分包合同签署后7天内向总承包商提交施工分包商为前述人员缴纳社会保险的有效证明、劳动合同。施工分包商不提交上述文件的，前述人员无权履行职责，总承包商有权要求更换，由此增加的费用和（或）延误的工期由施工分包商承担。</p>
                                                                    <p>3.2.2	施工分包商项目经理为施工分包商派驻施工场地的负责人，在施工分包商授权范围内决定分包合同履行过程中与施工分包商有关的具体事宜。施工分包商应对施工分包商项目经理的任何作为、疏忽或不作为负责。施工分包商项目经理应严格遵守住建部《建筑施工项目经理质量安全责任十项规定（试行）》相关要求。项目经理应常驻施工现场，且每月在施工现场时间不得少于专用合同条款约定的天数。除专用合同条款另有约定外，施工分包商项目经理不得同时担任其他项目的项目经理；否则，施工分包商应按专用合同条款的约定承担违约责任。</p>
                                                                    <p>3.2.3	施工分包商项目经理和其他主要项目管理人员应常驻施工场地。前述人员因故需要离开施工场地时，应事先取得总承包商的书面同意；否则，施工分包商应按照专用合同条款的约定承担违约责任。</p>
                                                                    <p>3.2.4	施工分包商需要更换施工分包商项目经理或其他主要项目管理人员的，应提前14天书面通知总承包商，并征得总承包商书面同意。未经总承包商书面同意，施工分包商不得擅自更换。施工分包商擅自更换的，应按照专用合同条款的约定承担违约责任。</p>
                                                                    <p>总承包商有权书面通知施工分包商更换不称职的施工分包商项目经理或其他主要项目管理人员的，施工分包商应在接到更换通知后7天内进行更换。施工分包商无正当理由拒绝更换，应按照专用合同条款的约定承担违约责任。</p>
                                                                    <p>3.3	特殊工种上岗作业要求</p>
                                                                    <p>法律规定需要持证上岗的特殊工种作业人员，必须具有行政管理部门颁发的相应岗位的特殊工种上岗证，并在相应作业人员进场前报送总承包商审核并备案。特殊工种作业人员不具备上岗证的，施工分包商应按专用合同条款的约定承担违约责任。</p>
                                                                    <p>3.4	禁止转包和再分包</p>
                                                                    <p>3.4.1	施工分包商不得将分包工程转包给第三人。施工分包商转包分包工程的，应按专用合同条款的约定承担违约责任。</p>
                                                                    <p>3.4.2	施工分包商不得将分包工程的任何部分违法分包给任何第三人。施工分包商违法分包工程的，应按专用合同条款的约定承担违约责任。</p>
                                                                    <p>3.4.3	施工分包商不得以劳务分包的名义转包或违法分包工程。</p>
                                                                    <p>3.5	履约担保</p>
                                                                    <p>总承包商需要施工分包商提供履约担保的，由合同当事人在专用合同条款中约定履约担保的方式、金额及期限等。</p>
                                                                    <p>3.6	联合体</p>
                                                                    <p>3.6.1	联合体各方应共同与总承包商签订合同协议书，联合体各方应为履行分包合同向总承包商承担连带责任。</p>
                                                                    <p>3.6.2	联合体协议经总承包商确认后作为专用合同条款附件，在履行分包合同过程中，未经总承包商书面同意，不得修改联合体协议。</p>
                                                                    <p>3.6.3	联合体应在联合体协议书中指明联合体的牵头人，牵头人必须有权约束联合体及每位成员，负责代表联合体与总承包商履行并接受指示，负责组织联合体各成员全面履行合同。</p>
                                                                    <p>4	总包合同</p>
                                                                    <p>4.1	总包合同的提供</p>
                                                                    <p>总承包商应提供总包合同供施工分包商查阅，并且在施工分包商要求时向施工分包商提供一份总包合同复印件，但有关总包工程价格及支付内容除外。施工分包商应仔细阅读并全面了解总包合同的各项约定，分包合同的签订视为施工分包商完全知悉总承包商在总包合同项下与分包工程有关的义务和责任。</p>
                                                                    <p>4.2	与总包合同有关的施工分包商义务</p>
                                                                    <p>施工分包商应履行并承担总包合同中与分包工程有关的总承包商的所有义务与责任，但分包合同明确约定由总承包商履行的义务除外。施工分包商应避免因其自身行为或疏忽造成总承包商违反总包合同约定，否则因此导致总承包商向业主承担责任时，总承包商有权向施工分包商追偿。</p>
                                                                    <p>4.3	总包合同解除</p>
                                                                    <p>总承包商与业主之间的总包合同解除时，总承包商、施工分包商均有权解除本合同。</p>
                                                                    <p>因施工分包商原因导致总包合同解除时，总承包商有权要求其承担赔偿损失等违约责任。</p>
                                                                    <p>非分包合同当事人原因导致总包合同解除时，施工分包商应配合总承包商向业主主张权利，否则，由其自行承担一切损失，并赔偿总承包商的损失。</p>
                                                                    <p>5	分包工程质量</p>
                                                                    <p>5.1	质量要求</p>
                                                                    <p>分包工程质量必须符合国家、行业及工程所在地的强制性技术标准和要求，同时应符合总包合同约定的质量标准。除分包合同协议书约定的质量标准外，有关分包工程质量的特殊标准或要求可以由分包合同当事人在专用合同条款中约定。</p>
                                                                    <p>因总承包商原因造成工程质量未达到合同约定标准的，施工分包商负责采取修复等补救措施，由总承包商承担由此增加的费用和（或）延误的工期。</p>
                                                                    <p>因施工分包商原因造成工程质量未达到合同约定标准的，总承包商有权要求施工分包商返工直至工程质量达到合同约定的标准为止，并由施工分包商承担由此增加的费用和（或）延误的工期。</p>
                                                                    <p>5.2	施工分包商的质量管理</p>
                                                                    <p>施工分包商应按照第7.1款【施工组织】向总承包商提交分包工程质量保证体系及措施文件，建立并实施一套有效的、符合GB/T 19001-2016中适用于本合同工程具体要求的质量管理体系，明确质量管理组织机构和职责、质量管理计划和控制程序、质量保证人员和职责、质量检查要求和程序、各级质量控制点设置、不合格品控制及改进程序、质量事件/事故调查处理程序等，同时应满足总承包商现场施工质量管理各项具体要求。</p>
                                                                    <p>施工分包商应对施工人员进行质量教育和技术培训，定期考核施工人员的劳动技能，严格执行施工规范和操作规程。</p>
                                                                    <p>施工分包商应按照法律规定和总承包商的要求，对材料、工程设备以及工程的所有部位及其施工工艺进行全过程的质量检查和检验，并作详细记录，编制工程质量报表，报送总承包商审查。此外，施工分包商还应按照法律及标准规范规定和总承包商的要求，进行施工现场取样试验、工程复核测量和设备性能检测，提供试验样品、提交试验报告和测量成果以及其他工作。</p>
                                                                    <p>5.3	总承包商的质量管理</p>
                                                                    <p>总承包商应根据法律及总包合同约定建立总包工程质量管理体系，并将分包工程质量管理纳入总包工程质量管理体系，对分包工程的质量进行监督管理。</p>
                                                                    <p>总承包商有权对工程的所有部位及其施工工艺、材料和工程设备进行检查和检验。施工分包商应认真按照标准、规范和设计图纸要求以及总承包商依据合同发出的指令施工，随时接受总承包商的检查、检验，为总承包商的检查和检验提供方便，包括总承包商到施工现场，或制造、加工地点，或合同约定的其他地方进行察看和查阅施工原始记录。总承包商、监理人、业主及其他相关方为此进行的检查和检验或其他类似行为，不免除或减轻施工分包商按照合同约定应当承担的任何责任。</p>
                                                                    <p>除法律、法规、标准规范及行业主管要求的检查和检验外，总承包商、监理人、业主及其他相关方所实施的其它检查和检验不应影响施工正常进行。相关的检查和检验影响施工正常进行的，且经检查检验不合格的，影响正常施工的费用由施工分包商承担，工期不予顺延；经检查检验合格的，由此增加的费用和（或）延误的工期由总承包商承担。</p>
                                                                    <p>5.4	不合格工程的处理</p>
                                                                    <p>5.4.1	因施工分包商原因造成分包工程不合格的，总承包商有权随时要求施工分包商采取补救措施，直至达到分包合同要求的质量标准，由此增加的费用和（或）延误的工期由施工分包商承担。无法采取措施补救的，按照第18.2款【拒绝接收全部或部分分包工程】执行。</p>
                                                                    <p>5.4.2	因总承包商原因造成分包工程不合格的，施工分包商应采取补救措施，由此增加的费用和（或）延误的工期由总承包商承担，并支付施工分包商合理的利润。</p>
                                                                    <p>6	HSE管理</p>
                                                                    <p>6.1	安全文明施工</p>
                                                                    <p>6.1.1	安全文明施工</p>
                                                                    <p>总承包商和施工分包商应按法律和分包合同采取安全文明施工措施并履行安全文明管理义务。总承包商应向施工分包商进行安全文明施工措施的详细交底。因安全文明施工措施不到位而发生的安全生产事故由责任方承担责任。合同当事人的具体安全文明施工措施详见专用合同条款。</p>
                                                                    <p>施工分包商对安全文明施工费应专款专用，并应在财务账目中单独列项备查，不得挪作他用，否则总承包商有权责令其停工并限期改正。施工分包商现场安全文明措施不满足总承包商要求，经总承包商通知后无整改或整改不到位，总承包商有权责令施工分包商暂停施工，总承包商可以另行委托其他单位完成，由此发生的所有费用和（或）延误的工期由施工分包商承担。</p>
                                                                    <p>施工分包商在本工程的安全文明施工投入费用应不低于合同结算总价的1.5%，每月向总承包商提供安全投入费用明细表，由总承包商现场安全主管人员审核，工程结束时如不足合同总价安全投入费用，按实际累计发生费用结算。</p>
                                                                    <p>6.1.2	安全保卫</p>
                                                                    <p>施工分包商应负责分包工程施工期间施工场地的安全保卫工作，总承包商应予以配合。施工分包商应妥善保管好己方的设备设施等财物，并承担自身保管不善发生的损失。在施工场地或生活区发生突发事件的，总承包商应积极协调业主和当地有关部门协助施工分包商采取措施平息事态，尽量避免人员伤亡和财产损失。</p>
                                                                    <p>6.1.3	紧急情况处理</p>
                                                                    <p>在分包工程实施期间或缺陷责任期内发生危及工程安全的事件，总承包商通知施工分包商进行抢救，施工分包商声明无能力或不愿立即执行的（口头的或书面的），总承包商有权雇佣其他人员进行抢救，由此增加的费用和（或）延误的工期由施工分包商承担。</p>
                                                                    <p>施工分包商在积极抢救后，紧急情况的发生非施工分包商原因造成的，由此增加的费用和（或）延误的工期由总承包商承担。</p>
                                                                    <p>6.1.4	事故处理</p>
                                                                    <p>分包工程施工过程中发生安全生产事故的，施工分包商应立即通知总承包商。总承包商和施工分包商应立即组织人员和设备进行紧急抢救和抢修，减少人员伤亡和财产损失并保护事故现场。需要移动现场物品时，应作出标记和书面记录，妥善保管有关证据。总承包商和施工分包商应按照法律规定及时向有关部门报告事故有关情况。</p>
                                                                    <p>6.1.5	安全生产责任</p>
                                                                    <p>因施工分包商原因发生重大伤亡或其他安全事故，总承包商将视情节对施工分包商采取以下处罚措施：</p>
                                                                    <p>（1）	罚款。累计罚款金额不超过分包工程结算款总价的10%。</p>
                                                                    <p>（2）	终止合同。</p>
                                                                    <p>6.2	环境保护</p>
                                                                    <p>施工分包商应在施工组织设计中列明环境保护的具体措施。施工分包商应按经总承包商批准的施工组织设计采取环境保护具体措施，防止环境损害和环境污染。施工分包商应当承担因其原因引起的环境污染责任，由此增加的费用和（或）延误的工期由施工分包商承担。</p>
                                                                    <p>6.3	职业健康</p>
                                                                    <p>6.3.1	施工分包商应与其为履行分包合同而雇佣的人员建立劳动关系，并按法律规定保障劳动者的各项权利。</p>
                                                                    <p>6.3.2	除专用合同条款另有约定外，施工分包商应为其雇佣的人员提供必要的膳宿条件；膳宿条件应达到工程所在地行政管理机关的标准和要求。总承包商应按工程所在地行政管理机关的标准和要求对劳务人员的宿舍和食堂进行监督检查。</p>
                                                                    <p>6.3.3	施工分包商获得的工程款应优先支付施工分包商雇佣人员的劳动报酬和劳务分包单位的劳务费用。施工分包商拖欠前述劳动报酬或劳务费用给本项目造成不利影响的，施工分包商应按照专用合同条款的约定承担违约责任。</p>
                                                                    <p>6.3.4	施工分包商必须在现场设置专职的劳资管理员，对雇佣人员的劳动报酬和劳务分包单位的劳务费用按月支付，建立台账和发放记录表，每月报送总承包商。有关劳资专管员的姓名和劳务费用发放台账在专用合同条款中约定。</p>
                                                                    <p>6.3.5	由于施工分包商原因造成的劳资纠纷由分包商负全部责任及相应连带责任，如对总承包商造成损失的，由施工分包商一并负责。</p>
                                                                    <p>7	工期和进度</p>
                                                                    <p>7.1	施工组织</p>
                                                                    <p>7.1.1	施工组织设计的编制和批准</p>
                                                                    <p>施工分包商应在分包合同签订后7天内，至迟不得晚于实际开工日期前7天，向总承包商提交分包工程详细施工组织设计。除专用合同条款另有约定外，总承包商应在收到施工组织设计后7天内确认或提出修改意见。对总承包商提出的合理意见和要求，施工分包商应予以修改完善。施工分包商未在前述期限内提交施工组织设计，或总承包商未在前述期限内确认或提出修改意见的，应按专用合同条款的约定承担违约责任。</p>
                                                                    <p>群体工程中单位工程分期进行施工的，施工分包商应按照总承包商提供的图纸及有关资料，在 7 日内提交单位工程进度计划，总承包商应在不迟于开工前7日内提出书面意见。</p>
                                                                    <p>经总承包商批准的施工组织设计是分包合同当事人控制分包工程进度的依据。总承包商、监理人、业主有权依据施工进度计划随时检查分包工程进度。未经总承包商批准，施工分包商不得修改施工进度计划。施工分包商应对所有现场作业、所有施工方法和全部工程的完备性、稳定性和安全性承担责任。</p>
                                                                    <p>如果施工分包商的实际进度已落后于计划进度或现场情况明确显示实际进度将落后于计划进度时，施工分包商应按照总承包商的要求提出改进措施（包括但不限于重新安排工作顺序、调整工作时间、增加人力配备和其他施工资源或措施等），经总承包商确认后执行，并自行承担可能增加的费用和（或）延误的工期。</p>
                                                                    <p>因施工分包商的原因导致工程实际进度明显滞后于计划进度时，总承包商有权采取其认为有必要的一切措施，包括将施工分包商承担的部分工作内容另行委托其他施工分包商执行。此时，总承包商有权核减工程合同价款，并从施工分包商处扣回由于此项工作使总承包商发生的费用。</p>
                                                                    <p>经总承包商批准修改的施工进度计划，施工分包商应严格执行。总承包商对施工分包商提交的施工进度计划的确认，仅是对进度计划中所提议的执行工程顺序或秩序的同意，不能减轻或免除施工分包商根据法律规定和合同约定应承担的任何责任和义务，如因施工进度计划修订导致费用增加或工期延误，应由施工分包商承担。</p>
                                                                    <p>任何情况下施工分包商应充分允许总承包商查取与工程进度计划有关的所有最新资料及所有支撑性数据（包括电子版本及其他格式）、有关说明及附属资料，且总承包商可以要求施工分包商提供在工程实施过程中用于支撑工程进度计划的任何关键路径分析的详细资料。</p>
                                                                    <p>7.1.2	交叉作业的工期管理</p>
                                                                    <p>总承包商应严格按总包工程施工组织设计对分包工程及其他工程的工期进行管理，并按分包工程施工组织设计向施工分包商提供正常施工所需的施工条件。因其他工程原因影响分包工程正常施工时，总承包商应及时通知施工分包商，施工分包商应立即调整施工组织设计并取得总承包商批准，因此产生的费用和（或）延误的工期由总承包商承担。</p>
                                                                    <p>施工分包商应按经批准的施工组织设计组织分包工程的施工，在施工过程中应配合其他工程的施工。由于施工分包商原因造成分包工程施工影响其他工程正常施工的，施工分包商应立即通知总承包商，调整施工组织设计并取得总承包商批准，因此产生的费用和（或）延误的工期由施工分包商承担。</p>
                                                                    <p>7.1.3	劳动力保障</p>
                                                                    <p>施工分包商应按照经批准的施工组织设计保证劳动力投入。因施工分包商劳动力短缺导致分包工程进度滞后的，施工分包商应在收到总承包商书面通知后7日内补足劳动力，否则，总承包商有权解除分包合同或取消施工分包商部分工作，且施工分包商应按照专用合同条款的约定承担违约责任。</p>
                                                                    <p>7.2	开工</p>
                                                                    <p>总承包商应在计划开工日期7天前按照分包合同约定依法向施工分包商发出开工通知，工期自开工通知中载明的开工日期起算。</p>
                                                                    <p>除专用合同条款另有约定外，因总承包商原因未能在计划开工日期之日起90天内发出开工通知的，施工分包商有权提出价格调整要求，或者解除合同。总承包商应当承担由此增加的费用和（或）延误的工期。</p>
                                                                    <p>7.3	测量放线</p>
                                                                    <p>7.3.1	除专用合同条款另有约定外，总承包商应在实际开工日期7天前向施工分包商提供测量基准点、基准线和水准点及其书面资料。施工分包商发现总承包商提供的测量基准点、基准线和水准点及其书面资料存在错误或疏漏的，应及时通知总承包商，总承包商应及时发出指令。</p>
                                                                    <p>7.3.2	施工分包商负责分包工程施工过程中的测量放线工作，并配备具有相应资质的人员、合格的仪器、设备和其他物品。施工分包商应矫正分包工程的位置、标高、尺寸或准线中出现的任何差错，并对分包工程各部分的定位负责。施工分包商负责对施工场地内水准点等测量标志物进行保护。</p>
                                                                    <p>7.4	工期延误</p>
                                                                    <p>7.4.1	因总承包商原因导致的工期延误</p>
                                                                    <p>分包合同履行过程中，因总承包商原因造成工期延误的，且施工分包商已经采取了令总承包商项目经理满意的最大努力来防止、避免、克服、化解及减轻所造成的延误，经总承包商确认后，由总承包商承担延误的工期和（或）增加的费用。</p>
                                                                    <p>7.4.2	因施工分包商原因导致工期延误</p>
                                                                    <p>因施工分包商原因造成工期延误的，施工分包商应按照专用合同条款的约定承担逾期竣工的违约责任。施工分包商支付逾期竣工违约金后，不免除或减轻施工分包商继续完成工程及修补缺陷的义务。</p>
                                                                    <p>7.4.3	工期延误的处理程序</p>
                                                                    <p>施工分包商在7.4.1项情况发生后7天内，就延误的工期以书面形式向总承包商提出报告，总承包商在收到报告后7天内予以确认。书面报告的主要内容包括：</p>
                                                                    <p>（1）事件发生的日期、时间；</p>
                                                                    <p>（2）导致事件发生的情况以及与事件发生有关的情况；</p>
                                                                    <p>（3）造成工程或其任何相关部分进度延误的原因及程度，以及事件的发生对竣工日期的影响；</p>
                                                                    <p>（4）基于对合同工程关键线路分析的基础上，修正后的包含逻辑关系的进度计划分析，以及说明施工分包商认为因事件的发生而必须做出的对任何工序活动的日期、持续时间或顺序的任何修改。</p>
                                                                    <p>7.4.4	非关键线路的工期延误</p>
                                                                    <p>工期延误如未影响到工程关键线路，则总工期不予顺延，施工分包商有责任和义务采取一切可能的措施予以消除此类影响，同时总承包商也应尽最大努力来避免、减少和消除此类原因。</p>
                                                                    <p>7.5	不利物质条件</p>
                                                                    <p>不利物质条件是指有经验的施工分包商在施工场地遇到的不可预见的自然物质条件、非自然的物质障碍和污染物，包括地表以下物质条件和水文条件以及专用合同条款约定的其他情形，但不包括气候条件。</p>
                                                                    <p>施工分包商遇到不利物质条件时，应采取克服不利物质条件的合理措施继续施工，并及时通知总承包商。通知应载明不利物质条件的内容、施工分包商认为不可预见的理由以及需要采取的合理措施和因此发生的费用。施工分包商因采取合理措施而增加的费用和（或）延误的工期由总承包商承担。</p>
                                                                    <p>7.6	异常恶劣的气候条件</p>
                                                                    <p>异常恶劣的气候条件是指在施工过程中遇到的，有经验的施工分包商在签订分包合同时不可预见的，对合同履行造成实质性影响的，但尚未构成不可抗力事件的恶劣气候条件。分包合同当事人可以在专用合同条款中约定异常恶劣的气候条件的具体情形。</p>
                                                                    <p>出现异常恶劣的气候条件时，施工分包商应及时通知总承包商。通知应载明异常恶劣的气候条件的内容、施工分包商认为不可预见的理由以及因此发生的费用。总承包商应及时予以审核、确认并发出是否构成工期延误条件对的指示。总承包商指示施工分包商采取合理措施继续施工时，因采取合理措施而增加的费用和（或）延误的工期由总承包商承担。</p>
                                                                    <p>7.7	暂停施工</p>
                                                                    <p>7.7.1	业主指示暂停施工的或因总承包商原因引起暂停施工的，总承包商应及时下达暂停施工指令，施工分包商应按总承包商指令暂停施工，总承包商应承担由此增加的费用和（或）延误的工期。</p>
                                                                    <p>7.7.2	因施工分包商原因引起的暂停施工，施工分包商应承担由此增加的费用和（或）延误的工期，且施工分包商在收到总承包商复工指示后14天内仍未复工的，视为施工分包商无法继续履行分包合同。总承包商有权解除合同并要求施工分包商赔偿损失。</p>
                                                                    <p>施工过程中，出现不利物质条件、异常恶劣的气候条件或不可抗力等危及工程质量和安全的紧急情况，且总承包商未及时下达暂停施工指示的，施工分包商可先暂停施工，并及时通知总承包商。总承包商应在接到通知后及时发出指示。</p>
                                                                    <p>7.7.3	暂停施工期间，施工分包商应负责妥善照管分包工程并提供安全保障，由此增加的费用由造成暂停施工的责任方承担。施工分包商应采取必要措施确保工程质量及安全，防止因暂停施工扩大损失。施工分包商未妥善履行上述义务造成损失的，应予赔偿。</p>
                                                                    <p>7.7.4	暂停施工后的复工</p>
                                                                    <p>暂停施工后，总承包商和施工分包商应采取有效措施积极消除暂停施工的影响。在分包工程复工前，总承包商和施工分包商应确定因暂停施工造成的损失，并确定工程复工条件。当工程具备复工条件时，总承包商向施工分包商发出复工通知，施工分包商应按照复工通知的要求复工。总承包商发出复工通知后，应组织有关方对受暂停影响的工程、设备及材料进行检查，施工分包商应修复在暂停期间工程、设备及材料中发生的任何损害和缺陷。</p>
                                                                    <p>施工分包商无故拖延或拒绝复工的，施工分包商承担由此增加的费用和（或）延误的工期；因总承包商原因无法按时复工的，按照第7.4款【工期延误】执行。</p>
                                                                    <p>7.7.5	暂停施工持续56天以上</p>
                                                                    <p>总承包商发出暂停施工指示后56天内未向施工分包商发出复工通知，除该项停工属于第7.7.2项及第22款【不可抗力】约定的情形外，施工分包商可向总承包商提交书面通知，要求总承包商在收到书面通知后28天内准许已暂停施工的部分或全部工程继续施工。总承包商逾期不予批准的，施工分包商可以通知总承包商，将工程受影响的部分视为按照第10.1款【分包合同变更的范围】第（2）项的可取消工作。</p>
                                                                    <p>7.7.6	暂停施工持续84天以上</p>
                                                                    <p>暂定施工持续84天以上不复工的，且该项停工不属于第7.7.2项及第22款【不可抗力】约定的情形，并已经影响到整体分包工程以及分包合同目的实现的，施工分包商有权提出价格调整要求或者解除合同。施工分包商要求解除合同的，按照第21.1款【总承包商违约】执行。</p>
                                                                    <p>7.8	竣工</p>
                                                                    <p>7.8.1	施工分包商必须按照合同约定的竣工日期或总承包商同意顺延的工期竣工，完成整个工程和每个分项工程，并且按照本合同条款第17款【竣工验收】条款规定获得通过。</p>
                                                                    <p>7.8.2	因施工分包商原因不能按照合同约定的竣工日期或总承包商同意顺延的工期竣工的，施工分包商承担违约责任。</p>
                                                                    <p>7.8.3	总承包商要求施工分包商提前竣工的，应下达提前竣工指示，施工分包商应向总承包商提交提前竣工建议书，其中应包括实施的方案、缩短的时间、增加的合同价格等内容。总承包商提出提前竣工要求的，应与施工分包商协商采取加快工程进度的措施，修订施工进度计划，由此增加的费用由总承包商承担。施工分包商认为提前竣工指示无法执行的，应及时提出书面异议。任何情况下，不得压缩合理工期。</p>
                                                                    <p>8	材料与设备</p>
                                                                    <p>8.1	针对工程设备和材料供应，总承包商和施工分包商应在专用合同条款中划分供应范围。</p>
                                                                    <p>8.2	在总包合同中约定就分包工程范围内由业主供应的材料和设备，视为总承包商供应。</p>
                                                                    <p>8.3	总承包商供应材料与工程设备</p>
                                                                    <p>8.3.1	总承包商供应工程设备和材料的，总承包商应向施工分包商提供合格证明及出厂证明，并对其质量负责。</p>
                                                                    <p>8.3.2	除合同专用条款另有约定外，总承包商供应的材料设备在运抵现场前24小时通知施工分包商，由施工分包商负责提供人员、机具，按照总承包商的要求将运抵现场的材料设备安全、及时地卸至指定地点。设备材料卸车费在专用条款中具体约定。</p>
                                                                    <p>8.3.3	除合同专用条款另有约定外，总承包商供应的其它材料设备从总承包商现场仓库出库到施工作业地点的二次搬运由施工分包商负责并确保材料设备的完好性，二次搬运费用在专用条款中具体约定。</p>
                                                                    <p>8.3.4	总承包商对其供应材料设备的质量、数量、进度负责。总承包商供应材料设备的质量、数量、进度如不能满足施工进度要求，总承包商、施工分包商都应积极采取措施尽量减少对工程进展的影响。如采取一切可能的措施后仍然影响到工程关键线路，经总承包商确认后工期相应顺延。</p>
                                                                    <p>8.3.5	施工分包商负责领用后的工程设备和材料的保管和报验工作，对领到的设备材料存放保管要妥善处理，任何因施工分包商原因造成的设备材料丢失、损坏或（和）施工不当造成的设备材料浪费问题、以及所造成的一切影响项目进度和费用方面的后果将全部由施工分包商负责。</p>
                                                                    <p>8.4	施工分包商供应材料与工程设备</p>
                                                                    <p>8.4.1	施工分包商采购其范围内的材料、工程设备，提供合格证明及出厂证明，并对材料、工程设备质量负责。总承包商在招标文件中指定了生产厂或供应商的，施工分包商应从指定的生产厂或供应商处采购材料设备。对于分包合同约定的由施工分包商负责供应的大宗材料设备，施工分包商在采购前必须向总承包商提交供应商短名单，并得到总承包商书面批准后执行。施工分包商采购的材料设备到现场后的存放、保管、二次倒运及报验工作全部由施工分包商负责，但总承包商有权监督施工分包商的工作。</p>
                                                                    <p>8.4.2	施工分包商采购的材料和工程设备，施工分包商应在材料和工程设备到货前24小时通知总承包商检验。施工分包商采购的材料和工程设备不符合图纸或有关标准要求时，施工分包商应在总承包商要求的合理期限内将不符合设计或有关标准要求的材料、工程设备运出施工场地，并重新采购符合要求的材料、工程设备，由此增加的费用和（或）延误的工期，由施工分包商承担。</p>
                                                                    <p>8.5	材料与工程设备的保管与使用</p>
                                                                    <p>8.5.1	施工分包商按照施工管理、设备材料储存及保管等的要求，在现场建立不低于总承包商要求的材料堆场和设备库房，配置足够的库房管理人员和设施进行设备材料的库房管理，并接受总承包商的材料管理程序和具体要求。施工分包商未按照总承包商要求进行工程设备和材料的保管，总承包商可以委托其他单位完成，因此所发生的有关费用由施工分包商承担。</p>
                                                                    <p>8.5.2	总承包商供应的材料和工程设备统一出库给施工分包商，施工分包商清点后由施工分包商妥善保管。</p>
                                                                    <p>8.5.3	任何因施工分包商原因造成的设备材料丢失、损坏或（和）施工不当造成的设备材料浪费问题、以及所造成的一切影响项目进度和费用方面的后果将全部由施工分包商负责。</p>
                                                                    <p>8.5.4	除合同专用条款另有约定外，总承包商和施工分包商供应的工程设备和材料使用前，法律和标准规范规定必须进行检验或试验的，由施工分包商负责检验或试验，检验或试验费用已包含在签约合同价内，检验或试验不合格的不得使用。超出规范要求的检验或试验，有关费用由总承包商负责。</p>
                                                                    <p>8.6	样品</p>
                                                                    <p>8.6.1	需要施工分包商报送样品的材料或工程设备，样品的种类、名称、规格、数量均应在专用合同条款中约定。样品的报送、封存及保管事宜按总承包合同的约定执行，施工分包商应履行总承包合同对应条款中约定的总承包商义务。</p>
                                                                    <p>8.7	材料与工程设备的替代</p>
                                                                    <p>除专用合同条款另有约定外，材料与工程设备的替代按总承包合同的约定执行，施工分包商应履行总承包合同对应条款中约定的总承包商义务。</p>
                                                                    <p>8.8	施工设备和临时设施</p>
                                                                    <p>8.8.1	总承包商提供的施工设备和临时设施在专用合同条款中约定。</p>
                                                                    <p>8.8.2	施工分包商应按分包合同的要求及时配置施工设备和修建临时设施并自行承担相关费用，需要临时占地的，应办理申请手续并承担可能需要的相应费用。施工设备应符合经总承包商批准的施工组织设计，并经总承包商会同业主、监理核查批准后才能投入使用。施工分包商更换分包合同约定的施工设备的，应报总承包商批准。</p>
                                                                    <p>8.8.3	施工分包商提供的施工设备不能满足施工组织设计和（或）分包工程质量要求时，总承包商有权要求施工分包商增加或更换施工设备，施工分包商应及时增加或更换，由此增加的费用和（或）延误的工期由施工分包商承担。</p>
                                                                    <p>8.9	材料与设备专用</p>
                                                                    <p>施工分包商运入施工场地的材料、工程设备、施工设备以及在施工场地建设的临时设施，包括备品备件、安装工具与资料，必须专用于分包工程。未经总承包商批准，施工分包商不得运出施工场地或挪作他用；经总承包商批准，施工分包商可以根据施工进度计划撤走闲置的施工设备和其他物品。</p>
                                                                    <p>9	试验和检验</p>
                                                                    <p>法律和标准规范规定必须进行试验或检验的，由施工分包商负责试验或检验，试验或检验费用已包含在签约合同价内，试验或检验不合格的不得使用。超出规范要求的试验或检验，有关费用由总承包商负责。</p>
                                                                    <p>10	分包合同变更</p>
                                                                    <p>10.1	分包合同变更的范围</p>
                                                                    <p>除专用合同条款另有约定外，分包合同履行过程中发生的以下情形属于变更：</p>
                                                                    <p>（1）	增加或减少分包合同中任何工作，或追加额外的工作；</p>
                                                                    <p>（2）	取消分包合同中任何工作，但转由他人实施的工作除外；</p>
                                                                    <p>（3）	改变分包合同中任何工作的质量标准或其他特性；</p>
                                                                    <p>（4）	改变分包工程的基线、标高、位置和尺寸；</p>
                                                                    <p>（5）	改变分包工程的时间安排或实施顺序。</p>
                                                                    <p>10.2	分包合同变更的提出和执行</p>
                                                                    <p>施工分包商收到经总承包商确认的业主发出的变更指令或总承包商发出的变更指令后，方可实施变更。未经许可，施工分包商不得擅自对工程的任何部分进行变更。涉及设计变更的，应由总承包商提供设计人签署的变更后的图纸和说明。</p>
                                                                    <p>施工分包商收到总承包商下达的变更指令后，认为不能执行的，应在收到变更指令后24小时内提出不能执行的理由。施工分包商认为可以执行变更的，应按第10.3款【分包合同变更估价】确定变更估价。</p>
                                                                    <p>10.3	分包合同变更估价</p>
                                                                    <p>10.3.1	变更估价原则</p>
                                                                    <p>除专用合同条款另有约定外，变更估价按照本项约定执行：</p>
                                                                    <p>（1）	已标价工程量清单或预算书有相同项目的，按照相同项目单价认定；</p>
                                                                    <p>（2）	已标价工程量清单或预算书中无相同项目，但有类似项目的，参照类似项目的单价认定；</p>
                                                                    <p>（3）	变更导致实际完成的工程量与已标价工程量清单或预算书中列明的该项目工程量的变化幅度超过15%的，或已标价工程量清单或预算书中无相同项目及类似项目单价的，由合同当事人按照成本加合理利润的原则认定。</p>
                                                                    <p>10.3.2	变更估价程序</p>
                                                                    <p>除专用合同条款另有约定外，施工分包商应在收到变更指令后7天内向总承包商提交变更估价申请。施工分包商在收到变更指令后7天内不向总承包商提出变更价款报告时，视为该项变更不涉及合同价款的变更。总承包商应在收到变更估价申请后21天内审查完毕，总承包商对变更估价申请有异议的，应通知施工分包商修改后重新提交。总承包商逾期未完成审批或未提出异议的，视为认可施工分包商提交的变更估价申请。</p>
                                                                    <p>因施工分包商原因影响施工进度，总承包商有权对本合同施工分包商的工作范围进行变更，同时对应调整合同价格。</p>
                                                                    <p>因变更引起的价格调整应计入最近一期的进度款中支付。</p>
                                                                    <p>10.4	施工分包商的合理化建议</p>
                                                                    <p>施工分包商可以向总承包商提出合理化建议并说明实施该建议对分包合同价格和工期的影响。总承包商批准前述合理化建议的，总承包商应及时发出变更指令。总承包商不同意前述合理化建议的，应书面通知施工分包商。</p>
                                                                    <p>合理化建议降低了分包合同价格或者提高了分包工程经济效益的，总承包商可对施工分包商给予奖励，奖励的方法和金额在专用合同条款中约定。</p>
                                                                    <p>10.5	变更引起的工期调整</p>
                                                                    <p>因变更引起工期变化的，合同当事人均可要求调整分包合同工期，由合同当事人按专用合同条款约定的方法确定增减工期天数。</p>
                                                                    <p>10.6	计日工</p>
                                                                    <p>
                                                                        需要采用计日工方式的，总承包商通知施工分包商以计日工计价方式实施相应的工作，其价款按已标价工程量清单或预算书中的计日工计价项目及其单价进行计算；已标价工程量清单或预算书中无相应的计日工单价的，由合同当事人按照成本加合理利润的原则确定计日工单价。
                            采用计日工计价的工作，施工分包商应每天提交以下报表送总承包商审查：
                                                                    </p>
                                                                    <p>（1）	工作名称、内容和数量；</p>
                                                                    <p>（2）	经确认的投入该工作的所有人员的姓名、专业、工种、级别和耗用工时；</p>
                                                                    <p>（3）	投入该工作的材料类别和数量；</p>
                                                                    <p>（4）	投入该工作的施工设备型号、台数和耗用台班时；</p>
                                                                    <p>（5）	其他有关资料和凭证。</p>
                                                                    <p>计日工应计入最近一期的进度款中支付。</p>
                                                                    <p>11	合同价格</p>
                                                                    <p>总承包商和施工分包商应在合同协议书中选择下列一种合同价格形式： </p>
                                                                    <p>（1）	单价合同</p>
                                                                    <p>单价合同是指合同当事人约定以工程量清单及其综合单价、综合费率、定额总价下浮、定额费率下浮进行合同价格计算、调整和确认的建设工程专业分包合同，在约定的风险范围内合同单价不作调整。合同当事人应在专用合同条款中约定综合单价或综合费率或定额总价下浮或定额费率下浮包含的风险范围和风险费用的计算方法，并约定风险范围以外的合同价格的调整方法，其中因市场价格波动引起的调整按第12.1款【市场价格波动引起的调整】约定执行，因法律变化引起的调整按第12.2款【法律变化引起的调整】约定执行。</p>
                                                                    <p>（2）	总价合同</p>
                                                                    <p>总价合同是指合同当事人约定以图纸、已标价工程量清单或预算书及有关条件进行合同价格计算、调整和确认的建设工程专业分包合同，在约定的风险范围内合同总价不作调整。合同当事人应在专用合同条款中约定总价包含的风险范围和风险费用的计算方法，并约定风险范围以外的合同价格的调整方法，其中因市场价格波动引起的调整按第12.1款【市场价格波动引起的调整】约定执行，因法律变化引起的调整按第12.2款【法律变化引起的调整】约定执行。</p>
                                                                    <p>（3）	其它价格形式</p>
                                                                    <p>合同当事人可在专用合同条款中约定其他合同价格形式。</p>
                                                                    <p>12	价格调整</p>
                                                                    <p>12.1	市场价格波动引起的调整</p>
                                                                    <p>除专用合同条款另有约定外，市场价格波动超过分包合同当事人约定的范围和浮动标准，分包合同价格应当调整。分包合同当事人可以在专用合同条款中约定市场价格波动超过当事人约定范围时进行价格调整的方式。</p>
                                                                    <p>因市场价格波动引起的价格调整金额列入最近一期进度付款申请单，经总承包商批准后列入进度付款。</p>
                                                                    <p>12.2	 法律变化引起的调整</p>
                                                                    <p>基准日期后，法律（包括相关标准、规范中的强制性执行要求）变化导致施工分包商在合同履行过程中所需要的费用发生增加或减少时，合同价格应相应增加或减少。</p>
                                                                    <p>因施工分包商原因造成工期延误，在工期延误期间出现法律（包括相关标准、规范中的强制性执行要求）变化的，由此增加的费用由施工分包商承担；由此减少的费用将从原合同价款中予以扣除。</p>
                                                                    <p>因法律变化引起的价格调整金额列入最近一期进度付款申请单，经总承包商批准后列入进度付款。</p>
                                                                    <p>13	计量</p>
                                                                    <p>13.1	计量原则和计量周期</p>
                                                                    <p>13.1.1	工程量计量按照分包合同约定的工程量计算规则、图纸及变更指示等进行计量。工程量计算规则应以相关的国家标准、行业标准等为依据，由合同当事人在专用合同条款中约定。</p>
                                                                    <p>13.1.2	除专用合同条款另有约定外，工程量的计量按月进行。</p>
                                                                    <p>13.2	计量程序</p>
                                                                    <p>除专用合同条款另有约定外，分包合同的计量按照本款约定程序执行：</p>
                                                                    <p>（1）	施工分包商应按总包合同约定的每月计量的起止日期进行计量，并于总包合同约定的总承包商每月提交工程量报告的期限届满3天前向总承包商报送当月工程量报告，并附具进度付款申请单、已完成工程量报表和有关资料。</p>
                                                                    <p>（2）	总承包商应在收到施工分包商提交的工程量报告后10天内完成对施工分包商提交的工程量报表的审核，以确定当月实际完成的工程量。总承包商对工程量有异议的，有权要求施工分包商进行共同复核或抽样复测。施工分包商应协助总承包商进行复核或抽样复测，并按总承包商要求提供补充计量资料。施工分包商未按总承包商要求参加复核或抽样复测的，总承包商复核或修正的工程量视为施工分包商实际完成的工程量。</p>
                                                                    <p>（3）	总承包商在收到施工分包商提交的工程量报表后的10天内未完成审核的或未提出异议的，施工分包商报送的工程量视为施工分包商实际完成的工程量。</p>
                                                                    <p>（4）	对施工分包商自行超出设计图纸范围和因施工分包商原因造成返工的工程量，总承包商不予计量。</p>
                                                                    <p>14	工程款支付</p>
                                                                    <p>14.1	预付款</p>
                                                                    <p>总承包商应按照专用合同条款的约定支付预付款。预付款应当用于材料、工程设备、施工设备的采购及组织施工队伍进场、临时设施建设等。</p>
                                                                    <p>除专用合同条款另有约定外，预付款在进度付款中同比例扣回，在完全扣回之前，分包商应保证预付款担保持续有效。在分包工程竣工验收合格前，分包合同解除的，尚未扣完的预付款应与分包合同价款一并结算。</p>
                                                                    <p>14.2	安全文明施工费的支付</p>
                                                                    <p>除专用合同条款另有约定外，安全文明施工费随工程进度款一并在进度付款中支付施工分包商。</p>
                                                                    <p>14.3	工程进度款支付</p>
                                                                    <p>14.3.1	除专用合同条款另有约定外，付款周期应与第13.1款【计量原则和计量周期】约定的计量周期保持一致。</p>
                                                                    <p>14.3.2	进度付款申请单严格按照总承包商要求的格式和内容进行报送。</p>
                                                                    <p>施工分包商应按照第13.2款【计量程序】约定的时间及专用合同条款约定的内容按付款周期向总承包商提交进度付款申请单，并附上已完成工程量报表和有关资料。本合同在施工图预算核对一致前，双方一致同意按照工程量清单暂定总价和里程碑节点权重进行计量和付款。</p>
                                                                    <p>14.3.3	进度款审核和支付</p>
                                                                    <p>（1）	除专用合同条款另有约定外，总承包商应在收到施工分包商进度付款申请单后21天内完成审核并签发进度款支付证书。总承包商逾期未完成审批且未提出异议的，视为已签发进度款支付证书。</p>
                                                                    <p>总承包商对施工分包商的进度付款申请单有异议的，有权要求施工分包商修正和提供补充资料，施工分包商应提交修正后的进度付款申请单。总承包商应在收到施工分包商修正后的进度付款申请单及相关资料后21天内完成审核，向施工分包商签发无异议部分的临时进度款支付证书。存在争议的部分，按照第25款【争议解决】的约定处理。</p>
                                                                    <p>（2）	除专用合同条款另有约定外，总承包商应在进度款支付证书或临时进度款支付证书签发后14天内完成支付。总承包商逾期支付进度款的，施工分包商可向总承包商发出要求付款的通知。总承包商收到施工分包商催款通知后仍不能按要求付款，可与施工分包商协商签订延期付款协议，经施工分包商同意后可延期支付；协议应明确延期支付的时间和从计量结果确认后第15天起计算应付款的贷款利息。</p>
                                                                    <p>（3）	总承包商签发进度款支付证书或临时进度款支付证书，不表明总承包商已同意、批准或认可了施工分包商完成的相应部分的工作。</p>
                                                                    <p>14.3.4	进度付款的修正</p>
                                                                    <p>在对已签发的进度款支付证书进行阶段汇总和复核中发现错误、遗漏或重复的，总承包商和施工分包商均有权提出修正申请。经总承包商和施工分包商同意的修正，应在下期进度付款中支付或扣除。</p>
                                                                    <p>14.4	支付账户</p>
                                                                    <p>总承包商应将合同价款支付至合同协议书中约定的施工分包商账户。</p>
                                                                    <p>15	成品保护</p>
                                                                    <p>15.1	分包工程的成品保护</p>
                                                                    <p>在分包工程移交总承包商前，施工分包商负责分包工程的成品保护工作。施工分包商应采取妥善的保护措施确保已完成的分包工程在本单位或其他施工单位正常施工时不被损坏。在此期间，分包工程被损坏的，无论损坏责任方是谁，均由施工分包商进行修复或重置，因此发生的费用和（或）延误的工期由施工分包商承担。</p>
                                                                    <p>在分包工程移交总承包商后，总承包商负责分包工程的成品保护工作。总承包商应采取妥善的保护措施确保分包工程不被损坏，因此发生的费用由总承包商承担。</p>
                                                                    <p>分包工程因非施工分包商原因被损坏的，总承包商可以自行修复，也可以要求施工分包商进行修复，因此发生的费用和（或）延误的工期由总承包商承担。</p>
                                                                    <p>15.2	其它工程的成品保护</p>
                                                                    <p>施工分包商在施工过程中应采取合理措施对分包工程之外的已完工程予以保护，避免对已完工程造成损坏。分包工程之外的其他工程因施工分包商原因损坏的，总承包商可以自行修复，也可以要求施工分包商进行修复，因此发生的费用和（或）延误的工期由施工分包商承担。</p>
                                                                    <p>16	试车</p>
                                                                    <p>16.1	试车组织和配合</p>
                                                                    <p>施工分包商负责单机试车工作，包括无负荷单机试车和有负荷单机试车；除专用合同条款另有约定外，试车内容应与施工分包商承包范围相一致。工程试车应按如下程序进行：</p>
                                                                    <p>（1）	具备无负荷单机试车条件，施工分包商负责组织试车，并在试车前48小时书面通知总承包商，通知中应载明试车内容、时间、地点。施工分包商准备试车记录，总承包商根据施工分包商要求为试车提供必要条件。试车合格的，总承包商在试车记录上签字。总承包商在试车合格后不在试车记录上签字，自试车结束满48小时后视为总承包商已经认可试车记录，施工分包商可继续施工或办理竣工验收手续。</p>
                                                                    <p>总承包商不能按时参加试车或不能为试车提供必要条件的，应在试车前24小时以书面形式向施工分包商提出延期要求，由此导致工期延误的，工期应予以顺延。总承包商未能在前述期限内提出延期要求，又不参加试车或不提供必要条件的，视为试车达到验收要求。</p>
                                                                    <p>（2）	具备有负荷单机试车条件，总承包商组织试车，并在试车前48小时以书面形式通知施工分包商。通知中应载明试车内容、时间、地点和对施工分包商的要求，施工分包商按要求做好准备工作。试车合格，合同当事人在试车记录上签字。试车不合格，施工分包商不在试车记录上签字，自试车结束满48小时后视为施工分包商已经认可试车记录。施工分包商无正当理由不参加试车的，视为认可试车记录。</p>
                                                                    <p>（3）	具备联动试车条件，总承包商组织试车，需要施工分包商进行保运的，总承包商在试车前48小时以书面形式通知施工分包商。保运工作范围由合同当事人在专用合同条款中约定。</p>
                                                                    <p>16.2	试车中的责任</p>
                                                                    <p>因设计原因导致试车达不到验收要求，总承包商应协调业主修改设计，施工分包商按修改后的设计重新施工。总承包商承担修改设计、拆除及重新施工的全部费用，工期相应顺延。如果是由于施工分包商负责深化设计的部分出现问题导致试车达不到验收要求，则由施工分包商承担修改设计、拆除及重新施工的全部费用，工期不予顺延。</p>
                                                                    <p>因工程设备制造原因导致试车达不到验收要求的，由采购该工程设备的合同当事人负责重新购置或修理，施工分包商负责拆除和重新安装，由此增加的修理、重新购置、拆除及重新安装的费用、延误的工期以及造成的其他相应损失由采购该工程设备的合同当事人承担。</p>
                                                                    <p>因施工分包商原因导致试车达不到验收要求，施工分包商按总承包商要求重新安装和试车，并承担重新安装和试车的费用，工期不予顺延，并赔偿总承包商因此发生的直接的和间接的损失。</p>
                                                                    <p>16.3	试车费用承担</p>
                                                                    <p>单机试车工作及有关费用由施工分包商承担；联动试车工作及有关费用由总承包商承担，但属于施工分包商责任引起的有关费用由施工分包商承担。</p>
                                                                    <p>需要施工分包商进行保运的，保运费用标准由合同当事人在专用合同条款中约定。</p>
                                                                    <p>16.4	投料试车</p>
                                                                    <p>如需进行投料试车的，总承包商应在工程竣工验收后组织投料试车。总承包商要求在工程竣工验收前进行或需要施工分包商配合时，应征得施工分包商同意，并书面约定有关事项。</p>
                                                                    <p>因施工分包商原因造成投料试车不合格的，施工分包商应按照总承包商要求进行整改，由此产生的整改费用由施工分包商承担；非因施工分包商原因导致投料试车不合格的，如总承包商要求施工分包商进行整改的，由此产生的费用由总承包商承担。</p>
                                                                    <p>17	竣工验收</p>
                                                                    <p>17.1	竣工验收条件</p>
                                                                    <p>分包工程具备以下条件的，施工分包商可以申请竣工验收：</p>
                                                                    <p>（1）	除总承包商同意的甩项工作和缺陷修补工作外，分包合同范围内的全部分包工程以及有关工作，包括分包合同要求的试验以及检验均已完成，并符合分包合同要求；</p>
                                                                    <p>（2）	已按分包合同约定编制了甩项工作和缺陷修补工作清单以及相应的施工计划；</p>
                                                                    <p>（3）	已按分包合同约定的内容和份数备齐竣工资料（包括竣工图）；</p>
                                                                    <p>（4）	专用合同条款约定的其他条件。</p>
                                                                    <p>17.2	竣工验收程序</p>
                                                                    <p>除专用合同条款另有约定外，施工分包商申请竣工验收的，应当按照以下程序进行：</p>
                                                                    <p>（1）	施工分包商向总承包商报送竣工验收申请报告，总承包商应在收到竣工验收申请报告后21天内完成审查，总承包商审查后认为不具备验收条件的，应通知施工分包商还需完成的工作内容，施工分包商应在完成总承包商通知的全部工作内容后，再次提交竣工验收申请报告。</p>
                                                                    <p>（2）	总承包商审查后认为已具备竣工验收条件的，应在收到竣工验收申请报告后28天内按分部分项工程验收要求组织相关单位进行竣工验收。</p>
                                                                    <p>（3）	竣工验收不合格的，总承包商应按照验收意见发出指示，要求施工分包商对不合格工程返工、修复或采取其他补救措施，由此增加的费用和（或）延误的工期由施工分包商承担。施工分包商完成不合格工程的返工、修复或采取其他补救措施后，应重新提交竣工验收申请报告，并按本款约定的程序重新进行验收。</p>
                                                                    <p>重新验收仍不合格，或者施工分包商拒绝返工、修复的，总承包商有权自行或者另行委托他方修复、返工，所需费用由施工分包商承担。总承包商有权在支付施工分包商的任何款项或施工分包商的履约保函中予以扣回，施工分包商并应承担相应违约责任。</p>
                                                                    <p>（4）	分包工程未经验收或验收不合格，总承包商或业主擅自使用的，自转移占有分包工程之日起，分包工程应被视为竣工验收合格。</p>
                                                                    <p>17.3	竣工日期</p>
                                                                    <p>分包工程经竣工验收合格的，以施工分包商提交竣工验收申请报告之日为实际竣工日期；因总承包商原因未在收到竣工验收申请报告28天内组织竣工验收的，以施工分包商提交竣工验收申请报告的日期为实际竣工日期；分包工程未经竣工验收，总承包商或业主擅自使用的，以转移占有分包工程之日为实际竣工日期。</p>
                                                                    <p>施工分包商应保证分包工程自竣工验收合格至移交总承包商的期间一直保持质量合格状态，否则施工分包商应按专用合同条款的约定承担违约责任。</p>
                                                                    <p>17.4	竣工退场</p>
                                                                    <p>分包工程竣工验收合格且完成分包工程范围内的所有工作后，施工分包商应按合同专用条款约定的期限和以下要求自费对施工场地进行清理后退场： </p>
                                                                    <p>（1）	施工现场内残留的垃圾已全部清除出场；</p>
                                                                    <p>（2）	临时工程已拆除，场地已进行清理、平整或复原；</p>
                                                                    <p>（3）	按合同约定应撤离的人员、施工分包商施工设备和剩余的材料，包括废弃的施工设备和材料，已按计划撤离施工现场；</p>
                                                                    <p>（4）	施工现场周边及其附近道路、河道的施工堆积物，已全部清理；</p>
                                                                    <p>（5）	施工现场其他场地清理工作已全部完成。</p>
                                                                    <p>施工分包商逾期未完成清理并退场的，总承包商有权出售或另行处理施工分包商遗留的物品，由此发生的费用由施工分包商承担。经总承包商书面同意，施工分包商可在总承包商指定的地点保留施工分包商履行缺陷责任期内的各项义务所需要的材料、施工设备和临时工程。</p>
                                                                    <p>施工分包商应按总承包商要求恢复临时占地及清理场地，施工分包商未按总承包商的要求恢复临时占地，或者场地清理未达到合同约定要求的，总承包商有权委托其他人恢复或清理，所发生的费用由施工分包商承担。</p>
                                                                    <p>18	分包工程移交</p>
                                                                    <p>18.1	分包工程移交时间</p>
                                                                    <p>除专用合同条款另有约定外，对于提前使用的分包工程，总承包商应于分包工程竣工验收合格后14天内向施工分包商颁发接收证书；对于其他分包工程，在施工分包商完成全部工程资料的移交后，总承包商应于总包工程竣工验收合格后14天内向施工分包商颁发分包工程接收证书。总承包商无正当理由逾期不颁发分包工程接收证书，自总包工程竣工验收合格后第15天起视为已颁发分包工程接收证书。颁发分包工程接收证书视为分包工程移交总承包商。</p>
                                                                    <p>合同当事人应当专用合同条款中约定工程资料的套数、内容。</p>
                                                                    <p>18.2	拒绝接收全部或部分分包工程</p>
                                                                    <p>对于竣工验收不合格的分包工程，施工分包商完成整改后，经重新组织验收仍不合格且无法采取补救措施的，总承包商可以拒绝接收，由此给总承包商造成的所有损失由施工分包商承担。</p>
                                                                    <p>19	结算</p>
                                                                    <p>19.1	结算申请</p>
                                                                    <p>除专用合同条款另有约定外，施工分包商应在分包工程竣工验收合格后28天内向总承包商提交分包工程结算申请单，并提交完整的结算资料，有关分包工程结算申请单的资料清单和份数等要求由合同当事人在专用合同条款中约定。</p>
                                                                    <p>除专用合同条款另有约定外，分包工程结算申请单至少应包括以下内容：</p>
                                                                    <p>（1）	分包工程结算价格；</p>
                                                                    <p>（2）	总承包商已支付施工分包商的款项；</p>
                                                                    <p>（3）	应扣留的质量保证金（已提供履约担保或其他工程质量担保方式的除外）；</p>
                                                                    <p>（4）	总承包商应支付施工分包商的合同价款。</p>
                                                                    <p>19.2	 结算审核</p>
                                                                    <p>19.2.1	除专用合同条款另有约定外，总承包商应在收到结算申请单后56天内完成审核，并在总承包商公司审计（如有）结束后向施工分包商签发竣工付款证书。总承包商对结算申请单有异议的，有权要求施工分包商进行修正和提供补充资料，施工分包商应提交修正后的结算申请单。</p>
                                                                    <p>总承包商在收到施工分包商提交结算申请单后56天内未完成审核且未提出异议的，视为总承包商认可施工分包商提交的结算申请单，并自总承包商收到施工分包商提交的结算申请单后第57天起视为已签发竣工付款证书。</p>
                                                                    <p>19.2.2	除专用合同条款另有约定外，总承包商应在签发竣工付款证书后14天内，完成对施工分包商的竣工付款。总承包商逾期支付的，按照中国人民银行发布的同期同类贷款基准利率支付违约金。</p>
                                                                    <p>19.2.3	施工分包商对总承包商签发的竣工付款证书有异议的，对于有异议部分应在收到总承包商签发的竣工付款证书后7天内提出异议，按照第25条【争议解决】的约定处理。施工分包商逾期未提出异议的，视为认可总承包商签发的竣工付款证书。</p>
                                                                    <p>19.3	最终结清</p>
                                                                    <p>19.3.1	除专用合同条款另有约定外，施工分包商应在分包工程缺陷责任期终止证书颁发后7天内，按专用合同条款约定的份数向总承包商提交最终结清申请单，并提供相关证明材料。</p>
                                                                    <p>除专用合同条款另有约定外，最终结清申请单应列明质量保证金、应扣除的质量保证金、分包工程竣工验收合格之日至缺陷责任期届满之日发生的增减费用。</p>
                                                                    <p>总承包商对最终结清申请单内容有异议的，有权要求施工分包商进行修正和提供补充资料，施工分包商应向总承包商提交修正后的最终结清申请单。</p>
                                                                    <p>19.3.2	除专用合同条款另有约定外，总承包商应在收到最终结清申请单后28天内完成审批并向施工分包商颁发最终结清证书。总承包商逾期未完成审批且未提出异议的，视为总承包商同意施工分包商提交的最终结清申请单，且自总承包商收到最终结清申请单后第29天起视为已颁发最终结清证书。</p>
                                                                    <p>除专用合同条款另有约定外，总承包商应在最终结清证书颁发后28天内完成支付。总承包商逾期支付的，按照中国人民银行发布的同期同类贷款基准利率支付违约金。</p>
                                                                    <p>20	缺陷责任期与保修期</p>
                                                                    <p>20.1	缺陷责任期</p>
                                                                    <p>20.1.1	除专用合同条款另有约定外，分包工程缺陷责任期自总包工程实际竣工之日起计算，时间为12个月。</p>
                                                                    <p>20.1.2	在分包工程缺陷责任期内，由施工分包商原因造成的缺陷，施工分包商应负责维修，并承担鉴定及维修费用。如施工分包商不维修也不承担费用，总承包商可从保证金或银行保函中扣除；费用超出保证金金额的，总承包商可向施工分包商进行索赔。施工分包商维修并承担相应费用后，不免除其对工程的损失赔偿责任。因施工分包商原因导致的缺陷或损坏致使分包工程或某项主要设备不能按原定目的使用的，总承包商有权要求施工分包商延长缺陷责任期，并应在原缺陷责任期届满前发出延长通知，但延长后的缺陷责任期不能超过24个月。</p>
                                                                    <p>20.1.3	除专用合同条款另有约定外，施工分包商应于缺陷责任期届满后7天内向总承包商发出缺陷责任期届满通知，总承包商应在收到缺陷责任期届满通知后14天内核实施工分包商是否履行缺陷修复义务，施工分包商未能履行缺陷修复义务的，总承包商有权扣除相应金额的维修费用。总承包商应在收到缺陷责任期届满通知后14天内，向施工分包商颁发缺陷责任期终止证书。</p>
                                                                    <p>20.2	保修期</p>
                                                                    <p>20.2.1	除专用合同条款另有约定外，提前使用分包工程的，分包工程保修期自开始使用之日起计算；未提前使用的，分包工程保修期自总包工程竣工验收合格之日起计算。保修期由合同当事人在专用合同条款中约定，但不得低于法定最低保修年限。在分包工程保修期内，施工分包商应根据法律规定和分包合同约定对因施工分包商原因产生的分包工程质量缺陷承担保修义务和损失赔偿责任。</p>
                                                                    <p>20.2.2	总承包商或业主未经竣工验收擅自使用分包工程的，保修期自分包工程转移占有之日起算。</p>
                                                                    <p>20.2.3	修复费用的处理</p>
                                                                    <p>（1）	保修期内，因施工分包商原因造成工程的缺陷、损坏，施工分包商应负责修复，并承担修复的费用以及因工程的缺陷、损坏造成的人身伤害和财产损失；</p>
                                                                    <p>（2）	保修期内，因总承包商原因造成工程的缺陷、损坏，可以委托施工分包商修复，但总承包商应承担修复的费用；</p>
                                                                    <p>（3）	因其他原因造成工程的缺陷、损坏，可以委托施工分包商修复，总承包商应承担修复的费用，因工程的缺陷、损坏造成的人身伤害和财产损失由责任方承担。</p>
                                                                    <p>20.2.4	修复通知</p>
                                                                    <p>在保修期内，总承包商发现已接收的工程存在缺陷或损坏的，应书面通知施工分包商予以修复，但情况紧急必须立即修复缺陷或损坏的，总承包商可以口头通知施工分包商并在口头通知后48小时内书面确认，施工分包商应在接到通知3日内到达工程现场并修复缺陷或损坏。</p>
                                                                    <p>20.2.5	未能修复</p>
                                                                    <p>因施工分包商原因造成工程的缺陷或损坏，施工分包商拒绝维修或未能在合理期限内修复缺陷或损坏，且经总承包商书面催告后仍未修复的，总承包商有权自行修复或委托第三方修复，所需费用由施工分包商承担。但修复范围超出缺陷或损坏范围的，超出范围部分的修复费用由总承包商承担。</p>
                                                                    <p>20.2.6	施工分包商出入权</p>
                                                                    <p>在保修期内，为了修复缺陷或损坏，施工分包商有权出入工程现场，除情况紧急必须立即修复缺陷或损坏外，施工分包商应提前24小时通知总承包商进场修复的时间。施工分包商进入工程现场前应获得总承包商同意，且不应影响业主正常的生产经营，并应遵守业主有关安全生产、保安和保密等规定。</p>
                                                                    <p>20.3	质量保证金</p>
                                                                    <p>20.3.1	合同当事人可以在专用合同条款中约定扣留质量保证金的金额和扣留方式。总承包商累计扣留的质量保证金在合同专用条款中具体约定。</p>
                                                                    <p>20.3.2	总承包商应按第19.3款【最终结清】的约定退还质量保证金。</p>
                                                                    <p>20.3.3	预留质量保证金的情况下，如果施工分包商在总承包商确认竣工结算报告及结算资料后28天内提交数额相等的质量保证金保函，总承包商应同时退还扣留的作为质量保证金的工程价款。</p>
                                                                    <p>21	违约</p>
                                                                    <p>21.1	总承包商违约</p>
                                                                    <p>21.1.1	总承包商未能按照分包合同约定履行合同义务的，施工分包商可向总承包商发出通知，要求总承包商采取有效措施纠正违约行为。总承包商应赔偿其违约行为给施工分包商造成的损失。合同当事人可在专用合同条款中约定总承包商应支付的违约金或违约金的计算方法。</p>
                                                                    <p>21.1.2	总承包商收到施工分包商通知后28天内仍不纠正违约行为且影响施工分包商正常施工的，施工分包商有权暂停相应部位工程施工，并通知总承包商。除专用合同条款另有约定外，施工分包商因总承包商违约暂停施工满28天后，总承包商仍不纠正其违约行为的，施工分包商有权解除合同。</p>
                                                                    <p>21.1.3	因总承包商违约解除合同</p>
                                                                    <p>施工分包商按照第21.1.2项的约定解除合同的，总承包商应在分包合同解除后28天内解除施工分包商履约担保，分包合同当事人应按专用合同条款的约定完成已完工程估价、清算和付款。</p>
                                                                    <p>合同当事人未能就解除合同后的结清达成一致的，按照第25款【争议解决】的约定处理。</p>
                                                                    <p>施工分包商应妥善做好已完分包工程和与分包工程有关的已购材料、工程设备、工程资料的保护和移交工作，并将施工设备和人员撤出施工场地，总承包商应为施工分包商撤出提供必要条件。</p>
                                                                    <p>施工分包商按照施工进度计划已经订货的材料、设备（需提供订货合同原件供审查），由施工分包商负责退货或解除订货合同。不能退还的货款和因退货、解除订货合同发生的费用，由总承包商承担，因未及时退货造成的损失由施工分包商承担。</p>
                                                                    <p>21.2	施工分包商违约</p>
                                                                    <p>21.2.1	施工分包商未能按照分包合同约定履行合同义务的，总承包商可向施工分包商发出整改通知，要求其在指定的期限内改正。施工分包商应赔偿其违约行为给总承包商造成的损失。合同当事人可在专用合同条款中约定施工分包商应支付的违约金或违约金的计算方法。</p>
                                                                    <p>21.2.2	除专用合同条款另有约定外，总承包商发出整改通知后，施工分包商在指定的合理期限内仍不纠正违约行为，总承包商有权解除合同。</p>
                                                                    <p>21.2.3	因施工分包商违约解除合同</p>
                                                                    <p>总承包商按照第21.2.2项的约定解除合同的，总承包商有权暂停对施工分包商的付款；分包合同当事人应在分包合同解除后按专用合同条款的约定完成分包工程估价、清算和付款。</p>
                                                                    <p>合同当事人未能就解除合同后的结清达成一致的，按照第25款【争议解决】的约定处理。</p>
                                                                    <p>合同解除后，总承包商有权要求施工分包商将其为实施分包合同而签订的材料和设备的采购合同的权益转让给总承包商，施工分包商应在收到解除分包合同通知后14天内，协助总承包商与采购合同的供应商达成相关的转让协议。总承包商及其指定的其他施工分包商有权使用施工分包商在现场机械设备、材料、临时设施、施工文件中他们认为合适的部分，该部分将在总承包商认为合适的时间指令施工分包商自费撤出现场；其余部分将在接到总承包商书面指令后，由施工分包商立即自费撤出现场。总承包商继续使用的行为不免除或减轻施工分包商应承担的违约责任。</p>
                                                                    <p>22	不可抗力</p>
                                                                    <p>22.1	不可抗力的确认和通知</p>
                                                                    <p>不可抗力是指合同当事人在签订合同时不可预见，在合同履行过程中不可避免且不能克服的自然灾害和社会性突发事件，如地震、海啸、瘟疫、骚乱、戒严、暴动、战争和专用合同条款中约定的其他情形。</p>
                                                                    <p>
                                                                        分包合同一方当事人遇到不可抗力事件，使其履行合同义务受到阻碍时，应立即通知合同另一方当事人，书面说明不可抗力和受阻碍的详细情况，并提供必要的证明。合同双方当事人应在
                            所能及的条件下迅速采取措施，尽力减少损失。不可抗力事件结束后48小时内由施工分包商向总承包商通报受害情况和损失情况、预计清理和修复的费用。
                                                                    </p>
                                                                    <p>不可抗力事件持续发生，施工分包商应每隔7天向总承包商报告一次受害情况。不可抗力事件结束后14天内，施工分包商向总承包商提交清理和修复费用的正式报告及有关资料。</p>
                                                                    <p>22.2	不被认为是不可抗力的情形</p>
                                                                    <p>包括但不限于下列情况将被视为在施工分包商控制范围内，不被认为是不可抗力：</p>
                                                                    <p>（1）	由于任何分包商、供货商或运输商的违约或失误；</p>
                                                                    <p>（2）	施工分包商和/或其分包商、供货商或运输商人员的罢工、劳务纠纷或其它行为；</p>
                                                                    <p>（3）	市场设备/材料短缺以及运输短缺；</p>
                                                                    <p>（4）	连续降雨及暴雨；</p>
                                                                    <p>（5）	哄抢；</p>
                                                                    <p>（6）	专用合同条款中约定的其他情形。</p>
                                                                    <p>22.3	不可抗力风险的承担</p>
                                                                    <p>22.3.1	不可抗力发生前已完成的分包工程应当按照分包合同约定进行计量支付。</p>
                                                                    <p>22.3.2	不可抗力风险由分包合同当事人按以下原则承担：</p>
                                                                    <p>（1）	永久工程、已运至施工场地的材料和工程设备的损坏，以及因分包工程损坏造成的第三人人员伤亡和财产损失由总承包商承担；</p>
                                                                    <p>（2）	施工分包商施工设备的损坏由施工分包商承担；</p>
                                                                    <p>（3）	总承包商和施工分包商承担各自人员伤亡和财产的损失；</p>
                                                                    <p>（4）	因不可抗力影响施工分包商履行合同约定的义务，已经引起或将引起工期延误的，应当顺延工期，由此导致施工分包商停工或窝工的费用损失由施工分包商承担；</p>
                                                                    <p>（5）	因不可抗力引起或将引起工期延误，总承包商要求赶工的，由此增加的赶工费用由总承包商承担；</p>
                                                                    <p>（6）	在停工期间，施工分包商承担工程照管和成品保护工作，但按照总承包商要求清理分包工程的费用由总承包商承担。</p>
                                                                    <p>不可抗力发生后，合同当事人均应采取措施尽量避免和减少损失的扩大，任何一方当事人没有采取有效措施导致损失扩大的，应对扩大的损失承担责任。</p>
                                                                    <p>22.4	不可抗力的后果</p>
                                                                    <p>22.4.1	如果发生不可抗力事件，遭受不可抗力一方仅可因确认的不可抗力影响通知书迟延履行合同义务的免责，并且一旦这种影响履行合同义务的不可抗力消失、减弱、排除时，遭受不可抗力一方应当立即恢复履行合同义务。</p>
                                                                    <p>22.4.2	尽管发生不可抗力，但这种不可抗力不直接影响履行合同或履行合同部分义务时，声称遭受不可抗力影响的当事方不因此免除迟延履行合同义务或合同部分义务的责任。</p>
                                                                    <p>22.4.3	因合同一方迟延履行合同义务，在迟延履行期间遭遇不可抗力的，不免除其违约责任。</p>
                                                                    <p>22.5	因不可抗力解除合同</p>
                                                                    <p>因不可抗力导致分包合同无法履行连续超过84天或累计超过140天的，总承包商和施工分包商均有权解除分包合同。分包合同解除后28天内，双方当事人应按专用合同条款的约定确定总承包商应支付的款项并完成支付。</p>
                                                                    <p>22.6	不可抗力事件发生后的配合义务</p>
                                                                    <p>不可抗力事件发生后，施工分包商应按照总承包商的要求及时提交相应报告和尽可能详尽的资料、数据，以利总承包商向业主或保险公司索赔。</p>
                                                                    <p>不可抗力事件发生后，因施工分包商未能按照总承包商的要求及时提交相应报告和尽可能详尽的资料、数据，以至总承包商向业主或保险公司的索赔不成功，则前述总承包商所承担的相应责任不成立，相关责任由施工分包商承担。</p>
                                                                    <p>不可抗力事件发生后，双方都应始终尽所有合理的努力，使不可抗力对履行合同造成的任何延误减至最小。</p>
                                                                    <p>23	保险</p>
                                                                    <p>23.1	保险标准</p>
                                                                    <p>总承包商及施工分包商应选择财务状况良好、信誉高、有实力的保险公司承保。总承包商和施工分包商不应违反本条款以及保险单的条件，任何一方违反保险单规定的条件时，应承担由此造成的其它一方损失。</p>
                                                                    <p>23.2	保险的一般要求</p>
                                                                    <p>23.2.1	施工分包商追偿权的放弃</p>
                                                                    <p>每个施工分包商在其保险合同中应放弃对下列各方追偿的权利：总承包商及其代表和代理、总承包商指定的其他人员，以及他们的子公司、分支机构、雇员、受让人、保险人。上述追偿的权利是指：在发生本条规定的施工分包商应持有的保险单责任范围内的损失时，可以从中获得赔偿的部分。保险人知晓保险单中包含有上述放弃追偿权的证据，由施工分包商按本合同相关条款的要求提供。</p>
                                                                    <p>23.2.2	交叉责任条款</p>
                                                                    <p>为维护总承包商及其代表和代理、施工分包商的利益，总承包商和施工分包商购买的所有的保险单，应在责任险项下附加交叉责任条款，以免除所有被保险人之间的索赔。</p>
                                                                    <p>23.2.3	相关的保险凭证</p>
                                                                    <p>在任何现场工程动工前及在每一个日历年度结束后30天以内，施工分包商应向总承包商提供相关的保险凭证（或提供合理的符合业主要求的保险证明），以：（a）确定此日期起，根据上述条款规定应持有的所有保险单均有效；（b）确认签发保单的保险人名称；（c）确认保险人的保险责任金额、保单期满日期以及确定已支付到期应予支付的保费。</p>
                                                                    <p>23.2.4	续保</p>
                                                                    <p>在生效保单期满日前至少15天，施工分包商应向总承包商提交其已办理续保的证明。</p>
                                                                    <p>23.2.5	保险责任范围内的损失事故和变更的通知</p>
                                                                    <p>施工分包商应及时向总承包商通报：在本条规定的保险项下超过10万元人民币的任何实际的或潜在的索赔，以及在本条规定要持有的保险单项下，与任何潜在损失事故、保单注销、不利变化及违约有关的任何书面通知。</p>
                                                                    <p>23.2.6	保单注销</p>
                                                                    <p>尽管按照本条规定购买了保险，但并不能因保险公司无力清偿或破产，而免除各方重新购买或安排保险的责任。一旦出现任何按照本条规定需履行的保险单的撤销，或者出单的保险公司无力清偿、破产或无法履行的情况，负责保险购买方需要及时按要求的保险金额和保障范围购买新保险。</p>
                                                                    <p>23.2.7	保险索赔</p>
                                                                    <p>除非保单另有规定，在收到业主签发的接收证书前，施工分包商有责任及时通知总承包商及其保险人与承包工程有关的任何保险索赔，并协助总承包商进行索赔。如果施工分包商违反本款的规定，应赔偿总承包商由此造成的损失。</p>
                                                                    <p>23.2.8	程序和服务</p>
                                                                    <p>施工分包商应严格遵守所有的程序和服务，包括完成各项必要的投保单、及时满足有关的审核要求、索赔报告程序，以及应当全面参与总承包商要求的防灾防损工作。</p>
                                                                    <p>23.2.9	无免责</p>
                                                                    <p>总承包商对施工分包商提供的工程产品的接收，并不能视作减少本条所规定的施工分包商的任何责任或义务。</p>
                                                                    <p>23.2.10	任何未保险的或未能从承保人获得赔付的损失金额，应由总承包商、施工分包商根据本合同规定的责任负担。</p>
                                                                    <p>23.3	总承包商负责提供的保险</p>
                                                                    <p>23.3.1	建安工程一切险</p>
                                                                    <p>该险种将保障总承包商和所有施工分包商、供货商所从事的工程项下的永久性和临时工程的损失或毁损，但不保障在工程实施期间施工分包商所拥有或租用的机具、设备、自有财产的损失或毁损；总承包商及参与本项目的任何施工分包商/供货商等都作为上述保险合同项下的共同被保险人。</p>
                                                                    <p>23.3.2	第三者责任险</p>
                                                                    <p>该险种作为本项目建筑安装工程一切险的附加险种，保障在本保险期限内，因发生与本项目直接相关的意外事故引起工地范围内及临近区域的第三者人身伤亡、疾病或财产损失，依法应由被保险人承担的经济赔偿。</p>
                                                                    <p>23.3.3	雇主责任险</p>
                                                                    <p>在所购买的保险单的责任险项下附加交叉责任条款，以免除所有被保险人之间的索赔。</p>
                                                                    <p>23.3.4	施工分包商必须在总承包商办理相应保险之前向总承包商提供其包括其分包方（如果有）进驻本工程现场的人员数量。</p>
                                                                    <p>23.3.5	业主已经办理的保险视为总承包商办理的保险。</p>
                                                                    <p>23.4	施工分包商负责提供的保险</p>
                                                                    <p>23.4.1	施工分包商的保险范围按照国家及工程所在地地方政府的相关规定执行。施工分包商购买在本条款中规定的保险，并保证所投险种持续到施工分包商在本合同下的义务终止；如果施工分包商未履行本款规定，应承担因此造成的损害或赔偿。</p>
                                                                    <p>23.4.2	施工分包商负责提供的保险包括但不限于：</p>
                                                                    <p>（1）	工伤保险：施工分包商应依照法律规定参加工伤保险，并为其履行合同的全部员工办理工伤保险，缴纳工伤保险费，并要求其聘请的第三方依法参加工伤保险。</p>
                                                                    <p>（2）	意外伤害保险：施工分包商应根据有关法律及业主要求投保员工人身意外伤害保险。施工分包商投保人身意外伤害保险的赔偿限额按照合同专用条款的约定执行。</p>
                                                                    <p>（3）	施工机具保险：投保范围覆盖所有施工分包商为完成本合同项下工作而投入项目现场的所有自有、非自有以及租赁的施工机具及设备；施工机具保险的保险金额按照合同专用条款的约定执行。</p>
                                                                    <p>（4）	为本工程使用的车辆投保机动车辆保险，投保险种包括但不限于车辆损失险和第三者责任保险。</p>
                                                                    <p>23.4.3	上述第23.4.2项中规定施工分包商负责购买的所有保险单中，免赔额均由施工分包商单独承担。</p>
                                                                    <p>23.5	保险凭证</p>
                                                                    <p>施工分包商应在专用合同条款约定的期限内向总承包商提交已投保的各项保险的凭证和保险单复印件。</p>
                                                                    <p>24	索赔</p>
                                                                    <p>24.1	施工分包商的索赔</p>
                                                                    <p>根据分包合同约定，施工分包商认为有权得到追加付款和（或）延长工期的，应按以下程序向总承包商提出索赔：</p>
                                                                    <p>（1）	施工分包商应在知道或应当知道索赔事件发生后14天内，向总承包商递交索赔意向通知书，说明发生索赔事件的事由并提供相应事件的证明材料；施工分包商未在前述14天内发出索赔意向通知书的，丧失要求追加付款和（或）延长工期的权利；</p>
                                                                    <p>（2）	施工分包商应在发出索赔意向通知书后14天内，向总承包商正式递交索赔报告；索赔报告应详细说明索赔理由以及要求追加的付款金额和（或）延长的工期，并附必要的记录和证明材料；</p>
                                                                    <p>（3）	索赔事件具有持续影响的，施工分包商应按合理时间间隔继续递交延续索赔通知，说明持续影响的实际情况和记录，列出累计的追加付款金额和（或）工期延长天数；</p>
                                                                    <p>（4）	在索赔事件影响结束后14天内，施工分包商应向总承包商递交最终索赔报告，说明最终要求索赔的追加付款金额和（或）延长的工期，并附必要的记录和证明材料。</p>
                                                                    <p>24.2	对施工分包商索赔的处理</p>
                                                                    <p>除专用合同条款另有约定外，对施工分包商的索赔处理如下：</p>
                                                                    <p>（1）	总承包商应在收到索赔报告后35天内完成审查。总承包商对索赔报告存在异议的，有权要求施工分包商提交全部原始记录副本。总承包商应在收到索赔报告或有关原始记录副本后的35天内向施工分包商出具索赔处理结果。总承包商逾期答复的，视为认可施工分包商的索赔要求。</p>
                                                                    <p>（2）	施工分包商接受索赔处理结果的，索赔款项在当期进度款中进行支付；施工分包商不接受索赔处理结果的，按照第25款【争议解决】的约定处理。</p>
                                                                    <p>24.3	总承包商的索赔</p>
                                                                    <p>根据分包合同约定，总承包商认为有权得到赔付金额和（或）延长缺陷责任期的，应向施工分包商发出通知并附有详细的证明。</p>
                                                                    <p>总承包商应在知道或应当知道索赔事件发生后14天内向施工分包商提出索赔意向通知书。总承包商应在发出索赔意向通知书后14天内向施工分包商正式递交索赔报告。</p>
                                                                    <p>24.4	对总承包商索赔的处理</p>
                                                                    <p>除专用合同条款另有约定外，对总承包商的索赔处理如下：</p>
                                                                    <p>（1）	施工分包商收到总承包商提交的索赔报告后，应及时审查索赔报告的内容、查验总承包商证明材料；</p>
                                                                    <p>（2）	施工分包商应在收到索赔报告或有关索赔的进一步证明材料后35天内，将索赔处理结果答复总承包商。如果施工分包商未在上述期限内作出答复的，则视为对总承包商索赔要求的认可；</p>
                                                                    <p>（3）	总承包商接受索赔处理结果的，总承包商可从应支付给施工分包商的合同价款中扣除赔付的金额或延长缺陷责任期；总承包商不接受索赔处理结果的，按第25款【争议解决】的约定处理。</p>
                                                                    <p>24.5	提出索赔的期限</p>
                                                                    <p>（1）	施工分包商按第19.2款【结算审核】的约定认可结算付款证书后，应被视为已无权就结算付款证书签发前所发生的事项提出任何索赔。</p>
                                                                    <p>（2）	施工分包商按第19.3款【最终结清】提交的最终结清申请单，只限于提出结算付款证书签发后发生的索赔。施工分包商提出索赔的期限自最终结清申请单被总承包商确认时终止。</p>
                                                                    <p>24.6	施工分包商索赔配合</p>
                                                                    <p>当总承包商与业主之间发生的索赔事项涉及分包工程时，施工分包商应按总承包商要求配合总承包商处理前述索赔事项。</p>
                                                                    <p>25	争议解决</p>
                                                                    <p>25.1	调解</p>
                                                                    <p>分包合同当事人可以就争议请求建设行政主管部门、行业协会或其他第三方进行调解，调解达成协议的，经双方签字并盖章后作为合同补充文件，双方均应遵照执行。</p>
                                                                    <p>25.2	仲裁或诉讼</p>
                                                                    <p>因分包合同及合同有关事项产生的争议，分包合同当事人可以在专用合同条款中约定以下列任意一种方式解决争议：</p>
                                                                    <p>1、	向专用合同条款约定的仲裁委员会申请仲裁；</p>
                                                                    <p>2、	向专用合同条款约定的有管辖权的人民法院起诉。</p>
                                                                    <p>25.3	与争议解决有关的事项</p>
                                                                    <p>在总承包商与业主进入争议解决程序且争议事项涉及分包工程时，施工分包商应配合总承包商处理争议事项。如因施工分包商怠于配合导致总承包商无法从业主处保障相关权益时，施工分包商应当赔偿损失。</p>
                                                                </f:ContentPanel>
                                                            </Items>
                                                        </f:Panel>
                                                    </Items>
                                                </f:FormRow>
                                            </Rows>
                                        </f:Form>
                                    </Items>
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                                            <Items>
                                                <%--  <f:Button ID="btnTab3Save" Icon="SystemSave" runat="server" ValidateForms="frTestSet"
                                            OnClick="btnTab3Save_Click">
                                        </f:Button>--%>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                </f:Tab>
                                <f:Tab ID="Tab4" Title="专用条款" BodyPadding="5px" Layout="Fit" IconFont="Bookmark" runat="server">
                                    <Items>
                                        <f:Form ID="Form_Tab4" ShowBorder="false" ShowHeader="false" AutoScroll="true" Title="专用合同条款"
                                            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                            <Rows>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:Label ID="Label51" runat="server" Text="专用合同条款" CssClass="formtitle f-widget-header"></f:Label>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:Label runat="server" ID="Labe00" Text="1&nbsp;一般约定" CssClass="lab"></f:Label>

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
                                                            Width="120px" LabelAlign="right" >
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
                                                            Width="120px" LabelAlign="right" >
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
                                                            Width="120px" LabelAlign="left" >
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
                                                            Width="120px" LabelAlign="right" >
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
                                                        <f:TextBox runat="server" Label="异常恶劣的气候条件包括" LabelWidth="300" ID="BadWeatherInclude" AutoPostBack="true" OnTextChanged="TextBoxChanged"
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
                                                         <f:TextBox runat="server" Label="关于预付款扣回的约定：" LabelWidth="300" ID="PaymentAgreement" AutoPostBack="true" OnTextChanged="TextBoxChanged"
                                                            Width="120px" LabelAlign="left">
                                                        </f:TextBox>
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
                                                        <f:Label runat="server" ID="Label53" Text="14.3.3进度款审核与支付"></f:Label>
 
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
                                                        <f:TextBox runat="server" Label="16.1试车的组织和配合" LabelWidth="300" ID="TextBox150" AutoPostBack="true" OnTextChanged="TextBoxChanged"
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
                                                            Width="120px" LabelAlign="left" >
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
                                                            Width="120px" LabelAlign="left" >
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
                                                         <f:Label runat="server" ID="Label1" Text="22.5因不可抗力解除合同" Width="120px" LabelAlign="left"></f:Label>
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
                                                            Width="120px" LabelAlign="right" >
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
                                                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="专用合同条款附件" EnableCollapse="true"
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
                                        </f:Form>
                                    </Items>
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar4" Position="Top" ToolbarAlign="Right" runat="server">
                                            <Items>
                                                <f:Button ID="btnSave_Tab4" ToolTip="保存" Text="保存" Icon="SystemSave" runat="server" Size="Medium"
                                                    OnClick="btnSave_Tab4__Click">
                                                </f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                </f:Tab>
                            </Tabs>
                        </f:TabStrip>
    
                    </Items>
                </f:Panel>
            </Items>
        </f:Form>

        <f:Window ID="Window1" Title="流程步骤设置" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" OnClose="Window1_Close"
            Width="1000px" Height="620">
        </f:Window>
        <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
    </form>
    <script type="text/javascript">      
        function onGridDataLoad(event) {
            this.mergeColumns(['FlowStep', 'GroupNum'], { depends: true });
        }
        // 同时只能选中一项
        function onCheckBoxListChange(event, checkbox, isChecked) {
            var me = this;

            // 当前操作是：选中
            if (isChecked) {
                // 仅选中这一项
                me.setValue(checkbox.getInputValue());
            }


            __doPostBack('', 'CheckBoxList1Change');
        }

    </script>
</body>
</html>

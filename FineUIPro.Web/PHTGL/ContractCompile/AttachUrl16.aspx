<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachUrl16.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.AttachUrl16" %>

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
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="编辑附件16" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label1" runat="server" Text="附件16 施工质量奖惩管理规定 " CssClass="formtitle f-widget-header" ></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" ShowBorder="false">
                            <f:Label runat="server" ID="Label2" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1	适用范围"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label3" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;本规定适用于公司EPC工程总承包及不同承包模式的工程项目施工质量奖惩的管理。"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label4" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2	管理职责"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label5" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.1  施工专业工程师负责对符合本规定的奖罚事项/事由向施工经理提议。"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label6" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.2  施工经理负责审批（必要时项目经理审批），并组织向施工分包商签发奖罚通知，组织奖罚会议等。"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label7" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3	奖惩依据"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label8" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.1  总承包商与施工分包商签订的施工分包合同相关条款。"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label9" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.2  项目发布的相关管理规定。"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label10" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4	奖惩细则"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label11" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.1  奖励细则"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label12" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.1.1总承包商施工专业工程师在施工过程中抽查分部、分项工程的自检记录、质量检验评定、施工资料是否与施工同步以及A、B级控制点执行情况，符合设计和标准规范要求的，且对工程实体抽查允许偏差项目的抽检点数中，建筑工程、安装工程有97％以上数据在允许偏差范围内的；焊接一次合格率在97％以上者，奖励施工分包商有关人员100~200元。"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label13" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.1.2由总承包商、监理或业主组织的质量大检查中，工程实体抽查允许偏差项目的抽检点数中，建筑工程、安装工程有97％以上数据在允许偏差范围内的；焊接一次合格率在97％以上者，一次性奖励施工分包商1000~3000元。"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label14" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.1.3由集团公司级别相关部门组织的工程质量大检查中，工程实体抽查允许偏差项目的抽检点数中，建筑工程、安装工程有97％以上数据在允许偏差范围内的；焊接一次合格率在97％以上者，一次性奖励施工分包商3000~5000元。"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label15" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.2  处罚细则"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label16" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;当发生下列情况时，属于施工分包商违约，施工分包商承担违约责任。违约金及违约罚款总额不超过合同金额的10％。"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label17" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.2.1施工分包商无专职质检员，或者专职质检员无证上岗，或者各施工分包商未对施工现场专职质检员、技术人员和试验人员进行质量培训考核的，违约罚金：5万元/每人。"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label18" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;a)	因施工质量影响试车、开工；"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label19" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;b)	因施工质量造成设备、材料损坏；"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label20" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;c)	工程质量不合格经设计人员认可不影响使用；"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label21" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;d)	重大工程质量事故。"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label22" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.2.3因施工质量发生安全事故，除按国家有关安全事故处理规定处理外，还需承担违约罚金：20万元人民币/每次。"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label23" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.2.4分项、分部、单位工程不合格，施工分包商除履行返修、重修、赔偿相应损失外，还需相应承担下列违约罚金："></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label24" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;a)	分项工程不合格，违约罚金：2000元人民币/每处；"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label25" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;b)	分部工程不合格，违约罚金：5万元人民币/每处；"></f:Label>
                            <br />
                             <f:Label runat="server" ID="Label26" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;c)	单位工程不合格，违约罚金：10万人民币/每处。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label27" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.2.5对有章不循，不执行业主及总承包商有关的施工质量管理规定和管理细则，出现下列情况之一的，违约罚金：1000元人民币/每次。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label28" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;a)	施工分包商不进行三级（施工人员、班组长、质检员）检查；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label29" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;b)	施工质量不自检或未自检合格，直接报总承包商或业主/监理的；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label30" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;c)	隐蔽工程不报检擅自隐蔽的；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label31" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;d)	特殊工种无证上岗的；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label32" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;e)	焊工上岗操作项目与所持有效证件及考试项目不符的；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label33" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;f)	原材料、零部件、整机没有合格证就使用的；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label34" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;g)	材料代用无审批手续的；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label35" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;h)	原材料不合格，以次充好，以旧代新的；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label36" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;i)	无损检查不符合设计文件及有关施工验收规范的；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label37" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;j)	未按标准、规范施工的其它工程质量问题。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label38" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.2.6对工程质量通病的违约罚金：100元人民币/每处。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label39" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.2.7交工技术文件有关罚则"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label40" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;a)	提交的交工技术文件中工程质量保证资料弄虚作假，数据不真实，不可靠、不完整，违约罚金：500元/每项；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label41" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;b)	交工技术文件中施工质量保证资料印章不全、字迹不清、记录不准确；工程质量保证资料缺项、漏项，违约罚金：50元/每项；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label42" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;c)	工程资料与工程施工不同步，违约罚金：50元/每次；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label43" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;d)	交工技术文件不按期提交，每拖期1天违约罚金：5000元/每天。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label44" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.2.8施工分包商由于施工方法不当、管理不力、程序不对、施工质量达不到合同规定或有关约定，导致以下情况发生的，承担以下违约罚金："></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label45" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;a)	业主、监理、质量监督站、总承包商下发质量不合格整改通知单、监理通知单等，施工分包商未按要求进行整改的，违约罚金：1000元/每单；自罚款第二日起，问题整改每延期一天，追罚500元/天；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label46" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;b)	业主、监理、质量监督站下发罚款通知单的，除承担业主、监理、质量监督站的全额处罚外，总承包商再追加一倍罚款；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label47" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;c)	施工分包商对总承包商、监理、业主工程管理人员提出的工程质量问题无理纠缠或人身攻击，违约罚金：500元/每人每次；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label48" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;d)	施工分包商擅自变更施工组织设计、安全技术措施、现有设施保护措施、施工方案，除了总承包商不支付擅自变更部分的措施费或扣除因擅自变更措施（方案）而相应减少的费用外，施工分包商还须承担违约罚金：1万元/每次每项；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label49" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;e)	施工分包商擅自变更设计内容，违约罚金：2万元/每次每项；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label50" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;f)	施工分包商提交的工程量计量报告或索赔报告不实，导致总承包商项目管理人员加大工作量的，承担总承包商的经济损失：1000元/每人每天；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label51" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;g)	施工分包商擅自分包工程，除了承担总承包商的全部损失外，总承包商还将没收履约保函直至终止合同；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label52" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;h)	施工分包商违反保密约定，除了承担总承包商的全部损失外，施工分包商还须承担合同总价的1%~2%的违约金；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label53" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;i)	施工分包商有违法违纪行为，致使总承包商名誉或经济受到损害的，除了承担总承包商的全部损失外，还须承担违约罚金：5000元/每次每项。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label54" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.2.9施工分包商不按合同约定履行义务的其它情况违约，除了承担总承包商的全部损失外，视情节轻重，施工分包商还须承担合同总价的1%~5%的违约金。"></f:Label>
                            <br />


                            </f:ContentPanel>
                        

                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1"  Position="Top" ToolbarAlign="Right" runat="server" >
                    <Items>
                        <f:ToolbarFill runat="server">
                        </f:ToolbarFill>
                        <%--<f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                        </f:Button>--%>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
</body>
</html>

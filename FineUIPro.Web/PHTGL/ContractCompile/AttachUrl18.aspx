<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachUrl18.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.AttachUrl18" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑附件</title>
    <style>
        .widthBlod {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="编辑附件18" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label1" runat="server" Text="附件18    落实施工作业人员待遇承诺书" CssClass="widthBlod"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" ShowBorder="false">
                       <%-- <f:HtmlEditor runat="server" Label="落实施工作业人员待遇承诺书" ID="HtmlEditor1" Editor="UMEditor" BasePath="~/res/third-party/umeditor/"  Height="450px">
                </f:HtmlEditor>--%>
                            <f:Label runat="server" ID="Labe00" Text="致："></f:Label>
                        <f:TextBox runat="server" Label="" ID="txtGeneralContractorName" AutoPostBack="true"  
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                            <br />
                        <f:Label runat="server" ID="Label2" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;由"></f:Label>
                        <f:TextBox runat="server" Label="" ID="txtSubcontractorsName" AutoPostBack="true"  
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>   
                        <f:Label runat="server" ID="Label3" Text="（以下称：本公司）承接的贵公司总承包项目下"></f:Label>
                        <f:TextBox runat="server" Label="" ID="txtProjectName" AutoPostBack="true"  
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox>
                        <f:Label runat="server" ID="Label4" Text="工程"></f:Label>
                         
                         <f:Label runat="server" ID="Label5" Text="（合同编号："></f:Label>
                        <f:TextBox runat="server" Label="" ID="txtContractId" AutoPostBack="true"  
                                        Width="120px" LabelAlign="right">
                                    </f:TextBox> 
                            <f:Label runat="server" ID="Label6" Text="）的施工工作,"></f:Label>
                            <br />
                            <f:DatePicker runat="server" Required="true" DateFormatString="yyyy-MM-dd" Label="工程合同工期从" EmptyText="请选择开始日期"
                                ID="txtStartDate" ShowRedStar="true">
                            </f:DatePicker>
                            <f:DatePicker runat="server" Required="true" DateFormatString="yyyy-MM-dd" Label="至" EmptyText="请选择开始日期"
                                ID="txtEndDate" ShowRedStar="true">
                            </f:DatePicker>
                            <br />
                             <f:Label ID="Label30" runat="server" Text="计划使用施工作业人员"></f:Label>
                            <f:NumberBox ID="txtPersonSum" runat="server"></f:NumberBox>
                            <f:Label ID="Label31" runat="server" Text="人次"></f:Label>
                            <br />
                              <f:Label runat="server" ID="Label7" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.	完全遵守并落实执行国家、工程所在地地方政府、业主、总承包商关于保护施工作业人员合法权益的各项法律、法规、规章和规定等相关要求。"></f:Label>
                            <br />
                              <f:Label runat="server" ID="Label8" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.	确保本公司合同工程现场项目部有关施工作业人员使用的组织管理、制度约束、设施建设及费用投入满足要求。本公司总部将认真履行监督、检查、督促整改的管理职能，一旦发现问题或接到总承包商及其他相关方的投诉，将及时处理并整改完善。"></f:Label>
                            <br />
                              <f:Label runat="server" ID="Label9" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.	贯彻执行“谁使用、谁管理”的原则，确保施工作业人员使用按照建制接收，签订劳务协议，建立完整的施工作业人员档案，及时上报贵公司备案，不接收零散农民工。"></f:Label>
                            <br />
                              <f:Label runat="server" ID="Label10" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.	确保对所有施工作业人员进行有效的岗位安全知识教育、作业前技术交底，确保施工作业人员有效参与事故应急预案演练。"></f:Label>
                            <br />
                              <f:Label runat="server" ID="Label11" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.	确保施工作业人员个人劳动保护用品的配置率及合格率100%，并满足总承包商的管理规定。"></f:Label>
                            <br />
                              <f:Label runat="server" ID="Label12" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.	确保施工作业人员生活环境及设施符合职业健康和安全的需要，并满足总承包商的管理规定。"></f:Label>
                            <br />
                              <f:Label runat="server" ID="Label13" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;7.	确保施工作业人员享受合理休息、休假及医疗的权益。"></f:Label>
                            <br />
                              <f:Label runat="server" ID="Label14" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.	确保按照法律规定参加工伤保险，并为本公司履行合同的全部员工办理工伤保险，缴纳工伤保险费；为从事危险作业的人员办理意外伤害保险，支付保险费。"></f:Label>
                            <br />
                              <f:Label runat="server" ID="Label15" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.	确保施工作业人员工资按月发放，并随时接受总承包商及相关管理机构的检查；承诺施工作业人员月工资足额发放率100%，不以工程款未到位等为由克扣或拖欠施工作业人员工资,不将合同应收工程款等经营风险转嫁给施工作业人员。"></f:Label>
                            <br />
                              <f:Label runat="server" ID="Label16" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.	若本公司工程施工范围内出现拖欠施工作业人员工资、或施工作业人员聚众讨薪、或施工作业人员上访讨薪："></f:Label>
                            <br />
                              <f:Label runat="server" ID="Label17" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（1）	总承包商无需得到本公司许可或同意，在通知本公司后可以直接从本公司工程款中扣减同等数额的价款直接发放相关施工作业人员，该部分价款视为已经支付本公司"></f:Label>
                            <br />
                              <f:Label runat="server" ID="Label18" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（2）	每发生一起施工作业人员聚众讨薪、或施工作业人员上访讨薪，不管责任方是否属于本公司，本公司均接受总承包商的相应罚款，罚款金额为20万元。"></f:Label>
                            <br />
                              <f:Label runat="server" ID="Label19" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（3）	因拖欠施工作业人员工资造成总承包商任何直接的或间接的损失，本公司愿意无条件承担。"></f:Label>
                            <br />
                              <f:Label runat="server" ID="Label20" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;11.	当总承包商支付工程款达到合同约定的支付比例时，确保施工作业人员工资结算和兑现率100%；在本公司提供施工作业人员工资结清证明后，总承包商方办理后续款项的结算和支付。"></f:Label>
                            <br />
                              <f:Label runat="server" ID="Label21" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;12.	由于本公司违反上述承诺所造成的相关事件、增加费用和处罚等将由本公司承担全部责任。"></f:Label>
                            <br />
                              <f:Label runat="server" ID="Label22" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;13.	当本公司违反上述承诺，对合同工程造成不利影响时，总承包商有权依据自身判断，采取相关措施予以应急处理，本公司坚决服从总承包商的处理决定，承担全部责任以及由此给总承包商造成的费用增加。"></f:Label>
                            <br />
                              <f:Label runat="server" ID="Label23" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;14.	本承诺书与主合同同时签订，其有效期为自本公司签字盖章后至主合同履行完毕为止。"></f:Label>
                            <br />
                             <br />
                              <f:Label runat="server" ID="Label24" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;施工分包商："     OffsetRight="100"></f:Label>
                            <br />
                              <f:Label runat="server" ID="Label25" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（公章或合同专用章）"></f:Label>
                            <br />
                            <br />
                              <f:Label runat="server" ID="Label26" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;法定代表人或其委托代理人："></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label28" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（签字）"></f:Label>
                             <br />
                             <br />
                            <f:Label runat="server" ID="Label27" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日期：    年    月    日" LabelAlign="Left"></f:Label>
                            <br />
                            </f:ContentPanel>
                        

                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill runat="server">
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
    </form>
</body>
</html>

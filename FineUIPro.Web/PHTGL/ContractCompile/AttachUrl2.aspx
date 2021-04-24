<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachUrl2.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.AttachUrl2" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑附件2</title>
    <style>
        .widthBlod {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:Panel ID="Panel4" IsFluid="true" CssClass="blockpanel" BodyPadding="10" Layout="VBox" MaxHeight="550" BoxConfigChildMargin="0 0 5 0" AutoScroll="true"
            EnableCollapse="true" Title="附件2    合同价格及支付办法" runat="server">
            <Items>
                <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" Title="" AutoScroll="true"
                    runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                    <Items>
                        <f:CheckBoxList ID="CheckBoxList1" Label="请选择支付办法" runat="server" ColumnNumber="1"  OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged" AutoPostBack="true">
                            <f:CheckItem Text="1、	合同总价" Value="1" />
                            <f:CheckItem Text="2、	综合单价" Value="2" />
                            <f:CheckItem Text="3、	综合费率" Value="3" />
                            <f:CheckItem Text="4、	定额计价总价下浮" Value="4" />
                            <f:CheckItem Text="5、	计日工价格、签证台班价格" Value="5" />
                            <f:CheckItem Text="6、	试车保运费 " Value="6" />
                        </f:CheckBoxList>
                    </Items>
                </f:Form>
                <f:GroupPanel Title="" BodyPadding="10" ID="GroupPanel1" EnableCollapse="true" Collapsed="true" runat="server">
                    <Items>
                        <f:SimpleForm ID="SimpleForm2" ShowBorder="false" ShowHeader="false" runat="server">
                            <Items>

                                <f:TextBox ID="txtContractPrice" runat="server" Label="1、	合同总价" LabelWidth="120px" Text="固定总价或暂定总价         元，价格分项详见附件3《工程价格清单》。"></f:TextBox>

                            </Items>
                        </f:SimpleForm>
                    </Items>
                </f:GroupPanel>
                <f:GroupPanel Title="" BodyPadding="10" ID="GroupPanel2" EnableCollapse="true" Collapsed="true" runat="server">
                    <Items>
                        <f:SimpleForm ID="SimpleForm3" ShowBorder="false" ShowHeader="false" runat="server">
                            <Items>

                                <f:TextBox ID="txtComprehensiveUnitPrice" runat="server" Label="2、	综合单价" LabelWidth="120px" Text="详见附件3《工程价格清单》。"></f:TextBox>

                            </Items>
                        </f:SimpleForm>
                    </Items>
                </f:GroupPanel>
                <f:GroupPanel Title="" BodyPadding="10" ID="GroupPanel3" EnableCollapse="true" Collapsed="true" runat="server">
                    <Items>
                        <f:SimpleForm ID="SimpleForm4" ShowBorder="false" ShowHeader="false" runat="server">
                            <Items>

                                <f:Label ID="Label6" runat="server" Label="3、	综合费率" LabelWidth="120px"></f:Label>


                                <f:TextBox ID="txtComprehensiveRate1" runat="server" Label="（1）" LabelWidth="65px" Text="土建工程综合费率   ，安装工程（除电仪）综合费率  ，电仪安装工程综合费率"></f:TextBox>

                                <f:TextBox ID="txtComprehensiveRate2" runat="server" Label="（2）	预算定额执行" LabelWidth="155px"></f:TextBox>

                                <f:TextBox ID="txtComprehensiveRate3" runat="server" Label="（3）	人工费的调整原则" LabelWidth="180px" Text="人工综合工日单价调整执行文件，人工调差部分只计取税金，不计取其他各项费用。"></f:TextBox>

                                <f:TextBox ID="txtComprehensiveRate4" runat="server" Label="（4）	材料费的调整原则" LabelWidth="180px" Text="材料费按照工程项目所在地同期信息价调整，信息价没有的，按照双方认价执行。"></f:TextBox>

                                <f:TextBox ID="txtComprehensiveRate5" runat="server" Label="（5）	施工机械费的调整原则" LabelWidth="210px" Text="施工机械费只调整机上人工费，其余不予调整。机上人工费的调账按照人工费的调整原则执行。"></f:TextBox>

                            </Items>
                        </f:SimpleForm>
                    </Items>
                </f:GroupPanel>
                <f:GroupPanel Title="" BodyPadding="10" ID="GroupPanel4" EnableCollapse="true" Collapsed="true" runat="server">
                    <Items>
                        <f:SimpleForm ID="SimpleForm5" ShowBorder="false" ShowHeader="false" runat="server">
                            <Items>

                                <f:Label ID="Label7" runat="server" Text="4、	定额计价总价下浮："></f:Label>


                                <f:TextBox ID="txtTotalPriceDown1" runat="server" Label="（1）" LabelWidth="65px" Text="土建工程下浮率   ，安装工程（除电仪）下浮率   ，电仪安装工程下浮率   。"></f:TextBox>

                                <f:TextBox ID="txtTotalPriceDown2" runat="server" Label="（2）	预算定额执行" LabelWidth="155px"></f:TextBox>

                                <f:TextBox ID="txtTotalPriceDown3" runat="server" Label="（3）	人工费的调整原则" LabelWidth="180px" Text="人工综合工日单价调整执行文件，人工调差部分只计取税金，不计取其他各项费用。"></f:TextBox>

                                <f:TextBox ID="txtTotalPriceDown4" runat="server" Label="（4）	材料费的调整原则" LabelWidth="180px" Text="材料费按照工程项目所在地同期信息价调整，信息价没有的，按照双方认价执行。"></f:TextBox>

                                <f:TextBox ID="txtTotalPriceDown5" runat="server" Label="（5）	施工机械费的调整原则" LabelWidth="210px" Text="施工机械费只调整机上人工费，其余不予调整。机上人工费的调整按照人工费的调整原则执行。"></f:TextBox>

                            </Items>
                        </f:SimpleForm>
                    </Items>
                </f:GroupPanel>
                <f:GroupPanel Title="" BodyPadding="10" ID="GroupPanel5" EnableCollapse="true" Collapsed="true" runat="server">
                    <Items>
                        <f:SimpleForm ID="SimpleForm6" ShowBorder="false" ShowHeader="false" runat="server">
                            <Items>

                                <f:Label ID="Label8" runat="server" Text="5、	计日工价格、签证台班价格"></f:Label>

                                <f:Panel Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                    <Items>
                                        <f:Label ID="Label9" runat="server" Text="（1）	计日工：技术工种" ColumnWidth="18%"></f:Label>
                                        <f:NumberBox ID="txtTechnicalWork" runat="server" ColumnWidth="20%"></f:NumberBox>
                                        <f:Label ID="Label10" runat="server" Text="元/人/天，力工" ColumnWidth="12%"></f:Label>
                                        <f:NumberBox ID="txtPhysicalLaborer" runat="server" ColumnWidth="20%"></f:NumberBox>
                                        <f:Label ID="Label11" runat="server" Text="元/人/天" ColumnWidth="30%"></f:Label>
                                    </Items>
                                </f:Panel>

                                <f:Label ID="Label12" runat="server" Text="（2）	签证台班价格：附表"></f:Label>

                                <f:Grid ID="Grid1" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" Title="吊车" EnableCollapse="false"
                                    runat="server" DataKeyNames="AttachUrlDetaild" AllowCellEditing="true" ClicksToEdit="1"
                                    EnableColumnLines="true" DataIDField="AttachUrlDetaild" Height="200px">
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar2" runat="server" Position="Top" ToolbarAlign="Left">
                                            <Items>
                                                <f:Label ID="lbl" runat="server" Text="吊车"></f:Label>
                                                <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                                <f:Button ID="btnNew" ToolTip="增加" Icon="Add" EnablePostBack="false" runat="server">
                                                </f:Button>
                                                <f:Button ID="btnDelete" ToolTip="删除" Icon="Delete" EnablePostBack="false" runat="server">
                                                </f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Columns>
                                        <f:RowNumberField HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center" Width="60px"></f:RowNumberField>
                                        <f:RenderField Width="200px" ColumnID="Specifications" DataField="Specifications" FieldType="String"
                                            HeaderText="规格型号" HeaderTextAlign="Center">
                                            <Editor>
                                                <f:TextBox ID="txtSpecifications" runat="server"></f:TextBox>
                                            </Editor>
                                        </f:RenderField>
                                        <f:RenderField Width="150px" ColumnID="MachineTeamPrice" DataField="MachineTeamPrice" FieldType="Float"
                                            HeaderText="台班价格" HeaderTextAlign="Center">
                                            <Editor>
                                                <f:NumberBox ID="txtMachineTeamPrice" runat="server" NoNegative="true"></f:NumberBox>
                                            </Editor>
                                        </f:RenderField>
                                        <f:RenderField Width="100px" ColumnID="Remark" DataField="Remark" ExpandUnusedSpace="true"
                                            HeaderText="备注" HeaderTextAlign="Center">
                                            <Editor>
                                                <f:TextBox ID="txtRemarks" runat="server">
                                                </f:TextBox>
                                            </Editor>
                                        </f:RenderField>
                                    </Columns>
                                </f:Grid>

                                <f:Grid ID="Grid2" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" Title="装载机" EnableCollapse="false"
                                    runat="server" DataKeyNames="AttachUrlDetaild" AllowCellEditing="true" ClicksToEdit="1"
                                    EnableColumnLines="true" DataIDField="AttachUrlDetaild" Height="200px">
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar3" runat="server" Position="Top" ToolbarAlign="Left">
                                            <Items>
                                                <f:Label ID="Label18" runat="server" Text="装载机"></f:Label>
                                                <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>
                                                <f:Button ID="btnAdd" ToolTip="增加" Icon="Add" EnablePostBack="false" runat="server">
                                                </f:Button>
                                                <f:Button ID="btnDel" ToolTip="删除" Icon="Delete" EnablePostBack="false" runat="server">
                                                </f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Columns>
                                        <f:RowNumberField HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center" Width="60px"></f:RowNumberField>
                                        <f:RenderField Width="200px" ColumnID="Specifications" DataField="Specifications" FieldType="String"
                                            HeaderText="规格型号" HeaderTextAlign="Center">
                                            <Editor>
                                                <f:TextBox ID="Specifications" runat="server"></f:TextBox>
                                            </Editor>
                                        </f:RenderField>
                                        <f:RenderField Width="150px" ColumnID="MachineTeamPrice" DataField="MachineTeamPrice" FieldType="Float"
                                            HeaderText="台班价格" HeaderTextAlign="Center">
                                            <Editor>
                                                <f:NumberBox ID="MachineTeamPrice" runat="server" NoNegative="true"></f:NumberBox>
                                            </Editor>
                                        </f:RenderField>
                                        <f:RenderField Width="100px" ColumnID="Remark" DataField="Remark" ExpandUnusedSpace="true"
                                            HeaderText="备注" HeaderTextAlign="Center">
                                            <Editor>
                                                <f:TextBox ID="Remark" runat="server">
                                                </f:TextBox>
                                            </Editor>
                                        </f:RenderField>
                                    </Columns>
                                </f:Grid>

                                <f:Panel ID="Panel12" runat="server" ShowHeader="false" ShowBorder="false">
                                    <Items>
                                        <f:ContentPanel ID="ContentPanel112" runat="server" ShowHeader="false" ShowBorder="false">
                                            <p>说明：1、本表价格均为人民币；</p>
                                            <p>2、本表价格均为含税全费用价格；</p>
                                            <p>3、本表价格均不再另计进出场费；</p>
                                            <p>4、本表价格中未包括相应型号的价格按照同类机械前后两种进行插值确认。</p>
                                        </f:ContentPanel>
                                    </Items>
                                </f:Panel>

                                <f:Panel ID="Panel2" runat="server" ShowHeader="false" ShowBorder="false">
                                    <Items>
                                        <f:ContentPanel ID="ContentPanel2" runat="server" ShowHeader="false" ShowBorder="false">
                                            <p>（3）	价格执行原则：零星用工和零星机械或现场签证使用的机械时采用计日工和签证台班价格。</p>
                                            <p>（4）	零星用工和零星机械或现场签证使用的机械的工程量执行现场签证。</p>
                                            
                                        </f:ContentPanel>
                                    </Items>
                                </f:Panel>

                            </Items>
                        </f:SimpleForm>
                    </Items>
                </f:GroupPanel>
                <f:GroupPanel Title="" BodyPadding="10" ID="GroupPanel6" EnableCollapse="true" Collapsed="true" runat="server">
                    <Items>
                        <f:SimpleForm ID="SimpleForm7" ShowBorder="false" ShowHeader="false" runat="server">
                            <Items>

                                <f:Label ID="Label19" ColumnWidth="67%" runat="server" Text="6、 试车保运费"></f:Label>
                                <f:Panel Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                    <Items>
                                        <f:Label ID="Label13" ColumnWidth="67%" runat="server" Text="将各种费用折合成工日单价进行计算，该工日单价为固定全费用综合单价形式，价格标准为技术工种"></f:Label>
                                        <f:NumberBox ID="txtTestCar1" ColumnWidth="15%" runat="server"></f:NumberBox>
                                        <f:Label ID="Label14" runat="server" ColumnWidth="18%" Text="元/人/日（含税）、"></f:Label>
                                    </Items>
                                </f:Panel>
                                <f:Panel Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                    <Items>
                                        <f:Label ID="Label16" runat="server" ColumnWidth="10%" Text="非技术工种"></f:Label>
                                        <f:NumberBox ID="txtTestCar2" runat="server" ColumnWidth="15%"></f:NumberBox>
                                        <f:Label ID="Label15" runat="server" ColumnWidth="75%" Text="元/人/日（含税）。"></f:Label>
                                    </Items>
                                </f:Panel>

                                <f:Panel ID="Panel1" runat="server" ShowHeader="false" ShowBorder="false">
                                    <Items>
                                        <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" ShowBorder="false">
                                            本固定全费用综合单价已经包括了除无损检测以及保运工作所需的主要材料、设备、专用工具、备品备件以外，为完成总承包商安排工作所发生的一切费用（包括焊机、常用工具、辅助材料、手段用料、脚手架等等和HSE费用、临时设施、夜间值班等措施费），不随工作范围、协议期限、人员数量及进出厂时间、税票开具和有关风险等的变化而调整，在分包合同执行期间综合单价固定不变。
                                        </f:ContentPanel>
                                    </Items>
                                </f:Panel>


                                <f:Panel ID="Panel3" runat="server" ShowHeader="false" ShowBorder="false">
                                    <Items>
                                        <f:ContentPanel ID="ContentPanel3" runat="server" ShowHeader="false" ShowBorder="false">
                                            保运工作所需的主要材料、设备、备品备件原则上由总承包商供应，特殊情况下由施工分包商采购的材料、设备、备品备件，按总承包商委托和签证为准据实结算。吊装及运输机具费用按总承包商委托和签证计取。
                                        </f:ContentPanel>
                                    </Items>
                                </f:Panel>

                            </Items>
                        </f:SimpleForm>
                    </Items>
                </f:GroupPanel>

                <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="编辑附件1" AutoScroll="true"
                    runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:Label ID="Label1" runat="server" Text="附件2    合同价格及支付办法" CssClass="widthBlod"></f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:Label ID="Label2" runat="server"></f:Label>
                                <f:Label ID="Label3" runat="server" Text="合同价格及支付办法"></f:Label>
                                <f:Label ID="Label4" runat="server"></f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:Label ID="Label5" runat="server" Text="一、	合同价格"></f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:Label ID="Label17" runat="server" Text="二、	合同价款支付办法（参考示例）"></f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:HtmlEditor runat="server" Label="合同价款支付办法" ID="txtPayWay" ShowLabel="false"
                                    Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="240" LabelAlign="Right" Text="1、	预付款
按照专用合同条款14.1款有关规定执行。
2、	工程进度款（适用于单价合同）
（1）	施工分包商在收到施工图后60天内向总承包商报送施工图预算和工程量计算书，双方完成费用核对后，将该部分工程费用以总价形式明确作为后续工程款支付和工程结算的依据。在施工图预算双方核对一致前，总承包商按照各装置各专业清单工程量进行工程款支付。
（2）	进度付款申请报告应包括的内容：进度付款申请表、当期进度款预结算书、当期完成工程量审核清单、财务收据、税票及其他有关资料。
（3）	进度付款申请报告应当在每月25日报送总承包商，体现上月25日到本月24日当期实际完成的工作量所应付的金额。
（4）	工程进度款按经总承包商审核确认的工程进度或工程量计算，计算值作为当期工程进度款应付金额。
（5）	总承包商将在当期工程进度核定金额中直接扣减    %的预算保留金、  %的质量保证金，结算值作为当期工程进度款应支付金额（当期核定金额的80%，其中含10%的预付款），并以此向施工分包商签发工程进度款支付证书。
（6）	施工分包商完成施工图预算，并与总承包商核对一致后，总承包商向施工分包商付款至预结算金额的    %。
（7）	分包工程竣工验收合格，双方完成竣工结算且经总承包商委托的第三方机构审计后总承包商向施工分包商付款至竣工结算金额的   %。
（8）	分包合同履行过程中发生的变更、索赔、奖励或罚款等费用，随当期工程进度款一并报审、同期支付。
（9）	上述工程预算保留金的暂扣或作为罚款的扣减，不影响合同条款有关管理规定的执行和施工分包商有关义务的履行。
（10）	保运费（分包工程范围外的）按照考勤记录进行核定，每月据实预结算一次，100%支付施工分包商。
（11）	施工分包商用水、用电费用按照现场计量表数量进行费用核定，总承包商代扣代缴，在工程进度款中进行扣减。
3、	工程进度款（适用于固定总价合同）
（1）	施工分包商应根据通用合同条款第7.1款【施工组织】施工组织设计中批准的施工进度计划以及签约合同价、工程量等因素对合同固定总价按月进行分解，编制支付分解表，在收到总承包商批准的施工进度计划后____天内，将支付分解表及编制支付分解表的支持性资料报送总承包商。总承包商应在收到后____天内完成审批，经批准的支付分解表为有约束力的支付依据。
（2）	进度付款申请报告应当在每月25日报送总承包商，体现上月25日到本月24日当期实际完成的工作量所应付的金额。
（3）	进度付款申请报告应包括的内容：进度付款申请表、当期进度款预结算书、财务收据、税票及其他有关资料。
（4）	总承包商收到施工分包商提交的进度款申请报告后，对工程量及工程进度是否符合进度计划进行确认，确认符合后14天内，由总承包商按照专用合同条款14.1款进行预付款的扣回，向施工分包商支付相应付款周期的合同价款；
（5）	分包合同履行过程中发生的变更、索赔、奖励或罚款等费用，随当期工程进度款一并报审、同期支付。
（6）	保运费（分包工程范围外的）按照考勤记录进行核定，每月据实预结算一次，100%支付施工分包商。
（7）	施工分包商用水、用电费用按照现场计量表数量进行费用核定，总承包商代扣代缴，在工程进度款中进行扣减。
4、	质量保证金
工程结算金额的    %作为质量保证金。
在缺陷责任期满，双方完成最终结清且总承包商收到业主退还的质量保证金后28天内无息支付施工分包商。
5、	其他
（1）	本合同规定所有的支付币种为人民币。
（2）	施工分包商必须缴清当月的安全、质量、进度等罚款后，总承包商支付当月进度款。
（3）	施工分包商按照税务部门的要求向总承包商开具增值税税票。">
                                </f:HtmlEditor>
                            </Items>
                        </f:FormRow>
                    </Rows>

                </f:Form>


            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:HiddenField ID="hdAttachUrlItemId" runat="server"></f:HiddenField>
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
        </f:Panel>

    </form>
</body>
</html>

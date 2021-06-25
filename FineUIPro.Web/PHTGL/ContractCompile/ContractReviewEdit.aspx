<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractReviewEdit.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.ContractReviewEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑合同基本信息</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="false" Title="基本信息"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel1" Title="会签评审" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <Items>
                                <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="false" Title="基本信息"
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Rows>
                                        <%--  <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="DropBidCode" runat="server" EmptyText="中标编号" LabelAlign="Right" AutoPostBack="true" LabelWidth="120px"></f:DropDownList>
                                            </Items>
                                        </f:FormRow>--%>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownBox runat="server" ID="drpProjectId" EmptyText="总承包合同编号" MatchFieldWidth="false"
                                                    AutoPostBack="true" OnTextChanged="DropDownBox1_TextChanged" EnableMultiSelect="false">
                                                    <PopPanel>
                                                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" DataIDField="ContractId" DataTextField="EPCCode"
                                                            DataKeyNames="ContractId" Hidden="true" Width="550px" Height="300px" EnableMultiSelect="false">
                                                            <Columns>
                                                                <f:RenderField ColumnID="EPCCode" DataField="EPCCode" Width="120px" FieldType="String" HeaderText="总承包合同编号" TextAlign="Center"
                                                                    HeaderTextAlign="Center">
                                                                </f:RenderField>
                                                                <f:RenderField ColumnID="ContractName" DataField="ContractName" Width="180px" FieldType="String" HeaderText="合同名称" TextAlign="Center"
                                                                    HeaderTextAlign="Center">
                                                                </f:RenderField>
                                                                <f:RenderField ColumnID="ContractNum" DataField="ContractNum" Width="180px" FieldType="String" HeaderText="合同编号" TextAlign="Center"
                                                                    HeaderTextAlign="Center">
                                                                </f:RenderField>
                                                                <f:RenderField ColumnID="ProjectShortName" DataField="ProjectShortName" Width="180px" FieldType="String" HeaderText="项目名称" TextAlign="Center"
                                                                    HeaderTextAlign="Center">
                                                                </f:RenderField>
                                                                <f:RenderField ColumnID="Parties" DataField="Parties" Width="120px" FieldType="String" HeaderText="签约方" TextAlign="Center"
                                                                    HeaderTextAlign="Center">
                                                                </f:RenderField>
                                                                <f:RenderField ColumnID="Currency" DataField="Currency" Width="100px" FieldType="String" HeaderText="币种" TextAlign="Center"
                                                                    HeaderTextAlign="Center">
                                                                </f:RenderField>
                                                                <f:RenderField ColumnID="ContractAmount" DataField="ContractAmount" Width="120px" FieldType="String" HeaderText="合同金额" TextAlign="Center"
                                                                    HeaderTextAlign="Center">
                                                                </f:RenderField>
                                                                <f:RenderField ColumnID="DepartName" DataField="DepartName" Width="120px" FieldType="String" HeaderText="主办部门" TextAlign="Center"
                                                                    HeaderTextAlign="Center">
                                                                </f:RenderField>
                                                                <f:RenderField ColumnID="AgentName" DataField="AgentName" Width="120px" FieldType="String" HeaderText="经办人" TextAlign="Center"
                                                                    HeaderTextAlign="Center">
                                                                </f:RenderField>
                                                                <f:RenderField ColumnID="ContractType" DataField="ContractType" Width="150px" FieldType="String" HeaderText="合同类型" TextAlign="Center"
                                                                    HeaderTextAlign="Center">
                                                                </f:RenderField>
                                                                <f:RenderField ColumnID="Remarks" DataField="Remarks" Width="220px" FieldType="String" HeaderText="合同摘要" TextAlign="Center"
                                                                    HeaderTextAlign="Center">
                                                                </f:RenderField>
                                                            </Columns>

                                                        </f:Grid>
                                                    </PopPanel>
                                                </f:DropDownBox>
                                                <f:TextBox ID="txtContractNum" runat="server" Label="文件编号" LabelAlign="Right" Hidden="true" Readonly="true" LabelWidth="140px"></f:TextBox>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="DropConstructionManager" runat="server" Label="施工经理" Required="true" ShowRedStar="true" AutoSelectFirstItem="false" LabelAlign="Right" AutoPostBack="true" EnableEdit="true" MaxLength="200" LabelWidth="120px"></f:DropDownList>
                                                <f:DropDownList ID="DropPurchasingManager" runat="server" Label="采购经理" Required="true" ShowRedStar="true" AutoSelectFirstItem="false" LabelAlign="Right" AutoPostBack="true" EnableEdit="true" MaxLength="30" LabelWidth="140px"></f:DropDownList>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="DropHSSEManager" runat="server" Label="HSE经理" Required="true" ShowRedStar="true" AutoSelectFirstItem="false" LabelAlign="Right" AutoPostBack="true" EnableEdit="true" MaxLength="200" LabelWidth="120px"></f:DropDownList>
                                                <f:DropDownList ID="DropControlManager" runat="server" Label="控制经理" Required="true" ShowRedStar="true" AutoSelectFirstItem="false" LabelAlign="Right" AutoPostBack="true" EnableEdit="true" MaxLength="30" LabelWidth="140px"></f:DropDownList>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="DropQAManager" runat="server" Label="质量经理" Required="true" ShowRedStar="true" AutoSelectFirstItem="false" LabelAlign="Right" AutoPostBack="true" EnableEdit="true" MaxLength="200" LabelWidth="120px"></f:DropDownList>
                                                <f:DropDownList ID="DropFinancialManager" runat="server" Label="财务经理" Required="true" ShowRedStar="true" AutoSelectFirstItem="false" LabelAlign="Right" AutoPostBack="true" EnableEdit="true" MaxLength="30" LabelWidth="140px"></f:DropDownList>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="DropProjectManager" runat="server" Label="项目经理" Required="true" ShowRedStar="true" AutoSelectFirstItem="false" AutoPostBack="true" EnableEdit="true" LabelAlign="Right" MaxLength="200" LabelWidth="120px"></f:DropDownList>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="dropCountersign_Construction" runat="server" Label="施工管理部" Required="true" AutoSelectFirstItem="false" ShowRedStar="true" AutoPostBack="true" EnableEdit="true" LabelAlign="Right" LabelWidth="140px"></f:DropDownList>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="DropCountersign_Law" runat="server" Label="法律合规部1" Required="true" ShowRedStar="true" AutoSelectFirstItem="false" AutoPostBack="true" EnableEdit="true" LabelAlign="Right" LabelWidth="140px"></f:DropDownList>
                                                <f:DropDownList ID="DropCountersign_Law2" runat="server" Label="法律合规部2" Required="true" ShowRedStar="true" AutoSelectFirstItem="false" AutoPostBack="true" EnableEdit="true" LabelAlign="Right" LabelWidth="140px"></f:DropDownList>
                                            </Items>
                                        </f:FormRow>


                                    </Rows>

                                </f:Form>
                            </Items>

                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel2" Title="签订评审" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <Items>
                                <f:Form ID="Form3" ShowBorder="false" ShowHeader="false" AutoScroll="false" Title=""
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Rows>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="dropApproval_Construction" runat="server" Label="施工管理部" Required="true" ShowRedStar="true" AutoSelectFirstItem="false" EnableEdit="true" LabelAlign="Right" AutoPostBack="true" LabelWidth="120px"></f:DropDownList>
                                                <f:DropDownList ID="DropGeneralAccountant" runat="server" Label="总会计师" Required="true" Hidden="true" ShowRedStar="true" AutoSelectFirstItem="false" AutoPostBack="true" EnableEdit="true" LabelAlign="Right" LabelWidth="140px"></f:DropDownList>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="dropApproval_Law" runat="server" Label="法务合规部" Required="true" ShowRedStar="true" AutoSelectFirstItem="false" AutoPostBack="true" EnableEdit="true" LabelAlign="Right" LabelWidth="120px"></f:DropDownList>
                                                <f:DropDownList ID="DropGeneralManager" runat="server" Label="总经理" Required="true" ShowRedStar="true" AutoSelectFirstItem="false" Hidden="true" AutoPostBack="true" EnableEdit="true" LabelAlign="Right" MaxLength="30" LabelWidth="140px"></f:DropDownList>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="DropDeputyGeneralManager" runat="server" Label="分管副总经理" Required="true" ShowRedStar="true" AutoSelectFirstItem="false" AutoPostBack="true" EnableEdit="true" LabelAlign="Right" MaxLength="200" LabelWidth="120px"></f:DropDownList>
                                                <f:DropDownList ID="DropChairman" runat="server" Label="董事长" Required="true" ShowRedStar="true" AutoSelectFirstItem="false" Hidden="true" AutoPostBack="true" EnableEdit="true" LabelAlign="Right" MaxLength="30" LabelWidth="140px"></f:DropDownList>
                                            </Items>
                                        </f:FormRow>

                                    </Rows>

                                </f:Form>
                            </Items>

                        </f:ContentPanel>
                    </Items>
                </f:FormRow>


            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSubmit" Icon="SystemSave" runat="server" ToolTip="提交" Text="提交" ValidateForms="SimpleForm1" Size="Medium"
                            OnClick="btnSubmit_Click">
                        </f:Button>

                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" Text="保存" ValidateForms="SimpleForm1" Size="Medium"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose" Size="Medium">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
</body>
</html>

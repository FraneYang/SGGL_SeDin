<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActionPlanFormationEdit.aspx.cs" Inherits="FineUIPro.Web.PHTGL.BiddingManagement.ActionPlanFormationEdit" %>

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
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="施工招标计划" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label1" runat="server" Text="施工招标计划" CssClass="widthBlod"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="drpProjectId" runat="server" Label="总承包合同编号" LabelAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="drpProjectId_SelectedIndexChanged" LabelWidth="120px"></f:DropDownList>
                        <f:TextBox ID="txtProjectName" runat="server" Label="项目名称" LabelAlign="Right" Readonly="true" LabelWidth="140px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label7" runat="server" Text="一、	招标实施计划总附表"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Grid ID="Grid1" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" Title="" EnableCollapse="false"
                            runat="server" DataKeyNames="ActionPlanItemID" AllowCellEditing="true" ClicksToEdit="1" ForceFit="true"
                            EnableColumnLines="true" DataIDField="ActionPlanItemID" Height="380px">
                            <%--     <Toolbars>
                                <f:Toolbar ID="Toolbar2" runat="server" Position="Top" ToolbarAlign="Left">
                                    <Items>
                                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                        <f:Button ID="btnNew" ToolTip="增加" Icon="Add" EnablePostBack="false" runat="server">
                                        </f:Button>
                                        <f:Button ID="btnDelete" ToolTip="删除" Icon="Delete" EnablePostBack="false" runat="server">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>--%>
                            <Columns>
                                <f:RowNumberField HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center" Width="60px"></f:RowNumberField>
                                <f:RenderField ColumnID="PlanningContent" DataField="PlanningContent" FieldType="String"
                                    HeaderText="策划内容" HeaderTextAlign="Center">
                                    <%-- <Editor>
                                        <f:TextBox ID="txtPlanningContent" runat="server"></f:TextBox>
                                    </Editor>--%>
                                </f:RenderField>
                                <f:RenderField ColumnID="ActionPlan" DataField="ActionPlan" FieldType="String"
                                    HeaderText="实施计划" HeaderTextAlign="Center">
                                    <Editor>
                                        <f:TextBox ID="txtActionPlan" runat="server"></f:TextBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField ColumnID="Remarks" DataField="Remarks" FieldType="String"
                                    HeaderText="备注" HeaderTextAlign="Center">
                                    <Editor>
                                        <f:TextBox ID="txtRemarks" runat="server">
                                        </f:TextBox>
                                    </Editor>
                                </f:RenderField>
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label2" runat="server" CssClass="widthBlod" Text="二、	项目概况"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtProject" runat="server" AutoGrowHeight="true" AutoGrowHeightMin="50" Label="1、	建设项目"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtUnit" runat="server" AutoGrowHeight="true" AutoGrowHeightMin="50" Label="2、	建设单位"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtConstructionSite" runat="server" AutoGrowHeight="true" AutoGrowHeightMin="50" Label="3、	建设地点"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label3" runat="server" CssClass="widthBlod" Text="三、	招标工程概况"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtBiddingProjectScope" runat="server" AutoGrowHeight="true" AutoGrowHeightMin="50" Label="1、	招标工程范围"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtBiddingProjectContent" runat="server" AutoGrowHeight="true" AutoGrowHeightMin="50" Label="2、	招标工程施工内容"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtTimeRequirements" runat="server" AutoGrowHeight="true" AutoGrowHeightMin="50" Label="3、	工期要求"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtQualityRequirement" runat="server" AutoGrowHeight="true" AutoGrowHeightMin="50" Label="4、	质量要求"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtHSERequirement" runat="server" AutoGrowHeight="true" AutoGrowHeightMin="50" Label="5、	HSE施工要求"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtTechnicalRequirement" runat="server" AutoGrowHeight="true" AutoGrowHeightMin="50" Label="6、	技术要求"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtCurrentRequirement" runat="server" AutoGrowHeight="true" AutoGrowHeightMin="50" Label="7、	招标工程当前现实条件" Text="（设计方面、采购方面、项目现场方面等）"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label4" runat="server" CssClass="widthBlod" Text="四、	分包方式选择、标段划分"></f:Label>
                    </Items>
                </f:FormRow>

                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtSub_Selection" runat="server" AutoGrowHeight="true" AutoGrowHeightMin="50" Label="1、	分包方式评估与选择" Text="（针对招标工程特点和概况，结合公司在施工分包上的经验和积累）"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtBid_Selection" runat="server" AutoGrowHeight="true" AutoGrowHeightMin="50" Label="2、	标段划分" Text="（针对招标工程特点和概况，结合公司在施工分包上的经验和积累）"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label5" runat="server" CssClass="widthBlod" Text="五、	承包方式和计价方式的选择"></f:Label>
                    </Items>
                </f:FormRow>

                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtContractingMode_Select" runat="server" AutoGrowHeight="true" AutoGrowHeightMin="50" Label="1、	承包方式评估与选择" Text="（针对招标工程特点和概况，结合公司在施工分包上的经验和积累）"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtPriceMode_Select" runat="server" AutoGrowHeight="true" AutoGrowHeightMin="50" Label="2、	计价方式评估与选择" Text="（针对工程分包方式，结合工程施工内容和当前显示条件，综合考虑公司在施工分包商的经验）"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label6" runat="server" CssClass="widthBlod" Text="六、	设备材料划分规划"></f:Label>
                    </Items>
                </f:FormRow>

                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtMaterialsDifferentiate" runat="server" AutoGrowHeight="true" AutoGrowHeightMin="50" Label="1、	设备材料划分原则" Text="（针对工程特点，结合分包方式、承包方式和计价方式进行综合策划）"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtImportExplain" runat="server" AutoGrowHeight="true" AutoGrowHeightMin="50" Label="2、	关键说明" Text=""></f:TextArea>
                    </Items>
                </f:FormRow>

                <f:FormRow>
                    <Items>
                        <f:Label ID="Label10" runat="server" CssClass="widthBlod" Text="七、	短名单资质标准确定"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtShortNameList" runat="server" AutoGrowHeight="true" AutoGrowHeightMin="50" Label="" Text="（针对标段划分，结合工程特点，特别是重难点）"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label8" runat="server" CssClass="widthBlod" Text="八、	针对性的评标策略"></f:Label>
                    </Items>
                </f:FormRow>

                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtEvaluationMethods" runat="server" AutoGrowHeight="true" AutoGrowHeightMin="50" Label="1、	评标办法评估与选择" Text=""></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtEvaluationPlan" runat="server" AutoGrowHeight="true" AutoGrowHeightMin="50" Label="2、	评标委员会结构规划" Text=""></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label9" runat="server" Text="九、	招标策划"></f:Label>
                    </Items>
                </f:FormRow>

                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtBiddingMethods_Select" runat="server" AutoGrowHeight="true" AutoGrowHeightMin="50" Label="1、	招标方式评估与选择" Text=""></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtSchedulePlan" runat="server" AutoGrowHeight="true" AutoGrowHeightMin="50" Label="2、	招标进度规划" Text=""></f:TextArea>
                    </Items>
                </f:FormRow>

            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" Text="保存" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" Text="关闭" runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
    <script>
        //function onGridDataLoad(event) {
        //    this.mergeColumns(['txtPlanningContent']);
        //}
    </script>
</body>
</html>

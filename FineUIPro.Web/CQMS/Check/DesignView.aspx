<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DesignView.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.DesignView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel2" Title="设计变更" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                              <f:TextBox ID="txtProjectName" runat="server" Readonly="true" Label="项目名称" LabelAlign="Right" LabelWidth="170px">
                                            </f:TextBox>
                                            <f:Label runat="server"></f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:DropDownList ID="drpMainItem" runat="server" Label="主项" LabelAlign="Right" EnableEdit="true" LabelWidth="170px" Readonly="true">
                                            </f:DropDownList>
                                            <f:DropDownList ID="drpCNProfessional" runat="server" Label="专业" LabelAlign="Right" EnableEdit="true" LabelWidth="170px" Readonly="true">
                                            </f:DropDownList>

                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel5" Title="变更信息 " runat="server" ShowHeader="true" EnableCollapse="true"
                            BodyPadding="0px">

                            <f:Form ID="Form5" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtDesignCode" runat="server" Label="变更编号"  LabelAlign="Right" LabelWidth="170px" Readonly="true">
                                            </f:TextBox>
                                            <f:DropDownList ID="drpDesignType" runat="server" Label="变更类型" EnableEdit="true" LabelAlign="right" LabelWidth="170px" Readonly="true">
                                            </f:DropDownList>

                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:DatePicker ID="txtDesignDate"  runat="server" Label="变更日期" LabelAlign="Right" Readonly="true"
                                                EnableEdit="true" LabelWidth="170px">
                                            </f:DatePicker>
                                            <f:Panel ID="Panel2" Width="300px" ShowHeader="false" ShowBorder="false" Layout="Column" CssClass="" runat="server">
                                                <Items>
                                                    <f:Label ID="Label1" runat="server" Label="上传附件"
                                                        LabelWidth="170px">
                                                    </f:Label>
                                                    <f:Button ID="btnAttach" Icon="TableCell" EnablePostBack="true" Text="附件" runat="server" OnClick="btnAttach_Click">
                                                    </f:Button>
                                                </Items>
                                            </f:Panel>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow runat="server">
                                        <Items>
                                            <f:TextArea ID="txtDesignContents" runat="server" Label="变更内容" MaxLength="3000" LabelWidth="170px" Readonly="true">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>

                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel1" Title="变更分析 " runat="server" ShowHeader="true" EnableCollapse="true"
                            BodyPadding="0px">
                            <f:Form ID="Form3" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>
                                    <f:FormRow runat="server">
                                        <Items>
                                            <f:TextBox runat="server" Label="实施单位" ID="txtCarryUnit"  Readonly="true" LabelWidth="170px">
                                            </f:TextBox>
                                            <f:TextBox runat="server" Label="增补材料采购方"  ID="txtBuyMaterialUnit" LabelWidth="170px" Readonly="true">
                                            </f:TextBox>

                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow runat="server">
                                        <Items>
                                            <f:TextBox runat="server" ID="rblIsNeedMaterial" Label="是否需要增补材料" AutoPostBack="true" LabelWidth="170px" Readonly="true">
                                            </f:TextBox>
                                            <f:TextBox runat="server" ID="rblIsNoChange" Label="是否已按原图纸施工" AutoPostBack="true" LabelWidth="170px" Readonly="true">
                                            </f:TextBox>
                                        </Items>

                                    </f:FormRow>
                                    <f:FormRow runat="server">
                                        <Items>
                                            <f:DatePicker ID="txtMaterialPlanReachDate" runat="server" Label="材料预计到齐时间" LabelAlign="Right" AutoPostBack="true" Readonly="true"  EnableEdit="true" LabelWidth="170px" >
                                            </f:DatePicker>
                                            <f:NumberBox ID="txtPlanDay" Label="预计施工周期(天)" runat="server"
                                                LabelWidth="170px" DecimalPrecision="1" NoNegative="true" AutoPostBack="true" Readonly="true">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow runat="server">
                                        <Items>
                                            <f:DatePicker ID="txtPlanCompleteDate" runat="server" Label="计划完成时间"  LabelAlign="Right" Readonly="true"  EnableEdit="true" LabelWidth="170px">
                                            </f:DatePicker>
                                            <f:Label runat="server"></f:Label>
                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>
                        </f:ContentPanel>
                    </Items>

                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel3" Title="变更实施 " runat="server" ShowHeader="true" EnableCollapse="true"
                            BodyPadding="0px">

                            <f:Form ID="Form4" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>
                                    <f:FormRow runat="server">
                                        <Items>
                                            <f:DatePicker ID="txtMaterialRealReachDate" runat="server" Label="增补材料到齐时间"  LabelAlign="Right" Readonly="true"
                                                EnableEdit="true" LabelWidth="170px">
                                            </f:DatePicker>
                                            <f:DatePicker ID="txtRealCompleteDate" runat="server" Label="施工完成时间"   LabelAlign="Right" Readonly="true"  EnableEdit="true" LabelWidth="170px">
                                            </f:DatePicker>
                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>

                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow ID="next">
                    <Items>
                        <f:ContentPanel ID="ContentPanel4" Title="设计变更审批流程设置 " runat="server" ShowHeader="true" EnableCollapse="true"
                            BodyPadding="0px">

                            <f:Form ID="Form6" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>
                                    <f:FormRow runat="server" ID="rblIsAgree">
                                        <Items>
                                            <f:Panel ID="Panel1" Width="300px" ShowHeader="false" ShowBorder="false" Layout="Column" CssClass="" runat="server">
                                                <Items>
                                                    <f:RadioButtonList runat="server" ID="RadioButtonList1" Label="是否同意"  AutoPostBack="true" LabelWidth="170px" Readonly="true">
                                                        <f:RadioItem Text="同意" Value="true" Selected="true" />
                                                        <f:RadioItem Text="不同意" Value="false" />
                                                    </f:RadioButtonList>
                                                </Items>
                                            </f:Panel>

                                            <f:Label runat="server" CssStyle="display:none" LabelWidth="170px"></f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:DropDownList ID="drpHandleType" LabelWidth="170px" AutoPostBack="true" runat="server" Label="办理步骤" LabelAlign="Right" EnableEdit="true" Readonly="true">
                                            </f:DropDownList>
                                            <f:DropDownList ID="drpHandleMan" runat="server" Label="办理人员"  LabelAlign="Right" EnableEdit="true" LabelWidth="170px" Readonly="true">
                                            </f:DropDownList>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow ID="HideOptions" runat="server">
                                        <Items>
                                            <f:TextArea ID="txtOpinions" runat="server" Label="我的意见" MaxLength="3000" LabelWidth="170px" Readonly="true">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow>

                                </Rows>
                            </f:Form>

                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow ID="plApprove2">
                    <Items>
                        <f:ContentPanel Title="设计变更审批列表" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Grid ID="gvApprove" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" runat="server" EnableCollapse="false" DataIDField="DesignApproveId" SortField="ApproveDate" SortDirection="ASC"
                                DataKeyNames="DesignApproveId" EnableColumnLines="true" ForceFit="true">
                                <Columns>
                                    <f:TemplateField ColumnID="tfPageIndex" Width="55px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                                        EnableLock="true" Locked="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPageIndex" runat="server" Text='<%# gvApprove.PageIndex * gvApprove.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </f:TemplateField>
                                   <f:TemplateField ColumnID="State" Width="250px" HeaderText="办理类型" HeaderTextAlign="Center" TextAlign="Center"
                                        EnableLock="true" Locked="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lbtype" runat="server" Text='<%# ConvertState(Eval("ApproveType")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </f:TemplateField>
                                    <f:BoundField Width="180px" DataField="UserName" HeaderTextAlign="Center" HeaderText="办理人员" TextAlign="Center" />
                                    <f:BoundField Width="200px" DataField="ApproveDate" HeaderTextAlign="Center" TextAlign="Center" DataFormatString="{0:yyyy-MM-dd}" HeaderText="办理时间" />
                                    <f:BoundField Width="180px" DataField="ApproveIdea" HeaderTextAlign="Center" TextAlign="Center" HeaderText="办理意见" />
                                </Columns>
                            </f:Grid>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
            </Rows>

        </f:Form>
        <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
    </form>
</body>
</html>

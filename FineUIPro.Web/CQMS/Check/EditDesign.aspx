<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditDesign.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.EditDesign" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .labcenter {
            text-align: center;
        }

        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }

        .f-grid-row.red {
            background-color: #FF7575;
            background-image: none;
        }

        .fontred {
            color: #FF7575;
            background-image: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:HiddenField ID="hdCheckDesignCode" runat="server"></f:HiddenField>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSave" OnClick="btnSave_Click" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1">
                        </f:Button>
                        <f:Button ID="btnSubmit" OnClick="btnSubmit_Click" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1">
                        </f:Button>
                        <f:HiddenField ID="hdId" runat="server">
                        </f:HiddenField>
                        <f:HiddenField ID="hdAttachUrl" runat="server">
                        </f:HiddenField>
                    </Items>
                </f:Toolbar>
            </Toolbars>
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
                                            <f:DropDownList ID="drpMainItem" runat="server" Label="主项"  EmptyText="--请选择--"  AutoSelectFirstItem="false" LabelAlign="Right" EnableEdit="true" LabelWidth="170px" ShowRedStar="true" Required="true">
                                            </f:DropDownList>
                                            <f:DropDownList ID="drpCNProfessional" runat="server" Label="专业"  EmptyText="--请选择--"  AutoSelectFirstItem="false" LabelAlign="Right" EnableEdit="true" LabelWidth="170px" ShowRedStar="true" Required="true">
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
                                            <f:TextBox ID="txtDesignCode" ShowRedStar="true" runat="server" Label="变更编号" Required="true" LabelAlign="Right" LabelWidth="170px">
                                            </f:TextBox>
                                            <f:DropDownList ID="drpDesignType" runat="server" Label="变更类型" EnableEdit="true"  EmptyText="--请选择--"  AutoSelectFirstItem="false" LabelAlign="right" LabelWidth="170px" ShowRedStar="true" Required="true">
                                            </f:DropDownList>

                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:DatePicker ID="txtDesignDate" ShowRedStar="true" runat="server" Label="变更日期" Required="true" LabelAlign="Right"
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
                                            <f:TextArea ID="txtDesignContents" runat="server" Label="变更内容" MaxLength="3000" LabelWidth="170px">
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
                                            <f:DropDownBox runat="server" Label="实施单位" ShowRedStar="true" 
                                                Required="true" ID="txtCarryUnit" EmptyText="--请选择--" EnableMultiSelect="true" MatchFieldWidth="true" LabelWidth="170px">
                                                <PopPanel>
                                                    <f:Grid ID="gvCarryUnit" DataIDField="UnitId"
                                                        EnableMultiSelect="true" KeepCurrentSelection="true" Height="300px" Hidden="true" SortField="UnitId" DataTextField="UnitName"
                                                        ShowBorder="true" ShowHeader="false"  ForceFit="true"
                                                        runat="server" EnableCheckBoxSelect="true">
                                                        <Columns>
                                                            <f:BoundField Width="100px" DataField="UnitId" SortField="UnitId" DataFormatString="{0}" Hidden="true" />
                                                            <f:BoundField Width="100px" DataField="UnitName" SortField="UnitName" DataFormatString="{0}"
                                                                HeaderText="工程名称" />
                                                        </Columns>
                                                    </f:Grid>
                                                </PopPanel>
                                            </f:DropDownBox>
                                            <f:DropDownBox runat="server" Label="增补材料采购方" ShowRedStar="true"
                                                Required="true" ID="txtBuyMaterialUnit" EmptyText="--请选择--" EnableMultiSelect="true" MatchFieldWidth="false" LabelWidth="170px">
                                                <PopPanel>
                                                    <f:Grid ID="gvBuyMaterialUnit" DataIDField="UnitId" DataTextField="UnitName" DataKeyNames="UnitId"
                                                        EnableMultiSelect="true" KeepCurrentSelection="true" Height="300px" Hidden="true"
                                                        ShowBorder="true" ShowHeader="false" ForceFit="true"
                                                        runat="server" EnableCheckBoxSelect="true">
                                                        <Columns>
                                                            <f:BoundField Width="100px" DataField="UnitId" SortField="UnitId" DataFormatString="{0}" Hidden="true" />
                                                            <f:BoundField Width="100px" DataField="UnitName" SortField="UnitName" DataFormatString="{0}"
                                                                 HeaderText="工程名称" />
                                                        </Columns>
                                                    </f:Grid>
                                                </PopPanel>
                                            </f:DropDownBox>

                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow runat="server">
                                        <Items>
                                            <f:RadioButtonList runat="server" ID="rblIsNeedMaterial" Label="是否需要增补材料" ShowRedStar="true" AutoPostBack="true" LabelWidth="170px" OnSelectedIndexChanged="rblIsNeedMaterial_SelectedIndexChanged">
                                                <f:RadioItem Text="是" Value="true" Selected="true" />
                                                <f:RadioItem Text="否" Value="false" />
                                            </f:RadioButtonList>
                                            <f:RadioButtonList runat="server" ID="rblIsNoChange" Label="是否已按原图纸施工" ShowRedStar="true" AutoPostBack="true" LabelWidth="170px">
                                                <f:RadioItem Text="是" Value="true" Selected="true" />
                                                <f:RadioItem Text="否" Value="false" />
                                            </f:RadioButtonList>
                                        </Items>

                                    </f:FormRow>
                                    <f:FormRow runat="server">
                                        <Items>
                                            <f:DatePicker ID="txtMaterialPlanReachDate" ShowRedStar="true" runat="server" Label="材料预计到齐时间" Required="true" LabelAlign="Right" AutoPostBack="true"
                                                EnableEdit="true" LabelWidth="170px" OnTextChanged="txtMaterialPlanReachDate_TextChanged">
                                            </f:DatePicker>
                                            <f:NumberBox ID="txtPlanDay" Label="预计施工周期(天)" runat="server"
                                                LabelWidth="170px" DecimalPrecision="1" NoNegative="true" ShowRedStar="true" Required="true" AutoPostBack="true" OnTextChanged="txtMaterialPlanReachDate_TextChanged">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow runat="server">
                                        <Items>
                                            <f:DatePicker ID="txtPlanCompleteDate" ShowRedStar="true" runat="server" Label="计划完成时间" Required="true" LabelAlign="Right"
                                                EnableEdit="true" LabelWidth="170px">
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
                                            <f:DatePicker ID="txtMaterialRealReachDate" ShowRedStar="true" runat="server" Label="增补材料到齐时间" Required="true" LabelAlign="Right"
                                                EnableEdit="true" LabelWidth="170px">
                                            </f:DatePicker>
                                            <f:DatePicker ID="txtRealCompleteDate" ShowRedStar="true" runat="server" Label="施工完成时间" Required="true" LabelAlign="Right"
                                                EnableEdit="true" LabelWidth="170px">
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
                                                    <f:RadioButtonList runat="server" ID="RadioButtonList1" Label="是否同意" ShowRedStar="true" AutoPostBack="true" LabelWidth="170px" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
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
                                            <f:DropDownList ID="drpHandleType" LabelWidth="170px" AutoPostBack="true" runat="server" Label="办理步骤" LabelAlign="Right" EnableEdit="true" Readonly="true" OnSelectedIndexChanged="drpHandleType_SelectedIndexChanged">
                                            </f:DropDownList>
                                            <f:DropDownList ID="drpHandleMan" runat="server" Label="办理人员" Required="true" LabelAlign="Right" EnableEdit="true" LabelWidth="170px" ShowRedStar="true">
                                            </f:DropDownList>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow ID="HideOptions" runat="server">
                                        <Items>
                                            <f:TextArea ID="txtOpinions" runat="server" Label="我的意见" MaxLength="3000" LabelWidth="170px">
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
                            <f:Grid ID="gvApprove" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" runat="server" EnableCollapse="false" DataIDField="DesignApproveId" SortField="ApproveDate" SortDirection="DESC"
                                DataKeyNames="DesignApproveId" EnableColumnLines="true" ForceFit="true">
                                <Columns>
                                    <f:TemplateField ColumnID="tfPageIndex" Width="50px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
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
                                    <f:BoundField Width="180px" DataField="ApproveIdea" HeaderTextAlign="Center" TextAlign="Center"  HeaderText="办理意见" />
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

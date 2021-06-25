<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActionPlanReviewEdit.aspx.cs" Inherits="FineUIPro.Web.PHTGL.BiddingManagement.ActionPlanReviewEdit" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="false" Title="审批信息"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel1" Title=" " ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <Items>
                                <f:Form ID="Form3" ShowBorder="false" ShowHeader="false" AutoScroll="false" Title="实施计划审批创建"
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Rows>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownBox runat="server" ID="drpProjectId" EmptyText="合同编号" MatchFieldWidth="false"
                                                    AutoPostBack="true" OnTextChanged="DropDownBox1_TextChanged" EnableMultiSelect="false">
                                                    <PopPanel>
                                                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" DataIDField="ActionPlanID" DataTextField="ProjectCode"
                                                            DataKeyNames="ActionPlanID" Hidden="true" Width="550px" Height="300px" EnableMultiSelect="false">
                                                            <Columns>
                                                                <f:TemplateField ColumnID="tfPageIndex" Width="55px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                                                                    EnableLock="true" Locked="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </f:TemplateField>
                                                                <f:RenderField ColumnID="ActionPlanCode" DataField="ActionPlanCode" Width="120px" FieldType="String" HeaderText="实施计划编号" TextAlign="Center"
                                                                    HeaderTextAlign="Center">
                                                                </f:RenderField>
                                                                <f:RenderField ColumnID="Name" DataField="Name" Width="120px" FieldType="String" HeaderText="项目名称" TextAlign="Center"
                                                                    HeaderTextAlign="Center">
                                                                </f:RenderField>
                                                                <f:RenderField ColumnID="ProjectCode" DataField="ProjectCode" Width="120px" FieldType="String" HeaderText="项目编号" TextAlign="Center"
                                                                    HeaderTextAlign="Center">
                                                                </f:RenderField>
                                                                <f:RenderField ColumnID="CreatUser" DataField="Unit" Width="180px" FieldType="String" HeaderText="创建人" TextAlign="Center"
                                                                    HeaderTextAlign="Center">
                                                                </f:RenderField>
                                                                <f:RenderField Width="120px" ColumnID="CreateTime" DataField="CreateTime" FieldType="Date"
                                                                    Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="创建日期">
                                                                </f:RenderField>

                                                            </Columns>
                                                        </f:Grid>
                                                    </PopPanel>
                                                </f:DropDownBox>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="DropConstructionManager" runat="server" AutoPostBack="true" EnableEdit="true" Label="施工经理" LabelAlign="Right" LabelWidth="120px"></f:DropDownList>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="DropPreliminaryMan" runat="server" AutoPostBack="true" EnableEdit="true" Label="预审人员" LabelAlign="Right" LabelWidth="120px"></f:DropDownList>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="Approval_Construction" runat="server" EnableEdit="true" Label="施工管理部" LabelAlign="Right" LabelWidth="120px"></f:DropDownList>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="DropProjectManager" runat="server" AutoPostBack="true" EnableEdit="true" Label="项目经理" LabelAlign="Right" LabelWidth="120px"></f:DropDownList>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="DropDeputyGeneralManager" runat="server" AutoPostBack="true" EnableEdit="true" Label="分管副总经理" LabelAlign="Right" LabelWidth="120px"></f:DropDownList>
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
                <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存"  Text="保存" ValidateForms="SimpleForm1" Size="Medium"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnSubmit" Icon="SystemSave" runat="server" ToolTip="提交" Text="提交" ValidateForms="SimpleForm1" Size="Medium"
                            OnClick="btnSubmit_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" Text="关闭"  runat="server" Icon="SystemClose" Size="Medium">
                        </f:Button>
    
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>

        <f:Window ID="Window1" Title="流程步骤设置" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true"
            Width="640px" Height="450px">
        </f:Window>
    </form>
</body>
</html>

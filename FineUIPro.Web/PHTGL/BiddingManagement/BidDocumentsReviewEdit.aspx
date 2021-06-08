<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BidDocumentsReviewEdit.aspx.cs" Inherits="FineUIPro.Web.PHTGL.BiddingManagement.BidDocumentsReviewEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑招标文件审批信息</title>
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
                                <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="false" Title="基本信息"
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Rows>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownBox runat="server" ID="drpProjectId" EmptyText="实施计划编号" MatchFieldWidth="false" Label="实施计划编号" LabelWidth="140px"
                                                    AutoPostBack="true" OnTextChanged="DropDownBox1_TextChanged" EnableMultiSelect="false">
                                                    <PopPanel>
                                                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" DataIDField="ActionPlanReviewId" DataTextField="ActionPlanCode"
                                                            DataKeyNames="ActionPlanReviewId" Hidden="true" Width="550px" Height="300px" EnableMultiSelect="false">
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
                                                <f:TextBox ID="txtProjectCode" runat="server" Label="项目编号" LabelAlign="Right" Readonly="true" LabelWidth="140px"></f:TextBox>

                                                <%-- <f:DropDownList ID="DropProjectId" runat="server" Label="项目编号" LabelAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="drpProjectId_SelectedIndexChanged" LabelWidth="120px"></f:DropDownList>--%>
                                                <f:TextBox ID="txtProjectName" runat="server" Label="项目名称" LabelAlign="Right" Readonly="true" LabelWidth="140px"></f:TextBox>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>

                                                <f:DropDownList ID="txtBidType" runat="server" Label="招标方式" LabelAlign="Right" LabelWidth="140px" Required="true" ShowRedStar="true"></f:DropDownList>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:TextBox ID="txtBidDocumentsCode" runat="server" Label="招标文件编号" LabelAlign="Right" MaxLength="30" LabelWidth="140px" Required="true" ShowRedStar="true"></f:TextBox>

                                                <f:TextBox ID="txtBidDocumentsName" runat="server" Label="招标文件名称" LabelAlign="Right" MaxLength="200" LabelWidth="140px" Required="true" ShowRedStar="true"></f:TextBox>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:TextBox ID="txtBidContent" runat="server" Label="招标内容" LabelAlign="Right" MaxLength="30" LabelWidth="140px" Required="true" ShowRedStar="true"></f:TextBox>

                                                <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                                                    OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                                                </f:Button>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:DatePicker runat="server" Required="true" DateFormatString="yyyy-MM-dd" Label="计划发标时间" EmptyText="请选择开始日期" LabelWidth="140px"
                                                    ID="Bidding_SendTime" ShowRedStar="true">
                                                </f:DatePicker>
                                                <f:DatePicker runat="server" Required="true" DateFormatString="yyyy-MM-dd" Label="计划开标时间" EmptyText="请选择开始日期" LabelWidth="140px"
                                                    ID="Bidding_StartTime" ShowRedStar="true">
                                                </f:DatePicker>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="DropConstructionManager" runat="server" AutoPostBack="true" EnableEdit="true" Label="施工经理" LabelAlign="Right" LabelWidth="140px"></f:DropDownList>
                                             </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="DropControlManager" runat="server" AutoPostBack="true" EnableEdit="true" Label="控制经理" LabelAlign="Right" LabelWidth="140px"></f:DropDownList>
                                              </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="DropProjectManager" runat="server" AutoPostBack="true" EnableEdit="true" Label="项目经理" LabelAlign="Right" LabelWidth="140px"></f:DropDownList>
                                              </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="DropPreliminaryMan" runat="server" AutoPostBack="true" EnableEdit="true" Label="预审人员" LabelAlign="Right" LabelWidth="140px"></f:DropDownList>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="Approval_Construction" runat="server" Label="施工管理部" LabelAlign="Right" LabelWidth="140px"></f:DropDownList>
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
                        <f:Label runat="server" ID="lbTemp">
                        </f:Label>
                        
                        <f:ToolbarFill ID="ToolbarFill1" runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" Text="保存" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" Text="提交" ValidateForms="SimpleForm1"
                            OnClick="btnSubmit_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" Text="关闭" runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
        <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
    </form>
</body>
</html>

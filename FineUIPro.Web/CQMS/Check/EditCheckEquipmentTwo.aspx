<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditCheckEquipmentTwo.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.EditCheckEquipmentTwo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:HiddenField ID="hdCheckEquipmentCode" runat="server"></f:HiddenField>
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
                        <f:ContentPanel ID="ContentPanel2" Title="检试验设备内容" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtProjectName" runat="server" Readonly="true" Label="项目名称" LabelAlign="Right" LabelWidth="130px">
                                            </f:TextBox>
                                            <f:DropDownList ID="drpUserUnitId" AutoPostBack="true" EmptyText="--请选择--" AutoSelectFirstItem="false" ShowRedStar="true" runat="server" Required="true" Label="使用单位" LabelAlign="Right" EnableEdit="true" LabelWidth="130px">
                                            </f:DropDownList>

                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtEquipmentName" runat="server" Label="设备器具名称"  LabelAlign="Right" LabelWidth="130px">
                                            </f:TextBox>
                                            <f:TextBox ID="txtFormat" runat="server" Label="规格型号" LabelAlign="Right" MaxLength="50" LabelWidth="130px">
                                            </f:TextBox>

                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtSetAccuracyGrade" ShowRedStar="true" runat="server" Label="规定精度等级" Required="true" LabelAlign="Right" MaxLength="50" LabelWidth="130px">
                                            </f:TextBox>
                                            <f:TextBox ID="txtRealAccuracyGrade" ShowRedStar="true" runat="server" Label="实际精度等级" Required="true" LabelAlign="Right" LabelWidth="130px"
                                                MaxLength="50">
                                            </f:TextBox>

                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtCheckCycle" Label="检定周期" runat="server" 
                                                LabelWidth="130px" >
                                            </f:TextBox>
                                            <f:DatePicker ID="txtCheckDay" runat="server" Label="检定日"  LabelAlign="Right" EnableEdit="true" LabelWidth="130px">
                                            </f:DatePicker>

                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="cbIsIdentification" runat="server" Label="是否有标识"
                                                LabelWidth="130px" >
                                            </f:TextBox>
                                            <f:TextBox ID="cbIsCheckCertificate" runat="server" Label="是否有检定证书"  LabelWidth="130px" >
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow runat="server">
                                        <Items>
                                            <f:Panel ID="Panel3" Width="300px" ShowHeader="false" ShowBorder="false" Layout="Column" CssClass="" runat="server">
                                                <Items>
                                                    <f:Label ID="CheckBox1" runat="server" Label="上传附件"
                                                        LabelWidth="130px">
                                                    </f:Label>
                                                    <f:Button ID="btnAttach" Icon="TableCell" EnablePostBack="true" Text="附件" runat="server" OnClick="btnAttach_Click">
                                                    </f:Button>
                                                </Items>
                                            </f:Panel>
                                            <f:DropDownList ID="drpIsdamage" AutoPostBack="true" EmptyText="--请选择--" AutoSelectFirstItem="false" ShowRedStar="true" runat="server" Required="true" Label="状态" LabelAlign="Right" EnableEdit="true" LabelWidth="130px" >
                                                <f:ListItem Text="正常" Value="正常" />
                                                <f:ListItem Text="损坏" Value="损坏" />
                                            </f:DropDownList>
                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow ID="next">
                    <Items>
                        <f:ContentPanel ID="ContentPanel5" Title="检试验设备审批流程设置 " runat="server" ShowHeader="true" EnableCollapse="true"
                            BodyPadding="0px">
                            <f:Form ID="Form5" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                            <f:DropDownList ID="drpHandleType" LabelWidth="130px" Readonly="true"
                                                AutoPostBack="true" runat="server" Label="办理步骤" LabelAlign="Right" EnableEdit="true">
                                            </f:DropDownList>
                                            <f:DropDownList ID="drpHandleMan" runat="server" Label="办理人员" Required="true" LabelAlign="Right" EnableEdit="true" LabelWidth="130px">
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
                        <f:ContentPanel Title="检试验设备审批列表" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Grid ID="gvApprove" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" runat="server" EnableCollapse="false" DataIDField="CheckEquipmentApproveId" SortField="ApproveDate"
                                DataKeyNames="CheckControlApproveId" EnableColumnLines="true" ForceFit="true">
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PauseNoticeEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Check.PauseNoticeEdit" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>工程暂停令</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" AjaxAspnetControls="divFile1" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            Layout="VBox" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
            LabelAlign="Right">
            <Rows>
                <f:FormRow ColumnWidths="45% 30% 25%">
                    <Items>
                        <f:DropDownList runat="server" EnableSimulateTree="True" Label="受检单位" ID="drpUnit"
                            AutoPostBack="true" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged" Required="true"
                            ShowRedStar="true" EnableEdit="true" Readonly="true">
                        </f:DropDownList>
                        <f:TextBox ID="txtProjectPlace" runat="server" Label="工程部位" Required="true" ShowRedStar="true"
                            MaxLength="200" Readonly="true">
                        </f:TextBox>
                        <f:TextBox ID="txtPauseNoticeCode" runat="server" Label="编号" Readonly="true" Required="true"
                            ShowRedStar="true" MaxLength="50" LabelWidth="80px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtWrongContent" runat="server" Label="因" Required="true" ShowRedStar="true"
                            MaxLength="150" Width="250px" LabelWidth="80px" Readonly="true">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DatePicker ID="txtPauseTime" ShowRedStar="true" LabelWidth="150px" DateFormatString="yyyy-MM-dd HH:mm" runat="server" Label="现要求你公司于" Required="true" LabelAlign="Right"
                            EnableEdit="true" ShowTime="true" ShowSecond="false" Readonly="true">
                        </f:DatePicker>
                        <f:Label runat="server" Text="日起停止施工"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Panel ID="Panel3" AutoScroll="true" runat="server" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:GroupPanel runat="server" Title="审核流程" BodyPadding="1px" ID="GroupPanel1"
                                    EnableCollapse="True" Collapsed="false" EnableCollapseEvent="true"
                                    EnableExpandEvent="true">
                                    <Items>
                                        <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                            <Rows>
                                                <f:FormRow ID="IsAgree" Hidden="true">
                                                    <Items>
                                                        <f:Panel ID="Panel1" ShowHeader="false" ShowBorder="false" Layout="Column" CssClass="" runat="server">
                                                            <Items>
                                                                <f:RadioButtonList runat="server" ID="rdbIsAgree" Label="是否同意" ShowRedStar="true" LabelWidth="200px" AutoPostBack="true" OnSelectedIndexChanged="rdbIsAgree_SelectedIndexChanged">
                                                                    <f:RadioItem Text="同意" Value="true" Selected="true" />
                                                                    <f:RadioItem Text="不同意" Value="false" />
                                                                </f:RadioButtonList>
                                                            </Items>
                                                        </f:Panel>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow ID="NoAgree" Hidden="true">
                                                    <Items>
                                                        <f:TextArea runat="server" ID="reason" Label="请输入原因" LabelWidth="200px"></f:TextArea>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow ID="BackMan" Hidden="true">
                                                    <Items>
                                                        <f:DropDownList runat="server" ID="drpHandleMan" Label="办理人员" LabelWidth="200px" Readonly="true"></f:DropDownList>
                                                    </Items>
                                                </f:FormRow>
                                            </Rows>
                                        </f:Form>
                                        <f:Form ID="HandleType" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                            <Rows>
                                                <f:FormRow runat="server" ID="next" Hidden="true">
                                                    <Items>
                                                        <f:DropDownList ID="drpSignPerson" runat="server" Label="总包施工经理" LabelWidth="200px"
                                                            LabelAlign="Right" EnableEdit="true" ShowRedStar="true">
                                                        </f:DropDownList>
                                                        <f:Label runat="server"></f:Label>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow Hidden="true" ID="next1">
                                                    <Items>
                                                        <f:Form runat="server" ShowBorder="false" ShowHeader="false">
                                                            <Rows>
                                                                <f:FormRow ID="step1_person">
                                                                    <Items>
                                                                        <f:DropDownList ID="drpApproveMan" runat="server" Label="总包项目经理" LabelAlign="Right" EnableEdit="true" ShowRedStar="true" LabelWidth="200px">
                                                                        </f:DropDownList>
                                                                        <f:Label runat="server">
                                                                        </f:Label>
                                                                    </Items>
                                                                </f:FormRow>
                                                                <f:FormRow>
                                                                    <Items>
                                                                        <f:Panel ID="Panel4" AutoScroll="true" runat="server" ShowBorder="false" ShowHeader="false">
                                                                            <Items>
                                                                                <f:GroupPanel runat="server" Title="抄送人" BodyPadding="1px" ID="GroupPanel2"
                                                                                    EnableCollapse="True" Collapsed="false" EnableCollapseEvent="true"
                                                                                    EnableExpandEvent="true">
                                                                                    <Items>
                                                                                        <f:Form ID="step1_person2" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                                                                            <Rows>
                                                                                                <f:FormRow>
                                                                                                    <Items>
                                                                                                        <f:DropDownList ID="drpProfessionalEngineer" runat="server" Label="专业工程师" LabelAlign="Right" EnableEdit="true" ShowRedStar="true" LabelWidth="160px">
                                                                                                        </f:DropDownList>
                                                                                                        <f:DropDownList ID="drpConstructionManager" runat="server" Label="施工经理" ShowRedStar="true" LabelAlign="Right" EnableEdit="true">
                                                                                                        </f:DropDownList>
                                                                                                        <f:DropDownList ID="drpUnitHeadMan" runat="server" Label="分包单位" ShowRedStar="true" LabelAlign="Right" EnableEdit="true">
                                                                                                        </f:DropDownList>

                                                                                                    </Items>
                                                                                                </f:FormRow>
                                                                                                <f:FormRow>
                                                                                                    <Items>
                                                                                                        <f:DropDownList ID="drpSupervisorMan" runat="server" Label="监理" ShowRedStar="true" LabelAlign="Right" EnableEdit="true" LabelWidth="160px">
                                                                                                        </f:DropDownList>
                                                                                                        <f:DropDownList ID="drpOwner" runat="server" Label="业主" ShowRedStar="true" LabelAlign="Right" EnableEdit="true">
                                                                                                        </f:DropDownList>
                                                                                                        <f:Label runat="server"></f:Label>
                                                                                                    </Items>
                                                                                                </f:FormRow>
                                                                                            </Rows>
                                                                                        </f:Form>
                                                                                    </Items>
                                                                                </f:GroupPanel>
                                                                            </Items>
                                                                        </f:Panel>
                                                                    </Items>

                                                                </f:FormRow>
                                                            </Rows>
                                                        </f:Form>

                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow ID="next2" Hidden="true">
                                                    <Items>
                                                        <f:Form ID="Form4" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                                            <Rows>
                                                                <f:FormRow>
                                                                    <Items>
                                                                        <f:DropDownList ID="drpDutyPerson" runat="server" Label="施工分包单位" LabelAlign="Right" EnableEdit="true" ShowRedStar="true" LabelWidth="200px">
                                                                        </f:DropDownList>
                                                                        <f:Label runat="server">
                                                                        </f:Label>
                                                                    </Items>
                                                                </f:FormRow>
                                                            </Rows>
                                                        </f:Form>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow ID="next3">
                                                    <Items>
                                                        <f:CheckBox ID="ckAccept" MarginLeft="40px" runat="server" Text="确认接受" LabelWidth="200px" Readonly="true" Hidden="true" Checked="true">
                                                        </f:CheckBox>
                                                    </Items>
                                                </f:FormRow>
                                            </Rows>
                                        </f:Form>
                                    </Items>
                                </f:GroupPanel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Panel ID="Panel2" AutoScroll="true" runat="server" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:GroupPanel runat="server" Title="审批步骤" BodyPadding="1px" ID="GroupPanel3"
                                    EnableCollapse="True" Collapsed="false" EnableCollapseEvent="true"
                                    EnableExpandEvent="true">
                                    <Items>
                                        <f:Form ID="Form3" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                            <Rows>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:Grid ID="gvFlowOperate" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                                                            DataIDField="FlowOperateId" AllowSorting="true" SortField="OperateTime"  ForceFit="true"
                                                            SortDirection="ASC" EnableTextSelection="True">
                                                            <Columns>
                                                                <f:RenderField Width="250px" ColumnID="OperateName" DataField="OperateName" 
                                                                    FieldType="String" HeaderText="操作步骤"  HeaderTextAlign="Center" TextAlign="Left">
                                                                </f:RenderField>
                                                                <f:RenderField Width="200px" ColumnID="UserName" DataField="UserName" 
                                                                    FieldType="String" HeaderText="操作人" HeaderTextAlign="Center" TextAlign="Left">
                                                                </f:RenderField>
                                                                <f:RenderField Width="150px" ColumnID="Opinion" DataField="Opinion" 
                                                                    FieldType="string" HeaderText="操作意见" HeaderTextAlign="Center" TextAlign="Left">
                                                                </f:RenderField>
                                                                <f:RenderField Width="160px" ColumnID="OperateTime" DataField="OperateTime" 
                                                                    FieldType="string" HeaderText="操作时间" HeaderTextAlign="Center" TextAlign="Center">
                                                                </f:RenderField>

                                                            </Columns>
                                                        </f:Grid>
                                                    </Items>
                                                </f:FormRow>
                                            </Rows>
                                        </f:Form>
                                    </Items>
                                </f:GroupPanel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:HiddenField ID="hdPauseNoticeId" runat="server"></f:HiddenField>
                        <f:Label runat="server" ID="lbTemp">
                        </f:Label>
                        <f:Button ID="btnNoticeUrl" Text="通知单" ToolTip="通知单上传及查看" Icon="TableCell" runat="server"
                            OnClick="btnNoticeUrl_Click" ValidateForms="SimpleForm1">
                        </f:Button>
                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click" Hidden="true">
                        </f:Button>
                        <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                            OnClick="btnSubmit_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
        <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
    </form>
</body>
</html>

﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PauseNoticeEdit.aspx.cs"
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
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" EnableTableStyle="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow ColumnWidths="45% 30% 25%">
                    <Items>
                        <f:DropDownList runat="server" EnableSimulateTree="True" Label="受检单位" ID="drpUnit"
                            AutoPostBack="true" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged" Required="true"
                            ShowRedStar="true" EnableEdit="true" Readonly="true" LabelWidth="150px">
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
                            MaxLength="150" Width="250px" Readonly="true" LabelWidth="150px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DatePicker ID="txtPauseTime" ShowRedStar="true" DateFormatString="yyyy-MM-dd HH:mm" runat="server" Label="现要求你公司于" Required="true" LabelAlign="Right" EnableEdit="true" ShowTime="true" ShowSecond="false" Readonly="true" LabelWidth="150px">
                        </f:DatePicker>
                        <f:Label runat="server" Text="日起停止施工"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow ID="IsAgree" Hidden="true">
                    <Items>
                        <f:RadioButtonList runat="server" ID="rdbIsAgree" Label="是否同意" ShowRedStar="true" LabelWidth="150px" AutoPostBack="true" OnSelectedIndexChanged="rdbIsAgree_SelectedIndexChanged">
                            <f:RadioItem Text="同意" Value="true" Selected="true" />
                            <f:RadioItem Text="不同意" Value="false" />
                        </f:RadioButtonList>
                    </Items>
                </f:FormRow>
                <f:FormRow ID="NoAgree" Hidden="true">
                    <Items>
                        <f:TextArea runat="server" ID="reason" Label="请输入原因" LabelWidth="150px"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow ID="BackMan">
                    <Items>
                        <f:DropDownList runat="server" ID="drpHandleMan" Label="办理人员" LabelWidth="150px" ShowRedStar="true"></f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:GroupPanel runat="server" Title="抄送人" BodyPadding="1px" ID="GroupPanel2"
                            EnableCollapse="True" Collapsed="false" EnableCollapseEvent="true"
                            EnableExpandEvent="true" Hidden="true">
                            <Items>
                                <f:DropDownList ID="drpProfessionalEngineer" runat="server" Label="总包专业工程师" LabelAlign="Right" EnableEdit="true" LabelWidth="150px">
                                </f:DropDownList>
                                <f:DropDownList ID="drpConstructionManager" runat="server" Label="总包施工经理" LabelAlign="Right" EnableEdit="true" LabelWidth="150px">
                                </f:DropDownList>
                                <f:DropDownList ID="drpUnitHeadMan" runat="server" Label="相关施工分包单位" LabelAlign="Right" EnableEdit="true" LabelWidth="150px">
                                </f:DropDownList>

                            </Items>
                            <Items>
                                <f:DropDownList ID="drpSupervisorMan" runat="server" Label="监理" LabelAlign="Right" EnableEdit="true" LabelWidth="150px">
                                </f:DropDownList>
                                <f:DropDownList ID="drpOwner" runat="server" Label="业主" LabelAlign="Right" EnableEdit="true" LabelWidth="150px">
                                </f:DropDownList>
                            </Items>
                        </f:GroupPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow ID="next3">
                    <Items>
                        <f:CheckBox ID="ckAccept" runat="server" Text="确认接受" LabelWidth="150px" Readonly="true" Hidden="true" Checked="true">
                        </f:CheckBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:GroupPanel runat="server" Title="审批步骤" BodyPadding="1px" ID="GroupPanel3"
                            EnableCollapse="True" Collapsed="false" EnableCollapseEvent="true"
                            EnableExpandEvent="true">
                            <Items>
                                <f:Grid ID="gvFlowOperate" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                                    DataIDField="FlowOperateId" AllowSorting="true" SortField="OperateTime" ForceFit="true"
                                    SortDirection="ASC" EnableTextSelection="True">
                                    <Columns>
                                        <f:TemplateField ColumnID="tfNumber" Width="55px" HeaderText="序号" HeaderTextAlign="Center"
                                            TextAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNumber" runat="server" Text='<%# gvFlowOperate.PageIndex * gvFlowOperate.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </f:TemplateField>
                                        <f:RenderField Width="250px" ColumnID="OperateName" DataField="OperateName"
                                            FieldType="String" HeaderText="步骤" HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:RenderField Width="200px" ColumnID="UserName" DataField="UserName"
                                            FieldType="String" HeaderText="操作人" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="150px" ColumnID="Opinion" DataField="Opinion"
                                            FieldType="string" HeaderText="意见" HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:RenderField Width="80px" ColumnID="IsAgree" DataField="IsAgree" FieldType="String"
                                            HeaderText="是否同意" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="160px" ColumnID="OperateTime" DataField="OperateTime"
                                            FieldType="string" HeaderText="时间" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>

                                    </Columns>
                                </f:Grid>
                            </Items>
                        </f:GroupPanel>
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
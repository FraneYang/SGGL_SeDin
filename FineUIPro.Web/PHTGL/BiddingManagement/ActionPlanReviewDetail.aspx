﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActionPlanReviewDetail.aspx.cs" Inherits="FineUIPro.Web.PHTGL.BiddingManagement.ActionPlanReviewDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:Panel ID="Panel4" IsFluid="true" CssClass="blockpanel" BodyPadding="10" Layout="VBox" BoxConfigChildMargin="0 0 5 0" AutoScroll="true"
            EnableCollapse="true" Title="" runat="server">
            <Toolbars>
                <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:Button ID="btnAttachUrl" Text="总承包合同附件" ToolTip="总承包合同附件" Icon="TableCell" runat="server"
                            OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                        </f:Button>
                        <f:Button ID="btnAgree" Icon="SystemSave" runat="server" ToolTip="同意" Text="同意" ValidateForms="SimpleForm1" Enabled="false" Size="Medium"
                            OnClick="btnAgree_Click" ConfirmText="请确认是否同意！">
                        </f:Button>
                        <f:Button ID="btnDisgree" Icon="SystemSave" runat="server" ToolTip="不同意" Text="不同意" ValidateForms="SimpleForm1" Enabled="false" Size="Medium"
                            OnClick="btnDisgree_Click" ConfirmText="请确认是否不同意！">
                        </f:Button>

                    </Items>

                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:Form ID="Form3" ShowBorder="false" ShowHeader="false" Title="审批流程设置"
                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:TextBox ID="txtProjectName" Label="项目名称" Margin="0 5 0 0" Readonly="true" ColumnWidth="50%" runat="server">
                                </f:TextBox>
                                <f:TextBox ID="txtProjectCode" Label="总承包合同编号" Margin="0 5 0 0" Readonly="true" ColumnWidth="50%" runat="server">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:TextBox ID="txtCreateUser" Label="经办人" Readonly="true" runat="server">
                                </f:TextBox>
                                <f:Button ID="LooK" Icon="Zoom" runat="server" ToolTip="查看" Text="查看文件" ValidateForms="SimpleForm1"
                                    OnClick="btnLooK_Click">
                                </f:Button>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:TextArea ID="txtApproveIdea" Height="200px" Required="true" Label="审批意见" ShowRedStar="true" runat="server" Text="">
                                </f:TextArea>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="审批记录"
                                    runat="server" BoxFlex="1" DataKeyNames="ApproveId" AllowCellEditing="true" ForceFit="true"
                                    ClicksToEdit="2" DataIDField="ApproveId" EnableColumnLines="true"  Height="300"
                                    EnableTextSelection="True">
                                    <Columns>
                                        <f:TemplateField ColumnID="tfPageIndex" Width="55px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                                            EnableLock="true" Locked="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </f:TemplateField>
                                        <f:RenderField ColumnID="ApproveMan" DataField="ApproveMan" Width="150px" FieldType="String" HeaderText="审批人" TextAlign="Center"
                                            HeaderTextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField ColumnID="IsAgree" DataField="IsAgree" Width="150px" FieldType="String" HeaderText="是否同意" TextAlign="Center"
                                            HeaderTextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField ColumnID="ApproveIdea" DataField="ApproveIdea" Width="320px" FieldType="String" HeaderText="审批意见" TextAlign="Left"
                                            HeaderTextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField ColumnID="ApproveDate" DataField="ApproveDate" Width="320px"  FieldType="String" HeaderText="审批时间" TextAlign="Left"
                                            HeaderTextAlign="Center">
                                        </f:RenderField>
                                    </Columns>
                                </f:Grid>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="查看" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="false" EnableDrag="true" EnableMinimize="true"
            Width="1000px" Height="800px">
        </f:Window>
        <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
    </form>
    <script type="text/javascript">      

        // 同时只能选中一项
        function onCheckBoxListChange(event, checkbox, isChecked) {
            var me = this;

            // 当前操作是：选中
            if (isChecked) {
                // 仅选中这一项
                me.setValue(checkbox.getInputValue());
            }


            __doPostBack('', 'CheckBoxList1Change');
        }

    </script>
</body>
</html>

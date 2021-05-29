<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproveUserReviewDetail.aspx.cs" Inherits="FineUIPro.Web.PHTGL.BiddingManagement.ApproveUserReviewDetail" %>

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
            <Items>
                <f:Form ID="Form3" ShowBorder="false" ShowHeader="false" Title="审批流程设置"
                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:Label ID="Label1" runat="server" Text="评标小组名单审批" CssClass="widthBlod"></f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:TextBox ID="txtProjectName" runat="server" Label="项目名称" LabelAlign="Right" Readonly="true" LabelWidth="140px"></f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:DropDownList ID="drpProjectId" runat="server" Label="招标项目" LabelAlign="Right" AutoPostBack="true"  LabelWidth="120px"></f:DropDownList>

                                <f:TextBox ID="txtBidDocumentsCode" runat="server" Label="招标文件编号" LabelAlign="Right" LabelWidth="140px"></f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:Label ID="Label7" runat="server" Text="一、	评标小组成员名单"></f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:Grid ID="Grid2" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" Title="" EnableCollapse="false"
                                    runat="server" DataKeyNames="ID" AllowCellEditing="true" ClicksToEdit="1" ForceFit="true"
                                    EnableColumnLines="true" DataIDField="ID" >
                                    <Columns>
                                        <f:RowNumberField HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center" Width="60px"></f:RowNumberField>
                                        <f:RenderField ColumnID="ApproveUserName" DataField="ApproveUserName" FieldType="String"
                                            HeaderText="姓名" HeaderTextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField ColumnID="ApproveUserSpecial" DataField="ApproveUserSpecial" FieldType="String"
                                            HeaderText="专业" HeaderTextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField ColumnID="ApproveUserUnit" DataField="ApproveUserUnit" FieldType="String"
                                            HeaderText="单位/部室" HeaderTextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField ColumnID="Remarks" DataField="Remarks" FieldType="String"
                                            HeaderText="说明" HeaderTextAlign="Center">
                                        </f:RenderField>
                                    </Columns>
                                </f:Grid>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:TextBox ID="txtCreateUser" Label="经办人" Readonly="true" runat="server">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>

                                <f:TextBox ID="txtApproveType" Label="当前节点" Margin="0 5 0 0" Readonly="true" ColumnWidth="50%" runat="server">
                                </f:TextBox>
                                <f:CheckBoxList ID="CBIsAgree" Label="是否同意" runat="server" ColumnWidth="50%">
                                    <Items>
                                        <f:CheckItem Text="同意" Value="2" />
                                        <f:CheckItem Text="不同意" Value="1" />
                                    </Items>

                                    <Listeners>
                                        <f:Listener Event="change" Handler="onCheckBoxListChange" />
                                    </Listeners>
                                </f:CheckBoxList>

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
                                    ClicksToEdit="2" DataIDField="ApproveId" EnableColumnLines="true" SortField="ApproveType" Height="300"
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
                                        <f:RenderField ColumnID="ApproveDate" DataField="ApproveDate" Width="320px" FieldType="String" HeaderText="审批时间" TextAlign="Left"
                                            HeaderTextAlign="Center">
                                        </f:RenderField>

                                    </Columns>
                                </f:Grid>

                            </Items>

                        </f:FormRow>

                    </Rows>


                </f:Form>
            </Items>

            <Toolbars>
                <f:Toolbar ID="Toolbar3" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" Text="保存" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" Text="关闭" runat="server" Icon="SystemClose">
                        </f:Button>
                        <%--   <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                    </f:Button>   --%>
                    </Items>

                </f:Toolbar>
            </Toolbars>
        </f:Panel>



        <f:Window ID="Window1" Title="流程步骤设置" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true"
            Width="640px" Height="450px">
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

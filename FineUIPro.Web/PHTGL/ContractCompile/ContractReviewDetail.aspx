<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractReviewDetail.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.ContractReviewDetail" ValidateRequest="false" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>系统环境设置</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }

        .LabelColor {
            color: Red;
            font-size: small;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:Panel ID="Panel2" runat="server" ShowHeader="false" ShowBorder="false" ColumnWidth="100%" MarginRight="5px">
            <Items>
               <f:TabStrip ID="TabStrip1" CssClass="f-tabstrip-theme-simple"  Height="700"    ShowBorder="true"
                    TabPosition="Top" MarginBottom="5px" EnableTabCloseMenu="false" runat="server"
                    ActiveTabIndex="1">
                    <Tabs>
                        <f:Tab ID="Tab1" Title="施工合同会签评审单" BodyPadding="5px" Layout="Fit" IconFont="Bookmark" runat="server">
                            <Items>
                                <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="true" Title="施工合同会签评审单"
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Items>
                                        <f:Panel ID="Panel3" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                            <Items>
                                                <f:Label runat="server" ID="Label4" Text=""></f:Label>

                                                <f:TextBox ID="txtProjectid" Label="编号" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel1" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                            <Items>
                                                <f:TextBox ID="txtContractName" Label="合同名称" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                                <f:TextBox ID="txtContractNum" Label="合同编号" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel13" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                            <Items>
                                                <f:TextBox ID="txtParties" Label="签约方" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                                <f:TextBox ID="txtContractAmount" Label="（预计）合同金额" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                            </Items>
                                        </f:Panel>
                                       <f:CheckBoxList ID="CBContractType" Label="合同类型" runat="server" ColumnNumber="3" ColumnWidth="50%">
                                            <%--<f:CheckItem Text="施工总承包分包合同" Value="value1" />
                                            <f:CheckItem Text="施工专业分包合同" Value="value2" />
                                            <f:CheckItem Text="施工劳务分包合同 " Value="value3" />
                                           <f:CheckItem Text="试车服务合同" Value="value3" />
                                           <f:CheckItem Text="租赁合同" Value="value3" />--%>
                                        </f:CheckBoxList>
                                        <f:Panel ID="Panel15" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                            <Items>
                                                <f:TextBox ID="txtDepartId" Label="主办部门" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                                <f:TextBox ID="txtAgent" Label="经办人" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                            </Items>
                                        </f:Panel>

                                        <f:TextArea ID="txtnode1" Height="150px" Required="true" Label="施工经理" ShowRedStar="true" runat="server" Text="">
                                        </f:TextArea>
                                        <f:TextArea ID="txtnode2" Height="150px" Required="true" Label="HSE经理" ShowRedStar="true" runat="server" Text="">
                                        </f:TextArea>
                                        <f:TextArea ID="txtnode3" Height="150px" Required="true" Label="质量经理" ShowRedStar="true" runat="server" Text="">
                                        </f:TextArea>
                                        <f:TextArea ID="txtnode4" Height="150px" Required="true" Label="采购经理" ShowRedStar="true" runat="server" Text="">
                                        </f:TextArea>
                                        <f:TextArea ID="txtnode5" Height="150px" Required="true" Label="控制经理" ShowRedStar="true" runat="server" Text="">
                                        </f:TextArea>
                                        <f:TextArea ID="txtnode6" Height="150px" Required="true" Label="财务经理" ShowRedStar="true" runat="server" Text="">
                                        </f:TextArea>
                                        <f:TextArea ID="txtnode7" Height="150px" Required="true" Label="项目经理" ShowRedStar="true" runat="server" Text="">
                                        </f:TextArea>
                                        <f:TextArea ID="txtnode8" Height="150px" Required="true" Label="施工管理部" ShowRedStar="true" runat="server" Text="">
                                        </f:TextArea>
                                        <f:TextArea ID="txtnode9" Height="150px" Required="true" Label="法律合规部" ShowRedStar="true" runat="server" Text="">
                                        </f:TextArea>
                                    </Items>
                                </f:Form>

                            </Items>
                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                                    <Items>
                                       <%-- <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1"
                                            OnClick="btnSave_Click">
                                        </f:Button>--%>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Tab>
                        <f:Tab ID="Tab2" Title="审批流程" BodyPadding="5px" Layout="Fit" IconFont="Bookmark" runat="server">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
                                    <Items>
                                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1" ToolTip="保存" Text="保存"
                                            OnClick="btnSave_Click">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Items>
                                <f:Form ID="Form3" ShowBorder="false" ShowHeader="false" AutoScroll="true" Title="审批流程"
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Items>
                                        <f:Panel ID="Panel4" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                            <Items>
                                                <f:TextBox ID="Tab2_txtContractName" Label="合同名称" Margin="0 5 0 0"  Readonly="true"  ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                                <f:TextBox ID="Tab2_txtContractNum" Label="合同编号" Margin="0 5 0 0"   Readonly="true"  ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel5" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                            <Items>
                                                <f:TextBox ID="Tab2_txtParties" Label="签约方" Margin="0 5 0 0"  Readonly="true"  ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                                <f:TextBox ID="Tab2_txtContractAmount" Label="（预计）合同金额" Margin="0 5 0 0"   Readonly="true"  ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                            </Items>
                                        </f:Panel>
                                        <f:CheckBoxList ID="Tab2_CBContractType" Label="合同类型" runat="server" ColumnNumber="3" ColumnWidth="50%">
 
                                        </f:CheckBoxList>
                                        <f:Panel ID="Panel6" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                            <Items>
                                                <f:TextBox ID="Tab2_txtDepartId" Label="主办部门" Margin="0 5 0 0"  Readonly="true"  ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                                <f:TextBox ID="Tab2_txtAgent" Label="经办人" Margin="0 5 0 0"  Readonly="true"  ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel7" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                            <Items>
                                                <f:TextBox ID="txtApproveType" Label="当前节点" Margin="0 5 0 0" Readonly="true"  ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                                <f:CheckBoxList ID="CBIsAgree" Label="是否同意" runat="server"  ColumnWidth="50%">
                                                    <Items>
                                                        <f:CheckItem Text="同意" Value="2" />
                                                       <f:CheckItem Text="不同意" Value="1" />
                                                        </Items>
                                                    
                                                    <Listeners>
                                                        <f:Listener Event="change" Handler="onCheckBoxListChange" />
                                                    </Listeners>
                                                </f:CheckBoxList>
                                            </Items>
                                        </f:Panel>
                                        
                                         <f:TextArea ID="txtApproveIdea" Height="200px" Required="true" Label="审批意见" ShowRedStar="true" runat="server" Text="">
                                        </f:TextArea>
                                        <f:Grid ID="Grid1" ShowBorder="true" EnableAjax="false" ShowHeader="true" Title="审批记录"
                                            runat="server" BoxFlex="1" DataKeyNames="ApproveId" AllowCellEditing="true"  ForceFit="true"
                                            ClicksToEdit="2" DataIDField="ApproveId" EnableColumnLines="true" SortField="ApproveDate" Height="300"
                                            EnableTextSelection="True" >

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
                                </f:Form>

                            </Items>
                        </f:Tab>
                        <f:Tab ID="Tab3" Title="施工合同签订审批表" BodyPadding="5px" Layout="Fit" IconFont="Bookmark" runat="server">
                            <Items>
                                <f:Form ID="Form4" ShowBorder="false" ShowHeader="false" AutoScroll="true" Title="施工合同会签评审单"
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Items>
                                        <f:Panel ID="Panel8" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                            <Items>
                                                <f:Label runat="server" ID="Label1" Text=""></f:Label>

                                                <f:TextBox ID="Tab3_txtProjectid" Label="编号" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel9" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                            <Items>
                                                <f:TextBox ID="Tab3_txtContractName" Label="合同名称" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                                <f:TextBox ID="Tab3_txtContractNum" Label="合同编号" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel10" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                            <Items>
                                                <f:TextBox ID="Tab3_txtParties" Label="签约方" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                                <f:TextBox ID="Tab3_txtContractAmount" Label="（预计）合同金额" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                            </Items>
                                        </f:Panel>
                                       <f:CheckBoxList ID="Tab3_CBContractType" Label="合同类型" runat="server" ColumnNumber="3" ColumnWidth="50%">
                                         </f:CheckBoxList>
                                        <f:Panel ID="Panel11" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                            <Items>
                                                <f:TextBox ID="Tab3_txtDepartId" Label="主办部门" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                                <f:TextBox ID="Tab3_txtAgent" Label="经办人" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                            </Items>
                                        </f:Panel>

                                        <f:TextArea ID="txtRemark" Height="150px" Required="true" Label="合同摘要" ShowRedStar="true" runat="server" Text="">
                                        </f:TextArea>
                                        <f:TextArea ID="TextArea2" Height="150px" Required="true" Label="合同评审意见落实情况及问题说明" ShowRedStar="true" runat="server" Text="已按评审意见修改完成，同意签订本合同。">
                                        </f:TextArea>
                                        <f:TextArea ID="txtnode10" Height="150px" Required="true" Label="施工管理部" ShowRedStar="true" runat="server" Text="">
                                        </f:TextArea>
                                        <f:TextArea ID="txtnode11" Height="150px" Required="true" Label="法律合规部" ShowRedStar="true" runat="server" Text="">
                                        </f:TextArea>
                                        <f:TextArea ID="txtnode12" Height="150px" Required="true" Label="项目经理" ShowRedStar="true" runat="server" Text="">
                                        </f:TextArea>
                                        <f:TextArea ID="txtnode13" Height="150px" Required="true" Label="分管副总经理" ShowRedStar="true" runat="server" Text="">
                                        </f:TextArea>
                                        <f:TextArea ID="txtnode14" Height="150px" Required="true" Label="总会计师" ShowRedStar="true" runat="server" Text="">
                                        </f:TextArea>
                                        <f:TextArea ID="txtnode15" Height="150px" Required="true" Label="总经理" ShowRedStar="true" runat="server" Text="">
                                        </f:TextArea>
                                        <f:TextArea ID="txtnode16" Height="150px" Required="true" Label="董事长" ShowRedStar="true" runat="server" Text="">
                                        </f:TextArea>
                                    </Items>
                                </f:Form>

                            </Items>
                            <Toolbars>
                                <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                                    <Items>
                                       <%-- <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1"
                                            OnClick="btnSave_Click">
                                        </f:Button>--%>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Tab>
                    </Tabs>
                </f:TabStrip>

            </Items>
                     <Toolbars>
                <f:Toolbar ID="Toolbar4" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                      
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
        function onGridDataLoad(event) {
            this.mergeColumns(['FlowStep', 'GroupNum'], { depends: true });
        }
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

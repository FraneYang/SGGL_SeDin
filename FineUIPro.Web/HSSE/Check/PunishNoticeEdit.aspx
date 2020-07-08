<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PunishNoticeEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Check.PunishNoticeEdit" ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑处罚通知单</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="处罚通知单" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtPunishNoticeCode" runat="server" Label="编号" LabelAlign="Right"
                            MaxLength="50" Readonly="true" LabelWidth="90px">
                        </f:TextBox>
                        <f:DatePicker ID="txtPunishNoticeDate" runat="server" Label="处罚时间" LabelAlign="Right"
                            EnableEdit="true" Required="true" ShowRedStar="true" LabelWidth="90px" Readonly="true">
                        </f:DatePicker>
                        <f:DropDownList ID="drpUnitId" runat="server" Label="受罚单位" LabelAlign="Right" EnableEdit="true" LabelWidth="90px"
                            Required="true" ShowRedStar="true" OnSelectedIndexChanged="drpUnitId_SelectedIndexChanged" AutoPostBack="true" Readonly="true">
                        </f:DropDownList>
                        <f:DropDownList ID="drpPunishPersonId" runat="server" Label="受罚人" LabelAlign="Right" EnableEdit="true" Readonly="true" LabelWidth="90px">
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>                        
                        <f:TextBox ID="txtIncentiveReason" runat="server" Label="处罚原因" LabelAlign="Right" MaxLength="300" Readonly="true" LabelWidth="90px">
                        </f:TextBox>
                       <f:TextBox ID="txtBasicItem" runat="server" Label="处罚根据" LabelAlign="Right" MaxLength="300" Readonly="true" LabelWidth="90px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow ColumnWidths="50% 20% 30%">
                    <Items>
                        <f:NumberBox runat="server" ID="txtPunishMoney" Label="处罚金额" OnBlur="txtPunishMoney_Blur"
                            EnableBlurEvent="true" NoNegative="true" LabelWidth="90px" Readonly="true">
                        </f:NumberBox>
                        <f:TextBox runat="server" ID="txtCurrency" Label="币种" MaxLength="50" LabelWidth="60px" Readonly="true">
                        </f:TextBox>
                        <f:TextBox runat="server" ID="txtBig" Label="大写" Readonly="true" LabelWidth="60px" >
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow Hidden="true">
                    <Items>
                        <f:HtmlEditor runat="server" Label="处罚原因/决定" ID="txtFileContents" ShowLabel="false"
                            Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="220" LabelAlign="Right" Readonly="true">
                        </f:HtmlEditor>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" AllowCellEditing="true" ForceFit="true" EnableCollapse="true" EnableColumnLines="true" EnableColumnMove="true" runat="server" BoxFlex="1" DataKeyNames="PunishNoticeItemId" DataIDField="PunishNoticeItemId" AllowSorting="true" SortField="PunishNoticeItemId" SortDirection="ASC" EnableTextSelection="True" Height="240px"
                            EnableRowDoubleClickEvent="true" OnRowCommand="Grid1_RowCommand" OnRowDataBound="Grid1_RowDataBound">
                            <Toolbars>
                                <f:Toolbar ID="toolAdd" Position="Top" ToolbarAlign="Right" runat="server">
                                    <Items>
                                        <f:Button ID="btnAdd" Icon="Add" runat="server" OnClick="btnAdd_Click" ToolTip="新增" Hidden="true">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:TemplateField ColumnID="tfNumber" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                                    TextAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:RenderField HeaderText="主键" ColumnID="PunishNoticeItemId" DataField="PunishNoticeItemId"
                                    SortField="PunishNoticeItemId" FieldType="String" HeaderTextAlign="Center" TextAlign="Center"
                                    Hidden="true">
                                </f:RenderField>
                                <f:RenderField Width="300px" ColumnID="PunishContent" DataField="PunishContent" FieldType="string"
                                    HeaderText="处罚原因">
                                    <Editor>
                                        <f:TextBox ID="tWrongContent" runat="server" MaxLength="800" ShowRedStar="true" Required="true">
                                        </f:TextBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="PunishMoney" DataField="PunishMoney" FieldType="string"
                                    HeaderText="金额">
                                    <Editor>
                                        <f:NumberBox runat="server" ID="txtMoney"
                                            EnableBlurEvent="true" NoNegative="true" LabelWidth="90px">
                                        </f:NumberBox>
                                    </Editor>
                                </f:RenderField>
                                <f:LinkButtonField ID="del" ColumnID="del" HeaderText="删除" Width="60px" CommandName="delete"
                                    Icon="Delete" Hidden="true" />
                            </Columns>
                            <Listeners>
                                <f:Listener Event="afteredit" Handler="onGridAfterEdit" />
                                <%--<f:Listener Event="dataload" Handler="onGridDataLoad" />--%>
                            </Listeners>
                        </f:Grid>
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
                                                                <f:RadioButtonList runat="server" ID="rdbIsAgree" Label="是否同意" ShowRedStar="true" LabelWidth="160px" AutoPostBack="true" OnSelectedIndexChanged="rdbIsAgree_SelectedIndexChanged">
                                                                    <f:RadioItem Text="同意" Value="true" Selected="true" />
                                                                    <f:RadioItem Text="不同意" Value="false" />
                                                                </f:RadioButtonList>
                                                            </Items>
                                                        </f:Panel>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow ID="NoAgree" Hidden="true">
                                                    <Items>
                                                        <f:TextArea runat="server" ID="reason" Label="请输入原因" LabelWidth="160px"></f:TextArea>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow ID="BackMan" Hidden="true">
                                                    <Items>
                                                        <f:DropDownList runat="server" ID="drpHandleMan" Label="办理人员" LabelWidth="160px" Readonly="true"></f:DropDownList>
                                                    </Items>
                                                </f:FormRow>
                                            </Rows>
                                        </f:Form>
                                        <f:Form ID="HandleType" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                            <Rows>
                                                <f:FormRow runat="server" ID="next" Hidden="true">
                                                    <Items>
                                                        <f:DropDownList ID="drpSignPerson" runat="server" Label="总包安全工程师/安全经理" LabelWidth="190px"
                                                            LabelAlign="Right" EnableEdit="true" ShowRedStar="true">
                                                        </f:DropDownList>
                                                        <f:Label runat="server"></f:Label>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow Hidden="true" ID="next1">
                                                    <Items>
                                                        <f:Form runat="server" ShowBorder="false" ShowHeader="false" BodyPadding="10px">
                                                            <Rows>
                                                                <f:FormRow ID="step1_person">
                                                                    <Items>
                                                                        <f:DropDownList ID="drpApproveMan" runat="server" Label="总包项目经理" LabelAlign="Right" EnableEdit="true" ShowRedStar="true" LabelWidth="150px">
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
                                                <f:FormRow ID="next3">
                                                    <Items>
                                                        <f:CheckBox ID="ckAccept" MarginLeft="40px" runat="server" Text="确认接受" LabelWidth="200px" Readonly="true" Hidden="true" Checked="true">
                                                        </f:CheckBox>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow ID="next2" Hidden="true">
                                                    <Items>
                                                        <f:Form ID="Form4" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                                            <Rows>
                                                                <f:FormRow>
                                                                    <Items>
                                                                        <f:DropDownList ID="drpDutyPerson" runat="server" Label="施工分包单位" LabelAlign="Right" EnableEdit="true" ShowRedStar="true" LabelWidth="150px">
                                                                        </f:DropDownList>
                                                                        <f:Label runat="server">
                                                                        </f:Label>
                                                                    </Items>
                                                                </f:FormRow>
                                                            </Rows>
                                                        </f:Form>
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
                                                        <f:Grid ID="gvFlowOperate" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server" DataIDField="FlowOperateId" AllowSorting="true" SortField="OperateTime" SortDirection="ASC" EnableTextSelection="True">
                                                            <Columns>
                                                                <f:RenderField Width="200px" ColumnID="OperateName" DataField="OperateName" FieldType="String" HeaderText="操作步骤" ExpandUnusedSpace="true" HeaderTextAlign="Center" TextAlign="Center">
                                                                </f:RenderField>
                                                                <f:RenderField Width="200px" ColumnID="UserName" DataField="UserName" FieldType="String" HeaderText="操作人" HeaderTextAlign="Center" TextAlign="Center">
                                                                </f:RenderField>
                                                                <f:RenderField Width="150px" ColumnID="Opinion" DataField="Opinion" FieldType="string" HeaderText="操作意见" HeaderTextAlign="Center" TextAlign="Center">
                                                                </f:RenderField>
                                                                <f:RenderField Width="150px" ColumnID="OperateTime" DataField="OperateTime" FieldType="string" HeaderText="操作时间" HeaderTextAlign="Center" TextAlign="Center">
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
                        <f:HiddenField ID="hdPunishNoticeId" runat="server"></f:HiddenField>
                        <f:Label runat="server" ID="lbTemp">
                        </f:Label>
                        <f:Button ID="btnPunishNoticeUrl" Text="通知单" ToolTip="通知单上传及查看" Icon="TableCell" runat="server"
                            OnClick="btnPunishNoticeUrl_Click" ValidateForms="SimpleForm1">
                        </f:Button>
                        <f:Button ID="btnAttachUrl" Text="回执单" ToolTip="回执单上传及查看" Icon="TableCell" runat="server"
                            OnClick="btnUploadResources_Click" ValidateForms="SimpleForm1">
                        </f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
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
    <script type="text/javascript">
        // function updateSummary() {
        //    var grid1 = F.ui.Grid1, TotalMoney = 0;
        //    grid1.getRowEls().each(function (index, tr) {
        //        TotalMoney += grid1.getCellValue(tr, 'PunishMoney');
        //    });

        //    // 第三个参数 true，强制更新，不显示左上角的更改标识
        //    grid1.updateSummaryCellValue('PunishMoney', TotalMoney, true);
        //}

        //// 表格数据加载后，重现计算合计行（这样就不必要在删除行时更新合计行）
        //function onGrid1DataLoad() {
        //    updateSummary();
        //}

        function onGridAfterEdit(event, value, params) {
            updateSummary();
        }
        function updateSummary() {
            // 回发到后台更新
            __doPostBack('', 'UPDATE_SUMMARY');
        }
    </script>
</body>
</html>

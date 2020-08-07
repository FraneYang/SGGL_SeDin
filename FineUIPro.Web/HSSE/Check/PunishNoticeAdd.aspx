<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PunishNoticeAdd.aspx.cs" Inherits="FineUIPro.Web.HSSE.Check.PunishNoticeAdd" %>

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
                            MaxLength="50" LabelWidth="90px">
                        </f:TextBox>
                        <f:DatePicker ID="txtPunishNoticeDate" runat="server" Label="处罚时间" LabelAlign="Right"
                            EnableEdit="true" Required="true" ShowRedStar="true" LabelWidth="90px">
                        </f:DatePicker>
                        <f:DropDownList ID="drpUnitId" runat="server" Label="受罚单位" LabelAlign="Right" EnableEdit="true" LabelWidth="90px"
                            Required="true" ShowRedStar="true" OnSelectedIndexChanged="drpUnitId_SelectedIndexChanged" AutoPostBack="true">
                        </f:DropDownList>
                        <f:DropDownList ID="drpPunishPersonId" runat="server" Label="受罚人" LabelAlign="Right" EnableEdit="true" LabelWidth="90px">
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
                
                <f:FormRow Hidden="true">
                    <Items>
                        <f:HtmlEditor runat="server" Label="处罚原因/决定" ID="txtFileContents" ShowLabel="false"
                            Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="220" LabelAlign="Right">
                        </f:HtmlEditor>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" AllowCellEditing="true" ForceFit="true" EnableCollapse="true"
                            EnableColumnLines="true" EnableColumnMove="true" runat="server" BoxFlex="1" DataKeyNames="PunishNoticeItemId"
                            DataIDField="PunishNoticeItemId" AllowSorting="true" SortField="PunishNoticeItemId" SortDirection="ASC"
                            EnableTextSelection="True" Height="240px" EnableRowDoubleClickEvent="true" OnRowCommand="Grid1_RowCommand"
                            OnRowDataBound="Grid1_RowDataBound">
                            <Toolbars>
                                <f:Toolbar ID="toolAdd" Position="Top" ToolbarAlign="Right" runat="server">
                                    <Items>
                                        <f:Button ID="btnAdd" Icon="Add" runat="server" OnClick="btnAdd_Click" ToolTip="新增">
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
                                    HeaderText="处罚原因" ExpandUnusedSpace="true">
                                    <Editor>
                                        <f:TextBox ID="tWrongContent" runat="server" MaxLength="800" ShowRedStar="true" Required="true">
                                        </f:TextBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField Width="300px" ColumnID="PunishBasicItem" DataField="PunishBasicItem" FieldType="string"
                                    HeaderText="处罚依据" ExpandUnusedSpace="true">
                                    <Editor>
                                        <f:TextBox ID="txtBasicItem" runat="server" MaxLength="800" ShowRedStar="true" Required="true">
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
                                    Icon="Delete" />
                            </Columns>
                            <Listeners>
                                <f:Listener Event="afteredit" Handler="onGridAfterEdit" />
                                <%--<f:Listener Event="dataload" Handler="onGridDataLoad" />--%>
                            </Listeners>
                        </f:Grid>
                    </Items>
                </f:FormRow>
                <f:FormRow ColumnWidths="50% 20% 30%" CssStyle="margin-top:10px">
                    <Items>
                        <f:NumberBox runat="server" ID="txtPunishMoney" Label="处罚金额" OnBlur="txtPunishMoney_Blur"
                            EnableBlurEvent="true" NoNegative="true" LabelWidth="90px">
                        </f:NumberBox>
                        <f:TextBox runat="server" ID="txtCurrency" Label="币种" MaxLength="50" LabelWidth="60px">
                        </f:TextBox>
                        <f:TextBox runat="server" ID="txtBig" Label="大写" LabelWidth="60px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow runat="server">
                    <Items>
                        <f:DropDownList ID="drpSignPerson" runat="server" Label="总包安全工程师/安全经理" LabelWidth="190px"
                            LabelAlign="Right" EnableEdit="true" ShowRedStar="true">
                        </f:DropDownList>
                        <f:Label runat="server"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Grid ID="gvFlowOperate" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server" DataIDField="FlowOperateId" AllowSorting="true" SortField="OperateTime" SortDirection="ASC" EnableTextSelection="True">
                            <Columns>
                                <f:TemplateField ColumnID="tfNumber" Width="55px" HeaderText="序号" HeaderTextAlign="Center"
                                    TextAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# gvFlowOperate.PageIndex * gvFlowOperate.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
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
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RectifyNoticesRecheck.aspx.cs" Inherits="FineUIPro.Web.HSSE.Check.RectifyNoticesRecheck" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Controls/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel4" />
        <f:Panel ID="Panel4" runat="server" BodyPadding="10px" ShowBorder="true" ShowHeader="false" AutoScroll="true">
            <Items>
                <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" EnableTableStyle="true"
                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                            <Items>
                                <f:HiddenField ID="hdRectifyNoticesId" runat="server"></f:HiddenField>
                                <f:Button ID="btnSave" OnClick="btnSave_Click" Icon="SystemSave" runat="server" ToolTip="保存">
                                </f:Button>
                                <f:Button ID="btnSubmit" OnClick="btnSubmit_Click" Icon="SystemSaveNew" runat="server" ToolTip="提交"
                                    ValidateForms="SimpleForm1">
                                </f:Button>
                                <f:Button ID="btnClose" EnablePostBack="false" runat="server" Icon="SystemClose">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Rows>
                        <f:FormRow ColumnWidths="33% 33% 34%">
                            <Items>
                                <f:TextBox ID="txtRectifyNoticesCode" runat="server" Label="编号" Readonly="true">
                                </f:TextBox>
                                <f:TextBox ID="txtUnit" runat="server" Label="受检单位" Readonly="true">
                                </f:TextBox>
                                <f:TextBox ID="txtWorkArea" runat="server" Label="单位工程" Readonly="true">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server" ColumnWidths="66% 34%">
                            <Items>
                                <f:TextBox ID="txtCheckMan" runat="server" Label="检查人员" LabelAlign="Right" Readonly="true">
                                </f:TextBox>
                                <f:TextBox runat="server" Label="检查人员" ID="txtCheckPerson" Readonly="true"></f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="33% 33% 34%">
                            <Items>
                                <f:TextBox runat="server" Label="检查日期" ID="txtCheckedDate" LabelAlign="right" Readonly="true">
                                </f:TextBox>
                                <f:TextBox ID="txtHiddenHazardType" runat="server" Label="隐患类别" LabelAlign="Right" Readonly="true">
                                </f:TextBox>
                                <f:Label runat="server"></f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全隐患及整改要求" runat="server" AllowCellEditing="true"
                                    DataKeyNames="RectifyNoticesItemId" DataIDField="RectifyNoticesItemId" EnableColumnLines="true"
                                    AllowSorting="true" SortField="RectifyNoticesItemId" SortDirection="ASC" EnableTextSelection="True"
                                    MinHeight="240px" PageSize="500">
                                    <Columns>
                                        <f:RenderField Width="200px" ColumnID="WrongContent" DataField="WrongContent" FieldType="string"
                                            HeaderText="具体位置及隐患内容" ExpandUnusedSpace="true" HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:RenderField Width="150px" ColumnID="Requirement" DataField="Requirement" FieldType="string"
                                            HeaderText="整改要求" HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:RenderField Width="120px" ColumnID="LimitTime" DataField="LimitTime" FieldType="Date"
                                            Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="整改期限">
                                        </f:RenderField>
                                        <f:TemplateField ColumnID="tfImageUrl1" Width="110px" HeaderText="整改前" HeaderTextAlign="Center"
                                            TextAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lbImageUrl" runat="server" Text='<%# ConvertImageUrlByImage(Eval("RectifyNoticesItemId")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </f:TemplateField>
                                        <f:TemplateField ColumnID="tfImageUrl2" Width="110px" HeaderText="整改后" HeaderTextAlign="Center"
                                            TextAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# ConvertImgUrlByImage(Eval("RectifyNoticesItemId")) %>'> %>'></asp:Label>
                                            </ItemTemplate>
                                        </f:TemplateField>
                                        <f:RenderField Width="120px" ColumnID="RectifyResults" DataField="RectifyResults" FieldType="string"
                                            HeaderText="整改结果" HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:RenderField Width="90px" ColumnID="IsRectify" DataField="IsRectifyName" FieldType="string"
                                            HeaderText="合格*" HeaderTextAlign="Center" TextAlign="Center">
                                            <Editor>
                                                <f:DropDownList ID="drpIsRectify" runat="server" MaxLength="800" ShowRedStar="true">
                                                    <f:ListItem Text="合格" Value="合格" />
                                                    <f:ListItem Text="不合格" Value="不合格" />
                                                </f:DropDownList>
                                            </Editor>
                                        </f:RenderField>
                                    </Columns>
                                    <Listeners>
                                        <f:Listener Event="afteredit" Handler="onGridAfterEdit" />
                                    </Listeners>
                                </f:Grid>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:RadioButtonList runat="server" ID="rdbIsAgree" Label="是否同意" ShowRedStar="true" AutoPostBack="true"
                                    OnSelectedIndexChanged="rdbIsAgree_SelectedIndexChanged" Readonly="true">
                                    <f:RadioItem Text="同意" Value="true" Selected="true" />
                                    <f:RadioItem Text="不同意" Value="false" />
                                </f:RadioButtonList>
                                <f:Label runat="server" ID="lbDutyPerson" Label="打回整改责任人" LabelAlign="Right" LabelWidth="170px"></f:Label>
                                <f:Label runat="server" ID="lbCom" Text="审核完成" LabelAlign="Right" LabelWidth="170px" Hidden="true"></f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:TextArea runat="server" ID="txtReCheckOpinion" Label="总包单位复查意见"  ShowRedStar="true"
                                    Height="50px"></f:TextArea>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="step1_person2">
                            <Items>
                                <f:GroupPanel runat="server" Title="抄送人" BodyPadding="1px" ID="GroupPanel1"
                                    EnableCollapse="True" Collapsed="false" EnableCollapseEvent="true"
                                    EnableExpandEvent="true">
                                    <Items>
                                        <f:TextBox ID="drpProfessionalEngineer" runat="server" Label="专业工程师" LabelAlign="Right"
                                            LabelWidth="110px" Readonly="true">
                                        </f:TextBox>
                                        <f:TextBox ID="drpConstructionManager" runat="server" Label="施工经理" LabelAlign="Right"
                                            LabelWidth="110px" Readonly="true">
                                        </f:TextBox>
                                        <f:TextBox ID="drpProjectManager" runat="server" Label="项目经理" LabelAlign="Right"
                                            LabelWidth="110px" Readonly="true">
                                        </f:TextBox>
                                    </Items>
                                </f:GroupPanel>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:Grid ID="gvFlowOperate" ShowBorder="true" ShowHeader="true" EnableTableStyle="true" Collapsed="true"
                                    EnableCollapse="true" runat="server" DataIDField="FlowOperateId" AllowSorting="true" EnableColumnLines="true"
                                    SortField="OperateTime" SortDirection="ASC" EnableTextSelection="True" Title="审核意见">
                                    <Columns>
                                        <f:TemplateField ColumnID="tfPageIndex" Width="50px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </f:TemplateField>
                                        <f:RenderField Width="200px" ColumnID="OperateName" DataField="OperateName"
                                            FieldType="String" HeaderText="步骤" HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:RenderField Width="100px" ColumnID="UserName" DataField="UserName"
                                            FieldType="String" HeaderText="操作人" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="80px" ColumnID="IsAgreeName" DataField="IsAgreeName" FieldType="String"
                                            HeaderText="是否同意" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="150px" ColumnID="Opinion" DataField="Opinion" ExpandUnusedSpace="true"
                                            FieldType="string" HeaderText="意见" HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:RenderField Width="150px" ColumnID="OperateTime" DataField="OperateTime"
                                            FieldType="string" HeaderText="时间" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                    </Columns>
                                </f:Grid>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
        </f:Panel>
    </form>
    <script type="text/javascript">
       var rdbIsAgreeID = '<%= rdbIsAgree.ClientID %>';
        var grid1ClientID = '<%= Grid1.ClientID %>';
        function onGridAfterEdit(event, value, params) {
            var rd = F(rdbIsAgreeID);
            var grid = F(grid1ClientID),allRectify="";                       
            grid.getRowEls().each(function (index, tr) {
                var vr = grid.getCellValue(tr, 'IsRectify');
                if (vr == "" || vr == null) {
                    vr = "不合格";
                }
                allRectify += vr;
            });

            if (allRectify.indexOf("不") == -1) {
                rd.setValue("true");
            } else {
                rd.setValue("false");
            }

            // 回发到后台更新
            __doPostBack('', 'UPDATE_SUMMARY');
        }
    </script>
</body>
</html>

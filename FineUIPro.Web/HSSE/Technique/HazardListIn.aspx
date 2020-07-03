<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HazardListIn.aspx.cs" Inherits="FineUIPro.Web.HSSE.Technique.HazardListIn" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>危险源清单导入</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" OnCustomEvent="PageManager1_CustomEvent" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Toolbars>
            <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnAudit" Icon="ApplicationEdit" runat="server" ToolTip="审核" ValidateForms="SimpleForm1"
                        OnClick="btnAudit_Click">
                    </f:Button>
                    <f:Button ID="btnImport" Icon="ApplicationGet" runat="server" ToolTip="导入" ValidateForms="SimpleForm1"
                        OnClick="btnImport_Click">
                    </f:Button>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnDownLoad" runat="server" Icon="ApplicationGo" ToolTip="下载模板" OnClick="btnDownLoad_Click">
                    </f:Button>
                    <f:Button ID="btnHelp" runat="server" ToolTip="点击下载本页面使用说明" Icon="Help" Text="帮助">
                        <Listeners>
                            <f:Listener Event="click" Handler="onToolSourceCodeClick" />
                        </Listeners>
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Rows>
            <f:FormRow>
                <Items>
                    <f:FileUpload runat="server" ID="fuAttachUrl" EmptyText="选择要导入的文件" Label="选择要导入的文件"
                        LabelWidth="150px">
                    </f:FileUpload>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        EnableColumnLines="true" BoxFlex="1" DataKeyNames="HazardId" AllowCellEditing="true"
                        ClicksToEdit="2" DataIDField="HazardId" AllowSorting="true" SortField="HazardCode"
                        PageSize="1000" Height="400px">
                        <Columns>
                            <f:TemplateField Width="50px" HeaderText="序号" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="80px" ColumnID="HazardCode" DataField="HazardCode" FieldType="String"
                                HeaderText="代码" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:TemplateField Width="120px" HeaderText="危险源类别" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblHazardListTypeId" runat="server" Text='<%# ConvertHazardListType(Eval("HazardListTypeId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="200px" ColumnID="HazardItems" DataField="HazardItems" SortField="HazardItems"
                                FieldType="String" HeaderText="危险因素明细" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="DefectsType" DataField="DefectsType" SortField="DefectsType"
                                FieldType="String" HeaderText="缺陷类型" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="MayLeadAccidents" DataField="MayLeadAccidents"
                                SortField="MayLeadAccidents" FieldType="String" HeaderText="可能导致的事故" HeaderTextAlign="Center"
                                TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="HelperMethod" DataField="HelperMethod" FieldType="String"
                                HeaderText="辅助方法" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="HazardJudge_L" DataField="HazardJudge_L" FieldType="String"
                                HeaderText="危险评价L" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="HazardJudge_E" DataField="HazardJudge_E" FieldType="String"
                                HeaderText="危险评价E" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="HazardJudge_C" DataField="HazardJudge_C" FieldType="String"
                                HeaderText="危险评价C" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="HazardJudge_D" DataField="HazardJudge_D" SortField="HazardJudge_D"
                                FieldType="String" HeaderText="危险评价D" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="HazardLevel" DataField="HazardLevel" SortField="HazardLevel"
                                FieldType="String" HeaderText="危险级别" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="ControlMeasures" DataField="ControlMeasures"
                                SortField="ControlMeasures" FieldType="String" HeaderText="控制措施" HeaderTextAlign="Center"
                                TextAlign="Center">
                            </f:RenderField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:HiddenField ID="hdFileName" runat="server">
                    </f:HiddenField>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label ID="lblBottom" runat="server" Text="说明：1 危险源清单导入模板中，灰色项为必填项。2 危险源类别等必须与基础信息中对应类型的名称一致,否则无法导入。3 如需修改已有危险源清单，请到系统中修改。4 数据导入后，点击“保存”，即可完成危险源清单导入。">
                    </f:Label>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    <f:Window ID="Window1" Title="审核危险源清单" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="false" CloseAction="HidePostBack"
        Width="900px" Height="600px">
    </f:Window>
    <f:Window ID="Window2" Title="导入危险源清单" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>
    </form>
    <script type="text/javascript">
        // 点击标题栏工具图标 - 查看源代码
        function onToolSourceCodeClick(event) {
            window.open('../Doc/危险源清单导入说明.doc', '_blank');
        }
    </script>
</body>
</html>

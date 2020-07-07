<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WPQIn.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.WPQ.WPQIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导入焊接工艺评定台账</title>
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
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnDownLoad" runat="server" Icon="ApplicationGo" ToolTip="下载模板" OnClick="btnDownLoad_Click">
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
                            EnableColumnLines="true" BoxFlex="1" DataKeyNames="WPQId" AllowCellEditing="true"
                            ClicksToEdit="2" DataIDField="WPQId" AllowSorting="true" SortField="WPQCode"
                            PageSize="12" Height="400px">
                            <Columns>
                                <f:TemplateField ColumnID="tfNumber" Width="55px" HeaderText="序号" HeaderTextAlign="Center"
                                    TextAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:RenderField Width="120px" ColumnID="WPQCode" DataField="WPQCode"
                                    FieldType="String" HeaderText="评定编号" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="MaterialId1" DataField="MaterialId1" FieldType="String"
                                    HeaderText="材质1" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="MaterialId2" DataField="MaterialId2" FieldType="String"
                                    HeaderText="材质2" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="Specifications" DataField="Specifications"
                                    FieldType="String" HeaderText="规格" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="WeldingRod" DataField="WeldingRod" FieldType="String"
                                    HeaderText="焊丝类别" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="WeldingWire" DataField="WeldingWire" FieldType="String"
                                    HeaderText="焊条类别" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="GrooveType" DataField="GrooveType"
                                    FieldType="String" HeaderText="坡口类型" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="90px" ColumnID="WeldingPosition" DataField="WeldingPosition"
                                    FieldType="String" HeaderText="焊接位置" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="WeldingMethodCode" DataField="WeldingMethodCode"
                                    FieldType="String" HeaderText="焊接方法" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="MinImpactDia" DataField="MinImpactDia" FieldType="String"
                                    HeaderText="外径最小值" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="MaxImpactDia" DataField="MaxImpactDia" FieldType="String"
                                    HeaderText="外径最大值" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="170px" ColumnID="MinImpactThickness" DataField="MinImpactThickness"
                                    FieldType="String" HeaderText="冲击时覆盖厚度最小值" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="170px" ColumnID="MaxImpactThickness" DataField="MaxImpactThickness"
                                    FieldType="String" HeaderText="冲击时覆盖厚度最大值" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="170px" ColumnID="NoMinImpactThickness" DataField="NoMinImpactThickness"
                                    FieldType="String" HeaderText="不冲击时覆盖厚度最小值" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="170px" ColumnID="NoMaxImpactThickness" DataField="NoMaxImpactThickness"
                                    FieldType="String" HeaderText="不冲击时覆盖厚度最大值" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="IsHotTreatment" DataField="IsHotProess"
                                    FieldType="String" HeaderText="是否热处理" HeaderTextAlign="Center" TextAlign="Center"
                                    RendererFunction="renderBool">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="JointType" DataField="JointType" FieldType="String"
                                    HeaderText="焊缝形式" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="Motorization" DataField="Motorization" FieldType="String"
                                    HeaderText="机动化程度" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="ProtectiveGas" DataField="ProtectiveGas" FieldType="String"
                                    HeaderText="保护气体" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="Stretching" DataField="Stretching" FieldType="String"
                                    HeaderText="检验项目拉伸" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="Bend" DataField="Bend" FieldType="String"
                                    HeaderText="检验项目弯曲" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="ToAttack" DataField="ToAttack" FieldType="String"
                                    HeaderText="检验项目冲击" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="Others" DataField="Others" FieldType="String"
                                    HeaderText="检验项目其他" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="WPQStandard" DataField="WPQStandard" FieldType="String"
                                    HeaderText="评定标准" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="150px" ColumnID="PreTemperature" DataField="PreTemperature" FieldType="String"
                                    HeaderText="预热温度" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="180px" ColumnID="Remark" DataField="Remark" FieldType="String"
                                    HeaderText="备注" HeaderTextAlign="Center" TextAlign="Center">
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
                        <f:Label ID="lblBottom" runat="server" Text="说明：1、焊接工艺评定台账导入模板中，灰色项为必填项。2、材质1、材质2、焊接方法、评定标准必须与基础信息中对应类型的名称一致,否则无法导入。3 如需修改已有焊接工艺评定台账，请到系统中修改。4 数据导入后，点击“提交”，即可完成焊接工艺评定台账导入。">
                        </f:Label>
                    </Items>
                </f:FormRow>
            </Rows>
        </f:Form>
        <f:Window ID="Window1" Title="审核焊接工艺评定台账" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="false" CloseAction="HidePostBack"
            Width="900px" Height="600px">
        </f:Window>
        <f:Window ID="Window2" Title="导入焊接工艺评定台账" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="false"
            CloseAction="HidePostBack" Width="900px" Height="600px">
        </f:Window>
    </form>
    <script type="text/javascript">
        function renderBool(value) {
            return value == "True" ? '是' : '否';
        }
    </script>
</body>
</html>

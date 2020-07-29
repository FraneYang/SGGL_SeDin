<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HazardListEdit.aspx.cs"
        Inherits="FineUIPro.Web.HSSE.Hazard.HazardListEdit" ValidateRequest="false"%>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编制风险评价清单列表</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panel2" ShowBorder="true" Layout="VBox" ShowHeader="false"
                BodyPadding="5px" IconFont="PlusCircle" AutoScroll="true" RegionPosition="Top">
                <Items>
                    <f:Form runat="server" ID="form2" ShowHeader="false" ShowBorder="true" BodyPadding="5px"
                        RedStarPosition="BeforeText">
                        <Items>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtHazardListCode" runat="server" Label="清单编号" MaxLength="50" LabelAlign="Right"
                                        Readonly="true" Required="true" ShowRedStar="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtWorkArea" runat="server" Label="单位工程" MaxLength="500" ShowRedStar="true"
                                        Required="true" FocusOnPageLoad="true" LabelAlign="Right">
                                    </f:TextBox>
                                    <f:DropDownList ID="drpWorkArea" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpWorkArea_SelectedIndexChanged">
                                    </f:DropDownList>
                                    <f:Label ID="Label1" runat="server" Text="说明：检查区域可选择也可手动编辑。" CssClass="lab" MarginLeft="5px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="辨识日期" ID="txtIdentificationDate"
                                        ShowRedStar="true" Required="true" LabelAlign="Right">
                                    </f:DatePicker>
                                    <f:DropDownList ID="drpControllingPerson" runat="server" Label="控制责任人" ShowRedStar="true"
                                        Required="true" LabelAlign="Right">
                                    </f:DropDownList>
                                    <f:DatePicker ID="txtCompileDate" runat="server" Label="编制日期" LabelAlign="Right"
                                        EnableEdit="false">
                                    </f:DatePicker>
                                    <f:DropDownList ID="drpCompileMan" runat="server" Label="编制人" LabelAlign="Right"
                                        Required="true" ShowRedStar="true">
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="85% 15%">
                                <Items>
                                    <f:TextBox ID="txtWorkStage" runat="server" Label="工作阶段" MaxLength="50" LabelAlign="Right"
                                        Required="true" ShowRedStar="true" Readonly="true">
                                    </f:TextBox>
                                    <f:Button ID="btnWorkStage" runat="server" Text="查找" OnClick="btnWorkStage_OnClick">
                                    </f:Button>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:HiddenField ID="hdWorkStage" runat="server">
                                    </f:HiddenField>
                                </Items>
                            </f:FormRow>
                        </Items>
                    </f:Form>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="危险源辨识与评价清单"
                TitleToolTip="危险源辨识与评价清单" AutoScroll="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:DropDownList ID="drpHelperMethods" runat="server" Label="辅助方法" LabelAlign="Right"
                                EnableMultiSelect="true" EnableCheckBoxSelect="true" AutoSelectFirstItem="false"
                                Hidden="true" AutoPostBack="true" OnSelectedIndexChanged="drpHelperMethods_OnSelectedIndexChanged">
                                <f:ListItem Value="I" Text="Ⅰ" />
                                <f:ListItem Value="Ⅱ" Text="Ⅱ" />
                                <f:ListItem Value="Ⅲ" Text="Ⅲ" />
                                <f:ListItem Value="Ⅳ" Text="Ⅳ" />
                                <f:ListItem Value="Ⅴ" Text="Ⅴ" />
                            </f:DropDownList>
                            <f:DropDownList ID="drpHazardLevel" runat="server" Label="危险级别" LabelAlign="Right"
                                EnableMultiSelect="true" EnableCheckBoxSelect="true" AutoSelectFirstItem="false"
                                AutoPostBack="true" OnSelectedIndexChanged="drpHazardLevel_OnSelectedIndexChanged">
                                <f:ListItem Value="1" Text="1级" />
                                <f:ListItem Value="2" Text="2级" />
                                <f:ListItem Value="3" Text="3级" />
                                <f:ListItem Value="4" Text="4级" />
                                <f:ListItem Value="5" Text="5级" />
                            </f:DropDownList>
                            <f:ToolbarFill runat="server" ID="toolFill">
                            </f:ToolbarFill>
                            <f:Button ID="Button1" runat="server" Text="说明" ToolTip="注: 1:危险级别划分：D值>320,为极其危险(1级)，不能继续作业；D值在160-320之间，为高度危险(2级)，要立即整改；D值在70-160之间，为显著危险(3级)，需要整改；D值在20-70之间，为一般危险(4级)，需要注意；D值<20，为稍有危险(5级)，可以接受   2:辅助方法判断依据：Ⅰ：不符合法律法规及其他要求；Ⅱ：曾发生过事故，仍未采取有效措施；Ⅲ：相关方合理抱怨或要求；Ⅳ：直接观察到的危险；Ⅴ：定量评价LEC法">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                    <f:Grid ID="Grid1" Width="100%" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                        PageSize="1000" runat="server" BoxFlex="1" DataKeyNames="HazardId" AllowCellEditing="true"
                        AllowSorting="true" SortField="HazardCode" ClicksToEdit="1" DataIDField="HazardId"  ForceFit="true"
                        EnableColumnLines="true" EnableTextSelection="True">
                        <Columns>
                            <f:TemplateField Width="120px" HeaderText="工作阶段" HeaderTextAlign="Center" TextAlign="Left" ColumnID="WorkStageName">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# ConvertWorkStage(Eval("WorkStage")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="1px" ColumnID="WorkStage" DataField="WorkStage" 
                                FieldType="String" HeaderText="工作阶段隐藏域"  Hidden="true" HeaderTextAlign="Center">                                        
                            </f:RenderField>
                            <f:RenderField Width="1px" ColumnID="HazardId" DataField="HazardId" 
                                FieldType="String" HeaderText="主键"  Hidden="true" HeaderTextAlign="Center">                                        
                            </f:RenderField>
                            <f:TemplateField Width="110px" HeaderText="危险源类别" HeaderTextAlign="Center" TextAlign="Left" ColumnID="SupHazardListType">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# ConvertSupHazardListTypeId(Eval("HazardListTypeId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="100px" HeaderText="危险源项" HeaderTextAlign="Center" TextAlign="Left" ColumnID="HazardListType">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# ConvertHazardListTypeId(Eval("HazardListTypeId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="140px" HeaderText="危险源代码" HeaderTextAlign="Center" TextAlign="Left" ColumnID="Hazard">
                                <ItemTemplate>
                                    <asp:Label ID="lblHazardId" runat="server" Text='<%# ConvertHazardCode(Eval("HazardId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="1px" ColumnID="HazardListTypeId" DataField="HazardListTypeId" 
                                FieldType="String" HeaderText=""  Hidden="true" HeaderTextAlign="Left">                                        
                            </f:RenderField>
                            <f:RenderField Width="1px" ColumnID="HazardListId" DataField="HazardListId" 
                                FieldType="String" HeaderText=""  Hidden="true" HeaderTextAlign="Left">                                        
                            </f:RenderField>
                            <f:RenderField Width="170px" ColumnID="HazardItems" DataField="HazardItems" FieldType="String"
                                HeaderText="危险因素明细" HeaderTextAlign="Center" TextAlign="Left">
                                <Editor>
                                    <f:TextBox ID="txtHazardItems" runat="server" Text='<%#Bind("HazardItems") %>'>
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="DefectsType" DataField="DefectsType" FieldType="String"
                                HeaderText="缺陷类型" HeaderTextAlign="Center" TextAlign="Left">
                                <Editor>
                                    <f:TextBox ID="txtDefectsType" runat="server" Text='<%#Bind("DefectsType") %>'>
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="160px" ColumnID="MayLeadAccidents" DataField="MayLeadAccidents"
                                FieldType="String" HeaderText="可能导致的事故" HeaderTextAlign="Center" TextAlign="Left">
                                <Editor>
                                    <f:TextBox ID="txtMayLeadAccidents" runat="server" Text='<%#Bind("MayLeadAccidents") %>'>
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="HelperMethod" DataField="HelperMethod" FieldType="String"
                                HeaderText="辅助方法" HeaderTextAlign="Center" TextAlign="Left" EnableColumnEdit="true">
                                <Editor>
                                    <f:DropDownList ID="drpHelperMethod" runat="server" Label="">
                                        <f:ListItem Value="0" Text="-请选择-" />
                                        <f:ListItem Value="I" Text="Ⅰ" />
                                        <f:ListItem Value="Ⅱ" Text="Ⅱ" />
                                        <f:ListItem Value="Ⅲ" Text="Ⅲ" />
                                        <f:ListItem Value="Ⅳ" Text="Ⅳ" />
                                        <f:ListItem Value="Ⅴ" Text="Ⅴ" />
                                    </f:DropDownList>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="50px" ColumnID="L" DataField="HazardJudge_L" FieldType="String"
                                HeaderText="L" HeaderTextAlign="Center" TextAlign="Left" EnableColumnEdit="true">
                                <Editor>
                                    <f:DropDownList ID="drpHazardJudge_L" runat="server" Label="">
                                        <f:ListItem Value="0" Text="-请选择-" />
                                        <f:ListItem Value="10.0" Text="10.0" />
                                        <f:ListItem Value="6.0" Text="6.0" />
                                        <f:ListItem Value="3.0" Text="3.0" />
                                        <f:ListItem Value="1.0" Text="1.0" />
                                        <f:ListItem Value="0.5" Text="0.5" />
                                        <f:ListItem Value="0.2" Text="0.2" />
                                        <f:ListItem Value="0.1" Text="0.1" />
                                    </f:DropDownList>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="50px" ColumnID="E" DataField="HazardJudge_E" FieldType="String"
                                HeaderText="E" HeaderTextAlign="Center" TextAlign="Left" EnableColumnEdit="true">
                                <Editor>
                                    <f:DropDownList ID="drpHazardJudge_E" runat="server" Label="">
                                        <f:ListItem Value="0" Text="-请选择-" />
                                        <f:ListItem Value="10.0" Text="10.0" />
                                        <f:ListItem Value="6.0" Text="6.0" />
                                        <f:ListItem Value="3.0" Text="3.0" />
                                        <f:ListItem Value="2.0" Text="2.0" />
                                        <f:ListItem Value="1.0" Text="1.0" />
                                        <f:ListItem Value="0.5" Text="0.5" />
                                    </f:DropDownList>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="50px" ColumnID="C" DataField="HazardJudge_C" FieldType="String"
                                HeaderText="E" HeaderTextAlign="Center" TextAlign="Left" EnableColumnEdit="true">
                                <Editor>
                                    <f:DropDownList ID="drpHazardJudge_C" runat="server" Label="">
                                        <f:ListItem Value="0" Text="-请选择-" />
                                        <f:ListItem Value="100.0" Text="100.0" />
                                        <f:ListItem Value="40.0" Text="40.0" />
                                        <f:ListItem Value="15.0" Text="15.0" />
                                        <f:ListItem Value="7.0" Text="7.0" />
                                        <f:ListItem Value="3.0" Text="3.0" />
                                        <f:ListItem Value="1.0" Text="1.0" />
                                    </f:DropDownList>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="60px" ColumnID="D" DataField="HazardJudge_D" FieldType="Double"
                                HeaderText="D" HeaderTextAlign="Center" TextAlign="Left">
                                <Editor>
                                    <f:NumberBox ID="nbD" NoDecimal="false" NoNegative="false" MaxValue="10000" MinValue="0"
                                        runat="server" Required="true" Readonly="true">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>                   
                            <f:RenderField Width="100px" ColumnID="G" DataField="HazardLevel" FieldType="Int"
                                HeaderText="危险级别" HeaderTextAlign="Center" TextAlign="Left">
                                <Editor>
                                    <f:NumberBox ID="nbG" NoDecimal="true" NoNegative="false" MaxValue="5" MinValue="0"
                                        runat="server" Required="true" Readonly="true">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="ControlMeasures" DataField="ControlMeasures"
                                FieldType="String" HeaderText="控制措施" HeaderTextAlign="Center" TextAlign="Left">
                                <Editor>
                                    <f:TextBox ID="txtControlMeasures" runat="server" Text='<%#Bind("ControlMeasures") %>'>
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="afteredit" Handler="onGridAfterEdit" />
                             <%--<f:Listener Event="dataload" Handler="onGridDataLoad" />--%>
                        </Listeners>
                    </f:Grid>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panel4" ShowBorder="true" Layout="VBox" ShowHeader="true"
                EnableCollapse="true" Collapsed="true" BodyPadding="5px" IconFont="PlusCircle"
                AutoScroll="true" RegionPosition="Bottom" Title="辨识内容">
                <Items>
                    <f:HtmlEditor runat="server" Label="辨识内容" ID="txtContents" ShowLabel="false" Editor="UMEditor"
                        BasePath="~/res/umeditor/" ToolbarSet="Basic" Height="120" LabelAlign="Right">
                    </f:HtmlEditor>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panel3" ShowBorder="true" Layout="VBox" ShowHeader="false"
                BodyPadding="5px" IconFont="PlusCircle" AutoScroll="true" RegionPosition="Bottom">
                <Items>
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true"
                        BodyPadding="0px">
                        <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
                    </f:ContentPanel>
                </Items>
                <Toolbars>
                    <f:Toolbar ID="Toolbar4" Position="Bottom" runat="server" ToolbarAlign="Right">
                        <Items>
                            <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                                OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                            </f:Button>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:HiddenField ID="hdNewTemplates" runat="server">
                            </f:HiddenField>
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
            </f:Panel>
        </Items>
    </f:Panel>
    <f:Window ID="Window2" Title="危险源辨识与评价模板" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="true"
        Width="1100px" Height="700px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
    <script type="text/javascript">
        function onGridAfterEdit(event, value, params) {
            var me = this, columnId = params.columnId, rowId = params.rowId;
            if (columnId === 'L' || columnId === 'E' || columnId === 'C') {
                var l = me.getCellValue(rowId, 'L');
                var e = me.getCellValue(rowId, 'E');
                var c = me.getCellValue(rowId, 'C');

                if (l != "0" && e != "0" && c != "0") {
                    var d = parseFloat(l) * parseFloat(e) * parseFloat(c);
                    me.updateCellValue(rowId, 'D', d);
                    if (d > 320) {
                        me.updateCellValue(rowId, 'G', '1');
                    }
                    else if (d >= 160 && d <= 320) {
                        me.updateCellValue(rowId, 'G', '2');
                    }
                    else if (d >= 70 && d <= 160) {
                        me.updateCellValue(rowId, 'G', '3');
                    }
                    else if (d >= 20 && d <= 70) {
                        me.updateCellValue(rowId, 'G', '4');
                    }
                    else if (d < 20) {
                        me.updateCellValue(rowId, 'G', '5');
                    }
                }
                else {
                    me.updateCellValue(rowId, 'D', "");
                    me.updateCellValue(rowId, 'G', "");
                }
            }
        }

        function onGridDataLoad(event) {
            this.mergeColumns(['WorkStageName']);
            this.mergeColumns(['SupHazardListType']);
            this.mergeColumns(['HazardListType']);
        }
    </script>
</body>
</html>

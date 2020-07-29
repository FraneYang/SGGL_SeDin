<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnvironmentalRiskListEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Hazard.EnvironmentalRiskListEdit" ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑环境危险源辨识与评价</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">        
        .lab
        {
            font-size: small;
            color: Red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        Layout="VBox" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
        LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtRiskCode" runat="server" Label="危险源编号" Readonly="true" MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtWorkArea" runat="server" Label="单位工程" MaxLength="500" ShowRedStar="true"
                        Required="true" FocusOnPageLoad="true">
                    </f:TextBox>
                    <f:DropDownList ID="drpWorkArea" runat="server"  EnableEdit="true"
                        AutoPostBack="true" OnSelectedIndexChanged="drpWorkArea_SelectedIndexChanged">
                    </f:DropDownList>
                    <f:Label runat="server" Text="说明：单位工程可从下拉框选择也可手动编辑。" CssClass="lab" MarginLeft="5px">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="辨识日期" ID="txtIdentificationDate"
                        ShowRedStar="true" Required="true">
                    </f:DatePicker>
                    <f:DropDownList ID="drpControllingPerson" runat="server" Label="控制责任人" ShowRedStar="true"
                        Required="true" EnableEdit="true">
                    </f:DropDownList>
                    <f:DropDownList ID="drpCompileMan" runat="server" Label="编制人" EnableEdit="true">
                    </f:DropDownList>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="编制日期" ID="txtCompileDate">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" 
                        DataIDField="EnvironmentalRiskItemId" DataKeyNames="EnvironmentalRiskItemId" ForceFit="true"
                        EnableMultiSelect="true" ShowGridHeader="true" EnableColumnLines="true" AllowSorting="true"
                        SortField="SmallTypeName" SortDirection="DESC" EnableRowDoubleClickEvent="true"
                        OnRowDoubleClick="Grid1_RowDoubleClick">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                    <f:Button ID="btnSelect" Icon="ShapeSquareSelect" runat="server" ToolTip="选择" ValidateForms="SimpleForm1"
                                        OnClick="btnSelect_Click">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RowNumberField HeaderText="序号" Width="45px" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:RenderField Width="100px" ColumnID="SmallTypeName" DataField="SmallTypeName" FieldType="String"
                                HeaderText="危险源类型" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="140px" ColumnID="ActivePoint" DataField="ActivePoint" FieldType="String"
                                HeaderText="分项工程/活动点" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="140px" ColumnID="EnvironmentalFactors" DataField="EnvironmentalFactors"
                                FieldType="String" HeaderText="环境因素" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="140px" ColumnID="EnvironmentEffect" DataField="EnvironmentEffect"
                                FieldType="String" HeaderText="环境影响" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:GroupField HeaderText="污染类" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="45px" ColumnID="AValue" DataField="AValue" FieldType="String"
                                        HeaderText="A值" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="45px" ColumnID="BValue" DataField="BValue" FieldType="String"
                                        HeaderText="B值" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="45px" ColumnID="CValue" DataField="CValue" FieldType="String"
                                        HeaderText="C值" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="45px" ColumnID="DValue" DataField="DValue" FieldType="String"
                                        HeaderText="D值" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="45px" ColumnID="EValue" DataField="EValue" FieldType="String"
                                        HeaderText="E值" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="45px" ColumnID="ZValue1" DataField="ZValue1" FieldType="String"
                                        HeaderText="Σ" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField HeaderText="能源资源类" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="45px" ColumnID="FValue" DataField="FValue" FieldType="String"
                                        HeaderText="F值" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="45px" ColumnID="GValue" DataField="GValue" FieldType="String"
                                        HeaderText="G值" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="45px" ColumnID="ZValue2" DataField="ZValue2" FieldType="String"
                                        HeaderText="Σ" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:CheckBoxField Width="60px" RenderAsStaticField="true" TextAlign="Center" DataField="IsImportant"
                                HeaderText="重要" />
                            <f:RenderField Width="100px" ColumnID="ControlMeasures" DataField="ControlMeasures"
                                FieldType="String" HeaderText="安全措施" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="Remark" DataField="Remark" FieldType="String"
                                HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="dataload" Handler="onGridDataLoad" />
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                        </Listeners>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Form ID="Form2" ShowBorder="false" AutoScroll="true" ShowHeader="true" Title="辨识内容"
                        EnableCollapse="true" Collapsed="true" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
                        LabelAlign="Right">
                        <Items>
                            <f:HtmlEditor runat="server" Label="辨识内容" ID="txtContents" ShowLabel="false" Editor="UMEditor"
                                BasePath="~/res/umeditor/" ToolbarSet="Full" Height="160px" LabelAlign="Right">
                            </f:HtmlEditor>
                        </Items>
                    </f:Form>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true"
                        BodyPadding="0px">
                        <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp">
                    </f:Label>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
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
    <f:Window ID="Window1" Title="编辑危险源明细" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="900px" Height="500px">
    </f:Window>
    <f:Window ID="Window2" Title="选择危险源明细" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1200px" Height="520px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Icon="Pencil" runat="server" Text="编辑" >
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Icon="Delete" ConfirmText="确定删除当前数据？" ConfirmTarget="Parent" runat="server" Text="删除">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/javascript">
        var menuID = '<%= Menu1.ClientID %>';

        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        function onGridDataLoad(event) {
            this.mergeColumns(['SmallTypeName']);
        }
    </script>
</body>
</html>

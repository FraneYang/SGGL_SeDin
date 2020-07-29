<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnvironmentalRiskListView.aspx.cs" 
    Inherits="FineUIPro.Web.HSSE.Hazard.EnvironmentalRiskListView" ValidateRequest="false" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看环境危险源辨识与评价</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .labcenter
        {
            text-align: center;
        }        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"  Layout="VBox"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>          
            <f:FormRow>
                <Items>
                     <f:TextBox ID="txtRiskCode" runat="server" Label="危险源编号" Readonly="true" >
                    </f:TextBox>
                     <f:TextBox ID="txtWorkArea" runat="server" Label="单位工程" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>                  
                    <f:TextBox ID="txtIdentificationDate" runat="server" Label="辨识日期" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtControllingPerson" runat="server" Label="控制责任人" Readonly="true">
                    </f:TextBox>
                   <f:TextBox ID="txtCompileMan" runat="server" Label="编制人" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtCompileDate" runat="server" Label="编制日期" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                   <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server"  ForceFit="true"
                        DataIDField="EnvironmentalRiskItemId" DataKeyNames="EnvironmentalRiskItemId"
                        EnableMultiSelect="true" ShowGridHeader="true" EnableColumnLines="true" PageSize="500"
                        AllowSorting="true" SortField="SmallTypeName" SortDirection="DESC" >                        
                        <Columns>
                            <f:RowNumberField HeaderText="序号" Width="45px" HeaderTextAlign="Center" TextAlign="Center" />                           
                           <f:RenderField Width="100px" ColumnID="SmallTypeName" DataField="SmallTypeName" FieldType="String"
                                HeaderText="危险源类型" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="180px" ColumnID="ActivePoint" DataField="ActivePoint" FieldType="String"
                                ExpandUnusedSpace="true" HeaderText="分项工程/活动点" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="EnvironmentalFactors" DataField="EnvironmentalFactors"
                                FieldType="String" HeaderText="环境因素" HeaderTextAlign="Center" TextAlign="Left">
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
                            <f:CheckBoxField Width="60px" RenderAsStaticField="true" TextAlign="Center"  DataField="IsImportant" HeaderText="重要" />
                            <f:RenderField Width="100px" ColumnID="ControlMeasures" DataField="ControlMeasures" FieldType="String"
                                HeaderText="安全措施" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="Remark" DataField="Remark" FieldType="String"
                                HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="dataload" Handler="onGridDataLoad" />
                        </Listeners>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Form ID="Form2" ShowBorder="false" AutoScroll="true" ShowHeader="true" Title="辨识内容" EnableCollapse="true" Collapsed="true"
                            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                        <Items>
                            <f:HtmlEditor runat="server" Label="辨识内容" ID="txtContents" ShowLabel="false" 
                                Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="200px" LabelAlign="Right">
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
        function onGridDataLoad(event) {
            this.mergeColumns(['SmallTypeName']);
        }
    </script>
</body>
</html>

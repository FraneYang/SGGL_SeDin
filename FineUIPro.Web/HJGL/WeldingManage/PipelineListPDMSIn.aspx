<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PipelineListPDMSIn.aspx.cs" Inherits="FineUIPro.Web.HJGL.WeldingManage.PipelineListPDMSIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>导入PDMS数据</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" OnCustomEvent="PageManager1_CustomEvent" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" 
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Toolbars>
                <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:HiddenField ID="hdFileName" runat="server">
                        </f:HiddenField>
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
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="管线信息" Height="350px"
                            EnableCollapse="true" runat="server" BoxFlex="1" DataKeyNames="PipelineId" AllowCellEditing="true"
                            AllowColumnLocking="true" EnableColumnLines="true" ClicksToEdit="2" DataIDField="PipelineId"
                            AllowSorting="true" SortField="PipelineCode" SortDirection="ASC"
                            AllowPaging="true" IsDatabasePaging="true" PageSize="15" 
                            EnableTextSelection="True" >
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true" HeaderText="序号"
                                    Width="60px" HeaderTextAlign="Center" TextAlign="Center" />
                                <f:RenderField Width="130px" ColumnID="PipelineCode" DataField="PipelineCode" SortField="PipelineCode"
                                    FieldType="String" HeaderText="管线号" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="SingleNumber" DataField="SingleNumber" SortField="SingleNumber"
                                    FieldType="String" HeaderText="单线图号" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="MediumName" DataField="MediumName" SortField="MediumName"
                                    FieldType="String" HeaderText="介质名称" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField HeaderText="管道等级" ColumnID="PipingClassCode"
                                    DataField="PipingClassCode" SortField="PipingClassCode" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="100px">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="DetectionRateCode" DataField="DetectionRateCode" SortField="DetectionRateCode"
                                    FieldType="String" HeaderText="探伤比例" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="DetectionTypeStr" DataField="DetectionTypeStr" SortField="DetectionTypeStr"
                                    FieldType="String" HeaderText="探伤类型" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="130px" ColumnID="DesignTemperature" DataField="DesignTemperature" SortField="DesignTemperature"
                                    FieldType="String" HeaderText="设计温度℃" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="130px" ColumnID="DesignPress" DataField="DesignPress" SortField="DesignPress"
                                    FieldType="String" HeaderText="设计压力 MPa(g)" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="130px" ColumnID="TestMediumName" DataField="TestMediumName" SortField="TestMediumName"
                                    FieldType="String" HeaderText="压力试验介质" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="160px" ColumnID="TestPressure" DataField="TestPressure" SortField="TestPressure"
                                    FieldType="String" HeaderText="压力试验压力 MPa(g)" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="130px" ColumnID="PressurePipingClassCode" DataField="PressurePipingClassCode" SortField="PressurePipingClassCode"
                                    FieldType="String" HeaderText="压力管道级别" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="PipeLenth" DataField="PipeLenth" SortField="PipeLenth"
                                    FieldType="Double" HeaderText="管线长度(m)" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="130px" ColumnID="LeakMediumName" DataField="LeakMediumName" SortField="LeakMediumName"
                                    FieldType="String" HeaderText="泄露试验介质" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="160px" ColumnID="LeakPressure" DataField="LeakPressure" SortField="LeakPressure"
                                    FieldType="String" HeaderText="泄露试验压力 MPa(g)" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="PurgeMethodCode" DataField="PurgeMethodCode" SortField="PurgeMethodCode"
                                    FieldType="String" HeaderText="吹洗要求" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="160px" ColumnID="VacuumPressure" DataField="VacuumPressure" SortField="VacuumPressure"
                                    FieldType="String" HeaderText="真空试验压力 KPa(a)" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="PipeMaterialCode" DataField="PipeMaterialCode" SortField="PipeMaterialCode"
                                    FieldType="String" HeaderText="材质" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="PipeRemark" DataField="PipeRemark" SortField="PipeRemark"
                                    FieldType="String" HeaderText="备注" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>

                                <f:RenderField Width="100px" ColumnID="WeldJointCode" DataField="WeldJointCode" SortField="WeldJointCode"
                                    FieldType="String" HeaderText="焊口号" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="Material1Code" DataField="Material1Code" SortField="Material1Code"
                                    FieldType="String" HeaderText="材质1" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="Material2Code" DataField="Material2Code" SortField="Material2Code"
                                    FieldType="String" HeaderText="材质2" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="Dia" DataField="Dia" SortField="Dia"
                                    FieldType="Double" HeaderText="外径" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="Size" DataField="Size" SortField="Size"
                                    FieldType="Double" HeaderText="达因" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="Thickness" DataField="Thickness" SortField="Thickness"
                                    FieldType="Double" HeaderText="壁厚" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="Specification" DataField="Specification" SortField="Specification"
                                    FieldType="String" HeaderText="规格" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="WeldTypeCode" DataField="WeldTypeCode" SortField="WeldTypeCode"
                                    FieldType="String" HeaderText="焊缝类型" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="DetectionTypeCode" DataField="DetectionTypeCode" SortField="DetectionTypeCode"
                                    FieldType="String" HeaderText="检测类型" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="ComponentsCode1" DataField="ComponentsCode1" SortField="ComponentsCode1"
                                    FieldType="String" HeaderText="组件1号" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="ComponentsCode2" DataField="ComponentsCode2" SortField="ComponentsCode2"
                                    FieldType="String" HeaderText="组件2号" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="DesignIsHotProessStr" DataField="DesignIsHotProessStr" SortField="DesignIsHotProessStr"
                                    FieldType="String" HeaderText="是否热处理" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                            </Columns>
                            <PageItems>
                                <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                </f:ToolbarSeparator>
                                <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                                </f:ToolbarText>
                                <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                    <f:ListItem Text="10" Value="10" />
                                    <f:ListItem Text="15" Value="15" />
                                    <f:ListItem Text="20" Value="20" />
                                    <f:ListItem Text="25" Value="25" />
                                    <f:ListItem Text="所有行" Value="10000" />
                                </f:DropDownList>
                            </PageItems>
                        </f:Grid>
                    </Items>
                </f:FormRow>
            </Rows>
        </f:Form>
    </form>
</body>
</html>

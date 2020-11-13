<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PipelineListIn.aspx.cs" Inherits="FineUIPro.Web.HJGL.WeldingManage.PipelineListIn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导入管线</title>
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
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="管线信息"
                            EnableCollapse="true" runat="server" BoxFlex="1" DataKeyNames="PipelineId" AllowCellEditing="true"
                            AllowColumnLocking="true" EnableColumnLines="true" ClicksToEdit="2" DataIDField="PipelineId"
                            AllowSorting="true" SortField="PipelineCode" SortDirection="ASC"
                            AllowPaging="true" IsDatabasePaging="true" PageSize="15" 
                            EnableTextSelection="True" >
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true" HeaderText="序号"
                                    Width="60px" HeaderTextAlign="Center" TextAlign="Center" />
                                <f:WindowField ColumnID="PipelineCode" HeaderTextAlign="Center" TextAlign="Left"
                                    Width="220px" WindowID="Window1" HeaderText="管线号" DataTextField="PipelineCode"
                                    DataIFrameUrlFields="PipelineId" DataIFrameUrlFormatString="PipelineEdit.aspx?PipelineId={0}"
                                    Title="管线号" DataToolTipField="PipelineCode" SortField="PipelineCode"
                                    Locked="true">
                                </f:WindowField>
                                <f:RenderField Width="90px" ColumnID="TotalDin" DataField="TotalDin" FieldType="Double"
                                    HeaderText="总达因数" HeaderTextAlign="Center" TextAlign="Right">
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="JointCount" DataField="JointCount" FieldType="Int"
                                    HeaderText="总焊口量" HeaderTextAlign="Center"
                                    TextAlign="Right">
                                </f:RenderField>
                                <f:RenderField Width="200px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                                    FieldType="String" HeaderText="单位名称" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:TemplateField Width="130px" HeaderText="无损检测类型" HeaderTextAlign="Center"
                                    TextAlign="Center" SortField="DetectionType">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# ConvertDetectionType(Eval("DetectionType")) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:RenderField Width="100px" ColumnID="DetectionRateCode" DataField="DetectionRateCode" SortField="DetectionRateCode"
                                    FieldType="String" HeaderText="探伤比例" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>

                                <f:RenderField Width="100px" ColumnID="MediumCode" DataField="MediumCode" SortField="MediumCode"
                                    FieldType="String" HeaderText="介质代号" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField HeaderText="管道等级" ColumnID="PipingClassCode"
                                    DataField="PipingClassCode" SortField="PipingClassCode" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="100px">
                                </f:RenderField>

                                <f:RenderField Width="100px" ColumnID="SingleNumber" DataField="SingleNumber" SortField="SingleNumber"
                                    FieldType="String" HeaderText="单线图号" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="130px" ColumnID="DesignPress" DataField="DesignPress" SortField="DesignPress"
                                    FieldType="Double" HeaderText="设计压力 MPa(g)" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="130px" ColumnID="DesignTemperature" DataField="DesignTemperature" SortField="DesignTemperature"
                                    FieldType="Double" HeaderText="设计温度℃" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="130px" ColumnID="TestMediumCode" DataField="TestMediumCode" SortField="TestMediumCode"
                                    FieldType="String" HeaderText="压力试验介质" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="160px" ColumnID="TestPressure" DataField="TestPressure" SortField="TestPressure"
                                    FieldType="Double" HeaderText="压力试验压力 MPa(g)" HeaderTextAlign="Center"
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
                                    FieldType="Double" HeaderText="泄露试验压力 MPa(g)" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="PCMediumName" DataField="PCMediumName" SortField="PCMediumName"
                                    FieldType="String" HeaderText="吹洗要求" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="160px" ColumnID="VacuumPressure" DataField="VacuumPressure" SortField="VacuumPressure"
                                    FieldType="Double" HeaderText="真空试验压力 KPa(a)" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="Remark" DataField="Remark" SortField="Remark"
                                    FieldType="String" HeaderText="备注" HeaderTextAlign="Center"
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


<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IsoCompreInfo.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.WeldingReport.IsoCompreInfo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>管线综合信息</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="焊口综合信息"
                EnableCollapse="true" runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="PipelineId"
                AllowCellEditing="true" ClicksToEdit="2" DataIDField="PipelineId" AllowSorting="true"
                SortField="UnitWorkCode,PipelineCode" SortDirection="DESC" OnSort="Grid1_Sort"
                AllowPaging="true" IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:DropDownList ID="drpUnitWork" runat="server" Label="区域"
                                LabelAlign="Right" Width="280px">
                            </f:DropDownList>
                            <f:TextBox ID="txtPipelineCode" runat="server" Label="管线号"
                                LabelAlign="Right" Width="280px">
                            </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="BtnAnalyse" Text="统计" Icon="ChartPie"
                                runat="server" OnClick="BtnAnalyse_Click">
                            </f:Button>
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" Text="导出"
                                Icon="TableGo" EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:TemplateField ColumnID="tfNumber" HeaderText="序号"
                        Width="60px" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="labNumber" runat="server" Text=' <%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1%>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField HeaderText="单位名称" ColumnID="UnitName" DataField="UnitName"
                        SortField="UnitName" FieldType="String" HeaderTextAlign="Center" Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="单位工程" ColumnID="UnitWorkCode" DataField="UnitWorkCode"
                        SortField="UnitWorkCode" FieldType="String" HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="管线号" ColumnID="PipelineCode"
                        DataField="PipelineCode" SortField="PipelineCode" FieldType="String" HeaderTextAlign="Center"
                        Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="总达因数" ColumnID="ISO_TotalDin"
                        DataField="ISO_TotalDin" SortField="ISO_TotalDin" FieldType="String" HeaderTextAlign="Center"
                        Width="120px">
                    </f:RenderField>
                    <f:RenderField HeaderText="焊口数" ColumnID="jot_count" DataField="jot_count"
                        SortField="jot_count" FieldType="String" HeaderTextAlign="Center" Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="固定口数" ColumnID="GDCount" DataField="GDCount"
                        SortField="GDCount" FieldType="String" HeaderTextAlign="Center" Width="120px">
                    </f:RenderField>
                    <f:RenderField HeaderText="活动口数" ColumnID="HDCount" DataField="HDCount"
                        SortField="HDCount" FieldType="String" HeaderTextAlign="Center" Width="120px">
                    </f:RenderField>
                    <f:RenderField HeaderText="管道等级" ColumnID="PipingClassCode" DataField="PipingClassCode"
                        SortField="PipingClassCode" FieldType="String" HeaderTextAlign="Center" Width="120px">
                    </f:RenderField>
                    <f:RenderField HeaderText="介质" ColumnID="MediumName" DataField="MediumName"
                        SortField="MediumName" FieldType="String" HeaderTextAlign="Center" Width="120px">
                    </f:RenderField>
                    <f:RenderField HeaderText="探伤比例" ColumnID="DetectionRateValue"
                        DataField="DetectionRateValue" SortField="DetectionRateValue" FieldType="String"
                        HeaderTextAlign="Center" Width="120px">
                    </f:RenderField>
                    
                     <f:TemplateField ColumnID="DetectionType" HeaderText="探伤类型" Width="180px"
                        HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lbDetectionType" runat="server" Text='<%# ConvertDetectionType(Eval("DetectionType")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>

                    <f:RenderField HeaderText="试验介质" ColumnID="TestMedium"
                        DataField="TestMedium" SortField="TestMedium" FieldType="String" HeaderTextAlign="Center"
                        Width="120px">
                    </f:RenderField>
                    <f:RenderField HeaderText="试验压力" ColumnID="TestPressure"
                        DataField="TestPressure" SortField="TestPressure" FieldType="String" HeaderTextAlign="Center"
                        Width="150px">
                    </f:RenderField>
                    
                    <f:RenderField HeaderText="单线图号" ColumnID="SingleNumber"
                        DataField="SingleNumber" SortField="SingleNumber" FieldType="String" HeaderTextAlign="Center"
                        Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="需热处理焊口数" ColumnID="is_proess" DataField="is_proess"
                        FieldType="String" HeaderTextAlign="Center" Width="180px">
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
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    </form>
</body>
</html>

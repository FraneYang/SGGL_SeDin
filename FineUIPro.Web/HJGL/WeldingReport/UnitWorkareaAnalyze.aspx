<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitWorkareaAnalyze.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.WeldingReport.UnitWorkareaAnalyze" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>单位工区进度分析</title>
     <style>
        .f-grid-row-summary .f-grid-cell-inner {
            font-weight: bold;
            color: red;
        }
    </style> 
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="单位工区进度分析"
                EnableCollapse="true" runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="baw_areano"
                AllowCellEditing="true" ClicksToEdit="2" DataIDField="baw_areano" AllowSorting="true"
                SortField="bsu_unitcode,InstallationName,baw_areano" SortDirection="DESC" OnSort="Grid1_Sort"
                AllowPaging="true" IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                EnableTextSelection="True" EnableSummary="true" SummaryPosition="Flow">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:DropDownList ID="drpUnitId" runat="server" Label="单位名称"
                                LabelAlign="Right" Width="280px" AutoPostBack="true" >
                            </f:DropDownList>
                            <f:DropDownList ID="drpUnitWork" runat="server" Label="单位工程名称"
                                LabelAlign="Right" Width="280px" AutoPostBack="true">
                            </f:DropDownList>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnSelectColumn" Text="选择显示列" Icon="ShapesManySelect"
                                runat="server" OnClick="btnSelectColumn_Click">
                            </f:Button>
                            <f:Button ID="BtnAnalyse" Text="统计" Icon="ChartPie"
                                runat="server" OnClick="BtnAnalyse_Click">
                            </f:Button>
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" Text="导出"
                                Icon="TableGo" EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:DropDownList ID="drpSteelType" runat="server" Label="钢材类型"
                                LabelAlign="Right" Width="280px">
                            </f:DropDownList>
                            <f:DatePicker runat="server" Label="日期" ID="txtStarTime" LabelAlign="Right"
                                LabelWidth="100px" Width="220px">
                            </f:DatePicker>
                            <f:Label ID="Label1" runat="server" Text="至">
                            </f:Label>
                            <f:DatePicker runat="server" ID="txtEndTime" LabelAlign="Right" LabelWidth="80px"
                                Width="110px">
                            </f:DatePicker>
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
                    <f:RenderField HeaderText="单位名称" ColumnID="bsu_unitname"
                        DataField="bsu_unitname" SortField="bsu_unitname" FieldType="String" HeaderTextAlign="Center"
                        Width="200px">
                    </f:RenderField>
                    <f:RenderField HeaderText="单位工程名称" ColumnID="UnitWorkName"
                        DataField="UnitWorkName" SortField="UnitWorkName" FieldType="String"
                        HeaderTextAlign="Center" Width="90px">
                    </f:RenderField>
                    <f:RenderField HeaderText="总焊口" ColumnID="total_jot" DataField="total_jot"
                        SortField="total_jot" FieldType="String" HeaderTextAlign="Center" Width="80px">
                    </f:RenderField>
                    <f:RenderField HeaderText="预制总焊口" ColumnID="total_sjot"
                        DataField="total_sjot" SortField="total_sjot" FieldType="String" HeaderTextAlign="Center"
                        Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="安装总焊口" ColumnID="total_fjot"
                        DataField="total_fjot" SortField="total_fjot" FieldType="String" HeaderTextAlign="Center"
                        Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="切除焊口" ColumnID="cut_total_jot"
                        DataField="cut_total_jot" SortField="cut_total_jot" FieldType="String" HeaderTextAlign="Center"
                        TextAlign="Right" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="总达因数" ColumnID="total_din" DataField="total_din"
                        SortField="total_din" FieldType="String" HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="预制总达因" ColumnID="total_Sdin"
                        DataField="total_Sdin" SortField="total_Sdin" FieldType="String" HeaderTextAlign="Center"
                        Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="安装总达因" ColumnID="total_Fdin"
                        DataField="total_Fdin" SortField="total_Fdin" FieldType="String" HeaderTextAlign="Center"
                        Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期完成焊口数" ColumnID="finished_total_jot_bq"
                        DataField="finished_total_jot_bq" SortField="finished_total_jot_bq" FieldType="String"
                        HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期完成预制焊口数" ColumnID="finished_total_sjot_bq"
                        DataField="finished_total_sjot_bq" SortField="finished_total_sjot_bq" FieldType="String"
                        HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期完成安装焊口数" ColumnID="finished_total_fjot_bq"
                        DataField="finished_total_fjot_bq" SortField="current_point_total_film" FieldType="String"
                        HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期完成比例" ColumnID="finisedrate_bq"
                        DataField="finisedrate_bq" SortField="finisedrate_bq" FieldType="String" HeaderTextAlign="Center"
                        Width="140px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期预制完成比例" ColumnID="finisedrate_s_bq"
                        DataField="finisedrate_s_bq" SortField="finisedrate_s_bq" FieldType="String"
                        HeaderTextAlign="Center" Width="140px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期安装完成比例" ColumnID="finisedrate_f_bq"
                        DataField="finisedrate_f_bq" SortField="finisedrate_f_bq" FieldType="String"
                        HeaderTextAlign="Center" Width="120px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期完成达因" ColumnID="finished_total_din_bq"
                        DataField="finished_total_din_bq" SortField="finished_total_din_bq" FieldType="String"
                        HeaderTextAlign="Center" Width="140px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期完成预制达因" ColumnID="finished_total_Sdin_bq"
                        DataField="finished_total_Sdin_bq" SortField="finished_total_Sdin_bq" FieldType="String"
                        HeaderTextAlign="Center" Width="140px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期完成安装达因" ColumnID="finished_total_Fdin_bq"
                        DataField="finished_total_Fdin_bq" SortField="finished_total_Fdin_bq" FieldType="String"
                        HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期完成达因比例" ColumnID="finisedrate_din_bq"
                        DataField="finisedrate_din_bq" SortField="finisedrate_din_bq" FieldType="String"
                        HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期完成预制达因比例" ColumnID="finisedrate_din_s_bq"
                        DataField="finisedrate_din_s_bq" SortField="finisedrate_din_s_bq" FieldType="String"
                        HeaderTextAlign="Center" Width="80px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期完成安装达因比例" ColumnID="finisedrate_din_f_bq"
                        DataField="finisedrate_din_f_bq" SortField="finisedrate_din_f_bq" FieldType="String"
                        HeaderTextAlign="Center" Width="80px">
                    </f:RenderField>
                    <f:RenderField HeaderText="完成焊口" ColumnID="finished_total_jot"
                        DataField="finished_total_jot" SortField="finished_total_jot" FieldType="String"
                        HeaderTextAlign="Center" Width="90px">
                    </f:RenderField>
                    <f:RenderField HeaderText="完成预制焊口" ColumnID="finished_total_sjot"
                        DataField="finished_total_sjot" SortField="finished_total_sjot" FieldType="String"
                        HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="完成安装焊口" ColumnID="finished_total_fjot"
                        DataField="finished_total_fjot" SortField="finished_total_fjot" FieldType="String"
                        HeaderTextAlign="Center" Width="120px">
                    </f:RenderField>
                    <f:RenderField HeaderText="完成比例" ColumnID="finisedrate"
                        DataField="finisedrate" SortField="finisedrate" FieldType="String" HeaderTextAlign="Center"
                        Width="120px">
                    </f:RenderField>
                    <f:RenderField HeaderText="安装完成比例" ColumnID="finisedrate_f"
                        DataField="finisedrate_f" SortField="finisedrate_f" FieldType="String" HeaderTextAlign="Center"
                        Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="预制完成比例" ColumnID="finisedrate_s"
                        DataField="finisedrate_s" SortField="finisedrate_s" FieldType="String" HeaderTextAlign="Center"
                        Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="完成达因" ColumnID="finished_total_din"
                        DataField="finished_total_din" SortField="finished_total_din" FieldType="String"
                        HeaderTextAlign="Center" Width="120px">
                    </f:RenderField>
                    <f:RenderField HeaderText="完成预制达因" ColumnID="finished_total_sdin"
                        DataField="finished_total_sdin" SortField="finished_total_sdin" FieldType="String"
                        HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="完成安装达因" ColumnID="finished_total_Fdin"
                        DataField="finished_total_Fdin" SortField="finished_total_Fdin" FieldType="String"
                        HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="完成达因比例" ColumnID="finisedrate_din"
                        DataField="finisedrate_din" SortField="finisedrate_din" FieldType="String" HeaderTextAlign="Center"
                        Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="完成预制达因比例" ColumnID="finisedrate_din_s"
                        DataField="finisedrate_din_s" SortField="finisedrate_din_s" FieldType="String"
                        HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="完成安装达因比例" ColumnID="finisedrate_din_f"
                        DataField="finisedrate_din_f" SortField="finisedrate_din_f" FieldType="String"
                        HeaderTextAlign="Center" Width="80px">
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
       <f:Window ID="Window1" Title="选择显示列" Hidden="true"
        EnableIFrame="true" EnableMaximize="false" Target="Top" EnableResize="false" runat="server"
        IsModal="true" Width="900px" Height="560px" OnClose="Window1_Close">
    </f:Window>
    </form>
</body>
</html>

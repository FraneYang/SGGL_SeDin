<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitWorkAreaQuality.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.WeldingReport.UnitWorkAreaQuality" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>单位工区质量分析</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="单位工区质量分析"
                EnableCollapse="true" runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="baw_areano"
                AllowCellEditing="true" ClicksToEdit="2" DataIDField="baw_areano" AllowSorting="true"
                SortField="bsu_unitcode,devicename,baw_areano" SortDirection="DESC" OnSort="Grid1_Sort"
                AllowPaging="true" IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:DropDownList ID="drpUnitId" runat="server" Label="单位名称"
                                LabelAlign="Right" Width="280px" AutoPostBack="true" >
                            </f:DropDownList>
                            <f:DropDownList ID="drpUnitWork" runat="server" Label="单位工程"
                                LabelAlign="Right" Width="280px" AutoPostBack="true" >
                            </f:DropDownList>
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
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
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
                    <f:RenderField HeaderText="单位工程名称" ColumnID="devicename"
                        DataField="devicename" SortField="devicename" FieldType="String" HeaderTextAlign="Center"
                        Width="90px">
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
                    <f:RenderField HeaderText="本期完成焊口数" ColumnID="finished_total_jot"
                        DataField="finished_total_jot" SortField="finished_total_jot" FieldType="String"
                        HeaderTextAlign="Center" TextAlign="Right" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期完成预制焊口数" ColumnID="finished_total_sjot"
                        DataField="finished_total_sjot" SortField="finished_total_sjot" FieldType="String"
                        HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期完成安装焊口数" ColumnID="finished_total_fjot"
                        DataField="finished_total_fjot" SortField="finished_total_fjot" FieldType="String"
                        HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期RT总拍片数" ColumnID="current_total_film"
                        DataField="current_total_film" SortField="current_total_film" FieldType="String"
                        HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期RT不合格片数" ColumnID="current_No_pass_film"
                        DataField="current_No_pass_film" SortField="current_No_pass_film" FieldType="String"
                        HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期RT不合格率(片数)" ColumnID="current_No_pass_rate"
                        DataField="current_No_pass_rate" SortField="current_No_pass_rate" FieldType="String"
                        HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期RT总焊口数" ColumnID="current_Total_JointNum"
                        DataField="current_Total_JointNum" SortField="current_Total_JointNum" FieldType="String"
                        HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期RT安装口不合格焊口数" ColumnID="current_No_Pass_JointNum"
                        DataField="current_No_Pass_JointNum" SortField="current_No_Pass_JointNum" FieldType="String"
                        HeaderTextAlign="Center" Width="140px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期RT预制口不合格率" ColumnID="current_No_Joint_rate"
                        DataField="current_No_Joint_rate" SortField="current_No_Joint_rate" FieldType="String"
                        HeaderTextAlign="Center" Width="140px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期RT安装口焊口总数" ColumnID="f_finished_total_jot"
                        DataField="f_finished_total_jot" SortField="f_finished_total_jot" FieldType="String"
                        HeaderTextAlign="Center" Width="120px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期RT预制口焊口总数" ColumnID="s_finished_total_jot"
                        DataField="s_finished_total_jot" SortField="s_finished_total_jot" FieldType="String"
                        HeaderTextAlign="Center" Width="140px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期RT安装口不合格焊口数" ColumnID="f_current_No_Pass_JointNum"
                        DataField="f_current_No_Pass_JointNum" SortField="f_current_No_Pass_JointNum"
                        FieldType="String" HeaderTextAlign="Center" Width="140px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期RT预制口不合格焊口数" ColumnID="s_current_No_Pass_JointNum"
                        DataField="s_current_No_Pass_JointNum" SortField="s_current_No_Pass_JointNum"
                        FieldType="String" HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期RT安装口不合格率" ColumnID="f_current_No_Joint_rate"
                        DataField="f_current_No_Joint_rate" SortField="f_current_No_Joint_rate" FieldType="String"
                        HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期RT预制口不合格率" ColumnID="s_current_No_Joint_rate"
                        DataField="s_current_No_Joint_rate" SortField="s_current_No_Joint_rate" FieldType="String"
                        HeaderTextAlign="Center" Width="80px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期RT安装口拍片总数" ColumnID="current_f_total_film"
                        DataField="current_f_total_film" SortField="current_f_total_film" FieldType="String"
                        HeaderTextAlign="Center" Width="80px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期RT预制口拍片总数" ColumnID="current_s_total_film"
                        DataField="current_s_total_film" SortField="current_s_total_film" FieldType="String"
                        HeaderTextAlign="Center" Width="90px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期RT安装口不合格片数" ColumnID="current_f_No_pass_film"
                        DataField="current_f_No_pass_film" SortField="current_f_No_pass_film" FieldType="String"
                        HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期RT预制口不合格片数" ColumnID="current_s_No_pass_film"
                        DataField="current_s_No_pass_film" SortField="current_s_No_pass_film" FieldType="String"
                        HeaderTextAlign="Center" Width="120px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期总MT焊口数" ColumnID="current_MT_JointNum"
                        DataField="current_MT_JointNum" SortField="current_MT_JointNum" FieldType="String"
                        HeaderTextAlign="Center" Width="120px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期总PT焊口数" ColumnID="current_PT_JointNum"
                        DataField="current_PT_JointNum" SortField="current_PT_JointNum" FieldType="String"
                        HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期总UT焊口数" ColumnID="current_UT_JointNum"
                        DataField="current_UT_JointNum" SortField="current_UT_JointNum" FieldType="String"
                        HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期总PMI焊口数" ColumnID="current_PMI_JointNum"
                        DataField="current_PMI_JointNum" SortField="current_PMI_JointNum" FieldType="String"
                        HeaderTextAlign="Center" Width="120px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期总PWHT焊口数" ColumnID="current_PWHT_JointNum"
                        DataField="current_PWHT_JointNum" SortField="current_PWHT_JointNum" FieldType="String"
                        HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期总HT焊口数" ColumnID="current_HT_JointNum"
                        DataField="current_HT_JointNum" SortField="current_HT_JointNum" FieldType="String"
                        HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="扩透总焊口数" ColumnID="extend_count_total"
                        DataField="extend_count_total" SortField="extend_count_total" FieldType="String"
                        HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="扩透返修口数" ColumnID="repair_count_total"
                        DataField="repair_count_total" SortField="repair_count_total" FieldType="String"
                        HeaderTextAlign="Center" Width="100px">
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

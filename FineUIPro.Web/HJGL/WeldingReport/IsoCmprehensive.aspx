<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IsoCmprehensive.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.WeldingReport.IsoCmprehensive" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>管线综合分析</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="管线综合分析"
                EnableCollapse="true" runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="PipelineCode"
                AllowCellEditing="true" ClicksToEdit="2" DataIDField="PipelineId" AllowSorting="true"
                SortField="UnitCode,WorkAreaCode,PipelineCode" SortDirection="DESC" OnSort="Grid1_Sort"
                AllowPaging="true" IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:DropDownList ID="drpUnitId" runat="server" Label="单位名称"
                                LabelAlign="Right" Width="280px" AutoPostBack="true" OnSelectedIndexChanged="drpUnitId_SelectedIndexChanged">
                            </f:DropDownList>
                            <f:DropDownList ID="drpWorkAreaId" runat="server" Label="区域"
                                LabelAlign="Right" Width="280px">
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
                            <f:TextBox ID="txtPipelineCode" runat="server" Label="管线号"
                                LabelAlign="Right" Width="280px">
                            </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill2" runat="server">
                            </f:ToolbarFill>
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
                    <f:RenderField HeaderText="单位代码" ColumnID="UnitCode"
                        DataField="UnitCode" SortField="UnitCode" FieldType="String" HeaderTextAlign="Center"
                        Width="200px">
                    </f:RenderField>
                    <f:RenderField HeaderText="单位名称" ColumnID="UnitName" DataField="UnitName"
                        SortField="UnitName" FieldType="String" HeaderTextAlign="Center" Width="150px">
                    </f:RenderField>
                    <f:RenderField HeaderText="区域" ColumnID="WorkAreaCode" DataField="WorkAreaCode"
                        SortField="WorkAreaCode" FieldType="String" HeaderTextAlign="Center" Width="150px">
                    </f:RenderField>
                    <f:RenderField HeaderText="管线号" ColumnID="PipelineCode"
                        DataField="PipelineCode" SortField="PipelineCode" FieldType="String" HeaderTextAlign="Center"
                        Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="最近焊期" ColumnID="maxdate" DataField="maxdate"
                        SortField="maxdate" FieldType="Date" Renderer="Date" HeaderTextAlign="Center" Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="总焊口" ColumnID="total_jot" DataField="total_jot"
                        SortField="total_jot" FieldType="String" HeaderTextAlign="Center" Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="预制总焊口" ColumnID="total_sjot"
                        DataField="total_sjot" SortField="total_sjot" FieldType="String" HeaderTextAlign="Center"
                        Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="安装总焊口" ColumnID="total_fjot"
                        DataField="total_fjot" SortField="total_fjot" FieldType="String" HeaderTextAlign="Center"
                        Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="完成焊口" ColumnID="finished_total_jot"
                        DataField="finished_total_jot" SortField="finished_total_jot" FieldType="String"
                        HeaderTextAlign="Center" Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="完成预制焊口" ColumnID="finished_total_sjot"
                        DataField="finished_total_sjot" SortField="finished_total_sjot" FieldType="String"
                        HeaderTextAlign="Center" Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="完成安装焊口" ColumnID="finished_total_fjot"
                        DataField="finished_total_fjot" SortField="finished_total_fjot" FieldType="String"
                        HeaderTextAlign="Center" Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="切除焊口" ColumnID="cut_total_jot"
                        DataField="cut_total_jot" SortField="cut_total_jot" FieldType="String" HeaderTextAlign="Center"
                        Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="完成比例" ColumnID="finisedrate"
                        DataField="finisedrate" SortField="finisedrate" FieldType="String" HeaderTextAlign="Center"
                        Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="预制完成比例" ColumnID="finisedrate_s"
                        DataField="finisedrate_s" SortField="finisedrate_s" FieldType="String" HeaderTextAlign="Center"
                        Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="安装完成比例" ColumnID="finisedrate_f"
                        DataField="finisedrate_f" SortField="finisedrate_f" FieldType="String" HeaderTextAlign="Center"
                        Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="总达因数" ColumnID="total_din" DataField="total_din"
                        SortField="total_din" FieldType="String" HeaderTextAlign="Center" Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="预制总达因" ColumnID="total_Sdin"
                        DataField="total_Sdin" SortField="total_Sdin" FieldType="String" HeaderTextAlign="Center"
                        Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="安装总达因" ColumnID="total_Fdin"
                        DataField="total_Fdin" SortField="total_Fdin" FieldType="String" HeaderTextAlign="Center"
                        Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="完成达因" ColumnID="finished_total_din"
                        DataField="finished_total_din" SortField="finished_total_din" FieldType="String"
                        HeaderTextAlign="Center" Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="完成预制达因" ColumnID="finished_total_Sdin"
                        DataField="finished_total_Sdin" SortField="finished_total_Sdin" FieldType="String"
                        HeaderTextAlign="Center" Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="完成安装达因" ColumnID="finished_total_Fdin"
                        DataField="finished_total_Fdin" SortField="finished_total_Fdin" FieldType="String"
                        HeaderTextAlign="Center" Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="完成比例" ColumnID="finisedrate_din"
                        DataField="finisedrate_din" SortField="finisedrate_din" FieldType="String" HeaderTextAlign="Center"
                        Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="预制完成比例" ColumnID="finisedrate_din_s"
                        DataField="finisedrate_din_s" SortField="finisedrate_din_s" FieldType="String"
                        HeaderTextAlign="Center" Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="安装完成比例" ColumnID="finisedrate_din_f"
                        DataField="finisedrate_din_f" SortField="finisedrate_din_f" FieldType="String"
                        HeaderTextAlign="Center" Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="总拍片数" ColumnID="total_film"
                        DataField="total_film" SortField="total_film" FieldType="String" HeaderTextAlign="Center"
                        Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="合格数" ColumnID="pass_film" DataField="pass_film"
                        SortField="pass_film" FieldType="String" HeaderTextAlign="Center" Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="合格率" ColumnID="passreate" DataField="passreate"
                        SortField="passreate" FieldType="String" HeaderTextAlign="Center" Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="扩透总片数" ColumnID="ext_total_film"
                        DataField="ext_total_film" SortField="ext_total_film" FieldType="String" HeaderTextAlign="Center"
                        Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="扩透合格片数" ColumnID="ext_pass_film"
                        DataField="ext_pass_film" SortField="ext_pass_film" FieldType="String" HeaderTextAlign="Center"
                        Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="扩透合格率" ColumnID="ext_passreate"
                        DataField="ext_passreate" SortField="ext_passreate" FieldType="String" HeaderTextAlign="Center"
                        Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="点口总片数" ColumnID="point_total_film"
                        DataField="point_total_film" SortField="point_total_film" FieldType="String"
                        HeaderTextAlign="Center" Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="点口合格片数" ColumnID="point_pass_film"
                        DataField="point_pass_film" SortField="point_pass_film" FieldType="String" HeaderTextAlign="Center"
                        Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="点口合格率" ColumnID="point_passreate"
                        DataField="point_passreate" SortField="point_passreate" FieldType="String" HeaderTextAlign="Center"
                        Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="切除总片数" ColumnID="cut_total_film"
                        DataField="cut_total_film" SortField="cut_total_film" FieldType="String" HeaderTextAlign="Center"
                        Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="切除合格片数" ColumnID="cut_pass_film"
                        DataField="cut_pass_film" SortField="cut_pass_film" FieldType="String" HeaderTextAlign="Center"
                        Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="扩透数" ColumnID="ext_jot" DataField="ext_jot"
                        SortField="ext_jot" FieldType="String" HeaderTextAlign="Center" Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="点口数" ColumnID="point_jot" DataField="point_jot"
                        SortField="point_jot" FieldType="String" HeaderTextAlign="Center" Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="委托数" ColumnID="trust_total_jot"
                        DataField="trust_total_jot" SortField="trust_total_jot" FieldType="String" HeaderTextAlign="Center"
                        Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="已探口数" ColumnID="check_total_jot"
                        DataField="check_total_jot" SortField="check_total_jot" FieldType="String" HeaderTextAlign="Center"
                        Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期RT返口数" ColumnID="total_repairjot"
                        DataField="total_repairjot" SortField="total_repairjot" FieldType="String" HeaderTextAlign="Center"
                        Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="委托比例" ColumnID="trustrate" DataField="trustrate"
                        SortField="trustrate" FieldType="String" HeaderTextAlign="Center" Width="180px">
                    </f:RenderField>
                    <f:RenderField HeaderText="已探比例" ColumnID="checkrate" DataField="checkrate"
                        SortField="checkrate" FieldType="String" HeaderTextAlign="Center" Width="180px">
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

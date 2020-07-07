<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WelderPerformance.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.WeldingReport.WelderPerformance" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊工业绩分析</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="焊工业绩分析"
                EnableCollapse="true" runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="WelderCode"
                AllowCellEditing="true" ClicksToEdit="2" DataIDField="WelderCode" AllowSorting="true"
                SortField="WelderCode" SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:DropDownList ID="drpUnitId" runat="server" Label="单位名称"
                                LabelAlign="Right" Width="280px" AutoPostBack="true" OnSelectedIndexChanged="drpUnitId_SelectedIndexChanged">
                            </f:DropDownList>
                            <f:DropDownList ID="drpUnitWork" runat="server" Label="单位工程"
                                LabelAlign="Right" Width="280px" AutoPostBack="true" OnSelectedIndexChanged="drpUnitId_SelectedIndexChanged">
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
                            <f:DropDownList ID="drpMaterialId" runat="server" Label="材质"
                                LabelAlign="Right" Width="280px">
                            </f:DropDownList>
                            <f:DropDownList ID="drpWelderId" runat="server" Label="焊工号"
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
                    <f:RenderField HeaderText="焊工号" ColumnID="WelderCode"
                        DataField="WelderCode" SortField="WelderCode" FieldType="String" HeaderTextAlign="Center"
                        Width="200px">
                    </f:RenderField>
                    <f:RenderField HeaderText="焊工姓名" ColumnID="PersonName"
                        DataField="PersonName" SortField="PersonName" FieldType="String" HeaderTextAlign="Center"
                        Width="90px">
                    </f:RenderField>
                    <f:RenderField HeaderText="性别" ColumnID="Sex" DataField="Sex"
                        SortField="Sex" FieldType="String" HeaderTextAlign="Center" Width="90px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期总达因值" ColumnID="nowtotal_din"
                        DataField="nowtotal_din" SortField="nowtotal_din" FieldType="String" HeaderTextAlign="Center"
                        Width="80px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期总焊口" ColumnID="nowtotal_jot"
                        DataField="nowtotal_jot" SortField="nowtotal_jot" FieldType="String" HeaderTextAlign="Center"
                        Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期RT总焊口数" ColumnID="rt_total_Jot"
                        DataField="rt_total_Jot" SortField="rt_total_Jot" FieldType="String" HeaderTextAlign="Center"
                        Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期RT返口数" ColumnID="nowtotal_repairjot"
                        DataField="nowtotal_repairjot" SortField="nowtotal_repairjot" FieldType="String"
                        HeaderTextAlign="Center" TextAlign="Right" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期RT返修率(焊口)" ColumnID="nowrepairrate"
                        DataField="nowrepairrate" SortField="nowrepairrate" FieldType="String" HeaderTextAlign="Center"
                        Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期RT总片数" ColumnID="nowtotalfilm"
                        DataField="nowtotalfilm" SortField="nowtotalfilm" FieldType="String" HeaderTextAlign="Center"
                        Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期RT不合格片数" ColumnID="nopassfilm"
                        DataField="nopassfilm" SortField="nopassfilm" FieldType="String" HeaderTextAlign="Center"
                        Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="本期RT返修率(片数)" ColumnID="nopassfilmrate"
                        DataField="nopassfilmrate" SortField="nopassfilmrate" FieldType="String" HeaderTextAlign="Center"
                        Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="所在班组" ColumnID="education" DataField="education"
                        SortField="education" FieldType="String" HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:TemplateField ColumnID="IsOnDuty" HeaderText="在岗状态"
                        Width="100px" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%#ConvertIsOnDuty(Eval("IsOnDuty")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
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

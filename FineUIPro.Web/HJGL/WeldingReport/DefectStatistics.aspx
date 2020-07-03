<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefectStatistics.aspx.cs" Inherits="FineUIPro.Web.HJGL.WeldingReport.DefectStatistics" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊接缺陷统计</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="管线综合分析"
                EnableCollapse="true" runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="ProjectId"
                AllowCellEditing="true" ClicksToEdit="2" DataIDField="ProjectId" AllowSorting="true"
                SortField="InstallationName,WorkAreaCode" SortDirection="DESC" OnSort="Grid1_Sort"
                AllowPaging="true" IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
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
                    <f:RenderField HeaderText="装置" ColumnID="InstallationName"
                        DataField="InstallationName" SortField="InstallationName" FieldType="String" HeaderTextAlign="Center"
                        Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="单位工程" ColumnID="WorkAreaCode" DataField="WorkAreaCode"
                        SortField="WorkAreaCode" FieldType="String" HeaderTextAlign="Center" Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="横裂纹" ColumnID="Defect1" DataField="Defect1"
                         FieldType="String" HeaderTextAlign="Center" Width="75px">
                    </f:RenderField>
                    <f:RenderField HeaderText="纵裂纹" ColumnID="Defect2"
                        DataField="Defect2"  FieldType="String" HeaderTextAlign="Center"
                        Width="75px">
                    </f:RenderField>
                     <f:RenderField HeaderText="弧坑裂纹" ColumnID="Defect3"
                        DataField="Defect3"  FieldType="String" HeaderTextAlign="Center"
                        Width="75px">
                    </f:RenderField>
                     <f:RenderField HeaderText="未焊透" ColumnID="Defect4"
                        DataField="Defect4" FieldType="String" HeaderTextAlign="Center"
                        Width="75px">
                    </f:RenderField>
                     <f:RenderField HeaderText="未熔合" ColumnID="Defect5"
                        DataField="Defect5"  FieldType="String" HeaderTextAlign="Center"
                        Width="75px">
                    </f:RenderField>
                     <f:RenderField HeaderText="条渣" ColumnID="Defect6"
                        DataField="Defect6"  FieldType="String" HeaderTextAlign="Center"
                        Width="75px">
                    </f:RenderField>
                     <f:RenderField HeaderText="点渣" ColumnID="Defect7"
                        DataField="Defect7"  FieldType="String" HeaderTextAlign="Center"
                        Width="75px">
                    </f:RenderField>
                     <f:RenderField HeaderText="夹钨" ColumnID="Defect8"
                        DataField="Defect8"  FieldType="String" HeaderTextAlign="Center"
                        Width="75px">
                    </f:RenderField>
                     <f:RenderField HeaderText="气孔" ColumnID="Defect9"
                        DataField="Defect9"  FieldType="String" HeaderTextAlign="Center"
                        Width="75px">
                    </f:RenderField>
                     <f:RenderField HeaderText="条孔" ColumnID="Defect10"
                        DataField="Defect10"  FieldType="String" HeaderTextAlign="Center"
                        Width="75px">
                    </f:RenderField>
                     <f:RenderField HeaderText="焊瘤" ColumnID="Defect11"
                        DataField="Defect11"  FieldType="String" HeaderTextAlign="Center"
                        Width="75px">
                    </f:RenderField>
                     <f:RenderField HeaderText="内凹" ColumnID="Defect12"
                        DataField="Defect12"  FieldType="String" HeaderTextAlign="Center"
                        Width="75px">
                    </f:RenderField>
                     <f:RenderField HeaderText="根部咬边" ColumnID="Defect13"
                        DataField="Defect13"  FieldType="String" HeaderTextAlign="Center"
                        Width="75px">
                    </f:RenderField>
                     <f:RenderField HeaderText="错口" ColumnID="Defect14"
                        DataField="Defect14"  FieldType="String" HeaderTextAlign="Center"
                        Width="75px">
                    </f:RenderField>
                     <f:RenderField HeaderText="表面沟槽" ColumnID="Defect15"
                        DataField="Defect15"  FieldType="String" HeaderTextAlign="Center"
                        Width="75px">
                    </f:RenderField>
                     <f:RenderField HeaderText="根部氧化" ColumnID="Defect16"
                        DataField="Defect16"  FieldType="String" HeaderTextAlign="Center"
                        Width="75px">
                    </f:RenderField>
                     <f:RenderField HeaderText="深孔" ColumnID="Defect17"
                        DataField="Defect17"  FieldType="String" HeaderTextAlign="Center"
                        Width="75px">
                    </f:RenderField>
                     <f:RenderField HeaderText="间隙超过1~3mm" ColumnID="Defect18"
                        DataField="Defect18"  FieldType="String" HeaderTextAlign="Center"
                        Width="75px">
                    </f:RenderField>
                     <f:RenderField HeaderText="原材料" ColumnID="Defect19"
                        DataField="Defect19"  FieldType="String" HeaderTextAlign="Center"
                        Width="75px">
                    </f:RenderField>
                     <f:RenderField HeaderText="焊缝" ColumnID="Defect20"
                        DataField="Defect20"  FieldType="String" HeaderTextAlign="Center"
                        Width="75px">
                    </f:RenderField>
                     <f:RenderField HeaderText="其它" ColumnID="Defect21"
                        DataField="Defect21"  FieldType="String" HeaderTextAlign="Center"
                        Width="75px">
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

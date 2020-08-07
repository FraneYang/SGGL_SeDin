<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NDTMonthlyReport.aspx.cs" Inherits="FineUIPro.Web.HJGL.WeldingReport.NDTMonthlyReport" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无损检测月报</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="无损检测月报"
                EnableCollapse="true" runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="UnitWorkName"
                AllowCellEditing="true" ClicksToEdit="2" DataIDField="UnitWorkName" AllowSorting="true"
                SortField="UnitCode,UnitWorkName" SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                           <f:DatePicker runat="server" Label="日期" ID="txtStarTime" LabelAlign="Right"
                                LabelWidth="100px" Width="220px">
                            </f:DatePicker>
                            <f:Label ID="Label1" runat="server" Text="至">
                            </f:Label>
                            <f:DatePicker runat="server" ID="txtEndTime" LabelAlign="Right" LabelWidth="80px"
                                Width="110px">
                            </f:DatePicker>
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
                    <f:GroupField HeaderText="检测单位：" TextAlign="Center">
                        <Columns>
                            <f:RenderField HeaderText="施工分包商" ColumnID="UnitCode"
                                DataField="UnitCode" SortField="WelderCode" FieldType="String" HeaderTextAlign="Center"
                                Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="单位工程" ColumnID="UnitWorkName"
                                DataField="UnitWorkName" SortField="UnitWorkName" FieldType="String" HeaderTextAlign="Center"
                                Width="150px">
                            </f:RenderField>
                        </Columns>
                    </f:GroupField>
                    <f:GroupField HeaderText="本月检测统计" TextAlign="Center">
                        <Columns>
                            <f:RenderField HeaderText="拍片总数(张)" ColumnID="OneCheckTotalFilm"
                                DataField="OneCheckTotalFilm" FieldType="String" HeaderTextAlign="Center"
                                Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="不合格数(张)" ColumnID="OneCheckNoPassFilm"
                                DataField="OneCheckNoPassFilm" FieldType="String" HeaderTextAlign="Center"
                                Width="100px">
                            </f:RenderField> 
                            <f:RenderField HeaderText="合格率(%)" ColumnID="FilmRate"
                               DataField="FilmRate" FieldType="String" HeaderTextAlign="Center" Width="100px">
                            </f:RenderField>
                        </Columns>
                    </f:GroupField>

                    <f:GroupField HeaderText="目前累计情况" TextAlign="Center">
                        <Columns>
                            <f:RenderField HeaderText="拍片总数(张)" ColumnID="TotalCheckTotalFilm"
                                DataField="TotalCheckTotalFilm" FieldType="String" HeaderTextAlign="Center"
                                Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="不合格数(张)" ColumnID="TotalCheckNoPassFilm"
                                DataField="TotalCheckNoPassFilm" FieldType="String" HeaderTextAlign="Center"
                                Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="合格率(%)" ColumnID="TotalFilmRate"
                                DataField="TotalFilmRate" FieldType="String" HeaderTextAlign="Center"
                                Width="100px">
                            </f:RenderField>
                        </Columns>
                    </f:GroupField>

                    <f:GroupField HeaderText="本月复检统计" TextAlign="Center">
                        <Columns>
                            <f:RenderField HeaderText="拍片总数(张)" ColumnID="ReCheckTotalFilm"
                                DataField="ReCheckTotalFilm" FieldType="String" HeaderTextAlign="Center"
                                Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="不合格数(张)" ColumnID="ReCheckNoPassFilm"
                                DataField="ReCheckNoPassFilm" FieldType="String" HeaderTextAlign="Center"
                                Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="合格率(%)" ColumnID="ReFilmRate"
                                DataField="ReFilmRate" FieldType="String" HeaderTextAlign="Center"
                                Width="100px">
                            </f:RenderField>
                        </Columns>
                    </f:GroupField>

                      <f:GroupField HeaderText="复检累计情况" TextAlign="Center">
                        <Columns>
                            <f:RenderField HeaderText="拍片总数(张)" ColumnID="TotalReCheckTotalFilm"
                                DataField="TotalReCheckTotalFilm" FieldType="String" HeaderTextAlign="Center"
                                Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="不合格数(张)" ColumnID="TotalReCheckNoPassFilm"
                                DataField="TotalReCheckNoPassFilm" FieldType="String" HeaderTextAlign="Center"
                                Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="合格率(%)" ColumnID="TotalReFilmRate"
                                DataField="TotalReFilmRate" FieldType="String" HeaderTextAlign="Center"
                                Width="100px">
                            </f:RenderField>
                        </Columns>
                    </f:GroupField>

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

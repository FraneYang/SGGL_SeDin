<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NDTWeeklyReport.aspx.cs" Inherits="FineUIPro.Web.HJGL.WeldingReport.NDTWeeklyReport" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无损检测周统计报表</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="无损检测周统计报表"
                EnableCollapse="true" runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="WelderCode"
                AllowCellEditing="true" ClicksToEdit="2" DataIDField="WelderCode" AllowSorting="true"
                SortField="WelderCode" SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:DropDownList ID="drpUnitId" runat="server" Label="单位名称"
                                LabelAlign="Right" Width="280px" >
                            </f:DropDownList>
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
                    
                    <f:RenderField HeaderText="焊工代号" ColumnID="WelderCode"
                        DataField="WelderCode" SortField="WelderCode" FieldType="String" HeaderTextAlign="Center"
                        Width="100px">
                    </f:RenderField>
                    <f:RenderField HeaderText="施焊焊口数" ColumnID="nowtotal_jot"
                        DataField="nowtotal_jot"  FieldType="String" HeaderTextAlign="Center"
                        Width="90px">
                    </f:RenderField>
                    <f:RenderField HeaderText="检验焊口数" ColumnID="OneCheckJotNum"
                        DataField="OneCheckJotNum"  FieldType="String" HeaderTextAlign="Center"
                        Width="90px">
                    </f:RenderField>
                    <f:RenderField HeaderText="一次合格焊口数" ColumnID="OneCheckPassJotNum"
                        DataField="OneCheckPassJotNum"  FieldType="String" HeaderTextAlign="Center"
                        Width="120px">
                    </f:RenderField>
                    <f:RenderField HeaderText="一次合格率" ColumnID="passRate"
                        DataField="passRate" FieldType="String" HeaderTextAlign="Center"
                        Width="120px">
                    </f:RenderField>
                    <f:RenderField HeaderText="拍片张数" ColumnID="OneCheckTotalFilm"
                        DataField="OneCheckTotalFilm" FieldType="String" HeaderTextAlign="Center"
                        Width="120px">
                    </f:RenderField>
                    <f:RenderField HeaderText="合格张数" ColumnID="OneCheckPassFilm"
                        DataField="OneCheckPassFilm"  FieldType="String" HeaderTextAlign="Center"
                        Width="120px">
                    </f:RenderField>
                    <f:RenderField HeaderText="拍片合格率" ColumnID="passfilmrate"
                        DataField="passfilmrate"  FieldType="String" HeaderTextAlign="Center"
                        Width="120px">
                    </f:RenderField>
                    <f:RenderField HeaderText="累计焊口合格率" ColumnID="passfilmrate"
                        DataField="passfilmrate"  FieldType="String" HeaderTextAlign="Center"
                        Width="120px">
                    </f:RenderField>
                    <f:RenderField HeaderText="累计拍片合格率" ColumnID="passfilmrate"
                        DataField="passfilmrate"  FieldType="String" HeaderTextAlign="Center"
                        Width="120px">
                    </f:RenderField>
                    <f:RenderField HeaderText="备注" ColumnID="passfilmrate"
                        DataField="passfilmrate"  FieldType="String" HeaderTextAlign="Center"
                        Width="120px">
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
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeldJointOut.aspx.cs" Inherits="FineUIPro.Web.HJGL.WeldingReport.WeldJointOut" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊口信息导出</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="焊口信息导出"
                    EnableCollapse="true" runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="WeldJointId"
                    AllowCellEditing="true" ClicksToEdit="2" DataIDField="WeldJointId" AllowSorting="true"
                    SortField="PipelineCode,WeldJointCode" SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true"
                    IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                    EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:DropDownList ID="drpUnitWork" runat="server" Label="单位工程" AutoPostBack="true" OnSelectedIndexChanged="drpUnitWork_SelectedIndexChanged"
                                    LabelAlign="Right" Width="280px">
                                </f:DropDownList>
                                <f:DropDownList ID="drpPipeline" runat="server" Label="管线号" EnableMultiSelect="true"
                                    LabelAlign="Right" Width="280px">
                                </f:DropDownList>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="BtnAnalyse" Text="统计" Icon="ChartPie"
                                    runat="server" OnClick="BtnAnalyse_Click">
                                </f:Button>
                                <f:Button ID="btnSelectColumn" Text="选择显示列" Icon="ShapesManySelect"
                                    runat="server" OnClick="btnSelectColumn_Click">
                                </f:Button>
                                <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" Text="导出"
                                    Icon="TableGo" EnableAjax="false" DisableControlBeforePostBack="false">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:RenderField HeaderText="管线号" ColumnID="PipelineCode"
                            DataField="PipelineCode" SortField="PipelineCode" FieldType="String" HeaderTextAlign="Center"
                            Width="130px">
                        </f:RenderField>
                        <f:RenderField HeaderText="焊口号" ColumnID="WeldJointCode"
                            DataField="WeldJointCode" SortField="WeldJointCode" FieldType="String" HeaderTextAlign="Center"
                            Width="80px">
                        </f:RenderField>
                        <f:RenderField HeaderText="材质1" ColumnID="Material1Code" DataField="Material1Code" SortField="Material1Code" FieldType="String" HeaderTextAlign="Center"
                            TextAlign="Left" Width="90px">
                        </f:RenderField>
                        <f:RenderField HeaderText="材质2" ColumnID="Material2Code"
                            DataField="Material2Code" SortField="Material2Code" FieldType="String" HeaderTextAlign="Center"
                            TextAlign="Left" Width="90px">
                        </f:RenderField>
                        <f:RenderField HeaderText="达因" ColumnID="Size" DataField="Size"
                            SortField="Size" FieldType="Double" HeaderTextAlign="Center" TextAlign="Left"
                            Width="90px">
                        </f:RenderField>
                        <f:RenderField HeaderText="外径" ColumnID="Dia"
                            DataField="Dia" SortField="Dia" FieldType="Double" HeaderTextAlign="Center" TextAlign="Left"
                            Width="90px">
                        </f:RenderField>
                        <f:RenderField HeaderText="壁厚" ColumnID="Thickness"
                            DataField="Thickness" SortField="Thickness" FieldType="Double" HeaderTextAlign="Center"
                            TextAlign="Left" Width="90px">
                        </f:RenderField>
                        <f:RenderField HeaderText="规格" ColumnID="Specification"
                            DataField="Specification" SortField="Specification" FieldType="String" HeaderTextAlign="Center"
                            TextAlign="Left" Width="90px">
                        </f:RenderField>
                        <f:RenderField HeaderText="焊缝类型" ColumnID="WeldTypeCode"
                            DataField="WeldTypeCode" SortField="WeldTypeCode" FieldType="String" HeaderTextAlign="Center"
                            TextAlign="Left" Width="90px">
                        </f:RenderField>
                        <f:RenderField HeaderText="组件1号" ColumnID="ComponentsCode1"
                            DataField="ComponentsCode1" SortField="ComponentsCode1" FieldType="String" HeaderTextAlign="Center"
                            TextAlign="Left" Width="90px">
                        </f:RenderField>
                        <f:RenderField HeaderText="组件2号" ColumnID="ComponentsCode2"
                            DataField="ComponentsCode2" SortField="ComponentsCode2" FieldType="String" HeaderTextAlign="Center"
                            TextAlign="Left" Width="90px">
                        </f:RenderField>
                        <f:RenderField HeaderText="对应WPS" ColumnID="WPQCode"
                            DataField="WPQCode" SortField="WPQCode" FieldType="String" HeaderTextAlign="Center"
                            TextAlign="Left" Width="100px">
                        </f:RenderField>
                        <f:RenderField HeaderText="坡口类型" ColumnID="GrooveTypeCode"
                            DataField="GrooveTypeCode" SortField="GrooveTypeCode" FieldType="String" HeaderTextAlign="Center"
                            TextAlign="Left" Width="90px">
                        </f:RenderField>
                        <f:RenderField HeaderText="焊接方法" ColumnID="WeldingMethodCode"
                            DataField="WeldingMethodCode" SortField="WeldingMethodCode" FieldType="String"
                            HeaderTextAlign="Center" TextAlign="Left" Width="90px">
                        </f:RenderField>
                        <f:RenderField HeaderText="焊丝" ColumnID="WeldingWireCode"
                            DataField="WeldingWireCode" SortField="WeldingWireCode" FieldType="String" HeaderTextAlign="Center"
                            TextAlign="Left" Width="90px">
                        </f:RenderField>
                        <f:RenderField HeaderText="焊条" ColumnID="WeldingRodCode"
                            DataField="WeldingRodCode" SortField="WeldingRodCode" FieldType="String" HeaderTextAlign="Center"
                            TextAlign="Left" Width="90px">
                        </f:RenderField>
                        <f:RenderField HeaderText="预热温度" ColumnID="PreTemperature"
                            DataField="PreTemperature" SortField="PreTemperature" FieldType="String" HeaderTextAlign="Center"
                            TextAlign="Left" Width="90px">
                        </f:RenderField>
                        <f:RenderField HeaderText="是否热处理" ColumnID="IsHotProessStr"
                            DataField="IsHotProessStr" FieldType="String" HeaderTextAlign="Center"
                            TextAlign="Left" Width="110px">
                        </f:RenderField>
                        <f:RenderField HeaderText="备注" ColumnID="Remark" DataField="Remark"
                            FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                            Width="90px">
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
                            <f:ListItem Text="25" Value="25" />
                            <f:ListItem Text="50" Value="50" />
                            <f:ListItem Text="100" Value="100" />
                        </f:DropDownList>
                    </PageItems>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="选择显示列" Hidden="true"
            EnableIFrame="true" EnableMaximize="false" Target="Top" EnableResize="false" runat="server"
            IsModal="true" Width="700px" Height="470px" OnClose="Window1_Close">
        </f:Window>
    </form>
</body>
</html>

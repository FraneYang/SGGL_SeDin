<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectPipeline.aspx.cs" Inherits="FineUIPro.Web.HJGL.LeakVacuum.SelectPipeline" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
            <Items>
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                    Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="管线信息"
                    TitleToolTip="管线信息" AutoScroll="true">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar3" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:HiddenField ID="hdPipelinesId" runat="server"></f:HiddenField>
                                <f:DropDownList ID="drpPipingClass" Label="管线等级" runat="server" LabelWidth="120px" LabelAlign="Right" Width="220px" EnableEdit="true">
                                </f:DropDownList>
                                 <f:NumberBox ID="numTestPressure" runat="server" Label="试验压力" LabelWidth="120px" Width="200px" LabelAlign="Right"></f:NumberBox>
                                <f:NumberBox ID="numTo" runat="server" Label="至" LabelWidth="30px" Width="110px"></f:NumberBox>
                                <f:ToolbarFill runat="server"></f:ToolbarFill>
                                 <f:Button ID="btnFind" Text="查询" ToolTip="查找符合条件的管线" ValidateForms="SimpleForm1" Icon="Find" runat="server" OnClick="btnFind_Click">
                                </f:Button>
                                <f:Button ID="btnRset" OnClick="btnRset_Click" ToolTip="重置" Icon="ArrowUndo" EnablePostBack="true" runat="server">
                                </f:Button>
                                <f:Button ID="btnSave" Text="保存" ToolTip="保存试压包信息" ValidateForms="SimpleForm1" Icon="SystemSave" runat="server" OnClick="btnSave_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                        
                    </Toolbars>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="试压包明细" EnableCollapse="true" Collapsed="false"
                            runat="server" BoxFlex="1" DataKeyNames="PipelineId" AllowCellEditing="true"
                            EnableColumnLines="true" ClicksToEdit="2" DataIDField="PipelineId" AllowSorting="true"
                            SortField="PipelineCode" SortDirection="ASC" OnSort="Grid1_Sort"
                            AllowPaging="true" IsDatabasePaging="true" PageSize="100" Height="360px"
                            OnPageIndexChange="Grid1_PageIndexChange" IsFluid="true" EnableCheckBoxSelect="true">
                            <Columns>
                                <f:RenderField HeaderText="管线编号" ColumnID="PipelineCode" DataField="PipelineCode" SortField="PipelineCode"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="160px" ExpandUnusedSpace="true">
                                </f:RenderField>
                                <f:RenderField HeaderText="设计压力" ColumnID="DesignPress" DataField="DesignPress" SortField="DesignPress"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="设计温度" ColumnID="DesignTemperature" DataField="DesignTemperature" SortField="DesignTemperature"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="试验环境温度" ColumnID="AmbientTemperature" DataField="AmbientTemperature" SortField="AmbientTemperature" FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="110px"> 
                                </f:RenderField>
                                <f:RenderField HeaderText="试验介质温度" ColumnID="TestMediumTemperature" DataField="TestMediumTemperature" SortField="TestMediumTemperature" FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="110px">
                                </f:RenderField>
                                <f:GroupField HeaderText="泄露性试验" TextAlign="Center">
                                    <Columns>
                                        <f:RenderField Width="100px" ColumnID="LeakPressure" DataField="LeakPressure"
                                            FieldType="String" HeaderText="压力" TextAlign="Center" HeaderTextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="110px" ColumnID="LeaMediumName" DataField="LeaMediumName"
                                            FieldType="String" HeaderText="介质" TextAlign="Center" HeaderTextAlign="Center">
                                        </f:RenderField>
                                    </Columns>
                                </f:GroupField>
                                <f:GroupField HeaderText="真空试验" TextAlign="Center">
                                    <Columns>
                                        <f:RenderField Width="100px" ColumnID="VacuumPressure" DataField="VacuumPressure"
                                            FieldType="String" HeaderText="压力" TextAlign="Center" HeaderTextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="110px" ColumnID="VacuumMediumName" DataField="VacuumMediumName"
                                            FieldType="String" HeaderText="介质" TextAlign="Center" HeaderTextAlign="Center">
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
                                    <f:ListItem Text="100" Value="100" />
                                    <f:ListItem Text="150" Value="150" />
                                    <f:ListItem Text="200" Value="200" />
                                    <f:ListItem Text="250" Value="250" />
                                </f:DropDownList>
                            </PageItems>
                        </f:Grid>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
    </form>
    <script>

</script>
</body>
</html>

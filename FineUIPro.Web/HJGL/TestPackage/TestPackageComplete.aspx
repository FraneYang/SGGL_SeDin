<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPackageComplete.aspx.cs" Inherits="FineUIPro.Web.HJGL.TestPackage.TestPackageComplete" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>试压包</title>
    <style type="text/css">
        .Cyan span {
            background-color: Cyan;
        }

        .Yellow span {
            background-color: Yellow;
        }

        .Green span {
            background-color: Green;
        }

        .Purple span {
            background-color: Purple;
        }

        .f-grid-row.Cyan {
            background-color: Cyan;
        }

        .f-grid-row.Yellow {
            background-color: Yellow;
        }

        .f-grid-row.Green {
            background-color: Green;
        }

        .f-grid-row.Purple {
            background-color: Purple;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
            <Items>
                <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                    EnableCollapse="true" Width="230px" Title="试压包" ShowBorder="true" Layout="VBox"
                    ShowHeader="true" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                    <Items>
                        <f:Tree ID="tvControlItem" ShowHeader="false" Title="试压包节点树" OnNodeCommand="tvControlItem_NodeCommand"
                            runat="server" ShowBorder="false" EnableCollapse="true" EnableSingleClickExpand="true" AutoLeafIdentification="true"
                            EnableSingleExpand="true" EnableTextSelection="true">
                            <Listeners>
                                <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                            </Listeners>
                        </f:Tree>
                        <f:HiddenField runat="server" ID="hdPTP_ID"></f:HiddenField>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"  Layout="VBox" ShowHeader="false" BodyPadding="2px" IconFont="PlusCircle" Title="试压包"
                    TitleToolTip="试压包" AutoScroll="true">
                    <Items>
                        <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" BodyPadding="2px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                        <f:Label ID="txtTestDate" Label="试验日期" runat="server" LabelWidth="130px">
                                        </f:Label>
                                        <f:Label ID="txtadjustTestPressure" Label="调整试验压力" runat="server" LabelWidth="130px">
                                        </f:Label>
                                        <f:Label ID="txtAmbientTemperature" Label="试验环境温度" runat="server" LabelWidth="130px">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:Label ID="txtTestMediumTemperature" Label="试验介质温度" runat="server" LabelWidth="130px">
                                        </f:Label>
                                        <f:Label ID="txtHoldingTime" Label="稳压时间" runat="server" LabelWidth="130px">
                                        </f:Label>
                                        <f:Label ID="txtFinishDef" Label="试验结论" runat="server" LabelWidth="130px">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="试压包明细" EnableCollapse="true" Collapsed="false"
                            runat="server" BoxFlex="1" DataKeyNames="PT_PipeId" AllowCellEditing="true"
                            EnableColumnLines="true" ClicksToEdit="2" DataIDField="PT_PipeId" AllowSorting="true"
                            SortField="PipelineCode" SortDirection="ASC" OnSort="Grid1_Sort" EnableTextSelection="True"
                            AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" ForceFit="true">
                            <Columns>
                                <f:RenderField HeaderText="管线编号" ColumnID="PipelineCode" DataField="PipelineCode" SortField="PipelineCode"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="160px">
                                </f:RenderField>
                                <f:RenderField HeaderText="设计压力Mpa(g)" ColumnID="DesignPress" DataField="DesignPress" SortField="DesignPress"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="设计温度℃" ColumnID="DesignTemperature" DataField="DesignTemperature" SortField="DesignTemperature"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="压力试验介质" ColumnID="MediumName" DataField="MediumName" SortField="MediumName"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="压力试验压力Mpa(g)" ColumnID="TestPressure" DataField="TestPressure" SortField="TestPressure"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">
                                </f:RenderField>
                            </Columns>
                            <PageItems>
                                <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                </f:ToolbarSeparator>
                                <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                                </f:ToolbarText>
                                <f:DropDownList runat="server" ID="ddlPageSize" Width="90px" AutoPostBack="true"
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
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="试压包维护" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Top" EnableResize="false" runat="server" OnClose="Window1_Close" IsModal="true"
            Width="1280px" Height="700px">
        </f:Window>
        <f:Window ID="Window2" Title="试压包打印" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Top" EnableResize="false" runat="server"
            IsModal="true" Width="1024px" Height="900px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <Items>
                <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Text="编辑" Icon="Pencil" OnClick="btnMenuModify_Click">
                </f:MenuButton>
            </Items>
        </f:Menu>
    </form>
    <script type="text/javascript">
        // 返回false，来阻止浏览器右键菜单
        var menuID = '<%= Menu1.ClientID %>';
        function onTreeNodeContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>

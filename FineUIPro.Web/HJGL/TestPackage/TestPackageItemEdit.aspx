<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPackageItemEdit.aspx.cs" Inherits="FineUIPro.Web.HJGL.TestPackage.TestPackageItemEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>试压包</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="VBox">
            <Items>
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                    Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="根据条件查找并勾选要进行试压的管线">
                    <Items>
                        <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" Title="试压包" AutoScroll="true" EnableCollapse="true" Collapsed="false"
                            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="txtTestPackageNo" Label="试压包号" ShowRedStar="true" Required="true" runat="server" FocusOnPageLoad="true" LabelWidth="120px">
                                        </f:TextBox>
                                        <f:TextBox ID="txtTestPackageName" Label="系统名称" runat="server" LabelWidth="120px">
                                        </f:TextBox>
                                        <f:DropDownList runat="server" ID="drpUnit" Label ="单位名称"></f:DropDownList>
                                        <f:DropDownList runat="server" ID="drpUnitWork" Label ="单位工程"></f:DropDownList>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="txtRemark" Label="备注" runat="server" LabelWidth="120px">
                                        </f:TextBox>
                                        <f:TextBox ID="txtadjustTestPressure" Label="调整试验压力" runat="server" LabelWidth="120px">
                                        </f:TextBox>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="panel3" RegionPosition="Center" ShowBorder="true"
                    Layout="VBox" ShowHeader="true" BodyPadding="5px" IconFont="PlusCircle" Title="根据条件查找并勾选要进行试压的管线">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Bottom" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:HiddenField ID="hdPipelinesId" runat="server"></f:HiddenField>
                                <f:Button ID="btnFind" Text="查询" ToolTip="查找符合条件的管线" ValidateForms="SimpleForm1" Icon="Find" runat="server" OnClick="btnFind_Click">
                                </f:Button>
                                <f:Button ID="btnSave" Text="保存" ToolTip="保存试压包信息" ValidateForms="SimpleForm1" Icon="SystemSave" runat="server" OnClick="btnSave_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                </f:Panel>
                <f:Panel runat="server" ID="panel2" RegionPosition="Center" ShowBorder="true"
                    ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" AutoScroll="true">
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="试压包明细" EnableCollapse="true" Collapsed="false"
                            runat="server" BoxFlex="1" DataKeyNames="PipelineId" AllowCellEditing="true"
                            EnableColumnLines="true" ClicksToEdit="1" DataIDField="PipelineId" AllowSorting="true"
                            SortField="PipelineCode" SortDirection="ASC" OnSort="Grid1_Sort"  IsDatabasePaging="true" PageSize="100" Height="360px"
                            OnPageIndexChange="Grid1_PageIndexChange" ForceFit="true">
                            <Columns>
                                <f:RenderField HeaderText="管线编号" ColumnID="PipelineCode" DataField="PipelineCode" SortField="PipelineCode"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="160px">
                                </f:RenderField>
                                <f:RenderField HeaderText="设计压力" ColumnID="DesignPress" DataField="DesignPress" SortField="DesignPress"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="设计温度" ColumnID="DesignTemperature" DataField="DesignTemperature" SortField="DesignTemperature"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="试验环境温度" ColumnID="AmbientTemperature" DataField="AmbientTemperature" SortField="AmbientTemperature"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="110px">
                                    <Editor>
                                        <f:NumberBox ID="txtAmbientTemperature" Required="true" runat="server">
                                        </f:NumberBox>
                                    </Editor>
                                </f:RenderField>
                                <f:TemplateField ColumnID="TestMedium" Width="130px" HeaderText="压力试验介质" HeaderTextAlign="Center" TextAlign="Center"
                                    EnableLock="true" Locked="False">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="drpTestMedium" runat="server" Height="22" Width="90%">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdTestMedium" runat="server" Value='<%# Bind("TestMedium") %>' />
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:RenderField HeaderText="试验介质温度" ColumnID="TestMediumTemperature" DataField="TestMediumTemperature" SortField="TestMediumTemperature"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="110px">
                                    <Editor>
                                        <f:NumberBox ID="txtTestMediumTemperature" Required="true" runat="server">
                                        </f:NumberBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField HeaderText="压力试验压力" ColumnID="TestPressure" DataField="TestPressure" SortField="TestPressure"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="110px">
                                    <Editor>
                                        <f:NumberBox ID="NumberBox1" Required="true" runat="server">
                                        </f:NumberBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField HeaderText="稳压时间min" ColumnID="HoldingTime" DataField="HoldingTime" SortField="HoldingTime"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="100px">
                                    <Editor>
                                        <f:NumberBox ID="txtHoldingTime" Required="true" runat="server">
                                        </f:NumberBox>
                                    </Editor>
                                </f:RenderField>
                            </Columns>
                            <Listeners>
                                <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                            </Listeners>
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
        <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
            Width="1000px" Height="660px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
                ConfirmText="删除选中行？" ConfirmTarget="Top" runat="server"
                Text="删除" Icon="Delete">
            </f:MenuButton>
        </f:Menu>
    </form>
    <script type="text/javascript">
        var menuID = '<%= Menu1.ClientID %>';
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>

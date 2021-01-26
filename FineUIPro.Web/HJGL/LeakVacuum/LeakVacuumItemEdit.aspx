<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeakVacuumItemEdit.aspx.cs" Inherits="FineUIPro.Web.HJGL.LeakVacuum.LeakVacuumItemEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>泄露性/真空试验包</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="VBox">
            <Items>
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                    Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="根据条件查找并勾选要进行试压的管线">
                    <Items>
                        <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" Title="泄露性/真空试验包" AutoScroll="true" EnableCollapse="true" Collapsed="false"
                            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="txtsysNo" Label="系统编号" ShowRedStar="true" Required="true" runat="server" FocusOnPageLoad="true" LabelWidth="120px">
                                        </f:TextBox>
                                        <f:TextBox ID="txtsysName" Label="系统名称" ShowRedStar="true" Required="true" runat="server" LabelWidth="120px">
                                        </f:TextBox>
                                        <f:DropDownList ID="drpUnit" Label="单位" runat="server" ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="120px"
                                            Readonly="true">
                                        </f:DropDownList>
                                        <f:DropDownList ID="drpUnitWork" Label="单位工程名称" runat="server" ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="120px"
                                            Readonly="true">
                                        </f:DropDownList>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ColumnWidths="25% 50% 25%">
                                    <Items>
                                        
                                        <f:DatePicker ID="txtTableDate" Label="试验日期" runat="server" DateFormatString="yyyy-MM-dd" LabelWidth="120px" ShowRedStar="true" Required="true">
                                        </f:DatePicker>
                                        <f:TextBox ID="txtFinishDef" Label="试验结论" runat="server" LabelWidth="120px" ShowRedStar="true" Required="true">
                                        </f:TextBox>
                                        <f:TextBox ID="txtRemark" Label="备注" runat="server" LabelWidth="130px">
                                        </f:TextBox>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="panel4" RegionPosition="Center" ShowBorder="true" Layout="VBox"
                    BodyPadding="2px" IconFont="PlusCircle" Title="试压前条件确认"
                    ShowHeader="true" AutoScroll="true" EnableCollapse="true" Collapsed="false" Height="200px">
                    <Items>
                        <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" Title="试压包" AutoScroll="true"
                            EnableCollapse="true" Collapsed="false" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
                            LabelAlign="Left">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                        <f:DropDownList ID="drpInstallationSpecification" runat="server" Label="1.管道压力试验合格"
                                            LabelAlign="Right" LabelWidth="350px">
                                            <f:ListItem Value="完成" Text="完成" Selected="true" />
                                            <f:ListItem Value="未完成" Text="未完成" />
                                            <f:ListItem Value="/" Text="/" />
                                        </f:DropDownList>
                                        <f:DropDownList ID="drpPressureTest" runat="server" Label="2.压力试验采用的临时法兰、螺栓、垫片等均己更换" LabelAlign="Right"
                                            LabelWidth="350px">
                                            <f:ListItem Value="完成" Text="完成" Selected="true" />
                                            <f:ListItem Value="未完成" Text="未完成" />
                                            <f:ListItem Value="/" Text="/" />
                                        </f:DropDownList>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:DropDownList ID="drpWorkRecord" runat="server" Label="3.未参与压力试验的安全附件及仪表等己安装复位" LabelAlign="Right"
                                            LabelWidth="350px">
                                            <f:ListItem Value="完成" Text="完成" Selected="true" />
                                            <f:ListItem Value="未完成" Text="未完成" />
                                            <f:ListItem Value="/" Text="/" />
                                        </f:DropDownList>
                                        <f:DropDownList ID="drpNDTConform" runat="server" Label="4.试验管道系统的阀门已全部开启"
                                            LabelAlign="Right" LabelWidth="350px">
                                            <f:ListItem Value="完成" Text="完成" Selected="true" />
                                            <f:ListItem Value="未完成" Text="未完成" />
                                            <f:ListItem Value="/" Text="/" />
                                        </f:DropDownList>
                                    </Items>
                                </f:FormRow>

                                <f:FormRow>
                                    <Items>
                                        <f:DropDownList ID="drpHotConform" runat="server" Label="5.试压用压力表、精度等级、检定状态符合规范要求"
                                            LabelAlign="Right" LabelWidth="350px">
                                            <f:ListItem Value="完成" Text="完成" Selected="true" />
                                            <f:ListItem Value="未完成" Text="未完成" />
                                            <f:ListItem Value="/" Text="/" />
                                        </f:DropDownList>
                                        <f:Label runat="server"></f:Label>
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
                            SortField="PipelineCode" SortDirection="ASC" OnSort="Grid1_Sort"
                            AllowPaging="true" IsDatabasePaging="true" PageSize="100" Height="360px"
                            OnPageIndexChange="Grid1_PageIndexChange">
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
                                <f:RenderField HeaderText="试验环境温度" ColumnID="AmbientTemperature" DataField="AmbientTemperature" SortField="AmbientTemperature"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="110px">
                                    <Editor>
                                        <f:NumberBox ID="txtAmbientTemperature" Required="true" runat="server">
                                        </f:NumberBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField HeaderText="试验介质温度" ColumnID="TestMediumTemperature" DataField="TestMediumTemperature" SortField="TestMediumTemperature" FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="110px">
                                    <Editor>
                                        <f:NumberBox ID="txtTestMediumTemperature" Required="true" runat="server">
                                        </f:NumberBox>
                                    </Editor>
                                </f:RenderField>
                                <f:GroupField HeaderText="泄露性试验" TextAlign="Center">
                                    <Columns>
                                        <f:RenderField Width="100px" ColumnID="LeakPressure" DataField="LeakPressure"
                                            FieldType="String" HeaderText="压力" TextAlign="Center" HeaderTextAlign="Center">
                                            <Editor>
                                                <f:NumberBox ID="txtLeakPressure" Required="true" runat="server">
                                                </f:NumberBox>
                                            </Editor>
                                        </f:RenderField>
                                        <f:TemplateField ColumnID="LeakMedium" Width="130px" HeaderText="介质" HeaderTextAlign="Center" TextAlign="Center"
                                            EnableLock="true" Locked="False">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpLeakMedium" runat="server" Height="22" Width="90%" >
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdLeakMedium" runat="server" Value='<%# Bind("LeakMedium") %>' />
                                            </ItemTemplate>
                                        </f:TemplateField>
                                    </Columns>
                                </f:GroupField>
                                <f:GroupField HeaderText="真空试验" TextAlign="Center">
                                    <Columns>
                                        <f:RenderField Width="100px" ColumnID="VacuumPressure" DataField="VacuumPressure"
                                            FieldType="String" HeaderText="压力" TextAlign="Center" HeaderTextAlign="Center">
                                            <Editor>
                                                <f:NumberBox ID="txtVacuumPressure" Required="true" runat="server">
                                                </f:NumberBox>
                                            </Editor>
                                        </f:RenderField>
                                        <f:TemplateField ColumnID="VacuumMedium" Width="130px" HeaderText="介质" HeaderTextAlign="Center" TextAlign="Center"
                                            EnableLock="true" Locked="False">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpVacuumMedium" runat="server" Height="22" Width="90%" >
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdVacuumMedium" runat="server" Value='<%# Bind("VacuumMedium") %>' />
                                            </ItemTemplate>
                                        </f:TemplateField>
                                    </Columns>
                                </f:GroupField>
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
            Target="Parent" EnableResize="false" runat="server" IsModal="true" OnClose="Window1_Close"
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

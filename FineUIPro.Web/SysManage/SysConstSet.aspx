<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysConstSet.aspx.cs" Inherits="FineUIPro.Web.SysManage.SysConstSet" ValidateRequest="false" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>系统环境设置</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }

        .LabelColor {
            color: Red;
            font-size: small;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:Panel ID="Panel2" runat="server" ShowHeader="false" ShowBorder="false" ColumnWidth="100%" MarginRight="5px">
            <Items>
                <f:TabStrip ID="TabStrip1" CssClass="f-tabstrip-theme-simple" Height="550px" ShowBorder="true"
                    TabPosition="Top" MarginBottom="5px" EnableTabCloseMenu="false" runat="server"
                    ActiveTabIndex="1">
                    <Tabs>
                        <f:Tab ID="Tab1" Title="系统环境" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">
                            <Items>
                                <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Rows>
                                        <%--    <f:FormRow ColumnWidths="35% 65%">
                                            <Items>
                                                <f:CheckBox ID="ckSynchronization" runat="server" Label="是否自动提取数据" LabelWidth="220px"> 
                                                </f:CheckBox>
                                                <f:Label ID="Label7" runat="server" Text="说明：与集团数据交换数据是否自动提取。" MarginLeft="50px"></f:Label>
                                            </Items>
                                        </f:FormRow>--%>
                                        <f:FormRow ColumnWidths="35% 65%">
                                            <Items>
                                                <f:NumberBox ID="txtNumberBox" runat="server" Label="有效期警示" LabelWidth="220px"></f:NumberBox>
                                                <f:Label ID="Label9" runat="server" Text="说明：用于资质有效期警示时间。" MarginLeft="50px"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ColumnWidths="35% 65%">
                                            <Items>
                                                <f:CheckBox ID="ckIsMonthReportGetAVG" runat="server" Label="人工时月报是否按平均数取值" LabelWidth="220px">
                                                </f:CheckBox>
                                                <f:Label ID="Label10" runat="server" Text="说明：人工时月报统计时，是否按照平均人数、平均工时数及平均天数取值。" MarginLeft="50px"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ColumnWidths="35% 65%">
                                            <Items>
                                                <f:NumberBox ID="txtMonthReportFreezeDay" runat="server" Label="管理月报冻结日期" NoDecimal="true" NoNegative="true" MinValue="1" LabelWidth="220px">
                                                </f:NumberBox>
                                                <f:Label ID="Label11" runat="server" Text="说明：项目现场管理月报的冻结日期，如设置为4，则当月月报在次月4日24时以后冻结，不可再进行修改。" MarginLeft="50px"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ColumnWidths="35% 65%" runat="server" ID="frFlowOperate" Hidden="true">
                                            <Items>
                                                <f:CheckBox ID="ckMenuFlowOperate" runat="server" Label="固定流程" LabelWidth="220px">
                                                </f:CheckBox>
                                                <f:Label ID="Label12" runat="server" Text="说明：设置固定流程单据审核流程按照已定义流程审核，不设置在在单据中自主选择流程。" MarginLeft="50px"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                    </Rows>
                                </f:Form>
                            </Items>
                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                                    <Items>
                                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1"
                                            OnClick="btnSave_Click">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Tab>
                        <f:Tab ID="Tab2" Title="菜单流程" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
                                    <Items>
                                        <f:RadioButtonList ID="rblMenuType" runat="server" Width="120px"
                                            OnSelectedIndexChanged="rblMenuType_SelectedIndexChanged" AutoPostBack="true">
                                            <f:RadioItem Value="MenuType_S" Text="本部" />
                                            <f:RadioItem Value="MenuType_P" Text="项目" Selected="true" />
                                        </f:RadioButtonList>
                                        <f:DropDownBox runat="server" ID="drpMenu" Values="henan" EmptyText="请选择末级菜单" Width="500px"
                                            EnableMultiSelect="false" AutoPostBack="true" OnTextChanged="drpMenu_TextChanged" EnableEdit="true">
                                            <PopPanel>
                                                <f:Tree ID="treeMenu" ShowHeader="false" Hidden="true" runat="server" EnableSingleExpand="true">
                                                </f:Tree>
                                            </PopPanel>
                                        </f:DropDownBox>
                                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Items>
                                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" EnableColumnLines="true" EnableColumnMove="true"
                                    runat="server" BoxFlex="1" DataKeyNames="FlowOperateId" AllowCellEditing="true" ClicksToEdit="2"
                                    DataIDField="FlowOperateId" AllowSorting="true" SortField="FlowStep,GroupNum,OrderNum" SortDirection="ASC" Height="320px"
                                    OnSort="Grid1_Sort" AllowPaging="true" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"
                                    EnableTextSelection="True">
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar4" Position="Top" ToolbarAlign="Right" runat="server">
                                            <Items>
                                                <f:Button ID="btnFlowOperateNew" ToolTip="增加" Icon="Add" runat="server" OnClick="btnFlowOperateNew_Click" />
                                                <f:Button ID="btnFlowOperateDelete" ToolTip="删除" Icon="Delete" ConfirmText="确定删除当前数据？" OnClick="btnFlowOperateDelete_Click" runat="server">
                                                </f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Columns>
                                        <f:RenderField Width="100px" ColumnID="FlowStep" DataField="FlowStep" FieldType="Int"
                                            HeaderText="步骤" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="100px" ColumnID="GroupNum" DataField="GroupNum" FieldType="Int"
                                            HeaderText="组号" HeaderTextAlign="Center" TextAlign="Center" Hidden="true">
                                        </f:RenderField>
                                        <f:RenderField Width="100px" ColumnID="OrderNum" DataField="OrderNum" FieldType="Int"
                                            HeaderText="组内序号" HeaderTextAlign="Center" TextAlign="Center" Hidden="true">
                                        </f:RenderField>
                                        <f:RenderField Width="250px" ColumnID="AuditFlowName" DataField="AuditFlowName" FieldType="String" HeaderText="名称" HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:TemplateField Width="300px" HeaderText="审批角色" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Label8" runat="server" Text='<%# ConvertRole(Eval("RoleId")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </f:TemplateField>
                                        <f:CheckBoxField Width="80px" RenderAsStaticField="true" TextAlign="Center" DataField="IsFlowEnd" HeaderText="是否结束" />
                                    </Columns>
                                    <Listeners>
                                        <f:Listener Event="dataload" Handler="onGridDataLoad" />
                                    </Listeners>
                                </f:Grid>
                            </Items>
                        </f:Tab>
                        <f:Tab ID="Tab3" Title="考试规则" BodyPadding="5px" Layout="Fit" IconFont="Bookmark" runat="server">
                            <Items>
                                <f:Form ID="frTestSet" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Rows>
                                        <f:FormRow ColumnWidths="35% 65%">
                                            <Items>
                                                <f:Label ID="lbTotalScore" Label="总分" runat="server" LabelWidth="220px"></f:Label>
                                                <f:Label ID="lbTotalCount" Label="题目数量" runat="server"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ColumnWidths="35% 65%">
                                            <Items>
                                                <f:NumberBox ID="txtDuration" runat="server" Label="考试时长(分钟)" ShowRedStar="true" Required="true"
                                                    NoDecimal="true" NoNegative="true" LabelWidth="220px">
                                                </f:NumberBox>
                                                <f:Label ID="Label14" runat="server" Text="说明：一场考试的用时。" MarginLeft="50px"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ColumnWidths="35% 65%">
                                            <Items>
                                                <f:NumberBox ID="txtPassingScore" runat="server" Label="及格分" ShowRedStar="true" Required="true"
                                                    NoDecimal="true" NoNegative="true" LabelWidth="220px">
                                                </f:NumberBox>
                                                <f:Label ID="Label21" runat="server" Text="说明：及格分数线。" MarginLeft="50px"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ColumnWidths="35% 65%">
                                            <Items>
                                                <f:NumberBox ID="txtSValue" runat="server" Label="单选题分值" ShowRedStar="true" Required="true"
                                                    NoDecimal="true" NoNegative="true" LabelWidth="220px" OnTextChanged="txtTab3_TextChanged" AutoPostBack="true">
                                                </f:NumberBox>
                                                <f:Label ID="Label15" runat="server" Text="说明：一个单选题多少分。" MarginLeft="50px"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ColumnWidths="35% 65%">
                                            <Items>
                                                <f:NumberBox ID="txtMValue" runat="server" Label="多选题分值" ShowRedStar="true" Required="true"
                                                    NoDecimal="true" NoNegative="true" LabelWidth="220px" OnTextChanged="txtTab3_TextChanged" AutoPostBack="true">
                                                </f:NumberBox>
                                                <f:Label ID="Label16" runat="server" Text="说明：一个多选题多少分。" MarginLeft="50px"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ColumnWidths="35% 65%">
                                            <Items>
                                                <f:NumberBox ID="txtJValue" runat="server" Label="判断题分值" ShowRedStar="true" Required="true"
                                                    NoDecimal="true" NoNegative="true" LabelWidth="220px" OnTextChanged="txtTab3_TextChanged" AutoPostBack="true">
                                                </f:NumberBox>
                                                <f:Label ID="Label17" runat="server" Text="说明：一个判断题多少分。" MarginLeft="50px"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ColumnWidths="35% 65%">
                                            <Items>
                                                <f:NumberBox ID="txtSCount" runat="server" Label="单选题数量" ShowRedStar="true" Required="true"
                                                    NoDecimal="true" NoNegative="true" LabelWidth="220px" OnTextChanged="txtTab3_TextChanged" AutoPostBack="true">
                                                </f:NumberBox>
                                                <f:Label ID="Label18" runat="server" Text="说明：一张试卷有多少个单选题。" MarginLeft="50px"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ColumnWidths="35% 65%">
                                            <Items>
                                                <f:NumberBox ID="txtMCount" runat="server" Label="多选题数量" ShowRedStar="true" Required="true"
                                                    NoDecimal="true" NoNegative="true" LabelWidth="220px" OnTextChanged="txtTab3_TextChanged" AutoPostBack="true">
                                                </f:NumberBox>
                                                <f:Label ID="Label19" runat="server" Text="说明：一张试卷有多少个多选题。" MarginLeft="50px"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ColumnWidths="35% 65%">
                                            <Items>
                                                <f:NumberBox ID="txtJCount" runat="server" Label="判断题数量" ShowRedStar="true" Required="true"
                                                    NoDecimal="true" NoNegative="true" LabelWidth="220px" OnTextChanged="txtTab3_TextChanged" AutoPostBack="true">
                                                </f:NumberBox>
                                                <f:Label ID="Label20" runat="server" Text="说明：一张试卷有多少个判断题。" MarginLeft="50px"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                    </Rows>
                                </f:Form>
                            </Items>
                            <Toolbars>
                                <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                                    <Items>
                                        <f:Button ID="btnTab3Save" Icon="SystemSave" runat="server" ValidateForms="frTestSet"
                                            OnClick="btnTab3Save_Click">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Tab>
                    </Tabs>
                </f:TabStrip>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="流程步骤设置" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Self" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
            Width="640px" Height="450px">
        </f:Window>
    </form>
    <script type="text/javascript">      
        function onGridDataLoad(event) {
            this.mergeColumns(['FlowStep', 'GroupNum'], { depends: true });
        }
    </script>
</body>
</html>

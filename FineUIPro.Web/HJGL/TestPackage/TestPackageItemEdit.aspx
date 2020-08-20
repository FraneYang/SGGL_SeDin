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
                                        <f:DropDownList ID="drpUnit" Label="单位" runat="server" ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="120px"
                                             Readonly="true">
                                        </f:DropDownList>
                                        <f:DropDownList ID="drpUnitWork" Label="单位工程名称" runat="server" ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="120px"
                                           Readonly="true" >
                                        </f:DropDownList>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ColumnWidths="25% 25% 50%">
                                    <Items>
                                        <f:DropDownList ID="drpTabler" Label="建档人" runat="server" EnableEdit="true" LabelWidth="120px" ShowRedStar="true" Required="true">
                                        </f:DropDownList>
                                        <f:DatePicker ID="txtTableDate" Label="建档日期" runat="server" DateFormatString="yyyy-MM-dd" LabelWidth="120px" ShowRedStar="true" Required="true">
                                        </f:DatePicker>
                                       <f:TextBox ID="txtRemark" Label="备注"  runat="server" LabelWidth="130px">
                                     </f:TextBox> 
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="panel4" RegionPosition="Center" ShowBorder="true" Layout="VBox"
                        BodyPadding="2px" IconFont="PlusCircle" Title="试压前条件确认" 
                        ShowHeader="true" AutoScroll="true"  EnableCollapse="true" Collapsed="false" Height="200px">
                        <Items>
                            <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" Title="试压包" AutoScroll="true"
                                EnableCollapse="true" Collapsed="false" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
                                LabelAlign="Left">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                            <f:DropDownList ID="drpInstallationSpecification" runat="server" Label="1.管道安装符合设计文件和规范要求"
                                                LabelAlign="Right" LabelWidth="350px">
                                                 <f:ListItem Value="True" Text="完成" Selected="true" />
                                                 <f:ListItem Value="False" Text="未完成" />
                                            </f:DropDownList>
                                             <f:DropDownList ID="drpPressureTest" runat="server" Label="2.管道耐压试验合格" LabelAlign="Right"
                                                LabelWidth="350px">
                                                <f:ListItem Value="True" Text="完成" Selected="true" />
                                                <f:ListItem Value="False" Text="未完成" />
                                            </f:DropDownList>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                             <f:DropDownList ID="drpWorkRecord" runat="server" Label="3.焊接工作记录齐全" LabelAlign="Right"
                                                LabelWidth="350px">
                                                <f:ListItem Value="True" Text="完成" Selected="true" />
                                                <f:ListItem Value="False" Text="未完成" />
                                            </f:DropDownList>
                                             <f:DropDownList ID="drpNDTConform" runat="server" Label="4.无损检测结果符合设计文件和规范要求" 
                                                LabelAlign="Right" LabelWidth="350px">
                                                <f:ListItem Value="True" Text="完成" Selected="true" />
                                                <f:ListItem Value="False" Text="未完成" />
                                            </f:DropDownList>
                                        </Items>
                                    </f:FormRow>
                                  
                                    <f:FormRow>
                                        <Items>
                                            <f:DropDownList ID="drpHotConform" runat="server" Label="5.热处理结果符合设计文件和规范要求" 
                                                LabelAlign="Right" LabelWidth="350px">
                                                <f:ListItem Value="True" Text="完成" Selected="true" />
                                                <f:ListItem Value="False" Text="未完成" />
                                            </f:DropDownList>
                                             <f:DropDownList ID="drpInstallationCorrectness" runat="server" Label="6.支、吊架安装正确" LabelAlign="Right"
                                                LabelWidth="350px">
                                                <f:ListItem Value="True" Text="完成" Selected="true" />
                                                <f:ListItem Value="False" Text="未完成" />
                                            </f:DropDownList>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:DropDownList ID="drpMarkClearly" runat="server" Label="7.合金钢管道材质标记清楚" 
                                                LabelAlign="Right" LabelWidth="350px">
                                                <f:ListItem Value="True" Text="完成" Selected="true" />
                                                <f:ListItem Value="False" Text="未完成" />
                                            </f:DropDownList>
                                            <f:DropDownList ID="drpIsolationOpening" runat="server" Label="8.不参与管道系统试验的安全附件、仪表已按规定拆除或隔离，参与试压的系统内的阀门全部开启"
                                                LabelAlign="Right" LabelWidth="350px">
                                                <f:ListItem Value="True" Text="完成" Selected="true" />
                                                <f:ListItem Value="False" Text="未完成" />
                                            </f:DropDownList> 
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                             <f:DropDownList ID="drpConstructionPlanAsk" runat="server" Label="9.临时加固措施、盲板位置与标识符合施工方案要求"
                                                LabelAlign="Right" LabelWidth="350px">
                                                 <f:ListItem Value="True" Text="完成" Selected="true" />
                                                 <f:ListItem Value="False" Text="未完成" />
                                            </f:DropDownList>
                                             <f:DropDownList ID="drpCover" runat="server" Label="10.焊接接头及需要检验的部位未被覆盖" LabelAlign="Right"
                                                LabelWidth="350px">
                                                 <f:ListItem Value="True" Text="完成" Selected="true" />
                                                 <f:ListItem Value="False" Text="未完成" />
                                            </f:DropDownList>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                           <f:DropDownList ID="drpMeetRequirements" runat="server" Label="11.试压用压力表量程、精度等级、检定状态符合规范要求"
                                                LabelAlign="Right" LabelWidth="350px">
                                                <f:ListItem Value="True" Text="完成" Selected="true" />
                                                 <f:ListItem Value="False" Text="未完成" />
                                            </f:DropDownList> 
                                            <f:DropDownList ID="drpStainlessTestWater" runat="server" Label="12.不锈钢管道试验用水符合规范要求" 
                                                LabelAlign="Right" LabelWidth="350px">
                                                 <f:ListItem Value="True" Text="完成" Selected="true" />
                                                <f:ListItem Value="False" Text="未完成" />
                                            </f:DropDownList>
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
                                <f:DropDownList ID="drpPipingClass" Label="管线等级" runat="server" LabelWidth="120px" LabelAlign="Right" Width="220px" EnableEdit="true">
                                </f:DropDownList>
                                <f:DropDownList ID="drpTestMedium" Label="试验介质" runat="server" LabelWidth="120px" Width="220px" LabelAlign="Right" EnableEdit="true">
                                </f:DropDownList>
                                <f:NumberBox ID="numTestPressure" runat="server" Label="试验压力" LabelWidth="120px" Width="200px" LabelAlign="Right"></f:NumberBox>
                                <f:NumberBox ID="numTo" runat="server" Label="至" LabelWidth="30px" Width="110px"></f:NumberBox>
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
                            EnableColumnLines="true" ClicksToEdit="2" DataIDField="PipelineId" AllowSorting="true"
                            SortField="PipelineCode" SortDirection="ASC" OnSort="Grid1_Sort"
                            AllowPaging="true" IsDatabasePaging="true" PageSize="100" Height="360px"
                            OnPageIndexChange="Grid1_PageIndexChange" EnableTextSelection="True">
                            <Columns>
                                 <f:RenderField HeaderText="管线编号" ColumnID="PipelineCode" DataField="PipelineCode" SortField="PipelineCode"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left"  Width="160px" ExpandUnusedSpace="true">                       
                            </f:RenderField> 
                             <f:RenderField HeaderText="设计压力" ColumnID="TestPressure" DataField="TestPressure" SortField="TestPressure"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">                       
                            </f:RenderField> 
                            <f:RenderField HeaderText="设计温度" ColumnID="TestPressure" DataField="TestPressure" SortField="TestPressure"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">                       
                            </f:RenderField> 
                             <f:RenderField HeaderText="试验环境温度" ColumnID="TestPressure" DataField="TestPressure" SortField="TestPressure"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="110px">                       
                            </f:RenderField>
                            <f:RenderField HeaderText="试验介质" ColumnID="TestMedium" DataField="TestMedium" SortField="TestMedium"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">                       
                            </f:RenderField>  
                             <f:RenderField HeaderText="试验介质温度" ColumnID="TestPressure" DataField="TestPressure" SortField="TestPressure"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="110px">                       
                            </f:RenderField>
                            <f:RenderField HeaderText="试验压力" ColumnID="TestPressure" DataField="TestPressure" SortField="TestPressure"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">                       
                            </f:RenderField> 
                             <f:RenderField HeaderText="稳压时间min" ColumnID="TestPressure" DataField="TestPressure" SortField="TestPressure"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="100px">                       
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
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnAllSelect" OnClick="btnAllSelect_Click" EnablePostBack="true"
                runat="server" Text="全选" Icon="Accept">
            </f:MenuButton>
            <f:MenuButton ID="btnNoSelect" OnClick="btnNoSelect_Click" EnablePostBack="true"
                runat="server" Text="全不选" Icon="Cancel">
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

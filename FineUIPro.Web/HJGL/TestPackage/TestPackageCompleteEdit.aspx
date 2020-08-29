<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPackageCompleteEdit.aspx.cs" Inherits="FineUIPro.Web.HJGL.TestPackage.TestPackageCompleteEdit" %>

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
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                    Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="试压包"
                    TitleToolTip="试压包" AutoScroll="true">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                            <Items>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                <f:Button ID="btnAudit" Text="保存"  Icon="TableKey" runat="server" OnClick="btnAudit_Click"  ValidateForms="SimpleForm1">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>
                        <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="true" Title="试压包" AutoScroll="true" EnableCollapse="true" Collapsed="false"
                            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                        <f:Label ID="txtTestPackageNo" Label="系统号" runat="server" LabelWidth="140px">
                                        </f:Label>
                                        <f:Label ID="txtTestPackageName" Label="系统名称" runat="server" LabelWidth="140px">
                                        </f:Label>
                                        <f:Label ID="txtRemark" Label="备注" runat="server" LabelWidth="140px">
                                        </f:Label>
                                        <f:Label ID="txtadjustTestPressure" Label="调整试验压力" runat="server" LabelWidth="140px">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:NumberBox ID="txtAmbientTemperature" Label="实验环境温度"  runat="server" LabelWidth="140px">
                                        </f:NumberBox>
                                        <f:NumberBox ID="txtTestMediumTemperature" Label="试验介质温度"  runat="server" LabelWidth="140px">
                                        </f:NumberBox>
                                        <f:NumberBox ID="txtHoldingTime" Label="稳压时间"  runat="server" LabelWidth="140px">
                                        </f:NumberBox>
                                        <f:TextBox runat="server" ID="txtFinishDef" Label="实验结论" LabelWidth="140px"></f:TextBox>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>

                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="试压包明细" EnableCollapse="true" Collapsed="false"
                            runat="server" BoxFlex="1" DataKeyNames="PT_PipeId" AllowCellEditing="true"
                            EnableColumnLines="true" ClicksToEdit="2" DataIDField="PT_PipeId" AllowSorting="true"
                            SortField="UnitWorkCode,PipelineCode" SortDirection="ASC" OnSort="Grid1_Sort" EnableTextSelection="True"  IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" ForceFit="true">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar3" Position="Bottom" runat="server">
                                    <Items>
                                        <f:Label CssClass="Cyan" runat="server" ID="lbCyan" Text="&nbsp;&nbsp;&nbsp;&nbsp;" LabelAlign="Right"></f:Label>
                                        <f:Label runat="server" ID="lab1" Label="未焊完" LabelWidth="70px" Width="120px" LabelAlign="Right"></f:Label>
                                        <f:Label CssClass="Yellow" runat="server" ID="Label1" Text="&nbsp;&nbsp;&nbsp;&nbsp;" LabelAlign="Right"></f:Label>
                                        <f:Label runat="server" ID="lab2" Label="已焊完，未达检测比例" LabelWidth="170px" Width="220px" LabelAlign="Right"></f:Label>
                                        <f:Label CssClass="Green" runat="server" ID="Label2" Text="&nbsp;&nbsp;&nbsp;&nbsp;" LabelAlign="Right"></f:Label>
                                        <f:Label runat="server" ID="lab3" Label="已焊完，已达检测比例，但有不合格" LabelWidth="260px" Width="310px" LabelAlign="Right"></f:Label>
                                        <f:Label CssClass="Purple" runat="server" ID="Label4" Text="&nbsp;&nbsp;&nbsp;&nbsp;" LabelAlign="Right"></f:Label>
                                        <f:Label runat="server" ID="lab4" Label="已通过" LabelWidth="70px" Width="120px" LabelAlign="Right"></f:Label>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center" />
                                <f:RenderField HeaderText="工作区" ColumnID="UnitWorkCode" DataField="UnitWorkCode" SortField="UnitWorkCode"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">
                                </f:RenderField>
                                 <f:RenderField HeaderText="管线编号" ColumnID="PipelineCode" DataField="PipelineCode" SortField="PipelineCode"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="70px">
                                </f:RenderField>
                                <f:RenderField HeaderText="总焊口" ColumnID="WeldJointCount" DataField="WeldJointCount" SortField="WeldJointCount"
                                    FieldType="Int" HeaderTextAlign="Center" TextAlign="Left" Width="70px">
                                </f:RenderField>
                                <f:RenderField HeaderText="完成总焊口" ColumnID="WeldJointCountT" DataField="WeldJointCountT" SortField="WeldJointCountT"
                                    FieldType="Int" HeaderTextAlign="Center" TextAlign="Left" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="合格数" ColumnID="CountS" DataField="CountS" SortField="CountS"
                                    FieldType="Int" HeaderTextAlign="Center" TextAlign="Left" Width="70px">
                                </f:RenderField>
                                <f:RenderField HeaderText="不合格数" ColumnID="CountU" DataField="CountU" SortField="CountU"
                                    FieldType="Int" HeaderTextAlign="Center" TextAlign="Left" Width="80px">
                                </f:RenderField>
                                <f:RenderField HeaderText="应检测比例" ColumnID="NDTR_Name" DataField="NDTR_Name" SortField="NDTR_Name"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="实际检测比例" ColumnID="Ratio" DataField="Ratio" SortField="Ratio"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="100px">
                                </f:RenderField>
                                <f:RenderField HeaderText="应检测比例值" ColumnID="NDTR_Rate" DataField="NDTR_Rate"
                                    FieldType="String" Hidden="true">
                                </f:RenderField>
                                <f:RenderField HeaderText="实际检测比例值" ColumnID="RatioC" DataField="RatioC"
                                    FieldType="String" Hidden="true">
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
                    <Items>
                        <f:Panel runat="server" ID="panel2" RegionPosition="Center" ShowBorder="true" Layout="VBox"
                            BodyPadding="2px" IconFont="PlusCircle" Title="试压前条件确认"
                            ShowHeader="true" AutoScroll="true" EnableCollapse="true" Collapsed="false" Height="200px">
                            <Items>
                                <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" Title="试压包" AutoScroll="true"
                                    EnableCollapse="true" Collapsed="false" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
                                    LabelAlign="Left">
                                    <Rows>
                                        <f:FormRow>
                                            <Items>
                                                <f:Label ID="txtInstallationSpecification" runat="server" Label="1.管道安装符合设计文件和规范要求"
                                                    LabelAlign="Right" LabelWidth="350px">
                                                </f:Label>
                                                <f:Label ID="txtPressureTest" runat="server" Label="2.管道耐压试验合格" LabelAlign="Right"
                                                    LabelWidth="350px">
                                                </f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:Label ID="txtWorkRecord" runat="server" Label="3.焊接工作记录齐全" LabelAlign="Right"
                                                    LabelWidth="350px">
                                                </f:Label>
                                                <f:Label ID="txtNDTConform" runat="server" Label="4.无损检测结果符合设计文件和规范要求"
                                                    LabelAlign="Right" LabelWidth="350px">
                                                </f:Label>
                                            </Items>
                                        </f:FormRow>

                                        <f:FormRow>
                                            <Items>
                                                <f:Label ID="txtHotConform" runat="server" Label="5.热处理结果符合设计文件和规范要求"
                                                    LabelAlign="Right" LabelWidth="350px">
                                                </f:Label>
                                                <f:Label ID="txtInstallationCorrectness" runat="server" Label="6.支、吊架安装正确" LabelAlign="Right"
                                                    LabelWidth="350px">
                                                </f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:Label ID="txtMarkClearly" runat="server" Label="7.合金钢管道材质标记清楚"
                                                    LabelAlign="Right" LabelWidth="350px">
                                                </f:Label>
                                                <f:Label ID="txtIsolationOpening" runat="server" Label="8.不参与管道系统试验的安全附件、仪表已按规定拆除或隔离，参与试压的系统内的阀门全部开启"
                                                    LabelAlign="Right" LabelWidth="350px">
                                                </f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:Label ID="txtConstructionPlanAsk" runat="server" Label="9.临时加固措施、盲板位置与标识符合施工方案要求"
                                                    LabelAlign="Right" LabelWidth="350px">
                                                </f:Label>
                                                <f:Label ID="txtCover" runat="server" Label="10.焊接接头及需要检验的部位未被覆盖" LabelAlign="Right"
                                                    LabelWidth="350px">
                                                </f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:Label ID="txtMeetRequirements" runat="server" Label="11.试压用压力表量程、精度等级、检定状态符合规范要求"
                                                    LabelAlign="Right" LabelWidth="350px">
                                                </f:Label>
                                                <f:Label ID="txtStainlessTestWater" runat="server" Label="12.不锈钢管道试验用水符合规范要求"
                                                    LabelAlign="Right" LabelWidth="350px">
                                                </f:Label>
                                            </Items>
                                        </f:FormRow>
                                    </Rows>
                                </f:Form>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="试压包维护" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
            Width="1000px" Height="660px">
        </f:Window>
        <f:Window ID="Window2" Title="试压包打印" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Top" EnableResize="true" runat="server"
            IsModal="true" Width="900px" Height="600px">
        </f:Window>
        <f:Window ID="Window3" Title="管线对应焊口详细" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Top" EnableResize="true" runat="server"
            IsModal="true" Width="1000px" Height="520px">
        </f:Window>
    </form>
    <script type="text/javascript">
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            // F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>

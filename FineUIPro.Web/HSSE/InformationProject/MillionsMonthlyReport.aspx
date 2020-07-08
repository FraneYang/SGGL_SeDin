<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MillionsMonthlyReport.aspx.cs"
    Inherits="FineUIPro.Web.InformationProject.MillionsMonthlyReport" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>百万工时安全统计月报</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="百万工时安全统计月报" EnableCollapse="true"
                runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="MillionsMonthlyReportId"
                AllowCellEditing="true" ClicksToEdit="2" DataIDField="MillionsMonthlyReportId"
                AllowSorting="true" SortField="CompileDate" SortDirection="DESC" OnSort="Grid1_Sort"
                AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:DropDownList ID="drpYear" runat="server" Label="年份" LabelAlign="Right" Width="250px"
                                AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                            </f:DropDownList>
                            <f:DropDownList ID="drpMonth" runat="server" Label="月度" LabelAlign="Right" Width="250px"
                                AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                            </f:DropDownList>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" Hidden="true"
                                runat="server">
                            </f:Button>                         
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="40px" HeaderTextAlign="Center"
                        TextAlign="Center" />
                    <f:RenderField Width="100px" ColumnID="YearAndMonth" DataField="YearAndMonth" SortField="YearAndMonth"
                        FieldType="String" HeaderText="月报日期" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                   <%-- <f:RenderField Width="150px" ColumnID="Affiliation" DataField="Affiliation" SortField="Affiliation"
                        FieldType="String" HeaderText="所属单位" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="Name" DataField="Name" SortField="Name" FieldType="String"
                        HeaderText="名称" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>--%>
                    <f:GroupField EnableLock="true" HeaderText="员工总数" TextAlign="Center">
                        <Columns>
                            <f:RenderField Width="80px" ColumnID="PostPersonNum" DataField="PostPersonNum" FieldType="String"
                                HeaderText="在岗员工" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="SnapPersonNum" DataField="SnapPersonNum" FieldType="String"
                                HeaderText="临时员工" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="70px" ColumnID="ContractorNum" DataField="ContractorNum" FieldType="String"
                                HeaderText="承包商" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="70px" ColumnID="SumPersonNum" DataField="SumPersonNum" FieldType="String"
                                HeaderText="合计" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                        </Columns>
                    </f:GroupField>
                    <f:RenderField Width="120px" ColumnID="TotalWorkNum" DataField="TotalWorkNum" FieldType="String"
                        HeaderText="总工时数（万）" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:GroupField EnableLock="true" HeaderText="总可记录事件" TextAlign="Center">
                        <Columns>
                            <f:GroupField EnableLock="true" HeaderText="重伤事故" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="70px" ColumnID="SeriousInjuriesNum" DataField="SeriousInjuriesNum"
                                        FieldType="String" HeaderText="起数" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="70px" ColumnID="SeriousInjuriesPersonNum" DataField="SeriousInjuriesPersonNum"
                                        FieldType="String" HeaderText="人数" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="SeriousInjuriesLossHour" DataField="SeriousInjuriesLossHour"
                                        FieldType="String" HeaderText="损失工时" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="轻伤事故" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="70px" ColumnID="MinorAccidentNum" DataField="MinorAccidentNum"
                                        FieldType="String" HeaderText="起数" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="70px" ColumnID="MinorAccidentPersonNum" DataField="MinorAccidentPersonNum"
                                        FieldType="String" HeaderText="人数" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="MinorAccidentLossHour" DataField="MinorAccidentLossHour"
                                        FieldType="String" HeaderText="损失工时" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="其它事故" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="70px" ColumnID="OtherAccidentNum" DataField="OtherAccidentNum"
                                        FieldType="String" HeaderText="起数" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="70px" ColumnID="OtherAccidentPersonNum" DataField="OtherAccidentPersonNum"
                                        FieldType="String" HeaderText="人数" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="OtherAccidentLossHour" DataField="OtherAccidentLossHour"
                                        FieldType="String" HeaderText="损失工时" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="工作受限" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="70px" ColumnID="RestrictedWorkPersonNum" DataField="RestrictedWorkPersonNum"
                                        FieldType="String" HeaderText="人数" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="RestrictedWorkLossHour" DataField="RestrictedWorkLossHour"
                                        FieldType="String" HeaderText="损失工时" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="医疗处置" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="70px" ColumnID="MedicalTreatmentPersonNum" DataField="MedicalTreatmentPersonNum"
                                        FieldType="String" HeaderText="人数" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="MedicalTreatmentLossHour" DataField="MedicalTreatmentLossHour"
                                        FieldType="String" HeaderText="损失工时" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                        </Columns>
                    </f:GroupField>
                    <f:GroupField EnableLock="true" HeaderText="无伤害事故" TextAlign="Center">
                        <Columns>
                            <f:GroupField EnableLock="true" HeaderText="火灾" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="70px" ColumnID="FireNum" DataField="FireNum" FieldType="String"
                                        HeaderText="起数" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="爆炸" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="70px" ColumnID="ExplosionNum" DataField="ExplosionNum" FieldType="String"
                                        HeaderText="起数" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="交通" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="70px" ColumnID="TrafficNum" DataField="TrafficNum" FieldType="String"
                                        HeaderText="起数" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="机械设备" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="80px" ColumnID="EquipmentNum" DataField="EquipmentNum" FieldType="String"
                                        HeaderText="起数" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="质量" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="70px" ColumnID="QualityNum" DataField="QualityNum" FieldType="String"
                                        HeaderText="起数" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="其它" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="70px" ColumnID="OtherNum" DataField="OtherNum" FieldType="String"
                                        HeaderText="起数" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                        </Columns>
                    </f:GroupField>
                    <f:GroupField EnableLock="true" HeaderText="急救包扎" TextAlign="Center">
                        <Columns>
                            <f:RenderField Width="80px" ColumnID="FirstAidDressingsNum" DataField="FirstAidDressingsNum"
                                FieldType="String" HeaderText="起数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                        </Columns>
                    </f:GroupField>
                    <f:GroupField EnableLock="true" HeaderText="未遂事件" TextAlign="Center">
                        <Columns>
                            <f:RenderField Width="80px" ColumnID="AttemptedEventNum" DataField="AttemptedEventNum"
                                FieldType="String" HeaderText="起数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                        </Columns>
                    </f:GroupField>
                    <f:RenderField Width="80px" ColumnID="LossDayNum" DataField="LossDayNum" FieldType="String"
                        HeaderText="损失工日" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="FlowOperateName" DataField="FlowOperateName"
                        SortField="FlowOperateName" FieldType="String" HeaderText="状态" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>                    
                    <f:TemplateField HeaderText="打印" Width="80px">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnPrint" runat="server" Text="打印" OnClick="btnPrint_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </f:TemplateField>
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
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="编辑百万工时安全统计月报表" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="1100px"
        Height="560px">
    </f:Window>
    <f:Window ID="Window2" IconUrl="~/res/images/16/11.png" runat="server" Hidden="true"
        IsModal="false" Target="Parent" EnableMaximize="true" EnableResize="true"
        Title="打印百万工时安全统计月报表" CloseAction="HidePostBack" EnableIFrame="true" Height="768px"
        Width="1024px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="TableEdit" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server"
            Text="删除">
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

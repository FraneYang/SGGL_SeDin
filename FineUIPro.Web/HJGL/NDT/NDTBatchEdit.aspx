<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NDTBatchEdit.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.NDT.NDTBatchEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑检测单</title>
    <style type="text/css">
        .customlabel span
        {
            color: red;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="检测单"
                TitleToolTip="检测单" AutoScroll="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:HiddenField runat="server" ID="hdItemsString">
                            </f:HiddenField>
                            <f:HiddenField runat="server" ID="hdTablerId">
                            </f:HiddenField>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnSave" ToolTip="保存" Icon="SystemSave" runat="server"
                                OnClick="btnSave_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                    <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtNDECode" Label="检测流水号" ShowRedStar="true"
                                        Required="true" runat="server" LabelWidth="130px" FocusOnPageLoad="true">
                                    </f:TextBox>
                                    <f:DropDownList ID="drpUnit" Label="单位名称" runat="server"
                                        ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="130px" AutoPostBack="true"
                                        OnSelectedIndexChanged="drpUnit_SelectedIndexChanged">
                                    </f:DropDownList>
                                    <f:DropDownList ID="drpUnitWork" runat="server" Label="单位工程名称"
                                        LabelAlign="Right" EnableEdit="true" ShowRedStar="true" Required="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="drpInstallation_SelectedIndexChanged" LabelWidth="140px">
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:DatePicker ID="txtNDEDate" Label="检测日期" runat="server"
                                        DateFormatString="yyyy-MM-dd" ShowRedStar="true" Required="true" LabelWidth="130px">
                                    </f:DatePicker>
                                    <f:DropDownList ID="drpNDEUnit" Label="检测单位" runat="server"
                                        ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="130px">
                                    </f:DropDownList>
                                    <f:DropDownList ID="drpDetectionType" Label="检测方法" runat="server"
                                        EnableEdit="true" LabelWidth="140px" ShowRedStar="true" Required="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="drpDetectionType_SelectedIndexChanged">
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="67% 33%">
                                <Items>
                                    <f:TextBox ID="txtPipelineCode" Label="管线号" runat="server"
                                        LabelWidth="130px" AutoPostBack="true" OnTextChanged="txtPipelineCode_TextChanged">
                                    </f:TextBox>
                                    <f:DropDownList ID="drpBatchTrust" Label="委托单号"
                                        runat="server" ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="140px"
                                        AutoPostBack="true" OnSelectedIndexChanged="drpBatchTrust_SelectedIndexChanged">
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="检测单"
                        EnableCollapse="true" runat="server" BoxFlex="1" DataKeyNames="NDEItemID" EnableColumnLines="true"
                        AllowCellEditing="true" ClicksToEdit="1" DataIDField="NDEItemID" 
                        SortField="PipelineCode,WeldJointCode" SortDirection="ASC"
                        AllowPaging="false" IsDatabasePaging="true" PageSize="10000" EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                                <Items>
                                    <f:CheckBox runat="server" ID="ckAllFilmDate" Label="是否批量填充检测日期"
                                        LabelWidth="160px" Checked="true">
                                    </f:CheckBox>
                                    <f:CheckBox runat="server" ID="ckAllReportDate" Label="是否批量填充报告日期"
                                        LabelWidth="160px" Checked="true">
                                    </f:CheckBox>
                                    <f:TextBox runat="server" ID="changeFilmDate" Hidden="true">
                                    </f:TextBox>
                                    <f:TextBox runat="server" ID="changeReportDate" Hidden="true">
                                    </f:TextBox>
                                    <f:TextBox runat="server" ID="changeId" Hidden="true">
                                    </f:TextBox>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:CheckBoxField ColumnID="ckbIsSelected" Width="50px" RenderAsStaticField="false"
                                HeaderText="选择" AutoPostBack="true" CommandName="IsSelected"
                                HeaderTextAlign="Center" />
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号"
                                Width="50px" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:RenderField HeaderText="管线号" ColumnID="PipelineCode"
                                DataField="PipelineCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="180px">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊口号" ColumnID="WeldJointCode"
                                DataField="WeldJointCode"  FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="70px">
                            </f:RenderField>
                            <f:RenderField HeaderText="单位工程" ColumnID="UnitWorkCode" DataField="UnitWorkCode"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊工号" ColumnID="WelderCode"
                                DataField="WelderCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="检测日期" ColumnID="FilmDate" DataField="FilmDate"
                                FieldType="Date" Renderer="Date" HeaderTextAlign="Center"
                                TextAlign="Left" RendererArgument="yyyy-MM-dd" Width="100px">
                                <Editor>
                                    <f:DatePicker ID="txtFilmDate" runat="server" DateFormatString="yyyy-MM-dd" Required="true">
                                    </f:DatePicker>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField HeaderText="报告日期" ColumnID="ReportDate"
                                DataField="ReportDate"  FieldType="Date" Renderer="Date"
                                HeaderTextAlign="Center" TextAlign="Left" RendererArgument="yyyy-MM-dd" Width="100px">
                                <Editor>
                                    <f:DatePicker ID="txtReportDate" runat="server" DateFormatString="yyyy-MM-dd" Required="true">
                                    </f:DatePicker>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField HeaderText="检测总数" ColumnID="TotalFilm" DataField="TotalFilm"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="100px">
                                <Editor>
                                    <f:NumberBox ID="txtTotalFilm" NoDecimal="true" NoNegative="true" runat="server">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField HeaderText="合格数" ColumnID="PassFilm" DataField="PassFilm"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="100px">
                                <Editor>
                                    <f:NumberBox ID="txtPassFilm" NoDecimal="true" NoNegative="true" runat="server">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField HeaderText="是否合格" ColumnID="CheckResultStr"
                                DataField="CheckResultStr" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="100px">
                                <Editor>
                                    <f:TextBox runat="server" ID="txtCheckResult" Readonly="true">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField HeaderText="评定级别" ColumnID="JudgeGrade"
                                DataField="JudgeGrade"  FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="100px">
                                <Editor>
                                    <f:DropDownList ID="drpJudgeGrade" runat="server">
                                    </f:DropDownList>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField HeaderText="缺陷" ColumnID="CheckDefects"
                                DataField="CheckDefects" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="120px">
                                <Editor>
                                    <f:DropDownList ID="drpCheckDefects" runat="server" EnableMultiSelect="true">
                                    </f:DropDownList>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField HeaderText="返修位置" ColumnID="RepairLocation"
                                DataField="RepairLocation"  FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="100px">
                                <Editor>
                                    <f:TextBox runat="server" ID="txtRepairLocation">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField HeaderText="探伤报告编号" ColumnID="NDEReportNo"
                                DataField="NDEReportNo"  FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="200px">
                                <Editor>
                                    <f:TextBox runat="server" ID="txtNDEReportNo">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField HeaderText="备注" ColumnID="Remark" DataField="Remark"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="100px">
                                <Editor>
                                    <f:TextBox runat="server" ID="txtRemark">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField HeaderText="NDEItemID" ColumnID="NDEItemID" DataField="NDEItemID"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="100px" Hidden="true">
                            </f:RenderField>
                            <f:RenderField HeaderText="TrustBatchItemId" ColumnID="TrustBatchItemId" DataField="TrustBatchItemId"
                               FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="100px" Hidden="true">
                            </f:RenderField>
                            <f:RenderField HeaderText="SubmitDate" ColumnID="SubmitDate" DataField="SubmitDate"
                                FieldType="Date" Renderer="Date" HeaderTextAlign="Center"
                                TextAlign="Left" Width="90px" Hidden="true">
                            </f:RenderField>
                            <f:RenderField Width="50px" ColumnID="ChangeId" DataField="Remark"
                         FieldType="String" HeaderText="ChangeId" Hidden="true" HeaderTextAlign="Center" TextAlign="Left">
                     </f:RenderField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="afteredit" Handler="onGridAfterEdit" />
                        </Listeners>
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    </form>
    <script type="text/javascript">
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
        var gridClientID = '<%= Grid1.ClientID %>';
        function onGridAfterEdit(event, value, params) {
            var ckAllFilmDate = F('<%= ckAllFilmDate.ClientID %>');
            var ckAllReportDate = F('<%= ckAllReportDate.ClientID %>');
            var changeFilmDate = F('<%= changeFilmDate.ClientID %>');
            var changeReportDate = F('<%= changeReportDate.ClientID %>');
            var changeId = F('<%= changeId.ClientID %>');
            var me = this, columnId = params.columnId, rowId = params.rowId;
            changeId.setValue(rowId);
            if (columnId === 'TotalFilm' || columnId === 'PassFilm') {
                var total = me.getCellValue(rowId, 'TotalFilm');
                var pass = me.getCellValue(rowId, 'PassFilm');

                if (columnId === 'TotalFilm' && (isNaN(pass) || pass == "") && !isNaN(total)) {
                    me.updateCellValue(rowId, 'PassFilm', total);
                    me.updateCellValue(rowId, 'CheckResultStr', '合格');
                }
                else {
                    if (total == pass) {
                        me.updateCellValue(rowId, 'CheckResultStr', '合格');
                    } else {
                        me.updateCellValue(rowId, 'CheckResultStr', '不合格');
                    }
                }
            }
            else if (columnId === 'FilmDate' && ckAllFilmDate.getValue()) {
                var filmDate = me.getCellValue(rowId, 'FilmDate');
                changeFilmDate.setValue(filmDate.toString());
                // 回发到后台更新
                __doPostBack('', 'UPDATEDate');
            }
            else if (columnId === 'ReportDate' && ckAllReportDate.getValue()) {
                var filmDate = me.getCellValue(rowId, 'ReportDate');
                changeReportDate.setValue(filmDate.toString());
                // 回发到后台更新
                __doPostBack('', 'UPDATEDate');
            }
        }

        function resolveRows(rowId, columnId, newValue) {
            var grid = F(gridClientID);
            grid.getRowEls().each(function () {
                grid.updateCellValue(this, columnId, newValue);
            });
        }
    </script>
</body>
</html>

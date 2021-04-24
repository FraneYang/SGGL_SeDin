<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NDTBatchAudit.aspx.cs" Inherits="FineUIPro.Web.HJGL.NDT.NDTBatchAudit" %>

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
                            <f:HiddenField runat="server" ID="hdDetectionType"></f:HiddenField>
                            <f:Button ID="btnSave" ToolTip="审核" Icon="TableKey" runat="server"
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
                                    <f:TextBox ID="txtNDECode" Label="检测流水号" 
                                        runat="server" LabelWidth="130px" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtUnit" Label="单位名称" 
                                        runat="server" LabelWidth="130px" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtUnitWork" Label="单位工程名称" 
                                        runat="server" LabelWidth="130px" Readonly="true">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtNDEUnit" Label="检测单位" 
                                        runat="server" LabelWidth="130px" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtDetectionType" Label="检测方法" 
                                        runat="server" LabelWidth="130px" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtTrustCode" Label="委托单号" 
                                        runat="server" LabelWidth="130px" Readonly="true">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="检测单"
                        EnableCollapse="true" runat="server" BoxFlex="1" DataKeyNames="NDEItemID" EnableColumnLines="true"
                        DataIDField="NDEItemID" 
                        SortField="PipelineCode,WeldJointCode" SortDirection="ASC"
                        AllowPaging="false" IsDatabasePaging="true" PageSize="10000" EnableTextSelection="True">
                        <Columns>
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
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Hidden="true"
                                Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊工号" ColumnID="WelderCode"
                                DataField="WelderCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="检测日期" ColumnID="FilmDate" DataField="FilmDate"
                                FieldType="Date" Renderer="Date" HeaderTextAlign="Center"
                                TextAlign="Left" RendererArgument="yyyy-MM-dd" Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="报告日期" ColumnID="ReportDate"
                                DataField="ReportDate"  FieldType="Date" Renderer="Date"
                                HeaderTextAlign="Center" TextAlign="Left" RendererArgument="yyyy-MM-dd" Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="检测总数" ColumnID="TotalFilm" DataField="TotalFilm"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="合格数" ColumnID="PassFilm" DataField="PassFilm"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="是否合格" ColumnID="CheckResultStr"
                                DataField="CheckResultStr" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="评定级别" ColumnID="JudgeGrade"
                                DataField="JudgeGrade"  FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="100px">
                            </f:RenderField>
                            <f:TemplateField Width="150px" HeaderText="缺陷" HeaderTextAlign="Center"
                                TextAlign="Center" SortField="CheckDefects">
                                <ItemTemplate>
                                    <asp:Label ID="lbCheckDefects" runat="server" Text='<%# ConvertCheckDefects(Eval("CheckDefects")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField HeaderText="返修位置" ColumnID="RepairLocation"
                                DataField="RepairLocation"  FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="探伤报告编号" ColumnID="NDEReportNo" Hidden="true"
                                DataField="NDEReportNo"  FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="200px">
                            </f:RenderField>
                            <f:RenderField HeaderText="备注" ColumnID="Remark" DataField="Remark"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="100px">
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
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HardReportEdit.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.HotProcessHard.HardReportEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑硬度报告</title>
    <base target="_self" />
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
            <Items>
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                    Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="硬度委托"
                    TitleToolTip="硬度委托" AutoScroll="true">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:HiddenField runat="server" ID="hdItemsString">
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
                                        <f:TextBox ID="txtHardTrustNo" Label="委托单号" runat="server" LabelWidth="190px" Readonly="true">
                                        </f:TextBox>
                                        <f:DropDownList ID="drpHardTrustUnit" Label="委托单位" runat="server" EnableEdit="true" LabelWidth="190px" Readonly="true">
                                        </f:DropDownList>
                                        <f:DropDownList ID="drpUnitWork" runat="server" Label="单位工程名称"
                                            LabelAlign="Right" LabelWidth="190px" EnableEdit="true" Readonly="true">
                                        </f:DropDownList>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:DropDownList ID="drpCheckUnit" Label="检测单位" runat="server"
                                            ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="190px" Readonly="true">
                                        </f:DropDownList>
                                        <f:DropDownList ID="drpHardTrustMan" Label="委托人" runat="server"
                                            ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="190px" Readonly="true">
                                        </f:DropDownList>
                                        <f:DatePicker ID="txtHardTrustDate" Label="委托日期" runat="server"
                                            ShowRedStar="true" Required="true" LabelWidth="190px" DateFormatString="yyyy-MM-dd" Readonly="true">
                                        </f:DatePicker>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="txtCheckName" Label="检件名称" runat="server"
                                            LabelWidth="190px">
                                        </f:TextBox>
                                        <f:TextBox ID="txtHardnessRate" Label="检测比例" runat="server"
                                            LabelWidth="190px" Readonly="true">
                                        </f:TextBox>
                                        <f:TextBox ID="txtStandards" Label="检测标准" runat="server"
                                            LabelWidth="190px" Readonly="true">
                                        </f:TextBox>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="txtAcceptStandard" Label="验收标准" runat="server"
                                            LabelWidth="190px">
                                        </f:TextBox>
                                        <f:TextBox ID="txtCheckNum" Label="外观检查合格焊口数" runat="server"
                                            LabelWidth="190px" Readonly="true">
                                        </f:TextBox>
                                        <f:TextBox ID="txtTestWeldNum" Label="委托检测焊口数" runat="server"
                                            LabelWidth="190px" Readonly="true">
                                        </f:TextBox>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ColumnWidths="67% 33%">
                                    <Items>
                                        <f:RadioButtonList runat="server" ID="rblDetectionTime" Label="检测时机"
                                            LabelWidth="190px" Readonly="true">
                                            <f:RadioItem Value="0" Text="工厂化预制焊口" />
                                            <f:RadioItem Value="1" Text="安装施工焊口" />
                                        </f:RadioButtonList>
                                        <f:DropDownList ID="drpSendee" Label="接收人" runat="server" EnableEdit="true"
                                            LabelWidth="190px" Readonly="true">
                                        </f:DropDownList>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="txtInspectionNum" Label="报告编号" runat="server"
                                            LabelWidth="190px">
                                        </f:TextBox>
                                        <f:TextBox ID="txtHardnessMethod" Label="检测方法" runat="server"
                                            LabelWidth="190px">
                                        </f:TextBox>
                                        <f:TextBox ID="txtEquipmentModel" Label="设备型号" runat="server"
                                            LabelWidth="190px">
                                        </f:TextBox>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="硬度委托明细" EnableCollapse="true"
                            runat="server" BoxFlex="1" DataKeyNames="HardTrustItemID" AllowCellEditing="true"
                            AllowColumnLocking="true" EnableColumnLines="true" ClicksToEdit="2" DataIDField="HardTrustItemID"
                            AllowSorting="true" SortField="WeldJointCode" SortDirection="ASC" OnSort="Grid1_Sort"
                            AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                            EnableTextSelection="True" ForceFit="true">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar3" Position="Top" runat="server" ToolbarAlign="Right">
                                    <Items>
                                        <f:TextBox ID="txtPipelineCode" runat="server" Label="管线号"
                                            EmptyText="输入查询条件" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"
                                            Width="290px" LabelWidth="120px" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:TextBox ID="txtWeldJointCode" runat="server" Label="总焊口量"
                                            EmptyText="输入查询条件" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"
                                            Width="360px" LabelWidth="160px" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                        </f:ToolbarFill>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true" HeaderText="序号"
                                    Width="50px" HeaderTextAlign="Center" TextAlign="Center" />
                                <f:RenderField HeaderText="管线号" ColumnID="PipelineCode"
                                    DataField="PipelineCode" SortField="PipelineCode" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="180px">
                                </f:RenderField>
                                <f:RenderField HeaderText="焊口号" ColumnID="WeldJointCode"
                                    DataField="WeldJointCode" SortField="WeldJointCode" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="100px">
                                </f:RenderField>
                                <f:RenderField HeaderText="焊工号" ColumnID="WelderCode"
                                    DataField="WelderCode" SortField="WelderCode" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="规格" ColumnID="Specification"
                                    DataField="Specification" SortField="Specification" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="材质" ColumnID="MaterialCode"
                                    DataField="MaterialCode" SortField="MaterialCode" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="备注" ColumnID="Remark" DataField="Remark"
                                    SortField="Remark" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                    Width="90px" ExpandUnusedSpace="true">
                                </f:RenderField>
                            </Columns>
                            <PageItems>
                                <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                </f:ToolbarSeparator>
                                <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                                </f:ToolbarText>
                                <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true"
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
        <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Parent" EnableResize="true" runat="server"
            IsModal="true" Width="1000px" Height="560px">
        </f:Window>
        <f:Window ID="WindowHardReport" Title="硬度报告" Hidden="true"
            EnableIFrame="true" EnableMaximize="true" Target="Top" EnableResize="true" runat="server"
            IsModal="true" Width="1000px" Height="520px">
        </f:Window>
    </form>
</body>
</html>

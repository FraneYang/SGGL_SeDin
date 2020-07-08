<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CostManageEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.CostGoods.CostManageEdit" ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑安全费用管理</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .customlabel span
        {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCostManageCode" runat="server" Label="编号" LabelAlign="Right" MaxLength="50"
                        Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtCostManageName" runat="server" Label="名称" LabelAlign="Right" MaxLength="50">
                    </f:TextBox>
                    <f:DropDownList ID="drpUnitId" runat="server" Label="分包商" Required="true" ShowRedStar="true"
                        LabelAlign="Right">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtContractNum" runat="server" Label="合同号" LabelAlign="Right" MaxLength="50">
                    </f:TextBox>
                    <f:DatePicker ID="txtCostManageDate" runat="server" Label="日期" EnableEdit="true">
                    </f:DatePicker>
                    <f:Label ID="Label1" runat="server">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="投入费用明细" EnableCollapse="true"
                        runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="CostManageItemId"
                        AllowCellEditing="true" ClicksToEdit="1" DataIDField="CostManageItemId" PageSize="100" OnPageIndexChange="Grid1_PageIndexChange" EnableSummary="true" SummaryPosition="Flow"
                        EnableTextSelection="True" OnRowCommand="gvMonthPlan_RowCommand" Height="300px">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                                <Items>
                                    <f:Label ID="lblT" runat="server" Text="投入费用明细" CssClass="customlabel span">
                                    </f:Label>
                                    <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                    </f:ToolbarFill>
                                    <f:Button ID="btnNew" Icon="Add" runat="server" ToolTip="新增投入费用明细" ValidateForms="SimpleForm1"
                                        OnClick="btnNew_Click">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Parent" CommandName="Delete"
                                ToolTip="删除" Icon="Delete" TextAlign="Center" />
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                TextAlign="Center" />
                            <f:RenderField HeaderText="投入费用项目" ColumnID="InvestCostProject" DataField="InvestCostProject"
                                SortField="InvestCostProject" HeaderTextAlign="Center" TextAlign="Center" Width="120px"
                                FieldType="String">
                                <Editor>
                                    <f:DropDownList ID="drpInvestCostProject" runat="server" EnableEdit="true">
                                    </f:DropDownList>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField HeaderText="费用使用原因" ColumnID="UseReason" DataField="UseReason" SortField="UseReason"
                                HeaderTextAlign="Center" TextAlign="Center" Width="150px" FieldType="String" ExpandUnusedSpace="true">
                                <Editor>
                                    <f:TextBox runat="server" ID="txtUseReason" MaxLength="50">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField HeaderText="数量" ColumnID="Counts" DataField="Counts" SortField="Counts"
                                HeaderTextAlign="Center" TextAlign="Center" Width="100px" FieldType="Int">
                                <Editor>
                                    <f:NumberBox ID="txtCounts" runat="server" NoNegative="true" NoDecimal="true">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField HeaderText="单价(元)" ColumnID="PriceMoney" DataField="PriceMoney" SortField="PriceMoney"
                                HeaderTextAlign="Center" TextAlign="Center" Width="100px" FieldType="Double">
                                <Editor>
                                    <f:NumberBox ID="txtPriceMoney" runat="server" NoNegative="true" NoDecimal="false">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField HeaderText="总价(元)" ColumnID="TotalMoney" DataField="TotalMoney" SortField="TotalMoney"
                                HeaderTextAlign="Center" TextAlign="Center" Width="100px" FieldType="Double">
                            </f:RenderField>
                            <f:RenderField HeaderText="审核数量" ColumnID="AuditCounts" DataField="AuditCounts" SortField="AuditCounts"
                                HeaderTextAlign="Center" TextAlign="Center" Width="100px" FieldType="Int">
                                <Editor>
                                    <f:NumberBox ID="txtAuditCounts" runat="server" NoNegative="true" NoDecimal="true">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField HeaderText="审核单价(元)" ColumnID="AuditPriceMoney" DataField="AuditPriceMoney"
                                SortField="AuditPriceMoney" HeaderTextAlign="Center" TextAlign="Center" Width="110px"
                                FieldType="Double">
                                <Editor>
                                    <f:NumberBox ID="txtAuditPriceMoney" runat="server" NoNegative="true" NoDecimal="false">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField HeaderText="审核总价(元)" ColumnID="AuditTotalMoney" DataField="AuditTotalMoney"
                                SortField="AuditTotalMoney" HeaderTextAlign="Center" TextAlign="Center" Width="110px"
                                FieldType="Double">
                            </f:RenderField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="afteredit" Handler="onGridAfterEdit" />
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
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtOpinion" runat="server" Label="分包负责人意见" LabelAlign="Right" MaxLength="500"
                        LabelWidth="140px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSubHSE" runat="server" Label="分包HSE经理" LabelAlign="Right" MaxLength="50"
                        LabelWidth="140px">
                    </f:TextBox>
                    <f:TextBox ID="txtSubCN" runat="server" Label="分包施工经理" LabelAlign="Right" MaxLength="50"
                        LabelWidth="140px">
                    </f:TextBox>
                    <f:TextBox ID="txtSubProject" runat="server" Label="分包项目经理" LabelAlign="Right" MaxLength="50"
                        LabelWidth="140px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true"
                        BodyPadding="0px">
                        <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp">
                    </f:Label>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                        OnClick="btnSubmit_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="850px" Height="420px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
    <script type="text/javascript">
        function onGridAfterEdit(event, value, params) {
            var me = this, columnId = params.columnId, rowId = params.rowId;
            var change = false;
            if (columnId === 'Counts') {
                var counts = me.getCellValue(rowId, 'Counts');
                var priceMoney = me.getCellValue(rowId, 'PriceMoney');
                if (counts.toString() == "" || priceMoney.toString() == "") {
                    me.updateCellValue(rowId, 'TotalMoney', 0);
                }
                else {
                    me.updateCellValue(rowId, 'TotalMoney', (counts * priceMoney).toFixed(2));
                }
                if (counts.toString() != "" && priceMoney.toString() != "") {
                    change = true;
                }
            }
            if (columnId === 'PriceMoney') {
                var counts = me.getCellValue(rowId, 'Counts');
                var priceMoney = me.getCellValue(rowId, 'PriceMoney');
                if (counts.toString() == "" || priceMoney.toString() == "") {
                    me.updateCellValue(rowId, 'TotalMoney', 0);
                }
                else {
                    me.updateCellValue(rowId, 'TotalMoney', (counts * priceMoney).toFixed(2));
                }
                if (counts.toString() != "" && priceMoney.toString() != "") {
                    change = true;
                }
            }
            if (columnId === 'AuditCounts') {
                var auditCounts = me.getCellValue(rowId, 'AuditCounts');
                var auditPriceMoney = me.getCellValue(rowId, 'AuditPriceMoney');
                if (auditCounts.toString() == "" || auditPriceMoney.toString() == "") {
                    me.updateCellValue(rowId, 'AuditTotalMoney', 0);
                }
                else {
                    me.updateCellValue(rowId, 'AuditTotalMoney', (auditCounts * auditPriceMoney).toFixed(2));
                }
                if (auditCounts.toString() != "" && auditPriceMoney.toString() != "") {
                    change = true;
                }
            }
            if (columnId === 'AuditPriceMoney') {
                var auditCounts = me.getCellValue(rowId, 'AuditCounts');
                var auditPriceMoney = me.getCellValue(rowId, 'AuditPriceMoney');
                if (auditCounts.toString() == "" || auditPriceMoney.toString() == "") {
                    me.updateCellValue(rowId, 'AuditTotalMoney', 0);
                }
                else {
                    me.updateCellValue(rowId, 'AuditTotalMoney', (auditCounts * auditPriceMoney).toFixed(2));
                }
                if (auditCounts.toString() != "" && auditPriceMoney.toString() != "") {
                    change = true;
                }
            }
            if (change == true) {
                updateSummary();
            }
        }

        function updateSummary() {
            // 回发到后台更新
            __doPostBack('', 'UPDATE_SUMMARY');
        }

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>

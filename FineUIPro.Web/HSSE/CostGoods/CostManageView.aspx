<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CostManageView.aspx.cs" Inherits="FineUIPro.Web.HSSE.CostGoods.CostManageView"  ValidateRequest="false" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查看安全费用管理</title>
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
                    <f:TextBox ID="txtCostManageCode" runat="server" Label="编号" LabelAlign="Right" 
                        Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtCostManageName" runat="server" Label="名称" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtUnitName" runat="server" Label="分包商" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtContractNum" runat="server" Label="合同号" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                <f:TextBox ID="txtCostManageDate" runat="server" Label="日期" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:Label ID="Label1" runat="server">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="投入费用明细" EnableCollapse="true"
                        runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="CostManageItemId"
                        AllowCellEditing="true" ClicksToEdit="2" DataIDField="CostManageItemId" PageSize="100" OnPageIndexChange="Grid1_PageIndexChange" EnableSummary="true" SummaryPosition="Flow"
                        EnableTextSelection="True"
                        Height="220px">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                                <Items>
                                    <f:Label ID="lblT" runat="server" Text="投入费用明细" CssClass="customlabel span">
                                    </f:Label>
                                    <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                    </f:ToolbarFill>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                TextAlign="Center" />
                            <f:RenderField Width="120px" ColumnID="InvestCostProject" DataField="InvestCostProject"
                                SortField="InvestCostProject" FieldType="String" HeaderText="投入费用项目" HeaderTextAlign="Center"
                                TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="UseReason" DataField="UseReason" SortField="UseReason"
                                FieldType="String" HeaderText="费用使用原因" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="Counts" DataField="Counts" SortField="Counts"
                                FieldType="Int" HeaderText="数量" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="PriceMoney" DataField="PriceMoney" SortField="PriceMoney"
                                FieldType="Float" HeaderText="单价（元）" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:TemplateField HeaderText="总价（元）" Width="120px" HeaderTextAlign="Center" TextAlign="Left" ColumnID="TotalMoney">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalMoney" runat="server" Text='<%# GetTotalMoney(Eval("CostManageItemId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="100px" ColumnID="AuditCounts" DataField="AuditCounts" SortField="AuditCounts"
                                FieldType="Int" HeaderText="数量" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="AuditPriceMoney" DataField="AuditPriceMoney" SortField="AuditPriceMoney"
                                FieldType="Float" HeaderText="单价（元）" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:TemplateField HeaderText="总价（元）" Width="120px" HeaderTextAlign="Center" TextAlign="Left" ColumnID="AuditTotalMoney">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# GetAuditTotalMoney(Eval("CostManageItemId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                        </Columns>
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
                    <f:TextArea ID="txtOpinion" runat="server" Label="分包商负责人意见" LabelAlign="Right" Readonly="true" LabelWidth="140px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSubHSE" runat="server" Label="分包商HSE经理" LabelAlign="Right" Readonly="true" LabelWidth="140px">
                    </f:TextBox>
                    <f:TextBox ID="txtSubCN" runat="server" Label="分包商施工经理" LabelAlign="Right" Readonly="true" LabelWidth="140px">
                    </f:TextBox>
                    <f:TextBox ID="txtSubProject" runat="server" Label="分包商项目经理" LabelAlign="Right" Readonly="true" LabelWidth="140px">
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
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>

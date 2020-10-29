<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EquipmentOutView.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.InApproveManager.EquipmentOutView" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查看特种设备机具出场报批</title>
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
                    <f:TextBox ID="txtEquipmentOutCode" runat="server" Label="设备出场编号" LabelAlign="Right"
                        Readonly="true">
                    </f:TextBox>
                    <f:Label ID="lblEmpty" runat="server">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtUnitName" runat="server" Label="申请单位" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtApplicationDate" runat="server" Label="申请日期" LabelAlign="Right"
                        Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCarNum" runat="server" Label="车牌号" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtCarModel" runat="server" Label="车型" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtDriverName" runat="server" Label="驾驶员姓名" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtDriverNum" runat="server" Label="驾驶证号" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 3% 35% 3% 35% 14%">
                <Items>
                    <f:Label ID="lblS" runat="server" Label="运送地点">
                    </f:Label>
                    <f:Label ID="Label2" runat="server" Text="起">
                    </f:Label>
                    <f:TextBox ID="txtTransPortStart" runat="server" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:Label ID="Label1" runat="server" Text="至">
                    </f:Label>
                    <f:TextBox ID="txtTransPortEnd" runat="server" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:Label ID="Label3" runat="server">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="出场机具设备清单" EnableCollapse="true"
                        runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="EquipmentOutItemId"
                        DataIDField="EquipmentOutItemId" AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                        EnableTextSelection="True" Height="230px">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                                <Items>
                                    <f:Label ID="lblT" runat="server" Text="出场机具设备清单" CssClass="customlabel span">
                                    </f:Label>
                                    <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                    </f:ToolbarFill>
                                    <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                        EnableAjax="false" DisableControlBeforePostBack="false">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:TemplateField ColumnID="tfNumber" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                                TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField ColumnID="tfSpecialEquipmentId" HeaderText="设备" Width="150px" HeaderTextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecialEquipmentId" runat="server" Text='<%#ConvertEqiupment(Eval("SpecialEquipmentId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="150px" ColumnID="SizeModel" DataField="SizeModel" SortField="SizeModel"
                                FieldType="String" HeaderText="规格型号" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="OwnerCheck" DataField="OwnerCheck" SortField="OwnerCheck"
                                FieldType="String" HeaderText="进场前自查自检情况" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="CertificateNum" DataField="CertificateNum"
                                SortField="CertificateNum" FieldType="String" HeaderText="施工设备合格证号" HeaderTextAlign="Center"
                                TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="InsuranceNum" DataField="InsuranceNum" SortField="InsuranceNum"
                                FieldType="String" HeaderText="保险单号" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="OutReason" DataField="OutReason" SortField="OutReason"
                                FieldType="String" HeaderText="出场理由" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
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
    <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>

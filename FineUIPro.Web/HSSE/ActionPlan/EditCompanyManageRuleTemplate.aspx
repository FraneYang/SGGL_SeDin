<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditCompanyManageRuleTemplate.aspx.cs" Inherits="FineUIPro.Web.HSSE.ActionPlan.EditCompanyManageRuleTemplate" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全制度</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全制度" EnableCollapse="true"
                runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="SafetyInstitutionId" ForceFit="true"
                DataIDField="SafetyInstitutionId" AllowSorting="true" SortField="EffectiveDate" SortDirection="DESC" 
                OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" PageSize="10" 
                 OnPageIndexChange="Grid1_PageIndexChange" OnRowCommand="Grid1_RowCommand">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox runat="server" Label="名称" ID="txtSafetyInstitutionName" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:DatePicker ID="txtStartDate" runat="server" Label="发布日期" LabelAlign="Right"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                            </f:DatePicker>
                            <f:Label ID="lblTo" runat="server" Text="至">
                            </f:Label>
                            <f:DatePicker ID="txtEndDate" runat="server" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged">
                            </f:DatePicker>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnSave" Icon="Accept" runat="server" ToolTip="确定" OnClick="btnSave_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                     <f:CheckBoxField ColumnID="ckbIsSelected" Width="50px" RenderAsStaticField="false" 
                        AutoPostBack="true" CommandName="IsSelected" HeaderText="选择" HeaderTextAlign="Center" />
                    <f:TemplateField ColumnID="tfNumber" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                        TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="400px" ColumnID="SafetyInstitutionName" DataField="SafetyInstitutionName" ExpandUnusedSpace="True"
                        SortField="SafetyInstitutionName" FieldType="String" HeaderText="制度名称" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="110px" ColumnID="EffectiveDate" DataField="EffectiveDate" SortField="EffectiveDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="发布日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="Scope" DataField="Scope" SortField="Scope"
                        FieldType="String" HeaderText="使用范围" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="Remark" DataField="Remark" SortField="Remark"
                        FieldType="String" HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left">
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
            <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true" Hidden="true"
                BodyPadding="0px">
                <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
            </f:ContentPanel>
        </Items>
    </f:Panel>
    </form>
</body>
</html>

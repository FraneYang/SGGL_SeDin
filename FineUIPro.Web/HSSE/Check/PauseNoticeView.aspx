<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PauseNoticeView.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Check.PauseNoticeView" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>工程暂停令</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" AjaxAspnetControls="divFile1" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            Layout="VBox" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
            LabelAlign="Right">
            <Rows>
                <f:FormRow ColumnWidths="25% 45% 30%">
                    <Items>
                        <f:TextBox ID="txtPauseNoticeCode" runat="server" Label="编号" Readonly="true" LabelWidth="150px">
                        </f:TextBox>
                        <f:TextBox ID="txtUnit" runat="server" Label="受检单位" Readonly="true">
                        </f:TextBox>
                        <f:TextBox ID="txtProjectPlace" runat="server" Label="工程部位" Readonly="true" MaxLength="200"
                            LabelWidth="100px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtWrongContent" runat="server" Label="因" Required="true" ShowRedStar="true"
                            MaxLength="150" Width="250px" Readonly="true" LabelWidth="150px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DatePicker ID="txtPauseTime" ShowRedStar="true" LabelWidth="150px" DateFormatString="yyyy-MM-dd HH:mm" runat="server" Label="现要求你公司于" Required="true" LabelAlign="Right"
                            EnableEdit="true" ShowTime="true" ShowSecond="false" Readonly="true">
                        </f:DatePicker>
                        <f:Label runat="server" Text="日起停止施工"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtSignMan" runat="server" Label="签发人" Readonly="true" LabelWidth="150px">
                        </f:TextBox>
                        <f:TextBox ID="txtApproveMan" runat="server" Label="批准人" Readonly="true">
                        </f:TextBox>
                        <f:TextBox ID="txtSignPerson" runat="server" Label="编制人" Readonly="true">
                        </f:TextBox>
                        <f:TextBox ID="txtComplieDate" runat="server" Label="编制时间" Readonly="true">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Grid ID="gvFlowOperate" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server" DataIDField="FlowOperateId" AllowSorting="true" SortField="OperateTime" SortDirection="ASC" EnableTextSelection="True" Height="260px">
                            <Columns>
                                <f:TemplateField ColumnID="tfNumber" Width="55px" HeaderText="序号" HeaderTextAlign="Center"
                                    TextAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNumber" runat="server" Text='<%# gvFlowOperate.PageIndex * gvFlowOperate.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:RenderField Width="250px" ColumnID="OperateName" DataField="OperateName"
                                    FieldType="String" HeaderText="步骤" HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="200px" ColumnID="UserName" DataField="UserName"
                                    FieldType="String" HeaderText="操作人" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="150px" ColumnID="Opinion" DataField="Opinion"
                                    FieldType="string" HeaderText="意见" HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="80px" ColumnID="IsAgree" DataField="IsAgree" FieldType="String"
                                    HeaderText="是否同意" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="160px" ColumnID="OperateTime" DataField="OperateTime"
                                    FieldType="string" HeaderText="时间" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>


                            </Columns>
                        </f:Grid>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:Label runat="server" ID="lbTemp">
                        </f:Label>
                        <f:Button ID="btnNoticeUrl" Text="通知单" ToolTip="通知单上传及查看" Icon="TableCell" runat="server"
                            OnClick="btnNoticeUrl_Click" ValidateForms="SimpleForm1">
                        </f:Button>
                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
        <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
    </form>
</body>
</html>

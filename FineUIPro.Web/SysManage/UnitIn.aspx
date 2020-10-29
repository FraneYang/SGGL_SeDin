<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitIn.aspx.cs" Inherits="FineUIPro.Web.SysManage.UnitIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>单位信息导入</title>
    <style>
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" OnCustomEvent="PageManager1_CustomEvent" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Toolbars>
                <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:Button ID="btnAudit" Icon="ApplicationEdit" runat="server" ToolTip="审核" ValidateForms="SimpleForm1"
                            OnClick="btnAudit_Click">
                        </f:Button>
                        <f:Button ID="btnImport" Icon="ApplicationGet" runat="server" ToolTip="导入" ValidateForms="SimpleForm1"
                            OnClick="btnImport_Click">
                        </f:Button>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnDownLoad" runat="server" Icon="ApplicationGo" ToolTip="下载模板" OnClick="btnDownLoad_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:FileUpload runat="server" ID="fuAttachUrl" EmptyText="选择要导入的文件" Label="选择要导入的文件"
                            LabelWidth="150px">
                        </f:FileUpload>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                            EnableColumnLines="true" BoxFlex="1" DataKeyNames="UnitId" AllowCellEditing="true"
                            ClicksToEdit="2" DataIDField="UnitId" AllowSorting="true" SortField="CardNo"
                            PageSize="12" Height="400px">
                            <Columns>
                                <f:TemplateField Width="55px" HeaderText="序号">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:RenderField Width="100px" ColumnID="UnitCode" DataField="UnitCode" FieldType="String"
                                    HeaderText="单位代码" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="180px" ColumnID="UnitName" DataField="UnitName"
                                    SortField="UnitName" FieldType="String" HeaderText="单位名称" HeaderTextAlign="Center"
                                    TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="UnitTypeId" DataField="UnitTypeId" FieldType="String" HeaderText="单位类型" HeaderTextAlign="Center"
                                    TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="200px" ColumnID="ProjectRange" DataField="ProjectRange" SortField="ProjectRange"
                                    FieldType="String" HeaderText="工程范围" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="Corporate" DataField="Corporate" FieldType="String"
                                    HeaderText="法人代表" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="150px" ColumnID="Address" DataField="Address" FieldType="String"
                                    HeaderText="单位地址" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="Telephone" DataField="Telephone" FieldType="String"
                                    HeaderText="联系电话" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="Fax" DataField="Fax" FieldType="String"
                                    HeaderText="传真" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="EMail" DataField="EMail" FieldType="String"
                                    HeaderText="邮箱" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:TemplateField HeaderText="是否分公司" HeaderTextAlign="Center" Width="100px" TextAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# ConvertIsBranch(Eval("IsBranch")) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:HiddenField ID="hdFileName" runat="server">
                        </f:HiddenField>
                        <f:HiddenField ID="hdCheckResult" runat="server">
                        </f:HiddenField>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="lblBottom" runat="server" Text="说明：1 单位信息导入模板中，灰色项为必填项。2 所属单位类型等必须与基础信息中对应类型的名称一致,否则无法导入。3 如需修改已有单位信息，请到系统中修改。4 数据导入后，点击“保存”，即可完成人员信息导入。">
                        </f:Label>
                    </Items>
                </f:FormRow>
            </Rows>
        </f:Form>
        <f:Window ID="Window1" Title="审核单位信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" OnClose="Window1_Close" IsModal="false"
            CloseAction="HidePostBack" Width="900px" Height="600px">
        </f:Window>
        <f:Window ID="Window2" Title="导入单位信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" OnClose="Window2_Close" IsModal="false"
            CloseAction="HidePostBack" Width="900px" Height="600px">
        </f:Window>
    </form>
</body>
</html>

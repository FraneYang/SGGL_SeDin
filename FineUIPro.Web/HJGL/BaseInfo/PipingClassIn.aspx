<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PipingClassIn.aspx.cs" Inherits="FineUIPro.Web.HJGL.BaseInfo.PipingClassIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导入管道等级</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" OnCustomEvent="PageManager1_CustomEvent" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Toolbars>
                <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                    <Items>                        
                        <f:HiddenField ID="hdFileName" runat="server">
                        </f:HiddenField>
                        <f:Button ID="btnAudit" Icon="ApplicationEdit" runat="server" ToolTip="审核" ValidateForms="SimpleForm1"
                            OnClick="btnAudit_Click">
                        </f:Button>
                        <f:Button ID="btnImport" Icon="ApplicationGet" runat="server" ToolTip="导入" ValidateForms="SimpleForm1"
                            OnClick="btnImport_Click">
                        </f:Button>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
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
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="管道等级"
                            EnableCollapse="true" runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="PipingClassId"
                            AllowCellEditing="true" ClicksToEdit="2" DataIDField="PipingClassId" AllowSorting="true"
                            SortField="PipingClassCode" SortDirection="DESC" AllowPaging="true"
                            IsDatabasePaging="true" EnableTextSelection="True" Height="400px">
                            <Columns>
                                <f:RenderField Width="200px" ColumnID="PipingClassCode" DataField="PipingClassCode"
                                    FieldType="String" HeaderText="管道等级代号" HeaderTextAlign="Center"
                                    TextAlign="Left" SortField="PipingClassCode">
                                </f:RenderField>
                                <f:RenderField Width="200px" ColumnID="PipingClassName" DataField="PipingClassName"
                                    FieldType="String" HeaderText="管道等级名称" HeaderTextAlign="Center"
                                    TextAlign="Left" SortField="PipingClassName">
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="SteelType" DataField="SteelType"
                                    FieldType="String" HeaderText="钢材类型" HeaderTextAlign="Center"
                                    TextAlign="Left" SortField="SteelType">
                                </f:RenderField>
                                <f:RenderField Width="160px" ColumnID="PNO" DataField="PNO" FieldType="String" HeaderText="P NO."
                                    HeaderTextAlign="Center" TextAlign="Left" SortField="PNO" Hidden="true">
                                </f:RenderField>
                                <f:RenderField Width="350px" ColumnID="Remark" DataField="Remark" FieldType="String"
                                    HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left"
                                    ExpandUnusedSpace="true">
                                </f:RenderField>
                            </Columns>
                            <PageItems>
                                <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                </f:ToolbarSeparator>
                                <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                                </f:ToolbarText>
                                <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true">
                                    <f:ListItem Text="10" Value="10" />
                                    <f:ListItem Text="15" Value="15" />
                                    <f:ListItem Text="20" Value="20" />
                                    <f:ListItem Text="25" Value="25" />
                                </f:DropDownList>
                            </PageItems>
                        </f:Grid>
                    </Items>
                </f:FormRow>
            </Rows>
        </f:Form>
    </form>
</body>
</html>

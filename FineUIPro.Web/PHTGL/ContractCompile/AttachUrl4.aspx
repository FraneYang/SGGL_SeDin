<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachUrl4.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.AttachUrl4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑附件</title>
    <style>
        .widthBlod {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="编辑附件" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label1" runat="server" Text="附件4    工程设备及材料分交" CssClass="widthBlod"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel3" Title="工程设备及材料分交" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Form ID="Form2" LabelAlign="Top" BodyPadding="10px" ShowBorder="false" ShowHeader="false" runat="server">
                                <Items>
                                    <f:Label ID="Lable1" runat="server" Text="总承包商和施工分包商对工程设备、材料采购进行划分，分交范围如下："></f:Label>
                                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" EnableCollapse="false"
                                        runat="server" BoxFlex="1" DataKeyNames="AttachUrlItemId" AllowCellEditing="true"
                                        ClicksToEdit="2" DataIDField="AttachUrlItemId" SortField="OrderNumber">
                                        <Toolbars>
                                            <f:Toolbar ID="Toolbar2" runat="server">
                                                <Items>
                                                    <f:Button ID="btnNew" ToolTip="增加" Icon="Add" EnablePostBack="true" runat="server" OnClick="btnAdd_Click">
                                                    </f:Button>
                                                    <f:Button ID="btnDelete" ToolTip="删除" Icon="Delete" EnablePostBack="false" runat="server">
                                                    </f:Button>
                                                </Items>
                                            </f:Toolbar>
                                        </Toolbars>
                                        <Columns>
                                             <f:RenderField ColumnID="OrderNumber" DataField="OrderNumber" Width="150px" FieldType="String" HeaderText="序号" TextAlign="Center"
                                                HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:TextBox ID="txtOrderNumber" Required="true" runat="server">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField ColumnID="Describe" DataField="Describe" Width="150px" FieldType="String" HeaderText="描述" TextAlign="Center"
                                                HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:TextBox ID="txtDescribe" Required="true" runat="server">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:GroupField HeaderText="采购责任" TextAlign="Center">
                                                <Columns>
                                                    <f:RenderCheckField ColumnID="Duty_Gen" DataField="Duty_Gen" Width="100px" RenderAsStaticField="false" HeaderText="总承包商" />
                                                     <f:RenderCheckField ColumnID="Duty_Sub" DataField="Duty_Sub" Width="100px" RenderAsStaticField="false" HeaderText="施工分包商" />
                                                </Columns>
                                            </f:GroupField>
                                            <f:RenderField ColumnID="Remarks" DataField="Remarks" Width="320px" FieldType="String" HeaderText="备注" TextAlign="Left"
                                                HeaderTextAlign="Center" ExpandUnusedSpace="true">
                                                <Editor>
                                                    <f:TextBox ID="txtNotes" Required="true" runat="server">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:Grid>

                                </Items>
                            </f:Form>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>

            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" Text="保存数据" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
</body>
</html>

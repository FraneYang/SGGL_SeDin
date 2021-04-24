<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachUrl10.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.AttachUrl10" %>

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
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="编辑附件10" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label1" runat="server" Text="附件10  施工分包商人员机械投入计划一览表" CssClass="widthBlod"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label7" runat="server" Text="10-1：施工人力投入计划表"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Grid ID="Grid1" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" Title="施工人力投入计划表" EnableCollapse="false"
                            runat="server" DataKeyNames="AttachUrlItemId" AllowCellEditing="true" ClicksToEdit="1"
                            EnableColumnLines="true" DataIDField="AttachUrlItemId" Height="200px">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar2" runat="server" Position="Top" ToolbarAlign="Left">
                                    <Items>
                                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                        <f:Button ID="btnNew" ToolTip="增加" Icon="Add" EnablePostBack="false" runat="server">
                                        </f:Button>
                                        <f:Button ID="btnDelete" ToolTip="删除" Icon="Delete" EnablePostBack="false" runat="server">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:RowNumberField HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center" Width="60px"></f:RowNumberField>
                                <f:RenderField Width="200px" ColumnID="Subject" DataField="Subject" FieldType="String"
                                    HeaderText="主项" HeaderTextAlign="Center">
                                    <Editor>
                                        <f:TextBox ID="txtSubject" runat="server"></f:TextBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField Width="200px" ColumnID="WorkType" DataField="WorkType" FieldType="String"
                                    HeaderText="工种" HeaderTextAlign="Center">
                                    <Editor>
                                        <f:TextBox ID="txtWorkType" runat="server"></f:TextBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField Width="150px" ColumnID="PersonNumber" DataField="PersonNumber" FieldType="Int"
                                    HeaderText="人数" HeaderTextAlign="Center">
                                    <Editor>
                                        <f:NumberBox ID="txtPersonNumber" runat="server" NoNegative="true" NoDecimal="true"></f:NumberBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField Width="200px" ColumnID="LifeTime" DataField="LifeTime" FieldType="String"
                                    HeaderText="使用周期" HeaderTextAlign="Center">
                                    <Editor>
                                        <f:TextBox ID="txtLifeTime" runat="server"></f:TextBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="Remarks" DataField="Remarks" ExpandUnusedSpace="true"
                                    HeaderText="备注" HeaderTextAlign="Center">
                                    <Editor>
                                        <f:TextBox ID="txtRemarks" runat="server">
                                        </f:TextBox>
                                    </Editor>
                                </f:RenderField>
                            </Columns>
                            <Listeners>
                                <f:Listener Event="dataload" Handler="onGridDataLoad" />
                            </Listeners>
                        </f:Grid>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label2" runat="server" Text="10-2：主要机械设备投入计划表"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Grid ID="Grid2" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" Title="主要机械设备投入计划表" EnableCollapse="false"
                            runat="server" DataKeyNames="AttachUrlItemId" AllowCellEditing="true" ClicksToEdit="1"
                            EnableColumnLines="true" DataIDField="AttachUrlItemId" Height="200px">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar3" runat="server" Position="Top" ToolbarAlign="Left">
                                    <Items>
                                        <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>
                                        <f:Button ID="btnAdd" ToolTip="增加" Icon="Add" EnablePostBack="false" runat="server">
                                        </f:Button>
                                        <f:Button ID="btnDel" ToolTip="删除" Icon="Delete" EnablePostBack="false" runat="server">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:RowNumberField HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center" Width="60px"></f:RowNumberField>
                                <f:RenderField Width="200px" ColumnID="MachineName" DataField="MachineName" FieldType="String"
                                    HeaderText="机械或设备名称" HeaderTextAlign="Center">
                                    <Editor>
                                        <f:TextBox ID="txtMachineName" runat="server"></f:TextBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField Width="200px" ColumnID="MachineSpec" DataField="MachineSpec" FieldType="String"
                                    HeaderText="规格型号" HeaderTextAlign="Center">
                                    <Editor>
                                        <f:TextBox ID="txtMachineSpec" runat="server"></f:TextBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField Width="150px" ColumnID="number" DataField="number" FieldType="Int"
                                    HeaderText="数量" HeaderTextAlign="Center">
                                    <Editor>
                                        <f:NumberBox ID="txtNumber" runat="server" NoNegative="true" NoDecimal="true"></f:NumberBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField Width="200px" ColumnID="LeasedOrOwned" DataField="LeasedOrOwned" FieldType="String"
                                    HeaderText="租赁或自有" HeaderTextAlign="Center">
                                    <Editor>
                                        <f:TextBox ID="txtLeasedOrOwned" runat="server"></f:TextBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="Remarks" DataField="Remarks" ExpandUnusedSpace="true"
                                    HeaderText="备注" HeaderTextAlign="Center">
                                    <Editor>
                                        <f:TextBox ID="TextBox4" runat="server">
                                        </f:TextBox>
                                    </Editor>
                                </f:RenderField>
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
    <script>
        function onGridDataLoad(event) {
            this.mergeColumns(['Subject']);
        }
    </script>
</body>
</html>

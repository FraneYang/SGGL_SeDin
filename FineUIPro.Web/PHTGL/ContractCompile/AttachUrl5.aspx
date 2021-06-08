<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachUrl5.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.AttachUrl5" %>

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
                        <f:Label ID="Label1" runat="server" Text="附件5  暂估价材料、工程设备一览表" CssClass="widthBlod"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>

                        <f:Grid ID="Grid1" ShowBorder="true" EnableAjax="false" ShowHeader="true" Title="5-1：材料暂估价表"
                            runat="server" BoxFlex="1" DataKeyNames="AttachUrlItemId" AllowCellEditing="true"
                            ClicksToEdit="2" DataIDField="AttachUrlItemId" EnableColumnLines="true" SortField="OrderNumber" Height="300"
                            EnableTextSelection="True" OnPreRowDataBound="Grid1_PreRowDataBound" OnRowCommand="Grid1_RowCommand">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar2" runat="server">
                                    <Items>
                                       <f:Button ID="btnNew" ToolTip="增加" Icon="Add" EnablePostBack="false" runat="server">
                                        </f:Button>
                                        <f:Button ID="btnDelete" ToolTip="删除" Icon="Delete" EnablePostBack="false" runat="server">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>

                                <%-- <f:TemplateField ColumnID="tfPageIndex" Width="55px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                                                   EnableLock="true" Locked="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                             </f:TemplateField>--%>
                                <f:RenderField ColumnID="OrderNumber" DataField="OrderNumber" Width="150px" FieldType="String" HeaderText="序号" TextAlign="Center"
                                    HeaderTextAlign="Center">
                                    <Editor>
                                        <f:TextBox ID="OrderNumber" Required="true" runat="server">
                                        </f:TextBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField ColumnID="Name" DataField="Name" Width="150px" FieldType="String" HeaderText="名称" TextAlign="Center"
                                    HeaderTextAlign="Center">
                                    <Editor>
                                        <f:TextBox ID="TextBox4" Required="true" runat="server">
                                        </f:TextBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField ColumnID="Spec" DataField="Spec" Width="320px" FieldType="String" HeaderText="规格型号" TextAlign="Left"
                                    HeaderTextAlign="Center">
                                    <Editor>
                                        <f:TextBox ID="TextBox5" Required="true" runat="server">
                                        </f:TextBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField ColumnID="Material" DataField="Material" Width="320px" FieldType="String" HeaderText="材质" TextAlign="Left"
                                    HeaderTextAlign="Center">
                                    <Editor>
                                        <f:TextBox ID="TextBox6" Required="true" runat="server">
                                        </f:TextBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField ColumnID="Company" DataField="Company" Width="320px" FieldType="String" HeaderText="单位" TextAlign="Left"
                                    HeaderTextAlign="Center">
                                    <Editor>
                                        <f:TextBox ID="TextBox7" Required="true" runat="server">
                                        </f:TextBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField ColumnID="UnitPrice" DataField="UnitPrice" Width="320px" FieldType="Double" HeaderText="单价（元）" TextAlign="Left"
                                    HeaderTextAlign="Center">
                                    <Editor>
                                        <f:TextBox ID="TextBox8" Required="true" runat="server">
                                        </f:TextBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField ColumnID="Remarks" DataField="Remarks" Width="320px" FieldType="String" HeaderText="备注" TextAlign="Left"
                                    HeaderTextAlign="Center">
                                    <Editor>
                                        <f:TextBox ID="TextBox9" Required="true" runat="server">
                                        </f:TextBox>
                                    </Editor>
                                </f:RenderField>
                            </Columns>
                        </f:Grid>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel1" Title="5-2：工程设备暂估价表" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <Items>
                                <f:Grid ID="Grid2" ShowBorder="true" EnableAjax="false" ShowHeader="true"
                                    runat="server" BoxFlex="1" DataKeyNames="AttachUrlItemId" AllowCellEditing="true"
                                    ClicksToEdit="2" DataIDField="AttachUrlItemId" EnableColumnLines="true" SortField="OrderNumber" Height="300"
                                    EnableTextSelection="True" OnPreRowDataBound="Grid1_PreRowDataBound" OnRowCommand="Grid1_RowCommand">
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar3" runat="server">
                                            <Items>
                                                 <f:Button ID="btnNew2" ToolTip="增加" Icon="Add" EnablePostBack="false" runat="server">
                                        </f:Button>
                                        <f:Button ID="btnDelete2" ToolTip="删除" Icon="Delete" EnablePostBack="false" runat="server">
                                        </f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Columns>

                                        <%-- <f:TemplateField ColumnID="tfPageIndex" Width="55px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                                                   EnableLock="true" Locked="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                             </f:TemplateField>--%>
                                        <f:RenderField ColumnID="OrderNumber" DataField="OrderNumber" Width="150px" FieldType="String" HeaderText="序号" TextAlign="Center"
                                            HeaderTextAlign="Center">
                                            <Editor>
                                                <f:TextBox ID="txtOrderNumber" Required="true" runat="server">
                                                </f:TextBox>
                                            </Editor>
                                        </f:RenderField>
                                        <f:RenderField ColumnID="Name" DataField="Name" Width="150px" FieldType="String" HeaderText="名称" TextAlign="Center"
                                            HeaderTextAlign="Center">
                                            <Editor>
                                                <f:TextBox ID="TextBox10" Required="true" runat="server">
                                                </f:TextBox>
                                            </Editor>
                                        </f:RenderField>
                                        <f:RenderField ColumnID="Company" DataField="Company" Width="320px" FieldType="String" HeaderText="单位" TextAlign="Left"
                                            HeaderTextAlign="Center">
                                            <Editor>
                                                <f:TextBox ID="TextBox11" Required="true" runat="server">
                                                </f:TextBox>
                                            </Editor>
                                        </f:RenderField>
                                        <f:RenderField ColumnID="amount" DataField="amount" Width="320px" FieldType="Int" HeaderText="数量" TextAlign="Left"
                                            HeaderTextAlign="Center">
                                            <Editor>
                                                <f:TextBox ID="TextBox12" Required="true" runat="server">
                                                </f:TextBox>
                                            </Editor>
                                        </f:RenderField>
                                        <f:RenderField ColumnID="UnitPrice" DataField="UnitPrice" Width="320px" FieldType="Double" HeaderText="单价（元）" TextAlign="Left"
                                            HeaderTextAlign="Center">
                                            <Editor>
                                                <f:TextBox ID="TextBox13" Required="true" runat="server">
                                                </f:TextBox>
                                            </Editor>
                                        </f:RenderField>
                                        <f:RenderField ColumnID="Totalprice" DataField="Totalprice" Width="320px" FieldType="Double" HeaderText="合价（元）" TextAlign="Left"
                                            HeaderTextAlign="Center">
                                            <Editor>
                                                <f:TextBox ID="TextBox14" Required="true" runat="server">
                                                </f:TextBox>
                                            </Editor>
                                        </f:RenderField>
                                        <f:RenderField ColumnID="Remarks" DataField="Remarks" Width="320px" FieldType="String" HeaderText="备注" TextAlign="Left"
                                            HeaderTextAlign="Center">
                                            <Editor>
                                                <f:TextBox ID="TextBox15" Required="true" runat="server">
                                                </f:TextBox>
                                            </Editor>
                                        </f:RenderField>
                                    </Columns>
                                </f:Grid>
                            </Items>

                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel2" Title="5-3：商品混凝土价格一览表" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Form ID="Form4" LabelAlign="Top" BodyPadding="10px" ShowBorder="false" ShowHeader="false" runat="server">
                                <Items>
                                    <f:TextBox ID="TextBox1" Label="各种拟使用的商品砼的价格" Margin="0 5 0 0" AutoPostBack="true" Required="true" ShowRedStar="true" runat="server">
                                    </f:TextBox>
                                    <f:TextBox ID="TextBox2" Label="按照设计增加外加剂的价格" Margin="0 5 0 0" AutoPostBack="true" Required="true" ShowRedStar="true" runat="server">
                                    </f:TextBox>
                                    <f:TextBox ID="TextBox3" Label="泵送费价格" Margin="0 5 0 0" AutoPostBack="true" Required="true" ShowRedStar="true" runat="server">
                                    </f:TextBox>
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
</body>
</html>

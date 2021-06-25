<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachUrl5.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.AttachUrl5" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑附件5</title>
    <style>
        .widthBlod {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:Panel ID="Panel4" IsFluid="true" CssClass="blockpanel" BodyPadding="10" Layout="VBox" MaxHeight="550" BoxConfigChildMargin="0 0 5 0" AutoScroll="true"
            EnableCollapse="true" Title="附件5    暂估价材料" runat="server">
            <Items>
                <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" Title="" AutoScroll="true"
                    runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                    <Items>
                        <f:FormRow>
                            <Items>
                                <f:Label ID="Label1" runat="server" Text="附件5    暂估价材料" CssClass="widthBlod"></f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:HtmlEditor runat="server" Label="合同价款支付办法" ID="txtAttachUrlContent" ShowLabel="false"
                                    Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="500px" LabelAlign="Right" Text="">
                                </f:HtmlEditor>
                            </Items>
                        </f:FormRow>
                        <%--<f:FormRow>
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

                                <f:TemplateField ColumnID="tfPageIndex" Width="55px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                                                   EnableLock="true" Locked="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                             </f:TemplateField>
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
                </f:FormRow>--%>
                    </Items>
                </f:Form>

            </Items>
             
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:HiddenField ID="hdAttachUrlItemId" runat="server" ></f:HiddenField>
                        <f:ToolbarFill runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" Text ="保存" Size="Medium" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" Text="关闭" Size="Medium" runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Panel>

    </form>
</body>
</html>

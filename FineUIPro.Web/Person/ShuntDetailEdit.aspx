<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShuntDetailEdit.aspx.cs" Inherits="FineUIPro.Web.Person.ShuntDetailEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户信息</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="用户选择"
                    EnableCollapse="true" runat="server" BoxFlex="1" EnableColumnLines="true" ForceFit="true"
                    DataKeyNames="UserId" AllowCellEditing="true" ClicksToEdit="1" DataIDField="UserId"
                    AllowSorting="true" SortField="UserCode" SortDirection="ASC" OnSort="Grid1_Sort"
                    PageSize="100" EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:DropDownList ID="drpWorkPost" runat="server" Label="岗位" Width="250px" LabelWidth="60px" LabelAlign="Right"
                                    EnableEdit="true" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                                </f:DropDownList>
                                <f:DropDownList ID="drpPostTitle" runat="server" Label="职称" Width="250px" LabelWidth="60px" LabelAlign="Right"
                                    EnableEdit="true" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                                </f:DropDownList>
                                <f:DropDownList ID="drpCertificate" runat="server" Label="职业资格证书" Width="250px" LabelWidth="105px" LabelAlign="Right"
                                    EnableEdit="true" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                                </f:DropDownList>
                                <f:ToolbarFill runat="server"></f:ToolbarFill>
                                <f:Button ID="btnSure" ToolTip="确定按钮" Icon="Accept" runat="server" OnClick="btnSure_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:TemplateField ColumnID="Check" Width="50px" HeaderText="选择" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="cbSelect" />
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center" />
                        <f:RenderField Width="80px" ColumnID="UserName" DataField="UserName" EnableFilter="true"
                            SortField="UserName" FieldType="String" HeaderText="姓名" HeaderTextAlign="Center"
                            TextAlign="Left">
                        </f:RenderField>
                        <f:TemplateField ColumnID="UserWorkPost" Width="100px" HeaderText="岗位" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# ConvertUserWorkPost(Eval("UserId")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>

                        <f:TemplateField ColumnID="OldWorkPost" Width="100px" HeaderText="历史岗位" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# ConvertOldWorkPost(Eval("UserId")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:TemplateField ColumnID="CurrWorkPost" Width="100px" HeaderText="当前岗位" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# ConvertCurrWorkPost(Eval("UserId")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField Width="100px" ColumnID="PostTitle" DataField="PostTitle" EnableFilter="true"
                            SortField="PostTitle" FieldType="String" HeaderText="职称" HeaderTextAlign="Center"
                            TextAlign="Left">
                        </f:RenderField>
                        <f:TemplateField ColumnID="Certificate" Width="120px" HeaderText="职业资格证书" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# ConvertCertificateName(Eval("UserId")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField Width="100px" ColumnID="WorkPost" DataField="WorkPost" FieldType="String"
                            HeaderText="拟聘岗位" HeaderTextAlign="Center">
                            <Editor>
                                <f:DropDownList ID="drpWP" Required="true" runat="server">
                                </f:DropDownList>
                            </Editor>
                        </f:RenderField>
                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="用户信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="1000px"
            Height="400px">
        </f:Window>
    </form>
</body>
</html>

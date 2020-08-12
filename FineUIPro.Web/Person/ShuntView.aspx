<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShuntView.aspx.cs" Inherits="FineUIPro.Web.Person.ShuntView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow ColumnWidths="30% 45% 25%">
                    <Items>
                        <f:TextBox runat="server" ID="txtCode" Label="编号" LabelWidth="60px" ShowRedStar="true" Required="true" Readonly="true"></f:TextBox>
                        <f:TextBox runat="server" ID="drpProject" Label="拟聘项目" LabelWidth="100px" ShowRedStar="true" Required="true" Readonly="true"></f:TextBox>
                        <f:TextBox runat="server" ID="txtCompileDate" Label="编制日期" LabelWidth="100px" ShowRedStar="true" Required="true" Readonly="true"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="拟聘人员" EnableCollapse="true"
                            runat="server" BoxFlex="1" DataKeyNames="ShuntDetailId" AllowCellEditing="true" EnableColumnLines="true"
                            ClicksToEdit="2" DataIDField="ShuntDetailId" AllowSorting="true" SortField="ShuntDetailId"
                            SortDirection="ASC" PageSize="100" ForceFit="true" EnableTextSelection="True">
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center" />
                                <f:RenderField Width="80px" ColumnID="UserName" DataField="UserName" EnableFilter="true"
                                    SortField="UserName" FieldType="String" HeaderText="姓名" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="PostTitle" DataField="PostTitle" EnableFilter="true"
                                    SortField="PostTitle" FieldType="String" HeaderText="职称" HeaderTextAlign="Center"
                                    TextAlign="Left">
                                </f:RenderField>
                                <f:TemplateField ColumnID="Certificate" Width="100px" HeaderText="职业资格证书" HeaderTextAlign="Center" TextAlign="Center"
                                    EnableLock="true" Locked="False">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# ConvertCertificateName(Eval("UserId")) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:TemplateField ColumnID="CurrProject" Width="100px" HeaderText="当前项目" HeaderTextAlign="Center" TextAlign="Center"
                                    EnableLock="true" Locked="False">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# ConvertCurrProject(Eval("UserId")) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:TemplateField ColumnID="CurrWorkPost" Width="100px" HeaderText="当前岗位" HeaderTextAlign="Center" TextAlign="Center"
                                    EnableLock="true" Locked="False">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# ConvertCurrWorkPost(Eval("UserId")) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:TemplateField ColumnID="OldWorkPost" Width="100px" HeaderText="历史岗位" HeaderTextAlign="Center" TextAlign="Center"
                                    EnableLock="true" Locked="False">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# ConvertOldWorkPost(Eval("UserId")) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:TemplateField ColumnID="WorkPost" Width="100px" HeaderText="拟聘岗位" HeaderTextAlign="Center" TextAlign="Center"
                                    EnableLock="true" Locked="False">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%# ConvertWorkPost(Eval("WorkPostId")) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:FormRow>
                <f:FormRow ID="plApprove2">
                    <Items>
                        <f:ContentPanel Title="分流管理审批列表" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Grid ID="gvApprove" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" runat="server" EnableCollapse="false"
                                DataKeyNames="ShuntApproveId" EnableColumnLines="true" ForceFit="true">
                                <Columns>
                                    <f:RowNumberField Width="40px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"/>
                                    <f:TemplateField ColumnID="State" Width="150px" HeaderText="办理类型" HeaderTextAlign="Center" TextAlign="Center"
                                        EnableLock="true" Locked="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lbtype" runat="server" Text='<%# ConvertState(Eval("ApproveType")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </f:TemplateField>
                                    <f:BoundField Width="100px" DataField="ApproveMan" HeaderTextAlign="Center" HeaderText="办理人员" TextAlign="Center" />
                                    <f:BoundField Width="100px" DataField="ApproveDate" HeaderTextAlign="Center" TextAlign="Center" DataFormatString="{0:yyyy-MM-dd}" HeaderText="办理时间" />
                                    <f:BoundField Width="180px" DataField="ApproveIdea" HeaderTextAlign="Center" TextAlign="Center" HeaderText="办理意见" />

                                </Columns>
                            </f:Grid>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
            </Rows>
        </f:Form>
    </form>
</body>
</html>

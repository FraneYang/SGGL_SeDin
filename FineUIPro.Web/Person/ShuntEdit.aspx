<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShuntEdit.aspx.cs" Inherits="FineUIPro.Web.Person.ShuntEdit" %>

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
                        <f:TextBox runat="server" ID="txtCode" Label="编号" LabelWidth="60px" ShowRedStar="true" Required="true"></f:TextBox>
                        <f:DropDownList ID="drpProject" runat="server" Label="拟聘项目" Width="300px" LabelWidth="100px"
                            EnableEdit="true" ShowRedStar="true" Required="true">
                        </f:DropDownList>
                        <f:DatePicker runat="server" Label="编制日期" ID="txtCompileDate" LabelWidth="100px" ShowRedStar="true" Required="true"></f:DatePicker>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="拟聘人员" EnableCollapse="true"
                            runat="server" BoxFlex="1" DataKeyNames="ShuntDetailId" AllowCellEditing="true" EnableColumnLines="true"
                            ClicksToEdit="2" DataIDField="ShuntDetailId" AllowSorting="true" SortField="ShuntDetailId"
                            SortDirection="ASC" PageSize="100" ForceFit="true" EnableTextSelection="True">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                                    <Items>
                                        <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                        </f:ToolbarFill>
                                        <f:Button ID="btnNew" ToolTip="新增" Icon="Add" runat="server"
                                            OnClick="btnNew_Click">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
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
                            <Listeners>
                                <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                            </Listeners>
                        </f:Grid>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel5" Title="分流管理审批流程设置" runat="server" ShowHeader="true" EnableCollapse="true"
                            BodyPadding="0px">
                            <f:Form ID="Form5" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                            <f:RadioButtonList runat="server" ID="rblIsAgree" Label="是否同意" ShowRedStar="true" AutoPostBack="true" OnSelectedIndexChanged="rblIsAgree_SelectedIndexChanged">
                                                <f:RadioItem Text="同意" Value="true" />
                                                <f:RadioItem Text="不同意" Value="false" />
                                            </f:RadioButtonList>
                                            <f:Label runat="server" CssStyle="display:none"></f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:DropDownList ID="drpHandleType" runat="server" Label="办理步骤" LabelAlign="Right" EnableEdit="true">
                                            </f:DropDownList>
                                            <f:DropDownList ID="drpHandleMan"  runat="server" Label="办理人员" Required="true" LabelAlign="Right" EnableEdit="true">
                                            </f:DropDownList>
                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow ID="plApprove1">
                    <Items>
                        <f:TextArea ID="txtOpinions" runat="server" Label="我的意见" MaxLength="3000">
                            </f:TextArea>
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
            <Toolbars>
                <f:Toolbar ID="Toolbar2" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill ID="ToolbarFill2" runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSave" OnClick="btnSave_Click" Icon="SystemSave" runat="server" ToolTip="保存" >
                        </f:Button>
                        <f:Button ID="btnSubmit" OnClick="btnSubmit_Click" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1">
                        </f:Button>
                        <%--  <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" OnClick="btnClose_Click" runat="server" Icon="SystemClose">
                        </f:Button>--%>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
        <f:Window ID="Window1" Title="编辑历史记录" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
            Height="420px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
                runat="server" Text="编辑" Icon="Pencil">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
                ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除"
                Icon="Delete">
            </f:MenuButton>
        </f:Menu>
    </form>
</body>
<script type="text/jscript">
    var menuID = '<%= Menu1.ClientID %>';
    // 返回false，来阻止浏览器右键菜单
    function onRowContextMenu(event, rowId) {
        F(menuID).show();  //showAt(event.pageX, event.pageY);
        return false;
    }

    function reloadGrid() {
        __doPostBack(null, 'reloadGrid');
    }
</script>
</html>

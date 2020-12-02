<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RectifyEdit.aspx.cs" Inherits="FineUIPro.Web.OfficeCheck.Check.RectifyEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>隐患整改</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />    
    <script type="text/javascript" src="../../Controls/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel4" />
        <f:Panel ID="Panel4" runat="server" BodyPadding="10px" ShowBorder="true" ShowHeader="false" AutoScroll="true">
            <Items>
                <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" EnableTableStyle="true"
                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                    <Rows>
                        <f:FormRow runat="server">
                            <Items>
                                <f:TextBox ID="txtRectifyNoticesCode" runat="server" Label="编号" MaxLength="70" >
                                </f:TextBox>
                                <f:DropDownList ID="drpProjectId" runat="server" Label="受检项目" 
                                    LabelAlign="Right" EnableEdit="true">
                                </f:DropDownList>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:DropDownList ID="drpCheckMan" runat="server" Label="检查人员" LabelAlign="Right" 
                                    EnableEdit="true" EnableMultiSelect="true" AutoPostBack="true" OnSelectedIndexChanged="drpCheckMan_SelectedIndexChanged" >
                                </f:DropDownList>
                                <f:TextBox runat="server" Label="检查人员" ID="txtCheckPerson"></f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                   <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="检查日期" ID="txtCheckedDate" 
                                    LabelAlign="right" ShowRedStar="true">
                                </f:DatePicker>
                                <f:DropDownList ID="drpHiddenHazardType" runat="server" Label="隐患类别" EnableEdit="true" EmptyText="--请选择--">
                                    <f:ListItem Text="一般" Value="1" />
                                    <f:ListItem Text="较大" Value="2" />
                                    <f:ListItem Text="重大" Value="3" />
                                </f:DropDownList>
                            </Items>
                        </f:FormRow>
                    </Rows>
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                            <Items>
                                <f:HiddenField ID="hdRectifyNoticesId" runat="server"></f:HiddenField>
                                <f:Button ID="btnSave" OnClick="btnSave_Click" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1" >
                                </f:Button>
                                 <f:Button ID="btnSubmit" OnClick="btnSubmit_Click" Icon="SystemSaveNew" runat="server" ToolTip="签发" ValidateForms="SimpleForm1" >
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                </f:Form>
                <f:Form ID="Form2" ShowBorder="true" ShowHeader="false" Title="安全隐患及整改要求" AutoScroll="true"
                    runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                    <Rows>                      
                        <f:FormRow>
                            <Items>
                                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" AllowCellEditing="true" ClicksToEdit="1" 
                                    ForceFit="true" EnableCollapse="true" EnableColumnLines="true" EnableColumnMove="true" runat="server" BoxFlex="1"
                                    DataKeyNames="RectifyItemId,IsRectify" DataIDField="RectifyItemId" AllowSorting="true" SortField="RectifyItemId"
                                    SortDirection="ASC" EnableTextSelection="True" MinHeight="240px" PageSize="500"
                                    EnableRowDoubleClickEvent="true" OnRowCommand="Grid1_RowCommand" OnRowDataBound="Grid1_RowDataBound">
                                    <Toolbars>
                                        <f:Toolbar ID="toolAdd" Position="Top" ToolbarAlign="Right" runat="server">
                                            <Items>
                                                <f:Button ID="btnAdd" Icon="Add" runat="server" OnClick="btnAdd_Click" ToolTip="新增">
                                                </f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Columns>
                                        <f:RenderField HeaderText="主键" ColumnID="RectifyItemId" DataField="RectifyItemId"
                                            SortField="RectifyItemId" FieldType="String" HeaderTextAlign="Center" TextAlign="Center"
                                            Hidden="true">
                                        </f:RenderField>
                                        <f:RenderField Width="300px" ColumnID="WrongContent" DataField="WrongContent" FieldType="string" 
                                            HeaderText="具体位置及隐患内容">
                                            <Editor>
                                                <f:TextBox ID="tWrongContent" runat="server" MaxLength="800" ShowRedStar="true" Required="true">
                                                </f:TextBox>
                                            </Editor>
                                        </f:RenderField>
                                        <f:RenderField Width="100px" ColumnID="Requirement" DataField="Requirement" FieldType="string"
                                            HeaderText="整改要求">
                                            <Editor>
                                                <f:TextBox ID="tRequirement" runat="server" MaxLength="800" ShowRedStar="true" Required="true">
                                                </f:TextBox>
                                            </Editor>
                                        </f:RenderField>
                                        <f:TemplateField ColumnID="LimitTime" Width="100px" HeaderText="整改期限" HeaderTextAlign="Center"
                                            TextAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtLimitTimes" runat="server" Text='<%# Eval("LimitTime")!=null? ConvertDate(Eval("LimitTime")):"" %>'
                                                    Width="98%" CssClass="Wdate" Style="width: 98%; cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"
                                                    BorderStyle="None">
                                                </asp:TextBox>
                                            </ItemTemplate>
                                        </f:TemplateField>
                                        <f:LinkButtonField HeaderText="整改前" ConfirmTarget="Top" Width="80" CommandName="AttachUrl"
                                            TextAlign="Center" ToolTip="整改照片" Text="详细" />
                                        <f:RenderField Width="100px" ColumnID="RectifyResults" DataField="RectifyResults" FieldType="string"
                                            HeaderText="整改结果">
                                            <Editor>
                                                <f:TextBox ID="txtRectifyResults" runat="server" MaxLength="800" LabelWidth="160px">
                                                </f:TextBox>
                                            </Editor>
                                        </f:RenderField>
                                        <f:LinkButtonField ColumnID="ReAttachUrl" HeaderText="整改后" ConfirmTarget="Top" Width="80" CommandName="ReAttachUrl"
                                            TextAlign="Center" ToolTip="整改照片" Text="整改后" />
                                        <f:TemplateField ColumnID="IsRectify" HeaderText="合格"  HeaderTextAlign="Center" TextAlign="Center"  Width="60px">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpIsRectify" runat="server">
                                                    <asp:ListItem Value="false">否</asp:ListItem>
                                                    <asp:ListItem Value="true">是</asp:ListItem>
                                                </asp:DropDownList>
                                                <f:HiddenField ID="hdIsRectify" runat="server" Text='<%# Eval("IsRectify") %>'></f:HiddenField>
                                            </ItemTemplate>
                                        </f:TemplateField>
                                        <f:LinkButtonField ID="del" ColumnID="del" HeaderText="删除" Width="60px" CommandName="delete"
                                            Icon="Delete" />
                                    </Columns>
                                </f:Grid>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Form ID="Form3" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                    <Rows>
                        <f:FormRow runat="server" ID="next">
                            <Items>
                                <f:DropDownList ID="drpSignPerson" runat="server" Label="项目安全经理" LabelWidth="110px"
                                    LabelAlign="Right" EnableEdit="true" ShowRedStar="true">
                                </f:DropDownList>
                                <f:Label runat="server"></f:Label>
                            </Items>
                        </f:FormRow>
                    </Rows>                    
                </f:Form>
            </Items>
        </f:Panel>
        <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
        </form>
</body>
</html>

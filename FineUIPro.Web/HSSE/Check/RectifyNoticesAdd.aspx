<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RectifyNoticesAdd.aspx.cs" Inherits="FineUIPro.Web.HSSE.Check.RectifyNoticesAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
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
                   <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                            <Items>
                                <f:HiddenField ID="hdRectifyNoticesId" runat="server"></f:HiddenField>
                                <f:Button ID="btnSave" OnClick="btnSave_Click" Icon="SystemSave" runat="server" ToolTip="保存" >
                                </f:Button>
                                <f:Button ID="btnSubmit" OnClick="btnSubmit_Click" Icon="SystemSaveNew" runat="server" ToolTip="提交"
                                    ValidateForms="SimpleForm1">
                                </f:Button>
                                <f:Button ID="btnClose" EnablePostBack="false"  runat="server" Icon="SystemClose">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Rows>
                        <f:FormRow ColumnWidths="33% 33% 34%">
                            <Items>
                                <f:TextBox ID="txtRectifyNoticesCode" runat="server" Label="编号" MaxLength="70" >
                                </f:TextBox>
                                <f:DropDownList ID="drpUnitId" runat="server" Label="受检单位" 
                                    LabelAlign="Right" EnableEdit="true" >
                                </f:DropDownList>
                                <f:DropDownList ID="drpWorkAreaId" runat="server" Label="单位工程"  EnableCheckBoxSelect="true"
                                    LabelAlign="Right" EnableEdit="true"  EnableMultiSelect="true" AutoPostBack="true" 
                                    OnSelectedIndexChanged="drpWorkAreaId_SelectedIndexChanged" >
                                </f:DropDownList>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server" ColumnWidths="66% 34%">
                            <Items>
                                <f:DropDownList ID="drpCheckMan" runat="server" Label="检查人员" LabelAlign="Right" EnableCheckBoxSelect="true"
                                    EnableEdit="true" EnableMultiSelect="true" AutoPostBack="true" 
                                    OnSelectedIndexChanged="drpCheckMan_SelectedIndexChanged" >
                                </f:DropDownList>
                                <f:TextBox runat="server" Label="检查人员" ID="txtCheckPerson" LabelAlign="Right" MaxLength="2000"></f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="33% 33% 34%">
                            <Items>
                                   <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="检查日期" ID="txtCheckedDate" 
                                    LabelAlign="right" ShowRedStar="true">
                                </f:DatePicker>
                                <f:DropDownList ID="drpHiddenHazardType" runat="server" Label="隐患类别" LabelAlign="Right" EnableEdit="true">
                                    <f:ListItem Text="一般" Value="1" Selected="true" />
                                    <f:ListItem Text="较大" Value="2" />
                                    <f:ListItem Text="重大" Value="3" />
                                </f:DropDownList>
                                <f:Label runat="server" ></f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" AllowCellEditing="true" Title="安全隐患及整改要求"                                    
                                    runat="server" BoxFlex="1" DataKeyNames="RectifyNoticesItemId" DataIDField="RectifyNoticesItemId" EnableColumnLines="true" 
                                     AllowSorting="true" SortField="RectifyNoticesItemId"  SortDirection="ASC" EnableTextSelection="True" Height="240px"                                    
                                    EnableRowDoubleClickEvent="true" OnRowCommand="Grid1_RowCommand" >
                                    <Toolbars>
                                        <f:Toolbar ID="toolAdd" Position="Top" ToolbarAlign="Right" runat="server">
                                            <Items>
                                                <f:Button ID="btnAdd" Icon="Add" runat="server" OnClick="btnAdd_Click" ToolTip="新增">
                                                </f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Columns>
                                        <f:RenderField HeaderText="主键" ColumnID="RectifyNoticesItemId" DataField="RectifyNoticesItemId"
                                            SortField="RectifyNoticesItemId" FieldType="String" HeaderTextAlign="Center" TextAlign="Center"
                                            Hidden="true">
                                        </f:RenderField>
                                        <f:RenderField Width="300px" ColumnID="WrongContent" DataField="WrongContent" FieldType="string" 
                                            HeaderText="具体位置及隐患内容" ExpandUnusedSpace="true" HeaderTextAlign="Center" TextAlign="Left">
                                            <Editor>
                                                <f:TextBox ID="tWrongContent" runat="server" MaxLength="800" ShowRedStar="true" Required="true">
                                                </f:TextBox>
                                            </Editor>
                                        </f:RenderField>                                         
                                        <f:RenderField Width="250px" ColumnID="Requirement" DataField="Requirement" FieldType="string"
                                            HeaderText="整改要求" HeaderTextAlign="Center" TextAlign="Left">
                                            <Editor>
                                                <f:TextBox ID="tRequirement" runat="server" MaxLength="800" ShowRedStar="true" Required="true">
                                                </f:TextBox>
                                            </Editor>
                                        </f:RenderField>
                                       <f:RenderField Width="140px" ColumnID="LimitTime" DataField="LimitTime" FieldType="Date"
                                                Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="整改期限">
                                                <Editor>
                                                    <f:DatePicker ID="txtLimitTimes" Required="true" runat="server">
                                                    </f:DatePicker>
                                                </Editor>
                                            </f:RenderField>
                                        <f:LinkButtonField HeaderText="整改前" ConfirmTarget="Top" Width="80" CommandName="AttachUrl"
                                           HeaderTextAlign="Center" TextAlign="Center" ToolTip="整改照片" Text="详细" />                                      
                                        <f:LinkButtonField ID="del" ColumnID="del" HeaderText="删除" Width="60px"  HeaderTextAlign="Center" 
                                            CommandName="delete"  Icon="Delete" />
                                    </Columns>
                                </f:Grid>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>                               
                                <f:DropDownList ID="drpSignPerson" runat="server" Label="项目安全经理" LabelWidth="120px"
                                    LabelAlign="Right" EnableEdit="true" ShowRedStar="true" Required="true">
                                </f:DropDownList>
                                <f:Label runat="server"></f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:Grid ID="gvFlowOperate" ShowBorder="true" ShowHeader="true" EnableTableStyle="true" Collapsed="true"
                                    EnableCollapse="true" runat="server" DataIDField="FlowOperateId" AllowSorting="true"  EnableColumnLines="true"
                                    SortField="OperateTime" SortDirection="ASC" EnableTextSelection="True"  Title="审核意见">
                                    <Columns>
                                          <f:TemplateField ColumnID="tfPageIndex" Width="50px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </f:TemplateField>
                                        <f:RenderField Width="200px" ColumnID="OperateName" DataField="OperateName" 
                                            FieldType="String" HeaderText="步骤"  HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:RenderField Width="100px" ColumnID="UserName" DataField="UserName" 
                                            FieldType="String" HeaderText="操作人" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="80px" ColumnID="IsAgreeName" DataField="IsAgreeName" FieldType="String" 
                                                HeaderText="是否同意"  HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="150px" ColumnID="Opinion" DataField="Opinion"  ExpandUnusedSpace="true"
                                            FieldType="string" HeaderText="意见" HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:RenderField Width="150px" ColumnID="OperateTime" DataField="OperateTime" 
                                            FieldType="string" HeaderText="时间" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                    </Columns>
                                </f:Grid>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
        </f:Panel>
        <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
    </form>
</body>
</html>

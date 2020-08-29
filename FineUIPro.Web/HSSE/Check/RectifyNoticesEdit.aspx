<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RectifyNoticesEdit.aspx.cs" Inherits="FineUIPro.Web.HSSE.Check.RectifyNoticesEdit" %>

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
                    <Rows>
                        <f:FormRow runat="server">
                            <Items>
                                <f:TextBox ID="txtRectifyNoticesCode" runat="server" Label="编号" MaxLength="70" Readonly="true">
                                </f:TextBox>
                                <f:DropDownList ID="drpUnitId" runat="server" Label="受检单位" 
                                    LabelAlign="Right" EnableEdit="true" Readonly="true">
                                </f:DropDownList>
                                <f:DropDownList ID="drpWorkAreaId" runat="server" Label="单位工程" 
                                    LabelAlign="Right" EnableEdit="true" Readonly="true">
                                </f:DropDownList>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server" ColumnWidths="66% 34%">
                            <Items>
                                <f:DropDownList ID="drpCheckMan" runat="server" Label="检查人员" LabelAlign="Right" 
                                    EnableEdit="true" EnableMultiSelect="true" AutoPostBack="true" OnSelectedIndexChanged="drpCheckMan_SelectedIndexChanged" Readonly="true">
                                </f:DropDownList>
                                <f:TextBox runat="server" Label="检查人员" ID="txtCheckPerson" Readonly="true"></f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                   <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="检查日期" ID="txtCheckedDate" Readonly="true"
                                    LabelAlign="right" ShowRedStar="true">
                                </f:DatePicker>
                                <f:DropDownList ID="drpHiddenHazardType" runat="server" Label="隐患类别" LabelAlign="Right" EnableEdit="true" EmptyText="--请选择--" Readonly="true">
                                    <f:ListItem Text="一般" Value="1" />
                                    <f:ListItem Text="较大" Value="2" />
                                    <f:ListItem Text="重大" Value="3" />
                                </f:DropDownList>
                                <f:Label runat="server" ></f:Label>
                            </Items>
                        </f:FormRow>
                    </Rows>
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                            <Items>
                                <f:HiddenField ID="hdRectifyNoticesId" runat="server"></f:HiddenField>
                                <f:Button ID="btnSave" OnClick="btnSave_Click" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1" Hidden="true">
                                </f:Button>
                                <f:Button ID="btnSubmit" OnClick="btnSubmit_Click" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1">
                                </f:Button>
                                <f:Button ID="btnClose" EnablePostBack="false"  runat="server" Icon="SystemClose">
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
                                    DataKeyNames="RectifyNoticesItemId,IsRectify" DataIDField="RectifyNoticesItemId" AllowSorting="true" SortField="RectifyNoticesItemId"
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
                                        <f:RenderField HeaderText="主键" ColumnID="RectifyNoticesItemId" DataField="RectifyNoticesItemId"
                                            SortField="RectifyNoticesItemId" FieldType="String" HeaderTextAlign="Center" TextAlign="Center"
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
                                        <f:TemplateField ColumnID="IsRectify" HeaderText="合格" HeaderTextAlign="Center" TextAlign="Center" EnableLock="true" Locked="False" Width="60px">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpIsRectify" runat="server" OnSelectedIndexChanged="drpIsRectify_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="false">否</asp:ListItem>
                                                    <asp:ListItem Value="true">是</asp:ListItem>
                                                </asp:DropDownList>
                                                <%--<asp:Label ID="Label1" runat="server" Text='<%# ConvertIsRectify(Eval("IsRectify")) %>'></asp:Label>--%>
                                            </ItemTemplate>
                                        </f:TemplateField>
                                        <f:LinkButtonField ID="del" ColumnID="del" HeaderText="删除" Width="60px" CommandName="delete"
                                            Icon="Delete" />
                                    </Columns>
                                    <%--<Listeners>
                                        <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                                    </Listeners>--%>
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
                        <f:FormRow ID="next1">
                            <Items>
                                <f:Form ID="Form6" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Rows>
                                        <f:FormRow>
                                            <Items>
                                                <f:Panel ID="Panel1" Width="300px" ShowHeader="false" ShowBorder="false" Layout="Column" CssClass="" runat="server">
                                                    <Items>
                                                        <f:RadioButtonList runat="server" ID="rdbIsAgree" Label="是否同意" ShowRedStar="true" AutoPostBack="true" OnSelectedIndexChanged="rdbIsAgree_SelectedIndexChanged">
                                                            <f:RadioItem Text="同意" Value="true" Selected="true" />
                                                            <f:RadioItem Text="不同意" Value="false" />
                                                        </f:RadioButtonList>
                                                    </Items>
                                                </f:Panel>

                                                <f:Label runat="server" CssStyle="display:none"></f:Label>
                                            </Items>
                                        </f:FormRow>

                                        <f:FormRow ID="step1_person">
                                            <Items>
                                                <f:DropDownList ID="drpDutyPerson" runat="server" Label="接收人" LabelAlign="Right" EnableEdit="true" ShowRedStar="true">
                                                </f:DropDownList>
                                                <f:Label runat="server"></f:Label>
                                                <f:Label runat="server"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="step1_person2">
                                            <Items>
                                                <f:Panel ID="Panel3" AutoScroll="true" runat="server" ShowBorder="false" ShowHeader="false">
                                                    <Items>
                                                        <f:GroupPanel runat="server" Title="抄送人" BodyPadding="1px" ID="GroupPanel1"
                                                            EnableCollapse="True" Collapsed="false" EnableCollapseEvent="true"
                                                            EnableExpandEvent="true">
                                                            <Items>
                                                                <f:Form ID="Form8" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                                                    <Rows>
                                                                        <f:FormRow>
                                                                            <Items>
                                                                                <f:DropDownList ID="drpProfessionalEngineer" runat="server" Label="专业工程师" LabelAlign="Right" EnableEdit="true" ShowRedStar="true">
                                                                                </f:DropDownList>
                                                                                <f:DropDownList ID="drpConstructionManager" runat="server" Label="施工经理" LabelAlign="Right" EnableEdit="true">
                                                                                </f:DropDownList>
                                                                                <f:DropDownList ID="drpProjectManager" runat="server" Label="项目经理" LabelAlign="Right" EnableEdit="true">
                                                                                </f:DropDownList>
                                                                            </Items>
                                                                        </f:FormRow>
                                                                    </Rows>
                                                                </f:Form>
                                                            </Items>
                                                        </f:GroupPanel>
                                                    </Items>
                                                </f:Panel>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:TextArea runat="server" Hidden="true" ID="reason" Label="请输入原因"></f:TextArea>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:TextBox runat="server" ID="txtHandleMan" Label="办理人员" Hidden="true"></f:TextBox>
                                                <f:Label runat="server"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                    </Rows>
                                </f:Form>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="next2">
                            <Items>
                                <f:Form ID="Form4" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Rows>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="drpUnitHeadManId" runat="server" Label="施工单位负责人"  LabelWidth="120px"
                                                    LabelAlign="Right" EnableEdit="true" ShowRedStar="true">
                                                </f:DropDownList>
                                                <f:Label runat="server">
                                                </f:Label>
                                            </Items>
                                        </f:FormRow>
                                    </Rows>
                                </f:Form>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="next3">
                            <Items>
                                <f:Form ID="Form5" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Rows>
                                        <f:FormRow>
                                            <Items>
                                                <f:Panel ID="Panel2" Width="300px" ShowHeader="false" ShowBorder="false" Layout="Column" CssClass="" runat="server">
                                                    <Items>
                                                        <f:RadioButtonList runat="server" ID="rdbUnitHeadManAgree" Label="是否同意" ShowRedStar="true" AutoPostBack="true" OnSelectedIndexChanged="rdbUnitHeadManAgree_SelectedIndexChanged">
                                                            <f:RadioItem Text="同意" Value="true" Selected="true" />
                                                            <f:RadioItem Text="不同意" Value="false" />
                                                        </f:RadioButtonList>
                                                    </Items>
                                                </f:Panel>

                                                <f:Label runat="server" CssStyle="display:none" LabelWidth="170px"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ID="step3_CheckPerson">
                                            <Items>
                                                <f:DropDownList ID="drpCheckPerson" runat="server" Label="安全经理/安全工程师"  LabelWidth="170px"
                                                    LabelAlign="Right" EnableEdit="true" ShowRedStar="true">
                                                </f:DropDownList>
                                                <f:Label runat="server"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:TextArea runat="server" Hidden="true" ID="reason1" Label="请输入原因"></f:TextArea>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:TextBox runat="server" ID="txtHandleMan1" Label="办理人员" Hidden="true"></f:TextBox>
                                                <f:Label runat="server"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                    </Rows>
                                </f:Form>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="next4">
                            <Items>
                                <f:Form ID="Form7" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Rows>
                                        <f:FormRow>
                                            <Items>
                                                <f:DropDownList ID="drpReCheckOpinion" runat="server" Label="复查意见" LabelAlign="Right" EnableEdit="true" Readonly="true">
                                                    <f:ListItem Text="不通过" Value="False" />
                                                    <f:ListItem Text="通过" Value="True" />
                                                </f:DropDownList>
                                                <f:TextBox runat="server" Label="整改意见" ID="opinion"></f:TextBox>
                                            </Items>
                                        </f:FormRow>
                                    </Rows>
                                </f:Form>
                            </Items>
                        </f:FormRow>
                    </Rows>
                    
                </f:Form>
                <f:Form ID="Form9" ShowBorder="true" ShowHeader="false" Title="审核步骤" AutoScroll="true"
                    runat="server" RedStarPosition="BeforeText" LabelAlign="Right" EnableTableStyle="true">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:Grid ID="gvFlowOperate" ShowBorder="true" ShowHeader="false" EnableTableStyle="true"
                                    EnableCollapse="true" runat="server" DataIDField="FlowOperateId" AllowSorting="true"  EnableColumnLines="true"
                                    SortField="OperateTime" SortDirection="ASC" EnableTextSelection="True"  ForceFit="true">
                                    <Columns>
                                          <f:TemplateField ColumnID="tfPageIndex" Width="55px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </f:TemplateField>
                                        <f:RenderField Width="120px" ColumnID="OperateName" DataField="OperateName" 
                                            FieldType="String" HeaderText="操作步骤"  HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:RenderField Width="100px" ColumnID="UserName" DataField="UserName" 
                                            FieldType="String" HeaderText="操作人" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="80px" ColumnID="IsAgreeName" DataField="IsAgreeName" FieldType="String" 
                                                HeaderText="是否同意"  HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="150px" ColumnID="Opinion" DataField="Opinion"  ExpandUnusedSpace="true"
                                            FieldType="string" HeaderText="操作意见" HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:RenderField Width="150px" ColumnID="OperateTime" DataField="OperateTime" 
                                            FieldType="string" HeaderText="操作时间" HeaderTextAlign="Center" TextAlign="Center">
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
        <%--<f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
                runat="server" Text="修改" Icon="TableEdit">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
                ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除"
                Icon="Delete">
            </f:MenuButton>
        </f:Menu>--%>
    </form>
    <%--<script type="text/javascript">
        var menuID = '<%= Menu1.ClientID %>';

        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>--%>
</body>
</html>

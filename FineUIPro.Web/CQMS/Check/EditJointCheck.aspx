<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditJointCheck.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.EditJointCheck" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>质量共检</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <script src="../../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
        .labcenter {
            text-align: center;
        }

        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }

        .f-grid-row.red {
            background-color: #FF7575;
            background-image: none;
        }

        .fontred {
            color: #FF7575;
            background-image: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel2" Title="质量共检" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>

                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtProjectName" runat="server" LabelWidth="120px" Readonly="true" Label="项目名称" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                            <f:TextBox ID="txtJointCheckCode" runat="server" LabelWidth="110px" Required="true" ShowRedStar="true" Label="编号" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:DropDownList ID="drpUnit" LabelWidth="120px" EmptyText="--请选择--" AutoSelectFirstItem="false"
                                                ShowRedStar="true" runat="server" Required="true" Label="受检施工单位" AutoPostBack="true" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged" 
                                                LabelAlign="Right" EnableEdit="true">
                                            </f:DropDownList>
                                            <f:DropDownList ID="drpCheckType" LabelWidth="110px" EmptyText="--请选择--" AutoSelectFirstItem="false"
                                                ShowRedStar="true" runat="server" Required="true" Label="检查类别" LabelAlign="Right" EnableEdit="true">
                                            </f:DropDownList>
                                        </Items>
                                    </f:FormRow>

                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtCheckName" runat="server" LabelWidth="120px" Label="检查名称" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                            <f:DatePicker ID="txtCheckDate" ShowRedStar="true" LabelWidth="110px" runat="server" Label="共检日期" Required="true" LabelAlign="Right"
                                                EnableEdit="true">
                                            </f:DatePicker>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow ColumnWidths="50% 50%">
                                        <Items>
                                            <f:DropDownList ID="drpProposeUnit" ShowRedStar="true"  EmptyText="--请选择--" AutoSelectFirstItem="false"
                                                LabelWidth="120px" Required="true" runat="server" Label="提出单位" LabelAlign="Right" EnableEdit="true">
                                            </f:DropDownList>
                                            <f:Label runat="server" Hidden="true"></f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:Label runat="server" Label="参与共检人" LabelWidth="120px"></f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:DropDownList ID="drpJointCheckMans1" runat="server" Label="总承包商" EmptyText="--请选择--" AutoSelectFirstItem="false" EnableMultiSelect="true" LabelWidth="120px" LabelAlign="Right" EnableEdit="true"  AutoPostBack="true" OnSelectedIndexChanged="drpJointCheckMans1_SelectedIndexChanged">
                                            </f:DropDownList>
                                            <f:DropDownList ID="drpJointCheckMans2" runat="server" Label="施工单位" EmptyText="--请选择--" AutoSelectFirstItem="false" EnableMultiSelect="true" LabelAlign="Right" EnableEdit="true"  AutoPostBack="true" OnSelectedIndexChanged="drpJointCheckMans2_SelectedIndexChanged">
                                            </f:DropDownList>
                                            <f:DropDownList ID="drpJointCheckMans3" runat="server" Label="监理单位" EmptyText="--请选择--" AutoSelectFirstItem="false" EnableMultiSelect="true" LabelAlign="Right" EnableEdit="true"  AutoPostBack="true" OnSelectedIndexChanged="drpJointCheckMans3_SelectedIndexChanged">
                                            </f:DropDownList>
                                            <f:DropDownList ID="drpJointCheckMans4" runat="server" Label="建设单位" EmptyText="--请选择--" AutoSelectFirstItem="false" EnableMultiSelect="true" LabelAlign="Right" EnableEdit="true" AutoPostBack="true" OnSelectedIndexChanged="drpJointCheckMans4_SelectedIndexChanged">
                                            </f:DropDownList>
                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow ID="plApprove">
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="共检问题列表" EnableCollapse="false" runat="server"
                            BoxFlex="1" DataKeyNames="JointCheckDetailId" AllowCellEditing="true" EnableColumnLines="true" ForceFit="true"
                            ClicksToEdit="1" DataIDField="JointCheckDetailId" AllowSorting="true"  SortField="CreateDate" OnPreDataBound="Grid1_PreDataBound"
                            OnSort="Grid1_Sort" EnableTextSelection="True" OnRowCommand="Grid1_RowCommand" OnRowDataBound="Grid1_RowDataBound">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar4" ToolbarAlign="Right" runat="server">
                                    <Items>
                                        <f:Button ID="btnNew" OnClick="btnNew_Click" Icon="Add" EnablePostBack="true" runat="server">
                                        </f:Button>

                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:RenderField HeaderText="主键" ColumnID="JointCheckDetailId" DataField="JointCheckDetailId"
                                    SortField="JointCheckDetailId" FieldType="String" HeaderTextAlign="Center" TextAlign="Center"
                                    Hidden="true">
                                </f:RenderField>
                                <f:TemplateField ColumnID="UnitWorkId" Width="200px" HeaderText="单位工程<font color='red'>(*)</font>" HeaderTextAlign="Center" TextAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="drpUnitWork" runat="server" Height="22" Width="98%">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdUnitWork" runat="server" Value='<%# Bind("UnitWorkId") %>' />
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:TemplateField ColumnID="CNProfessionalCode" Width="100px" HeaderText="专业<font color='red'>(*)</font>" HeaderTextAlign="Center" TextAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="drpCNProfessional" runat="server" Height="22" Width="90%">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdCNProfessional" runat="server" Value='<%# Bind("CNProfessionalCode") %>' />
                                    </ItemTemplate>
                                </f:TemplateField>

                                <f:RenderField HeaderText="部位<font color='red'>(*)</font>" ColumnID="CheckSite" DataField="CheckSite"
                                    HeaderTextAlign="Center" TextAlign="Center" Width="130px" FieldType="String">
                                    <Editor>
                                        <f:TextBox runat="server" ID="txtcheck_Site" Text='<%# Bind("CheckSite") %>' MaxLength="500">
                                        </f:TextBox>
                                    </Editor>
                                </f:RenderField>

                                <f:RenderField HeaderText="问题描述<font color='red'>(*)</font>" ColumnID="QuestionDef" DataField="QuestionDef"
                                    HeaderTextAlign="Center" TextAlign="Center" Width="150px" FieldType="String" >
                                    <Editor>
                                        <f:TextArea runat="server" AutoGrowHeight="true" AutoGrowHeightMin="60" AutoGrowHeightMax="300" ID="txtQuestion_Def" Text='<%# Bind("QuestionDef") %>'>
                                        </f:TextArea>
                                    </Editor>
                                </f:RenderField>


                                <f:TemplateField ColumnID="QuestionType" Width="100px" HeaderText="问题类别<font color='red'>(*)</font>" HeaderTextAlign="Center" TextAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="drpQuestionType" runat="server" Height="22" Width="99%">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdQuestionType" runat="server" Value='<%# Bind("QuestionType") %>' />
                                    </ItemTemplate>
                                </f:TemplateField>

                                <f:RenderField HeaderText="整改建议" ColumnID="RectifyOpinion" DataField="RectifyOpinion"
                                    HeaderTextAlign="Center" TextAlign="Center" Width="100px" FieldType="String">
                                    <Editor>
                                        <f:TextArea runat="server" AutoGrowHeight="true" AutoGrowHeightMin="60" AutoGrowHeightMax="300" ID="txt_RectifyOpinion" Text='<%# Bind("RectifyOpinion") %>'>
                                        </f:TextArea>

                                    </Editor>
                                </f:RenderField>

                                <f:TemplateField ColumnID="LimitDate" Width="120px" HeaderText="整改时间<font color='red'>(*)</font>" HeaderTextAlign="Center" TextAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="LimitDate" runat="server" Text='<%# ConvertDate(Eval("LimitDate")) %>'
                                            Width="98%" CssClass="Wdate" Style="width: 98%; cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"
                                            BorderStyle="None">
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </f:TemplateField>


                                <f:RenderField HeaderText="排序时间" ColumnID="CreateDate" DataField="CreateDate"
                                    SortField="CreateDate" FieldType="String" HeaderTextAlign="Center" TextAlign="Center"
                                    Hidden="true">
                                </f:RenderField>

                                <f:LinkButtonField Width="90px" HeaderText="问题图片" ConfirmTarget="Top" CommandName="attchUrl"
                                    TextAlign="Center" ToolTip="上传" Text="上传" />

                                <f:TemplateField ColumnID="State" Width="105px" HeaderText="办理步骤" HeaderTextAlign="Center" TextAlign="Center"
                                    EnableLock="true" Locked="False">
                                    <ItemTemplate>
                                        <b>分包专工回复</b>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:TemplateField ColumnID="HandleMan" Width="120px" HeaderText="办理人<font color='red'>(*)</font>" HeaderTextAlign="Center" TextAlign="Center" >
                                    <ItemTemplate>
                                        <asp:DropDownList ID="drpHandleMan" runat="server" Height="22" Width="90%">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdHandleMan" runat="server" Value='<%# Bind("HandleMan") %>' />
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:LinkButtonField HeaderText="删除" Width="60px" CommandName="delete"
                                    Icon="Delete" />
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                    </Items>
                </f:FormRow>

            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:Label runat="server" ID="lbTemp">
                        </f:Label>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" OnClick="btnSave_Click" runat="server" ToolTip="保存" ValidateForms="SimpleForm1">
                        </f:Button>
                        <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" OnClick="btnSubmit_Click" ToolTip="提交" ValidateForms="SimpleForm1">
                        </f:Button>
                        <%--  <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" OnClick="btnClose_Click" runat="server" Icon="SystemClose">
                        </f:Button>--%>
                        <f:HiddenField ID="hdId" runat="server">
                        </f:HiddenField>
                        <f:HiddenField ID="hdAttachUrl" runat="server">
                        </f:HiddenField>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
        <f:Window ID="Window1" Title="质量共检记录" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true"
            Width="1100px" Height="520px">
        </f:Window>
        <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" OnClose="WindowAtt_Close" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuEdit" EnablePostBack="true"
                Icon="Pencil" runat="server" Text="">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDelete" EnablePostBack="true"
                Icon="Delete" ConfirmText="确定删除当前数据？" ConfirmTarget="Parent" runat="server" Text="删除">
            </f:MenuButton>
        </f:Menu>

    </form>

    <script>

        var menuID = '<%= Menu1.ClientID %>';

        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>
</body>
</html>

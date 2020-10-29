<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditJointCheckTwo.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.EditJointCheckTwo" %>

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
    <script src="/Scripts/laydate/laydate.js"></script>
    <script type="text/javascript">
        //laydate.render({
        //    elem: '.txtRectifyDate'
        //    , position: 'fixed'
        //    , lang: 'zh-cn'//指定元素


        function myload() {
            laydate.render({
                elem: '#SimpleForm1_ctl02_txtOpinions-inputEl'
            });
        }
    </script>
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
                                                MaxLength="50" Enabled="false">
                                            </f:TextBox>
                                            <f:TextBox ID="txtJointCheckCode" runat="server" LabelWidth="110px" Required="true" ShowRedStar="true" Label="编号" LabelAlign="Right"
                                                MaxLength="50" Enabled="false">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:DropDownList ID="drpUnit" LabelWidth="120px" EmptyText="--请选择--" AutoSelectFirstItem="false"
                                                AutoPostBack="true" ShowRedStar="true" runat="server" Required="true" Label="受检施工单位"
                                                LabelAlign="Right" EnableEdit="true" Enabled="false">
                                            </f:DropDownList>
                                            <f:DropDownList ID="drpCheckType" LabelWidth="110px" EmptyText="--请选择--" AutoSelectFirstItem="false"
                                                Enabled="false" ShowRedStar="true" runat="server" Required="true" Label="检查类别" LabelAlign="Right" EnableEdit="true">
                                            </f:DropDownList>
                                        </Items>
                                    </f:FormRow>

                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtCheckName" runat="server" LabelWidth="120px" Label="检查名称" LabelAlign="Right"
                                                MaxLength="50" Enabled="false">
                                            </f:TextBox>
                                            <f:DatePicker ID="txtCheckDate" ShowRedStar="true" LabelWidth="110px" runat="server" Label="共检日期" Required="true" LabelAlign="Right"
                                                EnableEdit="true" Enabled="false">
                                            </f:DatePicker>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:DropDownList ID="drpProposeUnit" ShowRedStar="true"  EmptyText="--请选择--" AutoSelectFirstItem="false"
                                                LabelWidth="120px" Enabled="false" Required="true" runat="server" Label="提出单位" LabelAlign="Right" EnableEdit="true">
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
                                            <f:TextBox ID="txtJointCheckMans1" runat="server" LabelWidth="120px" Enabled="false" Readonly="true" Label="总承包商" LabelAlign="Right"
                                               >
                                            </f:TextBox>
                                            <f:TextBox ID="txtJointCheckMans2" runat="server" Readonly="true" Enabled="false" Label="施工单位" LabelAlign="Right"
                                               >
                                            </f:TextBox>
                                            <f:TextBox ID="txtJointCheckMans3" runat="server" Readonly="true" Enabled="false" Label="监理单位" LabelAlign="Right"
                                               >
                                            </f:TextBox>
                                            <f:TextBox ID="txtJointCheckMans4" runat="server" Readonly="true" Enabled="false" Label="建设单位" LabelAlign="Right"
                                                >
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="共检问题列表" EnableCollapse="false" runat="server"
                            BoxFlex="1" DataKeyNames="JointCheckDetailId" AllowCellEditing="true" EnableColumnLines="true" 
                            ClicksToEdit="1" DataIDField="JointCheckDetailId"  OnRowCommand="Grid1_RowCommand" AllowSorting="true" ForceFit="true" SortField="CreateDate"
                            EnableTextSelection="True">
                            <Columns>
                                <f:GroupField HeaderText="总包填写内容" TextAlign="Center">
                                    <Columns>
                                        <f:RenderField HeaderText="主键" ColumnID="JointCheckDetailId" DataField="JointCheckDetailId"
                                            SortField="JointCheckDetailId" FieldType="String" HeaderTextAlign="Center" TextAlign="Center"
                                            Hidden="true">
                                        </f:RenderField>
                                        <f:RenderField Width="100px" ColumnID="UnitWorkId" DataField="UnitWorkId"
                                            SortField="UnitWorkId" FieldType="String" HeaderText="单位工程" TextAlign="Center"
                                            HeaderTextAlign="Center">
                                        </f:RenderField>

                                        <f:RenderField ColumnID="CNProfessionalCode" Width="60px" DataField="CNProfessionalCode"
                                            SortField="CNProfessionalCode" FieldType="String" HeaderText="专业" TextAlign="Center"
                                            HeaderTextAlign="Center">
                                        </f:RenderField>

                                        <f:RenderField ColumnID="CheckSite" DataField="CheckSite"
                                            SortField="CheckSite" FieldType="String" HeaderText="部位" TextAlign="Center"
                                            HeaderTextAlign="Center">
                                        </f:RenderField>

                                        <f:RenderField ColumnID="QuestionDef" DataField="QuestionDef"
                                            SortField="QuestionDef" FieldType="String" HeaderText="问题描述" TextAlign="Center"
                                            HeaderTextAlign="Center">
                                        </f:RenderField>

                                        <f:RenderField ColumnID="QuestionType" DataField="QuestionType"
                                            SortField="QuestionType" FieldType="String" HeaderText="问题类别" TextAlign="Center"
                                            HeaderTextAlign="Center">
                                        </f:RenderField>

                                        <f:RenderField ColumnID="RectifyOpinion" DataField="RectifyOpinion"
                                            SortField="RectifyOpinion" FieldType="String" HeaderText="整改建议" TextAlign="Center"
                                            HeaderTextAlign="Center">
                                        </f:RenderField>

                                        <f:RenderField ColumnID="LimitDate" DataField="LimitDate"
                                            SortField="LimitDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="整改时间" TextAlign="Center"
                                            HeaderTextAlign="Center">
                                        </f:RenderField>

                                        <f:LinkButtonField HeaderText="问题图片" ConfirmTarget="Top" Width="80" CommandName="attchUrl"
                                            TextAlign="Center" ToolTip="问题图片" Text="问题图片" />
                                    </Columns>

                                </f:GroupField>

                                <f:GroupField HeaderText="分包填写内容" TextAlign="Center">
                                    <Columns>

                                        <f:RenderField HeaderText="整改方案<font color='red'>(*)</font>" ColumnID="HandleWay" DataField="HandleWay"
                                            HeaderTextAlign="Center" TextAlign="Center" Width="100px" FieldType="String">
                                            <Editor>
                                                <f:TextArea runat="server" AutoGrowHeight="true" AutoGrowHeightMin="90" AutoGrowHeightMax="300" ID="txt_HandleWay" Text='<%# Bind("HandleWay") %>'>
                                                </f:TextArea>

                                            </Editor>
                                        </f:RenderField>

                                        <f:TemplateField ColumnID="RectifyDate" Width="125px" HeaderText="实际整改时间<font color='red'>(*)</font>" HeaderTextAlign="Center" TextAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="RectifyDate" runat="server" Text='<%# ConvertDate(Eval("RectifyDate")) %>'
                                            Width="98%" CssClass="Wdate" Style="width: 98%; cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"
                                            BorderStyle="None">
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </f:TemplateField>
                                        
                                        <f:LinkButtonField HeaderText="整改照片" ConfirmTarget="Top" Width="80" CommandName="ReAttachUrl"
                                            TextAlign="Center" ToolTip="整改照片" Text="整改照片" />
                                    </Columns>
                                </f:GroupField>

                                <f:GroupField HeaderText="办理流程" TextAlign="Center">
                                    <Columns>
                                        <f:TemplateField ColumnID="HandleType" Width="130px" HeaderText="办理步骤" HeaderTextAlign="Center" TextAlign="Center"
                                            EnableLock="true" Locked="False">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdState" runat="server" Value='<%# Bind("state") %>' />
                                                <asp:DropDownList ID="drpHandleType" runat="server" Height="22" Width="90%"
                                                    OnSelectedIndexChanged="drpHandleType_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </f:TemplateField>

                                        <f:TemplateField ColumnID="HandleMan" Width="100px" HeaderText="办理人" HeaderTextAlign="Center" TextAlign="Center"
                                            EnableLock="true" Locked="False">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpHandleMan" runat="server"  Height="22" Width="90%">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdHandleMan" runat="server" Value='<%# Bind("HandleMan") %>' />

                                            </ItemTemplate>
                                        </f:TemplateField>
                                    </Columns>
                                </f:GroupField>

                            </Columns>
                        </f:Grid>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <%--<f:Label runat="server" Text="我的意见：" Width="20px" ShowLabel="false"></f:Label>--%>
                        <f:TextArea runat="server" CssStyle="padding:5px" AutoGrowHeight="true" Label="我的意见" AutoGrowHeightMin="100"
                            ID="txtOpinions">
                        </f:TextArea>

                    </Items>
                </f:FormRow>
                <f:FormRow ID="plApprove">
                    <Items>
                        <f:Grid ID="gvApprove" ShowBorder="true" ShowHeader="true" Title="质量共检审批列表" EnableCollapse="false" runat="server"
                            BoxFlex="1" DataKeyNames="JointCheckApproveId" AllowCellEditing="true" EnableColumnLines="true" ForceFit="true"
                            ClicksToEdit="1" DataIDField="JointCheckApproveId" AllowSorting="true" SortField="JointCheckDetailId"
                            EnableTextSelection="True">
                            <Columns>
                                <f:RowNumberField Width="40px" />
                                <f:RenderField HeaderText="主键" ColumnID="JointCheckApproveId" DataField="JointCheckApproveId"
                                    SortField="JointCheckApproveId" FieldType="String" HeaderTextAlign="Center" TextAlign="Center"
                                    Hidden="true">
                                </f:RenderField>

                                <f:TemplateField ColumnID="ApproveType" Width="140px" HeaderText="办理类型" HeaderTextAlign="Center" TextAlign="Center"
                                    EnableLock="true" Locked="False">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# ConvertState(Eval("ApproveType")) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>

                                <f:BoundField Width="200px" DataField="ApproveDate" HeaderTextAlign="Center" TextAlign="Center" DataFormatString="{0:yyyy-MM-dd}" HeaderText="办理时间" />

                                <f:RenderField HeaderText="办理人员" ColumnID="ApproveMan" DataField="ApproveMan"
                                    SortField="ApproveMan" FieldType="String" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>

                                <f:RenderField HeaderText="办理意见" ColumnID="ApproveIdea" DataField="ApproveIdea"
                                    SortField="ApproveIdea" FieldType="String" HeaderTextAlign="Center" TextAlign="Center"  >
                                </f:RenderField>



                            </Columns>
                        </f:Grid>
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
            Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
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

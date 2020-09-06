<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JointCheckView.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.JointCheckView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>质量共检</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
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
                                            <f:TextBox ID="txtProjectName"  LabelWidth="120" runat="server" Label="项目名称" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                            <f:TextBox ID="txtJointCheckCode"  runat="server" Label="编号" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>

                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="drpUnit"  LabelWidth="120" runat="server" Label="受检施工单位" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                            <f:TextBox ID="drpCheckType" runat="server" Label="检查类别" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>

                                        </Items>
                                    </f:FormRow>

                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtCheckName"  LabelWidth="120" runat="server" Label="检查名称" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                            <f:TextBox ID="txtCheckDate" runat="server" Label="共检日期" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>

                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="drpProposeUnit"  LabelWidth="120" runat="server" Label="提出单位" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                            <f:Label runat="server" Hidden="true"></f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:Label runat="server" Label="参与共检人" LabelWidth="110px"></f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtJointCheckMans1" runat="server" LabelWidth="110px" Readonly="true" Label="总承包商" LabelAlign="Right"
                                               >
                                            </f:TextBox>
                                            <f:TextBox ID="txtJointCheckMans2" runat="server" Readonly="true" Label="施工单位" LabelAlign="Right"
                                               >
                                            </f:TextBox>
                                            <f:TextBox ID="txtJointCheckMans3" runat="server" Readonly="true" Label="监理单位" LabelAlign="Right"
                                               >
                                            </f:TextBox>
                                            <f:TextBox ID="txtJointCheckMans4" runat="server" Readonly="true" Label="建设单位" LabelAlign="Right"
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

                        <f:Grid ID="gvJoinCheckDetail" ShowBorder="true"  ShowHeader="true" Title="共检问题列表" EnableCollapse="false"
                            runat="server"
                            BoxFlex="1" DataKeyNames="JointCheckDetailId" AllowCellEditing="true" EnableColumnLines="true"  
                            ClicksToEdit="1"  DataIDField="JointCheckDetailId" OnRowCommand="gvJoinCheckDetail_RowCommand" AllowSorting="true" ForceFit="true" SortField="CreateDate"
                            EnableTextSelection="True" >
                            <Columns>
                                <f:GroupField HeaderText="总包填写内容" TextAlign="Center">
                                    <Columns>
                                        <f:RenderField HeaderText="主键" ColumnID="JointCheckDetailId" DataField="JointCheckDetailId"
                                            SortField="JointCheckDetailId" FieldType="String" HeaderTextAlign="Center" TextAlign="Center"
                                            Hidden="true">
                                        </f:RenderField>
                                        <f:RenderField ColumnID="UnitWorkName" DataField="UnitWorkName"
                                            SortField="UnitWorkName" FieldType="String" HeaderText="单位工程" TextAlign="Center"
                                            HeaderTextAlign="Center">
                                        </f:RenderField>

                                        <f:RenderField Width="100px" ColumnID="ProfessionalName" DataField="ProfessionalName"
                                            SortField="ProfessionalName" FieldType="String" HeaderText="专业" TextAlign="Center"
                                            HeaderTextAlign="Center">
                                        </f:RenderField>

                                        <f:RenderField ColumnID="CheckSite" DataField="CheckSite"
                                            SortField="CheckSite" FieldType="String" HeaderText="部位" TextAlign="Center"
                                            HeaderTextAlign="Center">
                                        </f:RenderField>

                                        <f:RenderField ColumnID="QuestionDef" DataField="QuestionDef"
                                            SortField="QuestionDef" FieldType="String" HeaderText="问题描述" TextAlign="Center"
                                            HeaderTextAlign="Center" >
                                        </f:RenderField>

                                        <f:RenderField ColumnID="QuestionTypeStr" DataField="QuestionTypeStr"
                                            SortField="QuestionTypeStr" FieldType="String" HeaderText="问题类别" TextAlign="Center"
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
                                        <%--<f:ImageField  ImageUrl=""></f:ImageField>--%>
                                        <f:LinkButtonField HeaderText="问题图片" ConfirmTarget="Top" Width="80" CommandName="attchUrl"
                                            TextAlign="Center" ToolTip="问题图片" Text="问题图片" />
                                    </Columns>

                                </f:GroupField>

                                <f:GroupField HeaderText="分包填写内容" TextAlign="Center">
                                    <Columns>
                                        <f:RenderField ColumnID="HandleWay" DataField="HandleWay"
                                            SortField="HandleWay" FieldType="String" HeaderText="整改方案" TextAlign="Center"
                                            HeaderTextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField ColumnID="RectifyDate" DataField="RectifyDate" Width="110px"
                                            SortField="RectifyDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="实际整改时间" TextAlign="Center"
                                            HeaderTextAlign="Center">
                                        </f:RenderField>

                                        <f:LinkButtonField HeaderText="整改照片" ConfirmTarget="Top" Width="80" CommandName="ReAttachUrl"
                                            TextAlign="Center" ToolTip="整改照片" Text="整改照片" />
                                    </Columns>
                                </f:GroupField>

                                <f:GroupField HeaderText="办理流程" TextAlign="Center">
                                    <Columns>
                                        <f:TemplateField ColumnID="State" Width="130px" HeaderText="审批状态" HeaderTextAlign="Center" TextAlign="Center"
                                            EnableLock="true" Locked="False">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# ConvertState(Eval("State")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </f:TemplateField>

                                        <f:TemplateField ColumnID="JointCheckId"  HeaderText="办理人" HeaderTextAlign="Center" TextAlign="Center"
                                             >
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# ConvertMan(Eval("HandleMan")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </f:TemplateField>
                                    </Columns>
                                </f:GroupField>

                            </Columns>
                        </f:Grid>
                    </Items>
                </f:FormRow>

                <f:FormRow ID="plApprove">
                    <Items>
                        <f:Grid ID="gvApprove" ShowBorder="true" ShowHeader="true" Title="质量共检审批列表" EnableCollapse="false" runat="server"
                            BoxFlex="1" DataKeyNames="JointCheckApproveId" AllowCellEditing="true" EnableColumnLines="true"
                            ClicksToEdit="1" DataIDField="JointCheckApproveId" AllowSorting="true" SortField="JointCheckDetailId" ForceFit="true"
                            EnableTextSelection="True">
                            <Columns>
                                <f:RowNumberField Width="40px" />
                                <f:RenderField HeaderText="主键" ColumnID="JointCheckApproveId" DataField="JointCheckApproveId"
                                    SortField="JointCheckApproveId" FieldType="String" HeaderTextAlign="Center" TextAlign="Center"
                                    Hidden="true">
                                </f:RenderField>

                               <%-- <f:RenderField ColumnID="ApproveType" DataField="ApproveType"
                                    SortField="ApproveType" FieldType="String" HeaderText="办理类型" TextAlign="Left"
                                    HeaderTextAlign="Center">
                                </f:RenderField>--%>
                                 <f:TemplateField ColumnID="ApproveType" Width="140px"   HeaderText="办理类型" HeaderTextAlign="Center" TextAlign="Center"
                                             >
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# ConvertState(Eval("ApproveType")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </f:TemplateField>

                                <f:RenderField ColumnID="ApproveMan" DataField="ApproveMan"
                                    SortField="ApproveMan" FieldType="String" HeaderText="办理人员" TextAlign="Center"
                                    HeaderTextAlign="Center">
                                </f:RenderField>

                                <f:BoundField Width="200px" DataField="ApproveDate" HeaderTextAlign="Center" TextAlign="Center" DataFormatString="{0:yyyy-MM-dd}" HeaderText="办理时间" />


                                <f:RenderField ColumnID="ApproveIdea" DataField="ApproveIdea"
                                    SortField="ApproveIdea" FieldType="String" HeaderText="办理意见" TextAlign="Center"
                                    HeaderTextAlign="Center" >
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
                        <f:HiddenField ID="hdId" runat="server">
                        </f:HiddenField>
                        <f:HiddenField ID="hdAttachUrl" runat="server">
                        </f:HiddenField>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
        <f:Window ID="Window1" Title="质量共检记录" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true"
            Width="1100px" Height="520px">
        </f:Window>
        <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>


    </form>

    <script>

        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>
</body>
</html>

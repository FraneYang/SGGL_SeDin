<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckListView.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.CheckListView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>质量巡检查看</title>
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
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" Enabled="true" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel2" Title="总包填写内容" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>

                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtProjectName" runat="server" Readonly="true" Label="项目名称" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                            <f:TextBox ID="txtDocCode" runat="server" Required="true" Readonly="true" ShowRedStar="true" Label="编号" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="drpUnit" runat="server" Readonly="true" Label="施工单位" MaxLength="50">
                                            </f:TextBox>
                                            <f:TextBox ID="drpUnitWork" runat="server" Readonly="true" Label="单位工程" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="drpCNProfessional" runat="server" Readonly="true" Label="专业" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                            <f:TextBox ID="drpQuestionType" runat="server" Readonly="true" Label="问题类别" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>

                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtCheckSite" Readonly="true" ShowRedStar="true" runat="server" Label="部位" Required="true" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                            <f:TextBox ID="txtCheckDate" Readonly="true" ShowRedStar="true" runat="server" Label="巡检时间" Required="true" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>

                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="drpProposeUnit" runat="server" Readonly="true" Label="提出单位" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                            <f:TextBox ID="txtLimitDate" runat="server" Readonly="true" Label="整改时间" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextArea ID="txtQuestionDef" ShowRedStar="true" Required="true" Readonly="true" runat="server" Label="问题描述" MaxLength="3000">
                                            </f:TextArea>

                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextArea ID="txtRectifyOpinion" runat="server" Label="整改意见" Readonly="true" MaxLength="3000">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:Panel ID="Panel1" ShowHeader="false" ShowBorder="false" Layout="Column" runat="server">
                                                <Items>
                                                    <f:Label runat="server" Text="问题图片：" CssStyle="padding-left:25px" Width="110px" CssClass="marginr" ShowLabel="false"></f:Label>
                                                    <f:Button ID="imgBtnFile" Text="问题图片" ToolTip="上传及查看" Icon="TableCell" runat="server"
                                                        OnClick="imgBtnFile_Click">
                                                    </f:Button>

                                                </Items>
                                            </f:Panel>

                                        </Items>
                                    </f:FormRow>

                                </Rows>
                            </f:Form>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>

                        <f:ContentPanel ID="ContentPanel3" Title="分包填写内容" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Form ID="For" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextArea ID="txtHandleWay" ShowRedStar="true" Readonly="true" Required="true" runat="server" Label="整改方案" MaxLength="3000">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>                           
                                            <f:DatePicker ID="txtRectifyDate" ShowRedStar="true" Readonly="true" Required="true" runat="server" Label="整改时间" LabelAlign="Right"
                                                EnableEdit="true">
                                            </f:DatePicker>
                                            <f:Label runat="server"  Hidden="true"></f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:Panel ID="Panel2" ShowHeader="false" ShowBorder="false" Layout="Column" runat="server">
                                                <Items>
                                                    <f:Label ID="lblAttach" CssStyle="padding-left:23px" Width="100px" runat="server" CssClass="marginr" ShowLabel="false"
                                                        Text="整改图片：">
                                                    </f:Label>
                                                    <f:Button ID="btnAttach" Text="整改图片" ToolTip="上传及查看" Icon="TableCell" runat="server" OnClick="btnAttach_Click">
                                                    </f:Button>

                                                </Items>
                                            </f:Panel>
                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>
                        </f:ContentPanel>

                    </Items>
                </f:FormRow>
                <f:FormRow ID="plApprove">
                    <Items>
                        <f:ContentPanel Title="质量巡检审批列表" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Grid ID="gvApprove" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" runat="server" EnableCollapse="false"
                                DataKeyNames="CheckControlApproveId" EnableColumnLines="true" ForceFit="true">
                                <Columns>
                                    <f:RowNumberField Width="20px" />
                                    <f:TemplateField ColumnID="State" Width="250px" HeaderText="办理类型" HeaderTextAlign="Center" TextAlign="Center"
                                        EnableLock="true" Locked="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lbtype" runat="server" Text='<%# ConvertState(Eval("ApproveType")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </f:TemplateField>
                                    <f:BoundField Width="180px" DataField="ApproveMan" HeaderTextAlign="Center" TextAlign="Center" HeaderText="办理人员" />
                                    <f:BoundField Width="200px" DataField="ApproveDate" HeaderTextAlign="Center" TextAlign="Center" DataFormatString="{0:yyyy-MM-dd}" HeaderText="办理时间" />
                                    <f:BoundField Width="180px" DataField="ApproveIdea" HeaderTextAlign="Center" TextAlign="Center"  HeaderText="办理意见" />

                                </Columns>
                            </f:Grid>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                    </Items>
                </f:FormRow>

            </Rows>

        </f:Form>
        <f:Window ID="Window1" Title="编辑检查项明细" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true"
            Width="1100px" Height="520px">
        </f:Window>
        <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuEdit" EnablePostBack="true"
                Icon="Pencil" runat="server" Text="编辑">
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

        function onGridDataLoad(event) {
            this.mergeColumns(['CheckItemType']);
        }
    </script>
</body>
</html>

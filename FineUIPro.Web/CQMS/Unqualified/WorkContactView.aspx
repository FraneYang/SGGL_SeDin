<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkContactView.aspx.cs" Inherits="FineUIPro.Web.CQMS.Unqualified.WorkContactView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>工程联络单</title>
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
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel2" Title="工作联系单" ShowBorder="true"
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
                                            <f:TextBox ID="txtCode" runat="server" Readonly="true"  Label="编号" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>

                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="drpUnit" runat="server" Readonly="true" Label="提出单位" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                            <f:TextBox ID="rblIsReply" runat="server" Readonly="true" Label="答复" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtMainSendUnit" runat="server" Readonly="true" Label="主送单位" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                            <f:TextBox ID="txtCCUnit" runat="server" Readonly="true" Label="抄送单位" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtCause" runat="server" Label="事由" Readonly="true" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                            <f:Panel ID="CCfile" ShowHeader="false" ShowBorder="false" Layout="Column" runat="server">
                                                <Items>
                                                    <f:Label runat="server" Text="附件：" CssStyle="padding-left:52px" LabelWidth="100px" Width="100px" CssClass="marginr" ShowLabel="false"></f:Label>
                                                    <f:Button ID="imgfile" Text="附件" ToolTip="上传及查看" Icon="TableCell" runat="server" OnClick="imgfile_Click" >
                                                    </f:Button>
                                                </Items>
                                            </f:Panel>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextArea ID="txtContents" runat="server" Label="内容"  MaxLength="3000" Readonly="true">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow>

                                    <f:FormRow ID="ReOpinion" runat="server" CssStyle="padding-top:5px">
                                        <Items>
                                            <f:TextArea ID="txtReOpinion" runat="server" Readonly="true" Label="回复意见" MaxLength="3000">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow ID="HideReplyFile" runat="server" CssStyle="padding:5px">
                                        <Items>
                                            <f:Panel ID="Panel1" ShowHeader="false" ShowBorder="false" Layout="Column" runat="server">
                                                <Items>
                                                    <f:Label runat="server" Text="回复附件：" CssStyle="padding-left:24px" Width="100px" CssClass="marginr" ShowLabel="false"></f:Label>
                                                    <f:Button ID="ReplyFile" Text="回复附件" ToolTip="上传及查看" Icon="TableCell" runat="server" OnClick="ReplyFile_Click" >
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

                <f:FormRow ID="plApprove2">
                    <Items>

                        <f:ContentPanel Title="工作联系单审批列表" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Grid ID="gvApprove" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" runat="server" EnableCollapse="false"
                                DataKeyNames="CheckControlApproveId" EnableColumnLines="true" ForceFit="true">
                                <Columns>
                                    <f:RowNumberField Width="20px" />
                                    <f:TemplateField ColumnID="State" Width="250px" HeaderText="办理类型" HeaderTextAlign="Center" TextAlign="Center"
                                        EnableLock="true" Locked="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lbtype" runat="server" Text='<%# BLL.WorkContactService.ConvertState(Eval("ApproveType")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </f:TemplateField>
                                    <f:BoundField Width="180px" DataField="ApproveMan" HeaderTextAlign="Center" HeaderText="办理人员" TextAlign="Center" />
                                    <f:BoundField Width="200px" DataField="ApproveDate" HeaderTextAlign="Center" TextAlign="Center" DataFormatString="{0:yyyy-MM-dd}" HeaderText="办理时间" />
                                    <f:BoundField Width="180px" DataField="ApproveIdea" HeaderTextAlign="Center" TextAlign="Center"  HeaderText="办理意见" />

                                </Columns>
                            </f:Grid>
                        </f:ContentPanel>

                    </Items>

                </f:FormRow>

            </Rows>

        </f:Form>
        <f:Window ID="Window1" Title="工程联络单" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true"
            Width="700px" Height="500px">
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

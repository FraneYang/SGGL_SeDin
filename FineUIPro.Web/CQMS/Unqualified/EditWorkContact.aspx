<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditWorkContact.aspx.cs" Inherits="FineUIPro.Web.CQMS.Unqualified.EditWorkContact" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>工作联系单</title>
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
                                            <f:TextBox ID="txtCode" runat="server" Required="true" ShowRedStar="true" Label="编号" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>

                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:DropDownList ID="drpUnit" ShowRedStar="true" 
                                                AutoPostBack="true" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged"  EmptyText="--请选择--" AutoSelectFirstItem="false"
                                                runat="server" Required="true" Label="提出单位" LabelAlign="Right" EnableEdit="true">
                                            </f:DropDownList>
                                            <f:RadioButtonList ID="rblIsReply" Label="答复" runat="server" ShowRedStar="true" Required="true">
                                                <f:RadioItem Text="需要回复" Value="1" Selected="true" />
                                                <f:RadioItem Text="不需回复" Value="2" />
                                            </f:RadioButtonList>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:DropDownBox runat="server" Label="主送单位" ID="txtMainSendUnit" ShowRedStar="true" Required="true" EmptyText="--请选择--" EnableMultiSelect="true" MatchFieldWidth="true">
                                                <PopPanel>
                                                    <f:Grid ID="gvMainSendUnit" BoxFlex="1"
                                                        DataIDField="UnitId" DataTextField="UnitName" EnableMultiSelect="true" KeepCurrentSelection="true"
                                                        ShowBorder="true" ShowHeader="false" ForceFit="true"
                                                        runat="server" EnableCheckBoxSelect="true" DataKeyNames="UnitId" Hidden="true">
                                                        <Columns>
                                                            <%--<f:RowNumberField />--%>
                                                            <f:BoundField Width="100px" DataField="UnitId" SortField="UnitId" DataFormatString="{0}" Hidden="true" />
                                                            <f:BoundField Width="100px" DataField="UnitName" SortField="UnitName" DataFormatString="{0}"
                                                                HeaderText="名称" />
                                                        </Columns>
                                                    </f:Grid>
                                                </PopPanel>
                                            </f:DropDownBox>
                                            <f:DropDownBox runat="server" Label="抄送单位" ID="txtCCUnit" EmptyText="--请选择--" DataControlID="txtCCUnit" EnableMultiSelect="true" MatchFieldWidth="true">
                                                <PopPanel>
                                                    <f:Grid ID="gvCCUnit" BoxFlex="1"
                                                        DataIDField="UnitId" DataTextField="UnitName" EnableMultiSelect="true" KeepCurrentSelection="true"
                                                        ShowBorder="true" ShowHeader="false" ForceFit="true"
                                                        runat="server" EnableCheckBoxSelect="true" DataKeyNames="UnitId" Hidden="true">
                                                        <Columns>
                                                            <%--<f:RowNumberField />--%>
                                                            <f:BoundField Width="100px" DataField="UnitId" SortField="UnitId" DataFormatString="{0}" Hidden="true" />
                                                            <f:BoundField Width="100px" DataField="UnitName" SortField="UnitName" DataFormatString="{0}"
                                                                HeaderText="名称" />
                                                        </Columns>
                                                    </f:Grid>
                                                </PopPanel>
                                            </f:DropDownBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtCause" runat="server" Required="true" ShowRedStar="true" Label="事由" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                            <f:Panel ID="plfile" ShowHeader="false" ShowBorder="false" Layout="Column" runat="server">
                                                <Items>
                                                    <f:Panel ShowHeader="false" ShowBorder="false" Layout="Column" runat="server">
                                                        <Items>
                                                            <f:Label runat="server" Text="附件：" CssStyle="padding-left:53px" Width="100px" CssClass="marginr" ShowLabel="false"></f:Label>
                                                            <f:Button ID="imgBtnFile" Text="附件" ToolTip="上传及查看" OnClick="imgBtnFile_Click" Icon="TableCell" runat="server">
                                                            </f:Button>
                                                        </Items>
                                                    </f:Panel>
                                                </Items>
                                            </f:Panel>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextArea ID="txtContents" runat="server" Label="内容" MaxLength="3000" ShowRedStar="true" Required="true">
                                            </f:TextArea>

                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>

                <f:FormRow ID="next">
                    <Items>
                        <f:ContentPanel ID="ContentPanel5" Title="工作联系单审批流程设置" runat="server" ShowHeader="true" EnableCollapse="true"
                            BodyPadding="0px">
                            <f:Form ID="Form5" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                            <f:RadioButtonList runat="server" ID="rblIsAgree" Label="是否同意"
                                                OnSelectedIndexChanged="rblIsAgree_SelectedIndexChanged" ShowRedStar="true" AutoPostBack="true">
                                                <f:RadioItem Text="同意" Value="true" Selected="true" />
                                                <f:RadioItem Text="不同意" Value="false" />
                                            </f:RadioButtonList>
                                            <f:Label runat="server" CssStyle="display:none"></f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:DropDownList ID="drpHandleType"
                                                AutoPostBack="true" runat="server" Label="办理步骤" OnSelectedIndexChanged="drpHandleType_SelectedIndexChanged"
                                                LabelAlign="Right" EnableEdit="true">
                                            </f:DropDownList>
                                            <f:DropDownList ID="drpHandleMan" runat="server" Label="办理人员" Required="true" LabelAlign="Right" EnableEdit="true">
                                            </f:DropDownList>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow ID="HideOptions" runat="server" CssStyle="padding-top:2px">
                                        <Items>
                                            <f:TextArea ID="txtOpinions" runat="server" Label="我的意见" MaxLength="3000">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow ID="ReOpinion" runat="server" Hidden="false" CssStyle="padding-top:2px">
                                        <Items>
                                            <f:TextArea ID="txtReOpinion" runat="server" Label="回复意见" MaxLength="3000">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow ID="HideReplyFile" runat="server" CssStyle="padding-top:2px">
                                        <Items>
                                            <f:Panel ID="Panel2" ShowHeader="false" ShowBorder="false" Layout="Column" runat="server">
                                                <Items>
                                                    <f:Label runat="server" Text="回复附件：" CssStyle="padding-left:24px" Width="100px" CssClass="marginr" ShowLabel="false"></f:Label>
                                                    <f:Button ID="ReplyFile" Text="回复附件" OnClick="ReplyFile_Click" ToolTip="上传及查看" Icon="TableCell" runat="server">
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
                            <f:Grid ID="Grid1" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" runat="server" EnableCollapse="false"
                                DataKeyNames="CheckControlApproveId" EnableColumnLines="true" ForceFit="true">
                                <Columns>
                                    <f:RowNumberField Width="20px" />
                                    <f:TemplateField ColumnID="State" Width="250px" HeaderText="办理类型" HeaderTextAlign="Center" TextAlign="Center"
                                        EnableLock="true" Locked="False">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# BLL.WorkContactService.ConvertState(Eval("ApproveType")) %>'></asp:Label>
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
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server">
                        </f:ToolbarFill>
                        <f:HiddenField ID="HFWorkContactId" runat="server"></f:HiddenField>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" OnClick="btnSubmit_Click" ValidateForms="SimpleForm1">
                        </f:Button>
                        <%--  <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" OnClick="btnClose_Click" runat="server" Icon="SystemClose">
                        </f:Button>--%>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
</body>
</html>

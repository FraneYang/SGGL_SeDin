<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConstructionPlanEdit.aspx.cs" Inherits="FineUIPro.Web.PZHGL.InformationProject.ConstructionPlanEdit" ValidateRequest="false" %>

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
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel2" Title="总承包商施工计划" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>

                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtCode" runat="server" Required="true" ShowRedStar="true" Label="文件编号" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                            <f:DatePicker ID="txtCompileDate" runat="server" Label="编制日期" LabelAlign="Right"
                                                EnableEdit="true">
                                            </f:DatePicker>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:HtmlEditor runat="server" Label="文件内容" ID="txtContent" ShowLabel="false"
                                              Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="300" LabelAlign="Right">
                                            </f:HtmlEditor>
                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>

                <f:FormRow ID="next">
                    <Items>
                        <f:ContentPanel ID="ContentPanel5" Title="总承包商施工计划审批流程设置" runat="server" ShowHeader="true" EnableCollapse="true"
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
                                                Readonly="true" runat="server" Label="办理步骤"
                                                LabelAlign="Right" >
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
                                </Rows>
                            </f:Form>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow ID="plApprove2">
                    <Items>
                        <f:ContentPanel Title="总承包商施工计划审批列表" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Grid ID="Grid1" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" runat="server" EnableCollapse="false"
                                DataKeyNames="ConstructionPlanApproveId" EnableColumnLines="true" ForceFit="true">
                                <Columns>
                                    <f:RowNumberField Width="20px" />
                                    <f:TemplateField ColumnID="State" Width="250px" HeaderText="办理类型" HeaderTextAlign="Center" TextAlign="Center"
                                        EnableLock="true" Locked="False">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# BLL.ConstructionPlanService.ConvertState(Eval("ApproveType")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </f:TemplateField>
                                    <f:BoundField Width="180px" DataField="ApproveMan" HeaderTextAlign="Center" HeaderText="办理人员" TextAlign="Center" />
                                    <f:BoundField Width="200px" DataField="ApproveDate" HeaderTextAlign="Center" TextAlign="Center" DataFormatString="{0:yyyy-MM-dd}" HeaderText="办理时间" />
                                    <f:BoundField Width="180px" DataField="ApproveIdea" HeaderTextAlign="Center" TextAlign="Center" HeaderText="办理意见" />

                                </Columns>
                            </f:Grid>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:Button ID="imgBtnFile" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="imgBtnFile_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                    </f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server">
                        </f:ToolbarFill>
                        <f:HiddenField ID="HFConstructionPlanId" runat="server"></f:HiddenField>
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
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
</body>
</html>

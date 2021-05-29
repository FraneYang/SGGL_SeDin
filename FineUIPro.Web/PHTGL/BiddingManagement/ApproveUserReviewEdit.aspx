<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproveUserReviewEdit.aspx.cs" Inherits="FineUIPro.Web.PHTGL.BiddingManagement.ApproveUserReviewEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="评标小组名单审批" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label1" runat="server" Text="评标小组名单审批" CssClass="widthBlod"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                     <f:DropDownList ID="DrpProjectName" runat="server" Label="项目名称" LabelAlign="Right" AutoPostBack="true"  LabelWidth="120px"></f:DropDownList>
                     </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="drpBidDocumentCode" runat="server" Label="招标文件编号" LabelAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="drpProjectId_SelectedIndexChanged" LabelWidth="120px"></f:DropDownList>
                        <f:TextBox ID="txtBidProject" runat="server" Label="招标项目" LabelAlign="Right" LabelWidth="140px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label7" runat="server" Text="一、	评标小组成员名单"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Grid ID="Grid1" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" Title="" EnableCollapse="false"
                            runat="server" DataKeyNames="ID" AllowCellEditing="true" ClicksToEdit="1" ForceFit="true"
                            EnableColumnLines="true" DataIDField="ID" >
                            <Toolbars>
                                <f:Toolbar ID="Toolbar2" runat="server" Position="Top" ToolbarAlign="Left">
                                    <Items>
                                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                        <f:Button ID="btnNew" ToolTip="增加" Icon="Add" EnablePostBack="false" runat="server">
                                        </f:Button>
                                        <f:Button ID="btnDelete" ToolTip="删除" Icon="Delete" EnablePostBack="false" runat="server">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:RowNumberField HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center" Width="60px"></f:RowNumberField>
                                <f:RenderField ColumnID="ApproveUserName" DataField="ApproveUserName" FieldType="String"  RendererFunction="render_user"
                                    HeaderText="姓名" HeaderTextAlign="Center">
                                     <Editor>
                                        <f:DropDownList ID="DropUser" runat="server" Required="true"  EnableEdit="true" ></f:DropDownList>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField ColumnID="ApproveUserSpecial" DataField="ApproveUserSpecial" FieldType="String"
                                    HeaderText="专业" HeaderTextAlign="Center">
                                    <Editor>
                                        <f:TextBox ID="txtApproveUserSpecial" runat="server" Required="true"></f:TextBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField ColumnID="ApproveUserUnit" DataField="ApproveUserUnit" FieldType="String"
                                    HeaderText="单位/部室" HeaderTextAlign="Center">
                                    <Editor>
                                        <f:TextBox ID="txtApproveUserUnit" runat="server" Required="true"></f:TextBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField ColumnID="Remarks" DataField="Remarks" FieldType="String"
                                    HeaderText="说明" HeaderTextAlign="Center">
                                    <Editor>
                                        <f:TextBox ID="txtRemarks" runat="server" Required="true">
                                        </f:TextBox>
                                    </Editor>
                                </f:RenderField>
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:FormRow>

                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="DropConstructionManager" runat="server" AutoPostBack="true" EnableEdit="true" Label="施工经理" LabelAlign="Right" LabelWidth="120px"></f:DropDownList>

                        <%-- <f:TextBox ID="txtConstructionManager" runat="server" Label="施工经理" LabelAlign="Right" MaxLength="200" LabelWidth="120px" Readonly="true"></f:TextBox>--%>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="DropProjectManager" runat="server" AutoPostBack="true" EnableEdit="true" Label="项目经理" LabelAlign="Right" LabelWidth="120px"></f:DropDownList>

                        <%--<f:TextBox ID="txtProjectManager" runat="server" Label="项目经理" LabelAlign="Right" MaxLength="200" LabelWidth="120px"  Readonly="true"></f:TextBox>--%>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="DropApproval_Construction" runat="server" AutoPostBack="true" EnableEdit="true" Label="施工管理部" LabelAlign="Right" LabelWidth="120px"></f:DropDownList>

                        <%--   <f:TextBox ID="txtControlManager" runat="server" Label="控制经理" LabelAlign="Right" MaxLength="200" LabelWidth="120px" Readonly="true"></f:TextBox>--%>
                    </Items>
                </f:FormRow>

                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="DropDeputyGeneralManager" runat="server" AutoPostBack="true" EnableEdit="true" Label="分管副总经理" LabelAlign="Right" LabelWidth="120px"></f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="label12" runat="server" Text="注：“说明”栏填写内容为“技术或技术组小组长”或“商务或商务组小组长”或“评标组长”。"></f:Label>
                    </Items>
                </f:FormRow>

            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill runat="server">
                        </f:ToolbarFill>

                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" Text="保存" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnSubmit" Icon="SystemSave" runat="server" ToolTip="提交" Text="提交" ValidateForms="SimpleForm1"
                            OnClick="btnSubmit_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" Text="关闭" runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
    <script>
        //function onGridDataLoad(event) {
        //    this.mergeColumns(['txtPlanningContent']);
        //}
        var DropProgress_userID= '<%= DropUser.ClientID %>';
          function render_user(value) {
             return F(DropProgress_userID).getTextByValue(value);
        }
    </script>
</body>
</html>

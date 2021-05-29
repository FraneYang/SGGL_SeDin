<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetSubReviewEdit.aspx.cs" Inherits="FineUIPro.Web.PHTGL.BiddingManagement.SetSubReviewEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="false" Title="审批信息"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel1" Title=" " ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <Items>
                                <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="false" Title=""
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Rows>
                                        <f:FormRow>
                                            <Items>
                                                <f:TextBox ID="txtSetSubReviewCode" runat="server" Label="编号" ShowRedStar="true" LabelAlign="Right" LabelWidth="140px"></f:TextBox>
                                                <f:DropDownList ID="DropBidCode" runat="server" Label="招标编号" LabelAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="drpProjectId_SelectedIndexChanged" LabelWidth="120px"></f:DropDownList>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:TextBox ID="txtProjectName" Readonly="true" runat="server" Label="工程名称" LabelAlign="Right" MaxLength="30" LabelWidth="140px" Required="true" ShowRedStar="true"></f:TextBox>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:TextBox ID="txtBidContent" runat="server" Readonly="true" Label="招标内容" LabelAlign="Right" MaxLength="30" LabelWidth="140px" Required="true" ShowRedStar="true"></f:TextBox>
                                                <f:DatePicker runat="server" Required="true" DateFormatString="yyyy-MM-dd" Label="开标日期" EmptyText="请选择开始日期"
                                                    ID="StartTime" ShowRedStar="true" Readonly="true">
                                                </f:DatePicker>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:Grid ID="Grid1" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" Title="" EnableCollapse="false"
                                                    runat="server" DataKeyNames="ID" AllowCellEditing="true" ClicksToEdit="1" ForceFit="true"
                                                    EnableColumnLines="true" DataIDField="ID">
                                                    <Toolbars>
                                                        <f:Toolbar ID="Toolbar2" runat="server" Position="Top" ToolbarAlign="Left">
                                                            <Items>
                                                                <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>
                                                                <f:Button ID="btnNew" ToolTip="增加" Icon="Add" EnablePostBack="false" runat="server">
                                                                </f:Button>
                                                                <f:Button ID="btnDelete" ToolTip="删除" Icon="Delete" EnablePostBack="false" runat="server">
                                                                </f:Button>
                                                            </Items>
                                                        </f:Toolbar>
                                                    </Toolbars>
                                                    <Columns>
                                                        <f:RowNumberField />

                                                        <f:RenderField Width="100px" ColumnID="Company" DataField="Company" ExpandUnusedSpace="true"
                                                            HeaderText="投标单位" HeaderTextAlign="Center">
                                                            <Editor>
                                                                <f:TextBox ID="TextBox2" runat="server">
                                                                </f:TextBox>
                                                            </Editor>
                                                        </f:RenderField>
                                                        <f:GroupField HeaderText="评标委员会评审结果" TextAlign="Center">
                                                            <Columns>
                                                                <f:RenderField Width="100px" ColumnID="Price_ReviewResults" DataField="Price_ReviewResults"
                                                                    HeaderText="报价评审结果" HeaderTextAlign="Center">
                                                                    <Editor>
                                                                        <f:TextBox ID="TextBox3" runat="server">
                                                                        </f:TextBox>
                                                                    </Editor>
                                                                </f:RenderField>
                                                                <f:RenderField Width="100px" ColumnID="Skill_ReviewResults" DataField="Skill_ReviewResults" 
                                                                    HeaderText="技术评审结果" HeaderTextAlign="Center">
                                                                    <Editor>
                                                                        <f:TextBox ID="TextBox4" runat="server">
                                                                        </f:TextBox>
                                                                    </Editor>
                                                                </f:RenderField>
                                                                <f:RenderField Width="100px" ColumnID="Business_ReviewResults" DataField="Business_ReviewResults"
                                                                    HeaderText="商务评审结果" HeaderTextAlign="Center">
                                                                    <Editor>
                                                                        <f:TextBox ID="TextBox5" runat="server">
                                                                        </f:TextBox>
                                                                    </Editor>
                                                                </f:RenderField>
                                                                <f:RenderField Width="100px" ColumnID="Synthesize_ReviewResults" DataField="Synthesize_ReviewResults" 
                                                                    HeaderText="综合评审结果" HeaderTextAlign="Center">
                                                                    <Editor>
                                                                        <f:TextBox ID="TextBox6" runat="server">
                                                                        </f:TextBox>
                                                                    </Editor>
                                                                </f:RenderField>

                                                            </Columns>
                                                        </f:GroupField>
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
                                                <f:DropDownList ID="DropApproval_Construction" runat="server" AutoPostBack="true" EnableEdit="true" Label="施工管理部" LabelAlign="Right" LabelWidth="120px"></f:DropDownList>

                                                <%--   <f:TextBox ID="txtControlManager" runat="server" Label="控制经理" LabelAlign="Right" MaxLength="200" LabelWidth="120px" Readonly="true"></f:TextBox>--%>
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
                                                <f:DropDownList ID="DropDeputyGeneralManager" runat="server" AutoPostBack="true" EnableEdit="true" Label="分管副总经理" LabelAlign="Right" LabelWidth="120px"></f:DropDownList>
                                            </Items>
                                        </f:FormRow>

                                    </Rows>

                                </f:Form>
                            </Items>

                        </f:ContentPanel>
                    </Items>
                </f:FormRow>

            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:Label runat="server" ID="lbTemp">
                        </f:Label>
                        <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server" Hidden="true"
                            OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                        </f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                            OnClick="btnSubmit_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
        <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConstructSolutionView.aspx.cs" Inherits="FineUIPro.Web.CQMS.Solution.ConstructSolutionView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>施工方案</title>
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
                        <f:ContentPanel ID="ContentPanel2" Title="施工方案" ShowBorder="true"
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
                                            <f:TextBox ID="txtCode" runat="server" Required="true" Readonly="true" ShowRedStar="true" Label="编号" LabelAlign="Right"
                                                MaxLength="50" LabelWidth="120px">
                                            </f:TextBox>

                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="drpUnit" runat="server" Readonly="true" Label="施工单位" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                            <f:TextBox ID="txtSolutionName" runat="server" ShowRedStar="true" Readonly="true" Required="true" Label="方案名称" LabelAlign="Right"
                                                MaxLength="50" LabelWidth="120px">
                                            </f:TextBox>
                                            
                                            <%-- <f:DropDownList ID="drpUnit" ShowRedStar="true"
                                                runat="server" Required="true" Label="施工单位" LabelAlign="Right" EnableEdit="true">
                                            </f:DropDownList>

                                            <f:DropDownList ID="drpModelType" ShowRedStar="true" Required="true" runat="server" Width="350px" Label="方案类别" LabelAlign="Right" EnableEdit="true">
                                            </f:DropDownList>--%>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="drpModelType" runat="server" Readonly="true" Label="方案类别" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                            <f:TextBox ID="txtSpecialType" runat="server" Readonly="true" Label="分部分项工程" LabelAlign="Right"
                                                MaxLength="50" LabelWidth="120px">
                                            </f:TextBox>
                                            
                                            <%--  <f:DatePicker ID="txtCompileDate" ShowRedStar="true" Required="true" runat="server" Label="编制日期" LabelAlign="Right"
                                                EnableEdit="true">
                                            </f:DatePicker>--%>
                                          
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtCompileDate" runat="server" Readonly="true" Label="编制时间" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                            <f:NumberBox ID="txtEdition" Label="版次" runat="server" NoDecimal="true" Readonly="true"
                                                LabelWidth="120px" NoNegative="true" ShowRedStar="true" MaxLength="3" Required="true">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>

                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtCNProfessional" runat="server" Readonly="true" Label="专业" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                            <%--  <f:DropDownBox runat="server" Label="专业" ShowRedStar="true" Required="true" ID="txtCNProfessional" EmptyText="--请选择--" DataControlID="txtCNProfessional" EnableMultiSelect="true" MatchFieldWidth="true">
                                                <PopPanel>
                                                    <f:Grid ID="gvCNPro" BoxFlex="1"
                                                        DataIDField="CNProfessionalCode" DataTextField="ProfessionalName" EnableMultiSelect="true" KeepCurrentSelection="true"
                                                        ShowBorder="true" ShowHeader="false"
                                                        runat="server" EnableCheckBoxSelect="true" DataKeyNames="CNProfessionalCode" Hidden="true">
                                                        <Columns>
                                                            <f:RowNumberField />
                                                            <f:BoundField Width="100px" DataField="CNProfessionalCode" SortField="CNProfessionalCode" DataFormatString="{0}" Hidden="true" />
                                                            <f:BoundField Width="100px" DataField="ProfessionalName" SortField="ProfessionalName" DataFormatString="{0}"
                                                                ExpandUnusedSpace="true" HeaderText="名称" />
                                                        </Columns>
                                                    </f:Grid>
                                                </PopPanel>
                                            </f:DropDownBox>--%>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtUnitWork" runat="server" Readonly="true" Label="单位工程" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                            <%--  <f:DropDownBox runat="server" Label="单位工程" ShowRedStar="true"
                                                Required="true" ID="txtUnitWork" EmptyText="--请选择--" EnableMultiSelect="true" MatchFieldWidth="false">
                                                <PopPanel>
                                                    <f:Grid ID="gvUnitWork" DataIDField="UnitWorkId" DataTextField="UnitWorkName" DataKeyNames="UnitWorkId"
                                                        EnableMultiSelect="true" KeepCurrentSelection="true" Height="300px" Hidden="true"
                                                        ShowBorder="true" ShowHeader="false"
                                                        runat="server" EnableCheckBoxSelect="true">
                                                        <Columns>
                                                            <f:BoundField Width="100px" DataField="UnitWorkId" SortField="UnitWorkId" DataFormatString="{0}" Hidden="true" />
                                                            <f:BoundField Width="100px" DataField="UnitWorkName" SortField="UnitWorkName" DataFormatString="{0}"
                                                                ExpandUnusedSpace="true" HeaderText="工程名称" />
                                                        </Columns>
                                                    </f:Grid>
                                                </PopPanel>
                                            </f:DropDownBox>--%>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                              <f:Panel ID="plfile" ShowHeader="false" ShowBorder="false" Layout="Column" runat="server">
                                                <Items>

                                                    <f:Panel ShowHeader="false" ShowBorder="false" Layout="Column" runat="server">
                                                        <Items>
                                                            <f:Label runat="server" Text="附件：" ShowRedStar="true" CssStyle="padding-left:53px" Width="110px" CssClass="marginr" ShowLabel="false"></f:Label>
                                                            <f:Button ID="imgBtnFile" Text="附件" ToolTip="上传及查看" Icon="TableCell" OnClick="imgBtnFile_Click" runat="server">
                                                            </f:Button>
                                                        </Items>
                                                    </f:Panel>
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
                        <f:Panel ID="Panel2" IsFluid="true" CssClass="mytable blockpanel" runat="server" ShowBorder="true"
                            Layout="Table" TableConfigColumns="3" ShowHeader="true" Title="总包会签">
                            <Items>
                                <f:Panel ID="Panel1" Title="Panel1" Width="200px" Height="200px"
                                    TableRowspan="5" runat="server" BodyPadding="10px" AutoScroll="true" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                        <f:Tree runat="server" ID="trOne" ShowHeader="false" ShowBorder="false" EnableArrows="true">
                                        </f:Tree>
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel3" Title="Panel1" Width="200px" AutoScroll="true" Layout="Fit" Height="200px"
                                    TableRowspan="5" runat="server" BodyPadding="10px" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                        <f:Tree runat="server" ID="trTwo" ShowHeader="false" ShowBorder="false">
                                            <Nodes>
                                            </Nodes>
                                        </f:Tree>
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel4" Title="Panel1" Width="200px" AutoScroll="true" Layout="Fit" Height="200px"
                                    TableRowspan="5" runat="server" BodyPadding="10px" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                        <f:Tree runat="server" ID="trThree" ShowHeader="false" ShowBorder="false">
                                            <Nodes>
                                            </Nodes>
                                        </f:Tree>
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel5" Title="Panel1" Width="200px" AutoScroll="true" Layout="Fit" Height="200px"
                                    TableRowspan="5" runat="server" BodyPadding="10px" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                        <f:Tree runat="server" ID="trFour" ShowHeader="false" ShowBorder="false">
                                            <Nodes>
                                            </Nodes>
                                        </f:Tree>
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel6" Title="Panel1" Width="200px" AutoScroll="true" Layout="Fit" Height="200px"
                                    TableRowspan="5" runat="server" BodyPadding="10px" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                        <f:Tree runat="server" ID="trFive" ShowHeader="false" ShowBorder="false">
                                            <Nodes>
                                            </Nodes>
                                        </f:Tree>
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel7" Title="Panel1" Width="200px" AutoScroll="true" Layout="Fit" Height="200px"
                                    TableRowspan="5" runat="server" BodyPadding="10px" ShowBorder="false" ShowHeader="false">
                                    <Items>
                                        <f:Tree runat="server" ID="trSixe" ShowHeader="false" ShowBorder="false">
                                            <Nodes>
                                            </Nodes>
                                        </f:Tree>
                                    </Items>
                                </f:Panel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:FormRow>
                <f:FormRow ID="plApprove2">
                    <Items>

                        <f:ContentPanel Title="审批列表" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Grid ID="gvApprove" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" runat="server" EnableCollapse="false"
                                DataKeyNames="ConstructSolutionApproveId" EnableColumnLines="true" OnRowCommand="gvApprove_RowCommand" ForceFit="true">
                                <Columns>
                                    <f:RowNumberField Width="40px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center" />
                                    <f:TemplateField ColumnID="ApproveType" Width="150px" HeaderText="办理类型" HeaderTextAlign="Center" TextAlign="Center"
                                        EnableLock="true" Locked="False">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# BLL.CQMSConstructSolutionService.ConvertState(Eval("ApproveType")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </f:TemplateField>
                                    <f:TemplateField ColumnID="ApproveMan" Width="150px" HeaderText="办理人员" HeaderTextAlign="Center" TextAlign="Center"
                                        EnableLock="true" Locked="False">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# man(Eval("ApproveMan")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </f:TemplateField>
                                    <f:TemplateField ColumnID="IsAgree" Width="100px" HeaderText="是否同意" HeaderTextAlign="Center" TextAlign="Center"
                                        EnableLock="true" Locked="False">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# BLL.CQMSConstructSolutionService.IsAgree(Eval("ApproveType"),Eval("IsAgree")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </f:TemplateField>
                                    <f:BoundField Width="100px" DataField="ApproveDate" HeaderTextAlign="Center" TextAlign="Center" DataFormatString="{0:yyyy-MM-dd}" HeaderText="办理时间" />
                                    <f:BoundField Width="180px" DataField="ApproveIdea" HeaderTextAlign="Center" TextAlign="Center" HeaderText="办理意见" />
                                    <f:LinkButtonField HeaderText="附件" ColumnID="download" Width="60px" Icon="ArrowDown" CommandName="download" />
                                </Columns>
                            </f:Grid>
                        </f:ContentPanel>

                    </Items>

                </f:FormRow>



            </Rows>

        </f:Form>
    </form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
</body>
</html>

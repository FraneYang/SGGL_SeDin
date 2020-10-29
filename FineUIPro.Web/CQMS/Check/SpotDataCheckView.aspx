<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpotDataCheckView.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.SpotDataCheckView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>工序验收</title>
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

            .Yellow  {
            background-color: #FFFF93;
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
                        <f:ContentPanel ID="ContentPanel2" Title="工序验收记录" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtProjectName" runat="server" Readonly="true" LabelWidth="110px" Label="项目名称" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                            <f:TextBox ID="txtDocCode" runat="server" Required="true" LabelWidth="110px" ShowRedStar="true" Readonly="true" Label="编号" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                            <f:HiddenField ID="hdSpotCheckCode" runat="server"></f:HiddenField>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtUnit" ShowRedStar="true" runat="server" LabelWidth="110px" Readonly="true" Label="施工单位" LabelAlign="Right"
                                               >
                                            </f:TextBox>
                                            <f:TextBox ID="txtCNProfessional" ShowRedStar="true" LabelWidth="110px" runat="server" Readonly="true" Label="专业" LabelAlign="Right"
                                               >
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                      <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtControlPointType" runat="server" LabelWidth="110px" Readonly="true" Required="true" ShowRedStar="true" Label="控制点级别" LabelAlign="Right"
                                                MaxLength="100">
                                            </f:TextBox>
                                            <f:Label runat="server" ID="lb1"></f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:Label runat="server" Label="参与共检人" LabelWidth="110px"></f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtJointCheckMans" runat="server" LabelWidth="110px" Readonly="true" Label="总承包商" LabelAlign="Right"
                                               >
                                            </f:TextBox>
                                            <f:TextBox ID="txtJointCheckMans2" runat="server" Readonly="true" Label="监理单位" LabelAlign="Right"
                                               >
                                            </f:TextBox>
                                            <f:TextBox ID="txtJointCheckMans3" runat="server" Readonly="true" Label="建设单位" LabelAlign="Right"
                                                >
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtCheckDateType" runat="server" LabelWidth="110px" Readonly="true" Required="true" ShowRedStar="true" Label="共检时间方式" LabelAlign="Right"
                                                MaxLength="100">
                                            </f:TextBox>
                                            <f:Label runat="server" ></f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtSpotCheckDate" runat="server" LabelWidth="110px" Readonly="true" Required="true" ShowRedStar="true" Label="共检时间" LabelAlign="Right"
                                                MaxLength="100">
                                            </f:TextBox>
                                            <f:TextBox ID="txtSpotCheckDate2" Hidden="true" runat="server" LabelWidth="110px" Readonly="true" Required="true" ShowRedStar="true" Label="结束时间" LabelAlign="Right"
                                                MaxLength="100">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtCheckArea" runat="server" LabelWidth="110px" Readonly="true" Required="true" ShowRedStar="true" Label="共检地点" LabelAlign="Right"
                                                MaxLength="100">
                                            </f:TextBox>
                                            <%--<f:Button ID="imgBtnFile" Text="自检记录" ToolTip="上传及查看" Icon="TableCell" runat="server"
                                                        OnClick="imgBtnFile_Click">
                                                    </f:Button>--%>
                                            <f:Panel ID="Panel1" ShowHeader="false" ShowBorder="false" Layout="Column" runat="server">
                                                <Items>
                                                    <f:Label runat="server" Text="自检记录：" CssStyle="padding-left:35px" Width="110px" CssClass="marginr" ShowLabel="false"></f:Label>
                                                    <f:Button ID="imgBtnFile" Text="自检记录" ToolTip="上传及查看" Icon="TableCell" runat="server"
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
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="共检内容列表" EnableCollapse="false" runat="server"
                            BoxFlex="1" DataKeyNames="SpotCheckDetailId,ControlItemAndCycleId" AllowCellEditing="true" EnableColumnLines="true"
                            ClicksToEdit="1" DataIDField="SpotCheckDetailId" AllowSorting="true" ForceFit="true" SortField="CreateDate"
                            EnableTextSelection="True" OnRowCommand="Grid1_RowCommand">
                            <Columns>
                                <f:RowNumberField Width="40px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center" />
                                <f:TemplateField ColumnID="ControlItemAndCycleId" Width="400px" HeaderText="共检项目名称" HeaderTextAlign="Center" TextAlign="Center"
                                    >
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# ConvertDetailName(Eval("ControlItemAndCycleId")) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:TemplateField ColumnID="ControlPoint" Width="100px" HeaderText="控制点级别" HeaderTextAlign="Center" TextAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# ConvertControlPoint(Eval("ControlItemAndCycleId")) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                
                              <f:TemplateField ColumnID="attchUrl" Width="150px" HeaderText="交工资料" HeaderTextAlign="Center" TextAlign="Center">
                                    <ItemTemplate>  
                                 <asp:LinkButton runat="server" ID="attchUrl" OnClick="attchUrl_Click" CommandArgument='<%# Eval("SpotCheckDetailId") %>'  Text='<%# BLL.ControlItemAndCycleService.ConvertContronInfo(Eval("ControlItemAndCycleId")) %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:TemplateField ColumnID="IsOKStr" Width="100px" HeaderText="资料情况" HeaderTextAlign="Center" TextAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# ConvertIsOK(Eval("IsDataOK")) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                 <f:TemplateField ColumnID="State" Width="250px" HeaderText="状态" HeaderTextAlign="Center" TextAlign="Center"
                                        EnableLock="true" Locked="False">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# ConvertState(Eval("state")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </f:TemplateField>

                             
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:FormRow>
                <f:FormRow ID="plApprove2">
                    <Items>

                        <f:ContentPanel Title="资料验收审批列表" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Grid ID="gvApprove" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" runat="server" EnableCollapse="false"
                                DataKeyNames="CheckControlApproveId" EnableColumnLines="true" ForceFit="true">
                                <Columns>
                                    <f:RowNumberField Width="40px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center" />
                                    <f:TemplateField ColumnID="State" Width="250px" HeaderText="办理类型" HeaderTextAlign="Center" TextAlign="Center"
                                        EnableLock="true" Locked="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lbtype" runat="server" Text='<%# ConvertState(Eval("ApproveType")) %>'></asp:Label>
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
        <f:Window ID="Window1" Title="编辑检查项明细" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true"
            Width="1100px" Height="520px">
        </f:Window>
        <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" OnClose="WindowAtt_Close" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditSportDataCheck.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.EditSportDataCheck" %>

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

        .Yellow {
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
                                            <f:TextBox ID="txtDocCode" runat="server" Required="true" ShowRedStar="true" Label="编号" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:DropDownList ID="drpUnit" ShowRedStar="true" runat="server" LabelWidth="110px" Required="true" EmptyText="--请选择--" AutoSelectFirstItem="false" Label="施工单位" LabelAlign="Right" EnableEdit="true">
                                            </f:DropDownList>
                                            <f:DropDownList ID="drpCNProfessional" ShowRedStar="true" runat="server" EmptyText="--请选择--" AutoSelectFirstItem="false" Label="专业" Required="true" LabelAlign="Right" EnableEdit="true">
                                            </f:DropDownList>
                                        </Items>
                                    </f:FormRow>
                                      <f:FormRow>
                                        <Items>
                                            <f:DropDownList ID="drpControlPointType" ShowRedStar="true" Enabled="true" runat="server" LabelWidth="110px" Required="true" Label="控制点级别" LabelAlign="Right" EnableEdit="true">
                                                <f:ListItem Text="非C级" Value="D" />
                                                <f:ListItem Text="C级" Value="C" />
                                            </f:DropDownList>
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
                                            <f:DropDownList ID="drpJointCheckMans" runat="server" Label="总承包商" EmptyText="--请选择--" AutoSelectFirstItem="false" EnableMultiSelect="true" LabelWidth="110px" LabelAlign="Right" EnableEdit="true">
                                            </f:DropDownList>
                                            <f:DropDownList ID="drpJointCheckMans2" runat="server" Label="监理单位" EmptyText="--请选择--" AutoSelectFirstItem="false" EnableMultiSelect="true" LabelAlign="Right" EnableEdit="true">
                                            </f:DropDownList>
                                            <f:DropDownList ID="drpJointCheckMans3" runat="server" Label="建设单位" EmptyText="--请选择--" AutoSelectFirstItem="false" EnableMultiSelect="true" LabelAlign="Right" EnableEdit="true">
                                            </f:DropDownList>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:RadioButtonList runat="server" ID="rblCheckDateType" LabelWidth="110px" LabelAlign="Right" Label="共检时间方式" AutoPostBack="true" OnSelectedIndexChanged="rblCheckDateType_SelectedIndexChanged">
                                                <f:RadioItem Value="1" Text="点时间" Selected="true" />
                                                <f:RadioItem Value="2" Text="段时间" />
                                            </f:RadioButtonList>
                                            <f:Label runat="server"></f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:DatePicker ID="txtSpotCheckDate" ShowRedStar="true" LabelWidth="110px" DateFormatString="yyyy-MM-dd HH:mm" runat="server" Label="共检时间" Required="true" LabelAlign="Right"
                                                EnableEdit="true" ShowTime="true" ShowSecond="false">
                                            </f:DatePicker>
                                            <f:DatePicker ID="txtSpotCheckDate2" ShowRedStar="true" Hidden="true" DateFormatString="yyyy-MM-dd HH:mm" runat="server" Label="结束时间" Required="true" LabelAlign="Right"
                                                EnableEdit="true" ShowTime="true" ShowSecond="false">
                                            </f:DatePicker>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtCheckArea" runat="server" LabelWidth="110px" Required="true" ShowRedStar="true" Label="共检地点" LabelAlign="Right"
                                                MaxLength="100">
                                            </f:TextBox>

                                            <%--<f:Button ID="imgBtnFile" Text="自检记录" ToolTip="上传及查看" Icon="TableCell" runat="server"
                                                        OnClick="imgBtnFile_Click">
                                                    </f:Button>--%>
                                            <f:Panel ID="Panel1" ShowHeader="false" ShowBorder="false" Layout="Column" runat="server">
                                                <Items>
                                                    <f:Label runat="server" Text="自检记录：" CssStyle="padding-left:25px" Width="100px" CssClass="marginr" ShowLabel="false"></f:Label>
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
                            ClicksToEdit="1"   DataIDField="SpotCheckDetailId" OnRowCommand="Grid1_RowCommand" AllowSorting="true" ForceFit="true" SortField="CreateDate"
                            EnableTextSelection="True" >
                            <Columns>
                                <f:RowNumberField Width="40px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center" />
                                <f:TemplateField ColumnID="ControlItemAndCycleId" Width="400px" HeaderText="共检项目名称" HeaderTextAlign="Center" TextAlign="Center"
                                    >
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# ConvertDetailName(Eval("ControlItemAndCycleId")) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:TemplateField ColumnID="ControlPoint" Width="80px" HeaderText="控制点级别" HeaderTextAlign="Center" TextAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# ConvertControlPoint(Eval("ControlItemAndCycleId")) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:TemplateField ColumnID="IsOKStr" Width="80px" HeaderText="共检结果" HeaderTextAlign="Center" TextAlign="Center">
                                    <ItemTemplate>
                                           <asp:Label  ID="lblattchUrl" Visible="false"  runat="server"></asp:Label>
                                        <asp:Label ID="Label2" runat="server" Text="合格"></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                              
                                <f:TemplateField ColumnID="attchUrl" Width="150px" HeaderText="交工资料" HeaderTextAlign="Center" TextAlign="Center">
                                    <ItemTemplate>  
                                 <asp:LinkButton runat="server" ID="attchUrl" OnClick="attchUrl_Click" CommandArgument='<%# Eval("SpotCheckDetailId") %>'  Text='<%# BLL.ControlItemAndCycleService.ConvertContronInfo(Eval("ControlItemAndCycleId")) %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </f:TemplateField>

       
                                <f:TemplateField ColumnID="IsDataOKStrSet" Width="150px" HeaderText="资料情况确认" HeaderTextAlign="Center" TextAlign="Center">
                                    <ItemTemplate>
                                        <%--<asp:HiddenField ID="lblattchUrl" runat="server" />--%>
                                    
                                        <asp:Button ID="btnDataOK" Text="合格" UseSubmitBehavior="false" OnClick="btnDataOK_Click" runat="server" CommandArgument='<%# Bind("SpotCheckDetailId") %>' />
                                        <asp:Button ID="btnNotDataOK" Text="不合格" UseSubmitBehavior="false" OnClick="btnNotDataOK_Click" runat="server" CommandArgument='<%# Bind("SpotCheckDetailId") %>' />
                                    </ItemTemplate>
                                </f:TemplateField>

                                <f:TemplateField ColumnID="IsDataOKStr" Width="100px" HeaderText="资料情况" HeaderTextAlign="Center" TextAlign="Center">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="IsDataOk" runat="server" Value='<%# Bind("IsDataOK") %>' />
                                        <asp:Label ID="lblDataOk" runat="server" Text='<%# ConvertIsOK(Eval("IsDataOK")) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>


                                <f:RenderField HeaderText="是否资料合格" ColumnID="IsDataOK" DataField="IsDataOK"
                                    SortField="IsDataOK" FieldType="String" HeaderTextAlign="Center" TextAlign="Center"
                                    Hidden="true">
                                </f:RenderField>
                                <f:RenderField HeaderText="资料确认时间" ColumnID="DataConfirmDate" DataField="DataConfirmDate"
                                    SortField="DataConfirmDate" FieldType="String" HeaderTextAlign="Center" TextAlign="Center"
                                    Hidden="true">
                                </f:RenderField>
                                <f:GroupField HeaderText="办理流程" TextAlign="Center">
                                    <Columns>
                                        <f:TemplateField ColumnID="HandleType" Width="130px" HeaderText="办理步骤" HeaderTextAlign="Center" TextAlign="Center"
                                            EnableLock="true" Locked="False">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdState" runat="server" Value='<%# Bind("state") %>' />
                                                <asp:DropDownList ID="drpHandleType" runat="server" Height="22" Width="90%" OnSelectedIndexChanged="drpHandleType_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblhandtype" runat="server" Visible="false" Text="要显示的操作名称"></asp:Label>
                                            </ItemTemplate>
                                        </f:TemplateField>

                                        <f:TemplateField ColumnID="HandleMan" Width="100px" HeaderText="办理人" HeaderTextAlign="Center" TextAlign="Center"
                                            EnableLock="true" Locked="False">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpHandleMan" runat="server" Height="22" Width="90%">
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
                <f:FormRow ID="plApprove1">
                    <Items>
                        <f:TextArea ID="txtOpinions" runat="server" Label="我的意见" MaxLength="3000">
                        </f:TextArea>
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
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:HiddenField ID="hdSpotCheckCode" runat="server"></f:HiddenField>
                        <f:HiddenField ID="hdIds" runat="server"></f:HiddenField>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSave" OnClick="btnSave_Click" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1">
                        </f:Button>
                        <f:Button ID="btnSubmit" OnClick="btnSubmit_Click" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1">
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
        <f:Window ID="Window1" Title="选择工作包" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true"
            Width="1500px" Height="660px">
        </f:Window>
        <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" OnClose="WindowAtt_Close" runat="server" IsModal="true" Width="700px"
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

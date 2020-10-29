<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckColligationEdit.aspx.cs" ValidateRequest="false" Inherits="FineUIPro.Web.HSSE.Check.CheckColligationEdit" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑综合检查</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .labcenter
        {
            text-align: center;
        }
          .f-grid-row.red
        {
            background-color: #FF7575;
            background-image: none;
        }
        
        .fontred
        {
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
                    <f:TextBox ID="txtCheckColligationCode" runat="server" Label="检查编号" Readonly="true" MaxLength="50">
                    </f:TextBox>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="检查日期" ID="txtCheckDate">
                    </f:DatePicker>
                    <f:DropDownList ID="drpCheckType" runat="server" Label="检查类型" EnableEdit="true">
                       <f:ListItem Value="0" Text="周检" />
                       <f:ListItem Value="1" Text="月检" />
                       <f:ListItem Value="2" Text="其它" />
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                  <f:DropDownList ID="drpUnit" runat="server" Label="参与单位" EnableCheckBoxSelect="true" EnableMultiSelect="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpCheckPerson" runat="server" Label="检查组长" EnableEdit="true">
                    </f:DropDownList>
                    <f:Label runat="server"></f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpPartInPersons" runat="server" Label="组成员" EnableEdit="true" EnableMultiSelect="true"
                        ForceSelection="false" MaxLength="2000" EnableCheckBoxSelect="true">
                    </f:DropDownList>
                    <f:TextBox  runat="server" ID="txtPartInPersonNames" MaxLength="1000" Label="组成员"></f:TextBox>
                </Items>
            </f:FormRow>
       <%--     <f:FormRow>
                <Items>
                     <f:DropDownList ID="drpCheckAreas" runat="server" Label="单位工程" EnableCheckBoxSelect="true" EnableMultiSelect="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>--%>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" ClicksToEdit="1" DataIDField="CheckColligationDetailId"
                        DataKeyNames="CheckColligationDetailId" EnableMultiSelect="false" ShowGridHeader="true" Height="185" OnRowCommand="Grid1_RowCommand"
                        EnableColumnLines="true" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                    <%--<f:Button ID="btnCreateRectifyNotice" Icon="PageWhiteGet" runat="server" ToolTip="生成隐患整改通知单" ValidateForms="SimpleForm1"
                                        OnClick="btnCreateRectifyNotice_Click">
                                    </f:Button>--%>
                                    <f:Label ID="Label1" runat="server" CssClass="fontred" Label="说明" Text="双击检查项填报相关内容" LabelAlign="right" LabelWidth="50px"></f:Label>
                                     <f:ToolbarFill runat="server"></f:ToolbarFill>
                                    <f:Button ID="btnImport" ToolTip="导入" Icon="PageExcel" runat="server" ValidateForms="SimpleForm1"
                                            OnClick="btnImport_Click" Hidden="true">
                                    </f:Button>
                                    <f:Button ID="btnSelect" Icon="ShapeSquareSelect" runat="server" ToolTip="选择" ValidateForms="SimpleForm1"
                                        OnClick="btnSelect_Click">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RowNumberField HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:TemplateField Width="120px" HeaderText="检查类型" HeaderTextAlign="Center" TextAlign="Left" ColumnID="CheckItemType">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# ConvertCheckItemType(Eval("CheckItem")) %>'
                                        ToolTip='<%# ConvertCheckItemType(Eval("CheckItem")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <%--<f:RenderField Width="120px" ColumnID="CheckItemStr" DataField="CheckItemStr" SortField="CheckItemStr"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="检查项">
                            </f:RenderField>--%>
                            <f:LinkButtonField Width="120px" HeaderText="检查项" ConfirmTarget="Parent" CommandName="click"
                        TextAlign="Center" ToolTip="点击增加一条相同记录" DataTextField="CheckItemStr" ColumnID="CheckItemStr" />
                            <f:RenderField Width="220px" ColumnID="Unqualified" DataField="Unqualified" SortField="Unqualified"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="不合格项描述" ExpandUnusedSpace="true">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="Suggestions" DataField="Suggestions" SortField="Suggestions"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="整改要求">
                            </f:RenderField>
                            <f:RenderField Width="110px" ColumnID="WorkArea" DataField="WorkArea" SortField="WorkArea"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="单位工程">
                            </f:RenderField>
                            <f:RenderField Width="220px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="责任单位">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="HandleStepStr" DataField="HandleStepStr" SortField="HandleStepStr"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="处理措施">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="LimitedDate" DataField="LimitedDate" SortField="LimitedDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="限时时间"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:TemplateField Width="75px" HeaderText="整改情况" HeaderTextAlign="Center" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# ConvertCompleteStatus(Eval("CompleteStatus")) %>'
                                        ToolTip='<%# ConvertCompleteStatus(Eval("CompleteStatus")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="10px" ColumnID="UnitId" DataField="UnitId" SortField="UnitId"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Hidden="true" HeaderText="单位Id">
                            </f:RenderField>
                            <f:RenderField Width="10px" ColumnID="CheckArea" DataField="CheckArea" SortField="CheckArea"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Hidden="true" HeaderText="区域Id">
                            </f:RenderField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="dataload" Handler="onGridDataLoad" />
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                        </Listeners>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:HtmlEditor runat="server" Label="其他情况日小结" ID="txtDaySummary" ShowLabel="false"
                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="200" LabelAlign="Right">
                    </f:HtmlEditor>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true"
                        BodyPadding="0px">
                        <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp"></f:Label>
                     <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server" OnClick="btnAttachUrl_Click"
                        ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                        OnClick="btnSubmit_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                    <f:HiddenField ID="hdId" runat="server">
                    </f:HiddenField>
                    <f:HiddenField ID="hdAttachUrl" runat="server">
                    </f:HiddenField>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="Window1" Title="编辑检查项明细" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1100px" Height="520px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Icon="Pencil" runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Icon="Delete" ConfirmText="确定删除当前数据？" ConfirmTarget="Parent" runat="server" Text="删除"
            >
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccidentReportOtherEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Accident.AccidentReportOtherEdit" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑事故调查处理报告</title>
    <link href="../../Styles/Style.css" rel="stylesheetasp" type="text/css" />
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style type="text/css">
        .BackColor {
            color: Red;
            background-color: Silver;
        }

        .titler {
            color: Black;
            font-size: large;
        }

        .itemTitle {
            color: Black;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:Panel ID="Panel2" runat="server" Height="80px" ShowBorder="false"
            Title="事故调查处理报告表头" Layout="Table" TableConfigColumns="3" ShowHeader="false" BodyPadding="1px">
            <Items>
                <f:Panel ID="Panel1" Title="Panel1" Width="300px" Height="80px" MarginRight="0" TableRowspan="3"
                    runat="server" BodyPadding="1px" ShowBorder="false" ShowHeader="false">
                    <Items>
                        <f:Image ID="Image1" runat="server" ImageUrl="../../Images/Null.jpg" LabelAlign="right">
                        </f:Image>
                    </Items>
                </f:Panel>
                <f:Panel ID="Panel3" Title="Panel1" Width="400px" Height="30px" runat="server" BodyPadding="1px"
                    ShowBorder="false" ShowHeader="false">
                    <Items>
                        <f:Label ID="lblProjectName" runat="server" CssClass="titler" Margin="5 0 0 10">
                        </f:Label>
                    </Items>
                </f:Panel>
                <f:Panel ID="Panel5" Title="Panel1" Width="350px" Height="30px" runat="server" BodyPadding="1px"
                    ShowBorder="false" ShowHeader="false">
                    <Items>
                        <f:TextBox ID="txtAccidentReportOtherName" runat="server" LabelAlign="Right" Width="250px">
                        </f:TextBox>
                    </Items>
                </f:Panel>
                <f:Panel ID="Panel4" Title="Panel1" Width="300px" Height="50px" TableRowspan="2"
                    runat="server" BodyPadding="1px" ShowBorder="false" ShowHeader="false">
                    <Items>
                        <f:Label ID="Label3" runat="server" Text="事故调查处理报告" CssClass="titler" Margin="5 0 0 40">
                        </f:Label>
                    </Items>
                </f:Panel>
                <f:Panel ID="Panel7" Title="Panel1" Width="250px" Height="30px" runat="server" BodyPadding="1px"
                    ShowBorder="false" ShowHeader="false">
                    <Items>
                        <f:TextBox ID="txtAccidentReportOtherCode" runat="server" Label="事故编号" LabelAlign="Right"
                            Readonly="true">
                        </f:TextBox>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
        <f:TabStrip ID="TabStrip1" Height="230px" ShowBorder="true" TabPosition="Top"
            EnableTabCloseMenu="false" ActiveTabIndex="0" runat="server">
            <Tabs>
                <f:Tab ID="Tab1" Title="标签一" BodyPadding="5px" Layout="Fit" runat="server">
                    <Items>
                        <f:Form ID="Form2" runat="server" ShowHeader="false" AutoScroll="true">
                            <Items>
                                <f:FormRow ColumnWidths="33% 66%">
                                    <Items>
                                        <f:DropDownList ID="drpAccidentTypeId" runat="server" Label="事故类型" LabelAlign="Right"
                                            Required="true" ShowRedStar="true">
                                        </f:DropDownList>
                                        <f:TextBox ID="txtAbstract" runat="server" Label="提要" LabelAlign="Right" MaxLength="20"
                                            Required="true" ShowRedStar="true">
                                        </f:TextBox>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:DatePicker ID="txtAccidentDate" runat="server" Label="发生时间" LabelAlign="Right"
                                            EnableEdit="true" ShowRedStar="true" Required="true">
                                        </f:DatePicker>
                                        <f:TextBox ID="txtWorkArea" runat="server" Label="单位工程" LabelAlign="Right" MaxLength="200">
                                        </f:TextBox>
                                        <f:NumberBox ID="txtPeopleNum" runat="server" Label="人数" LabelAlign="Right" Required="true"
                                            ShowRedStar="true" NoDecimal="true" NoNegative="true">
                                        </f:NumberBox>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:DropDownList ID="drpUnitId" runat="server" Label="事故责任单位" LabelWidth="120px"
                                            LabelAlign="Right" Required="true" ShowRedStar="true">
                                        </f:DropDownList>
                                        <f:NumberBox ID="txtEconomicLoss" runat="server" Label="直接经济损失" LabelAlign="Right" LabelWidth="120px"
                                            NoDecimal="false" MinValue="0" NoNegative="true">
                                        </f:NumberBox>
                                        <f:NumberBox ID="txtEconomicOtherLoss" runat="server" Label="间接经济损失" LabelAlign="Right" LabelWidth="120px"
                                            NoDecimal="false" MinValue="0" NoNegative="true">
                                        </f:NumberBox>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="txtReportMan" runat="server" Label="报告人" LabelAlign="Right" MaxLength="50">
                                        </f:TextBox>
                                        <f:TextBox ID="txtReporterUnit" runat="server" Label="报告人单位" LabelAlign="Right" MaxLength="50">
                                        </f:TextBox>
                                        <f:DatePicker ID="txtReportDate" runat="server" Label="报告时间" LabelAlign="Right" EnableEdit="true">
                                        </f:DatePicker>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:TextArea ID="txtProcessDescription" runat="server" Label="事故过程描述" LabelAlign="Right"
                                            MaxLength="500" Height="50px">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:TextArea ID="txtEmergencyMeasures" runat="server" Label="紧急措施" LabelAlign="Right"
                                            MaxLength="500" Height="50px">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:TextArea ID="txtImmediateCause" runat="server" Label="直接原因" LabelAlign="Right"
                                            MaxLength="500" Height="50px">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:TextArea ID="txtIndirectReason" runat="server" Label="间接原因" LabelAlign="Right"
                                            MaxLength="500" Height="50px">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:TextArea ID="txtCorrectivePreventive" runat="server" Label="整改及预防措施" LabelAlign="Right"
                                            MaxLength="500" Height="50px">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:DropDownList ID="drpCompileMan" runat="server" Label="报告编制" LabelAlign="Right"
                                            EnableEdit="true">
                                        </f:DropDownList>
                                        <f:DatePicker ID="txtCompileDate" runat="server" Label="日期" LabelAlign="Right" EnableEdit="true">
                                        </f:DatePicker>
                                    </Items>
                                </f:FormRow>
                            </Items>
                            <Items>
                                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="调查组成员" EnableCollapse="true"
                                    runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="AccidentReportOtherItemId"
                                    DataIDField="AccidentReportOtherItemId" AllowPaging="true" IsDatabasePaging="true" PageSize="10"
                                    OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"
                                    EnableTextSelection="True" Height="220px">
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                                            <Items>
                                                <f:Label ID="lblT" runat="server" Text="调查组成员" CssClass="itemTitle">
                                                </f:Label>
                                                <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                                </f:ToolbarFill>
                                                <f:Button ID="btnNew" Icon="Add" runat="server" ToolTip="新增调查组成员" ValidateForms="SimpleForm1"
                                                    OnClick="btnNew_Click">
                                                </f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Columns>
                                        <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                            TextAlign="Center" />
                                        <f:TemplateField HeaderText="单位" Width="330px" HeaderTextAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# ConvertUnit(Eval("UnitId")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </f:TemplateField>
                                        <f:TemplateField HeaderText="姓名" Width="250px" HeaderTextAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# ConvertPerson(Eval("PersonId"))%>'></asp:Label>
                                            </ItemTemplate>
                                        </f:TemplateField>
                                        <f:TemplateField HeaderText="职务" Width="250px" HeaderTextAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# ConvertPosition(Eval("PositionId")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </f:TemplateField>
                                    </Columns>
                                    <Listeners>
                                        <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                                    </Listeners>
                                    <PageItems>
                                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                        </f:ToolbarSeparator>
                                        <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                                        </f:ToolbarText>
                                        <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                        </f:DropDownList>
                                    </PageItems>
                                </f:Grid>
                            </Items>
                        </f:Form>
                    </Items>
                </f:Tab>
                <f:Tab ID="Tab2" Title="标签二" BodyPadding="5px" runat="server">
                    <Items>
                        <f:HtmlEditor runat="server" Label="事故调查处理报告" ID="txtFileContents" ShowLabel="false"
                            Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="260" LabelAlign="Right">
                        </f:HtmlEditor>
                    </Items>
                </f:Tab>
            </Tabs>
        </f:TabStrip>
        <f:Panel ID="Panel6" Title="Panel1" Height="160px" runat="server" BodyPadding="1px"
            ShowBorder="false" ShowHeader="false">
            <Items>
                <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true"
                    BodyPadding="0px">
                    <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
                </f:ContentPanel>
            </Items>
        </f:Panel>
        <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
            <Items>
                <f:Label runat="server" ID="lbTemp">
                </f:Label>
                <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
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
        <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
            Width="650px" Height="220px">
        </f:Window>
        <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="Pencil" EnablePostBack="true"
                runat="server" Text="编辑">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
                Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除">
            </f:MenuButton>
        </f:Menu>
    </form>
</body>
</html>
<script type="text/javascript">
    var menuID = '<%= Menu1.ClientID %>';
    // 返回false，来阻止浏览器右键菜单
    function onRowContextMenu(event, rowId) {
        F(menuID).show();  //showAt(event.pageX, event.pageY);
        return false;
    }

    function reloadGrid() {
        __doPostBack(null, 'reloadGrid');
    }
</script>

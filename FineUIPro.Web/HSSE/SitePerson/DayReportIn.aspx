<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DayReportIn.aspx.cs" Inherits="FineUIPro.Web.HSSE.SitePerson.DayReportIn" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导入人工时日报</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" OnCustomEvent="PageManager1_CustomEvent" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Toolbars>
            <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnAudit" Icon="ApplicationEdit" runat="server" ToolTip="审核" ValidateForms="SimpleForm1"
                        OnClick="btnAudit_Click">
                    </f:Button>
                    <f:Button ID="btnImport" Icon="ApplicationGet" runat="server" ToolTip="导入" ValidateForms="SimpleForm1"
                        OnClick="btnImport_Click">
                    </f:Button>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnDownLoad" runat="server" Icon="ApplicationGo" ToolTip="下载模板" OnClick="btnDownLoad_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Rows>
            <f:FormRow>
                <Items>
                    <f:FileUpload runat="server" ID="fuAttachUrl" EmptyText="选择要导入的文件" Label="选择要导入的文件"
                        LabelWidth="150px">
                    </f:FileUpload>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        EnableColumnLines="true" BoxFlex="1" DataKeyNames="DayReportId" DataIDField="DayReportId" AllowSorting="true" SortField="CompileDate"
                        PageSize="12" Height="400px">
                        <Columns>
                            <f:TemplateField Width="55px" HeaderText="序号">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="200px" ColumnID="UnitName" DataField="UnitName" FieldType="String"
                                HeaderText="单位" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="StaffData" DataField="StaffData" FieldType="String"
                                HeaderText="人员情况" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="WorkPostName" DataField="WorkPostName" FieldType="String"
                                HeaderText="岗位" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="WorkTime" DataField="WorkTime" SortField="WorkTime"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="平均工时">
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="RealPersonNum" DataField="RealPersonNum" SortField="RealPersonNum"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="实际人数">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="PersonWorkTime" DataField="PersonWorkTime"
                                SortField="PersonWorkTime" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                HeaderText="当日人工时">
                            </f:RenderField>
                            <%--<f:RenderField Width="100px" ColumnID="YearPersonWorkTime" DataField="YearPersonWorkTime"
                                SortField="YearPersonWorkTime" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                HeaderText="当年人工时">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="TotalPersonWorkTime" DataField="TotalPersonWorkTime"
                                SortField="TotalPersonWorkTime" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                HeaderText="累计人工时">
                            </f:RenderField>--%>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:HiddenField ID="hdFileName" runat="server">
                    </f:HiddenField>
                    <f:HiddenField ID="hdCheckResult" runat="server">
                    </f:HiddenField>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label ID="lblBottom" runat="server" Text="说明：1 人工时日报导入模板中，灰色项为必填项。2 单位名称、岗位等必须与基础信息中对应类型的名称一致,否则无法导入。3 如需修改已有人工时日报，请到系统中修改。4 数据导入后，点击“保存”，即可完成人工时日报导入。">
                    </f:Label>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    <f:Window ID="Window1" Title="审核人工日报" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" OnClose="Window1_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>
    <f:Window ID="Window2" Title="导入人工日报" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" OnClose="Window2_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>
    </form>
</body>
</html>

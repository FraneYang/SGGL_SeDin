<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonChecking.aspx.cs" Inherits="FineUIPro.Web.Person.PersonChecking" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }

        .f-grid-cell.f-grid-cell-CheckContent .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }

        .f-grid-cell.f-grid-cell-TargetClass2 .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="员工绩效考核"
                    EnableCollapse="true" runat="server" BoxFlex="1" EnableColumnLines="true" DataIDField="QuarterCheckItemId"
                    AllowCellEditing="true" ClicksToEdit="1" SortField="SortId" SortDirection="Asc" EnableSummary="true" SummaryPosition="Flow">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:Label runat="server" ID="txtCheckedMan" Label="考核人" Readonly="true" LabelWidth="70px"></f:Label>
                                <f:Label runat="server" ID="txtRoleName" Label="岗位" Readonly="true" LabelWidth="60px" MarginLeft="20px"></f:Label>
                                <f:Label runat="server" ID="txtProject" Label="项目" Readonly="true" LabelWidth="60px" MarginLeft="20px"></f:Label>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1" OnClick="btnSave_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:TemplateField ColumnID="tfNumber" HeaderText="序号"
                            Width="60px" HeaderTextAlign="Center" TextAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="labNumber" runat="server" Text=' <%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1%>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField HeaderText="指标" ColumnID="TargetClass2"
                            DataField="TargetClass2" SortField="TargetClass2" FieldType="String" HeaderTextAlign="Center"
                            Width="150px"  HtmlEncode="false" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField HeaderText="考核内容" ColumnID="CheckContent"
                            DataField="CheckContent" SortField="CheckContent" FieldType="String" HeaderTextAlign="Center"
                            Width="500px" ExpandUnusedSpace="true" HtmlEncode="false">
                        </f:RenderField>
                        <f:RenderField HeaderText="满分" ColumnID="standardGrade"
                            DataField="standardGrade" SortField="standardGrade" FieldType="String" TextAlign="Center"
                            Width="80px" >
                        </f:RenderField>
                        <f:RenderField HeaderText="得分" ColumnID="Grade" DataField="Grade"
                            SortField="Grade" FieldType="String" TextAlign="Center" Width="80px">
                            <Editor>
                                <f:NumberBox ID="txtTotalCompleteNum1" runat="server" DecimalPrecision="1" MinValue="0" MaxValue="100" NoNegative="true" Text='<%# Bind("Grade") %>'>
                                </f:NumberBox>
                            </Editor>

                        </f:RenderField>
                    </Columns>
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Bottom" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:Label runat="server" ID="Label1" Text="考核结果分为优秀、称职、基本称职、不称职四个档次。考核得分为96-100分的确定为优秀；考核得分为76-95分的确定为称职；考核得分为60-75分的确定为基本称职；考核得分为59分及以下确定为不称职" Readonly="true" LabelWidth="1000px" CssStyle="color:red"></f:Label>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Listeners>
                        <f:Listener Event="afteredit" Handler="onGridAfterEdit" />
                    </Listeners>
                </f:Grid>
            </Items>
        </f:Panel>
    </form>
    <script type="text/javascript">
        function updateSummary() {
            // 回发到后台更新
            __doPostBack('', 'UPDATE_SUMMARY');
        }

        function onGridAfterEdit(event, value, params) {
            updateSummary();
        }
    </script>
</body>
</html>

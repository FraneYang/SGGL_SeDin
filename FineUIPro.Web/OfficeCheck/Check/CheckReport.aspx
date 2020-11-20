<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckReport.aspx.cs" Inherits="FineUIPro.Web.OfficeCheck.Check.CheckReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>检查报告</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <script src="../res/js/jquery-3.3.1.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" MarginBottom="5px"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server" ToolbarAlign="Right">
                    <Items>
                        <f:Label runat="server" ID="lbTitle" Text="监督检查报告"></f:Label>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                        <%--<f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Text="导出" Icon="FolderUp"
                            EnableAjax="false" DisableControlBeforePostBack="false">
                        </f:Button>--%>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" Text="保存" OnClick="btnSave_Click" >
                        </f:Button>
                        <f:HiddenField ID="hdCheckReportId" runat="server"></f:HiddenField>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:Label ID="lbName" Text="一、检查目的" CssClass="title" runat="server"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtValues1" runat="server" FocusOnPageLoad="true" Height="40px"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label1" Text="二、依据" CssClass="title" runat="server"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtValues2" runat="server" Height="45px"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label2" Text="三、受检单位（项目）安全管理基本情况" CssClass="title" runat="server"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtValues3" runat="server" Height="72px"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label3" Text="四、符合项" CssClass="title" runat="server"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtValues4" runat="server" Height="50px"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label4" Text="五、不符合项" CssClass="title" runat="server"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Grid ID="Grid2" ShowBorder="true" ShowHeader="false" AllowCellEditing="true" ClicksToEdit="1"
                            ForceFit="true" EnableCollapse="true" EnableColumnLines="true" EnableColumnMove="true" runat="server" BoxFlex="1"
                            DataKeyNames="CheckReportItemId" DataIDField="CheckReportItemId" AllowSorting="true" SortField="CheckReportItemId"
                            SortDirection="ASC" EnableTextSelection="True" MinHeight="240px" PageSize="500"
                            EnableRowDoubleClickEvent="true" OnRowCommand="Grid2_RowCommand" OnRowDataBound="Grid2_RowDataBound">
                            <Toolbars>
                                <f:Toolbar ID="toolAdd" Position="Top" ToolbarAlign="Right" runat="server">
                                    <Items>
                                        <f:Button ID="btnAdd" Icon="Add" runat="server" OnClick="btnAdd_Click" ToolTip="新增">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:RenderField HeaderText="主键" ColumnID="CheckReportItemId" DataField="CheckReportItemId"
                                    SortField="CheckReportItemId" FieldType="String" HeaderTextAlign="Center" TextAlign="Center"
                                    Hidden="true">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="CheckReportCode" DataField="CheckReportCode" FieldType="string"
                                    HeaderText="序号" HeaderTextAlign="Center">
                                    <Editor>
                                        <f:TextBox ID="txtCheckReportCode" runat="server" MaxLength="50">
                                        </f:TextBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField Width="300px" ColumnID="UnConformItem" DataField="UnConformItem" FieldType="string"
                                    HeaderText="不符合项" HeaderTextAlign="Center" ExpandUnusedSpace="true">
                                    <Editor>
                                        <f:TextBox ID="txtUnConformItem" runat="server" MaxLength="500">
                                        </f:TextBox>
                                    </Editor>
                                </f:RenderField>
                                <f:LinkButtonField HeaderText="整改前" ConfirmTarget="Top" Width="80" CommandName="AttachUrl"
                                    TextAlign="Center" ToolTip="整改照片" Text="详细" HeaderTextAlign="Center" />
                                <f:LinkButtonField ID="del" ColumnID="del" HeaderText="删除" Width="60px" CommandName="delete"
                                    Icon="Delete" TextAlign="Center" HeaderTextAlign="Center" />
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label5" Text="六、改进意见" CssClass="title" runat="server"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtValues6" runat="server" Height="64px"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label6" Text="七、检查结论" CssClass="title" runat="server"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtValues7" runat="server" Height="40px"></f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="检查报告"
                            runat="server" BoxFlex="1" DataKeyNames="CheckTeamId" AllowSorting="true" PageSize="100" SortField="SortIndex" SortDirection="ASC" OnSort="Grid1_Sort" EnableColumnLines="true" DataIDField="CheckTeamId" AllowPaging="false" EnableTextSelection="True">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar5" runat="server" ToolbarAlign="Right">
                                    <Items>
                                        <f:Label ID="Label8" runat="server" Text="八、检查工作组人员" CssClass="title"></f:Label>
                                        <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>
                                        <f:Button ID="btnRefresh" ToolTip="刷新工作组人员信息" Icon="ArrowRefresh" runat="server" OnClick="btnRefresh_Click">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:RenderField Width="70px" ColumnID="SortIndex" DataField="SortIndex"
                                    SortField="SortIndex" FieldType="String" HeaderText="序号"
                                    HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="90px" ColumnID="UserName" DataField="UserName"
                                    SortField="UserName" FieldType="String" HeaderText="姓名"
                                    HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="60px" ColumnID="SexName" DataField="SexName"
                                    SortField="SexName" FieldType="String" HeaderText="性别"
                                    HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="180px" ColumnID="UnitName" DataField="UnitName"
                                    SortField="UnitName" FieldType="String" HeaderText="所在单位" ExpandUnusedSpace="true"
                                    HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="220px" ColumnID="PostName" DataField="PostName"
                                    SortField="PostName" FieldType="String" HeaderText="所在单位职务"
                                    HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="WorkTitle" DataField="WorkTitle"
                                    SortField="WorkTitle" FieldType="String" HeaderText="职称"
                                    HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="CheckPostName" DataField="CheckPostName"
                                    SortField="CheckPostName" FieldType="String" HeaderText="检查工作组职务"
                                    HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="CheckDate" DataField="CheckDate"
                                    SortField="CheckDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                                    HeaderText="检查日期" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:FormRow>
            </Rows>
        </f:Form>
        <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
    </form>
</body>
</html>

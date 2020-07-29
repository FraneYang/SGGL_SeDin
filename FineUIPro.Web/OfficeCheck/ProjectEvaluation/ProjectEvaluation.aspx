<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectEvaluation.aspx.cs"
    Inherits="FineUIPro.Web.OfficeCheck.ProjectEvaluation.ProjectEvaluation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>项目评价</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .labcenter
        {
            text-align: center;
        }        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" />
    <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
        <Regions>
            <f:Region ID="Region1" ShowBorder="false" ShowHeader="false" RegionPosition="Left"
                BodyPadding="0 5 0 0" Width="200px" Layout="Fit" runat="server">
                <Items>
                    <f:Grid ID="Grid2" ShowBorder="true" ShowHeader="true" Title="项目名称" runat="server"
                        DataKeyNames="ProjectId,ProjectName" EnableMultiSelect="false" EnableRowSelectEvent="true"
                        OnRowSelect="Grid2_RowSelect" ShowGridHeader="false" EnableTextSelection="True">
                        <Columns>
                            <f:BoundField ExpandUnusedSpace="true" ColumnID="ProjectName" DataField="ProjectName"
                                DataFormatString="{0}" HeaderText="项目名称" />
                        </Columns>
                    </f:Grid>
                </Items>
            </f:Region>
            <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox"
                BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server">
                <Items>
                    <f:Grid ID="Grid1" Width="870px" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="PerfomanceRecordId" DataIDField="PerfomanceRecordId"  ForceFit="true"
                        AllowSorting="true" SortField="EvaluationDate" SortDirection="DESC" OnSort="Grid1_Sort"  EnableColumnLines="true"
                        AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                        EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                    <f:Button ID="btnNew" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNew_Click"
                                        Hidden="true">
                                    </f:Button>
                                    <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                        EnableAjax="false" DisableControlBeforePostBack="false">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:TemplateField ColumnID="tfNumber" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                                TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="100px" ColumnID="EvaluationDate" DataField="EvaluationDate"
                                SortField="EvaluationDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                                HeaderText="评价时间" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:TemplateField ColumnID="tfAssessmentGroup" Width="200px" HeaderText="评价组/人" HeaderTextAlign="Center"
                                TextAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssessmentGroup" runat="server" Text='<%# Bind("AssessmentGroup") %>'
                                        ToolTip='<%#Bind("AssessmentGroup") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField ColumnID="tfEvaluationDef" Width="260px" HeaderText="评价描述" HeaderTextAlign="Center"
                                TextAlign="Left" >
                                <ItemTemplate>
                                    <asp:Label ID="lblEvaluationDef" runat="server" Text='<%# Bind("EvaluationDef") %>'
                                        ToolTip='<%#Bind("EvaluationDef") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                           <%-- <f:WindowField TextAlign="Center" Width="80px" WindowID="WindowAtt" HeaderText="附件" Text="附件" ToolTip="附件上传查看"
                                DataIFrameUrlFields="PerfomanceRecordId" DataIFrameUrlFormatString="~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ProjectEvaluation&menuId=DEE90726-E00D-462B-A4BF-7E36180DD5B8" />--%>
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
            </f:Region>
        </Regions>
    </f:RegionPanel>
    <f:Window ID="Window1" Title="项目绩效评价" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1024px" Height="600px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="Pencil" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server"
            Text="删除">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/javascript">
        var menuID = '<%= Menu1.ClientID %>';
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>
</body>
</html>

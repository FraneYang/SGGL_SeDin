<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkPostStatistic.aspx.cs" Inherits="FineUIPro.Web.HSSE.SitePerson.WorkPostStatistic" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>现场岗位人工时统计</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>            
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="现场岗位人工时统计" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="ID" DataIDField="ID" AllowSorting="true" ForceFit="true" 
                 SortField="UnitName,WorkPostName" SortDirection="ASC"  EnableColumnLines="true" EnableTextSelection="True"
                AllowPaging="true" IsDatabasePaging="true" PageSize="20"    OnPageIndexChange="Grid1_PageIndexChange">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:DropDownList runat="server" ID="drpUnit" Label="单位" 
                                    Width="300px" LabelAlign="Right" LabelWidth="50px"></f:DropDownList>
                              <f:DropDownList runat="server" ID="drpWorkPost" Label="岗位"
                                    Width="200px" LabelAlign="Right" LabelWidth="50px"></f:DropDownList>
                            <f:DatePicker ID="txtStartDate" runat="server" Label="开始日期" LabelWidth="80px" Width="210px" EnableEdit="false">
                            </f:DatePicker>
                            <f:DatePicker ID="txtEndDate" runat="server" Label="结束日期"  LabelWidth="80px" Width="210px" EnableEdit="false">
                            </f:DatePicker>
                               <f:ToolbarFill runat="server"></f:ToolbarFill>
                            <f:Button ID="btnSearch" runat="server" Icon="SystemSearch" ToolTip="查询" OnClick="btnSearch_Click">
                            </f:Button>
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:TemplateField ColumnID="tfNumber" Width="55px" HeaderText="序号" HeaderTextAlign="Center"
                        TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="labNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="240px" ColumnID="UnitName" DataField="UnitName" 
                        SortField="UnitName" FieldType="String" HeaderText="单位名称" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="WorkPostName" DataField="WorkPostName"
                        SortField="WorkPostName" FieldType="String" HeaderText="岗位名称" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="PersonCount" DataField="PersonCount"
                        SortField="PersonCount" FieldType="Int" HeaderText="人数" HeaderTextAlign="Center"
                        TextAlign="Right">
                    </f:RenderField>
                    <f:TemplateField Width="120px" HeaderText="人工时" HeaderTextAlign="Center" TextAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# ConvertWorkTime(Eval("UnitWorkPostID")) %>'></asp:Label>                            
                        </ItemTemplate>
                    </f:TemplateField>
                </Columns>
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
    </f:Panel>
    </form>
</body>
</html>

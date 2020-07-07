<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonStatistic.aspx.cs" Inherits="FineUIPro.Web.HSSE.SitePerson.PersonStatistic" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>现场人员统计</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>            
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="现场人员统计" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="Number" DataIDField="Number" AllowSorting="true" ForceFit="true" 
                 SortField="Number" SortDirection="ASC"  EnableColumnLines="true" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:DropDownList runat="server" ID="drpUnit" Label="单位" 
                                    Width="300px" LabelAlign="Right" LabelWidth="50px"></f:DropDownList>
                              <f:DropDownList runat="server" ID="drpWorkPost" Label="岗位"
                                    Width="220px" LabelAlign="Right" LabelWidth="50px"></f:DropDownList>
                            <f:DatePicker ID="txtStartDate" runat="server" Label="开始日期" LabelWidth="80px" Width="220px">
                            </f:DatePicker>
                            <f:DatePicker ID="txtEndDate" runat="server" Label="结束日期"  LabelWidth="80px" Width="220px">
                            </f:DatePicker>
                           
                        </Items>
                    </f:Toolbar>
                     <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                             <f:RadioButtonList ID="rblIsUsed" runat="server" Label="进出场" LabelAlign="Right" Width="300px">
                                <f:RadioItem Value="0" Text="全部"  Selected="true"/>
                                <f:RadioItem Value="True" Text="进场" />
                                <f:RadioItem Value="False" Text="出场" />
                            </f:RadioButtonList>
                            <f:ToolbarFill runat="server"></f:ToolbarFill>
                            <f:Button ID="btnSearch" runat="server" Icon="BrickMagnify" ToolTip="查询" OnClick="btnSearch_Click">
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
                     <f:RenderField Width="200px" ColumnID="WorkAreaName" DataField="WorkAreaName"
                        SortField="WorkAreaName" FieldType="String" HeaderText="区域名称" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="WorkPostName" DataField="WorkPostName"
                        SortField="WorkPostName" FieldType="String" HeaderText="岗位名称" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="PersonCount" DataField="PersonCount"
                        SortField="PersonCount" FieldType="Int" HeaderText="出入人次" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <%-- <f:TemplateField Width="120px" HeaderText="总人数" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("PersonCount") %>' ToolTip='<%#Bind("PersonCount") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>--%>
                </Columns>
            </f:Grid>
        </Items>
    </f:Panel>
    </form>
</body>
</html>

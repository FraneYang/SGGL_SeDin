<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonInfo.aspx.cs" Inherits="FineUIPro.Web.HSSE.SitePerson.PersonInfo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>现场人员考勤管理</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="现场人员考勤管理" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="CheckingId"  DataIDField="CheckingId" AllowSorting="true" SortField="IntoOutTime"
                SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" ForceFit="true"
                PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableColumnLines="true" EnableTextSelection="True"
                OnRowDoubleClick="Grid1_RowDoubleClick" EnableRowDoubleClickEvent="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                                <f:DropDownList runat="server" ID="drpUnit" Label="单位"
                                    Width="300px" LabelAlign="Right" LabelWidth="50px"></f:DropDownList>
                               <f:TextBox runat="server" Label="姓名" ID="txtPersonName" EmptyText="输入查询条件"
                                         Width="200px" LabelWidth="50px" LabelAlign="right">
                                    </f:TextBox>
                                    <f:DatePicker runat="server" Label="时间" ID="txtStartDate" EnableEdit="true" LabelWidth="60px"  Width="180px"
                                        LabelAlign="Right">
                                    </f:DatePicker>
                                    <f:DatePicker runat="server" Label="至" ID="txtEndDate" EnableEdit="true" LabelWidth="40px" Width="180px"
                                        LabelAlign="Right">
                                    </f:DatePicker>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btSearch" ToolTip="查询" Icon="SystemSearch" runat="server" OnClick="btSearch_Click"></f:Button>
                            <f:Button ID="btnNew" ToolTip="增加" Icon="Add" runat="server" Hidden="true">
                            </f:Button>
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:TemplateField ColumnID="tfNumber" HeaderText="序号" Width="90px" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="labNumber" runat="server" Text=' <%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1%>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField> 
                    <f:RenderField Width="250px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                        FieldType="String" HeaderText="单位名称" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                      <f:RenderField Width="100px" ColumnID="PersonName" DataField="PersonName" SortField="PersonName"
                        FieldType="String" HeaderText="人员姓名" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                     <f:TemplateField ColumnID="tfIntoOut" Width="80px" HeaderText="进出" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIntoOut" runat="server" Text='<%# ConvertIntoOut(Eval("IntoOut")) %>'
                                ToolTip='<%#ConvertIntoOut(Eval("IntoOut")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="160px" ColumnID="IntoOutTime" DataField="IntoOutTime" SortField="IntoOutTime"
                        HeaderText="出入现场时间" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
        <%--            <f:TemplateField ColumnID="tfAddress" Width="130px" HeaderText="进出地点" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>' ToolTip='<%#Bind("Address") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>--%>
                    <f:TemplateField ColumnID="tfWorkAreaName" Width="120px" HeaderText="作业区域" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblWorkAreaName" runat="server" Text='<%# Bind("WorkAreaName") %>' ToolTip='<%#Bind("WorkAreaName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="100px" ColumnID="CardNo" DataField="CardNo" SortField="CardNo"
                        FieldType="String" HeaderText="卡号" HeaderTextAlign="Center"
                        TextAlign="Center">
                    </f:RenderField>
                    <f:TemplateField ColumnID="tfIdentityCard" Width="180px" HeaderText="身份证号" HeaderTextAlign="Center" TextAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblIdentityCard" runat="server" Text='<%# Bind("IdentityCard") %>' ToolTip='<%#Bind("IdentityCard") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>                     
                </Columns>
                <Listeners>
                      <f:Listener Event="dataload" Handler="onGridDataLoad" />
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
    </f:Panel>
    <f:Window ID="Window1" Title="编辑考勤" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="950px" Height="450px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Hidden="true" Icon="Pencil" runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" Icon="Delete" runat="server" Text="删除">
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

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }

        function onGridDataLoad(event) {
            this.mergeColumns(['UnitName']);            
        }
    </script>
</body>
</html>

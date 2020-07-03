<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccidentCase.aspx.cs" Inherits="FineUIPro.Web.HSSE.EduTrain.AccidentCase" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="230" Title="事故案例库" TitleToolTip="事故案例库" ShowBorder="true"
                ShowHeader="false" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Items>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>
                            <f:Button ID="btnNew" Icon="Add" runat="server" OnClick="btnNew_Click" Hidden="true">
                            </f:Button>
                            <f:Button ID="btnEdit" Icon="Pencil" runat="server" OnClick="btnEdit_Click" Hidden="true">
                            </f:Button>
                            <f:Button ID="btnDelete" Icon="Delete" ConfirmText="确定删除当前数据？" OnClick="btnDelete_Click"
                                runat="server" Hidden="true">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                    <f:Tree ID="trAccidentCase" Width="200px" EnableCollapse="true" ShowHeader="true"
                        Title="事故案例库" OnNodeCommand="trAccidentCase_NodeCommand" AutoLeafIdentification="true"
                        runat="server" EnableTextSelection="True">
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="事故案例明细" TitleToolTip="事故案例明细"
                Layout="VBox">
                <Items>
                    <f:Grid ID="Grid1" runat="server" Width="900px" ShowBorder="true" ShowHeader="false"
                        EnableCollapse="true" BoxFlex="1" DataKeyNames="AccidentCaseItemId" DataIDField="AccidentCaseItemId" 
                        AllowSorting="true" SortField="Activities" ForceFit="true"
                        SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" EnableColumnLines="true"
                        PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                        OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar3" Position="Top" runat="server">
                                <Items>
                                    <f:TextBox ID="Activities" runat="server" Label="作业活动" EmptyText="输入查询作业活动" AutoPostBack="true"
                                        OnTextChanged="TextBox_TextChanged" Width="230px" LabelWidth="80px" LabelAlign="Right">
                                    </f:TextBox>
                                    <f:TextBox ID="AccidentCaseName" runat="server" Label="事故类别" EmptyText="输入查询事故类别"
                                        AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="230px" LabelWidth="80px"
                                        LabelAlign="Right">
                                    </f:TextBox>
                                    <f:TextBox ID="AccidentName" runat="server" Label="事故名称" EmptyText="输入查询事故名称" AutoPostBack="true"
                                        OnTextChanged="TextBox_TextChanged" Width="230px" LabelWidth="80px" LabelAlign="Right">
                                    </f:TextBox>
                                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                                    <f:Button ID="btnNewDetail" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewDetail_Click"
                                        Hidden="true">
                                    </f:Button>
                                    <f:Button ID="btnSelectColumns" runat="server" ToolTip="导出" Icon="FolderUp" EnablePostBack="false" Hidden="true">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                           <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                            <f:TemplateField Width="120px" HeaderText="作业活动" HeaderTextAlign="Center" TextAlign="Center"
                                SortField="Activities">
                                <ItemTemplate>
                                    <asp:Label ID="lblActivities" runat="server" Text='<%# Bind("Activities") %>' ToolTip='<%#Bind("Activities") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                      <%--      <f:TemplateField Width="100px" HeaderText="事故类别" HeaderTextAlign="Center" TextAlign="Center"
                                SortField="AccidentCaseName">
                                <ItemTemplate>
                                    <asp:Label ID="lblAccidentCaseName" runat="server" Text='<%# Bind("AccidentCaseName") %>'
                                        ToolTip='<%#Bind("AccidentCaseName") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>--%>
                            <f:TemplateField Width="120px" HeaderText="事故名称" HeaderTextAlign="Center" TextAlign="Center"
                                SortField="AccidentName">
                                <ItemTemplate>
                                    <asp:Label ID="lblAccidentName" runat="server" Text='<%# Bind("AccidentName") %>'
                                        ToolTip='<%#Bind("AccidentName") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="280px" HeaderText="事故简况" HeaderTextAlign="Center" TextAlign="Left"
                                SortField="AccidentProfiles">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("AccidentProfiles") %>' ToolTip='<%#Bind("AccidentProfiles") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="300px" HeaderText="事故点评" HeaderTextAlign="Center" TextAlign="Left"
                                SortField="AccidentReview">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("AccidentReview") %>' ToolTip='<%#Bind("AccidentReview") %>'></asp:Label>
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
            </f:Panel>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="事故案例类型" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="400px" Height="200px">
    </f:Window>
    <f:Window ID="Window2" Title="安全事故案例" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="true"
        Width="600px" Height="400px">
    </f:Window>
    <f:Window ID="Window5" Title="选择需要导出的列" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window5_Close" IsModal="true"
        Width="450px" Height="250px" EnableAjax="false">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            runat="server" Text="编辑" Hidden="true" Icon="Pencil">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true" Icon="Delete"
            ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除" Hidden="true">
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
    </script>
</body>
</html>

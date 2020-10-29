<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Rectify.aspx.cs" Inherits="FineUIPro.Web.HSSE.Technique.Rectify" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>安全隐患</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />     
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="250" Title="安全隐患" TitleToolTip="安全隐患" ShowBorder="true"
                ShowHeader="false" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Items>
                     <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>
                            <f:Button ID="btnNew" Icon="Add" runat="server" Hidden="true" OnClick="btnNew_Click">
                            </f:Button>
                            <f:Button ID="btnEdit" Icon="Pencil" runat="server" Hidden="true" OnClick="btnEdit_Click">
                            </f:Button>
                            <f:Button ID="btnDelete" Icon="Delete" Hidden="true" ConfirmText="确定删除当前数据？" OnClick="btnDelete_Click"
                                runat="server">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                    <f:Tree ID="trRectify" Width="200px" EnableCollapse="true" ShowHeader="true" Title="作业类别"
                        OnNodeCommand="trRectify_NodeCommand" AutoLeafIdentification="true" runat="server"
                        EnableTextSelection="True">
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="安全隐患明细" TitleToolTip="安全隐患明细"
                Layout="VBox">
                <Items>
                    <f:Grid ID="Grid1" Width="870px" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="RectifyItemId" AllowCellEditing="true" EnableColumnLines="true"
                        ClicksToEdit="2" DataIDField="RectifyItemId" AllowSorting="true" SortField="RectifyName"
                        SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                        PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                        OnRowDoubleClick="Grid1_RowDoubleClick"  EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar3" Position="Top" runat="server">
                                <Items>
                                    <f:TextBox ID="RectifyName" runat="server" Label="作业类别" EmptyText="输入查询条件" AutoPostBack="true"
                                      Width="250px" LabelWidth="90px">
                                    </f:TextBox>
                                    <f:TextBox ID="HazardSourcePoint" runat="server" Label="隐患源点" EmptyText="输入查询条件"
                                        Width="250px" LabelWidth="90px">
                                    </f:TextBox>
                                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                                    <f:Button ID="btnQuery" ToolTip="查询" Icon="SystemSearch" EnablePostBack="true" 	
                                            OnClick="TextBox_TextChanged" runat="server" >
                                        </f:Button>
                                    <f:Button ID="btnNewDetail" ToolTip="新增" Hidden="true" Icon="Add" runat="server" OnClick="btnNewDetail_Click">
                                    </f:Button>               
                                    <f:Button ID="btnSelectColumns" runat="server" Hidden="true" ToolTip="导出" Icon="FolderUp" EnablePostBack="false">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>                       
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="45px" HeaderTextAlign="Center" TextAlign="Center"/>
                            <f:TemplateField Width="110px" HeaderText="作业类别" HeaderTextAlign="Center" TextAlign="Left"
                                SortField="RectifyName">
                                <ItemTemplate>
                                    <asp:Label ID="lblRectifyName" runat="server" Text='<%# Bind("RectifyName") %>' ToolTip='<%#Bind("RectifyName") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="200px" HeaderText="隐患源点" HeaderTextAlign="Center" TextAlign="Left"
                                SortField="HazardSourcePoint">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("ShortHazardSourcePoint") %>' ToolTip='<%#Bind("HazardSourcePoint") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="120px" HeaderText="风险分析" HeaderTextAlign="Center" TextAlign="Left"
                                SortField="RiskAnalysis">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("RiskAnalysis") %>' ToolTip='<%#Bind("RiskAnalysis") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="230px" HeaderText="风险防范" HeaderTextAlign="Center" TextAlign="Left"
                                SortField="RiskPrevention" ExpandUnusedSpace="true">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("ShortRiskPrevention") %>' ToolTip='<%#Bind("RiskPrevention") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="110px" HeaderText="同类风险" HeaderTextAlign="Center" TextAlign="Left"
                                SortField="SimilarRisk">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("ShortSimilarRisk") %>' ToolTip='<%#Bind("SimilarRisk") %>'></asp:Label>
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
    <f:Window ID="Window1" Title="作业类别" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="600px" Height="360px">
    </f:Window>
    <f:Window ID="Window2" Title="安全隐患" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" OnClose="Window2_Close" IsModal="true"
        Width="700px" Height="450px">
    </f:Window>
    <f:Window ID="Window5" Title="选择需要导出的列" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" OnClose="Window5_Close" IsModal="true"
        Width="450px" Height="250px" EnableAjax="false">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑" Icon="Pencil">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true" Icon="Delete"
            Hidden="true" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/jscript">
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

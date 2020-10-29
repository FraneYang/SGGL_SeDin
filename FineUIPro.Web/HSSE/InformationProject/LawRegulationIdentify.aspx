<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LawRegulationIdentify.aspx.cs" Inherits="FineUIPro.Web.HSSE.InformationProject.LawRegulationIdentify" %>

<!DOCTYPE html>     
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>法律法规辨识记录</title>
   <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" Title="法律法规辨识记录" ShowHeader="false"
        Layout="HBox">
        <Items>
            <f:Grid ID="Grid1" Title="法律法规辨识记录" ShowHeader="false" EnableCollapse="true" PageSize="10"
                ShowBorder="true" AllowPaging="true" IsDatabasePaging="true" runat="server" EnableColumnLines="true"
                DataKeyNames="LawRegulationIdentifyId" DataIDField="LawRegulationIdentifyId"  AllowSorting="true"
                SortField="LawRegulationIdentifyCode" SortDirection="DESC" OnSort="Grid1_Sort"  ForceFit="true"
                OnPageIndexChange="Grid1_PageIndexChange" AllowFilters="true" EnableRowDoubleClickEvent="true"
                OnRowDoubleClick="Grid1_RowDoubleClick" OnFilterChange="Grid1_FilterChange">
                <Toolbars>
                    <f:Toolbar runat="server" ID="Toolbar4">
                        <Items>
                            <f:TextBox runat="server" EmptyText="按编号查询" AutoPostBack="True" Label="编号" LabelWidth="70px"
                                Width="250px" ID="txtLawRegulationIdentifyCode" OnTextChanged="TextBox_TextChanged" LabelAlign="Right">
                            </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" Hidden="true" EnablePostBack="true" runat="server" OnClick="btnNew_Click">
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
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                        <f:RenderField Width="220px" ColumnID="LawRegulationIdentifyCode" DataField="LawRegulationIdentifyCode" FieldType="String"
                        HeaderText="编号" HeaderTextAlign="Center" TextAlign="Center" >
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="VersionNumber" DataField="VersionNumber" FieldType="String"
                        HeaderText="版本号" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="IdentifyPersonName" DataField="IdentifyPersonName"
                        FieldType="String" HeaderText="整理人" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="IdentifyDate" DataField="IdentifyDate" FieldType="Date"
                        Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="实施日期" HeaderTextAlign="Center"
                        TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="UpdateDate" DataField="UpdateDate" FieldType="Date"
                        Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="更新日期" HeaderTextAlign="Center"
                        TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="FlowOperateName" DataField="FlowOperateName"
                        SortField="FlowOperateName" FieldType="String" HeaderText="状态" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
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
    <f:Window ID="Window1" Title="法律法规辨识记录" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1300px" Height="600px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Hidden="true"
            Text="修改" Icon="Pencil" OnClick="btnMenuModify_Click">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true" Hidden="true"
            ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Icon="Delete" Text="删除">
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

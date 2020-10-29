<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectMap.aspx.cs" Inherits="FineUIPro.Web.PZHGL.ProjectData.ProjectMap" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>项目地图</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .customlabel span
        {
            color: red;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />  
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch" RegionPosition="Center">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="项目地图" EnableCollapse="true"
                runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="ProjectMapId" ForceFit="true"
                DataIDField="ProjectMapId" AllowSorting="true" SortField="UploadDate" SortDirection="DESC" OnSort="Grid1_Sort" 
                AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">               
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>                          
                            <f:DropDownBox runat="server" ID="drpProject" Label="项目" EmptyText="请从下拉表格中选择" MatchFieldWidth="false" LabelAlign="Right"
                                EnableMultiSelect="true"  LabelWidth="80px" Width="400px" AutoPostBack="true" OnTextChanged="drpProject_OnSelectedIndexChanged">
                                <PopPanel>
                                    <f:Grid ID="Grid2" ShowBorder="true" ShowHeader="false" runat="server" DataIDField="ProjectId" DataTextField="ProjectName"
                                        DataKeyNames="ProjectId"  AllowSorting="true" SortField="ProjectCode" SortDirection="ASC" EnableColumnLines="true"
                                        Hidden="true" Width="650px" Height="400px"  PageSize="300">
                                        <Toolbars>
                                            <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                                                <Items>
                                                    <f:TextBox runat="server" Label="名称" ID="txtProjectName" EmptyText="输入查询条件" 
                                                        AutoPostBack="true" OnTextChanged="cbType_SelectedIndexChanged" Width="190px" LabelWidth="50px">
                                                     </f:TextBox> 
                                                     <f:TextBox runat="server" Label="编号" ID="txtProjectCode" EmptyText="输入查询条件" 
                                                        AutoPostBack="true" OnTextChanged="cbType_SelectedIndexChanged" Width="180px" LabelWidth="50px">
                                                     </f:TextBox> 
                                                     <f:ToolbarFill runat="server"></f:ToolbarFill>
                                                    <f:RadioButtonList runat="server" ID="cbType" ShowLabel="true" Width="220px"  LabelAlign="Right"
                                                        OnSelectedIndexChanged="cbType_SelectedIndexChanged"  AutoPostBack="true" Label="状态" LabelWidth="50px">                                                            
                                                            <f:RadioItem Text="施工中" Value="1" Selected="true"/>
                                                            <f:RadioItem Text="暂停/完工" Value="2" />
                                                        </f:RadioButtonList>
                                                </Items>
                                            </f:Toolbar>
                                        </Toolbars>
                                        <Columns>
                                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" 
                                                HeaderTextAlign="Center" TextAlign="Center"/>
                                              <f:RenderField Width="120px" ColumnID="ProjectCode" DataField="ProjectCode" 
                                                SortField="ProjectCode" FieldType="String" HeaderText="编号" HeaderTextAlign="Center"
                                                TextAlign="Left">                       
                                            </f:RenderField>
                                            <f:RenderField Width="150px" ColumnID="ProjectName" DataField="ProjectName" EnableFilter="true"
                                                SortField="ProjectName" FieldType="String" HeaderText="名称" HeaderTextAlign="Center" ExpandUnusedSpace="true"
                                                TextAlign="Left">                      
                                            </f:RenderField>                                          
                                        </Columns>
                                    </f:Grid>
                                </PopPanel>
                            </f:DropDownBox>
                             <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                            <f:TextBox runat="server" Label="标题" ID="txtTitle" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="250px" LabelAlign="right" LabelWidth="70px">
                            </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" Hidden="true"
                                runat="server">
                            </f:Button>
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>                                      
                    <f:TemplateField ColumnID="tfNumber" Width="60px" HeaderText="序号" HeaderTextAlign="Center"
                        TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="220px" ColumnID="Title" DataField="Title" SortField="Title" 
                        FieldType="String" HeaderText="标题" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="150px" ColumnID="MapTypeName" DataField="MapTypeName" SortField="MapTypeName"
                        FieldType="String" HeaderText="分类" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="UploadDate" DataField="UploadDate" SortField="UploadDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="上传日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                     <f:RenderField Width="100px" ColumnID="CompileManName" DataField="CompileManName"
                        SortField="CompileManName" FieldType="String" HeaderText="整理人" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:TemplateField ColumnID="tfShortContentDef" HeaderText="简要说明" Width="350px" HeaderTextAlign="Center" TextAlign="Left" >
                        <ItemTemplate>
                            <asp:Label ID="lblShortContentDef" runat="server" Text='<%# Bind("ShortContentDef") %>' ToolTip='<%#Bind("ContentDef") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>                      
                  <%--  <f:WindowField TextAlign="Left" Width="80px" WindowID="WindowAtt" HeaderTextAlign="Center"
                        Text="附件" HeaderText="附件" ToolTip="附件查看" DataIFrameUrlFields="ProjectMapId" DataIFrameUrlFormatString="~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ProjectMapAttachUrl&type=-1&menuId=B58179BE-FE6E-4E91-84FC-D211E4692354"/>--%>
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
    <f:Window ID="Window1" Title="项目地图" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true"
        Width="800px" Height="400px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
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

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>

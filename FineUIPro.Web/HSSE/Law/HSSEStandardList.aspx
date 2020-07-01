<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSSEStandardList.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Law.HSSEStandardList" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全标准规范" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="StandardId" AllowCellEditing="true" ForceFit="true"
                ClicksToEdit="2" DataIDField="StandardId" AllowSorting="true" SortField="TypeName"
                SortDirection="ASC" OnSort="Grid1_Sort" EnableColumnLines="true" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                        <Items>
                            <f:TextBox ID="txtStandardNo" runat="server" Label="标准号" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px" LabelAlign="Right">
                            </f:TextBox>
                            <f:TextBox ID="txtStandardName" runat="server" Label="标准名称" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="90px"
                                LabelAlign="Right">
                            </f:TextBox>
                            <f:TextBox ID="txtTypeName" runat="server" Label="分类" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="60px" LabelAlign="Right">
                            </f:TextBox>
                            <f:ToolbarFill runat="server"></f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                Hidden="true">
                            </f:Button>                               
                            <f:Button ID="btnSelectColumns" runat="server" ToolTip="导出" Icon="FolderUp" EnablePostBack="false" Hidden="true">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                   <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                    <f:TemplateField Width="120px" HeaderText="标准号" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="StandardNo">
                        <ItemTemplate>
                            <asp:Label ID="lblStandardNo" runat="server" Text='<%# Bind("StandardNo") %>' ToolTip='<%#Bind("StandardNo") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="350px" HeaderText="标准名称" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="StandardName" >
                        <ItemTemplate>
                            <asp:Label ID="lblStandardName" runat="server" Text='<%# Bind("StandardName") %>'
                                ToolTip='<%#Bind("StandardName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="110px" HeaderText="分类" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="TypeName">
                        <ItemTemplate>
                            <asp:Label ID="lblTypeName" runat="server" Text='<%# Bind("TypeName") %>' ToolTip='<%#Bind("TypeName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="100px" HeaderText="标准级别" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="StandardGrade">
                        <ItemTemplate>
                            <asp:Label ID="lblStandardGrade" runat="server" Text='<%# Bind("StandardGrade") %>'
                                ToolTip='<%#Bind("StandardGrade") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:GroupField HeaderText="对应HSSE索引" TextAlign="Center" Hidden="true">
                        <Columns>
                            <f:RenderField Width="90px" ColumnID="IsSelected1" DataField="IsSelected1" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="01(地基处理)" HeaderToolTip="地基处理" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected2" DataField="IsSelected2" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="02(打桩)" HeaderToolTip="打桩" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected3" DataField="IsSelected3" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="03(基坑支护与降水工程)" HeaderToolTip="基坑支护与降水工程"
                                TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected4" DataField="IsSelected4" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="04(土方开挖工程)" HeaderToolTip="土方开挖工程"
                                TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected5" DataField="IsSelected5" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="05(模板工程)" HeaderToolTip="模板工程" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected6" DataField="IsSelected6" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="06(基础施工)" HeaderToolTip="基础施工" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected7" DataField="IsSelected7" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="07(钢筋混凝土结构)" HeaderToolTip="钢筋混凝土结构"
                                TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected8" DataField="IsSelected8" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="08(地下管道)" HeaderToolTip="地下管道" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected9" DataField="IsSelected9" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="09(钢结构)" HeaderToolTip="钢结构" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected10" DataField="IsSelected10" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="10(设备安装)" HeaderToolTip="设备安装" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected11" DataField="IsSelected11" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="11(大型起重吊装工程)" HeaderToolTip="大型起重吊装工程"
                                TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected12" DataField="IsSelected12" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="12(脚手架工程)" HeaderToolTip="脚手架工程"
                                TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected13" DataField="IsSelected13" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="13(机械安装)" HeaderToolTip="机械安装" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected14" DataField="IsSelected14" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="14(管道安装)" HeaderToolTip="管道安装">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected15" DataField="IsSelected15" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="15(电气仪表安装)" HeaderToolTip="电气仪表安装"
                                TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected16" DataField="IsSelected16" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="16(防腐保温防火)" HeaderToolTip="防腐保温防火"
                                TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected17" DataField="IsSelected17" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="17(拆除)" HeaderToolTip="拆除" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected18" DataField="IsSelected18" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="18(爆破工程)" HeaderToolTip="爆破工程" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected19" DataField="IsSelected19" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="19(试压)" HeaderToolTip="试压" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected20" DataField="IsSelected20" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="20(吹扫)" HeaderToolTip="吹扫" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected21" DataField="IsSelected21" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="21(试车)" HeaderToolTip="试车" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected22" DataField="IsSelected22" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="22(无损检测)" HeaderToolTip="无损检测" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected23" DataField="IsSelected23" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="23(其他危险性较大的工程)" HeaderToolTip="其他危险性较大的工程"
                                TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected24" DataField="IsSelected24" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="24(设计)" HeaderToolTip="设计" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected25" DataField="IsSelected25" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="25(工厂运行)" HeaderToolTip="工厂运行" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected90" DataField="IsSelected90" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="90(全部标准)" HeaderToolTip="全部标准" TextAlign="Center">
                            </f:RenderField>
                        </Columns>
                    </f:GroupField>
                    <f:WindowField TextAlign="Center" Width="80px" WindowID="WindowAtt" HeaderText="附件"
                        Text="详细" ToolTip="附件上传查看" DataIFrameUrlFields="StandardId" DataIFrameUrlFormatString="../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HSSEStandardsList&menuId=EFDSFVDE-RTHN-7UMG-4THA-5TGED48F8IOL"/>              
                </Columns>
                <Listeners>
                    <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                </Listeners>
                <PageItems>
                    <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                    </f:ToolbarSeparator>
                    <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                    </f:ToolbarText>
                    <f:DropDownList runat="server" ID="ddlPageSize" Width="90px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">                    
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="安全标准规范" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1100px" Height="550px">
    </f:Window>
    <f:Window ID="Window5" Title="选择需要导出的列" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window5_Close" IsModal="true"
        Width="350px" Height="150px" EnableAjax="false">
    </f:Window>
       <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Parent" EnableResize="true" runat="server"
            IsModal="true" Width="700px" Height="500px">
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
    <script type="text/jscript">
        function renderSelect(value) {
            return value == "True" ? '<font size="5">●</font>' : '';
        }

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

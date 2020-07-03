<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyEnvironmental.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Technique.CompanyEnvironmental" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>公司环境因素危险源</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />    
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="公司环境因素危险源" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="EnvironmentalId" EnableColumnLines="true" DataIDField="EnvironmentalId"
                AllowSorting="true" SortField="SmallTypeName,EType,Code" SortDirection="DESC" ForceFit="true"
                OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" PageSize="10"
                OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"
                EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                        <Items>
                            <f:DropDownList ID="drpSmallType" runat="server" Label="类型"
                                EnableEdit="true" LabelWidth="60px" Width="160px" LabelAlign="right" >
                            </f:DropDownList>
                            <f:DropDownList ID="drpEType" runat="server" Label="环境类型" EnableEdit="true"
                                LabelWidth="80px" Width="200px" LabelAlign="right" >
                            </f:DropDownList>
                            <f:TextBox ID="txtActivePoint" runat="server" Label="活动点" EmptyText="输入查询名称"
                                Width="200px" LabelWidth="70px"
                                LabelAlign="Right">
                            </f:TextBox>
                            <f:TextBox ID="txtEnvironmentalFactors" runat="server" Label="环境因素" EmptyText="输入查询名称"
                                 Width="200px" LabelWidth="80px"
                                LabelAlign="Right">
                            </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnQuery" ToolTip="查询" Icon="SystemSearch" EnablePostBack="true" 	
                                OnClick="TextBox_TextChanged" runat="server" >
                            </f:Button>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                Hidden="true">
                            </f:Button>
                            <f:Button ID="btnImport" ToolTip="导入" Icon="ApplicationGet" Hidden="true" runat="server"
                                OnClick="btnImport_Click">
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
                            <asp:Label ID="lbNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="70px" ColumnID="Code" DataField="Code" FieldType="String" HeaderText="代码"
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="SmallTypeName" DataField="SmallTypeName" FieldType="String"
                        HeaderText="类型" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="ETypeName" DataField="ETypeName" FieldType="String"
                        HeaderText="环境类型" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="180px" ColumnID="ActivePoint" DataField="ActivePoint" FieldType="String"
                        HeaderText="分项工程/活动点" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="EnvironmentalFactors" DataField="EnvironmentalFactors"
                        FieldType="String" HeaderText="环境因素" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:GroupField HeaderText="污染类" TextAlign="Center">
                        <Columns>
                            <f:RenderField Width="45px" ColumnID="AValue" DataField="AValue" FieldType="String"
                                HeaderText="A值" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="45px" ColumnID="BValue" DataField="BValue" FieldType="String"
                                HeaderText="B值" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="45px" ColumnID="CValue" DataField="CValue" FieldType="String"
                                HeaderText="C值" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="45px" ColumnID="DValue" DataField="DValue" FieldType="String"
                                HeaderText="D值" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="45px" ColumnID="EValue" DataField="EValue" FieldType="String"
                                HeaderText="E值" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="45px" ColumnID="ZValue1" DataField="ZValue1" FieldType="String"
                                HeaderText="Σ" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                        </Columns>
                    </f:GroupField>
                    <f:GroupField HeaderText="能源资源类" TextAlign="Center">
                        <Columns>
                            <f:RenderField Width="45px" ColumnID="FValue" DataField="FValue" FieldType="String"
                                HeaderText="F值" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="45px" ColumnID="GValue" DataField="GValue" FieldType="String"
                                HeaderText="G值" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="45px" ColumnID="ZValue2" DataField="ZValue2" FieldType="String"
                                HeaderText="Σ" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                        </Columns>
                    </f:GroupField>
                    <f:CheckBoxField Width="60px" RenderAsStaticField="true" TextAlign="Center" DataField="IsImportant"
                        HeaderText="重要" />
                    <f:RenderField Width="110px" ColumnID="ControlMeasures" DataField="ControlMeasures"
                        FieldType="String" HeaderText="安全措施" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                   <%-- <f:RenderField Width="120px" ColumnID="Remark" DataField="Remark" FieldType="String"
                        HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>--%>
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
    <f:Window ID="Window1" Title="编辑公司环境因素危险源" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1024px" Height="500px">
    </f:Window>
    <f:Window ID="Window2" Title="导入公司环境因素危险源" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1024px" Height="600px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            runat="server" Text="编辑" Icon="Pencil" Hidden="true">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除" Icon="Delete"
            Hidden="true">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/javascript">
        function renderBool(value) {
            return value == "True" ? '是' : '否';
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

        function onGridDataLoad(event) {
            this.mergeColumns(['ETypeName']);
            this.mergeColumns(['SmallTypeName']);
        }
    </script>
</body>
</html>

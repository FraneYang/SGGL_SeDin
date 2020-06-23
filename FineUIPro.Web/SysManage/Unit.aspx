<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Unit.aspx.cs" Inherits="FineUIPro.Web.SysManage.Unit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>单位设置</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }     
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true"  EnableCollapse="true" ShowHeader="false"
                runat="server" BoxFlex="1" DataKeyNames="UnitId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="UnitId" AllowSorting="true" SortField="UnitCode"
                SortDirection="ASC" OnSort="Grid1_Sort"  AllowPaging="true" IsDatabasePaging="true" 
                PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" ForceFit="true"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" 
                EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                             <f:TextBox runat="server" Label="单位名称" ID="txtUnitName" EmptyText="输入查询条件" 
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"></f:TextBox>                       
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>    
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server" Hidden="true">
                            </f:Button>   
                            <f:Button ID="btnImport" ToolTip="导入" Icon="ApplicationGet" Hidden="true" runat="server"
                                OnClick="btnImport_Click">
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
                    <f:RenderField Width="110px" ColumnID="UnitCode" DataField="UnitCode" SortField="UnitCode"
                        FieldType="String" HeaderText="单位代码" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="240px" ColumnID="UnitName" DataField="UnitName" TextAlign="Left"
                        SortField="UnitName" FieldType="String" HeaderText="名称" HeaderTextAlign="Center">                       
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="UnitTypeName" DataField="UnitTypeName" SortField="UnitTypeName"
                        FieldType="String" HeaderText="类型" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>                    
                    <f:RenderField Width="250px" ColumnID="Address" DataField="Address"
                        FieldType="String" HeaderText="地址" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="Telephone" DataField="Telephone"
                        FieldType="String" HeaderText="电话" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
              <%--      <f:CheckBoxField Width="75px" RenderAsStaticField="true"
                        TextAlign="Center"  DataField="IsBranch" HeaderText="分公司" />
                    <f:RenderField Width="230px" ColumnID="supUnitName" DataField="supUnitName" TextAlign="Left"
                        SortField="supUnitName" FieldType="String" HeaderText="上级单位" HeaderTextAlign="Center">                       
                    </f:RenderField>--%>
                    <f:WindowField ColumnID="SubUnit" Width="70px" WindowID="WindowSubUnit" HeaderText="资质"
                                Text="详细" ToolTip="资质详细信息" DataTextFormatString="{0}" DataIFrameUrlFields="UnitId"
                                DataIFrameUrlFormatString="../QualityAudit/SubUnitQualityEdit.aspx?UnitId={0}"/>             
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
    <f:Window ID="Window1" Title="设置" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server"  IsModal="true" 
        Width="900px" Height="400px">
    </f:Window>
    <f:Window ID="Window2" Title="导入" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="false"
        CloseAction="HidePostBack" Width="1200px" Height="600px">
    </f:Window>
    <f:Window ID="WindowSubUnit" Title="资质" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true"
        Width="1024px" Height="520px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true" Hidden="true"
            runat="server" Text="编辑" Icon="TableEdit">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true" Hidden="true"
            ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除" Icon="Delete">
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

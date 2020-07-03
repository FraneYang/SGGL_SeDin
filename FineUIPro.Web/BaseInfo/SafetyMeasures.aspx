<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SafetyMeasures.aspx.cs" Inherits="FineUIPro.Web.BaseInfo.SafetyMeasures" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全措施</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全措施" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="SafetyMeasuresId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="SafetyMeasuresId" AllowSorting="true" SortField="LicenseType,SortIndex"
                SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                 OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true" ForceFit="true"
                OnRowDoubleClick="Grid1_RowDoubleClick" Width="980px" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                         <Items>
                              <f:DropDownList ID="drpLicenseType" runat="server" Label="作业类型" AutoPostBack="true"
                                  EnableEdit="true" OnSelectedIndexChanged="TextBox_TextChanged" Width="250px" LabelWidth="100px" LabelAlign="Right">
                             </f:DropDownList>
                             <f:TextBox runat="server" Label="安全措施" ID="txtSafetyMeasures" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="100px" LabelAlign="Right">
                            </f:TextBox>         
                             <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                Hidden="true">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RenderField Width="200px" ColumnID="LicenseTypeName" DataField="LicenseTypeName" SortField="LicenseTypeName"
                        FieldType="String" HeaderText="作业类型" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>                  
                    <f:RenderField Width="200px" ColumnID="SortIndex" DataField="SortIndex" SortField="SortIndex"
                        FieldType="String" HeaderText="编号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="400px" ColumnID="SafetyMeasures" DataField="SafetyMeasures" SortField="SafetyMeasures"
                        FieldType="String" HeaderText="名称" HeaderTextAlign="Center" TextAlign="Left">
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
    <f:Window ID="Window1" Title="安全措施" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="800px"
        Height="380px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑" Icon="Pencil">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" ConfirmText="删除选中行？" ConfirmTarget="Top" runat="server" Text="删除"
            Icon="Delete">
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

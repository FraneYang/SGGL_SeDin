<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmergencyType.aspx.cs" Inherits="FineUIPro.Web.BaseInfo.EmergencyType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>应急预案类型</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />   
</head>
<body>
    <form id="form1" runat="server">
   <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" Title="应急预案类型"  ShowHeader="false"
        Layout="HBox">
        <Items>
            <f:Grid ID="Grid1" Title="应急预案类型" ShowHeader="false" EnableCollapse="true" PageSize="10" ForceFit="true"
                ShowBorder="true" AllowPaging="true" IsDatabasePaging="true" runat="server" Width="750px" EnableColumnLines="true"
                DataKeyNames="EmergencyTypeId" DataIDField="EmergencyTypeId" OnPageIndexChange="Grid1_PageIndexChange" AllowFilters="true" OnFilterChange="Grid1_FilterChange" EnableTextSelection="True">
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                    <f:RenderField Width="100px" ColumnID="EmergencyTypeCode" DataField="EmergencyTypeCode" FieldType="String"
                        HeaderText="编号" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="400px" ColumnID="EmergencyTypeName" DataField="EmergencyTypeName" FieldType="String"
                        HeaderText="名称" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="Remark" DataField="Remark" FieldType="String"
                        HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                    </f:RenderField>
                </Columns>
                <Listeners>
                    <f:Listener Event="rowselect" Handler="onGridRowSelect" />
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
            <f:SimpleForm ID="SimpleForm1" runat="server" ShowBorder="true" ShowHeader="false"
                LabelWidth="80px" BodyPadding="5px" Width="350px">
                <Items>
                    <f:HiddenField ID="hfFormID" runat="server">
                    </f:HiddenField>
                    <f:TextBox ID="txtEmergencyTypeCode" Label="编号" ShowRedStar="true" Required="true" runat="server"  MaxLength="50"
                        LabelAlign="right" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                    </f:TextBox>
                    <f:TextBox ID="txtEmergencyTypeName" Label="名称" ShowRedStar="true" Required="true" runat="server"  MaxLength="100"
                        LabelAlign="right" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                    </f:TextBox>
                    <f:TextArea ID="txtRemark" runat="server" Label="备注" LabelAlign="right"  MaxLength="1000">
                    </f:TextArea>
                </Items>
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Bottom" runat="server">
                        <Items>
                            <f:Button ID="btnNew"  Icon="Add" ToolTip="新增" EnablePostBack="false" runat="server" Hidden="true">
                                <Listeners>
                                    <f:Listener Event="click" Handler="onNewButtonClick" />
                                </Listeners>
                            </f:Button>
                            <f:Button ID="btnDelete" Enabled="false" ToolTip="删除" Icon="Delete" ConfirmText="确定删除当前数据？" Hidden="true"
                                OnClick="btnDelete_Click" runat="server">
                            </f:Button>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1" Hidden="true"
                                OnClick="btnSave_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
            </f:SimpleForm>
        </Items>
    </f:Panel>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true" Hidden="true"
            runat="server" Text="编辑" Icon="Pencil">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true" Hidden="true"
            ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除" Icon="Delete">
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

        var gridClientID = '<%= Grid1.ClientID %>';
        var btnDeleteClientID = '<%= btnDelete.ClientID %>';
        var btnSaveClientID = '<%= btnSave.ClientID %>';

        var formClientID = '<%= SimpleForm1.ClientID %>';
        var hfFormIDClientID = '<%= hfFormID.ClientID %>';
        var txtCodeClientID = '<%= txtEmergencyTypeCode.ClientID %>';
        var txtNameClientID = '<%= txtEmergencyTypeName.ClientID %>';
        var txtRemarkClientID = '<%=txtRemark.ClientID %>';

        function onGridRowSelect(event, rowId) {
            var grid = F(gridClientID);

            // 启用删除按钮
            F(btnDeleteClientID).enable();

            // 当前行数据
            var rowValue = grid.getRowValue(rowId);

            // 使用当前行数据填充表单字段
            F(hfFormIDClientID).setValue(rowId);
            F(txtCodeClientID).setValue(rowValue['EmergencyTypeCode']);
            F(txtNameClientID).setValue(rowValue['EmergencyTypeName']);
            F(txtRemarkClientID).setValue(rowValue['Remark']);

            // 更新保存按钮文本
//            F(btnSaveClientID).setText('保存数据（编辑）');
        }

        function onNewButtonClick() {
            // 重置表单字段
            F(formClientID).reset();
            F(hfFormIDClientID).reset();
            // 清空表格选中行
            F(gridClientID).clearSelections();
            // 禁用删除按钮
            F(btnDeleteClientID).disable();

            // 更新保存按钮文本
//            F(btnSaveClientID).setText('保存数据（新增）');
        }
    </script>
</body>
</html>

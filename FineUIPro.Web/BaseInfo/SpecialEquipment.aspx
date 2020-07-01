<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpecialEquipment.aspx.cs" Inherits="FineUIPro.Web.BaseInfo.SpecialEquipment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>机具设备</title>
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
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" Title="机具设备" Layout="HBox"  ShowHeader="false">
        <Items>
            <f:Grid ID="Grid1" Title="机具设备" ShowHeader="false" EnableCollapse="true" PageSize="10"  EnableColumnLines="true"
                ShowBorder="true" AllowPaging="true" IsDatabasePaging="true" runat="server" Width="750px"
                DataKeyNames="SpecialEquipmentId" DataIDField="SpecialEquipmentId" OnPageIndexChange="Grid1_PageIndexChange"
                AllowFilters="true" OnFilterChange="Grid1_FilterChange" EnableTextSelection="True" ForceFit="true"
                SortField="TypeName,SpecialEquipmentCode"
               SortDirection="DESC" >
                <Columns>
                   <%--<f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>--%>
                    <f:RenderField Width="80px" ColumnID="SpecialEquipmentCode" DataField="SpecialEquipmentCode" FieldType="String"
                        HeaderText="编号" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                      <f:RenderField Width="150px" ColumnID="TypeName" DataField="TypeName" FieldType="String"
                        HeaderText="类型" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="SpecialEquipmentName" DataField="SpecialEquipmentName" FieldType="String"
                        HeaderText="设备名称" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="IsSpecial" DataField="IsSpecial" FieldType="Boolean"
                        RendererFunction="renderIsSpecial" HeaderText="特种设备"  HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="Remark" DataField="Remark" FieldType="String"
                        HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                    <f:RenderField Width="1px" ColumnID="SpecialEquipmentType" DataField="SpecialEquipmentType" FieldType="String"
                        HeaderText="类型ID" HeaderTextAlign="Center" TextAlign="Left" Hidden="true">
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
                    <f:TextBox ID="txtSpecialEquipmentCode" Label="编号" ShowRedStar="true" Required="true" runat="server"
                        LabelAlign="right" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelWidth="80px">
                    </f:TextBox>
                     <f:DropDownList ID="drpType" Label="类型"  runat="server" LabelAlign="right"  LabelWidth="80px">                         
                    </f:DropDownList>
                    <f:TextBox ID="txtSpecialEquipmentName" Label="名称" ShowRedStar="true" Required="true" runat="server"
                        LabelAlign="right" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelWidth="80px">
                    </f:TextBox>
                    <f:CheckBox ID="ckbIsSpecial" runat="server" Label="特种设备" LabelAlign="right" LabelWidth="80px">
                    </f:CheckBox>
                    <f:TextArea ID="txtRemark" runat="server" Label="备注" LabelAlign="right" LabelWidth="80px" Height="200px">
                    </f:TextArea>
                </Items>
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Bottom" runat="server">
                        <Items>
                            <f:Button ID="btnNew" Text="" Icon="Add" ToolTip="新增" EnablePostBack="false" runat="server">
                                <Listeners>
                                    <f:Listener Event="click" Handler="onNewButtonClick" />
                                </Listeners>
                            </f:Button>
                            <f:Button ID="btnDelete" Enabled="false" Text="删除" Icon="Delete" ConfirmText="确定删除当前数据？"
                                OnClick="btnDelete_Click" runat="server">
                            </f:Button>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnSave" Icon="SystemSave" runat="server" Text="保存数据" ValidateForms="SimpleForm1"
                                OnClick="btnSave_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
            </f:SimpleForm>
        </Items>
    </f:Panel>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            runat="server" Text="编辑" Icon="Pencil">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除" Icon="Delete">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/javascript">
        function renderIsSpecial(value) {
            return value == true ? '是' : '否';
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


        var gridClientID = '<%= Grid1.ClientID %>';
        var btnDeleteClientID = '<%= btnDelete.ClientID %>';
        var btnSaveClientID = '<%= btnSave.ClientID %>';
        var formClientID = '<%= SimpleForm1.ClientID %>';
        var hfFormIDClientID = '<%= hfFormID.ClientID %>';
        var txtCodeClientID = '<%= txtSpecialEquipmentCode.ClientID %>';
        var txtNameClientID = '<%= txtSpecialEquipmentName.ClientID %>';
        var ckbIsSpecialClientID = '<%= ckbIsSpecial.ClientID %>';
        var txtRemarkClientID = '<%=txtRemark.ClientID %>';
       var drpTypeClientID = '<%=drpType.ClientID %>';

        function onGridRowSelect(event, rowId) {
            var grid = F(gridClientID);
            // 启用删除按钮
            F(btnDeleteClientID).enable();
            // 当前行数据
            var rowValue = grid.getRowValue(rowId);
            // 使用当前行数据填充表单字段
            F(hfFormIDClientID).setValue(rowId);
            F(txtCodeClientID).setValue(rowValue['SpecialEquipmentCode']);
            F(txtNameClientID).setValue(rowValue['SpecialEquipmentName']);
            F(ckbIsSpecialClientID).setValue(rowValue['IsSpecial']);
            F(txtRemarkClientID).setValue(rowValue['Remark']);
            F(drpTypeClientID).setValue(rowValue['SpecialEquipmentType']);
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
            F(ckbIsSpecialClientID).setValue(true);
        }
    </script>
</body>
</html>

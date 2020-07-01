<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSSEStandardListType.aspx.cs"
    Inherits="FineUIPro.Web.BaseInfo.HSSEStandardListType" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>标准规范分类</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .maingrid
        {
            border-width: 0 1px 0 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" Title="标准规范分类"  ShowHeader="false"
        Layout="HBox">
        <Items>
            <f:Grid ID="Grid1" Title="标准规范分类" ShowHeader="false" EnableCollapse="true" PageSize="10"
                ShowBorder="true" AllowPaging="true" IsDatabasePaging="true" runat="server" Width="750px"
                DataKeyNames="TypeId" DataIDField="TypeId" OnPageIndexChange="Grid1_PageIndexChange" ForceFit="true"
                AllowFilters="true" OnFilterChange="Grid1_FilterChange" EnableTextSelection="True"  EnableColumnLines="true">
                <Columns>
                    <f:TemplateField Width="50px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="100px" ColumnID="TypeCode" DataField="TypeCode" FieldType="String"
                        HeaderText="编号" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="300px" ColumnID="TypeName" DataField="TypeName" FieldType="String"
                        HeaderText="名称" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField ColumnID="Remark" DataField="Remark" FieldType="String" Width="300px"
                        HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left" >
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
                LabelWidth="80px" BodyPadding="5px" Width="360px">
                <Items>
                    <f:HiddenField ID="hfFormID" runat="server">
                    </f:HiddenField>
                    <f:TextBox ID="tbxTypeCode" Label="编号" ShowRedStar="true"  LabelWidth="80px"
                        Required="true" runat="server"  MaxLength="50" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelAlign="right">
                    </f:TextBox>
                    <f:TextBox ID="tbxTypeName" Label="名称" ShowRedStar="true"  LabelWidth="80px"
                        Required="true" runat="server"  MaxLength="50" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelAlign="right">
                    </f:TextBox>
                    <f:TextArea ID="tbxRemark" runat="server" Label="备注" LabelWidth="80px"
                        MaxLength="500" LabelAlign="right">
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
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            runat="server" Text="编辑" Icon="Pencil">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
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
        var tbxTypeCodeClientID = '<%= tbxTypeCode.ClientID %>';
        var tbxTypeNameClientID = '<%= tbxTypeName.ClientID %>';
        var tbxRemarkClientID = '<%= tbxRemark.ClientID %>';

        function onGridRowSelect(event, rowId) {
            var grid = F(gridClientID);

            // 启用删除按钮
            F(btnDeleteClientID).enable();

            // 当前行数据
            var rowValue = grid.getRowValue(rowId);

            // 使用当前行数据填充表单字段
            F(hfFormIDClientID).setValue(rowId);
            F(tbxTypeCodeClientID).setValue(rowValue['TypeCode']);
            F(tbxTypeNameClientID).setValue(rowValue['TypeName']);
            F(tbxRemarkClientID).setValue(rowValue['Remark']);

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

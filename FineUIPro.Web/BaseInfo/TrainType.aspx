<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainType.aspx.cs" Inherits="FineUIPro.Web.BaseInfo.TrainType" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>培训类型</title>
     <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" Title="培训类型" Layout="HBox"  ShowHeader="false">
        <Items>
            <f:Grid ID="Grid1" Title="培训类型" ShowHeader="false" EnableCollapse="true" PageSize="10" EnableColumnLines="true"
                ShowBorder="true" AllowPaging="true" IsDatabasePaging="true" runat="server" Width="750px" ForceFit="true"
                DataKeyNames="TrainTypeId" DataIDField="TrainTypeId" OnPageIndexChange="Grid1_PageIndexChange"
                AllowFilters="true" OnFilterChange="Grid1_FilterChange" EnableTextSelection="True">
                <Columns>
                   <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                    <f:RenderField Width="100px" ColumnID="TrainTypeCode" DataField="TrainTypeCode" FieldType="String"
                        HeaderText="编号" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="250px" ColumnID="TrainTypeName" DataField="TrainTypeName" FieldType="String"
                        HeaderText="名称" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="IsAboutSendCard" DataField="IsAboutSendCard" FieldType="Boolean"
                        RendererFunction="renderIsOrNo" HeaderText="关联发卡"  HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="IsRepeat" DataField="IsRepeat" FieldType="Boolean"
                        RendererFunction="renderIsOrNo" HeaderText="多次培训"  HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="Remark" DataField="Remark" FieldType="String"
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
                LabelWidth="80px" BodyPadding="5px" Width="350px">
                <Items>
                    <f:HiddenField ID="hfFormID" runat="server">
                    </f:HiddenField>
                    <f:TextBox ID="txtTrainTypeCode" Label="编号" ShowRedStar="true" Required="true" runat="server"
                        LabelAlign="right" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelWidth="100px">
                    </f:TextBox>
                    <f:TextBox ID="txtTrainTypeName" Label="名称" ShowRedStar="true" Required="true" runat="server"
                        LabelAlign="right" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelWidth="100px">
                    </f:TextBox>
                    <f:CheckBox ID="ckbIsAboutSendCard" runat="server" Label="关联发卡" LabelAlign="right" LabelWidth="100px">
                    </f:CheckBox>
                     <f:CheckBox ID="ckbIsRepeat" runat="server" Label="多次培训" LabelAlign="right" LabelWidth="100px">
                    </f:CheckBox>
                    <f:TextArea ID="txtRemark" runat="server" Label="备注" LabelAlign="right" LabelWidth="100px">
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
                            <f:Button ID="btnDelete" Enabled="false" ToolTip="删除" Icon="Delete" ConfirmText="确定删除当前数据？"
                                OnClick="btnDelete_Click" runat="server">
                            </f:Button>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                                OnClick="btnSave_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
            </f:SimpleForm>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="题型设置" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true"
        Width="1000px" Height="600px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            runat="server" Text="编辑" Icon="Pencil">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除" Icon="Delete">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuSet" OnClick="btnMenuSet_Click" EnablePostBack="true" Hidden="true"
            runat="server" Text="题型设置" Icon="CalendarAdd">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/javascript">
        function renderIsOrNo(value) {
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
        var txtCodeClientID = '<%= txtTrainTypeCode.ClientID %>';
        var txtNameClientID = '<%= txtTrainTypeName.ClientID %>';
        var ckbIsAboutSendCardClientID = '<%= ckbIsAboutSendCard.ClientID %>';
        var ckbIsRepeatClientID = '<%= ckbIsRepeat.ClientID %>';
        var txtRemarkClientID = '<%=txtRemark.ClientID %>';

        function onGridRowSelect(event, rowId) {
            var grid = F(gridClientID);

            // 启用删除按钮
            F(btnDeleteClientID).enable();

            // 当前行数据
            var rowValue = grid.getRowValue(rowId);

            // 使用当前行数据填充表单字段
            F(hfFormIDClientID).setValue(rowId);
            F(txtCodeClientID).setValue(rowValue['TrainTypeCode']);
            F(txtNameClientID).setValue(rowValue['TrainTypeName']);
            F(ckbIsAboutSendCardClientID).setValue(rowValue['IsAboutSendCard']);
            F(ckbIsRepeatClientID).setValue(rowValue['IsRepeat']);
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

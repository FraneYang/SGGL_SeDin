﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotProessTrustEdit.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.HotProcessHard.HotProessTrustEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑热处理委托及数据录入</title>
    <base target="_self" />
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="热处理委托"
                TitleToolTip="热处理委托" AutoScroll="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:HiddenField runat="server" ID="hdItemsString">
                            </f:HiddenField>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnSave" Text="保存" ToolTip="保存"
                                Icon="SystemSave" ValidateForms="SimpleForm1" runat="server" OnClick="btnSave_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                    <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtHotProessTrustNo" Label="委托单号"
                                        runat="server" LabelAlign="Right" Required="true" ShowRedStar="true" FocusOnPageLoad="true"
                                        LabelWidth="180px">
                                    </f:TextBox>
                                    <f:DatePicker ID="txtProessDate" runat="server" Label="热处理日期"
                                        LabelAlign="Right" Required="true" ShowRedStar="true" LabelWidth="140px">
                                    </f:DatePicker>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:DropDownList ID="drpUnitWork" Label="单位工程名称"
                                        runat="server" ShowRedStar="true" Required="true" Readonly="true" LabelAlign="Right"
                                        LabelWidth="180px">
                                    </f:DropDownList>
                                    <f:DropDownList ID="drpUnitId" Label="单位名称" runat="server"
                                        ShowRedStar="true" Required="true"  Readonly="true" LabelAlign="Right" LabelWidth="140px">
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtProessMethod" Label="热处理方法" runat="server"
                                        LabelAlign="Right" MaxLength="50" LabelWidth="180px">
                                    </f:TextBox>
                                    <f:TextBox ID="txtProessEquipment" Label="热处理设备" runat="server"
                                        LabelAlign="Right" MaxLength="50" LabelWidth="140px">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtTabler" Label="制表人" runat="server" LabelAlign="Right"
                                        MaxLength="50" LabelWidth="180px">
                                    </f:TextBox>
                                    <f:TextBox ID="txtRemark" Label="备注" runat="server" LabelAlign="Right"
                                        MaxLength="100" LabelWidth="140px">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="热处理委托"
                        EnableCollapse="true" runat="server" BoxFlex="1" DataKeyNames="WeldJointId,HotProessTrustItemId"
                        EnableColumnLines="true" AllowCellEditing="true" ClicksToEdit="1" DataIDField="HotProessTrustItemId"
                        AllowSorting="true" SortField="PipelineCode,WeldJointCode" SortDirection="ASC"
                        AllowPaging="false" IsDatabasePaging="true" PageSize="10000" EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                    <f:Button ID="btnDelete" Text="删除行" ToolTip="删除数据"
                                        ConfirmText="删除选中行" ConfirmTarget="Top" Icon="Delete"
                                        runat="server" OnClick="btnMenuDelete_Click">
                                    </f:Button>
                                    <f:Button runat="server" ID="ckSelect" Text="查找" Icon="Find"
                                        OnClick="ckSelect_Click">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号"
                                Width="90px" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:RenderField HeaderText="管线号" ColumnID="PipelineCode"
                                DataField="PipelineCode" SortField="PipelineCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="220px">
                            </f:RenderField>
                            <f:RenderField HeaderText="总焊口量" ColumnID="WeldJointCode"
                                DataField="WeldJointCode" SortField="WeldJointCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="160px">
                            </f:RenderField>
                            <f:RenderField HeaderText="规格" ColumnID="Specification"
                                DataField="Specification" SortField="Specification" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="160px">
                            </f:RenderField>
                            <f:RenderField HeaderText="材质代号" ColumnID="MaterialCode"
                                DataField="MaterialCode" SortField="MaterialCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="220px">
                            </f:RenderField>
                            <f:RenderField HeaderText="WeldJointId" ColumnID="WeldJointId" DataField="WeldJointId"
                                FieldType="String" Hidden="true">
                            </f:RenderField>
                            <f:RenderField HeaderText="HotProessTrustItemId" ColumnID="HotProessTrustItemId"
                                DataField="HotProessTrustItemId" FieldType="String" Hidden="true">
                            </f:RenderField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                            <f:Listener Event="dataload" Handler="onGridDataLoad" />
                        </Listeners>
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true"
        EnableMaximize="true" Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close"
        IsModal="true" Width="1200px" Height="560px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Icon="Delete" ConfirmText="删除选中行" ConfirmTarget="Top"
            runat="server" Text="删除">
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
        function onGridDataLoad(event) {
            this.mergeColumns(['PipelineCode']);
            this.mergeColumns(['WeldJointCode']);
        }
    </script>
</body>
</html>
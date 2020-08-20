<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HardTrustEdit.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.HotProcessHard.HardTrustEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑硬度委托</title>
    <style type="text/css">
        .customlabel span
        {
            color: red;
            font-weight: bold;
        }
        
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="硬度委托"
                TitleToolTip="硬度委托" AutoScroll="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:HiddenField runat="server" ID="hdItemsString">
                            </f:HiddenField>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnSave" ToolTip="保存" Icon="SystemSave" runat="server"
                                OnClick="btnSave_Click">
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
                                    <f:TextBox ID="txtHardTrustNo" Label="委托单号" ShowRedStar="true"
                                        Required="true" runat="server" LabelWidth="190px" FocusOnPageLoad="true">
                                    </f:TextBox>
                                    <f:DropDownList ID="drpHardTrustUnit" Label="委托单位" runat="server"
                                        ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="190px">
                                    </f:DropDownList>
                                    <f:DropDownList ID="drpUnitWork" runat="server" Label="单位工程名称"
                                        LabelAlign="Right"  LabelWidth="190px" EnableEdit="true" ShowRedStar="true" Required="true">
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:DropDownList ID="drpCheckUnit" Label="检测单位" runat="server"
                                        ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="190px">
                                    </f:DropDownList>
                                    <f:DropDownList ID="drpHardTrustMan" Label="委托人" runat="server"
                                        ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="190px">
                                    </f:DropDownList>
                                    <f:DatePicker ID="txtHardTrustDate" Label="委托日期" runat="server"
                                        ShowRedStar="true" Required="true" LabelWidth="190px" DateFormatString="yyyy-MM-dd">
                                    </f:DatePicker>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtHardnessMethod" Label="检测方法" runat="server"
                                        LabelWidth="190px">
                                    </f:TextBox>
                                    <f:TextBox ID="txtHardnessRate" Label="检测比例" runat="server"
                                        LabelWidth="190px">
                                    </f:TextBox>
                                    <f:TextBox ID="txtStandards" Label="执行标准" runat="server"
                                        LabelWidth="190px">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtInspectionNum" Label="报检/检查记录编号" runat="server"
                                        LabelWidth="190px">
                                    </f:TextBox>
                                    <f:TextBox ID="txtCheckNum" Label="外观检查合格焊口数" runat="server"
                                        LabelWidth="190px">
                                    </f:TextBox>
                                    <f:TextBox ID="txtTestWeldNum" Label="委托检测焊口数" runat="server"
                                        LabelWidth="190px">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="67% 33%">
                                <Items>
                                    <f:RadioButtonList runat="server" ID="rblDetectionTime" Label="检测时机"
                                        LabelWidth="190px">
                                        <f:RadioItem Value="0" Text="工厂化预制焊口" Selected="true" />
                                        <f:RadioItem Value="1" Text="安装施工焊口" />
                                    </f:RadioButtonList>
                                    <f:TextBox ID="txtSendee" Label="接收人" runat="server"
                                        LabelWidth="190px">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="硬度委托"
                        EnableCollapse="true" runat="server" BoxFlex="1" DataKeyNames="WeldJointId,HotProessTrustItemId"
                        EnableColumnLines="true" AllowCellEditing="true" ClicksToEdit="1" DataIDField="WeldJointId"
                        AllowSorting="true" SortField="PipelineCode,WeldJointCode" SortDirection="ASC"
                        OnSort="Grid1_Sort" AllowPaging="false" IsDatabasePaging="true" PageSize="10000"
                        EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                    <f:Button runat="server" ID="ckSelect" Icon="Find" OnClick="ckSelect_Click" ToolTip="查找">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号"
                                Width="50px" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:RenderField HeaderText="管线号" ColumnID="PipelineCode"
                                DataField="PipelineCode" SortField="PipelineCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="180px">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊口号" ColumnID="WeldJointCode"
                                DataField="WeldJointCode" SortField="WeldJointCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="130px">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊工号" ColumnID="WelderCode"
                                DataField="WelderCode" SortField="WelderCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="130px">
                            </f:RenderField>
                            <f:RenderField HeaderText="规格" ColumnID="Specification"
                                DataField="Specification" SortField="Specification" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="130px">
                            </f:RenderField>
                            <f:RenderField HeaderText="材质" ColumnID="MaterialCode"
                                DataField="MaterialCode" SortField="MaterialCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="130px">
                            </f:RenderField>
                            <f:RenderField HeaderText="单线图号" ColumnID="SingleNumber"
                                DataField="SingleNumber" SortField="SingleNumber" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="130px">
                            </f:RenderField>
                            <f:RenderField HeaderText="备注" ColumnID="Remark" DataField="Remark"
                                SortField="Remark" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="130px" ExpandUnusedSpace="true">
                            </f:RenderField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                        </Listeners>
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true"
        EnableMaximize="true" Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close"
        IsModal="true" Width="1000px" Height="560px">
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
    </script>
</body>
</html>

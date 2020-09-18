<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeldReportEdit.aspx.cs"
    Inherits="FineUIPro.Web.WeldingProcess.WeldingManage.WeldReportEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑焊接日报</title>
    <style type="text/css">
        .customlabel span
        {
            color: red;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="焊接日报"
                TitleToolTip="焊接日报" AutoScroll="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:HiddenField runat="server" ID="hdItemsString">
                            </f:HiddenField>
                            <f:HiddenField runat="server" ID="hdTablerId">
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
                                    <f:TextBox ID="txtWeldingDailyCode" Label="焊接日报编号"
                                        ShowRedStar="true" Required="true" runat="server" LabelWidth="120px" LabelAlign="Right" FocusOnPageLoad="true">
                                    </f:TextBox>
                                    <f:DropDownList ID="drpUnit" Label="单位名称" runat="server" Readonly="true"
                                        ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="120px"  LabelAlign="Right">
                                    </f:DropDownList>
                                    <f:DropDownList ID="drpUnitWork" runat="server" Label="单位工程编号" Readonly="true"
                                        LabelAlign="Right" EnableEdit="true" ShowRedStar="true" Required="true" LabelWidth="140px">
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:DatePicker ID="txtWeldingDate" Label="焊接日期" runat="server"
                                        DateFormatString="yyyy-MM-dd" ShowRedStar="true" Required="true" LabelWidth="120px" 
                                        LabelAlign="Right" OnTextChanged="WeldingDateText_OnTextChanged" AutoPostBack="true">
                                    </f:DatePicker>
                                    <f:TextBox ID="txtTabler" Label="填报人" Readonly="true" runat="server"
                                        LabelWidth="120px" LabelAlign="Right">
                                    </f:TextBox>
                                    <f:DatePicker ID="txtTableDate" Label="填报日期" runat="server"
                                        ShowRedStar="true" Required="true" LabelWidth="140px" LabelAlign="Right" DateFormatString="yyyy-MM-dd">
                                    </f:DatePicker>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtRemark" Label="备注" runat="server" LabelWidth="120px">
                                    </f:TextBox>
                                    <f:Label ID="lbAmount" Label="超量焊工" runat="server" LabelWidth="120px"
                                        Hidden="true" CssClass="customlabel">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="焊接日报"
                        EnableCollapse="true" runat="server" BoxFlex="1" DataKeyNames="WeldTaskId" EnableColumnLines="true"
                        AllowCellEditing="true" ClicksToEdit="1" DataIDField="WeldTaskId" AllowSorting="true"
                        SortField="PipelineCode,WeldJointCode" SortDirection="ASC" OnSort="Grid1_Sort" EnableCheckBoxSelect="true"
                        AllowPaging="false" IsDatabasePaging="true" PageSize="10000" EnableTextSelection="True">
                       
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号"
                                Width="50px" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:RenderField HeaderText="管线号" ColumnID="PipelineCode"
                                DataField="PipelineCode" SortField="PipelineCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="180px" ExpandUnusedSpace="true">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊口号" ColumnID="WeldJointCode"
                                DataField="WeldJointCode" SortField="WeldJointCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="70px">
                            </f:RenderField>
                             <f:RenderField HeaderText="盖面焊工" ColumnID="CoverWelderCode"
                                    DataField="CoverWelderCode" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="110px">
                                </f:RenderField>
                                <f:RenderField HeaderText="打底焊工" ColumnID="BackingWelderCode"
                                    DataField="BackingWelderCode"  FieldType="String"
                                     HeaderTextAlign="Center" TextAlign="Left" Width="110px">
                                </f:RenderField>
                            <f:RenderField HeaderText="焊口属性" ColumnID="JointAttribute"
                                DataField="JointAttribute" SortField="JointAttribute" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="100px">
                            </f:RenderField>
                             <f:RenderField HeaderText="焊接类型" ColumnID="WeldTypeCode"
                                DataField="WeldTypeCode" SortField="WeldTypeCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="70px">
                            </f:RenderField>
                            <f:RenderField HeaderText="达因" ColumnID="Size"
                                DataField="Size" SortField="Size" FieldType="Double" HeaderTextAlign="Center" TextAlign="Left"
                                Width="70px">
                            </f:RenderField>
                            <f:RenderField HeaderText="外径" ColumnID="Dia"
                                DataField="Dia" SortField="Dia" FieldType="Double" HeaderTextAlign="Center" TextAlign="Left"
                                Width="70px">
                            </f:RenderField>
                            <f:RenderField HeaderText="壁厚" ColumnID="Thickness"
                                DataField="Thickness" SortField="Thickness" FieldType="Double" HeaderTextAlign="Center"
                                TextAlign="Left" Width="70px">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊接方法" ColumnID="WeldingMethodCode"
                                DataField="WeldingMethodCode" SortField="WeldingMethodCode" FieldType="String"
                                HeaderTextAlign="Center" TextAlign="Left" Width="120px">
                            </f:RenderField>
                             <f:RenderField HeaderText="焊丝" ColumnID="WeldingWireCode"
                                    DataField="WeldingWireCode"  FieldType="String"
                                    HeaderTextAlign="Center" TextAlign="Left" Width="120px" ExpandUnusedSpace="true">
                                </f:RenderField>
                                <f:RenderField HeaderText="焊条" ColumnID="WeldingRodCode"
                                    DataField="WeldingRodCode"  FieldType="String"
                                    HeaderTextAlign="Center" TextAlign="Left" Width="120px" ExpandUnusedSpace="true">
                                </f:RenderField>
                            <f:RenderField HeaderText="WeldJointId" ColumnID="WeldJointId" DataField="WeldJointId"
                                FieldType="String" Hidden="true">
                            </f:RenderField>
                             <f:RenderField HeaderText="盖面焊工ID" ColumnID="CoverWelderId" DataField="CoverWelderId"
                                FieldType="String" Hidden="true">
                            </f:RenderField>
                             <f:RenderField HeaderText="打底焊工ID" ColumnID="BackingWelderId" DataField="BackingWelderId"
                                FieldType="String" Hidden="true">
                            </f:RenderField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
   <%-- <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true"
        EnableMaximize="true" Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close"
        IsModal="true" Width="1160px" Height="560px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Top"
            runat="server" Text="删除">
        </f:MenuButton>
    </f:Menu>--%>
    </form>
    <script type="text/javascript">
       
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

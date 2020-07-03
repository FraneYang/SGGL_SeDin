﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPackageAudit.aspx.cs" Inherits="FineUIPro.Web.HJGL.TestPackage.TestPackageAudit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>试压包</title>
    <style type="text/css">
        .Cyan span {
           background-color: Cyan;
        }
        .Yellow span {
           background-color: Yellow;
        }
         .Green span {
           background-color: Green;
        }
         .Purple span {
           background-color: Purple;
        }
         .f-grid-row.Cyan 
        {
           background-color: Cyan;
        }
        .f-grid-row.Yellow 
        {
           background-color: Yellow;
        }
        .f-grid-row.Green 
        {
           background-color: Green;
        }
        .f-grid-row.Purple 
        {
           background-color: Purple;
        }
      </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true" 
                EnableCollapse="true" Width="230px" Title="试压包" ShowBorder="true" Layout="VBox"
                ShowHeader="true" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Toolbars>
                     <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items> 
                            <f:DatePicker ID="txtSearchDate" runat="server" EmptyText="输入查询条件" AutoPostBack="true" OnTextChanged="Tree_TextChanged" 
                               DateFormatString="yyyy-MM" Label="按月份" LabelWidth="70px">
                            </f:DatePicker>                                                                                                
                        </Items>
                    </f:Toolbar>
                 </Toolbars>
                <Items>
                   <f:Tree ID="tvControlItem" ShowHeader="false" Title="试压包节点树" OnNodeCommand="tvControlItem_NodeCommand"  
                        runat="server" ShowBorder="false" EnableCollapse="true" EnableSingleClickExpand="true" AutoLeafIdentification="true" 
                        EnableSingleExpand="true" EnableTextSelection="true">
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="试压包"
                TitleToolTip="试压包" AutoScroll="true">
                <Toolbars>
                     <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>   
                             <f:HiddenField runat="server" ID="hdPTP_ID"></f:HiddenField>                                
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"> </f:ToolbarFill>
                            <f:Button ID="btnAudit" Text="审核" ToolTip="审核检测单" AjaxLoadingType="Mask" ShowAjaxLoadingMaskText="true"
                                AjaxLoadingMaskText="试压包审核中，请稍候"
                                Icon="TableKey" runat="server" OnClick="btnAudit_Click" Hidden="true" ValidateForms="SimpleForm1">
                            </f:Button>
                             <f:Button ID="btnCancelAudit" Text="取消审核" ToolTip="取消审核试压包" Icon="TableGo" runat="server" OnClick="btnCancelAudit_Click"
                                ConfirmText="确认取消审核此试压包?" ConfirmTarget="Top" Hidden="true">
                            </f:Button>
                             <f:Button ID="btnPrint" Text="打印" Icon="Printer" runat="server"  OnClick="btnPrint_Click" Hidden="true">
                            </f:Button>
                        </Items>
                    </f:Toolbar>                                 
                 </Toolbars> 
                <Items>
                    <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="true" Title="试压包" AutoScroll="true" EnableCollapse="true" Collapsed="false"
                            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="txtTestPackageNo" Label="试压包号" runat="server" LabelWidth="130px">
                                    </f:Label> 
                                    <f:Label ID="drpUnitWork" Label="单位工程名称" runat="server" LabelWidth="130px">
                                    </f:Label>
                                    <f:Label ID="txtTestPackageName" Label="系统名称" runat="server" LabelWidth="130px">
                                    </f:Label>
                                    <f:Label ID="drpTestType" Label="试验类型" runat="server" LabelWidth="130px">
                                    </f:Label>
                                    <f:Label ID="txtTestService" Label="试验介质" runat="server" LabelWidth="130px">
                                    </f:Label>  
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="txtTestHeat" Label="试验压力 MPa(g)" runat="server" LabelWidth="130px">
                                    </f:Label> 
                                     <f:Label ID="txtTestAmbientTemp" Label="试验环境温度" runat="server" LabelWidth="130px">
                                    </f:Label>
                                    <f:Label ID="txtTestMediumTemp" Label="试验介质温度" runat="server" LabelWidth="130px">
                                     </f:Label>
                                     <f:Label ID="txtVacuumTestService" Label="真空试验介质" runat="server" LabelWidth="130px">
                                     </f:Label>
                                    <f:Label ID="txtVacuumTestPressure" Label="真空试验压力" runat="server" LabelWidth="130px">
                                     </f:Label>     
                                </Items>
                            </f:FormRow>
                          
                             <f:FormRow>
                                <Items> 
                                     <f:Label ID="txtTightnessTestTime" Label="严密性试验时间" runat="server" LabelWidth="130px">
                                    </f:Label>
                                     <f:Label ID="txtTightnessTestTemp" Label="严密性试验温度" runat="server" LabelWidth="130px">
                                     </f:Label> 
                                     <f:Label ID="txtTightnessTest" Label="严密性试验压力"  runat="server" LabelWidth="130px">
                                    </f:Label>  
                                    <f:Label ID="txtTestPressure" Label="耐压试验压力"  runat="server" LabelWidth="130px">
                                    </f:Label> 
                                      <f:Label ID="txtTestPressureTemp" Label="耐压试验温度"  runat="server" LabelWidth="130px">
                                    </f:Label>  
                                </Items>
                            </f:FormRow>
                            <f:FormRow> 
                                <Items>
                                    <f:Label ID="txtTestPressureTime" Label="耐压试验时间"  runat="server" LabelWidth="130px">
                                    </f:Label> 
                                    <f:Label ID="txtOperationMedium" Label="操作介质"  runat="server" LabelWidth="130px">
                                    </f:Label> 
                                     <f:Label ID="txtPurgingMedium" Label="吹扫介质"  runat="server" LabelWidth="130px">
                                    </f:Label> 
                                    <f:Label ID="txtCleaningMedium" Label="清扫介质"  runat="server" LabelWidth="130px">
                                    </f:Label>
                                    <f:Label ID="txtLeakageTestService" Label="泄露性试验介质" runat="server" LabelWidth="130px">
                                     </f:Label> 
                                </Items>
                            </f:FormRow>
                              <f:FormRow>
                                <Items>
                                    <f:Label ID="txtLeakageTestPressure" Label="泄露性试验压力" runat="server" LabelWidth="130px">
                                    </f:Label>
                                    <f:Label ID="txtAllowSeepage" Label="允许渗水量"  runat="server" LabelWidth="130px">
                                    </f:Label>  
                                    <f:Label ID="txtFactSeepage" Label="实际渗水量"  runat="server" LabelWidth="130px">
                                    </f:Label> 
                                    <f:Label ID="drpModifier" Label="修改人" runat="server" LabelWidth="130px">
                                     </f:Label>
                                    <f:Label ID="txtModifyDate" Label="修改日期" runat="server" LabelWidth="130px">
                                    </f:Label>     
                                </Items>
                            </f:FormRow>
                            <f:FormRow> 
                                <Items>                   
                                    
                                    <f:Label ID="drpTabler" Label="创建人"  runat="server" LabelWidth="130px">
                                    </f:Label> 
                                     <f:Label ID="txtTableDate" Label="创建日期"  runat="server" LabelWidth="130px">
                                    </f:Label>
                                    <f:Label ID="txtRemark" Label="备注"  runat="server" LabelWidth="130px">
                                    </f:Label>
                                   <f:DropDownList ID="drpAuditMan" Label="审核人" runat="server" ShowRedStar="true" Required="true"  EnableEdit="true" LabelWidth="130px">
                                    </f:DropDownList> 
                                    <f:DatePicker ID="txtAuditDate" Label="审核日期" runat="server" DateFormatString="yyyy-MM-dd"
                                         ShowRedStar="true" Required="true" LabelWidth="130px" >
                                     </f:DatePicker>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                 </Items>              
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="试压包明细" EnableCollapse="true" Collapsed="false"
                        runat="server" BoxFlex="1" DataKeyNames="PT_PipeId" AllowCellEditing="true" 
                        EnableColumnLines="true" ClicksToEdit="2" DataIDField="PT_PipeId" AllowSorting="true" 
                        SortField="UnitWorkCode,PipelineCode" SortDirection="ASC" OnSort="Grid1_Sort" EnableTextSelection="True" 
                        AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange">                              
                        <Toolbars>
                            <f:Toolbar ID="Toolbar3" Position="Bottom" runat="server">
                                <Items> 
                                    <f:Label  CssClass="Cyan" runat="server" ID="lbCyan" Text="&nbsp;&nbsp;&nbsp;&nbsp;" LabelAlign="Right"></f:Label>
                                    <f:Label runat="server" ID="lab1" Label="未焊完" LabelWidth="70px" Width="120px" LabelAlign="Right"></f:Label>                                                    
                                    <f:Label  CssClass="Yellow" runat="server" ID="Label1" Text="&nbsp;&nbsp;&nbsp;&nbsp;" LabelAlign="Right"></f:Label>
                                    <f:Label runat="server" ID="lab2" Label="已焊完，未达检测比例" LabelWidth="170px" Width="220px" LabelAlign="Right"></f:Label>                                                    
                                    <f:Label  CssClass="Green" runat="server" ID="Label2" Text="&nbsp;&nbsp;&nbsp;&nbsp;" LabelAlign="Right"></f:Label>
                                    <f:Label runat="server" ID="lab3" Label="已焊完，已达检测比例，但有不合格" LabelWidth="260px" Width="310px" LabelAlign="Right"></f:Label>                                                    
                                    <f:Label  CssClass="Purple" runat="server" ID="Label4" Text="&nbsp;&nbsp;&nbsp;&nbsp;" LabelAlign="Right"></f:Label>
                                    <f:Label runat="server" ID="lab4" Label="已通过"  LabelWidth="70px" Width="120px" LabelAlign="Right"></f:Label>                    
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>                    
                             <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>                            
                             <f:RenderField HeaderText="工作区" ColumnID="UnitWorkCode" DataField="UnitWorkCode" SortField="UnitWorkCode"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">                       
                            </f:RenderField> 
                            <f:WindowField ColumnID="PipelineCode"  HeaderText="管线编号" DataTextField="PipelineCode"  SortField="PipelineCode"                               
                                WindowID="Window3" DataIFrameUrlFields="PipelineId"  DataIFrameUrlFormatString="../WeldingManage/PipelineManageEdit.aspx?PipelineId={0}&type=view"
                                 Title="管线对应焊口详细信息" DataToolTipField="PipelineCode" HeaderTextAlign="Center" TextAlign="Left" Width="220px">
                            </f:WindowField> 
                            <f:RenderField HeaderText="总焊口" ColumnID="WeldJointCount" DataField="WeldJointCount" SortField="WeldJointCount"
                                 FieldType="Int" HeaderTextAlign="Center" TextAlign="Left" Width="70px">                       
                            </f:RenderField> 
                            <f:RenderField HeaderText="完成总焊口" ColumnID="WeldJointCountT" DataField="WeldJointCountT" SortField="WeldJointCountT"
                                 FieldType="Int" HeaderTextAlign="Center" TextAlign="Left" Width="90px">                       
                            </f:RenderField> 
                            <f:RenderField HeaderText="合格数" ColumnID="CountS" DataField="CountS" SortField="CountS"
                                 FieldType="Int" HeaderTextAlign="Center" TextAlign="Left" Width="70px">                       
                            </f:RenderField>
                            <f:RenderField HeaderText="不合格数" ColumnID="CountU" DataField="CountU" SortField="CountU"
                                 FieldType="Int" HeaderTextAlign="Center" TextAlign="Left" Width="80px">                       
                            </f:RenderField>
                             <f:RenderField HeaderText="应检测比例" ColumnID="NDTR_Name" DataField="NDTR_Name" SortField="NDTR_Name"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">                       
                            </f:RenderField>
                             <f:RenderField HeaderText="实际检测比例" ColumnID="Ratio" DataField="Ratio" SortField="Ratio"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="100px">                       
                            </f:RenderField>
                            <f:RenderField  HeaderText="应检测比例值" ColumnID="NDTR_Rate" DataField="NDTR_Rate" 
                                FieldType="String" Hidden="true"></f:RenderField> 
                            <f:RenderField  HeaderText="实际检测比例值" ColumnID="RatioC" DataField="RatioC" 
                                FieldType="String" Hidden="true"></f:RenderField> 
                        </Columns>
                        <Listeners>
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                        </Listeners>
                        <PageItems>
                            <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                            </f:ToolbarSeparator>
                            <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                            </f:ToolbarText>
                            <f:DropDownList runat="server" ID="ddlPageSize" Width="90px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                <f:ListItem Text="10" Value="10" />
                                <f:ListItem Text="15" Value="15" />
                                <f:ListItem Text="20" Value="20" />
                                <f:ListItem Text="25" Value="25" />
                            </f:DropDownList>
                        </PageItems>
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>   
    <f:Window ID="Window1" Title="试压包维护" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1000px" Height="660px">
   </f:Window>
   <f:Window ID="Window2" Title="试压包打印" Hidden="true" EnableIFrame="true"
        EnableMaximize="true" Target="Top" EnableResize="true" runat="server"
        IsModal="true" Width="1024px" Height="640px">
    </f:Window>
    <f:Window ID="Window3" Title="管线对应焊口详细" Hidden="true" EnableIFrame="true"
        EnableMaximize="true" Target="Top" EnableResize="true" runat="server"
        IsModal="true" Width="1000px" Height="520px">
    </f:Window>
   </form>
    <script type="text/javascript">
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
           // F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>

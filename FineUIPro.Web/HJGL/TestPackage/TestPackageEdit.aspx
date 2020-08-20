<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPackageEdit.aspx.cs" Inherits="FineUIPro.Web.HJGL.TestPackage.TestPackageEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>试压包</title>
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
                        EnableSingleExpand="true" EnableTextSelection="true" >
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="2px" IconFont="PlusCircle" Title="试压包"
                TitleToolTip="试压包" AutoScroll="true">
                <Toolbars>
                     <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>   
                             <f:HiddenField runat="server" ID="hdPTP_ID"></f:HiddenField>                                
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"> </f:ToolbarFill>
                            <f:Button ID="btnNew" Text="增加" ToolTip="增加试压包" Icon="Add" runat="server" OnClick="btnNew_Click">
                            </f:Button>
                            <f:Button ID="btnEdit" Text="编辑" ToolTip="修改试压包信息" Icon="TableEdit" runat="server" OnClick="btnEdit_Click" Hidden="true">
                            </f:Button>
                             <f:Button ID="btnDelete" Text="删除" ToolTip="删除试压包" ConfirmText="确认删除此试压包?" ConfirmTarget="Top" Hidden="true"
                                         Icon="Delete" runat="server" OnClick="btnDelete_Click">
                            </f:Button>
                            <f:Button ID="btnPrint" Text="打印" Icon="Printer" runat="server"  OnClick="btnPrint_Click" Hidden="true">
                            </f:Button>
                        </Items>
                    </f:Toolbar>                                 
                 </Toolbars> 
                <Items>
                    <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true"  
                            BodyPadding="2px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="txtTestPackageNo" Label="系统号" runat="server" LabelWidth="130px">
                                    </f:Label> 
                                    <f:Label ID="txtTestPackageName" Label="系统名称" runat="server" LabelWidth="130px">
                                    </f:Label>
                                    <f:Label ID="drpTabler" Label="创建人"  runat="server" LabelWidth="130px">
                                    </f:Label> 
                                    <f:Label ID="txtTableDate" Label="创建日期"  runat="server" LabelWidth="130px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="25% 25% 50%"> 
                                <Items>                   
                                    <f:Label ID="drpAuditer" Label="审核人"  runat="server" LabelWidth="130px">
                                    </f:Label> 
                                    <f:Label ID="txtAduditDate" Label="审核日期"  runat="server" LabelWidth="130px">
                                    </f:Label> 
                                    <f:Label ID="txtRemark" Label="备注"  runat="server" LabelWidth="130px">
                                    </f:Label>   
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>   
                <Items>
                    <f:Panel runat="server" ID="panel2" RegionPosition="Center" ShowBorder="true" Layout="VBox"
                        BodyPadding="2px" IconFont="PlusCircle" Title="试压前条件确认" 
                        ShowHeader="true" AutoScroll="true"  EnableCollapse="true" Collapsed="false" Height="200px">
                        <Items>
                            <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" Title="试压包" AutoScroll="true"
                                EnableCollapse="true" Collapsed="false" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
                                LabelAlign="Left">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtInstallationSpecification" runat="server" Label="1.管道安装符合设计文件和规范要求"
                                                LabelAlign="Right" LabelWidth="350px">
                                            </f:TextBox>
                                             <f:TextBox ID="txtPressureTest" runat="server" Label="2.管道耐压试验合格" LabelAlign="Right"
                                                LabelWidth="350px">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                             <f:TextBox ID="txtWorkRecord" runat="server" Label="3.焊接工作记录齐全" LabelAlign="Right"
                                                LabelWidth="350px">
                                            </f:TextBox>
                                             <f:TextBox ID="txtNDTConform" runat="server" Label="4.无损检测结果符合设计文件和规范要求" 
                                                LabelAlign="Right" LabelWidth="350px">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                  
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtHotConform" runat="server" Label="5.热处理结果符合设计文件和规范要求" 
                                                LabelAlign="Right" LabelWidth="350px">
                                            </f:TextBox>
                                             <f:TextBox ID="txtInstallationCorrectness" runat="server" Label="6.支、吊架安装正确" LabelAlign="Right"
                                                LabelWidth="350px">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtMarkClearly" runat="server" Label="7.合金钢管道材质标记清楚" 
                                                LabelAlign="Right" LabelWidth="350px">
                                            </f:TextBox>
                                            <f:TextBox ID="txtIsolationOpening" runat="server" Label="8.不参与管道系统试验的安全附件、仪表已按规定拆除或隔离，参与试压的系统内的阀门全部开启"
                                                LabelAlign="Right" LabelWidth="350px">
                                            </f:TextBox> 
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                             <f:TextBox ID="txtConstructionPlanAsk" runat="server" Label="9.临时加固措施、盲板位置与标识符合施工方案要求"
                                                LabelAlign="Right" LabelWidth="350px">
                                            </f:TextBox>
                                             <f:TextBox ID="txtCover" runat="server" Label="10.焊接接头及需要检验的部位未被覆盖" LabelAlign="Right"
                                                LabelWidth="350px">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                           <f:TextBox ID="txtMeetRequirements" runat="server" Label="11.试压用压力表量程、精度等级、检定状态符合规范要求"
                                                LabelAlign="Right" LabelWidth="350px">
                                            </f:TextBox> 
                                            <f:TextBox ID="txtStainlessTestWater" runat="server" Label="12.不锈钢管道试验用水符合规范要求" 
                                                LabelAlign="Right" LabelWidth="350px">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>
                        </Items>
                    </f:Panel>
                </Items>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="试压包明细"  EnableCollapse="true" Collapsed="false"
                        runat="server" BoxFlex="1" DataKeyNames="PT_PipeId" AllowCellEditing="true"  
                        EnableColumnLines="true" ClicksToEdit="2" DataIDField="PT_PipeId" AllowSorting="true" 
                        SortField="PipelineCode" SortDirection="ASC" OnSort="Grid1_Sort" EnableTextSelection="True"  
                        AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange">                              
                        <Columns>                    
                                                                                                           
                            <f:RenderField HeaderText="管线编号" ColumnID="PipelineCode" DataField="PipelineCode" SortField="PipelineCode"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left"  Width="160px" ExpandUnusedSpace="true">                       
                            </f:RenderField> 
                             <f:RenderField HeaderText="设计压力" ColumnID="TestPressure" DataField="TestPressure" SortField="TestPressure"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">                       
                            </f:RenderField> 
                            <f:RenderField HeaderText="设计温度" ColumnID="TestPressure" DataField="TestPressure" SortField="TestPressure"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">                       
                            </f:RenderField> 
                             <f:RenderField HeaderText="试验环境温度" ColumnID="TestPressure" DataField="TestPressure" SortField="TestPressure"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="110px">                       
                            </f:RenderField>
                            <f:RenderField HeaderText="试验介质" ColumnID="TestMedium" DataField="TestMedium" SortField="TestMedium"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">                       
                            </f:RenderField>  
                             <f:RenderField HeaderText="试验介质温度" ColumnID="TestPressure" DataField="TestPressure" SortField="TestPressure"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="110px">                       
                            </f:RenderField>
                            <f:RenderField HeaderText="试验压力" ColumnID="TestPressure" DataField="TestPressure" SortField="TestPressure"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">                       
                            </f:RenderField> 
                             <f:RenderField HeaderText="稳压时间min" ColumnID="TestPressure" DataField="TestPressure" SortField="TestPressure"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="100px">                       
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
        Width="1280px" Height="900px">
   </f:Window>
    <f:Window ID="Window2" Title="试压包打印" Hidden="true" EnableIFrame="true"
        EnableMaximize="true" Target="Top" EnableResize="true" runat="server"
        IsModal="true" Width="1024px" Height="640px">
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPackageItemEdit.aspx.cs" Inherits="FineUIPro.Web.HJGL.TestPackage.TestPackageItemEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>试压包</title>  
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="VBox">
        <Items>           
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="根据条件查找并勾选要进行试压的管线" >
                 <Items>
                    <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" Title="试压包" AutoScroll="true" EnableCollapse="true" Collapsed="false"
                            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">                      
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtTestPackageNo" Label="试压包号" ShowRedStar="true" Required="true" runat="server" FocusOnPageLoad="true" LabelWidth="120px">
                                    </f:TextBox>
                                    <f:DropDownList ID="drpUnit" Label="单位" runat="server" ShowRedStar="true" Required="true"  EnableEdit="true" LabelWidth="120px"
                                         AutoPostBack="true">
                                    </f:DropDownList> 
                                    <f:DropDownList ID="drpUnitWork" Label="单位工程名称" runat="server" ShowRedStar="true" Required="true"  EnableEdit="true" LabelWidth="120px" 
                                        AutoPostBack="true" OnSelectedIndexChanged="drpInstallation_OnSelectedIndexChanged">
                                    </f:DropDownList> 
                                    <f:TextBox ID="txtTestPackageName" Label="系统名称" runat="server" LabelWidth="120px">
                                    </f:TextBox>
                                    <f:DropDownList ID="drpTestType" Label="试验类型" runat="server" EnableEdit="true" LabelWidth="120px">
                                    </f:DropDownList>  
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>                                                                        
                                     <f:TextBox ID="txtTestService" Label="试验介质" runat="server" LabelWidth="120px">
                                    </f:TextBox>
                                    <f:TextBox ID="txtTestHeat" Label="试验压力" runat="server" LabelWidth="120px">
                                    </f:TextBox>
                                    <f:TextBox ID="txtTestAmbientTemp" Label="试验环境温度" runat="server" LabelWidth="120px">
                                    </f:TextBox>
                                    <f:TextBox ID="txtTestMediumTemp" Label="试验介质温度" runat="server" LabelWidth="120px">
                                     </f:TextBox>
                                     <f:TextBox ID="txtVacuumTestService" Label="真空试验介质" runat="server" LabelWidth="120px">
                                     </f:TextBox>     
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items> 
                                    <f:TextBox ID="txtVacuumTestPressure" Label="真空试验压力" runat="server" LabelWidth="120px">
                                     </f:TextBox>
                                    <f:TextBox ID="txtTightnessTestTime" Label="严密性试验时间" runat="server" LabelWidth="120px">
                                    </f:TextBox>                                     
                                     <f:TextBox ID="txtTightnessTestTemp" Label="严密性试验温度" runat="server" LabelWidth="120px">
                                     </f:TextBox> 
                                     <f:TextBox ID="txtTightnessTest" Label="严密性试验压力"  runat="server" LabelWidth="120px">
                                    </f:TextBox>
                                    <f:TextBox ID="txtTestPressure" Label="耐压试验压力"  runat="server" LabelWidth="120px">
                                    </f:TextBox> 
                                    
                                </Items>
                            </f:FormRow>                          
                            <f:FormRow> 
                                <Items>
                                    <f:TextBox ID="txtTestPressureTemp" Label="耐压试验温度"  runat="server" LabelWidth="120px">
                                    </f:TextBox> 
                                    <f:TextBox ID="txtTestPressureTime" Label="耐压试验时间"  runat="server" LabelWidth="120px">
                                    </f:TextBox> 
                                    <f:TextBox ID="txtOperationMedium" Label="操作介质"  runat="server" LabelWidth="120px">
                                    </f:TextBox> 
                                     <f:TextBox ID="txtPurgingMedium" Label="吹扫介质"  runat="server" LabelWidth="120px">
                                    </f:TextBox> 
                                    <f:TextBox ID="txtCleaningMedium" Label="清扫介质"  runat="server" LabelWidth="120px">
                                    </f:TextBox>  
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                     <f:TextBox ID="txtLeakageTestService" Label="泄露性试验介质" runat="server" LabelWidth="120px">
                                     </f:TextBox>
                                    <f:TextBox ID="txtLeakageTestPressure" Label="泄露性试验压力" runat="server" LabelWidth="120px">
                                    </f:TextBox>
                                    <f:TextBox ID="txtAllowSeepage" Label="允许渗水量"  runat="server" LabelWidth="120px">
                                    </f:TextBox> 
                                    <f:TextBox ID="txtFactSeepage" Label="实际渗水量"  runat="server" LabelWidth="120px">
                                    </f:TextBox>
                                    <f:TextBox ID="txtRemark" Label="备注"  runat="server" LabelWidth="120px">
                                    </f:TextBox>         
                                </Items>
                            </f:FormRow>
                            <f:FormRow> 
                                <Items>                 
                                    <f:DropDownList ID="drpModifier" Label="修改人" runat="server" EnableEdit="true" LabelWidth="120px">
                                    </f:DropDownList> 
                                     <f:DatePicker ID="txtModifyDate" Label="修改日期" runat="server" DateFormatString="yyyy-MM-dd" LabelWidth="120px">
                                    </f:DatePicker>
                                    <f:DropDownList ID="drpTabler" Label="建档人" runat="server" EnableEdit="true" LabelWidth="120px" ShowRedStar="true" Required="true" >
                                    </f:DropDownList>
                                    <f:DatePicker ID="txtTableDate" Label="建档日期" runat="server" DateFormatString="yyyy-MM-dd" LabelWidth="120px" ShowRedStar="true" Required="true" >
                                    </f:DatePicker>  
                                    <f:CheckBox runat="server" ID="ckSelect" Label="只显示选中项" LabelWidth="120px" AutoPostBack="true" 
                                            OnCheckedChanged="ckSelect_OnCheckedChanged"></f:CheckBox>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                 </Items>  
                </f:Panel>
                 <f:Panel runat="server" ID="panel3" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="true" BodyPadding="5px" IconFont="PlusCircle" Title="根据条件查找并勾选要进行试压的管线" >
                 <Toolbars>
                     <f:Toolbar ID="Toolbar1"  Position="Bottom" runat="server" ToolbarAlign="Left" >
                        <Items>                          
                                 <f:DropDownList ID="drpPipingClass" Label="管线等级" runat="server" LabelWidth="120px"  LabelAlign="Right"  Width="220px" EnableEdit="true">
                                 </f:DropDownList>
                                 <f:DropDownList ID="drpTestMedium" Label="试验介质" runat="server" LabelWidth="120px" Width="220px"  LabelAlign="Right" EnableEdit="true" >
                                 </f:DropDownList>
                                 <f:NumberBox ID="numTestPressure" runat="server" Label="试验压力" LabelWidth="120px" Width="200px"  LabelAlign="Right"></f:NumberBox>
                                 <f:NumberBox ID="numTo" runat="server" Label="至" LabelWidth="30px" Width="110px"></f:NumberBox>
                                  <f:Button ID="btnFind" Text="查询" ToolTip="查找符合条件的管线" ValidateForms="SimpleForm1" Icon="Find" runat="server" OnClick="btnFind_Click">
                                 </f:Button>
                                  <f:Button ID="btnSave" Text="保存" ToolTip="保存试压包信息" ValidateForms="SimpleForm1" Icon="SystemSave" runat="server" OnClick="btnSave_Click">
                                  </f:Button>                                                                                     
                        </Items>
                    </f:Toolbar>
                 </Toolbars>
                </f:Panel>
                <f:Panel runat="server" ID="panel2" RegionPosition="Center" ShowBorder="true"
                         ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" AutoScroll="true">           
                  <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="试压包明细" EnableCollapse="true" Collapsed="false"
                        runat="server" BoxFlex="1" DataKeyNames="PipelineId" AllowCellEditing="true" 
                        EnableColumnLines="true" ClicksToEdit="2" DataIDField="PipelineId" AllowSorting="true"
                        SortField="PipelineCode" SortDirection="ASC" OnSort="Grid1_Sort" 
                        AllowPaging="true" IsDatabasePaging="true" PageSize="100" Height="360px"
                        OnPageIndexChange="Grid1_PageIndexChange" EnableTextSelection="True" >                                                     
                        <Columns>    
                                         
                             <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="55px" HeaderTextAlign="Center" TextAlign="Center"/>
                            <f:CheckBoxField ColumnID="ckbIsSelected" Width="50px" RenderAsStaticField="false" HeaderText="选择"
                                AutoPostBack="true" CommandName="IsSelected"  HeaderTextAlign="Center"/>   
                             <f:RenderField HeaderText="工作区" ColumnID="UnitWorkCode" DataField="UnitWorkCode" SortField="UnitWorkCode"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">                       
                            </f:RenderField>                                                                                  
                            <f:RenderField HeaderText="管线编号" ColumnID="PipelineCode" DataField="PipelineCode" SortField="PipelineCode"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left"  Width="220px">                       
                            </f:RenderField> 
                            <f:RenderField HeaderText="管道等级" ColumnID="PipingClassCode" DataField="PipingClassCode" SortField="PipingClassCode"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left"  Width="100px">                       
                            </f:RenderField>  
                            <f:RenderField HeaderText="试验压力" ColumnID="TestPressure" DataField="TestPressure" SortField="TestPressure"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">                       
                            </f:RenderField> 
                            <f:RenderField HeaderText="试验介质" ColumnID="TestMediumName" DataField="TestMediumName" SortField="TestMediumName"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">                       
                            </f:RenderField> 
                            <f:RenderField HeaderText="介质" ColumnID="MediumName" DataField="MediumName" SortField="MediumName"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">                       
                            </f:RenderField>
                            <f:RenderField HeaderText="单线图号" ColumnID="SingleNumber" DataField="SingleNumber" SortField="SingleNumber"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="180px">                       
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
                                <f:ListItem Text="100" Value="100" />
                                <f:ListItem Text="150" Value="150" />
                                <f:ListItem Text="200" Value="200" />
                                <f:ListItem Text="250" Value="250" />
                            </f:DropDownList>
                        </PageItems>
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>   
    <f:Menu ID="Menu1" runat="server">           
         <f:MenuButton ID="btnAllSelect" OnClick="btnAllSelect_Click" EnablePostBack="true"
           runat="server" Text="全选" Icon="Accept">
        </f:MenuButton> 
         <f:MenuButton ID="btnNoSelect" OnClick="btnNoSelect_Click" EnablePostBack="true"
           runat="server" Text="全不选" Icon="Cancel">
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

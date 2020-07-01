<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainTypeEdit.aspx.cs" Inherits="FineUIPro.Web.BaseInfo.TrainTypeEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>培训类型</title>
   <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" Layout="VBox" 
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" EnableTableStyle="true">
        <Rows>           
            <f:FormRow runat="server" ID="fRow0">
                <Items>                   
                    <f:DropDownBox runat="server" ID="drpTestTraining"  Required="true" ShowRedStar="true"
                        EnableMultiSelect="false" Label="题目类型" LabelWidth="120px">
                        <PopPanel>
                             <f:Tree ID="tvTestTraining" EnableCollapse="true" ShowHeader="false" Title="考试试题库" 
                                 AutoScroll = "true" Hidden="true" Width="300px" 
                                AutoLeafIdentification="true" runat="server" EnableTextSelection="True" 
                                 ShowBorder = "true"  EnableSingleClickExpand = "true">                       
                            </f:Tree>
                        </PopPanel>
                    </f:DropDownBox>
                    <f:Label runat="server"></f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                     <f:NumberBox runat="server" ID="txtSCount" Label="单选题数量" LabelWidth="120px"
                        Required="true" ShowRedStar="true"  NoDecimal="true" NoNegative="true"></f:NumberBox>
                    <f:NumberBox runat="server" ID="txtMCount" Label="多选题数量" LabelWidth="120px"
                        Required="true" ShowRedStar="true"  NoDecimal="true" NoNegative="true"></f:NumberBox>
                    <f:NumberBox runat="server" ID="txtJCount" Label="判断题数量" LabelWidth="120px"
                        Required="true" ShowRedStar="true"  NoDecimal="true" NoNegative="true"></f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                     <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="培训题型" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="TrainTypeItemId" AllowCellEditing="true" EnableColumnLines="true"
                        ClicksToEdit="2" DataIDField="TrainTypeItemId" AllowSorting="true" SortField="TrainingCode"
                        SortDirection="ASC" AllowPaging="false" OnSort="Grid1_Sort" IsDatabasePaging="true" PageSize="1000" 
                        EnableRowDoubleClickEvent="true" Height="420px"
                        OnRowDoubleClick="Grid1_RowDoubleClick" Width="800px" EnableTextSelection="True">
                        <Toolbars>
                           <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>                                     
                                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                    </f:ToolbarFill>
                                    <f:Button ID="btnAdd" ToolTip="新增" Icon="Add" ValidateForms="SimpleForm1" runat="server" 
                                        OnClick="btnAdd_Click" >
                                    </f:Button>
                                    <f:Button ID="btnSave" ToolTip="保存" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                                        OnClick="btnSave_Click">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>      
                           <f:RenderField Width="150px" ColumnID="TrainingCode" DataField="TrainingCode" FieldType="String"
                                HeaderText="试题类型编号" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="TrainingName" DataField="TrainingName" FieldType="String"
                                HeaderText="考试题目类型" HeaderTextAlign="Center" TextAlign="Left"  ExpandUnusedSpace="true">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="SCount" DataField="SCount" FieldType="Int"
                                HeaderText="单选题数量" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField> 
                            <f:RenderField Width="150px" ColumnID="MCount" DataField="MCount" FieldType="Int"
                                HeaderText="多选题数量" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="JCount" DataField="JCount" FieldType="Int"
                                HeaderText="判断题数量" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                        </Columns> 
                    <Listeners>
                        <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                    </Listeners>
                  </f:Grid> 
                </Items>
            </f:FormRow>          
        </Rows>       
    </f:Form>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
             runat="server" Text="编辑" Icon="Pencil">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
             ConfirmText="删除选中行？" ConfirmTarget="Top" runat="server" Text="删除"
            Icon="Delete">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/jscript">
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
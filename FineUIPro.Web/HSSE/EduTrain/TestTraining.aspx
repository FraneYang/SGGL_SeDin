<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestTraining.aspx.cs" Inherits="FineUIPro.Web.HSSE.EduTrain.TestTraining" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="210px" Title="考试试题库" TitleToolTip="考试试题库" ShowBorder="true"
                ShowHeader="false" AutoScroll="true" BodyPadding="2px" IconFont="ArrowCircleLeft"
                Layout="Fit">
                <Items>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>
                           <%-- <f:Button ID="btnNew" Icon="Add" runat="server" OnClick="btnNew_Click" Hidden="true">
                            </f:Button>--%>
                            <f:Button ID="btnOut" ToolTip="导出" Icon="FolderUp" runat="server"
                                    OnClick="btnMenuOut_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                    <f:Tree ID="tvTestTraining" EnableCollapse="true" ShowHeader="true" Title="考试试题库" OnNodeCommand="tvTestTraining_NodeCommand"
                        AutoLeafIdentification="true" runat="server" EnableTextSelection="True">
                        <Listeners>
                            <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                        </Listeners>
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="考试试题明细"
                TitleToolTip="考试试题明细" AutoScroll="true">
                <Items>
                    <f:Grid ID="Grid1" Width="870px" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="TrainingItemId"  DataIDField="TrainingItemId" 
                        AllowSorting="true" SortField="TrainingItemCode" ForceFit="true"
                        SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                        PageSize="15" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                        OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True" EnableColumnLines="true">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar3" Position="Top" runat="server">
                                <Items>
                                    <f:TextBox ID="txtName" runat="server" Label="查询" EmptyText="输入查询条件"
                                        AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"
                                        LabelAlign="Right">
                                    </f:TextBox>
                                    <f:DropDownList runat="server" ID="drptype" AutoPostBack="true" Label="题型" LabelWidth="60px" OnSelectedIndexChanged="TextBox_TextChanged">
                                        <f:ListItem Value="0" Text="全部" Selected="true"/>
                                        <f:ListItem Value="1" Text="单选题"/>
                                        <f:ListItem Value="2" Text="多选题"/>
                                        <f:ListItem Value="3" Text="判断题"/>
                                    </f:DropDownList>
                                    <f:CheckBox runat="server" ID="ckIsItem" Text="选项不全" AutoPostBack="true" OnCheckedChanged="ckIsItem_CheckedChanged"></f:CheckBox>
                                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                    </f:ToolbarFill>
                                    <f:Button ID="btnNewDetail" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewDetail_Click"
                                        Hidden="true">
                                    </f:Button>                                    
                                    <f:Button ID="btnImport" ToolTip="导入" Icon="PageExcel" Hidden="true" runat="server"
                                            OnClick="btnImport_Click">
                                    </f:Button> 
                                    <f:Button ID="Button1" OnClick="btnMenuOut1_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                        EnableAjax="false" DisableControlBeforePostBack="false">
                                    </f:Button>
                                      <%-- <f:Button ID="btnRefresh" ToolTip="按基础数据刷新" Icon="ArrowRefresh" runat="server" OnClick="Refresh_Click"
                                        Hidden="true">
                                    </f:Button>
                                    <f:Button ID="btnRefresh1" ToolTip="按列表名称刷新" Icon="ArrowRefresh" runat="server" OnClick="Refresh1_Click"
                                        Hidden="true">
                                    </f:Button>--%>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>                         
                            <f:RenderField ColumnID="TrainingItemCode" DataField="TrainingItemCode" SortField="TrainingItemCode" 
                                Width="80px" FieldType="String" HeaderText="编号" HeaderTextAlign="Center" TextAlign="left">
                            </f:RenderField>
                            <f:RenderField ColumnID="Abstracts" DataField="Abstracts" SortField="Abstracts" 
                                Width="600px"  FieldType="String" HeaderText="教材内容" HeaderTextAlign="Center" TextAlign="left">
                            </f:RenderField>
                             <f:RenderField ColumnID="WorkPostNames" DataField="WorkPostNames" SortField="WorkPostNames" 
                                Width="200px"  FieldType="String" HeaderText="适合岗位" HeaderTextAlign="Center" TextAlign="left">
                            </f:RenderField> 
                             <f:RenderField ColumnID="TestTypeName" DataField="TestTypeName" SortField="TestTypeName" 
                                Width="70px"  FieldType="String" HeaderText="题型" HeaderTextAlign="Center" TextAlign="left">
                            </f:RenderField>  
                            <%-- <f:RenderField Width="200px" ColumnID="AItem" DataField="AItem" FieldType="String"
                                HeaderText="答案项A" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="BItem" DataField="BItem" FieldType="String"
                                HeaderText="答案项B" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>   
                             <f:RenderField Width="200px" ColumnID="CItem" DataField="CItem" FieldType="String"
                                HeaderText="答案项C" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>   
                             <f:RenderField Width="200px" ColumnID="DItem" DataField="DItem" FieldType="String"
                                HeaderText="答案项D" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>   
                             <f:RenderField Width="200px" ColumnID="EItem" DataField="EItem" FieldType="String"
                                HeaderText="答案项E" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>   
                             <f:RenderField Width="90px" ColumnID="AnswerItems" DataField="AnswerItems" FieldType="String"
                                HeaderText="正确答案项" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>  --%>
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
                            </f:DropDownList>
                        </PageItems>
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="考试试题" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="600px" Height="350px">
    </f:Window>
    <f:Window ID="Window2" Title="考试试题详情" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="true"
        Width="900px" Height="580px">
    </f:Window>   
     <f:Window ID="Window3" Title="导入信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window3_Close" IsModal="false"
        CloseAction="HidePostBack" Width="1200px" Height="560px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            runat="server" Text="编辑" Hidden="true" Icon="Pencil">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除"
            Hidden="true">
        </f:MenuButton>
    </f:Menu>
    <f:Menu ID="Menu2" runat="server">
         <f:MenuButton ID="btnMenuADD" OnClick="btnMenuADD_Click" EnablePostBack="true"
            runat="server" Text="新增" Icon="Add" Hidden="true">
        </f:MenuButton>
        <f:MenuButton ID="btnTreeMenuEdit" OnClick="btnTreeMenuEdit_Click" EnablePostBack="true"
            runat="server" Text="编辑" Hidden="true" Icon="TableEdit">
        </f:MenuButton>
        <f:MenuButton ID="btnTreeMenuDelete" OnClick="btnTreeMenuDelete_Click" EnablePostBack="true"
            Icon="Delete" ConfirmText="删除选中节点？" ConfirmTarget="Parent" runat="server" Text="删除"
            Hidden="true">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/javascript">
        var treeID = '<%= tvTestTraining.ClientID %>';
        var menuID = '<%= Menu1.ClientID %>';
        var menuID2 = '<%= Menu2.ClientID %>';
        // 保存当前菜单对应的树节点ID
        var currentNodeId;

        // 返回false，来阻止浏览器右键菜单
        function onTreeNodeContextMenu(event, nodeId) {
            currentNodeId = nodeId;
            F(menuID2).show();
            return false;
        }
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

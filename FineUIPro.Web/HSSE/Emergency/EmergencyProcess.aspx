<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmergencyProcess.aspx.cs" Inherits="FineUIPro.Web.HSSE.Emergency.EmergencyProcess" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>应急流程</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="应急流程" EnableCollapse="true"
                runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="EmergencyProcessId" ForceFit="true"
                DataIDField="EmergencyProcessId" AllowSorting="true" SortField="ProcessSteps" SortDirection="ASC"  
                AllowPaging="false" IsDatabasePaging="true"  PageSize="100" EnableRowDoubleClickEvent="true" 
                OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">            
                <Columns>         
                    <f:RenderField Width="60px" ColumnID="ProcessSteps" DataField="ProcessSteps"
                        FieldType="String" HeaderText="步骤" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="ProcessName" DataField="ProcessName" 
                         FieldType="String" HeaderText="名称" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>   
                       <f:RenderField Width="180px" ColumnID="StepOperator" DataField="StepOperator"
                        FieldType="String" HeaderText="操作人员" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>    
                     <f:RenderField Width="500px" ColumnID="Remark" DataField="Remark" ExpandUnusedSpace="true"
                            FieldType="String" HeaderText="内容" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                </Columns>
                <Listeners>
                    <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                </Listeners>            
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="应急流程" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="1000px" Height="450px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="TableEdit" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑">
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

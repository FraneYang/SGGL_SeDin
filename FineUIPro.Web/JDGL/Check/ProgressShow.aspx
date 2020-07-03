<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProgressShow.aspx.cs" Inherits="FineUIPro.Web.JDGL.Check.ProgressShow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>项目施工进度统计</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <style>
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }
        .LightGreen { 
          background-color:lightgreen;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server" />
        <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
            <Regions>
                <f:Region ID="Region1" ShowBorder="false" ShowHeader="false" RegionPosition="Left"
                    Title="施工进度统计" BodyPadding="0 5 0 0" Width="300px" Layout="Fit" runat="server"
                    EnableCollapse="true">
                    <Items>
                        <f:Panel runat="server" ID="panel2" RegionPosition="Left" RegionSplit="true" EnableCollapse="true"
                            Width="400" Title="施工进度统计" TitleToolTip="施工进度统计" ShowBorder="true" ShowHeader="true"
                            BodyPadding="5px" IconFont="ArrowCircleLeft">
                            <Items>
                                <f:Tree ID="trWBS" Width="290" Height="600px" EnableCollapse="true" ShowHeader="true"
                                    OnNodeCommand="trWBS_NodeCommand" OnNodeExpand="trWBS_NodeExpand" AutoLeafIdentification="true"
                                    runat="server">
                                    <Listeners>
                                        <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                                    </Listeners>
                                </f:Tree>
                                <f:HiddenField runat="server" ID="hdSelectId">
                                </f:HiddenField>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Region>
                <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox"
                    BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server">
                    <Items>
                        <f:Grid ID="Grid1"   Width="900px" ShowBorder="true" ShowHeader="true" EnableCollapse="false"  ForceFit="true"
                            runat="server" BoxFlex="1" DataKeyNames="ControlItemAndCycleId" SortField="ControlItemAndCycleCode"  
                            SortDirection="ASC" AllowCellEditing="true"  EnableColumnLines="true" 
                            ShowSelectedCell="true" DataIDField="Id" AllowPaging="true"  IsDatabasePaging="true" 
                            PageSize="200" OnPageIndexChange="Grid1_PageIndexChange" AllowFilters="true" OnFilterChange="Grid1_FilterChange" >  
                            <Columns>
                                <f:BoundField ColumnID="ControlItemContent"  MinWidth="150px" TextAlign="Center" HeaderTextAlign="Center" DataField="ControlItemContent" HeaderText="工作包" />
                                <f:BoundField ColumnID="ControlPoint" MinWidth="100px" DataField="ControlPoint" HeaderText="控制点等级" TextAlign="Center" HeaderTextAlign="Center"/>
                                <f:BoundField ColumnID="Weights" MinWidth="80px"  DataField="Weights" HeaderText="权重%" TextAlign="Center" HeaderTextAlign="Center"/>
                                  <f:BoundField ColumnID="CheckNum" MinWidth="100px" DataField="CheckNum" HeaderText="检查次数" TextAlign="Center" HeaderTextAlign="Center" />
                                  <f:BoundField ColumnID="PlanCompleteDate" MinWidth="120px" DataField="PlanCompleteDate" HeaderText="计划完成时间" TextAlign="Center" HeaderTextAlign="Center"/>
                                <f:BoundField ColumnID="SpotCheckDate" MinWidth="120px" DataField="SpotCheckDate" HeaderText="实际完成时间" TextAlign="Center" HeaderTextAlign="Center"/>
                              
                                <f:BoundField ColumnID="Ok" MinWidth="70px" DataField="Ok" HeaderText="实体验收结果" TextAlign="Center" HeaderTextAlign="Center"/>
                                <f:BoundField ColumnID="IsDataOK" MinWidth="70px" DataField="IsDataOK" HeaderText="资料验收结果" TextAlign="Center" HeaderTextAlign="Center"/>
                              
                               <f:TemplateField ColumnID="attchUrl"  MinWidth="80px" HeaderText="交工资料" HeaderTextAlign="Center" TextAlign="Center">
                                    <ItemTemplate>  
                                     <asp:HiddenField ID="isShow" runat="server" Value='<%# Eval("isShow") %>'  />
                                    <asp:LinkButton runat="server" ID="attchUrl"  CommandArgument='<%# Eval("SpotCheckDetailId") %>' OnClick="attchUrl_Click"  Text='<%# BLL.ControlItemAndCycleService.ConvertContronInfo(Eval("ControlItemAndCycleId")) %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </f:TemplateField>
                           
                            </Columns>

                        </f:Grid>
                          
                    </Items>
                </f:Region>
            </Regions>
        </f:RegionPanel>
          <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true"  runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuMore" OnClick="btnMenuMore_Click" EnablePostBack="true" runat="server" Icon="ZoomIn"
                Text="展开全部">
            </f:MenuButton>
        
        </f:Menu>
    </form>
    <script type="text/javascript">
        var treeID = '<%= trWBS.ClientID %>';
        var menuID = '<%= Menu1.ClientID %>';
        // 保存当前菜单对应的树节点ID
        var currentNodeId;

        // 返回false，来阻止浏览器右键菜单
        function onTreeNodeContextMenu(event, nodeId) {
            currentNodeId = nodeId;
            F(menuID).show();
            return false;
        }

        // 设置所有菜单项的禁用状态
        function setMenuItemsDisabled(disabled) {
            var menu = F(menuID);
            $.each(menu.items, function (index, item) {
                item.setDisabled(disabled);
            });
        }

        // 显示菜单后，检查是否禁用菜单项
        function onMenuShow() {
            if (currentNodeId) {
                var tree = F(treeID);
                var nodeData = tree.getNodeData(currentNodeId);
                if (nodeData.leaf) {
                    setMenuItemsDisabled(true);
                } else {
                    setMenuItemsDisabled(false);
                }
            }
        }



    </script>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckItemSet.aspx.cs" Inherits="FineUIPro.Web.HSSE.Technique.CheckItemSet" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <style>
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
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="400" Title="检查项" TitleToolTip="右键点击添加、修改、删除" ShowBorder="true"
                ShowHeader="false" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Center" runat="server">
                        <Items>
                            <f:RadioButtonList ID="ckType" runat="server" AutoPostBack="true" ColumnNumber="4" 
                                AutoColumnWidth="true"  OnSelectedIndexChanged="ckType_SelectedIndexChanged">
                                <f:RadioItem Value="1" Text="日常检查"/>
                                <f:RadioItem Value="2" Text="专项检查" />
                                <f:RadioItem Value="3" Text="综合检查" />
                                <f:RadioItem Value="6" Text="联合检查" />
                           <%--     <f:RadioItem Value="4" Text="开工前检查" />
                                <f:RadioItem Value="5" Text="季节性/节假日检查"/>--%>
                            </f:RadioButtonList>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                 <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Bottom" ToolbarAlign="Right" runat="server">
                        <Items>
                            <f:Button ID="btnDelete" Icon="Delete" runat="server" ToolTip="删除检查项内容"  ConfirmText="确认删除此类型检查项所有内容？"
                                Hidden="true" OnClick="btnDelete_Click" AjaxLoadingType="Mask" ShowAjaxLoadingMaskText="true"
                                AjaxLoadingMaskText="正在删除检查项内容，请稍候">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                    <f:Tree ID="tvCheckItemSet" KeepCurrentSelection="true"
                        ShowHeader="false" OnNodeCommand="tvCheckItemSet_NodeCommand" runat="server"
                        ShowBorder="false" EnableSingleClickExpand="true">
                        <Listeners>
                            <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                        </Listeners>
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="检查项明细"
                AutoScroll="true">
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        BoxFlex="1" DataKeyNames="CheckItemDetailId" DataIDField="CheckItemDetailId" AllowSorting="true" SortField="SortIndex" SortDirection="ASC"
                        OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" EnableColumnLines="true"
                        PageSize="15"  OnPageIndexChange="Grid1_PageIndexChange" 
                        EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
                                <Items>
                                    <f:HiddenField runat="server" ID="hdSelectId"  ></f:HiddenField>
                                    <f:Button ID="btnNewDetail" ToolTip="新增" Icon="Add" Hidden="true" runat="server"
                                        OnClick="btnNewDetail_Click">
                                    </f:Button>                                   
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true"/>
                            <f:RenderField ColumnID="CheckContent" DataField="CheckContent" SortField="CheckContent" ExpandUnusedSpace="true"
                                FieldType="String" HeaderText="检查项目内容" HeaderTextAlign="Center" TextAlign="left">
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
                            </f:DropDownList>
                        </PageItems>
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    <f:Menu ID="Menu1" runat="server">
        <Items>
            <f:MenuButton ID="btnMenuNew" EnablePostBack="true" runat="server" Hidden="true" Icon="Add" Text="增加"
                OnClick="btnMenuNew_Click">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Hidden="true" Icon="Pencil" Text="修改"
                OnClick="btnMenuModify_Click">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server" Hidden="true" Icon="Delete" Text="删除" ConfirmText="删除选中检查项？"
                OnClick="btnMenuDel_Click">
            </f:MenuButton>
        </Items>
    </f:Menu>
    <f:Menu ID="Menu2" runat="server">
        <Items>
            <f:MenuButton ID="btnMenuModifyDetail" EnablePostBack="true" runat="server" Hidden="true" Text="修改" Icon="Pencil"
                OnClick="btnMenuModifyDetail_Click">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDelDetail" EnablePostBack="true" runat="server" Hidden="true" Text="删除"  Icon="Delete" ConfirmText="删除选中检查项明细？"
                OnClick="btnMenuDelDetail_Click">
            </f:MenuButton>
        </Items>
    </f:Menu>
    <f:Window ID="Window2" Title="检查项目设置" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" OnClose="Window2_Close" IsModal="true"
        Width="600px" Height="240px">
    </f:Window>
    <f:Window ID="Window1" Title="编辑检查项目明细" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="600px" Height="240px">
    </f:Window>
    </form>
    <script>
        var treeID = '<%= tvCheckItemSet.ClientID %>';
        var menuID = '<%= Menu1.ClientID %>';
        var menu2ID = '<%= Menu2.ClientID %>';
        // 保存当前菜单对应的树节点ID
        var currentNodeId;

        // 返回false，来阻止浏览器右键菜单
        function onTreeNodeContextMenu(event, nodeId) {
            currentNodeId = nodeId;
            F(menuID).show();
            return false;
        }

        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menu2ID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>
</body>
</html>

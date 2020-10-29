<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurgingCleaningEdit.aspx.cs" Inherits="FineUIPro.Web.HJGL.PurgingCleaning.PurgingCleaningEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>吹扫/清洗包</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
            <Items>
                <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                    EnableCollapse="true" Width="230px" Title="吹扫/清洗包" ShowBorder="true" Layout="VBox"
                    ShowHeader="true" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                    <Items>
                        <f:Tree ID="tvControlItem" ShowHeader="false" Title="吹扫/清洗包节点树" OnNodeCommand="tvControlItem_NodeCommand"
                            runat="server" ShowBorder="false" EnableCollapse="true" EnableSingleClickExpand="true" AutoLeafIdentification="true"
                            EnableSingleExpand="true" EnableTextSelection="true">
                            <Listeners>
                                <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                            </Listeners>
                        </f:Tree>
                        <f:HiddenField runat="server" ID="hdPurgingCleaningId"></f:HiddenField>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                    Layout="VBox" ShowHeader="false" BodyPadding="2px" IconFont="PlusCircle" Title="吹扫/清洗包"
                    TitleToolTip="吹扫/清洗包" AutoScroll="true">
                    <Items>
                        <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true"
                            BodyPadding="2px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                        <f:Label ID="txtSysNo" Label="系统号" runat="server" LabelWidth="130px">
                                        </f:Label>
                                        <f:Label ID="txtSysName" Label="系统名称" runat="server" LabelWidth="130px">
                                        </f:Label>
                                        <f:Label ID="drpTabler" Label="创建人" runat="server" LabelWidth="130px">
                                        </f:Label>
                                        <f:Label ID="txtTableDate" Label="创建日期" runat="server" LabelWidth="130px">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ColumnWidths="25% 25% 50%">
                                    <Items>
                                        <f:Label ID="drpAuditer" Label="审核人" runat="server" LabelWidth="130px">
                                        </f:Label>
                                        <f:Label ID="txtAduditDate" Label="审核日期" runat="server" LabelWidth="130px">
                                        </f:Label>
                                        <f:Label ID="txtRemark" Label="备注" runat="server" LabelWidth="130px">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                    <Items>
                        <f:Panel runat="server" ID="panel2" RegionPosition="Center" ShowBorder="true" Layout="VBox"
                            BodyPadding="2px" IconFont="PlusCircle" Title="吹扫/清洗前条件确认"
                            ShowHeader="true" AutoScroll="true" EnableCollapse="true" Collapsed="false" Height="200px">
                            <Items>
                                <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" Title="吹扫/清洗包" AutoScroll="true"
                                    EnableCollapse="true" Collapsed="false" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
                                    LabelAlign="Left">
                                    <Rows>
                                        <f:FormRow>
                                            <Items>
                                                <f:TextBox ID="txtCheck1" runat="server" Label="1.管道压力试验合格"
                                                    LabelAlign="Right" LabelWidth="350px">
                                                </f:TextBox>
                                                <f:TextBox ID="txtCheck2" runat="server" Label="2.不参与吹扫／清洗的安全附件及仪表等己隔离或拆除" LabelAlign="Right"
                                                    LabelWidth="350px">
                                                </f:TextBox>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:TextBox ID="txtCheck3" runat="server" Label="3.管道系统的阀门己全部开启" LabelAlign="Right"
                                                    LabelWidth="350px">
                                                </f:TextBox>
                                                <f:TextBox ID="txtCheck4" runat="server" Label="4.不锈钢管道用水符合规范要求"
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
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="吹扫/清洗包明细" EnableCollapse="true" Collapsed="false"
                            runat="server" BoxFlex="1" DataKeyNames="PC_PipeId" AllowCellEditing="true"
                            EnableColumnLines="true" ClicksToEdit="2" DataIDField="PC_PipeId" AllowSorting="true"
                            SortField="PipelineCode" SortDirection="ASC" OnSort="Grid1_Sort" EnableTextSelection="True"
                            AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange">
                            <Columns>
                                <f:RenderField HeaderText="管线编号" ColumnID="PipelineCode" DataField="PipelineCode" SortField="PipelineCode"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="160px" ExpandUnusedSpace="true">
                                </f:RenderField>
                                <f:RenderField HeaderText="材质" ColumnID="MaterialCode" DataField="MaterialCode" SortField=""
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="操作介质" ColumnID="MediumName" DataField="MediumName" SortField="MediumName"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="吹扫介质" ColumnID="PurgingMediumName" DataField="PurgingMediumName"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="110px">
                                </f:RenderField>
                                <f:RenderField HeaderText="清洗介质" ColumnID="CleaningMediumName" DataField="CleaningMediumName" FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="90px">
                                </f:RenderField>
                            </Columns>
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
        <f:Window ID="Window1" Title="吹扫/清洗包维护" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Top" EnableResize="false" runat="server" OnClose="Window1_Close" IsModal="true"
            Width="1280px" Height="900px">
        </f:Window>
        <f:Window ID="Window2" Title="吹扫/清洗包打印" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Top" EnableResize="false" runat="server"
            IsModal="true" Width="1024px" Height="900px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <Items>
                <f:MenuButton ID="btnMenuNew" EnablePostBack="true" runat="server" Text="新增" Icon="Add" OnClick="btnMenuNew_Click">
                </f:MenuButton>
                <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Text="编辑" Icon="Pencil" OnClick="btnMenuModify_Click">
                </f:MenuButton>
                <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server" Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？"
                    OnClick="btnMenuDel_Click">
                </f:MenuButton>
                <f:MenuButton ID="btnMenuPrint" EnablePostBack="true" runat="server" Text="打印" Icon="Pencil" OnClick="btnMenuPrint_Click">
                </f:MenuButton>
            </Items>
        </f:Menu>
    </form>
    <script type="text/javascript">
        // 返回false，来阻止浏览器右键菜单
        var menuID = '<%= Menu1.ClientID %>';
        function onTreeNodeContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>

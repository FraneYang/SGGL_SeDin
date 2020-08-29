<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurgingCleaningItemEdit.aspx.cs" Inherits="FineUIPro.Web.HJGL.PurgingCleaning.PurgingCleaningItemEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>吹扫/清洗试验包</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="VBox">
            <Items>
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                    Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="根据条件查找并勾选要进行试压的管线">
                    <Items>
                        <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" Title="吹扫/清洗试验包" AutoScroll="true" EnableCollapse="true" Collapsed="false"
                            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="txtsysNo" Label="吹扫/清洗试验包号" ShowRedStar="true" Required="true" runat="server" FocusOnPageLoad="true" LabelWidth="120px">
                                        </f:TextBox>
                                        <f:TextBox ID="txtsysName" Label="系统名称" runat="server" LabelWidth="120px">
                                        </f:TextBox>
                                        <f:DropDownList ID="drpUnit" Label="单位" runat="server" ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="120px"
                                            Readonly="true">
                                        </f:DropDownList>
                                        <f:DropDownList ID="drpUnitWork" Label="单位工程名称" runat="server" ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="120px"
                                            Readonly="true">
                                        </f:DropDownList>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ColumnWidths="25% 25% 50%">
                                    <Items>
                                        <f:DropDownList ID="drpTabler" Label="建档人" runat="server" EnableEdit="true" LabelWidth="120px" ShowRedStar="true" Required="true">
                                        </f:DropDownList>
                                        <f:DatePicker ID="txtTableDate" Label="建档日期" runat="server" DateFormatString="yyyy-MM-dd" LabelWidth="120px" ShowRedStar="true" Required="true">
                                        </f:DatePicker>
                                        <f:TextBox ID="txtRemark" Label="备注" runat="server" LabelWidth="130px">
                                        </f:TextBox>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="panel4" RegionPosition="Center" ShowBorder="true" Layout="VBox"
                    BodyPadding="2px" IconFont="PlusCircle" Title="试压前条件确认"
                    ShowHeader="true" AutoScroll="true" EnableCollapse="true" Collapsed="false" Height="200px">
                    <Items>
                        <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" Title="试压包" AutoScroll="true"
                            EnableCollapse="true" Collapsed="false" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
                            LabelAlign="Left">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                        <f:DropDownList ID="drpInstallationSpecification" runat="server" Label="1.管道压力试验合格"
                                            LabelAlign="Right" LabelWidth="350px">
                                            <f:ListItem Value="完成" Text="完成" Selected="true" />
                                            <f:ListItem Value="未完成" Text="未完成" />
                                            <f:ListItem Value="/" Text="/" />
                                        </f:DropDownList>
                                        <f:DropDownList ID="drpPressureTest" runat="server" Label="2.不参与吹扫／清洗的安全附件及仪表等己隔离或拆除" LabelAlign="Right"
                                            LabelWidth="350px">
                                            <f:ListItem Value="完成" Text="完成" Selected="true" />
                                            <f:ListItem Value="未完成" Text="未完成" />
                                            <f:ListItem Value="/" Text="/" />
                                        </f:DropDownList>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:DropDownList ID="drpWorkRecord" runat="server" Label="3.管道系统的阀门己全部开启" LabelAlign="Right"
                                            LabelWidth="350px">
                                            <f:ListItem Value="完成" Text="完成" Selected="true" />
                                            <f:ListItem Value="未完成" Text="未完成" />
                                            <f:ListItem Value="/" Text="/" />
                                        </f:DropDownList>
                                        <f:DropDownList ID="drpNDTConform" runat="server" Label="4.不锈钢管道用水符合规范要求"
                                            LabelAlign="Right" LabelWidth="350px">
                                            <f:ListItem Value="完成" Text="完成" Selected="true" />
                                            <f:ListItem Value="未完成" Text="未完成" />
                                            <f:ListItem Value="/" Text="/" />
                                        </f:DropDownList>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="panel3" RegionPosition="Center" ShowBorder="true"
                    Layout="VBox" ShowHeader="true" BodyPadding="5px" IconFont="PlusCircle" Title="根据条件查找并勾选要进行试压的管线">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Bottom" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:HiddenField ID="hdPipelinesId" runat="server"></f:HiddenField>
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
                            OnPageIndexChange="Grid1_PageIndexChange">
                            <Columns>
                                <f:RenderField HeaderText="管线编号" ColumnID="PipelineCode" DataField="PipelineCode" SortField="PipelineCode"
                                    FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="160px" ExpandUnusedSpace="true">
                                </f:RenderField>
                                <f:TemplateField ColumnID="MaterialId" Width="130px" HeaderText="材质" HeaderTextAlign="Center" TextAlign="Center"
                                    EnableLock="true" Locked="False">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="drpMaterialId" runat="server" Height="22" Width="90%">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdMaterialId" runat="server" Value='<%# Bind("MaterialId") %>' />
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:TemplateField ColumnID="MediumId" Width="130px" HeaderText="操作介质" HeaderTextAlign="Center" TextAlign="Center"
                                    EnableLock="true" Locked="False">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="drpMediumId" runat="server" Height="22" Width="90%">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdMediumId" runat="server" Value='<%# Bind("MediumId") %>' />
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:TemplateField ColumnID="PurgingMedium" Width="130px" HeaderText="吹扫介质" HeaderTextAlign="Center" TextAlign="Center"
                                    EnableLock="true" Locked="False">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="drpPurgingMedium" runat="server" Height="22" Width="90%">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdPurgingMedium" runat="server" Value='<%# Bind("PurgingMedium") %>' />
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:TemplateField ColumnID="CleaningMedium" Width="130px" HeaderText="清洗介质" HeaderTextAlign="Center" TextAlign="Center"
                                    EnableLock="true" Locked="False">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="drpCleaningMedium" runat="server" Height="22" Width="90%">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdCleaningMedium" runat="server" Value='<%# Bind("CleaningMedium") %>' />
                                    </ItemTemplate>
                                </f:TemplateField>
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
        <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
            Width="1000px" Height="660px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
                ConfirmText="删除选中行？" ConfirmTarget="Top" runat="server"
                Text="删除" Icon="Delete">
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckMonthFile.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.CheckMonthFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>

                <f:Grid ID="Grid1" ShowBorder="true" EnableAjax="false" ShowHeader="false" Title="质量月报" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="CheckMonthId" AllowCellEditing="true"
                    ClicksToEdit="2" DataIDField="CheckMonthId" AllowSorting="true" SortField="Months"
                    SortDirection="DESC" EnableColumnLines="true" ForceFit="true"
                    AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnRowCommand="Grid1_RowCommand"
                    EnableRowDoubleClickEvent="false" AllowFilters="true" EnableTextSelection="True"  OnPageIndexChange="Grid1_PageIndexChange">
                    
                    <Columns>
                        <f:TemplateField ColumnID="tfPageIndex" Width="55px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField Width="180px" ColumnID="Months" DataField="Months" SortField="Months" FieldType="Date" HeaderText="月份" TextAlign="Center" HeaderTextAlign="Center" RendererArgument="yyyy-MM">
                        </f:RenderField>
                        <f:RenderField Width="180px" ColumnID="CompileDate" DataField="CompileDate" SortField="CompileDate" FieldType="Date" HeaderText="编制日期" TextAlign="Center" HeaderTextAlign="Center" RendererArgument="yyyy-MM-dd">
                        </f:RenderField>
                        <f:RenderField Width="180px" ColumnID="UserName" DataField="UserName"
                            FieldType="String" HeaderText="编制人" TextAlign="Center" HeaderTextAlign="Center" >
                        </f:RenderField>

                            <f:LinkButtonField ColumnID="export" HeaderText="导出" Width="60px" Icon="ArrowUp" CommandName="export" />
                        <f:LinkButtonField HeaderText="附件" ColumnID="download" Width="60px" Icon="ArrowDown" CommandName="download" />
                    </Columns>
                    <Listeners>
                        <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                    </Listeners>
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                        </f:ToolbarText>
                        <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                            <f:ListItem Text="10" Value="10" />
                            <f:ListItem Text="15" Value="15" />
                            <f:ListItem Text="20" Value="20" />
                            <f:ListItem Text="25" Value="25" />
                            <f:ListItem Text="所有行" Value="100000" />
                        </f:DropDownList>
                        <f:ToolbarFill runat="server">
                        </f:ToolbarFill>

                    </PageItems>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="编辑月报" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" 
            Width="1290px" Height="800px" OnClose="Window1_Close">
        </f:Window>
         <f:Window ID="Windowtt" Title="文件柜" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true"
      Width="700px"   Height="500px" >
                </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <Items>
             
                <f:MenuButton ID="btnMenuView" EnablePostBack="true" runat="server" Text="查看" Icon="ApplicationViewIcons" OnClick="btnMenuView_Click">
                </f:MenuButton>
              
            </Items>
        </f:Menu>
    </form>
    <script type="text/javascript">
        var menuID = '<%= Menu1.ClientID %>';

        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>
</body>
</html>

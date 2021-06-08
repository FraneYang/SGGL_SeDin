<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BidDocumentsFile.aspx.cs" Inherits="FineUIPro.Web.PHTGL.Filing.BidDocumentsFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>招标文件审批</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="招标文件审批" EnableCollapse="true" EnableAjax="false"
                    runat="server" BoxFlex="1" DataKeyNames="BidDocumentsReviewId" AllowCellEditing="true"
                    ClicksToEdit="2" DataIDField="BidDocumentsReviewId" AllowSorting="true" OnSort="Grid1_Sort"
                    SortDirection="DESC" EnableColumnLines="true"   OnPageIndexChange="Grid1_PageIndexChange"
                    AllowPaging="true" IsDatabasePaging="true" PageSize="10" ForceFit="true" OnRowCommand="Grid1_RowCommand"
                    EnableRowDoubleClickEvent="true"
                    EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:TextBox runat="server" Label="招标文件编号" ID="txtBidDocumentsCode" EmptyText="输入查询条件" Width="300px" LabelWidth="140px"
                                    LabelAlign="left">
                                </f:TextBox>
                                <f:ToolbarFill runat="server"></f:ToolbarFill>
                                <f:Button ID="btnQuery" OnClick="btnSearch_Click" ToolTip="查询" Icon="SystemSearch" Text="查询" EnablePostBack="true" runat="server">
                                </f:Button>
                                <f:Button ID="btnRset" OnClick="btnRset_Click" ToolTip="重置" Icon="ArrowUndo" Text="重置" EnablePostBack="true" runat="server">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                     </Toolbars>
                    <Columns>
                        <f:TemplateField ColumnID="tfPageIndex" Width="55px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField ColumnID="BidDocumentsCode" DataField="BidDocumentsCode" Width="120px" FieldType="String" HeaderText="招标文件编号" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="BidDocumentsName" DataField="BidDocumentsName" Width="120px" FieldType="String" HeaderText="招标文件名称" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                         <f:RenderField ColumnID="ActionPlanCode" DataField="ActionPlanCode" Width="120px" FieldType="String" HeaderText="实施计划编号编号" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="ProjectName" DataField="ProjectName" Width="120px" FieldType="String" HeaderText="项目名称" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="ProjectCode" DataField="ProjectCode" Width="120px" FieldType="String" HeaderText="项目编号" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="Bidding_SendTime" DataField="Bidding_SendTime" Width="120px" FieldType="String" HeaderText="计划发标时间" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="Bidding_StartTime" DataField="Bidding_StartTime" Width="120px" FieldType="String" HeaderText="计划开标时间" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="CreateUser" DataField="CreateUser" Width="180px" FieldType="String" HeaderText="创建人" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="120px" ColumnID="CreatTime" DataField="CreatTime" FieldType="Date"
                            Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="创建日期">
                        </f:RenderField>
                         <f:LinkButtonField ColumnID="export" HeaderText="导出" Width="60px" Icon="ArrowUp" CommandName="export" />
                        <f:LinkButtonField HeaderText="附件" ColumnID="download" Width="60px" Icon="ArrowDown" CommandName="download" />
                    </Columns>
                     <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                        </f:ToolbarText>
                        <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                            <f:ListItem Text="10" Value="10" />
                            <f:ListItem Text="15" Value="15" />
                            <f:ListItem Text="20" Value="20" />
                            <f:ListItem Text="25" Value="25" />
                            <f:ListItem Text="所有行" Value="100000" />
                        </f:DropDownList>
                    </PageItems>
                </f:Grid>
            </Items>
        </f:Panel>
    <f:Window ID="Windowtt" Title="文件柜" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true"
            Width="700px" Height="500px">
        </f:Window>
 
    </form>
    <script type="text/javascript">
      
    </script>
</body>
</html>

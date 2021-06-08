<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractFile.aspx.cs" Inherits="FineUIPro.Web.PHTGL.Filing.ContractFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>合同基本信息</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Grid ID="Grid1" ShowBorder="true"  ShowHeader="false" Title="基本信息" EnableCollapse="true" EnableAjax="false"
                    runat="server" BoxFlex="1" DataKeyNames="ContractReviewId" AllowCellEditing="true"
                    ClicksToEdit="2" DataIDField="ContractReviewId" AllowSorting="true" SortField="ContractNum" OnSort="Grid1_Sort"
                    SortDirection="DESC" EnableColumnLines="true"   OnPageIndexChange="Grid1_PageIndexChange"
                    AllowPaging="true" IsDatabasePaging="true" PageSize="10"   EnableRowClickEvent="true" OnRowCommand="Grid1_RowCommand"  
                    EnableRowDoubleClickEvent="true"
                    EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:TextBox runat="server" Label="合同名称" ID="txtContractName" EmptyText="输入查询条件" Width="300px" LabelWidth="90px"
                                    LabelAlign="right">
                                </f:TextBox>
                                 <f:ToolbarFill runat="server"></f:ToolbarFill>
 
                                <f:Button ID="btnSearch" ToolTip="查询" Icon="SystemSearch" runat="server" OnClick="btnSearch_Click">
                                </f:Button>
                                 <f:Button ID="btnPrinter" EnablePostBack="true" runat="server"
                                    Text="导出评审单" Icon="Printer" OnClick="btnPrinter_Click" EnableAjax="false" DisableControlBeforePostBack="true">
                                </f:Button>
                                <f:Button ID="btnPrinterWord" EnablePostBack="true" runat="server"
                                    Text="导出文件" Icon="Printer" OnClick="btnPrinterWord_Click" EnableAjax="false" DisableControlBeforePostBack="true">
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
                         <f:LinkButtonField HeaderText="附件" ColumnID="download" Width="60px" Icon="ArrowDown" CommandName="download" />
                         <f:RenderField ColumnID="ProjectCode" DataField="ProjectCode" Width="120px" FieldType="String" HeaderText="总承包合同编号" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="ProjectName" DataField="ProjectName" Width="180px" FieldType="String" HeaderText="项目名称" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
 
                        <f:RenderField ColumnID="ContractName" DataField="ContractName" Width="180px" FieldType="String" HeaderText="合同名称" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="ContractNum" DataField="ContractNum" Width="180px" FieldType="String" HeaderText="合同编号" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="Parties" DataField="Parties" Width="120px" FieldType="String" HeaderText="签约方" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="Currency" DataField="Currency" Width="100px" FieldType="String" HeaderText="币种" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="ContractAmount" DataField="ContractAmount" Width="120px" FieldType="String" HeaderText="合同金额" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="DepartName" DataField="DepartName" Width="120px" FieldType="String" HeaderText="主办部门" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="AgentName" DataField="AgentName" Width="120px" FieldType="String" HeaderText="经办人" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="ContractType" DataField="ContractType" Width="150px" FieldType="String" HeaderText="合同类型" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="Remarks" DataField="Remarks" Width="220px" FieldType="String" HeaderText="合同摘要" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
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

﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractFormation.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.ContractFormation" %>

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
                <f:Grid ID="Grid1" ShowBorder="true"  ShowHeader="false" Title="基本信息" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="ContractId" AllowCellEditing="true"
                    ClicksToEdit="2" DataIDField="ContractId" AllowSorting="true" SortField="ContractNum" OnSort="Grid1_Sort"
                    SortDirection="DESC" EnableColumnLines="true" OnRowDoubleClick="Grid1_RowDoubleClick" OnPageIndexChange="Grid1_PageIndexChange"
                    AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnRowClick="Grid1_RowClick" EnableRowClickEvent="true"
                    EnableRowDoubleClickEvent="true"
                    EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:TextBox runat="server" Label="合同名称" ID="txtContractName" EmptyText="输入查询条件" Width="300px" LabelWidth="90px"
                                    LabelAlign="right">
                                </f:TextBox>
                                <f:DropDownList ID="drpStates" runat="server" Label="状态" AutoPostBack="true" OnSelectedIndexChanged="btnSearch_Click"
                                    LabelWidth="70px" LabelAlign="Right" Width="170px">
                                </f:DropDownList>
                                <f:ToolbarFill runat="server"></f:ToolbarFill>
                                <f:Button ID="btnSearch" ToolTip="查询" Icon="SystemSearch" runat="server" Text="查询" OnClick="btnSearch_Click">
                                </f:Button>
                                <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" Text="新增" runat="server"
                                    Hidden="true">
                                </f:Button>
                                <f:Button ID="btnMenuEdit" EnablePostBack="true" runat="server" Hidden="true" Text="编辑" Icon="Pencil"
                                    OnClick="btnMenuEdit_Click">
                                </f:Button>
                                 <f:Button ID="btnPrinterWord" EnablePostBack="true" runat="server"  
                                    Text="导出文件" Icon="Printer" OnClick="btnPrinterWord_Click" EnableAjax="false" DisableControlBeforePostBack="true">
                                </f:Button>
                                <f:Button ID="btnMenuDelete" EnablePostBack="true" runat="server" Hidden="true" Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？"
                                    OnClick="btnMenuDelete_Click">
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
                        <f:RenderField ColumnID="EPCCode" DataField="EPCCode" Width="200px" FieldType="String" HeaderText="总承包合同编号" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                         <f:RenderField ColumnID="ApproveState" DataField="ApproveState" Width="80px" FieldType="String" HeaderText="编制状态" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="ProjectShortName" DataField="ProjectShortName" Width="200px" FieldType="String" HeaderText="项目名称" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="ContractName" DataField="ContractName" Width="200px" FieldType="String" HeaderText="合同名称" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="ContractNum" DataField="ContractNum" Width="200px" FieldType="String" HeaderText="合同编号" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="Parties" DataField="Parties" Width="120px" FieldType="String" HeaderText="签约方" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="Currency" DataField="Currency" Width="80px" FieldType="String" HeaderText="币种" TextAlign="Center"
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
                 <%--       <f:RenderField ColumnID="Remarks" DataField="Remarks" Width="220px" FieldType="String" HeaderText="合同摘要" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>--%>
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
        <f:Window ID="Window1" Title="基本信息" Hidden="true" EnableIFrame="true" EnableMaximize="true" Maximized="true" EnableDrag="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="false" OnClose="Window1_Close" ConstrainSize="true" AutoScroll="true"
            Width="1200px" Height="650px">
        </f:Window>
        <f:Window ID="WindowAtt" Title="文件柜" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <Items>
                <%--<f:MenuButton ID="btnMenuEdit" EnablePostBack="true" runat="server" Hidden="true" Text="编辑" Icon="Pencil"
                    OnClick="btnMenuEdit_Click">
                </f:MenuButton>
                <f:MenuButton ID="btnMenuDelete" EnablePostBack="true" runat="server" Hidden="true" Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？"
                    OnClick="btnMenuDelete_Click">
                </f:MenuButton>--%>
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

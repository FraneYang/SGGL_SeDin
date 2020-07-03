<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlItemAndCycleSearch.aspx.cs" Inherits="FineUIPro.Web.CQMS.WBS.ControlItemAndCycleSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }

         .f-grid-colheader-text {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>

                <f:Grid ID="Grid1" ShowBorder="true" EnableAjax="false" ShowHeader="false" Title="项目施工WBS查询" EnableCollapse="true" runat="server"
                    BoxFlex="1" DataKeyNames="ControlItemAndCycleId" DataIDField="ControlItemAndCycleId" AllowSorting="true" SortField="ControlItemAndCycleCode"
                    SortDirection="DESC" EnableColumnLines="true" AllowPaging="true" IsDatabasePaging="true" PageSize="10" ForceFit="true"
                    EnableRowDoubleClickEvent="true" AllowFilters="true" EnableTextSelection="True" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:DropDownList ID="drpUnitWork" runat="server" Label="单位工程" LabelAlign="Right" EnableEdit="true" LabelWidth="110px">
                                </f:DropDownList>
                                <f:TextBox runat="server" ID="txtControlItemContent" Label="工作包" LabelWidth="70px" LabelAlign="Right"></f:TextBox>
                                <f:DropDownList ID="drpControlPoint" runat="server" Label="控制点等级" EnableMultiSelect="true" LabelAlign="Right" EnableEdit="true" LabelWidth="110px">
                                    <f:ListItem Text="A" Value="A" />
                                    <f:ListItem Text="AR" Value="AR" />
                                    <f:ListItem Text="B" Value="B" />  
                                    <f:ListItem Text="BR" Value="BR" />
                                    <f:ListItem Text="C" Value="C" />
                                    <f:ListItem Text="CR" Value="CR" />
                                </f:DropDownList>
                                <f:TextBox runat="server" ID="txtForms" Label="资料表格" LabelAlign="Right"></f:TextBox>
                                <f:ToolbarFill runat="server"></f:ToolbarFill>
                                <f:Button ID="btnSearch" Icon="SystemSearch" ToolTip="查询"
                                    EnablePostBack="true" runat="server" OnClick="btnSearch_Click">
                                </f:Button>
                                <f:Button ID="btnRset" ToolTip="重置" Icon="ArrowUndo" EnablePostBack="true" runat="server" OnClick="btnRset_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:TemplateField ColumnID="tfPageIndex" Width="50px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField Width="120px" ColumnID="UnitWorkName" DataField="UnitWorkName" FieldType="String"
                            HeaderText="单位工程" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="120px" ColumnID="ControlItemContent" DataField="ControlItemContent" FieldType="String"
                            HeaderText="工作包" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="95px" ColumnID="ControlPoint" DataField="ControlPoint" FieldType="String"
                            HeaderText="控制点等级" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="65px" ColumnID="Weights" DataField="Weights" FieldType="String"
                            HeaderText="权重%" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField HeaderText="控制点内容描述" ColumnID="ControlItemDef" DataField="ControlItemDef" SortField="ControlItemDef"
                            HeaderTextAlign="Center" TextAlign="Center" Width="250px" FieldType="String" >
                        </f:RenderField>
                        <f:RenderField Width="160px" ColumnID="HGFormsJZ" DataField="HGFormsJZ" FieldType="String"
                            HeaderText="对应的建筑资料表格" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="160px" ColumnID="HGFormsAZ" DataField="HGFormsAZ" FieldType="String"
                            HeaderText="对应的化工资料表格" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="160px" ColumnID="SHForms" DataField="SHForms" FieldType="String"
                            HeaderText="对应的石化资料表格" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField HeaderText="质量验收规范" ColumnID="Standard" DataField="Standard" SortField="Standard"
                            HeaderTextAlign="Center" TextAlign="Center" Width="120px" FieldType="String">
                        </f:RenderField>
                        <f:RenderField HeaderText="条款号" ColumnID="ClauseNo" DataField="ClauseNo" SortField="ClauseNo"
                            HeaderTextAlign="Center" TextAlign="Center" Width="100px" FieldType="String">
                        </f:RenderField>
                        <f:RenderField HeaderText="检查次数" ColumnID="CheckNum" DataField="CheckNum"
                            SortField="CheckNum" HeaderTextAlign="Center" TextAlign="Center" Width="100px"
                            FieldType="String">
                        </f:RenderField>
                    </Columns>
                    <%--<Listeners>
                        <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                    </Listeners>--%>
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
    </form>
</body>
</html>

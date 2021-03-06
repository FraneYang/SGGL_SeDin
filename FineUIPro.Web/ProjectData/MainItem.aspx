﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainItem.aspx.cs" Inherits="FineUIPro.Web.ProjectData.MainItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>项目主项</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .labcenter {
            text-align: center;
        }

        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }

        .f-grid-row.red {
            background-color: #FF7575;
            background-image: none;
        }

        .f-grid-colheader-text {
            white-space: normal;
            word-break: break-all;
        }

        .fontred {
            color: #FF7575;
            background-image: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>

                <f:Grid ID="Grid1" ShowBorder="true" EnableAjax="false" ShowHeader="false" Title="项目主项" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="MainItemId" AllowCellEditing="true"
                    ClicksToEdit="2" DataIDField="MainItemId" AllowSorting="true" SortField="MainItemCode"
                    SortDirection="DESC" EnableColumnLines="true"  ForceFit="true"
                    AllowPaging="true" IsDatabasePaging="true" PageSize="10"
                    EnableRowDoubleClickEvent="true" AllowFilters="true" EnableTextSelection="True" OnRowDoubleClick="Grid1_RowDoubleClick" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:TextBox ID="txtMainItemCode" runat="server" Label="主项编号" Width="220px" LabelWidth="80px"
                                    LabelAlign="Right">
                                </f:TextBox>

                                <f:ToolbarFill runat="server"></f:ToolbarFill>
                                <f:Button ID="btnSearch" Icon="SystemSearch"
                                    EnablePostBack="true" runat="server" OnClick="btnSearch_Click">
                                </f:Button>
                                <f:Button ID="btnRset" OnClick="btnRset_Click" ToolTip="重置" Icon="ArrowUndo" EnablePostBack="true" runat="server">
                                </f:Button>
                                <f:Button ID="btnNew" Icon="Add" EnablePostBack="true" Hidden="true"
                                    runat="server">
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
                        <f:RenderField Width="110px" ColumnID="MainItemCode" DataField="MainItemCode"
                            SortField="MainItemCode" FieldType="String" HeaderText="主项编号" TextAlign="center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="110px" ColumnID="MainItemName" DataField="MainItemName" FieldType="String" HeaderText="主项名称" TextAlign="Center" 
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:TemplateField ColumnID="UnitWorkIds" Width="260px" HeaderText="包含设计专业" HeaderTextAlign="Center" TextAlign="Center"
                                    EnableLock="true" Locked="False" >
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# ConvertProfessionalName(Eval("DesignProfessionalIds")) %>'></asp:Label>
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
        <f:Window ID="Window1" Title="项目主项" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" OnClose="Window1_Close"
            Width="800px" Height="460px">
        </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <Items>
                <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Text="修改" Icon="Pencil" OnClick="btnMenuModify_Click" Hidden="true">
                </f:MenuButton>
                <%--<f:MenuButton ID="btnMenuView" EnablePostBack="true" runat="server" Text="查看" Icon="ApplicationViewIcons" OnClick="btnMenuView_Click">
                </f:MenuButton>--%>
                <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server" Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？"
                    OnClick="btnMenuDel_Click" Hidden="true">
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

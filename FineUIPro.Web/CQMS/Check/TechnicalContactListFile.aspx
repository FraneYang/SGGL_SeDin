﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TechnicalContactListFile.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.TechnicalContactListFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <title>工程联络单</title>
    <style type="text/css">
        .labcenter {
            text-align: center;
        }

        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }

        .LabelColor {
            color: Red;
            font-size: small;
        }

        .Green {
            background-color: Green;
        }

        .Yellow {
            background-color: #FFFF93;
        }

        .HotPink {
            background-color: HotPink;
        }

        .LightGreen {
            background-color: LightGreen
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
       <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>

                <f:Grid ID="Grid1" ShowBorder="true" EnableAjax="false" ShowHeader="false" Title="工程联络单" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="TechnicalContactListId" AllowCellEditing="true"   OnPageIndexChange="Grid1_PageIndexChange"
                    ClicksToEdit="2" DataIDField="TechnicalContactListId"  AllowSorting="true" SortField="CompileDate"
                    SortDirection="DESC" EnableColumnLines="true" OnRowCommand="Grid1_RowCommand"
                    AllowPaging="true" IsDatabasePaging="true" PageSize="10" ForceFit="true"
                    EnableRowDoubleClickEvent="false" AllowFilters="true"
                    EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                 <f:DropDownList ID="drpProposeUnit"  runat="server"  Width="350px"   Label="提出单位" LabelAlign="Right" EnableEdit="true">
                                            </f:DropDownList>
                                     <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="开始日期" ID="txtStartTime"
                                    LabelAlign="right" >
                                </f:DatePicker>
                                <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="结束日期" ID="txtEndTime"
                                    LabelAlign="right" >
                                </f:DatePicker>
                                <f:DropDownList ID="drpUnitWork" runat="server" Label="单位工程" LabelAlign="Right" EnableEdit="true">
                                </f:DropDownList>
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar ID="Toolbar3" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:DropDownList ID="drpCNProfessional" runat="server" Label="专业" Width="350px" LabelAlign="Right" EnableEdit="true">
                                </f:DropDownList>
                                <f:DropDownList ID="drpContactListType" runat="server" Label="分类" LabelAlign="Right" EnableEdit="true">
                                        <f:ListItem Text="图纸类" Value="1" />
                                        <f:ListItem Text="非图纸类" Value="2" />
                                </f:DropDownList>
                                 <f:DropDownList ID="drpIsReply" runat="server" Label="答复" LabelAlign="Right" EnableEdit="true">
                                        <f:ListItem Text="需要回复" Value="1" />
                                        <f:ListItem Text="不需回复" Value="2" />
                                </f:DropDownList>
                              
                                 <f:ToolbarFill runat="server"></f:ToolbarFill>
                                  <f:Button ID="btnQuery" OnClick="btnQuery_Click" ToolTip="查询"    Icon="SystemSearch" EnablePostBack="true" runat="server" >
                                </f:Button>
                                    <f:Button ID="btnRset"  OnClick="btnRset_Click" ToolTip="重置"    Icon="ArrowUndo" EnablePostBack="true" runat="server" >
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
                        <f:RenderField ColumnID="Code" DataField="Code"
                            SortField="Code" FieldType="String" HeaderText="编号" TextAlign="Center"
                            HeaderTextAlign="Center"  >
                        </f:RenderField>
                        <f:RenderField ColumnID="UnitName" DataField="UnitName"
                            SortField="ProposedUnit" FieldType="String" HeaderText="提出单位" TextAlign="Center" 
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField ColumnID="UnitWorkId" Width="120px" DataField="UnitWorkId"
                            SortField="UnitWorkId" FieldType="String" HeaderText="单位工程" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="120px" ColumnID="CNProfessionalCode"  DataField="CNProfessionalCode" SortField="CNProfessionalCode"
                            FieldType="String" HeaderText="专业" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="120px" ColumnID="ContactListType" DataField="ContactListType" SortField="ContactListType"
                            FieldType="String" HeaderText="分类" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="95px" ColumnID="IsReply" DataField="IsReply" SortField="IsReply"
                            FieldType="String" HeaderText="答复" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="CompileDate" DataField="CompileDate" SortField="CompileDate"
                            FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="提出日期" TextAlign="Center" HeaderTextAlign="Center">
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
                        <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
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
        <f:Window ID="Window1" Title="工程联络单" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" OnClose="Window1_Close" runat="server" IsModal="true"
            Width="1300px" Height="660px">
        </f:Window>
         <f:Window ID="Windowtt" Title="文件柜" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true"
      Width="700px"   Height="500px" >
                </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <Items>
              
                <f:MenuButton ID="btnMenuView" EnablePostBack="true" OnClick="btnMenuView_Click" runat="server" Text="查看" Icon="ApplicationViewIcons">
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactList.aspx.cs" Inherits="FineUIPro.Web.CQMS.Unqualified.ContactList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <title>工作联系单</title>
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
        .f-grid-colheader-text {
            white-space: normal;
            word-break: break-all;
        }
        /*.f-grid-row.LightRedRow {
            background-color: #FF60AF;
        }

        .f-grid-row.LightYellowRow {
            background-color: #FFFF93;
        }

        .f-grid-row.LightGreenRow {
            background-color: #79FF79;
        }*/

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
        <f:pagemanager id="PageManager1" autosizepanelid="Panel1" runat="server" />
        <f:panel id="Panel1" runat="server" margin="5px" bodypadding="5px" showborder="false"
            showheader="false" layout="VBox" boxconfigalign="Stretch">
            <Items>

                <f:Grid ID="Grid1" ShowBorder="true" EnableAjax="false" ShowHeader="false" Title="工作联系单" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="WorkContactId" AllowCellEditing="true"
                    ClicksToEdit="2" DataIDField="WorkContactId" AllowSorting="true" SortField="code"
                    SortDirection="DESC"  EnableColumnLines="true" OnRowDoubleClick="Grid1_RowDoubleClick"  OnPageIndexChange="Grid1_PageIndexChange"
                    AllowPaging="true" IsDatabasePaging="true" PageSize="10" ForceFit="true"
                    EnableRowDoubleClickEvent="true" AllowFilters="true"
                   EnableTextSelection="True">
                    <Toolbars>
                        
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                  <f:DropDownList ID="drpProposeUnit"  runat="server"  Width="350px"   Label="提出单位" LabelAlign="Right" EnableEdit="true">
                                            </f:DropDownList>
                                  <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="开始日期" ID="txtStartTime"
                                      LabelAlign="right" LabelWidth="80px" Width="190px">
                                </f:DatePicker>
                                  <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="结束日期" ID="txtEndTime"
                                     LabelAlign="right"  LabelWidth="80px" Width="190px">
                                </f:DatePicker>
                                <f:DropDownList ID="drpIsReply" runat="server" Label="答复" LabelAlign="Right" EnableEdit="true" LabelWidth="50px" Width="150px">
                                        <f:ListItem Text="需要回复" Value="1" />
                                        <f:ListItem Text="不需回复" Value="2" />
                                </f:DropDownList>
                                 <f:DropDownList ID="drpState" runat="server" Label="审批状态" LabelAlign="Right" EnableEdit="true" LabelWidth="80px" Width="190px">
                                        <f:ListItem Text="已闭合" Value="1" />
                                        <f:ListItem Text="未闭合" Value="0" />
                                </f:DropDownList>   
                                 <f:ToolbarFill runat="server"></f:ToolbarFill>
                                   <f:Button ID="btnFinalFile"  ToolTip="线下文件上传"    Icon="RssGo" EnablePostBack="false" runat="server" >
                                </f:Button>
                                 <f:Button ID="btnQuery"  ToolTip="查询" OnClick="btnQuery_Click"    Icon="SystemSearch" EnablePostBack="true" runat="server" >
                                </f:Button>
                                 <f:Button ID="btnRset"   ToolTip="重置"  OnClick="btnRset_Click"   Icon="ArrowUndo" EnablePostBack="true" runat="server" >
                                </f:Button>
                                <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                    Hidden="true">
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
                        <f:RenderField ColumnID="Code" DataField="Code" Width="80px" 
                            SortField="Code" FieldType="String" HeaderText="文件编号" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>

                        <f:RenderField ColumnID="UnitName" DataField="UnitName" Width="160px" 
                            SortField="ProposedUnit" FieldType="String" HeaderText="提出单位" TextAlign="Center" 
                            HeaderTextAlign="Center" >
                        </f:RenderField>

                           <f:RenderField ColumnID="MainSendUnitIds" Width="200px" DataField="MainSendUnitIds"
                            SortField="MainSendUnitIds" FieldType="String" HeaderText="主送单位" TextAlign="Center"
                            HeaderTextAlign="Center" >
                        </f:RenderField>

                          <f:RenderField ColumnID="CCUnitIds" Width="160px" DataField="CCUnitIds"
                            SortField="CCUnitIds" FieldType="String" HeaderText="抄送单位" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                          <f:RenderField ColumnID="IsReply" Width="50px" DataField="IsReply"
                            SortField="IsReply" FieldType="String" HeaderText="答复" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                           <f:RenderField ColumnID="Cause" Width="70px" DataField="Cause"
                            SortField="Cause" FieldType="String" HeaderText="事由" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                     
                        <f:RenderField Width="100px" ColumnID="CompileDate" DataField="CompileDate" SortField="CompileDate"
                            FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="编制日期" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="70px" ColumnID="userName" DataField="userName" SortField="userName"
                            FieldType="String" HeaderText="发起人" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:TemplateField ColumnID="State" Width="100px" HeaderText="审批状态" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# BLL.WorkContactService.ConvertState(Eval("State")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:TemplateField ColumnID="AuditMan" Width="60px" HeaderText="办理人" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="Label41" runat="server" Text='<%# BLL.WorkContactService.ConvertMan(Eval("WorkContactId")) %>'></asp:Label>
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
        </f:panel>
        <f:window id="Window1" title="工作联系单" hidden="true" enableiframe="true" enablemaximize="true"
            target="Parent" enableresize="true" runat="server" ismodal="true"  OnClose="Window1_Close"
            width="850px" height="500px">
        </f:window>
           <f:Window ID="window_tt" Title="工作联系单" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true"  OnClose="window_tt_Close"
            Width="1300px" Height="660px">
        </f:Window>
        <f:menu id="Menu1" runat="server">
            <Items>
                <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Hidden="true" Text="修改" Icon="Pencil"
                 OnClick="btnMenuModify_Click"   >
                </f:MenuButton>
                <f:MenuButton ID="btnMenuView" EnablePostBack="true" runat="server" Text="查看" Icon="ApplicationViewIcons"
                    OnClick="btnMenuView_Click"  >
                </f:MenuButton>
                <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server" Hidden="true" Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？"
                  OnClick="btnMenuDel_Click"  >
                </f:MenuButton>
            </Items>
        </f:menu>
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

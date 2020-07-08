<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PunishNoticeStatistics.aspx.cs" Inherits="FineUIPro.Web.HSSE.Check.PunishNoticeStatistics" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>处罚单统计</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="处罚单统计" EnableCollapse="true"
                runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="UnitId" DataIDField="UnitId" AllowSorting="true"
                SortField="UnitCode" SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" ForceFit="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                 EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>            
                            <f:RadioButtonList runat="server" ID="rbState" Label="状态"  Width="350px" 
                                LabelAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="rbState_SelectedIndexChanged">
                                <f:RadioItem Value="0" Text="全部" Selected="true" />
                                <f:RadioItem Value="1"  Text="已审核" />
                                <f:RadioItem Value="2" Text="已闭环" />
                            </f:RadioButtonList>                          
                            <f:DropDownList ID="drpUnitId" runat="server" Label="单位" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged"  LabelWidth="70px" Width="250px">
                            </f:DropDownList>             
                            <f:DatePicker ID="txtStartDate" runat="server" Label="处罚时间" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"
                                Width="210px" EmptyText="开始时间" LabelAlign="Right">
                            </f:DatePicker>
                            <f:Label ID="lblTo" runat="server" Text="至">
                            </f:Label>
                            <f:DatePicker ID="txtEndDate" runat="server" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"
                                Width="120px" EmptyText="结束时间">
                            </f:DatePicker>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>           
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:TemplateField ColumnID="tfNumber" Width="100px" HeaderText="序号" HeaderTextAlign="Center"
                        TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>    
                    <f:RenderField Width="150px" ColumnID="UnitCode" DataField="UnitCode" SortField="UnitCode"
                        FieldType="String" HeaderText="单位代码" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>                
                    <f:RenderField Width="400px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                        FieldType="String" HeaderText="单位名称" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>                 
                    <f:RenderField Width="250px" ColumnID="PunishMoney" DataField="PunishMoney" SortField="PunishMoney"
                        FieldType="Float" HeaderText="处罚金额" HeaderTextAlign="Center" TextAlign="Right">
                    </f:RenderField>                   
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
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>    
    </form>
    <script type="text/javascript">
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
           // F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendCard.aspx.cs" Inherits="FineUIPro.Web.HSSE.SitePerson.SendCard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>发卡管理</title>
  <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />   
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="发卡管理" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="PersonId" DataIDField="PersonId" AllowSorting="true" SortField="PersonName" SortDirection="ASC"
                OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" PageSize="10" EnableColumnLines="true"
                OnPageIndexChange="Grid1_PageIndexChange" EnableTextSelection="True" ForceFit="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
                        <Items>
                            <f:TextBox runat="server" Label="姓名" ID="txtPersonName" EmptyText="输入查询条件" 
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="单位" ID="txtUnitName" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server"></f:ToolbarSeparator> 
                            <f:CheckBoxList runat="server" ID="cbIsSend" ShowLabel="false" Width="200px"  LabelAlign="Right"
                                OnSelectedIndexChanged="TextBox_TextChanged"  AutoPostBack="true">
                                <f:CheckItem Text="未发卡" Value="0" Selected="true"/>
                                <f:CheckItem Text="已发卡" Value="1"  />
                             </f:CheckBoxList>  
                                <f:CheckBoxList runat="server" ID="ckPrint" ShowLabel="false" Width="200px"  LabelAlign="Right"
                                OnSelectedIndexChanged="TextBox_TextChanged"  AutoPostBack="true" Hidden="true">
                                <f:CheckItem Text="未打印" Value="0" Selected="true"/>
                                <f:CheckItem Text="已打印" Value="1"  />
                             </f:CheckBoxList>  
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>                                                     
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                    EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>                  
                     <f:TemplateField ColumnID="tfNumber" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="labNumber" runat="server" Text=' <%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1%>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>  
                    <f:RenderField Width="100px" ColumnID="PersonName" DataField="PersonName" SortField="PersonName"
                        FieldType="String" HeaderText="人员姓名" HeaderTextAlign="Center"
                        TextAlign="Center">                        
                    </f:RenderField>
                     <f:RenderField Width="100px" ColumnID="CardNo" DataField="CardNo" SortField="CardNo"
                        FieldType="String" HeaderText="卡号" HeaderTextAlign="Center"
                        TextAlign="Left">                        
                    </f:RenderField>
                      <f:TemplateField ColumnID="tfI" HeaderText="身份证号" Width="170px" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lbI" runat="server" Text=' <%# Bind("IdentityCard") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField> 
                    <f:RenderField Width="250px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                        FieldType="String" HeaderText="所属单位" HeaderTextAlign="Center" 
                        TextAlign="Left">                        
                    </f:RenderField>
                     <f:RenderField HeaderText="岗位名称" ColumnID="WorkPostName" DataField="WorkPostName" SortField="WorkPostName"
                            FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="120px">                               
                    </f:RenderField>
                     <f:RenderField HeaderText="单位工程" ColumnID="WorkAreaName" DataField="WorkAreaName" SortField="WorkAreaName"
                            FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="120px">                               
                    </f:RenderField>
                    <f:TemplateField  ColumnID="tflTrainResult" Width="100px" HeaderText="培训结果" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblTrainResult" runat="server" Text='<%# ConvertTrainResult(Eval("PersonId")) %>'></asp:Label>
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
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="发卡" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="500px" Height="380px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnSendCard" OnClick="btnSendCard_Click" EnablePostBack="true"
             runat="server" Icon="Vcard" Text="发卡">
        </f:MenuButton>
        <f:MenuButton ID="btnPrint" OnClick="btnPrint_Click" EnableAjax="false" DisableControlBeforePostBack="false"
             runat="server" Icon="Printer" Text="导出" Hidden="true">
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

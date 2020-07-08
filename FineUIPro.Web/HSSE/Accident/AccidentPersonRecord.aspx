<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccidentPersonRecord.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Accident.AccidentPersonRecord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HSE事故(对人员)记录</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="HSE事故(对人员)记录" EnableCollapse="true"
                runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="AccidentPersonRecordId" ForceFit="true"
                DataIDField="AccidentPersonRecordId" AllowSorting="true" SortField="AccidentDate" SortDirection="DESC" 
                OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" PageSize="10"  EnableTextSelection="True"
                OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" >
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox runat="server" Label="事故类型" ID="txtAccidentTypeName" EmptyText="输入查询条件"
                                Width="250px" LabelWidth="80px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:DatePicker ID="txtStartDate" runat="server" Label="发生时间" 
                                Width="250px" EmptyText="开始时间" LabelAlign="Right">
                            </f:DatePicker>
                            <f:Label ID="lblTo" runat="server" Text="至">
                            </f:Label>
                            <f:DatePicker ID="txtEndDate" runat="server" 
                                Width="150px" EmptyText="结束时间">
                            </f:DatePicker>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>                            
                            <f:Button ID="btnQuery" ToolTip="查询" Icon="SystemSearch" EnablePostBack="true" 	
                                OnClick="TextBox_TextChanged" runat="server" >
                            </f:Button>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" Hidden="true"
                                runat="server">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                        TextAlign="Center" />
                    <f:RenderField Width="120px" ColumnID="AccidentTypeName" DataField="AccidentTypeName"
                        SortField="AccidentTypeName" FieldType="String" HeaderText="事故类型" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="WorkAreaName" DataField="WorkAreaName" SortField="WorkAreaName"
                        FieldType="String" HeaderText="发生区域" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="AccidentDate" DataField="AccidentDate" SortField="AccidentDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="发生时间"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="PersonName" DataField="PersonName" SortField="PersonName"
                        FieldType="String" HeaderText="人员" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="CardNo" DataField="CardNo" SortField="CardNo"
                        FieldType="String" HeaderText="卡号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
               <%--     <f:RenderField Width="160px" ColumnID="IdentityCard" DataField="IdentityCard" SortField="IdentityCard"
                        FieldType="String" HeaderText="身份证号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>--%>
                    <f:RenderField Width="120px" ColumnID="WorkPostName" DataField="WorkPostName" SortField="WorkPostName"
                        FieldType="String" HeaderText="岗位" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:TemplateField Width="100px" HeaderText="伤害情况" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblInjury" runat="server" Text='<%#ConvertInjury(Eval("Injury")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                   <%-- <f:RenderField Width="100px" ColumnID="InjuryPart" DataField="InjuryPart" SortField="InjuryPart"
                        FieldType="String" HeaderText="受伤部位" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="InjuryResult" DataField="InjuryResult" SortField="InjuryResult"
                        FieldType="String" HeaderText="事故经过及原因" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="180px" ColumnID="PreventiveAction" DataField="PreventiveAction"
                        SortField="PreventiveAction" FieldType="String" HeaderText="预防事故重复发生的措施" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                   <f:RenderField Width="150px" ColumnID="HandleOpinion" DataField="HandleOpinion" SortField="HandleOpinion"
                        FieldType="String" HeaderText="处理意见" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>--%>
                    <f:RenderField Width="100px" ColumnID="HssePersons" DataField="HssePersons" SortField="HssePersons"
                        FieldType="String" HeaderText="责任区安全员" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="FlowOperateName" DataField="FlowOperateName"
                        SortField="FlowOperateName" FieldType="String" HeaderText="状态" HeaderTextAlign="Center"
                        TextAlign="Left">
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
    <f:Window ID="Window1" Title="HSE事故(对人员)记录" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="1200px"
        Height="600px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="Pencil" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server"
            Text="删除">
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

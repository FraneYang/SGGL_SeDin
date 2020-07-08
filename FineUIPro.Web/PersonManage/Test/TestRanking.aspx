<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestRanking.aspx.cs" Inherits="FineUIPro.Web.PersonManage.Test.TestRanking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>答题记录</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
       .f-grid-row.Red {
             background-color: red;
         } 
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="答题记录" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="TestRecordId" EnableColumnLines="true" DataIDField="TestRecordId" 
                AllowSorting="true" SortField="TestScores" SortDirection="DESC"  OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange" 
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server" >
                        <Items>          
                            <f:DropDownList runat="server" ID="drpPlan" Label="名称"  AutoPostBack="true" EnableEdit="true"
                                    OnSelectedIndexChanged="TextBox_TextChanged" LabelWidth="60px" Width="250px" LabelAlign="Right"></f:DropDownList>
                           <f:CheckBoxList runat="server" ID="cblUserType" Label="人员" LabelWidth="60px" LabelAlign="Right" ShowLabel="false"
                               AutoPostBack="true"  OnSelectedIndexChanged="TextBox_TextChanged">
                                    <f:CheckItem Text="管理人员" Value="1" />
                                    <f:CheckItem Text="临时用户" Value="2" />
                                    <f:CheckItem Text="作业人员" Value="3" />
                                </f:CheckBoxList>
                              <f:TextBox ID="txtName" runat="server" Label="查询" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="350px" LabelWidth="60px" LabelAlign="Right">
                            </f:TextBox>
                            <f:CheckBox runat="server" ID="isNoT" Label="非本单位" Width="100px" LabelWidth="90px" LabelAlign="Right"
                                AutoPostBack="true" OnCheckedChanged="isNoT_CheckedChanged"> </f:CheckBox>
                            <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>
                        </Items>
                    </f:Toolbar>
                  <f:Toolbar ID="Toolbar2" Position="Top" runat="server" >
                      <Items>
                          <f:DropDownList runat="server" ID="drpUnit" Label="单位"  AutoPostBack="true" EnableEdit="true"
                                    OnSelectedIndexChanged="TextBox_TextChanged" LabelWidth="60px" Width="350px" LabelAlign="Right"></f:DropDownList>
                            <f:DropDownList runat="server" ID="drpDepart" Label="部门"  AutoPostBack="true" EnableEdit="true"
                                    OnSelectedIndexChanged="TextBox_TextChanged" LabelWidth="60px" Width="200px" LabelAlign="Right"></f:DropDownList>
                            <f:DropDownList runat="server" ID="drpProject" Label="项目"  AutoPostBack="true" EnableEdit="true"
                                    OnSelectedIndexChanged="TextBox_TextChanged" LabelWidth="60px" Width="350px" LabelAlign="Right"></f:DropDownList>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                          <f:Button ID="btnOut" OnClick="btnMenuOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                      </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:TemplateField ColumnID="tfNumber" HeaderText="排名" Width="60px" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="labNumber" runat="server" Text=' <%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1%>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>                     
                    <f:RenderField Width="250px" ColumnID="UnitName" DataField="UnitName" FieldType="String"
                        HeaderText="单位" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="TestManName" DataField="TestManName" FieldType="String"
                        HeaderText="姓名" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="ManTypeName" DataField="ManTypeName" FieldType="String"
                        HeaderText="类型" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="TestStartTime" DataField="TestStartTime" FieldType="String"
                        HeaderText="考试开始时间" HeaderTextAlign="Center" TextAlign="Left" SortField="TestStartTime">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="TestEndTime" DataField="TestEndTime" FieldType="String"
                        HeaderText="考试结束时间" HeaderTextAlign="Center" TextAlign="Left" SortField="TestEndTime">
                    </f:RenderField>  
                     <f:RenderField Width="80px" ColumnID="TestScores" DataField="TestScores" FieldType="String"
                        HeaderText="成绩" HeaderTextAlign="Center" TextAlign="Right" SortField="TestScores">
                    </f:RenderField>
                      <f:RenderField Width="120px" ColumnID="UseTimes" DataField="UseTimes" FieldType="String"
                        HeaderText="用时" HeaderTextAlign="Center" TextAlign="Right" SortField="UserTimesD">
                    </f:RenderField>
                    <%--<f:RenderField Width="110px" ColumnID="DepartName" DataField="DepartName" FieldType="String"
                        HeaderText="部门" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>--%>
                      <f:RenderField Width="300px" ColumnID="DProjectName" DataField="DProjectName" FieldType="String"
                        HeaderText="部门/项目" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                     <f:RenderField Width="200px" ColumnID="IdentityCard" DataField="IdentityCard" FieldType="String"
                        HeaderText="身份证号码" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="120px" ColumnID="Telephone" DataField="Telephone" FieldType="String"
                        HeaderText="电话" HeaderTextAlign="Center" TextAlign="Left">
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
    <f:Window ID="Window1" Title="答题记录查看" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true"
        Width="1200px" Height="560px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuView" OnClick="btnMenuView_Click" EnablePostBack="true"
            runat="server" Text="查看" Icon="Find">
        </f:MenuButton>
        <f:MenuButton ID="btnPrint" OnClick="btnPrint_Click" EnablePostBack="true"
            runat="server" Text="打印" Icon="Printer">
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

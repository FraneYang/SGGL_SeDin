<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestRecord.aspx.cs" Inherits="FineUIPro.Web.HSSE.EduTrain.TestRecord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考试记录</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="考试记录" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="TestRecordId" EnableColumnLines="true" DataIDField="TestRecordId" AllowSorting="true"
                SortField="TestStartTime" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" ForceFit="true"
                IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                        <Items>                            
                            <f:TextBox ID="txtName" runat="server" Label="查询" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="180px" LabelWidth="60px" LabelAlign="Right">
                            </f:TextBox>
                            <f:NumberBox runat="server" ID="txtMinScores" Label="分值范围" AutoPostBack="true" NoNegative="true"
                                OnTextChanged="TextBox_TextChanged" Width="180px" LabelWidth="80px" LabelAlign="Right"></f:NumberBox>
                            <f:NumberBox runat="server" ID="txtMaxScores" Label="至" AutoPostBack="true" NoNegative="true"
                                OnTextChanged="TextBox_TextChanged" Width="130px" LabelWidth="30px" LabelAlign="Right"></f:NumberBox>
                            <f:CheckBox runat="server" Label="临时考生" ID="IsTemp" AutoPostBack="true" Hidden="true"
                                    OnCheckedChanged="IsTemp_CheckedChanged" LabelWidth="80px" Width="90px">
                            </f:CheckBox>
                              <f:DatePicker ID="txtStartDate" runat="server" Width="160px" LabelWidth="60px" LabelAlign="Right" Label="时间"
                                EnableEdit="false" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                            </f:DatePicker>
                            <f:DatePicker ID="txtEndDate" runat="server" Width="150px" LabelAlign="Right" LabelWidth="30px"
                                EnableEdit="false"  Label="至" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                            </f:DatePicker>
                            <f:CheckBox runat="server" Label="时间空" ID="ckIsNULL" AutoPostBack="true" 
                                    OnCheckedChanged="IsTemp_CheckedChanged" LabelWidth="70px" Width="80px">
                            </f:CheckBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                             <f:Button ID="btnOut" OnClick="btnMenuOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
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
                    <f:RenderField Width="100px" ColumnID="PlanName" DataField="PlanName" FieldType="String"
                        HeaderText="考试名称" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="230px" ColumnID="UnitName" DataField="UnitName" FieldType="String"
                        HeaderText="单位" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                    <f:RenderField Width="95px" ColumnID="TestManName" DataField="TestManName" FieldType="String"
                        HeaderText="考生" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="145px" ColumnID="TestStartTime" DataField="TestStartTime" FieldType="String"
                        HeaderText="考试开始时间" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="145px" ColumnID="TestEndTime" DataField="TestEndTime" FieldType="String"
                        HeaderText="考试结束时间" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>                   
                     <f:RenderField Width="70px" ColumnID="TestScores" DataField="TestScores" FieldType="String"
                        HeaderText="成绩" HeaderTextAlign="Center" TextAlign="Right">
                    </f:RenderField>
                     <f:RenderField Width="70px" ColumnID="TotalScore" DataField="TotalScore" FieldType="String"
                        HeaderText="总分" HeaderTextAlign="Center" TextAlign="Right">
                    </f:RenderField>
                     <f:RenderField Width="70px" ColumnID="Duration" DataField="Duration" FieldType="String"
                        HeaderText="时长" HeaderTextAlign="Center" TextAlign="Right">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="QuestionCount" DataField="QuestionCount" FieldType="String"
                        HeaderText="题目数量" HeaderTextAlign="Center" TextAlign="Right">
                    </f:RenderField>
                   <%-- <f:RenderField Width="150px" ColumnID="TestPalce" DataField="TestPalce" FieldType="String"
                        HeaderText="考试地点" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField> --%>                   
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
    <f:Window ID="Window1" Title="考试记录查看" Hidden="true" EnableIFrame="true" EnableMaximize="true"
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
        <%--<f:MenuButton ID="btnFile" OnClick="btnMenuFile_Click" EnablePostBack="true"
            runat="server" Text="归档" Icon="FolderPage">
        </f:MenuButton>--%>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除"
            Hidden="true">
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

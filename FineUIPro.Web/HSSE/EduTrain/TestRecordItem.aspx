<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestRecordItem.aspx.cs" Inherits="FineUIPro.Web.HSSE.EduTrain.TestRecordItem" %>

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
                runat="server" BoxFlex="1" DataKeyNames="TestRecordItemId" 
                EnableColumnLines="true" DataIDField="TestRecordItemId" AllowSorting="true"
                SortField="TestType,TrainingItemCode" SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                        <Items>
                            <f:TextBox ID="txtName" runat="server" Label="查询" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="350px" LabelWidth="80px" LabelAlign="Right">
                            </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                             <f:Button ID="btnAttachUrl"  ToolTip="考试图片查看" Icon="TableCell" runat="server"
                                    OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                                </f:Button>
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
                    <f:RenderField Width="75px" ColumnID="TestTypeName" DataField="TestTypeName" FieldType="String"
                        HeaderText="题型" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>                        
                    <f:RenderField Width="300px" ColumnID="Abstracts" DataField="Abstracts" FieldType="String"
                        HeaderText="试题内容" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                    </f:RenderField>
                        <f:RenderField Width="90px" ColumnID="AItem" DataField="AItem" FieldType="String"
                        HeaderText="答案项A" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="BItem" DataField="BItem" FieldType="String"
                        HeaderText="答案项B" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>   
                        <f:RenderField Width="90px" ColumnID="CItem" DataField="CItem" FieldType="String"
                        HeaderText="答案项C" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>   
                        <f:RenderField Width="90px" ColumnID="DItem" DataField="DItem" FieldType="String"
                        HeaderText="答案项D" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>   
                        <f:RenderField Width="80px" ColumnID="EItem" DataField="EItem" FieldType="String"
                        HeaderText="答案项E" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>   
                   <f:RenderField Width="85px" ColumnID="AnswerItems" DataField="AnswerItems" FieldType="String"
                        HeaderText="正确答案" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField> 
                    <f:RenderField Width="70px" ColumnID="Score" DataField="Score" FieldType="Int"
                        HeaderText="分值" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField> 
                      <f:RenderField Width="80px" ColumnID="SelectedItem" DataField="SelectedItem" FieldType="String"
                        HeaderText="选择项" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                      <f:RenderField Width="70px" ColumnID="SubjectScore" DataField="SubjectScore" FieldType="Int"
                        HeaderText="得分" HeaderTextAlign="Center" TextAlign="Left">
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
    <f:Window ID="Window1" Title="考试记录查看" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server"  IsModal="true"
        Width="900px" Height="500px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuView" OnClick="btnMenuView_Click" EnablePostBack="true"
            runat="server" Text="查看" Icon="TableGo">
        </f:MenuButton>
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

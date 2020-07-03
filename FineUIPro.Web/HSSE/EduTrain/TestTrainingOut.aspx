<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestTrainingOut.aspx.cs" Inherits="FineUIPro.Web.HSSE.EduTrain.TestTrainingOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>培训试题</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />  
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="考试试题库" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="TrainingItemId"  EnableColumnLines="true" ForceFit="true"
                DataIDField="TrainingItemId" AllowSorting="true" SortField="TrainingCode,TrainingItemCode"
                SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                PageSize="15" OnPageIndexChange="Grid1_PageIndexChange" Width="980px" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server"  ToolbarAlign="Right">
                         <Items>   
                            <f:Button ID="btnOut" OnClick="btnMenuOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>                             
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>                   
                     <f:RenderField Width="90px" ColumnID="TrainingCode" DataField="TrainingCode" FieldType="String"
                        HeaderText="类型编号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="TrainingName" DataField="TrainingName" FieldType="String"
                        HeaderText="试题类型" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="TrainingItemCode" DataField="TrainingItemCode" FieldType="String"
                        HeaderText="试题编号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>                   
                     <f:RenderField Width="80px" ColumnID="TestTypeName" DataField="TestTypeName" FieldType="String"
                        HeaderText="题型" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                      <f:RenderField Width="150px" ColumnID="WorkPostNames" DataField="WorkPostNames" FieldType="String"
                        HeaderText="适合岗位" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="300px" ColumnID="Abstracts" DataField="Abstracts" FieldType="String"
                        HeaderText="试题内容" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>  
                    <f:RenderField Width="150px" ColumnID="AItem" DataField="AItem" FieldType="String"
                        HeaderText="答案项A" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>  
                    <f:RenderField Width="150px" ColumnID="BItem" DataField="BItem" FieldType="String"
                        HeaderText="答案项B" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>  
                    <f:RenderField Width="150px" ColumnID="CItem" DataField="CItem" FieldType="String"
                        HeaderText="答案项C" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>  
                    <f:RenderField Width="150px" ColumnID="DItem" DataField="DItem" FieldType="String"
                        HeaderText="答案项D" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>  
                    <f:RenderField Width="150px" ColumnID="EItem" DataField="EItem" FieldType="String"
                        HeaderText="答案项E" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>  
                    <f:RenderField Width="80px" ColumnID="AnswerItems" DataField="AnswerItems" FieldType="String"
                        HeaderText="答案项" HeaderTextAlign="Center" TextAlign="Left">
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
    <script type="text/jscript">
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

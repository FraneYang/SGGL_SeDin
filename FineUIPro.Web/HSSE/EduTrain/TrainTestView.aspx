<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainTestView.aspx.cs" Inherits="FineUIPro.Web.HSSE.EduTrain.TrainTestView" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>培训试题</title>
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
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" Title="培训试题" Layout="Fit"
            ShowHeader="false">
            <Items>
                <f:Grid ID="Grid1" Title="培训试题" ShowHeader="false" EnableCollapse="true" PageSize="500" ForceFit="true"
                    ShowBorder="true" AllowPaging="false" IsDatabasePaging="true" runat="server" DataKeyNames="TrainTestId"
                    DataIDField="TrainTestId"  SortField="COrder" SortDirection="ASC" EnableTextSelection="True" EnableColumnLines="true">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar3" Position="Top" runat="server">
                            <Items>
                                <f:TextBox ID="txtQsnContent" runat="server" Label="内容" EmptyText="输入查询条件" AutoPostBack="true"
                                    OnTextChanged="TextBox_TextChanged" Width="300px" LabelWidth="70px" LabelAlign="Right">
                                </f:TextBox>
                                   <f:ToolbarFill runat="server"></f:ToolbarFill>
                                <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                    EnableAjax="false" DisableControlBeforePostBack="false">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>                       
                        <f:RenderField Width="60px" ColumnID="COrder" DataField="COrder" FieldType="String"
                            HeaderText="序号" HeaderTextAlign="Center" TextAlign="Left" SortField="COrder">
                        </f:RenderField>
                        <f:RenderField Width="500px" ColumnID="QsnContent" DataField="QsnContent" FieldType="String"
                            HeaderText="试题内容" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="90px" ColumnID="QsnAnswer" DataField="QsnAnswer" FieldType="String"
                            HeaderText="正确答案" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="90px" ColumnID="Description" DataField="Description" FieldType="String"
                            HeaderText="勾选项" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="QsnCategoryName" DataField="QsnCategoryName" FieldType="String"
                            HeaderText="试题分类" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="QsnKindName" DataField="QsnKindName" FieldType="String"
                            HeaderText="试题类型" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="QsnImportantName" DataField="QsnImportantName" FieldType="String"
                            HeaderText="重要程度" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="150px" ColumnID="Analysis" DataField="Analysis" FieldType="String"
                            HeaderText="试题解析" HeaderTextAlign="Center" TextAlign="Left" >
                        </f:RenderField>
                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestTrainingItemIn.aspx.cs" Inherits="FineUIPro.Web.HSSE.EduTrain.TestTrainingItemIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导入考试试题</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />   
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" OnCustomEvent="PageManager1_CustomEvent" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Toolbars>
            <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                <Items>                  
                    <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                    <f:Button ID="btnAudit" Icon="ApplicationEdit" runat="server" ToolTip="数据导入" ValidateForms="SimpleForm1"
                        OnClick="btnAudit_Click">
                    </f:Button>  
                    <f:Button ID="btnDownLoad" runat="server" Icon="ApplicationGo" ToolTip="下载模板" OnClick="btnDownLoad_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Rows>
            <f:FormRow>
                <Items>
                    <f:FileUpload runat="server" ID="fuAttachUrl" EmptyText="选择要导入的文件" Label="选择要导入的文件"
                        LabelWidth="150px">
                    </f:FileUpload>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        EnableColumnLines="true" BoxFlex="1" DataKeyNames="TrainingItemId"  ForceFit="true"
                        DataIDField="TrainingItemId" AllowSorting="true" SortField="TrainingCode,TrainingItemCode"
                        PageSize="50" Height="360px">
                        <Columns>
                            <f:TemplateField Width="55px" HeaderText="序号">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>                              
                            <f:RenderField Width="90px" ColumnID="TrainingCode" DataField="TrainingCode" FieldType="String"
                                HeaderText="类型编号" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="110px" ColumnID="TrainingName" DataField="TrainingName" FieldType="String"
                                HeaderText="试题类型" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                             <f:RenderField Width="80px" ColumnID="TrainingItemCode" DataField="TrainingItemCode" FieldType="String"
                                HeaderText="编号" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="TestTypeName" DataField="TestTypeName" FieldType="String"
                                HeaderText="题型" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField> 
                             <f:RenderField Width="140px" ColumnID="WorkPostNames" DataField="WorkPostNames" FieldType="String"
                                HeaderText="适合岗位" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                             <f:RenderField Width="200px" ColumnID="Abstracts" DataField="Abstracts" FieldType="String"
                                HeaderText="试题内容" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                             <f:RenderField Width="80px" ColumnID="AItem" DataField="AItem" FieldType="String"
                                HeaderText="答案项A" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="BItem" DataField="BItem" FieldType="String"
                                HeaderText="答案项B" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>   
                             <f:RenderField Width="80px" ColumnID="CItem" DataField="CItem" FieldType="String"
                                HeaderText="答案项C" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>   
                             <f:RenderField Width="80px" ColumnID="DItem" DataField="DItem" FieldType="String"
                                HeaderText="答案项D" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>   
                             <f:RenderField Width="80px" ColumnID="EItem" DataField="EItem" FieldType="String"
                                HeaderText="答案项E" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>   
                             <f:RenderField Width="90px" ColumnID="AnswerItems" DataField="AnswerItems" FieldType="String"
                                HeaderText="正确答案项" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>   
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:HiddenField ID="hdFileName" runat="server">
                    </f:HiddenField>
                    <f:HiddenField ID="hdCheckResult" runat="server">
                    </f:HiddenField>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label ID="lblBottom" runat="server" Text="说明：1 导入模板为.xls后缀的EXCEL文件，黑体字为必填项。2 对于导入信息中重复记录自动过滤插入一条记录。3 题型为单选题、多选题、判断题，多岗位时用,隔开。4 适合岗位空时为通用试题。 5 数据导入完成，成功后自动返回，如果有不成功数据页面弹出提示框，列表显示导入成功数据。">
                    </f:Label>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    <f:Window ID="Window1" Title="导入信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>
    </form>
</body>
</html>

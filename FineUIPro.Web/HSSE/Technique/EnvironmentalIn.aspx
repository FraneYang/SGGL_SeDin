<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnvironmentalIn.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Technique.EnvironmentalIn" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导入环境因素危险源</title>
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
                    <f:FileUpload runat="server" ID="fuAttachUrl" EmptyText="选择要导入的文件" Label="选择要导入的文件"
                        LabelWidth="150px">
                    </f:FileUpload>
                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                    <f:Button ID="btnAudit" Icon="ApplicationEdit" runat="server" ToolTip="审核" ValidateForms="SimpleForm1"
                        OnClick="btnAudit_Click">
                    </f:Button>
                    <f:Button ID="btnImport" Icon="ApplicationGet" runat="server" ToolTip="导入" ValidateForms="SimpleForm1"
                        OnClick="btnImport_Click">
                    </f:Button>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnDownLoad" runat="server" Icon="ApplicationGo" ToolTip="下载模板" OnClick="btnDownLoad_Click" >
                    </f:Button>
                    <f:Button ID="btnHelp" runat="server" ToolTip="点击下载本页面使用说明" Icon="Help" Text="帮助">
                        <Listeners>
                            <f:Listener Event="click" Handler="onToolSourceCodeClick" />
                        </Listeners>
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Rows>    
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        EnableColumnLines="true" BoxFlex="1" DataKeyNames="EnvironmentalId" AllowCellEditing="true"
                        ClicksToEdit="2" DataIDField="EnvironmentalId" AllowSorting="true" SortField="Code"
                        PageSize="12" Height="400px">
                        <Columns>
                            <f:TemplateField Width="55px" HeaderText="序号" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="100px" ColumnID="Code" DataField="Code" FieldType="String" HeaderText="危险源代码"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:TemplateField Width="120px" HeaderText="危险源类型" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSmallType" runat="server" Text='<%# ConvertSmallType(Eval("SmallType")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="120px" HeaderText="环境类型" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# ConvertEType(Eval("EType")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="200px" ColumnID="ActivePoint" DataField="ActivePoint" SortField="ActivePoint"
                                FieldType="String" HeaderText="分项工程/活动点" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="160px" ColumnID="EnvironmentalFactors" DataField="EnvironmentalFactors"
                                FieldType="String" HeaderText="环境因素" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:GroupField HeaderText="污染类" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="45px" ColumnID="AValue" DataField="AValue" FieldType="String"
                                        HeaderText="A值" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="45px" ColumnID="BValue" DataField="BValue" FieldType="String"
                                        HeaderText="B值" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="45px" ColumnID="CValue" DataField="CValue" FieldType="String"
                                        HeaderText="C值" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="45px" ColumnID="DValue" DataField="DValue" FieldType="String"
                                        HeaderText="D值" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="45px" ColumnID="EValue" DataField="EValue" FieldType="String"
                                        HeaderText="E值" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="45px" ColumnID="ZValue" DataField="ZValue" FieldType="String"
                                        HeaderText="Σ" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField HeaderText="能源资源类" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="45px" ColumnID="FValue" DataField="FValue" FieldType="String"
                                        HeaderText="F值" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="45px" ColumnID="GValue" DataField="GValue" FieldType="String"
                                        HeaderText="G值" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:TemplateField Width="45px" HeaderText="Σ" TextAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# ConvertZValue2(Eval("EnvironmentalId")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </f:TemplateField>
                                </Columns>
                            </f:GroupField>
                            <f:RenderField Width="120px" ColumnID="ControlMeasures" DataField="ControlMeasures"
                                FieldType="String" HeaderText="安全措施" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:HiddenField ID="hdFileName" runat="server">
                    </f:HiddenField>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label ID="lblBottom" runat="server" Text="说明：数字在EXCEl中输入框格式设置文本；同时建议不要一次导入很多行，数量较多时分段分次导入。">
                    </f:Label>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    <f:Window ID="Window1" Title="审核环境因素危险源" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="false" CloseAction="HidePostBack"
        Width="900px" Height="600px">
    </f:Window>
    <f:Window ID="Window2" Title="导入环境因素危险源" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" OnClose="Window2_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>
    </form>
    <script type="text/javascript">
        // 点击标题栏工具图标 - 查看源代码
        function onToolSourceCodeClick(event) {
            window.open('../Doc/环境因素危险源导入说明.doc', '_blank');
        }
    </script>
</body>
</html>

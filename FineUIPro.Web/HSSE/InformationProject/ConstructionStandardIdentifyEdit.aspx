<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConstructionStandardIdentifyEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.InformationProject.ConstructionStandardIdentifyEdit"
    ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑标准规范辨识</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="true" ShowHeader="false" Layout="VBox"
        Margin="5px" BodyPadding="5px">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="false" ShowHeader="false" Title="标准规范辨识" EnableCollapse="true"
                IsDatabasePaging="true" PageSize="10" AllowPaging="true" runat="server" BoxFlex="1" 
                DataKeyNames="StandardId" AllowCellEditing="true" OnPageIndexChange="Grid1_PageIndexChange"
                EnableColumnLines="true" ClicksToEdit="1" DataIDField="StandardId" AllowSorting="true"
                OnRowCommand="Grid1_RowCommand" AllowFilters="true" OnFilterChange="Grid1_FilterChange"
                EnableTextSelection="true" Height="350px">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                            <f:TextBox runat="server" EmptyText="输入查询条件" AutoPostBack="True" Label="标准级别" LabelWidth="80px"
                                Width="200px" ID="txtStandardGrade" OnTextChanged="TextBox_TextChanged">
                            </f:TextBox>
                            <f:TextBox runat="server" EmptyText="输入查询条件" AutoPostBack="True" Label="标准号" LabelWidth="70px"
                                Width="200px" ID="txtStandardNo" OnTextChanged="TextBox_TextChanged">
                            </f:TextBox>
                            <f:TextBox runat="server" EmptyText="输入查询条件" AutoPostBack="True" Label="标准名称" LabelWidth="80px"
                                Width="250px" ID="txtStandardName" OnTextChanged="TextBox_TextChanged">
                            </f:TextBox>
                             <f:DropDownList ID="drpCNProfessional" runat="server" Label="对应方案" AutoPostBack="true"
                                  Width="300px" LabelWidth="120px" LabelAlign="right" OnSelectedIndexChanged="TextBox_TextChanged">                            </f:DropDownList>
                            <f:CheckBox ID="ckbAll" runat="server" AutoPostBack="True" OnCheckedChanged="ckbAll_CheckedChanged"
                                Text="显示所有" LabelAlign="Right">
                            </f:CheckBox>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:CheckBoxField ColumnID="ckbIsSelected" Width="50px" RenderAsStaticField="false"
                        AutoPostBack="true" CommandName="IsSelected" HeaderText="选择" HeaderTextAlign="Center" />
                    <f:RenderField Width="100px" ColumnID="StandardGrade" DataField="StandardGrade" FieldType="String"
                        HeaderText="标准级别" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="StandardNo" DataField="StandardNo" FieldType="String"
                        HeaderText="标准号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="StandardName" DataField="StandardName" FieldType="String"
                        HeaderText="标准名称" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:GroupField EnableLock="true" HeaderText="对应方案" TextAlign="Center">
                        <Columns>
                            <f:RenderField Width="90px" ColumnID="IsSelected1" DataField="IsSelected1" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="01(地基处理)" HeaderToolTip="地基处理" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected2" DataField="IsSelected2" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="02(打桩)" HeaderToolTip="打桩" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected3" DataField="IsSelected3" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="03(基坑支护与降水工程)" HeaderToolTip="基坑支护与降水工程" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected4" DataField="IsSelected4" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="04(土方开挖工程)" HeaderToolTip="土方开挖工程" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected5" DataField="IsSelected5" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="05(模板工程)" HeaderToolTip="模板工程" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected6" DataField="IsSelected6" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="06(基础施工)" HeaderToolTip="基础施工" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected7" DataField="IsSelected7" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="07(钢筋混凝土结构)" HeaderToolTip="钢筋混凝土结构" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected8" DataField="IsSelected8" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="08(地下管道)" HeaderToolTip="地下管道" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected9" DataField="IsSelected9" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="09(钢结构)" HeaderToolTip="钢结构" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected10" DataField="IsSelected10" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="10(设备安装)" HeaderToolTip="设备安装" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected11" DataField="IsSelected11" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="11(大型起重吊装工程)" HeaderToolTip="大型起重吊装工程" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected12" DataField="IsSelected12" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="12(脚手架工程)" HeaderToolTip="脚手架工程" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected13" DataField="IsSelected13" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="13(机械安装)" HeaderToolTip="机械安装" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected14" DataField="IsSelected14" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="14(管道安装)" HeaderToolTip="管道安装" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected15" DataField="IsSelected15" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="15(电气仪表安装)" HeaderToolTip="电气仪表安装" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected16" DataField="IsSelected16" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="16(防腐保温防火)" HeaderToolTip="防腐保温防火" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected17" DataField="IsSelected17" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="17(拆除)" HeaderToolTip="拆除" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected18" DataField="IsSelected18" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="18(爆破工程)" HeaderToolTip="爆破工程" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected19" DataField="IsSelected19" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="19(试压)" HeaderToolTip="试压" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected20" DataField="IsSelected20" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="20(吹扫)" HeaderToolTip="吹扫" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected21" DataField="IsSelected21" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="21(试车)" HeaderToolTip="试车" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected22" DataField="IsSelected22" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="22(试车方案)" HeaderToolTip="试车方案" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected23" DataField="IsSelected23" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="23(无损检测)" HeaderToolTip="无损检测" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsSelected90" DataField="IsSelected90" FieldType="String"
                                RendererFunction="renderSelect" HeaderText="90(全部标准)" HeaderToolTip="全部标准" TextAlign="Center">
                            </f:RenderField>
                            <f:WindowField TextAlign="Center" Width="70px" WindowID="WindowAtt" HeaderText="原文"
                             Text="详细" ToolTip="查看" DataIFrameUrlFields="StandardId" DataIFrameUrlFormatString="../../AttachFile/webuploader.aspx?toKeyId={0}&type=-1&path=FileUpload/HSSEStandardsList&menuId=EFDSFVDE-RTHN-7UMG-4THA-5TGED48F8IOL"/>
                        </Columns>
                    </f:GroupField>
                </Columns>
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
        <Items>
            <f:TextBox ID="txtConstructionStandardIdentifyCode" runat="server" Label="编号" LabelAlign="Right"
                    MaxLength="30" Readonly="true">
                </f:TextBox>
                    <f:TextBox ID="txtRemark" runat="server" Label="备注" LabelAlign="Right" MaxLength="150">
                </f:TextBox>     
        </Items>
        <Items>
            <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true"
                BodyPadding="0px">
                <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
            </f:ContentPanel>
        </Items>
        <Toolbars>
            <f:Toolbar ID="Toolbar3" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                        OnClick="btnSubmit_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Panel>
    </form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <script type="text/jscript">
        function renderSelect(value) {
            return value == "True" ? '<font size="5">●</font>' : '';
        }
    </script>
</body>
</html>

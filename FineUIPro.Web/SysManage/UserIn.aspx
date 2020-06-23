<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserIn.aspx.cs" Inherits="FineUIPro.Web.SysManage.UserIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导入人员信息</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <style>
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" OnCustomEvent="PageManager1_CustomEvent" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Toolbars>
            <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnAudit" Icon="ApplicationEdit" runat="server" ToolTip="审核" ValidateForms="SimpleForm1"
                        OnClick="btnAudit_Click">
                    </f:Button>                 
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
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
                        EnableColumnLines="true" BoxFlex="1" DataKeyNames="RCount" AllowCellEditing="true"
                        ClicksToEdit="2" DataIDField="RCount" AllowSorting="true" SortField="RCount"
                        PageSize="50" Height="360px">
                        <Columns>
                            <f:TemplateField Width="50px" HeaderText="序号">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                              <f:RenderField Width="80px" ColumnID="RCount" DataField="RCount" FieldType="Int"
                                HeaderText="行号" HeaderTextAlign="Center" TextAlign="Right">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="UserCode" DataField="UserCode" FieldType="String"
                                HeaderText="用户编号" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="UserName" DataField="UserName"
                                SortField="UserName" FieldType="String" HeaderText="人员姓名" HeaderTextAlign="Center"
                                TextAlign="Left">
                            </f:RenderField>
                             <f:RenderField Width="80px" ColumnID="Account" DataField="Account"
                                SortField="Account" FieldType="String" HeaderText="账号" HeaderTextAlign="Center"
                                TextAlign="Left">
                            </f:RenderField>
                              <f:RenderField Width="220px" ColumnID="UnitName" DataField="UnitName" FieldType="String"
                                HeaderText="所属单位" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                             <f:RenderField Width="90px" ColumnID="RoleName" DataField="RoleName" FieldType="String"
                                HeaderText="角色" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>                          
                            <f:RenderField Width="200px" ColumnID="IdentityCard" DataField="IdentityCard" SortField="IdentityCard"
                                FieldType="String" HeaderText="身份证号" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                              <f:RenderField Width="120px" ColumnID="Telephone" DataField="Telephone" SortField="Telephone"
                                FieldType="String" HeaderText="手机号码" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:CheckBoxField Width="60px" SortField="IsPost" RenderAsStaticField="true" DataField="IsPost"
                                HeaderText="在岗" HeaderTextAlign="Center" TextAlign="Left">
                            </f:CheckBoxField>  
                               <f:CheckBoxField Width="60px" SortField="IsOffice" RenderAsStaticField="true" DataField="IsOffice"
                                HeaderText="本部" HeaderTextAlign="Center" TextAlign="Left">
                            </f:CheckBoxField>  
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
                    <f:Label ID="lblBottom" runat="server" Text="说明：1 人员信息导入模板为.xls后缀的EXCEL文件，黑体字为必填项。2 身份证号码必须为15或18位，所属单位、角色等必须与基础信息中对应类型的名称一致,否则无法导入。3 如需修改已有人员信息，请到系统中修改。4 数据审核后，点击“保存”，即可完成人员信息导入。">
                    </f:Label>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    <f:Window ID="Window1" Title="审核人员信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>
    <f:Window ID="Window2" Title="导入人员信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>
    </form>
</body>
</html>

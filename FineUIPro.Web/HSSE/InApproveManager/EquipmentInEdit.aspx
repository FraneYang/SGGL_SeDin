<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EquipmentInEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.InApproveManager.EquipmentInEdit" ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑特种设备机具入场报批</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .customlabel span
        {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtEquipmentInCode" runat="server" Label="编号" LabelAlign="Right" LabelWidth="70px"
                        MaxLength="50" Readonly="true">
                    </f:TextBox>
                    <f:DropDownList ID="drpUnitId" runat="server" Label="单位" Required="true" ShowRedStar="true" LabelWidth="70px"
                        LabelAlign="Right">
                    </f:DropDownList>
                           <f:TextBox ID="txtCarNumber" runat="server" Label="车牌号" LabelAlign="Right" MaxLength="50" LabelWidth="70px">
                    </f:TextBox>
                    <f:TextBox ID="txtSubProjectName" runat="server" Label="分包工程" LabelAlign="Right" LabelWidth="90px"
                        MaxLength="50">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtContentDef" runat="server" Label="施工内容简述" LabelWidth="110px"
                        LabelAlign="Right" MaxLength="500"
                        Height="50px">
                    </f:TextArea>
                        <f:TextArea ID="txtOtherDef" runat="server" Label="其它情况简述" LabelWidth="110px"
                            LabelAlign="Right" MaxLength="500"
                        Height="50px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="主要设备基本情况" EnableCollapse="true"
                        runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="EquipmentInItemId"
                        DataIDField="EquipmentInItemId" AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                        EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True"
                        Height="220px">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                                <Items>
                                    <f:Label ID="lblT" runat="server" Text="主要设备基本情况" CssClass="customlabel span">
                                    </f:Label>
                                    <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                    </f:ToolbarFill>
                                    <f:Button ID="btnNew" Icon="Add" runat="server" ToolTip="新增主要设备基本情况" ValidateForms="SimpleForm1"
                                        OnClick="btnNew_Click">
                                    </f:Button>
                                    <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                        EnableAjax="false" DisableControlBeforePostBack="false">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:TemplateField ColumnID="tfNumber" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                                TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField ColumnID="tfSpecialEquipmentId" HeaderText="设备" Width="100px" HeaderTextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecialEquipmentId" runat="server" Text='<%#ConvertEqiupment(Eval("SpecialEquipmentId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="100px" ColumnID="SizeModel" DataField="SizeModel" SortField="SizeModel"
                                FieldType="String" HeaderText="规格型号" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="OwnerCheck" DataField="OwnerCheck" SortField="OwnerCheck"
                                FieldType="String" HeaderText="进场前自查自检情况" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="CertificateNum" DataField="CertificateNum"
                                SortField="CertificateNum" FieldType="String" HeaderText="施工设备合格证号" HeaderTextAlign="Center"
                                TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="SafetyInspectionNum" DataField="SafetyInspectionNum"
                                SortField="SafetyInspectionNum" FieldType="String" HeaderText="安全检验合格证号" HeaderTextAlign="Center"
                                TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="DrivingLicenseNum" DataField="DrivingLicenseNum"
                                SortField="DrivingLicenseNum" FieldType="String" HeaderText="驾驶证号" HeaderTextAlign="Center"
                                TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="RegistrationNum" DataField="RegistrationNum"
                                SortField="RegistrationNum" FieldType="String" HeaderText="行驶证号" HeaderTextAlign="Center"
                                TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="OperationQualificationNum" DataField="OperationQualificationNum"
                                SortField="OperationQualificationNum" FieldType="String" HeaderText="操作资质证号"
                                HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="InsuranceNum" DataField="InsuranceNum" SortField="InsuranceNum"
                                FieldType="String" HeaderText="交险保单号" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="CommercialInsuranceNum" DataField="CommercialInsuranceNum"
                                SortField="CommercialInsuranceNum" FieldType="String" HeaderText="商业险保单号" HeaderTextAlign="Center"
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
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true"
                        BodyPadding="0px">
                        <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp">
                    </f:Label>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
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
    </f:Form>
    <f:Window ID="Window1" Title="主要设备基本情况" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="700px" Height="500px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="Pencil" EnablePostBack="true"
            runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除">
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

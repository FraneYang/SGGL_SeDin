<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LicenseManagerEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.License.LicenseManagerEdit" ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑现场作业许可证</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" 
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow ColumnWidths="34% 66%">
                    <Items>
                        <f:TextBox ID="txtLicenseManagerCode" runat="server" Label="许可证编号" LabelAlign="Right"
                            MaxLength="50" Readonly="true">
                        </f:TextBox>
                        <f:DropDownList ID="drpUnitId" runat="server" Label="申请单位" LabelAlign="Right" Required="true"
                            ShowRedStar="true" ForceSelection="false" EnableEdit="true" FocusOnPageLoad="true">
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow ColumnWidths="34% 33% 33%">
                    <Items>
                        <f:DropDownList ID="drpLicenseTypeId" runat="server" Label="许可证类型" LabelAlign="Right" EnableEdit="true"
                            Required="true" ShowRedStar="true" AutoPostBack="true" OnSelectedIndexChanged="drpLicenseTypeId_SelectedIndexChanged">
                        </f:DropDownList>
                           <f:DropDownList ID="drpWorkAreaId" runat="server" Label="作业区域" LabelAlign="Right" EnableEdit="true" EnableMultiSelect="true" EnableCheckBoxSelect="true" AutoSelectFirstItem="false">
                        </f:DropDownList>
                          <f:DropDownList ID="drpStates" runat="server" Label="状态" LabelAlign="Right" >
                              <f:ListItem Text="待开工"   Value="1"/>
                              <f:ListItem Text="作业中" Value="2"/>
                               <f:ListItem Text="已关闭" Value="3"/>
                              <f:ListItem Text="已取消" Value="-1"/>
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                     <f:DatePicker ID="txtStartDate" runat="server" Label="开始时间" LabelAlign="Right"
                            EnableEdit="true">
                        </f:DatePicker>
                        <f:DatePicker ID="txtEndDate" runat="server" Label="结束时间" LabelAlign="Right"
                            EnableEdit="true">
                        </f:DatePicker>
                        <f:TextBox ID="txtApplicantMan" runat="server" Label="申请人" LabelAlign="Right" MaxLength="50"></f:TextBox>
                        <f:DatePicker ID="txtCompileDate" runat="server" Label="申请日期" LabelAlign="Right"
                            EnableEdit="true">
                        </f:DatePicker>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:HtmlEditor runat="server" Label="许可证内容" ID="txtLicenseManageContents" ShowLabel="false"
                            Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="240" LabelAlign="Right">
                        </f:HtmlEditor>
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
                         <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                            OnClick="btnSubmit_Click">
                        </f:Button>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>                       
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
        <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
    </form>
</body>
</html>

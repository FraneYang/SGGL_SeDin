<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSETechnicalView.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.License.HSETechnicalView" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查看安全技术交底</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtHSETechnicalCode" runat="server" Label="编号" LabelAlign="Right"
                        Readonly="true">
                    </f:TextBox>
                       <f:TextBox ID="txtHSETechnicalDate" runat="server" Label="时间" LabelAlign="Right"
                        Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtUnitName" runat="server" Label="单位" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                     <f:TextBox ID="txtTeamGroupName" runat="server" Label="班组" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                       <f:TextBox ID="drpTechnicalMan" runat="server" Label="交底人" LabelAlign="Right" Readonly="true"></f:TextBox>
                    <f:Label runat="server" ID="lbHid"></f:Label>
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                        <f:TextBox ID="drpPartTechnicalMans" runat="server" Label="参加人员" Readonly="true">
                    </f:TextBox>
                    <f:TextBox  runat="server" ID="txtPartTechnicalManNames" Readonly="true" Label="补充人员"></f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtWorkContents" runat="server" Label="作业内容" LabelAlign="Right" Readonly="true" Height="50px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtAddress" runat="server" Label="地点" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
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
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>

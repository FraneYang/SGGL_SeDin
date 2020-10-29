<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthMeetingEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Meeting.MonthMeetingEdit" ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑安全月例会</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="安全月例会" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtMonthMeetingCode" runat="server" Label="会议编号" Readonly="true" LabelAlign="Right"
                        MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtMonthMeetingName" runat="server" Label="会议名称" Required="true" ShowRedStar="true"
                        LabelAlign="Right" MaxLength="50">
                    </f:TextBox>
                    <f:DatePicker ID="txtMonthMeetingDate" runat="server" Label="会议日期" LabelAlign="Right"
                        EnableEdit="true">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpCompileMan" runat="server" Label="整理人" LabelAlign="Right">
                    </f:DropDownList>
                    <f:NumberBox ID="txtMeetingHours" NoDecimal="false" NoNegative="true" MinValue="0"
                        runat="server" Label="时数" LabelAlign="Right">
                    </f:NumberBox>
                    <f:TextBox ID="txtMeetingHostMan" runat="server" Label="主持人" LabelAlign="Right" MaxLength="50">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="33% 67%">
                <Items>
                    <f:NumberBox ID="txtAttentPersonNum" NoDecimal="true" NoNegative="true" MinValue="0"
                        runat="server" Label="参会人数" ShowRedStar="true" Required="true" LabelAlign="Right">
                    </f:NumberBox>
                    <f:TextBox ID="txtAttentPerson" runat="server" Label="参会人员" LabelAlign="Right" MaxLength="300" EnableBlurEvent="true" OnBlur="txtAttentPerson_Blur">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:HtmlEditor runat="server" Label="会议内容" ID="txtMonthMeetingContents" ShowLabel="false"
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
                    <f:Button ID="btnAttachUrl" Text="内容" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button>
                        <f:Button ID="btnAttachUrl1" Text="签到表" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl1_Click" ValidateForms="SimpleForm1">
                    </f:Button>
                     <f:Button ID="btnAttachUrl2" Text="会议过程" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl2_Click" ValidateForms="SimpleForm1">
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
    <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>

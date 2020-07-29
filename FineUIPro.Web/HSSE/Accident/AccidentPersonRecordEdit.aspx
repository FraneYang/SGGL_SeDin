<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccidentPersonRecordEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Accident.AccidentPersonRecordEdit" ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑HSE事故(对人员)记录</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="HSE事故(对人员)记录"
        AutoScroll="true" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
        LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtProjectName" runat="server" Label="项目" LabelAlign="Right" MaxLength="50" LabelWidth="110px"
                        Readonly="true">
                    </f:TextBox>
                    <f:DropDownList ID="drpAccidentTypeId" runat="server" Label="事故类别" LabelAlign="Right"
                        EnableEdit="true" Required="true" ShowRedStar="true" LabelWidth="110px">
                    </f:DropDownList>
                     <f:DropDownList ID="drpWorkAreaId" runat="server" Label="单位工程" LabelAlign="Right"
                        EnableEdit="true" Required="true" ShowRedStar="true" LabelWidth="110px">
                    </f:DropDownList>
                    <f:DatePicker ID="txtAccidentDate" runat="server" Label="发生时间" LabelAlign="Right"
                        EnableEdit="true" Required="true" ShowRedStar="true" LabelWidth="110px">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpPersonId" runat="server" Label="人员姓名" LabelAlign="Right" EnableEdit="true"
                        Required="true" ShowRedStar="true" LabelWidth="110px">
                    </f:DropDownList>
                    <f:DropDownList ID="drpInjury" runat="server" Label="伤害情况" LabelAlign="Right" EnableEdit="true" LabelWidth="110px">
                    </f:DropDownList>
                        <f:TextBox ID="txtInjuryPart" runat="server" Label="受伤部位" LabelAlign="Right" MaxLength="50" LabelWidth="110px">
                    </f:TextBox>
                    <f:TextBox ID="txtHssePersons" runat="server" Label="责任区安全员" LabelAlign="Right" MaxLength="50" LabelWidth="110px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtInjuryResult" runat="server" Label="事故经过及原因" LabelAlign="Right"
                        MaxLength="500" Height="50px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtPreventiveAction" runat="server" Label="预防事故重复发生的措施" LabelAlign="Right"
                        MaxLength="500" Height="50px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtHandleOpinion" runat="server" Label="处理意见" LabelAlign="Right"
                        MaxLength="500" Height="50px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:HtmlEditor runat="server" Label="事故报告" ID="txtFileContents" ShowLabel="false"
                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="200" LabelAlign="Right">
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
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>

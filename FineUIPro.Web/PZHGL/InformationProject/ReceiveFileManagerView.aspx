<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceiveFileManagerView.aspx.cs"
    Inherits="FineUIPro.Web.PZHGL.InformationProject.ReceiveFileManagerView" ValidateRequest="false" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>一般来文管理</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="一般来文管理" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>   
                    <f:TextBox ID="txtFileType" runat="server" Label="类型" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtReceiveFileCode" runat="server" Label="来文编号" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>                    
                    <f:TextBox ID="txtReceiveFileName" runat="server" Label="文件名称" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtGetFileDate" runat="server" Label="收文日期" LabelAlign="Right" Readonly="true"></f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                     <f:TextBox ID="txtFileCode" runat="server" Label="原文编号" LabelAlign="Right" Readonly="true"></f:TextBox>
                     <f:TextBox ID="txtFilePageNum" runat="server" Label="原文页数" LabelAlign="Right" Readonly="true"></f:TextBox>
                </Items>
            </f:FormRow>         
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtVersion" runat="server" Label="版本号" LabelAlign="Right" Readonly="true"></f:TextBox>
                    <f:TextBox ID="drpSendPerson" runat="server" Label="传送人"  LabelAlign="Right" Readonly="true"></f:TextBox>
                </Items>
            </f:FormRow>   
              <f:FormRow>
                <Items>       
                     <f:TextBox ID="drpUnit" runat="server" Label="来文单位" LabelAlign="Right" Readonly="true"></f:TextBox>   
                </Items>
            </f:FormRow> 
            <f:FormRow>
                <Items>
                  <f:TextBox ID="txtUnitNames" runat="server" Label="接收单位" LabelAlign="Right" Readonly="true"></f:TextBox>        
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:HtmlEditor runat="server" Label="来文内容" ID="txtMainContent" ShowLabel="false"
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
                    <f:Button ID="btnAttachUrl" Text="文件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                    </f:Button>
                     <f:Button ID="btnAttachUrl1" Text="回复" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl1_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                    </f:Button>
                    <f:ToolbarFill runat="server"> </f:ToolbarFill>                   
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

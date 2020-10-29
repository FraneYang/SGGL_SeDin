<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeEdit.aspx.cs" Inherits="FineUIPro.Web.Notice.NoticeEdit" ValidateRequest="false" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑通知管理</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="通知" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Layout="VBox">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtNoticeCode" runat="server" Label="编号" LabelAlign="Right"
                        MaxLength="50" Required="true" ShowRedStar="true">
                    </f:TextBox>
                    <f:TextBox ID="txtNoticeTitle" runat="server" Label="标题" Required="true" ShowRedStar="true"
                        LabelAlign="Right" MaxLength="200" FocusOnPageLoad="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>            
            <f:FormRow>
                <Items>
                    <f:HtmlEditor runat="server" Label="内容" ID="txtMainContent" ShowLabel="false"
                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="190px" LabelAlign="Right">
                    </f:HtmlEditor>
                </Items>
            </f:FormRow>
            <f:FormRow runat="server" ID="trProjects">
                <Items>
                    <f:DropDownList ID="drpProjects" runat="server" Label="接收对象" EnableEdit="true" LabelWidth="80px"
                        LabelAlign="right" EnableMultiSelect="true" EnableCheckBoxSelect="true" >
                    </f:DropDownList>
                     </Items>                   
            </f:FormRow>
            <f:FormRow  runat="server" ID="trProjects1" ColumnWidths="86% 7% 7% ">
                <Items>  
                    <f:Label runat="server"></f:Label>                  
                    <f:Button ID="SelectALL" runat="server" Text="全选" OnClick="SelectALL_Click"></f:Button>
                    <f:Button ID="SelectNoALL" runat="server" Text="全不选" 
                        OnClick="SelectNoALL_Click"></f:Button>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DatePicker ID="txtCompileDate" runat="server" Label="整理日期" LabelAlign="Right"
                        EnableEdit="true">
                    </f:DatePicker>
                    <f:DropDownList ID="drpCompileMan" runat="server" Label="整理人" LabelAlign="Right" EnableEdit="true">
                    </f:DropDownList>
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
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                    </f:Button>                              
                    <f:ToolbarFill runat="server"> </f:ToolbarFill>
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
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>  
    </form>
</body>
</html>

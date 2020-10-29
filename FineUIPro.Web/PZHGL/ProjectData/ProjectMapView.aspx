<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectMapView.aspx.cs" Inherits="FineUIPro.Web.PZHGL.ProjectData.ProjectMapView" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑项目地图</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
   <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtType" runat="server" Label="分类" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtTitle" runat="server" Label="图片标题" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
             <f:FormRow ColumnWidths="5% 90%">
                <Items>
                     <f:Label ID="Label1" runat="server" Label="图片"></f:Label>
                     <f:Button ID="btnAttachUrl" Text="上传及查看" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtContentDef" runat="server" Label="简要说明" LabelAlign="Right" Readonly="true">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtUploadDate" runat="server" Label="上传日期" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                     <f:TextBox ID="drpCompileMan" runat="server" Label="整理人" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp">
                    </f:Label>                    
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

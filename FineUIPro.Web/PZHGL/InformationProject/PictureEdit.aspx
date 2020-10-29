<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PictureEdit.aspx.cs" Inherits="FineUIPro.Web.PZHGL.InformationProject.PictureEdit" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑项目图片</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" Title="项目图片"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                     <f:TextBox ID="txtCode" runat="server" Label="编号" LabelAlign="Right"
                        MaxLength="50" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtTitle" runat="server" Label="标题" LabelAlign="Right" MaxLength="50"
                        Required="true" ShowRedStar="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 45% 45%">
                <Items>
                     <f:Label runat="server" Label="图片"></f:Label>
                     <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:Label runat="server" ></f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtContentDef" runat="server" Label="简要说明" LabelAlign="Right" MaxLength="800">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpPictureType" runat="server" Label="图片分类" LabelAlign="Right" EnableEdit="true">                       
                    </f:DropDownList>
                    <f:Label runat="server" ID="lb1"></f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DatePicker ID="txtUploadDate" runat="server" Label="上传日期" LabelAlign="Right" EnableEdit="true">
                    </f:DatePicker>
                    <f:DropDownList ID="drpCompileMan" runat="server" Label="整理人" LabelAlign="Right">
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
                    <f:Label runat="server" ID="lbTemp">
                    </f:Label>                   
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
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>

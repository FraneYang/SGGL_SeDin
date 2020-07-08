<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceiveFileManagerEdit.aspx.cs"
    Inherits="FineUIPro.Web.PZHGL.InformationProject.ReceiveFileManagerEdit" ValidateRequest="false" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑一般来文管理</title>
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
                      <f:RadioButtonList runat="server" ID="rbFileType" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="rbFileType_SelectedIndexChanged">
                        <f:RadioItem Value="0" Text="项目发文" Selected="true"/>
                        <f:RadioItem Value="1" Text="公司来文"/>
                    </f:RadioButtonList>
                    <f:TextBox ID="txtReceiveFileCode" runat="server" Label="来文编号" LabelAlign="Right"
                        MaxLength="50" Readonly="true">
                    </f:TextBox>                 
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtReceiveFileName" runat="server" Label="文件名称" Required="true" ShowRedStar="true"
                        LabelAlign="Right" MaxLength="200" FocusOnPageLoad="true">
                    </f:TextBox>
                      <f:DatePicker ID="txtGetFileDate" runat="server" Label="收文日期" LabelAlign="Right"
                        EnableEdit="true">
                    </f:DatePicker>  
                </Items>
            </f:FormRow>            
            <f:FormRow>
                <Items>
                     <f:TextBox ID="txtFileCode" runat="server" Label="原文编号" LabelAlign="Right" MaxLength="50">
                    </f:TextBox>
                     <f:NumberBox ID="txtFilePageNum" runat="server" Label="原文页数" NoDecimal="true" NoNegative="true">
                    </f:NumberBox>
                </Items>
            </f:FormRow>         
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtVersion" runat="server" Label="版本号" LabelAlign="Right" MaxLength="50">
                    </f:TextBox>
                    <f:DropDownList ID="drpSendPerson" runat="server" Label="传送人" EnableEdit="true"  LabelAlign="Right">
                    </f:DropDownList>                
                </Items>
            </f:FormRow>       
            <f:FormRow>
                <Items>                   
                    <f:DropDownList ID="drpUnit" runat="server" Label="来文单位" EnableEdit="true" LabelAlign="Right">
                    </f:DropDownList>                   
                </Items>
            </f:FormRow> 
            <f:FormRow ColumnWidths="87% 6% 7%">
                <Items>
                   <f:DropDownList ID="drpUnitIds" runat="server" Label="接收单位" EnableEdit="false" 
                        LabelAlign="right" EnableMultiSelect="true" EnableCheckBoxSelect="true" AutoSelectFirstItem="false">
                    </f:DropDownList>
                    <f:Button ID="SelectALL" runat="server" Text="全选" OnClick="SelectALL_Click"></f:Button>
                    <f:Button ID="SelectNoALL" runat="server" Text="全不选" 
                        OnClick="SelectNoALL_Click"></f:Button>               
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
